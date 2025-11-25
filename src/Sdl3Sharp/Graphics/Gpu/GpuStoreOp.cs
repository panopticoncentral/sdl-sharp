namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies how the contents of a texture attached to a render pass are treated at the end of the render pass.
/// </summary>
public enum GpuStoreOp
{
    /// <summary>The contents generated during the render pass will be written to memory.</summary>
    Store,

    /// <summary>The contents generated during the render pass are not needed and may be discarded.</summary>
    DontCare,

    /// <summary>The multisample contents generated during the render pass will be resolved to a non-multisample texture.</summary>
    Resolve,

    /// <summary>The multisample contents generated during the render pass will be resolved to a non-multisample texture. The contents in the multisample texture will be written to memory.</summary>
    ResolveAndStore
}
