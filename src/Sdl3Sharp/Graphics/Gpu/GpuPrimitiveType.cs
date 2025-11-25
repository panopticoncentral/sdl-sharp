namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies the primitive topology of a graphics pipeline.
/// </summary>
public enum GpuPrimitiveType
{
    /// <summary>A series of separate triangles.</summary>
    TriangleList,

    /// <summary>A series of connected triangles.</summary>
    TriangleStrip,

    /// <summary>A series of separate lines.</summary>
    LineList,

    /// <summary>A series of connected lines.</summary>
    LineStrip,

    /// <summary>A series of separate points.</summary>
    PointList
}
