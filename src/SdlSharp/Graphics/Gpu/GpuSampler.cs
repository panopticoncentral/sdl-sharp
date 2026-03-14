using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU sampler (SDL_GPUSampler).
/// Samplers define how textures are filtered and addressed during sampling.
/// </summary>
public sealed unsafe class GpuSampler : IDisposable
{
    private readonly GpuDevice _device;

    /// <summary>
    /// The underlying native SDL_GPUSampler pointer.
    /// </summary>
    internal SDL_GPUSampler* Handle { get; private set; }

    internal GpuSampler(GpuDevice device, SDL_GPUSampler* handle)
    {
        _device = device;
        Handle = handle;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (Handle != null)
        {
            SDL_ReleaseGPUSampler(_device.Handle, Handle);
            Handle = null;
        }
    }
}
