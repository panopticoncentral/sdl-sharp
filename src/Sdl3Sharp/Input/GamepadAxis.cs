namespace Sdl3Sharp.Input;

/// <summary>
/// The list of axes available on a gamepad.
/// Thumbstick axis values range from -32768 to 32767.
/// Trigger axis values range from 0 (released) to 32767 (fully pressed).
/// </summary>
public enum GamepadAxis
{
    /// <summary>Left stick X axis.</summary>
    LeftX = 0,
    /// <summary>Left stick Y axis.</summary>
    LeftY,
    /// <summary>Right stick X axis.</summary>
    RightX,
    /// <summary>Right stick Y axis.</summary>
    RightY,
    /// <summary>Left trigger axis.</summary>
    LeftTrigger,
    /// <summary>Right trigger axis.</summary>
    RightTrigger
}
