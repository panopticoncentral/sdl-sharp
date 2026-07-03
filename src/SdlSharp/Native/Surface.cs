using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Skipped: IO stream variants (SDL_LoadBMP_IO, SDL_LoadPNG_IO, SDL_LoadSurface_IO, SDL_SaveBMP_IO, SDL_SavePNG_IO) —
// stream I/O is handled via .NET streams, not SDL_IOStream.
// Skipped: raw pixel-buffer trio (SDL_ConvertPixels, SDL_ConvertPixelsAndColorspace, SDL_PremultiplyAlpha) —
// operates on raw pixel buffers; Surface-based equivalents (SDL_ConvertSurface[AndColorspace], SDL_PremultiplySurfaceAlpha) cover the managed use cases.
// Skipped: SDL_BlitSurfaceUnchecked, SDL_BlitSurfaceUncheckedScaled — niche/unsafe (no bounds clipping);
// the clipping SDL_BlitSurface/SDL_BlitSurfaceScaled cover the managed use cases.
// Skipped: SDL_MUSTLOCK — this is a macro (`#define`), not an exported function; exposed as a managed property instead.

/// <summary>
/// Surface flags.
/// </summary>
[Flags]
public enum SDL_SurfaceFlags : uint
{
    /// <summary>Surface uses preallocated pixel memory.</summary>
    SDL_SURFACE_PREALLOCATED = 0x00000001u,
    /// <summary>Surface needs to be locked to access pixels.</summary>
    SDL_SURFACE_LOCK_NEEDED = 0x00000002u,
    /// <summary>Surface is currently locked.</summary>
    SDL_SURFACE_LOCKED = 0x00000004u,
    /// <summary>Surface uses pixel memory allocated with SDL_aligned_alloc().</summary>
    SDL_SURFACE_SIMD_ALIGNED = 0x00000008u,
}

/// <summary>
/// The scaling mode.
/// </summary>
public enum SDL_ScaleMode
{
    /// <summary>Invalid scale mode.</summary>
    SDL_SCALEMODE_INVALID = -1,
    /// <summary>Nearest pixel sampling.</summary>
    SDL_SCALEMODE_NEAREST,
    /// <summary>Linear filtering.</summary>
    SDL_SCALEMODE_LINEAR,
    /// <summary>Nearest pixel sampling with improved scaling for pixel art.</summary>
    SDL_SCALEMODE_PIXELART,
}

/// <summary>
/// The flip mode.
/// </summary>
public enum SDL_FlipMode
{
    /// <summary>Do not flip.</summary>
    SDL_FLIP_NONE,
    /// <summary>Flip horizontally.</summary>
    SDL_FLIP_HORIZONTAL,
    /// <summary>Flip vertically.</summary>
    SDL_FLIP_VERTICAL,
    /// <summary>Flip horizontally and vertically.</summary>
    SDL_FLIP_HORIZONTAL_AND_VERTICAL = SDL_FLIP_HORIZONTAL | SDL_FLIP_VERTICAL,
}

/// <summary>
/// A collection of pixels used in software blitting.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_Surface
{
    /// <summary>The flags of the surface, read-only.</summary>
    public SDL_SurfaceFlags flags;
    /// <summary>The format of the surface, read-only.</summary>
    public SDL_PixelFormat format;
    /// <summary>The width of the surface, read-only.</summary>
    public int w;
    /// <summary>The height of the surface, read-only.</summary>
    public int h;
    /// <summary>The distance in bytes between rows of pixels, read-only.</summary>
    public int pitch;
    /// <summary>A pointer to the pixels of the surface.</summary>
    public void* pixels;
    /// <summary>Application reference count.</summary>
    public int refcount;
    /// <summary>Reserved for internal use.</summary>
    public void* reserved;
}

/// <summary>
/// Native bindings for SDL_surface.h — surface creation, manipulation, and blitting.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Surface
{
    /// <summary>Property name: for HDR10 and floating point surfaces, this defines the value of 100% diffuse white, with higher values being displayed in the High Dynamic Range headroom (float).</summary>
    public const string SDL_PROP_SURFACE_SDR_WHITE_POINT_FLOAT = "SDL.surface.SDR_white_point";
    /// <summary>Property name: for HDR10 and floating point surfaces, this defines the maximum dynamic range used by the content, in terms of the SDR white point (float).</summary>
    public const string SDL_PROP_SURFACE_HDR_HEADROOM_FLOAT = "SDL.surface.HDR_headroom";
    /// <summary>Property name: the tone mapping operator used when compressing from a surface with high dynamic range to another with lower dynamic range (string).</summary>
    public const string SDL_PROP_SURFACE_TONEMAP_OPERATOR_STRING = "SDL.surface.tonemap";
    /// <summary>Property name: the hotspot pixel offset from the left edge of the image, if this surface is being used as a cursor (number).</summary>
    public const string SDL_PROP_SURFACE_HOTSPOT_X_NUMBER = "SDL.surface.hotspot.x";
    /// <summary>Property name: the hotspot pixel offset from the top edge of the image, if this surface is being used as a cursor (number).</summary>
    public const string SDL_PROP_SURFACE_HOTSPOT_Y_NUMBER = "SDL.surface.hotspot.y";
    /// <summary>Property name: the number of degrees a surface's data is meant to be rotated clockwise to make the image right-side up (float).</summary>
    public const string SDL_PROP_SURFACE_ROTATION_FLOAT = "SDL.surface.rotation";

    /// <summary>
    /// Allocate a new surface with a specific pixel format.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_CreateSurface(int width, int height, SDL_PixelFormat format);

    /// <summary>
    /// Allocate a new surface with a specific pixel format and existing pixel data.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateSurfaceFrom")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_CreateSurfaceFrom(int width, int height, SDL_PixelFormat format, void* pixels, int pitch);

    /// <summary>
    /// Free a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroySurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DestroySurface(SDL_Surface* surface);

    /// <summary>
    /// Get the properties associated with a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSurfaceProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PropertiesID SDL_GetSurfaceProperties(SDL_Surface* surface);

    /// <summary>
    /// Set the colorspace used by a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetSurfaceColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetSurfaceColorspace(SDL_Surface* surface, SDL_Colorspace colorspace);

    /// <summary>
    /// Get the colorspace used by a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSurfaceColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Colorspace SDL_GetSurfaceColorspace(SDL_Surface* surface);

    /// <summary>
    /// Set an additional color value multiplied into blit operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetSurfaceColorMod(SDL_Surface* surface, byte r, byte g, byte b);

    /// <summary>
    /// Get the additional color value multiplied into blit operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetSurfaceColorMod(SDL_Surface* surface, out byte r, out byte g, out byte b);

    /// <summary>
    /// Set an additional alpha value used in blit operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetSurfaceAlphaMod(SDL_Surface* surface, byte alpha);

    /// <summary>
    /// Get the additional alpha value used in blit operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetSurfaceAlphaMod(SDL_Surface* surface, out byte alpha);

    /// <summary>
    /// Set the blend mode used for blit operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetSurfaceBlendMode(SDL_Surface* surface, SDL_BlendMode blendMode);

    /// <summary>
    /// Get the blend mode used for blit operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetSurfaceBlendMode(SDL_Surface* surface, out SDL_BlendMode blendMode);

    /// <summary>
    /// Set the clipping rectangle for a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetSurfaceClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetSurfaceClipRect(SDL_Surface* surface, in SDL_Rect rect);

    /// <summary>
    /// Set the clipping rectangle for a surface (nullable variant to disable clipping).
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetSurfaceClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetSurfaceClipRect(SDL_Surface* surface, SDL_Rect* rect);

    /// <summary>
    /// Get the clipping rectangle for a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSurfaceClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetSurfaceClipRect(SDL_Surface* surface, out SDL_Rect rect);

    /// <summary>
    /// Set up a surface for directly accessing the pixels.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_LockSurface(SDL_Surface* surface);

    /// <summary>
    /// Release a surface after directly accessing the pixels.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UnlockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_UnlockSurface(SDL_Surface* surface);

    /// <summary>
    /// Load a BMP image from a file.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LoadBMP")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_LoadBMP(ReadOnlySpan<byte> file);

    /// <summary>
    /// Save a surface to a file in BMP format.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SaveBMP")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SaveBMP(SDL_Surface* surface, ReadOnlySpan<byte> file);

    /// <summary>
    /// Perform a fast fill of a rectangle with a specific color (nullable rect variant).
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_FillSurfaceRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_FillSurfaceRect(SDL_Surface* dst, SDL_Rect* rect, uint color);

    /// <summary>
    /// Perform a fast fill of a rectangle with a specific color.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_FillSurfaceRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_FillSurfaceRect(SDL_Surface* dst, in SDL_Rect rect, uint color);

    /// <summary>
    /// Perform a fast fill of a set of rectangles with a specific color.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_FillSurfaceRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_FillSurfaceRects(SDL_Surface* dst, SDL_Rect* rects, int count, uint color);

    /// <summary>
    /// Performs a fast blit from the source surface to the destination surface with clipping.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BlitSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_BlitSurface(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

    /// <summary>
    /// Perform a scaled blit to a destination surface, which may be of a different format.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BlitSurfaceScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_BlitSurfaceScaled(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect, SDL_ScaleMode scaleMode);

    /// <summary>
    /// Creates a new surface identical to the existing surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DuplicateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_DuplicateSurface(SDL_Surface* surface);

    /// <summary>
    /// Copy an existing surface to a new surface of the specified format.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ConvertSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_ConvertSurface(SDL_Surface* surface, SDL_PixelFormat format);

    /// <summary>
    /// Clear a surface with a specific color, with floating point precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ClearSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ClearSurface(SDL_Surface* surface, float r, float g, float b, float a);

    /// <summary>
    /// Create a palette and associate it with a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Palette* SDL_CreateSurfacePalette(SDL_Surface* surface);

    /// <summary>
    /// Set the palette used by a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetSurfacePalette(SDL_Surface* surface, SDL_Palette* palette);

    /// <summary>
    /// Get the palette used by a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Palette* SDL_GetSurfacePalette(SDL_Surface* surface);

    /// <summary>
    /// Add an alternate version of a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AddSurfaceAlternateImage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_AddSurfaceAlternateImage(SDL_Surface* surface, SDL_Surface* image);

    /// <summary>
    /// Return whether a surface has alternate versions available.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SurfaceHasAlternateImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SurfaceHasAlternateImages(SDL_Surface* surface);

    /// <summary>
    /// Get an array including all versions of a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSurfaceImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface** SDL_GetSurfaceImages(SDL_Surface* surface, out int count);

    /// <summary>
    /// Remove all alternate versions of a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RemoveSurfaceAlternateImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_RemoveSurfaceAlternateImages(SDL_Surface* surface);

    /// <summary>
    /// Load a BMP or PNG image from a file.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LoadSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_LoadSurface(ReadOnlySpan<byte> file);

    /// <summary>
    /// Load a PNG image from a file.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LoadPNG")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_LoadPNG(ReadOnlySpan<byte> file);

    /// <summary>
    /// Save a surface to a file in PNG format.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SavePNG")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SavePNG(SDL_Surface* surface, ReadOnlySpan<byte> file);

    /// <summary>
    /// Set the RLE acceleration hint for a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetSurfaceRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetSurfaceRLE(SDL_Surface* surface, [MarshalAs(UnmanagedType.U1)] bool enabled);

    /// <summary>
    /// Returns whether the surface is RLE enabled.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SurfaceHasRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SurfaceHasRLE(SDL_Surface* surface);

    /// <summary>
    /// Set the color key (transparent pixel) in a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetSurfaceColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetSurfaceColorKey(SDL_Surface* surface, [MarshalAs(UnmanagedType.U1)] bool enabled, uint key);

    /// <summary>
    /// Returns whether the surface has a color key.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SurfaceHasColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SurfaceHasColorKey(SDL_Surface* surface);

    /// <summary>
    /// Get the color key (transparent pixel) for a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSurfaceColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetSurfaceColorKey(SDL_Surface* surface, out uint key);

    /// <summary>
    /// Flip a surface vertically or horizontally.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_FlipSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_FlipSurface(SDL_Surface* surface, SDL_FlipMode flip);

    /// <summary>
    /// Return a copy of a surface rotated clockwise a number of degrees.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RotateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_RotateSurface(SDL_Surface* surface, float angle);

    /// <summary>
    /// Creates a new surface identical to the existing surface, scaled to the desired size.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ScaleSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_ScaleSurface(SDL_Surface* surface, int width, int height, SDL_ScaleMode scaleMode);

    /// <summary>
    /// Copy an existing surface to a new surface of the specified format and colorspace.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ConvertSurfaceAndColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_ConvertSurfaceAndColorspace(SDL_Surface* surface, SDL_PixelFormat format, SDL_Palette* palette, SDL_Colorspace colorspace, SDL_PropertiesID props);

    /// <summary>
    /// Premultiply the alpha in a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PremultiplySurfaceAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_PremultiplySurfaceAlpha(SDL_Surface* surface, [MarshalAs(UnmanagedType.U1)] bool linear);

    /// <summary>
    /// Perform a stretched pixel copy from one surface to another.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_StretchSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_StretchSurface(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect, SDL_ScaleMode scaleMode);

    /// <summary>
    /// Perform a tiled blit to a destination surface, which may be of a different format.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BlitSurfaceTiled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_BlitSurfaceTiled(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

    /// <summary>
    /// Perform a scaled and tiled blit to a destination surface, which may be of a different format.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BlitSurfaceTiledWithScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_BlitSurfaceTiledWithScale(SDL_Surface* src, SDL_Rect* srcrect, float scale, SDL_ScaleMode scaleMode, SDL_Surface* dst, SDL_Rect* dstrect);

    /// <summary>
    /// Perform a scaled blit using the 9-grid algorithm to a destination surface, which may be of a different format.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BlitSurface9Grid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_BlitSurface9Grid(SDL_Surface* src, SDL_Rect* srcrect, int left_width, int right_width, int top_height, int bottom_height, float scale, SDL_ScaleMode scaleMode, SDL_Surface* dst, SDL_Rect* dstrect);

    /// <summary>
    /// Map an RGB triple to an opaque pixel value for a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_MapSurfaceRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint SDL_MapSurfaceRGB(SDL_Surface* surface, byte r, byte g, byte b);

    /// <summary>
    /// Map an RGBA quadruple to a pixel value for a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_MapSurfaceRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint SDL_MapSurfaceRGBA(SDL_Surface* surface, byte r, byte g, byte b, byte a);

    /// <summary>
    /// Retrieves a single pixel from a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReadSurfacePixel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ReadSurfacePixel(SDL_Surface* surface, int x, int y, out byte r, out byte g, out byte b, out byte a);

    /// <summary>
    /// Retrieves a single pixel from a surface, with floating point precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReadSurfacePixelFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ReadSurfacePixelFloat(SDL_Surface* surface, int x, int y, out float r, out float g, out float b, out float a);

    /// <summary>
    /// Writes a single pixel to a surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WriteSurfacePixel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_WriteSurfacePixel(SDL_Surface* surface, int x, int y, byte r, byte g, byte b, byte a);

    /// <summary>
    /// Writes a single pixel to a surface, with floating point precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WriteSurfacePixelFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_WriteSurfacePixelFloat(SDL_Surface* surface, int x, int y, float r, float g, float b, float a);
}
