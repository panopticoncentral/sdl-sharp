using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Video;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Provides video subsystem functionality for SDL.
/// </summary>
public static class Video
{
    /// <summary>
    /// Gets a read-only collection of video drivers compiled into SDL.
    /// </summary>
    public static VideoDriverCollection VideoDrivers => VideoDriverCollection.Instance;

    /// <summary>
    /// Gets the name of the currently initialized video driver.
    /// </summary>
    public static string? CurrentVideoDriver => SDL_GetCurrentVideoDriver();

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
