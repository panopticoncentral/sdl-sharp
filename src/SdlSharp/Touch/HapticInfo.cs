namespace SdlSharp.Touch
{
    /// <summary>
    /// Information about a haptic effect.
    /// </summary>
    public unsafe sealed class HapticInfo : NativeStaticIndexBase<int, HapticInfo>
    {
        /// <summary>
        /// The name of the effect.
        /// </summary>
        public string Name =>
            Native.SDL_HapticName(Index);

        /// <summary>
        /// Whether the effect has been opened.
        /// </summary>
        public bool IsOpened =>
            Native.SDL_HapticOpened(Index);

        /// <summary>
        /// Opens the effect.
        /// </summary>
        /// <returns>The effect instance.</returns>
        public Haptic Open() =>
            Haptic.PointerToInstanceNotNull(Native.SDL_HapticOpen(Index));
    }
}
