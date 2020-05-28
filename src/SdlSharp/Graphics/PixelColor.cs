using System;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// A pixel color in a particular pixel format.
    /// </summary>
    public readonly struct PixelColor : IEquatable<PixelColor>
    {
        /// <summary>
        /// The value.
        /// </summary>
        public readonly uint Value { get; }

        /// <summary>
        /// Checks to see if two pixel colors are the same.
        /// </summary>
        /// <param name="other">The other pixel color to check.</param>
        /// <returns>Whether they are equal.</returns>
        public bool Equals(PixelColor other) => Value == other.Value;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is PixelColor p && Equals(p);

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                return (int)Value;
            }
        }

        /// <summary>
        /// Checks to see if two pixel colors are the same.
        /// </summary>
        /// <param name="left">One pixel color.</param>
        /// <param name="right">The other pixel color.</param>
        /// <returns>Whether they are equal.</returns>
        public static bool operator ==(PixelColor left, PixelColor right) => left.Equals(right);

        /// <summary>
        /// Checks to see if two pixel colors are not the same.
        /// </summary>
        /// <param name="left">One pixel color.</param>
        /// <param name="right">The other pixel color.</param>
        /// <returns>Whether they are not equal.</returns>
        public static bool operator !=(PixelColor left, PixelColor right) => !left.Equals(right);
    }
}
