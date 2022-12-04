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
        None = 0x0,

        /// <summary>
        /// Renders in software.
        /// </summary>
        Software = 0x1,

        /// <summary>
        /// Renders in hardware.
        /// </summary>
        Accelerated = 0x2,

        /// <summary>
        /// Present is synchronized with the vertical sync
        /// </summary>
        PresentVSync = 0x4,

        /// <summary>
        /// Supports rendering to a texture.
        /// </summary>
        TargetTexture = 0x8
    }
}
