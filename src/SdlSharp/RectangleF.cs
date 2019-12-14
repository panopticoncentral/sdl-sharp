using System;
using System.Diagnostics;

namespace SdlSharp
{
    /// <summary>
    /// A rectangle.
    /// </summary>
    [DebuggerDisplay("({Location.X}, {Location.Y}, {Size.Width}, {Size.Height})")]
    public struct RectangleF : IEquatable<RectangleF>
    {
        /// <summary>
        /// The location of the upper-left corner of the rectangle.
        /// </summary>
        public PointF Location { get; }

        /// <summary>
        /// The size of the rectangle.
        /// </summary>
        public SizeF Size { get; }

        /// <summary>
        /// Constructs a new rectangle.
        /// </summary>
        /// <param name="location">The location of the upper-left corner of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public RectangleF(PointF location, SizeF size)
        {
            Location = location;
            Size = size;
        }

        public static explicit operator RectangleF((PointF location, SizeF size) tuple) => new RectangleF(tuple.location, tuple.size);

        /// <summary>
        /// Whether the rectangle is empty.
        /// </summary>
        public bool IsEmpty => Size.Width == 0 && Size.Height == 0;

        public bool Equals(RectangleF other) =>
            Location.X == other.Location.X && Location.Y == other.Location.Y
            && Size.Width == other.Size.Width && Size.Height == other.Size.Height;

        /// <summary>
        /// Returns whether the rectangle contains the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>true if it does, false otherwise.</returns>
        public bool Contains(PointF point) =>
            point.X >= Location.X && point.X < Location.X + Size.Width
            && point.Y >= Location.Y && point.Y < Location.Y + Size.Height;
    }
}
