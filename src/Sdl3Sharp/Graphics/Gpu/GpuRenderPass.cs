using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents a GPU render pass for rendering operations.
/// </summary>
public sealed unsafe class GpuRenderPass
{
    private readonly GpuCommandBuffer _commandBuffer;

    /// <summary>
    /// Gets the underlying SDL_GPURenderPass pointer.
    /// </summary>
    public SDL_GPURenderPass* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPURenderPass pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPURenderPass pointer.</param>
    /// <param name="commandBuffer">The command buffer that created this render pass.</param>
    internal GpuRenderPass(SDL_GPURenderPass* handle, GpuCommandBuffer commandBuffer)
    {
        Handle = handle;
        _commandBuffer = commandBuffer;
    }

    /// <summary>
    /// Binds a graphics pipeline on this render pass.
    /// </summary>
    /// <param name="pipeline">The graphics pipeline to bind.</param>
    public void BindGraphicsPipeline(GpuGraphicsPipeline pipeline)
    {
        SDL_BindGPUGraphicsPipeline(Handle, pipeline.Handle);
    }

    /// <summary>
    /// Sets the current viewport state.
    /// </summary>
    /// <param name="viewport">The viewport to set.</param>
    public void SetViewport(GpuViewport viewport)
    {
        SDL_GPUViewport nativeViewport = viewport.ToNative();
        SDL_SetGPUViewport(Handle, &nativeViewport);
    }

    /// <summary>
    /// Sets the current scissor state.
    /// </summary>
    /// <param name="scissor">The scissor rectangle.</param>
    public void SetScissor(Rectangle scissor)
    {
        SDL_SetGPUScissor(Handle, &scissor.Native);
    }

    /// <summary>
    /// Sets the current blend constants.
    /// </summary>
    /// <param name="blendConstants">The blend constant color.</param>
    public void SetBlendConstants(ColorF blendConstants)
    {
        SDL_SetGPUBlendConstants(Handle, blendConstants.ToNative());
    }

    /// <summary>
    /// Sets the current stencil reference value.
    /// </summary>
    /// <param name="reference">The stencil reference value.</param>
    public void SetStencilReference(byte reference)
    {
        SDL_SetGPUStencilReference(Handle, reference);
    }

    /// <summary>
    /// Binds vertex buffers for use with subsequent draw calls.
    /// </summary>
    /// <param name="firstSlot">The vertex buffer slot to begin binding from.</param>
    /// <param name="bindings">The buffer bindings.</param>
    public void BindVertexBuffers(uint firstSlot, GpuBufferBinding[] bindings)
    {
        var nativeBindings = new SDL_GPUBufferBinding[bindings.Length];
        for (var i = 0; i < bindings.Length; i++)
        {
            nativeBindings[i] = bindings[i].ToNative();
        }

        fixed (SDL_GPUBufferBinding* bindingsPtr = nativeBindings)
        {
            SDL_BindGPUVertexBuffers(Handle, firstSlot, bindingsPtr, (uint)bindings.Length);
        }
    }

    /// <summary>
    /// Binds an index buffer for use with subsequent draw calls.
    /// </summary>
    /// <param name="binding">The buffer binding.</param>
    /// <param name="indexElementSize">Whether the index values are 16- or 32-bit.</param>
    public void BindIndexBuffer(GpuBufferBinding binding, GpuIndexElementSize indexElementSize)
    {
        SDL_GPUBufferBinding nativeBinding = binding.ToNative();
        SDL_BindGPUIndexBuffer(Handle, &nativeBinding, (SDL_GPUIndexElementSize)indexElementSize);
    }

    /// <summary>
    /// Binds texture-sampler pairs for use on the vertex shader.
    /// </summary>
    /// <param name="firstSlot">The vertex sampler slot to begin binding from.</param>
    /// <param name="bindings">The texture-sampler bindings.</param>
    public void BindVertexSamplers(uint firstSlot, GpuTextureSamplerBinding[] bindings)
    {
        var nativeBindings = new SDL_GPUTextureSamplerBinding[bindings.Length];
        for (var i = 0; i < bindings.Length; i++)
        {
            nativeBindings[i] = bindings[i].ToNative();
        }

        fixed (SDL_GPUTextureSamplerBinding* bindingsPtr = nativeBindings)
        {
            SDL_BindGPUVertexSamplers(Handle, firstSlot, bindingsPtr, (uint)bindings.Length);
        }
    }

    /// <summary>
    /// Binds storage textures for use on the vertex shader.
    /// </summary>
    /// <param name="firstSlot">The vertex storage texture slot to begin binding from.</param>
    /// <param name="textures">The storage textures.</param>
    public void BindVertexStorageTextures(uint firstSlot, GpuTexture[] textures)
    {
        var nativeTextures = new SDL_GPUTexture*[textures.Length];
        for (var i = 0; i < textures.Length; i++)
        {
            nativeTextures[i] = textures[i].Handle;
        }

        fixed (SDL_GPUTexture** texturesPtr = nativeTextures)
        {
            SDL_BindGPUVertexStorageTextures(Handle, firstSlot, texturesPtr, (uint)textures.Length);
        }
    }

    /// <summary>
    /// Binds storage buffers for use on the vertex shader.
    /// </summary>
    /// <param name="firstSlot">The vertex storage buffer slot to begin binding from.</param>
    /// <param name="buffers">The storage buffers.</param>
    public void BindVertexStorageBuffers(uint firstSlot, GpuBuffer[] buffers)
    {
        var nativeBuffers = new SDL_GPUBuffer*[buffers.Length];
        for (var i = 0; i < buffers.Length; i++)
        {
            nativeBuffers[i] = buffers[i].Handle;
        }

        fixed (SDL_GPUBuffer** buffersPtr = nativeBuffers)
        {
            SDL_BindGPUVertexStorageBuffers(Handle, firstSlot, buffersPtr, (uint)buffers.Length);
        }
    }

    /// <summary>
    /// Binds texture-sampler pairs for use on the fragment shader.
    /// </summary>
    /// <param name="firstSlot">The fragment sampler slot to begin binding from.</param>
    /// <param name="bindings">The texture-sampler bindings.</param>
    public void BindFragmentSamplers(uint firstSlot, GpuTextureSamplerBinding[] bindings)
    {
        var nativeBindings = new SDL_GPUTextureSamplerBinding[bindings.Length];
        for (var i = 0; i < bindings.Length; i++)
        {
            nativeBindings[i] = bindings[i].ToNative();
        }

        fixed (SDL_GPUTextureSamplerBinding* bindingsPtr = nativeBindings)
        {
            SDL_BindGPUFragmentSamplers(Handle, firstSlot, bindingsPtr, (uint)bindings.Length);
        }
    }

    /// <summary>
    /// Binds storage textures for use on the fragment shader.
    /// </summary>
    /// <param name="firstSlot">The fragment storage texture slot to begin binding from.</param>
    /// <param name="textures">The storage textures.</param>
    public void BindFragmentStorageTextures(uint firstSlot, GpuTexture[] textures)
    {
        var nativeTextures = new SDL_GPUTexture*[textures.Length];
        for (var i = 0; i < textures.Length; i++)
        {
            nativeTextures[i] = textures[i].Handle;
        }

        fixed (SDL_GPUTexture** texturesPtr = nativeTextures)
        {
            SDL_BindGPUFragmentStorageTextures(Handle, firstSlot, texturesPtr, (uint)textures.Length);
        }
    }

    /// <summary>
    /// Binds storage buffers for use on the fragment shader.
    /// </summary>
    /// <param name="firstSlot">The fragment storage buffer slot to begin binding from.</param>
    /// <param name="buffers">The storage buffers.</param>
    public void BindFragmentStorageBuffers(uint firstSlot, GpuBuffer[] buffers)
    {
        var nativeBuffers = new SDL_GPUBuffer*[buffers.Length];
        for (var i = 0; i < buffers.Length; i++)
        {
            nativeBuffers[i] = buffers[i].Handle;
        }

        fixed (SDL_GPUBuffer** buffersPtr = nativeBuffers)
        {
            SDL_BindGPUFragmentStorageBuffers(Handle, firstSlot, buffersPtr, (uint)buffers.Length);
        }
    }

    /// <summary>
    /// Draws data using bound graphics state with an index buffer and instancing enabled.
    /// </summary>
    /// <param name="numIndices">The number of indices to draw per instance.</param>
    /// <param name="numInstances">The number of instances to draw.</param>
    /// <param name="firstIndex">The starting index within the index buffer.</param>
    /// <param name="vertexOffset">Value added to vertex index before indexing into the vertex buffer.</param>
    /// <param name="firstInstance">The ID of the first instance to draw.</param>
    public void DrawIndexedPrimitives(uint numIndices, uint numInstances, uint firstIndex, int vertexOffset, uint firstInstance)
    {
        SDL_DrawGPUIndexedPrimitives(Handle, numIndices, numInstances, firstIndex, vertexOffset, firstInstance);
    }

    /// <summary>
    /// Draws data using bound graphics state.
    /// </summary>
    /// <param name="numVertices">The number of vertices to draw.</param>
    /// <param name="numInstances">The number of instances that will be drawn.</param>
    /// <param name="firstVertex">The index of the first vertex to draw.</param>
    /// <param name="firstInstance">The ID of the first instance to draw.</param>
    public void DrawPrimitives(uint numVertices, uint numInstances, uint firstVertex, uint firstInstance)
    {
        SDL_DrawGPUPrimitives(Handle, numVertices, numInstances, firstVertex, firstInstance);
    }

    /// <summary>
    /// Draws data using bound graphics state and with draw parameters set from a buffer.
    /// </summary>
    /// <param name="buffer">A buffer containing draw parameters.</param>
    /// <param name="offset">The offset to start reading from the draw buffer.</param>
    /// <param name="drawCount">The number of draw parameter sets that should be read from the draw buffer.</param>
    public void DrawPrimitivesIndirect(GpuBuffer buffer, uint offset, uint drawCount)
    {
        SDL_DrawGPUPrimitivesIndirect(Handle, buffer.Handle, offset, drawCount);
    }

    /// <summary>
    /// Draws data using bound graphics state with an index buffer enabled and with draw parameters set from a buffer.
    /// </summary>
    /// <param name="buffer">A buffer containing draw parameters.</param>
    /// <param name="offset">The offset to start reading from the draw buffer.</param>
    /// <param name="drawCount">The number of draw parameter sets that should be read from the draw buffer.</param>
    public void DrawIndexedPrimitivesIndirect(GpuBuffer buffer, uint offset, uint drawCount)
    {
        SDL_DrawGPUIndexedPrimitivesIndirect(Handle, buffer.Handle, offset, drawCount);
    }

    /// <summary>
    /// Ends the render pass.
    /// </summary>
    public void End()
    {
        SDL_EndGPURenderPass(Handle);
        Handle = null;
    }
}
