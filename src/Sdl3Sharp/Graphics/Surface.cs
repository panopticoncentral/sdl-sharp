using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Surface;
using static Sdl3Sharp.Native.Pixels;
using static Sdl3Sharp.Native.BlendMode;
using static Sdl3Sharp.Native.Rect;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Represents an SDL surface - a collection of pixels used in software blitting.
/// </summary>
public sealed unsafe class Surface : IDisposable
{
    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Gets the underlying SDL_Surface pointer.
    /// </summary>
    public SDL_Surface* Handle { get; private set; }

    /// <summary>
    /// Gets the properties associated with this surface.
    /// </summary>
    public PropertyGroup Properties
    {
        get
        {
            ThrowIfDisposed();
            return new(SDL_GetSurfaceProperties(Handle), ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets the width of the surface in pixels.
    /// </summary>
    public int Width
    {
        get
        {
            ThrowIfDisposed();
            return Handle->w;
        }
    }

    /// <summary>
    /// Gets the height of the surface in pixels.
    /// </summary>
    public int Height
    {
        get
        {
            ThrowIfDisposed();
            return Handle->h;
        }
    }

    /// <summary>
    /// Gets the size of the surface.
    /// </summary>
    public Size Size
    {
        get
        {
            ThrowIfDisposed();
            return new(Handle->w, Handle->h);
        }
    }

    /// <summary>
    /// Gets the pitch (distance in bytes between rows of pixels).
    /// </summary>
    public int Pitch
    {
        get
        {
            ThrowIfDisposed();
            return Handle->pitch;
        }
    }

    /// <summary>
    /// Gets the pixel format of the surface.
    /// </summary>
    public PixelFormat Format
    {
        get
        {
            ThrowIfDisposed();
            return new(Handle->format);
        }
    }

    /// <summary>
    /// Gets or sets the colorspace used by the surface.
    /// </summary>
    public Colorspace Colorspace
    {
        get
        {
            ThrowIfDisposed();
            return new Colorspace(SDL_GetSurfaceColorspace(Handle));
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetSurfaceColorspace(Handle, value.Value));
        }
    }


    /// <summary>
    /// Gets or sets whether the surface is RLE enabled.
    /// </summary>
    public bool IsRle
    {
        get
        {
            ThrowIfDisposed();
            return SDL_SurfaceHasRLE(Handle);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetSurfaceRLE(Handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the palette used by the surface.
    /// </summary>
    public Palette? GetPalette()
    {
        ThrowIfDisposed();
        SDL_Palette* palette = SDL_GetSurfacePalette(Handle);
        return palette != null ? new Palette(palette) : null;
    }

    /// <summary>
    /// Sets the palette used by the surface.
    /// </summary>
    /// <param name="palette">The palette to use.</param>
    public void SetPalette(Palette palette)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetSurfacePalette(Handle, palette.ToNative()));
    }

    /// <summary>
    /// Creates a new palette and associates it with this surface.
    /// </summary>
    /// <returns>A new palette compatible with this surface.</returns>
    public Palette CreatePalette()
    {
        ThrowIfDisposed();
        return new Palette(CheckErrorPointer(SDL_CreateSurfacePalette(Handle)));
    }

    /// <summary>
    /// Gets whether the surface has a color key.
    /// </summary>
    public bool HasColorKey
    {
        get
        {
            ThrowIfDisposed();
            return SDL_SurfaceHasColorKey(Handle);
        }
    }

    /// <summary>
    /// Gets or sets the color key (transparent pixel) for the surface.
    /// </summary>
    public uint? ColorKey
    {
        get
        {
            ThrowIfDisposed();
            if (!SDL_SurfaceHasColorKey(Handle))
            {
                return null;
            }

            uint key;
            _ = CheckErrorBool(SDL_GetSurfaceColorKey(Handle, &key));
            return key;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetSurfaceColorKey(Handle, value.HasValue, value ?? 0));
        }
    }

    /// <summary>
    /// Gets or sets the color modulation for blit operations.
    /// </summary>
    public Color ColorMod
    {
        get
        {
            ThrowIfDisposed();
            byte r, g, b;
            _ = CheckErrorBool(SDL_GetSurfaceColorMod(Handle, &r, &g, &b));
            return new(r, g, b);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetSurfaceColorMod(Handle, value.Red, value.Green, value.Blue));
        }
    }

    /// <summary>
    /// Gets or sets the alpha modulation for blit operations.
    /// </summary>
    public byte AlphaMod
    {
        get
        {
            ThrowIfDisposed();
            byte alpha;
            _ = CheckErrorBool(SDL_GetSurfaceAlphaMod(Handle, &alpha));
            return alpha;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetSurfaceAlphaMod(Handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the blend mode used for blit operations.
    /// </summary>
    public BlendMode BlendMode
    {
        get
        {
            ThrowIfDisposed();
            SDL_BlendMode mode;
            _ = CheckErrorBool(SDL_GetSurfaceBlendMode(Handle, &mode));
            return new BlendMode(mode);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetSurfaceBlendMode(Handle, value.ToNative()));
        }
    }

    /// <summary>
    /// Gets whether the surface has alternate images.
    /// </summary>
    public bool HasAlternateImages
    {
        get
        {
            ThrowIfDisposed();
            return SDL_SurfaceHasAlternateImages(Handle);
        }
    }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_Surface pointer.
    /// </summary>
    /// <param name="handle">The SDL_Surface pointer.</param>
    /// <param name="ownsHandle">Whether this instance owns the handle and should free it on disposal.</param>
    internal Surface(SDL_Surface* handle, bool ownsHandle)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Creates a new surface with a specific pixel format.
    /// </summary>
    /// <param name="width">The width of the surface.</param>
    /// <param name="height">The height of the surface.</param>
    /// <param name="format">The pixel format for the new surface.</param>
    /// <returns>A new surface.</returns>
    public static Surface Create(int width, int height, PixelFormat format)
    {
        return new Surface(CheckErrorPointer(SDL_CreateSurface(width, height, format.Format)), ownsHandle: true);
    }

    /// <summary>
    /// Creates a new surface with a specific pixel format and existing pixel data.
    /// </summary>
    /// <param name="width">The width of the surface.</param>
    /// <param name="height">The height of the surface.</param>
    /// <param name="format">The pixel format for the new surface.</param>
    /// <param name="pixels">A pointer to existing pixel data.</param>
    /// <param name="pitch">The number of bytes between each row, including padding.</param>
    /// <returns>A new surface.</returns>
    public static Surface CreateFrom(int width, int height, PixelFormat format, void* pixels, int pitch)
    {
        return new Surface(CheckErrorPointer(SDL_CreateSurfaceFrom(width, height, format.Format, pixels, pitch)), ownsHandle: true);
    }

    /// <summary>
    /// Loads a BMP image from a file.
    /// </summary>
    /// <param name="file">The path to the BMP file.</param>
    /// <returns>A new surface containing the loaded image.</returns>
    public static Surface LoadBmp(string file)
    {
        return new Surface(CheckErrorPointer(SDL_LoadBMP(file)), ownsHandle: true);
    }

    /// <summary>
    /// Loads a BMP image from an I/O stream.
    /// </summary>
    /// <param name="stream">The I/O stream to read from.</param>
    /// <param name="closeStream">Whether to close the stream after loading.</param>
    /// <returns>A new surface containing the loaded image.</returns>
    public static Surface LoadBmp(IOStream stream, bool closeStream)
    {
        return new Surface(CheckErrorPointer(SDL_LoadBMP_IO(stream.Handle, closeStream)), ownsHandle: true);
    }

    /// <summary>
    /// Saves the surface to a BMP file.
    /// </summary>
    /// <param name="file">The path to save the BMP file to.</param>
    public void SaveBmp(string file)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SaveBMP(Handle, file));
    }

    /// <summary>
    /// Saves the surface to an I/O stream in BMP format.
    /// </summary>
    /// <param name="stream">The I/O stream to write to.</param>
    /// <param name="closeStream">Whether to close the stream after saving.</param>
    public void SaveBmp(IOStream stream, bool closeStream)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SaveBMP_IO(Handle, stream.Handle, closeStream));
    }

    /// <summary>
    /// Locks the surface for direct pixel access.
    /// </summary>
    public void Lock()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_LockSurface(Handle));
    }

    /// <summary>
    /// Unlocks the surface after direct pixel access.
    /// </summary>
    public void Unlock()
    {
        ThrowIfDisposed();
        SDL_UnlockSurface(Handle);
    }

    /// <summary>
    /// Gets a span of the surface's pixels.
    /// </summary>
    /// <typeparam name="T">The type of pixel data.</typeparam>
    /// <returns>A span over the pixel data.</returns>
    public Span<T> GetPixels<T>() where T : unmanaged
    {
        ThrowIfDisposed();
        var size = Handle->pitch * Handle->h / sizeof(T);
        return new Span<T>(Handle->pixels, size);
    }

    /// <summary>
    /// Sets the clipping rectangle for the surface.
    /// </summary>
    /// <param name="rect">The clipping rectangle, or null to disable clipping.</param>
    /// <returns>True if the rectangle intersects the surface, false otherwise.</returns>
    public bool SetClipRect(Rectangle? rect)
    {
        ThrowIfDisposed();
        SDL_Rect nativeRect;
        return SDL_SetSurfaceClipRect(Handle, Rectangle.ToNative(rect, &nativeRect));
    }

    /// <summary>
    /// Gets the clipping rectangle for the surface.
    /// </summary>
    /// <returns>The current clipping rectangle.</returns>
    public Rectangle GetClipRect()
    {
        ThrowIfDisposed();
        SDL_Rect rect;
        _ = CheckErrorBool(SDL_GetSurfaceClipRect(Handle, &rect));
        return new Rectangle(rect);
    }

    /// <summary>
    /// Flips the surface vertically or horizontally.
    /// </summary>
    /// <param name="flip">The direction to flip.</param>
    public void Flip(FlipMode flip)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_FlipSurface(Handle, (SDL_FlipMode)flip));
    }

    /// <summary>
    /// Creates a duplicate of the surface.
    /// </summary>
    /// <returns>A new surface that is a copy of this surface.</returns>
    public Surface Duplicate()
    {
        ThrowIfDisposed();
        return new Surface(CheckErrorPointer(SDL_DuplicateSurface(Handle)), ownsHandle: true);
    }

    /// <summary>
    /// Creates a scaled copy of the surface.
    /// </summary>
    /// <param name="width">The width of the new surface.</param>
    /// <param name="height">The height of the new surface.</param>
    /// <param name="scaleMode">The scaling mode to use.</param>
    /// <returns>A new scaled surface.</returns>
    public Surface Scale(int width, int height, ScaleMode scaleMode)
    {
        ThrowIfDisposed();
        return new Surface(CheckErrorPointer(SDL_ScaleSurface(Handle, width, height, (SDL_ScaleMode)scaleMode)), ownsHandle: true);
    }

    /// <summary>
    /// Converts the surface to a new pixel format.
    /// </summary>
    /// <param name="format">The new pixel format.</param>
    /// <returns>A new surface in the specified format.</returns>
    public Surface Convert(PixelFormat format)
    {
        ThrowIfDisposed();
        return new Surface(CheckErrorPointer(SDL_ConvertSurface(Handle, format.Format)), ownsHandle: true);
    }

    /// <summary>
    /// Converts the surface to a new pixel format and colorspace.
    /// </summary>
    /// <param name="format">The new pixel format.</param>
    /// <param name="palette">An optional palette to use for indexed formats.</param>
    /// <param name="colorspace">The new colorspace.</param>
    /// <param name="properties">Additional color properties, or null.</param>
    /// <returns>A new surface in the specified format and colorspace.</returns>
    public Surface Convert(PixelFormat format, Palette? palette, Colorspace colorspace, PropertyGroup? properties = null)
    {
        ThrowIfDisposed();
        SDL_Palette* palettePtr = null;
        if (palette != null)
        {
            palettePtr = palette.ToNative();
        }

        Native.Properties.SDL_PropertiesID propsId = properties?.Id ?? 0;
        return new Surface(CheckErrorPointer(SDL_ConvertSurfaceAndColorspace(Handle, format.Format, palettePtr, colorspace.Value, propsId)), ownsHandle: true);
    }

    /// <summary>
    /// Premultiplies the alpha in the surface.
    /// </summary>
    /// <param name="linear">True to convert from sRGB to linear space for alpha multiplication.</param>
    public void PremultiplyAlpha(bool linear)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_PremultiplySurfaceAlpha(Handle, linear));
    }

    /// <summary>
    /// Clears the surface with a specific color.
    /// </summary>
    /// <param name="color">The color to clear with.</param>
    public void Clear(ColorF color)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_ClearSurface(Handle, color.Red, color.Green, color.Blue, color.Alpha));
    }

    /// <summary>
    /// Fills a rectangle with a specific color.
    /// </summary>
    /// <param name="rect">The rectangle to fill, or null to fill the entire surface.</param>
    /// <param name="color">The color to fill with.</param>
    public void FillRect(Rectangle? rect, PixelColor color)
    {
        ThrowIfDisposed();
        SDL_Rect nativeRect;
        _ = CheckErrorBool(SDL_FillSurfaceRect(Handle, Rectangle.ToNative(rect, &nativeRect), color.Value));
    }

    /// <summary>
    /// Fills multiple rectangles with a specific color.
    /// </summary>
    /// <param name="rects">The rectangles to fill.</param>
    /// <param name="color">The color to fill with.</param>
    public void FillRects(Rectangle[] rects, PixelColor color)
    {
        ThrowIfDisposed();
        fixed (Rectangle* rectsPtr = rects)
        {
            _ = CheckErrorBool(SDL_FillSurfaceRects(Handle, (SDL_Rect*)rectsPtr, rects.Length, color.Value));
        }
    }

    /// <summary>
    /// Blits (copies) from this surface to a destination surface.
    /// </summary>
    /// <param name="srcRect">The source rectangle, or null to use the entire source surface.</param>
    /// <param name="dst">The destination surface.</param>
    /// <param name="dstRect">The destination rectangle for positioning.</param>
    public void Blit(Rectangle? srcRect, Surface dst, Rectangle? dstRect)
    {
        ThrowIfDisposed();
        SDL_Rect srcNative, dstNative;
        _ = CheckErrorBool(SDL_BlitSurface(Handle, Rectangle.ToNative(srcRect, &srcNative), dst.Handle, Rectangle.ToNative(dstRect, &dstNative)));
    }

    /// <summary>
    /// Blits (copies) from this surface to a destination surface with scaling.
    /// </summary>
    /// <param name="srcRect">The source rectangle, or null to use the entire source surface.</param>
    /// <param name="dst">The destination surface.</param>
    /// <param name="dstRect">The destination rectangle.</param>
    /// <param name="scaleMode">The scaling mode to use.</param>
    public void BlitScaled(Rectangle? srcRect, Surface dst, Rectangle? dstRect, ScaleMode scaleMode)
    {
        ThrowIfDisposed();
        SDL_Rect srcNative, dstNative;
        _ = CheckErrorBool(SDL_BlitSurfaceScaled(Handle, Rectangle.ToNative(srcRect, &srcNative), dst.Handle, Rectangle.ToNative(dstRect, &dstNative), (SDL_ScaleMode)scaleMode));
    }

    /// <summary>
    /// Performs a stretched pixel copy from this surface to a destination surface.
    /// </summary>
    /// <param name="srcRect">The source rectangle, or null to use the entire source surface.</param>
    /// <param name="dst">The destination surface.</param>
    /// <param name="dstRect">The destination rectangle.</param>
    /// <param name="scaleMode">The scaling mode to use.</param>
    public void Stretch(Rectangle? srcRect, Surface dst, Rectangle? dstRect, ScaleMode scaleMode)
    {
        ThrowIfDisposed();
        SDL_Rect srcNative, dstNative;
        _ = CheckErrorBool(SDL_StretchSurface(Handle, Rectangle.ToNative(srcRect, &srcNative), dst.Handle, Rectangle.ToNative(dstRect, &dstNative), (SDL_ScaleMode)scaleMode));
    }

    /// <summary>
    /// Blits (copies) from this surface to a destination surface with tiling.
    /// </summary>
    /// <param name="srcRect">The source rectangle, or null to use the entire source surface.</param>
    /// <param name="dst">The destination surface.</param>
    /// <param name="dstRect">The destination rectangle to fill with tiles.</param>
    public void BlitTiled(Rectangle? srcRect, Surface dst, Rectangle? dstRect)
    {
        ThrowIfDisposed();
        SDL_Rect srcNative, dstNative;
        _ = CheckErrorBool(SDL_BlitSurfaceTiled(Handle, Rectangle.ToNative(srcRect, &srcNative), dst.Handle, Rectangle.ToNative(dstRect, &dstNative)));
    }

    /// <summary>
    /// Blits (copies) from this surface to a destination surface with scaled tiling.
    /// </summary>
    /// <param name="srcRect">The source rectangle, or null to use the entire source surface.</param>
    /// <param name="scale">The scale factor for the tiles.</param>
    /// <param name="scaleMode">The scaling mode to use.</param>
    /// <param name="dst">The destination surface.</param>
    /// <param name="dstRect">The destination rectangle to fill with tiles.</param>
    public void BlitTiledWithScale(Rectangle? srcRect, float scale, ScaleMode scaleMode, Surface dst, Rectangle? dstRect)
    {
        ThrowIfDisposed();
        SDL_Rect srcNative, dstNative;
        _ = CheckErrorBool(SDL_BlitSurfaceTiledWithScale(Handle, Rectangle.ToNative(srcRect, &srcNative), scale, (SDL_ScaleMode)scaleMode, dst.Handle, Rectangle.ToNative(dstRect, &dstNative)));
    }

    /// <summary>
    /// Blits using the 9-grid algorithm.
    /// </summary>
    /// <param name="srcRect">The source rectangle for the 9-grid.</param>
    /// <param name="leftWidth">The width of the left corners.</param>
    /// <param name="rightWidth">The width of the right corners.</param>
    /// <param name="topHeight">The height of the top corners.</param>
    /// <param name="bottomHeight">The height of the bottom corners.</param>
    /// <param name="scale">The scale for the corners.</param>
    /// <param name="scaleMode">The scaling mode to use.</param>
    /// <param name="dst">The destination surface.</param>
    /// <param name="dstRect">The destination rectangle.</param>
    public void Blit9Grid(Rectangle? srcRect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, Surface dst, Rectangle? dstRect)
    {
        ThrowIfDisposed();
        SDL_Rect srcNative, dstNative;
        _ = CheckErrorBool(SDL_BlitSurface9Grid(Handle, Rectangle.ToNative(srcRect, &srcNative), leftWidth, rightWidth, topHeight, bottomHeight, scale, (SDL_ScaleMode)scaleMode, dst.Handle, Rectangle.ToNative(dstRect, &dstNative)));
    }

    /// <summary>
    /// Maps an RGB triple to an opaque pixel value for this surface.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <returns>A pixel value.</returns>
    public PixelColor MapRgb(Color color)
    {
        ThrowIfDisposed();
        return new(SDL_MapSurfaceRGB(Handle, color.Red, color.Green, color.Blue));
    }

    /// <summary>
    /// Maps an RGBA quadruple to a pixel value for this surface.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <returns>A pixel value.</returns>
    public PixelColor MapRgba(Color color)
    {
        ThrowIfDisposed();
        return new(SDL_MapSurfaceRGBA(Handle, color.Red, color.Green, color.Blue, color.Alpha));
    }

    /// <summary>
    /// Reads a single pixel from the surface.
    /// </summary>
    /// <param name="x">The horizontal coordinate.</param>
    /// <param name="y">The vertical coordinate.</param>
    /// <returns>The pixel color as RGBA values.</returns>
    public Color ReadPixel(int x, int y)
    {
        ThrowIfDisposed();
        byte r, g, b, a;
        _ = CheckErrorBool(SDL_ReadSurfacePixel(Handle, x, y, &r, &g, &b, &a));
        return new(r, g, b, a);
    }

    /// <summary>
    /// Reads a single pixel from the surface as floating point values.
    /// </summary>
    /// <param name="x">The horizontal coordinate.</param>
    /// <param name="y">The vertical coordinate.</param>
    /// <returns>The pixel color as RGBA values, normally 0-1.</returns>
    public ColorF ReadPixelFloat(int x, int y)
    {
        ThrowIfDisposed();
        float r, g, b, a;
        _ = CheckErrorBool(SDL_ReadSurfacePixelFloat(Handle, x, y, &r, &g, &b, &a));
        return new(r, g, b, a);
    }

    /// <summary>
    /// Writes a single pixel to the surface.
    /// </summary>
    /// <param name="point">The point to write to.</param>
    /// <param name="color">The color to write.</param>
    public void WritePixel(Point point, Color color)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteSurfacePixel(Handle, point.X, point.Y, color.Red, color.Green, color.Blue, color.Alpha));
    }

    /// <summary>
    /// Writes a single pixel to the surface as floating point values.
    /// </summary>
    /// <param name="point">The point to write to.</param>
    /// <param name="color">The color to write.</param>
    public void WritePixelFloat(Point point, ColorF color)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WriteSurfacePixelFloat(Handle, point.X, point.Y, color.Red, color.Green, color.Blue, color.Alpha));
    }

    /// <summary>
    /// Adds an alternate version of the surface.
    /// </summary>
    /// <param name="image">The alternate image to add.</param>
    public void AddAlternateImage(Surface image)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_AddSurfaceAlternateImage(Handle, image.Handle));
    }

    /// <summary>
    /// Gets all versions of the surface (including this surface and alternates).
    /// </summary>
    /// <returns>An array of all surface versions.</returns>
    public Surface[] GetImages()
    {
        ThrowIfDisposed();
        int count;
        SDL_Surface** images = SDL_GetSurfaceImages(Handle, &count);
        if (images == null)
        {
            throw new SdlException();
        }

        var result = new Surface[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = new Surface(images[i], ownsHandle: false);
        }

        SDL_free(images);
        return result;
    }

    /// <summary>
    /// Removes all alternate versions of the surface.
    /// </summary>
    public void RemoveAlternateImages()
    {
        ThrowIfDisposed();
        SDL_RemoveSurfaceAlternateImages(Handle);
    }

    /// <summary>
    /// Converts a block of pixels from one format to another.
    /// </summary>
    /// <param name="size">The size of the block in pixels.</param>
    /// <param name="srcFormat">The source pixel format.</param>
    /// <param name="src">Pointer to the source pixels.</param>
    /// <param name="srcPitch">The pitch of the source pixels in bytes.</param>
    /// <param name="dstFormat">The destination pixel format.</param>
    /// <param name="dst">Pointer to the destination pixels.</param>
    /// <param name="dstPitch">The pitch of the destination pixels in bytes.</param>
    public static void ConvertPixels(Size size, PixelFormat srcFormat, void* src, int srcPitch, PixelFormat dstFormat, void* dst, int dstPitch)
    {
        _ = CheckErrorBool(SDL_ConvertPixels(size.Width, size.Height, srcFormat.Format, src, srcPitch, dstFormat.Format, dst, dstPitch));
    }

    /// <summary>
    /// Converts a block of pixels from one format and colorspace to another.
    /// </summary>
    /// <param name="size">The size of the block in pixels.</param>
    /// <param name="srcFormat">The source pixel format.</param>
    /// <param name="srcColorspace">The source colorspace.</param>
    /// <param name="srcProperties">Additional source color properties, or null.</param>
    /// <param name="src">Pointer to the source pixels.</param>
    /// <param name="srcPitch">The pitch of the source pixels in bytes.</param>
    /// <param name="dstFormat">The destination pixel format.</param>
    /// <param name="dstColorspace">The destination colorspace.</param>
    /// <param name="dstProperties">Additional destination color properties, or null.</param>
    /// <param name="dst">Pointer to the destination pixels.</param>
    /// <param name="dstPitch">The pitch of the destination pixels in bytes.</param>
    public static void ConvertPixelsAndColorspace(Size size, PixelFormat srcFormat, Colorspace srcColorspace, PropertyGroup? srcProperties, void* src, int srcPitch, PixelFormat dstFormat, Colorspace dstColorspace, PropertyGroup? dstProperties, void* dst, int dstPitch)
    {
        Native.Properties.SDL_PropertiesID srcPropsId = srcProperties?.Id ?? 0;
        Native.Properties.SDL_PropertiesID dstPropsId = dstProperties?.Id ?? 0;
        _ = CheckErrorBool(SDL_ConvertPixelsAndColorspace(size.Width, size.Height, srcFormat.Format, srcColorspace.Value, srcPropsId, src, srcPitch, dstFormat.Format, dstColorspace.Value, dstPropsId, dst, dstPitch));
    }

    /// <summary>
    /// Premultiplies the alpha on a block of pixels.
    /// </summary>
    /// <param name="size">The size of the block in pixels.</param>
    /// <param name="srcFormat">The source pixel format.</param>
    /// <param name="src">Pointer to the source pixels.</param>
    /// <param name="srcPitch">The pitch of the source pixels in bytes.</param>
    /// <param name="dstFormat">The destination pixel format.</param>
    /// <param name="dst">Pointer to the destination pixels.</param>
    /// <param name="dstPitch">The pitch of the destination pixels in bytes.</param>
    /// <param name="linear">True to convert from sRGB to linear space for alpha multiplication.</param>
    public static void PremultiplyAlpha(Size size, PixelFormat srcFormat, void* src, int srcPitch, PixelFormat dstFormat, void* dst, int dstPitch, bool linear)
    {
        _ = CheckErrorBool(SDL_PremultiplyAlpha(size.Width, size.Height, srcFormat.Format, src, srcPitch, dstFormat.Format, dst, dstPitch, linear));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Handle != null)
        {
            SDL_DestroySurface(Handle);
        }

        Handle = null;
        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    internal SDL_Surface* ToNative()
    {
        ThrowIfDisposed();
        return Handle;
    }
}
