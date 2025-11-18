using System.Diagnostics;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Rect;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// A rectangle.
/// </summary>
[DebuggerDisplay("({Location.X}, {Location.Y}, {Size.Width}, {Size.Height})")]
public readonly unsafe record struct Rectangle(Point Location, Size Size)
{
    /// <summary>
    /// Constructs a new rectangle with an origin location.
    /// </summary>
    /// <param name="size">The size of the rectangle.</param>
    public Rectangle(Size size) : this(Point.Origin, size)
    {
    }

    internal Rectangle(SDL_Rect native) : this(new(native.x, native.y), new(native.w, native.h))
    {
    }

    /// <summary>
    /// Whether the rectangle is empty.
    /// </summary>
    public bool IsEmpty
    {
        get
        {
            SDL_Rect nativeRect;
            return SDL_RectEmpty(ToNative(this, &nativeRect));
        }
    }

    /// <summary>
    /// Whether there is an intersection between the two rectangles.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>true if there is, false otherwise.</returns>
    public bool HasIntersection(Rectangle other)
    {
        SDL_Rect rect, otherRect;
        return SDL_HasRectIntersection(ToNative(this, &rect), ToNative(other, &otherRect));
    }

    /// <summary>
    /// Returns the intersection of two rectangles.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>The intersection of the two rectangles if there was an intersection, null otherwise.</returns>
    public Rectangle? Intersect(Rectangle other)
    {
        SDL_Rect rect, otherRect, resultRect;
        var result = SDL_GetRectIntersection(ToNative(this, &rect), ToNative(other, &otherRect), &resultRect);
        return result ? new(resultRect) : null;
    }

    /// <summary>
    /// Returns the union of two rectangles.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>The union of the two rectangles.</returns>
    public Rectangle Union(Rectangle other)
    {
        SDL_Rect rect, otherRect, resultRect;
        _ = CheckErrorBool(SDL_GetRectUnion(ToNative(this, &rect), ToNative(other, &otherRect), &resultRect));
        return new(resultRect);
    }

    /// <summary>
    /// Calculates the minumum rectangle that encloses a set of points.
    /// </summary>
    /// <param name="points">The points.</param>
    /// <param name="clip">A clipping rectangle.</param>
    /// <returns>The enclosing rectangle if all the points were enclosed, null otherwise.</returns>
    public static Rectangle? EnclosePoints(Point[] points, Rectangle? clip)
    {
        fixed (Point* pointsPtr = points)
        {
            SDL_Rect clipRect, resultRect;
            var result = SDL_GetRectEnclosingPoints((SDL_Point*)pointsPtr, points.Length, ToNative(clip, &clipRect), &resultRect);
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
        SDL_Rect rect;
        var result = SDL_GetRectAndLineIntersection(ToNative(this, &rect), &x1, &y1, &x2, &y2);
        return result ? new(new(x1, y1), new(x2, y2)) : null;
    }

    /// <summary>
    /// Returns whether the rectangle contains the point.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>true if it does, false otherwise.</returns>
    public bool Contains(Point point)
    {
        SDL_Point nativePoint;
        SDL_Rect nativeRect;
        return SDL_PointInRect(Point.ToNative(point, &nativePoint), ToNative(this, &nativeRect));
    }

    /// <summary>
    /// Returns whether this rectangle contains another rectangle.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>true if it does, false otherwise.</returns>
    public bool Contains(Rectangle other)
    {
        return other.Location.X >= Location.X && other.Location.X + other.Size.Width <= Location.X + Size.Width
        && other.Location.Y >= Location.Y && other.Location.Y + other.Size.Height <= Location.Y + Size.Height;
    }

    /// <summary>
    /// Returns the center of the rectangle.
    /// </summary>
    /// <returns>The center point.</returns>
    public Point Center()
    {
        return new(Location.X + (Size.Width / 2), Location.Y + (Size.Height / 2));
    }

    internal static SDL_Rect* ToNative(Rectangle rect, SDL_Rect* nativeRect)
    {
        *nativeRect = new(rect.Location.X, rect.Location.Y, rect.Size.Width, rect.Size.Height);
        return nativeRect;
    }

    internal static SDL_Rect* ToNative(Rectangle? rect, SDL_Rect* nativeRect)
    {
        return rect == null ? (SDL_Rect*)null : ToNative(rect.Value, nativeRect);
    }
}
