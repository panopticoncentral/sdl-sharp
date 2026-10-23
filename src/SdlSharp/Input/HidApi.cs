using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.HidApi;

namespace SdlSharp.Input;

/// <summary>Owns an initialized HIDAPI session. Dispose its devices and scan scopes before disposing the session.</summary>
/// <remarks>All concurrently active sessions must be created, enumerated and disposed on the same thread, so native device discovery has a consistent event-loop thread. Device I/O is independently serialized per device. SDL enumerates controllers by default; set <see cref="HintNames.HidapiEnumerateOnlyControllers"/> to "0" before opening a session to include other HID devices.</remarks>
public sealed unsafe class HidApi : IDisposable
{
    private static readonly object Gate = new();
    private static long _nextId;
    private static int _sessions;
    private static int _thread;
    private static int _scans;
    internal readonly NativeLifetime Lifetime;
    private bool _disposed;

    /// <summary>Initializes a reference-counted HIDAPI session.</summary>
    public HidApi()
    {
        lock (Gate)
        {
            var thread = Environment.CurrentManagedThreadId;
            if (_sessions != 0 && _thread != thread) throw new InvalidOperationException("Create HID sessions on the existing HID discovery thread.");
            HidDevice.CheckResult(SDL_hid_init());
            try { Lifetime = NativeLifetime.Get(typeof(HidApi), checked((nint)Interlocked.Increment(ref _nextId)), true, threadAffine: true); }
            catch { SDL_hid_exit(); throw; }
            _sessions++;
            _thread = thread;
        }
    }
    private void CheckAlive() { ObjectDisposedException.ThrowIf(_disposed, this); Lifetime.Check(); }

    /// <summary>Gets the device change counter; zero means change detection is unavailable.</summary>
    public uint DeviceChangeCount { get { lock (Gate) { CheckAlive(); return SDL_hid_device_change_count(); } } }

    /// <summary>Copies matching device information and frees the native enumeration. Zero identifiers match any device.</summary>
    /// <remarks>An empty array means no matching devices were returned; native backends do not reliably distinguish enumeration failure from an empty result.</remarks>
    public HidDeviceInfo[] EnumerateDevices(ushort vendorId = 0, ushort productId = 0)
    {
        lock (Gate)
        {
            CheckAlive();
            var devices = SDL_hid_enumerate(vendorId, productId);
            try
            {
                var result = new List<HidDeviceInfo>();
                for (var item = devices; item != null; item = item->next) result.Add(HidText.Snapshot(item));
                return result.ToArray();
            }
            finally { SDL_hid_free_enumeration(devices); }
        }
    }

    /// <summary>Opens a device by vendor/product identifiers and an optional serial number.</summary>
    public HidDevice OpenDevice(ushort vendorId, ushort productId, string? serialNumber = null)
    {
        lock (Gate)
        {
            CheckAlive();
            fixed (byte* serial = HidText.Encode(serialNumber, HidText.CharacterSize))
                return Wrap(Check(SDL_hid_open(vendorId, productId, serial)));
        }
    }

    /// <summary>Opens the platform-specific path from a device information snapshot.</summary>
    public HidDevice OpenDevice(string path)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        if (path.Contains('\0')) throw new ArgumentException("Embedded null characters are not allowed.", nameof(path));
        lock (Gate) { CheckAlive(); return Wrap(Check(SDL_hid_open_path(ToUtf8(path)))); }
    }
    private HidDevice Wrap(SDL_hid_device* handle)
    {
        try { return new HidDevice(handle, Lifetime); }
        catch { SDL_hid_close(handle); throw; }
    }

    /// <summary>Starts a scoped Steam Controller BLE pairing scan on iOS/tvOS. Other platforms ignore the request.</summary>
    /// <remarks>Overlapping scopes share the scan. Dispose the scope on the HID session thread.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The returned scope owns and releases the session lease.")]
    public SdlScope BeginBluetoothScan()
    {
        lock (Gate)
        {
            CheckAlive();
            var lease = Lifetime.Retain();
            if (_scans++ == 0) SDL_hid_ble_scan(true);
            return new SdlScope(() => { lock (Gate) { if (--_scans == 0) SDL_hid_ble_scan(false); lease.Dispose(); } });
        }
    }

    /// <summary>Releases this initialization reference exactly once; live devices or scan scopes prevent shutdown.</summary>
    public void Dispose()
    {
        lock (Gate)
        {
            if (_disposed) return;
            var result = 0;
            Lifetime.Release(() => result = SDL_hid_exit());
            _disposed = true;
            if (--_sessions == 0) _thread = 0;
            HidDevice.CheckResult(result);
        }
    }
}
