using System.Diagnostics;
using System.Runtime.InteropServices;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Rect;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// A rectangle with floating-point coordinates.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay("({Location.X}, {Location.Y}, {Size.Width}, {Size.Height})")]
public readonly unsafe record struct RectangleF
{
    /// <summary>
    /// Gets the underlying native SDL_FRect structure representing the rectangle in unmanaged memory.
    /// </summary>
    public readonly SDL_FRect Native;

    /// <summary>
    /// The location of the rectangle.
    /// </summary>
    public PointF Location => new(Native.x, Native.y);

    /// <summary>
    /// The size of the rectangle.
    /// </summary>
    public SizeF Size => new(Native.w, Native.h);

    /// <summary>
    /// Constructs a new rectangle.
    /// </summary>
    /// <param name="location">The location of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    public RectangleF(PointF location, SizeF size)
    {
        Native = new SDL_FRect(location.X, location.Y, size.Width, size.Height);
    }

    /// <summary>
    /// Constructs a new rectangle with an origin location.
    /// </summary>
    /// <param name="size">The size of the rectangle.</param>
    public RectangleF(SizeF size) : this(PointF.Origin, size)
    {
    }

    internal RectangleF(SDL_FRect native)
    {
        Native = native;
    }

    /// <summary>
    /// Whether the rectangle is empty.
    /// </summary>
    public bool IsEmpty
    {
        get
        {
            fixed (SDL_FRect* nativeRect = &Native)
            {
                return SDL_RectEmptyFloat(nativeRect);
            }
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
        return Math.Abs(Native.x - other.Native.x) <= epsilon
            && Math.Abs(Native.y - other.Native.y) <= epsilon
            && Math.Abs(Native.w - other.Native.w) <= epsilon
            && Math.Abs(Native.h - other.Native.h) <= epsilon;
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
        fixed (SDL_FRect* rect = &Native)
        {
            return SDL_HasRectIntersectionFloat(rect, &other.Native);
        }
    }

    /// <summary>
    /// Returns the intersection of two rectangles.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>The intersection of the two rectangles if there was an intersection, null otherwise.</returns>
    public RectangleF? Intersect(RectangleF other)
    {
        SDL_FRect resultRect;
        bool result;
        fixed (SDL_FRect* rect = &Native)
        {
            result = SDL_GetRectIntersectionFloat(rect, &other.Native, &resultRect);
        }

        return result ? new(resultRect) : null;
    }

    /// <summary>
    /// Returns the union of two rectangles.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>The union of the two rectangles.</returns>
    public RectangleF Union(RectangleF other)
    {
        SDL_FRect resultRect;
        fixed (SDL_FRect* rect = &Native)
        {
            _ = CheckErrorBool(SDL_GetRectUnionFloat(rect, &other.Native, &resultRect));
        }

        return new(resultRect);
    }

    /// <summary>
    /// Calculates the minumum rectangle that encloses a set of points.
    /// </summary>
    /// <param name="points">The points.</param>
    /// <returns>The enclosing rectangle if all the points were enclosed, null otherwise.</returns>
    public static RectangleF? EnclosePoints(PointF[] points)
    {
        fixed (PointF* pointsPtr = points)
        {
            SDL_FRect resultRect;
            var result = SDL_GetRectEnclosingPointsFloat((SDL_FPoint*)pointsPtr, points.Length, null, &resultRect);
            return result ? new(resultRect) : null;
        }
    }

    /// <summary>
    /// Calculates the minumum rectangle that encloses a set of points.
    /// </summary>
    /// <param name="points">The points.</param>
    /// <param name="clip">A clipping rectangle.</param>
    /// <returns>The enclosing rectangle if all the points were enclosed, null otherwise.</returns>
    public static RectangleF? EnclosePoints(PointF[] points, RectangleF clip)
    {
        fixed (PointF* pointsPtr = points)
        {
            SDL_FRect resultRect;
            var result = SDL_GetRectEnclosingPointsFloat((SDL_FPoint*)pointsPtr, points.Length, &clip.Native, &resultRect);
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
        bool result;
        fixed (SDL_FRect* rect = &Native)
        {
            result = SDL_GetRectAndLineIntersectionFloat(rect, &x1, &y1, &x2, &y2);
        }

        return result ? new(new(x1, y1), new(x2, y2)) : null;
    }

    /// <summary>
    /// Returns whether the rectangle contains the point.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>true if it does, false otherwise.</returns>
    public bool Contains(PointF point)
    {
        fixed (SDL_FRect* nativeRect = &Native)
        {
            return SDL_PointInRectFloat(&point.Native, nativeRect);
        }
    }

    /// <summary>
    /// Returns whether this rectangle contains another rectangle.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>true if it does, false otherwise.</returns>
    public bool Contains(RectangleF other)
    {
        return other.Native.x >= Native.x && other.Native.x + other.Native.w <= Native.x + Native.w
            && other.Native.y >= Native.y && other.Native.y + other.Native.h <= Native.y + Native.h;
    }

    /// <summary>
    /// Returns the center of the rectangle.
    /// </summary>
    /// <returns>The center point.</returns>
    public PointF Center()
    {
        return new(Native.x + (Native.w / 2), Native.y + (Native.h / 2));
    }

    /// <summary>
    /// Gets a pointer to the native SDL_FRect if the rectangle has a value, or null otherwise.
    /// </summary>
    /// <param name="rect">The rectangle to convert, or null.</param>
    /// <param name="nativeRect">The location to store the native rectangle if not null.</param>
    /// <returns>A pointer to the native SDL_FRect, or null.</returns>
    internal static SDL_FRect* ToNative(RectangleF? rect, SDL_FRect* nativeRect)
    {
        if (rect == null)
        {
            return null;
        }

        *nativeRect = rect.Value.Native;
        return nativeRect;
    }
}
