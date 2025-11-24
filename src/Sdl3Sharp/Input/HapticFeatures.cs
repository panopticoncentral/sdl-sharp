namespace Sdl3Sharp.Input;

/// <summary>
/// Haptic device features and supported effect types.
/// These flags can be combined to represent multiple capabilities.
/// </summary>
[Flags]
public enum HapticFeatures : uint
{
    /// <summary>No features supported.</summary>
    None = 0,

    /// <summary>Constant effect supported.</summary>
    Constant = 1u << 0,

    /// <summary>Sine wave effect supported.</summary>
    Sine = 1u << 1,

    /// <summary>Square wave effect supported.</summary>
    Square = 1u << 2,

    /// <summary>Triangle wave effect supported.</summary>
    Triangle = 1u << 3,

    /// <summary>Sawtooth up wave effect supported.</summary>
    SawtoothUp = 1u << 4,

    /// <summary>Sawtooth down wave effect supported.</summary>
    SawtoothDown = 1u << 5,

    /// <summary>Ramp effect supported.</summary>
    Ramp = 1u << 6,

    /// <summary>Spring effect supported - uses axes position.</summary>
    Spring = 1u << 7,

    /// <summary>Damper effect supported - uses axes velocity.</summary>
    Damper = 1u << 8,

    /// <summary>Inertia effect supported - uses axes acceleration.</summary>
    Inertia = 1u << 9,

    /// <summary>Friction effect supported - uses axes movement.</summary>
    Friction = 1u << 10,

    /// <summary>Left/Right effect supported.</summary>
    LeftRight = 1u << 11,

    /// <summary>Custom effect is supported.</summary>
    Custom = 1u << 15,

    /// <summary>Device can set global gain.</summary>
    Gain = 1u << 16,

    /// <summary>Device can set autocenter.</summary>
    Autocenter = 1u << 17,

    /// <summary>Device can be queried for effect status.</summary>
    Status = 1u << 18,

    /// <summary>Device can be paused.</summary>
    Pause = 1u << 19
}
