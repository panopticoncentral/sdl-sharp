namespace SdlSharp.Input;

/// <summary>
/// Gamepad buttons.
/// </summary>
public enum GamepadButton
{
    /// <summary>Invalid button value.</summary>
    Invalid = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_INVALID,
    /// <summary>Bottom face button (A on Xbox, Cross on PlayStation).</summary>
    South = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_SOUTH,
    /// <summary>Right face button (B on Xbox, Circle on PlayStation).</summary>
    East = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_EAST,
    /// <summary>Left face button (X on Xbox, Square on PlayStation).</summary>
    West = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_WEST,
    /// <summary>Top face button (Y on Xbox, Triangle on PlayStation).</summary>
    North = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_NORTH,
    /// <summary>Back/Select button.</summary>
    Back = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_BACK,
    /// <summary>Guide/Home/PS button.</summary>
    Guide = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_GUIDE,
    /// <summary>Start/Options button.</summary>
    Start = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_START,
    /// <summary>Left stick click button.</summary>
    LeftStick = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_STICK,
    /// <summary>Right stick click button.</summary>
    RightStick = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_STICK,
    /// <summary>Left shoulder (bumper) button.</summary>
    LeftShoulder = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_SHOULDER,
    /// <summary>Right shoulder (bumper) button.</summary>
    RightShoulder = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_SHOULDER,
    /// <summary>D-pad up.</summary>
    DpadUp = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_UP,
    /// <summary>D-pad down.</summary>
    DpadDown = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_DOWN,
    /// <summary>D-pad left.</summary>
    DpadLeft = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_LEFT,
    /// <summary>D-pad right.</summary>
    DpadRight = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_RIGHT,
    /// <summary>Additional miscellaneous button 1 (e.g. Share, Capture).</summary>
    Misc1 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC1,
    /// <summary>Upper right paddle button (Xbox Elite, Steam Deck).</summary>
    RightPaddle1 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_PADDLE1,
    /// <summary>Upper left paddle button (Xbox Elite, Steam Deck).</summary>
    LeftPaddle1 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_PADDLE1,
    /// <summary>Lower right paddle button (Xbox Elite, Steam Deck).</summary>
    RightPaddle2 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_PADDLE2,
    /// <summary>Lower left paddle button (Xbox Elite, Steam Deck).</summary>
    LeftPaddle2 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_PADDLE2,
    /// <summary>Touchpad click button (PlayStation).</summary>
    Touchpad = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_TOUCHPAD,
    /// <summary>Additional miscellaneous button 2.</summary>
    Misc2 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC2,
    /// <summary>Additional miscellaneous button 3.</summary>
    Misc3 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC3,
    /// <summary>Additional miscellaneous button 4.</summary>
    Misc4 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC4,
    /// <summary>Additional miscellaneous button 5.</summary>
    Misc5 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC5,
    /// <summary>Additional miscellaneous button 6.</summary>
    Misc6 = (int)Native.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC6,
}
