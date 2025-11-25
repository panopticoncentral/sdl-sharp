namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies how the contents of a texture attached to a render pass are treated at the beginning of the render pass.
/// </summary>
public enum GpuLoadOp
{
    /// <summary>The previous contents of the texture will be preserved.</summary>
    Load,

    /// <summary>The contents of the texture will be cleared to a color.</summary>
    Clear,

    /// <summary>The previous contents of the texture need not be preserved. The contents will be undefined.</summary>
    DontCare
}
