namespace SdlSharp.Graphics;

/// <summary>
/// Colorspace definitions.
/// </summary>
public enum Colorspace : uint
{
    /// <summary>Unknown colorspace.</summary>
    Unknown = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_UNKNOWN,
    /// <summary>sRGB (default for 8-bit RGB surfaces).</summary>
    Srgb = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_SRGB,
    /// <summary>Linear sRGB (default for float surfaces).</summary>
    SrgbLinear = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_SRGB_LINEAR,
    /// <summary>HDR10.</summary>
    Hdr10 = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_HDR10,
    /// <summary>JPEG colorspace.</summary>
    Jpeg = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_JPEG,
    /// <summary>BT.601, limited range.</summary>
    Bt601Limited = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_BT601_LIMITED,
    /// <summary>BT.601, full range.</summary>
    Bt601Full = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_BT601_FULL,
    /// <summary>BT.709, limited range.</summary>
    Bt709Limited = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_BT709_LIMITED,
    /// <summary>BT.709, full range.</summary>
    Bt709Full = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_BT709_FULL,
    /// <summary>BT.2020, limited range.</summary>
    Bt2020Limited = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_BT2020_LIMITED,
    /// <summary>BT.2020, full range.</summary>
    Bt2020Full = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_BT2020_FULL,
    /// <summary>Default colorspace for RGB surfaces (alias for Srgb).</summary>
    RgbDefault = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_RGB_DEFAULT,
    /// <summary>Default colorspace for YUV surfaces (alias for Bt601Limited).</summary>
    YuvDefault = (uint)Native.SDL_Colorspace.SDL_COLORSPACE_YUV_DEFAULT,
}
