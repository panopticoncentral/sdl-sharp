using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents a GPU texture for use in graphics or compute workflows.
/// </summary>
public sealed unsafe class GpuTexture : IDisposable
{
    private bool _disposed;
    private readonly GpuDevice _device;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Gets the underlying SDL_GPUTexture pointer.
    /// </summary>
    public SDL_GPUTexture* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPUTexture pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPUTexture pointer.</param>
    /// <param name="device">The GPU device that created this texture.</param>
    /// <param name="ownsHandle">Whether this instance owns the handle and should free it on disposal.</param>
    internal GpuTexture(SDL_GPUTexture* handle, GpuDevice device, bool ownsHandle)
    {
        Handle = handle;
        _device = device;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Creates a texture object.
    /// </summary>
    /// <param name="device">The GPU device.</param>
    /// <param name="createInfo">The texture creation info.</param>
    /// <returns>A new texture.</returns>
    public static GpuTexture Create(GpuDevice device, GpuTextureCreateInfo createInfo)
    {
        SDL_GPUTextureCreateInfo nativeInfo = createInfo.ToNative();
        return new GpuTexture(CheckErrorPointer(SDL_CreateGPUTexture(device.Handle, &nativeInfo)), device, ownsHandle: true);
    }

    /// <summary>
    /// Sets an arbitrary string constant to label this texture for debugging.
    /// </summary>
    /// <param name="name">The name to set.</param>
    public void SetName(string name)
    {
        ThrowIfDisposed();
        SDL_SetGPUTextureName(_device.Handle, Handle, name);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Handle != null)
        {
            SDL_ReleaseGPUTexture(_device.Handle, Handle);
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
/// Describes the parameters for creating a GPU texture.
/// </summary>
public struct GpuTextureCreateInfo
{
    /// <summary>
    /// The base dimensionality of the texture.
    /// </summary>
    public GpuTextureType Type { get; set; }

    /// <summary>
    /// The pixel format of the texture.
    /// </summary>
    public GpuTextureFormat Format { get; set; }

    /// <summary>
    /// How the texture is intended to be used by the client.
    /// </summary>
    public GpuTextureUsage Usage { get; set; }

    /// <summary>
    /// The width of the texture.
    /// </summary>
    public uint Width { get; set; }

    /// <summary>
    /// The height of the texture.
    /// </summary>
    public uint Height { get; set; }

    /// <summary>
    /// The layer count or depth of the texture.
    /// </summary>
    public uint LayerCountOrDepth { get; set; }

    /// <summary>
    /// The number of mip levels in the texture.
    /// </summary>
    public uint NumLevels { get; set; }

    /// <summary>
    /// The number of samples per texel.
    /// </summary>
    public GpuSampleCount SampleCount { get; set; }

    internal SDL_GPUTextureCreateInfo ToNative()
    {
        return new SDL_GPUTextureCreateInfo
        {
            type = (SDL_GPUTextureType)Type,
            format = (SDL_GPUTextureFormat)Format,
            usage = (SDL_GPUTextureUsageFlags)Usage,
            width = Width,
            height = Height,
            layer_count_or_depth = LayerCountOrDepth,
            num_levels = NumLevels,
            sample_count = (SDL_GPUSampleCount)SampleCount,
            props = 0
        };
    }
}
