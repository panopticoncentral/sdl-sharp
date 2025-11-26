using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for mouse motion events.
/// </summary>
public readonly struct MouseMotionEventData
{
    private readonly SDL_MouseMotionEvent _event;

    internal MouseMotionEventData(SDL_MouseMotionEvent e) => _event = e;

    /// <summary>Gets the window ID with mouse focus.</summary>
    public uint WindowId => _event.windowID;

    /// <summary>Gets the mouse instance ID.</summary>
    public uint MouseId => _event.which;

    /// <summary>Gets the current button state as a bitmask.</summary>
    public uint ButtonState => _event.state;

    /// <summary>Gets the X coordinate relative to the window.</summary>
    public float X => _event.x;

    /// <summary>Gets the Y coordinate relative to the window.</summary>
    public float Y => _event.y;

    /// <summary>Gets the relative motion in the X direction.</summary>
    public float RelativeX => _event.xrel;

    /// <summary>Gets the relative motion in the Y direction.</summary>
    public float RelativeY => _event.yrel;
}
