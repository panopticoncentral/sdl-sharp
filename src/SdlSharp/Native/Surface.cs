using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: IO stream variants (SDL_LoadBMP_IO, SDL_SaveBMP_IO, SDL_LoadSurface_IO, SDL_LoadPNG_IO, SDL_SavePNG_IO),
// palette functions (SDL_CreateSurfacePalette, SDL_SetSurfacePalette, SDL_GetSurfacePalette),
// alternate image functions (SDL_AddSurfaceAlternateImage, SDL_SurfaceHasAlternateImages, SDL_GetSurfaceImages, SDL_RemoveSurfaceAlternateImages),
// pixel read/write (SDL_ReadSurfacePixel, SDL_ReadSurfacePixelFloat, SDL_WriteSurfacePixel, SDL_WriteSurfacePixelFloat),
// advanced blit variants (SDL_BlitSurfaceUnchecked, SDL_BlitSurfaceUncheckedScaled, SDL_BlitSurfaceTiled, SDL_BlitSurfaceTiledWithScale, SDL_BlitSurface9Grid, SDL_StretchSurface),
// rotate/scale (SDL_RotateSurface, SDL_ScaleSurface), flip (SDL_FlipSurface),
// RLE (SDL_SetSurfaceRLE, SDL_SurfaceHasRLE), color key (SDL_SetSurfaceColorKey, SDL_SurfaceHasColorKey, SDL_GetSurfaceColorKey),
// premultiply alpha (SDL_PremultiplySurfaceAlpha, SDL_PremultiplyAlpha),
// pixel conversion (SDL_ConvertPixels, SDL_ConvertPixelsAndColorspace, SDL_ConvertSurfaceAndColorspace),
// map/get (SDL_MapSurfaceRGB, SDL_MapSurfaceRGBA, SDL_GetSurfaceMapRGB, SDL_GetSurfaceMapRGBA),
// PNG (SDL_LoadPNG, SDL_SavePNG), generic load (SDL_LoadSurface).

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
}
