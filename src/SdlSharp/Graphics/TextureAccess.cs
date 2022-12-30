namespace SdlSharp.Graphics
{
    /// <summary>
    /// Access pattern for a texture.
    /// </summary>
    public enum TextureAccess
    {
        /// <summary>
        /// Changes rarely, not lockable.
        /// </summary>
        Static = Native.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC,

        /// <summary>
        /// Changes frequently, lockable.
        /// </summary>
        Streaming = Native.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING,

        /// <summary>
        /// Texture can be used as a render target.
        /// </summary>
        Target = Native.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET
    }
}
