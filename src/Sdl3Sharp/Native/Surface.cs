using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static Sdl3Sharp.Native.Pixels;
using static Sdl3Sharp.Native.Properties;
using static Sdl3Sharp.Native.BlendMode;
using static Sdl3Sharp.Native.Rect;
using static Sdl3Sharp.Native.IOStream;
using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_surface.h - Surface creation and manipulation.
/// </summary>
public static unsafe partial class Surface
{
    /// <summary>
    /// The flags on an SDL_Surface.
    /// </summary>
    [Flags]
    public enum SDL_SurfaceFlags : uint
    {
        /// <summary>
        /// Surface uses preallocated pixel memory.
        /// </summary>
        SDL_SURFACE_PREALLOCATED = 0x00000001u,

        /// <summary>
        /// Surface needs to be locked to access pixels.
        /// </summary>
        SDL_SURFACE_LOCK_NEEDED = 0x00000002u,

        /// <summary>
        /// Surface is currently locked.
        /// </summary>
        SDL_SURFACE_LOCKED = 0x00000004u,

        /// <summary>
        /// Surface uses pixel memory allocated with SDL_aligned_alloc().
        /// </summary>
        SDL_SURFACE_SIMD_ALIGNED = 0x00000008u
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
        SDL_SCALEMODE_LINEAR
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
        SDL_FLIP_VERTICAL
    }

    /// <summary>
    /// A collection of pixels used in software blitting.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Surface
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
        /// <summary>A pointer to the pixels of the surface, the pixels are writeable if non-NULL.</summary>
        public void* pixels;
        /// <summary>Application reference count, used when freeing surface.</summary>
        public int refcount;
        /// <summary>Reserved for internal use.</summary>
        public void* reserved;
    }

    /// <summary>
    /// Property name for the SDR white point value of a surface.
    /// </summary>
    public const string SDL_PROP_SURFACE_SDR_WHITE_POINT_FLOAT = "SDL.surface.SDR_white_point";

    /// <summary>
    /// Property name for the HDR headroom value of a surface.
    /// </summary>
    public const string SDL_PROP_SURFACE_HDR_HEADROOM_FLOAT = "SDL.surface.HDR_headroom";

    /// <summary>
    /// Property name for the tonemap operator of a surface.
    /// </summary>
    public const string SDL_PROP_SURFACE_TONEMAP_OPERATOR_STRING = "SDL.surface.tonemap";

    /// <summary>
    /// Property name for the hotspot X coordinate of a surface.
    /// </summary>
    public const string SDL_PROP_SURFACE_HOTSPOT_X_NUMBER = "SDL.surface.hotspot.x";

    /// <summary>
    /// Property name for the hotspot Y coordinate of a surface.
    /// </summary>
    public const string SDL_PROP_SURFACE_HOTSPOT_Y_NUMBER = "SDL.surface.hotspot.y";

    /// <summary>
    /// Allocate a new surface with a specific pixel format.
    /// </summary>
    /// <param name="width">The width of the surface.</param>
    /// <param name="height">The height of the surface.</param>
    /// <param name="format">The SDL_PixelFormat for the new surface's pixel format.</param>
    /// <returns>The new SDL_Surface structure or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Surface* SDL_CreateSurface(int width, int height, SDL_PixelFormat format);

    /// <summary>
    /// Allocate a new surface with a specific pixel format and existing pixel data.
    /// </summary>
    /// <param name="width">The width of the surface.</param>
    /// <param name="height">The height of the surface.</param>
    /// <param name="format">The SDL_PixelFormat for the new surface's pixel format.</param>
    /// <param name="pixels">A pointer to existing pixel data.</param>
    /// <param name="pitch">The number of bytes between each row, including padding.</param>
    /// <returns>The new SDL_Surface structure or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreateSurfaceFrom")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Surface* SDL_CreateSurfaceFrom(int width, int height, SDL_PixelFormat format, void* pixels, int pitch);

    /// <summary>
    /// Free a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface to free.</param>
    [LibraryImport(Sdl3, EntryPoint = "SDL_DestroySurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroySurface(SDL_Surface* surface);

    /// <summary>
    /// Get the properties associated with a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to query.</param>
    /// <returns>A valid property ID on success or 0 on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetSurfaceProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetSurfaceProperties(SDL_Surface* surface);

    /// <summary>
    /// Set the colorspace used by a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to update.</param>
    /// <param name="colorspace">An SDL_Colorspace value describing the surface colorspace.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetSurfaceColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetSurfaceColorspace(SDL_Surface* surface, SDL_Colorspace colorspace);

    /// <summary>
    /// Get the colorspace used by a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to query.</param>
    /// <returns>The colorspace used by the surface.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetSurfaceColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Colorspace SDL_GetSurfaceColorspace(SDL_Surface* surface);

    /// <summary>
    /// Create a palette and associate it with a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to update.</param>
    /// <returns>A new SDL_Palette structure or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreateSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Palette* SDL_CreateSurfacePalette(SDL_Surface* surface);

    /// <summary>
    /// Set the palette used by a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to update.</param>
    /// <param name="palette">The SDL_Palette structure to use.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetSurfacePalette(SDL_Surface* surface, SDL_Palette* palette);

    /// <summary>
    /// Get the palette used by a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to query.</param>
    /// <returns>A pointer to the palette used by the surface, or NULL.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Palette* SDL_GetSurfacePalette(SDL_Surface* surface);

    /// <summary>
    /// Add an alternate version of a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to update.</param>
    /// <param name="image">A pointer to an alternate SDL_Surface to associate with this surface.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_AddSurfaceAlternateImage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_AddSurfaceAlternateImage(SDL_Surface* surface, SDL_Surface* image);

    /// <summary>
    /// Return whether a surface has alternate versions available.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to query.</param>
    /// <returns>True if alternate versions are available or false otherwise.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SurfaceHasAlternateImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SurfaceHasAlternateImages(SDL_Surface* surface);

    /// <summary>
    /// Get an array including all versions of a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to query.</param>
    /// <param name="count">A pointer filled in with the number of surface pointers returned.</param>
    /// <returns>A NULL terminated array of SDL_Surface pointers or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetSurfaceImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Surface** SDL_GetSurfaceImages(SDL_Surface* surface, int* count);

    /// <summary>
    /// Remove all alternate versions of a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to update.</param>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RemoveSurfaceAlternateImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_RemoveSurfaceAlternateImages(SDL_Surface* surface);

    /// <summary>
    /// Set up a surface for directly accessing the pixels.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to be locked.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_LockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_LockSurface(SDL_Surface* surface);

    /// <summary>
    /// Release a surface after directly accessing the pixels.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to be unlocked.</param>
    [LibraryImport(Sdl3, EntryPoint = "SDL_UnlockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnlockSurface(SDL_Surface* surface);

    /// <summary>
    /// Load a BMP image from a seekable SDL data stream.
    /// </summary>
    /// <param name="src">The data stream for the surface.</param>
    /// <param name="closeio">If true, calls SDL_CloseIO() on src before returning.</param>
    /// <returns>A pointer to a new SDL_Surface structure or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_LoadBMP_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Surface* SDL_LoadBMP_IO(SDL_IOStream* src, [MarshalAs(UnmanagedType.U1)] bool closeio);

    /// <summary>
    /// Load a BMP image from a file.
    /// </summary>
    /// <param name="file">The BMP file to load.</param>
    /// <returns>A pointer to a new SDL_Surface structure or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_LoadBMP", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Surface* SDL_LoadBMP(string file);

    /// <summary>
    /// Save a surface to a seekable SDL data stream in BMP format.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure containing the image to be saved.</param>
    /// <param name="dst">A data stream to save to.</param>
    /// <param name="closeio">If true, calls SDL_CloseIO() on dst before returning.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SaveBMP_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SaveBMP_IO(SDL_Surface* surface, SDL_IOStream* dst, [MarshalAs(UnmanagedType.U1)] bool closeio);

    /// <summary>
    /// Save a surface to a file.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure containing the image to be saved.</param>
    /// <param name="file">A file to save to.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SaveBMP", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SaveBMP(SDL_Surface* surface, string file);

    /// <summary>
    /// Set the RLE acceleration hint for a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to optimize.</param>
    /// <param name="enabled">True to enable RLE acceleration, false to disable it.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetSurfaceRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetSurfaceRLE(SDL_Surface* surface, [MarshalAs(UnmanagedType.U1)] bool enabled);

    /// <summary>
    /// Returns whether the surface is RLE enabled.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to query.</param>
    /// <returns>True if the surface is RLE enabled, false otherwise.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SurfaceHasRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SurfaceHasRLE(SDL_Surface* surface);

    /// <summary>
    /// Set the color key (transparent pixel) in a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to update.</param>
    /// <param name="enabled">True to enable color key, false to disable color key.</param>
    /// <param name="key">The transparent pixel.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetSurfaceColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetSurfaceColorKey(SDL_Surface* surface, [MarshalAs(UnmanagedType.U1)] bool enabled, uint key);

    /// <summary>
    /// Returns whether the surface has a color key.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to query.</param>
    /// <returns>True if the surface has a color key, false otherwise.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SurfaceHasColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SurfaceHasColorKey(SDL_Surface* surface);

    /// <summary>
    /// Get the color key (transparent pixel) for a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to query.</param>
    /// <param name="key">A pointer filled in with the transparent pixel.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetSurfaceColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetSurfaceColorKey(SDL_Surface* surface, uint* key);

    /// <summary>
    /// Set an additional color value multiplied into blit operations.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to update.</param>
    /// <param name="r">The red color value multiplied into blit operations.</param>
    /// <param name="g">The green color value multiplied into blit operations.</param>
    /// <param name="b">The blue color value multiplied into blit operations.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetSurfaceColorMod(SDL_Surface* surface, byte r, byte g, byte b);

    /// <summary>
    /// Get the additional color value multiplied into blit operations.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to query.</param>
    /// <param name="r">A pointer filled in with the current red color value.</param>
    /// <param name="g">A pointer filled in with the current green color value.</param>
    /// <param name="b">A pointer filled in with the current blue color value.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetSurfaceColorMod(SDL_Surface* surface, byte* r, byte* g, byte* b);

    /// <summary>
    /// Set an additional alpha value used in blit operations.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to update.</param>
    /// <param name="alpha">The alpha value multiplied into blit operations.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetSurfaceAlphaMod(SDL_Surface* surface, byte alpha);

    /// <summary>
    /// Get the additional alpha value used in blit operations.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to query.</param>
    /// <param name="alpha">A pointer filled in with the current alpha value.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetSurfaceAlphaMod(SDL_Surface* surface, byte* alpha);

    /// <summary>
    /// Set the blend mode used for blit operations.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to update.</param>
    /// <param name="blendMode">The SDL_BlendMode to use for blit blending.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetSurfaceBlendMode(SDL_Surface* surface, SDL_BlendMode blendMode);

    /// <summary>
    /// Get the blend mode used for blit operations.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to query.</param>
    /// <param name="blendMode">A pointer filled in with the current SDL_BlendMode.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetSurfaceBlendMode(SDL_Surface* surface, SDL_BlendMode* blendMode);

    /// <summary>
    /// Set the clipping rectangle for a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure to be clipped.</param>
    /// <param name="rect">The SDL_Rect structure representing the clipping rectangle, or NULL to disable clipping.</param>
    /// <returns>True if the rectangle intersects the surface, otherwise false.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetSurfaceClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetSurfaceClipRect(SDL_Surface* surface, SDL_Rect* rect);

    /// <summary>
    /// Get the clipping rectangle for a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure representing the surface to be clipped.</param>
    /// <param name="rect">An SDL_Rect structure filled in with the clipping rectangle for the surface.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetSurfaceClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetSurfaceClipRect(SDL_Surface* surface, SDL_Rect* rect);

    /// <summary>
    /// Flip a surface vertically or horizontally.
    /// </summary>
    /// <param name="surface">The surface to flip.</param>
    /// <param name="flip">The direction to flip.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_FlipSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_FlipSurface(SDL_Surface* surface, SDL_FlipMode flip);

    /// <summary>
    /// Creates a new surface identical to the existing surface.
    /// </summary>
    /// <param name="surface">The surface to duplicate.</param>
    /// <returns>A copy of the surface or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_DuplicateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Surface* SDL_DuplicateSurface(SDL_Surface* surface);

    /// <summary>
    /// Creates a new surface identical to the existing surface, scaled to the desired size.
    /// </summary>
    /// <param name="surface">The surface to duplicate and scale.</param>
    /// <param name="width">The width of the new surface.</param>
    /// <param name="height">The height of the new surface.</param>
    /// <param name="scaleMode">The SDL_ScaleMode to be used.</param>
    /// <returns>A copy of the surface or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_ScaleSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Surface* SDL_ScaleSurface(SDL_Surface* surface, int width, int height, SDL_ScaleMode scaleMode);

    /// <summary>
    /// Copy an existing surface to a new surface of the specified format.
    /// </summary>
    /// <param name="surface">The existing SDL_Surface structure to convert.</param>
    /// <param name="format">The new pixel format.</param>
    /// <returns>The new SDL_Surface structure or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_ConvertSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Surface* SDL_ConvertSurface(SDL_Surface* surface, SDL_PixelFormat format);

    /// <summary>
    /// Copy an existing surface to a new surface of the specified format and colorspace.
    /// </summary>
    /// <param name="surface">The existing SDL_Surface structure to convert.</param>
    /// <param name="format">The new pixel format.</param>
    /// <param name="palette">An optional palette to use for indexed formats.</param>
    /// <param name="colorspace">The new colorspace.</param>
    /// <param name="props">An SDL_PropertiesID with additional color properties, or 0.</param>
    /// <returns>The new SDL_Surface structure or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_ConvertSurfaceAndColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Surface* SDL_ConvertSurfaceAndColorspace(SDL_Surface* surface, SDL_PixelFormat format, SDL_Palette* palette, SDL_Colorspace colorspace, SDL_PropertiesID props);

    /// <summary>
    /// Copy a block of pixels of one format to another format.
    /// </summary>
    /// <param name="width">The width of the block to copy, in pixels.</param>
    /// <param name="height">The height of the block to copy, in pixels.</param>
    /// <param name="src_format">An SDL_PixelFormat value of the src pixels format.</param>
    /// <param name="src">A pointer to the source pixels.</param>
    /// <param name="src_pitch">The pitch of the source pixels, in bytes.</param>
    /// <param name="dst_format">An SDL_PixelFormat value of the dst pixels format.</param>
    /// <param name="dst">A pointer to be filled in with new pixel data.</param>
    /// <param name="dst_pitch">The pitch of the destination pixels, in bytes.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_ConvertPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ConvertPixels(int width, int height, SDL_PixelFormat src_format, void* src, int src_pitch, SDL_PixelFormat dst_format, void* dst, int dst_pitch);

    /// <summary>
    /// Copy a block of pixels of one format and colorspace to another format and colorspace.
    /// </summary>
    /// <param name="width">The width of the block to copy, in pixels.</param>
    /// <param name="height">The height of the block to copy, in pixels.</param>
    /// <param name="src_format">An SDL_PixelFormat value of the src pixels format.</param>
    /// <param name="src_colorspace">An SDL_Colorspace value describing the colorspace of the src pixels.</param>
    /// <param name="src_properties">An SDL_PropertiesID with additional source color properties, or 0.</param>
    /// <param name="src">A pointer to the source pixels.</param>
    /// <param name="src_pitch">The pitch of the source pixels, in bytes.</param>
    /// <param name="dst_format">An SDL_PixelFormat value of the dst pixels format.</param>
    /// <param name="dst_colorspace">An SDL_Colorspace value describing the colorspace of the dst pixels.</param>
    /// <param name="dst_properties">An SDL_PropertiesID with additional destination color properties, or 0.</param>
    /// <param name="dst">A pointer to be filled in with new pixel data.</param>
    /// <param name="dst_pitch">The pitch of the destination pixels, in bytes.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_ConvertPixelsAndColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ConvertPixelsAndColorspace(int width, int height, SDL_PixelFormat src_format, SDL_Colorspace src_colorspace, SDL_PropertiesID src_properties, void* src, int src_pitch, SDL_PixelFormat dst_format, SDL_Colorspace dst_colorspace, SDL_PropertiesID dst_properties, void* dst, int dst_pitch);

    /// <summary>
    /// Premultiply the alpha on a block of pixels.
    /// </summary>
    /// <param name="width">The width of the block to convert, in pixels.</param>
    /// <param name="height">The height of the block to convert, in pixels.</param>
    /// <param name="src_format">An SDL_PixelFormat value of the src pixels format.</param>
    /// <param name="src">A pointer to the source pixels.</param>
    /// <param name="src_pitch">The pitch of the source pixels, in bytes.</param>
    /// <param name="dst_format">An SDL_PixelFormat value of the dst pixels format.</param>
    /// <param name="dst">A pointer to be filled in with premultiplied pixel data.</param>
    /// <param name="dst_pitch">The pitch of the destination pixels, in bytes.</param>
    /// <param name="linear">True to convert from sRGB to linear space for the alpha multiplication.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_PremultiplyAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_PremultiplyAlpha(int width, int height, SDL_PixelFormat src_format, void* src, int src_pitch, SDL_PixelFormat dst_format, void* dst, int dst_pitch, [MarshalAs(UnmanagedType.U1)] bool linear);

    /// <summary>
    /// Premultiply the alpha in a surface.
    /// </summary>
    /// <param name="surface">The surface to modify.</param>
    /// <param name="linear">True to convert from sRGB to linear space for the alpha multiplication.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_PremultiplySurfaceAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_PremultiplySurfaceAlpha(SDL_Surface* surface, [MarshalAs(UnmanagedType.U1)] bool linear);

    /// <summary>
    /// Clear a surface with a specific color, with floating point precision.
    /// </summary>
    /// <param name="surface">The SDL_Surface to clear.</param>
    /// <param name="r">The red component of the pixel, normally in the range 0-1.</param>
    /// <param name="g">The green component of the pixel, normally in the range 0-1.</param>
    /// <param name="b">The blue component of the pixel, normally in the range 0-1.</param>
    /// <param name="a">The alpha component of the pixel, normally in the range 0-1.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_ClearSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ClearSurface(SDL_Surface* surface, float r, float g, float b, float a);

    /// <summary>
    /// Perform a fast fill of a rectangle with a specific color.
    /// </summary>
    /// <param name="dst">The SDL_Surface structure that is the drawing target.</param>
    /// <param name="rect">The SDL_Rect structure representing the rectangle to fill, or NULL to fill the entire surface.</param>
    /// <param name="color">The color to fill with.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_FillSurfaceRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_FillSurfaceRect(SDL_Surface* dst, SDL_Rect* rect, uint color);

    /// <summary>
    /// Perform a fast fill of a set of rectangles with a specific color.
    /// </summary>
    /// <param name="dst">The SDL_Surface structure that is the drawing target.</param>
    /// <param name="rects">An array of SDL_Rects representing the rectangles to fill.</param>
    /// <param name="count">The number of rectangles in the array.</param>
    /// <param name="color">The color to fill with.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_FillSurfaceRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_FillSurfaceRects(SDL_Surface* dst, SDL_Rect* rects, int count, uint color);

    /// <summary>
    /// Performs a fast blit from the source surface to the destination surface with clipping.
    /// </summary>
    /// <param name="src">The SDL_Surface structure to be copied from.</param>
    /// <param name="srcrect">The SDL_Rect structure representing the rectangle to be copied, or NULL to copy the entire surface.</param>
    /// <param name="dst">The SDL_Surface structure that is the blit target.</param>
    /// <param name="dstrect">The SDL_Rect structure representing the x and y position in the destination surface.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_BlitSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_BlitSurface(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

    /// <summary>
    /// Perform low-level surface blitting only.
    /// </summary>
    /// <param name="src">The SDL_Surface structure to be copied from.</param>
    /// <param name="srcrect">The SDL_Rect structure representing the rectangle to be copied.</param>
    /// <param name="dst">The SDL_Surface structure that is the blit target.</param>
    /// <param name="dstrect">The SDL_Rect structure representing the target rectangle in the destination surface.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_BlitSurfaceUnchecked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_BlitSurfaceUnchecked(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

    /// <summary>
    /// Perform a scaled blit to a destination surface, which may be of a different format.
    /// </summary>
    /// <param name="src">The SDL_Surface structure to be copied from.</param>
    /// <param name="srcrect">The SDL_Rect structure representing the rectangle to be copied, or NULL to copy the entire surface.</param>
    /// <param name="dst">The SDL_Surface structure that is the blit target.</param>
    /// <param name="dstrect">The SDL_Rect structure representing the target rectangle in the destination surface.</param>
    /// <param name="scaleMode">The SDL_ScaleMode to be used.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_BlitSurfaceScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_BlitSurfaceScaled(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect, SDL_ScaleMode scaleMode);

    /// <summary>
    /// Perform low-level surface scaled blitting only.
    /// </summary>
    /// <param name="src">The SDL_Surface structure to be copied from.</param>
    /// <param name="srcrect">The SDL_Rect structure representing the rectangle to be copied.</param>
    /// <param name="dst">The SDL_Surface structure that is the blit target.</param>
    /// <param name="dstrect">The SDL_Rect structure representing the target rectangle in the destination surface.</param>
    /// <param name="scaleMode">The SDL_ScaleMode to be used.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_BlitSurfaceUncheckedScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_BlitSurfaceUncheckedScaled(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect, SDL_ScaleMode scaleMode);

    /// <summary>
    /// Perform a stretched pixel copy from one surface to another.
    /// </summary>
    /// <param name="src">The SDL_Surface structure to be copied from.</param>
    /// <param name="srcrect">The SDL_Rect structure representing the rectangle to be copied, or NULL to copy the entire surface.</param>
    /// <param name="dst">The SDL_Surface structure that is the blit target.</param>
    /// <param name="dstrect">The SDL_Rect structure representing the target rectangle in the destination surface.</param>
    /// <param name="scaleMode">The SDL_ScaleMode to be used.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_StretchSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_StretchSurface(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect, SDL_ScaleMode scaleMode);

    /// <summary>
    /// Perform a tiled blit to a destination surface, which may be of a different format.
    /// </summary>
    /// <param name="src">The SDL_Surface structure to be copied from.</param>
    /// <param name="srcrect">The SDL_Rect structure representing the rectangle to be copied, or NULL to copy the entire surface.</param>
    /// <param name="dst">The SDL_Surface structure that is the blit target.</param>
    /// <param name="dstrect">The SDL_Rect structure representing the target rectangle in the destination surface.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_BlitSurfaceTiled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_BlitSurfaceTiled(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

    /// <summary>
    /// Perform a scaled and tiled blit to a destination surface, which may be of a different format.
    /// </summary>
    /// <param name="src">The SDL_Surface structure to be copied from.</param>
    /// <param name="srcrect">The SDL_Rect structure representing the rectangle to be copied, or NULL to copy the entire surface.</param>
    /// <param name="scale">The scale used to transform srcrect into the destination rectangle.</param>
    /// <param name="scaleMode">Scale algorithm to be used.</param>
    /// <param name="dst">The SDL_Surface structure that is the blit target.</param>
    /// <param name="dstrect">The SDL_Rect structure representing the target rectangle in the destination surface.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_BlitSurfaceTiledWithScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_BlitSurfaceTiledWithScale(SDL_Surface* src, SDL_Rect* srcrect, float scale, SDL_ScaleMode scaleMode, SDL_Surface* dst, SDL_Rect* dstrect);

    /// <summary>
    /// Perform a scaled blit using the 9-grid algorithm to a destination surface.
    /// </summary>
    /// <param name="src">The SDL_Surface structure to be copied from.</param>
    /// <param name="srcrect">The SDL_Rect structure representing the rectangle to be used for the 9-grid.</param>
    /// <param name="left_width">The width, in pixels, of the left corners in srcrect.</param>
    /// <param name="right_width">The width, in pixels, of the right corners in srcrect.</param>
    /// <param name="top_height">The height, in pixels, of the top corners in srcrect.</param>
    /// <param name="bottom_height">The height, in pixels, of the bottom corners in srcrect.</param>
    /// <param name="scale">The scale used to transform the corner of srcrect into the corner of dstrect.</param>
    /// <param name="scaleMode">Scale algorithm to be used.</param>
    /// <param name="dst">The SDL_Surface structure that is the blit target.</param>
    /// <param name="dstrect">The SDL_Rect structure representing the target rectangle in the destination surface.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_BlitSurface9Grid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_BlitSurface9Grid(SDL_Surface* src, SDL_Rect* srcrect, int left_width, int right_width, int top_height, int bottom_height, float scale, SDL_ScaleMode scaleMode, SDL_Surface* dst, SDL_Rect* dstrect);

    /// <summary>
    /// Map an RGB triple to an opaque pixel value for a surface.
    /// </summary>
    /// <param name="surface">The surface to use for the pixel format and palette.</param>
    /// <param name="r">The red component of the pixel in the range 0-255.</param>
    /// <param name="g">The green component of the pixel in the range 0-255.</param>
    /// <param name="b">The blue component of the pixel in the range 0-255.</param>
    /// <returns>A pixel value.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_MapSurfaceRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_MapSurfaceRGB(SDL_Surface* surface, byte r, byte g, byte b);

    /// <summary>
    /// Map an RGBA quadruple to a pixel value for a surface.
    /// </summary>
    /// <param name="surface">The surface to use for the pixel format and palette.</param>
    /// <param name="r">The red component of the pixel in the range 0-255.</param>
    /// <param name="g">The green component of the pixel in the range 0-255.</param>
    /// <param name="b">The blue component of the pixel in the range 0-255.</param>
    /// <param name="a">The alpha component of the pixel in the range 0-255.</param>
    /// <returns>A pixel value.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_MapSurfaceRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_MapSurfaceRGBA(SDL_Surface* surface, byte r, byte g, byte b, byte a);

    /// <summary>
    /// Retrieves a single pixel from a surface.
    /// </summary>
    /// <param name="surface">The surface to read.</param>
    /// <param name="x">The horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">The vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="r">A pointer filled in with the red channel, 0-255, or NULL to ignore this channel.</param>
    /// <param name="g">A pointer filled in with the green channel, 0-255, or NULL to ignore this channel.</param>
    /// <param name="b">A pointer filled in with the blue channel, 0-255, or NULL to ignore this channel.</param>
    /// <param name="a">A pointer filled in with the alpha channel, 0-255, or NULL to ignore this channel.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_ReadSurfacePixel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadSurfacePixel(SDL_Surface* surface, int x, int y, byte* r, byte* g, byte* b, byte* a);

    /// <summary>
    /// Retrieves a single pixel from a surface.
    /// </summary>
    /// <param name="surface">The surface to read.</param>
    /// <param name="x">The horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">The vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="r">A pointer filled in with the red channel, normally in the range 0-1, or NULL to ignore this channel.</param>
    /// <param name="g">A pointer filled in with the green channel, normally in the range 0-1, or NULL to ignore this channel.</param>
    /// <param name="b">A pointer filled in with the blue channel, normally in the range 0-1, or NULL to ignore this channel.</param>
    /// <param name="a">A pointer filled in with the alpha channel, normally in the range 0-1, or NULL to ignore this channel.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_ReadSurfacePixelFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ReadSurfacePixelFloat(SDL_Surface* surface, int x, int y, float* r, float* g, float* b, float* a);

    /// <summary>
    /// Writes a single pixel to a surface.
    /// </summary>
    /// <param name="surface">The surface to write.</param>
    /// <param name="x">The horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">The vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="r">The red channel value, 0-255.</param>
    /// <param name="g">The green channel value, 0-255.</param>
    /// <param name="b">The blue channel value, 0-255.</param>
    /// <param name="a">The alpha channel value, 0-255.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_WriteSurfacePixel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteSurfacePixel(SDL_Surface* surface, int x, int y, byte r, byte g, byte b, byte a);

    /// <summary>
    /// Writes a single pixel to a surface.
    /// </summary>
    /// <param name="surface">The surface to write.</param>
    /// <param name="x">The horizontal coordinate, 0 &lt;= x &lt; width.</param>
    /// <param name="y">The vertical coordinate, 0 &lt;= y &lt; height.</param>
    /// <param name="r">The red channel value, normally in the range 0-1.</param>
    /// <param name="g">The green channel value, normally in the range 0-1.</param>
    /// <param name="b">The blue channel value, normally in the range 0-1.</param>
    /// <param name="a">The alpha channel value, normally in the range 0-1.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_WriteSurfacePixelFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WriteSurfacePixelFloat(SDL_Surface* surface, int x, int y, float r, float g, float b, float a);
}
