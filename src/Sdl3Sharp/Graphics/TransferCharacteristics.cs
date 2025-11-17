namespace Sdl3Sharp.Graphics;

/// <summary>
/// Colorspace transfer characteristics.
/// </summary>
public enum TransferCharacteristics
{
    /// <summary>Unknown transfer characteristics.</summary>
    Unknown = 0,
    /// <summary>Rec. ITU-R BT.709-6 / ITU-R BT1361 transfer characteristics.</summary>
    BT709 = 1,
    /// <summary>Unspecified transfer characteristics.</summary>
    Unspecified = 2,
    /// <summary>ITU-R BT.470-6 System M / ITU-R BT1700 625 PAL and SECAM (gamma 2.2).</summary>
    Gamma22 = 4,
    /// <summary>ITU-R BT.470-6 System B, G (gamma 2.8).</summary>
    Gamma28 = 5,
    /// <summary>SMPTE ST 170M / ITU-R BT.601-7 525 or 625 transfer characteristics.</summary>
    BT601 = 6,
    /// <summary>SMPTE ST 240M transfer characteristics.</summary>
    SMPTE240 = 7,
    /// <summary>Linear transfer characteristics.</summary>
    Linear = 8,
    /// <summary>Logarithmic transfer (100:1 range).</summary>
    Log100 = 9,
    /// <summary>Logarithmic transfer (100*sqrt(10):1 range).</summary>
    Log100Sqrt10 = 10,
    /// <summary>IEC 61966-2-4 transfer characteristics.</summary>
    IEC61966 = 11,
    /// <summary>ITU-R BT1361 Extended Colour Gamut transfer characteristics.</summary>
    BT1361 = 12,
    /// <summary>IEC 61966-2-1 (sRGB or sYCC) transfer characteristics.</summary>
    SRGB = 13,
    /// <summary>ITU-R BT2020 for 10-bit system transfer characteristics.</summary>
    BT2020_10Bit = 14,
    /// <summary>ITU-R BT2020 for 12-bit system transfer characteristics.</summary>
    BT2020_12Bit = 15,
    /// <summary>SMPTE ST 2084 for 10-, 12-, 14- and 16-bit systems (Perceptual Quantizer / PQ).</summary>
    PQ = 16,
    /// <summary>SMPTE ST 428-1 transfer characteristics.</summary>
    SMPTE428 = 17,
    /// <summary>ARIB STD-B67 "hybrid log-gamma" (HLG) transfer characteristics.</summary>
    HLG = 18,
    /// <summary>Custom transfer characteristics.</summary>
    Custom = 31
}
