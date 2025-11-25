namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies a blending factor to be used when pixels in a render target are blended with existing pixels in the texture.
/// </summary>
public enum GpuBlendFactor
{
    /// <summary>Invalid blend factor.</summary>
    Invalid,

    /// <summary>Factor is 0.</summary>
    Zero,

    /// <summary>Factor is 1.</summary>
    One,

    /// <summary>Factor is source color.</summary>
    SrcColor,

    /// <summary>Factor is 1 - source color.</summary>
    OneMinusSrcColor,

    /// <summary>Factor is destination color.</summary>
    DstColor,

    /// <summary>Factor is 1 - destination color.</summary>
    OneMinusDstColor,

    /// <summary>Factor is source alpha.</summary>
    SrcAlpha,

    /// <summary>Factor is 1 - source alpha.</summary>
    OneMinusSrcAlpha,

    /// <summary>Factor is destination alpha.</summary>
    DstAlpha,

    /// <summary>Factor is 1 - destination alpha.</summary>
    OneMinusDstAlpha,

    /// <summary>Factor is blend constant.</summary>
    ConstantColor,

    /// <summary>Factor is 1 - blend constant.</summary>
    OneMinusConstantColor,

    /// <summary>Factor is min(source alpha, 1 - destination alpha).</summary>
    SrcAlphaSaturate
}
