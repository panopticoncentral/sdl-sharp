namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a joystick button event.
    /// </summary>
    public sealed class JoystickButtonEventArgs : SdlEventArgs
    {
        /// <summary>
        /// Which button.
        /// </summary>
        public byte Button { get; }

        /// <summary>
        /// Whether the button is pressed.
        /// </summary>
        public bool IsPressed { get; }

        internal JoystickButtonEventArgs(Native.SDL_JoyButtonEvent button) : base(button.timestamp)
        {
            Button = button.button;
            IsPressed = button.state != 0;
        }
    }
}
