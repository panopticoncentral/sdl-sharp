namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies the facing direction in which triangle faces will be culled.
/// </summary>
public enum GpuCullMode
{
    /// <summary>No triangles are culled.</summary>
    None,

    /// <summary>Front-facing triangles are culled.</summary>
    Front,

    /// <summary>Back-facing triangles are culled.</summary>
    Back
}
