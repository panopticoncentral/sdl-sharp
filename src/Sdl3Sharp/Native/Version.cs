using System.Runtime.InteropServices;
using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_version.h - SDL version query APIs.
/// </summary>
public static unsafe partial class Version
{
    /// <summary>
    /// The current major version of SDL headers.
    /// If this were SDL version 3.2.1, this value would be 3.
    /// </summary>
    public const int SDL_MAJOR_VERSION = 3;

    /// <summary>
    /// The current minor version of the SDL headers.
    /// If this were SDL version 3.2.1, this value would be 2.
    /// </summary>
    public const int SDL_MINOR_VERSION = 2;

    /// <summary>
    /// The current micro (or patchlevel) version of the SDL headers.
    /// If this were SDL version 3.2.1, this value would be 1.
    /// </summary>
    public const int SDL_MICRO_VERSION = 26;

    /// <summary>
    /// This turns the version numbers into a numeric value.
    /// (1,2,3) becomes 1002003.
    /// </summary>
    /// <param name="major">the major version number.</param>
    /// <param name="minor">the minor version number.</param>
    /// <param name="patch">the patch version number.</param>
    /// <returns>A numeric version value.</returns>
    public static int SDL_VERSIONNUM(int major, int minor, int patch)
    {
        return (major * 1000000) + (minor * 1000) + patch;
    }

    /// <summary>
    /// This extracts the major version from a version number.
    /// 1002003 becomes 1.
    /// </summary>
    /// <param name="version">the version number.</param>
    /// <returns>The major version.</returns>
    public static int SDL_VERSIONNUM_MAJOR(int version)
    {
        return version / 1000000;
    }

    /// <summary>
    /// This extracts the minor version from a version number.
    /// 1002003 becomes 2.
    /// </summary>
    /// <param name="version">the version number.</param>
    /// <returns>The minor version.</returns>
    public static int SDL_VERSIONNUM_MINOR(int version)
    {
        return version / 1000 % 1000;
    }

    /// <summary>
    /// This extracts the micro version from a version number.
    /// 1002003 becomes 3.
    /// </summary>
    /// <param name="version">the version number.</param>
    /// <returns>The micro version.</returns>
    public static int SDL_VERSIONNUM_MICRO(int version)
    {
        return version % 1000;
    }

    /// <summary>
    /// This is the version number value for the current SDL version.
    /// </summary>
    public static readonly int SDL_VERSION = SDL_VERSIONNUM(SDL_MAJOR_VERSION, SDL_MINOR_VERSION, SDL_MICRO_VERSION);

    /// <summary>
    /// This evaluates to true if compiled with SDL at least X.Y.Z.
    /// </summary>
    /// <param name="X">major version.</param>
    /// <param name="Y">minor version.</param>
    /// <param name="Z">micro version.</param>
    /// <returns>true if SDL version is at least X.Y.Z.</returns>
    public static bool SDL_VERSION_ATLEAST(int X, int Y, int Z)
    {
        return SDL_VERSION >= SDL_VERSIONNUM(X, Y, Z);
    }

    /// <summary>
    /// Get the version of SDL that is linked against your program.
    /// </summary>
    /// <returns>the version of the linked library.</returns>
    /// <remarks>
    /// <para>If you are linking to SDL dynamically, then it is possible that the current
    /// version will be different than the version you compiled against. This
    /// function returns the current version, while SDL_VERSION is the version you
    /// compiled with.</para>
    /// <para>This function may be called safely at any time, even before SDL_Init().</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial int SDL_GetVersion();

    /// <summary>
    /// Get the code revision of SDL that is linked against your program.
    /// </summary>
    /// <returns>an arbitrary string, uniquely identifying the exact revision of the SDL library in use. This string is owned by SDL and should NOT be freed.</returns>
    /// <remarks>
    /// <para>This value is the revision of the code you are linked with and may be
    /// different from the code you are compiling with, which is found in the
    /// constant SDL_REVISION.</para>
    /// <para>The revision is arbitrary string (a hash value) uniquely identifying the
    /// exact revision of the SDL library in use, and is only useful in comparing
    /// against other revisions. It is NOT an incrementing number.</para>
    /// <para>If SDL wasn't built from a git repository with the appropriate tools, this
    /// will return an empty string.</para>
    /// <para>You shouldn't use this function for anything but logging it for debugging
    /// purposes. The string is not intended to be reliable in any way.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    public static partial byte* SDL_GetRevision();
}
