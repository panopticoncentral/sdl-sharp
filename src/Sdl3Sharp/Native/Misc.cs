using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_misc.h - Miscellaneous SDL API functions that don't fit elsewhere.
/// </summary>
public static unsafe partial class Misc
{
    /// <summary>
    /// Open a URL/URI in the browser or other appropriate external application.
    /// </summary>
    /// <param name="url">A valid URL/URI to open. Use <c>file:///full/path/to/file</c> for local files, if supported.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>
    /// <para>Open a URL in a separate, system-provided application. How this works will
    /// vary wildly depending on the platform. This will likely launch what makes
    /// sense to handle a specific URL's protocol (a web browser for <c>http://</c>,
    /// etc), but it might also be able to launch file managers for directories and
    /// other things.</para>
    /// <para>What happens when you open a URL varies wildly as well: your game window
    /// may lose focus (and may or may not lose focus if your game was fullscreen
    /// or grabbing input at the time). On mobile devices, your app will likely
    /// move to the background or your process might be paused. Any given platform
    /// may or may not handle a given URL.</para>
    /// <para>If this is unimplemented (or simply unavailable) for a platform, this will
    /// fail with an error. A successful result does not mean the URL loaded, just
    /// that we launched <i>something</i> to handle it (or at least believe we did).</para>
    /// <para>All this to say: this function can be useful, but you should definitely
    /// test it on every platform you target.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_OpenURL(byte* url);
}
