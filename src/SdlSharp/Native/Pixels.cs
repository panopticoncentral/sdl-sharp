using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Skipped sub-enums: SDL_PixelType, SDL_BitmapOrder, SDL_PackedOrder, SDL_ArrayOrder, SDL_PackedLayout.
// These are only meaningful for the C macros (SDL_PIXELTYPE, SDL_PIXELORDER, etc.) which decompose
// format values. The raw integer values in SDL_PixelFormat already encode all this information.

/// <summary>
/// Pixel format.
/// </summary>
public enum SDL_PixelFormat : uint
{
    /// <summary>Unknown pixel format.</summary>
    SDL_PIXELFORMAT_UNKNOWN = 0,
    /// <summary>1-bit indexed, LSB first.</summary>
    SDL_PIXELFORMAT_INDEX1LSB = 0x11100100u,
    /// <summary>1-bit indexed, MSB first.</summary>
    SDL_PIXELFORMAT_INDEX1MSB = 0x11200100u,
    /// <summary>2-bit indexed, LSB first.</summary>
    SDL_PIXELFORMAT_INDEX2LSB = 0x1c100200u,
    /// <summary>2-bit indexed, MSB first.</summary>
    SDL_PIXELFORMAT_INDEX2MSB = 0x1c200200u,
    /// <summary>4-bit indexed, LSB first.</summary>
    SDL_PIXELFORMAT_INDEX4LSB = 0x12100400u,
    /// <summary>4-bit indexed, MSB first.</summary>
    SDL_PIXELFORMAT_INDEX4MSB = 0x12200400u,
    /// <summary>8-bit indexed.</summary>
    SDL_PIXELFORMAT_INDEX8 = 0x13000801u,
    /// <summary>Packed 8-bit, RGB332.</summary>
    SDL_PIXELFORMAT_RGB332 = 0x14110801u,
    /// <summary>Packed 16-bit, XRGB4444.</summary>
    SDL_PIXELFORMAT_XRGB4444 = 0x15120c02u,
    /// <summary>Packed 16-bit, XBGR4444.</summary>
    SDL_PIXELFORMAT_XBGR4444 = 0x15520c02u,
    /// <summary>Packed 16-bit, XRGB1555.</summary>
    SDL_PIXELFORMAT_XRGB1555 = 0x15130f02u,
    /// <summary>Packed 16-bit, XBGR1555.</summary>
    SDL_PIXELFORMAT_XBGR1555 = 0x15530f02u,
    /// <summary>Packed 16-bit, ARGB4444.</summary>
    SDL_PIXELFORMAT_ARGB4444 = 0x15321002u,
    /// <summary>Packed 16-bit, RGBA4444.</summary>
    SDL_PIXELFORMAT_RGBA4444 = 0x15421002u,
    /// <summary>Packed 16-bit, ABGR4444.</summary>
    SDL_PIXELFORMAT_ABGR4444 = 0x15721002u,
    /// <summary>Packed 16-bit, BGRA4444.</summary>
    SDL_PIXELFORMAT_BGRA4444 = 0x15821002u,
    /// <summary>Packed 16-bit, ARGB1555.</summary>
    SDL_PIXELFORMAT_ARGB1555 = 0x15331002u,
    /// <summary>Packed 16-bit, RGBA5551.</summary>
    SDL_PIXELFORMAT_RGBA5551 = 0x15441002u,
    /// <summary>Packed 16-bit, ABGR1555.</summary>
    SDL_PIXELFORMAT_ABGR1555 = 0x15731002u,
    /// <summary>Packed 16-bit, BGRA5551.</summary>
    SDL_PIXELFORMAT_BGRA5551 = 0x15841002u,
    /// <summary>Packed 16-bit, RGB565.</summary>
    SDL_PIXELFORMAT_RGB565 = 0x15151002u,
    /// <summary>Packed 16-bit, BGR565.</summary>
    SDL_PIXELFORMAT_BGR565 = 0x15551002u,
    /// <summary>Array 24-bit, RGB byte order.</summary>
    SDL_PIXELFORMAT_RGB24 = 0x17101803u,
    /// <summary>Array 24-bit, BGR byte order.</summary>
    SDL_PIXELFORMAT_BGR24 = 0x17401803u,
    /// <summary>Packed 32-bit, XRGB8888.</summary>
    SDL_PIXELFORMAT_XRGB8888 = 0x16161804u,
    /// <summary>Packed 32-bit, RGBX8888.</summary>
    SDL_PIXELFORMAT_RGBX8888 = 0x16261804u,
    /// <summary>Packed 32-bit, XBGR8888.</summary>
    SDL_PIXELFORMAT_XBGR8888 = 0x16561804u,
    /// <summary>Packed 32-bit, BGRX8888.</summary>
    SDL_PIXELFORMAT_BGRX8888 = 0x16661804u,
    /// <summary>Packed 32-bit, ARGB8888.</summary>
    SDL_PIXELFORMAT_ARGB8888 = 0x16362004u,
    /// <summary>Packed 32-bit, RGBA8888.</summary>
    SDL_PIXELFORMAT_RGBA8888 = 0x16462004u,
    /// <summary>Packed 32-bit, ABGR8888.</summary>
    SDL_PIXELFORMAT_ABGR8888 = 0x16762004u,
    /// <summary>Packed 32-bit, BGRA8888.</summary>
    SDL_PIXELFORMAT_BGRA8888 = 0x16862004u,
    /// <summary>Packed 32-bit, XRGB2101010.</summary>
    SDL_PIXELFORMAT_XRGB2101010 = 0x16172004u,
    /// <summary>Packed 32-bit, XBGR2101010.</summary>
    SDL_PIXELFORMAT_XBGR2101010 = 0x16572004u,
    /// <summary>Packed 32-bit, ARGB2101010.</summary>
    SDL_PIXELFORMAT_ARGB2101010 = 0x16372004u,
    /// <summary>Packed 32-bit, ABGR2101010.</summary>
    SDL_PIXELFORMAT_ABGR2101010 = 0x16772004u,
    /// <summary>Array 48-bit, RGB unsigned 16-bit.</summary>
    SDL_PIXELFORMAT_RGB48 = 0x18103006u,
    /// <summary>Array 48-bit, BGR unsigned 16-bit.</summary>
    SDL_PIXELFORMAT_BGR48 = 0x18403006u,
    /// <summary>Array 64-bit, RGBA unsigned 16-bit.</summary>
    SDL_PIXELFORMAT_RGBA64 = 0x18204008u,
    /// <summary>Array 64-bit, ARGB unsigned 16-bit.</summary>
    SDL_PIXELFORMAT_ARGB64 = 0x18304008u,
    /// <summary>Array 64-bit, BGRA unsigned 16-bit.</summary>
    SDL_PIXELFORMAT_BGRA64 = 0x18504008u,
    /// <summary>Array 64-bit, ABGR unsigned 16-bit.</summary>
    SDL_PIXELFORMAT_ABGR64 = 0x18604008u,
    /// <summary>Array 48-bit, RGB float16.</summary>
    SDL_PIXELFORMAT_RGB48_FLOAT = 0x1a103006u,
    /// <summary>Array 48-bit, BGR float16.</summary>
    SDL_PIXELFORMAT_BGR48_FLOAT = 0x1a403006u,
    /// <summary>Array 64-bit, RGBA float16.</summary>
    SDL_PIXELFORMAT_RGBA64_FLOAT = 0x1a204008u,
    /// <summary>Array 64-bit, ARGB float16.</summary>
    SDL_PIXELFORMAT_ARGB64_FLOAT = 0x1a304008u,
    /// <summary>Array 64-bit, BGRA float16.</summary>
    SDL_PIXELFORMAT_BGRA64_FLOAT = 0x1a504008u,
    /// <summary>Array 64-bit, ABGR float16.</summary>
    SDL_PIXELFORMAT_ABGR64_FLOAT = 0x1a604008u,
    /// <summary>Array 96-bit, RGB float32.</summary>
    SDL_PIXELFORMAT_RGB96_FLOAT = 0x1b10600cu,
    /// <summary>Array 96-bit, BGR float32.</summary>
    SDL_PIXELFORMAT_BGR96_FLOAT = 0x1b40600cu,
    /// <summary>Array 128-bit, RGBA float32.</summary>
    SDL_PIXELFORMAT_RGBA128_FLOAT = 0x1b208010u,
    /// <summary>Array 128-bit, ARGB float32.</summary>
    SDL_PIXELFORMAT_ARGB128_FLOAT = 0x1b308010u,
    /// <summary>Array 128-bit, BGRA float32.</summary>
    SDL_PIXELFORMAT_BGRA128_FLOAT = 0x1b508010u,
    /// <summary>Array 128-bit, ABGR float32.</summary>
    SDL_PIXELFORMAT_ABGR128_FLOAT = 0x1b608010u,

    // YUV formats
    /// <summary>Planar mode: Y + V + U (3 planes).</summary>
    SDL_PIXELFORMAT_YV12 = 0x32315659u,
    /// <summary>Planar mode: Y + U + V (3 planes).</summary>
    SDL_PIXELFORMAT_IYUV = 0x56555949u,
    /// <summary>Packed mode: Y0+U0+Y1+V0 (1 plane).</summary>
    SDL_PIXELFORMAT_YUY2 = 0x32595559u,
    /// <summary>Packed mode: U0+Y0+V0+Y1 (1 plane).</summary>
    SDL_PIXELFORMAT_UYVY = 0x59565955u,
    /// <summary>Packed mode: Y0+V0+Y1+U0 (1 plane).</summary>
    SDL_PIXELFORMAT_YVYU = 0x55595659u,
    /// <summary>Planar mode: Y + U/V interleaved (2 planes).</summary>
    SDL_PIXELFORMAT_NV12 = 0x3231564eu,
    /// <summary>Planar mode: Y + V/U interleaved (2 planes).</summary>
    SDL_PIXELFORMAT_NV21 = 0x3132564eu,
    /// <summary>Planar mode: Y + U/V interleaved, 10-bit (2 planes).</summary>
    SDL_PIXELFORMAT_P010 = 0x30313050u,
    /// <summary>Android video texture format.</summary>
    SDL_PIXELFORMAT_EXTERNAL_OES = 0x2053454fu,
    /// <summary>Motion JPEG.</summary>
    SDL_PIXELFORMAT_MJPG = 0x47504a4du,

    // Byte-array aliases — use little-endian mappings (.NET targets are overwhelmingly LE).
    /// <summary>RGBA byte array (alias for ABGR8888 on little-endian).</summary>
    SDL_PIXELFORMAT_RGBA32 = SDL_PIXELFORMAT_ABGR8888,
    /// <summary>ARGB byte array (alias for BGRA8888 on little-endian).</summary>
    SDL_PIXELFORMAT_ARGB32 = SDL_PIXELFORMAT_BGRA8888,
    /// <summary>BGRA byte array (alias for ARGB8888 on little-endian).</summary>
    SDL_PIXELFORMAT_BGRA32 = SDL_PIXELFORMAT_ARGB8888,
    /// <summary>ABGR byte array (alias for RGBA8888 on little-endian).</summary>
    SDL_PIXELFORMAT_ABGR32 = SDL_PIXELFORMAT_RGBA8888,
    /// <summary>RGBX byte array (alias for XBGR8888 on little-endian).</summary>
    SDL_PIXELFORMAT_RGBX32 = SDL_PIXELFORMAT_XBGR8888,
    /// <summary>XRGB byte array (alias for BGRX8888 on little-endian).</summary>
    SDL_PIXELFORMAT_XRGB32 = SDL_PIXELFORMAT_BGRX8888,
    /// <summary>BGRX byte array (alias for XRGB8888 on little-endian).</summary>
    SDL_PIXELFORMAT_BGRX32 = SDL_PIXELFORMAT_XRGB8888,
    /// <summary>XBGR byte array (alias for RGBX8888 on little-endian).</summary>
    SDL_PIXELFORMAT_XBGR32 = SDL_PIXELFORMAT_RGBX8888,
}

// Skipped colorspace component enums: SDL_ColorType, SDL_ColorRange, SDL_ColorPrimaries,
// SDL_TransferCharacteristics, SDL_MatrixCoefficients, SDL_ChromaLocation. These exist only
// for SDL_DEFINE_COLORSPACE and the decomposition macros (SDL_COLORSPACETYPE, etc.) which
// are not ported. The named SDL_Colorspace values cover all practical use cases. If custom
// colorspace composition is needed later, these enums can be added back.

/// <summary>
/// Colorspace definitions.
/// </summary>
public enum SDL_Colorspace : uint
{
    /// <summary>Unknown.</summary>
    SDL_COLORSPACE_UNKNOWN = 0,
    /// <summary>sRGB (default for 8-bit RGB surfaces).</summary>
    SDL_COLORSPACE_SRGB = 0x120005a0u,
    /// <summary>Linear sRGB (default for float surfaces).</summary>
    SDL_COLORSPACE_SRGB_LINEAR = 0x12000500u,
    /// <summary>HDR10.</summary>
    SDL_COLORSPACE_HDR10 = 0x12002600u,
    /// <summary>JPEG colorspace.</summary>
    SDL_COLORSPACE_JPEG = 0x220004c6u,
    /// <summary>BT.601, limited range.</summary>
    SDL_COLORSPACE_BT601_LIMITED = 0x211018c6u,
    /// <summary>BT.601, full range.</summary>
    SDL_COLORSPACE_BT601_FULL = 0x221018c6u,
    /// <summary>BT.709, limited range.</summary>
    SDL_COLORSPACE_BT709_LIMITED = 0x21100421u,
    /// <summary>BT.709, full range.</summary>
    SDL_COLORSPACE_BT709_FULL = 0x22100421u,
    /// <summary>BT.2020, limited range.</summary>
    SDL_COLORSPACE_BT2020_LIMITED = 0x21102609u,
    /// <summary>BT.2020, full range.</summary>
    SDL_COLORSPACE_BT2020_FULL = 0x22102609u,
    /// <summary>Default colorspace for RGB surfaces (alias for SRGB).</summary>
    SDL_COLORSPACE_RGB_DEFAULT = SDL_COLORSPACE_SRGB,
    /// <summary>Default colorspace for YUV surfaces (alias for BT601_LIMITED).</summary>
    SDL_COLORSPACE_YUV_DEFAULT = SDL_COLORSPACE_BT601_LIMITED,
}

/// <summary>
/// A color as RGBA components (8-bit per channel).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_Color
{
    /// <summary>Red component.</summary>
    public byte r;
    /// <summary>Green component.</summary>
    public byte g;
    /// <summary>Blue component.</summary>
    public byte b;
    /// <summary>Alpha component.</summary>
    public byte a;
}

/// <summary>
/// A color as RGBA components (floating point).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_FColor
{
    /// <summary>Red component.</summary>
    public float r;
    /// <summary>Green component.</summary>
    public float g;
    /// <summary>Blue component.</summary>
    public float b;
    /// <summary>Alpha component.</summary>
    public float a;
}

/// <summary>
/// A set of indexed colors representing a palette.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_Palette
{
    /// <summary>Number of colors in the palette.</summary>
    public int ncolors;
    /// <summary>Pointer to the color array.</summary>
    public SDL_Color* colors;
    /// <summary>Internal use only.</summary>
    public uint version;
    /// <summary>Internal use only.</summary>
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
    /// <summary>Bits per pixel.</summary>
    public byte bits_per_pixel;
    /// <summary>Bytes per pixel.</summary>
    public byte bytes_per_pixel;
    /// <summary>Padding.</summary>
    public byte padding0;
    /// <summary>Padding.</summary>
    public byte padding1;
    /// <summary>Red mask.</summary>
    public uint Rmask;
    /// <summary>Green mask.</summary>
    public uint Gmask;
    /// <summary>Blue mask.</summary>
    public uint Bmask;
    /// <summary>Alpha mask.</summary>
    public uint Amask;
    /// <summary>Red bits.</summary>
    public byte Rbits;
    /// <summary>Green bits.</summary>
    public byte Gbits;
    /// <summary>Blue bits.</summary>
    public byte Bbits;
    /// <summary>Alpha bits.</summary>
    public byte Abits;
    /// <summary>Red shift.</summary>
    public byte Rshift;
    /// <summary>Green shift.</summary>
    public byte Gshift;
    /// <summary>Blue shift.</summary>
    public byte Bshift;
    /// <summary>Alpha shift.</summary>
    public byte Ashift;
}

/// <summary>
/// Native bindings for SDL_pixels.h — pixel format and color functions.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Pixels
{
    /// <summary>A fully opaque 8-bit alpha value.</summary>
    public const byte SDL_ALPHA_OPAQUE = 255;

    /// <summary>A fully opaque floating point alpha value.</summary>
    public const float SDL_ALPHA_OPAQUE_FLOAT = 1.0f;

    /// <summary>A fully transparent 8-bit alpha value.</summary>
    public const byte SDL_ALPHA_TRANSPARENT = 0;

    /// <summary>A fully transparent floating point alpha value.</summary>
    public const float SDL_ALPHA_TRANSPARENT_FLOAT = 0.0f;

    /// <summary>
    /// Get the human readable name of a pixel format.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPixelFormatName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetPixelFormatName(SDL_PixelFormat format);

    /// <summary>
    /// Convert a pixel format to a bpp value and RGBA masks.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetMasksForPixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetMasksForPixelFormat(SDL_PixelFormat format, out int bpp, out uint Rmask, out uint Gmask, out uint Bmask, out uint Amask);

    /// <summary>
    /// Convert a bpp value and RGBA masks to a pixel format.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPixelFormatForMasks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PixelFormat SDL_GetPixelFormatForMasks(int bpp, uint Rmask, uint Gmask, uint Bmask, uint Amask);

    /// <summary>
    /// Create a pixel format details structure.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPixelFormatDetails")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PixelFormatDetails* SDL_GetPixelFormatDetails(SDL_PixelFormat format);

    /// <summary>
    /// Create a palette with the specified number of color entries.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreatePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Palette* SDL_CreatePalette(int ncolors);

    /// <summary>
    /// Set a range of colors in a palette.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetPaletteColors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetPaletteColors(SDL_Palette* palette, SDL_Color* colors, int firstcolor, int ncolors);

    /// <summary>
    /// Free a palette created with SDL_CreatePalette.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyPalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DestroyPalette(SDL_Palette* palette);

    /// <summary>
    /// Map an RGB triple to an opaque pixel value.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_MapRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint SDL_MapRGB(SDL_PixelFormatDetails* format, SDL_Palette* palette, byte r, byte g, byte b);

    /// <summary>
    /// Map an RGBA quadruple to a pixel value.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_MapRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint SDL_MapRGBA(SDL_PixelFormatDetails* format, SDL_Palette* palette, byte r, byte g, byte b, byte a);

    /// <summary>
    /// Get RGB values from a pixel in the specified format.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_GetRGB(uint pixelvalue, SDL_PixelFormatDetails* format, SDL_Palette* palette, out byte r, out byte g, out byte b);

    /// <summary>
    /// Get RGBA values from a pixel in the specified format.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_GetRGBA(uint pixelvalue, SDL_PixelFormatDetails* format, SDL_Palette* palette, out byte r, out byte g, out byte b, out byte a);
}
