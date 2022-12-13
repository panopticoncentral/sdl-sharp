namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a joystick hat event.
    /// </summary>
    public sealed class JoystickHatMotionEventArgs : SdlEventArgs
    {
        /// <summary>
        /// Which hat.
        /// </summary>
        public byte Hat { get; }

        /// <summary>
        /// The state of the hat.
        /// </summary>
        public HatState Value { get; }

        internal JoystickHatMotionEventArgs(Native.SDL_JoyHatEvent hat) : base(hat.timestamp)
        {
            Hat = hat.hat;
            Value = (HatState)hat.value;
        }
    }
}
