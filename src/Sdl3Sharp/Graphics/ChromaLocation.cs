namespace Sdl3Sharp.Graphics;

/// <summary>
/// Colorspace chroma sample location.
/// </summary>
public enum ChromaLocation
{
    /// <summary>RGB, no chroma sampling.</summary>
    None = 0,
    /// <summary>MPEG-2, MPEG-4, AVC: Cb and Cr at midpoint of left-edge of 2x2 square.</summary>
    Left = 1,
    /// <summary>JPEG/JFIF, H.261, MPEG-1: Cb and Cr at center of 2x2 square.</summary>
    Center = 2,
    /// <summary>HEVC (BT.2020/BT.2100 on Blu-ray): Cb and Cr co-located with top-left Y pixel.</summary>
    TopLeft = 3
}
