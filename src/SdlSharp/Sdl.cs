using System.Runtime.InteropServices;

using static SdlSharp.Native.Version;

namespace SdlSharp;

/// <summary>
/// Information about the linked SDL library.
/// </summary>
public static unsafe class Sdl
{
    /// <summary>
    /// Gets the version of the SDL library in use (decoded from SDL's
    /// major*1000000 + minor*1000 + micro encoding).
    /// </summary>
    public static System.Version Version
    {
        get
        {
            var v = SDL_GetVersion();
            return new System.Version(v / 1000000, v / 1000 % 1000, v % 1000);
        }
    }

    /// <summary>Gets the source revision of the SDL library in use, or null if unavailable.</summary>
    public static string? Revision => Marshal.PtrToStringUTF8((nint)SDL_GetRevision());
}
