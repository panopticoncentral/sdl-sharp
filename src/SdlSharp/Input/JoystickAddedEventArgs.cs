namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a joystick device added event.
    /// </summary>
    public sealed class JoystickAddedEventArgs : SdlEventArgs
    {
        private readonly int _index;

        /// <summary>
        /// The new device.
        /// </summary>
        public JoystickInfo Device => JoystickInfo.Get(_index);

        internal JoystickAddedEventArgs(Native.SDL_JoyDeviceEvent device) : base(device.timestamp)
        {
            _index = device.which;
        }
    }
}
