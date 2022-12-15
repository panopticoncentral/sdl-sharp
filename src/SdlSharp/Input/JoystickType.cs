namespace SdlSharp.Input
{
    /// <summary>
    /// The type of joystick.
    /// </summary>
    public enum JoystickType
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_UNKNOWN,

        /// <summary>
        /// A game controller.
        /// </summary>
        GameController = Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_GAMECONTROLLER,

        /// <summary>
        /// A wheel.
        /// </summary>
        Wheel = Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_WHEEL,

        /// <summary>
        /// An arcade stick.
        /// </summary>
        ArcadeStick = Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_ARCADE_STICK,

        /// <summary>
        /// A flight stick.
        /// </summary>
        FlightStick = Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_FLIGHT_STICK,

        /// <summary>
        /// A dance pad.
        /// </summary>
        DancePad = Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_DANCE_PAD,

        /// <summary>
        /// A guitar.
        /// </summary>
        Guitar = Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_GUITAR,

        /// <summary>
        /// A drum kit.
        /// </summary>
        DrumKit = Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_DRUM_KIT,

        /// <summary>
        /// An arcade pad.
        /// </summary>
        ArcadePad = Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_ARCADE_PAD,

        /// <summary>
        /// A throttle.
        /// </summary>
        Throttle = Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_THROTTLE
    }
}
