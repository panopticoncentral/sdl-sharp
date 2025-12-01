using static Sdl3Sharp.Native.Video;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Display orientation values; the way a display is rotated.
/// </summary>
public enum DisplayOrientation
{
    /// <summary>The display orientation can't be determined.</summary>
    Unknown = SDL_DisplayOrientation.SDL_ORIENTATION_UNKNOWN,
    /// <summary>The display is in landscape mode, with the right side up, relative to portrait mode.</summary>
    Landscape = SDL_DisplayOrientation.SDL_ORIENTATION_LANDSCAPE,
    /// <summary>The display is in landscape mode, with the left side up, relative to portrait mode.</summary>
    LandscapeFlipped = SDL_DisplayOrientation.SDL_ORIENTATION_LANDSCAPE_FLIPPED,
    /// <summary>The display is in portrait mode.</summary>
    Portrait = SDL_DisplayOrientation.SDL_ORIENTATION_PORTRAIT,
    /// <summary>The display is in portrait mode, upside down.</summary>
    PortraitFlipped = SDL_DisplayOrientation.SDL_ORIENTATION_PORTRAIT_FLIPPED
}
