using Sdl3Sharp.Graphics;
using Sdl3Sharp.ImGui.Native;
using Sdl3Sharp.ImGui.Native.Backends;

using ImGuiNative = Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui.Backends;

/// <summary>
/// Provides SDL Renderer backend initialization and rendering for Dear ImGui.
/// </summary>
/// <remarks>
/// This backend provides rendering of ImGui draw data using SDL_Renderer.
/// This is a simpler, CPU-assisted rendering path suitable for basic applications.
/// </remarks>
public static unsafe class SDLRenderer3Backend
{
    /// <summary>
    /// Initializes the SDL Renderer backend.
    /// </summary>
    /// <param name="renderer">The SDL renderer to use for rendering.</param>
    /// <returns><c>true</c> if initialization was successful; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Call this after initializing the SDL3 platform backend and before the main loop.
    /// </remarks>
    public static bool Init(Renderer renderer)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        return ImGuiSdl3Renderer.ImGuiSdl3RendererInit(renderer.Handle);
    }

    /// <summary>
    /// Shuts down the SDL Renderer backend.
    /// </summary>
    /// <remarks>
    /// Call this before shutting down the SDL3 platform backend during application shutdown.
    /// </remarks>
    public static void Shutdown()
    {
        ImGuiSdl3Renderer.ImGuiSdl3RendererShutdown();
    }

    /// <summary>
    /// Starts a new ImGui frame for the SDL Renderer backend.
    /// </summary>
    /// <remarks>
    /// Call this at the beginning of each frame, after calling <see cref="SDL3Backend.NewFrame"/>
    /// and before calling <see cref="Context.NewFrame"/>.
    /// </remarks>
    public static void NewFrame()
    {
        ImGuiSdl3Renderer.ImGuiSdl3RendererNewFrame();
    }

    /// <summary>
    /// Renders ImGui draw data using the SDL Renderer.
    /// </summary>
    /// <param name="renderer">The SDL renderer to render with.</param>
    /// <remarks>
    /// Call this after <see cref="Context.Render"/> to render the ImGui frame.
    /// This should be called before presenting the renderer output.
    /// </remarks>
    public static void RenderDrawData(Renderer renderer)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        ImDrawData* drawData = ImGuiNative.ImGui_GetDrawData();
        ImGuiSdl3Renderer.ImGuiSdl3RendererRenderDrawData(drawData, renderer.Handle);
    }

    /// <summary>
    /// Creates the device objects required for rendering.
    /// </summary>
    /// <remarks>
    /// This is typically called automatically by <see cref="Init"/> and <see cref="NewFrame"/>.
    /// You only need to call this manually if you've called <see cref="DestroyDeviceObjects"/>
    /// and need to recreate the resources.
    /// </remarks>
    public static void CreateDeviceObjects()
    {
        ImGuiSdl3Renderer.ImGuiSdl3RendererCreateDeviceObjects();
    }

    /// <summary>
    /// Destroys the device objects used for rendering.
    /// </summary>
    /// <remarks>
    /// This is typically called automatically by <see cref="Shutdown"/>.
    /// You may call this manually if you need to reset rendering resources.
    /// </remarks>
    public static void DestroyDeviceObjects()
    {
        ImGuiSdl3Renderer.ImGuiSdl3RendererDestroyDeviceObjects();
    }

    /// <summary>
    /// Manually updates a texture for the SDL Renderer.
    /// </summary>
    /// <param name="textureData">A pointer to the texture data to update.</param>
    /// <remarks>
    /// <para>This is an advanced function for precisely controlling texture update timing.</para>
    /// <para>Use this if you need staged rendering by setting <c>ImDrawData.Textures = NULL</c>
    /// and handling texture updates manually.</para>
    /// </remarks>
    public static void UpdateTexture(ImTextureData* textureData)
    {
        ImGuiSdl3Renderer.ImGuiSdl3RendererUpdateTexture(textureData);
    }
}
