namespace SdlSharp.Graphics;

/// <summary>
/// The operating system's current color theme.
/// </summary>
public enum SystemTheme
{
    /// <summary>The system theme could not be determined.</summary>
    Unknown = (int)Native.SDL_SystemTheme.SDL_SYSTEM_THEME_UNKNOWN,
    /// <summary>A light-colored system theme.</summary>
    Light = (int)Native.SDL_SystemTheme.SDL_SYSTEM_THEME_LIGHT,
    /// <summary>A dark-colored system theme.</summary>
    Dark = (int)Native.SDL_SystemTheme.SDL_SYSTEM_THEME_DARK,
}
