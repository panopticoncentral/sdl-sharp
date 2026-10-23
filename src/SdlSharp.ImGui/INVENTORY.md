# Dear ImGui managed API and ownership

Targets Dear ImGui 1.93.0, native ABI 1, and .NET 10. This is an alpha API: ownership and .NET usability take priority over source compatibility.

[API_COVERAGE.json](API_COVERAGE.json) accounts for **all 923 native exports**. Each entry identifies its implementation files or links it to an explicit design decision. [API_DECISIONS.json](API_DECISIONS.json) records the equivalents and intentional exclusions. Run:

```sh
python3 scripts/check-imgui-model-coverage.py
```

A newly generated native export fails this check until it has a managed implementation or a reviewed decision. The generated imports, enums, UTF-8 adapters, and ABI probes come from the restored `ImguiSharp.Redist` package; this repository keeps no generated copy. `ImguiSharpIncludeBindings` opts this assembly into compiling them, and `IGSHARP_EXTERNAL_SDL` selects SdlSharp's SDL handles. Regenerate in `imgui-sharp-native`, publish or locally pack the matching native package, update the package reference here, and restore before running this check. The checker asks MSBuild for `ImguiSharpBindingsDirectory`, so it follows the exact package used by the build. After reviewing changes, update the coverage manifest with `scripts/check-imgui-model-coverage.py --write`.

## Ownership rules

| Surface | Contract |
|---|---|
| `Context` | Explicit, thread-bound owner. `Enter()` restores the previously active managed context. Disposal shuts down children in dependency order. Context switches/destruction during callbacks are rejected. Disposal checks external leases throughout the ownership tree before releasing any frame resource or child; a rejected disposal preserves the usable frame, textures, and callbacks. Internal container/snapshot leases still release automatically during successful teardown. |
| `ImGuiBackend`, `SdlPlatformBackend` | Bound to their creating context; retain SDL window/device/renderer/GL/gamepad owners. Pass the original owning SDL wrappers; borrowed aliases are rejected because they cannot keep native resources alive. Dispose before those SDL resources. Event processing and shutdown activate the correct context. |
| `FontAtlas`, `Font` | Guarded borrowed views. Context disposal, atlas clearing, or font removal invalidates affected views. `OwnedFontAtlas` supports sharing between contexts on one thread. Dispose the contexts before the shared atlas. |
| `DrawList`, `DrawData`, `Viewport`, `FontBaked`, table/multi-select views | Borrowed, context-checked views. Frame views expire at the next managed frame. A standalone list's view expires at the next frame, reset, or disposal; reset the owner before drawing in a new frame. |
| `ClonedDrawList` | Immutable output owner, with geometry copying and managed callback invocation. Retains callbacks, ImGui texture data, typed SDL GPU textures, and the managed GPU backend across frames. Drawing itself also retains owned textures until frame/reset cleanup. No editable `List` property. `Transform` creates another snapshot. |
| `OwnedDrawData`, `TextureList` | Retaining containers. Attached owners cannot be disposed until detached, cleared, or released by container disposal. Owned submissions retain immutable draw lists; geometry-changing operations apply to frame output instead. |
| `OwnedTextureData` | Owns CPU pixels. Its renderer owns GPU allocations. Custom renderers must defer destruction when `IsRetainedForRendering` is true; the bundled managed GPU backend does this automatically. |
| `DrawCmd`, `FontGlyph`, `SelectionRequest`, `TableColumnSortSpecs` | Metadata value copies, safe from vector relocation. Texture references within command metadata remain lifetime-checked; glyph UVs describe the atlas at capture time. |
| `FontConfig`, `TextBuffer`, CPU helper allocations | Deterministic `IDisposable` ownership with SafeHandle fallback for independent native allocations. Font source range copies transfer only on successful loading, and are released on removal/input clearing. |
| `StateStorageScope` | `using (ImGui.UseStateStorage(storage))` temporarily replaces the current window storage. Dispose in reverse nesting order in that same window, before ending it or its frame. The scope retains both storages and its context, then restores and releases them immediately. Scrolling-table and drag-preview window endings reject outstanding storage scopes before changing native state, so restore the scope and retry the end operation. Ordinary tables and drag sources without preview windows may remain inside a host-window storage scope. Replaces the former `SetStateStorage` API. |
| Platform handler registrations | `SetClipboardHandlers`, `SetOpenInShellHandler`, and `SetImeDataHandler` return disposable overrides. Dispose nested overrides in reverse order to restore original callbacks and native user data. Context teardown also restores/releases them. |

Frame-dependent widget APIs require a current managed context and its creating thread. A stale view throws a managed exception before dereferencing native memory. The default value of a view is empty; check `IsValid` where an empty result is expected. Value-copy metadata remains readable independently of its original container.

Use `Context.NewFrame()` for headless or custom backends, and `ImGuiBackend.NewFrame()` for the combined SDL/GPU backend. Raw native calls bypass managed invalidation and lifetime tracking and are internal to the assembly.

Spans over native pixels are short-lived borrows: **do not retain them across mutation, frames, upload, or disposal**. Use `CopyPixels`, `CopyVertices`, or `CopyIndices` for retained managed data. Arbitrary numeric GPU texture IDs are application-owned; keep those GPU resources alive until all submissions using them finish. The managed library cannot infer ownership from an integer ID.

## Typical use

```csharp
using var context = Context.Create();
using var backend = ImGuiBackend.Init(window, device, targetFormat);
using var edit = new TextBuffer("Edit me");

backend.NewFrame();
using (var panel = ImGui.Window("Inspector"))
{
    if (panel.Visible)
    {
        ImGui.InputText("Name", edit);
        using var color = ImGui.ColorScope(Col.Text, 0xff80ffff);
        ImGui.Text(edit.Text);
    }
}
ImGui.Render();
```

Windows and child scopes always call their matching End, including a false Begin result. Table scopes call EndTable only when BeginTable succeeded. ID, font, clipping, and scalar/vector style scopes also balance their stacks. Dispose scopes in nesting order within the same frame.

`InputText` string overloads copy edits back even when flags such as `EnterReturnsTrue` make the native return value false. Reusable `TextBuffer` overloads grow automatically and accept scoped callbacks. `InputTextCallbackData` is a ref struct; raw buffer replacement is internal. Callback cursor/selection positions are UTF-8 byte offsets. Fixed byte spans require a NUL terminator and cannot use resize callbacks. `TextFilter` rejects expressions exceeding its native 255-byte UTF-8 capacity instead of silently truncating them.

Synchronous callback exceptions are rethrown after the native call. Persistent callbacks queue failures; `ImGui.Render`, `TryInvokeCallback`, or `ImGui.ThrowPendingCallbackExceptions()` reports them at a managed boundary. `UnhandledCallbackException` can additionally observe them.

Typed `GpuTexture` and `TextureReference` drawing overloads retain the SDL native identity until frame/list cleanup, and clones carry those leases forward. The bundled GPU backend retains submitted textures, custom pipelines, and itself until command-buffer submission or cancellation. Resources and passes must match the backend device and command buffer; preparation requires an idle command buffer. Swapchain textures cannot be retained by ImGui. Raw numeric texture IDs remain caller-owned.

Compressed font inputs are decoded with checked lengths, backreferences, output size, trailer, checksum, and Base85 alphabet before calling the copied font loader. The default decompressed limit is 64 MiB, adjustable through `maximumDecompressedSize`. Malformed compressed streams throw `InvalidDataException`. Font content still follows the native font loader's format requirements. Empty glyph ranges are a no-op. Plot ring offsets must be nonnegative and within the supplied values (zero for an empty set). Span ID operations enforce the same context/thread rules as string operations.

`Font.CalcTextSize` has overloads returning the consumed prefix length: UTF-16 code units for strings, bytes for UTF-8 spans. `Font.RenderCharacter` accepts an optional clipping rectangle and clips both geometry and UVs.

## Additional renderer and font APIs

- Owned render submissions, typed texture lists, texture upload/status/backend associations, geometry copies, validated indexed meshes, callback kinds/invocation, and sampler/reset commands.
- Independent/shared font atlases, copied font loading and `TryAddFontFromMemoryTTF`, glyph range builders, character/text rendering, cache/output clearing, and checked collection access.
- SDL platform-only initialization for SDL GPU, SDL Renderer, OpenGL, Vulkan, Metal, Direct3D, and application-defined renderers. Gamepad selection, mouse capture, device-object recreation, and texture updates are available on their backend owners.
- Managed `StyleSnapshot` creation/capture/apply, platform session date, clipper user index, selection swapping, UTF-8 ID ranges, `FrameGate`, and BCL vector conversions.

Allocator replacement, C++ font-loader objects, escaping pointers into native vectors/storage, and unmanaged font-memory transfer are intentionally excluded from the ordinary managed API. Complete explanations and safe equivalents are in the coverage decisions; there are no unclassified exports.

## Validation and release status

See [the high-level test instructions](../../tests/SdlSharp.ImGui.Tests/README.md). Tests exercise real native widgets and frame sequences, plus SDL's dummy video driver. The final-review regressions cover malformed compressed streams and Base85 input, empty ranges, plot bounds, span ID thread guards, storage scope restoration, consumed text offsets, character clipping, and typed GPU texture retention through transformed clones and command completion. GPU ownership and backend preconditions use controlled native identities; hardware GPU rendering is not covered by the headless suite.

The referenced `ImguiSharp.Redist` 0.4.0-preview.3 package includes the CloneOutput fix that preserves callback payloads and completed-buffer invariants for debug submission (introduced in 0.4.0-preview.2). A fresh restore from public NuGet was verified on October 22, 2026; this branch no longer depends on an unpublished native companion fix or a sibling checkout. The managed facade remains an alpha API. This work does not publish either managed package; hardware rendering and remote platform validation remain outstanding.
