using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU fence (SDL_GPUFence).
/// Fences are signaled when submitted GPU work has completed.
/// </summary>
public sealed unsafe class GpuFence : IDisposable
{
    private readonly GpuDevice _device;

    /// <summary>
    /// The underlying native SDL_GPUFence pointer.
    /// </summary>
    internal SDL_GPUFence* Handle { get; private set; }

    internal GpuFence(GpuDevice device, SDL_GPUFence* handle)
    {
        _device = device;
        Handle = handle;
    }

    /// <summary>
    /// Gets whether this fence has been signaled by the GPU.
    /// </summary>
    public bool IsSignaled => SDL_QueryGPUFence(_device.Handle, Handle);

    /// <inheritdoc/>
    public void Dispose()
    {
        if (Handle != null)
        {
            SDL_ReleaseGPUFence(_device.Handle, Handle);
            Handle = null;
        }
    }
}
