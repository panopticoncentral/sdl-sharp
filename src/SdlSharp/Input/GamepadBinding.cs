namespace SdlSharp.Input;
/// <summary>The kind of joystick or gamepad control.</summary>
public enum GamepadBindingType
{
    /// <summary>No control.</summary>
    None = Native.SDL_GamepadBindingType.SDL_GAMEPAD_BINDTYPE_NONE,
    /// <summary>A button.</summary>
    Button = Native.SDL_GamepadBindingType.SDL_GAMEPAD_BINDTYPE_BUTTON,
    /// <summary>An axis with minimum and maximum values.</summary>
    Axis = Native.SDL_GamepadBindingType.SDL_GAMEPAD_BINDTYPE_AXIS,
    /// <summary>A hat, with its mask in the minimum field.</summary>
    Hat = Native.SDL_GamepadBindingType.SDL_GAMEPAD_BINDTYPE_HAT
}
/// <summary>A copied mapping. Index is a button, axis, or hat index; ranges apply to axes and InputMinimum holds a hat mask.</summary>
public readonly record struct GamepadBinding(GamepadBindingType InputType, int InputIndex, int InputMinimum, int InputMaximum,
    GamepadBindingType OutputType, int OutputIndex, int OutputMinimum, int OutputMaximum);
