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

        public bool Equals(PixelColor other) => Value == other.Value;

        public override bool Equals(object obj) => obj is PixelColor p && Equals(p);

        public override int GetHashCode()
        {
            unchecked
            {
                return (int)Value;
            }
        }

        public static bool operator ==(PixelColor left, PixelColor right) => left.Equals(right);

        public static bool operator !=(PixelColor left, PixelColor right) => !left.Equals(right);
    }
}
