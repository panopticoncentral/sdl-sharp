using System;

namespace SdlSharp.Input
{
    /// <summary>
    /// A sensor in the device.
    /// </summary>
    public unsafe sealed class Sensor : NativePointerBase<Native.SDL_Sensor, Sensor>
    {
        /// <summary>
        /// The sensor's name.
        /// </summary>
        public string Name =>
            Native.SDL_SensorGetName(Pointer);

        /// <summary>
        /// The sensor type.
        /// </summary>
        public SensorType Type =>
            Native.SDL_SensorGetType(Pointer);

        /// <summary>
        /// A non-portable sensor type.
        /// </summary>
        public int NonPortableType =>
            Native.SDL_SensorGetNonPortableType(Pointer);

        /// <summary>
        /// An event fired when a sensor is updated.
        /// </summary>
        public event EventHandler<SensorUpdatedEventArgs> Updated;

        internal static Sensor Get(Native.SDL_SensorID instanceId) =>
            PointerToInstanceNotNull(Native.SDL_SensorFromInstanceID(instanceId));

        /// <summary>
        /// Gets the data from the sensor.
        /// </summary>
        /// <param name="data">Where to put the data.</param>
        public void GetData(float[] data) =>
            Native.CheckError(Native.SDL_SensorGetData(Pointer, data, data.Length));

        /// <inheritdoc/>
        public override void Dispose()
        {
            Native.SDL_SensorClose(Pointer);
            base.Dispose();
        }

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            var sensor = Get(e.Sensor.Which);

            switch (e.Type)
            {
                case Native.SDL_EventType.SensorUpdate:
                    sensor.Updated?.Invoke(sensor, new SensorUpdatedEventArgs(e.Sensor));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
