using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// A primary data type.
/// </summary>
public enum DataType
{
    /// <summary>
    /// Signed 8-bit integer (signed char / char with sensible compilers).
    /// </summary>
    S8 = ImGuiDataType.S8,

    /// <summary>
    /// Unsigned 8-bit integer (unsigned char).
    /// </summary>
    U8 = ImGuiDataType.U8,

    /// <summary>
    /// Signed 16-bit integer (short).
    /// </summary>
    S16 = ImGuiDataType.S16,

    /// <summary>
    /// Unsigned 16-bit integer (unsigned short).
    /// </summary>
    U16 = ImGuiDataType.U16,

    /// <summary>
    /// Signed 32-bit integer (int).
    /// </summary>
    S32 = ImGuiDataType.S32,

    /// <summary>
    /// Unsigned 32-bit integer (unsigned int).
    /// </summary>
    U32 = ImGuiDataType.U32,

    /// <summary>
    /// Signed 64-bit integer (long long / __int64).
    /// </summary>
    S64 = ImGuiDataType.S64,

    /// <summary>
    /// Unsigned 64-bit integer (unsigned long long / unsigned __int64).
    /// </summary>
    U64 = ImGuiDataType.U64,

    /// <summary>
    /// 32-bit floating point (float).
    /// </summary>
    Float = ImGuiDataType.Float,

    /// <summary>
    /// 64-bit floating point (double).
    /// </summary>
    Double = ImGuiDataType.Double,

    /// <summary>
    /// Boolean (provided for user convenience, not supported by scalar widgets).
    /// </summary>
    Bool = ImGuiDataType.Bool,

    /// <summary>
    /// String (provided for user convenience, not supported by scalar widgets).
    /// </summary>
    String = ImGuiDataType.String
}
