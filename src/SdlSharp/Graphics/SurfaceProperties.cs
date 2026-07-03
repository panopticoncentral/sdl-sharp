using static SdlSharp.Native.Surface;

namespace SdlSharp.Graphics;

/// <summary>
/// Well-known property names for <see cref="Surface.Properties"/>.
/// </summary>
public static class SurfaceProperties
{
    /// <summary>For HDR10 and floating point surfaces, the value of 100% diffuse white, with higher values being displayed in the High Dynamic Range headroom (float).</summary>
    public const string SdrWhitePoint = SDL_PROP_SURFACE_SDR_WHITE_POINT_FLOAT;

    /// <summary>For HDR10 and floating point surfaces, the maximum dynamic range used by the content, in terms of the SDR white point (float).</summary>
    public const string HdrHeadroom = SDL_PROP_SURFACE_HDR_HEADROOM_FLOAT;

    /// <summary>The tone mapping operator used when compressing from a surface with high dynamic range to another with lower dynamic range (string).</summary>
    public const string TonemapOperator = SDL_PROP_SURFACE_TONEMAP_OPERATOR_STRING;

    /// <summary>The hotspot pixel offset from the left edge of the image, if this surface is being used as a cursor (number).</summary>
    public const string HotspotX = SDL_PROP_SURFACE_HOTSPOT_X_NUMBER;

    /// <summary>The hotspot pixel offset from the top edge of the image, if this surface is being used as a cursor (number).</summary>
    public const string HotspotY = SDL_PROP_SURFACE_HOTSPOT_Y_NUMBER;

    /// <summary>The number of degrees a surface's data is meant to be rotated clockwise to make the image right-side up (float). <see cref="Surface.Rotate"/> sets an adjusted value on the surfaces it returns.</summary>
    public const string Rotation = SDL_PROP_SURFACE_ROTATION_FLOAT;
}
