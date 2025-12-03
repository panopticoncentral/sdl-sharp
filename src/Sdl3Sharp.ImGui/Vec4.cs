using System.Numerics;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a 4D vector with X, Y, Z, and W components.
/// Used for clipping rectangles, colors, and other 4-component values.
/// </summary>
/// <param name="X">The X component of the vector.</param>
/// <param name="Y">The Y component of the vector.</param>
/// <param name="Z">The Z component of the vector.</param>
/// <param name="W">The W component of the vector.</param>
public readonly record struct Vec4(float X, float Y, float Z, float W)
{
    /// <summary>
    /// A vector with all components set to zero.
    /// </summary>
    public static Vec4 Zero => new(0, 0, 0, 0);

    /// <summary>
    /// A vector with all components set to one.
    /// </summary>
    public static Vec4 One => new(1, 1, 1, 1);

    /// <summary>
    /// Creates a Vec4 from RGBA color values (0-255 range).
    /// </summary>
    /// <param name="r">Red component (0-255).</param>
    /// <param name="g">Green component (0-255).</param>
    /// <param name="b">Blue component (0-255).</param>
    /// <param name="a">Alpha component (0-255).</param>
    /// <returns>A Vec4 with normalized color values (0-1 range).</returns>
    public static Vec4 FromRgba(byte r, byte g, byte b, byte a = 255)
    {
        return new(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    /// <summary>
    /// Converts this Vec4 to a native ImVec4.
    /// </summary>
    /// <returns>The native ImVec4.</returns>
    internal ImVec4 ToNative()
    {
        return new() { X = X, Y = Y, Z = Z, W = W };
    }

    /// <summary>
    /// Creates a Vec4 from a native ImVec4.
    /// </summary>
    /// <param name="native">The native ImVec4.</param>
    /// <returns>The managed Vec4.</returns>
    internal static Vec4 FromNative(ImVec4 native)
    {
        return new(native.X, native.Y, native.Z, native.W);
    }

    /// <summary>
    /// Implicitly converts a Vec4 to a System.Numerics.Vector4.
    /// </summary>
    /// <param name="v">The Vec4 to convert.</param>
    public static implicit operator Vector4(Vec4 v)
    {
        return new(v.X, v.Y, v.Z, v.W);
    }

    /// <summary>
    /// Implicitly converts a System.Numerics.Vector4 to a Vec4.
    /// </summary>
    /// <param name="v">The Vector4 to convert.</param>
    public static implicit operator Vec4(Vector4 v)
    {
        return new(v.X, v.Y, v.Z, v.W);
    }

    /// <summary>
    /// Adds two vectors.
    /// </summary>
    public static Vec4 operator +(Vec4 a, Vec4 b)
    {
        return new(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
    }

    /// <summary>
    /// Subtracts two vectors.
    /// </summary>
    public static Vec4 operator -(Vec4 a, Vec4 b)
    {
        return new(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
    }

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    public static Vec4 operator *(Vec4 v, float s)
    {
        return new(v.X * s, v.Y * s, v.Z * s, v.W * s);
    }

    /// <summary>
    /// Multiplies a scalar by a vector.
    /// </summary>
    public static Vec4 operator *(float s, Vec4 v)
    {
        return new(v.X * s, v.Y * s, v.Z * s, v.W * s);
    }

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    public static Vec4 operator /(Vec4 v, float s)
    {
        return new(v.X / s, v.Y / s, v.Z / s, v.W / s);
    }

    /// <summary>
    /// Negates a vector.
    /// </summary>
    public static Vec4 operator -(Vec4 v)
    {
        return new(-v.X, -v.Y, -v.Z, -v.W);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"({X}, {Y}, {Z}, {W})";
    }
}
