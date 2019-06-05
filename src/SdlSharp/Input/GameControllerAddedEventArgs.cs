namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a game controller device added event.
    /// </summary>
    public sealed class GameControllerAddedEventArgs : SdlEventArgs
    {
        private readonly int _index;

        /// <summary>
        /// The new device.
        /// </summary>
        public JoystickInfo Device => JoystickInfo.Get(_index);

        internal GameControllerAddedEventArgs(Native.SDL_ControllerDeviceEvent device) : base(device.Timestamp)
        {
            _index = device.Which;
        }
    }
}
