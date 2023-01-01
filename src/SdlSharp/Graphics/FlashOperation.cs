namespace SdlSharp.Graphics
{
    /// <summary>
    /// The type of flashing a window should do.
    /// </summary>
    public enum FlashOperation
    {
        /// <summary>
        /// Cancel the flashing operation.
        /// </summary>
        Cancel = Native.SDL_FlashOperation.SDL_FLASH_CANCEL,

        /// <summary>
        /// Flash the window briefly.
        /// </summary>
        Briefly = Native.SDL_FlashOperation.SDL_FLASH_BRIEFLY,

        /// <summary>
        /// Flash the window until it gets focus.
        /// </summary>
        UntilFocused = Native.SDL_FlashOperation.SDL_FLASH_UNTIL_FOCUSED
    }
}
