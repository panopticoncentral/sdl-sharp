using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Native;

public struct SDL_hid_device;
public enum SDL_hid_bus_type
{
    SDL_HID_API_BUS_UNKNOWN = 0,
    SDL_HID_API_BUS_USB = 1,
    SDL_HID_API_BUS_BLUETOOTH = 2,
    SDL_HID_API_BUS_I2C = 3,
    SDL_HID_API_BUS_SPI = 4
}
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_hid_device_info
{
    public byte* path;
    public ushort vendor_id;
    public ushort product_id;
    // wchar_t is UTF-16 on Windows and UTF-32 on Unix; do not marshal as char*.
    public void* serial_number;
    public ushort release_number;
    public void* manufacturer_string;
    public void* product_string;
    public ushort usage_page;
    public ushort usage;
    public int interface_number;
    public int interface_class;
    public int interface_subclass;
    public int interface_protocol;
    public SDL_hid_bus_type bus_type;
    public SDL_hid_device_info* next;
}

public static unsafe partial class HidApi
{
    public const string SDL_PROP_HIDAPI_LIBUSB_DEVICE_HANDLE_POINTER = "SDL.hidapi.libusb.device.handle";

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_init")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_init();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_exit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_exit();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_device_change_count")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_hid_device_change_count();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_enumerate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_hid_device_info* SDL_hid_enumerate(ushort vendor_id, ushort product_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_free_enumeration")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_hid_free_enumeration(SDL_hid_device_info* devs);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_open")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_hid_device* SDL_hid_open(ushort vendor_id, ushort product_id, void* serial_number);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_open_path")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_hid_device* SDL_hid_open_path(ReadOnlySpan<byte> path);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_get_properties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_hid_get_properties(SDL_hid_device* dev);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_write")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_write(SDL_hid_device* dev, byte* data, nuint length);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_read_timeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_read_timeout(SDL_hid_device* dev, byte* data, nuint length, int milliseconds);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_read")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_read(SDL_hid_device* dev, byte* data, nuint length);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_set_nonblocking")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_set_nonblocking(SDL_hid_device* dev, int nonblock);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_send_feature_report")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_send_feature_report(SDL_hid_device* dev, byte* data, nuint length);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_get_feature_report")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_get_feature_report(SDL_hid_device* dev, byte* data, nuint length);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_get_input_report")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_get_input_report(SDL_hid_device* dev, byte* data, nuint length);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_close")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_close(SDL_hid_device* dev);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_get_manufacturer_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_get_manufacturer_string(SDL_hid_device* dev, void* text, nuint maxlen);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_get_product_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_get_product_string(SDL_hid_device* dev, void* text, nuint maxlen);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_get_serial_number_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_get_serial_number_string(SDL_hid_device* dev, void* text, nuint maxlen);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_get_indexed_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_get_indexed_string(SDL_hid_device* dev, int string_index, void* text, nuint maxlen);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_get_device_info")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_hid_device_info* SDL_hid_get_device_info(SDL_hid_device* dev);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_get_report_descriptor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_hid_get_report_descriptor(SDL_hid_device* dev, byte* buf, nuint buf_size);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_hid_ble_scan")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_hid_ble_scan([MarshalAs(UnmanagedType.U1)] bool active);

}
