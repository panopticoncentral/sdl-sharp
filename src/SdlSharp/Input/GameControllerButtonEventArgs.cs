namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a game controller button event.
    /// </summary>
    public sealed class GameControllerButtonEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The button.
        /// </summary>
        public GameControllerButton Button { get; }

        /// <summary>
        /// Whether the button is pressed.
        /// </summary>
        public bool IsPressed { get; }

        internal GameControllerButtonEventArgs(Native.SDL_ControllerButtonEvent button) : base(button.timestamp)
        {
            Button = (GameControllerButton)button.button;
            IsPressed = button.state != 0;
        }
    }
}
