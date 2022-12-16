namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a game controller touchpad event.
    /// </summary>
    public sealed class GameControllerTouchpadEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The button.
        /// </summary>
        public GameControllerButton Button { get; }

        /// <summary>
        /// Whether the button is pressed.
        /// </summary>
        public bool IsPressed { get; }

        internal GameControllerTouchpadEventArgs(Native.SDL_ControllerTouchpadEvent touchpad) : base(touchpad.timestamp)
        {
            Button = new((Native.SDL_GameControllerButton)button.button);
            IsPressed = button.state != 0;
        }
    }
}
