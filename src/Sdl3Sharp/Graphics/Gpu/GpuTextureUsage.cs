namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies how a texture is intended to be used by the client.
/// </summary>
[Flags]
public enum GpuTextureUsage : uint
{
    /// <summary>Texture supports sampling.</summary>
    Sampler = 1u << 0,

    /// <summary>Texture is a color render target.</summary>
    ColorTarget = 1u << 1,

    /// <summary>Texture is a depth stencil target.</summary>
    DepthStencilTarget = 1u << 2,

    /// <summary>Texture supports storage reads in graphics stages.</summary>
    GraphicsStorageRead = 1u << 3,

    /// <summary>Texture supports storage reads in the compute stage.</summary>
    ComputeStorageRead = 1u << 4,

    /// <summary>Texture supports storage writes in the compute stage.</summary>
    ComputeStorageWrite = 1u << 5,

    /// <summary>Texture supports reads and writes in the same compute shader.</summary>
    ComputeStorageSimultaneousReadWrite = 1u << 6
}
