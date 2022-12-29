namespace SdlSharp
{
    /// <summary>
    /// A color for a message box.
    /// </summary>
    public readonly struct MessageBoxColor
    {
        /// <summary>
        /// The red component.
        /// </summary>
        public readonly byte Red { get; }

        /// <summary>
        /// The green component.
        /// </summary>
        public readonly byte Green { get; }

        /// <summary>
        /// The blue component.
        /// </summary>
        public readonly byte Blue { get; }

        /// <summary>
        /// Creates a new message box color.
        /// </summary>
        /// <param name="red">The red component.</param>
        /// <param name="green">The green component.</param>
        /// <param name="blue">The blue component.</param>
        public MessageBoxColor(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        internal Native.SDL_MessageBoxColor ToNative()
        {
            return new Native.SDL_MessageBoxColor
            {
                r = Red,
                g = Green,
                b = Blue
            };
        }
    }
}
