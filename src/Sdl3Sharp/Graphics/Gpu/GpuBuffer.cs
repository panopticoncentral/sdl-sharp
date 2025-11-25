using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents a GPU buffer for use in graphics or compute workflows.
/// </summary>
public sealed unsafe class GpuBuffer : IDisposable
{
    private bool _disposed;
    private readonly GpuDevice _device;

    /// <summary>
    /// Gets the underlying SDL_GPUBuffer pointer.
    /// </summary>
    public SDL_GPUBuffer* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPUBuffer pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPUBuffer pointer.</param>
    /// <param name="device">The GPU device that created this buffer.</param>
    internal GpuBuffer(SDL_GPUBuffer* handle, GpuDevice device)
    {
        Handle = handle;
        _device = device;
    }

    /// <summary>
    /// Creates a buffer object.
    /// </summary>
    /// <param name="device">The GPU device.</param>
    /// <param name="usage">How the buffer is intended to be used.</param>
    /// <param name="size">The size in bytes of the buffer.</param>
    /// <returns>A new buffer.</returns>
    public static GpuBuffer Create(GpuDevice device, GpuBufferUsage usage, uint size)
    {
        var createInfo = new SDL_GPUBufferCreateInfo
        {
            usage = (SDL_GPUBufferUsageFlags)usage,
            size = size,
            props = 0
        };
        return new GpuBuffer(CheckErrorPointer(SDL_CreateGPUBuffer(device.Handle, &createInfo)), device);
    }

    /// <summary>
    /// Sets an arbitrary string constant to label this buffer for debugging.
    /// </summary>
    /// <param name="name">The name to set.</param>
    public void SetName(string name)
    {
        ThrowIfDisposed();
        SDL_SetGPUBufferName(_device.Handle, Handle, name);
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
            SDL_ReleaseGPUBuffer(_device.Handle, Handle);
        }

        Handle = null;
        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
