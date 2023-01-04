namespace SdlSharp.Input
{
    /// <summary>
    /// A HID device.
    /// </summary>
    public sealed unsafe class HidDevice : IDisposable
    {
        private readonly Native.SDL_hid_device* _device;

        /// <summary>
        /// The ID of the device.
        /// </summary>
        public nuint Id => (nuint)_device;

        internal HidDevice(Native.SDL_hid_device* device)
        {
            _device = device;
        }

        /// <summary>
        /// Write output to a HID device.
        /// </summary>
        /// <param name="data">The data to write.</param>
        /// <returns>The amount of data written.</returns>
        public int Write(Span<byte> data)
        {
            fixed (byte* ptr = data)
            {
                return Native.CheckError(Native.SDL_hid_write(_device, ptr, (nuint)data.Length));
            }
        }

        /// <summary>
        /// Reads input from a HID device.
        /// </summary>
        /// <param name="data">The data storage.</param>
        /// <param name="timeout">A timeout.</param>
        /// <returns>The number of bytes read.</returns>
        public int Read(Span<byte> data, int timeout)
        {
            fixed (byte* ptr = data)
            {
                return Native.CheckError(Native.SDL_hid_read_timeout(_device, ptr, (nuint)data.Length, timeout));
            }
        }

        /// <summary>
        /// Reads input from a HID device.
        /// </summary>
        /// <param name="data">The data storage.</param>
        /// <returns>The number of bytes read.</returns>
        public int Read(Span<byte> data)
        {
            fixed (byte* ptr = data)
            {
                return Native.CheckError(Native.SDL_hid_read(_device, ptr, (nuint)data.Length));
            }
        }

        /// <summary>
        /// Sets the device to be non-blocking.
        /// </summary>
        /// <param name="nonBlocking">Whether the device is non-blocking.</param>
        public void SetNonBlocking(bool nonBlocking) =>
            _ = Native.CheckError(Native.SDL_hid_set_nonblocking(_device, Native.BoolToInt(nonBlocking)));

        /// <summary>
        /// Starts or stops a BLE scan.
        /// </summary>
        /// <param name="active">Whether the scan is active.</param>
        public static void SetBleScan(bool active) =>
            Native.SDL_hid_ble_scan(active);

        /// <summary>
        /// Sends a feature report.
        /// </summary>
        /// <param name="data">The report.</param>
        /// <returns>The number of bytes sent.</returns>
        public int SendFeatureReport(Span<byte> data)
        {
            fixed (byte* ptr = data)
            {
                return Native.CheckError(Native.SDL_hid_send_feature_report(_device, ptr, (nuint)data.Length));
            }
        }

        /// <summary>
        /// Gets a feature report.
        /// </summary>
        /// <param name="data">The storage.</param>
        /// <returns>The number of bytes read.</returns>
        public int GetFeatureReport(Span<byte> data)
        {
            fixed (byte* ptr = data)
            {
                return Native.CheckError(Native.SDL_hid_get_feature_report(_device, ptr, (nuint)data.Length));
            }
        }

        /// <summary>
        /// Disposes the device.
        /// </summary>
        public void Dispose() => Native.SDL_hid_close(_device);

        /// <summary>
        /// Gets the manufacturer string.
        /// </summary>
        /// <param name="chars">The storage.</param>
        public void GetManufacturer(Span<char> chars)
        {
            fixed (char* ptr = chars)
            {
                _ = Native.CheckError(Native.SDL_hid_get_manufacturer_string(_device, ptr, (nuint)chars.Length));
            }
        }

        /// <summary>
        /// Gets the product string.
        /// </summary>
        /// <param name="chars">The storage.</param>
        public void GetProduct(Span<char> chars)
        {
            fixed (char* ptr = chars)
            {
                _ = Native.CheckError(Native.SDL_hid_get_product_string(_device, ptr, (nuint)chars.Length));
            }
        }

        /// <summary>
        /// Gets the serial number string.
        /// </summary>
        /// <param name="chars">The storage.</param>
        public void GetSerialNumber(Span<char> chars)
        {
            fixed (char* ptr = chars)
            {
                _ = Native.CheckError(Native.SDL_hid_get_serial_number_string(_device, ptr, (nuint)chars.Length));
            }
        }

        /// <summary>
        /// Gets an indexed string.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="chars">The storage.</param>
        public void GetIndexed(int index, Span<char> chars)
        {
            fixed (char* ptr = chars)
            {
                _ = Native.CheckError(Native.SDL_hid_get_indexed_string(_device, index, ptr, (nuint)chars.Length));
            }
        }
    }
}
