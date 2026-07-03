namespace SdlSharp.Input;

/// <summary>Event data for pen proximity (enter/leave) events.</summary>
/// <param name="WindowId">The window with pen focus.</param>
/// <param name="Which">The pen instance ID.</param>
public readonly record struct PenProximityEventArgs(uint WindowId, uint Which);

/// <summary>Event data for pen motion events.</summary>
/// <param name="WindowId">The window with pen focus.</param>
/// <param name="Which">The pen instance ID.</param>
/// <param name="X">X coordinate relative to the window.</param>
/// <param name="Y">Y coordinate relative to the window.</param>
public readonly record struct PenMotionEventArgs(uint WindowId, uint Which, float X, float Y);

/// <summary>Event data for pen touch (down/up) events.</summary>
/// <param name="WindowId">The window with pen focus.</param>
/// <param name="Which">The pen instance ID.</param>
/// <param name="X">X coordinate relative to the window.</param>
/// <param name="Y">Y coordinate relative to the window.</param>
/// <param name="IsEraser">True if the eraser end is used.</param>
/// <param name="IsDown">True if the pen is touching.</param>
public readonly record struct PenTouchEventArgs(uint WindowId, uint Which, float X, float Y, bool IsEraser, bool IsDown);

/// <summary>Event data for pen button events.</summary>
/// <param name="WindowId">The window with pen focus.</param>
/// <param name="Which">The pen instance ID.</param>
/// <param name="X">X coordinate relative to the window.</param>
/// <param name="Y">Y coordinate relative to the window.</param>
/// <param name="Button">The pen button index.</param>
/// <param name="IsDown">True if pressed.</param>
public readonly record struct PenButtonEventArgs(uint WindowId, uint Which, float X, float Y, byte Button, bool IsDown);

/// <summary>Event data for pen axis events.</summary>
/// <param name="WindowId">The window with pen focus.</param>
/// <param name="Which">The pen instance ID.</param>
/// <param name="X">X coordinate relative to the window.</param>
/// <param name="Y">Y coordinate relative to the window.</param>
/// <param name="Axis">The axis that changed.</param>
/// <param name="Value">The new axis value.</param>
public readonly record struct PenAxisEventArgs(uint WindowId, uint Which, float X, float Y, PenAxis Axis, float Value);
