namespace SdlSharp
{
    /// <summary>
    /// Flags for a message box button.
    /// </summary>
    public enum MessageBoxButtonOptions : uint
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// This button is the return key button.
        /// </summary>
        ReturnKeyDefault = Native.SDL_MessageBoxButtonFlags.SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT,

        /// <summary>
        /// This button is the escape key button.
        /// </summary>
        EscapeKeyDefault = Native.SDL_MessageBoxButtonFlags.SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT
    }
}
