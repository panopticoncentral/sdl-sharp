namespace Sdl3Sharp.Graphics;

/// <summary>
/// The access pattern allowed for a texture.
/// </summary>
public enum TextureAccess
{
    /// <summary>
    /// Changes rarely, not lockable.
    /// </summary>
    Static,

    /// <summary>
    /// Changes frequently, lockable.
    /// </summary>
    Streaming,

    /// <summary>
    /// Texture can be used as a render target.
    /// </summary>
    Target
}
