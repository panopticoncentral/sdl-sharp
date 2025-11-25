namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies the sample count of a texture.
/// </summary>
public enum GpuSampleCount
{
    /// <summary>No multisampling.</summary>
    Count1,

    /// <summary>MSAA 2x.</summary>
    Count2,

    /// <summary>MSAA 4x.</summary>
    Count4,

    /// <summary>MSAA 8x.</summary>
    Count8
}
