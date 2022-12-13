namespace SdlSharp.Graphics
{
    /// <summary>
    /// Event aguments when a display's orientation changes.
    /// </summary>
    public sealed class OrientationChangedEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The new orientation.
        /// </summary>
        public DisplayOrientation Orientation { get; }

        internal OrientationChangedEventArgs(Native.SDL_DisplayEvent display) : base(display.timestamp)
        {
            Orientation = (DisplayOrientation)display.data1;
        }
    }
}
