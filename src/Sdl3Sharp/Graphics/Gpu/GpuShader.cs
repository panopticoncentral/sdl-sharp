using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents a compiled GPU shader object.
/// </summary>
public sealed unsafe class GpuShader : IDisposable
{
    private bool _disposed;
    private readonly GpuDevice _device;

    /// <summary>
    /// Gets the underlying SDL_GPUShader pointer.
    /// </summary>
    public SDL_GPUShader* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPUShader pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPUShader pointer.</param>
    /// <param name="device">The GPU device that created this shader.</param>
    internal GpuShader(SDL_GPUShader* handle, GpuDevice device)
    {
        Handle = handle;
        _device = device;
    }

    /// <summary>
    /// Creates a shader object.
    /// </summary>
    /// <param name="device">The GPU device.</param>
    /// <param name="createInfo">The shader creation info.</param>
    /// <returns>A new shader.</returns>
    public static GpuShader Create(GpuDevice device, GpuShaderCreateInfo createInfo)
    {
        fixed (byte* codePtr = createInfo.Code)
        fixed (byte* entrypointPtr = createInfo.EntrypointUtf8)
        {
            var nativeInfo = new SDL_GPUShaderCreateInfo
            {
                code_size = (nuint)createInfo.Code.Length,
                code = codePtr,
                entrypoint = entrypointPtr,
                format = (SDL_GPUShaderFormat)createInfo.Format,
                stage = (SDL_GPUShaderStage)createInfo.Stage,
                num_samplers = createInfo.NumSamplers,
                num_storage_textures = createInfo.NumStorageTextures,
                num_storage_buffers = createInfo.NumStorageBuffers,
                num_uniform_buffers = createInfo.NumUniformBuffers,
                props = 0
            };
            return new GpuShader(CheckErrorPointer(SDL_CreateGPUShader(device.Handle, &nativeInfo)), device);
        }
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
            SDL_ReleaseGPUShader(_device.Handle, Handle);
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
/// Describes the parameters for creating a GPU shader.
/// </summary>
public struct GpuShaderCreateInfo
{
    /// <summary>
    /// The shader code.
    /// </summary>
    public byte[] Code { get; set; }

    /// <summary>
    /// The entry point function name as UTF-8 bytes (including null terminator).
    /// </summary>
    public byte[] EntrypointUtf8 { get; set; }

    /// <summary>
    /// The format of the shader code.
    /// </summary>
    public GpuShaderFormat Format { get; set; }

    /// <summary>
    /// The stage the shader program corresponds to.
    /// </summary>
    public GpuShaderStage Stage { get; set; }

    /// <summary>
    /// The number of samplers defined in the shader.
    /// </summary>
    public uint NumSamplers { get; set; }

    /// <summary>
    /// The number of storage textures defined in the shader.
    /// </summary>
    public uint NumStorageTextures { get; set; }

    /// <summary>
    /// The number of storage buffers defined in the shader.
    /// </summary>
    public uint NumStorageBuffers { get; set; }

    /// <summary>
    /// The number of uniform buffers defined in the shader.
    /// </summary>
    public uint NumUniformBuffers { get; set; }

    /// <summary>
    /// Creates a shader create info with an entrypoint string.
    /// </summary>
    /// <param name="code">The shader code.</param>
    /// <param name="entrypoint">The entry point function name.</param>
    /// <param name="format">The shader format.</param>
    /// <param name="stage">The shader stage.</param>
    /// <returns>A new shader create info.</returns>
    public static GpuShaderCreateInfo FromEntrypoint(byte[] code, string entrypoint, GpuShaderFormat format, GpuShaderStage stage)
    {
        return new GpuShaderCreateInfo
        {
            Code = code,
            EntrypointUtf8 = System.Text.Encoding.UTF8.GetBytes(entrypoint + "\0"),
            Format = format,
            Stage = stage
        };
    }
}
