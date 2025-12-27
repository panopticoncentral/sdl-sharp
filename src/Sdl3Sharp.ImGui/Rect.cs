using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a recatangle.
/// </summary>
public readonly record struct Rect
{
    internal readonly ImVec4 Value { get; }

    /// <summary>
    /// The upper left.
    /// </summary>
    public Point UpperLeft => new(Value.X, Value.Y);

    /// <summary>
    /// The lower right.
    /// </summary>
    public Point LowerRight => new(Value.Z, Value.W);

    /// <summary>
    /// Creates a new Vec4 instance.
    /// </summary>
    /// <param name="x">The X component of the vector.</param>
    /// <param name="y">The Y component of the vector.</param>
    /// <param name="z">The Z component of the vector.</param>
    /// <param name="w">The W component of the vector.</param>
    public Rect(Point upperLeft, Point lowerRight)
    {
        Value = new ImVec4() { X = upperLeft.X, Y = upperLeft.Y, Z = lowerRight.X, W = lowerRight.Y };
    }

    internal Rect(ImVec4 value)
    {
        Value = value;
    }

    /// <summary>
    /// Implicitly converts a tuple to a Rect.
    /// </summary>
    /// <param name="tuple">The tuple containing upper left and lower right values.</param>
    public static implicit operator Rect((Point UpperLeft, Point LowerRight) tuple)
    {
        return new Rect(tuple.UpperLeft, tuple.LowerRight);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"({UpperLeft.X}, {UpperLeft.Y}) - ({LowerRight.X}, {LowerRight.Y})";
    }
}
