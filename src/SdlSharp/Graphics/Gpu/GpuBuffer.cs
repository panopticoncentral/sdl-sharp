using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU buffer (SDL_GPUBuffer).
/// GPU buffers hold vertex, index, indirect, or storage data on the GPU.
/// </summary>
public sealed unsafe class GpuBuffer : IDisposable
{
    internal readonly NativeLifetime Lifetime;
    private readonly GpuDevice _device;
    private readonly Native.SDL_GPUDevice* _devicePointer;
    private Native.SDL_GPUDevice* DeviceHandle { get { _device.Lifetime.Check(); return _devicePointer; } }
    internal GpuDevice Device => _device;

    /// <summary>
    /// The underlying native SDL_GPUBuffer pointer.
    /// </summary>
    internal SDL_GPUBuffer* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            Lifetime.Check();
            return _handle;
        }
    }

    private SDL_GPUBuffer* _handle;

    internal GpuBuffer(GpuDevice device, SDL_GPUBuffer* handle)
    {
        _device = new GpuDevice(device.Handle, ownsHandle: false);
        _devicePointer = device.Handle;
        _handle = handle;
        Lifetime = NativeLifetime.Get(typeof(GpuBuffer), (nint)handle, true, parent: device.Lifetime);
    }

    /// <summary>
    /// Sets a debug name for this buffer, visible in GPU debugging tools.
    /// </summary>
    /// <param name="name">The debug name.</param>
    public void SetName(string name) => SDL_SetGPUBufferName(DeviceHandle, Handle, ToUtf8(name));

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null && _device.Lifetime.IsAlive)
        {
            Lifetime.Release(() => { SDL_ReleaseGPUBuffer(DeviceHandle, _handle); });
        }
        _handle = null;
        _device.Dispose();
    }
}
