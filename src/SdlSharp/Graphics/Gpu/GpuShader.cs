using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU shader (SDL_GPUShader).
/// Shaders are compiled shader programs used in graphics or compute pipelines.
/// </summary>
public sealed unsafe class GpuShader : IDisposable
{
    private readonly GpuDevice _device;

    /// <summary>
    /// The underlying native SDL_GPUShader pointer.
    /// </summary>
    internal SDL_GPUShader* Handle { get; private set; }

    internal GpuShader(GpuDevice device, SDL_GPUShader* handle)
    {
        _device = device;
        Handle = handle;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (Handle != null)
        {
            SDL_ReleaseGPUShader(_device.Handle, Handle);
            Handle = null;
        }
    }
}
