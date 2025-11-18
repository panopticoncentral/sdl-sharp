using static Sdl3Sharp.Native.BlendMode;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// A mode for blending textures and drawing operations.
/// </summary>
public readonly record struct BlendMode
{
    /// <summary>
    /// No blending: dstRGBA = srcRGBA.
    /// </summary>
    public static readonly BlendMode None = new(SDL_BlendMode.SDL_BLENDMODE_NONE);

    /// <summary>
    /// Alpha blending: dstRGB = (srcRGB * srcA) + (dstRGB * (1-srcA)), dstA = srcA + (dstA * (1-srcA)).
    /// </summary>
    public static readonly BlendMode Blend = new(SDL_BlendMode.SDL_BLENDMODE_BLEND);

    /// <summary>
    /// Pre-multiplied alpha blending: dstRGBA = srcRGBA + (dstRGBA * (1-srcA)).
    /// </summary>
    public static readonly BlendMode BlendPremultiplied = new(SDL_BlendMode.SDL_BLENDMODE_BLEND_PREMULTIPLIED);

    /// <summary>
    /// Additive blending: dstRGB = (srcRGB * srcA) + dstRGB, dstA = dstA.
    /// </summary>
    public static readonly BlendMode Add = new(SDL_BlendMode.SDL_BLENDMODE_ADD);

    /// <summary>
    /// Pre-multiplied additive blending: dstRGB = srcRGB + dstRGB, dstA = dstA.
    /// </summary>
    public static readonly BlendMode AddPremultiplied = new(SDL_BlendMode.SDL_BLENDMODE_ADD_PREMULTIPLIED);

    /// <summary>
    /// Color modulate: dstRGB = srcRGB * dstRGB, dstA = dstA.
    /// </summary>
    public static readonly BlendMode Modulate = new(SDL_BlendMode.SDL_BLENDMODE_MOD);

    /// <summary>
    /// Color multiply: dstRGB = (srcRGB * dstRGB) + (dstRGB * (1-srcA)), dstA = dstA.
    /// </summary>
    public static readonly BlendMode Multiply = new(SDL_BlendMode.SDL_BLENDMODE_MUL);

    /// <summary>
    /// Invalid blend mode.
    /// </summary>
    public static readonly BlendMode Invalid = new(SDL_BlendMode.SDL_BLENDMODE_INVALID);

    private readonly SDL_BlendMode _mode;

    internal BlendMode(SDL_BlendMode mode)
    {
        _mode = mode;
    }

    /// <summary>
    /// Creates a custom blending mode.
    /// </summary>
    /// <param name="sourceColorFactor">The blend factor applied to the red, green, and blue components of the source pixels.</param>
    /// <param name="destinationColorFactor">The blend factor applied to the red, green, and blue components of the destination pixels.</param>
    /// <param name="colorOperation">The blend operation used to combine the red, green, and blue components of the source and destination pixels.</param>
    /// <param name="sourceAlphaFactor">The blend factor applied to the alpha component of the source pixels.</param>
    /// <param name="destinationAlphaFactor">The blend factor applied to the alpha component of the destination pixels.</param>
    /// <param name="alphaOperation">The blend operation used to combine the alpha component of the source and destination pixels.</param>
    /// <returns>The custom blend mode.</returns>
    public static BlendMode Custom(BlendFactor sourceColorFactor, BlendFactor destinationColorFactor, BlendOperation colorOperation, BlendFactor sourceAlphaFactor, BlendFactor destinationAlphaFactor, BlendOperation alphaOperation)
    {
        return new(SDL_ComposeCustomBlendMode((SDL_BlendFactor)sourceColorFactor, (SDL_BlendFactor)destinationColorFactor, (SDL_BlendOperation)colorOperation, (SDL_BlendFactor)sourceAlphaFactor, (SDL_BlendFactor)destinationAlphaFactor, (SDL_BlendOperation)alphaOperation));
    }

    internal SDL_BlendMode ToNative()
    {
        return _mode;
    }
}
