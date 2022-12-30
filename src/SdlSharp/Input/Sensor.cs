namespace SdlSharp.Input
{
    /// <summary>
    /// A sensor in the device.
    /// </summary>
    public sealed unsafe class Sensor : IDisposable
    {
        private readonly Native.SDL_Sensor* _sensor;

        /// <summary>
        /// The sensor's name.
        /// </summary>
        public string? Name =>
            Native.Utf8ToString(Native.SDL_SensorGetName(_sensor));

        /// <summary>
        /// The sensor type.
        /// </summary>
        public SensorType Type =>
            (SensorType)Native.SDL_SensorGetType(_sensor);

        /// <summary>
        /// A non-portable sensor type.
        /// </summary>
        public int NonPortableType =>
            Native.SDL_SensorGetNonPortableType(_sensor);

        /// <summary>
        /// An event fired when a sensor is updated.
        /// </summary>
        public static event EventHandler<SensorUpdatedEventArgs>? Updated;

        internal static Sensor Get(Native.SDL_SensorID instanceId) =>
            new(Native.SDL_SensorFromInstanceID(instanceId));

        internal Sensor(Native.SDL_Sensor* sensor)
        {
            _sensor = sensor;
        }

        /// <summary>
        /// Gets the data from the sensor.
        /// </summary>
        /// <param name="data">Where to put the data.</param>
        public void GetData(float[] data)
        {
            fixed (float* ptr = data)
            {
                _ = Native.CheckError(Native.SDL_SensorGetData(_sensor, ptr, data.Length));
            }
        }

        /// <summary>
        /// Gets the data from the sensor.
        /// </summary>
        /// <param name="data">Where to put the data.</param>
        /// <param name="timestamp">The timestamp of the data.</param>
        public void GetData(float[] data, out ulong timestamp)
        {
            ulong timestampLocal;
            fixed (float* ptr = data)
            {
                _ = Native.CheckError(Native.SDL_SensorGetDataWithTimestamp(_sensor, &timestampLocal, ptr, data.Length));
                timestamp = timestampLocal;
            }
        }

        /// <inheritdoc/>
        public void Dispose() => Native.SDL_SensorClose(_sensor);

        /// <summary>
        /// Updates sensor events.
        /// </summary>
        public static void Update() =>
            Native.SDL_SensorUpdate();

        /// <summary>
        /// Locks access to sensors.
        /// </summary>
        public static void Lock() => Native.SDL_LockSensors();

        /// <summary>
        /// Unlocks access to sensors.
        /// </summary>
        public static void Unlock() => Native.SDL_UnlockSensors();

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch ((Native.SDL_EventType)e.type)
            {
                case Native.SDL_EventType.SDL_SENSORUPDATE:
                    Updated?.Invoke(Get(new(e.sensor.which)), new SensorUpdatedEventArgs(e.sensor));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
