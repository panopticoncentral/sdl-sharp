using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// SDL_BlendMode is a Uint32 typedef with #define constants, not a C enum.
// We define it as a C# enum for convenience, but custom blend modes from
// SDL_ComposeCustomBlendMode may produce values outside this enum.

/// <summary>
/// Predefined blend mode values.
/// </summary>
public enum SDL_BlendMode : uint
{
    /// <summary>No blending: dstRGBA = srcRGBA.</summary>
    SDL_BLENDMODE_NONE = 0x00000000u,
    /// <summary>Alpha blending.</summary>
    SDL_BLENDMODE_BLEND = 0x00000001u,
    /// <summary>Pre-multiplied alpha blending.</summary>
    SDL_BLENDMODE_BLEND_PREMULTIPLIED = 0x00000010u,
    /// <summary>Additive blending.</summary>
    SDL_BLENDMODE_ADD = 0x00000002u,
    /// <summary>Pre-multiplied additive blending.</summary>
    SDL_BLENDMODE_ADD_PREMULTIPLIED = 0x00000020u,
    /// <summary>Color modulate.</summary>
    SDL_BLENDMODE_MOD = 0x00000004u,
    /// <summary>Color multiply.</summary>
    SDL_BLENDMODE_MUL = 0x00000008u,
    /// <summary>Invalid blend mode.</summary>
    SDL_BLENDMODE_INVALID = 0x7FFFFFFFu,
}

/// <summary>
/// The blend operation used when combining source and destination pixel components.
/// </summary>
public enum SDL_BlendOperation
{
    /// <summary>dst + src.</summary>
    SDL_BLENDOPERATION_ADD = 0x1,
    /// <summary>src - dst.</summary>
    SDL_BLENDOPERATION_SUBTRACT = 0x2,
    /// <summary>dst - src.</summary>
    SDL_BLENDOPERATION_REV_SUBTRACT = 0x3,
    /// <summary>min(dst, src).</summary>
    SDL_BLENDOPERATION_MINIMUM = 0x4,
    /// <summary>max(dst, src).</summary>
    SDL_BLENDOPERATION_MAXIMUM = 0x5,
}

/// <summary>
/// The normalized factor used to multiply pixel components.
/// </summary>
public enum SDL_BlendFactor
{
    /// <summary>0, 0, 0, 0.</summary>
    SDL_BLENDFACTOR_ZERO = 0x1,
    /// <summary>1, 1, 1, 1.</summary>
    SDL_BLENDFACTOR_ONE = 0x2,
    /// <summary>srcR, srcG, srcB, srcA.</summary>
    SDL_BLENDFACTOR_SRC_COLOR = 0x3,
    /// <summary>1-srcR, 1-srcG, 1-srcB, 1-srcA.</summary>
    SDL_BLENDFACTOR_ONE_MINUS_SRC_COLOR = 0x4,
    /// <summary>srcA, srcA, srcA, srcA.</summary>
    SDL_BLENDFACTOR_SRC_ALPHA = 0x5,
    /// <summary>1-srcA, 1-srcA, 1-srcA, 1-srcA.</summary>
    SDL_BLENDFACTOR_ONE_MINUS_SRC_ALPHA = 0x6,
    /// <summary>dstR, dstG, dstB, dstA.</summary>
    SDL_BLENDFACTOR_DST_COLOR = 0x7,
    /// <summary>1-dstR, 1-dstG, 1-dstB, 1-dstA.</summary>
    SDL_BLENDFACTOR_ONE_MINUS_DST_COLOR = 0x8,
    /// <summary>dstA, dstA, dstA, dstA.</summary>
    SDL_BLENDFACTOR_DST_ALPHA = 0x9,
    /// <summary>1-dstA, 1-dstA, 1-dstA, 1-dstA.</summary>
    SDL_BLENDFACTOR_ONE_MINUS_DST_ALPHA = 0xA,
}

/// <summary>
/// Native bindings for SDL_blendmode.h — blend mode operations.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class BlendMode
{
    /// <summary>
    /// Compose a custom blend mode for renderers.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ComposeCustomBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_BlendMode SDL_ComposeCustomBlendMode(
        SDL_BlendFactor srcColorFactor,
        SDL_BlendFactor dstColorFactor,
        SDL_BlendOperation colorOperation,
        SDL_BlendFactor srcAlphaFactor,
        SDL_BlendFactor dstAlphaFactor,
        SDL_BlendOperation alphaOperation);
}
