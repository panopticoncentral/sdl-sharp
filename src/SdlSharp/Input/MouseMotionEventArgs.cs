using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for mouse motion events.
    /// </summary>
    public sealed class MouseMotionEventArgs : SdlEventArgs
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
        /// The state of the buttons.
        /// </summary>
        public MouseButton Buttons { get; }

        /// <summary>
        /// The location of the event.
        /// </summary>
        public Point Location { get; }

        /// <summary>
        /// The relative location of the event.
        /// </summary>
        public Point RelativeLocation { get; }

        internal MouseMotionEventArgs(Native.SDL_MouseMotionEvent motion) : base(motion.timestamp)
        {
            _windowId = motion.windowID;
            IsTouch = motion.which == uint.MaxValue;
            Buttons = (MouseButton)motion.state;
            Location = new(motion.x, motion.y);
            RelativeLocation = new(motion.xrel, motion.yrel);
        }
    }
}
