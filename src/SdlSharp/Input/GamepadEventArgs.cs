namespace SdlSharp.Input;

/// <summary>Event data for gamepad axis motion.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Axis">The gamepad axis.</param>
/// <param name="Value">The axis value (-32768 to 32767).</param>
public readonly record struct GamepadAxisEventArgs(uint Which, GamepadAxis Axis, short Value);

/// <summary>Event data for gamepad button events.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Button">The gamepad button.</param>
/// <param name="IsDown">True if pressed.</param>
public readonly record struct GamepadButtonEventArgs(uint Which, GamepadButton Button, bool IsDown);

/// <summary>Event data for gamepad device add/remove/remap/update events.</summary>
/// <param name="Which">The joystick instance ID.</param>
public readonly record struct GamepadDeviceEventArgs(uint Which);

/// <summary>Event data for gamepad touchpad events.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Touchpad">The touchpad index.</param>
/// <param name="Finger">The finger index.</param>
/// <param name="X">Normalized X (0-1).</param>
/// <param name="Y">Normalized Y (0-1).</param>
/// <param name="Pressure">Normalized pressure (0-1).</param>
public readonly record struct GamepadTouchpadEventArgs(
    uint Which, int Touchpad, int Finger, float X, float Y, float Pressure);

/// <summary>Event data for gamepad sensor updates.</summary>
/// <param name="Which">The joystick instance ID.</param>
/// <param name="Sensor">The sensor type as reported by SDL.</param>
/// <param name="Data1">First sensor value.</param>
/// <param name="Data2">Second sensor value.</param>
/// <param name="Data3">Third sensor value.</param>
/// <param name="SensorTimestamp">The sensor reading timestamp, in nanoseconds.</param>
public readonly record struct GamepadSensorEventArgs(
    uint Which, int Sensor, float Data1, float Data2, float Data3, ulong SensorTimestamp);
