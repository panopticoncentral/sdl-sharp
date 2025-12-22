using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a color.
/// </summary>
public readonly record struct Color
{
    internal readonly ImVec4 Value { get; }

    /// <summary>
    /// The red component of the vector.
    /// </summary>
    public float Red => Value.X;

    /// <summary>
    /// The green component of the vector.
    /// </summary>
    public float Green => Value.Y;

    /// <summary>
    /// The blue component of the vector.
    /// </summary>
    public float Blue => Value.Z;

    /// <summary>
    /// The alpha component of the vector.
    /// </summary>
    public float Alpha => Value.W;

    /// <summary>
    /// Creates a new Color instance.
    /// </summary>
    /// <param name="red">The red component.</param>
    /// <param name="blue">The blue component.</param>
    /// <param name="green">The green component.</param>
    /// <param name="alpha">The alpha component.</param>
    public Color(float red, float green, float blue, float alpha)
    {
        Value = new ImVec4() { X = red, Y = green, Z = blue, W = alpha };
    }

    internal Color(ImVec4 value)
    {
        Value = value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        // Return the color in RGBA format like 0xRRGGBBAA
        return $"0x{(byte)(Red * 255):X2}{(byte)(Green * 255):X2}{(byte)(Blue * 255):X2}{(byte)(Alpha * 255):X2}";
    }
}
