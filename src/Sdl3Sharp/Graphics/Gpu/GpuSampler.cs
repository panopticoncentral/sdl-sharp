using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents a GPU sampler for texture sampling operations.
/// </summary>
public sealed unsafe class GpuSampler : IDisposable
{
    private bool _disposed;
    private readonly GpuDevice _device;

    /// <summary>
    /// Gets the underlying SDL_GPUSampler pointer.
    /// </summary>
    public SDL_GPUSampler* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPUSampler pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPUSampler pointer.</param>
    /// <param name="device">The GPU device that created this sampler.</param>
    internal GpuSampler(SDL_GPUSampler* handle, GpuDevice device)
    {
        Handle = handle;
        _device = device;
    }

    /// <summary>
    /// Creates a sampler object.
    /// </summary>
    /// <param name="device">The GPU device.</param>
    /// <param name="createInfo">The sampler creation info.</param>
    /// <returns>A new sampler.</returns>
    public static GpuSampler Create(GpuDevice device, GpuSamplerCreateInfo createInfo)
    {
        var nativeInfo = new SDL_GPUSamplerCreateInfo
        {
            min_filter = (SDL_GPUFilter)createInfo.MinFilter,
            mag_filter = (SDL_GPUFilter)createInfo.MagFilter,
            mipmap_mode = (SDL_GPUSamplerMipmapMode)createInfo.MipmapMode,
            address_mode_u = (SDL_GPUSamplerAddressMode)createInfo.AddressModeU,
            address_mode_v = (SDL_GPUSamplerAddressMode)createInfo.AddressModeV,
            address_mode_w = (SDL_GPUSamplerAddressMode)createInfo.AddressModeW,
            mip_lod_bias = createInfo.MipLodBias,
            max_anisotropy = createInfo.MaxAnisotropy,
            compare_op = (SDL_GPUCompareOp)createInfo.CompareOp,
            min_lod = createInfo.MinLod,
            max_lod = createInfo.MaxLod,
            enable_anisotropy = createInfo.EnableAnisotropy,
            enable_compare = createInfo.EnableCompare,
            props = 0
        };
        return new GpuSampler(CheckErrorPointer(SDL_CreateGPUSampler(device.Handle, &nativeInfo)), device);
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
            SDL_ReleaseGPUSampler(_device.Handle, Handle);
        }

        Handle = null;
        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}

/// <summary>
/// Describes the parameters for creating a GPU sampler.
/// </summary>
public struct GpuSamplerCreateInfo
{
    /// <summary>
    /// The minification filter to apply to lookups.
    /// </summary>
    public GpuFilter MinFilter { get; set; }

    /// <summary>
    /// The magnification filter to apply to lookups.
    /// </summary>
    public GpuFilter MagFilter { get; set; }

    /// <summary>
    /// The mipmap filter to apply to lookups.
    /// </summary>
    public GpuSamplerMipmapMode MipmapMode { get; set; }

    /// <summary>
    /// The addressing mode for U coordinates outside [0, 1).
    /// </summary>
    public GpuSamplerAddressMode AddressModeU { get; set; }

    /// <summary>
    /// The addressing mode for V coordinates outside [0, 1).
    /// </summary>
    public GpuSamplerAddressMode AddressModeV { get; set; }

    /// <summary>
    /// The addressing mode for W coordinates outside [0, 1).
    /// </summary>
    public GpuSamplerAddressMode AddressModeW { get; set; }

    /// <summary>
    /// The bias to be added to mipmap LOD calculation.
    /// </summary>
    public float MipLodBias { get; set; }

    /// <summary>
    /// The anisotropy value clamp used by the sampler.
    /// </summary>
    public float MaxAnisotropy { get; set; }

    /// <summary>
    /// The comparison operator to apply to fetched data before filtering.
    /// </summary>
    public GpuCompareOp CompareOp { get; set; }

    /// <summary>
    /// Clamps the minimum of the computed LOD value.
    /// </summary>
    public float MinLod { get; set; }

    /// <summary>
    /// Clamps the maximum of the computed LOD value.
    /// </summary>
    public float MaxLod { get; set; }

    /// <summary>
    /// True to enable anisotropic filtering.
    /// </summary>
    public bool EnableAnisotropy { get; set; }

    /// <summary>
    /// True to enable comparison against a reference value during lookups.
    /// </summary>
    public bool EnableCompare { get; set; }
}
