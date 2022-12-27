namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a joystick battery update event.
    /// </summary>
    public sealed class JoystickBatteryUpdatedEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The new power level.
        /// </summary>
        public JoystickPowerLevel PowerLevel { get; }

        internal JoystickBatteryUpdatedEventArgs(Native.SDL_JoyBatteryEvent battery) : base(battery.timestamp)
        {
            PowerLevel = (JoystickPowerLevel)battery.level;
        }
    }
}
