using static Sdl3Sharp.Native.Sensor;

namespace Sdl3Sharp.Input;

/// <summary>
/// The different sensor types defined by SDL.
/// </summary>
public enum SensorType
{
    /// <summary>Returned for an invalid sensor.</summary>
    Invalid = SDL_SensorType.SDL_SENSOR_INVALID,
    /// <summary>Unknown sensor type.</summary>
    Unknown = SDL_SensorType.SDL_SENSOR_UNKNOWN,
    /// <summary>Accelerometer.</summary>
    Accelerometer = SDL_SensorType.SDL_SENSOR_ACCEL,
    /// <summary>Gyroscope.</summary>
    Gyroscope = SDL_SensorType.SDL_SENSOR_GYRO,
    /// <summary>Accelerometer for left Joy-Con controller and Wii nunchuk.</summary>
    AccelerometerLeft = SDL_SensorType.SDL_SENSOR_ACCEL_L,
    /// <summary>Gyroscope for left Joy-Con controller.</summary>
    GyroscopeLeft = SDL_SensorType.SDL_SENSOR_GYRO_L,
    /// <summary>Accelerometer for right Joy-Con controller.</summary>
    AccelerometerRight = SDL_SensorType.SDL_SENSOR_ACCEL_R,
    /// <summary>Gyroscope for right Joy-Con controller.</summary>
    GyroscopeRight = SDL_SensorType.SDL_SENSOR_GYRO_R
}
