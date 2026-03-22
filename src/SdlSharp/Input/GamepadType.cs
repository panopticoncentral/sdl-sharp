namespace SdlSharp.Input;

/// <summary>
/// Standard gamepad types.
/// </summary>
public enum GamepadType
{
    /// <summary>Unknown gamepad type.</summary>
    Unknown = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_UNKNOWN,
    /// <summary>Standard gamepad with no specific platform layout.</summary>
    Standard = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_STANDARD,
    /// <summary>Xbox 360 controller.</summary>
    Xbox360 = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_XBOX360,
    /// <summary>Xbox One controller.</summary>
    XboxOne = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_XBOXONE,
    /// <summary>PlayStation 3 controller.</summary>
    PS3 = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_PS3,
    /// <summary>PlayStation 4 controller.</summary>
    PS4 = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_PS4,
    /// <summary>PlayStation 5 controller.</summary>
    PS5 = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_PS5,
    /// <summary>Nintendo Switch Pro controller.</summary>
    NintendoSwitchPro = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_PRO,
    /// <summary>Nintendo Switch Joy-Con left controller.</summary>
    NintendoSwitchJoyConLeft = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_LEFT,
    /// <summary>Nintendo Switch Joy-Con right controller.</summary>
    NintendoSwitchJoyConRight = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_RIGHT,
    /// <summary>Nintendo Switch Joy-Con pair used as one controller.</summary>
    NintendoSwitchJoyConPair = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_PAIR,
    /// <summary>Nintendo GameCube controller.</summary>
    GameCube = (int)Native.SDL_GamepadType.SDL_GAMEPAD_TYPE_GAMECUBE,
}
