namespace SdlSharp.Input
{
    /// <summary>
    /// A mouse button.
    /// </summary>
    [Flags]
    public enum MouseButton
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Left
        /// </summary>
        Left = Native.SDL_BUTTON_LEFT,

        /// <summary>
        /// Middle
        /// </summary>
        Middle = Native.SDL_BUTTON_MIDDLE,

        /// <summary>
        /// Right
        /// </summary>
        Right = Native.SDL_BUTTON_RIGHT,

        /// <summary>
        /// X1
        /// </summary>
        X1 = Native.SDL_BUTTON_X1,

        /// <summary>
        /// X2
        /// </summary>
        X2 = Native.SDL_BUTTON_X2
    }
}
