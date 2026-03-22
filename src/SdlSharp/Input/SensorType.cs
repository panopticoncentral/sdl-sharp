namespace SdlSharp.Input;

/// <summary>
/// Types of sensors.
/// </summary>
public enum SensorType
{
    /// <summary>Invalid sensor type.</summary>
    Invalid = (int)Native.SDL_SensorType.SDL_SENSOR_INVALID,
    /// <summary>Unknown sensor type.</summary>
    Unknown = (int)Native.SDL_SensorType.SDL_SENSOR_UNKNOWN,
    /// <summary>Accelerometer sensor.</summary>
    Accelerometer = (int)Native.SDL_SensorType.SDL_SENSOR_ACCEL,
    /// <summary>Gyroscope sensor.</summary>
    Gyroscope = (int)Native.SDL_SensorType.SDL_SENSOR_GYRO,
    /// <summary>Accelerometer sensor for the left Joy-Con.</summary>
    AccelerometerLeft = (int)Native.SDL_SensorType.SDL_SENSOR_ACCEL_L,
    /// <summary>Gyroscope sensor for the left Joy-Con.</summary>
    GyroscopeLeft = (int)Native.SDL_SensorType.SDL_SENSOR_GYRO_L,
    /// <summary>Accelerometer sensor for the right Joy-Con.</summary>
    AccelerometerRight = (int)Native.SDL_SensorType.SDL_SENSOR_ACCEL_R,
    /// <summary>Gyroscope sensor for the right Joy-Con.</summary>
    GyroscopeRight = (int)Native.SDL_SensorType.SDL_SENSOR_GYRO_R,
}
