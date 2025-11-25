namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies which color components are written in a graphics pipeline.
/// </summary>
[Flags]
public enum GpuColorComponent : byte
{
    /// <summary>No components.</summary>
    None = 0,

    /// <summary>The red component.</summary>
    R = 1 << 0,

    /// <summary>The green component.</summary>
    G = 1 << 1,

    /// <summary>The blue component.</summary>
    B = 1 << 2,

    /// <summary>The alpha component.</summary>
    A = 1 << 3,

    /// <summary>All components (RGBA).</summary>
    All = R | G | B | A
}
