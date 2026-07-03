using System.Runtime.InteropServices;

using SdlSharp.Native;
using static SdlSharp.Native.Rect;

namespace SdlSharp.Graphics;

/// <summary>
/// A rectangle using floating point coordinates.
/// </summary>
[StructLayout(LayoutKind.Sequential)] // Layout must match SDL_FRect (x, y, w, h floats): spans of this type are reinterpret-cast to SDL_FRect*.
public readonly record struct FRectangle(float X, float Y, float W, float H)
{
    /// <summary>
    /// Whether this rectangle has no area (width or height &lt; 0).
    /// </summary>
    public bool IsEmpty => W < 0f || H < 0f;

    /// <summary>
    /// Whether the given point is inside this rectangle.
    /// </summary>
    public bool Contains(FPoint p) =>
        p.X >= X && p.X <= X + W && p.Y >= Y && p.Y <= Y + H;

    /// <summary>
    /// Whether this rectangle is equal to another within the given epsilon.
    /// </summary>
    public bool Equals(FRectangle other, float epsilon) =>
        MathF.Abs(X - other.X) <= epsilon &&
        MathF.Abs(Y - other.Y) <= epsilon &&
        MathF.Abs(W - other.W) <= epsilon &&
        MathF.Abs(H - other.H) <= epsilon;

    /// <summary>
    /// Whether this rectangle intersects with another.
    /// </summary>
    public bool IntersectsWith(FRectangle other)
    {
        var a = ToNative();
        var b = other.ToNative();
        return SDL_HasRectIntersectionFloat(in a, in b);
    }

    /// <summary>
    /// Returns the intersection of this rectangle with another, or null if they don't intersect.
    /// </summary>
    public FRectangle? Intersect(FRectangle other)
    {
        var a = ToNative();
        var b = other.ToNative();
        return SDL_GetRectIntersectionFloat(in a, in b, out var result) ? FromNative(result) : null;
    }

    /// <summary>
    /// Returns the union of this rectangle with another.
    /// </summary>
    public FRectangle Union(FRectangle other)
    {
        var a = ToNative();
        var b = other.ToNative();
        Native.Common.Check(SDL_GetRectUnionFloat(in a, in b, out var result));
        return FromNative(result);
    }

    /// <summary>
    /// Calculates a minimal rectangle enclosing a set of points.
    /// </summary>
    /// <param name="points">The points to enclose.</param>
    /// <param name="clip">An optional clipping rectangle. Only points inside the clip are considered.</param>
    /// <returns>The enclosing rectangle, or null if no points were enclosed.</returns>
    public static unsafe FRectangle? EnclosePoints(ReadOnlySpan<FPoint> points, FRectangle? clip = null)
    {
        fixed (FPoint* ptr = points)
        {
            if (clip is { } c)
            {
                var clipNative = c.ToNative();
                return SDL_GetRectEnclosingPointsFloat((SDL_FPoint*)ptr, points.Length, in clipNative, out var result)
                    ? FromNative(result) : null;
            }
            else
            {
                return SDL_GetRectEnclosingPointsFloat((SDL_FPoint*)ptr, points.Length, null, out var result)
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
    public bool ClipLine(ref float x1, ref float y1, ref float x2, ref float y2)
    {
        var r = ToNative();
        return SDL_GetRectAndLineIntersectionFloat(in r, ref x1, ref y1, ref x2, ref y2);
    }

    internal SDL_FRect ToNative() => new() { x = X, y = Y, w = W, h = H };

    internal static FRectangle FromNative(SDL_FRect r) => new(r.x, r.y, r.w, r.h);
}
