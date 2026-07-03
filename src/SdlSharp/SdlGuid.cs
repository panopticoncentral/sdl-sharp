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
    /// <returns>
    /// The decoded GUID. Malformed input is not rejected: SDL decodes any character that is not a
    /// hex digit as the nibble 0, so invalid input silently yields a partial — possibly non-zero —
    /// meaningless GUID. <see cref="IsZero"/> is therefore not a reliable validity check; only an
    /// empty or entirely non-hex string reliably produces the zero GUID.
    /// </returns>
    public static SdlGuid Parse(string text) => new(SDL_StringToGUID(ToUtf8(text)));

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
}
