using System.Diagnostics;

namespace SdlSharp
{
    /// <summary>
    /// A point.
    /// </summary>
    [DebuggerDisplay("({X}, {Y})")]
    public readonly struct PointF
    {
        /// <summary>
        /// A point representing the origin (0, 0).
        /// </summary>
        public static readonly PointF Origin = (PointF)(0.0f, 0.0f);

        /// <summary>
        /// The X value of the point.
        /// </summary>
        public float X { get; }

        /// <summary>
        /// The Y value of the point.
        /// </summary>
        public float Y { get; }

        /// <summary>
        /// Constructs a new point.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        public PointF(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Converts a tuple to a point.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public static explicit operator PointF((float X, float Y) tuple) => new PointF(tuple.X, tuple.Y);

        /// <summary>
        /// Adds two points together.
        /// </summary>
        /// <param name="left">One point.</param>
        /// <param name="right">The other point.</param>
        /// <returns>The sum of the two points.</returns>
        public static PointF operator +(PointF left, PointF right) => (PointF)(left.X + right.X, left.Y + right.Y);

        /// <summary>
        /// Subtracts two points.
        /// </summary>
        /// <param name="left">One point.</param>
        /// <param name="right">The other point.</param>
        /// <returns>The difference between the two points.</returns>
        public static PointF operator -(PointF left, PointF right) => (PointF)(left.X - right.X, left.Y - right.Y);

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

            return (PointF)(newX, newY);
        }
    }
}
