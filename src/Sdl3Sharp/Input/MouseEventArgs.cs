using Sdl3Sharp.Graphics;
using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Event arguments for mouse motion events.
/// </summary>
public sealed class MouseMotionEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID with mouse focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the mouse instance ID.
    /// </summary>
    public uint MouseId { get; }

    /// <summary>
    /// Gets the current button state.
    /// </summary>
    public MousePressedButtons ButtonState { get; }

    /// <summary>
    /// Gets the X coordinate relative to the window.
    /// </summary>
    public float X { get; }

    /// <summary>
    /// Gets the Y coordinate relative to the window.
    /// </summary>
    public float Y { get; }

    /// <summary>
    /// Gets the relative motion in the X direction.
    /// </summary>
    public float RelativeX { get; }

    /// <summary>
    /// Gets the relative motion in the Y direction.
    /// </summary>
    public float RelativeY { get; }

    /// <summary>
    /// Gets the position relative to the window.
    /// </summary>
    public PointF Position => new(X, Y);

    /// <summary>
    /// Gets the relative motion.
    /// </summary>
    public PointF RelativeMotion => new(RelativeX, RelativeY);

    /// <summary>
    /// Gets the window with mouse focus, if any.
    /// </summary>
    public Window? Window => Window.FromID(WindowId);

    internal MouseMotionEventArgs(SDL_MouseMotionEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        MouseId = e.which;
        ButtonState = (MousePressedButtons)e.state.Value;
        X = e.x;
        Y = e.y;
        RelativeX = e.xrel;
        RelativeY = e.yrel;
    }
}

/// <summary>
/// Event arguments for mouse button events.
/// </summary>
public sealed class MouseButtonEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID with mouse focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the mouse instance ID.
    /// </summary>
    public uint MouseId { get; }

    /// <summary>
    /// Gets the button that was pressed or released.
    /// </summary>
    public MouseButton Button { get; }

    /// <summary>
    /// Gets a value indicating whether the button is pressed.
    /// </summary>
    public bool IsPressed { get; }

    /// <summary>
    /// Gets the number of clicks (1 for single-click, 2 for double-click, etc.).
    /// </summary>
    public byte Clicks { get; }

    /// <summary>
    /// Gets the X coordinate relative to the window.
    /// </summary>
    public float X { get; }

    /// <summary>
    /// Gets the Y coordinate relative to the window.
    /// </summary>
    public float Y { get; }

    /// <summary>
    /// Gets the position relative to the window.
    /// </summary>
    public PointF Position => new(X, Y);

    /// <summary>
    /// Gets the window with mouse focus, if any.
    /// </summary>
    public Window? Window => Window.FromID(WindowId);

    internal MouseButtonEventArgs(SDL_MouseButtonEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        MouseId = e.which;
        Button = (MouseButton)e.button;
        IsPressed = e.down;
        Clicks = e.clicks;
        X = e.x;
        Y = e.y;
    }
}

/// <summary>
/// Event arguments for mouse wheel events.
/// </summary>
public sealed class MouseWheelEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the window ID with mouse focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the mouse instance ID.
    /// </summary>
    public uint MouseId { get; }

    /// <summary>
    /// Gets the horizontal scroll amount (positive = right, negative = left).
    /// </summary>
    public float X { get; }

    /// <summary>
    /// Gets the vertical scroll amount (positive = away from user, negative = toward user).
    /// </summary>
    public float Y { get; }

    /// <summary>
    /// Gets the scroll direction mode.
    /// </summary>
    public MouseWheelDirection Direction { get; }

    /// <summary>
    /// Gets the mouse X position relative to the window.
    /// </summary>
    public float MouseX { get; }

    /// <summary>
    /// Gets the mouse Y position relative to the window.
    /// </summary>
    public float MouseY { get; }

    /// <summary>
    /// Gets the horizontal scroll amount accumulated to whole scroll ticks.
    /// </summary>
    public int IntegerX { get; }

    /// <summary>
    /// Gets the vertical scroll amount accumulated to whole scroll ticks.
    /// </summary>
    public int IntegerY { get; }

    /// <summary>
    /// Gets the mouse position relative to the window.
    /// </summary>
    public PointF MousePosition => new(MouseX, MouseY);

    /// <summary>
    /// Gets the window with mouse focus, if any.
    /// </summary>
    public Window? Window => Window.FromID(WindowId);

    internal MouseWheelEventArgs(SDL_MouseWheelEvent e) : base(e.timestamp)
    {
        WindowId = e.windowID;
        MouseId = e.which;
        X = e.x;
        Y = e.y;
        Direction = (MouseWheelDirection)e.direction;
        MouseX = e.mouse_x;
        MouseY = e.mouse_y;
        IntegerX = e.integer_x;
        IntegerY = e.integer_y;
    }
}

/// <summary>
/// Event arguments for mouse device events.
/// </summary>
public sealed class MouseDeviceEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the mouse instance ID.
    /// </summary>
    public uint MouseId { get; }

    internal MouseDeviceEventArgs(SDL_MouseDeviceEvent e) : base(e.timestamp)
    {
        MouseId = e.which;
    }
}
