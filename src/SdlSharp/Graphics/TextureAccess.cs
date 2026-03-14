namespace SdlSharp.Graphics;

/// <summary>
/// The access pattern allowed for a texture.
/// </summary>
public enum TextureAccess
{
    /// <summary>Changes rarely, not lockable.</summary>
    Static = (int)Native.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC,
    /// <summary>Changes frequently, lockable.</summary>
    Streaming = (int)Native.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING,
    /// <summary>Texture can be used as a render target.</summary>
    Target = (int)Native.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET,
}
