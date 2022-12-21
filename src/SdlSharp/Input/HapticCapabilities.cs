namespace SdlSharp.Input
{
    /// <summary>
    /// Capabilities that a haptic device can support.
    /// </summary>
    [Flags]
    public enum HapticCapabilities : uint
    {
        /// <summary>
        /// No capabilities.
        /// </summary>
        None,

        /// <summary>
        /// Device supports setting the global gain.
        /// </summary>
        Gain = Native.SDL_HAPTIC_GAIN,

        /// <summary>
        /// Device supports setting autocenter.
        /// </summary>
        Autocenter = Native.SDL_HAPTIC_AUTOCENTER,

        /// <summary>
        /// Device supports querying effect status.
        /// </summary>
        Status = Native.SDL_HAPTIC_STATUS,

        /// <summary>
        /// Devices supports being paused.
        /// </summary>
        Pause = Native.SDL_HAPTIC_PAUSE,

        /// <summary>
        /// All capabilities.
        /// </summary>
        All = Gain | Autocenter | Status | Pause
    }
}
