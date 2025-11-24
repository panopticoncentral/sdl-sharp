namespace Sdl3Sharp.Input;

/// <summary>
/// Standard gamepad types.
/// This type does not necessarily map to first-party controllers from
/// Microsoft/Sony/Nintendo; in many cases, third-party controllers can report
/// as these, either because they were designed for a specific console, or they
/// simply most closely match that console's controllers.
/// </summary>
public enum GamepadType
{
    /// <summary>Unknown gamepad type.</summary>
    Unknown = 0,
    /// <summary>Standard gamepad.</summary>
    Standard,
    /// <summary>Xbox 360 controller.</summary>
    Xbox360,
    /// <summary>Xbox One controller.</summary>
    XboxOne,
    /// <summary>PlayStation 3 controller.</summary>
    PS3,
    /// <summary>PlayStation 4 controller.</summary>
    PS4,
    /// <summary>PlayStation 5 controller.</summary>
    PS5,
    /// <summary>Nintendo Switch Pro controller.</summary>
    NintendoSwitchPro,
    /// <summary>Nintendo Switch Joy-Con (left).</summary>
    NintendoSwitchJoyConLeft,
    /// <summary>Nintendo Switch Joy-Con (right).</summary>
    NintendoSwitchJoyConRight,
    /// <summary>Nintendo Switch Joy-Con pair.</summary>
    NintendoSwitchJoyConPair
}
