using System.Runtime.InteropServices;

namespace SdlSharp.ImGui;

/// <summary>
/// A 2D float vector used by ImGui draw APIs. Layout-compatible with ImVec2.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct Vec2(float X, float Y)
{
    /// <summary>Converts to the BCL vector type.</summary>
    public static implicit operator System.Numerics.Vector2(Vec2 value) => new(value.X, value.Y);
    /// <summary>Converts from the BCL vector type.</summary>
    public static implicit operator Vec2(System.Numerics.Vector2 value) => new(value.X, value.Y);

    /// <summary>Alias for <see cref="X"/> when the vector represents a size.</summary>
    public float Width => X;

    /// <summary>Alias for <see cref="Y"/> when the vector represents a size.</summary>
    public float Height => Y;
}
