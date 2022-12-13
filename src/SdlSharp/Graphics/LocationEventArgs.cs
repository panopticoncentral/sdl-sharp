namespace SdlSharp.Graphics
{
    /// <summary>
    /// Event arguments for a window event that has a position.
    /// </summary>
    public sealed class LocationEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The location of the event.
        /// </summary>
        public Point Location { get; }

        internal LocationEventArgs(Native.SDL_WindowEvent e) : base(e.timestamp)
        {
            Location = new Point(e.data1, e.data2);
        }
    }
}
