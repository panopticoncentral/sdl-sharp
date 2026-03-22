// src/SdlSharp/Input/GamepadAxis.cs
namespace SdlSharp.Input;

/// <summary>
/// Gamepad axes.
/// </summary>
public enum GamepadAxis
{
    Invalid = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_INVALID,
    LeftX = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFTX,
    LeftY = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFTY,
    RightX = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHTX,
    RightY = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHTY,
    LeftTrigger = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFT_TRIGGER,
    RightTrigger = (int)Native.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHT_TRIGGER,
}
