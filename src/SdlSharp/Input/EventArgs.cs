namespace SdlSharp.Input;

/// <summary>
/// Event data for keyboard key events.
/// </summary>
/// <param name="WindowId">The window ID with keyboard focus.</param>
/// <param name="Scancode">The physical key scancode.</param>
/// <param name="Key">The virtual key code.</param>
/// <param name="Modifiers">Active key modifiers.</param>
/// <param name="IsDown">True if the key is pressed.</param>
/// <param name="IsRepeat">True if this is a key repeat.</param>
public readonly record struct KeyEventArgs(
    uint WindowId,
    Scancode Scancode,
    Keycode Key,
    KeyModifiers Modifiers,
    bool IsDown,
    bool IsRepeat);

/// <summary>
/// Event data for text input events.
/// </summary>
/// <param name="WindowId">The window ID with keyboard focus.</param>
/// <param name="Text">The input text (UTF-8).</param>
public readonly record struct TextInputEventArgs(
    uint WindowId,
    string? Text);

/// <summary>
/// Event data for mouse motion events.
/// </summary>
/// <param name="WindowId">The window with mouse focus.</param>
/// <param name="X">X coordinate relative to the window.</param>
/// <param name="Y">Y coordinate relative to the window.</param>
/// <param name="RelativeX">Relative X motion.</param>
/// <param name="RelativeY">Relative Y motion.</param>
/// <param name="ButtonState">Current button bitmask.</param>
public readonly record struct MouseMotionEventArgs(
    uint WindowId,
    float X, float Y,
    float RelativeX, float RelativeY,
    uint ButtonState);

/// <summary>
/// Event data for mouse button events.
/// </summary>
/// <param name="WindowId">The window with mouse focus.</param>
/// <param name="Button">The mouse button.</param>
/// <param name="IsDown">True if the button is pressed.</param>
/// <param name="Clicks">Number of clicks (1 = single, 2 = double).</param>
/// <param name="X">X coordinate relative to the window.</param>
/// <param name="Y">Y coordinate relative to the window.</param>
public readonly record struct MouseButtonEventArgs(
    uint WindowId,
    MouseButton Button,
    bool IsDown,
    byte Clicks,
    float X, float Y);

/// <summary>
/// Event data for mouse wheel events.
/// </summary>
/// <param name="WindowId">The window with mouse focus.</param>
/// <param name="X">Horizontal scroll amount.</param>
/// <param name="Y">Vertical scroll amount.</param>
/// <param name="MouseX">Mouse X position.</param>
/// <param name="MouseY">Mouse Y position.</param>
/// <param name="Direction">The scroll direction. When <see cref="MouseWheelDirection.Flipped"/>, the scroll amounts are inverted relative to normal scrolling. Multiply <see cref="X"/> and <see cref="Y"/> by -1 to normalize the values when the direction is <see cref="MouseWheelDirection.Flipped"/>.</param>
public readonly record struct MouseWheelEventArgs(
    uint WindowId,
    float X, float Y,
    float MouseX, float MouseY,
    MouseWheelDirection Direction);

/// <summary>
/// Event data for window events.
/// </summary>
/// <param name="WindowId">The window ID.</param>
/// <param name="Data1">Event-dependent data.</param>
/// <param name="Data2">Event-dependent data.</param>
public readonly record struct WindowEventArgs(
    uint WindowId,
    int Data1, int Data2);

/// <summary>
/// Event data for quit events.
/// </summary>
public readonly record struct QuitEventArgs;

/// <summary>
/// Event data for user-defined events (types at or above <see cref="EventType.User"/>).
/// </summary>
/// <param name="Type">The registered event type.</param>
/// <param name="WindowId">The associated window ID, or 0 if none.</param>
/// <param name="Code">The user-defined event code.</param>
/// <param name="Data1">The first user-defined data pointer; the caller owns any pointed-to memory.</param>
/// <param name="Data2">The second user-defined data pointer; the caller owns any pointed-to memory.</param>
public readonly record struct UserEventArgs(
    EventType Type,
    uint WindowId,
    int Code,
    nint Data1,
    nint Data2);
