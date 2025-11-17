namespace Sdl3Sharp.Graphics;

/// <summary>
/// Colorspace color primaries, as described by https://www.itu.int/rec/T-REC-H.273-201612-S/en
/// </summary>
public enum ColorPrimaries
{
    /// <summary>Unknown color primaries.</summary>
    Unknown = 0,
    /// <summary>ITU-R BT.709-6 color primaries.</summary>
    BT709 = 1,
    /// <summary>Unspecified color primaries.</summary>
    Unspecified = 2,
    /// <summary>ITU-R BT.470-6 System M color primaries.</summary>
    BT470M = 4,
    /// <summary>ITU-R BT.470-6 System B, G / ITU-R BT.601-7 625 color primaries.</summary>
    BT470BG = 5,
    /// <summary>ITU-R BT.601-7 525, SMPTE 170M color primaries.</summary>
    BT601 = 6,
    /// <summary>SMPTE 240M color primaries (functionally the same as BT601).</summary>
    SMPTE240 = 7,
    /// <summary>Generic film color primaries (color filters using Illuminant C).</summary>
    GenericFilm = 8,
    /// <summary>ITU-R BT.2020-2 / ITU-R BT.2100-0 color primaries.</summary>
    BT2020 = 9,
    /// <summary>SMPTE ST 428-1 color primaries (CIE 1931 XYZ).</summary>
    XYZ = 10,
    /// <summary>SMPTE RP 431-2 color primaries.</summary>
    SMPTE431 = 11,
    /// <summary>SMPTE EG 432-1 / DCI P3 color primaries.</summary>
    SMPTE432 = 12,
    /// <summary>EBU Tech. 3213-E color primaries.</summary>
    EBU3213 = 22,
    /// <summary>Custom color primaries.</summary>
    Custom = 31
}
