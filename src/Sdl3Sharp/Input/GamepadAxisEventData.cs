using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for gamepad axis events.
/// </summary>
public readonly struct GamepadAxisEventData
{
    private readonly SDL_GamepadAxisEvent _event;

    internal GamepadAxisEventData(SDL_GamepadAxisEvent e) => _event = e;

    /// <summary>Gets the joystick instance ID.</summary>
    public uint JoystickId => _event.which;

    /// <summary>Gets the gamepad axis.</summary>
    public byte Axis => _event.axis;

    /// <summary>Gets the axis value (range: -32768 to 32767).</summary>
    public short Value => _event.value;
}
