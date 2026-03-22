namespace SdlSharp.Graphics;

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
