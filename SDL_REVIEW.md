# SDL managed API and ownership review

Implemented against SDL 3.4.18 / SdlSharp.Redist 1.0.1 on October 18, 2026. Managed packages remain unpublished.

This implementation addresses the ownership and safety findings from the earlier review and fills the missing managed APIs in the core SDL facade, including all 23 HID and 17 storage functions. Alpha API compatibility was deliberately broken where a disposable acquisition or a checked buffer is safer than a raw pointer plus a separate release call.

## Coverage

The release dependency, Steam gamepad type, Front key/scancode, pen device/state fields, all 266 hint names, 32-bit audio-format ABI, and byte-aligned SDL GUID remain in place. The authoritative inventory is [the generated audit](audit/sdl3-coverage-3.4.18.json); [the checker](scripts/check-sdl-coverage.py) rejects unreviewed declaration, binding, managed-call-site, and hint drift.

| Classification | Functions |
|---|---:|
| Imported and referenced by managed implementation | 899 |
| Deliberately native-only diagnostics/callback getters | 3 |
| Missing wrappers or unclassified core function gaps | 0 |
| Explicit .NET equivalents | 293 |
| Other intentional equivalents or exclusions | 30 |
| Unimplemented HID/storage functions | 0 |
| Platform/bootstrap scope exclusions | 45 |
| Total declared functions | 1,270 |

There are 902 distinct imports. A managed reference means an implementation call site, including internal bridges; it is not a claim of runtime validation on every platform. Inline helpers, macros, and SDL's test library are not native exports.

Raw HID and SDL title/user/local/custom storage are implemented. Cloud-specific behavior still depends on the selected native backend and its host SDK. The separate SDL_image, SDL_mixer, and SDL_ttf libraries have no managed projects here. Platform-specific bootstrap/system/EGL APIs remain outside the existing desktop facade. Standard byte transport, binary primitives, threading, timers, filesystem, and process operations use the .NET BCL. SDL's image/audio/mapping decoders now accept managed streams through an actual SDL IO bridge.

## Ownership and safety implementation

| Earlier finding | Implemented behavior |
|---|---|
| Independent wrappers could use or double-destroy the same native object | Shared native identity tracks owners, borrowed aliases, parent destruction, and leases. SDL reference-counted opens keep their identity alive until the last managed owner closes. Borrowed wrappers have local disposal without independent native destruction. |
| Parents could destroy children still in use | Renderer/texture, window/surface/popup, property views, gamepad/joystick, GPU device/resource, and tray trees share invalidation. Software renderers retain backing surfaces. GL contexts, claimed GPU windows, Vulkan surfaces, and Metal views retain their dependencies. ImGui backend retain hooks also use the common lifetime state. |
| Camera frames could be released through the wrong camera or twice | `Camera.AcquireFrame()` returns `CameraFrame?`, carrying its timestamp and borrowed surface. Disposing the lease returns exactly that acquisition and invalidates the surface. An outstanding frame prevents camera shutdown. Use `frame.Surface.Duplicate()` for an independent image. |
| Locks/maps could unlock freed memory or unlock twice | Reference-based `AudioStreamLock`, `SdlScope`, `PixelLock`, `TextureSurfaceLock`, and `GpuBufferMap` share once-only disposal and retain the native owner. Acquisition and release must occur on the same thread. |
| GPU passes and swapchain textures escaped command-buffer lifetime | Passes retain their command buffer, overlap/submission checks enforce state transitions, and swapchain textures are confined to their acquiring command buffer. Completion invalidates them. Cancellation after successful swapchain acquisition is rejected. Recorded resources are retained until submission/cancellation. Cross-device resource, shader, and fence use is rejected. |
| Short spans could cause native overreads or stack exhaustion | Texture updates validate format, rectangle, pitch, plane sizes, and checked arithmetic; partial locks expose bounded row spans. Geometry validates strides, counts, and indices. Planar audio validates channel count/sample byte lengths. GPU and audio pointer arrays use bounded stack allocation or managed arrays. Pixel conversion checks both buffers and overlap. |
| Clipboard replacement recycled live callback state | Each offer has independent callback state; all returned buffers remain pinned until SDL cleanup. Replacing or failing an offer frees only its own state. Caller-owned clipboard strings and data are copied and freed. |
| Callback exceptions crossed native boundaries | Reverse callbacks catch exceptions. Persistent callback failures are available through `Application.ThrowPendingCallbackException()`. Awaitable dialogs fault their tasks; synchronous main-thread actions rethrow to their caller. Destructive reentrancy from native data callbacks is rejected. |
| Registration cleanup raced with callbacks | Non-reused callback tokens replace recycled GCHandle userdata. A callback obtains a managed strong reference before invoking user code. Replacement installs native state before retiring the prior token; owner cleanup and failed registration retire tokens. Event-watch removal does not hold the managed dictionary lock across native unregistration. |
| Removed tray aliases and descendants remained callable | Entry/menu identities form a lifetime tree. Removal invalidates aliases and descendants and frees descendant callback registrations. Later access fails before P/Invoke. |
| Application disposal quit other sessions or live resources | One active `Application` is allowed. Disposal is idempotent, thread-checked, and rejects outstanding owners, leases, and asynchronous operations. Static events and managed global callbacks are reset at shutdown. Disposing an old application cannot shut down a newer session. |
| Dialog cancellation and failure were indistinguishable | Native errors become faulted tasks or `DialogResult.Error`; cancellation has `IsCanceled`. Task continuations run asynchronously. Pending dialogs retain their parent window, SDL session, filter array and UTF-8 filter strings until completion. Properties-based dialogs accept managed filters and reject unowned filter pointers. |
| Value APIs hid unmanaged pointer hazards or surprising parsing | Pixel-format fields are copied values with safe defaults; mapping an uninitialized format throws. `RawEvent` is a ref struct and can copy descriptive metadata to `EventSnapshot`. GUID Parse/TryParse require exactly 32 hex digits. DateTime conversions preserve ticks, DateTimeOffset represents instants, and unspecified DateTime kinds are rejected. |

Resource wrappers still require deterministic disposal. They deliberately do not invoke thread-affine native APIs from finalizers. Dispose on SDL's required thread; do not race an instance's disposal with another operation or mutate callback registrations concurrently. Concurrent creation/release of independent GPU resources uses thread-safe lifetime collections. GPU command buffers and their passes remain confined to their acquisition thread.

A span already obtained from an unmanaged mapping cannot be revoked by .NET. It must not escape its lease. In particular, do not resize a window or pump events that can replace its surface while accessing window-surface pixels. Prefer copied surfaces for retained work. Raw `nint`/native-handle overloads remain explicit escape hatches: external pointers, Vulkan instances, and custom memory owners must outlive their use. Native-only callers must not destroy objects behind the managed lifetime model.

## Completed managed APIs

- Keyboard/mouse enumeration, instance names, focus views, key/scancode reverse lookup, persistent scancode naming, screen-keyboard visibility, relative mouse mode, and nonportable sensor type queries.
- Primary clipboard selection, hint subscriptions, relative-motion transforms, pointer properties with native cleanup, ICC profiles, and callback-based batch event peeking/removal.
- Managed `Stream` overloads for surface/BMP/PNG and WAV loading, BMP/PNG saving, and gamepad mappings. Streams remain open; non-seekable streams are buffered when codecs require seeking. Managed IO exceptions retain their original exception identity.
- No-copy audio submission accepting `ReadOnlyMemory<byte>` and returning a task that completes when SDL releases the pin, including queue clearing or stream destruction.
- Gamepad mapping/binding snapshots; virtual joystick descriptions, output callbacks, input injection, sensor/touchpad support, scoped joystick locking, and GUID device metadata.
- Bitmap/animated cursors, bounded pixel conversion/colorspace conversion/premultiplication, and pinned managed-memory surfaces.
- GPU capability checks by properties, pixel/GPU format conversion, custom GPU renderer state with retained shader/resource dependencies, Metal renderer handles and owned Metal views, Vulkan loader/extensions/presentation checks and owned surfaces, and Vulkan renderer semaphores.

## HID and storage completion

`SdlSharp.Input.HidApi` owns a matching HID initialization/exit reference. Dispose devices and BLE scan scopes before the session. Concurrent sessions use one discovery thread; each device serializes its I/O and disposal independently. A finite `TimeSpan` read timeout keeps device shutdown responsive. Enumeration and `HidDevice.Info` return detached records, and borrowed property groups invalidate on close. BLE pairing scopes share a reference count and stop the scan only after the last scope closes.

HID report APIs accept spans with explicit lengths. Output and feature reports include their report ID (zero for unnumbered reports); feature/control input methods accept the requested ID separately and initialize `buffer[0]`. Returned byte counts are preserved, timeout/no-data returns zero, and negative results throw. Wide strings are encoded/decoded as native UTF-16 on Windows and UTF-32 elsewhere, including supplementary Unicode characters. Native string output buffers use wide-character counts, not byte counts. Set `HintNames.HidapiEnumerateOnlyControllers` to `"0"` before creating the session when generic HID enumeration is desired; the wrapper preserves SDL's default filtering policy.

```csharp
using var hid = new SdlSharp.Input.HidApi();
using var device = hid.OpenDevice(vendorId, productId);
Span<byte> report = stackalloc byte[64];
int count = device.Read(report, TimeSpan.FromMilliseconds(25));
// Consume report[..count]; disposal closes the device before the session.
```

`SdlSharp.IO.StorageContainer` opens title, user, local and custom storage. Operations are serialized and synchronous; `IsReady` supports polling from an event loop without spinning. Paths are relative, use '/' separators, and reject embedded nulls and '.'/'..' components. Whole-file reads use exact-sized spans or checked byte-array allocation. Enumeration and globs return copied paths; visitor callbacks can stop early and exceptions return to managed callers after SDL unwinds. Title storage preserves read-only behavior, and user storage should be closed promptly after a save batch so cloud backends can flush. Filesystem root paths are native OS paths; paths inside the container use the portable storage rules. Path validation is not a sandbox against symlinks in a local backing directory.

```csharp
using var storage = SdlSharp.IO.StorageContainer.OpenLocal(saveDirectory);
storage.CreateDirectory("saves");
storage.WriteAllBytes("saves/current.bin", saveBytes);
byte[] restored = storage.ReadAllBytes("saves/current.bin");
```

Subclass `StorageBackend` for a managed provider, then call `StorageContainer.Open(backend)`. Ownership transfers after successful creation; `leaveOpen: true` keeps disposal with the caller. An instance can belong to only one open container at a time. Default operations throw `NotSupportedException`, permitting read-only/write-only implementations. Spans and enumeration iterators stay within synchronous callbacks; early termination disposes the iterator. Callback state remains rooted until close and rethrows original exceptions without crossing the unmanaged boundary. Reentering or disposing the container from its callbacks is rejected. SDL frees a container even when close/flush fails, so the wrapper invalidates it and retires callbacks before propagating that failure; later disposal is a no-op. Caller-owned backends may be reused after their container closes. Direct disposal while attached is rejected; backend disposal is idempotent even when cleanup throws, and disposed backends cannot be reopened.

Do not manually dispose a backend while its container is open, or destroy HID/storage handles through the native layer behind the managed model. Deterministic disposal is required; native thread-sensitive shutdown is not delegated to finalizers.

The HID/storage regression suite adds actual local/title storage round trips, custom callbacks through SDL, all callback failure paths, early enumeration termination, ownership transfer, leave-open reuse, close failures, concurrent serialization, native session lifecycle/enumeration, and a device transport fixture for HID reports and strings. Both wchar widths are tested on every host, and the independent C ABI probe covers the platform's actual HID and storage layouts. Tests do not send reports to attached HID hardware. User-backend selection failure is tested without creating real save data. Physical HID transfers, BLE pairing, Steam/cloud synchronization and console storage remain integration-test requirements.

## Intentional alpha API changes

| Previous shape | Replacement |
|---|---|
| `Camera.AcquireFrame(out timestamp)` / `ReleaseFrame(surface)` | `using CameraFrame? frame = camera.AcquireFrame()`; use `TimestampNanoseconds` and `Surface` |
| Texture `Lock(..., out pixels/pitch)` followed by `Unlock()` | `using PixelLock pixels = texture.Lock(rectangle)`; access `GetRowSpan(row)` |
| Texture `LockToSurface()` followed by `Unlock()` | `using TextureSurfaceLock mapping = texture.LockToSurface()`; use `mapping.Surface` |
| Surface or property `Lock()` / `Unlock()` | Dispose the returned pixel lock or scope |
| GPU transfer-buffer `Map()` / `Unmap()` | Dispose the returned `GpuBufferMap`; its `Span` is temporary |
| GPU passes needing a separate `End()` | `IDisposable` passes; `End()` remains available |
| Unchecked GUID decoding through `Parse` | Strict `Parse` and `TryParse`; native permissive decoding stays in `SdlSharp.Native` |
| Copyable raw event records | Callback-limited `RawEvent` plus copied `EventSnapshot` metadata |

## Final ownership review fixes

Managed-memory surfaces keep their pin in a native SDL property cleanup registration. The pin is released only when SDL destroys the final surface reference, including references retained as alternate images by multiple parent surfaces. The `SdlSharp.` property prefix is reserved: public mutations reject it, enumeration hides it, and property copies filter it while preserving SDL's exclusion of cleanup registrations. This prevents property clearing or replacement from releasing a live pin. Disposing the original managed surface no longer releases its backing memory prematurely; callers must also keep any external memory owner alive until those alternate references are released.

Haptics opened from joysticks retain the joystick's shared native lifetime. Repeated opens of the same native haptic retain their dependencies until the last managed native reference closes. Failed opens release their temporary lease. Regression tests cover actual alternate-surface references and compacting GC, pin cleanup exactly once, repeated haptic opens, and failed acquisition. Haptic lifetime tests use an injected native open/close seam and do not exercise physical force-feedback devices.

## Validation

### October 22 follow-up review

The follow-up review corrected pixel buffer sizing for padded 12-/15-bit RGB formats, destructive reentrancy through managed codec streams and audio-memory completion, borrowed-window disposal during callbacks or retained operations, device property ownership, independent opens of previously borrowed native identities, and clipboard text replacement during a data callback. Regression tests cover these cases, and the assertion counters now use atomic increments for concurrent checks.

GPU properties follow their device lifetime. Gamepad properties share the underlying joystick's native property group. Gamepad opens account for SDL's implicit joystick reference, so either kind of independent open keeps that group valid when the other closes. A native identity first observed through a borrowed parent becomes independent when an owning reference is opened without a native parent. Borrowed views still retain their own view-parent validity checks.

The CI branch filters now include `main` and `sdl3`, with manual dispatch available. A clean restore from public NuGet confirmed both dependencies, including `ImguiSharp.Redist` 0.4.0-preview.3 with the native clone fix. See [TODO.md](TODO.md) for the remaining release and platform checks.

The full solution and all samples build with .NET 10. The independent C probe covers 116 concrete structs, nine scalar ID wrappers, enum sizes/values, field offsets/widths, alignment, and callback-interface fields. Every import is checked against the loaded native library.

The SDL regression executable tests native CPU/dummy-video/audio behavior, parent/alias invalidation, lock/map idempotence, partial pixel rows, buffer bounds, callback replacement/failure/reentrancy, UTF-8 clipboard offers, pinned memory across GC, stream round trips and IO failures, virtual joystick input/reference counts, strict values, and application sessions. GPU state, camera-return ownership, and tray-descendant invalidation also have deterministic tests that do not need physical devices or operating-system UI. Those tests do not claim to exercise native GPU submission, camera capture, or tray integration.

The existing ImGui regression suite is retained. CI now uses .NET 10, builds without packing, and runs ABI/coverage/managed tests on Windows and macOS. CI changes are prepared locally; remote runs require pushing the changes.

See [the test instructions](tests/SdlSharp.Tests/README.md) for commands and platform limitations. Local runtime validation is macOS ARM64. Native GPU rendering, physical cameras/haptics, native dialogs/trays, Metal/Vulkan integration, desktop clipboard interaction, and other OS/architecture combinations still require integration runs. No managed package has been published by this work.
