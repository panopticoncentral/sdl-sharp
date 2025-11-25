using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents a GPU compute pipeline for compute shader operations.
/// </summary>
public sealed unsafe class GpuComputePipeline : IDisposable
{
    private bool _disposed;
    private readonly GpuDevice _device;

    /// <summary>
    /// Gets the underlying SDL_GPUComputePipeline pointer.
    /// </summary>
    public SDL_GPUComputePipeline* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPUComputePipeline pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPUComputePipeline pointer.</param>
    /// <param name="device">The GPU device that created this pipeline.</param>
    internal GpuComputePipeline(SDL_GPUComputePipeline* handle, GpuDevice device)
    {
        Handle = handle;
        _device = device;
    }

    /// <summary>
    /// Creates a compute pipeline object.
    /// </summary>
    /// <param name="device">The GPU device.</param>
    /// <param name="createInfo">The compute pipeline creation info.</param>
    /// <returns>A new compute pipeline.</returns>
    public static GpuComputePipeline Create(GpuDevice device, GpuComputePipelineCreateInfo createInfo)
    {
        fixed (byte* codePtr = createInfo.Code)
        fixed (byte* entrypointPtr = createInfo.EntrypointUtf8)
        {
            var nativeInfo = new SDL_GPUComputePipelineCreateInfo
            {
                code_size = (nuint)createInfo.Code.Length,
                code = codePtr,
                entrypoint = entrypointPtr,
                format = (SDL_GPUShaderFormat)createInfo.Format,
                num_samplers = createInfo.NumSamplers,
                num_readonly_storage_textures = createInfo.NumReadonlyStorageTextures,
                num_readonly_storage_buffers = createInfo.NumReadonlyStorageBuffers,
                num_readwrite_storage_textures = createInfo.NumReadwriteStorageTextures,
                num_readwrite_storage_buffers = createInfo.NumReadwriteStorageBuffers,
                num_uniform_buffers = createInfo.NumUniformBuffers,
                threadcount_x = createInfo.ThreadCountX,
                threadcount_y = createInfo.ThreadCountY,
                threadcount_z = createInfo.ThreadCountZ,
                props = 0
            };
            return new GpuComputePipeline(CheckErrorPointer(SDL_CreateGPUComputePipeline(device.Handle, &nativeInfo)), device);
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
            SDL_ReleaseGPUComputePipeline(_device.Handle, Handle);
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
/// Describes the parameters for creating a GPU compute pipeline.
/// </summary>
public struct GpuComputePipelineCreateInfo
{
    /// <summary>
    /// The compute shader code.
    /// </summary>
    public byte[] Code { get; set; }

    /// <summary>
    /// The entry point function name as UTF-8 bytes (including null terminator).
    /// </summary>
    public byte[] EntrypointUtf8 { get; set; }

    /// <summary>
    /// The format of the compute shader code.
    /// </summary>
    public GpuShaderFormat Format { get; set; }

    /// <summary>
    /// The number of samplers defined in the shader.
    /// </summary>
    public uint NumSamplers { get; set; }

    /// <summary>
    /// The number of readonly storage textures defined in the shader.
    /// </summary>
    public uint NumReadonlyStorageTextures { get; set; }

    /// <summary>
    /// The number of readonly storage buffers defined in the shader.
    /// </summary>
    public uint NumReadonlyStorageBuffers { get; set; }

    /// <summary>
    /// The number of read-write storage textures defined in the shader.
    /// </summary>
    public uint NumReadwriteStorageTextures { get; set; }

    /// <summary>
    /// The number of read-write storage buffers defined in the shader.
    /// </summary>
    public uint NumReadwriteStorageBuffers { get; set; }

    /// <summary>
    /// The number of uniform buffers defined in the shader.
    /// </summary>
    public uint NumUniformBuffers { get; set; }

    /// <summary>
    /// The number of threads in the X dimension.
    /// </summary>
    public uint ThreadCountX { get; set; }

    /// <summary>
    /// The number of threads in the Y dimension.
    /// </summary>
    public uint ThreadCountY { get; set; }

    /// <summary>
    /// The number of threads in the Z dimension.
    /// </summary>
    public uint ThreadCountZ { get; set; }

    /// <summary>
    /// Creates a compute pipeline create info with an entrypoint string.
    /// </summary>
    /// <param name="code">The compute shader code.</param>
    /// <param name="entrypoint">The entry point function name.</param>
    /// <param name="format">The shader format.</param>
    /// <param name="threadCountX">The number of threads in the X dimension.</param>
    /// <param name="threadCountY">The number of threads in the Y dimension.</param>
    /// <param name="threadCountZ">The number of threads in the Z dimension.</param>
    /// <returns>A new compute pipeline create info.</returns>
    public static GpuComputePipelineCreateInfo FromEntrypoint(
        byte[] code,
        string entrypoint,
        GpuShaderFormat format,
        uint threadCountX,
        uint threadCountY,
        uint threadCountZ)
    {
        return new GpuComputePipelineCreateInfo
        {
            Code = code,
            EntrypointUtf8 = System.Text.Encoding.UTF8.GetBytes(entrypoint + "\0"),
            Format = format,
            ThreadCountX = threadCountX,
            ThreadCountY = threadCountY,
            ThreadCountZ = threadCountZ
        };
    }
}
