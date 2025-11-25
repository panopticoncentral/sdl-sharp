using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents a GPU fence for synchronization.
/// </summary>
public sealed unsafe class GpuFence : IDisposable
{
    private bool _disposed;
    private readonly GpuDevice _device;

    /// <summary>
    /// Gets the underlying SDL_GPUFence pointer.
    /// </summary>
    public SDL_GPUFence* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPUFence pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPUFence pointer.</param>
    /// <param name="device">The GPU device that created this fence.</param>
    internal GpuFence(SDL_GPUFence* handle, GpuDevice device)
    {
        Handle = handle;
        _device = device;
    }

    /// <summary>
    /// Queries the status of a fence.
    /// </summary>
    /// <returns>True if the fence is signaled, false otherwise.</returns>
    public bool QueryStatus()
    {
        ThrowIfDisposed();
        return SDL_QueryGPUFence(_device.Handle, Handle);
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
            SDL_ReleaseGPUFence(_device.Handle, Handle);
        }

        Handle = null;
        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
