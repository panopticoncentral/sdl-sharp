using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents an SDL GPU device context for 3D graphics and compute operations.
/// </summary>
public sealed unsafe class GpuDevice : IDisposable
{
    private bool _disposed;

    /// <summary>
    /// Gets the underlying SDL_GPUDevice pointer.
    /// </summary>
    public SDL_GPUDevice* Handle { get; private set; }

    /// <summary>
    /// Gets the name of the GPU driver backend used by this device.
    /// </summary>
    public string? Driver
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetGPUDeviceDriver(Handle);
        }
    }

    /// <summary>
    /// Gets the supported shader formats for this GPU context.
    /// </summary>
    public GpuShaderFormat SupportedShaderFormats
    {
        get
        {
            ThrowIfDisposed();
            return (GpuShaderFormat)SDL_GetGPUShaderFormats(Handle);
        }
    }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPUDevice pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPUDevice pointer.</param>
    internal GpuDevice(SDL_GPUDevice* handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// Gets the number of GPU drivers compiled into SDL.
    /// </summary>
    public static int DriverCount => SDL_GetNumGPUDrivers();

    /// <summary>
    /// Gets the name of a built-in GPU driver.
    /// </summary>
    /// <param name="index">The index of the GPU driver.</param>
    /// <returns>The name of the GPU driver.</returns>
    public static string? GetDriverName(int index)
    {
        return SDL_GetGPUDriver(index);
    }

    /// <summary>
    /// Checks for GPU runtime support with the specified shader formats.
    /// </summary>
    /// <param name="formatFlags">A bitmask of shader formats you can provide.</param>
    /// <param name="name">The preferred GPU driver, or null to let SDL pick the optimal driver.</param>
    /// <returns>True if supported, false otherwise.</returns>
    public static bool SupportsShaderFormats(GpuShaderFormat formatFlags, string? name = null)
    {
        return SDL_GPUSupportsShaderFormats((SDL_GPUShaderFormat)formatFlags, name);
    }

    /// <summary>
    /// Checks for GPU runtime support with the specified properties.
    /// </summary>
    /// <param name="properties">The properties to use.</param>
    /// <returns>True if supported, false otherwise.</returns>
    public static bool SupportsProperties(PropertyGroup properties)
    {
        return SDL_GPUSupportsProperties(properties.Id);
    }

    /// <summary>
    /// Creates a GPU device context.
    /// </summary>
    /// <param name="formatFlags">A bitmask of shader formats you can provide.</param>
    /// <param name="debugMode">Enable debug mode properties and validations.</param>
    /// <param name="name">The preferred GPU driver, or null to let SDL pick the optimal driver.</param>
    /// <returns>A new GPU device.</returns>
    public static GpuDevice Create(GpuShaderFormat formatFlags, bool debugMode = false, string? name = null)
    {
        return new GpuDevice(CheckErrorPointer(SDL_CreateGPUDevice((SDL_GPUShaderFormat)formatFlags, debugMode, name)));
    }

    /// <summary>
    /// Creates a GPU device context with properties.
    /// </summary>
    /// <param name="properties">The properties to use.</param>
    /// <returns>A new GPU device.</returns>
    public static GpuDevice Create(PropertyGroup properties)
    {
        return new GpuDevice(CheckErrorPointer(SDL_CreateGPUDeviceWithProperties(properties.Id)));
    }

    /// <summary>
    /// Claims a window, creating a swapchain structure for it.
    /// </summary>
    /// <param name="window">The window to claim.</param>
    public void ClaimWindow(Window window)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_ClaimWindowForGPUDevice(Handle, window.Handle));
    }

    /// <summary>
    /// Unclaims a window, destroying its swapchain structure.
    /// </summary>
    /// <param name="window">The window to release.</param>
    public void ReleaseWindow(Window window)
    {
        ThrowIfDisposed();
        SDL_ReleaseWindowFromGPUDevice(Handle, window.Handle);
    }

    /// <summary>
    /// Determines whether a swapchain composition is supported by the window.
    /// </summary>
    /// <param name="window">The window to check.</param>
    /// <param name="composition">The swapchain composition to check.</param>
    /// <returns>True if supported, false otherwise.</returns>
    public bool WindowSupportsSwapchainComposition(Window window, GpuSwapchainComposition composition)
    {
        ThrowIfDisposed();
        return SDL_WindowSupportsGPUSwapchainComposition(Handle, window.Handle, (SDL_GPUSwapchainComposition)composition);
    }

    /// <summary>
    /// Determines whether a presentation mode is supported by the window.
    /// </summary>
    /// <param name="window">The window to check.</param>
    /// <param name="presentMode">The presentation mode to check.</param>
    /// <returns>True if supported, false otherwise.</returns>
    public bool WindowSupportsPresentMode(Window window, GpuPresentMode presentMode)
    {
        ThrowIfDisposed();
        return SDL_WindowSupportsGPUPresentMode(Handle, window.Handle, (SDL_GPUPresentMode)presentMode);
    }

    /// <summary>
    /// Changes the swapchain parameters for the given claimed window.
    /// </summary>
    /// <param name="window">The window that has been claimed.</param>
    /// <param name="composition">The desired composition of the swapchain.</param>
    /// <param name="presentMode">The desired present mode for the swapchain.</param>
    public void SetSwapchainParameters(Window window, GpuSwapchainComposition composition, GpuPresentMode presentMode)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetGPUSwapchainParameters(Handle, window.Handle, (SDL_GPUSwapchainComposition)composition, (SDL_GPUPresentMode)presentMode));
    }

    /// <summary>
    /// Configures the maximum allowed number of frames in flight.
    /// </summary>
    /// <param name="allowedFramesInFlight">The maximum number of frames that can be pending on the GPU.</param>
    public void SetAllowedFramesInFlight(uint allowedFramesInFlight)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetGPUAllowedFramesInFlight(Handle, allowedFramesInFlight));
    }

    /// <summary>
    /// Obtains the texture format of the swapchain for the given window.
    /// </summary>
    /// <param name="window">The window that has been claimed.</param>
    /// <returns>The texture format of the swapchain.</returns>
    public GpuTextureFormat GetSwapchainTextureFormat(Window window)
    {
        ThrowIfDisposed();
        return (GpuTextureFormat)SDL_GetGPUSwapchainTextureFormat(Handle, window.Handle);
    }

    /// <summary>
    /// Blocks the thread until a swapchain texture is available to be acquired.
    /// </summary>
    /// <param name="window">The window that has been claimed.</param>
    public void WaitForSwapchain(Window window)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WaitForGPUSwapchain(Handle, window.Handle));
    }

    /// <summary>
    /// Acquires a command buffer for recording commands.
    /// </summary>
    /// <returns>A new command buffer.</returns>
    public GpuCommandBuffer AcquireCommandBuffer()
    {
        ThrowIfDisposed();
        return new GpuCommandBuffer(CheckErrorPointer(SDL_AcquireGPUCommandBuffer(Handle)), this);
    }

    /// <summary>
    /// Blocks the thread until the GPU is completely idle.
    /// </summary>
    public void WaitForIdle()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_WaitForGPUIdle(Handle));
    }

    /// <summary>
    /// Determines whether a texture format is supported for a given type and usage.
    /// </summary>
    /// <param name="format">The texture format to check.</param>
    /// <param name="type">The type of texture (2D, 3D, Cube).</param>
    /// <param name="usage">A bitmask of all usage scenarios to check.</param>
    /// <returns>Whether the texture format is supported for this type and usage.</returns>
    public bool TextureSupportsFormat(GpuTextureFormat format, GpuTextureType type, GpuTextureUsage usage)
    {
        ThrowIfDisposed();
        return SDL_GPUTextureSupportsFormat(Handle, (SDL_GPUTextureFormat)format, (SDL_GPUTextureType)type, (SDL_GPUTextureUsageFlags)usage);
    }

    /// <summary>
    /// Determines if a sample count for a texture format is supported.
    /// </summary>
    /// <param name="format">The texture format to check.</param>
    /// <param name="sampleCount">The sample count to check.</param>
    /// <returns>Whether the sample count is supported for this texture format.</returns>
    public bool TextureSupportsSampleCount(GpuTextureFormat format, GpuSampleCount sampleCount)
    {
        ThrowIfDisposed();
        return SDL_GPUTextureSupportsSampleCount(Handle, (SDL_GPUTextureFormat)format, (SDL_GPUSampleCount)sampleCount);
    }

    /// <summary>
    /// Obtains the texel block size for a texture format.
    /// </summary>
    /// <param name="format">The texture format.</param>
    /// <returns>The texel block size of the texture format.</returns>
    public static uint GetTextureFormatTexelBlockSize(GpuTextureFormat format)
    {
        return SDL_GPUTextureFormatTexelBlockSize((SDL_GPUTextureFormat)format);
    }

    /// <summary>
    /// Calculate the size in bytes of a texture format with dimensions.
    /// </summary>
    /// <param name="format">A texture format.</param>
    /// <param name="width">Width in pixels.</param>
    /// <param name="height">Height in pixels.</param>
    /// <param name="depthOrLayerCount">Depth for 3D textures or layer count otherwise.</param>
    /// <returns>The size of a texture with this format and dimensions.</returns>
    public static uint CalculateTextureFormatSize(GpuTextureFormat format, uint width, uint height, uint depthOrLayerCount)
    {
        return SDL_CalculateGPUTextureFormatSize((SDL_GPUTextureFormat)format, width, height, depthOrLayerCount);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (Handle != null)
        {
            SDL_DestroyGPUDevice(Handle);
        }

        Handle = null;
        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
