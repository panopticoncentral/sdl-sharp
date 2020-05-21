using System;
using System.Diagnostics;

namespace SdlSharp
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
        public SizeF Scale(float scale) => new SizeF(Width * scale, Height * scale);

        public override int GetHashCode() => HashCode.Combine(Width, Height);

        public override bool Equals(object obj) => obj is SizeF sizef && Equals(sizef);

        public bool Equals(SizeF other) => Width == other.Width && Height == other.Height;

        public static implicit operator SizeF((float Width, float Height) tuple) => new SizeF(tuple.Width, tuple.Height);

        public static SizeF operator +(SizeF left, SizeF right) => (left.Width + right.Width, left.Height + right.Height);

        public static SizeF operator -(SizeF left, SizeF right) => (left.Width - right.Width, left.Height - right.Height);

        public static SizeF operator *(SizeF left, int scale) => (left.Width * scale, left.Height * scale);

        public static SizeF operator *(SizeF left, float scale) => ((int)(left.Width * scale), (int)(left.Height * scale));

        public static SizeF operator *(SizeF left, SizeF right) => (left.Width * right.Width, left.Height * right.Height);

        public static SizeF operator /(SizeF left, SizeF right) => (left.Width / right.Width, left.Height / right.Height);
    }
}
