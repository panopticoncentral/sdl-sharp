using static Sdl3Sharp.Native.Joystick;

namespace Sdl3Sharp.Input;

/// <summary>
/// Possible connection states for a joystick device.
/// </summary>
public enum JoystickConnectionState
{
    /// <summary>Invalid connection state.</summary>
    Invalid = SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_INVALID,
    /// <summary>Unknown connection state.</summary>
    Unknown = SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_UNKNOWN,
    /// <summary>Joystick is connected via a wire.</summary>
    Wired = SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_WIRED,
    /// <summary>Joystick is connected wirelessly.</summary>
    Wireless = SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_WIRELESS
}
