using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Pixels;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// A pixel format.
/// </summary>
public readonly unsafe record struct PixelFormat(SDL_PixelFormat Format)
{
    /// <summary>Unknown pixel format.</summary>
    public static readonly PixelFormat Unknown = new(SDL_PixelFormat.SDL_PIXELFORMAT_UNKNOWN);

    /// <summary>1-bit indexed, LSB (least significant bit) order.</summary>
    public static readonly PixelFormat Index1LSB = new(SDL_PixelFormat.SDL_PIXELFORMAT_INDEX1LSB);

    /// <summary>1-bit indexed, MSB (most significant bit) order.</summary>
    public static readonly PixelFormat Index1MSB = new(SDL_PixelFormat.SDL_PIXELFORMAT_INDEX1MSB);

    /// <summary>2-bit indexed, LSB order.</summary>
    public static readonly PixelFormat Index2LSB = new(SDL_PixelFormat.SDL_PIXELFORMAT_INDEX2LSB);

    /// <summary>2-bit indexed, MSB order.</summary>
    public static readonly PixelFormat Index2MSB = new(SDL_PixelFormat.SDL_PIXELFORMAT_INDEX2MSB);

    /// <summary>4-bit indexed, LSB order.</summary>
    public static readonly PixelFormat Index4LSB = new(SDL_PixelFormat.SDL_PIXELFORMAT_INDEX4LSB);

    /// <summary>4-bit indexed, MSB order.</summary>
    public static readonly PixelFormat Index4MSB = new(SDL_PixelFormat.SDL_PIXELFORMAT_INDEX4MSB);

    /// <summary>8-bit indexed.</summary>
    public static readonly PixelFormat Index8 = new(SDL_PixelFormat.SDL_PIXELFORMAT_INDEX8);

    /// <summary>8-bit RGB 3-3-2 format.</summary>
    public static readonly PixelFormat RGB332 = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGB332);

    /// <summary>16-bit XRGB 4-4-4-4 format.</summary>
    public static readonly PixelFormat XRGB4444 = new(SDL_PixelFormat.SDL_PIXELFORMAT_XRGB4444);

    /// <summary>16-bit XBGR 4-4-4-4 format.</summary>
    public static readonly PixelFormat XBGR4444 = new(SDL_PixelFormat.SDL_PIXELFORMAT_XBGR4444);

    /// <summary>16-bit XRGB 1-5-5-5 format.</summary>
    public static readonly PixelFormat XRGB1555 = new(SDL_PixelFormat.SDL_PIXELFORMAT_XRGB1555);

    /// <summary>16-bit XBGR 1-5-5-5 format.</summary>
    public static readonly PixelFormat XBGR1555 = new(SDL_PixelFormat.SDL_PIXELFORMAT_XBGR1555);

    /// <summary>16-bit ARGB 4-4-4-4 format.</summary>
    public static readonly PixelFormat ARGB4444 = new(SDL_PixelFormat.SDL_PIXELFORMAT_ARGB4444);

    /// <summary>16-bit RGBA 4-4-4-4 format.</summary>
    public static readonly PixelFormat RGBA4444 = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGBA4444);

    /// <summary>16-bit ABGR 4-4-4-4 format.</summary>
    public static readonly PixelFormat ABGR4444 = new(SDL_PixelFormat.SDL_PIXELFORMAT_ABGR4444);

    /// <summary>16-bit BGRA 4-4-4-4 format.</summary>
    public static readonly PixelFormat BGRA4444 = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGRA4444);

    /// <summary>16-bit ARGB 1-5-5-5 format.</summary>
    public static readonly PixelFormat ARGB1555 = new(SDL_PixelFormat.SDL_PIXELFORMAT_ARGB1555);

    /// <summary>16-bit RGBA 5-5-5-1 format.</summary>
    public static readonly PixelFormat RGBA5551 = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGBA5551);

    /// <summary>16-bit ABGR 1-5-5-5 format.</summary>
    public static readonly PixelFormat ABGR1555 = new(SDL_PixelFormat.SDL_PIXELFORMAT_ABGR1555);

    /// <summary>16-bit BGRA 5-5-5-1 format.</summary>
    public static readonly PixelFormat BGRA5551 = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGRA5551);

    /// <summary>16-bit RGB 5-6-5 format.</summary>
    public static readonly PixelFormat RGB565 = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGB565);

    /// <summary>16-bit BGR 5-6-5 format.</summary>
    public static readonly PixelFormat BGR565 = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGR565);

    /// <summary>24-bit RGB format (byte order: R, G, B).</summary>
    public static readonly PixelFormat RGB24 = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGB24);

    /// <summary>24-bit BGR format (byte order: B, G, R).</summary>
    public static readonly PixelFormat BGR24 = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGR24);

    /// <summary>32-bit XRGB 8-8-8-8 format.</summary>
    public static readonly PixelFormat XRGB8888 = new(SDL_PixelFormat.SDL_PIXELFORMAT_XRGB8888);

    /// <summary>32-bit RGBX 8-8-8-8 format.</summary>
    public static readonly PixelFormat RGBX8888 = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGBX8888);

    /// <summary>32-bit XBGR 8-8-8-8 format.</summary>
    public static readonly PixelFormat XBGR8888 = new(SDL_PixelFormat.SDL_PIXELFORMAT_XBGR8888);

    /// <summary>32-bit BGRX 8-8-8-8 format.</summary>
    public static readonly PixelFormat BGRX8888 = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGRX8888);

    /// <summary>32-bit ARGB 8-8-8-8 format.</summary>
    public static readonly PixelFormat ARGB8888 = new(SDL_PixelFormat.SDL_PIXELFORMAT_ARGB8888);

    /// <summary>32-bit RGBA 8-8-8-8 format.</summary>
    public static readonly PixelFormat RGBA8888 = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGBA8888);

    /// <summary>32-bit ABGR 8-8-8-8 format.</summary>
    public static readonly PixelFormat ABGR8888 = new(SDL_PixelFormat.SDL_PIXELFORMAT_ABGR8888);

    /// <summary>32-bit BGRA 8-8-8-8 format.</summary>
    public static readonly PixelFormat BGRA8888 = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGRA8888);

    /// <summary>32-bit XRGB 2-10-10-10 format.</summary>
    public static readonly PixelFormat XRGB2101010 = new(SDL_PixelFormat.SDL_PIXELFORMAT_XRGB2101010);

    /// <summary>32-bit XBGR 2-10-10-10 format.</summary>
    public static readonly PixelFormat XBGR2101010 = new(SDL_PixelFormat.SDL_PIXELFORMAT_XBGR2101010);

    /// <summary>32-bit ARGB 2-10-10-10 format.</summary>
    public static readonly PixelFormat ARGB2101010 = new(SDL_PixelFormat.SDL_PIXELFORMAT_ARGB2101010);

    /// <summary>32-bit ABGR 2-10-10-10 format.</summary>
    public static readonly PixelFormat ABGR2101010 = new(SDL_PixelFormat.SDL_PIXELFORMAT_ABGR2101010);

    /// <summary>48-bit RGB format (16 bits per component).</summary>
    public static readonly PixelFormat RGB48 = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGB48);

    /// <summary>48-bit BGR format (16 bits per component).</summary>
    public static readonly PixelFormat BGR48 = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGR48);

    /// <summary>64-bit RGBA format (16 bits per component).</summary>
    public static readonly PixelFormat RGBA64 = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGBA64);

    /// <summary>64-bit ARGB format (16 bits per component).</summary>
    public static readonly PixelFormat ARGB64 = new(SDL_PixelFormat.SDL_PIXELFORMAT_ARGB64);

    /// <summary>64-bit BGRA format (16 bits per component).</summary>
    public static readonly PixelFormat BGRA64 = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGRA64);

    /// <summary>64-bit ABGR format (16 bits per component).</summary>
    public static readonly PixelFormat ABGR64 = new(SDL_PixelFormat.SDL_PIXELFORMAT_ABGR64);

    /// <summary>48-bit RGB floating point format (16-bit float per component).</summary>
    public static readonly PixelFormat RGB48Float = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGB48_FLOAT);

    /// <summary>48-bit BGR floating point format (16-bit float per component).</summary>
    public static readonly PixelFormat BGR48Float = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGR48_FLOAT);

    /// <summary>64-bit RGBA floating point format (16-bit float per component).</summary>
    public static readonly PixelFormat RGBA64Float = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGBA64_FLOAT);

    /// <summary>64-bit ARGB floating point format (16-bit float per component).</summary>
    public static readonly PixelFormat ARGB64Float = new(SDL_PixelFormat.SDL_PIXELFORMAT_ARGB64_FLOAT);

    /// <summary>64-bit BGRA floating point format (16-bit float per component).</summary>
    public static readonly PixelFormat BGRA64Float = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGRA64_FLOAT);

    /// <summary>64-bit ABGR floating point format (16-bit float per component).</summary>
    public static readonly PixelFormat ABGR64Float = new(SDL_PixelFormat.SDL_PIXELFORMAT_ABGR64_FLOAT);

    /// <summary>96-bit RGB floating point format (32-bit float per component).</summary>
    public static readonly PixelFormat RGB96Float = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGB96_FLOAT);

    /// <summary>96-bit BGR floating point format (32-bit float per component).</summary>
    public static readonly PixelFormat BGR96Float = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGR96_FLOAT);

    /// <summary>128-bit RGBA floating point format (32-bit float per component).</summary>
    public static readonly PixelFormat RGBA128Float = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGBA128_FLOAT);

    /// <summary>128-bit ARGB floating point format (32-bit float per component).</summary>
    public static readonly PixelFormat ARGB128Float = new(SDL_PixelFormat.SDL_PIXELFORMAT_ARGB128_FLOAT);

    /// <summary>128-bit BGRA floating point format (32-bit float per component).</summary>
    public static readonly PixelFormat BGRA128Float = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGRA128_FLOAT);

    /// <summary>128-bit ABGR floating point format (32-bit float per component).</summary>
    public static readonly PixelFormat ABGR128Float = new(SDL_PixelFormat.SDL_PIXELFORMAT_ABGR128_FLOAT);

    /// <summary>Planar YUV format: Y + V + U (3 planes).</summary>
    public static readonly PixelFormat YV12 = new(SDL_PixelFormat.SDL_PIXELFORMAT_YV12);

    /// <summary>Planar YUV format: Y + U + V (3 planes).</summary>
    public static readonly PixelFormat IYUV = new(SDL_PixelFormat.SDL_PIXELFORMAT_IYUV);

    /// <summary>Packed YUV format: Y0+U0+Y1+V0 (1 plane).</summary>
    public static readonly PixelFormat YUY2 = new(SDL_PixelFormat.SDL_PIXELFORMAT_YUY2);

    /// <summary>Packed YUV format: U0+Y0+V0+Y1 (1 plane).</summary>
    public static readonly PixelFormat UYVY = new(SDL_PixelFormat.SDL_PIXELFORMAT_UYVY);

    /// <summary>Packed YUV format: Y0+V0+Y1+U0 (1 plane).</summary>
    public static readonly PixelFormat YVYU = new(SDL_PixelFormat.SDL_PIXELFORMAT_YVYU);

    /// <summary>Planar YUV format: Y + U/V interleaved (2 planes).</summary>
    public static readonly PixelFormat NV12 = new(SDL_PixelFormat.SDL_PIXELFORMAT_NV12);

    /// <summary>Planar YUV format: Y + V/U interleaved (2 planes).</summary>
    public static readonly PixelFormat NV21 = new(SDL_PixelFormat.SDL_PIXELFORMAT_NV21);

    /// <summary>Planar YUV 10-bit format: Y + U/V interleaved (2 planes).</summary>
    public static readonly PixelFormat P010 = new(SDL_PixelFormat.SDL_PIXELFORMAT_P010);

    /// <summary>Android video texture format.</summary>
    public static readonly PixelFormat ExternalOES = new(SDL_PixelFormat.SDL_PIXELFORMAT_EXTERNAL_OES);

    /// <summary>Motion JPEG format.</summary>
    public static readonly PixelFormat MJPG = new(SDL_PixelFormat.SDL_PIXELFORMAT_MJPG);

    /// <summary>32-bit RGBA byte array format (platform-dependent).</summary>
    public static readonly PixelFormat RGBA32 = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGBA32);

    /// <summary>32-bit ARGB byte array format (platform-dependent).</summary>
    public static readonly PixelFormat ARGB32 = new(SDL_PixelFormat.SDL_PIXELFORMAT_ARGB32);

    /// <summary>32-bit BGRA byte array format (platform-dependent).</summary>
    public static readonly PixelFormat BGRA32 = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGRA32);

    /// <summary>32-bit ABGR byte array format (platform-dependent).</summary>
    public static readonly PixelFormat ABGR32 = new(SDL_PixelFormat.SDL_PIXELFORMAT_ABGR32);

    /// <summary>32-bit RGBX byte array format (platform-dependent).</summary>
    public static readonly PixelFormat RGBX32 = new(SDL_PixelFormat.SDL_PIXELFORMAT_RGBX32);

    /// <summary>32-bit XRGB byte array format (platform-dependent).</summary>
    public static readonly PixelFormat XRGB32 = new(SDL_PixelFormat.SDL_PIXELFORMAT_XRGB32);

    /// <summary>32-bit BGRX byte array format (platform-dependent).</summary>
    public static readonly PixelFormat BGRX32 = new(SDL_PixelFormat.SDL_PIXELFORMAT_BGRX32);

    /// <summary>32-bit XBGR byte array format (platform-dependent).</summary>
    public static readonly PixelFormat XBGR32 = new(SDL_PixelFormat.SDL_PIXELFORMAT_XBGR32);

    /// <summary>
    /// The name of the format.
    /// </summary>
    public string Name => SDL_GetPixelFormatName(Format);

    /// <summary>
    /// Gets the details of the pixel format.
    /// </summary>
    public PixelFormatDetails Details => new(CheckPointer(SDL_GetPixelFormatDetails(Format)));

    /// <summary>
    /// Gets the pixel format flag.
    /// </summary>
    public byte Flag => (byte)SDL_PIXELFLAG(Format);

    /// <summary>
    /// Gets the pixel format type.
    /// </summary>
    public PixelType Type => (PixelType)SDL_PIXELTYPE(Format);

    /// <summary>
    /// Gets the pixel format order.
    /// </summary>
    public PixelOrder Order => (PixelOrder)SDL_PIXELORDER(Format);

    /// <summary>
    /// Gets the pixel format layout.
    /// </summary>
    public PixelLayout Layout => (PixelLayout)SDL_PIXELLAYOUT(Format);

    /// <summary>
    /// Gets the number of bits used to represent each pixel.
    /// </summary>
    public int BitsPerPixel => (int)SDL_BITSPERPIXEL(Format);

    /// <summary>
    /// Gets the number of bytes used to represent each pixel.
    /// </summary>
    public int BytesPerPixel => (int)SDL_BYTESPERPIXEL(Format);

    /// <summary>
    /// Gets a value indicating whether this is an indexed pixel format.
    /// </summary>
    public bool IsIndexed => SDL_ISPIXELFORMAT_INDEXED(Format);

    /// <summary>
    /// Gets a value indicating whether this is a packed pixel format.
    /// </summary>
    public bool IsPacked => SDL_ISPIXELFORMAT_PACKED(Format);

    /// <summary>
    /// Gets a value indicating whether this is an array pixel format.
    /// </summary>
    public bool IsArray => SDL_ISPIXELFORMAT_ARRAY(Format);

    /// <summary>
    /// Gets a value indicating whether this is a FourCC pixel format.
    /// </summary>
    public bool IsFourCC => SDL_ISPIXELFORMAT_FOURCC(Format);

    /// <summary>
    /// Gets a value indicating whether this is a floating point pixel format.
    /// </summary>
    public bool IsFloat => SDL_ISPIXELFORMAT_FLOAT(Format);

    /// <summary>
    /// Gets a value indicating whether this is a 10-bit pixel format.
    /// </summary>
    public bool Is10Bit => SDL_ISPIXELFORMAT_10BIT(Format);

    /// <summary>
    /// Gets a value indicating whether this is an alpha pixel format.
    /// </summary>
    public bool IsAlpha => SDL_ISPIXELFORMAT_ALPHA(Format);

    /// <summary>
    /// Gets the pixel mask for this pixel format.
    /// </summary>
    public PixelFormatMask Mask
    {
        get
        {
            int bits;
            uint red, green, blue, alpha;
            _ = CheckErrorBool(SDL_GetMasksForPixelFormat(Format, &bits, &red, &green, &blue, &alpha));
            return new PixelFormatMask(bits, red, green, blue, alpha);
        }
    }

    /// <summary>
    /// Creates a pixel format from a FourCC code.
    /// </summary>
    /// <param name="a">The first byte of the FourCC code.</param>
    /// <param name="b">The second byte of the FourCC code.</param>
    /// <param name="c">The third byte of the FourCC code.</param>
    /// <param name="d">The fourth byte of the FourCC code.</param>
    public PixelFormat(byte a, byte b, byte c, byte d)
        : this(SDL_DEFINE_PIXELFOURCC(a, b, c, d))
    {
    }

    /// <summary>
    /// Creates a custom PixelFormat.
    /// </summary>
    /// <param name="type">The pixel type that defines the color encoding (such as RGB, indexed, etc.).</param>
    /// <param name="order">The pixel order that specifies the arrangement of color components within each pixel.</param>
    /// <param name="layout">The pixel layout that determines how pixel data is organized in memory.</param>
    /// <param name="bits">The number of bits used to represent each pixel. Must be a positive integer.</param>
    /// <param name="bytes">The number of bytes used to store each pixel. Must be a positive integer.</param>
    public PixelFormat(PixelType type, PixelOrder order, PixelLayout layout, int bits, int bytes)
        : this(SDL_DEFINE_PIXELFORMAT((int)type, (int)order, (int)layout, bits, bytes))
    {
    }

    /// <summary>
    /// Creates a PixelFormat from a pixel mask.
    /// </summary>
    /// <param name="mask">The pixel mask.</param>
    public PixelFormat(PixelFormatMask mask)
        : this(SDL_GetPixelFormatForMasks(mask.Bits, mask.Red, mask.Green, mask.Blue, mask.Alpha))
    {

    }
}
