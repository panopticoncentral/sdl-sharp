using System.Diagnostics;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// A color.
    /// </summary>
    [DebuggerDisplay("({Red}, {Green}, {Blue}, {Alpha})")]
    public readonly struct Color
    {
        /// <summary>
        /// The opaque alpha value.
        /// </summary>
        public const byte AlphaOpaque = 255;

        /// <summary>
        /// The transparent alpha value.
        /// </summary>
        public const byte AlphaTransparent = 0;

        /// <summary>
        /// The red value.
        /// </summary>
        public byte Red { get; }

        /// <summary>
        /// The green value.
        /// </summary>
        public byte Green { get; }

        /// <summary>
        /// The blue value.
        /// </summary>
        public byte Blue { get; }

        /// <summary>
        /// The alpha value.
        /// </summary>
        public byte Alpha { get; }

        /// <summary>
        /// Constructs a color.
        /// </summary>
        /// <param name="red">The red value.</param>
        /// <param name="green">The green value.</param>
        /// <param name="blue">The blue value.</param>
        /// <param name="alpha">The alpha value.</param>
        public Color(byte red, byte green, byte blue, byte alpha = AlphaOpaque)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public static implicit operator Color((byte Red, byte Green, byte Blue) tuple) => new Color(tuple.Red, tuple.Green, tuple.Blue);

        public static implicit operator Color((byte Red, byte Green, byte Blue, byte Alpha) tuple) => new Color(tuple.Red, tuple.Green, tuple.Blue, tuple.Alpha);
    }
}
