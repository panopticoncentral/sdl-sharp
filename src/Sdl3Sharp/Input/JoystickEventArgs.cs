using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Event arguments for joystick axis motion events.
/// </summary>
public sealed class JoystickAxisEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the joystick instance ID.
    /// </summary>
    public uint JoystickId { get; }

    /// <summary>
    /// Gets the joystick axis index.
    /// </summary>
    public byte Axis { get; }

    /// <summary>
    /// Gets the axis value (range: -32768 to 32767).
    /// </summary>
    public short Value { get; }

    internal JoystickAxisEventArgs(SDL_JoyAxisEvent e) : base(e.timestamp)
    {
        JoystickId = e.which;
        Axis = e.axis;
        Value = e.value;
    }
}

/// <summary>
/// Event arguments for joystick ball motion events.
/// </summary>
public sealed class JoystickBallEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the joystick instance ID.
    /// </summary>
    public uint JoystickId { get; }

    /// <summary>
    /// Gets the joystick trackball index.
    /// </summary>
    public byte Ball { get; }

    /// <summary>
    /// Gets the relative motion in the X direction.
    /// </summary>
    public short RelativeX { get; }

    /// <summary>
    /// Gets the relative motion in the Y direction.
    /// </summary>
    public short RelativeY { get; }

    internal JoystickBallEventArgs(SDL_JoyBallEvent e) : base(e.timestamp)
    {
        JoystickId = e.which;
        Ball = e.ball;
        RelativeX = e.xrel;
        RelativeY = e.yrel;
    }
}

/// <summary>
/// Event arguments for joystick hat motion events.
/// </summary>
public sealed class JoystickHatEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the joystick instance ID.
    /// </summary>
    public uint JoystickId { get; }

    /// <summary>
    /// Gets the joystick hat index.
    /// </summary>
    public byte Hat { get; }

    /// <summary>
    /// Gets the hat position.
    /// </summary>
    public HatPosition Position { get; }

    internal JoystickHatEventArgs(SDL_JoyHatEvent e) : base(e.timestamp)
    {
        JoystickId = e.which;
        Hat = e.hat;
        Position = (HatPosition)e.value;
    }
}

/// <summary>
/// Event arguments for joystick button events.
/// </summary>
public sealed class JoystickButtonEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the joystick instance ID.
    /// </summary>
    public uint JoystickId { get; }

    /// <summary>
    /// Gets the joystick button index.
    /// </summary>
    public byte Button { get; }

    /// <summary>
    /// Gets a value indicating whether the button is pressed.
    /// </summary>
    public bool IsPressed { get; }

    internal JoystickButtonEventArgs(SDL_JoyButtonEvent e) : base(e.timestamp)
    {
        JoystickId = e.which;
        Button = e.button;
        IsPressed = e.down;
    }
}

/// <summary>
/// Event arguments for joystick device events.
/// </summary>
public sealed class JoystickDeviceEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the joystick instance ID for add/remove events.
    /// </summary>
    public uint JoystickId { get; }

    internal JoystickDeviceEventArgs(SDL_JoyDeviceEvent e) : base(e.timestamp)
    {
        JoystickId = e.which;
    }
}

/// <summary>
/// Event arguments for joystick battery updated events.
/// </summary>
public sealed class JoystickBatteryEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the joystick instance ID.
    /// </summary>
    public uint JoystickId { get; }

    /// <summary>
    /// Gets the battery state.
    /// </summary>
    public PowerState State { get; }

    /// <summary>
    /// Gets the battery percent charged (0-100), or -1 if unknown.
    /// </summary>
    public int Percent { get; }

    internal JoystickBatteryEventArgs(SDL_JoyBatteryEvent e) : base(e.timestamp)
    {
        JoystickId = e.which;
        State = (PowerState)e.state;
        Percent = e.percent;
    }
}
