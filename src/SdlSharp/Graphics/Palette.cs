namespace SdlSharp.Graphics
{
    /// <summary>
    /// A palette of colors.
    /// </summary>
    public sealed unsafe class Palette : NativePointerBase<Native.SDL_Palette, Palette>
    {
        /// <summary>
        /// Creates a new palette.
        /// </summary>
        /// <param name="colorCount">The number of colors in the palette.</param>
        /// <returns>The palette.</returns>
        public static Palette Create(int colorCount) =>
            PointerToInstanceNotNull(Native.SDL_AllocPalette(colorCount));

        /// <inheritdoc/>
        public override void Dispose()
        {
            Native.SDL_FreePalette(Pointer);
            base.Dispose();
        }

        /// <summary>
        /// Sets the colors in the palette.
        /// </summary>
        /// <param name="colors">The colors.</param>
        /// <param name="firstColor">The first color to set in the palette.</param>
        public void SetColors(Color[] colors, int firstColor) =>
            Native.CheckError(Native.SDL_SetPaletteColors(Pointer, colors, firstColor, colors.Length));

        /// <summary>
        /// Calculates a gamma ramp.
        /// </summary>
        /// <param name="gamma">The gamma value.</param>
        /// <returns>The gamma ramp.</returns>
        public static ushort[] CalculateGammaRamp(float gamma)
        {
            Native.SDL_CalculateGammaRamp(gamma, out var ramp);
            return ramp;
        }
    }
}
