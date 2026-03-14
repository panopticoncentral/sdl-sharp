using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU render pass (SDL_GPURenderPass).
/// Render passes record draw commands and are ended by calling <see cref="End"/>.
/// </summary>
public sealed unsafe class GpuRenderPass
{
    /// <summary>
    /// The underlying native SDL_GPURenderPass pointer.
    /// </summary>
    internal SDL_GPURenderPass* Handle { get; }

    internal GpuRenderPass(SDL_GPURenderPass* handle) { Handle = handle; }

    /// <summary>
    /// Binds a graphics pipeline for subsequent draw commands.
    /// </summary>
    /// <param name="pipeline">The graphics pipeline to bind.</param>
    public void BindGraphicsPipeline(GpuGraphicsPipeline pipeline) =>
        SDL_BindGPUGraphicsPipeline(Handle, pipeline.Handle);

    /// <summary>
    /// Sets the viewport for the render pass.
    /// </summary>
    /// <param name="viewport">The viewport parameters.</param>
    public void SetViewport(in SDL_GPUViewport viewport)
    {
        fixed (SDL_GPUViewport* p = &viewport)
            SDL_SetGPUViewport(Handle, p);
    }

    /// <summary>
    /// Sets the scissor rectangle for the render pass.
    /// </summary>
    /// <param name="scissor">The scissor rectangle.</param>
    public void SetScissor(in SDL_Rect scissor)
    {
        fixed (SDL_Rect* p = &scissor)
            SDL_SetGPUScissor(Handle, p);
    }

    /// <summary>
    /// Sets the blend constant color values.
    /// </summary>
    /// <param name="constants">The blend constants as an RGBA float color.</param>
    public void SetBlendConstants(SDL_FColor constants) =>
        SDL_SetGPUBlendConstants(Handle, constants);

    /// <summary>
    /// Sets the stencil reference value.
    /// </summary>
    /// <param name="reference">The stencil reference value.</param>
    public void SetStencilReference(byte reference) =>
        SDL_SetGPUStencilReference(Handle, reference);

    /// <summary>
    /// Binds vertex buffers to the render pass.
    /// </summary>
    /// <param name="firstSlot">The first vertex buffer slot to bind to.</param>
    /// <param name="bindings">Pointer to the buffer binding array.</param>
    /// <param name="numBindings">Number of buffer bindings.</param>
    public void BindVertexBuffers(uint firstSlot, SDL_GPUBufferBinding* bindings, uint numBindings) =>
        SDL_BindGPUVertexBuffers(Handle, firstSlot, bindings, numBindings);

    /// <summary>
    /// Binds an index buffer to the render pass.
    /// </summary>
    /// <param name="binding">The index buffer binding.</param>
    /// <param name="elementSize">The size of each index element.</param>
    public void BindIndexBuffer(in SDL_GPUBufferBinding binding, SDL_GPUIndexElementSize elementSize)
    {
        fixed (SDL_GPUBufferBinding* p = &binding)
            SDL_BindGPUIndexBuffer(Handle, p, elementSize);
    }

    /// <summary>
    /// Binds texture-sampler pairs to vertex shader sampler slots.
    /// </summary>
    /// <param name="firstSlot">The first sampler slot to bind to.</param>
    /// <param name="bindings">Pointer to the texture-sampler binding array.</param>
    /// <param name="numBindings">Number of bindings.</param>
    public void BindVertexSamplers(uint firstSlot, SDL_GPUTextureSamplerBinding* bindings, uint numBindings) =>
        SDL_BindGPUVertexSamplers(Handle, firstSlot, bindings, numBindings);

    /// <summary>
    /// Binds texture-sampler pairs to fragment shader sampler slots.
    /// </summary>
    /// <param name="firstSlot">The first sampler slot to bind to.</param>
    /// <param name="bindings">Pointer to the texture-sampler binding array.</param>
    /// <param name="numBindings">Number of bindings.</param>
    public void BindFragmentSamplers(uint firstSlot, SDL_GPUTextureSamplerBinding* bindings, uint numBindings) =>
        SDL_BindGPUFragmentSamplers(Handle, firstSlot, bindings, numBindings);

    /// <summary>
    /// Binds storage textures to vertex shader storage texture slots.
    /// </summary>
    /// <param name="firstSlot">The first storage texture slot to bind to.</param>
    /// <param name="textures">Pointer to the texture pointer array.</param>
    /// <param name="count">Number of textures to bind.</param>
    public void BindVertexStorageTextures(uint firstSlot, SDL_GPUTexture** textures, uint count) =>
        SDL_BindGPUVertexStorageTextures(Handle, firstSlot, textures, count);

    /// <summary>
    /// Binds storage textures to fragment shader storage texture slots.
    /// </summary>
    /// <param name="firstSlot">The first storage texture slot to bind to.</param>
    /// <param name="textures">Pointer to the texture pointer array.</param>
    /// <param name="count">Number of textures to bind.</param>
    public void BindFragmentStorageTextures(uint firstSlot, SDL_GPUTexture** textures, uint count) =>
        SDL_BindGPUFragmentStorageTextures(Handle, firstSlot, textures, count);

    /// <summary>
    /// Binds storage buffers to vertex shader storage buffer slots.
    /// </summary>
    /// <param name="firstSlot">The first storage buffer slot to bind to.</param>
    /// <param name="buffers">Pointer to the buffer pointer array.</param>
    /// <param name="count">Number of buffers to bind.</param>
    public void BindVertexStorageBuffers(uint firstSlot, SDL_GPUBuffer** buffers, uint count) =>
        SDL_BindGPUVertexStorageBuffers(Handle, firstSlot, buffers, count);

    /// <summary>
    /// Binds storage buffers to fragment shader storage buffer slots.
    /// </summary>
    /// <param name="firstSlot">The first storage buffer slot to bind to.</param>
    /// <param name="buffers">Pointer to the buffer pointer array.</param>
    /// <param name="count">Number of buffers to bind.</param>
    public void BindFragmentStorageBuffers(uint firstSlot, SDL_GPUBuffer** buffers, uint count) =>
        SDL_BindGPUFragmentStorageBuffers(Handle, firstSlot, buffers, count);

    /// <summary>
    /// Draws non-indexed primitives.
    /// </summary>
    /// <param name="numVertices">Number of vertices to draw.</param>
    /// <param name="numInstances">Number of instances to draw.</param>
    /// <param name="firstVertex">Index of the first vertex.</param>
    /// <param name="firstInstance">Index of the first instance.</param>
    public void DrawPrimitives(uint numVertices, uint numInstances = 1, uint firstVertex = 0, uint firstInstance = 0) =>
        SDL_DrawGPUPrimitives(Handle, numVertices, numInstances, firstVertex, firstInstance);

    /// <summary>
    /// Draws indexed primitives.
    /// </summary>
    /// <param name="numIndices">Number of indices to draw.</param>
    /// <param name="numInstances">Number of instances to draw.</param>
    /// <param name="firstIndex">Index of the first index in the index buffer.</param>
    /// <param name="vertexOffset">Value added to each index before fetching the vertex.</param>
    /// <param name="firstInstance">Index of the first instance.</param>
    public void DrawIndexedPrimitives(uint numIndices, uint numInstances = 1, uint firstIndex = 0, int vertexOffset = 0, uint firstInstance = 0) =>
        SDL_DrawGPUIndexedPrimitives(Handle, numIndices, numInstances, firstIndex, vertexOffset, firstInstance);

    /// <summary>
    /// Draws primitives using indirect draw arguments from a buffer.
    /// </summary>
    /// <param name="buffer">The buffer containing indirect draw arguments.</param>
    /// <param name="offset">Byte offset into the buffer.</param>
    /// <param name="drawCount">Number of draw commands to execute.</param>
    public void DrawPrimitivesIndirect(GpuBuffer buffer, uint offset, uint drawCount) =>
        SDL_DrawGPUPrimitivesIndirect(Handle, buffer.Handle, offset, drawCount);

    /// <summary>
    /// Draws indexed primitives using indirect draw arguments from a buffer.
    /// </summary>
    /// <param name="buffer">The buffer containing indirect draw arguments.</param>
    /// <param name="offset">Byte offset into the buffer.</param>
    /// <param name="drawCount">Number of draw commands to execute.</param>
    public void DrawIndexedPrimitivesIndirect(GpuBuffer buffer, uint offset, uint drawCount) =>
        SDL_DrawGPUIndexedPrimitivesIndirect(Handle, buffer.Handle, offset, drawCount);

    /// <summary>
    /// Ends the render pass.
    /// </summary>
    public void End() => SDL_EndGPURenderPass(Handle);
}
