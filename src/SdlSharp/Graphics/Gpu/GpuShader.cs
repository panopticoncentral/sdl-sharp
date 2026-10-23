using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU shader (SDL_GPUShader).
/// Shaders are compiled shader programs used in graphics or compute pipelines.
/// </summary>
public sealed unsafe class GpuShader : IDisposable
{
    internal readonly NativeLifetime Lifetime;
    private readonly GpuDevice _device;
    private readonly Native.SDL_GPUDevice* _devicePointer;
    private Native.SDL_GPUDevice* DeviceHandle { get { _device.Lifetime.Check(); return _devicePointer; } }
    internal GpuDevice Device => _device;

    /// <summary>
    /// The underlying native SDL_GPUShader pointer.
    /// </summary>
    internal SDL_GPUShader* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            Lifetime.Check();
            return _handle;
        }
    }

    private SDL_GPUShader* _handle;

    internal GpuShader(GpuDevice device, SDL_GPUShader* handle)
    {
        _device = new GpuDevice(device.Handle, ownsHandle: false);
        _devicePointer = device.Handle;
        _handle = handle;
        Lifetime = NativeLifetime.Get(typeof(GpuShader), (nint)handle, true, parent: device.Lifetime);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null && _device.Lifetime.IsAlive)
        {
            Lifetime.Release(() => { SDL_ReleaseGPUShader(DeviceHandle, _handle); });
        }
        _handle = null;
        _device.Dispose();
    }
}
