namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a game controller touchpad event.
    /// </summary>
    public sealed class GameControllerTouchpadEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The touchpad being used.
        /// </summary>
        public int Touchpad { get; }

        /// <summary>
        /// The finger being used.
        /// </summary>
        public int Finger { get; }

        /// <summary>
        /// The location of the touch.
        /// </summary>
        public PointF Location { get; }

        /// <summary>
        /// The pressure of the touch.
        /// </summary>
        public float Pressure { get; }

        internal GameControllerTouchpadEventArgs(Native.SDL_ControllerTouchpadEvent touchpad) : base(touchpad.timestamp)
        {
            Touchpad = touchpad.touchpad;
            Finger = touchpad.finger;
            Location = (PointF)(touchpad.x, touchpad.y);
            Pressure = touchpad.pressure;
        }
    }
}
