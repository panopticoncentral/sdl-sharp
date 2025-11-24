using Sdl3Sharp.Graphics;

namespace Sdl3Sharp.Input;

/// <summary>
/// The details of an output format for a camera device.
/// Cameras often support multiple formats; each one will be encapsulated in this struct.
/// </summary>
/// <param name="Format">The pixel format of the frame.</param>
/// <param name="Colorspace">The colorspace of the frame.</param>
/// <param name="Size">The size of the frame in pixels.</param>
/// <param name="FramerateNumerator">Frame rate numerator ((num / denom) == FPS).</param>
/// <param name="FramerateDenominator">Frame rate denominator ((denom / num) == duration in seconds).</param>
public readonly record struct CameraSpec(
    PixelFormat Format,
    Colorspace Colorspace,
    Size Size,
    int FramerateNumerator,
    int FramerateDenominator)
{
    /// <summary>
    /// Gets the frames per second as a floating point value.
    /// </summary>
    public float Fps => FramerateDenominator != 0 ? (float)FramerateNumerator / FramerateDenominator : 0;
}
