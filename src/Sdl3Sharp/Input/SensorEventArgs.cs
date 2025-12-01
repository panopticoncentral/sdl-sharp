using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Event arguments for sensor update events.
/// </summary>
public sealed unsafe class SensorEventArgs : SdlEventArgs
{
    /// <summary>
    /// Gets the sensor instance ID.
    /// </summary>
    public uint SensorId { get; }

    /// <summary>
    /// Gets the sensor data (up to 6 values).
    /// </summary>
    public float[] Data { get; }

    /// <summary>
    /// Gets the timestamp of the sensor reading in nanoseconds.
    /// </summary>
    public ulong SensorTimestampNs { get; }

    internal SensorEventArgs(SDL_SensorEvent e) : base(e.timestamp)
    {
        SensorId = e.which;
        Data = [e.data[0], e.data[1], e.data[2], e.data[3], e.data[4], e.data[5]];
        SensorTimestampNs = e.sensor_timestamp;
    }
}
