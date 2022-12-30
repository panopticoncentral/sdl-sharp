namespace SdlSharp.Graphics
{
    /// <summary>
    /// A surface.
    /// </summary>
    public sealed unsafe class Surface : IDisposable
    {
        private readonly Native.SDL_Surface* _surface;

        /// <summary>
        /// The YUV conversion mode.
        /// </summary>
        public static YuvConversionMode YuvConversionMode
        {
            get => (YuvConversionMode)Native.SDL_GetYUVConversionMode();
            set => Native.SDL_SetYUVConversionMode((Native.SDL_YUV_CONVERSION_MODE)value);
        }

        /// <summary>
        /// The size of the surface.
        /// </summary>
        public Size Size => (_surface->w, _surface->h);

        /// <summary>
        /// The pitch of the surface.
        /// </summary>
        public int Pitch => _surface->pitch;

        /// <summary>
        /// The color modulator.
        /// </summary>
        public (byte Red, byte Green, byte Blue) ColorMod
        {
            get
            {
                byte red, green, blue;
                _ = Native.CheckError(Native.SDL_GetSurfaceColorMod(_surface, &red, &green, &blue));
                return (red, green, blue);
            }
            set => _ = Native.CheckError(Native.SDL_SetSurfaceColorMod(_surface, value.Red, value.Green, value.Blue));
        }

        /// <summary>
        /// The alpha modulator.
        /// </summary>
        public byte AlphaMod
        {
            get
            {
                byte alpha;
                _ = Native.CheckError(Native.SDL_GetSurfaceAlphaMod(_surface, &alpha));
                return alpha;
            }
            set => _ = Native.CheckError(Native.SDL_SetSurfaceAlphaMod(_surface, value));
        }

        /// <summary>
        /// The blend mode.
        /// </summary>
        public BlendMode BlendMode
        {
            get
            {
                Native.SDL_BlendMode mode;
                _ = Native.CheckError(Native.SDL_GetSurfaceBlendMode(_surface, &mode));
                return new(mode);
            }

            set => Native.CheckError(Native.SDL_SetSurfaceBlendMode(_surface, value.ToNative()));
        }

        /// <summary>
        /// The color key defines a pixel value that will be treated as transparent in a blit.
        /// </summary>
        public PixelColor? ColorKey
        {
            get
            {
                if (!Native.SDL_HasColorKey(_surface))
                {
                    return null;
                }

                uint key;
                _ = Native.CheckError(Native.SDL_GetColorKey(_surface, &key));
                return new(key);
            }
            set => _ = Native.CheckError(Native.SDL_SetColorKey(_surface, value != null ? 1 : 0, (value ?? (default)).Value));
        }

        /// <summary>
        /// The pixel format.
        /// </summary>
        public PixelFormat PixelFormat =>
            new(_surface->format);

        /// <summary>
        /// Whether RLE is enabled for this surface.
        /// </summary>
        public bool HasRle => Native.SDL_HasSurfaceRLE(_surface);

        internal Surface(Native.SDL_Surface* surface)
        {
            _surface = surface;
        }

        /// <summary>
        /// Creates a surface.
        /// </summary>
        /// <param name="size">The size of the surface.</param>
        /// <param name="depth">The pixel depth.</param>
        /// <param name="mask">The color mask.</param>
        /// <returns>The surface.</returns>
        public static Surface Create(Size size, int depth, Color mask) =>
            new(Native.SDL_CreateRGBSurface(0, size.Width, size.Height, depth, mask.Red, mask.Green, mask.Blue, mask.Alpha));

        /// <summary>
        /// Creates a surface.
        /// </summary>
        /// <param name="size">The size of the surface.</param>
        /// <param name="depth">The pixel depth.</param>
        /// <param name="format">The pixel format.</param>
        /// <returns>The surface.</returns>
        public static Surface Create(Size size, int depth, EnumeratedPixelFormat format) =>
            new(Native.SDL_CreateRGBSurfaceWithFormat(0, size.Width, size.Height, depth, format.Value));

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
                return new(Native.SDL_CreateRGBSurfaceFrom(pixelsPointer, size.Width, size.Height, depth, pitch, mask.Red, mask.Green, mask.Blue, mask.Alpha));
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
                return new(Native.SDL_CreateRGBSurfaceWithFormatFrom(pixelsPointer, size.Width, size.Height, depth, pitch, format.Value));
            }
        }

        /// <summary>
        /// Gets the YUV conversion mode for a particular resolution.
        /// </summary>
        /// <param name="size">The resolution.</param>
        /// <returns>The conversion mode.</returns>
        public static YuvConversionMode GetYuvConversionModeForResolution(Size size) =>
            (YuvConversionMode)Native.SDL_GetYUVConversionModeForResolution(size.Width, size.Height);

        /// <inheritdoc/>
        public void Dispose() => Native.SDL_FreeSurface(_surface);

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
            new(Native.SDL_LoadBMP_RW(rwops.ToNative(), Native.BoolToInt(shouldDispose)));

        /// <summary>
        /// Loads a BMP into a surface compatible with a target surface.
        /// </summary>
        /// <param name="filename">The file name.</param>
        /// <param name="targetSurface">The target surface.</param>
        /// <returns>The surface.</returns>
        public static Surface LoadBmp(string filename, Surface targetSurface)
        {
            using var loadedSurface = LoadBmp(filename);
            return loadedSurface.Convert(targetSurface.PixelFormat);
        }

        /// <summary>
        /// Sets the palette.
        /// </summary>
        /// <param name="palette">The palette.</param>
        public void SetPalette(Palette palette) =>
            _ = Native.CheckError(Native.SDL_SetSurfacePalette(_surface, palette.GetPointer()));

        /// <summary>
        /// Locks the surface.
        /// </summary>
        public void Lock() =>
            _ = Native.CheckError(Native.SDL_LockSurface(_surface));

        /// <summary>
        /// Unlocks the surface.
        /// </summary>
        public void Unlock() =>
            Native.SDL_UnlockSurface(_surface);

        /// <summary>
        /// The pixels of the surface.
        /// </summary>
        /// <typeparam name="T">The type of the pixel.</typeparam>
        /// <returns>The pixels.</returns>
        public Span<T> GetPixels<T>() => Native.PixelsToSpan<T>(_surface->Pixels, _surface->Pitch, _surface->Height);

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
            Native.CheckError(Native.SDL_SaveBMP_RW(_surface, rwops.ToNative(), Native.BoolToInt(shouldDispose)));

        /// <summary>
        /// Sets RLE acceleration hint for the surface.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public void SetRle(bool flag) =>
            Native.CheckError(Native.SDL_SetSurfaceRLE(_surface, Native.BoolToInt(flag)));

        /// <summary>
        /// Gets the clipping rectangle.
        /// </summary>
        /// <returns></returns>
        public Rectangle GetClippingRectangle()
        {
            Native.SDL_Rect rect;
            Native.SDL_GetClipRect(_surface, &rect);
            return new(rect);
        }

        /// <summary>
        /// Sets the clipping rectangle.
        /// </summary>
        /// <param name="clipRect">The clipping rectangle.</param>
        /// <returns><c>true</c> if the rectangle intersects the surface, otherwise <c>false</c> and blits will be completely clipped.</returns>
        public bool SetClippingRectangle(Rectangle? clipRect)
        {
            Native.SDL_Rect rect;
            return Native.SDL_SetClipRect(_surface, Rectangle.ToNative(clipRect, &rect));
        }

        /// <summary>
        /// Duplicates the surface.
        /// </summary>
        /// <returns>The duplicate surface.</returns>
        public Surface Duplicate() =>
            new(Native.SDL_DuplicateSurface(_surface));

        /// <summary>
        /// Converts the surface to a new pixel format.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <returns>The converted surface.</returns>
        public Surface Convert(PixelFormat format) =>
            new(Native.SDL_ConvertSurface(_surface, format.GetPointer()));

        /// <summary>
        /// Converts the surface to a new pixel format.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <returns>The converted surface.</returns>
        public Surface Convert(EnumeratedPixelFormat format) =>
           new(Native.SDL_ConvertSurfaceFormat(_surface, format.Value));

        /// <summary>
        /// Fills a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="color">The pixel color.</param>
        public void FillRectangle(Rectangle? rectangle, PixelColor color)
        {
            Native.SDL_Rect rect;
            _ = Native.CheckError(Native.SDL_FillRect(_surface, Rectangle.ToNative(rectangle, &rect), color.Value));
        }

        /// <summary>
        /// Fills rectangles.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        /// <param name="color">The pixel color.</param>
        public void FillRectangles(Rectangle[] rectangles, PixelColor color)
        {
            fixed (Rectangle* ptr = rectangles)
            {
                _ = Native.CheckError(Native.SDL_FillRects(_surface, (Native.SDL_Rect*)ptr, rectangles.Length, color.Value));
            }
        }

        /// <summary>
        /// Blits from the surface to another surface.
        /// </summary>
        /// <param name="destination">The destination surface.</param>
        /// <param name="sourceRectangle">The source area.</param>
        /// <param name="destinationRectangle">The destination area.</param>
        public void Blit(Surface destination, Rectangle? sourceRectangle = null, Rectangle? destinationRectangle = null)
        {
            Native.SDL_Rect sourceRect, destRect;
            _ = Native.CheckError(Native.SDL_BlitSurface(_surface, Rectangle.ToNative(sourceRectangle, &sourceRect), destination._surface, Rectangle.ToNative(destinationRectangle, &destRect)));
        }

        /// <summary>
        /// Blits from the surface to another surface with scaling.
        /// </summary>
        /// <param name="destination">The destination surface.</param>
        /// <param name="sourceRectangle">The source area.</param>
        /// <param name="destinationRectangle">The destination area.</param>
        public void BlitScaled(Surface destination, Rectangle? sourceRectangle = null, Rectangle? destinationRectangle = null)
        {
            Native.SDL_Rect sourceRect, destRect;
            _ = Native.CheckError(Native.SDL_BlitScaled(_surface, Rectangle.ToNative(sourceRectangle, &sourceRect), destination._surface, Rectangle.ToNative(destinationRectangle, &destRect)));
        }

        internal Native.SDL_Surface* ToNative() => _surface;
    }
}
