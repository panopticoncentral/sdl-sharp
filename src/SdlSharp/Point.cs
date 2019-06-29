using System;

namespace SdlSharp
{
    /// <summary>
    /// A point.
    /// </summary>
    public readonly struct Point
    {
        /// <summary>
        /// The X value of the point.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// The Y value of the point.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Whether the point is in a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns><c>true</c> if the point is in the rectangle, <c>false</c> otherwise.</returns>
        public bool InRectangle(Rectangle rectangle) =>
            X >= rectangle.Location.X && X < rectangle.Location.X + rectangle.Size.Width
            && Y >= rectangle.Location.Y && Y < rectangle.Location.Y + rectangle.Size.Height;

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

        /// <summary>
        /// Bounds a point to a given size.
        /// </summary>
        /// <param name="bound">The bound.</param>
        /// <returns>The bounded point.</returns>
        public Point Bound(Size bound)
        {
            int newX = X;
            int newY = Y;

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
