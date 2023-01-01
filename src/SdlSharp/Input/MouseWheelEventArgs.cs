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
        public Window Window => new(_windowId);

        /// <summary>
        /// The event comes from touch rather than a mouse.
        /// </summary>
        public bool IsTouch { get; }

        /// <summary>
        /// The scroll amount of the event.
        /// </summary>
        public Point Scroll { get; }

        /// <summary>
        /// The direction of the wheel movement.
        /// </summary>
        public MouseWheelDirection Direction { get; }

        /// <summary>
        /// The precise location of the event.
        /// </summary>
        public PointF PreciseLocation { get; }

        /// <summary>
        /// The mouse location relative to the window.
        /// </summary>
        public Point Location { get; }

        internal MouseWheelEventArgs(Native.SDL_MouseWheelEvent wheel) : base(wheel.timestamp)
        {
            _windowId = wheel.windowID;
            IsTouch = wheel.which == uint.MaxValue;
            Scroll = (wheel.x, wheel.y);
            Direction = (MouseWheelDirection)wheel.direction;
            PreciseLocation = (PointF)(wheel.preciseX, wheel.preciseY);
            Location = (wheel.mouseX, wheel.mouseY);
        }
    }
}
