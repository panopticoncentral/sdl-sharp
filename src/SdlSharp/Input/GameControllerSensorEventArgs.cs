namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a game controller sensor event.
    /// </summary>
    public sealed class GameControllerSensorEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The button.
        /// </summary>
        public GameControllerButton Button { get; }

        /// <summary>
        /// Whether the button is pressed.
        /// </summary>
        public bool IsPressed { get; }

        internal GameControllerSensorEventArgs(Native.SDL_ControllerSensorEvent sensor) : base(sensor.timestamp)
        {
            Button = new((Native.SDL_GameControllerButton)button.button);
            IsPressed = button.state != 0;
        }
    }
}
