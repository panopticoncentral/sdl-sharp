using System;
using System.Diagnostics;

namespace SdlSharp
{
    /// <summary>
    /// A size.
    /// </summary>
    [DebuggerDisplay("({Width}, {Height})")]
    public readonly struct Size : IEquatable<Size>
    {
        /// <summary>
        /// The width.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The height.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Constructs a new size.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Converts a tuple to a size.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public static implicit operator Size((int Width, int Height) tuple) => new Size(tuple.Width, tuple.Height);

        /// <summary>
        /// Adds two sizes together.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The sum of the two sizes.</returns>
        public static Size operator +(Size left, Size right) => (left.Width + right.Width, left.Height + right.Height);

        /// <summary>
        /// Subtracts one size from another.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The difference of the two sizes.</returns>
        public static Size operator -(Size left, Size right) => (left.Width - right.Width, left.Height - right.Height);

        /// <summary>
        /// Scales a size.
        /// </summary>
        /// <param name="left">The size.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The scaled size.</returns>
        public static Size operator *(Size left, int scale) => (left.Width * scale, left.Height * scale);

        /// <summary>
        /// Scales a size.
        /// </summary>
        /// <param name="left">The size.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The scaled size.</returns>
        public static Size operator *(Size left, float scale) => ((int)(left.Width * scale), (int)(left.Height * scale));

        /// <summary>
        /// Multiplies two sizes together.
        /// </summary>
        /// <param name="left">One size.</param>
        /// <param name="right">The other size.</param>
        /// <returns>The two sizes multiplied together.</returns>
        public static Size operator *(Size left, Size right) => (left.Width * right.Width, left.Height * right.Height);

        /// <summary>
        /// Divides one size by the other.
        /// </summary>
        /// <param name="left">One size.</param>
        /// <param name="right">The other.</param>
        /// <returns>The size divided by the other size.</returns>
        public static Size operator /(Size left, Size right) => (left.Width / right.Width, left.Height / right.Height);

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Size size && Equals(size);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Width, Height);

        /// <summary>
        /// Determines if two sizes are equal.
        /// </summary>
        /// <param name="other">The other size.</param>
        /// <returns>If they are equal.</returns>
        public bool Equals(Size other) => Width == other.Width && Height == other.Height;
    }
}
