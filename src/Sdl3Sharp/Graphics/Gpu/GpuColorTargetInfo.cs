using Sdl3Sharp.Graphics;
using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes the parameters for a color target in a render pass.
/// </summary>
public struct GpuColorTargetInfo
{
    /// <summary>
    /// The texture that will be used as a color target by a render pass.
    /// </summary>
    public GpuTexture? Texture { get; set; }

    /// <summary>
    /// The mip level to use as a color target.
    /// </summary>
    public uint MipLevel { get; set; }

    /// <summary>
    /// The layer index or depth plane to use as a color target.
    /// </summary>
    public uint LayerOrDepthPlane { get; set; }

    /// <summary>
    /// The color to clear the color target to at the start of the render pass.
    /// </summary>
    public ColorF ClearColor { get; set; }

    /// <summary>
    /// What is done with the contents of the color target at the beginning of the render pass.
    /// </summary>
    public GpuLoadOp LoadOp { get; set; }

    /// <summary>
    /// What is done with the results of the render pass.
    /// </summary>
    public GpuStoreOp StoreOp { get; set; }

    /// <summary>
    /// The texture that will receive the results of a multisample resolve operation.
    /// </summary>
    public GpuTexture? ResolveTexture { get; set; }

    /// <summary>
    /// The mip level of the resolve texture to use for the resolve operation.
    /// </summary>
    public uint ResolveMipLevel { get; set; }

    /// <summary>
    /// The layer index of the resolve texture to use for the resolve operation.
    /// </summary>
    public uint ResolveLayer { get; set; }

    /// <summary>
    /// True cycles the texture if the texture is bound and load_op is not LOAD.
    /// </summary>
    public bool Cycle { get; set; }

    /// <summary>
    /// True cycles the resolve texture if the resolve texture is bound.
    /// </summary>
    public bool CycleResolveTexture { get; set; }

    internal unsafe SDL_GPUColorTargetInfo ToNative()
    {
        return new SDL_GPUColorTargetInfo
        {
            texture = Texture != null ? Texture.Handle : null,
            mip_level = MipLevel,
            layer_or_depth_plane = LayerOrDepthPlane,
            clear_color = ClearColor.ToNative(),
            load_op = (SDL_GPULoadOp)LoadOp,
            store_op = (SDL_GPUStoreOp)StoreOp,
            resolve_texture = ResolveTexture != null ? ResolveTexture.Handle : null,
            resolve_mip_level = ResolveMipLevel,
            resolve_layer = ResolveLayer,
            cycle = Cycle,
            cycle_resolve_texture = CycleResolveTexture
        };
    }
}
