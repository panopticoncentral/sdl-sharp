namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies the rate at which vertex attributes are pulled from buffers.
/// </summary>
public enum GpuVertexInputRate
{
    /// <summary>Attribute addressing is a function of the vertex index.</summary>
    Vertex,

    /// <summary>Attribute addressing is a function of the instance index.</summary>
    Instance
}
