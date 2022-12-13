namespace SdlSharp.Graphics
{
    /// <summary>
    /// Event arguments for a window event that relates to a window's size.
    /// </summary>
    public sealed class SizeEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The new size of the window.
        /// </summary>
        public Size Size { get; }

        internal SizeEventArgs(Native.SDL_WindowEvent e) : base(e.timestamp)
        {
            Size = (e.data1, e.data2);
        }
    }
}
