namespace SdlSharp.Input;

/// <summary>Event data for touch finger events.</summary>
/// <param name="TouchId">The touch device ID.</param>
/// <param name="FingerId">The finger ID.</param>
/// <param name="X">Normalized X (0-1).</param>
/// <param name="Y">Normalized Y (0-1).</param>
/// <param name="DeltaX">Normalized X motion.</param>
/// <param name="DeltaY">Normalized Y motion.</param>
/// <param name="Pressure">Normalized pressure (0-1).</param>
/// <param name="WindowId">The window under the touch.</param>
public readonly record struct TouchFingerEventArgs(
    ulong TouchId, ulong FingerId, float X, float Y, float DeltaX, float DeltaY, float Pressure, uint WindowId);

/// <summary>Event data for pinch gesture events.</summary>
/// <param name="Scale">The relative pinch scale factor.</param>
/// <param name="WindowId">The window under the gesture.</param>
public readonly record struct PinchFingerEventArgs(float Scale, uint WindowId);
