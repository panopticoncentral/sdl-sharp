namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies how a buffer is intended to be used by the client.
/// </summary>
[Flags]
public enum GpuBufferUsage : uint
{
    /// <summary>Buffer is a vertex buffer.</summary>
    Vertex = 1u << 0,

    /// <summary>Buffer is an index buffer.</summary>
    Index = 1u << 1,

    /// <summary>Buffer is an indirect buffer.</summary>
    Indirect = 1u << 2,

    /// <summary>Buffer supports storage reads in graphics stages.</summary>
    GraphicsStorageRead = 1u << 3,

    /// <summary>Buffer supports storage reads in the compute stage.</summary>
    ComputeStorageRead = 1u << 4,

    /// <summary>Buffer supports storage writes in the compute stage.</summary>
    ComputeStorageWrite = 1u << 5
}
