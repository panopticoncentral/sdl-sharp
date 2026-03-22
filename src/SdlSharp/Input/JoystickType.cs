// src/SdlSharp/Input/JoystickType.cs
namespace SdlSharp.Input;

/// <summary>
/// Common joystick types.
/// </summary>
public enum JoystickType
{
    Unknown = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_UNKNOWN,
    Gamepad = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_GAMEPAD,
    Wheel = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_WHEEL,
    ArcadeStick = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_ARCADE_STICK,
    FlightStick = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_FLIGHT_STICK,
    DancePad = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_DANCE_PAD,
    Guitar = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_GUITAR,
    DrumKit = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_DRUM_KIT,
    ArcadePad = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_ARCADE_PAD,
    Throttle = (int)Native.SDL_JoystickType.SDL_JOYSTICK_TYPE_THROTTLE,
}
