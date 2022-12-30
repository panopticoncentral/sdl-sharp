namespace SdlSharp.Graphics
{
    /// <summary>
    /// The mode for a scaling operation.
    /// </summary>
    public enum ScaleMode
    {
        /// <summary>
        /// Nearest pixel sampling.
        /// </summary>
        Nearest = Native.SDL_ScaleMode.SDL_ScaleModeNearest,

        /// <summary>
        /// Linear filtering.
        /// </summary>
        Linear = Native.SDL_ScaleMode.SDL_ScaleModeLinear,

        /// <summary>
        /// Anisotropic filtering.
        /// </summary>
        Best = Native.SDL_ScaleMode.SDL_ScaleModeBest
    }
}
