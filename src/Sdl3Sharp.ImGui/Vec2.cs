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

    /// <summary>
    /// A vector with all components set to zero.
    /// </summary>
    public static Vec2 Zero => new(0, 0);

    /// <summary>
    /// A vector with all components set to one.
    /// </summary>
    public static Vec2 One => new(1, 1);

    /// <summary>
    /// A unit vector pointing in the X direction.
    /// </summary>
    public static Vec2 UnitX => new(1, 0);

    /// <summary>
    /// A unit vector pointing in the Y direction.
    /// </summary>
    public static Vec2 UnitY => new(0, 1);

    /// <summary>
    /// Adds two vectors.
    /// </summary>
    public static Vec2 operator +(Vec2 a, Vec2 b)
    {
        return new(a.X + b.X, a.Y + b.Y);
    }

    /// <summary>
    /// Subtracts two vectors.
    /// </summary>
    public static Vec2 operator -(Vec2 a, Vec2 b)
    {
        return new(a.X - b.X, a.Y - b.Y);
    }

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    public static Vec2 operator *(Vec2 v, float s)
    {
        return new(v.X * s, v.Y * s);
    }

    /// <summary>
    /// Multiplies a scalar by a vector.
    /// </summary>
    public static Vec2 operator *(float s, Vec2 v)
    {
        return new(v.X * s, v.Y * s);
    }

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    public static Vec2 operator /(Vec2 v, float s)
    {
        return new(v.X / s, v.Y / s);
    }

    /// <summary>
    /// Negates a vector.
    /// </summary>
    public static Vec2 operator -(Vec2 v)
    {
        return new(-v.X, -v.Y);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
