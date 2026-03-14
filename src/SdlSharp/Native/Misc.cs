using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

/// <summary>
/// Native bindings for SDL_misc.h — miscellaneous utilities.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Misc
{
    /// <summary>
    /// Open a URL/URI in the browser or other appropriate external application.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_OpenURL")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_OpenURL(ReadOnlySpan<byte> url);
}
