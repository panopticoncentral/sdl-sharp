using System.Runtime.InteropServices;
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

    /// <summary>
    /// Copies data into the transfer buffer, mapping and unmapping it in one call.
    /// </summary>
    /// <param name="data">The bytes to write.</param>
    /// <param name="offset">The byte offset into the transfer buffer to write at.</param>
    /// <param name="cycle">Whether to cycle the buffer to avoid GPU stalls.</param>
    /// <exception cref="ArgumentOutOfRangeException">The data does not fit at the given offset.</exception>
    public void Write(ReadOnlySpan<byte> data, uint offset = 0, bool cycle = false)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(offset + (ulong)data.Length, Size, nameof(data));
        var mapped = Check((byte*)SDL_MapGPUTransferBuffer(_device.Handle, Handle, cycle));
        data.CopyTo(new Span<byte>(mapped + offset, data.Length));
        SDL_UnmapGPUTransferBuffer(_device.Handle, Handle);
    }

    /// <summary>
    /// Copies a span of unmanaged values into the transfer buffer, mapping and unmapping it in one call.
    /// </summary>
    /// <typeparam name="T">The unmanaged element type.</typeparam>
    /// <param name="data">The values to write.</param>
    /// <param name="offset">The byte offset into the transfer buffer to write at.</param>
    /// <param name="cycle">Whether to cycle the buffer to avoid GPU stalls.</param>
    /// <exception cref="ArgumentOutOfRangeException">The data does not fit at the given offset.</exception>
    public void Write<T>(ReadOnlySpan<T> data, uint offset = 0, bool cycle = false) where T : unmanaged =>
        Write(MemoryMarshal.AsBytes(data), offset, cycle);

    /// <summary>
    /// Copies data out of the transfer buffer, mapping and unmapping it in one call.
    /// </summary>
    /// <param name="destination">The span to fill with bytes from the transfer buffer.</param>
    /// <param name="offset">The byte offset into the transfer buffer to read from.</param>
    /// <exception cref="ArgumentOutOfRangeException">The requested range does not fit within the buffer.</exception>
    public void Read(Span<byte> destination, uint offset = 0)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(offset + (ulong)destination.Length, Size, nameof(destination));
        var mapped = Check((byte*)SDL_MapGPUTransferBuffer(_device.Handle, Handle, false));
        new ReadOnlySpan<byte>(mapped + offset, destination.Length).CopyTo(destination);
        SDL_UnmapGPUTransferBuffer(_device.Handle, Handle);
    }

    /// <summary>
    /// Copies unmanaged values out of the transfer buffer, mapping and unmapping it in one call.
    /// </summary>
    /// <typeparam name="T">The unmanaged element type.</typeparam>
    /// <param name="destination">The span to fill with values from the transfer buffer.</param>
    /// <param name="offset">The byte offset into the transfer buffer to read from.</param>
    /// <exception cref="ArgumentOutOfRangeException">The requested range does not fit within the buffer.</exception>
    public void Read<T>(Span<T> destination, uint offset = 0) where T : unmanaged =>
        Read(MemoryMarshal.AsBytes(destination), offset);

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
