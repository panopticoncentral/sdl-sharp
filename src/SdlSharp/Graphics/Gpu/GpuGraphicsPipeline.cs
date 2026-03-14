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
    internal SDL_GPUGraphicsPipeline* Handle { get; private set; }

    internal GpuGraphicsPipeline(GpuDevice device, SDL_GPUGraphicsPipeline* handle)
    {
        _device = device;
        Handle = handle;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (Handle != null)
        {
            SDL_ReleaseGPUGraphicsPipeline(_device.Handle, Handle);
            Handle = null;
        }
    }
}
