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
    /// <summary>
    /// The underlying native SDL_GPUDevice pointer.
    /// </summary>
    internal SDL_GPUDevice* Handle { get; private set; }

    private GpuDevice(SDL_GPUDevice* handle) { Handle = handle; }

    /// <summary>
    /// Creates a new GPU device with the specified shader format support.
    /// </summary>
    /// <param name="shaderFormats">The shader formats the device should support.</param>
    /// <param name="debugMode">Whether to enable GPU debug mode.</param>
    /// <param name="preferredBackend">An optional preferred GPU backend name, or null for auto-selection.</param>
    /// <returns>A new GPU device.</returns>
    public static GpuDevice Create(GpuShaderFormat shaderFormats, bool debugMode = false, string? preferredBackend = null) =>
        new(Check(SDL_CreateGPUDevice((SDL_GPUShaderFormat)shaderFormats, debugMode, ToUtf8(preferredBackend))));

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
    /// Creates a GPU shader from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The shader creation parameters.</param>
    /// <returns>A new GPU shader.</returns>
    public GpuShader CreateShader(in SDL_GPUShaderCreateInfo createInfo)
    {
        fixed (SDL_GPUShaderCreateInfo* p = &createInfo)
            return new GpuShader(this, Check(SDL_CreateGPUShader(Handle, p)));
    }

    /// <summary>
    /// Creates a graphics pipeline from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The graphics pipeline creation parameters.</param>
    /// <returns>A new GPU graphics pipeline.</returns>
    public GpuGraphicsPipeline CreateGraphicsPipeline(in SDL_GPUGraphicsPipelineCreateInfo createInfo)
    {
        fixed (SDL_GPUGraphicsPipelineCreateInfo* p = &createInfo)
            return new GpuGraphicsPipeline(this, Check(SDL_CreateGPUGraphicsPipeline(Handle, p)));
    }

    /// <summary>
    /// Creates a compute pipeline from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The compute pipeline creation parameters.</param>
    /// <returns>A new GPU compute pipeline.</returns>
    public GpuComputePipeline CreateComputePipeline(in SDL_GPUComputePipelineCreateInfo createInfo)
    {
        fixed (SDL_GPUComputePipelineCreateInfo* p = &createInfo)
            return new GpuComputePipeline(this, Check(SDL_CreateGPUComputePipeline(Handle, p)));
    }

    /// <summary>
    /// Creates a texture sampler from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The sampler creation parameters.</param>
    /// <returns>A new GPU sampler.</returns>
    public GpuSampler CreateSampler(in SDL_GPUSamplerCreateInfo createInfo)
    {
        fixed (SDL_GPUSamplerCreateInfo* p = &createInfo)
            return new GpuSampler(this, Check(SDL_CreateGPUSampler(Handle, p)));
    }

    /// <summary>
    /// Creates a GPU texture from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The texture creation parameters.</param>
    /// <returns>A new GPU texture.</returns>
    public GpuTexture CreateTexture(in SDL_GPUTextureCreateInfo createInfo)
    {
        fixed (SDL_GPUTextureCreateInfo* p = &createInfo)
            return new GpuTexture(this, Check(SDL_CreateGPUTexture(Handle, p)));
    }

    /// <summary>
    /// Creates a GPU buffer from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The buffer creation parameters.</param>
    /// <returns>A new GPU buffer.</returns>
    public GpuBuffer CreateBuffer(in SDL_GPUBufferCreateInfo createInfo)
    {
        fixed (SDL_GPUBufferCreateInfo* p = &createInfo)
            return new GpuBuffer(this, Check(SDL_CreateGPUBuffer(Handle, p)));
    }

    /// <summary>
    /// Creates a GPU transfer buffer from the specified creation info.
    /// </summary>
    /// <param name="createInfo">The transfer buffer creation parameters.</param>
    /// <returns>A new GPU transfer buffer.</returns>
    public GpuTransferBuffer CreateTransferBuffer(in SDL_GPUTransferBufferCreateInfo createInfo)
    {
        fixed (SDL_GPUTransferBufferCreateInfo* p = &createInfo)
            return new GpuTransferBuffer(this, Check(SDL_CreateGPUTransferBuffer(Handle, p)));
    }

    /// <summary>
    /// Acquires a command buffer for recording GPU commands.
    /// </summary>
    /// <returns>A new command buffer.</returns>
    public GpuCommandBuffer AcquireCommandBuffer() => new(this, Check(SDL_AcquireGPUCommandBuffer(Handle)));

    /// <summary>
    /// Claims a window for GPU presentation on this device.
    /// </summary>
    /// <param name="window">The window to claim.</param>
    public void ClaimWindow(Window window) => Check(SDL_ClaimWindowForGPUDevice(Handle, window.Handle));

    /// <summary>
    /// Releases a window from GPU presentation on this device.
    /// </summary>
    /// <param name="window">The window to release.</param>
    public void ReleaseWindow(Window window) => SDL_ReleaseWindowFromGPUDevice(Handle, window.Handle);

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

    /// <inheritdoc/>
    public void Dispose()
    {
        if (Handle != null)
        {
            SDL_DestroyGPUDevice(Handle);
            Handle = null;
        }
    }
}
