namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies what happens to a stored stencil value if stencil tests fail or pass.
/// </summary>
public enum GpuStencilOp
{
    /// <summary>Invalid stencil operation.</summary>
    Invalid,

    /// <summary>Keeps the current value.</summary>
    Keep,

    /// <summary>Sets the value to 0.</summary>
    Zero,

    /// <summary>Sets the value to reference.</summary>
    Replace,

    /// <summary>Increments the current value and clamps to the maximum value.</summary>
    IncrementAndClamp,

    /// <summary>Decrements the current value and clamps to 0.</summary>
    DecrementAndClamp,

    /// <summary>Bitwise-inverts the current value.</summary>
    Invert,

    /// <summary>Increments the current value and wraps back to 0.</summary>
    IncrementAndWrap,

    /// <summary>Decrements the current value and wraps to the maximum value.</summary>
    DecrementAndWrap
}
