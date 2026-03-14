using static SdlSharp.Native.BlendMode;

namespace SdlSharp.Graphics;

/// <summary>
/// A blend mode, either a predefined standard mode or a custom-composed mode.
/// Custom blend modes from <see cref="Compose"/> may produce arbitrary uint values,
/// so this is a value type rather than an enum.
/// </summary>
public readonly record struct BlendMode(uint Value)
{
    /// <summary>No blending.</summary>
    public static readonly BlendMode None = new((uint)Native.SDL_BlendMode.SDL_BLENDMODE_NONE);
    /// <summary>Alpha blending.</summary>
    public static readonly BlendMode Blend = new((uint)Native.SDL_BlendMode.SDL_BLENDMODE_BLEND);
    /// <summary>Pre-multiplied alpha blending.</summary>
    public static readonly BlendMode BlendPremultiplied = new((uint)Native.SDL_BlendMode.SDL_BLENDMODE_BLEND_PREMULTIPLIED);
    /// <summary>Additive blending.</summary>
    public static readonly BlendMode Add = new((uint)Native.SDL_BlendMode.SDL_BLENDMODE_ADD);
    /// <summary>Pre-multiplied additive blending.</summary>
    public static readonly BlendMode AddPremultiplied = new((uint)Native.SDL_BlendMode.SDL_BLENDMODE_ADD_PREMULTIPLIED);
    /// <summary>Color modulate.</summary>
    public static readonly BlendMode Mod = new((uint)Native.SDL_BlendMode.SDL_BLENDMODE_MOD);
    /// <summary>Color multiply.</summary>
    public static readonly BlendMode Mul = new((uint)Native.SDL_BlendMode.SDL_BLENDMODE_MUL);

    /// <summary>
    /// Compose a custom blend mode for renderers.
    /// </summary>
    public static BlendMode Compose(
        BlendFactor srcColorFactor,
        BlendFactor dstColorFactor,
        BlendOperation colorOperation,
        BlendFactor srcAlphaFactor,
        BlendFactor dstAlphaFactor,
        BlendOperation alphaOperation) =>
        new((uint)SDL_ComposeCustomBlendMode(
            (Native.SDL_BlendFactor)srcColorFactor,
            (Native.SDL_BlendFactor)dstColorFactor,
            (Native.SDL_BlendOperation)colorOperation,
            (Native.SDL_BlendFactor)srcAlphaFactor,
            (Native.SDL_BlendFactor)dstAlphaFactor,
            (Native.SDL_BlendOperation)alphaOperation));

    internal Native.SDL_BlendMode ToNative() => (Native.SDL_BlendMode)Value;

    internal static BlendMode FromNative(Native.SDL_BlendMode m) => new((uint)m);
}

/// <summary>
/// The blend operation used when combining source and destination pixel components.
/// </summary>
public enum BlendOperation
{
    /// <summary>dst + src.</summary>
    Add = (int)Native.SDL_BlendOperation.SDL_BLENDOPERATION_ADD,
    /// <summary>src - dst.</summary>
    Subtract = (int)Native.SDL_BlendOperation.SDL_BLENDOPERATION_SUBTRACT,
    /// <summary>dst - src.</summary>
    RevSubtract = (int)Native.SDL_BlendOperation.SDL_BLENDOPERATION_REV_SUBTRACT,
    /// <summary>min(dst, src).</summary>
    Minimum = (int)Native.SDL_BlendOperation.SDL_BLENDOPERATION_MINIMUM,
    /// <summary>max(dst, src).</summary>
    Maximum = (int)Native.SDL_BlendOperation.SDL_BLENDOPERATION_MAXIMUM,
}

/// <summary>
/// The normalized factor used to multiply pixel components.
/// </summary>
public enum BlendFactor
{
    /// <summary>0, 0, 0, 0.</summary>
    Zero = (int)Native.SDL_BlendFactor.SDL_BLENDFACTOR_ZERO,
    /// <summary>1, 1, 1, 1.</summary>
    One = (int)Native.SDL_BlendFactor.SDL_BLENDFACTOR_ONE,
    /// <summary>srcR, srcG, srcB, srcA.</summary>
    SrcColor = (int)Native.SDL_BlendFactor.SDL_BLENDFACTOR_SRC_COLOR,
    /// <summary>1-srcR, 1-srcG, 1-srcB, 1-srcA.</summary>
    OneMinusSrcColor = (int)Native.SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_SRC_COLOR,
    /// <summary>srcA, srcA, srcA, srcA.</summary>
    SrcAlpha = (int)Native.SDL_BlendFactor.SDL_BLENDFACTOR_SRC_ALPHA,
    /// <summary>1-srcA, 1-srcA, 1-srcA, 1-srcA.</summary>
    OneMinusSrcAlpha = (int)Native.SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_SRC_ALPHA,
    /// <summary>dstR, dstG, dstB, dstA.</summary>
    DstColor = (int)Native.SDL_BlendFactor.SDL_BLENDFACTOR_DST_COLOR,
    /// <summary>1-dstR, 1-dstG, 1-dstB, 1-dstA.</summary>
    OneMinusDstColor = (int)Native.SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_DST_COLOR,
    /// <summary>dstA, dstA, dstA, dstA.</summary>
    DstAlpha = (int)Native.SDL_BlendFactor.SDL_BLENDFACTOR_DST_ALPHA,
    /// <summary>1-dstA, 1-dstA, 1-dstA, 1-dstA.</summary>
    OneMinusDstAlpha = (int)Native.SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_DST_ALPHA,
}
