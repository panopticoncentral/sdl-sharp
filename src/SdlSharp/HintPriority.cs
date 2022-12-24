namespace SdlSharp
{
    /// <summary>
    /// The priority of the hint value.
    /// </summary>
    public enum HintPriority
    {
        /// <summary>
        /// Only change the default value of the hint.
        /// </summary>
        Default = Native.SDL_HintPriority.SDL_HINT_DEFAULT,

        /// <summary>
        /// Changes the normal values of the hint.
        /// </summary>
        Normal = Native.SDL_HintPriority.SDL_HINT_NORMAL,

        /// <summary>
        /// Overrides all settings of the hint.
        /// </summary>
        Override = Native.SDL_HintPriority.SDL_HINT_OVERRIDE
    }
}
