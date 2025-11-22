using static Sdl3Sharp.Native.Joystick;

namespace Sdl3Sharp.Input;

/// <summary>
/// Common joystick types that SDL can identify.
/// </summary>
public enum JoystickType
{
    /// <summary>Unknown joystick type.</summary>
    Unknown = SDL_JoystickType.SDL_JOYSTICK_TYPE_UNKNOWN,
    /// <summary>Gamepad.</summary>
    Gamepad = SDL_JoystickType.SDL_JOYSTICK_TYPE_GAMEPAD,
    /// <summary>Steering wheel.</summary>
    Wheel = SDL_JoystickType.SDL_JOYSTICK_TYPE_WHEEL,
    /// <summary>Arcade stick.</summary>
    ArcadeStick = SDL_JoystickType.SDL_JOYSTICK_TYPE_ARCADE_STICK,
    /// <summary>Flight stick.</summary>
    FlightStick = SDL_JoystickType.SDL_JOYSTICK_TYPE_FLIGHT_STICK,
    /// <summary>Dance pad.</summary>
    DancePad = SDL_JoystickType.SDL_JOYSTICK_TYPE_DANCE_PAD,
    /// <summary>Guitar controller.</summary>
    Guitar = SDL_JoystickType.SDL_JOYSTICK_TYPE_GUITAR,
    /// <summary>Drum kit.</summary>
    DrumKit = SDL_JoystickType.SDL_JOYSTICK_TYPE_DRUM_KIT,
    /// <summary>Arcade pad.</summary>
    ArcadePad = SDL_JoystickType.SDL_JOYSTICK_TYPE_ARCADE_PAD,
    /// <summary>Throttle.</summary>
    Throttle = SDL_JoystickType.SDL_JOYSTICK_TYPE_THROTTLE
}
