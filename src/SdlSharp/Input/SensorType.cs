// src/SdlSharp/Input/SensorType.cs
namespace SdlSharp.Input;

/// <summary>
/// Types of sensors.
/// </summary>
public enum SensorType
{
    Invalid = (int)Native.SDL_SensorType.SDL_SENSOR_INVALID,
    Unknown = (int)Native.SDL_SensorType.SDL_SENSOR_UNKNOWN,
    Accelerometer = (int)Native.SDL_SensorType.SDL_SENSOR_ACCEL,
    Gyroscope = (int)Native.SDL_SensorType.SDL_SENSOR_GYRO,
    AccelerometerLeft = (int)Native.SDL_SensorType.SDL_SENSOR_ACCEL_L,
    GyroscopeLeft = (int)Native.SDL_SensorType.SDL_SENSOR_GYRO_L,
    AccelerometerRight = (int)Native.SDL_SensorType.SDL_SENSOR_ACCEL_R,
    GyroscopeRight = (int)Native.SDL_SensorType.SDL_SENSOR_GYRO_R,
}
