using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for pen motion events.
/// </summary>
public readonly struct PenMotionEventData
{
    private readonly SDL_PenMotionEvent _event;

    internal PenMotionEventData(SDL_PenMotionEvent e) => _event = e;

    /// <summary>Gets the window ID with pen focus.</summary>
    public uint WindowId => _event.windowID;

    /// <summary>Gets the pen instance ID.</summary>
    public uint PenId => _event.which;

    /// <summary>Gets the pen input state flags.</summary>
    public uint PenState => (uint)_event.pen_state;

    /// <summary>Gets the X coordinate relative to the window.</summary>
    public float X => _event.x;

    /// <summary>Gets the Y coordinate relative to the window.</summary>
    public float Y => _event.y;
}
