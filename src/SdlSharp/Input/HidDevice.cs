using SdlSharp.Native;
using static SdlSharp.Native.Common;

namespace SdlSharp.Input;

/// <summary>An owned HID device with span-based report I/O and detached string/device snapshots.</summary>
/// <remarks>Operations and disposal are serialized. A blocking read holds the device until it returns; use a finite timeout when shutdown must remain responsive. Report buffers include the report ID where required by HID; interrupt reads include it only for numbered reports.</remarks>
public sealed unsafe class HidDevice : IDisposable
{
    private readonly object _gate = new();
    private readonly HidDeviceApi _api;
    private readonly IDisposable _sessionLease;
    internal readonly NativeLifetime Lifetime;
    private SDL_hid_device* _handle;
    private bool _nonblocking;
    internal HidDevice(SDL_hid_device* handle, NativeLifetime session, HidDeviceApi? api = null)
    {
        _api = api ?? HidDeviceApi.Default;
        _sessionLease = session.Retain();
        try { Lifetime = NativeLifetime.Get(typeof(HidDevice), (nint)handle, true); }
        catch { _sessionLease.Dispose(); throw; }
        _handle = handle;
    }
    private SDL_hid_device* Handle
    {
        get { ObjectDisposedException.ThrowIf(_handle == null, this); Lifetime.Check(); return _handle; }
    }
    internal static int CheckResult(int result) => result < 0 ? throw new SdlException() : result;
    private static int CheckCount(int result, int capacity)
    {
        CheckResult(result);
        if (result > capacity) throw new InvalidDataException("The native HID backend returned more bytes than the supplied buffer.");
        return result;
    }
    private static void RequireBuffer(int length)
    {
        if (length == 0) throw new ArgumentException("Supply a nonempty HID report buffer, including its report ID where required.");
    }

    /// <summary>Name of the read-only native libusb device handle property. Its pointer is borrowed until device disposal.</summary>
    public const string LibUsbDeviceHandleProperty = Native.HidApi.SDL_PROP_HIDAPI_LIBUSB_DEVICE_HANDLE_POINTER;
    /// <summary>Gets borrowed native properties, invalidated when the device closes.</summary>
    public PropertyGroup Properties { get { lock (_gate) return new(CheckId(_api.GetProperties(Handle)), false, Lifetime); } }
    /// <summary>Gets a detached device-information snapshot.</summary>
    public HidDeviceInfo Info { get { lock (_gate) return HidText.Snapshot(Check(_api.GetInfo(Handle))); } }
    /// <summary>Gets or sets whether <see cref="Read(Span{byte})"/> returns immediately when no report is available. Initially false.</summary>
    public bool Nonblocking
    {
        get { lock (_gate) { _ = Handle; return _nonblocking; } }
        set { lock (_gate) { CheckResult(_api.SetNonblocking(Handle, value ? 1 : 0)); _nonblocking = value; } }
    }

    /// <summary>Reads an interrupt input report; returns zero when nonblocking and no report is available.</summary>
    public int Read(Span<byte> buffer)
    {
        lock (_gate) { var device = Handle; RequireBuffer(buffer.Length); fixed (byte* p = buffer) return CheckCount(_api.Read(device, p, (nuint)buffer.Length), buffer.Length); }
    }
    /// <summary>Reads an interrupt input report with a timeout; zero means no report arrived. Use <see cref="Timeout.InfiniteTimeSpan"/> to wait indefinitely.</summary>
    public int Read(Span<byte> buffer, TimeSpan timeout)
    {
        lock (_gate)
        {
            var device = Handle;
            RequireBuffer(buffer.Length);
            var milliseconds = TimeoutMilliseconds(timeout);
            fixed (byte* p = buffer) return CheckCount(_api.ReadTimeout(device, p, (nuint)buffer.Length, milliseconds), buffer.Length);
        }
    }
    internal static int TimeoutMilliseconds(TimeSpan timeout)
    {
        if (timeout == Timeout.InfiniteTimeSpan) return -1;
        if (timeout < TimeSpan.Zero || timeout.TotalMilliseconds > int.MaxValue) throw new ArgumentOutOfRangeException(nameof(timeout));
        return (int)Math.Ceiling(timeout.TotalMilliseconds);
    }
    /// <summary>Writes an output report whose first byte is the report ID (zero for an unnumbered report). Returns bytes written, including the ID.</summary>
    public int Write(ReadOnlySpan<byte> report)
    {
        lock (_gate) { var device = Handle; RequireBuffer(report.Length); fixed (byte* p = report) return CheckCount(_api.Write(device, p, (nuint)report.Length), report.Length); }
    }
    /// <summary>Sends a feature report whose first byte is the report ID. Returns bytes written, including the ID.</summary>
    public int SendFeatureReport(ReadOnlySpan<byte> report)
    {
        lock (_gate) { var device = Handle; RequireBuffer(report.Length); fixed (byte* p = report) return CheckCount(_api.SendFeatureReport(device, p, (nuint)report.Length), report.Length); }
    }
    /// <summary>Reads a feature report, placing the requested report ID in buffer[0]. Returned bytes include the ID.</summary>
    public int GetFeatureReport(byte reportId, Span<byte> buffer)
    {
        lock (_gate) { var device = Handle; RequireBuffer(buffer.Length); buffer[0] = reportId; fixed (byte* p = buffer) return CheckCount(_api.GetFeatureReport(device, p, (nuint)buffer.Length), buffer.Length); }
    }
    /// <summary>Reads an input report through the control endpoint, placing the requested report ID in buffer[0]. Returned bytes include the ID.</summary>
    public int GetInputReport(byte reportId, Span<byte> buffer)
    {
        lock (_gate) { var device = Handle; RequireBuffer(buffer.Length); buffer[0] = reportId; fixed (byte* p = buffer) return CheckCount(_api.GetInputReport(device, p, (nuint)buffer.Length), buffer.Length); }
    }
    /// <summary>Copies the report descriptor into a caller buffer and returns bytes copied. SDL recommends a 4096-byte buffer.</summary>
    public int GetReportDescriptor(Span<byte> buffer)
    {
        lock (_gate) { var device = Handle; RequireBuffer(buffer.Length); fixed (byte* p = buffer) return CheckCount(_api.GetReportDescriptor(device, p, (nuint)buffer.Length), buffer.Length); }
    }

    /// <summary>Gets the manufacturer string.</summary>
    public string Manufacturer => ReadString(0, 0);
    /// <summary>Gets the product string.</summary>
    public string Product => ReadString(1, 0);
    /// <summary>Gets the serial number string.</summary>
    public string SerialNumber => ReadString(2, 0);
    /// <summary>Gets an indexed HID string.</summary>
    public string GetIndexedString(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        return ReadString(3, index);
    }
    private string ReadString(int kind, int index)
    {
        lock (_gate)
        {
            var device = Handle;
            // USB strings are bounded by their descriptor; 1024 wide characters also
            // accommodates backend-provided names without relying on UTF-16 sizeof(char).
            const int capacity = 1024;
            var buffer = new byte[capacity * HidText.CharacterSize];
            fixed (byte* text = buffer)
            {
                CheckResult(kind switch
                {
                    0 => _api.GetManufacturer(device, text, capacity),
                    1 => _api.GetProduct(device, text, capacity),
                    2 => _api.GetSerialNumber(device, text, capacity),
                    _ => _api.GetIndexedString(device, index, text, capacity)
                });
                return HidText.Decode(text, HidText.CharacterSize, capacity)!;
            }
        }
    }

    /// <summary>Closes the device once, invalidates borrowed properties, and releases its HID session.</summary>
    public void Dispose()
    {
        lock (_gate)
        {
            if (_handle == null) return;
            var result = 0;
            Lifetime.Release(() => result = _api.Close(_handle));
            _handle = null;
            _sessionLease.Dispose();
            CheckResult(result);
        }
    }
}
