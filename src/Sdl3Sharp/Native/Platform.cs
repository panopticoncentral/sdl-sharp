using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_platform.h - Platform identification API.
/// </summary>
public static unsafe partial class Platform
{
    /// <summary>
    /// Get the name of the platform.
    /// </summary>
    /// <returns>The name of the platform. If the correct platform name is not available, returns a string beginning with the text "Unknown".</returns>
    /// <remarks>
    /// <para>Here are the names returned for some (but not all) supported platforms:</para>
    /// <list type="bullet">
    /// <item><description>"Windows"</description></item>
    /// <item><description>"macOS"</description></item>
    /// <item><description>"Linux"</description></item>
    /// <item><description>"iOS"</description></item>
    /// <item><description>"Android"</description></item>
    /// </list>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* SDL_GetPlatform();
}
