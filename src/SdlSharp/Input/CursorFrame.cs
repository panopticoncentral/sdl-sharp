namespace SdlSharp.Input;
/// <summary>An animation frame. A zero duration displays the frame indefinitely.</summary>
public readonly record struct CursorFrame(Graphics.Surface Surface, uint DurationMilliseconds);
