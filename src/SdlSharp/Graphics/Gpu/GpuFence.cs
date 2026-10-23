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
    internal readonly NativeLifetime Lifetime;
    private readonly GpuDevice _device;
    private readonly Native.SDL_GPUDevice* _devicePointer;
    private Native.SDL_GPUDevice* DeviceHandle { get { _device.Lifetime.Check(); return _devicePointer; } }
    internal GpuDevice Device => _device;

    /// <summary>
    /// The underlying native SDL_GPUFence pointer.
    /// </summary>
    internal SDL_GPUFence* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            Lifetime.Check();
            return _handle;
        }
    }

    private SDL_GPUFence* _handle;

    internal GpuFence(GpuDevice device, SDL_GPUFence* handle)
    {
        _device = new GpuDevice(device.Handle, ownsHandle: false);
        _devicePointer = device.Handle;
        _handle = handle;
        Lifetime = NativeLifetime.Get(typeof(GpuFence), (nint)handle, true, parent: device.Lifetime);
    }

    /// <summary>
    /// Gets whether this fence has been signaled by the GPU.
    /// </summary>
    public bool IsSignaled => SDL_QueryGPUFence(DeviceHandle, Handle);

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
        Span<nint> addresses = fences.Length <= 64 ? stackalloc nint[fences.Length] : new nint[fences.Length];
        fixed (nint* rawPointers = addresses)
        {
        var pointers = (SDL_GPUFence**)rawPointers;
        for (var i = 0; i < fences.Length; i++)
        {
            if (!ReferenceEquals(fences[i].Device.Lifetime, device.Lifetime)) throw new ArgumentException("Fences must belong to the same GPU device.", nameof(fences));
            pointers[i] = fences[i].Handle;
        }
        Check(SDL_WaitForGPUFences(device.Handle, waitAll, pointers, (uint)fences.Length));
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null && _device.Lifetime.IsAlive)
        {
            Lifetime.Release(() => { SDL_ReleaseGPUFence(DeviceHandle, _handle); });
        }
        _handle = null;
        _device.Dispose();
    }
}
