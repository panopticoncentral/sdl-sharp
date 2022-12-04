namespace SdlSharp
{
    /// <summary>
    /// Common event arguments for SDL events
    /// </summary>
    public class SdlEventArgs : EventArgs
    {
        /// <summary>
        /// Timestamp of when the event happened.
        /// </summary>
        public uint Timestamp { get; }

        internal SdlEventArgs(uint timestamp)
        {
            Timestamp = timestamp;
        }

        internal SdlEventArgs(Native.SDL_CommonEvent e) : this(e.Timestamp)
        {
        }
    }
}
