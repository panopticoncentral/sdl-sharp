using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SdlSharp.Native;
using static SdlSharp.ImGui.Native.ImGui;
// ReSharper disable InconsistentNaming

namespace SdlSharp.ImGui.Native;

/// <summary>
/// Native P/Invoke bindings for imgui_sharp SDL3 and SDL_GPU backend functions.
/// </summary>
public static unsafe partial class ImGuiBackend
{
    // --- SDL3 Platform Backend ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDL3_InitForSDLGPU")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ImplSDL3_InitForSDLGPU(SDL_Window* window);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDL3_Shutdown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDL3_Shutdown();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDL3_NewFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDL3_NewFrame();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDL3_ProcessEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ImplSDL3_ProcessEvent(SDL_Event* sdl_event);

    // --- SDL_GPU Renderer Backend ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDLGPU3_Init")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ImplSDLGPU3_Init(SDL_GPUDevice* device, int color_target_format, int msaa_samples);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDLGPU3_Shutdown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDLGPU3_Shutdown();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDLGPU3_NewFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDLGPU3_NewFrame();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDLGPU3_PrepareDrawData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDLGPU3_PrepareDrawData(void* draw_data, SDL_GPUCommandBuffer* command_buffer);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDLGPU3_RenderDrawData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDLGPU3_RenderDrawData(void* draw_data, SDL_GPUCommandBuffer* command_buffer, SDL_GPURenderPass* render_pass);
}
