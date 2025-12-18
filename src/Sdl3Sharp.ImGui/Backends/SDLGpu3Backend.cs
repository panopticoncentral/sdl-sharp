using Sdl3Sharp.Graphics.Gpu;
using Sdl3Sharp.ImGui.Native;
using Sdl3Sharp.ImGui.Native.Backends;

using ImGuiNative = Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui.Backends;

/// <summary>
/// Provides SDL GPU renderer backend initialization and rendering for Dear ImGui.
/// </summary>
/// <remarks>
/// This backend provides GPU-accelerated rendering of ImGui draw data using SDL_GPU.
/// Use this for high-performance rendering with modern graphics APIs.
/// </remarks>
public static unsafe class SDLGpu3Backend
{
    /// <summary>
    /// Initializes the SDL GPU renderer backend.
    /// </summary>
    /// <param name="device">The GPU device to use for rendering.</param>
    /// <param name="colorTargetFormat">The texture format for color targets. Use <see cref="GpuDevice.GetSwapchainTextureFormat"/> to query the correct format for swapchain rendering.</param>
    /// <param name="msaaSamples">The number of MSAA samples to use.</param>
    /// <returns><c>true</c> if initialization was successful; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Call this after initializing the SDL3 platform backend and before the main loop.
    /// </remarks>
    public static bool Init(GpuDevice device, GpuTextureFormat colorTargetFormat, GpuSampleCount msaaSamples = GpuSampleCount.Count1)
    {
        ArgumentNullException.ThrowIfNull(device);
        var info = new ImGui_ImplSDLGPU3_InitInfo
        {
            Device = device.Handle,
            ColorTargetFormat = (Sdl3Sharp.Native.Gpu.SDL_GPUTextureFormat)colorTargetFormat,
            MSAASamples = (Sdl3Sharp.Native.Gpu.SDL_GPUSampleCount)msaaSamples
        };
        return ImGuiSdl3Gpu.Init(&info);
    }

    /// <summary>
    /// Initializes the SDL GPU renderer backend with multi-viewport support.
    /// </summary>
    /// <param name="device">The GPU device to use for rendering.</param>
    /// <param name="colorTargetFormat">The texture format for color targets.</param>
    /// <param name="msaaSamples">The number of MSAA samples to use.</param>
    /// <param name="swapchainComposition">The swapchain composition mode (used for multi-viewports).</param>
    /// <param name="presentMode">The present mode (used for multi-viewports).</param>
    /// <returns><c>true</c> if initialization was successful; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Use this overload when enabling ImGui multi-viewport support.
    /// </remarks>
    public static bool Init(GpuDevice device, GpuTextureFormat colorTargetFormat, GpuSampleCount msaaSamples, GpuSwapchainComposition swapchainComposition, GpuPresentMode presentMode)
    {
        ArgumentNullException.ThrowIfNull(device);
        var info = new ImGui_ImplSDLGPU3_InitInfo
        {
            Device = device.Handle,
            ColorTargetFormat = (Sdl3Sharp.Native.Gpu.SDL_GPUTextureFormat)colorTargetFormat,
            MSAASamples = (Sdl3Sharp.Native.Gpu.SDL_GPUSampleCount)msaaSamples,
            SwapchainComposition = (Sdl3Sharp.Native.Gpu.SDL_GPUSwapchainComposition)swapchainComposition,
            PresentMode = (Sdl3Sharp.Native.Gpu.SDL_GPUPresentMode)presentMode
        };
        return ImGuiSdl3Gpu.Init(&info);
    }

    /// <summary>
    /// Shuts down the SDL GPU renderer backend.
    /// </summary>
    /// <remarks>
    /// Call this before shutting down the SDL3 platform backend during application shutdown.
    /// </remarks>
    public static void Shutdown()
    {
        ImGuiSdl3Gpu.Shutdown();
    }

    /// <summary>
    /// Starts a new ImGui frame for the SDL GPU renderer backend.
    /// </summary>
    /// <remarks>
    /// Call this at the beginning of each frame, after calling <see cref="SDL3Backend.NewFrame"/>
    /// and before calling <see cref="Context.NewFrame"/>.
    /// </remarks>
    public static void NewFrame()
    {
        ImGuiSdl3Gpu.NewFrame();
    }

    /// <summary>
    /// Prepares ImGui draw data for GPU rendering.
    /// </summary>
    /// <param name="commandBuffer">The command buffer to record preparation commands into.</param>
    /// <remarks>
    /// <para>Call this after <see cref="Context.Render"/> to prepare the draw data for rendering.
    /// This uploads vertex/index buffers and updates textures as needed.</para>
    /// <para>You must call this before <see cref="RenderDrawData"/> and before beginning a render pass.</para>
    /// </remarks>
    public static void PrepareDrawData(GpuCommandBuffer commandBuffer)
    {
        ArgumentNullException.ThrowIfNull(commandBuffer);
        ImDrawData* drawData = ImGuiNative.ImGui_GetDrawData();
        ImGuiSdl3Gpu.PrepareDrawData(drawData, commandBuffer.Handle);
    }

    /// <summary>
    /// Renders ImGui draw data using the GPU renderer.
    /// </summary>
    /// <param name="commandBuffer">The command buffer to record render commands into.</param>
    /// <param name="renderPass">The active render pass to render into.</param>
    /// <remarks>
    /// Call this within an active render pass after calling <see cref="PrepareDrawData"/>.
    /// Uses the default ImGui graphics pipeline.
    /// </remarks>
    public static void RenderDrawData(GpuCommandBuffer commandBuffer, GpuRenderPass renderPass)
    {
        ArgumentNullException.ThrowIfNull(commandBuffer);
        ArgumentNullException.ThrowIfNull(renderPass);
        ImDrawData* drawData = ImGuiNative.ImGui_GetDrawData();
        ImGuiSdl3Gpu.RenderDrawDataEx(drawData, commandBuffer.Handle, renderPass.Handle, null);
    }

    /// <summary>
    /// Renders ImGui draw data using the GPU renderer with a custom pipeline.
    /// </summary>
    /// <param name="commandBuffer">The command buffer to record render commands into.</param>
    /// <param name="renderPass">The active render pass to render into.</param>
    /// <param name="pipeline">A custom graphics pipeline to use instead of the default ImGui pipeline, or <c>null</c> to use the default.</param>
    /// <remarks>
    /// Call this within an active render pass after calling <see cref="PrepareDrawData"/>.
    /// </remarks>
    public static void RenderDrawData(GpuCommandBuffer commandBuffer, GpuRenderPass renderPass, GpuGraphicsPipeline? pipeline)
    {
        ArgumentNullException.ThrowIfNull(commandBuffer);
        ArgumentNullException.ThrowIfNull(renderPass);
        ImDrawData* drawData = ImGuiNative.ImGui_GetDrawData();
        ImGuiSdl3Gpu.RenderDrawDataEx(drawData, commandBuffer.Handle, renderPass.Handle, pipeline != null ? pipeline.Handle : null);
    }

    /// <summary>
    /// Creates the GPU device objects required for rendering.
    /// </summary>
    /// <remarks>
    /// Use this if you need to reset your rendering device without losing Dear ImGui state.
    /// After calling <see cref="DestroyDeviceObjects"/> and resetting your device, call this
    /// method to recreate the necessary GPU resources.
    /// </remarks>
    public static void CreateDeviceObjects()
    {
        ImGuiSdl3Gpu.CreateDeviceObjects();
    }

    /// <summary>
    /// Destroys the GPU device objects used for rendering.
    /// </summary>
    /// <remarks>
    /// Use this if you need to reset your rendering device without losing Dear ImGui state.
    /// After calling this, reset your device, then call <see cref="CreateDeviceObjects"/>.
    /// </remarks>
    public static void DestroyDeviceObjects()
    {
        ImGuiSdl3Gpu.DestroyDeviceObjects();
    }

    /// <summary>
    /// Manually updates a texture for the GPU renderer.
    /// </summary>
    /// <param name="textureData">A pointer to the texture data to update.</param>
    /// <remarks>
    /// <para>This is an advanced function for precisely controlling texture update timing.</para>
    /// <para>Use this if you need staged rendering by setting <c>ImDrawData.Textures = NULL</c>
    /// and handling texture updates manually.</para>
    /// </remarks>
    public static void UpdateTexture(ImTextureData* textureData)
    {
        ImGuiSdl3Gpu.UpdateTexture(textureData);
    }
}
