using static Sdl3Sharp.Native.Video;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Represents a display mode with resolution, pixel format, and refresh rate information.
/// </summary>
public unsafe readonly record struct DisplayMode
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
    /// Gets the size in pixels.
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
        return $"{Size.Width}x{Size.Height} @ {RefreshRate:F2}Hz";
    }

    /// <summary>
    /// Converts a managed DisplayMode to a native SDL_DisplayMode.
    /// </summary>
    /// <param name="mode">The managed DisplayMode to convert.</param>
    /// <param name="nativeMode">The location to store the native SDL_DisplayMode if not null.</param>
    /// <returns>A pointer to the native SDL_DisplayMode, or null.</returns>
    internal static SDL_DisplayMode* ToNative(DisplayMode? mode, SDL_DisplayMode* nativeMode)
    {
        if (mode == null)
        {
            return null;
        }

        *nativeMode = mode.Value._mode;
        return nativeMode;
    }
}
