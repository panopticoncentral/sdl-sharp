using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes a region of a texture for blit operations.
/// </summary>
public struct GpuBlitRegion
{
    /// <summary>
    /// The texture.
    /// </summary>
    public GpuTexture? Texture { get; set; }

    /// <summary>
    /// The mip level index of the region.
    /// </summary>
    public uint MipLevel { get; set; }

    /// <summary>
    /// The layer index or depth plane of the region.
    /// </summary>
    public uint LayerOrDepthPlane { get; set; }

    /// <summary>
    /// The left offset of the region.
    /// </summary>
    public uint X { get; set; }

    /// <summary>
    /// The top offset of the region.
    /// </summary>
    public uint Y { get; set; }

    /// <summary>
    /// The width of the region.
    /// </summary>
    public uint Width { get; set; }

    /// <summary>
    /// The height of the region.
    /// </summary>
    public uint Height { get; set; }

    internal unsafe SDL_GPUBlitRegion ToNative()
    {
        return new SDL_GPUBlitRegion
        {
            texture = Texture != null ? Texture.Handle : null,
            mip_level = MipLevel,
            layer_or_depth_plane = LayerOrDepthPlane,
            x = X,
            y = Y,
            w = Width,
            h = Height
        };
    }
}