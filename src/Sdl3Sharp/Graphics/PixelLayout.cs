namespace Sdl3Sharp.Graphics;

/// <summary>
/// Packed component layout.
/// </summary>
public enum PixelLayout
{
    /// <summary>No specific packed layout.</summary>
    None,
    /// <summary>Packed layout 332 (3 bits red, 3 bits green, 2 bits blue).</summary>
    Layout332,
    /// <summary>Packed layout 4444 (4 bits per component).</summary>
    Layout4444,
    /// <summary>Packed layout 1555 (1 bit alpha, 5 bits per color component).</summary>
    Layout1555,
    /// <summary>Packed layout 5551 (5 bits per color component, 1 bit alpha).</summary>
    Layout5551,
    /// <summary>Packed layout 565 (5 bits red, 6 bits green, 5 bits blue).</summary>
    Layout565,
    /// <summary>Packed layout 8888 (8 bits per component).</summary>
    Layout8888,
    /// <summary>Packed layout 2101010 (2 bits alpha, 10 bits per color component).</summary>
    Layout2101010,
    /// <summary>Packed layout 1010102 (10 bits per color component, 2 bits alpha).</summary>
    Layout1010102
}
