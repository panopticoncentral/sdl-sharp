using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU graphics pipeline (SDL_GPUGraphicsPipeline).
/// Graphics pipelines define the full rendering state for draw calls.
/// </summary>
public sealed unsafe class GpuGraphicsPipeline : IDisposable
{
    private readonly GpuDevice _device;

    /// <summary>
    /// The underlying native SDL_GPUGraphicsPipeline pointer.
    /// </summary>
    internal SDL_GPUGraphicsPipeline* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private SDL_GPUGraphicsPipeline* _handle;

    internal GpuGraphicsPipeline(GpuDevice device, SDL_GPUGraphicsPipeline* handle)
    {
        _device = device;
        _handle = handle;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null && !_device.IsDisposed)
        {
            SDL_ReleaseGPUGraphicsPipeline(_device.Handle, _handle);
        }
        _handle = null;
    }
}
