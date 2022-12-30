namespace SdlSharp.Input
{
    /// <summary>
    /// A sensor device.
    /// </summary>
    public sealed unsafe class SensorDevice
    {
        private readonly int _index;

        /// <summary>
        /// Standard gravity constant.
        /// </summary>
        public const float StandardGravity = Native.SDL_STANDARD_GRAVITY;

        /// <summary>
        /// The devices in the system.
        /// </summary>
        public static IReadOnlyList<SensorDevice> Devices => Native.GetIndexedCollection(i => new SensorDevice(i), Native.SDL_NumSensors);

        /// <summary>
        /// The name of the device, if any.
        /// </summary>
        public string? Name =>
            Native.Utf8ToString(Native.SDL_SensorGetDeviceName(_index));

        /// <summary>
        /// The sensor type.
        /// </summary>
        public SensorType Type =>
            (SensorType)Native.SDL_SensorGetDeviceType(_index);

        /// <summary>
        /// The non-portable sensor type.
        /// </summary>
        public int NonPortableType =>
            Native.SDL_SensorGetDeviceNonPortableType(_index);

        internal SensorDevice(int index)
        {
            _index = index;
        }

        /// <summary>
        /// Opens the device.
        /// </summary>
        /// <returns>The sensor.</returns>
        public Sensor Open() =>
            new(Native.SDL_SensorOpen(_index));
    }
}
