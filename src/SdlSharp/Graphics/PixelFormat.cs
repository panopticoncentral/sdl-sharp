namespace SdlSharp.Graphics
{
    /// <summary>
    /// A pixel format.
    /// </summary>
    public sealed unsafe class PixelFormat : IDisposable
    {
        private readonly Native.SDL_PixelFormat* _format;

        /// <summary>
        /// The ID of the format.
        /// </summary>
        public nuint Id => (nuint)_format;

        internal PixelFormat(Native.SDL_PixelFormat* format)
        {
            _format = format;
        }

        /// <inheritdoc/>
        public void Dispose() => Native.SDL_FreeFormat(_format);

        /// <summary>
        /// Sets the palette for this pixel format.
        /// </summary>
        /// <param name="palette">The palette.</param>
        public void SetPalette(Palette palette) =>
            Native.CheckError(Native.SDL_SetPixelFormatPalette(_format, palette.GetPointer()));

        /// <summary>
        /// Maps an RGB value to a pixel color in this format.
        /// </summary>
        /// <param name="red">The red value.</param>
        /// <param name="green">The green value.</param>
        /// <param name="blue">The blue value.</param>
        /// <returns>The pixel color.</returns>
        public PixelColor Map(byte red, byte green, byte blue) =>
            new(Native.SDL_MapRGB(_format, red, green, blue));

        /// <summary>
        /// Maps an RGBA value to a pixel color in this format.
        /// </summary>
        /// <param name="red">The red value.</param>
        /// <param name="green">The green value.</param>
        /// <param name="blue">The blue value.</param>
        /// <param name="alpha">The alpha value.</param>
        /// <returns>The pixel color.</returns>
        public PixelColor Map(byte red, byte green, byte blue, byte alpha) =>
            new(Native.SDL_MapRGBA(_format, red, green, blue, alpha));

        /// <summary>
        /// Maps a color to a pixel color in this format.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The pixel color.</returns>
        public PixelColor Map(Color color) =>
            new(Native.SDL_MapRGBA(_format, color.Red, color.Green, color.Blue, color.Alpha));

        /// <summary>
        /// Converts a pixel color to red, green, and blue values.
        /// </summary>
        /// <param name="pixel">The pixel color.</param>
        /// <returns>The values.</returns>
        public (byte Red, byte Green, byte Blue) GetRgb(PixelColor pixel)
        {
            byte red, green, blue;
            Native.SDL_GetRGB(pixel.Value, _format, &red, &green, &blue);
            return (red, green, blue);
        }

        /// <summary>
        /// Converts a pixel color to red, green, blue, and alpha values.
        /// </summary>
        /// <param name="pixel">The pixel color.</param>
        /// <returns>The values.</returns>
        public (byte Red, byte Green, byte Blue, byte Alpha) GetRgba(PixelColor pixel)
        {
            byte red, green, blue, alpha;
            Native.SDL_GetRGBA(pixel.Value, _format, &red, &green, &blue, &alpha);
            return (red, green, blue, alpha);
        }

        internal Native.SDL_PixelFormat* GetPointer() => _format;
    }
}
