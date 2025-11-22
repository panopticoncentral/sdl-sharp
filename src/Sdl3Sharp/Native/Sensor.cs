using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_sensor.h - Sensor management for gyros and accelerometers.
/// </summary>
public static unsafe partial class Sensor
{
    /// <summary>
    /// A unique ID for a sensor for the time it is connected to the system,
    /// and is never reused for the lifetime of the application.
    /// The value 0 is an invalid ID.
    /// </summary>
    /// <param name="value">The sensor ID value.</param>
    public readonly struct SDL_SensorID(uint value)
    {
        /// <summary>The underlying sensor ID value.</summary>
        public readonly uint Value = value;

        /// <summary>Implicitly converts an SDL_SensorID to uint.</summary>
        /// <param name="id">The sensor ID to convert.</param>
        public static implicit operator uint(SDL_SensorID id)
        {
            return id.Value;
        }

        /// <summary>Implicitly converts a uint to SDL_SensorID.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_SensorID(uint value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// A constant to represent standard gravity for accelerometer sensors.
    /// The accelerometer returns the current acceleration in SI meters per second
    /// squared. This measurement includes the force of gravity, so a device at
    /// rest will have a value of SDL_STANDARD_GRAVITY away from the center of the
    /// earth, which is a positive Y value.
    /// </summary>
    public const float SDL_STANDARD_GRAVITY = 9.80665f;

    /// <summary>
    /// The different sensors defined by SDL.
    /// Additional sensors may be available, using platform dependent semantics.
    /// </summary>
    public enum SDL_SensorType
    {
        /// <summary>Returned for an invalid sensor.</summary>
        SDL_SENSOR_INVALID = -1,
        /// <summary>Unknown sensor type.</summary>
        SDL_SENSOR_UNKNOWN,
        /// <summary>Accelerometer.</summary>
        SDL_SENSOR_ACCEL,
        /// <summary>Gyroscope.</summary>
        SDL_SENSOR_GYRO,
        /// <summary>Accelerometer for left Joy-Con controller and Wii nunchuk.</summary>
        SDL_SENSOR_ACCEL_L,
        /// <summary>Gyroscope for left Joy-Con controller.</summary>
        SDL_SENSOR_GYRO_L,
        /// <summary>Accelerometer for right Joy-Con controller.</summary>
        SDL_SENSOR_ACCEL_R,
        /// <summary>Gyroscope for right Joy-Con controller.</summary>
        SDL_SENSOR_GYRO_R,
        /// <summary>Number of sensor types.</summary>
        SDL_SENSOR_COUNT
    }

    /// <summary>
    /// Get a list of currently connected sensors.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of sensors returned, may be NULL.</param>
    /// <returns>A 0 terminated array of sensor instance IDs or NULL on failure; call SDL_GetError() for more information. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_SensorID* SDL_GetSensors(int* count);

    /// <summary>
    /// Get the implementation dependent name of a sensor.
    /// This can be called before any sensors are opened.
    /// </summary>
    /// <param name="instance_id">The sensor instance ID.</param>
    /// <returns>The sensor name, or NULL if instance_id is not valid.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetSensorNameForID(SDL_SensorID instance_id);

    /// <summary>
    /// Get the type of a sensor.
    /// This can be called before any sensors are opened.
    /// </summary>
    /// <param name="instance_id">The sensor instance ID.</param>
    /// <returns>The SDL_SensorType, or SDL_SENSOR_INVALID if instance_id is not valid.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_SensorType SDL_GetSensorTypeForID(SDL_SensorID instance_id);

    /// <summary>
    /// Get the platform dependent type of a sensor.
    /// This can be called before any sensors are opened.
    /// </summary>
    /// <param name="instance_id">The sensor instance ID.</param>
    /// <returns>The sensor platform dependent type, or -1 if instance_id is not valid.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetSensorNonPortableTypeForID(SDL_SensorID instance_id);

    /// <summary>
    /// Open a sensor for use.
    /// </summary>
    /// <param name="instance_id">The sensor instance ID.</param>
    /// <returns>An SDL_Sensor object or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Sensor* SDL_OpenSensor(SDL_SensorID instance_id);

    /// <summary>
    /// Return the SDL_Sensor associated with an instance ID.
    /// </summary>
    /// <param name="instance_id">The sensor instance ID.</param>
    /// <returns>An SDL_Sensor object or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Sensor* SDL_GetSensorFromID(SDL_SensorID instance_id);

    /// <summary>
    /// Get the properties associated with a sensor.
    /// </summary>
    /// <param name="sensor">The SDL_Sensor object.</param>
    /// <returns>A valid property ID on success or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_GetSensorProperties(SDL_Sensor* sensor);

    /// <summary>
    /// Get the implementation dependent name of a sensor.
    /// </summary>
    /// <param name="sensor">The SDL_Sensor object.</param>
    /// <returns>The sensor name or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetSensorName(SDL_Sensor* sensor);

    /// <summary>
    /// Get the type of a sensor.
    /// </summary>
    /// <param name="sensor">The SDL_Sensor object to inspect.</param>
    /// <returns>The SDL_SensorType type, or SDL_SENSOR_INVALID if sensor is NULL.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_SensorType SDL_GetSensorType(SDL_Sensor* sensor);

    /// <summary>
    /// Get the platform dependent type of a sensor.
    /// </summary>
    /// <param name="sensor">The SDL_Sensor object to inspect.</param>
    /// <returns>The sensor platform dependent type, or -1 if sensor is NULL.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetSensorNonPortableType(SDL_Sensor* sensor);

    /// <summary>
    /// Get the instance ID of a sensor.
    /// </summary>
    /// <param name="sensor">The SDL_Sensor object to inspect.</param>
    /// <returns>The sensor instance ID, or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_SensorID SDL_GetSensorID(SDL_Sensor* sensor);

    /// <summary>
    /// Get the current state of an opened sensor.
    /// The number of values and interpretation of the data is sensor dependent.
    /// </summary>
    /// <param name="sensor">The SDL_Sensor object to query.</param>
    /// <param name="data">A pointer filled with the current sensor state.</param>
    /// <param name="num_values">The number of values to write to data.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetSensorData(SDL_Sensor* sensor, float* data, int num_values);

    /// <summary>
    /// Close a sensor previously opened with SDL_OpenSensor().
    /// </summary>
    /// <param name="sensor">The SDL_Sensor object to close.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_CloseSensor(SDL_Sensor* sensor);

    /// <summary>
    /// Update the current state of the open sensors.
    /// This is called automatically by the event loop if sensor events are enabled.
    /// This needs to be called from the thread that initialized the sensor subsystem.
    /// </summary>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UpdateSensors();

    /// <summary>
    /// The opaque structure used to identify an opened SDL sensor.
    /// </summary>
    public struct SDL_Sensor
    {
    }
}
