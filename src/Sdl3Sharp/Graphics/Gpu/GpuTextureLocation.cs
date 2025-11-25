using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes a location in a texture.
/// </summary>
public struct GpuTextureLocation
{
    /// <summary>
    /// The texture used in the copy operation.
    /// </summary>
    public GpuTexture? Texture { get; set; }

    /// <summary>
    /// The mip level index of the location.
    /// </summary>
    public uint MipLevel { get; set; }

    /// <summary>
    /// The layer index of the location.
    /// </summary>
    public uint Layer { get; set; }

    /// <summary>
    /// The left offset of the location.
    /// </summary>
    public uint X { get; set; }

    /// <summary>
    /// The top offset of the location.
    /// </summary>
    public uint Y { get; set; }

    /// <summary>
    /// The front offset of the location.
    /// </summary>
    public uint Z { get; set; }

    internal unsafe SDL_GPUTextureLocation ToNative()
    {
        return new SDL_GPUTextureLocation
        {
            texture = Texture != null ? Texture.Handle : null,
            mip_level = MipLevel,
            layer = Layer,
            x = X,
            y = Y,
            z = Z
        };
    }
}