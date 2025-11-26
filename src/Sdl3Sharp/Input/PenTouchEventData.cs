using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for pen touch events.
/// </summary>
public readonly struct PenTouchEventData
{
    private readonly SDL_PenTouchEvent _event;

    internal PenTouchEventData(SDL_PenTouchEvent e) => _event = e;

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

    /// <summary>Gets a value indicating whether the eraser end is being used.</summary>
    public bool IsEraser => _event.eraser;

    /// <summary>Gets a value indicating whether the pen is touching the surface.</summary>
    public bool IsDown => _event.down;
}
