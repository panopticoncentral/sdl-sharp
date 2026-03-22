// src/SdlSharp/Input/GamepadButton.cs
namespace SdlSharp.Input;

/// <summary>
/// Gamepad buttons.
/// </summary>
public enum GamepadButton
{
    Invalid = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_INVALID,
    South = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_SOUTH,
    East = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_EAST,
    West = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_WEST,
    North = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_NORTH,
    Back = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_BACK,
    Guide = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_GUIDE,
    Start = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_START,
    LeftStick = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_STICK,
    RightStick = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_STICK,
    LeftShoulder = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_SHOULDER,
    RightShoulder = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_SHOULDER,
    DpadUp = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_UP,
    DpadDown = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_DOWN,
    DpadLeft = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_LEFT,
    DpadRight = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_RIGHT,
    Misc1 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC1,
    RightPaddle1 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_PADDLE1,
    LeftPaddle1 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_PADDLE1,
    RightPaddle2 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_PADDLE2,
    LeftPaddle2 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_PADDLE2,
    Touchpad = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_TOUCHPAD,
    Misc2 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC2,
    Misc3 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC3,
    Misc4 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC4,
    Misc5 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC5,
    Misc6 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC6,
}
