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
    private readonly GpuDevice _device;

    /// <summary>
    /// The underlying native SDL_GPUBuffer pointer.
    /// </summary>
    internal SDL_GPUBuffer* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private SDL_GPUBuffer* _handle;

    internal GpuBuffer(GpuDevice device, SDL_GPUBuffer* handle)
    {
        _device = device;
        _handle = handle;
    }

    /// <summary>
    /// Sets a debug name for this buffer, visible in GPU debugging tools.
    /// </summary>
    /// <param name="name">The debug name.</param>
    public void SetName(string name) => SDL_SetGPUBufferName(_device.Handle, Handle, ToUtf8(name));

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null && !_device.IsDisposed)
        {
            SDL_ReleaseGPUBuffer(_device.Handle, _handle);
        }
        _handle = null;
    }
}
