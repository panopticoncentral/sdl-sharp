using Sdl3Sharp.Graphics;
using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Event arguments for pen proximity events.
/// </summary>
public sealed class PenProximityEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID with pen focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the pen instance ID.
    /// </summary>
    public uint PenId { get; }

    /// <summary>
    /// Gets the window with pen focus, if any.
    /// </summary>
    public Window? Window => Window.FromID(WindowId);

    internal PenProximityEventArgs(SDL_PenProximityEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        PenId = e.which;
    }
}

/// <summary>
/// Event arguments for pen touch events.
/// </summary>
public sealed class PenTouchEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID with pen focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the pen instance ID.
    /// </summary>
    public uint PenId { get; }

    /// <summary>
    /// Gets the pen tip state.
    /// </summary>
    public PenInputFlags PenState { get; }

    /// <summary>
    /// Gets the X coordinate relative to the window.
    /// </summary>
    public float X { get; }

    /// <summary>
    /// Gets the Y coordinate relative to the window.
    /// </summary>
    public float Y { get; }

    /// <summary>
    /// Gets a value indicating whether the pen is erasing.
    /// </summary>
    public bool IsErasing { get; }

    /// <summary>
    /// Gets a value indicating whether the pen is down (touching surface).
    /// </summary>
    public bool IsDown { get; }

    /// <summary>
    /// Gets the window with pen focus, if any.
    /// </summary>
    public Window? Window => Window.FromID(WindowId);

    internal PenTouchEventArgs(SDL_PenTouchEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        PenId = e.which;
        PenState = (PenInputFlags)e.pen_state;
        X = e.x;
        Y = e.y;
        IsErasing = e.eraser;
        IsDown = e.down;
    }
}

/// <summary>
/// Event arguments for pen motion events.
/// </summary>
public sealed class PenMotionEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID with pen focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the pen instance ID.
    /// </summary>
    public uint PenId { get; }

    /// <summary>
    /// Gets the pen tip state.
    /// </summary>
    public PenInputFlags PenState { get; }

    /// <summary>
    /// Gets the X coordinate relative to the window.
    /// </summary>
    public float X { get; }

    /// <summary>
    /// Gets the Y coordinate relative to the window.
    /// </summary>
    public float Y { get; }

    /// <summary>
    /// Gets the window with pen focus, if any.
    /// </summary>
    public Window? Window => Window.FromID(WindowId);

    internal PenMotionEventArgs(SDL_PenMotionEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        PenId = e.which;
        PenState = (PenInputFlags)e.pen_state;
        X = e.x;
        Y = e.y;
    }
}

/// <summary>
/// Event arguments for pen button events.
/// </summary>
public sealed class PenButtonEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID with pen focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the pen instance ID.
    /// </summary>
    public uint PenId { get; }

    /// <summary>
    /// Gets the pen tip state.
    /// </summary>
    public PenInputFlags PenState { get; }

    /// <summary>
    /// Gets the X coordinate relative to the window.
    /// </summary>
    public float X { get; }

    /// <summary>
    /// Gets the Y coordinate relative to the window.
    /// </summary>
    public float Y { get; }

    /// <summary>
    /// Gets the button that changed state.
    /// </summary>
    public byte Button { get; }

    /// <summary>
    /// Gets a value indicating whether the button is pressed.
    /// </summary>
    public bool IsDown { get; }

    /// <summary>
    /// Gets the window with pen focus, if any.
    /// </summary>
    public Window? Window => Window.FromID(WindowId);

    internal PenButtonEventArgs(SDL_PenButtonEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        PenId = e.which;
        PenState = (PenInputFlags)e.pen_state;
        X = e.x;
        Y = e.y;
        Button = e.button;
        IsDown = e.down;
    }
}

/// <summary>
/// Event arguments for pen axis events.
/// </summary>
public sealed class PenAxisEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID with pen focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the pen instance ID.
    /// </summary>
    public uint PenId { get; }

    /// <summary>
    /// Gets the pen tip state.
    /// </summary>
    public PenInputFlags PenState { get; }

    /// <summary>
    /// Gets the X coordinate relative to the window.
    /// </summary>
    public float X { get; }

    /// <summary>
    /// Gets the Y coordinate relative to the window.
    /// </summary>
    public float Y { get; }

    /// <summary>
    /// Gets the axis that changed.
    /// </summary>
    public PenAxis Axis { get; }

    /// <summary>
    /// Gets the new value for the axis.
    /// </summary>
    public float Value { get; }

    /// <summary>
    /// Gets the window with pen focus, if any.
    /// </summary>
    public Window? Window => Window.FromID(WindowId);

    internal PenAxisEventArgs(SDL_PenAxisEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        PenId = e.which;
        PenState = (PenInputFlags)e.pen_state;
        X = e.x;
        Y = e.y;
        Axis = (PenAxis)e.axis;
        Value = e.value;
    }
}
