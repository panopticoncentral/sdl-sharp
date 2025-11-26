using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for gamepad touchpad events.
/// </summary>
public readonly struct GamepadTouchpadEventData
{
    private readonly SDL_GamepadTouchpadEvent _event;

    internal GamepadTouchpadEventData(SDL_GamepadTouchpadEvent e) => _event = e;

    /// <summary>Gets the joystick instance ID.</summary>
    public uint JoystickId => _event.which;

    /// <summary>Gets the touchpad index.</summary>
    public int Touchpad => _event.touchpad;

    /// <summary>Gets the finger index on the touchpad.</summary>
    public int Finger => _event.finger;

    /// <summary>Gets the X position (normalized 0-1, 0 is left).</summary>
    public float X => _event.x;

    /// <summary>Gets the Y position (normalized 0-1, 0 is top).</summary>
    public float Y => _event.y;

    /// <summary>Gets the pressure (normalized 0-1).</summary>
    public float Pressure => _event.pressure;
}
