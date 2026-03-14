using static SdlSharp.Native.Common;
using static SdlSharp.Native.Pixels;

namespace SdlSharp.Graphics;

/// <summary>
/// Details about the format of a pixel. Returned from a shared cache — do not modify.
/// </summary>
public readonly unsafe struct PixelFormatDetails
{
    private readonly Native.SDL_PixelFormatDetails* _handle;

    internal PixelFormatDetails(Native.SDL_PixelFormatDetails* handle)
    {
        _handle = handle;
    }

    /// <summary>
    /// Gets the details for a pixel format.
    /// </summary>
    public static PixelFormatDetails Get(PixelFormat format) =>
        new(Check(SDL_GetPixelFormatDetails((Native.SDL_PixelFormat)format)));

    /// <summary>The pixel format.</summary>
    public PixelFormat Format => (PixelFormat)_handle->format;

    /// <summary>Bits per pixel.</summary>
    public int BitsPerPixel => _handle->bits_per_pixel;

    /// <summary>Bytes per pixel.</summary>
    public int BytesPerPixel => _handle->bytes_per_pixel;

    /// <summary>Red mask.</summary>
    public uint RedMask => _handle->Rmask;

    /// <summary>Green mask.</summary>
    public uint GreenMask => _handle->Gmask;

    /// <summary>Blue mask.</summary>
    public uint BlueMask => _handle->Bmask;

    /// <summary>Alpha mask.</summary>
    public uint AlphaMask => _handle->Amask;

    /// <summary>Red bits.</summary>
    public int RedBits => _handle->Rbits;

    /// <summary>Green bits.</summary>
    public int GreenBits => _handle->Gbits;

    /// <summary>Blue bits.</summary>
    public int BlueBits => _handle->Bbits;

    /// <summary>Alpha bits.</summary>
    public int AlphaBits => _handle->Abits;

    /// <summary>Red shift.</summary>
    public int RedShift => _handle->Rshift;

    /// <summary>Green shift.</summary>
    public int GreenShift => _handle->Gshift;

    /// <summary>Blue shift.</summary>
    public int BlueShift => _handle->Bshift;

    /// <summary>Alpha shift.</summary>
    public int AlphaShift => _handle->Ashift;

    /// <summary>
    /// Maps an RGB color to a pixel value for this format.
    /// </summary>
    public uint MapRgb(byte r, byte g, byte b) =>
        SDL_MapRGB(_handle, null, r, g, b);

    /// <summary>
    /// Maps an RGB color to a pixel value for this format using a palette.
    /// </summary>
    public uint MapRgb(Palette palette, byte r, byte g, byte b) =>
        SDL_MapRGB(_handle, palette.Handle, r, g, b);

    /// <summary>
    /// Maps an RGBA color to a pixel value for this format.
    /// </summary>
    public uint MapRgba(byte r, byte g, byte b, byte a) =>
        SDL_MapRGBA(_handle, null, r, g, b, a);

    /// <summary>
    /// Maps an RGBA color to a pixel value for this format using a palette.
    /// </summary>
    public uint MapRgba(Palette palette, byte r, byte g, byte b, byte a) =>
        SDL_MapRGBA(_handle, palette.Handle, r, g, b, a);

    /// <summary>
    /// Gets RGB values from a pixel value in this format.
    /// </summary>
    public (byte R, byte G, byte B) GetRgb(uint pixel)
    {
        SDL_GetRGB(pixel, _handle, null, out var r, out var g, out var b);
        return (r, g, b);
    }

    /// <summary>
    /// Gets RGB values from a pixel value in this format using a palette.
    /// </summary>
    public (byte R, byte G, byte B) GetRgb(uint pixel, Palette palette)
    {
        SDL_GetRGB(pixel, _handle, palette.Handle, out var r, out var g, out var b);
        return (r, g, b);
    }

    /// <summary>
    /// Gets RGBA values from a pixel value in this format.
    /// </summary>
    public (byte R, byte G, byte B, byte A) GetRgba(uint pixel)
    {
        SDL_GetRGBA(pixel, _handle, null, out var r, out var g, out var b, out var a);
        return (r, g, b, a);
    }

    /// <summary>
    /// Gets RGBA values from a pixel value in this format using a palette.
    /// </summary>
    public (byte R, byte G, byte B, byte A) GetRgba(uint pixel, Palette palette)
    {
        SDL_GetRGBA(pixel, _handle, palette.Handle, out var r, out var g, out var b, out var a);
        return (r, g, b, a);
    }
}
