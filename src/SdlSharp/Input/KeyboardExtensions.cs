namespace SdlSharp.Input
{
    /// <summary>
    /// Extensions for keyboard values.
    /// </summary>
    public static class KeyboardExtensions
    {
        /// <summary>
        /// Converts a scancode to a keycode.
        /// </summary>
        /// <param name="scancode">The scancode.</param>
        /// <returns>The keycode.</returns>
        public static Keycode ToKeycode(this Scancode scancode) =>
            Native.SDL_GetKeyFromScancode(scancode);

        /// <summary>
        /// Converts a keycode to a scancode.
        /// </summary>
        /// <param name="keycode">The keycode.</param>
        /// <returns>The scancode.</returns>
        public static Scancode ToScancode(this Keycode keycode) =>
            Native.SDL_GetScancodeFromKey(keycode);

        /// <summary>
        /// Converts a scancode to a string.
        /// </summary>
        /// <param name="scancode">The scancode.</param>
        /// <returns>The name of the scancode.</returns>
        public static string ToString(this Scancode scancode) =>
            Native.SDL_GetScancodeName(scancode);

        /// <summary>
        /// Converts a string to a scancode.
        /// </summary>
        /// <param name="name">The name of the scancode.</param>
        /// <returns>The scancode.</returns>
        public static Scancode ToScancode(this string name) =>
            Native.SDL_GetScancodeFromName(name);

        /// <summary>
        /// Converts a keycode to a string.
        /// </summary>
        /// <param name="keycode">The keycode.</param>
        /// <returns>The name of the keycode.</returns>
        public static string ToString(this Keycode keycode) =>
            Native.SDL_GetKeyName(keycode);

        /// <summary>
        /// Converts a string to a keycode.
        /// </summary>
        /// <param name="name">The name of the keycode.</param>
        /// <returns>The keycode.</returns>
        public static Keycode ToKeycode(this string name) =>
            Native.SDL_GetKeyFromName(name);
    }
}
