namespace SdlSharp.Input;

/// <summary>
/// Property names for <see cref="Gamepad.Properties"/> capability queries.
/// </summary>
public static class GamepadProperties
{
    /// <summary>True if the gamepad has an LED with a single color (boolean).</summary>
    public const string CapMonoLed = Native.Gamepad.SDL_PROP_GAMEPAD_CAP_MONO_LED_BOOLEAN;
    /// <summary>True if the gamepad has an RGB LED (boolean).</summary>
    public const string CapRgbLed = Native.Gamepad.SDL_PROP_GAMEPAD_CAP_RGB_LED_BOOLEAN;
    /// <summary>True if the gamepad has a player LED (boolean).</summary>
    public const string CapPlayerLed = Native.Gamepad.SDL_PROP_GAMEPAD_CAP_PLAYER_LED_BOOLEAN;
    /// <summary>True if the gamepad has left/right rumble (boolean).</summary>
    public const string CapRumble = Native.Gamepad.SDL_PROP_GAMEPAD_CAP_RUMBLE_BOOLEAN;
    /// <summary>True if the gamepad has simple trigger rumble (boolean).</summary>
    public const string CapTriggerRumble = Native.Gamepad.SDL_PROP_GAMEPAD_CAP_TRIGGER_RUMBLE_BOOLEAN;
}
