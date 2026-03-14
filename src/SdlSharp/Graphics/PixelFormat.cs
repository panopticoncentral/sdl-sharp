using System.Runtime.InteropServices;

using static SdlSharp.Native.Pixels;

namespace SdlSharp.Graphics;

/// <summary>
/// Pixel format identifiers.
/// </summary>
public enum PixelFormat : uint
{
    /// <summary>Unknown pixel format.</summary>
    Unknown = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_UNKNOWN,

    // Indexed formats
    /// <summary>1-bit indexed, LSB first.</summary>
    Index1Lsb = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_INDEX1LSB,
    /// <summary>1-bit indexed, MSB first.</summary>
    Index1Msb = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_INDEX1MSB,
    /// <summary>2-bit indexed, LSB first.</summary>
    Index2Lsb = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_INDEX2LSB,
    /// <summary>2-bit indexed, MSB first.</summary>
    Index2Msb = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_INDEX2MSB,
    /// <summary>4-bit indexed, LSB first.</summary>
    Index4Lsb = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_INDEX4LSB,
    /// <summary>4-bit indexed, MSB first.</summary>
    Index4Msb = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_INDEX4MSB,
    /// <summary>8-bit indexed.</summary>
    Index8 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_INDEX8,

    // Packed 8-bit
    /// <summary>Packed 8-bit, RGB332.</summary>
    Rgb332 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGB332,

    // Packed 16-bit
    /// <summary>Packed 16-bit, XRGB4444.</summary>
    Xrgb4444 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_XRGB4444,
    /// <summary>Packed 16-bit, XBGR4444.</summary>
    Xbgr4444 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_XBGR4444,
    /// <summary>Packed 16-bit, XRGB1555.</summary>
    Xrgb1555 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_XRGB1555,
    /// <summary>Packed 16-bit, XBGR1555.</summary>
    Xbgr1555 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_XBGR1555,
    /// <summary>Packed 16-bit, ARGB4444.</summary>
    Argb4444 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ARGB4444,
    /// <summary>Packed 16-bit, RGBA4444.</summary>
    Rgba4444 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGBA4444,
    /// <summary>Packed 16-bit, ABGR4444.</summary>
    Abgr4444 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ABGR4444,
    /// <summary>Packed 16-bit, BGRA4444.</summary>
    Bgra4444 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGRA4444,
    /// <summary>Packed 16-bit, ARGB1555.</summary>
    Argb1555 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ARGB1555,
    /// <summary>Packed 16-bit, RGBA5551.</summary>
    Rgba5551 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGBA5551,
    /// <summary>Packed 16-bit, ABGR1555.</summary>
    Abgr1555 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ABGR1555,
    /// <summary>Packed 16-bit, BGRA5551.</summary>
    Bgra5551 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGRA5551,
    /// <summary>Packed 16-bit, RGB565.</summary>
    Rgb565 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGB565,
    /// <summary>Packed 16-bit, BGR565.</summary>
    Bgr565 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGR565,

    // Array 24-bit
    /// <summary>Array 24-bit, RGB byte order.</summary>
    Rgb24 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGB24,
    /// <summary>Array 24-bit, BGR byte order.</summary>
    Bgr24 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGR24,

    // Packed 32-bit
    /// <summary>Packed 32-bit, XRGB8888.</summary>
    Xrgb8888 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_XRGB8888,
    /// <summary>Packed 32-bit, RGBX8888.</summary>
    Rgbx8888 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGBX8888,
    /// <summary>Packed 32-bit, XBGR8888.</summary>
    Xbgr8888 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_XBGR8888,
    /// <summary>Packed 32-bit, BGRX8888.</summary>
    Bgrx8888 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGRX8888,
    /// <summary>Packed 32-bit, ARGB8888.</summary>
    Argb8888 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ARGB8888,
    /// <summary>Packed 32-bit, RGBA8888.</summary>
    Rgba8888 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGBA8888,
    /// <summary>Packed 32-bit, ABGR8888.</summary>
    Abgr8888 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ABGR8888,
    /// <summary>Packed 32-bit, BGRA8888.</summary>
    Bgra8888 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGRA8888,
    /// <summary>Packed 32-bit, XRGB2101010.</summary>
    Xrgb2101010 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_XRGB2101010,
    /// <summary>Packed 32-bit, XBGR2101010.</summary>
    Xbgr2101010 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_XBGR2101010,
    /// <summary>Packed 32-bit, ARGB2101010.</summary>
    Argb2101010 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ARGB2101010,
    /// <summary>Packed 32-bit, ABGR2101010.</summary>
    Abgr2101010 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ABGR2101010,

    // Array 48-bit unsigned
    /// <summary>Array 48-bit, RGB unsigned 16-bit.</summary>
    Rgb48 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGB48,
    /// <summary>Array 48-bit, BGR unsigned 16-bit.</summary>
    Bgr48 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGR48,

    // Array 64-bit unsigned
    /// <summary>Array 64-bit, RGBA unsigned 16-bit.</summary>
    Rgba64 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGBA64,
    /// <summary>Array 64-bit, ARGB unsigned 16-bit.</summary>
    Argb64 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ARGB64,
    /// <summary>Array 64-bit, BGRA unsigned 16-bit.</summary>
    Bgra64 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGRA64,
    /// <summary>Array 64-bit, ABGR unsigned 16-bit.</summary>
    Abgr64 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ABGR64,

    // Array float16
    /// <summary>Array 48-bit, RGB float16.</summary>
    Rgb48Float = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGB48_FLOAT,
    /// <summary>Array 48-bit, BGR float16.</summary>
    Bgr48Float = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGR48_FLOAT,
    /// <summary>Array 64-bit, RGBA float16.</summary>
    Rgba64Float = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGBA64_FLOAT,
    /// <summary>Array 64-bit, ARGB float16.</summary>
    Argb64Float = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ARGB64_FLOAT,
    /// <summary>Array 64-bit, BGRA float16.</summary>
    Bgra64Float = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGRA64_FLOAT,
    /// <summary>Array 64-bit, ABGR float16.</summary>
    Abgr64Float = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ABGR64_FLOAT,

    // Array float32
    /// <summary>Array 96-bit, RGB float32.</summary>
    Rgb96Float = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGB96_FLOAT,
    /// <summary>Array 96-bit, BGR float32.</summary>
    Bgr96Float = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGR96_FLOAT,
    /// <summary>Array 128-bit, RGBA float32.</summary>
    Rgba128Float = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGBA128_FLOAT,
    /// <summary>Array 128-bit, ARGB float32.</summary>
    Argb128Float = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ARGB128_FLOAT,
    /// <summary>Array 128-bit, BGRA float32.</summary>
    Bgra128Float = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGRA128_FLOAT,
    /// <summary>Array 128-bit, ABGR float32.</summary>
    Abgr128Float = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ABGR128_FLOAT,

    // YUV formats
    /// <summary>Planar mode: Y + V + U (3 planes).</summary>
    Yv12 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_YV12,
    /// <summary>Planar mode: Y + U + V (3 planes).</summary>
    Iyuv = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_IYUV,
    /// <summary>Packed mode: Y0+U0+Y1+V0 (1 plane).</summary>
    Yuy2 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_YUY2,
    /// <summary>Packed mode: U0+Y0+V0+Y1 (1 plane).</summary>
    Uyvy = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_UYVY,
    /// <summary>Packed mode: Y0+V0+Y1+U0 (1 plane).</summary>
    Yvyu = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_YVYU,
    /// <summary>Planar mode: Y + U/V interleaved (2 planes).</summary>
    Nv12 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_NV12,
    /// <summary>Planar mode: Y + V/U interleaved (2 planes).</summary>
    Nv21 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_NV21,
    /// <summary>Planar mode: Y + U/V interleaved, 10-bit (2 planes).</summary>
    P010 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_P010,
    /// <summary>Android video texture format.</summary>
    ExternalOes = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_EXTERNAL_OES,
    /// <summary>Motion JPEG.</summary>
    Mjpg = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_MJPG,

    // Byte-array aliases (little-endian mappings — .NET targets are overwhelmingly LE)
    /// <summary>RGBA byte array (platform-appropriate alias).</summary>
    Rgba32 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGBA32,
    /// <summary>ARGB byte array (platform-appropriate alias).</summary>
    Argb32 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ARGB32,
    /// <summary>BGRA byte array (platform-appropriate alias).</summary>
    Bgra32 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGRA32,
    /// <summary>ABGR byte array (platform-appropriate alias).</summary>
    Abgr32 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_ABGR32,
    /// <summary>RGBX byte array (platform-appropriate alias).</summary>
    Rgbx32 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_RGBX32,
    /// <summary>XRGB byte array (platform-appropriate alias).</summary>
    Xrgb32 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_XRGB32,
    /// <summary>BGRX byte array (platform-appropriate alias).</summary>
    Bgrx32 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_BGRX32,
    /// <summary>XBGR byte array (platform-appropriate alias).</summary>
    Xbgr32 = (uint)Native.SDL_PixelFormat.SDL_PIXELFORMAT_XBGR32,
}

/// <summary>
/// Pixel format helpers.
/// </summary>
public static class PixelFormatExtensions
{
    /// <summary>
    /// Gets the human readable name of a pixel format.
    /// </summary>
    public static unsafe string GetName(this PixelFormat format) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetPixelFormatName((Native.SDL_PixelFormat)format)) ?? "SDL_PIXELFORMAT_UNKNOWN";

    /// <summary>
    /// Gets the details for a pixel format.
    /// </summary>
    public static PixelFormatDetails GetDetails(this PixelFormat format) =>
        PixelFormatDetails.Get(format);

    /// <summary>
    /// Converts a pixel format to a bpp value and RGBA masks.
    /// </summary>
    public static bool GetMasks(this PixelFormat format, out int bpp, out uint rMask, out uint gMask, out uint bMask, out uint aMask) =>
        SDL_GetMasksForPixelFormat((Native.SDL_PixelFormat)format, out bpp, out rMask, out gMask, out bMask, out aMask);

    /// <summary>
    /// Converts a bpp value and RGBA masks to a pixel format.
    /// </summary>
    public static PixelFormat FromMasks(int bpp, uint rMask, uint gMask, uint bMask, uint aMask) =>
        (PixelFormat)SDL_GetPixelFormatForMasks(bpp, rMask, gMask, bMask, aMask);
}
