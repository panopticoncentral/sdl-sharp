namespace SdlSharp.Graphics
{
    /// <summary>
    /// A color.
    /// </summary>
    /// <param name="Red">The red value.</param>
    /// <param name="Green">The green value.</param>
    /// <param name="Blue">The blue value.</param>
    /// <param name="Alpha">The alpha value.</param>
    public readonly record struct Color(byte Red, byte Green, byte Blue, byte Alpha = Color.AlphaOpaque)
    {
        /// <summary>
        /// The opaque alpha value.
        /// </summary>
        public const byte AlphaOpaque = 255;

        /// <summary>
        /// The transparent alpha value.
        /// </summary>
        public const byte AlphaTransparent = 0;

        internal Color(Native.SDL_Color color) : this(color.r, color.g, color.b, color.a)
        {
        }

        internal Native.SDL_Color ToNative() => new(Red, Green, Blue, Alpha);
    }
}
