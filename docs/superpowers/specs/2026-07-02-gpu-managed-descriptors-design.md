# GPU Managed Descriptor Layer — Design

**Date:** 2026-07-02
**Phase:** 7.2 (TODO.md, "Phases 7–11: SDL3 Surface Completion")
**Problem:** The `SdlSharp.Graphics.Gpu` high-level classes expose `SdlSharp.Native` types
through ~57 public members — descriptor structs (`SDL_GPUShaderCreateInfo`,
`SDL_GPUColorTargetInfo*`, …), raw pointer arrays (`SDL_GPUTexture**`), and swapchain
returns (`out SDL_GPUTexture*`) — violating the architecture rule that native types never
appear in the public API.

## Decisions

1. **API shape:** mirror SDL's structs 1:1 as public C# structs (C# naming, real `bool`s,
   handle-class references instead of pointers, spans instead of pointer+count), plus
   convenience statics for canonical configurations. SDL documentation translates directly.
2. **Breaking change:** the native-type overloads are deleted outright, not kept as an
   escape hatch. Consumers (samples, ImGui backend) migrate in the same change.
3. **Hot path:** per-frame binding structs hold `GpuTexture`/`GpuBuffer` class references;
   each call stackallocs the small native array and converts. Zero GC allocation.

## New public enums (`GpuEnums.cs`)

Appended alongside the existing ten, same `(uint)Native.SDL_GPU…` member-mapping pattern:

`GpuLoadOp`, `GpuStoreOp`, `GpuShaderStage`, `GpuTransferBufferUsage`, `GpuCubeMapFace`,
`GpuVertexElementFormat`, `GpuVertexInputRate`, `GpuFillMode`, `GpuCullMode`,
`GpuFrontFace`, `GpuCompareOp`, `GpuStencilOp`, `GpuBlendOp`, `GpuBlendFactor`,
`GpuFilter`, `GpuSamplerMipmapMode`, `GpuSamplerAddressMode`, `GpuColorComponentFlags`.

`GpuBlendFactor`/`GpuBlendOp` stay distinct from `Graphics.BlendFactor`/`BlendOperation`
(SDL keeps the GPU and renderer blend enums separate; their values differ).

## New public structs

Two files, split by lifecycle.

### `GpuDescriptors.cs` — creation-time (cold path)

Plain `record struct`s with array properties and defaulted fields. `byte` bools become
`bool`; `SDL_PropertiesID props` becomes optional `PropertyGroup? Props`.

| Managed type | Notes vs native |
|---|---|
| `GpuSamplerCreateInfo` | field-for-field |
| `GpuTextureCreateInfo` | field-for-field |
| `GpuBufferCreateInfo` | field-for-field |
| `GpuTransferBufferCreateInfo` | field-for-field |
| `GpuShaderCreateInfo` | `ReadOnlyMemory<byte> Code`; `string EntryPoint = "main"` |
| `GpuComputePipelineCreateInfo` | `ReadOnlyMemory<byte> Code`; `string EntryPoint = "main"` |
| `GpuGraphicsPipelineCreateInfo` | `GpuShader VertexShader/FragmentShader` class refs; nested types below |
| `GpuVertexInputState` | `GpuVertexBufferDescription[]`, `GpuVertexAttribute[]` |
| `GpuVertexBufferDescription`, `GpuVertexAttribute` | field-for-field |
| `GpuRasterizerState`, `GpuMultisampleState` | field-for-field, bools |
| `GpuDepthStencilState` | nested `GpuStencilOpState` |
| `GpuStencilOpState` | field-for-field |
| `GpuColorTargetBlendState` | field-for-field, bools |
| `GpuGraphicsPipelineTargetInfo` | `GpuColorTargetDescription[]`; `GpuTextureFormat? DepthStencilFormat` (nullable replaces `has_depth_stencil_target`) |
| `GpuColorTargetDescription` | nested `GpuColorTargetBlendState` |
| `GpuIndirectDrawCommand`, `GpuIndexedIndirectDrawCommand`, `GpuIndirectDispatchCommand` | layout-compatible blittable mirrors (`[StructLayout(Sequential)]`, plain fields, no conversion) — these are written directly into GPU buffers by user code, so their byte layout must match SDL's exactly |

Convenience statics: `GpuColorTargetBlendState.Disabled` / `.AlphaBlend` /
`.PremultipliedAlpha`; `GpuRasterizerState.Default` (fill, no cull, CCW);
`GpuMultisampleState.None`; `GpuDepthStencilState.Disabled`.

### `GpuBindings.cs` — per-frame (hot path)

`readonly record struct`s holding handle-class references:

- `GpuBufferBinding(GpuBuffer Buffer, uint Offset = 0)`
- `GpuTextureSamplerBinding(GpuTexture Texture, GpuSampler Sampler)`
- `GpuStorageBufferReadWriteBinding`, `GpuStorageTextureReadWriteBinding`
- `GpuColorTargetInfo` — `GpuTexture Texture`, `GpuTexture? ResolveTexture`,
  `FColor ClearColor`, `GpuLoadOp`/`GpuStoreOp`, `bool Cycle`, …
- `GpuDepthStencilTargetInfo`
- `GpuTextureTransferInfo`, `GpuTransferBufferLocation`, `GpuTextureLocation`,
  `GpuTextureRegion`, `GpuBlitRegion`, `GpuBufferLocation`, `GpuBufferRegion`,
  `GpuBlitInfo`, `GpuViewport`

Existing public value types replace native ones in signatures: `Rectangle` for the
scissor `SDL_Rect`, `FColor` for `SDL_FColor`.

## Conversion strategy

Each managed struct gets an `internal ToNative()` method next to its definition.

- **Hot path:** methods take `ReadOnlySpan<T>` and stackalloc the native array, converting
  in a loop — e.g. `BindVertexBuffers(uint firstSlot, ReadOnlySpan<GpuBufferBinding>)`
  stackallocs `SDL_GPUBufferBinding` (16 bytes × count). Pointer-array binds become
  `ReadOnlySpan<GpuTexture>` / `ReadOnlySpan<GpuBuffer>` with stackalloc'd pointer arrays.
- **Cold path:** pipeline creation pins descriptor arrays with `fixed` and stackallocs
  nested arrays; shader code pins the `ReadOnlyMemory<byte>`; entrypoints convert via a
  stackalloc'd null-terminated UTF-8 buffer.
- No `GCHandle`, no heap allocation in either path.
- All `pointer + count` parameter pairs collapse to one span; single-struct parameters
  become `in` managed structs.

## Swapchain acquisition

```csharp
public GpuTexture? AcquireSwapchainTexture(Window window, out Size size)
public GpuTexture? WaitAndAcquireSwapchainTexture(Window window, out Size size)
```

- `Common.Check` on the native bool — `false` is a real error → `SdlException`.
- `true` with a null texture means "skip this frame" → `null` return.
- The returned `GpuTexture` is **non-owning**: `GpuTexture` gains an `_ownsHandle` flag
  (the `Window` pattern); `Dispose` on a swapchain texture never calls
  `SDL_ReleaseGPUTexture`. One small gen0 object per frame; a per-window cached instance
  can be added later without an API change if profiling warrants.

## Native-only functions exposed (audit's 8)

- `GpuFence.WaitAll(GpuDevice, ReadOnlySpan<GpuFence>)` / `WaitAny(…)` —
  `SDL_WaitForGPUFences` with `waitAll` true/false, stackalloc'd pointer array.
- `GpuDevice.Create(PropertyGroup props)` + static class `GpuDeviceProperties` with the
  21 `SDL_PROP_GPU_DEVICE_CREATE_*` names as `const string`.
- `GpuDevice.Properties` — non-owning `PropertyGroup` over `SDL_GetGPUDeviceProperties`.
- `GpuDevice.SupportsShaderFormats(GpuShaderFormat, string? name)` (static).
- `GpuDevice.WindowSupportsPresentMode(Window, GpuPresentMode)`.
- `GpuDevice.WindowSupportsSwapchainComposition(Window, GpuSwapchainComposition)`.
- `GpuTextureFormatExtensions`: `TexelBlockSize()`, `CalculateSize(w, h, depth)`.

## Migration (same change)

- `Samples/ImGuiDemo/Program.cs` — hand-built `SDL_GPUColorTargetInfo` → managed struct;
  new swapchain signature.
- `Samples/GpuInfo/Program.cs` — whatever signatures it touches.
- `src/SdlSharp.ImGui/ImGuiBackend.cs` — if it touches changed signatures.
- `GpuTexture.NativeHandle` (`nuint`) stays: not a `Native`-namespace type, and the ImGui
  `ImTextureID` interop requires it.

## Error handling

Unchanged pattern: `Common.Check` wherever SDL reports failure. `ArgumentException` only
where a bad input would corrupt a stackalloc conversion (mismatched span lengths).

## Verification

1. `dotnet build` clean — 0 warnings, 0 errors.
2. Run `Samples/ImGuiDemo` end-to-end (exercises device creation, swapchain acquire,
   render pass, ImGui GPU backend).
3. Run `Samples/GpuInfo`.
4. Grep gate — acceptance criterion for phase 7.2: no `SDL_`-prefixed type in any
   `public` signature under `src/SdlSharp/Graphics/Gpu/`.
5. Update the `SDL_gpu.h` section of INVENTORY.md (managed-wrapper column for the newly
   exposed functions).
