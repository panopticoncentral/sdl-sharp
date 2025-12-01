using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Event arguments for gamepad axis motion events.
/// </summary>
public sealed class GamepadAxisEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the gamepad instance ID.
    /// </summary>
    public uint GamepadId { get; }

    /// <summary>
    /// Gets the gamepad axis.
    /// </summary>
    public GamepadAxis Axis { get; }

    /// <summary>
    /// Gets the axis value (range: -32768 to 32767).
    /// </summary>
    public short Value { get; }

    internal GamepadAxisEventArgs(SDL_GamepadAxisEvent e) : base(e.timestamp)
    {
        GamepadId = e.which;
        Axis = (GamepadAxis)e.axis;
        Value = e.value;
    }
}

/// <summary>
/// Event arguments for gamepad button events.
/// </summary>
public sealed class GamepadButtonEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the gamepad instance ID.
    /// </summary>
    public uint GamepadId { get; }

    /// <summary>
    /// Gets the gamepad button.
    /// </summary>
    public GamepadButton Button { get; }

    /// <summary>
    /// Gets a value indicating whether the button is pressed.
    /// </summary>
    public bool IsPressed { get; }

    internal GamepadButtonEventArgs(SDL_GamepadButtonEvent e) : base(e.timestamp)
    {
        GamepadId = e.which;
        Button = (GamepadButton)e.button;
        IsPressed = e.down;
    }
}

/// <summary>
/// Event arguments for gamepad device events.
/// </summary>
public sealed class GamepadDeviceEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the gamepad instance ID.
    /// </summary>
    public uint GamepadId { get; }

    internal GamepadDeviceEventArgs(SDL_GamepadDeviceEvent e) : base(e.timestamp)
    {
        GamepadId = e.which;
    }
}

/// <summary>
/// Event arguments for gamepad touchpad events.
/// </summary>
public sealed class GamepadTouchpadEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the gamepad instance ID.
    /// </summary>
    public uint GamepadId { get; }

    /// <summary>
    /// Gets the touchpad index.
    /// </summary>
    public int Touchpad { get; }

    /// <summary>
    /// Gets the finger index.
    /// </summary>
    public int Finger { get; }

    /// <summary>
    /// Gets the X position on the touchpad (0.0 to 1.0).
    /// </summary>
    public float X { get; }

    /// <summary>
    /// Gets the Y position on the touchpad (0.0 to 1.0).
    /// </summary>
    public float Y { get; }

    /// <summary>
    /// Gets the pressure on the touchpad (0.0 to 1.0).
    /// </summary>
    public float Pressure { get; }

    internal GamepadTouchpadEventArgs(SDL_GamepadTouchpadEvent e) : base(e.timestamp)
    {
        GamepadId = e.which;
        Touchpad = e.touchpad;
        Finger = e.finger;
        X = e.x;
        Y = e.y;
        Pressure = e.pressure;
    }
}

/// <summary>
/// Event arguments for gamepad sensor events.
/// </summary>
public sealed class GamepadSensorEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the gamepad instance ID.
    /// </summary>
    public uint GamepadId { get; }

    /// <summary>
    /// Gets the sensor type.
    /// </summary>
    public SensorType Sensor { get; }

    /// <summary>
    /// Gets the sensor data (up to 3 values).
    /// </summary>
    public float[] Data { get; }

    /// <summary>
    /// Gets the timestamp of the sensor reading in nanoseconds.
    /// </summary>
    public ulong SensorTimestampNs { get; }

    internal unsafe GamepadSensorEventArgs(SDL_GamepadSensorEvent e) : base(e.timestamp)
    {
        GamepadId = e.which;
        Sensor = (SensorType)e.sensor;
        Data = [e.data[0], e.data[1], e.data[2]];
        SensorTimestampNs = e.sensor_timestamp;
    }
}
