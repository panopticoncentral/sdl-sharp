using static Sdl3Sharp.Native.BlendMode;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// The blend operation used when combining source and destination pixel components.
/// </summary>
public enum BlendOperation
{
    /// <summary>dst + src: supported by all renderers.</summary>
    Add = SDL_BlendOperation.SDL_BLENDOPERATION_ADD,
    /// <summary>src - dst: supported by D3D, OpenGL, OpenGLES, and Vulkan.</summary>
    Subtract = SDL_BlendOperation.SDL_BLENDOPERATION_SUBTRACT,
    /// <summary>dst - src: supported by D3D, OpenGL, OpenGLES, and Vulkan.</summary>
    ReverseSubtract = SDL_BlendOperation.SDL_BLENDOPERATION_REV_SUBTRACT,
    /// <summary>min(dst, src): supported by D3D, OpenGL, OpenGLES, and Vulkan.</summary>
    Minimum = SDL_BlendOperation.SDL_BLENDOPERATION_MINIMUM,
    /// <summary>max(dst, src): supported by D3D, OpenGL, OpenGLES, and Vulkan.</summary>
    Maximum = SDL_BlendOperation.SDL_BLENDOPERATION_MAXIMUM
}
