using static Sdl3Sharp.Native.BlendMode;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// The normalized factor used to multiply pixel components.
/// </summary>
public enum BlendFactor
{
    /// <summary>0, 0, 0, 0.</summary>
    Zero = SDL_BlendFactor.SDL_BLENDFACTOR_ZERO,
    /// <summary>1, 1, 1, 1.</summary>
    One = SDL_BlendFactor.SDL_BLENDFACTOR_ONE,
    /// <summary>srcR, srcG, srcB, srcA.</summary>
    SourceColor = SDL_BlendFactor.SDL_BLENDFACTOR_SRC_COLOR,
    /// <summary>1-srcR, 1-srcG, 1-srcB, 1-srcA.</summary>
    OneMinusSourceColor = SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_SRC_COLOR,
    /// <summary>srcA, srcA, srcA, srcA.</summary>
    SourceAlpha = SDL_BlendFactor.SDL_BLENDFACTOR_SRC_ALPHA,
    /// <summary>1-srcA, 1-srcA, 1-srcA, 1-srcA.</summary>
    OneMinusSourceAlpha = SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_SRC_ALPHA,
    /// <summary>dstR, dstG, dstB, dstA.</summary>
    DestinationColor = SDL_BlendFactor.SDL_BLENDFACTOR_DST_COLOR,
    /// <summary>1-dstR, 1-dstG, 1-dstB, 1-dstA.</summary>
    OneMinusDestinationColor = SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_DST_COLOR,
    /// <summary>dstA, dstA, dstA, dstA.</summary>
    DestinationAlpha = SDL_BlendFactor.SDL_BLENDFACTOR_DST_ALPHA,
    /// <summary>1-dstA, 1-dstA, 1-dstA, 1-dstA.</summary>
    OneMinusDestinationAlpha = SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_DST_ALPHA
}
