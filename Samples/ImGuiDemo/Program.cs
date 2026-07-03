// ImGuiDemo sample — creates an SDL3 window with GPU device and renders
// the Dear ImGui demo window using the SDL_GPU backend.
//
// Two demo windows are shown side-by-side:
//   - The native ImGui::ShowDemoWindow (C++ canonical demo).
//   - The C# port in DemoWindow.cs, used as a coverage test of the
//     SdlSharp.ImGui wrappers.

using ImGuiDemo;
using SdlSharp;
using SdlSharp.Graphics;
using SdlSharp.Graphics.Gpu;
using SdlSharp.ImGui;
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
    using var ctx = Context.Create();
    ImGui.StyleColorsDark();

    // Initialize backends
    ImGuiBackend.Init(window, device, swapFormat);

    var showDemo = true;
    var showCSharpDemo = true;

    while (!Application.DispatchEvents())
    {
        // Start ImGui frame
        ImGuiBackend.NewFrame();

        // Show the native and ported demo windows side-by-side.
        if (showDemo) ImGui.ShowDemoWindow(ref showDemo);
        if (showCSharpDemo) DemoWindow.Show(ref showCSharpDemo);

        // Render ImGui
        ImGui.Render();
        var drawData = ImGui.GetDrawData();

        // GPU rendering
        var cmdBuf = device.AcquireCommandBuffer();

        if (cmdBuf.WaitAndAcquireSwapchainTexture(window, out _) is { } swapTex)
        {
            // Upload vertex/index buffers BEFORE starting the render pass
            ImGuiBackend.PrepareDrawData(drawData, cmdBuf);

            var colorTarget = new GpuColorTargetInfo
            {
                Texture = swapTex,
                ClearColor = new FColor(0.45f, 0.55f, 0.60f, 1.0f),
                LoadOp = GpuLoadOp.Clear,
                StoreOp = GpuStoreOp.Store,
            };

            var renderPass = cmdBuf.BeginRenderPass(colorTarget);

            // Render ImGui draw commands inside the render pass
            ImGuiBackend.RenderDrawData(drawData, cmdBuf, renderPass);

            renderPass.End();
        }

        cmdBuf.Submit();
    }

    device.WaitForIdle();
    ImGuiBackend.Shutdown();
}
