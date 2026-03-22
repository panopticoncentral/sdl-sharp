// src/SdlSharp/Input/JoystickConnectionState.cs
namespace SdlSharp.Input;

/// <summary>
/// Possible connection states for a joystick device.
/// </summary>
public enum JoystickConnectionState
{
    Invalid = (int)Native.SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_INVALID,
    Unknown = (int)Native.SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_UNKNOWN,
    Wired = (int)Native.SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_WIRED,
    Wireless = (int)Native.SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_WIRELESS,
}
