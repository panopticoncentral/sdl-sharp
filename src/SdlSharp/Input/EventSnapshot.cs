namespace SdlSharp.Input;
/// <summary>Retained event metadata copied from a transient raw event view. Use typed event arguments for specific payload fields.</summary>
public sealed record EventSnapshot(EventType Type, ulong TimestampNanoseconds, uint? WindowId, string Description);
