using static Sdl3Sharp.Native.Rect;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// A point with floating-point coordinates.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay("({X}, {Y})")]
public readonly unsafe record struct PointF
{
    /// <summary>
    /// Gets the underlying native SDL_FPoint structure representing the coordinates in unmanaged memory.
    /// </summary>
    public readonly SDL_FPoint Native;

    /// <summary>
    /// The X coordinate.
    /// </summary>
    public float X => Native.x;

    /// <summary>
    /// The Y coordinate.
    /// </summary>
    public float Y => Native.y;

    /// <summary>
    /// Creates a new point.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    public PointF(float x, float y)
    {
        Native = new SDL_FPoint(x, y);
    }

    /// <summary>
    /// A point representing the origin (0, 0).
    /// </summary>
    public static readonly PointF Origin = new(0.0f, 0.0f);

    /// <summary>
    /// Adds two points together.
    /// </summary>
    /// <param name="left">One point.</param>
    /// <param name="right">The other point.</param>
    /// <returns>The sum of the two points.</returns>
    public static PointF operator +(PointF left, PointF right)
    {
        return new(left.X + right.X, left.Y + right.Y);
    }

    /// <summary>
    /// Subtracts two points.
    /// </summary>
    /// <param name="left">One point.</param>
    /// <param name="right">The other point.</param>
    /// <returns>The difference between the two points.</returns>
    public static PointF operator -(PointF left, PointF right)
    {
        return new(left.X - right.X, left.Y - right.Y);
    }

    /// <summary>
    /// Scales a point.
    /// </summary>
    /// <param name="left">The point.</param>
    /// <param name="scale">The scale.</param>
    /// <returns>The scaled point.</returns>
    public static PointF operator *(PointF left, int scale)
    {
        return new(left.X * scale, left.Y * scale);
    }

    /// <summary>
    /// Scales a point.
    /// </summary>
    /// <param name="left">The point.</param>
    /// <param name="scale">The scale.</param>
    /// <returns>The scaled point.</returns>
    public static PointF operator *(PointF left, float scale)
    {
        return new(left.X * scale, left.Y * scale);
    }

    /// <summary>
    /// Multiplies two points together.
    /// </summary>
    /// <param name="left">One point.</param>
    /// <param name="right">The other point.</param>
    /// <returns>The two points multiplied together.</returns>
    public static PointF operator *(PointF left, PointF right)
    {
        return new(left.X * right.X, left.Y * right.Y);
    }

    /// <summary>
    /// Divides one point by the other.
    /// </summary>
    /// <param name="left">One point.</param>
    /// <param name="right">The other.</param>
    /// <returns>The point divided by the other point.</returns>
    public static PointF operator /(PointF left, PointF right)
    {
        return new(left.X / right.X, left.Y / right.Y);
    }

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

        return new(newX, newY);
    }
}
