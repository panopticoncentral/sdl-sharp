namespace Sdl3Sharp.Graphics;

/// <summary>
/// Colorspace matrix coefficients.
/// </summary>
public enum MatrixCoefficients
{
    /// <summary>Identity matrix (RGB).</summary>
    Identity = 0,
    /// <summary>ITU-R BT.709-6 matrix coefficients.</summary>
    BT709 = 1,
    /// <summary>Unspecified matrix coefficients.</summary>
    Unspecified = 2,
    /// <summary>US FCC Title 47 matrix coefficients.</summary>
    FCC = 4,
    /// <summary>ITU-R BT.470-6 System B, G / ITU-R BT.601-7 625 matrix coefficients (functionally the same as BT601).</summary>
    BT470BG = 5,
    /// <summary>ITU-R BT.601-7 525 matrix coefficients.</summary>
    BT601 = 6,
    /// <summary>SMPTE 240M matrix coefficients.</summary>
    SMPTE240 = 7,
    /// <summary>YCgCo matrix coefficients.</summary>
    YCgCo = 8,
    /// <summary>ITU-R BT.2020-2 non-constant luminance matrix coefficients.</summary>
    BT2020NCL = 9,
    /// <summary>ITU-R BT.2020-2 constant luminance matrix coefficients.</summary>
    BT2020CL = 10,
    /// <summary>SMPTE ST 2085 matrix coefficients.</summary>
    SMPTE2085 = 11,
    /// <summary>Chromaticity-derived non-constant luminance matrix coefficients.</summary>
    ChromaDerivedNCL = 12,
    /// <summary>Chromaticity-derived constant luminance matrix coefficients.</summary>
    ChromaDerivedCL = 13,
    /// <summary>ITU-R BT.2100-0 ICTCP matrix coefficients.</summary>
    ICTCP = 14,
    /// <summary>Custom matrix coefficients.</summary>
    Custom = 31
}
