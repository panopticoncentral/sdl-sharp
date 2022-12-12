namespace SdlSharp.Graphics
{
    /// <summary>
    /// The normalized factor used to multiply pixel components.
    /// </summary>
    public enum BlendFactor
    {
        /// <summary>
        /// Zero.
        /// </summary>
        Zero = Native.SDL_BlendFactor.SDL_BLENDFACTOR_ZERO,

        /// <summary>
        /// One.
        /// </summary>
        One = Native.SDL_BlendFactor.SDL_BLENDFACTOR_ONE,

        /// <summary>
        /// Source color.
        /// </summary>
        SourceColor = Native.SDL_BlendFactor.SDL_BLENDFACTOR_SRC_COLOR,

        /// <summary>
        /// One minus source color.
        /// </summary>
        OneMinusSourceColor = Native.SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_SRC_COLOR,

        /// <summary>
        /// Source alpha.
        /// </summary>
        SourceAlpha = Native.SDL_BlendFactor.SDL_BLENDFACTOR_SRC_ALPHA,

        /// <summary>
        /// One minus source alpha.
        /// </summary>
        OneMinusSourceAlpha = Native.SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_SRC_ALPHA,

        /// <summary>
        /// Destination color.
        /// </summary>
        DestinationColor = Native.SDL_BlendFactor.SDL_BLENDFACTOR_DST_COLOR,

        /// <summary>
        /// One minus destination color.
        /// </summary>
        OneMinusDestinationColor = Native.SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_DST_COLOR,

        /// <summary>
        /// Destination alpha.
        /// </summary>
        DestinationAlpha = Native.SDL_BlendFactor.SDL_BLENDFACTOR_DST_ALPHA,

        /// <summary>
        /// One minus destination alpha.
        /// </summary>
        OneMinusDestinationAlpha = Native.SDL_BlendFactor.SDL_BLENDFACTOR_ONE_MINUS_DST_ALPHA
    }
}
