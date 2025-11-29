using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents a GPU command buffer for recording and submitting commands.
/// </summary>
public sealed unsafe class GpuCommandBuffer
{
    private readonly GpuDevice _device;

    /// <summary>
    /// Gets the underlying SDL_GPUCommandBuffer pointer.
    /// </summary>
    public SDL_GPUCommandBuffer* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPUCommandBuffer pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPUCommandBuffer pointer.</param>
    /// <param name="device">The GPU device that created this command buffer.</param>
    internal GpuCommandBuffer(SDL_GPUCommandBuffer* handle, GpuDevice device)
    {
        Handle = handle;
        _device = device;
    }

    /// <summary>
    /// Pushes data to a vertex uniform slot on the command buffer.
    /// </summary>
    /// <typeparam name="T">The type of data to push.</typeparam>
    /// <param name="slotIndex">The vertex uniform slot to push data to.</param>
    /// <param name="data">The data to push.</param>
    public void PushVertexUniformData<T>(uint slotIndex, ref T data) where T : unmanaged
    {
        fixed (T* ptr = &data)
        {
            SDL_PushGPUVertexUniformData(Handle, slotIndex, ptr, (uint)sizeof(T));
        }
    }

    /// <summary>
    /// Pushes data to a fragment uniform slot on the command buffer.
    /// </summary>
    /// <typeparam name="T">The type of data to push.</typeparam>
    /// <param name="slotIndex">The fragment uniform slot to push data to.</param>
    /// <param name="data">The data to push.</param>
    public void PushFragmentUniformData<T>(uint slotIndex, ref T data) where T : unmanaged
    {
        fixed (T* ptr = &data)
        {
            SDL_PushGPUFragmentUniformData(Handle, slotIndex, ptr, (uint)sizeof(T));
        }
    }

    /// <summary>
    /// Pushes data to a compute uniform slot on the command buffer.
    /// </summary>
    /// <typeparam name="T">The type of data to push.</typeparam>
    /// <param name="slotIndex">The compute uniform slot to push data to.</param>
    /// <param name="data">The data to push.</param>
    public void PushComputeUniformData<T>(uint slotIndex, ref T data) where T : unmanaged
    {
        fixed (T* ptr = &data)
        {
            SDL_PushGPUComputeUniformData(Handle, slotIndex, ptr, (uint)sizeof(T));
        }
    }

    /// <summary>
    /// Begins a render pass on the command buffer.
    /// </summary>
    /// <param name="colorTargetInfos">The color target infos for the render pass.</param>
    /// <param name="depthStencilTargetInfo">The depth-stencil target info, or null if not used.</param>
    /// <returns>A new render pass.</returns>
    public GpuRenderPass BeginRenderPass(GpuColorTargetInfo[] colorTargetInfos, GpuDepthStencilTargetInfo? depthStencilTargetInfo = null)
    {
        var nativeColorTargets = new SDL_GPUColorTargetInfo[colorTargetInfos.Length];
        for (var i = 0; i < colorTargetInfos.Length; i++)
        {
            nativeColorTargets[i] = colorTargetInfos[i].ToNative();
        }

        fixed (SDL_GPUColorTargetInfo* colorTargetsPtr = nativeColorTargets)
        {
            if (depthStencilTargetInfo.HasValue)
            {
                SDL_GPUDepthStencilTargetInfo nativeDepthStencil = depthStencilTargetInfo.Value.ToNative();
                return new GpuRenderPass(
                    CheckErrorPointer(SDL_BeginGPURenderPass(Handle, colorTargetsPtr, (uint)colorTargetInfos.Length, &nativeDepthStencil)),
                    this);
            }
            else
            {
                return new GpuRenderPass(
                    CheckErrorPointer(SDL_BeginGPURenderPass(Handle, colorTargetsPtr, (uint)colorTargetInfos.Length, null)),
                    this);
            }
        }
    }

    /// <summary>
    /// Begins a compute pass on the command buffer.
    /// </summary>
    /// <param name="storageTextureBindings">The storage texture bindings, or null if not used.</param>
    /// <param name="storageBufferBindings">The storage buffer bindings, or null if not used.</param>
    /// <returns>A new compute pass.</returns>
    public GpuComputePass BeginComputePass(GpuStorageTextureReadWriteBinding[]? storageTextureBindings = null, GpuStorageBufferReadWriteBinding[]? storageBufferBindings = null)
    {
        var numTextures = (uint)(storageTextureBindings?.Length ?? 0);
        var numBuffers = (uint)(storageBufferBindings?.Length ?? 0);

        SDL_GPUStorageTextureReadWriteBinding[]? nativeTextureBindings = storageTextureBindings != null ? new SDL_GPUStorageTextureReadWriteBinding[storageTextureBindings.Length] : null;
        SDL_GPUStorageBufferReadWriteBinding[]? nativeBufferBindings = storageBufferBindings != null ? new SDL_GPUStorageBufferReadWriteBinding[storageBufferBindings.Length] : null;

        if (storageTextureBindings != null)
        {
            for (var i = 0; i < storageTextureBindings.Length; i++)
            {
                nativeTextureBindings![i] = storageTextureBindings[i].ToNative();
            }
        }

        if (storageBufferBindings != null)
        {
            for (var i = 0; i < storageBufferBindings.Length; i++)
            {
                nativeBufferBindings![i] = storageBufferBindings[i].ToNative();
            }
        }

        fixed (SDL_GPUStorageTextureReadWriteBinding* texturesPtr = nativeTextureBindings)
        fixed (SDL_GPUStorageBufferReadWriteBinding* buffersPtr = nativeBufferBindings)
        {
            return new GpuComputePass(
                CheckErrorPointer(SDL_BeginGPUComputePass(Handle, texturesPtr, numTextures, buffersPtr, numBuffers)),
                this);
        }
    }

    /// <summary>
    /// Begins a copy pass on the command buffer.
    /// </summary>
    /// <returns>A new copy pass.</returns>
    public GpuCopyPass BeginCopyPass()
    {
        return new GpuCopyPass(CheckErrorPointer(SDL_BeginGPUCopyPass(Handle)), this);
    }

    /// <summary>
    /// Acquires a swapchain texture for the given window.
    /// </summary>
    /// <param name="window">The window to acquire a swapchain texture for.</param>
    /// <param name="texture">The acquired swapchain texture.</param>
    /// <param name="width">The width of the swapchain texture.</param>
    /// <param name="height">The height of the swapchain texture.</param>
    /// <returns>True if the texture was acquired successfully, false otherwise.</returns>
    public bool AcquireSwapchainTexture(Window window, out GpuTexture? texture, out uint width, out uint height)
    {
        SDL_GPUTexture* texturePtr;
        uint w, h;
        var result = CheckErrorBool(SDL_AcquireGPUSwapchainTexture(Handle, window.Handle, &texturePtr, &w, &h));

        if (result && texturePtr != null)
        {
            texture = new GpuTexture(texturePtr, _device, ownsHandle: false);
            width = w;
            height = h;
            return true;
        }

        texture = null;
        width = 0;
        height = 0;
        return result;
    }

    /// <summary>
    /// Waits for and acquires a swapchain texture for the given window.
    /// </summary>
    /// <param name="window">The window to acquire a swapchain texture for.</param>
    /// <param name="texture">The acquired swapchain texture.</param>
    /// <param name="width">The width of the swapchain texture.</param>
    /// <param name="height">The height of the swapchain texture.</param>
    /// <returns>True if the texture was acquired successfully, false otherwise.</returns>
    public bool WaitAndAcquireSwapchainTexture(Window window, out GpuTexture? texture, out uint width, out uint height)
    {
        SDL_GPUTexture* texturePtr;
        uint w, h;
        var result = CheckErrorBool(SDL_WaitAndAcquireGPUSwapchainTexture(Handle, window.Handle, &texturePtr, &w, &h));

        if (result && texturePtr != null)
        {
            texture = new GpuTexture(texturePtr, _device, ownsHandle: false);
            width = w;
            height = h;
            return true;
        }

        texture = null;
        width = 0;
        height = 0;
        return result;
    }

    /// <summary>
    /// Inserts an arbitrary string label into the command buffer callstream.
    /// </summary>
    /// <param name="text">The label text.</param>
    public void InsertDebugLabel(string text)
    {
        SDL_InsertGPUDebugLabel(Handle, text);
    }

    /// <summary>
    /// Begins a debug group with an arbitrary name.
    /// </summary>
    /// <param name="name">The name of the debug group.</param>
    public void PushDebugGroup(string name)
    {
        SDL_PushGPUDebugGroup(Handle, name);
    }

    /// <summary>
    /// Ends the most-recently pushed debug group.
    /// </summary>
    public void PopDebugGroup()
    {
        SDL_PopGPUDebugGroup(Handle);
    }

    /// <summary>
    /// Generates mipmaps for the given texture.
    /// </summary>
    /// <param name="texture">The texture to generate mipmaps for.</param>
    public void GenerateMipmaps(GpuTexture texture)
    {
        SDL_GenerateMipmapsForGPUTexture(Handle, texture.Handle);
    }

    /// <summary>
    /// Blits from a source texture region to a destination texture region.
    /// </summary>
    /// <param name="info">The blit info.</param>
    public void Blit(GpuBlitInfo info)
    {
        SDL_GPUBlitInfo nativeInfo = info.ToNative();
        SDL_BlitGPUTexture(Handle, &nativeInfo);
    }

    /// <summary>
    /// Submits the command buffer so its commands can be processed on the GPU.
    /// </summary>
    public void Submit()
    {
        _ = CheckErrorBool(SDL_SubmitGPUCommandBuffer(Handle));
        Handle = null;
    }

    /// <summary>
    /// Submits the command buffer and acquires a fence associated with it.
    /// </summary>
    /// <returns>A fence associated with the command buffer.</returns>
    public GpuFence SubmitAndAcquireFence()
    {
        var fence = new GpuFence(CheckErrorPointer(SDL_SubmitGPUCommandBufferAndAcquireFence(Handle)), _device);
        Handle = null;
        return fence;
    }

    /// <summary>
    /// Cancels the command buffer.
    /// </summary>
    public void Cancel()
    {
        _ = CheckErrorBool(SDL_CancelGPUCommandBuffer(Handle));
        Handle = null;
    }
}
