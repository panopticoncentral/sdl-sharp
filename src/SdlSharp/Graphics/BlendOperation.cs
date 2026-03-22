namespace SdlSharp.Graphics;

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
