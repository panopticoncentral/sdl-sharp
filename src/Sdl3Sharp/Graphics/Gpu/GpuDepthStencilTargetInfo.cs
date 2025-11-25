using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes the parameters for a depth-stencil target in a render pass.
/// </summary>
public struct GpuDepthStencilTargetInfo
{
    /// <summary>
    /// The texture that will be used as the depth stencil target by the render pass.
    /// </summary>
    public GpuTexture? Texture { get; set; }

    /// <summary>
    /// The value to clear the depth component to at the beginning of the render pass.
    /// </summary>
    public float ClearDepth { get; set; }

    /// <summary>
    /// What is done with the depth contents at the beginning of the render pass.
    /// </summary>
    public GpuLoadOp LoadOp { get; set; }

    /// <summary>
    /// What is done with the depth results of the render pass.
    /// </summary>
    public GpuStoreOp StoreOp { get; set; }

    /// <summary>
    /// What is done with the stencil contents at the beginning of the render pass.
    /// </summary>
    public GpuLoadOp StencilLoadOp { get; set; }

    /// <summary>
    /// What is done with the stencil results of the render pass.
    /// </summary>
    public GpuStoreOp StencilStoreOp { get; set; }

    /// <summary>
    /// True cycles the texture if the texture is bound and any load ops are not LOAD.
    /// </summary>
    public bool Cycle { get; set; }

    /// <summary>
    /// The value to clear the stencil component to at the beginning of the render pass.
    /// </summary>
    public byte ClearStencil { get; set; }

    internal unsafe SDL_GPUDepthStencilTargetInfo ToNative()
    {
        return new SDL_GPUDepthStencilTargetInfo
        {
            texture = Texture != null ? Texture.Handle : null,
            clear_depth = ClearDepth,
            load_op = (SDL_GPULoadOp)LoadOp,
            store_op = (SDL_GPUStoreOp)StoreOp,
            stencil_load_op = (SDL_GPULoadOp)StencilLoadOp,
            stencil_store_op = (SDL_GPUStoreOp)StencilStoreOp,
            cycle = Cycle,
            clear_stencil = ClearStencil
        };
    }
}
