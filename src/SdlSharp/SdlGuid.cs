using System.Text;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Guid;

namespace SdlSharp;

/// <summary>
/// A 128-bit SDL device GUID identifying a joystick or gamepad model
/// (an encoding of bus type, vendor, and product — not an RFC 4122 UUID).
/// Its canonical string form is the 32-character lowercase hex string used by
/// the SDL controller mapping database (gamecontrollerdb.txt).
/// </summary>
public readonly record struct SdlGuid
{
    private readonly ulong _data0;
    private readonly ulong _data1;

    internal SdlGuid(Native.SDL_GUID native)
    {
        _data0 = native.data0;
        _data1 = native.data1;
    }

    internal Native.SDL_GUID ToNative() => new() { data0 = _data0, data1 = _data1 };

    /// <summary>
    /// Gets whether this is the zero GUID (returned by SDL for invalid devices).
    /// </summary>
    public bool IsZero => _data0 == 0 && _data1 == 0;

    /// <summary>
    /// Parses a GUID from its canonical 32-character hex string form.
    /// </summary>
    /// <param name="text">The GUID string.</param>
    /// <returns>The decoded GUID.</returns>
    /// <exception cref="FormatException">The input is not exactly 32 hexadecimal digits.</exception>
    public static SdlGuid Parse(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        return TryParse(text, out var value) ? value : throw new FormatException("An SDL GUID must contain exactly 32 hexadecimal digits.");
    }

    /// <summary>Attempts to parse exactly 32 hexadecimal digits.</summary>
    public static bool TryParse(string? text, out SdlGuid value)
    {
        value = default;
        if (text == null || text.Length != 32 || !text.All(char.IsAsciiHexDigit)) return false;
        value = new(SDL_StringToGUID(ToUtf8(text)));
        return true;
    }

    /// <summary>
    /// Returns the canonical 32-character lowercase hex string form.
    /// </summary>
    public override unsafe string ToString()
    {
        Span<byte> buffer = stackalloc byte[33];
        fixed (byte* p = buffer)
            SDL_GUIDToString(ToNative(), p, buffer.Length);
        var terminator = buffer.IndexOf((byte)0);
        return Encoding.ASCII.GetString(buffer[..(terminator < 0 ? buffer.Length : terminator)]);
    }
    /// <summary>Decodes USB identifiers and mapping CRC from a joystick GUID.</summary>
    public unsafe (ushort Vendor, ushort Product, ushort Version, ushort Crc) GetDeviceInfo()
    {
        ushort vendor, product, version, crc;
        Native.Joystick.SDL_GetJoystickGUIDInfo(ToNative(), &vendor, &product, &version, &crc);
        return (vendor, product, version, crc);
    }
}
