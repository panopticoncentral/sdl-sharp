namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for a game controller sensor event.
    /// </summary>
    public sealed class GameControllerSensorEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The sensor.
        /// </summary>
        public SensorType Sensor { get; }

        /// <summary>
        /// The sensor data.
        /// </summary>
        public float[] Data { get; }

        /// <summary>
        /// The sensor data timestamp.
        /// </summary>
        public ulong SensorTimestamp { get; }

        internal GameControllerSensorEventArgs(Native.SDL_ControllerSensorEvent sensor) : base(sensor.timestamp)
        {
            Sensor = (SensorType)sensor.sensor;
            Data = new[] { sensor.data1, sensor.data2, sensor.data3 };
            SensorTimestamp = sensor.timestamp;
        }
    }
}
