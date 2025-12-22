using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a point.
/// </summary>
public readonly record struct Point
{
    internal readonly ImVec2 Value { get; }

    /// <summary>
    /// The X component of the point.
    /// </summary>
    public float X => Value.X;

    /// <summary>
    /// The Y component of the point.
    /// </summary>
    public float Y => Value.Y;

    /// <summary>
    /// Creates a new Point instance.
    /// </summary>
    /// <param name="x">The X component of the vector.</param>
    /// <param name="y">The Y component of the vector.</param>
    public Point(float x, float y)
    {
        Value = new ImVec2 { X = x, Y = y };
    }

    internal Point(ImVec2 value)
    {
        Value = value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
