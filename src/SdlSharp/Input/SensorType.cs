namespace SdlSharp.Input
{
    /// <summary>
    /// The type of a sensor.
    /// </summary>
    public enum SensorType
    {
        /// <summary>
        /// Invalid
        /// </summary>
        Invalid = Native.SDL_SensorType.SDL_SENSOR_INVALID,

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = Native.SDL_SensorType.SDL_SENSOR_UNKNOWN,

        /// <summary>
        /// Accelerometer
        /// </summary>
        Accelerometer = Native.SDL_SensorType.SDL_SENSOR_ACCEL,

        /// <summary>
        /// Gyroscope
        /// </summary>
        Gyroscope = Native.SDL_SensorType.SDL_SENSOR_GYRO,

        /// <summary>
        /// Accelerometer for left Joy-Con controller and Wii nunchuk.
        /// </summary>
        AccelerometerLeft = Native.SDL_SensorType.SDL_SENSOR_ACCEL_L,

        /// <summary>
        /// Gyroscope for left Joy-Con controller.
        /// </summary>
        GyroscopeLeft = Native.SDL_SensorType.SDL_SENSOR_GYRO_L,

        /// <summary>
        /// Accelerometer for right Joy-Con controller.
        /// </summary>
        AccelerometerRight = Native.SDL_SensorType.SDL_SENSOR_ACCEL_R,

        /// <summary>
        /// Gyroscope for right Joy-Con controller.
        /// </summary>
        GyroscopeRight = Native.SDL_SensorType.SDL_SENSOR_GYRO_R
    }
}
