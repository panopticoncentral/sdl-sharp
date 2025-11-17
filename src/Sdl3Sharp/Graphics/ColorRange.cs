namespace Sdl3Sharp.Graphics;

/// <summary>
/// Colorspace color range, as described by https://www.itu.int/rec/R-REC-BT.2100-2-201807-I/en
/// </summary>
public enum ColorRange
{
    /// <summary>Unknown color range.</summary>
    Unknown = 0,
    /// <summary>Narrow/limited range (e.g., 16-235 for 8-bit RGB and luma, 16-240 for 8-bit chroma).</summary>
    Limited = 1,
    /// <summary>Full range (e.g., 0-255 for 8-bit RGB and luma, 1-255 for 8-bit chroma).</summary>
    Full = 2
}
