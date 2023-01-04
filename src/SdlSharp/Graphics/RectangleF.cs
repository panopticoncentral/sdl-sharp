namespace SdlSharp.Graphics
{
    /// <summary>
    /// A rectangle.
    /// </summary>
    public readonly unsafe record struct RectangleF(PointF Location, SizeF Size)
    {
        /// <summary>
        /// Constructs a new rectangle with an origin location.
        /// </summary>
        /// <param name="size">The size of the rectangle.</param>
        public RectangleF(SizeF size) : this(PointF.Origin, size)
        {
        }

        private RectangleF(Native.SDL_FRect native) : this((PointF)(native.x, native.y), (SizeF)(native.w, native.h))
        {
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

        /// <summary>
        /// Determines whether the rectangles are equal within a given epsilon.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>Whether the rectangles are equal.</returns>
        public bool EqualsEpsilon(RectangleF other, float epsilon) =>
            Math.Abs(Location.X - other.Location.X) <= epsilon
            && Math.Abs(Location.Y - other.Location.Y) <= epsilon
            && Math.Abs(Size.Width - other.Size.Width) <= epsilon
            && Math.Abs(Size.Height - other.Size.Height) <= epsilon;

        /// <summary>
        /// Determines whether the rectangles are equal within a given default epsilon.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns>Whether the rectangles are equal.</returns>
        public bool EqualsEpsilon(RectangleF other) => EqualsEpsilon(other, 1.192092896e-07F);

        /// <summary>
        /// Whether there is an intersection between the two rectangles.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns>true if there is, false otherwise.</returns>
        public bool HasIntersection(RectangleF other)
        {
            Native.SDL_FRect rect, otherRect;
            return Native.SDL_HasIntersectionF(ToNative(this, &rect), ToNative(other, &otherRect));
        }

        /// <summary>
        /// Returns the intersection of two rectangles.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns>The intersection of the two rectangles if there was an intersection, null otherwise.</returns>
        public RectangleF? Intersect(RectangleF other)
        {
            Native.SDL_FRect rect, otherRect, resultRect;
            var result = Native.SDL_IntersectFRect(ToNative(this, &rect), ToNative(other, &otherRect), &resultRect);
            return result ? new(resultRect) : null;
        }

        /// <summary>
        /// Returns the union of two rectangles.
        /// </summary>
        /// <param name="other">The other rectangle.</param>
        /// <returns>The union of the two rectangles.</returns>
        public RectangleF Union(RectangleF other)
        {
            Native.SDL_FRect rect, otherRect, resultRect;
            Native.SDL_UnionFRect(ToNative(this, &rect), ToNative(other, &otherRect), &resultRect);
            return new(resultRect);
        }

        /// <summary>
        /// Calculates the minumum recangle that encloses a set of points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="clip">A clipping rectangle.</param>
        /// <returns>The enclosing rectangle if all the points were enclosed, null otherwise.</returns>
        public static RectangleF? EnclosePoints(PointF[] points, RectangleF clip)
        {
            fixed (PointF* pointsPtr = points)
            {
                Native.SDL_FRect clipRect, resultRect;
                var result = Native.SDL_EncloseFPoints((Native.SDL_FPoint*)pointsPtr, points.Length, ToNative(clip, &clipRect), &resultRect);
                return result ? new(resultRect) : null;
            }
        }

        /// <summary>
        /// Calculates the intersection of the rectangle and a line segment.
        /// </summary>
        /// <param name="line">The line segment.</param>
        /// <returns>The intersecting line segment if there is one, null otherwise.</returns>
        public LineF? IntersectLine(LineF line)
        {
            var x1 = line.Start.X;
            var y1 = line.Start.Y;
            var x2 = line.End.X;
            var y2 = line.End.Y;
            Native.SDL_FRect rect;
            var result = Native.SDL_IntersectFRectAndLine(ToNative(this, &rect), &x1, &y1, &x2, &y2);
            return result ? ((PointF)(x1, y1), (PointF)(x2, y2)) : null;
        }

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
            other.Location.X >= Location.X && other.Location.X + other.Size.Width <= Location.X + Size.Width
            && other.Location.Y >= Location.Y && other.Location.Y + other.Size.Height <= Location.Y + Size.Height;

        /// <summary>
        /// Returns the center of the rectangle.
        /// </summary>
        /// <returns>The center point.</returns>
        public PointF Center() =>
            (PointF)(Location.X + Size.Width / 2, Location.Y + Size.Height / 2);

        internal static Native.SDL_FRect* ToNative(RectangleF rect, Native.SDL_FRect* nativeRect)
        {
            *nativeRect = new(rect.Location.X, rect.Location.Y, rect.Size.Width, rect.Size.Height);
            return nativeRect;
        }

        internal static Native.SDL_FRect* ToNative(RectangleF? rect, Native.SDL_FRect* nativeRect) =>
            rect == null ? (Native.SDL_FRect*)null : ToNative(rect.Value, nativeRect);
    }
}
