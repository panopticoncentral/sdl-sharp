namespace SdlSharp.Input;

/// <summary>Event data for display events (orientation, add/remove, moved, etc.).</summary>
/// <param name="Type">The specific display event type.</param>
/// <param name="DisplayId">The display instance ID.</param>
/// <param name="Data1">Event-dependent data.</param>
/// <param name="Data2">Event-dependent data.</param>
public readonly record struct DisplayEventArgs(EventType Type, uint DisplayId, int Data1, int Data2);
