namespace SdlSharp.Graphics
{
    /// <summary>
    /// Axes for flipping a render.
    /// </summary>
    public enum RendererFlip
    {
        /// <summary>
        /// None
        /// </summary>
        None = Native.SDL_RendererFlip.SDL_FLIP_NONE,

        /// <summary>
        /// Horizontal
        /// </summary>
        Horizontal = Native.SDL_RendererFlip.SDL_FLIP_HORIZONTAL,

        /// <summary>
        /// Vertical
        /// </summary>
        Vertical = Native.SDL_RendererFlip.SDL_FLIP_VERTICAL
    }
}
