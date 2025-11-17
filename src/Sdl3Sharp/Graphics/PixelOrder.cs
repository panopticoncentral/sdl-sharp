namespace Sdl3Sharp.Graphics;

/// <summary>
/// Ordering of pixels
/// </summary>
public enum PixelOrder
{
    /// <summary>No specific order.</summary>
    ArrayNone = 0,
    /// <summary>Array order RGB (red, green, blue).</summary>
    ArrayRGB,
    /// <summary>Array order RGBA (red, green, blue, alpha).</summary>
    ArrayRGBA,
    /// <summary>Array order ARGB (alpha, red, green, blue).</summary>
    ArrayARGB,
    /// <summary>Array order BGR (blue, green, red).</summary>
    ArrayBGR,
    /// <summary>Array order BGRA (blue, green, red, alpha).</summary>
    ArrayBGRA,
    /// <summary>Array order ABGR (alpha, blue, green, red).</summary>
    ArrayABGR,

    /// <summary>No specific order.</summary>
    BitmapNone = 0,
    /// <summary>Bitmap order 4321 (high bit to low bit).</summary>
    Bitmap4321,
    /// <summary>Bitmap order 1234 (high bit to low bit).</summary>
    Bitmap1234,

    /// <summary>No specific order.</summary>
    PackedNone = 0,
    /// <summary>Packed order XRGB (unused, red, green, blue).</summary>
    PackedXRGB,
    /// <summary>Packed order RGBX (red, green, blue, unused).</summary>
    PackedRGBX,
    /// <summary>Packed order ARGB (alpha, red, green, blue).</summary>
    PackedARGB,
    /// <summary>Packed order RGBA (red, green, blue, alpha).</summary>
    PackedRGBA,
    /// <summary>Packed order XBGR (unused, blue, green, red).</summary>
    PackedXBGR,
    /// <summary>Packed order BGRX (blue, green, red, unused).</summary>
    PackedBGRX,
    /// <summary>Packed order ABGR (alpha, blue, green, red).</summary>
    PackedABGR,
    /// <summary>Packed order BGRA (blue, green, red, alpha).</summary>
    PackedBGRA
}
