using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU fence (SDL_GPUFence).
/// Fences are signaled when submitted GPU work has completed.
/// </summary>
public sealed unsafe class GpuFence : IDisposable
{
    private readonly GpuDevice _device;

    /// <summary>
    /// The underlying native SDL_GPUFence pointer.
    /// </summary>
    internal SDL_GPUFence* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private SDL_GPUFence* _handle;

    internal GpuFence(GpuDevice device, SDL_GPUFence* handle)
    {
        _device = device;
        _handle = handle;
    }

    /// <summary>
    /// Gets whether this fence has been signaled by the GPU.
    /// </summary>
    public bool IsSignaled => SDL_QueryGPUFence(_device.Handle, Handle);

    /// <summary>
    /// Blocks until all of the given fences are signaled.
    /// All fences must belong to the given device.
    /// </summary>
    /// <param name="device">The device the fences belong to.</param>
    /// <param name="fences">The fences to wait for; all must have been created by <paramref name="device"/>.</param>
    public static void WaitAll(GpuDevice device, ReadOnlySpan<GpuFence> fences) => Wait(device, waitAll: true, fences);

    /// <summary>
    /// Blocks until at least one of the given fences is signaled.
    /// All fences must belong to the given device.
    /// </summary>
    /// <param name="device">The device the fences belong to.</param>
    /// <param name="fences">The fences to wait for; all must have been created by <paramref name="device"/>.</param>
    public static void WaitAny(GpuDevice device, ReadOnlySpan<GpuFence> fences) => Wait(device, waitAll: false, fences);

    private static void Wait(GpuDevice device, bool waitAll, ReadOnlySpan<GpuFence> fences)
    {
        var pointers = stackalloc SDL_GPUFence*[fences.Length];
        for (var i = 0; i < fences.Length; i++) pointers[i] = fences[i].Handle;
        Check(SDL_WaitForGPUFences(device.Handle, waitAll, pointers, (uint)fences.Length));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null && !_device.IsDisposed)
        {
            SDL_ReleaseGPUFence(_device.Handle, _handle);
        }
        _handle = null;
    }
}
