namespace SdlSharp.Graphics;

/// <summary>
/// Display orientation values.
/// </summary>
public enum DisplayOrientation
{
    /// <summary>The display orientation can't be determined.</summary>
    Unknown = (int)Native.SDL_DisplayOrientation.SDL_ORIENTATION_UNKNOWN,
    /// <summary>The display is in landscape mode, with the right side up.</summary>
    Landscape = (int)Native.SDL_DisplayOrientation.SDL_ORIENTATION_LANDSCAPE,
    /// <summary>The display is in landscape mode, with the left side up.</summary>
    LandscapeFlipped = (int)Native.SDL_DisplayOrientation.SDL_ORIENTATION_LANDSCAPE_FLIPPED,
    /// <summary>The display is in portrait mode.</summary>
    Portrait = (int)Native.SDL_DisplayOrientation.SDL_ORIENTATION_PORTRAIT,
    /// <summary>The display is in portrait mode, upside down.</summary>
    PortraitFlipped = (int)Native.SDL_DisplayOrientation.SDL_ORIENTATION_PORTRAIT_FLIPPED,
}
