using System.Collections.Generic;

namespace SdlSharp.Input
{
    /// <summary>
    /// A sensor device.
    /// </summary>
    public unsafe sealed class SensorDevice : NativeStaticIndexBase<int, SensorDevice>
    {
        private static ItemCollection<SensorDevice>? s_devices;

        /// <summary>
        /// Standard gravity constant.
        /// </summary>
        public const float StandardGravity = 9.80665f;

        /// <summary>
        /// The devices in the system.
        /// </summary>
        public static IReadOnlyList<SensorDevice> Devices => s_devices ?? (s_devices = new ItemCollection<SensorDevice>(Get, Native.SDL_NumSensors));

        /// <summary>
        /// The name of the device, if any.
        /// </summary>
        public string? Name =>
            Native.SDL_SensorGetDeviceName(Index);

        /// <summary>
        /// The sensor type.
        /// </summary>
        public SensorType Type =>
            Native.SDL_SensorGetDeviceType(Index);

        /// <summary>
        /// The non-portable sensor type.
        /// </summary>
        public int NonPortableType =>
            Native.SDL_SensorGetDeviceNonPortableType(Index);

        internal static SensorDevice Get(int index) =>
            IndexToInstance(index);

        /// <summary>
        /// Opens the device.
        /// </summary>
        /// <returns>The sensor.</returns>
        public Sensor Open() =>
            Sensor.PointerToInstanceNotNull(Native.SDL_SensorOpen(Index));

        /// <summary>
        /// Updates sensor events.
        /// </summary>
        public static void Update() =>
            Native.SDL_SensorUpdate();
    }
}
