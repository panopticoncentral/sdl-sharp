using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for joystick axis events.
/// </summary>
public readonly struct JoyAxisEventData
{
    private readonly SDL_JoyAxisEvent _event;

    internal JoyAxisEventData(SDL_JoyAxisEvent e) => _event = e;

    /// <summary>Gets the joystick instance ID.</summary>
    public uint JoystickId => _event.which;

    /// <summary>Gets the joystick axis index.</summary>
    public byte Axis => _event.axis;

    /// <summary>Gets the axis value (range: -32768 to 32767).</summary>
    public short Value => _event.value;
}
