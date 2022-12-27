namespace SdlSharp.Input
{
    /// <summary>
    /// Flags describing a hat's state.
    /// </summary>
    [Flags]
    public enum HatState : byte
    {
        /// <summary>
        /// Centered.
        /// </summary>
        Centered = Native.SDL_HAT_CENTERED,

        /// <summary>
        /// Up.
        /// </summary>
        Up = Native.SDL_HAT_UP,

        /// <summary>
        /// Right.
        /// </summary>
        Right = Native.SDL_HAT_RIGHT,

        /// <summary>
        /// Right and up.
        /// </summary>
        RightUp = Native.SDL_HAT_RIGHTUP,

        /// <summary>
        /// Down.
        /// </summary>
        Down = Native.SDL_HAT_DOWN,

        /// <summary>
        /// Right and down.
        /// </summary>
        RightDown = Native.SDL_HAT_RIGHTDOWN,

        /// <summary>
        /// Left.
        /// </summary>
        Left = Native.SDL_HAT_LEFT,

        /// <summary>
        /// Left and up.
        /// </summary>
        LeftUp = Native.SDL_HAT_LEFTUP,

        /// <summary>
        /// Left and down.
        /// </summary>
        LeftDown = Native.SDL_HAT_LEFTDOWN
    }
}
