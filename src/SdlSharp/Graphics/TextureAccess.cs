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
}
