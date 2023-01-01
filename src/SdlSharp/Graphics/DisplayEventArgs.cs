namespace SdlSharp.Graphics
{
    /// <summary>
    /// Event arguments for a window event that relates to a window's display.
    /// </summary>
    public sealed class DisplayEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The new display of the window.
        /// </summary>
        public Display Display { get; }

        internal DisplayEventArgs(Native.SDL_WindowEvent e) : base(e.timestamp)
        {
            Display = new(e.data1);
        }
    }
}
