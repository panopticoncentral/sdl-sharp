namespace SdlSharp.Graphics;

/// <summary>
/// The flip mode used for surface operations.
/// </summary>
public enum FlipMode
{
    /// <summary>Do not flip.</summary>
    None = (int)Native.SDL_FlipMode.SDL_FLIP_NONE,
    /// <summary>Flip horizontally.</summary>
    Horizontal = (int)Native.SDL_FlipMode.SDL_FLIP_HORIZONTAL,
    /// <summary>Flip vertically.</summary>
    Vertical = (int)Native.SDL_FlipMode.SDL_FLIP_VERTICAL,
    /// <summary>Flip horizontally and vertically.</summary>
    HorizontalAndVertical = (int)Native.SDL_FlipMode.SDL_FLIP_HORIZONTAL_AND_VERTICAL,
}
