using static Sdl3Sharp.Native.Video;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Represents a display mode with resolution, pixel format, and refresh rate information.
/// </summary>
public sealed unsafe class DisplayMode
{
    private readonly SDL_DisplayMode _mode;

    internal DisplayMode(SDL_DisplayMode* mode)
    {
        if (mode == null)
        {
            throw new ArgumentNullException(nameof(mode));
        }

        _mode = *mode;
    }

    internal DisplayMode(SDL_DisplayMode mode)
    {
        _mode = mode;
    }

    /// <summary>
    /// Gets the display ID this mode is associated with.
    /// </summary>
    public uint DisplayID => _mode.displayID;

    /// <summary>
    /// Gets the pixel format.
    /// </summary>
    public PixelFormat PixelFormat => new(_mode.format);

    /// <summary>
    /// Gets the width in pixels.
    /// </summary>
    public int Width => _mode.w;

    /// <summary>
    /// Gets the height in pixels.
    /// </summary>
    public int Height => _mode.h;

    /// <summary>
    /// Gets the size of this display mode.
    /// </summary>
    public Size Size => new(_mode.w, _mode.h);

    /// <summary>
    /// Gets the scale converting size to pixels (e.g. a 1920x1080 mode with 2.0 scale would have 3840x2160 pixels).
    /// </summary>
    public float PixelDensity => _mode.pixel_density;

    /// <summary>
    /// Gets the refresh rate in Hz (or 0.0f for unspecified).
    /// </summary>
    public float RefreshRate => _mode.refresh_rate;

    /// <summary>
    /// Gets the precise refresh rate numerator (or 0 for unspecified).
    /// </summary>
    public int RefreshRateNumerator => _mode.refresh_rate_numerator;

    /// <summary>
    /// Gets the precise refresh rate denominator.
    /// </summary>
    public int RefreshRateDenominator => _mode.refresh_rate_denominator;

    /// <summary>
    /// Returns a string representation of this display mode.
    /// </summary>
    public override string ToString()
    {
        return $"{Width}x{Height} @ {RefreshRate:F2}Hz";
    }

    internal SDL_DisplayMode ToNative()
    {
        return _mode;
    }
}
