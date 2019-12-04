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

        public static implicit operator PointF((float X, float Y) tuple) => new PointF(tuple.X, tuple.Y);

        public static PointF operator +(PointF left, PointF right) => (left.X + right.X, left.Y + right.Y);

        public static PointF operator -(PointF left, PointF right) => (left.X - right.X, left.Y - right.Y);

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

            return (newX, newY);
        }
    }
}
