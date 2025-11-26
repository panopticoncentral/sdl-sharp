using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for gamepad button events.
/// </summary>
public readonly struct GamepadButtonEventData
{
    private readonly SDL_GamepadButtonEvent _event;

    internal GamepadButtonEventData(SDL_GamepadButtonEvent e) => _event = e;

    /// <summary>Gets the joystick instance ID.</summary>
    public uint JoystickId => _event.which;

    /// <summary>Gets the gamepad button.</summary>
    public byte Button => _event.button;

    /// <summary>Gets a value indicating whether the button is pressed.</summary>
    public bool IsPressed => _event.down;
}
