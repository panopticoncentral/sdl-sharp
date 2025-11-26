using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for joystick button events.
/// </summary>
public readonly struct JoyButtonEventData
{
    private readonly SDL_JoyButtonEvent _event;

    internal JoyButtonEventData(SDL_JoyButtonEvent e) => _event = e;

    /// <summary>Gets the joystick instance ID.</summary>
    public uint JoystickId => _event.which;

    /// <summary>Gets the joystick button index.</summary>
    public byte Button => _event.button;

    /// <summary>Gets a value indicating whether the button is pressed.</summary>
    public bool IsPressed => _event.down;
}
