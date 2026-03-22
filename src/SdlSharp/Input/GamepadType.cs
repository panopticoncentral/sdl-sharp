// src/SdlSharp/Input/GamepadType.cs
namespace SdlSharp.Input;

/// <summary>
/// Standard gamepad types.
/// </summary>
public enum GamepadType
{
    Unknown = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_UNKNOWN,
    Standard = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_STANDARD,
    Xbox360 = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_XBOX360,
    XboxOne = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_XBOXONE,
    PS3 = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_PS3,
    PS4 = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_PS4,
    PS5 = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_PS5,
    NintendoSwitchPro = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_PRO,
    NintendoSwitchJoyConLeft = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_LEFT,
    NintendoSwitchJoyConRight = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_RIGHT,
    NintendoSwitchJoyConPair = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_PAIR,
    GameCube = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_GAMECUBE,
}
