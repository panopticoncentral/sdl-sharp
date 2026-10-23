using System.Runtime.InteropServices;
using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// A managed wrapper around an SDL GPU device (SDL_GPUDevice).
/// The device is the primary object used to create GPU resources and submit work.
/// </summary>
public sealed unsafe class GpuDevice : IDisposable
{
    private readonly bool _ownsHandle;
    internal readonly NativeLifetime Lifetime;
    private readonly NativeLifetime? _viewParent;

    /// <summary>
    /// The underlying native SDL_GPUDevice pointer.
    /// </summary>
    internal SDL_GPUDevice* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            Lifetime.Check();
            _viewParent?.Check();
            return _handle;
        }
    }

    private SDL_GPUDevice* _handle;

    internal GpuDevice(SDL_GPUDevice* handle, bool ownsHandle = true, NativeLifetime? parent = null)
    {
        _handle = handle;
        Lifetime = NativeLifetime.Get(typeof(GpuDevice), (nint)handle, ownsHandle, threadAffine: false, parent: parent);
        _ownsHandle = ownsHandle;
        _viewParent = ownsHandle ? null : parent;
    }

    /// <summary>
    /// Whether this device has been disposed. Lets dependent GPU resources skip
    /// their native release when the device is already gone.
    /// </summary>
    internal bool IsDisposed => _handle == null || !Lifetime.IsAlive;

    /// <summary>
    /// Creates a new GPU device with the specified shader format support.
    /// </summary>
    /// <param name="shaderFormats">The shader formats the device should support.</param>
    /// <param name="debugMode">Whether to enable GPU debug mode.</param>
    /// <param name="preferredBackend">An optional preferred GPU backend name, or null for auto-selection.</param>
    /// <returns>A new GPU device.</returns>
    /// <remarks>
    /// For debug or backend-specific creation knobs beyond these parameters, use
    /// <see cref="Create(PropertyGroup)"/> with the property names in <see cref="GpuDeviceProperties"/>.
    /// </remarks>
    /// <seealso cref="Create(PropertyGroup)"/>
    public static GpuDevice Create(GpuShaderFormat shaderFormats, bool debugMode = false, string? preferredBackend = null) =>
        new(Check(SDL_CreateGPUDevice((SDL_GPUShaderFormat)shaderFormats, debugMode, ToUtf8(preferredBackend))));

    /// <summary>
    /// Creates a new GPU device from a property group. Property names are in <see cref="GpuDeviceProperties"/>.
    /// </summary>
    /// <param name="props">The creation properties.</param>
    /// <returns>A new GPU device.</returns>
    public static GpuDevice Create(PropertyGroup props) =>
        new(Check(SDL_CreateGPUDeviceWithProperties(props.Id)));

    /// <summary>
    /// Checks whether a GPU device with the given shader format support can be created.
    /// </summary>
    /// <param name="shaderFormats">The required shader formats.</param>
    /// <param name="preferredBackend">An optional GPU backend name, or null for any.</param>
    /// <returns>True if a device supporting the formats can be created.</returns>
    public static bool SupportsShaderFormats(GpuShaderFormat shaderFormats, string? preferredBackend = null) =>
        SDL_GPUSupportsShaderFormats((SDL_GPUShaderFormat)shaderFormats, ToUtf8(preferredBackend));

    /// <summary>
    /// Gets the number of GPU drivers compiled into SDL.
    /// </summary>
    public static int NumDrivers => SDL_GetNumGPUDrivers();

    /// <summary>
    /// Gets the name of a built-in GPU driver by index.
    /// </summary>
    /// <param name="index">The zero-based index of the driver.</param>
    /// <returns>The name of the driver, or null.</returns>
    public static string? GetDriver(int index) => Marshal.PtrToStringUTF8((nint)SDL_GetGPUDriver(index));

    /// <summary>
    /// Gets the name of the backend used by this GPU device.
    /// </summary>
    public string? Driver => Marshal.PtrToStringUTF8((nint)SDL_GetGPUDeviceDriver(Handle));

    /// <summary>
    /// Gets the shader formats supported by this GPU device.
    /// </summary>
    public GpuShaderFormat ShaderFormats => (GpuShaderFormat)SDL_GetGPUShaderFormats(Handle);

    /// <summary>
    /// Gets the properties of this device (name, driver info). The returned group is owned by SDL.
    /// Each access returns a new wrapper around the same underlying SDL property set;
    /// the wrappers are not equal to each other.
    /// </summary>
    public PropertyGroup Properties => new(CheckId(SDL_GetGPUDeviceProperties(Handle)), ownsHandle: false, parent: Lifetime);

    /// <summary>
    /// Creates a GPU shader from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The shader creation parameters.</param>
    /// <returns>A new GPU shader.</returns>
    public GpuShader CreateShader(in GpuShaderCreateInfo createInfo)
    {
        fixed (byte* code = createInfo.Code.Span)
        fixed (byte* entryPoint = ToUtf8(createInfo.EntryPoint ?? "main"))
        {
            var native = new SDL_GPUShaderCreateInfo
            {
                code_size = (nuint)createInfo.Code.Length,
                code = code,
                entrypoint = entryPoint,
                format = (SDL_GPUShaderFormat)createInfo.Format,
                stage = (SDL_GPUShaderStage)createInfo.Stage,
                num_samplers = createInfo.NumSamplers,
                num_storage_textures = createInfo.NumStorageTextures,
                num_storage_buffers = createInfo.NumStorageBuffers,
                num_uniform_buffers = createInfo.NumUniformBuffers,
                props = createInfo.Props?.Id ?? default,
            };
            return new GpuShader(this, Check(SDL_CreateGPUShader(Handle, &native)));
        }
    }

    /// <summary>
    /// Creates a graphics pipeline from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The graphics pipeline creation parameters.</param>
    /// <returns>A new GPU graphics pipeline.</returns>
    public GpuGraphicsPipeline CreateGraphicsPipeline(in GpuGraphicsPipelineCreateInfo createInfo)
    {
        if (!ReferenceEquals(createInfo.VertexShader.Device.Lifetime, Lifetime) || !ReferenceEquals(createInfo.FragmentShader.Device.Lifetime, Lifetime))
            throw new ArgumentException("Pipeline shaders must belong to this GPU device.", nameof(createInfo));
        var vertexBufferDescs = createInfo.VertexInputState.VertexBufferDescriptions ?? [];
        var vertexAttributes = createInfo.VertexInputState.VertexAttributes ?? [];
        var colorTargetDescs = createInfo.TargetInfo.ColorTargetDescriptions ?? [];

        Span<SDL_GPUVertexBufferDescription> nativeVertexBuffers = vertexBufferDescs.Length <= 64 ? stackalloc SDL_GPUVertexBufferDescription[vertexBufferDescs.Length] : new SDL_GPUVertexBufferDescription[vertexBufferDescs.Length];
        for (var i = 0; i < vertexBufferDescs.Length; i++) nativeVertexBuffers[i] = vertexBufferDescs[i].ToNative();
        Span<SDL_GPUVertexAttribute> nativeAttributes = vertexAttributes.Length <= 64 ? stackalloc SDL_GPUVertexAttribute[vertexAttributes.Length] : new SDL_GPUVertexAttribute[vertexAttributes.Length];
        for (var i = 0; i < vertexAttributes.Length; i++) nativeAttributes[i] = vertexAttributes[i].ToNative();
        Span<SDL_GPUColorTargetDescription> nativeColorTargets = colorTargetDescs.Length <= 64 ? stackalloc SDL_GPUColorTargetDescription[colorTargetDescs.Length] : new SDL_GPUColorTargetDescription[colorTargetDescs.Length];
        for (var i = 0; i < colorTargetDescs.Length; i++) nativeColorTargets[i] = colorTargetDescs[i].ToNative();

        fixed (SDL_GPUVertexBufferDescription* pVertexBuffers = nativeVertexBuffers)
        fixed (SDL_GPUVertexAttribute* pAttributes = nativeAttributes)
        fixed (SDL_GPUColorTargetDescription* pColorTargets = nativeColorTargets)
        {
            var native = new SDL_GPUGraphicsPipelineCreateInfo
            {
                vertex_shader = createInfo.VertexShader.Handle,
                fragment_shader = createInfo.FragmentShader.Handle,
                vertex_input_state = new SDL_GPUVertexInputState
                {
                    vertex_buffer_descriptions = pVertexBuffers,
                    num_vertex_buffers = (uint)vertexBufferDescs.Length,
                    vertex_attributes = pAttributes,
                    num_vertex_attributes = (uint)vertexAttributes.Length,
                },
                primitive_type = (SDL_GPUPrimitiveType)createInfo.PrimitiveType,
                rasterizer_state = createInfo.RasterizerState.ToNative(),
                multisample_state = createInfo.MultisampleState.ToNative(),
                depth_stencil_state = createInfo.DepthStencilState.ToNative(),
                target_info = new SDL_GPUGraphicsPipelineTargetInfo
                {
                    color_target_descriptions = pColorTargets,
                    num_color_targets = (uint)colorTargetDescs.Length,
                    depth_stencil_format = (SDL_GPUTextureFormat)(createInfo.TargetInfo.DepthStencilFormat ?? GpuTextureFormat.Invalid),
                    has_depth_stencil_target = (byte)(createInfo.TargetInfo.DepthStencilFormat is not null ? 1 : 0),
                },
                props = createInfo.Props?.Id ?? default,
            };
            return new GpuGraphicsPipeline(this, Check(SDL_CreateGPUGraphicsPipeline(Handle, &native)));
        }
    }

    /// <summary>
    /// Creates a compute pipeline from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The compute pipeline creation parameters.</param>
    /// <returns>A new GPU compute pipeline.</returns>
    public GpuComputePipeline CreateComputePipeline(in GpuComputePipelineCreateInfo createInfo)
    {
        fixed (byte* code = createInfo.Code.Span)
        fixed (byte* entryPoint = ToUtf8(createInfo.EntryPoint ?? "main"))
        {
            var native = new SDL_GPUComputePipelineCreateInfo
            {
                code_size = (nuint)createInfo.Code.Length,
                code = code,
                entrypoint = entryPoint,
                format = (SDL_GPUShaderFormat)createInfo.Format,
                num_samplers = createInfo.NumSamplers,
                num_readonly_storage_textures = createInfo.NumReadonlyStorageTextures,
                num_readonly_storage_buffers = createInfo.NumReadonlyStorageBuffers,
                num_readwrite_storage_textures = createInfo.NumReadwriteStorageTextures,
                num_readwrite_storage_buffers = createInfo.NumReadwriteStorageBuffers,
                num_uniform_buffers = createInfo.NumUniformBuffers,
                threadcount_x = createInfo.ThreadcountX,
                threadcount_y = createInfo.ThreadcountY,
                threadcount_z = createInfo.ThreadcountZ,
                props = createInfo.Props?.Id ?? default,
            };
            return new GpuComputePipeline(this, Check(SDL_CreateGPUComputePipeline(Handle, &native)));
        }
    }

    /// <summary>
    /// Creates a texture sampler from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The sampler creation parameters.</param>
    /// <returns>A new GPU sampler.</returns>
    public GpuSampler CreateSampler(in GpuSamplerCreateInfo createInfo)
    {
        var native = createInfo.ToNative();
        return new GpuSampler(this, Check(SDL_CreateGPUSampler(Handle, &native)));
    }

    /// <summary>
    /// Creates a GPU texture from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The texture creation parameters.</param>
    /// <returns>A new GPU texture.</returns>
    public GpuTexture CreateTexture(in GpuTextureCreateInfo createInfo)
    {
        var native = createInfo.ToNative();
        return new GpuTexture(this, Check(SDL_CreateGPUTexture(Handle, &native)));
    }

    /// <summary>
    /// Creates a GPU buffer from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The buffer creation parameters.</param>
    /// <returns>A new GPU buffer.</returns>
    public GpuBuffer CreateBuffer(in GpuBufferCreateInfo createInfo)
    {
        var native = createInfo.ToNative();
        return new GpuBuffer(this, Check(SDL_CreateGPUBuffer(Handle, &native)));
    }

    /// <summary>
    /// Creates a GPU transfer buffer from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The transfer buffer creation parameters.</param>
    /// <returns>A new GPU transfer buffer.</returns>
    public GpuTransferBuffer CreateTransferBuffer(in GpuTransferBufferCreateInfo createInfo)
    {
        var native = createInfo.ToNative();
        return new GpuTransferBuffer(this, Check(SDL_CreateGPUTransferBuffer(Handle, &native)), createInfo.Size);
    }

    /// <summary>
    /// Acquires a command buffer for recording GPU commands.
    /// </summary>
    /// <returns>A new command buffer.</returns>
    public GpuCommandBuffer AcquireCommandBuffer() => new(this, Check(SDL_AcquireGPUCommandBuffer(Handle)));

    private Dictionary<NativeLifetime, (nint Pointer, IDisposable Lease)> Claims => (Dictionary<NativeLifetime, (nint, IDisposable)>)(Lifetime.Attachment ??= new Dictionary<NativeLifetime, (nint, IDisposable)>());
    internal void CheckClaim(Window window)
    {
        _ = Handle; _ = window.Handle;
        if (!Claims.ContainsKey(window.Lifetime)) throw new InvalidOperationException("Claim the window with this GPU device first.");
    }
    /// <summary>Claims a window for this GPU device, retaining it until release or device disposal.</summary>
    public void ClaimWindow(Window window)
    {
        _ = Handle; _ = window.Handle;
        if (Claims.ContainsKey(window.Lifetime)) return;
        var pointer = window.Handle;
        var lease = window.Lifetime.Retain();
        try { Check(SDL_ClaimWindowForGPUDevice(Handle, pointer)); Claims.Add(window.Lifetime, ((nint)pointer, lease)); }
        catch { lease.Dispose(); throw; }
    }

    /// <summary>
    /// Releases a window from GPU presentation on this device.
    /// </summary>
    /// <param name="window">The window to release.</param>
    public void ReleaseWindow(Window window)
    {
        CheckClaim(window);
        window.Lifetime.EnsureLeaseCountAtMost(1);
        SDL_ReleaseWindowFromGPUDevice(Handle, window.Handle);
        Claims.Remove(window.Lifetime, out var claim); claim.Lease.Dispose();
    }

    /// <summary>
    /// Sets the swapchain parameters for a window.
    /// </summary>
    /// <param name="window">The window to configure.</param>
    /// <param name="composition">The swapchain color space composition.</param>
    /// <param name="presentMode">The presentation/vsync mode.</param>
    public void SetSwapchainParameters(Window window, GpuSwapchainComposition composition, GpuPresentMode presentMode) =>
        Check(SDL_SetGPUSwapchainParameters(Handle, window.Handle, (SDL_GPUSwapchainComposition)composition, (SDL_GPUPresentMode)presentMode));

    /// <summary>
    /// Sets the maximum number of frames allowed in flight at once.
    /// </summary>
    /// <param name="count">The maximum number of frames in flight.</param>
    public void SetAllowedFramesInFlight(uint count) => Check(SDL_SetGPUAllowedFramesInFlight(Handle, count));

    /// <summary>
    /// Gets the texture format used by the swapchain for a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The swapchain texture format.</returns>
    public GpuTextureFormat GetSwapchainTextureFormat(Window window) =>
        (GpuTextureFormat)SDL_GetGPUSwapchainTextureFormat(Handle, window.Handle);

    /// <summary>
    /// Checks whether a window's swapchain supports the given present mode.
    /// </summary>
    /// <param name="window">The claimed window.</param>
    /// <param name="presentMode">The present mode to check.</param>
    /// <returns>True if supported.</returns>
    public bool WindowSupportsPresentMode(Window window, GpuPresentMode presentMode) =>
        SDL_WindowSupportsGPUPresentMode(Handle, window.Handle, (SDL_GPUPresentMode)presentMode);

    /// <summary>
    /// Checks whether a window's swapchain supports the given composition.
    /// </summary>
    /// <param name="window">The claimed window.</param>
    /// <param name="composition">The swapchain composition to check.</param>
    /// <returns>True if supported.</returns>
    public bool WindowSupportsSwapchainComposition(Window window, GpuSwapchainComposition composition) =>
        SDL_WindowSupportsGPUSwapchainComposition(Handle, window.Handle, (SDL_GPUSwapchainComposition)composition);

    /// <summary>
    /// Blocks until the GPU is idle.
    /// </summary>
    public void WaitForIdle() => Check(SDL_WaitForGPUIdle(Handle));

    /// <summary>
    /// Blocks until a swapchain texture is available for a window.
    /// </summary>
    /// <param name="window">The window to wait for.</param>
    public void WaitForSwapchain(Window window) => Check(SDL_WaitForGPUSwapchain(Handle, window.Handle));

    /// <summary>
    /// Checks whether the device supports a given texture format with the specified type and usage.
    /// </summary>
    /// <param name="format">The texture format to query.</param>
    /// <param name="type">The texture dimensionality type.</param>
    /// <param name="usage">The intended texture usage flags.</param>
    /// <returns>True if the format is supported; false otherwise.</returns>
    public bool SupportsTextureFormat(GpuTextureFormat format, GpuTextureType type, GpuTextureUsage usage) =>
        SDL_GPUTextureSupportsFormat(Handle, (SDL_GPUTextureFormat)format, (SDL_GPUTextureType)type, (SDL_GPUTextureUsageFlags)usage);

    /// <summary>
    /// Checks whether the device supports a given sample count for a texture format.
    /// </summary>
    /// <param name="format">The texture format to query.</param>
    /// <param name="sampleCount">The sample count to check.</param>
    /// <returns>True if the sample count is supported; false otherwise.</returns>
    public bool SupportsSampleCount(GpuTextureFormat format, GpuSampleCount sampleCount) =>
        SDL_GPUTextureSupportsSampleCount(Handle, (SDL_GPUTextureFormat)format, (SDL_GPUSampleCount)sampleCount);

    private int _dependentCount;
    private readonly Stack<IDisposable> _dependentLeases = new();
    internal void ValidateDependentOwner() { _ = Handle; if (!_ownsHandle) throw new ArgumentException("Pass the original owning SDL wrapper; borrowed aliases cannot retain native lifetime."); }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The lease is transferred to the returned scope or lifetime cleanup, which releases it exactly once.")]
    internal void RetainDependent() { ValidateDependentOwner(); _dependentLeases.Push(Lifetime.Retain()); _dependentCount++; }
    internal void ReleaseDependent() { _dependentLeases.Pop().Dispose(); _dependentCount--; }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_dependentCount != 0) throw new InvalidOperationException("Dispose dependent backends before this resource.");
        if (_ownsHandle && _handle != null && Lifetime.IsAlive)
        {
            foreach (var owner in Claims.Keys) owner.Check();
            Lifetime.Release(() =>
            {
            foreach (var claim in Claims.Values) SDL_ReleaseWindowFromGPUDevice(_handle, (SDL_Window*)claim.Pointer);
            SDL_DestroyGPUDevice(_handle);
            foreach (var claim in Claims.Values) claim.Lease.Dispose();
            Claims.Clear();
            });
        }
        _handle = null;
    }
    /// <summary>Checks whether a device with these creation properties is supported.</summary>
    public static bool Supports(PropertyGroup properties) => SDL_GPUSupportsProperties(properties.Id);
}
