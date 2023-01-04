namespace SdlSharp.Input
{
    /// <summary>
    /// Information about a haptic effect.
    /// </summary>
    public sealed unsafe class HapticInfo
    {
        /// <summary>
        /// The index of the haptic information.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// The name of the effect.
        /// </summary>
        public string Name =>
            Native.Utf8ToString(Native.SDL_HapticName(Index))!;

        /// <summary>
        /// Whether the effect has been opened.
        /// </summary>
        public bool IsOpened =>
            Native.SDL_HapticOpened(Index) != 0;

        /// <summary>
        /// Opens the effect.
        /// </summary>
        /// <returns>The effect instance.</returns>
        public Haptic Open() =>
            new(Native.SDL_HapticOpen(Index));

        internal HapticInfo(int index)
        {
            Index = index;
        }
    }
}
