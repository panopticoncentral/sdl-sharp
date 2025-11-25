using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes storage texture binding for compute passes.
/// </summary>
public struct GpuStorageTextureReadWriteBinding
{
    /// <summary>
    /// The texture to bind.
    /// </summary>
    public GpuTexture? Texture { get; set; }

    /// <summary>
    /// The mip level index to bind.
    /// </summary>
    public uint MipLevel { get; set; }

    /// <summary>
    /// The layer index to bind.
    /// </summary>
    public uint Layer { get; set; }

    /// <summary>
    /// True cycles the texture if it is already bound.
    /// </summary>
    public bool Cycle { get; set; }

    internal unsafe SDL_GPUStorageTextureReadWriteBinding ToNative()
    {
        return new SDL_GPUStorageTextureReadWriteBinding
        {
            texture = Texture != null ? Texture.Handle : null,
            mip_level = MipLevel,
            layer = Layer,
            cycle = Cycle
        };
    }
}
