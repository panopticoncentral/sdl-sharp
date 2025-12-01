using System.Runtime.InteropServices;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Video;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Provides video subsystem functionality for SDL.
/// </summary>
public unsafe static class Video
{
    /// <summary>
    /// Enumerates the video drivers compiled into SDL.
    /// </summary>
    /// <returns>An enumerable of video driver names.</returns>
    public static string[] Drivers()
    {
        var count = SDL_GetNumVideoDrivers();
        var drivers = new string[count];
        for (var i = 0; i < count; i++)
        {
            drivers[i] = Marshal.PtrToStringUTF8((nint)SDL_GetVideoDriver(i))!;
        }

        return drivers;
    }

    /// <summary>
    /// Gets the name of the currently initialized video driver.
    /// </summary>
    public static string? CurrentDriver => Marshal.PtrToStringUTF8((nint)SDL_GetCurrentVideoDriver());

    /// <summary>
    /// Gets the current system theme.
    /// </summary>
    public static SystemTheme SystemTheme => (SystemTheme)SDL_GetSystemTheme();

    /// <summary>
    /// Gets or sets whether the screensaver is enabled.
    /// </summary>
    public static bool ScreenSaverEnabled
    {
        get => SDL_ScreenSaverEnabled(); 
        set => _ = value ? CheckErrorBool(SDL_EnableScreenSaver()) : CheckErrorBool(SDL_DisableScreenSaver());
    }
}
