using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU graphics pipeline (SDL_GPUGraphicsPipeline).
/// Graphics pipelines define the full rendering state for draw calls.
/// </summary>
public sealed unsafe class GpuGraphicsPipeline : IDisposable
{
    internal readonly NativeLifetime Lifetime;
    private readonly GpuDevice _device;
    private readonly Native.SDL_GPUDevice* _devicePointer;
    private Native.SDL_GPUDevice* DeviceHandle { get { _device.Lifetime.Check(); return _devicePointer; } }
    internal GpuDevice Device => _device;

    /// <summary>
    /// The underlying native SDL_GPUGraphicsPipeline pointer.
    /// </summary>
    internal SDL_GPUGraphicsPipeline* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            Lifetime.Check();
            return _handle;
        }
    }

    private SDL_GPUGraphicsPipeline* _handle;

    internal GpuGraphicsPipeline(GpuDevice device, SDL_GPUGraphicsPipeline* handle)
    {
        _device = new GpuDevice(device.Handle, ownsHandle: false);
        _devicePointer = device.Handle;
        _handle = handle;
        Lifetime = NativeLifetime.Get(typeof(GpuGraphicsPipeline), (nint)handle, true, parent: device.Lifetime);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null && _device.Lifetime.IsAlive)
        {
            Lifetime.Release(() => { SDL_ReleaseGPUGraphicsPipeline(DeviceHandle, _handle); });
        }
        _handle = null;
        _device.Dispose();
    }
}
