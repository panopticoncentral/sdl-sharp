using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for joystick hat events.
/// </summary>
public readonly struct JoyHatEventData
{
    private readonly SDL_JoyHatEvent _event;

    internal JoyHatEventData(SDL_JoyHatEvent e) => _event = e;

    /// <summary>Gets the joystick instance ID.</summary>
    public uint JoystickId => _event.which;

    /// <summary>Gets the joystick hat index.</summary>
    public byte Hat => _event.hat;

    /// <summary>Gets the hat position value.</summary>
    public byte Value => _event.value;
}
