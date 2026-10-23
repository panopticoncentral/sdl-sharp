using System.Runtime.InteropServices;

namespace SdlSharp.ImGui;

/// <summary>
/// A 4D float vector used for RGBA colors and padding/margin quads. Layout-compatible with ImVec4.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct Vec4(float X, float Y, float Z, float W)
{
    /// <summary>Converts to the BCL vector type.</summary>
    public static implicit operator System.Numerics.Vector4(Vec4 v) => new(v.X, v.Y, v.Z, v.W);
    /// <summary>Converts from the BCL vector type.</summary>
    public static implicit operator Vec4(System.Numerics.Vector4 v) => new(v.X, v.Y, v.Z, v.W);
}
