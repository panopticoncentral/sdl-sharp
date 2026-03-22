namespace SdlSharp.Input;

/// <summary>
/// Possible connection states for a joystick device.
/// </summary>
public enum JoystickConnectionState
{
    /// <summary>Invalid or error connection state.</summary>
    Invalid = (int)Native.SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_INVALID,
    /// <summary>Connection state is unknown.</summary>
    Unknown = (int)Native.SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_UNKNOWN,
    /// <summary>Connected via a wired connection.</summary>
    Wired = (int)Native.SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_WIRED,
    /// <summary>Connected wirelessly.</summary>
    Wireless = (int)Native.SDL_JoystickConnectionState.SDL_JOYSTICK_CONNECTION_WIRELESS,
}
