namespace SdlSharp.Graphics
{
    /// <summary>
    /// A pixel format.
    /// </summary>
    public readonly unsafe record struct EnumeratedPixelFormat(uint Value)
    {
        /// <summary>
        /// Unknown format
        /// </summary>
        public static readonly EnumeratedPixelFormat Unknown = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_UNKNOWN);

        /// <summary>
        /// Index1Lsb format
        /// </summary>
        public static readonly EnumeratedPixelFormat Index1Lsb = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_INDEX1LSB);

        /// <summary>
        /// Index1Msb format
        /// </summary>
        public static readonly EnumeratedPixelFormat Index1Msb = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_INDEX1MSB);

        /// <summary>
        /// Index4Lsb format
        /// </summary>
        public static readonly EnumeratedPixelFormat Index4Lsb = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_INDEX4LSB);

        /// <summary>
        /// Index4Msb format
        /// </summary>
        public static readonly EnumeratedPixelFormat Index4Msb = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_INDEX4MSB);

        /// <summary>
        /// Index8 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Index8 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_INDEX8);
 
        /// <summary>
        /// Rgb332 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Rgb332 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_RGB332);

        /// <summary>
        /// Rgb444 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Rgb444 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_RGB444);

        /// <summary>
        /// Bgr444 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Bgr444 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_BGR444);

        /// <summary>
        /// Rgb555 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Rgb555 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_RGB555);

        /// <summary>
        /// Bgr555 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Bgr555 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_BGR555);

        /// <summary>
        /// ARgb4444 format
        /// </summary>
        public static readonly EnumeratedPixelFormat ARgb4444 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_ARGB4444);

        /// <summary>
        /// Rgba4444 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Rgba4444 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_RGBA4444);

        /// <summary>
        /// Abgr4444 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Abgr4444 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_ABGR4444);

        /// <summary>
        /// Bgra4444 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Bgra4444 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_BGRA4444);

        /// <summary>
        /// Argb1555 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Argb1555 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_ARGB1555);

        /// <summary>
        /// RgbA5551 format
        /// </summary>
        public static readonly EnumeratedPixelFormat RgbA5551 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_RGBA5551);

        /// <summary>
        /// Abgr1555 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Abgr1555 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_ABGR1555);

        /// <summary>
        /// Bgra5551 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Bgra5551 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_BGRA5551);

        /// <summary>
        /// Rgb565 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Rgb565 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_RGB565);

        /// <summary>
        /// Bgr565 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Bgr565 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_BGR565);

        /// <summary>
        /// Rgb24 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Rgb24 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_RGB24);

        /// <summary>
        /// Bgr24 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Bgr24 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_BGR24);

        /// <summary>
        /// Rgb888 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Rgb888 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_RGB888);

        /// <summary>
        /// Rgbx8888 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Rgbx8888 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_RGBX8888);

        /// <summary>
        /// Bgr888 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Bgr888 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_BGR888);

        /// <summary>
        /// Bgrx8888 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Bgrx8888 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_BGRX8888);

        /// <summary>
        /// Argb8888 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Argb8888 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_ARGB8888);

        /// <summary>
        /// Rgba8888 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Rgba8888 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_RGBA8888);

        /// <summary>
        /// Abgr8888 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Abgr8888 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_ABGR8888);

        /// <summary>
        /// Bgra8888 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Bgra8888 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_BGRA8888);

        /// <summary>
        /// Argb2101010 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Argb2101010 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_ARGB2101010);

        /// <summary>
        /// Rgba32 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Rgba32 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_RGBA32);

        /// <summary>
        /// Argb32 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Argb32 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_ARGB32);

        /// <summary>
        /// Bgra32 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Bgra32 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_BGRA32);

        /// <summary>
        /// Abgr32 format
        /// </summary>
        public static readonly EnumeratedPixelFormat Abgr32 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_ABGR32);

        /// <summary>
        /// Planar mode: Y + V + U  (3 planes)
        /// </summary>
        public static readonly EnumeratedPixelFormat Yv12 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_YV12);

        /// <summary>
        /// Planar mode: Y + U + V  (3 planes)
        /// </summary>
        public static readonly EnumeratedPixelFormat Iyuv = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_IYUV);

        /// <summary>
        /// Packed mode: Y0+U0+Y1+V0 (1 plane)
        /// </summary>
        public static readonly EnumeratedPixelFormat Yuy2 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_YUY2);

        /// <summary>
        /// Packed mode: U0+Y0+V0+Y1 (1 plane)
        /// </summary>
        public static readonly EnumeratedPixelFormat Uyvy = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_UYVY);

        /// <summary>
        /// Packed mode: Y0+V0+Y1+U0 (1 plane)
        /// </summary>
        public static readonly EnumeratedPixelFormat Yvyu = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_YVYU);

        /// <summary>
        /// Planar mode: Y + U/V interleaved  (2 planes)
        /// </summary>
        public static readonly EnumeratedPixelFormat Nv12 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_NV12);

        /// <summary>
        /// Planar mode: Y + V/U interleaved  (2 planes)
        /// </summary>
        public static readonly EnumeratedPixelFormat Nv21 = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_NV21);

        /// <summary>
        /// Android video texture format
        /// </summary>
        public static readonly EnumeratedPixelFormat ExternalOes = new(Native.SDL_PixelFormatEnum.SDL_PIXELFORMAT_EXTERNAL_OES);

        /// <summary>
        /// The name of the pixel format, if any.
        /// </summary>
        public string? Name => Native.Utf8ToString(Native.SDL_GetPixelFormatName(Value));

        /// <summary>
        /// Converts pixels from one format to another.
        /// </summary>
        /// <param name="size">The size of the buffers.</param>
        /// <param name="sourceFormat">The source pixel format.</param>
        /// <param name="source">The source pixels.</param>
        /// <param name="sourcePitch">The source pitch.</param>
        /// <param name="destinationFormat">The destination pixel format.</param>
        /// <param name="destination">The destination buffer.</param>
        /// <param name="destinationPitch">The destination pitch.</param>
        public static void ConvertPixels(Size size, EnumeratedPixelFormat sourceFormat, Span<byte> source, int sourcePitch, EnumeratedPixelFormat destinationFormat, Span<byte> destination, int destinationPitch)
        {
            fixed (byte* sourceBuffer = source)
            {
                fixed (byte* destinationBuffer = destination)
                {
                    _ = Native.CheckError(Native.SDL_ConvertPixels(size.Width, size.Height, sourceFormat, sourceBuffer, sourcePitch, destinationFormat, destinationBuffer, destinationPitch));
                }
            }
        }

        /// <summary>
        /// Gets the masks for the format.
        /// </summary>
        /// <param name="bitsPerPlane">The number of bits per plane.</param>
        /// <param name="redMask">The red mask.</param>
        /// <param name="greenMask">The green mask.</param>
        /// <param name="blueMask">The blue mask.</param>
        /// <param name="alphaMask">The alpha mask.</param>
        /// <returns>If the conversion succeeded.</returns>
        public bool ToMasks(out int bitsPerPlane, out uint redMask, out uint greenMask, out uint blueMask, out uint alphaMask)
        {
            int bpp;
            uint rmask, gmask, bmask, amask;
            var succeeded = Native.SDL_PixelFormatEnumToMasks(Value, &bpp, &rmask, &gmask, &bmask, &amask);
            bitsPerPlane = bpp;
            redMask = rmask;
            greenMask = gmask;
            blueMask = bmask;
            alphaMask = amask;
            return succeeded;
        }

        /// <summary>
        /// Gets a pixel format from masks.
        /// </summary>
        /// <param name="bitsPerPlane">The number of bits per plane.</param>
        /// <param name="redMask">The red mask.</param>
        /// <param name="greenMask">The green mask.</param>
        /// <param name="blueMask">The blue mask.</param>
        /// <param name="alphaMask">The alpha mask.</param>
        /// <returns>The pixel format.</returns>
        public static EnumeratedPixelFormat FromMasks(int bitsPerPlane, uint redMask, uint greenMask, uint blueMask, uint alphaMask) =>
            new(Native.SDL_MasksToPixelFormatEnum(bitsPerPlane, redMask, greenMask, blueMask, alphaMask));

        /// <summary>
        /// Converts to a pixel format.
        /// </summary>
        /// <returns>The pixel format.</returns>
        public PixelFormat ToPixelFormat() =>
            new(Native.SDL_AllocFormat(Value));
    }
}
