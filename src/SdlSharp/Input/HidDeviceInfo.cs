namespace SdlSharp.Input
{
    /// <summary>
    /// Information about a Human Interface Device.
    /// </summary>
    public sealed unsafe class HidDeviceInfo
    {
        /// <summary>
        /// The platform-specific device path.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The device vendor ID.
        /// </summary>
        public ushort VendorId { get; }

        /// <summary>
        /// The device product ID.
        /// </summary>
        public ushort ProductId { get; }

        /// <summary>
        /// The device's serial number.
        /// </summary>
        public string SerialNumber { get; }

        /// <summary>
        /// The device release number.
        /// </summary>
        public ushort ReleaseNumber { get; }

        /// <summary>
        /// The manufacturer.
        /// </summary>
        public string Manufacturer { get; }

        /// <summary>
        /// The product.
        /// </summary>
        public string Product { get; }

        /// <summary>
        /// The device's usage page.
        /// </summary>
        public ushort UsagePage { get; }

        /// <summary>
        /// The device's usage.
        /// </summary>
        public ushort Usage { get; }

        /// <summary>
        /// The device's interface number.
        /// </summary>
        public int InterfaceNumber { get; }

        /// <summary>
        /// The device's interface class.
        /// </summary>
        public int InterfaceClass { get; }

        /// <summary>
        /// The device's interface sub-class.
        /// </summary>
        public int InterfaceSubclass { get; }

        /// <summary>
        /// The device's interface protocol.
        /// </summary>
        public int InterfaceProtocol { get; }

        internal HidDeviceInfo(Native.SDL_hid_device_info* info)
        {
            Path = Native.Utf8ToString(info->path)!;
            VendorId = info->vendor_id;
            ProductId = info->product_id;
            SerialNumber = new(info->serial_number);
            ReleaseNumber = info->release_number;
            Manufacturer = new(info->manufacturer_string);
            Product = new(info->product_string);
            UsagePage = info->usage_page;
            Usage = info->usage;
            InterfaceNumber = info->interface_number;
            InterfaceClass = info->interface_class;
            InterfaceSubclass = info->interface_subclass;
            InterfaceProtocol = info->interface_protocol;
        }
    }
}
