using System.Numerics;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a 2D vector with X and Y components.
/// </summary>
/// <param name="X">The X component of the vector.</param>
/// <param name="Y">The Y component of the vector.</param>
public readonly record struct Vec2(float X, float Y)
{
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
    /// Converts this Vec2 to a native ImVec2.
    /// </summary>
    /// <returns>The native ImVec2.</returns>
    internal ImVec2 ToNative()
    {
        return new() { X = X, Y = Y };
    }

    /// <summary>
    /// Creates a Vec2 from a native ImVec2.
    /// </summary>
    /// <param name="native">The native ImVec2.</param>
    /// <returns>The managed Vec2.</returns>
    internal static Vec2 FromNative(ImVec2 native)
    {
        return new(native.X, native.Y);
    }

    /// <summary>
    /// Implicitly converts a Vec2 to a System.Numerics.Vector2.
    /// </summary>
    /// <param name="v">The Vec2 to convert.</param>
    public static implicit operator Vector2(Vec2 v)
    {
        return new(v.X, v.Y);
    }

    /// <summary>
    /// Implicitly converts a System.Numerics.Vector2 to a Vec2.
    /// </summary>
    /// <param name="v">The Vector2 to convert.</param>
    public static implicit operator Vec2(Vector2 v)
    {
        return new(v.X, v.Y);
    }

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
