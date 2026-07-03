using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Surface;

namespace SdlSharp.Graphics;

/// <summary>
/// A managed wrapper around an SDL surface (SDL_Surface).
/// </summary>
public sealed unsafe class Surface : IDisposable
{
    private readonly bool _ownsHandle;

    /// <summary>
    /// The underlying native SDL_Surface pointer.
    /// </summary>
    internal SDL_Surface* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private SDL_Surface* _handle;

    internal Surface(SDL_Surface* handle, bool ownsHandle = true)
    {
        _handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Gets the width of the surface in pixels.
    /// </summary>
    public int Width => Handle->w;

    /// <summary>
    /// Gets the height of the surface in pixels.
    /// </summary>
    public int Height => Handle->h;

    /// <summary>
    /// Gets the pitch (bytes per row) of the surface.
    /// </summary>
    public int Pitch => Handle->pitch;

    /// <summary>
    /// Gets the pixel format of the surface.
    /// </summary>
    public PixelFormat Format => (PixelFormat)Handle->format;

    /// <summary>
    /// Gets the size of the surface.
    /// </summary>
    public Size Size => new(Handle->w, Handle->h);

    /// <summary>
    /// Gets or sets the blend mode used for blit operations.
    /// </summary>
    public BlendMode BlendMode
    {
        get
        {
            Check(SDL_GetSurfaceBlendMode(Handle, out var mode));
            return (BlendMode)(uint)mode;
        }
        set => Check(SDL_SetSurfaceBlendMode(Handle, (Native.SDL_BlendMode)(uint)value));
    }

    /// <summary>
    /// Gets or sets the additional color value multiplied into blit operations.
    /// </summary>
    public Color ColorMod
    {
        get
        {
            Check(SDL_GetSurfaceColorMod(Handle, out var r, out var g, out var b));
            return new Color(r, g, b);
        }
        set => Check(SDL_SetSurfaceColorMod(Handle, value.R, value.G, value.B));
    }

    /// <summary>
    /// Gets or sets the additional alpha value used in blit operations.
    /// </summary>
    public byte AlphaMod
    {
        get
        {
            Check(SDL_GetSurfaceAlphaMod(Handle, out var alpha));
            return alpha;
        }
        set => Check(SDL_SetSurfaceAlphaMod(Handle, value));
    }

    /// <summary>
    /// Gets the properties associated with this surface. The returned group is owned
    /// by SDL and must not be disposed.
    /// </summary>
    public PropertyGroup Properties =>
        new(CheckId(SDL_GetSurfaceProperties(Handle)), ownsHandle: false);

    /// <summary>
    /// Gets or sets the colorspace used by this surface.
    /// </summary>
    public Colorspace Colorspace
    {
        get => (Colorspace)SDL_GetSurfaceColorspace(Handle);
        set => Check(SDL_SetSurfaceColorspace(Handle, (SDL_Colorspace)value));
    }

    /// <summary>
    /// Gets or sets the clipping rectangle for this surface.
    /// Set to <c>null</c> to disable clipping.
    /// </summary>
    public Rectangle? ClipRect
    {
        get
        {
            Check(SDL_GetSurfaceClipRect(Handle, out var rect));
            return rect.w == 0 && rect.h == 0 ? null : Rectangle.FromNative(rect);
        }
        set
        {
            if (value is { } r)
            {
                var native = r.ToNative();
                Check(SDL_SetSurfaceClipRect(Handle, in native));
            }
            else
            {
                Check(SDL_SetSurfaceClipRect(Handle, (SDL_Rect*)null));
            }
        }
    }

    /// <summary>
    /// Gets the flags describing this surface's internal state and memory layout.
    /// </summary>
    public SurfaceFlags Flags => (SurfaceFlags)Handle->flags;

    /// <summary>
    /// Gets whether this surface must be locked (via <see cref="Lock"/>) before its pixels
    /// can be accessed directly. Mirrors the <c>SDL_MUSTLOCK</c> macro.
    /// </summary>
    public bool MustLock => (Flags & SurfaceFlags.LockNeeded) != 0;

    /// <summary>
    /// Sets whether RLE acceleration is enabled for this surface.
    /// </summary>
    /// <param name="enabled"><c>true</c> to enable RLE acceleration, <c>false</c> to disable it.</param>
    /// <remarks>
    /// RLE (run-length encoding) acceleration speeds up color-keyed blits at the cost of
    /// making direct pixel access require the surface to be locked (see <see cref="Lock"/>),
    /// even on surfaces that would otherwise report <see cref="MustLock"/> as <c>false</c>.
    /// The change takes effect the next time the surface is blitted from.
    /// </remarks>
    public void SetRle(bool enabled) => Check(SDL_SetSurfaceRLE(Handle, enabled));

    /// <summary>
    /// Gets whether RLE acceleration is currently enabled for this surface.
    /// </summary>
    public bool HasRle => SDL_SurfaceHasRLE(Handle);

    /// <summary>
    /// Creates a new surface with the specified dimensions and pixel format.
    /// </summary>
    /// <param name="width">The width of the surface in pixels.</param>
    /// <param name="height">The height of the surface in pixels.</param>
    /// <param name="format">The pixel format for the surface.</param>
    /// <returns>A new surface.</returns>
    public static Surface Create(int width, int height, PixelFormat format) =>
        new(Check(SDL_CreateSurface(width, height, (SDL_PixelFormat)format)));

    /// <summary>
    /// Creates a new surface from existing pixel data.
    /// </summary>
    /// <param name="width">The width of the surface in pixels.</param>
    /// <param name="height">The height of the surface in pixels.</param>
    /// <param name="format">The pixel format for the surface.</param>
    /// <param name="pixels">A pointer to existing pixel data.</param>
    /// <param name="pitch">The number of bytes between each row of pixel data.</param>
    /// <returns>A new surface.</returns>
    public static Surface CreateFrom(int width, int height, PixelFormat format, nint pixels, int pitch) =>
        new(Check(SDL_CreateSurfaceFrom(width, height, (SDL_PixelFormat)format, (void*)pixels, pitch)));

    /// <summary>
    /// Loads a BMP image from a file.
    /// </summary>
    /// <param name="file">The path to the BMP file.</param>
    /// <returns>A new surface containing the loaded image.</returns>
    public static Surface LoadBmp(string file) =>
        new(Check(SDL_LoadBMP(ToUtf8(file))));

    /// <summary>
    /// Loads a BMP or PNG image from a file, detecting the format automatically.
    /// </summary>
    /// <param name="file">The path to the image file.</param>
    /// <returns>A new surface containing the loaded image.</returns>
    public static Surface Load(string file) =>
        new(Check(SDL_LoadSurface(ToUtf8(file))));

    /// <summary>
    /// Loads a PNG image from a file.
    /// </summary>
    /// <param name="file">The path to the PNG file.</param>
    /// <returns>A new surface containing the loaded image.</returns>
    public static Surface LoadPng(string file) =>
        new(Check(SDL_LoadPNG(ToUtf8(file))));

    /// <summary>
    /// Creates a duplicate of this surface.
    /// </summary>
    /// <returns>A new surface that is a copy of this one.</returns>
    public Surface Duplicate() =>
        new(Check(SDL_DuplicateSurface(Handle)));

    /// <summary>
    /// Creates a copy of this surface converted to the specified pixel format.
    /// </summary>
    /// <param name="format">The target pixel format.</param>
    /// <returns>A new surface in the specified format.</returns>
    public Surface Convert(PixelFormat format) =>
        new(Check(SDL_ConvertSurface(Handle, (SDL_PixelFormat)format)));

    /// <summary>
    /// Creates a copy of this surface converted to the specified pixel format and colorspace.
    /// </summary>
    /// <param name="format">The target pixel format.</param>
    /// <param name="colorspace">The target colorspace.</param>
    /// <param name="palette">An optional palette to use for indexed formats, or <c>null</c>.</param>
    /// <param name="properties">Additional color properties, or <c>null</c>.</param>
    /// <returns>A new surface in the specified format and colorspace.</returns>
    public Surface ConvertWithColorspace(PixelFormat format, Colorspace colorspace, Palette? palette = null, PropertyGroup? properties = null) =>
        new(Check(SDL_ConvertSurfaceAndColorspace(
            Handle,
            (SDL_PixelFormat)format,
            palette?.Handle,
            (SDL_Colorspace)colorspace,
            properties?.Id ?? default)));

    /// <summary>
    /// Saves this surface to a file in BMP format.
    /// </summary>
    /// <param name="file">The path to the output BMP file.</param>
    public void SaveBmp(string file) =>
        Check(SDL_SaveBMP(Handle, ToUtf8(file)));

    /// <summary>
    /// Saves this surface to a file in PNG format.
    /// </summary>
    /// <param name="file">The path to the output PNG file.</param>
    public void SavePng(string file) =>
        Check(SDL_SavePNG(Handle, ToUtf8(file)));

    /// <summary>
    /// Locks the surface for direct pixel access.
    /// </summary>
    public void Lock() => Check(SDL_LockSurface(Handle));

    /// <summary>
    /// Unlocks the surface after direct pixel access.
    /// </summary>
    public void Unlock() => SDL_UnlockSurface(Handle);

    /// <summary>
    /// Fills a rectangle with a specific color value.
    /// </summary>
    /// <param name="rect">The rectangle to fill, or <c>null</c> to fill the entire surface.</param>
    /// <param name="color">The raw pixel color value.</param>
    public void FillRect(Rectangle? rect, uint color)
    {
        if (rect is { } r)
        {
            var nr = r.ToNative();
            Check(SDL_FillSurfaceRect(Handle, in nr, color));
        }
        else
        {
            Check(SDL_FillSurfaceRect(Handle, (SDL_Rect*)null, color));
        }
    }

    /// <summary>
    /// Fills a set of rectangles with a specific color value.
    /// </summary>
    /// <param name="rects">The rectangles to fill.</param>
    /// <param name="color">The raw pixel color value.</param>
    public void FillRects(ReadOnlySpan<Rectangle> rects, uint color)
    {
        fixed (Rectangle* ptr = rects)
        {
            Check(SDL_FillSurfaceRects(Handle, (SDL_Rect*)ptr, rects.Length, color));
        }
    }

    /// <summary>
    /// Clears the entire surface with a specific color, with floating point precision.
    /// </summary>
    /// <param name="r">The red component, normally in the range 0-1.</param>
    /// <param name="g">The green component, normally in the range 0-1.</param>
    /// <param name="b">The blue component, normally in the range 0-1.</param>
    /// <param name="a">The alpha component, normally in the range 0-1.</param>
    public void Clear(float r, float g, float b, float a) =>
        Check(SDL_ClearSurface(Handle, r, g, b, a));

    /// <summary>
    /// Performs a fast blit from this surface to a destination surface.
    /// </summary>
    /// <param name="srcRect">The source rectangle, or <c>null</c> for the entire surface.</param>
    /// <param name="dst">The destination surface.</param>
    /// <param name="dstRect">The destination position/rectangle, or <c>null</c> for (0,0).</param>
    public void Blit(Rectangle? srcRect, Surface dst, Rectangle? dstRect)
    {
        SDL_Rect nSrc;
        SDL_Rect nDst;
        var pSrc = (SDL_Rect*)null;
        var pDst = (SDL_Rect*)null;

        if (srcRect is { } sr)
        {
            nSrc = sr.ToNative();
            pSrc = &nSrc;
        }

        if (dstRect is { } dr)
        {
            nDst = dr.ToNative();
            pDst = &nDst;
        }

        Check(SDL_BlitSurface(Handle, pSrc, dst.Handle, pDst));
    }

    /// <summary>
    /// Performs a scaled blit from this surface to a destination surface.
    /// </summary>
    /// <param name="srcRect">The source rectangle, or <c>null</c> for the entire surface.</param>
    /// <param name="dst">The destination surface.</param>
    /// <param name="dstRect">The destination rectangle, or <c>null</c> for the entire destination.</param>
    /// <param name="scaleMode">The scaling algorithm to use.</param>
    public void BlitScaled(Rectangle? srcRect, Surface dst, Rectangle? dstRect, ScaleMode scaleMode)
    {
        SDL_Rect nSrc;
        SDL_Rect nDst;
        var pSrc = (SDL_Rect*)null;
        var pDst = (SDL_Rect*)null;

        if (srcRect is { } sr)
        {
            nSrc = sr.ToNative();
            pSrc = &nSrc;
        }

        if (dstRect is { } dr)
        {
            nDst = dr.ToNative();
            pDst = &nDst;
        }

        Check(SDL_BlitSurfaceScaled(Handle, pSrc, dst.Handle, pDst, (SDL_ScaleMode)scaleMode));
    }

    /// <summary>
    /// Performs a stretched pixel copy from this surface to a destination surface, which may
    /// be of a different format. Unlike <see cref="BlitScaled"/>, no clipping is applied.
    /// </summary>
    /// <param name="sourceRect">The source rectangle, or <c>null</c> for the entire surface.</param>
    /// <param name="destination">The destination surface.</param>
    /// <param name="destinationRect">The destination rectangle, or <c>null</c> for the entire destination.</param>
    /// <param name="scaleMode">The scaling algorithm to use.</param>
    public void Stretch(Surface destination, Rectangle? sourceRect, Rectangle? destinationRect, ScaleMode scaleMode)
    {
        SDL_Rect nSrc;
        SDL_Rect nDst;
        var pSrc = (SDL_Rect*)null;
        var pDst = (SDL_Rect*)null;

        if (sourceRect is { } sr)
        {
            nSrc = sr.ToNative();
            pSrc = &nSrc;
        }

        if (destinationRect is { } dr)
        {
            nDst = dr.ToNative();
            pDst = &nDst;
        }

        Check(SDL_StretchSurface(Handle, pSrc, destination.Handle, pDst, (SDL_ScaleMode)scaleMode));
    }

    /// <summary>
    /// Performs a tiled blit from this surface to a destination surface, repeating the source
    /// (or the source rectangle) to fill the destination rectangle.
    /// </summary>
    /// <param name="destination">The destination surface.</param>
    /// <param name="sourceRect">The source rectangle to tile, or <c>null</c> for the entire surface.</param>
    /// <param name="destinationRect">The destination rectangle to fill, or <c>null</c> for the entire destination.</param>
    public void BlitTiled(Surface destination, Rectangle? sourceRect = null, Rectangle? destinationRect = null)
    {
        SDL_Rect nSrc;
        SDL_Rect nDst;
        var pSrc = (SDL_Rect*)null;
        var pDst = (SDL_Rect*)null;

        if (sourceRect is { } sr)
        {
            nSrc = sr.ToNative();
            pSrc = &nSrc;
        }

        if (destinationRect is { } dr)
        {
            nDst = dr.ToNative();
            pDst = &nDst;
        }

        Check(SDL_BlitSurfaceTiled(Handle, pSrc, destination.Handle, pDst));
    }

    /// <summary>
    /// Performs a scaled and tiled blit from this surface to a destination surface, repeating
    /// the (scaled) source to fill the destination rectangle.
    /// </summary>
    /// <param name="destination">The destination surface.</param>
    /// <param name="scale">The scale factor applied to each tile.</param>
    /// <param name="scaleMode">The scaling algorithm to use.</param>
    /// <param name="sourceRect">The source rectangle to tile, or <c>null</c> for the entire surface.</param>
    /// <param name="destinationRect">The destination rectangle to fill, or <c>null</c> for the entire destination.</param>
    public void BlitTiledWithScale(Surface destination, float scale, ScaleMode scaleMode, Rectangle? sourceRect = null, Rectangle? destinationRect = null)
    {
        SDL_Rect nSrc;
        SDL_Rect nDst;
        var pSrc = (SDL_Rect*)null;
        var pDst = (SDL_Rect*)null;

        if (sourceRect is { } sr)
        {
            nSrc = sr.ToNative();
            pSrc = &nSrc;
        }

        if (destinationRect is { } dr)
        {
            nDst = dr.ToNative();
            pDst = &nDst;
        }

        Check(SDL_BlitSurfaceTiledWithScale(Handle, pSrc, scale, (SDL_ScaleMode)scaleMode, destination.Handle, pDst));
    }

    /// <summary>
    /// Performs a scaled blit using the 9-grid algorithm from this surface to a destination
    /// surface: the source is split into a 3x3 grid, the corners are blitted unscaled, the
    /// edges are tiled along their length, and the center is tiled in both directions to fill
    /// the destination rectangle.
    /// </summary>
    /// <param name="destination">The destination surface.</param>
    /// <param name="leftWidth">The width, in pixels, of the left edge of the 9-grid.</param>
    /// <param name="rightWidth">The width, in pixels, of the right edge of the 9-grid.</param>
    /// <param name="topHeight">The height, in pixels, of the top edge of the 9-grid.</param>
    /// <param name="bottomHeight">The height, in pixels, of the bottom edge of the 9-grid.</param>
    /// <param name="scale">The scale factor applied to the corners and tiled edges/center.</param>
    /// <param name="scaleMode">The scaling algorithm to use.</param>
    /// <param name="sourceRect">The source rectangle, or <c>null</c> for the entire surface.</param>
    /// <param name="destinationRect">The destination rectangle to fill, or <c>null</c> for the entire destination.</param>
    public void Blit9Grid(Surface destination, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, Rectangle? sourceRect = null, Rectangle? destinationRect = null)
    {
        SDL_Rect nSrc;
        SDL_Rect nDst;
        var pSrc = (SDL_Rect*)null;
        var pDst = (SDL_Rect*)null;

        if (sourceRect is { } sr)
        {
            nSrc = sr.ToNative();
            pSrc = &nSrc;
        }

        if (destinationRect is { } dr)
        {
            nDst = dr.ToNative();
            pDst = &nDst;
        }

        Check(SDL_BlitSurface9Grid(Handle, pSrc, leftWidth, rightWidth, topHeight, bottomHeight, scale, (SDL_ScaleMode)scaleMode, destination.Handle, pDst));
    }

    /// <summary>
    /// Sets the color key (transparent pixel) for this surface from a color, mapping it to a
    /// raw pixel value via <see cref="MapColor"/>.
    /// </summary>
    /// <param name="color">The color to treat as transparent.</param>
    public void SetColorKey(Color color) => SetColorKey(MapColor(color));

    /// <summary>
    /// Sets the color key (transparent pixel) for this surface from a raw, format-dependent
    /// pixel value, as generated by <see cref="MapColor"/> or <see cref="MapColorRgb"/>.
    /// </summary>
    /// <param name="key">The raw pixel value to treat as transparent.</param>
    public void SetColorKey(uint key) => Check(SDL_SetSurfaceColorKey(Handle, enabled: true, key));

    /// <summary>
    /// Clears (disables) the color key for this surface.
    /// </summary>
    public void ClearColorKey() => Check(SDL_SetSurfaceColorKey(Handle, enabled: false, key: 0));

    /// <summary>
    /// Gets whether this surface has a color key set.
    /// </summary>
    public bool HasColorKey => SDL_SurfaceHasColorKey(Handle);

    /// <summary>
    /// Gets the raw color key (transparent pixel) for this surface, or <c>null</c> if none is
    /// set. The value is a format-dependent pixel value; SDL provides no way to unmap it back
    /// to a <see cref="Color"/>.
    /// </summary>
    /// <returns>The raw color key, or <c>null</c> if the surface has no color key.</returns>
    public uint? GetColorKey() =>
        SDL_GetSurfaceColorKey(Handle, out var key) ? key : null;

    /// <summary>
    /// Reads a single pixel from this surface.
    /// </summary>
    /// <param name="x">The X coordinate of the pixel.</param>
    /// <param name="y">The Y coordinate of the pixel.</param>
    /// <returns>The pixel's color.</returns>
    public Color ReadPixel(int x, int y)
    {
        Check(SDL_ReadSurfacePixel(Handle, x, y, out var r, out var g, out var b, out var a));
        return new Color(r, g, b, a);
    }

    /// <summary>
    /// Writes a single pixel to this surface.
    /// </summary>
    /// <param name="x">The X coordinate of the pixel.</param>
    /// <param name="y">The Y coordinate of the pixel.</param>
    /// <param name="color">The color to write.</param>
    public void WritePixel(int x, int y, Color color) =>
        Check(SDL_WriteSurfacePixel(Handle, x, y, color.R, color.G, color.B, color.A));

    /// <summary>
    /// Reads a single pixel from this surface, with floating point precision.
    /// </summary>
    /// <param name="x">The X coordinate of the pixel.</param>
    /// <param name="y">The Y coordinate of the pixel.</param>
    /// <returns>The pixel's color.</returns>
    public FColor ReadPixelFloat(int x, int y)
    {
        Check(SDL_ReadSurfacePixelFloat(Handle, x, y, out var r, out var g, out var b, out var a));
        return new FColor(r, g, b, a);
    }

    /// <summary>
    /// Writes a single pixel to this surface, with floating point precision.
    /// </summary>
    /// <param name="x">The X coordinate of the pixel.</param>
    /// <param name="y">The Y coordinate of the pixel.</param>
    /// <param name="color">The color to write.</param>
    public void WritePixelFloat(int x, int y, FColor color) =>
        Check(SDL_WriteSurfacePixelFloat(Handle, x, y, color.R, color.G, color.B, color.A));

    /// <summary>
    /// Creates a copy of this surface, scaled to the specified size.
    /// </summary>
    /// <param name="width">The width of the new surface.</param>
    /// <param name="height">The height of the new surface.</param>
    /// <param name="scaleMode">The scaling algorithm to use.</param>
    /// <returns>A new, scaled surface.</returns>
    public Surface Scale(int width, int height, ScaleMode scaleMode) =>
        new(Check(SDL_ScaleSurface(Handle, width, height, (SDL_ScaleMode)scaleMode)));

    /// <summary>
    /// Flips this surface vertically or horizontally, in place.
    /// </summary>
    /// <param name="flip">The direction to flip.</param>
    public void Flip(FlipMode flip) => Check(SDL_FlipSurface(Handle, (SDL_FlipMode)flip));

    /// <summary>
    /// Creates a copy of this surface, rotated clockwise by an arbitrary angle. The result
    /// surface is sized to fit the rotated content.
    /// </summary>
    /// <remarks>
    /// A negative angle rotates counter-clockwise. When the rotation is not a multiple of
    /// 90 degrees, the resulting surface is larger than the original, with the background
    /// filled with the color key if one is set, or RGBA 255/255/255/0 (transparent white)
    /// if not. If this surface has the <see cref="SurfaceProperties.Rotation"/> property
    /// set, the result surface gets an adjusted value (the rotation remaining to make the
    /// image upright).
    /// </remarks>
    /// <param name="angleDegrees">The angle, in degrees, to rotate the surface clockwise.</param>
    /// <returns>A new, rotated surface.</returns>
    public Surface Rotate(float angleDegrees) =>
        new(Check(SDL_RotateSurface(Handle, angleDegrees)));

    /// <summary>
    /// Maps an RGBA color to an opaque or transparent pixel value for this surface's format.
    /// If the surface has a palette, the returned value is the index of the closest matching
    /// color in the palette.
    /// </summary>
    /// <param name="color">The color to map.</param>
    /// <returns>The raw pixel value.</returns>
    public uint MapColor(Color color) => SDL_MapSurfaceRGBA(Handle, color.R, color.G, color.B, color.A);

    /// <summary>
    /// Maps an RGB color to an opaque pixel value for this surface's format. The alpha
    /// component is ignored. If the surface has a palette, the returned value is the index
    /// of the closest matching color in the palette.
    /// </summary>
    /// <param name="color">The color to map (alpha is ignored).</param>
    /// <returns>The raw pixel value.</returns>
    public uint MapColorRgb(Color color) => SDL_MapSurfaceRGB(Handle, color.R, color.G, color.B);

    /// <summary>
    /// Creates a palette for this surface and associates it with the surface.
    /// </summary>
    /// <returns>
    /// The new palette. The palette is owned by the surface: it is destroyed automatically
    /// when the surface is destroyed, and disposing the returned wrapper has no effect on the
    /// native palette.
    /// </returns>
    public Palette CreatePalette() =>
        new(Check(SDL_CreateSurfacePalette(Handle)), ownsHandle: false);

    /// <summary>
    /// Gets the palette used by this surface, if any.
    /// </summary>
    /// <returns>
    /// The surface's palette (a non-owning wrapper), or <c>null</c> if the surface has no
    /// palette.
    /// </returns>
    public Palette? GetPalette()
    {
        var handle = SDL_GetSurfacePalette(Handle);
        return handle == null ? null : new Palette(handle, ownsHandle: false);
    }

    /// <summary>
    /// Sets the palette used by this surface. The surface keeps an internal reference to the
    /// palette, which can be safely disposed afterwards.
    /// </summary>
    /// <param name="palette">The palette to use.</param>
    public void SetPalette(Palette palette) => Check(SDL_SetSurfacePalette(Handle, palette.Handle));

    /// <summary>
    /// Premultiplies the alpha in this surface, in place.
    /// </summary>
    /// <param name="linear">
    /// <c>true</c> to convert to linear space, premultiply, and convert back (physically
    /// correct for most content); <c>false</c> to premultiply in gamma space.
    /// </param>
    public void PremultiplyAlpha(bool linear) => Check(SDL_PremultiplySurfaceAlpha(Handle, linear));

    /// <summary>
    /// Adds an alternate version of this surface, usually used for content with high-DPI
    /// representations like cursors or icons. The size, format, and content do not need to
    /// match this surface, and the alternate version will not be updated when this surface
    /// changes.
    /// </summary>
    /// <param name="image">
    /// The alternate surface to associate with this one. This surface adds its own reference,
    /// so the caller retains ownership of <paramref name="image"/> and should still dispose it.
    /// </param>
    public void AddAlternateImage(Surface image) => Check(SDL_AddSurfaceAlternateImage(Handle, image.Handle));

    /// <summary>
    /// Gets whether this surface has alternate versions available.
    /// </summary>
    public bool HasAlternateImages => SDL_SurfaceHasAlternateImages(Handle);

    /// <summary>
    /// Gets all versions of this surface, with this surface as the first element.
    /// </summary>
    /// <returns>
    /// Non-owning wrappers for this surface and all of its alternates. The surfaces remain
    /// owned by this surface's reference graph; dispose the returned wrappers freely without
    /// affecting the underlying surfaces (the backing array is freed internally).
    /// </returns>
    public Surface[] GetImages()
    {
        var ptr = SDL_GetSurfaceImages(Handle, out var count);
        if (ptr == null)
        {
            throw new SdlException();
        }

        try
        {
            var result = new Surface[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = new Surface(ptr[i], ownsHandle: false);
            }
            return result;
        }
        finally
        {
            SDL_free(ptr);
        }
    }

    /// <summary>
    /// Removes all alternate versions of this surface, destroying them if this was the last
    /// reference to them.
    /// </summary>
    public void RemoveAlternateImages() => SDL_RemoveSurfaceAlternateImages(Handle);

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && _handle != null)
        {
            SDL_DestroySurface(_handle);
        }
        _handle = null;
    }
}
