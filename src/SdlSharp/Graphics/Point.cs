namespace SdlSharp.Graphics
{
    /// <summary>
    /// A point.
    /// </summary>
    public readonly unsafe record struct Point(int X, int Y)
    {
        /// <summary>
        /// A point representing the origin (0, 0).
        /// </summary>
        public static readonly Point Origin = (0, 0);

        /// <summary>
        /// Converts a tuple to a point.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public static implicit operator Point((int X, int Y) tuple) => new(tuple.X, tuple.Y);

        /// <summary>
        /// Adds two points together.
        /// </summary>
        /// <param name="left">One point.</param>
        /// <param name="right">The other point.</param>
        /// <returns>The sum of the two points.</returns>
        public static Point operator +(Point left, Point right) => (left.X + right.X, left.Y + right.Y);

        /// <summary>
        /// Subtracts two points.
        /// </summary>
        /// <param name="left">One point.</param>
        /// <param name="right">The other point.</param>
        /// <returns>The difference between the two points.</returns>
        public static Point operator -(Point left, Point right) => (left.X - right.X, left.Y - right.Y);

        /// <summary>
        /// Scales a point.
        /// </summary>
        /// <param name="left">The point.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The scaled point.</returns>
        public static Point operator *(Point left, int scale) => (left.X * scale, left.Y * scale);

        /// <summary>
        /// Scales a point.
        /// </summary>
        /// <param name="left">The point.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The scaled point.</returns>
        public static Point operator *(Point left, float scale) => ((int)(left.X * scale), (int)(left.Y * scale));

        /// <summary>
        /// Multiplies two points together.
        /// </summary>
        /// <param name="left">One point.</param>
        /// <param name="right">The other point.</param>
        /// <returns>The two points multiplied together.</returns>
        public static Point operator *(Point left, Point right) => (left.X * right.X, left.Y * right.Y);

        /// <summary>
        /// Divides one point by the other.
        /// </summary>
        /// <param name="left">One point.</param>
        /// <param name="right">The other.</param>
        /// <returns>The point divided by the other point.</returns>
        public static Point operator /(Point left, Point right) => (left.X / right.X, left.Y / right.Y);

        /// <summary>
        /// Bounds a point to a given size.
        /// </summary>
        /// <param name="bound">The bound.</param>
        /// <returns>The bounded point.</returns>
        public Point Bound(Size bound)
        {
            var newX = X;
            var newY = Y;

            if (X < 0)
            {
                newX = 0;
            }
            else if (X >= bound.Width)
            {
                newX = bound.Width - 1;
            }

            if (Y < 0)
            {
                newY = 0;
            }
            else if (Y >= bound.Height)
            {
                newY = bound.Height - 1;
            }

            return (newX, newY);
        }

        internal static Native.SDL_Point* ToNative(Point point, Native.SDL_Point* nativePoint)
        {
            *nativePoint = new(point.X, point.Y);
            return nativePoint;
        }

        internal static Native.SDL_Point* ToNative(Point? point, Native.SDL_Point* nativePoint) =>
            point == null ? (Native.SDL_Point*)null : ToNative(point.Value, nativePoint);
    }
}
