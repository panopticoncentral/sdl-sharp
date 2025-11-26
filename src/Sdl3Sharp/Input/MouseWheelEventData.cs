using static Sdl3Sharp.Native.Events;
using static Sdl3Sharp.Native.Mouse;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for mouse wheel events.
/// </summary>
public readonly struct MouseWheelEventData
{
    private readonly SDL_MouseWheelEvent _event;

    internal MouseWheelEventData(SDL_MouseWheelEvent e) => _event = e;

    /// <summary>Gets the window ID with mouse focus.</summary>
    public uint WindowId => _event.windowID;

    /// <summary>Gets the mouse instance ID.</summary>
    public uint MouseId => _event.which;

    /// <summary>Gets the horizontal scroll amount (positive=right, negative=left).</summary>
    public float X => _event.x;

    /// <summary>Gets the vertical scroll amount (positive=away from user, negative=toward user).</summary>
    public float Y => _event.y;

    /// <summary>Gets a value indicating whether the scroll direction is flipped/natural.</summary>
    public bool IsFlipped => _event.direction == SDL_MouseWheelDirection.SDL_MOUSEWHEEL_FLIPPED;

    /// <summary>Gets the mouse X coordinate relative to the window.</summary>
    public float MouseX => _event.mouse_x;

    /// <summary>Gets the mouse Y coordinate relative to the window.</summary>
    public float MouseY => _event.mouse_y;

    /// <summary>Gets the horizontal scroll amount in whole integer ticks.</summary>
    public int IntegerX => _event.integer_x;

    /// <summary>Gets the vertical scroll amount in whole integer ticks.</summary>
    public int IntegerY => _event.integer_y;
}
