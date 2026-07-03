using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU transfer buffer (SDL_GPUTransferBuffer).
/// Transfer buffers are used to move data between the CPU and GPU.
/// </summary>
public sealed unsafe class GpuTransferBuffer : IDisposable
{
    private readonly GpuDevice _device;

    /// <summary>
    /// The underlying native SDL_GPUTransferBuffer pointer.
    /// </summary>
    internal SDL_GPUTransferBuffer* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private SDL_GPUTransferBuffer* _handle;

    /// <summary>
    /// The size of the transfer buffer in bytes, as specified at creation.
    /// </summary>
    public uint Size { get; }

    internal GpuTransferBuffer(GpuDevice device, SDL_GPUTransferBuffer* handle, uint size)
    {
        _device = device;
        _handle = handle;
        Size = size;
    }

    /// <summary>
    /// Maps the transfer buffer for CPU access.
    /// The returned span is valid only until <see cref="Unmap"/> is called.
    /// </summary>
    /// <param name="cycle">Whether to cycle the buffer to avoid GPU stalls.</param>
    /// <returns>A span over the mapped memory.</returns>
    public Span<byte> Map(bool cycle = false) =>
        new(Check((byte*)SDL_MapGPUTransferBuffer(_device.Handle, Handle, cycle)), (int)Size);

    /// <summary>
    /// Unmaps the transfer buffer, making it available for GPU operations again.
    /// </summary>
    public void Unmap() => SDL_UnmapGPUTransferBuffer(_device.Handle, Handle);

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null && !_device.IsDisposed)
        {
            SDL_ReleaseGPUTransferBuffer(_device.Handle, _handle);
        }
        _handle = null;
    }
}
