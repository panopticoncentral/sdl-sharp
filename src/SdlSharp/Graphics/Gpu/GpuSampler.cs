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
    internal SDL_GPUSampler* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private SDL_GPUSampler* _handle;

    internal GpuSampler(GpuDevice device, SDL_GPUSampler* handle)
    {
        _device = device;
        _handle = handle;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null && !_device.IsDisposed)
        {
            SDL_ReleaseGPUSampler(_device.Handle, _handle);
        }
        _handle = null;
    }
}
