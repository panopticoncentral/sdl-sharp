using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_pixels.h - Pixel format and color space definitions.
/// </summary>
public static unsafe partial class Pixels
{
    /// <summary>
    /// A fully opaque 8-bit alpha value.
    /// </summary>
    public const byte SDL_ALPHA_OPAQUE = 255;

    /// <summary>
    /// A fully opaque floating point alpha value.
    /// </summary>
    public const float SDL_ALPHA_OPAQUE_FLOAT = 1.0f;

    /// <summary>
    /// A fully transparent 8-bit alpha value.
    /// </summary>
    public const byte SDL_ALPHA_TRANSPARENT = 0;

    /// <summary>
    /// A fully transparent floating point alpha value.
    /// </summary>
    public const float SDL_ALPHA_TRANSPARENT_FLOAT = 0.0f;

    /// <summary>
    /// Pixel type.
    /// </summary>
    public enum SDL_PixelType
    {
        /// <summary>Unknown pixel type.</summary>
        SDL_PIXELTYPE_UNKNOWN,
        /// <summary>1-bit indexed pixel type.</summary>
        SDL_PIXELTYPE_INDEX1,
        /// <summary>4-bit indexed pixel type.</summary>
        SDL_PIXELTYPE_INDEX4,
        /// <summary>8-bit indexed pixel type.</summary>
        SDL_PIXELTYPE_INDEX8,
        /// <summary>8-bit packed pixel type.</summary>
        SDL_PIXELTYPE_PACKED8,
        /// <summary>16-bit packed pixel type.</summary>
        SDL_PIXELTYPE_PACKED16,
        /// <summary>32-bit packed pixel type.</summary>
        SDL_PIXELTYPE_PACKED32,
        /// <summary>8-bit array pixel type.</summary>
        SDL_PIXELTYPE_ARRAYU8,
        /// <summary>16-bit array pixel type.</summary>
        SDL_PIXELTYPE_ARRAYU16,
        /// <summary>32-bit array pixel type.</summary>
        SDL_PIXELTYPE_ARRAYU32,
        /// <summary>16-bit floating point array pixel type.</summary>
        SDL_PIXELTYPE_ARRAYF16,
        /// <summary>32-bit floating point array pixel type.</summary>
        SDL_PIXELTYPE_ARRAYF32,
        /// <summary>2-bit indexed pixel type (appended for SDL2 compatibility).</summary>
        SDL_PIXELTYPE_INDEX2
    }

    /// <summary>
    /// Bitmap pixel order, high bit -> low bit.
    /// </summary>
    public enum SDL_BitmapOrder
    {
        /// <summary>No specific bitmap order.</summary>
        SDL_BITMAPORDER_NONE,
        /// <summary>Bitmap order 4321 (high bit to low bit).</summary>
        SDL_BITMAPORDER_4321,
        /// <summary>Bitmap order 1234 (high bit to low bit).</summary>
        SDL_BITMAPORDER_1234
    }

    /// <summary>
    /// Packed component order, high bit -> low bit.
    /// </summary>
    public enum SDL_PackedOrder
    {
        /// <summary>No specific packed order.</summary>
        SDL_PACKEDORDER_NONE,
        /// <summary>Packed order XRGB (unused, red, green, blue).</summary>
        SDL_PACKEDORDER_XRGB,
        /// <summary>Packed order RGBX (red, green, blue, unused).</summary>
        SDL_PACKEDORDER_RGBX,
        /// <summary>Packed order ARGB (alpha, red, green, blue).</summary>
        SDL_PACKEDORDER_ARGB,
        /// <summary>Packed order RGBA (red, green, blue, alpha).</summary>
        SDL_PACKEDORDER_RGBA,
        /// <summary>Packed order XBGR (unused, blue, green, red).</summary>
        SDL_PACKEDORDER_XBGR,
        /// <summary>Packed order BGRX (blue, green, red, unused).</summary>
        SDL_PACKEDORDER_BGRX,
        /// <summary>Packed order ABGR (alpha, blue, green, red).</summary>
        SDL_PACKEDORDER_ABGR,
        /// <summary>Packed order BGRA (blue, green, red, alpha).</summary>
        SDL_PACKEDORDER_BGRA
    }

    /// <summary>
    /// Array component order, low byte -> high byte.
    /// </summary>
    public enum SDL_ArrayOrder
    {
        /// <summary>No specific array order.</summary>
        SDL_ARRAYORDER_NONE,
        /// <summary>Array order RGB (red, green, blue).</summary>
        SDL_ARRAYORDER_RGB,
        /// <summary>Array order RGBA (red, green, blue, alpha).</summary>
        SDL_ARRAYORDER_RGBA,
        /// <summary>Array order ARGB (alpha, red, green, blue).</summary>
        SDL_ARRAYORDER_ARGB,
        /// <summary>Array order BGR (blue, green, red).</summary>
        SDL_ARRAYORDER_BGR,
        /// <summary>Array order BGRA (blue, green, red, alpha).</summary>
        SDL_ARRAYORDER_BGRA,
        /// <summary>Array order ABGR (alpha, blue, green, red).</summary>
        SDL_ARRAYORDER_ABGR
    }

    /// <summary>
    /// Packed component layout.
    /// </summary>
    public enum SDL_PackedLayout
    {
        /// <summary>No specific packed layout.</summary>
        SDL_PACKEDLAYOUT_NONE,
        /// <summary>Packed layout 332 (3 bits red, 3 bits green, 2 bits blue).</summary>
        SDL_PACKEDLAYOUT_332,
        /// <summary>Packed layout 4444 (4 bits per component).</summary>
        SDL_PACKEDLAYOUT_4444,
        /// <summary>Packed layout 1555 (1 bit alpha, 5 bits per color component).</summary>
        SDL_PACKEDLAYOUT_1555,
        /// <summary>Packed layout 5551 (5 bits per color component, 1 bit alpha).</summary>
        SDL_PACKEDLAYOUT_5551,
        /// <summary>Packed layout 565 (5 bits red, 6 bits green, 5 bits blue).</summary>
        SDL_PACKEDLAYOUT_565,
        /// <summary>Packed layout 8888 (8 bits per component).</summary>
        SDL_PACKEDLAYOUT_8888,
        /// <summary>Packed layout 2101010 (2 bits alpha, 10 bits per color component).</summary>
        SDL_PACKEDLAYOUT_2101010,
        /// <summary>Packed layout 1010102 (10 bits per color component, 2 bits alpha).</summary>
        SDL_PACKEDLAYOUT_1010102
    }

    /// <summary>
    /// Defining custom FourCC pixel formats.
    /// </summary>
    /// <param name="A">the first character of the FourCC code.</param>
    /// <param name="B">the second character of the FourCC code.</param>
    /// <param name="C">the third character of the FourCC code.</param>
    /// <param name="D">the fourth character of the FourCC code.</param>
    /// <returns>a format value in the style of SDL_PixelFormat.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SDL_PixelFormat SDL_DEFINE_PIXELFOURCC(byte A, byte B, byte C, byte D)
    {
        return (SDL_PixelFormat)(((uint)A << 0) | ((uint)B << 8) | ((uint)C << 16) | ((uint)D << 24));
    }

    /// <summary>
    /// Defining custom non-FourCC pixel formats.
    /// </summary>
    /// <param name="type">the type of the new format, probably a SDL_PixelType value.</param>
    /// <param name="order">the order of the new format, probably a SDL_BitmapOrder, SDL_PackedOrder, or SDL_ArrayOrder value.</param>
    /// <param name="layout">the layout of the new format, probably an SDL_PackedLayout value or zero.</param>
    /// <param name="bits">the number of bits per pixel of the new format.</param>
    /// <param name="bytes">the number of bytes per pixel of the new format.</param>
    /// <returns>a format value in the style of SDL_PixelFormat.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SDL_PixelFormat SDL_DEFINE_PIXELFORMAT(int type, int order, int layout, int bits, int bytes)
    {
        return (SDL_PixelFormat)((1u << 28) | ((uint)type << 24) | ((uint)order << 20) | ((uint)layout << 16) | ((uint)bits << 8) | ((uint)bytes << 0));
    }

    /// <summary>
    /// Retrieve the flags of an SDL_PixelFormat.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>the flags of format.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SDL_PIXELFLAG(SDL_PixelFormat format)
    {
        return ((uint)format >> 28) & 0x0F;
    }

    /// <summary>
    /// Retrieve the type of an SDL_PixelFormat.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>the type of format.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SDL_PIXELTYPE(SDL_PixelFormat format)
    {
        return ((uint)format >> 24) & 0x0F;
    }

    /// <summary>
    /// Retrieve the order of an SDL_PixelFormat.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>the order of format.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SDL_PIXELORDER(SDL_PixelFormat format)
    {
        return ((uint)format >> 20) & 0x0F;
    }

    /// <summary>
    /// Retrieve the layout of an SDL_PixelFormat.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>the layout of format.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SDL_PIXELLAYOUT(SDL_PixelFormat format)
    {
        return ((uint)format >> 16) & 0x0F;
    }

    /// <summary>
    /// Determine an SDL_PixelFormat's bits per pixel.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>the bits-per-pixel of format.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SDL_BITSPERPIXEL(SDL_PixelFormat format)
    {
        return SDL_ISPIXELFORMAT_FOURCC(format) ? 0 : (((uint)format >> 8) & 0xFF);
    }

    /// <summary>
    /// Determine an SDL_PixelFormat's bytes per pixel.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>the bytes-per-pixel of format.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SDL_BYTESPERPIXEL(SDL_PixelFormat format)
    {
        return SDL_ISPIXELFORMAT_FOURCC(format) ?
            (format is SDL_PixelFormat.SDL_PIXELFORMAT_YUY2 or
             SDL_PixelFormat.SDL_PIXELFORMAT_UYVY or
             SDL_PixelFormat.SDL_PIXELFORMAT_YVYU or
             SDL_PixelFormat.SDL_PIXELFORMAT_P010 ? 2u : 1u) : (((uint)format >> 0) & 0xFF);
    }

    /// <summary>
    /// Determine if an SDL_PixelFormat is an indexed format.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>true if the format is indexed, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SDL_ISPIXELFORMAT_INDEXED(SDL_PixelFormat format)
    {
        var type = SDL_PIXELTYPE(format);
        return !SDL_ISPIXELFORMAT_FOURCC(format) &&
               ((type == (uint)SDL_PixelType.SDL_PIXELTYPE_INDEX1) ||
                (type == (uint)SDL_PixelType.SDL_PIXELTYPE_INDEX2) ||
                (type == (uint)SDL_PixelType.SDL_PIXELTYPE_INDEX4) ||
                (type == (uint)SDL_PixelType.SDL_PIXELTYPE_INDEX8));
    }

    /// <summary>
    /// Determine if an SDL_PixelFormat is a packed format.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>true if the format is packed, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SDL_ISPIXELFORMAT_PACKED(SDL_PixelFormat format)
    {
        var type = SDL_PIXELTYPE(format);
        return !SDL_ISPIXELFORMAT_FOURCC(format) &&
               ((type == (uint)SDL_PixelType.SDL_PIXELTYPE_PACKED8) ||
                (type == (uint)SDL_PixelType.SDL_PIXELTYPE_PACKED16) ||
                (type == (uint)SDL_PixelType.SDL_PIXELTYPE_PACKED32));
    }

    /// <summary>
    /// Determine if an SDL_PixelFormat is an array format.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>true if the format is an array, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SDL_ISPIXELFORMAT_ARRAY(SDL_PixelFormat format)
    {
        var type = SDL_PIXELTYPE(format);
        return !SDL_ISPIXELFORMAT_FOURCC(format) &&
               ((type == (uint)SDL_PixelType.SDL_PIXELTYPE_ARRAYU8) ||
                (type == (uint)SDL_PixelType.SDL_PIXELTYPE_ARRAYU16) ||
                (type == (uint)SDL_PixelType.SDL_PIXELTYPE_ARRAYU32) ||
                (type == (uint)SDL_PixelType.SDL_PIXELTYPE_ARRAYF16) ||
                (type == (uint)SDL_PixelType.SDL_PIXELTYPE_ARRAYF32));
    }

    /// <summary>
    /// Determine if an SDL_PixelFormat is a 10-bit format.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>true if the format is 10-bit, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SDL_ISPIXELFORMAT_10BIT(SDL_PixelFormat format)
    {
        return !SDL_ISPIXELFORMAT_FOURCC(format) &&
               (SDL_PIXELTYPE(format) == (uint)SDL_PixelType.SDL_PIXELTYPE_PACKED32) &&
               (SDL_PIXELLAYOUT(format) == (uint)SDL_PackedLayout.SDL_PACKEDLAYOUT_2101010);
    }

    /// <summary>
    /// Determine if an SDL_PixelFormat is a floating point format.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>true if the format is floating point, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SDL_ISPIXELFORMAT_FLOAT(SDL_PixelFormat format)
    {
        var type = SDL_PIXELTYPE(format);
        return !SDL_ISPIXELFORMAT_FOURCC(format) &&
               ((type == (uint)SDL_PixelType.SDL_PIXELTYPE_ARRAYF16) ||
                (type == (uint)SDL_PixelType.SDL_PIXELTYPE_ARRAYF32));
    }

    /// <summary>
    /// Determine if an SDL_PixelFormat has an alpha channel.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>true if the format has alpha, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SDL_ISPIXELFORMAT_ALPHA(SDL_PixelFormat format)
    {
        var order = SDL_PIXELORDER(format);
        return (SDL_ISPIXELFORMAT_PACKED(format) &&
                ((order == (uint)SDL_PackedOrder.SDL_PACKEDORDER_ARGB) ||
                 (order == (uint)SDL_PackedOrder.SDL_PACKEDORDER_RGBA) ||
                 (order == (uint)SDL_PackedOrder.SDL_PACKEDORDER_ABGR) ||
                 (order == (uint)SDL_PackedOrder.SDL_PACKEDORDER_BGRA))) ||
               (SDL_ISPIXELFORMAT_ARRAY(format) &&
                ((order == (uint)SDL_ArrayOrder.SDL_ARRAYORDER_ARGB) ||
                 (order == (uint)SDL_ArrayOrder.SDL_ARRAYORDER_RGBA) ||
                 (order == (uint)SDL_ArrayOrder.SDL_ARRAYORDER_ABGR) ||
                 (order == (uint)SDL_ArrayOrder.SDL_ARRAYORDER_BGRA)));
    }

    /// <summary>
    /// Determine if an SDL_PixelFormat is a "FourCC" format.
    /// </summary>
    /// <param name="format">an SDL_PixelFormat to check.</param>
    /// <returns>true if the format is FourCC, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SDL_ISPIXELFORMAT_FOURCC(SDL_PixelFormat format)
    {
        return (format != SDL_PixelFormat.SDL_PIXELFORMAT_UNKNOWN) && (SDL_PIXELFLAG(format) != 1);
    }

    /// <summary>
    /// Pixel format.
    /// </summary>
    public enum SDL_PixelFormat : uint
    {
        /// <summary>Unknown pixel format.</summary>
        SDL_PIXELFORMAT_UNKNOWN = 0,
        /// <summary>1-bit indexed, LSB (least significant bit) order.</summary>
        SDL_PIXELFORMAT_INDEX1LSB = 0x11100100u,
        /// <summary>1-bit indexed, MSB (most significant bit) order.</summary>
        SDL_PIXELFORMAT_INDEX1MSB = 0x11200100u,
        /// <summary>2-bit indexed, LSB order.</summary>
        SDL_PIXELFORMAT_INDEX2LSB = 0x1c100200u,
        /// <summary>2-bit indexed, MSB order.</summary>
        SDL_PIXELFORMAT_INDEX2MSB = 0x1c200200u,
        /// <summary>4-bit indexed, LSB order.</summary>
        SDL_PIXELFORMAT_INDEX4LSB = 0x12100400u,
        /// <summary>4-bit indexed, MSB order.</summary>
        SDL_PIXELFORMAT_INDEX4MSB = 0x12200400u,
        /// <summary>8-bit indexed.</summary>
        SDL_PIXELFORMAT_INDEX8 = 0x13000801u,
        /// <summary>8-bit RGB 3-3-2 format.</summary>
        SDL_PIXELFORMAT_RGB332 = 0x14110801u,
        /// <summary>16-bit XRGB 4-4-4-4 format.</summary>
        SDL_PIXELFORMAT_XRGB4444 = 0x15120c02u,
        /// <summary>16-bit XBGR 4-4-4-4 format.</summary>
        SDL_PIXELFORMAT_XBGR4444 = 0x15520c02u,
        /// <summary>16-bit XRGB 1-5-5-5 format.</summary>
        SDL_PIXELFORMAT_XRGB1555 = 0x15130f02u,
        /// <summary>16-bit XBGR 1-5-5-5 format.</summary>
        SDL_PIXELFORMAT_XBGR1555 = 0x15530f02u,
        /// <summary>16-bit ARGB 4-4-4-4 format.</summary>
        SDL_PIXELFORMAT_ARGB4444 = 0x15321002u,
        /// <summary>16-bit RGBA 4-4-4-4 format.</summary>
        SDL_PIXELFORMAT_RGBA4444 = 0x15421002u,
        /// <summary>16-bit ABGR 4-4-4-4 format.</summary>
        SDL_PIXELFORMAT_ABGR4444 = 0x15721002u,
        /// <summary>16-bit BGRA 4-4-4-4 format.</summary>
        SDL_PIXELFORMAT_BGRA4444 = 0x15821002u,
        /// <summary>16-bit ARGB 1-5-5-5 format.</summary>
        SDL_PIXELFORMAT_ARGB1555 = 0x15331002u,
        /// <summary>16-bit RGBA 5-5-5-1 format.</summary>
        SDL_PIXELFORMAT_RGBA5551 = 0x15441002u,
        /// <summary>16-bit ABGR 1-5-5-5 format.</summary>
        SDL_PIXELFORMAT_ABGR1555 = 0x15731002u,
        /// <summary>16-bit BGRA 5-5-5-1 format.</summary>
        SDL_PIXELFORMAT_BGRA5551 = 0x15841002u,
        /// <summary>16-bit RGB 5-6-5 format.</summary>
        SDL_PIXELFORMAT_RGB565 = 0x15151002u,
        /// <summary>16-bit BGR 5-6-5 format.</summary>
        SDL_PIXELFORMAT_BGR565 = 0x15551002u,
        /// <summary>24-bit RGB format (byte order: R, G, B).</summary>
        SDL_PIXELFORMAT_RGB24 = 0x17101803u,
        /// <summary>24-bit BGR format (byte order: B, G, R).</summary>
        SDL_PIXELFORMAT_BGR24 = 0x17401803u,
        /// <summary>32-bit XRGB 8-8-8-8 format.</summary>
        SDL_PIXELFORMAT_XRGB8888 = 0x16161804u,
        /// <summary>32-bit RGBX 8-8-8-8 format.</summary>
        SDL_PIXELFORMAT_RGBX8888 = 0x16261804u,
        /// <summary>32-bit XBGR 8-8-8-8 format.</summary>
        SDL_PIXELFORMAT_XBGR8888 = 0x16561804u,
        /// <summary>32-bit BGRX 8-8-8-8 format.</summary>
        SDL_PIXELFORMAT_BGRX8888 = 0x16661804u,
        /// <summary>32-bit ARGB 8-8-8-8 format.</summary>
        SDL_PIXELFORMAT_ARGB8888 = 0x16362004u,
        /// <summary>32-bit RGBA 8-8-8-8 format.</summary>
        SDL_PIXELFORMAT_RGBA8888 = 0x16462004u,
        /// <summary>32-bit ABGR 8-8-8-8 format.</summary>
        SDL_PIXELFORMAT_ABGR8888 = 0x16762004u,
        /// <summary>32-bit BGRA 8-8-8-8 format.</summary>
        SDL_PIXELFORMAT_BGRA8888 = 0x16862004u,
        /// <summary>32-bit XRGB 2-10-10-10 format.</summary>
        SDL_PIXELFORMAT_XRGB2101010 = 0x16172004u,
        /// <summary>32-bit XBGR 2-10-10-10 format.</summary>
        SDL_PIXELFORMAT_XBGR2101010 = 0x16572004u,
        /// <summary>32-bit ARGB 2-10-10-10 format.</summary>
        SDL_PIXELFORMAT_ARGB2101010 = 0x16372004u,
        /// <summary>32-bit ABGR 2-10-10-10 format.</summary>
        SDL_PIXELFORMAT_ABGR2101010 = 0x16772004u,
        /// <summary>48-bit RGB format (16 bits per component).</summary>
        SDL_PIXELFORMAT_RGB48 = 0x18103006u,
        /// <summary>48-bit BGR format (16 bits per component).</summary>
        SDL_PIXELFORMAT_BGR48 = 0x18403006u,
        /// <summary>64-bit RGBA format (16 bits per component).</summary>
        SDL_PIXELFORMAT_RGBA64 = 0x18204008u,
        /// <summary>64-bit ARGB format (16 bits per component).</summary>
        SDL_PIXELFORMAT_ARGB64 = 0x18304008u,
        /// <summary>64-bit BGRA format (16 bits per component).</summary>
        SDL_PIXELFORMAT_BGRA64 = 0x18504008u,
        /// <summary>64-bit ABGR format (16 bits per component).</summary>
        SDL_PIXELFORMAT_ABGR64 = 0x18604008u,
        /// <summary>48-bit RGB floating point format (16-bit float per component).</summary>
        SDL_PIXELFORMAT_RGB48_FLOAT = 0x1a103006u,
        /// <summary>48-bit BGR floating point format (16-bit float per component).</summary>
        SDL_PIXELFORMAT_BGR48_FLOAT = 0x1a403006u,
        /// <summary>64-bit RGBA floating point format (16-bit float per component).</summary>
        SDL_PIXELFORMAT_RGBA64_FLOAT = 0x1a204008u,
        /// <summary>64-bit ARGB floating point format (16-bit float per component).</summary>
        SDL_PIXELFORMAT_ARGB64_FLOAT = 0x1a304008u,
        /// <summary>64-bit BGRA floating point format (16-bit float per component).</summary>
        SDL_PIXELFORMAT_BGRA64_FLOAT = 0x1a504008u,
        /// <summary>64-bit ABGR floating point format (16-bit float per component).</summary>
        SDL_PIXELFORMAT_ABGR64_FLOAT = 0x1a604008u,
        /// <summary>96-bit RGB floating point format (32-bit float per component).</summary>
        SDL_PIXELFORMAT_RGB96_FLOAT = 0x1b10600cu,
        /// <summary>96-bit BGR floating point format (32-bit float per component).</summary>
        SDL_PIXELFORMAT_BGR96_FLOAT = 0x1b40600cu,
        /// <summary>128-bit RGBA floating point format (32-bit float per component).</summary>
        SDL_PIXELFORMAT_RGBA128_FLOAT = 0x1b208010u,
        /// <summary>128-bit ARGB floating point format (32-bit float per component).</summary>
        SDL_PIXELFORMAT_ARGB128_FLOAT = 0x1b308010u,
        /// <summary>128-bit BGRA floating point format (32-bit float per component).</summary>
        SDL_PIXELFORMAT_BGRA128_FLOAT = 0x1b508010u,
        /// <summary>128-bit ABGR floating point format (32-bit float per component).</summary>
        SDL_PIXELFORMAT_ABGR128_FLOAT = 0x1b608010u,
        /// <summary>Planar YUV format: Y + V + U (3 planes).</summary>
        SDL_PIXELFORMAT_YV12 = 0x32315659u,
        /// <summary>Planar YUV format: Y + U + V (3 planes).</summary>
        SDL_PIXELFORMAT_IYUV = 0x56555949u,
        /// <summary>Packed YUV format: Y0+U0+Y1+V0 (1 plane).</summary>
        SDL_PIXELFORMAT_YUY2 = 0x32595559u,
        /// <summary>Packed YUV format: U0+Y0+V0+Y1 (1 plane).</summary>
        SDL_PIXELFORMAT_UYVY = 0x59565955u,
        /// <summary>Packed YUV format: Y0+V0+Y1+U0 (1 plane).</summary>
        SDL_PIXELFORMAT_YVYU = 0x55595659u,
        /// <summary>Planar YUV format: Y + U/V interleaved (2 planes).</summary>
        SDL_PIXELFORMAT_NV12 = 0x3231564eu,
        /// <summary>Planar YUV format: Y + V/U interleaved (2 planes).</summary>
        SDL_PIXELFORMAT_NV21 = 0x3132564eu,
        /// <summary>Planar YUV 10-bit format: Y + U/V interleaved (2 planes).</summary>
        SDL_PIXELFORMAT_P010 = 0x30313050u,
        /// <summary>Android video texture format.</summary>
        SDL_PIXELFORMAT_EXTERNAL_OES = 0x2053454fu,
        /// <summary>Motion JPEG format.</summary>
        SDL_PIXELFORMAT_MJPG = 0x47504a4du,

        // Platform-dependent aliases for byte array formats
        // Since .NET is almost exclusively little-endian, we define these accordingly

        /// <summary>32-bit RGBA byte array format (platform-dependent).</summary>
        SDL_PIXELFORMAT_RGBA32 = SDL_PIXELFORMAT_ABGR8888,
        /// <summary>32-bit ARGB byte array format (platform-dependent).</summary>
        SDL_PIXELFORMAT_ARGB32 = SDL_PIXELFORMAT_BGRA8888,
        /// <summary>32-bit BGRA byte array format (platform-dependent).</summary>
        SDL_PIXELFORMAT_BGRA32 = SDL_PIXELFORMAT_ARGB8888,
        /// <summary>32-bit ABGR byte array format (platform-dependent).</summary>
        SDL_PIXELFORMAT_ABGR32 = SDL_PIXELFORMAT_RGBA8888,
        /// <summary>32-bit RGBX byte array format (platform-dependent).</summary>
        SDL_PIXELFORMAT_RGBX32 = SDL_PIXELFORMAT_XBGR8888,
        /// <summary>32-bit XRGB byte array format (platform-dependent).</summary>
        SDL_PIXELFORMAT_XRGB32 = SDL_PIXELFORMAT_BGRX8888,
        /// <summary>32-bit BGRX byte array format (platform-dependent).</summary>
        SDL_PIXELFORMAT_BGRX32 = SDL_PIXELFORMAT_XRGB8888,
        /// <summary>32-bit XBGR byte array format (platform-dependent).</summary>
        SDL_PIXELFORMAT_XBGR32 = SDL_PIXELFORMAT_RGBX8888
    }

    /// <summary>
    /// Colorspace color type.
    /// </summary>
    public enum SDL_ColorType
    {
        /// <summary>Unknown color type.</summary>
        SDL_COLOR_TYPE_UNKNOWN = 0,
        /// <summary>RGB color type (red, green, blue channels).</summary>
        SDL_COLOR_TYPE_RGB = 1,
        /// <summary>YCbCr (YUV) color type (luma and chroma channels).</summary>
        SDL_COLOR_TYPE_YCBCR = 2
    }

    /// <summary>
    /// Colorspace color range, as described by https://www.itu.int/rec/R-REC-BT.2100-2-201807-I/en
    /// </summary>
    public enum SDL_ColorRange
    {
        /// <summary>Unknown color range.</summary>
        SDL_COLOR_RANGE_UNKNOWN = 0,
        /// <summary>Narrow/limited range (e.g., 16-235 for 8-bit RGB and luma, 16-240 for 8-bit chroma).</summary>
        SDL_COLOR_RANGE_LIMITED = 1,
        /// <summary>Full range (e.g., 0-255 for 8-bit RGB and luma, 1-255 for 8-bit chroma).</summary>
        SDL_COLOR_RANGE_FULL = 2
    }

    /// <summary>
    /// Colorspace color primaries, as described by https://www.itu.int/rec/T-REC-H.273-201612-S/en
    /// </summary>
    public enum SDL_ColorPrimaries
    {
        /// <summary>Unknown color primaries.</summary>
        SDL_COLOR_PRIMARIES_UNKNOWN = 0,
        /// <summary>ITU-R BT.709-6 color primaries.</summary>
        SDL_COLOR_PRIMARIES_BT709 = 1,
        /// <summary>Unspecified color primaries.</summary>
        SDL_COLOR_PRIMARIES_UNSPECIFIED = 2,
        /// <summary>ITU-R BT.470-6 System M color primaries.</summary>
        SDL_COLOR_PRIMARIES_BT470M = 4,
        /// <summary>ITU-R BT.470-6 System B, G / ITU-R BT.601-7 625 color primaries.</summary>
        SDL_COLOR_PRIMARIES_BT470BG = 5,
        /// <summary>ITU-R BT.601-7 525, SMPTE 170M color primaries.</summary>
        SDL_COLOR_PRIMARIES_BT601 = 6,
        /// <summary>SMPTE 240M color primaries (functionally the same as BT601).</summary>
        SDL_COLOR_PRIMARIES_SMPTE240 = 7,
        /// <summary>Generic film color primaries (color filters using Illuminant C).</summary>
        SDL_COLOR_PRIMARIES_GENERIC_FILM = 8,
        /// <summary>ITU-R BT.2020-2 / ITU-R BT.2100-0 color primaries.</summary>
        SDL_COLOR_PRIMARIES_BT2020 = 9,
        /// <summary>SMPTE ST 428-1 color primaries (CIE 1931 XYZ).</summary>
        SDL_COLOR_PRIMARIES_XYZ = 10,
        /// <summary>SMPTE RP 431-2 color primaries.</summary>
        SDL_COLOR_PRIMARIES_SMPTE431 = 11,
        /// <summary>SMPTE EG 432-1 / DCI P3 color primaries.</summary>
        SDL_COLOR_PRIMARIES_SMPTE432 = 12,
        /// <summary>EBU Tech. 3213-E color primaries.</summary>
        SDL_COLOR_PRIMARIES_EBU3213 = 22,
        /// <summary>Custom color primaries.</summary>
        SDL_COLOR_PRIMARIES_CUSTOM = 31
    }

    /// <summary>
    /// Colorspace transfer characteristics.
    /// </summary>
    public enum SDL_TransferCharacteristics
    {
        /// <summary>Unknown transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_UNKNOWN = 0,
        /// <summary>Rec. ITU-R BT.709-6 / ITU-R BT1361 transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_BT709 = 1,
        /// <summary>Unspecified transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_UNSPECIFIED = 2,
        /// <summary>ITU-R BT.470-6 System M / ITU-R BT1700 625 PAL and SECAM (gamma 2.2).</summary>
        SDL_TRANSFER_CHARACTERISTICS_GAMMA22 = 4,
        /// <summary>ITU-R BT.470-6 System B, G (gamma 2.8).</summary>
        SDL_TRANSFER_CHARACTERISTICS_GAMMA28 = 5,
        /// <summary>SMPTE ST 170M / ITU-R BT.601-7 525 or 625 transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_BT601 = 6,
        /// <summary>SMPTE ST 240M transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_SMPTE240 = 7,
        /// <summary>Linear transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_LINEAR = 8,
        /// <summary>Logarithmic transfer (100:1 range).</summary>
        SDL_TRANSFER_CHARACTERISTICS_LOG100 = 9,
        /// <summary>Logarithmic transfer (100*sqrt(10):1 range).</summary>
        SDL_TRANSFER_CHARACTERISTICS_LOG100_SQRT10 = 10,
        /// <summary>IEC 61966-2-4 transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_IEC61966 = 11,
        /// <summary>ITU-R BT1361 Extended Colour Gamut transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_BT1361 = 12,
        /// <summary>IEC 61966-2-1 (sRGB or sYCC) transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_SRGB = 13,
        /// <summary>ITU-R BT2020 for 10-bit system transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_BT2020_10BIT = 14,
        /// <summary>ITU-R BT2020 for 12-bit system transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_BT2020_12BIT = 15,
        /// <summary>SMPTE ST 2084 for 10-, 12-, 14- and 16-bit systems (Perceptual Quantizer / PQ).</summary>
        SDL_TRANSFER_CHARACTERISTICS_PQ = 16,
        /// <summary>SMPTE ST 428-1 transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_SMPTE428 = 17,
        /// <summary>ARIB STD-B67 "hybrid log-gamma" (HLG) transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_HLG = 18,
        /// <summary>Custom transfer characteristics.</summary>
        SDL_TRANSFER_CHARACTERISTICS_CUSTOM = 31
    }

    /// <summary>
    /// Colorspace matrix coefficients.
    /// </summary>
    public enum SDL_MatrixCoefficients
    {
        /// <summary>Identity matrix (RGB).</summary>
        SDL_MATRIX_COEFFICIENTS_IDENTITY = 0,
        /// <summary>ITU-R BT.709-6 matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_BT709 = 1,
        /// <summary>Unspecified matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_UNSPECIFIED = 2,
        /// <summary>US FCC Title 47 matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_FCC = 4,
        /// <summary>ITU-R BT.470-6 System B, G / ITU-R BT.601-7 625 matrix coefficients (functionally the same as BT601).</summary>
        SDL_MATRIX_COEFFICIENTS_BT470BG = 5,
        /// <summary>ITU-R BT.601-7 525 matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_BT601 = 6,
        /// <summary>SMPTE 240M matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_SMPTE240 = 7,
        /// <summary>YCgCo matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_YCGCO = 8,
        /// <summary>ITU-R BT.2020-2 non-constant luminance matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_BT2020_NCL = 9,
        /// <summary>ITU-R BT.2020-2 constant luminance matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_BT2020_CL = 10,
        /// <summary>SMPTE ST 2085 matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_SMPTE2085 = 11,
        /// <summary>Chromaticity-derived non-constant luminance matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_CHROMA_DERIVED_NCL = 12,
        /// <summary>Chromaticity-derived constant luminance matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_CHROMA_DERIVED_CL = 13,
        /// <summary>ITU-R BT.2100-0 ICTCP matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_ICTCP = 14,
        /// <summary>Custom matrix coefficients.</summary>
        SDL_MATRIX_COEFFICIENTS_CUSTOM = 31
    }

    /// <summary>
    /// Colorspace chroma sample location.
    /// </summary>
    public enum SDL_ChromaLocation
    {
        /// <summary>RGB, no chroma sampling.</summary>
        SDL_CHROMA_LOCATION_NONE = 0,
        /// <summary>MPEG-2, MPEG-4, AVC: Cb and Cr at midpoint of left-edge of 2x2 square.</summary>
        SDL_CHROMA_LOCATION_LEFT = 1,
        /// <summary>JPEG/JFIF, H.261, MPEG-1: Cb and Cr at center of 2x2 square.</summary>
        SDL_CHROMA_LOCATION_CENTER = 2,
        /// <summary>HEVC (BT.2020/BT.2100 on Blu-ray): Cb and Cr co-located with top-left Y pixel.</summary>
        SDL_CHROMA_LOCATION_TOPLEFT = 3
    }

    /// <summary>
    /// Defining custom SDL_Colorspace formats.
    /// </summary>
    /// <param name="type">the type of the new format, probably an SDL_ColorType value.</param>
    /// <param name="range">the range of the new format, probably a SDL_ColorRange value.</param>
    /// <param name="primaries">the primaries of the new format, probably an SDL_ColorPrimaries value.</param>
    /// <param name="transfer">the transfer characteristics of the new format, probably an SDL_TransferCharacteristics value.</param>
    /// <param name="matrix">the matrix coefficients of the new format, probably an SDL_MatrixCoefficients value.</param>
    /// <param name="chroma">the chroma sample location of the new format, probably an SDL_ChromaLocation value.</param>
    /// <returns>a format value in the style of SDL_Colorspace.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SDL_DEFINE_COLORSPACE(SDL_ColorType type, SDL_ColorRange range, SDL_ColorPrimaries primaries,
        SDL_TransferCharacteristics transfer, SDL_MatrixCoefficients matrix, SDL_ChromaLocation chroma)
    {
        return ((uint)type << 28) | ((uint)range << 24) | ((uint)chroma << 20) |
               ((uint)primaries << 10) | ((uint)transfer << 5) | ((uint)matrix << 0);
    }

    /// <summary>
    /// Retrieve the type of an SDL_Colorspace.
    /// </summary>
    /// <param name="cspace">an SDL_Colorspace to check.</param>
    /// <returns>the SDL_ColorType for cspace.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SDL_ColorType SDL_COLORSPACETYPE(SDL_Colorspace cspace)
    {
        return (SDL_ColorType)(((uint)cspace >> 28) & 0x0F);
    }

    /// <summary>
    /// Retrieve the range of an SDL_Colorspace.
    /// </summary>
    /// <param name="cspace">an SDL_Colorspace to check.</param>
    /// <returns>the SDL_ColorRange of cspace.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SDL_ColorRange SDL_COLORSPACERANGE(SDL_Colorspace cspace)
    {
        return (SDL_ColorRange)(((uint)cspace >> 24) & 0x0F);
    }

    /// <summary>
    /// Retrieve the chroma sample location of an SDL_Colorspace.
    /// </summary>
    /// <param name="cspace">an SDL_Colorspace to check.</param>
    /// <returns>the SDL_ChromaLocation of cspace.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SDL_ChromaLocation SDL_COLORSPACECHROMA(SDL_Colorspace cspace)
    {
        return (SDL_ChromaLocation)(((uint)cspace >> 20) & 0x0F);
    }

    /// <summary>
    /// Retrieve the primaries of an SDL_Colorspace.
    /// </summary>
    /// <param name="cspace">an SDL_Colorspace to check.</param>
    /// <returns>the SDL_ColorPrimaries of cspace.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SDL_ColorPrimaries SDL_COLORSPACEPRIMARIES(SDL_Colorspace cspace)
    {
        return (SDL_ColorPrimaries)(((uint)cspace >> 10) & 0x1F);
    }

    /// <summary>
    /// Retrieve the transfer characteristics of an SDL_Colorspace.
    /// </summary>
    /// <param name="cspace">an SDL_Colorspace to check.</param>
    /// <returns>the SDL_TransferCharacteristics of cspace.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SDL_TransferCharacteristics SDL_COLORSPACETRANSFER(SDL_Colorspace cspace)
    {
        return (SDL_TransferCharacteristics)(((uint)cspace >> 5) & 0x1F);
    }

    /// <summary>
    /// Retrieve the matrix coefficients of an SDL_Colorspace.
    /// </summary>
    /// <param name="cspace">an SDL_Colorspace to check.</param>
    /// <returns>the SDL_MatrixCoefficients of cspace.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SDL_MatrixCoefficients SDL_COLORSPACEMATRIX(SDL_Colorspace cspace)
    {
        return (SDL_MatrixCoefficients)((uint)cspace & 0x1F);
    }

    /// <summary>
    /// Determine if an SDL_Colorspace uses BT601 (or BT470BG) matrix coefficients.
    /// </summary>
    /// <param name="cspace">an SDL_Colorspace to check.</param>
    /// <returns>true if BT601 or BT470BG, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SDL_ISCOLORSPACE_MATRIX_BT601(SDL_Colorspace cspace)
    {
        SDL_MatrixCoefficients matrix = SDL_COLORSPACEMATRIX(cspace);
        return matrix is SDL_MatrixCoefficients.SDL_MATRIX_COEFFICIENTS_BT601 or
               SDL_MatrixCoefficients.SDL_MATRIX_COEFFICIENTS_BT470BG;
    }

    /// <summary>
    /// Determine if an SDL_Colorspace uses BT709 matrix coefficients.
    /// </summary>
    /// <param name="cspace">an SDL_Colorspace to check.</param>
    /// <returns>true if BT709, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SDL_ISCOLORSPACE_MATRIX_BT709(SDL_Colorspace cspace)
    {
        return SDL_COLORSPACEMATRIX(cspace) == SDL_MatrixCoefficients.SDL_MATRIX_COEFFICIENTS_BT709;
    }

    /// <summary>
    /// Determine if an SDL_Colorspace uses BT2020_NCL matrix coefficients.
    /// </summary>
    /// <param name="cspace">an SDL_Colorspace to check.</param>
    /// <returns>true if BT2020_NCL, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SDL_ISCOLORSPACE_MATRIX_BT2020_NCL(SDL_Colorspace cspace)
    {
        return SDL_COLORSPACEMATRIX(cspace) == SDL_MatrixCoefficients.SDL_MATRIX_COEFFICIENTS_BT2020_NCL;
    }

    /// <summary>
    /// Determine if an SDL_Colorspace has a limited range.
    /// </summary>
    /// <param name="cspace">an SDL_Colorspace to check.</param>
    /// <returns>true if limited range, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SDL_ISCOLORSPACE_LIMITED_RANGE(SDL_Colorspace cspace)
    {
        return SDL_COLORSPACERANGE(cspace) != SDL_ColorRange.SDL_COLOR_RANGE_FULL;
    }

    /// <summary>
    /// Determine if an SDL_Colorspace has a full range.
    /// </summary>
    /// <param name="cspace">an SDL_Colorspace to check.</param>
    /// <returns>true if full range, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SDL_ISCOLORSPACE_FULL_RANGE(SDL_Colorspace cspace)
    {
        return SDL_COLORSPACERANGE(cspace) == SDL_ColorRange.SDL_COLOR_RANGE_FULL;
    }

    /// <summary>
    /// Colorspace definitions.
    /// </summary>
    public enum SDL_Colorspace : uint
    {
        /// <summary>Unknown colorspace.</summary>
        SDL_COLORSPACE_UNKNOWN = 0,
        /// <summary>sRGB colorspace (gamma corrected, default for SDL rendering and 8-bit RGB). Equivalent to DXGI_COLOR_SPACE_RGB_FULL_G22_NONE_P709.</summary>
        SDL_COLORSPACE_SRGB = 0x120005a0u,
        /// <summary>Linear sRGB colorspace (default for floating point surfaces). Equivalent to DXGI_COLOR_SPACE_RGB_FULL_G10_NONE_P709.</summary>
        SDL_COLORSPACE_SRGB_LINEAR = 0x12000500u,
        /// <summary>HDR10 colorspace (non-linear HDR, default for 10-bit surfaces). Equivalent to DXGI_COLOR_SPACE_RGB_FULL_G2084_NONE_P2020.</summary>
        SDL_COLORSPACE_HDR10 = 0x12002600u,
        /// <summary>JPEG colorspace. Equivalent to DXGI_COLOR_SPACE_YCBCR_FULL_G22_NONE_P709_X601.</summary>
        SDL_COLORSPACE_JPEG = 0x220004c6u,
        /// <summary>BT.601 colorspace with limited range. Equivalent to DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P601.</summary>
        SDL_COLORSPACE_BT601_LIMITED = 0x211018c6u,
        /// <summary>BT.601 colorspace with full range.</summary>
        SDL_COLORSPACE_BT601_FULL = 0x221018c6u,
        /// <summary>BT.709 colorspace with limited range. Equivalent to DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P709.</summary>
        SDL_COLORSPACE_BT709_LIMITED = 0x21100421u,
        /// <summary>BT.709 colorspace with full range.</summary>
        SDL_COLORSPACE_BT709_FULL = 0x22100421u,
        /// <summary>BT.2020 colorspace with limited range. Equivalent to DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P2020.</summary>
        SDL_COLORSPACE_BT2020_LIMITED = 0x21102609u,
        /// <summary>BT.2020 colorspace with full range. Equivalent to DXGI_COLOR_SPACE_YCBCR_FULL_G22_LEFT_P2020.</summary>
        SDL_COLORSPACE_BT2020_FULL = 0x22102609u,
        /// <summary>Default colorspace for RGB surfaces (sRGB).</summary>
        SDL_COLORSPACE_RGB_DEFAULT = SDL_COLORSPACE_SRGB,
        /// <summary>Default colorspace for YUV surfaces (JPEG).</summary>
        SDL_COLORSPACE_YUV_DEFAULT = SDL_COLORSPACE_JPEG
    }

    /// <summary>
    /// A structure that represents a color as RGBA components.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Color(byte r, byte g, byte b, byte a)
    {
        /// <summary>Red component (0-255).</summary>
        public byte r = r;
        /// <summary>Green component (0-255).</summary>
        public byte g = g;
        /// <summary>Blue component (0-255).</summary>
        public byte b = b;
        /// <summary>Alpha component (0-255, 0=transparent, 255=opaque).</summary>
        public byte a = a;
    }

    /// <summary>
    /// A structure that represents a color as floating point RGBA components.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_FColor(float r, float g, float b, float a)
    {
        /// <summary>Red component (0.0-1.0).</summary>
        public float r = r;
        /// <summary>Green component (0.0-1.0).</summary>
        public float g = g;
        /// <summary>Blue component (0.0-1.0).</summary>
        public float b = b;
        /// <summary>Alpha component (0.0-1.0, 0.0=transparent, 1.0=opaque).</summary>
        public float a = a;
    }

    /// <summary>
    /// A set of indexed colors representing a palette.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_Palette
    {
        /// <summary>Number of elements in the colors array.</summary>
        public int ncolors;
        /// <summary>Pointer to an array of colors, ncolors long.</summary>
        public SDL_Color* colors;
        /// <summary>Internal use only, do not touch.</summary>
        public uint version;
        /// <summary>Internal use only, do not touch.</summary>
        public int refcount;
    }

    /// <summary>
    /// Details about the format of a pixel.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_PixelFormatDetails
    {
        /// <summary>The pixel format.</summary>
        public SDL_PixelFormat format;
        /// <summary>Number of significant bits in a pixel value.</summary>
        public byte bits_per_pixel;
        /// <summary>Number of bytes required to store a pixel value.</summary>
        public byte bytes_per_pixel;
        /// <summary>Padding bytes (internal use).</summary>
        public fixed byte padding[2];
        /// <summary>Mask for red component of a pixel.</summary>
        public uint Rmask;
        /// <summary>Mask for green component of a pixel.</summary>
        public uint Gmask;
        /// <summary>Mask for blue component of a pixel.</summary>
        public uint Bmask;
        /// <summary>Mask for alpha component of a pixel.</summary>
        public uint Amask;
        /// <summary>Number of bits used for red component.</summary>
        public byte Rbits;
        /// <summary>Number of bits used for green component.</summary>
        public byte Gbits;
        /// <summary>Number of bits used for blue component.</summary>
        public byte Bbits;
        /// <summary>Number of bits used for alpha component.</summary>
        public byte Abits;
        /// <summary>Number of bits to left shift red component.</summary>
        public byte Rshift;
        /// <summary>Number of bits to left shift green component.</summary>
        public byte Gshift;
        /// <summary>Number of bits to left shift blue component.</summary>
        public byte Bshift;
        /// <summary>Number of bits to left shift alpha component.</summary>
        public byte Ashift;
    }

    /// <summary>
    /// Get the human readable name of a pixel format.
    /// </summary>
    /// <param name="format">the pixel format to query.</param>
    /// <returns>the human readable name of the specified pixel format or "SDL_PIXELFORMAT_UNKNOWN" if the format isn't recognized.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string SDL_GetPixelFormatName(SDL_PixelFormat format);

    /// <summary>
    /// Convert one of the enumerated pixel formats to a bpp value and RGBA masks.
    /// </summary>
    /// <param name="format">one of the SDL_PixelFormat values.</param>
    /// <param name="bpp">a bits per pixel value; usually 15, 16, or 32.</param>
    /// <param name="Rmask">a pointer filled in with the red mask for the format.</param>
    /// <param name="Gmask">a pointer filled in with the green mask for the format.</param>
    /// <param name="Bmask">a pointer filled in with the blue mask for the format.</param>
    /// <param name="Amask">a pointer filled in with the alpha mask for the format.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetMasksForPixelFormat(SDL_PixelFormat format, int* bpp, uint* Rmask, uint* Gmask, uint* Bmask, uint* Amask);

    /// <summary>
    /// Convert a bpp value and RGBA masks to an enumerated pixel format.
    /// </summary>
    /// <param name="bpp">a bits per pixel value; usually 15, 16, or 32.</param>
    /// <param name="Rmask">the red mask for the format.</param>
    /// <param name="Gmask">the green mask for the format.</param>
    /// <param name="Bmask">the blue mask for the format.</param>
    /// <param name="Amask">the alpha mask for the format.</param>
    /// <returns>the SDL_PixelFormat value corresponding to the format masks, or SDL_PIXELFORMAT_UNKNOWN if there isn't a match.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PixelFormat SDL_GetPixelFormatForMasks(int bpp, uint Rmask, uint Gmask, uint Bmask, uint Amask);

    /// <summary>
    /// Create an SDL_PixelFormatDetails structure corresponding to a pixel format.
    /// </summary>
    /// <param name="format">one of the SDL_PixelFormat values.</param>
    /// <returns>a pointer to a SDL_PixelFormatDetails structure or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PixelFormatDetails* SDL_GetPixelFormatDetails(SDL_PixelFormat format);

    /// <summary>
    /// Create a palette structure with the specified number of color entries.
    /// </summary>
    /// <param name="ncolors">represents the number of color entries in the color palette.</param>
    /// <returns>a new SDL_Palette structure on success or NULL on failure (e.g. if there wasn't enough memory); call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Palette* SDL_CreatePalette(int ncolors);

    /// <summary>
    /// Set a range of colors in a palette.
    /// </summary>
    /// <param name="palette">the SDL_Palette structure to modify.</param>
    /// <param name="colors">an array of SDL_Color structures to copy into the palette.</param>
    /// <param name="firstcolor">the index of the first palette entry to modify.</param>
    /// <param name="ncolors">the number of entries to modify.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetPaletteColors(SDL_Palette* palette, SDL_Color* colors, int firstcolor, int ncolors);

    /// <summary>
    /// Free a palette created with SDL_CreatePalette().
    /// </summary>
    /// <param name="palette">the SDL_Palette structure to be freed.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyPalette(SDL_Palette* palette);

    /// <summary>
    /// Map an RGB triple to an opaque pixel value for a given pixel format.
    /// </summary>
    /// <param name="format">a pointer to SDL_PixelFormatDetails describing the pixel format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be NULL.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <returns>a pixel value.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_MapRGB(SDL_PixelFormatDetails* format, SDL_Palette* palette, byte r, byte g, byte b);

    /// <summary>
    /// Map an RGBA quadruple to a pixel value for a given pixel format.
    /// </summary>
    /// <param name="format">a pointer to SDL_PixelFormatDetails describing the pixel format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be NULL.</param>
    /// <param name="r">the red component of the pixel in the range 0-255.</param>
    /// <param name="g">the green component of the pixel in the range 0-255.</param>
    /// <param name="b">the blue component of the pixel in the range 0-255.</param>
    /// <param name="a">the alpha component of the pixel in the range 0-255.</param>
    /// <returns>a pixel value.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_MapRGBA(SDL_PixelFormatDetails* format, SDL_Palette* palette, byte r, byte g, byte b, byte a);

    /// <summary>
    /// Get RGB values from a pixel in the specified format.
    /// </summary>
    /// <param name="pixel">a pixel value.</param>
    /// <param name="format">a pointer to SDL_PixelFormatDetails describing the pixel format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be NULL.</param>
    /// <param name="r">a pointer filled in with the red component, may be NULL.</param>
    /// <param name="g">a pointer filled in with the green component, may be NULL.</param>
    /// <param name="b">a pointer filled in with the blue component, may be NULL.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_GetRGB(uint pixel, SDL_PixelFormatDetails* format, SDL_Palette* palette, byte* r, byte* g, byte* b);

    /// <summary>
    /// Get RGBA values from a pixel in the specified format.
    /// </summary>
    /// <param name="pixel">a pixel value.</param>
    /// <param name="format">a pointer to SDL_PixelFormatDetails describing the pixel format.</param>
    /// <param name="palette">an optional palette for indexed formats, may be NULL.</param>
    /// <param name="r">a pointer filled in with the red component, may be NULL.</param>
    /// <param name="g">a pointer filled in with the green component, may be NULL.</param>
    /// <param name="b">a pointer filled in with the blue component, may be NULL.</param>
    /// <param name="a">a pointer filled in with the alpha component, may be NULL.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_GetRGBA(uint pixel, SDL_PixelFormatDetails* format, SDL_Palette* palette, byte* r, byte* g, byte* b, byte* a);
}
