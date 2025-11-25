using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes a region of a texture.
/// </summary>
public struct GpuTextureRegion
{
    /// <summary>
    /// The texture used in the copy operation.
    /// </summary>
    public GpuTexture? Texture { get; set; }

    /// <summary>
    /// The mip level index to transfer.
    /// </summary>
    public uint MipLevel { get; set; }

    /// <summary>
    /// The layer index to transfer.
    /// </summary>
    public uint Layer { get; set; }

    /// <summary>
    /// The left offset of the region.
    /// </summary>
    public uint X { get; set; }

    /// <summary>
    /// The top offset of the region.
    /// </summary>
    public uint Y { get; set; }

    /// <summary>
    /// The front offset of the region.
    /// </summary>
    public uint Z { get; set; }

    /// <summary>
    /// The width of the region.
    /// </summary>
    public uint Width { get; set; }

    /// <summary>
    /// The height of the region.
    /// </summary>
    public uint Height { get; set; }

    /// <summary>
    /// The depth of the region.
    /// </summary>
    public uint Depth { get; set; }

    internal unsafe SDL_GPUTextureRegion ToNative()
    {
        return new SDL_GPUTextureRegion
        {
            texture = Texture != null ? Texture.Handle : null,
            mip_level = MipLevel,
            layer = Layer,
            x = X,
            y = Y,
            z = Z,
            w = Width,
            h = Height,
            d = Depth
        };
    }
}