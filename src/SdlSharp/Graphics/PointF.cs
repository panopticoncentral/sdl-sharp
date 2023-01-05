namespace SdlSharp.Graphics
{
    /// <summary>
    /// A point.
    /// </summary>
    public readonly unsafe record struct PointF(float X, float Y)
    {
        /// <summary>
        /// A point representing the origin (0, 0).
        /// </summary>
        public static readonly PointF Origin = new(0.0f, 0.0f);

        /// <summary>
        /// Adds two points together.
        /// </summary>
        /// <param name="left">One point.</param>
        /// <param name="right">The other point.</param>
        /// <returns>The sum of the two points.</returns>
        public static PointF operator +(PointF left, PointF right) => new(left.X + right.X, left.Y + right.Y);

        /// <summary>
        /// Subtracts two points.
        /// </summary>
        /// <param name="left">One point.</param>
        /// <param name="right">The other point.</param>
        /// <returns>The difference between the two points.</returns>
        public static PointF operator -(PointF left, PointF right) => new(left.X - right.X, left.Y - right.Y);

        /// <summary>
        /// Scales a point.
        /// </summary>
        /// <param name="left">The point.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The scaled point.</returns>
        public static PointF operator *(PointF left, int scale) => new(left.X * scale, left.Y * scale);

        /// <summary>
        /// Scales a point.
        /// </summary>
        /// <param name="left">The point.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>The scaled point.</returns>
        public static PointF operator *(PointF left, float scale) => new((int)(left.X * scale), (int)(left.Y * scale));

        /// <summary>
        /// Multiplies two points together.
        /// </summary>
        /// <param name="left">One point.</param>
        /// <param name="right">The other point.</param>
        /// <returns>The two points multiplied together.</returns>
        public static PointF operator *(PointF left, PointF right) => new(left.X * right.X, left.Y * right.Y);

        /// <summary>
        /// Divides one point by the other.
        /// </summary>
        /// <param name="left">One point.</param>
        /// <param name="right">The other.</param>
        /// <returns>The point divided by the other point.</returns>
        public static PointF operator /(PointF left, PointF right) => new(left.X / right.X, left.Y / right.Y);

        /// <summary>
        /// Bounds a point to a given size.
        /// </summary>
        /// <param name="bound">The bound.</param>
        /// <returns>The bounded point.</returns>
        public PointF Bound(SizeF bound)
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

            return new(newX, newY);
        }

        internal static Native.SDL_FPoint* ToNative(PointF point, Native.SDL_FPoint* nativePoint)
        {
            *nativePoint = new(point.X, point.Y);
            return nativePoint;
        }

        internal static Native.SDL_FPoint* ToNative(PointF? point, Native.SDL_FPoint* nativePoint) =>
            point == null ? (Native.SDL_FPoint*)null : ToNative(point.Value, nativePoint);
    }
}
