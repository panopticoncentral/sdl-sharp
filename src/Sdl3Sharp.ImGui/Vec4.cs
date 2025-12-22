using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a 4D vector with X, Y, Z, and W components.
/// Used for clipping rectangles, colors, and other 4-component values.
/// </summary>
public readonly record struct Vec4
{
    internal readonly ImVec4 Value { get; }

    /// <summary>
    /// The X component of the vector.
    /// </summary>
    public float X => Value.X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    public float Y => Value.Y;

    /// <summary>
    /// The Z component of the vector.
    /// </summary>
    public float Z => Value.Z;

    /// <summary>
    /// The W component of the vector.
    /// </summary>
    public float W => Value.W;

    /// <summary>
    /// Creates a new Vec4 instance.
    /// </summary>
    /// <param name="x">The X component of the vector.</param>
    /// <param name="y">The Y component of the vector.</param>
    /// <param name="z">The Z component of the vector.</param>
    /// <param name="w">The W component of the vector.</param>
    public Vec4(float x, float y, float z, float w)
    {
        Value = new ImVec4() { X = x, Y = y, Z = z, W = w };
    }

    internal Vec4(ImVec4 value)
    {
        Value = value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"({X}, {Y}, {Z}, {W})";
    }
}
