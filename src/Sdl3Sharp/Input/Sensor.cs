using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Sensor;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp.Input;

/// <summary>
/// Represents an opened SDL sensor device for accessing gyros and accelerometers.
/// </summary>
public sealed unsafe class Sensor : IDisposable
{
    /// <summary>
    /// Occurs when a sensor is updated.
    /// </summary>
    public static event EventHandler<SensorEventArgs>? Updated;

    internal static void DispatchEvent(Event e)
    {
        switch (e.Type)
        {
            case EventType.SensorUpdate:
                Updated?.Invoke(null, (SensorEventArgs)e.TranslateEvent());
                break;
        }
    }

    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// A constant to represent standard gravity for accelerometer sensors.
    /// The accelerometer returns the current acceleration in SI meters per second squared.
    /// </summary>
    public const float StandardGravity = SDL_STANDARD_GRAVITY;

    /// <summary>
    /// Gets the underlying SDL_Sensor pointer.
    /// </summary>
    public SDL_Sensor* Handle { get; private set; }

    /// <summary>
    /// Gets the properties associated with this sensor.
    /// </summary>
    public PropertyGroup Properties
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetSensorProperties(Handle)), ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets the implementation dependent name of this sensor.
    /// </summary>
    public string Name
    {
        get
        {
            ThrowIfDisposed();
            return CheckErrorNull(SDL_GetSensorName(Handle));
        }
    }

    /// <summary>
    /// Gets the type of this sensor.
    /// </summary>
    public SensorType Type
    {
        get
        {
            ThrowIfDisposed();
            return (SensorType)SDL_GetSensorType(Handle);
        }
    }

    /// <summary>
    /// Gets the platform dependent type of this sensor.
    /// </summary>
    public int NonPortableType
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetSensorNonPortableType(Handle);
        }
    }

    /// <summary>
    /// Gets the descriptor for this sensor.
    /// </summary>
    public SensorDescriptor Descriptor
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetSensorID(Handle)));
        }
    }

    /// <summary>
    /// Gets a list of currently connected sensors.
    /// </summary>
    /// <returns>An array of sensor descriptors.</returns>
    public static SensorDescriptor[] GetSensors()
    {
        int count;
        SDL_SensorID* sensors = CheckErrorPointer(SDL_GetSensors(&count));

        try
        {
            var result = new SensorDescriptor[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = new SensorDescriptor(sensors[i]);
            }

            return result;
        }
        finally
        {
            SDL_free(sensors);
        }
    }

    /// <summary>
    /// Updates the current state of all open sensors.
    /// This is called automatically by the event loop if sensor events are enabled.
    /// This needs to be called from the thread that initialized the sensor subsystem.
    /// </summary>
    public static void UpdateAll()
    {
        SDL_UpdateSensors();
    }

    /// <summary>
    /// Wraps an existing SDL_Sensor pointer.
    /// </summary>
    /// <param name="handle">The SDL_Sensor pointer to wrap.</param>
    /// <param name="ownsHandle">Whether this instance should close the sensor when disposed.</param>
    internal Sensor(SDL_Sensor* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Gets the current state of this sensor.
    /// The number of values and interpretation of the data is sensor dependent.
    /// </summary>
    /// <param name="data">A span to receive the sensor data.</param>
    public void GetData(Span<float> data)
    {
        ThrowIfDisposed();

        fixed (float* ptr = data)
        {
            _ = CheckErrorBool(SDL_GetSensorData(Handle, ptr, data.Length));
        }
    }

    /// <summary>
    /// Gets the current state of this sensor.
    /// </summary>
    /// <param name="numValues">The number of values to read.</param>
    /// <returns>An array containing the sensor data.</returns>
    public float[] GetData(int numValues)
    {
        var data = new float[numValues];
        GetData(data);
        return data;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Handle is not null)
        {
            SDL_CloseSensor(Handle);
            Handle = null;
        }

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
