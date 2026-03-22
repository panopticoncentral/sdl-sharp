namespace SdlSharp.Input;

/// <summary>
/// Common joystick types.
/// </summary>
public enum JoystickType
{
    /// <summary>Unknown joystick type.</summary>
    Unknown = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_UNKNOWN,
    /// <summary>Standard gamepad controller.</summary>
    Gamepad = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_GAMEPAD,
    /// <summary>Steering wheel controller.</summary>
    Wheel = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_WHEEL,
    /// <summary>Arcade stick controller.</summary>
    ArcadeStick = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_ARCADE_STICK,
    /// <summary>Flight stick controller.</summary>
    FlightStick = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_FLIGHT_STICK,
    /// <summary>Dance pad controller.</summary>
    DancePad = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_DANCE_PAD,
    /// <summary>Guitar controller.</summary>
    Guitar = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_GUITAR,
    /// <summary>Drum kit controller.</summary>
    DrumKit = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_DRUM_KIT,
    /// <summary>Arcade pad controller.</summary>
    ArcadePad = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_ARCADE_PAD,
    /// <summary>Throttle controller.</summary>
    Throttle = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_THROTTLE,
}
