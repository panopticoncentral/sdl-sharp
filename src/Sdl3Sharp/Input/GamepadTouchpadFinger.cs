namespace Sdl3Sharp.Input;

/// <summary>
/// Represents the state of a finger on a gamepad touchpad.
/// </summary>
/// <param name="IsDown">Whether the finger is currently touching the touchpad.</param>
/// <param name="X">The x position, normalized 0 to 1, with the origin in the upper left.</param>
/// <param name="Y">The y position, normalized 0 to 1, with the origin in the upper left.</param>
/// <param name="Pressure">The pressure value of the touch.</param>
public readonly record struct GamepadTouchpadFinger(bool IsDown, float X, float Y, float Pressure);
