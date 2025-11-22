using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Sensor;

namespace Sdl3Sharp.Input;

/// <summary>
/// Describes a sensor that is connected to the system but not yet opened.
/// Use <see cref="Open"/> to open the sensor for reading data.
/// </summary>
/// <param name="id">The underlying SDL sensor ID.</param>
public readonly unsafe struct SensorDescriptor(SDL_SensorID id)
{
    /// <summary>
    /// Gets the underlying SDL sensor ID.
    /// </summary>
    public SDL_SensorID Id { get; } = id;

    /// <summary>
    /// Gets the implementation dependent name of this sensor.
    /// </summary>
    public string Name => CheckErrorNull(SDL_GetSensorNameForID(Id));

    /// <summary>
    /// Gets the type of this sensor.
    /// </summary>
    public SensorType Type => (SensorType)SDL_GetSensorTypeForID(Id);

    /// <summary>
    /// Gets the platform dependent type of this sensor.
    /// Returns -1 if the sensor ID is not valid.
    /// </summary>
    public int NonPortableType => SDL_GetSensorNonPortableTypeForID(Id);

    /// <summary>
    /// Opens this sensor for use.
    /// </summary>
    /// <returns>A new Sensor instance.</returns>
    public Sensor Open()
    {
        return new(CheckErrorPointer(SDL_OpenSensor(Id)), ownsHandle: true);
    }

    /// <summary>
    /// Gets the Sensor associated with a descriptor, if it is already open.
    /// </summary>
    /// <returns>A Sensor instance.</returns>
    public static Sensor? Get(SensorDescriptor descriptor)
    {
        return new(CheckErrorPointer(SDL_GetSensorFromID(descriptor.Id)), ownsHandle: false);
    }

    /// <summary>
    /// Implicitly converts an SDL_SensorID to a SensorDescriptor.
    /// </summary>
    /// <param name="id">The sensor ID to convert.</param>
    public static implicit operator SensorDescriptor(SDL_SensorID id)
    {
        return new(id);
    }

    /// <summary>
    /// Implicitly converts a SensorDescriptor to an SDL_SensorID.
    /// </summary>
    /// <param name="descriptor">The descriptor to convert.</param>
    public static implicit operator SDL_SensorID(SensorDescriptor descriptor)
    {
        return descriptor.Id;
    }
}
