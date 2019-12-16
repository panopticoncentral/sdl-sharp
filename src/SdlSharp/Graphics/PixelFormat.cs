namespace SdlSharp.Graphics
{
    /// <summary>
    /// A pixel format.
    /// </summary>
    public sealed unsafe class PixelFormat : NativePointerBase<Native.SDL_PixelFormat, PixelFormat>
    {
        /// <inheritdoc/>
        public override void Dispose()
        {
            Native.SDL_FreeFormat(Pointer);
            base.Dispose();
        }

        /// <summary>
        /// Sets the palette for this pixel format.
        /// </summary>
        /// <param name="palette">The palette.</param>
        public void SetPalette(Palette palette) =>
            Native.CheckError(Native.SDL_SetPixelFormatPalette(Pointer, palette.Pointer));

        /// <summary>
        /// Maps an RGB value to a pixel color in this format.
        /// </summary>
        /// <param name="red">The red value.</param>
        /// <param name="green">The green value.</param>
        /// <param name="blue">The blue value.</param>
        /// <returns>The pixel color.</returns>
        public PixelColor Map(byte red, byte green, byte blue) =>
            Native.SDL_MapRGB(Pointer, red, green, blue);

        /// <summary>
        /// Maps an RGBA value to a pixel color in this format.
        /// </summary>
        /// <param name="red">The red value.</param>
        /// <param name="green">The green value.</param>
        /// <param name="blue">The blue value.</param>
        /// <param name="alpha">The alpha value.</param>
        /// <returns>The pixel color.</returns>
        public PixelColor Map(byte red, byte green, byte blue, byte alpha) =>
            Native.SDL_MapRGBA(Pointer, red, green, blue, alpha);

        /// <summary>
        /// Maps a color to a pixel color in this format.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The pixel color.</returns>
        public PixelColor Map(Color color) =>
            Native.SDL_MapRGBA(Pointer, color.Red, color.Green, color.Blue, color.Alpha);

        /// <summary>
        /// Converts a pixel color to red, green, and blue values.
        /// </summary>
        /// <param name="pixel">The pixel color.</param>
        /// <returns>The values.</returns>
        public (byte Red, byte Green, byte Blue) GetRgb(PixelColor pixel)
        {
            Native.SDL_GetRGB(pixel, Pointer, out var red, out var green, out var blue);
            return (red, green, blue);
        }

        /// <summary>
        /// Converts a pixel color to red, green, blue, and alpha values.
        /// </summary>
        /// <param name="pixel">The pixel color.</param>
        /// <returns>The values.</returns>
        public (byte Red, byte Green, byte Blue, byte Alpha) GetRgba(PixelColor pixel)
        {
            Native.SDL_GetRGBA(pixel, Pointer, out var red, out var green, out var blue, out var alpha);
            return (red, green, blue, alpha);
        }
    }
}
