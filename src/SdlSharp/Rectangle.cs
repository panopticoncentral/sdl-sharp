using System;

namespace SdlSharp
{
    /// <summary>
    /// A rectangle.
    /// </summary>
    public struct Rectangle : IEquatable<Rectangle>
    {
        public Point Location { get; }

        public Size Size { get; }

        public Rectangle(Point location, Size size)
        {
            Location = location;
            Size = size;
        }

        public static implicit operator Rectangle((Point location, Size size) tuple) => new Rectangle(tuple.location, tuple.size);

        public bool IsEmpty => Size.Width == 0 && Size.Height == 0;

        public bool Equals(Rectangle other) =>
            Location.X == other.Location.X && Location.Y == other.Location.Y
            && Size.Width == other.Size.Width && Size.Height == other.Size.Height;

        public bool HasIntersection(Rectangle other) =>
            Native.SDL_HasIntersection(in this, in other);

        public bool Intersect(Rectangle other, out Rectangle intersection) =>
            Native.SDL_IntersectRect(in this, in other, out intersection);

        public void Union(Rectangle other, out Rectangle union) =>
            Native.SDL_UnionRect(in this, in other, out union);

        public bool EnclosePoints(Point[] points, Rectangle clip, out Rectangle result) =>
            Native.SDL_EnclosePoints(points, points.Length, in clip, out result);

        public bool IntersectLine(Point location1, Point location2)
        {
            var x1 = location1.X;
            var y1 = location1.Y;
            var x2 = location2.X;
            var y2 = location2.Y;
            return Native.SDL_IntersectRectAndLine(in this, in x1, in y1, in x2, in y2);
        }
    }
}
