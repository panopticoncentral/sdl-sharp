namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies behavior of texture sampling when the coordinates exceed the 0-1 range.
/// </summary>
public enum GpuSamplerAddressMode
{
    /// <summary>Specifies that the coordinates will wrap around.</summary>
    Repeat,

    /// <summary>Specifies that the coordinates will wrap around mirrored.</summary>
    MirroredRepeat,

    /// <summary>Specifies that the coordinates will clamp to the 0-1 range.</summary>
    ClampToEdge
}
