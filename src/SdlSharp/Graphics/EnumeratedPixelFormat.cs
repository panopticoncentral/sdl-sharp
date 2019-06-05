using System;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// A pixel format.
    /// </summary>
    public unsafe readonly struct EnumeratedPixelFormat
    {
        public static readonly EnumeratedPixelFormat Unknown = new EnumeratedPixelFormat(0);

        public static readonly EnumeratedPixelFormat Index1Lsb =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Index1, (uint)Native.SDL_BitmapOrder.Order4321, 0, 1, 0);

        public static readonly EnumeratedPixelFormat Index1Msb =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Index1, (uint)Native.SDL_BitmapOrder.Order1234, 0, 1, 0);

        public static readonly EnumeratedPixelFormat Index4Lsb =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Index4, (uint)Native.SDL_BitmapOrder.Order4321, 0, 4, 0);

        public static readonly EnumeratedPixelFormat Index4Msb =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Index4, (uint)Native.SDL_BitmapOrder.Order1234, 0, 4, 0);

        public static readonly EnumeratedPixelFormat Index8 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Index8, 0, 0, 8, 1);

        public static readonly EnumeratedPixelFormat Rgb332 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed8, (uint)Native.SDL_PackedOrder.Xrgb, (uint)Native.SDL_PackedLayout.Layout332, 8, 1);

        public static readonly EnumeratedPixelFormat Rgb444 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Xrgb, (uint)Native.SDL_PackedLayout.Layout4444, 12, 2);

        public static readonly EnumeratedPixelFormat Rgb555 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Xrgb, (uint)Native.SDL_PackedLayout.Layout1555, 15, 2);

        public static readonly EnumeratedPixelFormat Bgr555 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Xbgr, (uint)Native.SDL_PackedLayout.Layout1555, 15, 2);

        public static readonly EnumeratedPixelFormat ARgb4444 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Argb, (uint)Native.SDL_PackedLayout.Layout4444, 16, 2);

        public static readonly EnumeratedPixelFormat Rgba4444 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Rgba, (uint)Native.SDL_PackedLayout.Layout4444, 16, 2);

        public static readonly EnumeratedPixelFormat Abgr4444 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Abgr, (uint)Native.SDL_PackedLayout.Layout4444, 16, 2);

        public static readonly EnumeratedPixelFormat Bgra4444 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Bgra, (uint)Native.SDL_PackedLayout.Layout4444, 16, 2);

        public static readonly EnumeratedPixelFormat Argb1555 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Argb, (uint)Native.SDL_PackedLayout.Layout1555, 16, 2);

        public static readonly EnumeratedPixelFormat RgbA5551 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Rgba, (uint)Native.SDL_PackedLayout.Layout5551, 16, 2);

        public static readonly EnumeratedPixelFormat Abgr1555 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Abgr, (uint)Native.SDL_PackedLayout.Layout1555, 16, 2);

        public static readonly EnumeratedPixelFormat Bgra5551 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Bgra, (uint)Native.SDL_PackedLayout.Layout5551, 16, 2);

        public static readonly EnumeratedPixelFormat Rgb565 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Xrgb, (uint)Native.SDL_PackedLayout.Layout565, 16, 2);

        public static readonly EnumeratedPixelFormat Bgr565 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed16, (uint)Native.SDL_PackedOrder.Xbgr, (uint)Native.SDL_PackedLayout.Layout565, 16, 2);

        public static readonly EnumeratedPixelFormat Rgb24 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.ArrayU8, (uint)Native.SDL_ArrayOrder.Rgb, 0, 24, 3);

        public static readonly EnumeratedPixelFormat Bgr24 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.ArrayU8, (uint)Native.SDL_ArrayOrder.Bgr, 0, 24, 3);

        public static readonly EnumeratedPixelFormat Rgb888 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed32, (uint)Native.SDL_PackedOrder.Xrgb, (uint)Native.SDL_PackedLayout.Layout8888, 24, 4);

        public static readonly EnumeratedPixelFormat Rgbx8888 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed32, (uint)Native.SDL_PackedOrder.Rgbx, (uint)Native.SDL_PackedLayout.Layout8888, 24, 4);

        public static readonly EnumeratedPixelFormat Bgr888 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed32, (uint)Native.SDL_PackedOrder.Xbgr, (uint)Native.SDL_PackedLayout.Layout8888, 24, 4);

        public static readonly EnumeratedPixelFormat Bgrx8888 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed32, (uint)Native.SDL_PackedOrder.Bgrx, (uint)Native.SDL_PackedLayout.Layout8888, 24, 4);

        public static readonly EnumeratedPixelFormat Argb8888 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed32, (uint)Native.SDL_PackedOrder.Argb, (uint)Native.SDL_PackedLayout.Layout8888, 32, 4);

        public static readonly EnumeratedPixelFormat Rgba8888 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed32, (uint)Native.SDL_PackedOrder.Rgba, (uint)Native.SDL_PackedLayout.Layout8888, 32, 4);

        public static readonly EnumeratedPixelFormat Abgr8888 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed32, (uint)Native.SDL_PackedOrder.Abgr, (uint)Native.SDL_PackedLayout.Layout8888, 32, 4);

        public static readonly EnumeratedPixelFormat Bgra8888 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed32, (uint)Native.SDL_PackedOrder.Bgra, (uint)Native.SDL_PackedLayout.Layout8888, 32, 4);

        public static readonly EnumeratedPixelFormat Argb2101010 =
            new EnumeratedPixelFormat((uint)Native.SDL_PixelType.Packed32, (uint)Native.SDL_PackedOrder.Argb, (uint)Native.SDL_PackedLayout.Layout2101010, 32, 4);

        public static readonly EnumeratedPixelFormat Rgba32 = Abgr8888;
        public static readonly EnumeratedPixelFormat Argb32 = Bgra8888;
        public static readonly EnumeratedPixelFormat Bgra32 = Argb8888;
        public static readonly EnumeratedPixelFormat Abgr32 = Rgba8888;

        public static readonly EnumeratedPixelFormat Yv12 =      /**< Planar mode: Y + V + U  (3 planes) */
            new EnumeratedPixelFormat('Y', 'V', '1', '2');

        public static readonly EnumeratedPixelFormat Iyuv =      /**< Planar mode: Y + U + V  (3 planes) */
            new EnumeratedPixelFormat('I', 'Y', 'U', 'V');

        public static readonly EnumeratedPixelFormat Yuy2 =      /**< Packed mode: Y0+U0+Y1+V0 (1 plane) */
            new EnumeratedPixelFormat('Y', 'U', 'Y', '2');

        public static readonly EnumeratedPixelFormat Uyvy =      /**< Packed mode: U0+Y0+V0+Y1 (1 plane) */
            new EnumeratedPixelFormat('U', 'Y', 'V', 'Y');

        public static readonly EnumeratedPixelFormat Yvyu =      /**< Packed mode: Y0+V0+Y1+U0 (1 plane) */
            new EnumeratedPixelFormat('Y', 'V', 'Y', 'U');

        public static readonly EnumeratedPixelFormat Nv12 =      /**< Planar mode: Y + U/V interleaved  (2 planes) */
            new EnumeratedPixelFormat('N', 'V', '1', '2');

        public static readonly EnumeratedPixelFormat Nv21 =      /**< Planar mode: Y + V/U interleaved  (2 planes) */
            new EnumeratedPixelFormat('N', 'V', '2', '1');

        public static readonly EnumeratedPixelFormat ExternalOes =      /**< Android video texture format */
            new EnumeratedPixelFormat('O', 'E', 'S', ' ');

        internal readonly uint Format { get; }

        /// <summary>
        /// The name of the pixel format, if any.
        /// </summary>
        public string? Name => Native.SDL_GetPixelFormatName(Format);

        internal EnumeratedPixelFormat(uint format)
        {
            Format = format;
        }

        private EnumeratedPixelFormat(uint type, uint order, uint layout, uint bits, uint bytes)
        {
            Format = Native.SDL_DefinePixelFormat(type, order, layout, bits, bytes);
        }

        private EnumeratedPixelFormat(char a, char b, char c, char d)
        {
            Format = Native.SDL_DefinePixelFormatCharacters(a, b, c, d);
        }

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
        public bool ToMasks(out int bitsPerPlane, out uint redMask, out uint greenMask, out uint blueMask, out uint alphaMask) =>
            Native.SDL_PixelFormatEnumToMasks(Format, out bitsPerPlane, out redMask, out greenMask, out blueMask, out alphaMask);

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
            new EnumeratedPixelFormat(Native.SDL_MasksToPixelFormatEnum(bitsPerPlane, redMask, greenMask, blueMask, alphaMask));

        /// <summary>
        /// Converts to a pixel format.
        /// </summary>
        /// <returns>The pixel format.</returns>
        public PixelFormat ToPixelFormat() =>
            PixelFormat.PointerToInstanceNotNull(Native.SDL_AllocFormat(Format));
    }
}
