namespace SdlSharp.Input;

/// <summary>
/// Property names for <see cref="Joystick.Properties"/> capability queries.
/// </summary>
public static class JoystickProperties
{
    /// <summary>True if the joystick has an LED with a single color (boolean).</summary>
    public const string CapMonoLed = Native.Joystick.SDL_PROP_JOYSTICK_CAP_MONO_LED_BOOLEAN;
    /// <summary>True if the joystick has an RGB LED (boolean).</summary>
    public const string CapRgbLed = Native.Joystick.SDL_PROP_JOYSTICK_CAP_RGB_LED_BOOLEAN;
    /// <summary>True if the joystick has a player LED (boolean).</summary>
    public const string CapPlayerLed = Native.Joystick.SDL_PROP_JOYSTICK_CAP_PLAYER_LED_BOOLEAN;
    /// <summary>True if the joystick has left/right rumble (boolean).</summary>
    public const string CapRumble = Native.Joystick.SDL_PROP_JOYSTICK_CAP_RUMBLE_BOOLEAN;
    /// <summary>True if the joystick has simple trigger rumble (boolean).</summary>
    public const string CapTriggerRumble = Native.Joystick.SDL_PROP_JOYSTICK_CAP_TRIGGER_RUMBLE_BOOLEAN;
}
