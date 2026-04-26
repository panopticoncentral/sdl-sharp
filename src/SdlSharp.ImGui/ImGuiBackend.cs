using SdlSharp.Graphics;
using SdlSharp.Graphics.Gpu;
using SdlSharp.Native;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Manages the Dear ImGui SDL3 platform and SDL_GPU renderer backends.
/// </summary>
public static unsafe class ImGuiBackend
{
    /// <summary>
    /// Initializes both the SDL3 platform backend and the SDL_GPU renderer backend.
    /// </summary>
    /// <param name="window">The SDL window.</param>
    /// <param name="device">The GPU device.</param>
    /// <param name="colorTargetFormat">The swapchain color format. Use <see cref="GpuDevice.GetSwapchainTextureFormat"/>.</param>
    /// <param name="msaaSamples">MSAA sample count (default 1 = no MSAA).</param>
    public static void Init(Window window, GpuDevice device, GpuTextureFormat colorTargetFormat, GpuSampleCount msaaSamples = GpuSampleCount.One)
    {
        IGSharp_ImplSDL3_InitForSDLGPU(window.Handle);
        IGSharp_ImplSDLGPU3_Init(device.Handle, (int)colorTargetFormat, (int)msaaSamples);

        // Hook into Application's raw event filter so ImGui processes events
        Application.RawEventFilter += OnRawEvent;
    }

    /// <summary>
    /// Starts a new ImGui frame. Call once per frame before submitting ImGui commands.
    /// </summary>
    public static void NewFrame()
    {
        IGSharp_ImplSDLGPU3_NewFrame();
        IGSharp_ImplSDL3_NewFrame();
        IGSharp_NewFrame();
    }

    /// <summary>
    /// Uploads vertex/index buffers to the GPU. Must be called BEFORE <see cref="GpuCommandBuffer.BeginRenderPass"/>.
    /// </summary>
    /// <param name="drawData">The opaque draw data pointer from <see cref="ImGui.GetDrawData"/>.</param>
    /// <param name="commandBuffer">The GPU command buffer.</param>
    public static void PrepareDrawData(void* drawData, GpuCommandBuffer commandBuffer)
    {
        IGSharp_ImplSDLGPU3_PrepareDrawData(drawData, commandBuffer.Handle);
    }

    /// <summary>
    /// Renders ImGui draw data into the active render pass. Must be called AFTER <see cref="PrepareDrawData"/>
    /// and inside an active render pass.
    /// </summary>
    /// <param name="drawData">The opaque draw data pointer from <see cref="ImGui.GetDrawData"/>.</param>
    /// <param name="commandBuffer">The GPU command buffer.</param>
    /// <param name="renderPass">The active GPU render pass.</param>
    public static void RenderDrawData(void* drawData, GpuCommandBuffer commandBuffer, GpuRenderPass renderPass)
    {
        IGSharp_ImplSDLGPU3_RenderDrawData(drawData, commandBuffer.Handle, renderPass.Handle);
    }

    /// <summary>
    /// Shuts down both backends and unhooks the event filter.
    /// </summary>
    public static void Shutdown()
    {
        Application.RawEventFilter -= OnRawEvent;
        IGSharp_ImplSDLGPU3_Shutdown();
        IGSharp_ImplSDL3_Shutdown();
    }

    private static void OnRawEvent(SDL_Event* e)
    {
        IGSharp_ImplSDL3_ProcessEvent(e);
    }
}
