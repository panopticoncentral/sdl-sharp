namespace Sdl3Sharp.Graphics;

/// <summary>
/// Display orientation values; the way a display is rotated.
/// </summary>
public enum DisplayOrientation
{
    /// <summary>The display orientation can't be determined.</summary>
    Unknown,
    /// <summary>The display is in landscape mode, with the right side up, relative to portrait mode.</summary>
    Landscape,
    /// <summary>The display is in landscape mode, with the left side up, relative to portrait mode.</summary>
    LandscapeFlipped,
    /// <summary>The display is in portrait mode.</summary>
    Portrait,
    /// <summary>The display is in portrait mode, upside down.</summary>
    PortraitFlipped
}
