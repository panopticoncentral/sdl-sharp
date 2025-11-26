using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for gamepad device events.
/// </summary>
public readonly struct GamepadDeviceEventData
{
    private readonly SDL_GamepadDeviceEvent _event;

    internal GamepadDeviceEventData(SDL_GamepadDeviceEvent e) => _event = e;

    /// <summary>Gets the joystick instance ID.</summary>
    public uint JoystickId => _event.which;
}
