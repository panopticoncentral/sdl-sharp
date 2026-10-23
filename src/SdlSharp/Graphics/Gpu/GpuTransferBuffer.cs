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
    internal readonly NativeLifetime Lifetime;
    private readonly GpuDevice _device;
    private readonly Native.SDL_GPUDevice* _devicePointer;
    private Native.SDL_GPUDevice* DeviceHandle { get { _device.Lifetime.Check(); return _devicePointer; } }
    internal GpuDevice Device => _device;

    /// <summary>
    /// The underlying native SDL_GPUTransferBuffer pointer.
    /// </summary>
    internal SDL_GPUTransferBuffer* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            Lifetime.Check();
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
        _device = new GpuDevice(device.Handle, ownsHandle: false);
        _devicePointer = device.Handle;
        _handle = handle;
        Lifetime = NativeLifetime.Get(typeof(GpuTransferBuffer), (nint)handle, true, parent: device.Lifetime);
        Size = size;
    }

    /// <summary>Maps this buffer until the returned lease is disposed.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The lease is transferred to the returned scope or lifetime cleanup, which releases it exactly once.")]
    public GpuBufferMap Map(bool cycle = false)
    {
        var handle = Handle;
        var device = DeviceHandle;
        var size = checked((int)Size);
        var lease = Lifetime.RetainExclusive();
        try
        {
            var pixels = Check((byte*)SDL_MapGPUTransferBuffer(device, handle, cycle));
            return new GpuBufferMap((nint)pixels, size, () => { SDL_UnmapGPUTransferBuffer(device, handle); lease.Dispose(); });
        }
        catch { lease.Dispose(); throw; }
    }

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
        using var mapping = Map(cycle);
        data.CopyTo(mapping.Span.Slice(checked((int)offset), data.Length));
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
        using var mapping = Map();
        mapping.Span.Slice(checked((int)offset), destination.Length).CopyTo(destination);
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
        if (_handle != null && _device.Lifetime.IsAlive)
        {
            Lifetime.Release(() => { SDL_ReleaseGPUTransferBuffer(DeviceHandle, _handle); });
        }
        _handle = null;
        _device.Dispose();
    }
}
