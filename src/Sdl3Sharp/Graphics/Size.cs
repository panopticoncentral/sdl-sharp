using System.Diagnostics;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// A size.
/// </summary>
/// <param name="Width">The width.</param>
/// <param name="Height">The height.</param>
[DebuggerDisplay("({Width}, {Height})")]
public readonly record struct Size(int Width, int Height)
{
    /// <summary>
    /// Adds two sizes together.
    /// </summary>
    /// <param name="left">The first size.</param>
    /// <param name="right">The second size.</param>
    /// <returns>The sum of the two sizes.</returns>
    public static Size operator +(Size left, Size right)
    {
        return new(left.Width + right.Width, left.Height + right.Height);
    }

    /// <summary>
    /// Subtracts one size from another.
    /// </summary>
    /// <param name="left">The first size.</param>
    /// <param name="right">The second size.</param>
    /// <returns>The difference of the two sizes.</returns>
    public static Size operator -(Size left, Size right)
    {
        return new(left.Width - right.Width, left.Height - right.Height);
    }

    /// <summary>
    /// Scales a size.
    /// </summary>
    /// <param name="left">The size.</param>
    /// <param name="scale">The scale.</param>
    /// <returns>The scaled size.</returns>
    public static Size operator *(Size left, int scale)
    {
        return new(left.Width * scale, left.Height * scale);
    }

    /// <summary>
    /// Scales a size.
    /// </summary>
    /// <param name="left">The size.</param>
    /// <param name="scale">The scale.</param>
    /// <returns>The scaled size.</returns>
    public static Size operator *(Size left, float scale)
    {
        return new((int)(left.Width * scale), (int)(left.Height * scale));
    }

    /// <summary>
    /// Multiplies two sizes together.
    /// </summary>
    /// <param name="left">One size.</param>
    /// <param name="right">The other size.</param>
    /// <returns>The two sizes multiplied together.</returns>
    public static Size operator *(Size left, Size right)
    {
        return new(left.Width * right.Width, left.Height * right.Height);
    }

    /// <summary>
    /// Divides one size by the other.
    /// </summary>
    /// <param name="left">One size.</param>
    /// <param name="right">The other.</param>
    /// <returns>The size divided by the other size.</returns>
    public static Size operator /(Size left, Size right)
    {
        return new(left.Width / right.Width, left.Height / right.Height);
    }
}
