using System.Diagnostics;

namespace SdlSharp
{
    /// <summary>
    /// A point.
    /// </summary>
    [DebuggerDisplay("({X}, {Y})")]
    public readonly struct Point
    {
        /// <summary>
        /// A point representing the origin (0, 0).
        /// </summary>
        public static readonly Point Origin = (0, 0);

        /// <summary>
        /// The X value of the point.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// The Y value of the point.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Constructs a new point.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);

        public static Point operator +(Point left, Point right) => (left.X + right.X, left.Y + right.Y);

        public static Point operator -(Point left, Point right) => (left.X - right.X, left.Y - right.Y);

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
    }
}
