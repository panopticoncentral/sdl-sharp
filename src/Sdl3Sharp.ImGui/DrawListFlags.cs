namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for ImDrawList instance.
/// </summary>
/// <remarks>
/// These are set automatically by ImGui functions from ImGuiIO settings, and generally not manipulated directly.
/// It is however possible to temporarily alter flags between calls to DrawList methods.
/// </remarks>
[Flags]
public enum DrawListFlags
{
    /// <summary>
    /// No flags.
    /// </summary>
    None = 0,

    /// <summary>
    /// Enable anti-aliased lines/borders.
    /// </summary>
    /// <remarks>
    /// Uses *2 the number of triangles for 1.0f wide line or lines thin enough to be drawn using textures, otherwise *3 the number of triangles.
    /// </remarks>
    AntiAliasedLines = 1 << 0,

    /// <summary>
    /// Enable anti-aliased lines/borders using textures when possible.
    /// </summary>
    /// <remarks>
    /// Requires backend to render with bilinear filtering (NOT point/nearest filtering).
    /// </remarks>
    AntiAliasedLinesUseTex = 1 << 1,

    /// <summary>
    /// Enable anti-aliased edge around filled shapes (rounded rectangles, circles).
    /// </summary>
    AntiAliasedFill = 1 << 2,

    /// <summary>
    /// Can emit 'VtxOffset > 0' to allow large meshes.
    /// </summary>
    /// <remarks>
    /// Set when 'ImGuiBackendFlags_RendererHasVtxOffset' is enabled.
    /// </remarks>
    AllowVtxOffset = 1 << 3
}