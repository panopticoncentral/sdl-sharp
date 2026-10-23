using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU sampler (SDL_GPUSampler).
/// Samplers define how textures are filtered and addressed during sampling.
/// </summary>
public sealed unsafe class GpuSampler : IDisposable
{
    internal readonly NativeLifetime Lifetime;
    private readonly GpuDevice _device;
    private readonly Native.SDL_GPUDevice* _devicePointer;
    private Native.SDL_GPUDevice* DeviceHandle { get { _device.Lifetime.Check(); return _devicePointer; } }
    internal GpuDevice Device => _device;

    /// <summary>
    /// The underlying native SDL_GPUSampler pointer.
    /// </summary>
    internal SDL_GPUSampler* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            Lifetime.Check();
            return _handle;
        }
    }

    private SDL_GPUSampler* _handle;

    internal GpuSampler(GpuDevice device, SDL_GPUSampler* handle)
    {
        _device = new GpuDevice(device.Handle, ownsHandle: false);
        _devicePointer = device.Handle;
        _handle = handle;
        Lifetime = NativeLifetime.Get(typeof(GpuSampler), (nint)handle, true, parent: device.Lifetime);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null && _device.Lifetime.IsAlive)
        {
            Lifetime.Release(() => { SDL_ReleaseGPUSampler(DeviceHandle, _handle); });
        }
        _handle = null;
        _device.Dispose();
    }
}
