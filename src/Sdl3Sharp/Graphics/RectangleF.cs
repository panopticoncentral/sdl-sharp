using System.Diagnostics;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Rect;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// A rectangle.
/// </summary>
[DebuggerDisplay("({Location.X}, {Location.Y}, {Size.Width}, {Size.Height})")]
public readonly unsafe record struct RectangleF(PointF Location, SizeF Size)
{
    /// <summary>
    /// Constructs a new rectangle with an origin location.
    /// </summary>
    /// <param name="size">The size of the rectangle.</param>
    public RectangleF(SizeF size) : this(PointF.Origin, size)
    {
    }

    private RectangleF(SDL_FRect native) : this(new(native.x, native.y), new(native.w, native.h))
    {
    }

    /// <summary>
    /// Whether the rectangle is empty.
    /// </summary>
    public bool IsEmpty
    {
        get
        {
            SDL_FRect nativeRect;
            return SDL_RectEmptyFloat(ToNative(this, &nativeRect));
        }
    }

    /// <summary>
    /// Determines whether the rectangles are equal within a given epsilon.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>Whether the rectangles are equal.</returns>
    public bool EqualsEpsilon(RectangleF other, float epsilon)
    {
        return Math.Abs(Location.X - other.Location.X) <= epsilon
        && Math.Abs(Location.Y - other.Location.Y) <= epsilon
        && Math.Abs(Size.Width - other.Size.Width) <= epsilon
        && Math.Abs(Size.Height - other.Size.Height) <= epsilon;
    }

    /// <summary>
    /// Determines whether the rectangles are equal within a given default epsilon.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>Whether the rectangles are equal.</returns>
    public bool EqualsEpsilon(RectangleF other)
    {
        return EqualsEpsilon(other, float.Epsilon);
    }

    /// <summary>
    /// Whether there is an intersection between the two rectangles.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>true if there is, false otherwise.</returns>
    public bool HasIntersection(RectangleF other)
    {
        SDL_FRect rect, otherRect;
        return SDL_HasRectIntersectionFloat(ToNative(this, &rect), ToNative(other, &otherRect));
    }

    /// <summary>
    /// Returns the intersection of two rectangles.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>The intersection of the two rectangles if there was an intersection, null otherwise.</returns>
    public RectangleF? Intersect(RectangleF other)
    {
        SDL_FRect rect, otherRect, resultRect;
        var result = SDL_GetRectIntersectionFloat(ToNative(this, &rect), ToNative(other, &otherRect), &resultRect);
        return result ? new(resultRect) : null;
    }

    /// <summary>
    /// Returns the union of two rectangles.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>The union of the two rectangles.</returns>
    public RectangleF Union(RectangleF other)
    {
        SDL_FRect rect, otherRect, resultRect;
        _ = CheckError(SDL_GetRectUnionFloat(ToNative(this, &rect), ToNative(other, &otherRect), &resultRect));
        return new(resultRect);
    }

    /// <summary>
    /// Calculates the minumum rectangle that encloses a set of points.
    /// </summary>
    /// <param name="points">The points.</param>
    /// <param name="clip">A clipping rectangle.</param>
    /// <returns>The enclosing rectangle if all the points were enclosed, null otherwise.</returns>
    public static RectangleF? EnclosePoints(PointF[] points, RectangleF? clip)
    {
        fixed (PointF* pointsPtr = points)
        {
            SDL_FRect clipRect, resultRect;
            var result = SDL_GetRectEnclosingPointsFloat((SDL_FPoint*)pointsPtr, points.Length, ToNative(clip, &clipRect), &resultRect);
            return result ? new(resultRect) : null;
        }
    }

    /// <summary>
    /// Calculates the intersection of the rectangle and a line segment.
    /// </summary>
    /// <param name="line">The line segment.</param>
    /// <returns>The intersecting line segment if there is one, null otherwise.</returns>
    public LineF? IntersectLine(LineF line)
    {
        var x1 = line.Start.X;
        var y1 = line.Start.Y;
        var x2 = line.End.X;
        var y2 = line.End.Y;
        SDL_FRect rect;
        var result = SDL_GetRectAndLineIntersectionFloat(ToNative(this, &rect), &x1, &y1, &x2, &y2);
        return result ? new(new(x1, y1), new(x2, y2)) : null;
    }

    /// <summary>
    /// Returns whether the rectangle contains the point.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>true if it does, false otherwise.</returns>
    public bool Contains(PointF point)
    {
        SDL_FPoint nativePoint;
        SDL_FRect nativeRect;
        return SDL_PointInRectFloat(PointF.ToNative(point, &nativePoint), ToNative(this, &nativeRect));
    }

    /// <summary>
    /// Returns whether this rectangle contains another rectangle.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>true if it does, false otherwise.</returns>
    public bool Contains(RectangleF other)
    {
        return other.Location.X >= Location.X && other.Location.X + other.Size.Width <= Location.X + Size.Width
        && other.Location.Y >= Location.Y && other.Location.Y + other.Size.Height <= Location.Y + Size.Height;
    }

    /// <summary>
    /// Returns the center of the rectangle.
    /// </summary>
    /// <returns>The center point.</returns>
    public PointF Center()
    {
        return new(Location.X + (Size.Width / 2), Location.Y + (Size.Height / 2));
    }

    internal static SDL_FRect* ToNative(RectangleF rect, SDL_FRect* nativeRect)
    {
        *nativeRect = new(rect.Location.X, rect.Location.Y, rect.Size.Width, rect.Size.Height);
        return nativeRect;
    }

    internal static SDL_FRect* ToNative(RectangleF? rect, SDL_FRect* nativeRect)
    {
        return rect == null ? (SDL_FRect*)null : ToNative(rect.Value, nativeRect);
    }
}
