using static Sdl3Sharp.Native.Video;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// System theme (light or dark mode).
/// </summary>
public enum SystemTheme
{
    /// <summary>Unknown system theme.</summary>
    Unknown = SDL_SystemTheme.SDL_SYSTEM_THEME_UNKNOWN,
    /// <summary>Light colored system theme.</summary>
    Light = SDL_SystemTheme.SDL_SYSTEM_THEME_LIGHT,
    /// <summary>Dark colored system theme.</summary>
    Dark = SDL_SystemTheme.SDL_SYSTEM_THEME_DARK
}
