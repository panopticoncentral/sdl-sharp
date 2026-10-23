using SdlSharp.Native;

namespace SdlSharp.Input;

// A per-device dispatch seam lets hardware-independent tests exercise the same ownership and buffer code.
internal unsafe class HidDeviceApi
{
    internal static readonly HidDeviceApi Default = new();
    internal virtual int Close(SDL_hid_device* device) => Native.HidApi.SDL_hid_close(device);
    internal virtual SDL_PropertiesID GetProperties(SDL_hid_device* device) => Native.HidApi.SDL_hid_get_properties(device);
    internal virtual SDL_hid_device_info* GetInfo(SDL_hid_device* device) => Native.HidApi.SDL_hid_get_device_info(device);
    internal virtual int SetNonblocking(SDL_hid_device* device, int value) => Native.HidApi.SDL_hid_set_nonblocking(device, value);
    internal virtual int ReadTimeout(SDL_hid_device* device, byte* data, nuint length, int timeout) => Native.HidApi.SDL_hid_read_timeout(device, data, length, timeout);
    internal virtual int Write(SDL_hid_device* device, byte* data, nuint length) => Native.HidApi.SDL_hid_write(device, data, length);
    internal virtual int Read(SDL_hid_device* device, byte* data, nuint length) => Native.HidApi.SDL_hid_read(device, data, length);
    internal virtual int SendFeatureReport(SDL_hid_device* device, byte* data, nuint length) => Native.HidApi.SDL_hid_send_feature_report(device, data, length);
    internal virtual int GetFeatureReport(SDL_hid_device* device, byte* data, nuint length) => Native.HidApi.SDL_hid_get_feature_report(device, data, length);
    internal virtual int GetInputReport(SDL_hid_device* device, byte* data, nuint length) => Native.HidApi.SDL_hid_get_input_report(device, data, length);
    internal virtual int GetReportDescriptor(SDL_hid_device* device, byte* data, nuint length) => Native.HidApi.SDL_hid_get_report_descriptor(device, data, length);
    internal virtual int GetManufacturer(SDL_hid_device* device, void* text, nuint length) => Native.HidApi.SDL_hid_get_manufacturer_string(device, text, length);
    internal virtual int GetProduct(SDL_hid_device* device, void* text, nuint length) => Native.HidApi.SDL_hid_get_product_string(device, text, length);
    internal virtual int GetSerialNumber(SDL_hid_device* device, void* text, nuint length) => Native.HidApi.SDL_hid_get_serial_number_string(device, text, length);
    internal virtual int GetIndexedString(SDL_hid_device* device, int index, void* text, nuint length) => Native.HidApi.SDL_hid_get_indexed_string(device, index, text, length);
}
