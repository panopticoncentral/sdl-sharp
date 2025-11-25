namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies the vertex winding that will cause a triangle to be determined to be front-facing.
/// </summary>
public enum GpuFrontFace
{
    /// <summary>A triangle with counter-clockwise vertex winding will be considered front-facing.</summary>
    CounterClockwise,

    /// <summary>A triangle with clockwise vertex winding will be considered front-facing.</summary>
    Clockwise
}
