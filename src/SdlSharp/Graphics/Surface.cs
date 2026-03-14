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
    internal SDL_Surface* Handle { get; private set; }

    internal Surface(SDL_Surface* handle, bool ownsHandle = true)
    {
        Handle = handle;
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
            return BlendMode.FromNative(mode);
        }
        set => Check(SDL_SetSurfaceBlendMode(Handle, value.ToNative()));
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
    /// Gets the properties associated with this surface.
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
    /// Saves this surface to a file in BMP format.
    /// </summary>
    /// <param name="file">The path to the output BMP file.</param>
    public void SaveBmp(string file) =>
        Check(SDL_SaveBMP(Handle, ToUtf8(file)));

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

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_DestroySurface(Handle);
            Handle = null;
        }
    }
}
