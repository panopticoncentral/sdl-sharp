namespace SdlSharp.Input;

/// <summary>
/// Gamepad button labels (platform-specific face button names).
/// </summary>
public enum GamepadButtonLabel
{
    /// <summary>Unknown or no label.</summary>
    Unknown = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_UNKNOWN,
    /// <summary>Xbox-style A label.</summary>
    A = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_A,
    /// <summary>Xbox-style B label.</summary>
    B = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_B,
    /// <summary>Xbox-style X label.</summary>
    X = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_X,
    /// <summary>Xbox-style Y label.</summary>
    Y = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_Y,
    /// <summary>PlayStation Cross label.</summary>
    Cross = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_CROSS,
    /// <summary>PlayStation Circle label.</summary>
    Circle = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_CIRCLE,
    /// <summary>PlayStation Square label.</summary>
    Square = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_SQUARE,
    /// <summary>PlayStation Triangle label.</summary>
    Triangle = (int)Native.SDL_GamepadButtonLabel.SDL_GAMEPAD_BUTTON_LABEL_TRIANGLE,
}
