using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a 2D vector with X and Y components.
/// </summary>
public readonly record struct Vec2
{
    internal readonly ImVec2 Value { get; }

    /// <summary>
    /// The X component of the vector.
    /// </summary>
    public float X => Value.X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    public float Y => Value.Y;

    /// <summary>
    /// Creates a new Vec2 instance.
    /// </summary>
    /// <param name="x">The X component of the vector.</param>
    /// <param name="y">The Y component of the vector.</param>
    public Vec2(float x, float y)
    {
        Value = new ImVec2 { X = x, Y = y };
    }

    internal Vec2(ImVec2 value)
    {
        Value = value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    /// <summary>
    /// Implicitly converts a tuple to a Vec2.
    /// </summary>
    /// <param name="tuple">The tuple containing X and Y values.</param>
    public static implicit operator Vec2((float X, float Y) tuple)
    {
        return new(tuple.X, tuple.Y);
    }
}
