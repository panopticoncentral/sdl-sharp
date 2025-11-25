namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies the format of a vertex attribute.
/// </summary>
public enum GpuVertexElementFormat
{
    /// <summary>Invalid format.</summary>
    Invalid,

    /// <summary>Single 32-bit signed integer.</summary>
    Int,
    /// <summary>Two 32-bit signed integers.</summary>
    Int2,
    /// <summary>Three 32-bit signed integers.</summary>
    Int3,
    /// <summary>Four 32-bit signed integers.</summary>
    Int4,

    /// <summary>Single 32-bit unsigned integer.</summary>
    UInt,
    /// <summary>Two 32-bit unsigned integers.</summary>
    UInt2,
    /// <summary>Three 32-bit unsigned integers.</summary>
    UInt3,
    /// <summary>Four 32-bit unsigned integers.</summary>
    UInt4,

    /// <summary>Single 32-bit float.</summary>
    Float,
    /// <summary>Two 32-bit floats.</summary>
    Float2,
    /// <summary>Three 32-bit floats.</summary>
    Float3,
    /// <summary>Four 32-bit floats.</summary>
    Float4,

    /// <summary>Two 8-bit signed integers.</summary>
    Byte2,
    /// <summary>Four 8-bit signed integers.</summary>
    Byte4,

    /// <summary>Two 8-bit unsigned integers.</summary>
    UByte2,
    /// <summary>Four 8-bit unsigned integers.</summary>
    UByte4,

    /// <summary>Two 8-bit signed normalized values.</summary>
    Byte2Norm,
    /// <summary>Four 8-bit signed normalized values.</summary>
    Byte4Norm,

    /// <summary>Two 8-bit unsigned normalized values.</summary>
    UByte2Norm,
    /// <summary>Four 8-bit unsigned normalized values.</summary>
    UByte4Norm,

    /// <summary>Two 16-bit signed integers.</summary>
    Short2,
    /// <summary>Four 16-bit signed integers.</summary>
    Short4,

    /// <summary>Two 16-bit unsigned integers.</summary>
    UShort2,
    /// <summary>Four 16-bit unsigned integers.</summary>
    UShort4,

    /// <summary>Two 16-bit signed normalized values.</summary>
    Short2Norm,
    /// <summary>Four 16-bit signed normalized values.</summary>
    Short4Norm,

    /// <summary>Two 16-bit unsigned normalized values.</summary>
    UShort2Norm,
    /// <summary>Four 16-bit unsigned normalized values.</summary>
    UShort4Norm,

    /// <summary>Two 16-bit floats (half precision).</summary>
    Half2,
    /// <summary>Four 16-bit floats (half precision).</summary>
    Half4
}
