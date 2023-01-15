using System.Collections;

namespace SdlSharp.Input
{
    /// <summary>
    /// APIs to interface with Human Interface Devices (HIDs).
    /// </summary>
    public static unsafe class Hid
    {
        private sealed class HidInfoEnumerable : IEnumerable<HidDeviceInfo>
        {
            private readonly ushort _vendorId;
            private readonly ushort _productId;

            public HidInfoEnumerable(ushort vendorId, ushort productId)
            {
                _vendorId = vendorId;
                _productId = productId;
            }

            public IEnumerator<HidDeviceInfo> GetEnumerator() =>
                new HidInfoEnumerator(Native.CheckPointer(Native.SDL_hid_enumerate(_vendorId, _productId)));

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private sealed unsafe class HidInfoEnumerator : IEnumerator<HidDeviceInfo>
        {
            private Native.SDL_hid_device_info* _head;
            private Native.SDL_hid_device_info* _current;

            public HidDeviceInfo Current => new(_head);

            object IEnumerator.Current => Current;

            public HidInfoEnumerator(Native.SDL_hid_device_info* head)
            {
                _head = head;
                _current = null;
            }

            public void Dispose()
            {
                Native.SDL_hid_free_enumeration(_head);
                _head = _current = null;
            }
            public bool MoveNext()
            {
                if (_current != null)
                {
                    _current = _current->next;
                }

                return _current != null;
            }
            public void Reset() => _current = _head;
        }

        /// <summary>
        /// Initializes the HID system.
        /// </summary>
        public static void Init() => Native.CheckError(Native.SDL_hid_init());

        /// <summary>
        /// Exits the HID system.
        /// </summary>
        public static void Exit() => Native.CheckError(Native.SDL_hid_exit());

        /// <summary>
        /// Gets information about devices in the HID system.
        /// </summary>
        /// <param name="vendorId">The vendor ID, or 0 for all.</param>
        /// <param name="productId">The product ID, or 0 for all.</param>
        /// <returns>The device information.</returns>
        public static IEnumerable<HidDeviceInfo> GetDeviceInfos(ushort vendorId, ushort productId) =>
            new HidInfoEnumerable(vendorId, productId);

        /// <summary>
        /// Get the number of device changes since the function was last called.
        /// </summary>
        /// <returns>The number of device changes.</returns>
        public static uint GetDeviceChangeCount() =>
            Native.SDL_hid_device_change_count();

        /// <summary>
        /// Opens a HID device.
        /// </summary>
        /// <param name="vendorId">The vendor ID.</param>
        /// <param name="productId">The produce ID.</param>
        /// <param name="serialNumber">The serial number (or null).</param>
        /// <returns>The device.</returns>
        public static HidDevice Open(ushort vendorId, ushort productId, string? serialNumber)
        {
            fixed (char* serialPtr = serialNumber)
            {
                return new(Native.CheckPointer(Native.SDL_hid_open(vendorId, productId, serialPtr)));
            }
        }

        /// <summary>
        /// Opens a HID device.
        /// </summary>
        /// <param name="path">The path name of the device.</param>
        /// <param name="exclusive">Whether the open is exclusive.</param>
        /// <returns>The device.</returns>
        public static HidDevice OpenPath(string path, bool exclusive = false) => Native.StringToUtf8Func(path, ptr => new HidDevice(Native.CheckPointer(Native.SDL_hid_open_path(ptr, Native.BoolToInt(exclusive)))));
    }
}
