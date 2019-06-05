using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for mouse wheel motion.
    /// </summary>
    public sealed class MouseWheelEventArgs : SdlEventArgs
    {
        private readonly uint _windowId;

        /// <summary>
        /// The window that had focus, if any.
        /// </summary>
        public Window Window => Window.Get(_windowId);

        /// <summary>
        /// The event comes from touch rather than a mouse.
        /// </summary>
        public bool IsTouch { get; }

        /// <summary>
        /// The location of the event.
        /// </summary>
        public Point Location { get; }

        /// <summary>
        /// The direction of the wheel movement.
        /// </summary>
        public MouseWheelDirection Direction { get; }

        internal MouseWheelEventArgs(Native.SDL_MouseWheelEvent wheel) : base(wheel.Timestamp)
        {
            _windowId = wheel.WindowId;
            IsTouch = wheel.Which == uint.MaxValue;
            Location = (wheel.X, wheel.Y);
            Direction = wheel.Direction;
        }
    }
}
