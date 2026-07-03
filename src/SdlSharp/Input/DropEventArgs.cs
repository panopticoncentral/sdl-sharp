namespace SdlSharp.Input;

/// <summary>Event data for drag-and-drop events.</summary>
/// <param name="Type">The specific drop event type.</param>
/// <param name="WindowId">The window the drop targets.</param>
/// <param name="X">Drop X coordinate relative to the window (for position/complete).</param>
/// <param name="Y">Drop Y coordinate relative to the window (for position/complete).</param>
/// <param name="Source">The drag source application, or null.</param>
/// <param name="Data">The dropped file path or text, or null (for file/text events).</param>
public readonly record struct DropEventArgs(
    EventType Type, uint WindowId, float X, float Y, string? Source, string? Data);
