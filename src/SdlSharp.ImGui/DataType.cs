namespace SdlSharp.ImGui;

/// <summary>Primitive data type identifier for the scalar widgets (<see cref="ImGui.DragScalar"/> etc.).</summary>
public enum DataType
{
    /// <summary>signed 8-bit integer (sbyte).</summary>
    S8,
    /// <summary>unsigned 8-bit integer (byte).</summary>
    U8,
    /// <summary>signed 16-bit integer (short).</summary>
    S16,
    /// <summary>unsigned 16-bit integer (ushort).</summary>
    U16,
    /// <summary>signed 32-bit integer (int).</summary>
    S32,
    /// <summary>unsigned 32-bit integer (uint).</summary>
    U32,
    /// <summary>signed 64-bit integer (long).</summary>
    S64,
    /// <summary>unsigned 64-bit integer (ulong).</summary>
    U64,
    /// <summary>32-bit float.</summary>
    Float,
    /// <summary>64-bit double.</summary>
    Double,
    /// <summary>bool (provided for user convenience, not supported by scalar widgets).</summary>
    Bool,
    /// <summary>char* (provided for user convenience, not supported by scalar widgets).</summary>
    String,
}
