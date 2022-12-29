namespace SdlSharp
{
    /// <summary>
    /// The state of device power.
    /// </summary>
    public enum PowerState
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = Native.SDL_PowerState.SDL_POWERSTATE_UNKNOWN,

        /// <summary>
        /// Running on battery.
        /// </summary>
        OnBattery = Native.SDL_PowerState.SDL_POWERSTATE_ON_BATTERY,

        /// <summary>
        /// No battery.
        /// </summary>
        NoBattery = Native.SDL_PowerState.SDL_POWERSTATE_NO_BATTERY,

        /// <summary>
        /// Battery is charging.
        /// </summary>
        Charging = Native.SDL_PowerState.SDL_POWERSTATE_CHARGING,

        /// <summary>
        /// Battery is charged.
        /// </summary>
        Charged = Native.SDL_PowerState.SDL_POWERSTATE_CHARGED
    }
}
