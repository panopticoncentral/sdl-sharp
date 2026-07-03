using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU compute pass (SDL_GPUComputePass).
/// Compute passes record compute dispatch commands and are ended by calling <see cref="End"/>.
/// </summary>
public sealed unsafe class GpuComputePass
{
    /// <summary>
    /// The underlying native SDL_GPUComputePass pointer.
    /// </summary>
    internal SDL_GPUComputePass* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    private SDL_GPUComputePass* _handle;

    internal GpuComputePass(SDL_GPUComputePass* handle) { _handle = handle; }

    /// <summary>
    /// Binds a compute pipeline for subsequent dispatch commands.
    /// </summary>
    /// <param name="pipeline">The compute pipeline to bind.</param>
    public void BindPipeline(GpuComputePipeline pipeline) =>
        SDL_BindGPUComputePipeline(Handle, pipeline.Handle);

    /// <summary>
    /// Binds texture-sampler pairs to compute shader sampler slots.
    /// </summary>
    /// <param name="firstSlot">The first sampler slot to bind to.</param>
    /// <param name="bindings">The texture-sampler bindings to bind.</param>
    public void BindSamplers(uint firstSlot, ReadOnlySpan<GpuTextureSamplerBinding> bindings)
    {
        Span<SDL_GPUTextureSamplerBinding> native = stackalloc SDL_GPUTextureSamplerBinding[bindings.Length];
        for (var i = 0; i < bindings.Length; i++) native[i] = bindings[i].ToNative();
        fixed (SDL_GPUTextureSamplerBinding* p = native)
            SDL_BindGPUComputeSamplers(Handle, firstSlot, p, (uint)bindings.Length);
    }

    /// <summary>
    /// Binds storage textures to compute shader storage texture slots.
    /// </summary>
    /// <param name="firstSlot">The first storage texture slot to bind to.</param>
    /// <param name="textures">The storage textures to bind.</param>
    public void BindStorageTextures(uint firstSlot, ReadOnlySpan<GpuTexture> textures)
    {
        var pointers = stackalloc SDL_GPUTexture*[textures.Length];
        for (var i = 0; i < textures.Length; i++) pointers[i] = textures[i].Handle;
        SDL_BindGPUComputeStorageTextures(Handle, firstSlot, pointers, (uint)textures.Length);
    }

    /// <summary>
    /// Binds storage buffers to compute shader storage buffer slots.
    /// </summary>
    /// <param name="firstSlot">The first storage buffer slot to bind to.</param>
    /// <param name="buffers">The storage buffers to bind.</param>
    public void BindStorageBuffers(uint firstSlot, ReadOnlySpan<GpuBuffer> buffers)
    {
        var pointers = stackalloc SDL_GPUBuffer*[buffers.Length];
        for (var i = 0; i < buffers.Length; i++) pointers[i] = buffers[i].Handle;
        SDL_BindGPUComputeStorageBuffers(Handle, firstSlot, pointers, (uint)buffers.Length);
    }

    /// <summary>
    /// Dispatches compute work groups.
    /// </summary>
    /// <param name="groupCountX">Number of work groups in the X dimension.</param>
    /// <param name="groupCountY">Number of work groups in the Y dimension.</param>
    /// <param name="groupCountZ">Number of work groups in the Z dimension.</param>
    public void Dispatch(uint groupCountX, uint groupCountY, uint groupCountZ) =>
        SDL_DispatchGPUCompute(Handle, groupCountX, groupCountY, groupCountZ);

    /// <summary>
    /// Dispatches compute work using indirect arguments from a buffer.
    /// </summary>
    /// <param name="buffer">The buffer containing indirect dispatch arguments.</param>
    /// <param name="offset">Byte offset into the buffer.</param>
    public void DispatchIndirect(GpuBuffer buffer, uint offset) =>
        SDL_DispatchGPUComputeIndirect(Handle, buffer.Handle, offset);

    /// <summary>
    /// Ends the compute pass.
    /// </summary>
    public void End()
    {
        var handle = Handle;
        _handle = null;
        SDL_EndGPUComputePass(handle);
    }
}
