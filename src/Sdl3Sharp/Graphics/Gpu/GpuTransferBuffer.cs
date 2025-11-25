using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents a GPU transfer buffer for uploading to or downloading from graphics resources.
/// </summary>
public sealed unsafe class GpuTransferBuffer : IDisposable
{
    private bool _disposed;
    private readonly GpuDevice _device;

    /// <summary>
    /// Gets the underlying SDL_GPUTransferBuffer pointer.
    /// </summary>
    public SDL_GPUTransferBuffer* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPUTransferBuffer pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPUTransferBuffer pointer.</param>
    /// <param name="device">The GPU device that created this buffer.</param>
    internal GpuTransferBuffer(SDL_GPUTransferBuffer* handle, GpuDevice device)
    {
        Handle = handle;
        _device = device;
    }

    /// <summary>
    /// Creates a transfer buffer object.
    /// </summary>
    /// <param name="device">The GPU device.</param>
    /// <param name="usage">How the transfer buffer is intended to be used.</param>
    /// <param name="size">The size in bytes of the transfer buffer.</param>
    /// <returns>A new transfer buffer.</returns>
    public static GpuTransferBuffer Create(GpuDevice device, GpuTransferBufferUsage usage, uint size)
    {
        var createInfo = new SDL_GPUTransferBufferCreateInfo
        {
            usage = (SDL_GPUTransferBufferUsage)usage,
            size = size,
            props = 0
        };
        return new GpuTransferBuffer(CheckErrorPointer(SDL_CreateGPUTransferBuffer(device.Handle, &createInfo)), device);
    }

    /// <summary>
    /// Maps the transfer buffer into application address space.
    /// </summary>
    /// <param name="cycle">If true, cycles the transfer buffer if it is already bound.</param>
    /// <returns>A pointer to the mapped memory.</returns>
    public void* Map(bool cycle = false)
    {
        ThrowIfDisposed();
        return CheckErrorPointer(SDL_MapGPUTransferBuffer(_device.Handle, Handle, cycle));
    }

    /// <summary>
    /// Maps the transfer buffer and returns a span of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of elements in the buffer.</typeparam>
    /// <param name="count">The number of elements.</param>
    /// <param name="cycle">If true, cycles the transfer buffer if it is already bound.</param>
    /// <returns>A span over the mapped memory.</returns>
    public Span<T> Map<T>(int count, bool cycle = false) where T : unmanaged
    {
        ThrowIfDisposed();
        var ptr = CheckErrorPointer(SDL_MapGPUTransferBuffer(_device.Handle, Handle, cycle));
        return new Span<T>(ptr, count);
    }

    /// <summary>
    /// Unmaps a previously mapped transfer buffer.
    /// </summary>
    public void Unmap()
    {
        ThrowIfDisposed();
        SDL_UnmapGPUTransferBuffer(_device.Handle, Handle);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (Handle != null)
        {
            SDL_ReleaseGPUTransferBuffer(_device.Handle, Handle);
        }

        Handle = null;
        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
