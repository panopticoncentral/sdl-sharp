using Sdl3Sharp.Graphics;

namespace Sdl3Sharp;

/// <summary>
/// Represents an RGB color used in message box color schemes.
/// </summary>
/// <param name="Red">The red component (0-255).</param>
/// <param name="Green">The green component (0-255).</param>
/// <param name="Blue">The blue component (0-255).</param>
public readonly record struct MessageBoxColor(byte Red, byte Green, byte Blue)
{
    /// <summary>
    /// Creates a <see cref="MessageBoxColor"/> from RGB values.
    /// </summary>
    /// <param name="red">The red component (0-255).</param>
    /// <param name="green">The green component (0-255).</param>
    /// <param name="blue">The blue component (0-255).</param>
    /// <returns>A new <see cref="MessageBoxColor"/> instance.</returns>
    public static MessageBoxColor FromRgb(byte red, byte green, byte blue)
    {
        return new(red, green, blue);
    }

    /// <summary>
    /// Implicit conversion operation from Color to MessageBoxColor.
    /// </summary>
    public static implicit operator MessageBoxColor(Color color)
    {
        return new MessageBoxColor(color.Red, color.Green, color.Blue);
    }
}
