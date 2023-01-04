namespace SdlSharp.Graphics
{
    /// <summary>
    /// The orientation of a display.
    /// </summary>
    public enum DisplayOrientation
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = Native.SDL_DisplayOrientation.SDL_ORIENTATION_UNKNOWN,

        /// <summary>
        /// Landscape
        /// </summary>
        Landscape = Native.SDL_DisplayOrientation.SDL_ORIENTATION_LANDSCAPE,

        /// <summary>
        /// Flipped landscape
        /// </summary>
        LandscapeFlipped = Native.SDL_DisplayOrientation.SDL_ORIENTATION_LANDSCAPE_FLIPPED,

        /// <summary>
        /// Portrait
        /// </summary>
        Portrait = Native.SDL_DisplayOrientation.SDL_ORIENTATION_PORTRAIT,

        /// <summary>
        /// Flipped portrait
        /// </summary>
        PortraitFlipped = Native.SDL_DisplayOrientation.SDL_ORIENTATION_PORTRAIT_FLIPPED
    }
}
