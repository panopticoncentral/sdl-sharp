using SdlSharp.Native;
using static SdlSharp.Native.Rect;

namespace SdlSharp.Graphics;

/// <summary>
/// A rectangle with integer coordinates, origin at the upper left.
/// </summary>
public readonly record struct Rectangle(int X, int Y, int W, int H)
{
    /// <summary>
    /// Whether this rectangle has no area (width or height &lt;= 0).
    /// </summary>
    public bool IsEmpty => W <= 0 || H <= 0;

    /// <summary>
    /// The size of this rectangle.
    /// </summary>
    public Size Size => new(W, H);

    /// <summary>
    /// Whether the given point is inside this rectangle.
    /// </summary>
    public bool Contains(Point p) =>
        p.X >= X && p.X < X + W && p.Y >= Y && p.Y < Y + H;

    /// <summary>
    /// Whether this rectangle intersects with another.
    /// </summary>
    public bool IntersectsWith(Rectangle other)
    {
        var a = ToNative();
        var b = other.ToNative();
        return SDL_HasRectIntersection(in a, in b);
    }

    /// <summary>
    /// Returns the intersection of this rectangle with another, or null if they don't intersect.
    /// </summary>
    public Rectangle? Intersect(Rectangle other)
    {
        var a = ToNative();
        var b = other.ToNative();
        return SDL_GetRectIntersection(in a, in b, out var result) ? FromNative(result) : null;
    }

    /// <summary>
    /// Returns the union of this rectangle with another.
    /// </summary>
    public Rectangle Union(Rectangle other)
    {
        var a = ToNative();
        var b = other.ToNative();
        Native.Common.Check(SDL_GetRectUnion(in a, in b, out var result));
        return FromNative(result);
    }

    /// <summary>
    /// Calculates a minimal rectangle enclosing a set of points.
    /// </summary>
    /// <param name="points">The points to enclose.</param>
    /// <param name="clip">An optional clipping rectangle. Only points inside the clip are considered.</param>
    /// <returns>The enclosing rectangle, or null if no points were enclosed.</returns>
    public static unsafe Rectangle? EnclosePoints(ReadOnlySpan<Point> points, Rectangle? clip = null)
    {
        fixed (Point* ptr = points)
        {
            if (clip is { } c)
            {
                var clipNative = c.ToNative();
                return SDL_GetRectEnclosingPoints((SDL_Point*)ptr, points.Length, in clipNative, out var result)
                    ? FromNative(result) : null;
            }
            else
            {
                return SDL_GetRectEnclosingPoints((SDL_Point*)ptr, points.Length, null, out var result)
                    ? FromNative(result) : null;
            }
        }
    }

    /// <summary>
    /// Clips a line segment to this rectangle, returning the clipped endpoints.
    /// </summary>
    /// <param name="x1">Starting X coordinate.</param>
    /// <param name="y1">Starting Y coordinate.</param>
    /// <param name="x2">Ending X coordinate.</param>
    /// <param name="y2">Ending Y coordinate.</param>
    /// <returns>true if the line intersects the rectangle (endpoints are clipped), false otherwise.</returns>
    public bool ClipLine(ref int x1, ref int y1, ref int x2, ref int y2)
    {
        var r = ToNative();
        return SDL_GetRectAndLineIntersection(in r, ref x1, ref y1, ref x2, ref y2);
    }

    /// <summary>
    /// Converts this integer rectangle to a floating point rectangle.
    /// </summary>
    public FRectangle ToFloat() => new(X, Y, W, H);

    internal SDL_Rect ToNative() => new() { x = X, y = Y, w = W, h = H };

    internal static Rectangle FromNative(SDL_Rect r) => new(r.x, r.y, r.w, r.h);
}
