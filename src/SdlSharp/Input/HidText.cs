using System.Runtime.InteropServices;
using System.Text;

namespace SdlSharp.Input;

internal static unsafe class HidText
{
    internal static int CharacterSize => OperatingSystem.IsWindows() ? 2 : 4;
    private static Encoding EncodingFor(int width) => width switch
    {
        2 => new UnicodeEncoding(!BitConverter.IsLittleEndian, false, true),
        4 => new UTF32Encoding(!BitConverter.IsLittleEndian, false, true),
        _ => throw new ArgumentOutOfRangeException(nameof(width))
    };
    internal static byte[]? Encode(string? text, int width)
    {
        if (text == null) return null;
        if (text.Contains('\0')) throw new ArgumentException("Embedded null characters are not allowed.", nameof(text));
        var encoding = EncodingFor(width);
        var buffer = new byte[checked(encoding.GetByteCount(text) + width)];
        encoding.GetBytes(text, buffer);
        return buffer;
    }
    internal static string? Decode(void* text, int width, int capacity = int.MaxValue)
    {
        if (text == null) return null;
        var length = 0;
        while (length < capacity && (width == 2 ? ((ushort*)text)[length] != 0 : ((uint*)text)[length] != 0)) length++;
        if (length == capacity) throw new InvalidDataException("The HID string is not null-terminated within its buffer.");
        return EncodingFor(width).GetString(new ReadOnlySpan<byte>(text, checked(length * width)));
    }
    internal static HidDeviceInfo Snapshot(Native.SDL_hid_device_info* info) => new(
        Marshal.PtrToStringUTF8((nint)info->path) ?? string.Empty,
        info->vendor_id, info->product_id, Decode(info->serial_number, CharacterSize), info->release_number,
        Decode(info->manufacturer_string, CharacterSize), Decode(info->product_string, CharacterSize),
        info->usage_page, info->usage, info->interface_number, info->interface_class,
        info->interface_subclass, info->interface_protocol, (HidBusType)info->bus_type);
}
