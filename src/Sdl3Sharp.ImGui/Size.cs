using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a 2D size (width and height).
/// </summary>
public readonly record struct Size
{
    internal readonly ImVec2 Value { get; }

    /// <summary>
    /// The width.
    /// </summary>
    public float Width => Value.X;

    /// <summary>
    /// The height.
    /// </summary>
    public float Height => Value.Y;

    /// <summary>
    /// Creates a new Size instance.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="heigh">The height.</param>
    public Size(float width, float height)
    {
        Value = new ImVec2 { X = width, Y = height };
    }

    internal Size(ImVec2 value)
    {
        Value = value;
    }
    /// <inheritdoc />
    public override string ToString()
    {
        return $"({Width}, {Height})";
    }
}
