namespace Sdl3Sharp;

/// <summary>
/// Base class for all SDL event arguments.
/// </summary>
public class SdlEventArgs : EventArgs
{
    /// <summary>
    /// Gets the timestamp of when this event occurred, in nanoseconds.
    /// </summary>
    public ulong TimestampNs { get; }

    /// <summary>
    /// Gets the timestamp of when this event occurred.
    /// </summary>
    public TimeSpan Timestamp => TimeSpan.FromMicroseconds(TimestampNs / 1000.0);

    /// <summary>
    /// Initializes a new instance of the <see cref="SdlEventArgs"/> class.
    /// </summary>
    /// <param name="timestampNs">The timestamp in nanoseconds.</param>
    internal SdlEventArgs(ulong timestampNs)
    {
        TimestampNs = timestampNs;
    }
}
