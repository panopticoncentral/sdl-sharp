using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU command buffer (SDL_GPUCommandBuffer).
/// Command buffers record GPU commands that are submitted for execution.
/// </summary>
public sealed unsafe class GpuCommandBuffer
{
    private readonly GpuDevice _device;

    /// <summary>
    /// The underlying native SDL_GPUCommandBuffer pointer.
    /// </summary>
    internal SDL_GPUCommandBuffer* Handle { get; }

    internal GpuCommandBuffer(GpuDevice device, SDL_GPUCommandBuffer* handle)
    {
        _device = device;
        Handle = handle;
    }

    /// <summary>
    /// Begins a render pass with the specified color and optional depth/stencil targets.
    /// </summary>
    /// <param name="colorTargets">Pointer to the color target info array.</param>
    /// <param name="numColorTargets">Number of color targets.</param>
    /// <param name="depthStencilTarget">Optional depth/stencil target info, or null.</param>
    /// <returns>A new render pass.</returns>
    public GpuRenderPass BeginRenderPass(SDL_GPUColorTargetInfo* colorTargets, uint numColorTargets, SDL_GPUDepthStencilTargetInfo* depthStencilTarget = null) =>
        new(Check(SDL_BeginGPURenderPass(Handle, colorTargets, numColorTargets, depthStencilTarget)));

    /// <summary>
    /// Begins a compute pass with the specified storage texture and buffer bindings.
    /// </summary>
    /// <param name="storageTextures">Pointer to storage texture read-write bindings, or null.</param>
    /// <param name="numStorageTextures">Number of storage texture bindings.</param>
    /// <param name="storageBuffers">Pointer to storage buffer read-write bindings, or null.</param>
    /// <param name="numStorageBuffers">Number of storage buffer bindings.</param>
    /// <returns>A new compute pass.</returns>
    public GpuComputePass BeginComputePass(SDL_GPUStorageTextureReadWriteBinding* storageTextures, uint numStorageTextures, SDL_GPUStorageBufferReadWriteBinding* storageBuffers, uint numStorageBuffers) =>
        new(Check(SDL_BeginGPUComputePass(Handle, storageTextures, numStorageTextures, storageBuffers, numStorageBuffers)));

    /// <summary>
    /// Begins a copy pass for transferring data between CPU and GPU resources.
    /// </summary>
    /// <returns>A new copy pass.</returns>
    public GpuCopyPass BeginCopyPass() => new(Check(SDL_BeginGPUCopyPass(Handle)));

    /// <summary>
    /// Pushes uniform data for a vertex shader slot.
    /// </summary>
    /// <param name="slot">The uniform buffer slot index.</param>
    /// <param name="data">Pointer to the uniform data.</param>
    /// <param name="length">Size of the data in bytes.</param>
    public void PushVertexUniformData(uint slot, void* data, uint length) =>
        SDL_PushGPUVertexUniformData(Handle, slot, data, length);

    /// <summary>
    /// Pushes uniform data for a fragment shader slot.
    /// </summary>
    /// <param name="slot">The uniform buffer slot index.</param>
    /// <param name="data">Pointer to the uniform data.</param>
    /// <param name="length">Size of the data in bytes.</param>
    public void PushFragmentUniformData(uint slot, void* data, uint length) =>
        SDL_PushGPUFragmentUniformData(Handle, slot, data, length);

    /// <summary>
    /// Pushes uniform data for a compute shader slot.
    /// </summary>
    /// <param name="slot">The uniform buffer slot index.</param>
    /// <param name="data">Pointer to the uniform data.</param>
    /// <param name="length">Size of the data in bytes.</param>
    public void PushComputeUniformData(uint slot, void* data, uint length) =>
        SDL_PushGPUComputeUniformData(Handle, slot, data, length);

    /// <summary>
    /// Generates mipmaps for the specified texture.
    /// </summary>
    /// <param name="texture">The texture to generate mipmaps for.</param>
    public void GenerateMipmaps(GpuTexture texture) =>
        SDL_GenerateMipmapsForGPUTexture(Handle, texture.Handle);

    /// <summary>
    /// Blits (copies with potential scaling/filtering) between texture regions.
    /// </summary>
    /// <param name="info">The blit operation parameters.</param>
    public void Blit(in SDL_GPUBlitInfo info)
    {
        fixed (SDL_GPUBlitInfo* p = &info)
            SDL_BlitGPUTexture(Handle, p);
    }

    /// <summary>
    /// Acquires a swapchain texture for rendering to a window.
    /// </summary>
    /// <param name="window">The window to acquire the swapchain texture from.</param>
    /// <param name="texture">Receives the swapchain texture pointer, or null if not ready.</param>
    /// <param name="width">Receives the swapchain texture width.</param>
    /// <param name="height">Receives the swapchain texture height.</param>
    /// <returns>True if a texture was acquired; false if not ready yet (texture will be null).</returns>
    public bool AcquireSwapchainTexture(Window window, out SDL_GPUTexture* texture, out uint width, out uint height)
    {
        SDL_GPUTexture* tex;
        uint w, h;
        var result = SDL_AcquireGPUSwapchainTexture(Handle, window.Handle, &tex, &w, &h);
        texture = tex;
        width = w;
        height = h;
        if (!result) throw new SdlException();
        return tex != null;
    }

    /// <summary>
    /// Waits for and acquires a swapchain texture for rendering to a window.
    /// </summary>
    /// <param name="window">The window to acquire the swapchain texture from.</param>
    /// <param name="texture">Receives the swapchain texture pointer, or null if not ready.</param>
    /// <param name="width">Receives the swapchain texture width.</param>
    /// <param name="height">Receives the swapchain texture height.</param>
    /// <returns>True if a texture was acquired; false if not ready yet (texture will be null).</returns>
    public bool WaitAndAcquireSwapchainTexture(Window window, out SDL_GPUTexture* texture, out uint width, out uint height)
    {
        SDL_GPUTexture* tex;
        uint w, h;
        var result = SDL_WaitAndAcquireGPUSwapchainTexture(Handle, window.Handle, &tex, &w, &h);
        texture = tex;
        width = w;
        height = h;
        if (!result) throw new SdlException();
        return tex != null;
    }

    /// <summary>
    /// Submits the command buffer for execution by the GPU.
    /// </summary>
    public void Submit() => Check(SDL_SubmitGPUCommandBuffer(Handle));

    /// <summary>
    /// Submits the command buffer and acquires a fence that is signaled when execution completes.
    /// </summary>
    /// <returns>A fence that will be signaled when the submitted work completes.</returns>
    public GpuFence SubmitAndAcquireFence() => new(_device, Check(SDL_SubmitGPUCommandBufferAndAcquireFence(Handle)));

    /// <summary>
    /// Cancels the command buffer, discarding all recorded commands.
    /// </summary>
    public void Cancel() => Check(SDL_CancelGPUCommandBuffer(Handle));

    /// <summary>
    /// Inserts a debug label into the command buffer for GPU debugging tools.
    /// </summary>
    /// <param name="text">The label text.</param>
    public void InsertDebugLabel(string text) => SDL_InsertGPUDebugLabel(Handle, ToUtf8(text));

    /// <summary>
    /// Pushes a named debug group onto the command buffer for GPU debugging tools.
    /// </summary>
    /// <param name="name">The debug group name.</param>
    public void PushDebugGroup(string name) => SDL_PushGPUDebugGroup(Handle, ToUtf8(name));

    /// <summary>
    /// Pops the most recent debug group from the command buffer.
    /// </summary>
    public void PopDebugGroup() => SDL_PopGPUDebugGroup(Handle);
}
