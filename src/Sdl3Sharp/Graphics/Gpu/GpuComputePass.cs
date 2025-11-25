using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents a GPU compute pass for compute shader operations.
/// </summary>
public sealed unsafe class GpuComputePass
{
    private readonly GpuCommandBuffer _commandBuffer;

    /// <summary>
    /// Gets the underlying SDL_GPUComputePass pointer.
    /// </summary>
    public SDL_GPUComputePass* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPUComputePass pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPUComputePass pointer.</param>
    /// <param name="commandBuffer">The command buffer that created this compute pass.</param>
    internal GpuComputePass(SDL_GPUComputePass* handle, GpuCommandBuffer commandBuffer)
    {
        Handle = handle;
        _commandBuffer = commandBuffer;
    }

    /// <summary>
    /// Binds a compute pipeline on this compute pass.
    /// </summary>
    /// <param name="pipeline">The compute pipeline to bind.</param>
    public void BindComputePipeline(GpuComputePipeline pipeline)
    {
        SDL_BindGPUComputePipeline(Handle, pipeline.Handle);
    }

    /// <summary>
    /// Binds texture-sampler pairs for use on the compute shader.
    /// </summary>
    /// <param name="firstSlot">The compute sampler slot to begin binding from.</param>
    /// <param name="bindings">The texture-sampler bindings.</param>
    public void BindSamplers(uint firstSlot, GpuTextureSamplerBinding[] bindings)
    {
        var nativeBindings = new SDL_GPUTextureSamplerBinding[bindings.Length];
        for (var i = 0; i < bindings.Length; i++)
        {
            nativeBindings[i] = bindings[i].ToNative();
        }

        fixed (SDL_GPUTextureSamplerBinding* bindingsPtr = nativeBindings)
        {
            SDL_BindGPUComputeSamplers(Handle, firstSlot, bindingsPtr, (uint)bindings.Length);
        }
    }

    /// <summary>
    /// Binds storage textures as readonly for use on the compute pipeline.
    /// </summary>
    /// <param name="firstSlot">The compute storage texture slot to begin binding from.</param>
    /// <param name="textures">The storage textures.</param>
    public void BindStorageTextures(uint firstSlot, GpuTexture[] textures)
    {
        var nativeTextures = new SDL_GPUTexture*[textures.Length];
        for (var i = 0; i < textures.Length; i++)
        {
            nativeTextures[i] = textures[i].Handle;
        }

        fixed (SDL_GPUTexture** texturesPtr = nativeTextures)
        {
            SDL_BindGPUComputeStorageTextures(Handle, firstSlot, texturesPtr, (uint)textures.Length);
        }
    }

    /// <summary>
    /// Binds storage buffers as readonly for use on the compute pipeline.
    /// </summary>
    /// <param name="firstSlot">The compute storage buffer slot to begin binding from.</param>
    /// <param name="buffers">The storage buffers.</param>
    public void BindStorageBuffers(uint firstSlot, GpuBuffer[] buffers)
    {
        var nativeBuffers = new SDL_GPUBuffer*[buffers.Length];
        for (var i = 0; i < buffers.Length; i++)
        {
            nativeBuffers[i] = buffers[i].Handle;
        }

        fixed (SDL_GPUBuffer** buffersPtr = nativeBuffers)
        {
            SDL_BindGPUComputeStorageBuffers(Handle, firstSlot, buffersPtr, (uint)buffers.Length);
        }
    }

    /// <summary>
    /// Dispatches compute work.
    /// </summary>
    /// <param name="groupCountX">Number of local workgroups to dispatch in the X dimension.</param>
    /// <param name="groupCountY">Number of local workgroups to dispatch in the Y dimension.</param>
    /// <param name="groupCountZ">Number of local workgroups to dispatch in the Z dimension.</param>
    public void Dispatch(uint groupCountX, uint groupCountY, uint groupCountZ)
    {
        SDL_DispatchGPUCompute(Handle, groupCountX, groupCountY, groupCountZ);
    }

    /// <summary>
    /// Dispatches compute work with parameters set from a buffer.
    /// </summary>
    /// <param name="buffer">A buffer containing dispatch parameters.</param>
    /// <param name="offset">The offset to start reading from the dispatch buffer.</param>
    public void DispatchIndirect(GpuBuffer buffer, uint offset)
    {
        SDL_DispatchGPUComputeIndirect(Handle, buffer.Handle, offset);
    }

    /// <summary>
    /// Ends the compute pass.
    /// </summary>
    public void End()
    {
        SDL_EndGPUComputePass(Handle);
        Handle = null;
    }
}
