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
    /// Begins a render pass with a single color target and no depth/stencil target.
    /// </summary>
    /// <param name="colorTarget">The color target info.</param>
    /// <returns>A new render pass.</returns>
    public GpuRenderPass BeginRenderPass(in GpuColorTargetInfo colorTarget)
    {
        var native = colorTarget.ToNative();
        return new(Check(SDL_BeginGPURenderPass(Handle, &native, 1, null)));
    }

    /// <summary>
    /// Begins a render pass with the specified color targets and no depth/stencil target.
    /// </summary>
    /// <param name="colorTargets">The color target infos.</param>
    /// <returns>A new render pass.</returns>
    public GpuRenderPass BeginRenderPass(ReadOnlySpan<GpuColorTargetInfo> colorTargets)
    {
        Span<SDL_GPUColorTargetInfo> native = stackalloc SDL_GPUColorTargetInfo[colorTargets.Length];
        for (var i = 0; i < colorTargets.Length; i++) native[i] = colorTargets[i].ToNative();
        fixed (SDL_GPUColorTargetInfo* p = native)
            return new(Check(SDL_BeginGPURenderPass(Handle, p, (uint)colorTargets.Length, null)));
    }

    /// <summary>
    /// Begins a render pass with the specified color targets and a depth/stencil target.
    /// </summary>
    /// <param name="colorTargets">The color target infos.</param>
    /// <param name="depthStencilTarget">The depth/stencil target info.</param>
    /// <returns>A new render pass.</returns>
    public GpuRenderPass BeginRenderPass(ReadOnlySpan<GpuColorTargetInfo> colorTargets, in GpuDepthStencilTargetInfo depthStencilTarget)
    {
        Span<SDL_GPUColorTargetInfo> native = stackalloc SDL_GPUColorTargetInfo[colorTargets.Length];
        for (var i = 0; i < colorTargets.Length; i++) native[i] = colorTargets[i].ToNative();
        var nativeDepth = depthStencilTarget.ToNative();
        fixed (SDL_GPUColorTargetInfo* p = native)
            return new(Check(SDL_BeginGPURenderPass(Handle, p, (uint)colorTargets.Length, &nativeDepth)));
    }

    /// <summary>
    /// Begins a compute pass with the specified read-write storage bindings.
    /// </summary>
    /// <param name="storageTextures">The storage texture read-write bindings.</param>
    /// <param name="storageBuffers">The storage buffer read-write bindings.</param>
    /// <returns>A new compute pass.</returns>
    public GpuComputePass BeginComputePass(ReadOnlySpan<GpuStorageTextureReadWriteBinding> storageTextures, ReadOnlySpan<GpuStorageBufferReadWriteBinding> storageBuffers)
    {
        Span<SDL_GPUStorageTextureReadWriteBinding> nativeTextures = stackalloc SDL_GPUStorageTextureReadWriteBinding[storageTextures.Length];
        for (var i = 0; i < storageTextures.Length; i++) nativeTextures[i] = storageTextures[i].ToNative();
        Span<SDL_GPUStorageBufferReadWriteBinding> nativeBuffers = stackalloc SDL_GPUStorageBufferReadWriteBinding[storageBuffers.Length];
        for (var i = 0; i < storageBuffers.Length; i++) nativeBuffers[i] = storageBuffers[i].ToNative();
        fixed (SDL_GPUStorageTextureReadWriteBinding* pt = nativeTextures)
        fixed (SDL_GPUStorageBufferReadWriteBinding* pb = nativeBuffers)
            return new(Check(SDL_BeginGPUComputePass(Handle, pt, (uint)storageTextures.Length, pb, (uint)storageBuffers.Length)));
    }

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
    public void Blit(in GpuBlitInfo info)
    {
        var native = info.ToNative();
        SDL_BlitGPUTexture(Handle, &native);
    }

    /// <summary>
    /// Acquires a swapchain texture for rendering to a window.
    /// Returns null when no texture is available this frame (too many frames in flight) — skip rendering.
    /// The returned texture is owned by the swapchain; disposing it does not release the underlying texture.
    /// The texture is valid only until this command buffer is submitted or canceled, must only be used
    /// with this command buffer, and must not be retained across frames.
    /// This method must be called from the thread that created the window.
    /// Prefer <see cref="WaitAndAcquireSwapchainTexture"/> unless you are managing frame timing yourself:
    /// acquiring without waiting can allocate many command buffers while the CPU outpaces the GPU,
    /// causing unbounded memory growth.
    /// </summary>
    /// <param name="window">The window to acquire the swapchain texture from.</param>
    /// <param name="size">Receives the swapchain texture size.</param>
    /// <returns>The swapchain texture, or null if not ready yet this frame.</returns>
    public GpuTexture? AcquireSwapchainTexture(Window window, out Size size)
    {
        SDL_GPUTexture* tex;
        uint w, h;
        Check(SDL_AcquireGPUSwapchainTexture(Handle, window.Handle, &tex, &w, &h));
        size = new Size((int)w, (int)h);
        return tex == null ? null : new GpuTexture(_device, tex, ownsHandle: false);
    }

    /// <summary>
    /// Waits for and acquires a swapchain texture for rendering to a window.
    /// Returns null when no texture is available (e.g. the window is minimized) — skip rendering.
    /// The returned texture is owned by the swapchain; disposing it does not release the underlying texture.
    /// The texture is valid only until this command buffer is submitted or canceled, must only be used
    /// with this command buffer, and must not be retained across frames.
    /// This method must be called from the thread that created the window.
    /// </summary>
    /// <param name="window">The window to acquire the swapchain texture from.</param>
    /// <param name="size">Receives the swapchain texture size.</param>
    /// <returns>The swapchain texture, or null when no texture is available (e.g. the window is minimized).</returns>
    public GpuTexture? WaitAndAcquireSwapchainTexture(Window window, out Size size)
    {
        SDL_GPUTexture* tex;
        uint w, h;
        Check(SDL_WaitAndAcquireGPUSwapchainTexture(Handle, window.Handle, &tex, &w, &h));
        size = new Size((int)w, (int)h);
        return tex == null ? null : new GpuTexture(_device, tex, ownsHandle: false);
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
