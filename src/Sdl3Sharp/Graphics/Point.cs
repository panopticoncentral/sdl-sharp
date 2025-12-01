using static Sdl3Sharp.Native.Rect;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// A point.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay("({X}, {Y})")]
public readonly unsafe record struct Point
{
    /// <summary>
    /// Gets the underlying native SDL_Point structure representing the coordinates in unmanaged memory.
    /// </summary>
    public readonly SDL_Point Native;

    /// <summary>
    /// The X coordinate.
    /// </summary>
    public int X => Native.x;

    /// <summary>
    /// The Y coordinate.
    /// </summary>
    public int Y => Native.y;

    /// <summary>
    /// Creates a new point.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    public Point(int x, int y)
    {
        Native = new SDL_Point(x, y);
    }

    /// <summary>
    /// A point representing the origin (0, 0).
    /// </summary>
    public static readonly Point Origin = new(0, 0);

    /// <summary>
    /// A point representing an undefined position.
    /// </summary>
    public static readonly Point Undefined = new(Window.PositionUndefined, Window.PositionUndefined);

    /// <summary>
    /// A point representing a centered position.
    /// </summary>
    public static readonly Point Centered = new(Window.PositionCentered, Window.PositionCentered);

    /// <summary>
    /// Adds two points together.
    /// </summary>
    /// <param name="left">One point.</param>
    /// <param name="right">The other point.</param>
    /// <returns>The sum of the two points.</returns>
    public static Point operator +(Point left, Point right)
    {
        return new(left.X + right.X, left.Y + right.Y);
    }

    /// <summary>
    /// Subtracts two points.
    /// </summary>
    /// <param name="left">One point.</param>
    /// <param name="right">The other point.</param>
    /// <returns>The difference between the two points.</returns>
    public static Point operator -(Point left, Point right)
    {
        return new(left.X - right.X, left.Y - right.Y);
    }

    /// <summary>
    /// Scales a point.
    /// </summary>
    /// <param name="left">The point.</param>
    /// <param name="scale">The scale.</param>
    /// <returns>The scaled point.</returns>
    public static Point operator *(Point left, int scale)
    {
        return new(left.X * scale, left.Y * scale);
    }

    /// <summary>
    /// Scales a point.
    /// </summary>
    /// <param name="left">The point.</param>
    /// <param name="scale">The scale.</param>
    /// <returns>The scaled point.</returns>
    public static Point operator *(Point left, float scale)
    {
        return new((int)(left.X * scale), (int)(left.Y * scale));
    }

    /// <summary>
    /// Multiplies two points together.
    /// </summary>
    /// <param name="left">One point.</param>
    /// <param name="right">The other point.</param>
    /// <returns>The two points multiplied together.</returns>
    public static Point operator *(Point left, Point right)
    {
        return new(left.X * right.X, left.Y * right.Y);
    }

    /// <summary>
    /// Divides one point by the other.
    /// </summary>
    /// <param name="left">One point.</param>
    /// <param name="right">The other.</param>
    /// <returns>The point divided by the other point.</returns>
    public static Point operator /(Point left, Point right)
    {
        return new(left.X / right.X, left.Y / right.Y);
    }

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

        return new(newX, newY);
    }
}
