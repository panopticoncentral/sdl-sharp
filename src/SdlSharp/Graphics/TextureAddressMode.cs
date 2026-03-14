using SdlSharp.Native;

namespace SdlSharp.Graphics;

/// <summary>
/// The texture address mode for texture coordinates.
/// </summary>
public enum TextureAddressMode
{
    /// <summary>Invalid address mode.</summary>
    Invalid = (int)SDL_TextureAddressMode.SDL_TEXTURE_ADDRESS_INVALID,
    /// <summary>Wrapping is enabled if texture coordinates are outside [0, 1], this is the default.</summary>
    Auto = (int)SDL_TextureAddressMode.SDL_TEXTURE_ADDRESS_AUTO,
    /// <summary>Texture coordinates are clamped to the [0, 1] range.</summary>
    Clamp = (int)SDL_TextureAddressMode.SDL_TEXTURE_ADDRESS_CLAMP,
    /// <summary>The texture is repeated (tiled).</summary>
    Wrap = (int)SDL_TextureAddressMode.SDL_TEXTURE_ADDRESS_WRAP,
}
