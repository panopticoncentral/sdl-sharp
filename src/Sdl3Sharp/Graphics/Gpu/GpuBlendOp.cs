namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies the operator to be used when pixels in a render target are blended with existing pixels in the texture.
/// </summary>
public enum GpuBlendOp
{
    /// <summary>Invalid blend operation.</summary>
    Invalid,

    /// <summary>(source * source_factor) + (destination * destination_factor).</summary>
    Add,

    /// <summary>(source * source_factor) - (destination * destination_factor).</summary>
    Subtract,

    /// <summary>(destination * destination_factor) - (source * source_factor).</summary>
    ReverseSubtract,

    /// <summary>min(source, destination).</summary>
    Min,

    /// <summary>max(source, destination).</summary>
    Max
}
