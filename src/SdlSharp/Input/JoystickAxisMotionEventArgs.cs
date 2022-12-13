namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a joystick axis motion event.
    /// </summary>
    public sealed class JoystickAxisMotionEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The axis.
        /// </summary>
        public byte Axis { get; }

        /// <summary>
        /// The new value.
        /// </summary>
        public short Value { get; }

        internal JoystickAxisMotionEventArgs(Native.SDL_JoyAxisEvent axis) : base(axis.timestamp)
        {
            Axis = axis.axis;
            Value = axis.value;
        }
    }
}
