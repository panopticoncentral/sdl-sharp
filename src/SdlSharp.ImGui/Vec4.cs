using System.Runtime.InteropServices;

namespace SdlSharp.ImGui;

/// <summary>
/// A 4D float vector used for RGBA colors and padding/margin quads. Layout-compatible with ImVec4.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct Vec4(float X, float Y, float Z, float W);
