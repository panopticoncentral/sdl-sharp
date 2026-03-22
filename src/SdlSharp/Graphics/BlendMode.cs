using static SdlSharp.Native.BlendMode;

namespace SdlSharp.Graphics;

/// <summary>
/// A blend mode used for drawing operations.
/// </summary>
public enum BlendMode : uint
{
    /// <summary>No blending.</summary>
    None = (uint)Native.SDL_BlendMode.SDL_BLENDMODE_NONE,
    /// <summary>Alpha blending.</summary>
    Blend = (uint)Native.SDL_BlendMode.SDL_BLENDMODE_BLEND,
    /// <summary>Pre-multiplied alpha blending.</summary>
    BlendPremultiplied = (uint)Native.SDL_BlendMode.SDL_BLENDMODE_BLEND_PREMULTIPLIED,
    /// <summary>Additive blending.</summary>
    Add = (uint)Native.SDL_BlendMode.SDL_BLENDMODE_ADD,
    /// <summary>Pre-multiplied additive blending.</summary>
    AddPremultiplied = (uint)Native.SDL_BlendMode.SDL_BLENDMODE_ADD_PREMULTIPLIED,
    /// <summary>Color modulate.</summary>
    Mod = (uint)Native.SDL_BlendMode.SDL_BLENDMODE_MOD,
    /// <summary>Color multiply.</summary>
    Mul = (uint)Native.SDL_BlendMode.SDL_BLENDMODE_MUL,
}

/// <summary>
/// Extension methods for <see cref="BlendMode"/>.
/// </summary>
public static class BlendModeExtensions
{
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
        (BlendMode)(uint)SDL_ComposeCustomBlendMode(
            (Native.SDL_BlendFactor)srcColorFactor,
            (Native.SDL_BlendFactor)dstColorFactor,
            (Native.SDL_BlendOperation)colorOperation,
            (Native.SDL_BlendFactor)srcAlphaFactor,
            (Native.SDL_BlendFactor)dstAlphaFactor,
            (Native.SDL_BlendOperation)alphaOperation);
}
