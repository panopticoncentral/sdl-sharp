using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for joystick device events.
/// </summary>
public readonly struct JoyDeviceEventData
{
    private readonly SDL_JoyDeviceEvent _event;

    internal JoyDeviceEventData(SDL_JoyDeviceEvent e) => _event = e;

    /// <summary>Gets the joystick instance ID.</summary>
    public uint JoystickId => _event.which;
}
