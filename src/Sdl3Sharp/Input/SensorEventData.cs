using static Sdl3Sharp.Native.Events;

namespace Sdl3Sharp.Input;

/// <summary>
/// Data for sensor events.
/// </summary>
public readonly unsafe struct SensorEventData
{
    private readonly SDL_SensorEvent _event;

    internal SensorEventData(SDL_SensorEvent e) => _event = e;

    /// <summary>Gets the sensor instance ID.</summary>
    public uint SensorId => _event.which;

    /// <summary>Gets the sensor data values (up to 6 values).</summary>
    public ReadOnlySpan<float> Data
    {
        get
        {
            fixed (float* ptr = _event.data)
            {
                return new ReadOnlySpan<float>(ptr, 6);
            }
        }
    }

    /// <summary>Gets the timestamp of the sensor reading in nanoseconds.</summary>
    public ulong SensorTimestampNs => _event.sensor_timestamp;
}
