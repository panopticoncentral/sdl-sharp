# SdlSharp — SDL3 C# Wrapper

Wrapping SDL 3.4.18 for C# on .NET 10.0, supplied by SdlSharp.Redist 1.0.1. Use the release headers from the redist SDK (or the matching upstream release) for coverage and ABI checks.

## Architecture Rules

Follow these patterns when adding new wrappers.

### Native layer (`SdlSharp.Native`)

- **`[LibraryImport]`** with `[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]` — not `[DllImport]`
- **`[return: MarshalAs(UnmanagedType.U1)]`** on all bool-returning functions
- **Skipped APIs get comments**: When skipping a function/type/callback from an SDL header, add a comment in the native wrapper file explaining what was skipped and why (see `Init.cs` app callbacks comment for the pattern)
- **`ReadOnlySpan<byte>`** for all native string input params — no automatic marshalling. String constants as `static ReadOnlySpan<byte> Foo => "..."u8;`
- **`byte*` return** for C strings returned by SDL (converted via `Marshal.PtrToStringUTF8` in high-level wrappers)
- **Separate static classes per SDL header** in `SdlSharp.Native` namespace (e.g., `Init`, `Error`, `Properties`)
- **Opaque pointer types** as empty structs: `public struct SDL_Window;` for type safety
- **Callbacks**: `[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]` static methods + non-reused `CallbackHandle` tokens for userdata; contain all exceptions and use `CallbackErrors` or task completion to report them
- **No XML docs required**: CS1591 is suppressed for this layer via `src/SdlSharp/Native/.editorconfig` — the SDL headers are the documentation. Doc comments are welcome but optional here.

### High-level wrappers (`SdlSharp`, `SdlSharp.Graphics`, etc.)

- **XML docs required**: every public type and member gets a `///` doc comment (`GenerateDocumentationFile` is on; CS1591 must stay at zero)
- **`Common.ToUtf8(string?)`** converts `string?` to null-terminated UTF-8 `byte[]?` for passing to native layer (null input → null → null pointer)
- **Handle wrappers**: `sealed unsafe class` with `IDisposable`, internal guarded `Handle`/`Id`, and shared `NativeLifetime` identity. Track real native parent destruction separately from retained dependencies. Borrowed views do not destroy native state.
- **Use-after-dispose guard**: check both the wrapper's local disposed state and `NativeLifetime.Check()`. Register parent/child relationships and leases before exposing native resources. Dispose through `Lifetime.Release` so aliases and native children invalidate together; preserve SDL's reference counts for repeated opens. Never rely on a borrowed owner's wrapper-local disposed bit to determine native lifetime.
- **Acquisition scopes**: locks, maps, camera frames, GPU passes, and callback subscriptions have shared once-only release state. Retain dependencies until native completion and check required thread affinity. Do not call thread-affine SDL destruction from finalizers.
- **Bounds and callbacks**: validate spans, strides, counts, and checked byte lengths before P/Invoke; bound stack allocation. Native callbacks must not leak exceptions or recycled userdata addresses. Async task continuations must not run inside native callbacks.
- **Coverage and tests**: update `audit/sdl3-coverage-3.4.18.json`, regenerate the native probe, and run `scripts/test-sdl.py` plus the ImGui tests. Consult `SDL_REVIEW.md` for current contracts and intentional exclusions. Build with `-p:GeneratePackageOnBuild=false`; publishing managed packages requires a separate explicit request.
- **Error checking**: `Common.Check(bool)`, `Common.Check<T>(T*)`, `Common.CheckId(uint)` → throw `SdlException`
- **Value types**: `readonly record struct` (Color, Point, Rectangle, etc.)
- **Event args**: `readonly record struct` (not classes) to avoid GC pressure
- **No native types in public API**: High-level wrappers must never expose `SdlSharp.Native` types. Wrap native enums/structs/IDs with public equivalents in the `SdlSharp` namespace (e.g., `SDL_InitFlags` → `InitFlags`, `SDL_PropertyType` → `PropertyType`). Cast between them internally. When wrapping enums, express outer members in terms of inner members:
  ```csharp
  // Native enum — preserve original C names:
  public enum SDL_InitFlags : uint { SDL_INIT_AUDIO = 0x00000010u, ... }

  // Outer enum — clean C# names, referencing native:
  public enum InitFlags : uint { Audio = (uint)Native.SDL_InitFlags.SDL_INIT_AUDIO, ... }
  ```

### HID and storage rules

- HID native `wchar_t` pointers use explicit UTF-16 on Windows and UTF-32 on Unix. Never marshal them unconditionally as .NET `char*` or `LPWStr`.
- `HidApi` owns initialization references and discovery thread affinity; devices and BLE scopes retain that session. Device report operations use spans and serialize against disposal.
- `StorageContainer` owns SDL storage. Custom `StorageBackend` callbacks are synchronous and must contain exceptions. Ownership transfers only after successful open, with explicit `leaveOpen` support. A failed native close still frees the container: invalidate before rethrowing.
- HID/storage regression tests must not send reports to attached hardware or modify real user/cloud saves. Use the internal HID transport fixture and temporary/custom storage providers.

### Namespaces

- **Root**: `SdlSharp` (not Sdl3Sharp)
- **Sub-namespaces**: `SdlSharp.Graphics`, `SdlSharp.Input`, `SdlSharp.Audio`, `SdlSharp.Graphics.Gpu`, `SdlSharp.IO`

## Code Examples

### Adding a new native binding file

```csharp
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Native;

public static partial class Video  // one class per SDL header
{
    public struct SDL_Window;  // opaque type marker

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window* SDL_CreateWindow(ReadOnlySpan<byte> title, int w, int h, ulong flags);
}
```

### Adding a new handle wrapper

```csharp
using static SdlSharp.Native.Video;

namespace SdlSharp.Graphics;

public sealed unsafe class Window : IDisposable
{
    private readonly bool _ownsHandle;
    internal readonly NativeLifetime Lifetime;
    private readonly NativeLifetime? _viewParent;

    internal SDL_Window* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            Lifetime.Check();
            _viewParent?.Check();
            return _handle;
        }
    }

    private SDL_Window* _handle;

    internal Window(SDL_Window* handle, bool ownsHandle = true, NativeLifetime? parent = null)
    {
        _handle = handle;
        Lifetime = NativeLifetime.Get(typeof(Window), (nint)handle, ownsHandle,
            threadAffine: true, parent: parent);
        _ownsHandle = ownsHandle;
        _viewParent = ownsHandle ? null : parent;
    }

    public static Window Create(string title, int w, int h, WindowFlags flags = WindowFlags.None) =>
        new(Common.Check(SDL_CreateWindow(Common.ToUtf8(title), w, h, (SDL_WindowFlags)flags)));

    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            Lifetime.Release(() => SDL_DestroyWindow(_handle));
        }
        _handle = null;
    }
}
```

### String returns from SDL

```csharp
// SDL-owned string (don't free):
public static unsafe string? GetError() =>
    Marshal.PtrToStringUTF8((nint)Error.SDL_GetError());

// Caller-owned string (must free — rare in SDL3):
// Convert then call SDL_free()
```

## Reference

- **Upstream SDL3 headers**: release 3.4.18, available in the matching `../sdl-sharp-redist/` extracted SDK. Pass its include path to the coverage/probe scripts.
- **SDL3 redist packaging**: `../sdl-sharp-redist/`
- **Old SDL2 wrappers**: `git show sdl2:src/SdlSharp/` (for reference, not to copy)
- **SDL3 first draft**: `git show sdl3-first-draft:src/SdlSharp/` (for reference)
- **Task tracking**: `TODO.md`; current ownership/API review and platform validation limits: `SDL_REVIEW.md`.
