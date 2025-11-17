namespace Sdl3Sharp.Graphics;

/// <summary>
/// Colorspace color type.
/// </summary>
public enum ColorType
{
    /// <summary>Unknown color type.</summary>
    Unknown = 0,
    /// <summary>RGB color type (red, green, blue channels).</summary>
    Rgb = 1,
    /// <summary>YCbCr (YUV) color type (luma and chroma channels).</summary>
    YCbCr = 2
}
