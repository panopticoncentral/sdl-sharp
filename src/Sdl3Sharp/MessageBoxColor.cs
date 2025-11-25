namespace Sdl3Sharp;

/// <summary>
/// Represents an RGB color used in message box color schemes.
/// </summary>
/// <param name="R">The red component (0-255).</param>
/// <param name="G">The green component (0-255).</param>
/// <param name="B">The blue component (0-255).</param>
public readonly record struct MessageBoxColor(byte R, byte G, byte B)
{
    /// <summary>
    /// Creates a <see cref="MessageBoxColor"/> from RGB values.
    /// </summary>
    /// <param name="r">The red component (0-255).</param>
    /// <param name="g">The green component (0-255).</param>
    /// <param name="b">The blue component (0-255).</param>
    /// <returns>A new <see cref="MessageBoxColor"/> instance.</returns>
    public static MessageBoxColor FromRgb(byte r, byte g, byte b) => new(r, g, b);
}
