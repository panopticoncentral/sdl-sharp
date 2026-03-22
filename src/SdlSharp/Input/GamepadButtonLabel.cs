// src/SdlSharp/Input/GamepadButtonLabel.cs
namespace SdlSharp.Input;

/// <summary>
/// Gamepad button labels (platform-specific face button names).
/// </summary>
public enum GamepadButtonLabel
{
    Unknown = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_UNKNOWN,
    A = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_A,
    B = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_B,
    X = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_X,
    Y = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_Y,
    Cross = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_CROSS,
    Circle = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_CIRCLE,
    Square = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_SQUARE,
    Triangle = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_TRIANGLE,
}
