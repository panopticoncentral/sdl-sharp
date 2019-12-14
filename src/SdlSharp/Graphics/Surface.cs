using System;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// A surface.
    /// </summary>
    public sealed unsafe class Surface : NativePointerBase<Native.SDL_Surface, Surface>
    {
        /// <summary>
        /// The YUV conversion mode.
        /// </summary>
        public static YuvConversionMode YuvConversionMode
        {
            get => Native.SDL_GetYUVConversionMode();
            set => Native.SDL_SetYUVConversionMode(value);
        }

        /// <summary>
        /// The size of the surface.
        /// </summary>
        public Size Size => (Pointer->Width, Pointer->Height);

        /// <summary>
        /// The pitch of the surface.
        /// </summary>
        public int Pitch => Pointer->Pitch;

        /// <summary>
        /// The color modulator.
        /// </summary>
        public (byte Red, byte Green, byte Blue) ColorMod
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetSurfaceColorMod(Pointer, out var red, out var green, out var blue));
                return (red, green, blue);
            }
            set => _ = Native.CheckError(Native.SDL_SetSurfaceColorMod(Pointer, value.Red, value.Green, value.Blue));
        }

        /// <summary>
        /// The alpha modulator.
        /// </summary>
        public byte AlphaMod
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetSurfaceAlphaMod(Pointer, out var alpha));
                return alpha;
            }
            set => _ = Native.CheckError(Native.SDL_SetSurfaceAlphaMod(Pointer, value));
        }

        /// <summary>
        /// The blend mode.
        /// </summary>
        public BlendMode BlendMode
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetSurfaceBlendMode(Pointer, out var mode));
                return mode;
            }

            set => Native.CheckError(Native.SDL_SetSurfaceBlendMode(Pointer, value));
        }

        /// <summary>
        /// The color key defines a pixel value that will be treated as transparent in a blit.
        /// </summary>
        public PixelColor? ColorKey
        {
            get
            {
                if (!Native.SDL_HasColorKey(Pointer))
                {
                    return null;
                }

                _ = Native.CheckError(Native.SDL_GetColorKey(Pointer, out var key));
                return key;
            }
            set => _ = Native.CheckError(Native.SDL_SetColorKey(Pointer, value != null, value ?? (default)));
        }

        /// <summary>
        /// The pixel format.
        /// </summary>
        public PixelFormat PixelFormat =>
            PixelFormat.PointerToInstanceNotNull(Pointer->Format);

        /// <summary>
        /// Creates a surface.
        /// </summary>
        /// <param name="size">The size of the surface.</param>
        /// <param name="depth">The pixel depth.</param>
        /// <param name="mask">The color mask.</param>
        /// <returns>The surface.</returns>
        public static Surface Create(Size size, int depth, Color mask) =>
            PointerToInstanceNotNull(Native.SDL_CreateRGBSurface(0, size.Width, size.Height, depth, mask.Red, mask.Green, mask.Blue, mask.Alpha));

        /// <summary>
        /// Creates a surface.
        /// </summary>
        /// <param name="size">The size of the surface.</param>
        /// <param name="depth">The pixel depth.</param>
        /// <param name="format">The pixel format.</param>
        /// <returns>The surface.</returns>
        public static Surface Create(Size size, int depth, EnumeratedPixelFormat format) =>
            PointerToInstanceNotNull(Native.SDL_CreateRGBSurfaceWithFormat(0, size.Width, size.Height, depth, format.Format));

        /// <summary>
        /// Creates a surface from a set of pixels.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="size">The size of the surface.</param>
        /// <param name="depth">The pixel depth.</param>
        /// <param name="pitch">The pitch of the surface.</param>
        /// <param name="mask">The color mask.</param>
        /// <returns>The surface.</returns>
        public static Surface Create(Span<byte> pixels, Size size, int depth, int pitch, Color mask)
        {
            fixed (byte* pixelsPointer = pixels)
            {
                return PointerToInstanceNotNull(Native.SDL_CreateRGBSurfaceFrom(pixelsPointer, size.Width, size.Height, depth, pitch, mask.Red, mask.Green, mask.Blue, mask.Alpha));
            }
        }

        /// <summary>
        /// Creates a surface from a set of pixels.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="size">The size of the surface.</param>
        /// <param name="depth">The pixel depth.</param>
        /// <param name="pitch">The pitch of the surface.</param>
        /// <param name="format">The pixel format.</param>
        /// <returns>The surface.</returns>
        public static Surface Create(Span<byte> pixels, Size size, int depth, int pitch, EnumeratedPixelFormat format)
        {
            fixed (byte* pixelsPointer = pixels)
            {
                return PointerToInstanceNotNull(Native.SDL_CreateRGBSurfaceWithFormatFrom(pixelsPointer, size.Width, size.Height, depth, pitch, format.Format));
            }
        }

        /// <summary>
        /// Gets the YUV conversion mode for a particular resolution.
        /// </summary>
        /// <param name="size">The resolution.</param>
        /// <returns>The conversion mode.</returns>
        public static YuvConversionMode GetYuvConversionModeForResolution(Size size) =>
            Native.SDL_GetYUVConversionModeForResolution(size.Width, size.Height);

        /// <inheritdoc/>
        public override void Dispose()
        {
            Native.SDL_FreeSurface(Pointer);
            base.Dispose();
        }

        /// <summary>
        /// Loads a BMP into a surface.
        /// </summary>
        /// <param name="filename">The file name.</param>
        /// <returns>The surface.</returns>
        public static Surface LoadBmp(string filename) =>
            LoadBmp(RWOps.Create(filename, "rb"), true);

        /// <summary>
        /// Loads a BMP into a surface.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after loading the surface.</param>
        /// <returns>The surface.</returns>
        public static Surface LoadBmp(RWOps rwops, bool shouldDispose) =>
            PointerToInstanceNotNull(Native.SDL_LoadBMP_RW(rwops.Pointer, shouldDispose));

        /// <summary>
        /// Sets the palette.
        /// </summary>
        /// <param name="palette">The palette.</param>
        public void SetPalette(Palette palette) =>
            _ = Native.CheckError(Native.SDL_SetSurfacePalette(Pointer, palette.Pointer));

        /// <summary>
        /// Locks the surface.
        /// </summary>
        public void Lock() =>
            _ = Native.CheckError(Native.SDL_LockSurface(Pointer));

        /// <summary>
        /// Unlocks the surface.
        /// </summary>
        public void Unlock() =>
            Native.SDL_UnlockSurface(Pointer);

        /// <summary>
        /// The pixels of the surface.
        /// </summary>
        /// <typeparam name="T">The type of the pixel.</typeparam>
        /// <param name="bytesPerPixel">The number of bytes per pixel.</param>
        /// <returns>The pixels.</returns>
        public Span<T> GetPixels<T>() => Native.PixelsToSpan<T>(Pointer->Pixels, Pointer->Pitch, Pointer->Height);

        /// <summary>
        /// Saves the surface to a BMP.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void SaveBmp(string filename) =>
            SaveBmp(RWOps.Create(filename, "wb"), true);

        /// <summary>
        /// Saves the surface to a storage.
        /// </summary>
        /// <param name="rwops">The storage.</param>
        /// <param name="shouldDispose">Whether the storage should be disposed after saving the surface.</param>
        public void SaveBmp(RWOps rwops, bool shouldDispose) =>
            Native.CheckError(Native.SDL_SaveBMP_RW(Pointer, rwops.Pointer, shouldDispose));

        /// <summary>
        /// Sets RLE acceleration hint for the surface.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public void SetRle(bool flag) =>
            Native.CheckError(Native.SDL_SetSurfaceRLE(Pointer, flag));

        /// <summary>
        /// Gets the clipping rectangle.
        /// </summary>
        /// <returns></returns>
        public Rectangle GetClippingRectangle()
        {
            Native.SDL_GetClipRect(Pointer, out var rect);
            return rect;
        }

        /// <summary>
        /// Sets the clipping rectangle.
        /// </summary>
        /// <param name="clipRect">The clipping rectangle.</param>
        /// <returns><c>true</c> if the rectangle intersects the surface, otherwise <c>false</c> and blits will be completely clipped.</returns>
        public bool SetClippingRectangle(Rectangle? clipRect)
        {
            var rectPointer = (Rectangle*)null;
            Rectangle rect;

            if (clipRect.HasValue)
            {
                rect = clipRect.Value;
                rectPointer = &rect;
            }

            return Native.SDL_SetClipRect(Pointer, rectPointer);
        }

        /// <summary>
        /// Duplicates the surface.
        /// </summary>
        /// <returns>The duplicate surface.</returns>
        public Surface Duplicate() =>
            PointerToInstanceNotNull(Native.SDL_DuplicateSurface(Pointer));

        /// <summary>
        /// Converts the surface to a new pixel format.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <returns>The converted surface.</returns>
        public Surface Convert(PixelFormat format) =>
            PointerToInstanceNotNull(Native.SDL_ConvertSurface(Pointer, format.Pointer));

        /// <summary>
        /// Converts the surface to a new pixel format.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <returns>The converted surface.</returns>
        public Surface Convert(EnumeratedPixelFormat format) =>
           PointerToInstanceNotNull(Native.SDL_ConvertSurfaceFormat(Pointer, format));

        /// <summary>
        /// Fills a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="color">The pixel color.</param>
        public void FillRectangle(Rectangle? rectangle, PixelColor color)
        {
            var rectPointer = (Rectangle*)null;
            Rectangle rect;

            if (rectangle.HasValue)
            {
                rect = rectangle.Value;
                rectPointer = &rect;
            }

            _ = Native.CheckError(Native.SDL_FillRect(Pointer, rectPointer, color));
        }

        /// <summary>
        /// Fills rectangles.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        /// <param name="color">The pixel color.</param>
        public void FillRectangles(Rectangle[] rectangles, PixelColor color) =>
            Native.CheckError(Native.SDL_FillRects(Pointer, rectangles, rectangles.Length, color));

        /// <summary>
        /// Blits from the surface to another surface.
        /// </summary>
        /// <param name="destination">The destination surface.</param>
        /// <param name="sourceRectangle">The source area.</param>
        /// <param name="destinationRectangle">The destination area.</param>
        public void Blit(Surface destination, Rectangle? sourceRectangle = null, Rectangle? destinationRectangle = null)
        {
            var sourcePointer = (Rectangle*)null;
            var destinationPointer = (Rectangle*)null;
            Rectangle sourceRect;
            Rectangle destinationRect;

            if (sourceRectangle.HasValue)
            {
                sourceRect = sourceRectangle.Value;
                sourcePointer = &sourceRect;
            }

            if (destinationRectangle.HasValue)
            {
                destinationRect = destinationRectangle.Value;
                destinationPointer = &destinationRect;
            }

            _ = Native.CheckError(Native.SDL_UpperBlit(Pointer, sourcePointer, destination.Pointer, destinationPointer));
        }

        /// <summary>
        /// Blits from the surface to another surface with scaling.
        /// </summary>
        /// <param name="destination">The destination surface.</param>
        /// <param name="sourceRectangle">The source area.</param>
        /// <param name="destinationRectangle">The destination area.</param>
        public void BlitScaled(Surface destination, Rectangle? sourceRectangle = null, Rectangle? destinationRectangle = null)
        {
            var sourcePointer = (Rectangle*)null;
            var destinationPointer = (Rectangle*)null;
            Rectangle sourceRect;
            Rectangle destinationRect;

            if (sourceRectangle.HasValue)
            {
                sourceRect = sourceRectangle.Value;
                sourcePointer = &sourceRect;
            }

            if (destinationRectangle.HasValue)
            {
                destinationRect = destinationRectangle.Value;
                destinationPointer = &destinationRect;
            }

            _ = Native.CheckError(Native.SDL_UpperBlitScaled(Pointer, sourcePointer, destination.Pointer, destinationPointer));
        }
    }
}
