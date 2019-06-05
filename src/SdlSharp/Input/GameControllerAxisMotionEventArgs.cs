namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a game controller axis motion event.
    /// </summary>
    public sealed class GameControllerAxisMotionEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The axis.
        /// </summary>
        public GameControllerAxis Axis { get; }

        /// <summary>
        /// The new value.
        /// </summary>
        public short Value { get; }

        internal GameControllerAxisMotionEventArgs(Native.SDL_ControllerAxisEvent axis) : base(axis.Timestamp)
        {
            Axis = (GameControllerAxis)axis.Axis;
            Value = axis.Value;
        }
    }
}
