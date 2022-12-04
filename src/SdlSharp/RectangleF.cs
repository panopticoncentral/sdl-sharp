﻿using System.Diagnostics;

namespace SdlSharp
{
    /// <summary>
    /// A rectangle.
    /// </summary>
    [DebuggerDisplay("({Location.X}, {Location.Y}, {Size.Width}, {Size.Height})")]
    public readonly struct RectangleF : IEquatable<RectangleF>
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

        /// <summary>
        /// Constructs a new rectangle with an origin location.
        /// </summary>
        /// <param name="size">The size of the rectangle.</param>
        public RectangleF(SizeF size)
        {
            Location = PointF.Origin;
            Size = size;
        }

        /// <summary>
        /// Converts a tuple to a recangle.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public static explicit operator RectangleF((PointF location, SizeF size) tuple) => new(tuple.location, tuple.size);

        /// <summary>
        /// Whether the rectangle is empty.
        /// </summary>
        public bool IsEmpty => Size.Width == 0 && Size.Height == 0;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is RectangleF rectangle && Equals(rectangle);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Location, Size);

        /// <summary>
        /// Compares two rectangles.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns>Whether the two rectangles are equal.</returns>
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

        /// <summary>
        /// Returns whether this rectangle contains another rectangle.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns>true if it does, false otherwise.</returns>
        public bool Contains(RectangleF other) =>
            other.Location.X >= Location.X && (other.Location.X + other.Size.Width) <= (Location.X + Size.Width)
            && other.Location.Y >= Location.Y && (other.Location.Y + other.Size.Height) <= (Location.Y + Size.Height);

        /// <summary>
        /// Returns the center of the rectangle.
        /// </summary>
        /// <returns>The center point.</returns>
        public PointF Center() =>
            (PointF)(Location.X + (Size.Width / 2), Location.Y + (Size.Height / 2));


        /// <summary>
        /// Compares two rectangles for equality.
        /// </summary>
        /// <param name="left">The left rectangle.</param>
        /// <param name="right">The right rectangle.</param>
        /// <returns>Whether they are equal.</returns>
        public static bool operator ==(RectangleF left, RectangleF right) => left.Equals(right);

        /// <summary>
        /// Compares two rectangles for inequality.
        /// </summary>
        /// <param name="left">The left rectangle.</param>
        /// <param name="right">The right rectangle.</param>
        /// <returns>Whether they are not equal.</returns>
        public static bool operator !=(RectangleF left, RectangleF right) => !(left == right);
    }
}
