namespace SdlSharp.Input
{
    /// <summary>
    /// Information about a haptic effect.
    /// </summary>
    public sealed unsafe class HapticInfo
    {
        private readonly int _index;

        /// <summary>
        /// The name of the effect.
        /// </summary>
        public string Name =>
            Native.Utf8ToString(Native.SDL_HapticName(_index))!;

        /// <summary>
        /// Whether the effect has been opened.
        /// </summary>
        public bool IsOpened =>
            Native.SDL_HapticOpened(_index) != 0;

        /// <summary>
        /// Opens the effect.
        /// </summary>
        /// <returns>The effect instance.</returns>
        public Haptic Open() =>
            new(Native.SDL_HapticOpen(_index));

        internal HapticInfo(int index)
        {
            _index = index;
        }
    }
}
