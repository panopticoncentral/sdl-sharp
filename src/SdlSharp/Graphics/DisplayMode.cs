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

    /// <summary>
    /// Converts this mode to its native representation. The native struct's
    /// internal driver data pointer is left null; modes intended for
    /// exclusive-fullscreen matching (e.g. via <see cref="Window.SetFullscreenMode"/>)
    /// should originate from <see cref="Display.GetFullscreenModes"/> rather than
    /// being constructed by hand, so SDL can match them against a real mode.
    /// </summary>
    internal SDL_DisplayMode ToNative() =>
        new()
        {
            displayID = new SDL_DisplayID(DisplayId),
            format = (Native.SDL_PixelFormat)Format,
            w = W,
            h = H,
            pixel_density = PixelDensity,
            refresh_rate = RefreshRate,
            refresh_rate_numerator = RefreshRateNumerator,
            refresh_rate_denominator = RefreshRateDenominator,
            @internal = 0,
        };
}
