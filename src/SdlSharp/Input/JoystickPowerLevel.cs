namespace SdlSharp.Input
{
    /// <summary>
    /// The power level of a joystick.
    /// </summary>
    public enum JoystickPowerLevel
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = Native.SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_UNKNOWN,

        /// <summary>
        /// Empty (5% or less).
        /// </summary>
        Empty = Native.SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_EMPTY,

        /// <summary>
        /// Low (20% or less).
        /// </summary>
        Low = Native.SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_LOW,

        /// <summary>
        /// Medium (70% or less).
        /// </summary>
        Medium = Native.SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_MEDIUM,

        /// <summary>
        /// Full (100% or less).
        /// </summary>
        Full = Native.SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_FULL,

        /// <summary>
        /// Wired.
        /// </summary>
        Wired = Native.SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_WIRED,

        /// <summary>
        /// The maximum value.
        /// </summary>
        Max = Native.SDL_JoystickPowerLevel.SDL_JOYSTICK_POWER_MAX
    }
}
