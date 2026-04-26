namespace SdlSharp.ImGui;

/// <summary>Backend capabilities flags stored in io.BackendFlags.</summary>
[Flags]
public enum BackendFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Backend Platform supports gamepad and currently has one connected.</summary>
    HasGamepad = 1 << 0,
    /// <summary>Backend Platform supports honoring GetMouseCursor() to change the OS cursor shape.</summary>
    HasMouseCursors = 1 << 1,
    /// <summary>Backend Platform supports io.WantSetMousePos requests.</summary>
    HasSetMousePos = 1 << 2,
    /// <summary>Backend Renderer supports ImDrawCmd::VtxOffset (enables meshes with 64K+ vertices).</summary>
    RendererHasVtxOffset = 1 << 3,
    /// <summary>Backend Renderer supports ImTextureData requests.</summary>
    RendererHasTextures = 1 << 4,
}
