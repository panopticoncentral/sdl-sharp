using System.Diagnostics;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// A floating-point size.
    /// </summary>
    [DebuggerDisplay("({Width}, {Height})")]
    public readonly struct SizeF : IEquatable<SizeF>
    {
        /// <summary>
        /// The width.
        /// </summary>
        public float Width { get; }

        /// <summary>
        /// The height.
        /// </summary>
        public float Height { get; }

        /// <summary>
        /// Constructs a new size.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public SizeF(float width, float height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Scales a size by a factor.
        /// </summary>
        /// <param name="scale">The scaling factor.</param>
        /// <returns>The scaled size.</returns>
        public SizeF Scale(float scale) => new(Width * scale, Height * scale);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Width, Height);

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is SizeF sizef && Equals(sizef);

        /// <summary>
        /// Determines if two sizes are equal.
        /// </summary>
        /// <param name="other">The other size.</param>
        /// <returns>If they are equal.</returns>
        public bool Equals(SizeF other) => Width == other.Width && Height == other.Height;

        /// <summary>
        /// Adds two sizes together.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The sum of the two sizes.</returns>
        public static SizeF operator +(SizeF left, SizeF right) => new(left.Width + right.Width, left.Height + right.Height);

        /// <summary>
        /// Subtracts one size from another.
        /// </summary>
        /// <param name="left">The first size.</param>
        /// <param name="right">The second size.</param>
        /// <returns>The difference of the two sizes.</returns>
        public static SizeF operator -(SizeF left, SizeF right) => new(left.Width - right.Width, left.Height - right.Height);

        /// <summary>
        /// Scales a size.
        /// </summary>
        /// <param name="left">The size.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The scaled size.</returns>
        public static SizeF operator *(SizeF left, int scale) => new(left.Width * scale, left.Height * scale);

        /// <summary>
        /// Scales a size.
        /// </summary>
        /// <param name="left">The size.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The scaled size.</returns>
        public static SizeF operator *(SizeF left, float scale) => new((int)(left.Width * scale), (int)(left.Height * scale));

        /// <summary>
        /// Multiplies two sizes together.
        /// </summary>
        /// <param name="left">One size.</param>
        /// <param name="right">The other size.</param>
        /// <returns>The two sizes multiplied together.</returns>
        public static SizeF operator *(SizeF left, SizeF right) => new(left.Width * right.Width, left.Height * right.Height);

        /// <summary>
        /// Divides one size by the other.
        /// </summary>
        /// <param name="left">One size.</param>
        /// <param name="right">The other.</param>
        /// <returns>The size divided by the other size.</returns>
        public static SizeF operator /(SizeF left, SizeF right) => new(left.Width / right.Width, left.Height / right.Height);

        /// <summary>
        /// Determines if two sizes are equal.
        /// </summary>
        /// <param name="left">One size.</param>
        /// <param name="right">The other size.</param>
        /// <returns>Whether they are equal.</returns>
        public static bool operator ==(SizeF left, SizeF right) => left.Equals(right);

        /// <summary>
        /// Determines if two sizes are not equal.
        /// </summary>
        /// <param name="left">One size.</param>
        /// <param name="right">The other size.</param>
        /// <returns>Whether they are not equal.</returns>
        public static bool operator !=(SizeF left, SizeF right) => !(left == right);
    }
}
