namespace SdlSharp.Input
{
    /// <summary>
    /// A game controller sensor.
    /// </summary>
    public sealed unsafe class GameControllerSensor
    {
        private readonly Native.SDL_GameController* _gameController;
        private readonly Native.SDL_SensorType _sensorType;

        /// <summary>
        /// Whether the sensor is enabled.
        /// </summary>
        public bool Enabled
        {
            get => Native.SDL_GameControllerIsSensorEnabled(_gameController, _sensorType);
            set => Native.CheckError(Native.SDL_GameControllerSetSensorEnabled(_gameController, _sensorType, value));
        }

        /// <summary>
        /// The data rate of the sensor.
        /// </summary>
        public float DataRate => Native.SDL_GameControllerGetSensorDataRate(_gameController, _sensorType);

        /// <summary>
        /// Gets the sensor data.
        /// </summary>
        /// <param name="data">The data storage.</param>
        public void GetData(Span<float> data)
        {
            fixed (float* ptr = data)
            {
                _ = Native.CheckError(Native.SDL_GameControllerGetSensorData(_gameController, _sensorType, ptr, data.Length));
            }
        }

        /// <summary>
        /// Gets the sensor data with a timestamp.
        /// </summary>
        /// <param name="data">The data storage.</param>
        /// <param name="timestamp">The timestamp.</param>
        public void GetData(Span<float> data, out ulong timestamp)
        {
            fixed (float* ptr = data)
            {
                _ = Native.CheckError(Native.SDL_GameControllerGetSensorDataWithTimestamp(_gameController, _sensorType, &timestamp, ptr, data.Length));
            }
        }

        internal GameControllerSensor(Native.SDL_GameController* gameController, Native.SDL_SensorType type)
        {
            _gameController = gameController;
            _sensorType = type;
        }
    }
}
