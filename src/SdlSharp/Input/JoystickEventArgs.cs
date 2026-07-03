namespace SdlSharp.Input;

/// <summary>Event data for joystick axis motion.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Axis">The axis index.</param>
/// <param name="Value">The axis value (-32768 to 32767).</param>
public readonly record struct JoyAxisEventArgs(uint Which, byte Axis, short Value);

/// <summary>Event data for joystick trackball motion.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Ball">The trackball index.</param>
/// <param name="RelativeX">Relative X motion.</param>
/// <param name="RelativeY">Relative Y motion.</param>
public readonly record struct JoyBallEventArgs(uint Which, byte Ball, short RelativeX, short RelativeY);

/// <summary>Event data for joystick hat motion.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Hat">The hat index.</param>
/// <param name="Value">The hat position.</param>
public readonly record struct JoyHatEventArgs(uint Which, byte Hat, HatPosition Value);

/// <summary>Event data for joystick button events.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Button">The button index.</param>
/// <param name="IsDown">True if pressed.</param>
public readonly record struct JoyButtonEventArgs(uint Which, byte Button, bool IsDown);

/// <summary>Event data for joystick device add/remove/update events.</summary>
/// <param name="Which">The joystick instance ID.</param>
public readonly record struct JoyDeviceEventArgs(uint Which);

/// <summary>Event data for joystick battery-level changes.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="State">The power state.</param>
/// <param name="Percent">The battery percentage (0-100), or -1 if unknown.</param>
public readonly record struct JoyBatteryEventArgs(uint Which, PowerState State, int Percent);
