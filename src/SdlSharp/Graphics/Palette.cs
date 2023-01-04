namespace SdlSharp.Graphics
{
    /// <summary>
    /// A palette of colors.
    /// </summary>
    public sealed unsafe class Palette : IDisposable
    {
        private readonly Native.SDL_Palette* _palette;

        /// <summary>
        /// The ID of the palette.
        /// </summary>
        public nuint Id => (nuint)_palette;

        internal Palette(Native.SDL_Palette* palette)
        {
            _palette = palette;
        }

        /// <summary>
        /// Creates a new palette.
        /// </summary>
        /// <param name="colorCount">The number of colors in the palette.</param>
        /// <returns>The palette.</returns>
        public static Palette Create(int colorCount) =>
            new(Native.SDL_AllocPalette(colorCount));

        /// <inheritdoc/>
        public void Dispose() => Native.SDL_FreePalette(_palette);

        /// <summary>
        /// Sets the colors in the palette.
        /// </summary>
        /// <param name="colors">The colors.</param>
        /// <param name="firstColor">The first color to set in the palette.</param>
        public void SetColors(Color[] colors, int firstColor)
        {
            fixed (Color* ptr = colors)
            {
                _ = Native.CheckError(Native.SDL_SetPaletteColors(_palette, (Native.SDL_Color*)ptr, firstColor, colors.Length));
            }
        }

        /// <summary>
        /// Calculates a gamma ramp.
        /// </summary>
        /// <param name="gamma">The gamma value.</param>
        /// <returns>The gamma ramp.</returns>
        public static ushort[] CalculateGammaRamp(float gamma)
        {
            var ramp = new ushort[256];
            fixed (ushort* ptr = ramp)
            {
                Native.SDL_CalculateGammaRamp(gamma, ptr);
            }
            return ramp;
        }

        internal Native.SDL_Palette* ToNative() => _palette;
    }
}
