namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies a comparison operator for depth, stencil and sampler operations.
/// </summary>
public enum GpuCompareOp
{
    /// <summary>Invalid compare operation.</summary>
    Invalid,

    /// <summary>The comparison always evaluates false.</summary>
    Never,

    /// <summary>The comparison evaluates reference less than test.</summary>
    Less,

    /// <summary>The comparison evaluates reference equal to test.</summary>
    Equal,

    /// <summary>The comparison evaluates reference less than or equal to test.</summary>
    LessOrEqual,

    /// <summary>The comparison evaluates reference greater than test.</summary>
    Greater,

    /// <summary>The comparison evaluates reference not equal to test.</summary>
    NotEqual,

    /// <summary>The comparison evaluates reference greater than or equal to test.</summary>
    GreaterOrEqual,

    /// <summary>The comparison always evaluates true.</summary>
    Always
}
