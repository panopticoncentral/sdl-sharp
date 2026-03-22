namespace SdlSharp.Input;

/// <summary>
/// Gamepad axes.
/// </summary>
public enum GamepadAxis
{
    /// <summary>Invalid axis value.</summary>
    Invalid = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_INVALID,
    /// <summary>Left stick horizontal axis.</summary>
    LeftX = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFTX,
    /// <summary>Left stick vertical axis.</summary>
    LeftY = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFTY,
    /// <summary>Right stick horizontal axis.</summary>
    RightX = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHTX,
    /// <summary>Right stick vertical axis.</summary>
    RightY = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHTY,
    /// <summary>Left trigger axis.</summary>
    LeftTrigger = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFT_TRIGGER,
    /// <summary>Right trigger axis.</summary>
    RightTrigger = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHT_TRIGGER,
}
