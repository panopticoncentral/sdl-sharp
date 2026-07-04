using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// SDL_version.h's remaining content is compile-time macros — SDL_MAJOR_VERSION,
// SDL_VERSIONNUM*, SDL_VERSION_ATLEAST — which describe the headers used to
// compile SdlSharp, not the loaded library; the runtime need is covered by the
// managed decode of SDL_GetVersion below.

/// <summary>
/// Native bindings for SDL_version.h.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Version
{
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetVersion();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRevision")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetRevision();
}
