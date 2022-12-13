namespace SdlSharp.Input
{
    /// <summary>
    /// A sensor in the device.
    /// </summary>
    public sealed unsafe class Sensor : NativePointerBase<Native.SDL_Sensor, Sensor>
    {
        /// <summary>
        /// The sensor's name.
        /// </summary>
        public string Name =>
            SdlSharp.Native.SDL_SensorGetName(Native);

        /// <summary>
        /// The sensor type.
        /// </summary>
        public SensorType Type =>
            SdlSharp.Native.SDL_SensorGetType(Native);

        /// <summary>
        /// A non-portable sensor type.
        /// </summary>
        public int NonPortableType =>
            SdlSharp.Native.SDL_SensorGetNonPortableType(Native);

        /// <summary>
        /// An event fired when a sensor is updated.
        /// </summary>
        public event EventHandler<SensorUpdatedEventArgs>? Updated;

        internal static Sensor Get(Native.SDL_SensorID instanceId) =>
            PointerToInstanceNotNull(SdlSharp.Native.SDL_SensorFromInstanceID(instanceId));

        /// <summary>
        /// Gets the data from the sensor.
        /// </summary>
        /// <param name="data">Where to put the data.</param>
        public void GetData(float[] data) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SensorGetData(Native, data, data.Length));

        /// <inheritdoc/>
        public override void Dispose()
        {
            SdlSharp.Native.SDL_SensorClose(Native);
            base.Dispose();
        }

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            var sensor = Get(new(e.sensor.which));

            switch ((Native.SDL_EventType)e.type)
            {
                case SdlSharp.Native.SDL_EventType.SDL_SENSORUPDATE:
                    sensor.Updated?.Invoke(sensor, new SensorUpdatedEventArgs(e.sensor));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
