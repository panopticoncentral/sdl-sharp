using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Backend capabilities flags for Dear ImGui.
/// </summary>
[Flags]
public enum BackendFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiBackendFlags.None,

    /// <summary>
    /// Backend Platform supports gamepad and currently has one connected.
    /// </summary>
    HasGamepad = ImGuiBackendFlags.HasGamepad,

    /// <summary>
    /// Backend Platform supports honoring GetMouseCursor() value to change the OS cursor shape.
    /// </summary>
    HasMouseCursors = ImGuiBackendFlags.HasMouseCursors,

    /// <summary>
    /// Backend Platform supports io.WantSetMousePos requests to reposition the OS mouse position
    /// (only used if <see cref="IO.ConfigNavMoveSetMousePos"/> is set).
    /// </summary>
    HasSetMousePos = ImGuiBackendFlags.HasSetMousePos,

    /// <summary>
    /// Backend Renderer supports ImDrawCmd::VtxOffset. This enables output of large meshes (64K+ vertices)
    /// while still using 16-bit indices.
    /// </summary>
    RendererHasVtxOffset = ImGuiBackendFlags.RendererHasVtxOffset,

    /// <summary>
    /// Backend Renderer supports ImTextureData requests to create/update/destroy textures.
    /// This enables incremental texture updates and texture reloads.
    /// </summary>
    RendererHasTextures = ImGuiBackendFlags.RendererHasTextures
}
