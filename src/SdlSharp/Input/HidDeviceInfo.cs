namespace SdlSharp.Input;

/// <summary>The underlying bus of a HID device.</summary>
public enum HidBusType
{
    /// <summary>Unknown bus.</summary>
    Unknown = Native.SDL_hid_bus_type.SDL_HID_API_BUS_UNKNOWN,
    /// <summary>USB.</summary>
    Usb = Native.SDL_hid_bus_type.SDL_HID_API_BUS_USB,
    /// <summary>Bluetooth or Bluetooth Low Energy.</summary>
    Bluetooth = Native.SDL_hid_bus_type.SDL_HID_API_BUS_BLUETOOTH,
    /// <summary>I2C.</summary>
    I2c = Native.SDL_hid_bus_type.SDL_HID_API_BUS_I2C,
    /// <summary>SPI.</summary>
    Spi = Native.SDL_hid_bus_type.SDL_HID_API_BUS_SPI
}

/// <summary>A detached snapshot of HID device information; it remains valid after enumeration or device disposal.</summary>
/// <param name="Path">Platform-specific path used to open this device.</param>
/// <param name="VendorId">USB vendor identifier.</param>
/// <param name="ProductId">USB product identifier.</param>
/// <param name="SerialNumber">Serial number, when available.</param>
/// <param name="ReleaseNumber">Binary-coded decimal device version.</param>
/// <param name="Manufacturer">Manufacturer, when available.</param>
/// <param name="Product">Product name, when available.</param>
/// <param name="UsagePage">HID usage page.</param>
/// <param name="Usage">HID usage.</param>
/// <param name="InterfaceNumber">USB interface number, or -1 when unavailable.</param>
/// <param name="InterfaceClass">USB interface class, when available.</param>
/// <param name="InterfaceSubclass">USB interface subclass, when available.</param>
/// <param name="InterfaceProtocol">USB interface protocol, when available.</param>
/// <param name="BusType">Underlying bus.</param>
public sealed record HidDeviceInfo(string Path, ushort VendorId, ushort ProductId, string? SerialNumber,
    ushort ReleaseNumber, string? Manufacturer, string? Product, ushort UsagePage, ushort Usage,
    int InterfaceNumber, int InterfaceClass, int InterfaceSubclass, int InterfaceProtocol, HidBusType BusType);
