namespace SdlSharp.Graphics
{
    /// <summary>
    /// A rectangle.
    /// </summary>
    public readonly unsafe record struct Rectangle(Point Location, Size Size)
    {
        /// <summary>
        /// Constructs a new rectangle with an origin location.
        /// </summary>
        /// <param name="size">The size of the rectangle.</param>
        public Rectangle(Size size) : this(Point.Origin, size)
        {
        }

        internal Rectangle(Native.SDL_Rect native) : this((native.x, native.y), (native.w, native.h))
        {
        }

        /// <summary>
        /// Converts a tuple to a recangle.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public static implicit operator Rectangle((Point location, Size size) tuple) => new(tuple.location, tuple.size);

        /// <summary>
        /// Whether the rectangle is empty.
        /// </summary>
        public bool IsEmpty => Size.Width == 0 && Size.Height == 0;

        /// <summary>
        /// Whether there is an intersection between the two rectangles.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns>true if there is, false otherwise.</returns>
        public bool HasIntersection(Rectangle other)
        {
            Native.SDL_Rect rect, otherRect;
            return Native.SDL_HasIntersection(ToNative(this, &rect), ToNative(other, &otherRect));
        }

        /// <summary>
        /// Returns the intersection of two rectangles.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns>The intersection of the two rectangles if there was an intersection, null otherwise.</returns>
        public Rectangle? Intersect(Rectangle other)
        {
            Native.SDL_Rect rect, otherRect, resultRect;
            var result = Native.SDL_IntersectRect(ToNative(this, &rect), ToNative(other, &otherRect), &resultRect);
            return result ? new(resultRect) : null;
        }

        /// <summary>
        /// Returns the union of two rectangles.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns>The union of the two rectangles.</returns>
        public Rectangle Union(Rectangle other)
        {
            Native.SDL_Rect rect, otherRect, resultRect;
            Native.SDL_UnionRect(ToNative(this, &rect), ToNative(other, &otherRect), &resultRect);
            return new(resultRect);
        }

        /// <summary>
        /// Calculates the minumum recangle that encloses a set of points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="clip">A clipping rectangle.</param>
        /// <returns>The enclosing rectangle if all the points were enclosed, null otherwise.</returns>
        public static Rectangle? EnclosePoints(Point[] points, Rectangle clip)
        {
            fixed (Point* pointsPtr = points)
            {
                Native.SDL_Rect clipRect, resultRect;
                var result = Native.SDL_EnclosePoints((Native.SDL_Point*)pointsPtr, points.Length, ToNative(clip, &clipRect), &resultRect);
                return result ? new(resultRect) : null;
            }
        }

        /// <summary>
        /// Calculates the intersection of the rectangle and a line segment.
        /// </summary>
        /// <param name="line">The line segment.</param>
        /// <returns>The intersecting line segment if there is one, null otherwise.</returns>
        public Line? IntersectLine(Line line)
        {
            var x1 = line.Start.X;
            var y1 = line.Start.Y;
            var x2 = line.End.X;
            var y2 = line.End.Y;
            Native.SDL_Rect rect;
            var result = Native.SDL_IntersectRectAndLine(ToNative(this, &rect), &x1, &y1, &x2, &y2);
            return result ? ((x1, y1), (x2, y2)) : null;
        }

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
            other.Location.X >= Location.X && other.Location.X + other.Size.Width <= Location.X + Size.Width
            && other.Location.Y >= Location.Y && other.Location.Y + other.Size.Height <= Location.Y + Size.Height;

        /// <summary>
        /// Returns the center of the rectangle.
        /// </summary>
        /// <returns>The center point.</returns>
        public Point Center() =>
            (Location.X + Size.Width / 2, Location.Y + Size.Height / 2);

        internal static Native.SDL_Rect* ToNative(Rectangle rect, Native.SDL_Rect* nativeRect)
        {
            *nativeRect = new(rect.Location.X, rect.Location.Y, rect.Size.Width, rect.Size.Height);
            return nativeRect;
        }

        internal static Native.SDL_Rect* ToNative(Rectangle? rect, Native.SDL_Rect* nativeRect) =>
            rect == null ? (Native.SDL_Rect*)null : ToNative(rect.Value, nativeRect);
    }
}
