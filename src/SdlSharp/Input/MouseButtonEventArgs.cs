using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for mouse button events.
    /// </summary>
    public sealed class MouseButtonEventArgs : SdlEventArgs
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
        /// The button.
        /// </summary>
        public MouseButton Button { get; }

        /// <summary>
        /// Whether the button is pressed.
        /// </summary>
        public bool IsPressed { get; }

        /// <summary>
        /// Number of clicks.
        /// </summary>
        public byte Clicks { get; }

        /// <summary>
        /// Location of event.
        /// </summary>
        public Point Location { get; }

        internal MouseButtonEventArgs(Native.SDL_MouseButtonEvent button) : base(button.Timestamp)
        {
            _windowId = button.WindowId;
            IsTouch = button.Which == uint.MaxValue;
            Button = (MouseButton)button.Button;
            IsPressed = button.State;
            Clicks = button.Clicks;
            Location = (button.X, button.Y);
        }
    }
}
