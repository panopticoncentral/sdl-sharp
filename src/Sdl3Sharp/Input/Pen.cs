using static Sdl3Sharp.Native.Pen;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents a pen (stylus) input device.
/// Pens may provide more than simple touch input; they might have other axes,
/// such as pressure, tilt, rotation, etc.
/// </summary>
/// <remarks>
/// To get started with pens, handle pen events from the event system. When a pen
/// starts providing input, SDL will assign it a unique ID, which will remain for
/// the life of the process, as long as the pen stays connected.
/// </remarks>
/// <param name="Id">The unique pen instance ID.</param>
public readonly record struct Pen(uint Id)
{
    /// <summary>
    /// Occurs when a pen has become available (entered proximity of the tablet).
    /// </summary>
    public static event EventHandler<PenProximityEventArgs>? ProximityIn;

    /// <summary>
    /// Occurs when a pen has become unavailable (left proximity of the tablet).
    /// </summary>
    public static event EventHandler<PenProximityEventArgs>? ProximityOut;

    /// <summary>
    /// Occurs when a pen touches the drawing surface.
    /// </summary>
    public static event EventHandler<PenTouchEventArgs>? Down;

    /// <summary>
    /// Occurs when a pen is lifted from the drawing surface.
    /// </summary>
    public static event EventHandler<PenTouchEventArgs>? Up;

    /// <summary>
    /// Occurs when a pen button is pressed.
    /// </summary>
    public static event EventHandler<PenButtonEventArgs>? ButtonDown;

    /// <summary>
    /// Occurs when a pen button is released.
    /// </summary>
    public static event EventHandler<PenButtonEventArgs>? ButtonUp;

    /// <summary>
    /// Occurs when a pen is moving on the tablet.
    /// </summary>
    public static event EventHandler<PenMotionEventArgs>? Motion;

    /// <summary>
    /// Occurs when a pen axis changes (pressure, tilt, etc.).
    /// </summary>
    public static event EventHandler<PenAxisEventArgs>? Axis;

    internal static void DispatchEvent(Event e)
    {
        switch (e.Type)
        {
            case EventType.PenProximityIn:
                ProximityIn?.Invoke(null, (PenProximityEventArgs)e.TranslateEvent());
                break;
            case EventType.PenProximityOut:
                ProximityOut?.Invoke(null, (PenProximityEventArgs)e.TranslateEvent());
                break;
            case EventType.PenDown:
                Down?.Invoke(null, (PenTouchEventArgs)e.TranslateEvent());
                break;
            case EventType.PenUp:
                Up?.Invoke(null, (PenTouchEventArgs)e.TranslateEvent());
                break;
            case EventType.PenButtonDown:
                ButtonDown?.Invoke(null, (PenButtonEventArgs)e.TranslateEvent());
                break;
            case EventType.PenButtonUp:
                ButtonUp?.Invoke(null, (PenButtonEventArgs)e.TranslateEvent());
                break;
            case EventType.PenMotion:
                Motion?.Invoke(null, (PenMotionEventArgs)e.TranslateEvent());
                break;
            case EventType.PenAxis:
                Axis?.Invoke(null, (PenAxisEventArgs)e.TranslateEvent());
                break;
        }
    }

    /// <summary>
    /// The mouse for mouse events simulated with pen input.
    /// Use this to filter out simulated mouse events when processing pen input separately.
    /// </summary>
    public static Mouse PenMouse => new(SDL_PEN_MOUSEID);

    /// <summary>
    /// The touch device for touch events simulated with pen input.
    /// Use this to filter out simulated touch events when processing pen input separately.
    /// </summary>
    public static TouchDevice PenTouch => new(SDL_PEN_TOUCHID);
}
