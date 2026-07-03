using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU compute pipeline (SDL_GPUComputePipeline).
/// Compute pipelines define the state for compute dispatch calls.
/// </summary>
public sealed unsafe class GpuComputePipeline : IDisposable
{
    private readonly GpuDevice _device;

    /// <summary>
    /// The underlying native SDL_GPUComputePipeline pointer.
    /// </summary>
    internal SDL_GPUComputePipeline* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private SDL_GPUComputePipeline* _handle;

    internal GpuComputePipeline(GpuDevice device, SDL_GPUComputePipeline* handle)
    {
        _device = device;
        _handle = handle;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null && !_device.IsDisposed)
        {
            SDL_ReleaseGPUComputePipeline(_device.Handle, _handle);
        }
        _handle = null;
    }
}
