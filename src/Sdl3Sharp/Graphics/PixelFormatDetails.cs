using static Sdl3Sharp.Native.Pixels;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Details about the format of a pixel.
/// </summary>
public readonly unsafe struct PixelFormatDetails
{
    private readonly SDL_PixelFormatDetails* _details;

    internal PixelFormatDetails(SDL_PixelFormatDetails* details)
    {
        _details = details;
    }

    /// <summary>
    /// Gets the pixel format.
    /// </summary>
    public PixelFormat Format => new(_details->format);

    /// <summary>
    /// Gets the number of significant bits in a pixel value.
    /// </summary>
    public byte BitsPerPixel => _details->bits_per_pixel;

    /// <summary>
    /// Gets the number of bytes required to store a pixel value.
    /// </summary>
    public byte BytesPerPixel => _details->bytes_per_pixel;

    /// <summary>
    /// Gets the mask for the red component of a pixel.
    /// </summary>
    public uint RedMask => _details->Rmask;

    /// <summary>
    /// Gets the mask for the green component of a pixel.
    /// </summary>
    public uint GreenMask => _details->Gmask;

    /// <summary>
    /// Gets the mask for the blue component of a pixel.
    /// </summary>
    public uint BlueMask => _details->Bmask;

    /// <summary>
    /// Gets the mask for the alpha component of a pixel.
    /// </summary>
    public uint AlphaMask => _details->Amask;

    /// <summary>
    /// Gets the number of bits used for the red component.
    /// </summary>
    public byte RedBits => _details->Rbits;

    /// <summary>
    /// Gets the number of bits used for the green component.
    /// </summary>
    public byte GreenBits => _details->Gbits;

    /// <summary>
    /// Gets the number of bits used for the blue component.
    /// </summary>
    public byte BlueBits => _details->Bbits;

    /// <summary>
    /// Gets the number of bits used for the alpha component.
    /// </summary>
    public byte AlphaBits => _details->Abits;

    /// <summary>
    /// Gets the number of bits to left shift the red component.
    /// </summary>
    public byte RedShift => _details->Rshift;

    /// <summary>
    /// Gets the number of bits to left shift the green component.
    /// </summary>
    public byte GreenShift => _details->Gshift;

    /// <summary>
    /// Gets the number of bits to left shift the blue component.
    /// </summary>
    public byte BlueShift => _details->Bshift;

    /// <summary>
    /// Gets the number of bits to left shift the alpha component.
    /// </summary>
    public byte AlphaShift => _details->Ashift;

    /// <summary>
    /// Gets a value indicating whether this format has an alpha channel.
    /// </summary>
    public bool HasAlpha => _details->Amask != 0;

    /// <summary>
    /// Maps an RGB triple to an opaque pixel value.
    /// </summary>
    /// <param name="red">The red component (0-255).</param>
    /// <param name="green">The green component (0-255).</param>
    /// <param name="blue">The blue component (0-255).</param>
    /// <param name="palette">The optional palette for indexed formats.</param>
    /// <returns>A pixel value.</returns>
    public PixelColor MapRgb(byte red, byte green, byte blue, Palette? palette = null)
    {
        SDL_Palette* palettePtr = palette == null ? null : palette.ToNative();
        return new(SDL_MapRGB(_details, palettePtr, red, green, blue));
    }

    /// <summary>
    /// Maps an RGBA quadruple to a pixel value.
    /// </summary>
    /// <param name="red">The red component (0-255).</param>
    /// <param name="green">The green component (0-255).</param>
    /// <param name="blue">The blue component (0-255).</param>
    /// <param name="alpha">The alpha component (0-255).</param>
    /// <param name="palette">The optional palette for indexed formats.</param>
    /// <returns>A pixel value.</returns>
    public PixelColor MapRgba(byte red, byte green, byte blue, byte alpha, Palette? palette = null)
    {
        SDL_Palette* palettePtr = palette == null ? null : palette.ToNative();
        return new(SDL_MapRGBA(_details, palettePtr, red, green, blue, alpha));
    }

    /// <summary>
    /// Maps a color to a pixel value.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <param name="palette">The optional palette for indexed formats.</param>
    /// <returns>A pixel value.</returns>
    public PixelColor Map(Color color, Palette? palette = null)
    {
        SDL_Palette* palettePtr = palette == null ? null : palette.ToNative();
        return new(SDL_MapRGBA(_details, palettePtr, color.Red, color.Green, color.Blue, color.Alpha));
    }

    /// <summary>
    /// Gets the RGB values from a pixel.
    /// </summary>
    /// <param name="pixel">The pixel value.</param>
    /// <param name="palette">The optional palette for indexed formats.</param>
    /// <returns>The RGB values.</returns>
    public Color GetRgb(PixelColor pixel, Palette? palette = null)
    {
        byte red, green, blue;
        SDL_Palette* palettePtr = palette == null ? null : palette.ToNative();
        SDL_GetRGB(pixel.Value, _details, palettePtr, &red, &green, &blue);
        return new(red, green, blue);
    }

    /// <summary>
    /// Gets the RGBA values from a pixel.
    /// </summary>
    /// <param name="pixel">The pixel value.</param>
    /// <param name="palette">The optional palette for indexed formats.</param>
    /// <returns>The RGBA values.</returns>
    public Color GetRgba(PixelColor pixel, Palette? palette = null)
    {
        byte red, green, blue, alpha;
        SDL_Palette* palettePtr = palette == null ? null : palette.ToNative();
        SDL_GetRGBA(pixel.Value, _details, palettePtr, &red, &green, &blue, &alpha);
        return new(red, green, blue, alpha);
    }

    internal SDL_PixelFormatDetails* ToNative()
    {
        return _details;
    }
}
