using System.Diagnostics;
using System.Runtime.InteropServices;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Rect;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// A rectangle.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay("({Location.X}, {Location.Y}, {Size.Width}, {Size.Height})")]
public readonly unsafe record struct Rectangle
{
    /// <summary>
    /// Gets the underlying native SDL_Rect structure representing the rectangle in unmanaged memory.
    /// </summary>
    public readonly SDL_Rect Native;

    /// <summary>
    /// The location of the rectangle.
    /// </summary>
    public Point Location => new(Native.x, Native.y);

    /// <summary>
    /// The size of the rectangle.
    /// </summary>
    public Size Size => new(Native.w, Native.h);

    /// <summary>
    /// Constructs a new rectangle.
    /// </summary>
    /// <param name="location">The location of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    public Rectangle(Point location, Size size)
    {
        Native = new SDL_Rect(location.X, location.Y, size.Width, size.Height);
    }

    /// <summary>
    /// Constructs a new rectangle with an origin location.
    /// </summary>
    /// <param name="size">The size of the rectangle.</param>
    public Rectangle(Size size) : this(Point.Origin, size)
    {
    }

    internal Rectangle(SDL_Rect native)
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
            fixed (SDL_Rect* nativeRect = &Native)
            {
                return SDL_RectEmpty(nativeRect);
            }
        }
    }

    /// <summary>
    /// Whether there is an intersection between the two rectangles.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>true if there is, false otherwise.</returns>
    public bool HasIntersection(Rectangle other)
    {
        fixed (SDL_Rect* rect = &Native)
        {
            return SDL_HasRectIntersection(rect, &other.Native);
        }
    }

    /// <summary>
    /// Returns the intersection of two rectangles.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>The intersection of the two rectangles if there was an intersection, null otherwise.</returns>
    public Rectangle? Intersect(Rectangle other)
    {
        SDL_Rect resultRect;
        bool result;
        fixed (SDL_Rect* rect = &Native)
        {
            result = SDL_GetRectIntersection(rect, &other.Native, &resultRect);
        }

        return result ? new(resultRect) : null;
    }

    /// <summary>
    /// Returns the union of two rectangles.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>The union of the two rectangles.</returns>
    public Rectangle Union(Rectangle other)
    {
        SDL_Rect resultRect;
        fixed (SDL_Rect* rect = &Native)
        {
            _ = CheckErrorBool(SDL_GetRectUnion(rect, &other.Native, &resultRect));
        }

        return new(resultRect);
    }

    /// <summary>
    /// Calculates the minumum rectangle that encloses a set of points.
    /// </summary>
    /// <param name="points">The points.</param>
    /// <returns>The enclosing rectangle if all the points were enclosed, null otherwise.</returns>
    public static Rectangle? EnclosePoints(Point[] points)
    {
        fixed (Point* pointsPtr = points)
        {
            SDL_Rect resultRect;
            var result = SDL_GetRectEnclosingPoints((SDL_Point*)pointsPtr, points.Length, null, &resultRect);
            return result ? new(resultRect) : null;
        }
    }

    /// <summary>
    /// Calculates the minumum rectangle that encloses a set of points.
    /// </summary>
    /// <param name="points">The points.</param>
    /// <param name="clip">A clipping rectangle.</param>
    /// <returns>The enclosing rectangle if all the points were enclosed, null otherwise.</returns>
    public static Rectangle? EnclosePoints(Point[] points, Rectangle clip)
    {
        fixed (Point* pointsPtr = points)
        {
            SDL_Rect resultRect;
            var result = SDL_GetRectEnclosingPoints((SDL_Point*)pointsPtr, points.Length, &clip.Native, &resultRect);
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
        bool result;
        fixed (SDL_Rect* rect = &Native)
        {
            result = SDL_GetRectAndLineIntersection(rect, &x1, &y1, &x2, &y2);
        }

        return result ? new(new(x1, y1), new(x2, y2)) : null;
    }

    /// <summary>
    /// Returns whether the rectangle contains the point.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>true if it does, false otherwise.</returns>
    public bool Contains(Point point)
    {
        fixed (SDL_Rect* nativeRect = &Native)
        {
            return SDL_PointInRect(&point.Native, nativeRect);
        }
    }

    /// <summary>
    /// Returns whether this rectangle contains another rectangle.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>true if it does, false otherwise.</returns>
    public bool Contains(Rectangle other)
    {
        return other.Native.x >= Native.x && other.Native.x + other.Native.w <= Native.x + Native.w
            && other.Native.y >= Native.y && other.Native.y + other.Native.h <= Native.y + Native.h;
    }

    /// <summary>
    /// Returns the center of the rectangle.
    /// </summary>
    /// <returns>The center point.</returns>
    public Point Center()
    {
        return new(Native.x + (Native.w / 2), Native.y + (Native.h / 2));
    }

    /// <summary>
    /// Gets a pointer to the native SDL_Rect if the rectangle has a value, or null otherwise.
    /// </summary>
    /// <param name="rect">The rectangle to convert, or null.</param>
    /// <param name="nativeRect">The location to store the native rectangle if not null.</param>
    /// <returns>A pointer to the native SDL_Rect, or null.</returns>
    internal static SDL_Rect* ToNative(Rectangle? rect, SDL_Rect* nativeRect)
    {
        if (rect == null)
        {
            return null;
        }

        *nativeRect = rect.Value.Native;
        return nativeRect;
    }
}
