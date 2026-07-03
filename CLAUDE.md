# SdlSharp — SDL3 C# Wrapper

Wrapping SDL 3.4.2 (at `../SDL`) for C# on .NET 10.0. Upstream headers are at `../SDL/include/SDL3/`.

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
- **Callbacks**: `[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]` static methods + `GCHandle` for userdata
- **No XML docs required**: CS1591 is suppressed for this layer via `src/SdlSharp/Native/.editorconfig` — the SDL headers are the documentation. Doc comments are welcome but optional here.

### High-level wrappers (`SdlSharp`, `SdlSharp.Graphics`, etc.)

- **XML docs required**: every public type and member gets a `///` doc comment (`GenerateDocumentationFile` is on; CS1591 must stay at zero)
- **`Common.ToUtf8(string?)`** converts `string?` to null-terminated UTF-8 `byte[]?` for passing to native layer (null input → null → null pointer)
- **Handle wrappers**: `sealed unsafe class` with `IDisposable`, raw pointer `Handle` property, `_ownsHandle` flag
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

### Namespaces

- **Root**: `SdlSharp` (not Sdl3Sharp)
- **Sub-namespaces**: `SdlSharp.Graphics`, `SdlSharp.Input`, `SdlSharp.Audio`, `SdlSharp.Graphics.Gpu`

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
    internal SDL_Window* Handle { get; private set; }

    internal Window(SDL_Window* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    public static Window Create(string title, int w, int h, ulong flags = 0) =>
        new(Common.Check(SDL_CreateWindow(Common.ToUtf8(title), w, h, flags)));

    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_DestroyWindow(Handle);
            Handle = null;
        }
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

- **Upstream SDL3 headers**: `../SDL/include/SDL3/` (86 header files)
- **SDL3 redist packaging**: `../sdl-sharp-redist/`
- **Old SDL2 wrappers**: `git show HEAD~1:src/SdlSharp/` (for reference, not to copy)
- **SDL3 first draft**: `git show sdl3-first-draft:src/SdlSharp/` (for reference)
- **Task tracking**: `TODO.md`
