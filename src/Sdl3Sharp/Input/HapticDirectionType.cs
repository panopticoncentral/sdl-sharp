namespace Sdl3Sharp.Input;

/// <summary>
/// Direction encoding types for haptic effects.
/// Specifies how the direction of a haptic effect is encoded.
/// </summary>
public enum HapticDirectionType : byte
{
    /// <summary>Uses polar coordinates for the direction.</summary>
    Polar = 0,

    /// <summary>Uses cartesian coordinates for the direction.</summary>
    Cartesian = 1,

    /// <summary>Uses spherical coordinates for the direction.</summary>
    Spherical = 2,

    /// <summary>Use this value to play an effect on the steering wheel axis.</summary>
    SteeringAxis = 3
}
