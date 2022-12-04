using System.Diagnostics;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// A color.
    /// </summary>
    [DebuggerDisplay("({Red}, {Green}, {Blue}, {Alpha})")]
    public readonly struct Color : IEquatable<Color>
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

        /// <summary>
        /// Converts an RGB tuple to a color.
        /// </summary>
        /// <param name="tuple">The RGB value.</param>
        public static implicit operator Color((byte Red, byte Green, byte Blue) tuple) => new(tuple.Red, tuple.Green, tuple.Blue);

        /// <summary>
        /// Converts an RGBA tuple to a color.
        /// </summary>
        /// <param name="tuple">The RGBA value.</param>
        public static implicit operator Color((byte Red, byte Green, byte Blue, byte Alpha) tuple) => new(tuple.Red, tuple.Green, tuple.Blue, tuple.Alpha);

        /// <summary>
        /// Compares two colors.
        /// </summary>
        /// <param name="left">The left color.</param>
        /// <param name="right">The right color.</param>
        /// <returns>Whether the colors are equal.</returns>
        public static bool operator ==(Color left, Color right) => left.Equals(right);

        /// <summary>
        /// Compares two colors.
        /// </summary>
        /// <param name="left">The left color.</param>
        /// <param name="right">The right color.</param>
        /// <returns>Whether the colors are not equal.</returns>
        public static bool operator !=(Color left, Color right) => !left.Equals(right);

        /// <summary>
        /// Compares two colors.
        /// </summary>
        /// <param name="other">The other color to compare.</param>
        /// <returns>Whether the two colors are equal.</returns>
        public bool Equals(Color other) => Red == other.Red && Green == other.Green && Blue == other.Blue;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Color other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Red, Green, Blue);
    }
}
