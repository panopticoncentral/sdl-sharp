using SdlSharp.Native;

namespace SdlSharp.Graphics;

/// <summary>
/// The structure that defines a display mode.
/// </summary>
public readonly record struct DisplayMode(
    uint DisplayId,
    PixelFormat Format,
    int W,
    int H,
    float PixelDensity,
    float RefreshRate,
    int RefreshRateNumerator,
    int RefreshRateDenominator)
{
    internal static unsafe DisplayMode FromNative(SDL_DisplayMode* mode) =>
        new(mode->displayID.Value,
            (PixelFormat)mode->format,
            mode->w,
            mode->h,
            mode->pixel_density,
            mode->refresh_rate,
            mode->refresh_rate_numerator,
            mode->refresh_rate_denominator);
}
