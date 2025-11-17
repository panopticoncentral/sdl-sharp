namespace Sdl3Sharp.Graphics;

/// <summary>
/// Pixel type.
/// </summary>
public enum PixelType
{
    /// <summary>Unknown pixel type.</summary>
    Unknown,
    /// <summary>1-bit indexed pixel type.</summary>
    Index1,
    /// <summary>4-bit indexed pixel type.</summary>
    Index4,
    /// <summary>8-bit indexed pixel type.</summary>
    Index8,
    /// <summary>8-bit packed pixel type.</summary>
    Packed8,
    /// <summary>16-bit packed pixel type.</summary>
    Packed16,
    /// <summary>32-bit packed pixel type.</summary>
    Packed32,
    /// <summary>8-bit array pixel type.</summary>
    ArrayU8,
    /// <summary>16-bit array pixel type.</summary>
    ArrayU16,
    /// <summary>32-bit array pixel type.</summary>
    ArrayU32,
    /// <summary>16-bit floating point array pixel type.</summary>
    ArrayF16,
    /// <summary>32-bit floating point array pixel type.</summary>
    ArrayF32,
    /// <summary>2-bit indexed pixel type (appended for SDL2 compatibility).</summary>
    Index2
}
