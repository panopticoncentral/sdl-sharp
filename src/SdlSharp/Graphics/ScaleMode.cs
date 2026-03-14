namespace SdlSharp.Graphics;

/// <summary>
/// The scaling mode used for surface operations.
/// </summary>
public enum ScaleMode
{
    /// <summary>Nearest pixel sampling.</summary>
    Nearest = (int)Native.SDL_ScaleMode.SDL_SCALEMODE_NEAREST,
    /// <summary>Linear filtering.</summary>
    Linear = (int)Native.SDL_ScaleMode.SDL_SCALEMODE_LINEAR,
    /// <summary>Nearest pixel sampling with improved scaling for pixel art.</summary>
    PixelArt = (int)Native.SDL_ScaleMode.SDL_SCALEMODE_PIXELART,
}
