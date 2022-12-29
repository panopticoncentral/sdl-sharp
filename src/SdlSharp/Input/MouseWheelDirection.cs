namespace SdlSharp.Input
{
    /// <summary>
    /// A mouse wheel direction.
    /// </summary>
    public enum MouseWheelDirection
    {
        /// <summary>
        /// Normal
        /// </summary>
        Normal = Native.SDL_MouseWheelDirection.SDL_MOUSEWHEEL_NORMAL,

        /// <summary>
        /// Flipped
        /// </summary>
        Flipped = Native.SDL_MouseWheelDirection.SDL_MOUSEWHEEL_FLIPPED
    }
}
