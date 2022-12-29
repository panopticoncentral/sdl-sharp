namespace SdlSharp
{
    /// <summary>
    /// Flags for a message box.
    /// </summary>
    public enum MessageBoxType
    {
        /// <summary>
        /// Error message box.
        /// </summary>
        Error = Native.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR,

        /// <summary>
        /// Warning message box.
        /// </summary>
        Warning = Native.SDL_MessageBoxFlags.SDL_MESSAGEBOX_WARNING,

        /// <summary>
        /// Information message box.
        /// </summary>
        Information = Native.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION,

        /// <summary>
        /// Buttons placed left to right.
        /// </summary>
        ButtonsLeftToRight = Native.SDL_MessageBoxFlags.SDL_MESSAGEBOX_BUTTONS_LEFT_TO_RIGHT,

        /// <summary>
        /// Buttons placed right to left.
        /// </summary>
        ButtonsRightToLeft = Native.SDL_MessageBoxFlags.SDL_MESSAGEBOX_BUTTONS_RIGHT_TO_LEFT,
    }
}
