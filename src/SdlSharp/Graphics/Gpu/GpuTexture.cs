using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU texture (SDL_GPUTexture).
/// GPU textures hold image data on the GPU for sampling, rendering, or storage.
/// </summary>
public sealed unsafe class GpuTexture : IDisposable
{
    private readonly GpuDevice _device;

    /// <summary>
    /// The underlying native SDL_GPUTexture pointer.
    /// </summary>
    internal SDL_GPUTexture* Handle { get; private set; }

    internal GpuTexture(GpuDevice device, SDL_GPUTexture* handle)
    {
        _device = device;
        Handle = handle;
    }

    /// <summary>
    /// Sets a debug name for this texture, visible in GPU debugging tools.
    /// </summary>
    /// <param name="name">The debug name.</param>
    public void SetName(string name) => SDL_SetGPUTextureName(_device.Handle, Handle, ToUtf8(name));

    /// <inheritdoc/>
    public void Dispose()
    {
        if (Handle != null)
        {
            SDL_ReleaseGPUTexture(_device.Handle, Handle);
            Handle = null;
        }
    }
}
