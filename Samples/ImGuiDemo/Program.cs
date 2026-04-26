// ImGuiDemo sample — creates an SDL3 window with GPU device and renders
// the Dear ImGui demo window using the SDL_GPU backend.

using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Graphics.Gpu;
using SdlSharp.ImGui;
using SdlSharp.Native;
using WindowFlags = SdlSharp.Graphics.WindowFlags;

unsafe
{
    using var app = new Application(InitFlags.Video);

    using var window = Window.Create("ImGui Demo", 1280, 800,
        WindowFlags.Resizable | WindowFlags.HighPixelDensity);

    using var device = GpuDevice.Create(
        GpuShaderFormat.Spirv | GpuShaderFormat.Msl | GpuShaderFormat.Dxil | GpuShaderFormat.MetalLib,
        debugMode: true);

    device.ClaimWindow(window);

    var swapFormat = device.GetSwapchainTextureFormat(window);

    // Initialize ImGui
    using var ctx = ImGuiContext.Create();
    ImGui.StyleColorsDark();

    // Initialize backends
    ImGuiBackend.Init(window, device, swapFormat);

    var showDemo = true;

    while (!Application.DispatchEvents())
    {
        // Start ImGui frame
        ImGuiBackend.NewFrame();

        // Show demo window
        ImGui.ShowDemoWindow(ref showDemo);

        // Render ImGui
        ImGui.Render();
        var drawData = ImGui.GetDrawData();

        // GPU rendering
        var cmdBuf = device.AcquireCommandBuffer();

        if (cmdBuf.WaitAndAcquireSwapchainTexture(window, out var swapTex, out _, out _) && swapTex != null)
        {
            // Upload vertex/index buffers BEFORE starting the render pass
            ImGuiBackend.PrepareDrawData(drawData, cmdBuf);

            var colorTarget = new SDL_GPUColorTargetInfo
            {
                texture = swapTex,
                clear_color = new SDL_FColor { r = 0.45f, g = 0.55f, b = 0.60f, a = 1.0f },
                load_op = SDL_GPULoadOp.SDL_GPU_LOADOP_CLEAR,
                store_op = SDL_GPUStoreOp.SDL_GPU_STOREOP_STORE,
            };

            var renderPass = cmdBuf.BeginRenderPass(&colorTarget, 1);

            // Render ImGui draw commands inside the render pass
            ImGuiBackend.RenderDrawData(drawData, cmdBuf, renderPass);

            renderPass.End();
        }

        cmdBuf.Submit();
    }

    device.WaitForIdle();
    ImGuiBackend.Shutdown();
}
