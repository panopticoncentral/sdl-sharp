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
    internal SDL_GPUComputePass* Handle { get; }

    internal GpuComputePass(SDL_GPUComputePass* handle) { Handle = handle; }

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
    /// <param name="bindings">Pointer to the texture-sampler binding array.</param>
    /// <param name="numBindings">Number of bindings.</param>
    public void BindSamplers(uint firstSlot, SDL_GPUTextureSamplerBinding* bindings, uint numBindings) =>
        SDL_BindGPUComputeSamplers(Handle, firstSlot, bindings, numBindings);

    /// <summary>
    /// Binds storage textures to compute shader storage texture slots.
    /// </summary>
    /// <param name="firstSlot">The first storage texture slot to bind to.</param>
    /// <param name="textures">Pointer to the texture pointer array.</param>
    /// <param name="count">Number of textures to bind.</param>
    public void BindStorageTextures(uint firstSlot, SDL_GPUTexture** textures, uint count) =>
        SDL_BindGPUComputeStorageTextures(Handle, firstSlot, textures, count);

    /// <summary>
    /// Binds storage buffers to compute shader storage buffer slots.
    /// </summary>
    /// <param name="firstSlot">The first storage buffer slot to bind to.</param>
    /// <param name="buffers">Pointer to the buffer pointer array.</param>
    /// <param name="count">Number of buffers to bind.</param>
    public void BindStorageBuffers(uint firstSlot, SDL_GPUBuffer** buffers, uint count) =>
        SDL_BindGPUComputeStorageBuffers(Handle, firstSlot, buffers, count);

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
    public void End() => SDL_EndGPUComputePass(Handle);
}
