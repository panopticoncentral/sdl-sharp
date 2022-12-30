namespace SdlSharp.Graphics
{
    /// <summary>
    /// Flags for a renderer.
    /// </summary>
    [Flags]
    public enum RendererOptions
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Renders in software.
        /// </summary>
        Software = Native.SDL_RendererFlags.SDL_RENDERER_SOFTWARE,

        /// <summary>
        /// Renders in hardware.
        /// </summary>
        Accelerated = Native.SDL_RendererFlags.SDL_RENDERER_ACCELERATED,

        /// <summary>
        /// Present is synchronized with the vertical sync
        /// </summary>
        PresentVSync = Native.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC,

        /// <summary>
        /// Supports rendering to a texture.
        /// </summary>
        TargetTexture = Native.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE
    }
}
