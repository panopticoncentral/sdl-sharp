using System;
using System.Diagnostics;

namespace SdlSharp
{
    /// <summary>
    /// A rectangle.
    /// </summary>
    [DebuggerDisplay("({Location.X}, {Location.Y}, {Size.Width}, {Size.Height})")]
    public struct Rectangle : IEquatable<Rectangle>
    {
        /// <summary>
        /// The location of the upper-left corner of the rectangle.
        /// </summary>
        public Point Location { get; }

        /// <summary>
        /// The size of the rectangle.
        /// </summary>
        public Size Size { get; }

        /// <summary>
        /// Constructs a new rectangle.
        /// </summary>
        /// <param name="location">The location of the upper-left corner of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public Rectangle(Point location, Size size)
        {
            Location = location;
            Size = size;
        }

        /// <summary>
        /// Constructs a new rectangle with an origin location.
        /// </summary>
        /// <param name="size">The size of the rectangle.</param>
        public Rectangle(Size size)
        {
            Location = Point.Origin;
            Size = size;
        }

        /// <summary>
        /// Converts a tuple to a recangle.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public static implicit operator Rectangle((Point location, Size size) tuple) => new Rectangle(tuple.location, tuple.size);

        /// <summary>
        /// Whether the rectangle is empty.
        /// </summary>
        public bool IsEmpty => Size.Width == 0 && Size.Height == 0;

        /// <summary>
        /// Compares two rectangles.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns>Whether the two rectangles are equal.</returns>
        public bool Equals(Rectangle other) =>
            Location.X == other.Location.X && Location.Y == other.Location.Y
            && Size.Width == other.Size.Width && Size.Height == other.Size.Height;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Rectangle rectangle && Equals(rectangle);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Location, Size);

        /// <summary>
        /// Whether there is an intersection between the two rectangles.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns>true if there is, false otherwise.</returns>
        public bool HasIntersection(Rectangle other) =>
            Native.SDL_HasIntersection(in this, in other);

        /// <summary>
        /// Returns the intersection of two rectangles.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <param name="intersection">The intersection of the two rectangles.</param>
        /// <returns>true if there was an intersection, false otherwise.</returns>
        public bool Intersect(Rectangle other, out Rectangle intersection) =>
            Native.SDL_IntersectRect(in this, in other, out intersection);

        /// <summary>
        /// Returns the union of two rectangles.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <param name="union">The union of the two rectangles.</param>
        public void Union(Rectangle other, out Rectangle union) =>
            Native.SDL_UnionRect(in this, in other, out union);

        /// <summary>
        /// Calculates the minumum recangle that encloses a set of points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="clip">A clipping rectangle.</param>
        /// <param name="result">The enclosing rectangle.</param>
        /// <returns>true of all the points were enclosed, false otherwise.</returns>
        public static bool EnclosePoints(Point[] points, Rectangle clip, out Rectangle result) =>
            Native.SDL_EnclosePoints(points, points.Length, in clip, out result);

        /// <summary>
        /// Returns whether the rectangle contains the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>true if it does, false otherwise.</returns>
        public bool Contains(Point point) =>
            point.X >= Location.X && point.X < Location.X + Size.Width
            && point.Y >= Location.Y && point.Y < Location.Y + Size.Height;

        /// <summary>
        /// Returns whether this rectangle contains another rectangle.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns>true if it does, false otherwise.</returns>
        public bool Contains(Rectangle other) =>
            other.Location.X >= Location.X && (other.Location.X + other.Size.Width) <= (Location.X + Size.Width)
            && other.Location.Y >= Location.Y && (other.Location.Y + other.Size.Height) <= (Location.Y + Size.Height);

        /// <summary>
        /// Returns the center of the rectangle.
        /// </summary>
        /// <returns>The center point.</returns>
        public Point Center() =>
            (Location.X + (Size.Width / 2), Location.Y + (Size.Height / 2));

        /// <summary>
        /// Determines if there is an intersection between a line and the rectangle.
        /// </summary>
        /// <param name="start">The start of the line.</param>
        /// <param name="end">The end of the line.</param>
        /// <returns>true if they intersect, false otherwise.</returns>
        public bool IntersectLine(Point start, Point end)
        {
            var x1 = start.X;
            var y1 = start.Y;
            var x2 = end.X;
            var y2 = end.Y;
            return Native.SDL_IntersectRectAndLine(in this, in x1, in y1, in x2, in y2);
        }
    }
}
