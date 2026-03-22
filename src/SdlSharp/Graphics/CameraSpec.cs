namespace SdlSharp.Graphics;

/// <summary>
/// Camera format and resolution specification.
/// </summary>
public readonly record struct CameraSpec(
    PixelFormat Format,
    Colorspace Colorspace,
    int Width,
    int Height,
    int FramerateNumerator,
    int FramerateDenominator);
