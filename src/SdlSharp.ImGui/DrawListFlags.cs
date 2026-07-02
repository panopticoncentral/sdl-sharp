namespace SdlSharp.ImGui;

/// <summary>Flags controlling <see cref="DrawList"/> behavior (anti-aliasing, vertex offsets). Mirrors ImDrawListFlags.</summary>
[Flags]
public enum DrawListFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Enable anti-aliased lines/borders (*2 the number of triangles for 1.0f wide line or lines thin enough to be drawn using textures, otherwise *3 the number of triangles).</summary>
    AntiAliasedLines = 1 << 0,
    /// <summary>Enable anti-aliased lines/borders using textures when possible.</summary>
    AntiAliasedLinesUseTex = 1 << 1,
    /// <summary>Enable anti-aliased edges around filled shapes (rounded rectangles, circles).</summary>
    AntiAliasedFill = 1 << 2,
    /// <summary>Can emit 'VtxOffset > 0' to allow large meshes.</summary>
    AllowVtxOffset = 1 << 3,
}
