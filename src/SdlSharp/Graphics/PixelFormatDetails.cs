using static SdlSharp.Native.Common;
using static SdlSharp.Native.Pixels;

namespace SdlSharp.Graphics;

/// <summary>
/// Details about the format of a pixel. Returned from a shared cache — do not modify.
/// </summary>
public readonly unsafe struct PixelFormatDetails
{
    private readonly Native.SDL_PixelFormatDetails _value;
    private Native.SDL_PixelFormatDetails* Handle => _value.format == 0 ? throw new InvalidOperationException("Initialize pixel format details before mapping colors.") : Check(SDL_GetPixelFormatDetails(_value.format));

    internal PixelFormatDetails(Native.SDL_PixelFormatDetails* handle)
    {
        _value = *handle;
    }

    /// <summary>
    /// Gets the details for a pixel format.
    /// </summary>
    public static PixelFormatDetails Get(PixelFormat format) =>
        new(Check(SDL_GetPixelFormatDetails((Native.SDL_PixelFormat)format)));

    /// <summary>The pixel format.</summary>
    public PixelFormat Format => (PixelFormat)_value.format;

    /// <summary>Bits per pixel.</summary>
    public int BitsPerPixel => _value.bits_per_pixel;

    /// <summary>Bytes per pixel.</summary>
    public int BytesPerPixel => _value.bytes_per_pixel;

    // Color bits can exclude unused storage bits (for example XRGB4444 and
    // XRGB1555). Only indexed sub-byte formats pack multiple pixels per byte.
    internal int GetRowByteCount(int width) => BitsPerPixel >= 8
        ? checked(width * BytesPerPixel)
        : checked((int)(((long)width * BitsPerPixel + 7) / 8));

    /// <summary>Red mask.</summary>
    public uint RedMask => _value.Rmask;

    /// <summary>Green mask.</summary>
    public uint GreenMask => _value.Gmask;

    /// <summary>Blue mask.</summary>
    public uint BlueMask => _value.Bmask;

    /// <summary>Alpha mask.</summary>
    public uint AlphaMask => _value.Amask;

    /// <summary>Red bits.</summary>
    public int RedBits => _value.Rbits;

    /// <summary>Green bits.</summary>
    public int GreenBits => _value.Gbits;

    /// <summary>Blue bits.</summary>
    public int BlueBits => _value.Bbits;

    /// <summary>Alpha bits.</summary>
    public int AlphaBits => _value.Abits;

    /// <summary>Red shift.</summary>
    public int RedShift => _value.Rshift;

    /// <summary>Green shift.</summary>
    public int GreenShift => _value.Gshift;

    /// <summary>Blue shift.</summary>
    public int BlueShift => _value.Bshift;

    /// <summary>Alpha shift.</summary>
    public int AlphaShift => _value.Ashift;

    /// <summary>
    /// Maps an RGB color to a pixel value for this format.
    /// </summary>
    public uint MapRgb(byte r, byte g, byte b) =>
        SDL_MapRGB(Handle, null, r, g, b);

    /// <summary>
    /// Maps an RGB color to a pixel value for this format using a palette.
    /// </summary>
    public uint MapRgb(Palette palette, byte r, byte g, byte b) =>
        SDL_MapRGB(Handle, palette.Handle, r, g, b);

    /// <summary>
    /// Maps an RGBA color to a pixel value for this format.
    /// </summary>
    public uint MapRgba(byte r, byte g, byte b, byte a) =>
        SDL_MapRGBA(Handle, null, r, g, b, a);

    /// <summary>
    /// Maps an RGBA color to a pixel value for this format using a palette.
    /// </summary>
    public uint MapRgba(Palette palette, byte r, byte g, byte b, byte a) =>
        SDL_MapRGBA(Handle, palette.Handle, r, g, b, a);

    /// <summary>
    /// Gets RGB values from a pixel value in this format.
    /// </summary>
    public (byte R, byte G, byte B) GetRgb(uint pixel)
    {
        SDL_GetRGB(pixel, Handle, null, out var r, out var g, out var b);
        return (r, g, b);
    }

    /// <summary>
    /// Gets RGB values from a pixel value in this format using a palette.
    /// </summary>
    public (byte R, byte G, byte B) GetRgb(uint pixel, Palette palette)
    {
        SDL_GetRGB(pixel, Handle, palette.Handle, out var r, out var g, out var b);
        return (r, g, b);
    }

    /// <summary>
    /// Gets RGBA values from a pixel value in this format.
    /// </summary>
    public (byte R, byte G, byte B, byte A) GetRgba(uint pixel)
    {
        SDL_GetRGBA(pixel, Handle, null, out var r, out var g, out var b, out var a);
        return (r, g, b, a);
    }

    /// <summary>
    /// Gets RGBA values from a pixel value in this format using a palette.
    /// </summary>
    public (byte R, byte G, byte B, byte A) GetRgba(uint pixel, Palette palette)
    {
        SDL_GetRGBA(pixel, Handle, palette.Handle, out var r, out var g, out var b, out var a);
        return (r, g, b, a);
    }
}
