namespace SdlSharp.Input;

/// <summary>Event data for sensor updates.</summary>
/// <param name="Which">The sensor instance ID.</param>
/// <param name="Data">Up to six sensor values.</param>
/// <param name="SensorTimestamp">The sensor reading timestamp, in nanoseconds.</param>
public readonly record struct SensorEventArgs(uint Which, float[] Data, ulong SensorTimestamp);

/// <summary>Event data for audio-device hotplug events.</summary>
/// <param name="Type">The specific audio device event type.</param>
/// <param name="Which">The audio device ID.</param>
/// <param name="IsRecording">True if a recording device, false if playback.</param>
public readonly record struct AudioDeviceEventArgs(EventType Type, uint Which, bool IsRecording);

/// <summary>Event data for camera-device hotplug/permission events.</summary>
/// <param name="Type">The specific camera device event type.</param>
/// <param name="Which">The camera device ID.</param>
public readonly record struct CameraDeviceEventArgs(EventType Type, uint Which);

/// <summary>Event data for render-target/device reset/lost events.</summary>
/// <param name="Type">The specific render event type.</param>
/// <param name="WindowId">The window whose renderer is affected.</param>
public readonly record struct RenderEventArgs(EventType Type, uint WindowId);

/// <summary>Event data for clipboard-update events.</summary>
/// <param name="OwnerIsSelf">True if this application owns the clipboard.</param>
/// <param name="MimeTypes">The available MIME types.</param>
public readonly record struct ClipboardEventArgs(bool OwnerIsSelf, string?[] MimeTypes);
