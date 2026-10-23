using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU command buffer (SDL_GPUCommandBuffer).
/// Command buffers record GPU commands that are submitted for execution.
/// </summary>
public sealed unsafe class GpuCommandBuffer : IDisposable
{
    private readonly GpuDevice _device;
    private readonly Native.SDL_GPUDevice* _devicePointer;
    private Native.SDL_GPUDevice* DeviceHandle { get { _device.Lifetime.Check(); return _devicePointer; } }
    internal readonly NativeLifetime Lifetime;
    private readonly IDisposable _deviceLease;
    private readonly Dictionary<NativeLifetime, IDisposable> _resources = [];
    private readonly Dictionary<object, IDisposable> _dependencies = [];
    private object? _activePass;
    internal void CheckDevice(GpuDevice device)
    {
        _ = Handle;
        device.Lifetime.Check();
        if (!ReferenceEquals(device.Lifetime, _device.Lifetime)) throw new ArgumentException("The command buffer belongs to a different GPU device.", nameof(device));
    }
    internal void RetainResource(NativeLifetime resource, NativeLifetime device)
    {
        _ = Handle;
        if (!ReferenceEquals(device, _device.Lifetime)) throw new ArgumentException("GPU resources must belong to this command buffer's device.");
        if (!_resources.ContainsKey(resource)) _resources[resource] = resource.Retain();
    }
    internal void RetainDependency(object key, Func<IDisposable> retain)
    {
        _ = Handle;
        if (!_dependencies.ContainsKey(key)) _dependencies[key] = retain();
    }
    private bool _hasSwapchain;

    internal void CheckIdle()
    {
        _ = Handle;
        if (_activePass != null) throw new InvalidOperationException("End the current GPU pass first.");
    }
    internal void BeginPass(object pass) { CheckIdle(); _activePass = pass; }
    internal void CheckPass(object pass)
    {
        _ = Handle;
        if (!ReferenceEquals(_activePass, pass)) throw new InvalidOperationException("This GPU pass is no longer active.");
    }
    internal void EndPass(object pass) { CheckPass(pass); _activePass = null; }
    private void UseLifetime(NativeLifetime lifetime, GpuDevice device)
    {
        _ = Handle;
        if (!ReferenceEquals(device.Lifetime, _device.Lifetime)) throw new ArgumentException("GPU resources must belong to this command buffer's device.");
        if (!_resources.ContainsKey(lifetime)) _resources[lifetime] = lifetime.Retain();
    }
    internal SDL_GPUBuffer* Use(GpuBuffer resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        UseLifetime(resource.Lifetime, resource.Device);
        return resource.Handle;
    }
    internal SDL_GPUTexture* Use(GpuTexture resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        if (resource.OwnerCommand != null && resource.OwnerCommand != this) throw new ArgumentException("A swapchain texture can only be used by its acquiring command buffer.");
        UseLifetime(resource.Lifetime, resource.Device);
        return resource.Handle;
    }
    internal SDL_GPUSampler* Use(GpuSampler resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        UseLifetime(resource.Lifetime, resource.Device);
        return resource.Handle;
    }
    internal SDL_GPUShader* Use(GpuShader resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        UseLifetime(resource.Lifetime, resource.Device);
        return resource.Handle;
    }
    internal SDL_GPUGraphicsPipeline* Use(GpuGraphicsPipeline resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        UseLifetime(resource.Lifetime, resource.Device);
        return resource.Handle;
    }
    internal SDL_GPUComputePipeline* Use(GpuComputePipeline resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        UseLifetime(resource.Lifetime, resource.Device);
        return resource.Handle;
    }
    internal SDL_GPUTransferBuffer* Use(GpuTransferBuffer resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        UseLifetime(resource.Lifetime, resource.Device);
        return resource.Handle;
    }
    internal SDL_GPUFence* Use(GpuFence resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        UseLifetime(resource.Lifetime, resource.Device);
        return resource.Handle;
    }

    internal void Complete()
    {
        _handle = null;
        foreach (var lease in _resources.Values) lease.Dispose();
        _resources.Clear();
        foreach (var lease in _dependencies.Values) lease.Dispose();
        _dependencies.Clear();
        Lifetime.Invalidate();
        _deviceLease.Dispose();
        _device.Dispose();
    }

    /// <summary>Cancels unsubmitted work. A buffer with a swapchain texture must be submitted explicitly.</summary>
    public void Dispose() { if (_handle != null) Cancel(); }


    /// <summary>
    /// The underlying native SDL_GPUCommandBuffer pointer.
    /// </summary>
    internal SDL_GPUCommandBuffer* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            Lifetime.Check();
            return _handle;
        }
    }

    private SDL_GPUCommandBuffer* _handle;
    private SDL_GPUCommandBuffer* IdleHandle { get { CheckIdle(); return Handle; } }

    internal GpuCommandBuffer(GpuDevice device, SDL_GPUCommandBuffer* handle)
    {
        _device = new GpuDevice(device.Handle, ownsHandle: false);
        _devicePointer = device.Handle;
        _handle = handle;
        Lifetime = NativeLifetime.Get(typeof(GpuCommandBuffer), (nint)handle, true, threadAffine: true, parent: device.Lifetime);
        _deviceLease = device.Lifetime.Retain();
    }

    /// <summary>
    /// Begins a render pass with a single color target and no depth/stencil target.
    /// </summary>
    /// <param name="colorTarget">The color target info.</param>
    /// <returns>A new render pass.</returns>
    public GpuRenderPass BeginRenderPass(in GpuColorTargetInfo colorTarget)
    {
        CheckIdle();
        var native = colorTarget.ToNative(this);
        return new(this, Check(SDL_BeginGPURenderPass(Handle, &native, 1, null)));
    }

    /// <summary>
    /// Begins a render pass with the specified color targets and no depth/stencil target.
    /// </summary>
    /// <param name="colorTargets">The color target infos.</param>
    /// <returns>A new render pass.</returns>
    public GpuRenderPass BeginRenderPass(ReadOnlySpan<GpuColorTargetInfo> colorTargets)
    {
        CheckIdle();
        Span<SDL_GPUColorTargetInfo> native = colorTargets.Length <= 64 ? stackalloc SDL_GPUColorTargetInfo[colorTargets.Length] : new SDL_GPUColorTargetInfo[colorTargets.Length];
        for (var i = 0; i < colorTargets.Length; i++) native[i] = colorTargets[i].ToNative(this);
        fixed (SDL_GPUColorTargetInfo* p = native)
            return new(this, Check(SDL_BeginGPURenderPass(Handle, p, (uint)colorTargets.Length, null)));
    }

    /// <summary>
    /// Begins a render pass with the specified color targets and a depth/stencil target.
    /// </summary>
    /// <param name="colorTargets">The color target infos.</param>
    /// <param name="depthStencilTarget">The depth/stencil target info.</param>
    /// <returns>A new render pass.</returns>
    public GpuRenderPass BeginRenderPass(ReadOnlySpan<GpuColorTargetInfo> colorTargets, in GpuDepthStencilTargetInfo depthStencilTarget)
    {
        CheckIdle();
        Span<SDL_GPUColorTargetInfo> native = colorTargets.Length <= 64 ? stackalloc SDL_GPUColorTargetInfo[colorTargets.Length] : new SDL_GPUColorTargetInfo[colorTargets.Length];
        for (var i = 0; i < colorTargets.Length; i++) native[i] = colorTargets[i].ToNative(this);
        var nativeDepth = depthStencilTarget.ToNative(this);
        fixed (SDL_GPUColorTargetInfo* p = native)
            return new(this, Check(SDL_BeginGPURenderPass(Handle, p, (uint)colorTargets.Length, &nativeDepth)));
    }

    /// <summary>
    /// Begins a compute pass with the specified read-write storage bindings.
    /// </summary>
    /// <param name="storageTextures">The storage texture read-write bindings.</param>
    /// <param name="storageBuffers">The storage buffer read-write bindings.</param>
    /// <returns>A new compute pass.</returns>
    public GpuComputePass BeginComputePass(ReadOnlySpan<GpuStorageTextureReadWriteBinding> storageTextures, ReadOnlySpan<GpuStorageBufferReadWriteBinding> storageBuffers)
    {
        CheckIdle();
        Span<SDL_GPUStorageTextureReadWriteBinding> nativeTextures = storageTextures.Length <= 64 ? stackalloc SDL_GPUStorageTextureReadWriteBinding[storageTextures.Length] : new SDL_GPUStorageTextureReadWriteBinding[storageTextures.Length];
        for (var i = 0; i < storageTextures.Length; i++) nativeTextures[i] = storageTextures[i].ToNative(this);
        Span<SDL_GPUStorageBufferReadWriteBinding> nativeBuffers = storageBuffers.Length <= 64 ? stackalloc SDL_GPUStorageBufferReadWriteBinding[storageBuffers.Length] : new SDL_GPUStorageBufferReadWriteBinding[storageBuffers.Length];
        for (var i = 0; i < storageBuffers.Length; i++) nativeBuffers[i] = storageBuffers[i].ToNative(this);
        fixed (SDL_GPUStorageTextureReadWriteBinding* pt = nativeTextures)
        fixed (SDL_GPUStorageBufferReadWriteBinding* pb = nativeBuffers)
            return new(this, Check(SDL_BeginGPUComputePass(Handle, pt, (uint)storageTextures.Length, pb, (uint)storageBuffers.Length)));
    }

    /// <summary>
    /// Begins a copy pass for transferring data between CPU and GPU resources.
    /// </summary>
    /// <returns>A new copy pass.</returns>
    public GpuCopyPass BeginCopyPass() { CheckIdle(); return new(this, Check(SDL_BeginGPUCopyPass(Handle))); }

    /// <summary>
    /// Pushes uniform data for a vertex shader slot.
    /// </summary>
    /// <typeparam name="T">The unmanaged type of the uniform data.</typeparam>
    /// <param name="slot">The uniform buffer slot index.</param>
    /// <param name="data">The uniform data.</param>
    public void PushVertexUniformData<T>(uint slot, in T data) where T : unmanaged
    {
        fixed (T* p = &data)
        {
            SDL_PushGPUVertexUniformData(Handle, slot, p, (uint)sizeof(T));
        }
    }

    /// <summary>
    /// Pushes uniform data for a vertex shader slot.
    /// </summary>
    /// <param name="slot">The uniform buffer slot index.</param>
    /// <param name="data">The uniform data bytes.</param>
    public void PushVertexUniformData(uint slot, ReadOnlySpan<byte> data)
    {
        fixed (byte* p = data)
        {
            SDL_PushGPUVertexUniformData(Handle, slot, p, (uint)data.Length);
        }
    }

    /// <summary>
    /// Pushes uniform data for a vertex shader slot.
    /// </summary>
    /// <typeparam name="T">The unmanaged element type of the uniform data.</typeparam>
    /// <param name="slot">The uniform buffer slot index.</param>
    /// <param name="data">The uniform data values.</param>
    public void PushVertexUniformData<T>(uint slot, ReadOnlySpan<T> data) where T : unmanaged
    {
        fixed (T* p = data)
        {
            SDL_PushGPUVertexUniformData(Handle, slot, p, (uint)data.Length * (uint)sizeof(T));
        }
    }

    /// <summary>
    /// Pushes uniform data for a fragment shader slot.
    /// </summary>
    /// <typeparam name="T">The unmanaged type of the uniform data.</typeparam>
    /// <param name="slot">The uniform buffer slot index.</param>
    /// <param name="data">The uniform data.</param>
    public void PushFragmentUniformData<T>(uint slot, in T data) where T : unmanaged
    {
        fixed (T* p = &data)
        {
            SDL_PushGPUFragmentUniformData(Handle, slot, p, (uint)sizeof(T));
        }
    }

    /// <summary>
    /// Pushes uniform data for a fragment shader slot.
    /// </summary>
    /// <param name="slot">The uniform buffer slot index.</param>
    /// <param name="data">The uniform data bytes.</param>
    public void PushFragmentUniformData(uint slot, ReadOnlySpan<byte> data)
    {
        fixed (byte* p = data)
        {
            SDL_PushGPUFragmentUniformData(Handle, slot, p, (uint)data.Length);
        }
    }

    /// <summary>
    /// Pushes uniform data for a fragment shader slot.
    /// </summary>
    /// <typeparam name="T">The unmanaged element type of the uniform data.</typeparam>
    /// <param name="slot">The uniform buffer slot index.</param>
    /// <param name="data">The uniform data values.</param>
    public void PushFragmentUniformData<T>(uint slot, ReadOnlySpan<T> data) where T : unmanaged
    {
        fixed (T* p = data)
        {
            SDL_PushGPUFragmentUniformData(Handle, slot, p, (uint)data.Length * (uint)sizeof(T));
        }
    }

    /// <summary>
    /// Pushes uniform data for a compute shader slot.
    /// </summary>
    /// <typeparam name="T">The unmanaged type of the uniform data.</typeparam>
    /// <param name="slot">The uniform buffer slot index.</param>
    /// <param name="data">The uniform data.</param>
    public void PushComputeUniformData<T>(uint slot, in T data) where T : unmanaged
    {
        fixed (T* p = &data)
        {
            SDL_PushGPUComputeUniformData(Handle, slot, p, (uint)sizeof(T));
        }
    }

    /// <summary>
    /// Pushes uniform data for a compute shader slot.
    /// </summary>
    /// <param name="slot">The uniform buffer slot index.</param>
    /// <param name="data">The uniform data bytes.</param>
    public void PushComputeUniformData(uint slot, ReadOnlySpan<byte> data)
    {
        fixed (byte* p = data)
        {
            SDL_PushGPUComputeUniformData(Handle, slot, p, (uint)data.Length);
        }
    }

    /// <summary>
    /// Pushes uniform data for a compute shader slot.
    /// </summary>
    /// <typeparam name="T">The unmanaged element type of the uniform data.</typeparam>
    /// <param name="slot">The uniform buffer slot index.</param>
    /// <param name="data">The uniform data values.</param>
    public void PushComputeUniformData<T>(uint slot, ReadOnlySpan<T> data) where T : unmanaged
    {
        fixed (T* p = data)
        {
            SDL_PushGPUComputeUniformData(Handle, slot, p, (uint)data.Length * (uint)sizeof(T));
        }
    }

    /// <summary>
    /// Generates mipmaps for the specified texture.
    /// </summary>
    /// <param name="texture">The texture to generate mipmaps for.</param>
    public void GenerateMipmaps(GpuTexture texture) =>
        SDL_GenerateMipmapsForGPUTexture(IdleHandle, Use(texture));

    /// <summary>
    /// Blits (copies with potential scaling/filtering) between texture regions.
    /// </summary>
    /// <param name="info">The blit operation parameters.</param>
    public void Blit(in GpuBlitInfo info)
    {
        CheckIdle();
        var native = info.ToNative(this);
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
        CheckIdle();
        _device.CheckClaim(window);
        SDL_GPUTexture* tex;
        uint w, h;
        Check(SDL_AcquireGPUSwapchainTexture(Handle, window.Handle, &tex, &w, &h));
        size = new Size((int)w, (int)h);
        if (tex == null) return null;
        _hasSwapchain = true;
        UseLifetime(window.Lifetime, _device);
        return new GpuTexture(_device, tex, ownsHandle: false, parent: Lifetime) { OwnerCommand = this };
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
        CheckIdle();
        _device.CheckClaim(window);
        SDL_GPUTexture* tex;
        uint w, h;
        Check(SDL_WaitAndAcquireGPUSwapchainTexture(Handle, window.Handle, &tex, &w, &h));
        size = new Size((int)w, (int)h);
        if (tex == null) return null;
        _hasSwapchain = true;
        UseLifetime(window.Lifetime, _device);
        return new GpuTexture(_device, tex, ownsHandle: false, parent: Lifetime) { OwnerCommand = this };
    }

    /// <summary>
    /// Submits the command buffer for execution by the GPU.
    /// </summary>
    public void Submit()
    {
        CheckIdle();
        var handle = Handle;
        try { Check(SDL_SubmitGPUCommandBuffer(handle)); }
        finally { Complete(); }
    }

    /// <summary>
    /// Submits the command buffer and acquires a fence that is signaled when execution completes.
    /// </summary>
    /// <returns>A fence that will be signaled when the submitted work completes.</returns>
    public GpuFence SubmitAndAcquireFence()
    {
        CheckIdle();
        var handle = Handle;
        try { return new(_device, Check(SDL_SubmitGPUCommandBufferAndAcquireFence(handle))); }
        finally { Complete(); }
    }

    /// <summary>
    /// Cancels the command buffer, discarding all recorded commands.
    /// </summary>
    public void Cancel()
    {
        CheckIdle();
        if (_hasSwapchain) throw new InvalidOperationException("Submit a command buffer after acquiring a swapchain texture; SDL does not permit cancellation.");
        var handle = Handle;
        try { Check(SDL_CancelGPUCommandBuffer(handle)); }
        finally { Complete(); }
    }

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
