using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Describes a texture-sampler binding.
/// </summary>
public struct GpuTextureSamplerBinding
{
    /// <summary>
    /// The texture to bind.
    /// </summary>
    public GpuTexture? Texture { get; set; }

    /// <summary>
    /// The sampler to bind.
    /// </summary>
    public GpuSampler? Sampler { get; set; }

    internal unsafe SDL_GPUTextureSamplerBinding ToNative()
    {
        return new SDL_GPUTextureSamplerBinding
        {
            texture = Texture != null ? Texture.Handle : null,
            sampler = Sampler != null ? Sampler.Handle : null
        };
    }
}