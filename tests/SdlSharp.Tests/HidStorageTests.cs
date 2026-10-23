using System.Runtime.InteropServices;
using SdlSharp;
using SdlSharp.Input;
using SdlSharp.IO;
using N = SdlSharp.Native;

internal static unsafe class HidStorageTests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "Backend ownership is explicitly transferred to StorageContainer; these tests verify once-only disposal including throwing close callbacks.")]
    internal static void Run(Action<bool, string> check)
    {
        void Throws<T>(Action action, string name) where T : Exception
        {
            try { action(); check(false, name + " did not throw"); }
            catch (T) { check(true, name); }
        }
        using var app = new Application(InitFlags.Events);
        var registrations = CallbackHandle.Count;
        foreach (var width in new[] { 2, 4 })
        {
            var encoded = HidText.Encode("HID é 中文 🕹", width)!;
            fixed (byte* text = encoded)
                check(HidText.Decode(text, width, encoded.Length / width) == "HID é 中文 🕹", $"HID {width}-byte wchar Unicode round trip");
            check(encoded.AsSpan(encoded.Length - width).IndexOfAnyExcept((byte)0) < 0, "HID wide terminator");
            Throws<ArgumentException>(() => HidText.Encode("nul\0tail", width), "HID rejects embedded null serial");
            Throws<System.Text.EncoderFallbackException>(() => HidText.Encode("\ud800", width), "HID rejects invalid Unicode");
        }
        check(HidText.Encode(null, HidText.CharacterSize) == null, "Null HID serial stays null");
        check(HidDevice.TimeoutMilliseconds(TimeSpan.FromTicks(1)) == 1 && HidDevice.TimeoutMilliseconds(Timeout.InfiniteTimeSpan) == -1, "HID timeout rounding and infinite wait");
        Throws<ArgumentOutOfRangeException>(() => HidDevice.TimeoutMilliseconds(TimeSpan.FromMilliseconds(-2)), "HID negative timeout rejected");
        Throws<ArgumentOutOfRangeException>(() => HidDevice.TimeoutMilliseconds(TimeSpan.MaxValue), "HID oversized timeout rejected");
        using (var hid = new HidApi())
        {
            Throws<InvalidOperationException>(app.Dispose, "HID session prevents SDL shutdown");
            using (var second = new HidApi())
                check(second.EnumerateDevices(0xffff, 0xffff).All(d => d.VendorId == 0xffff && d.ProductId == 0xffff), "Real HID enumeration snapshots respect filters");
            _ = hid.DeviceChangeCount;
            check(true, "HID change query after another session closes");
            using (var scan = hid.BeginBluetoothScan())
            using (var otherScan = hid.BeginBluetoothScan())
            {
                Throws<InvalidOperationException>(hid.Dispose, "BLE scan retains HID session");
                scan.Dispose(); scan.Dispose();
                Throws<InvalidOperationException>(hid.Dispose, "Second BLE scope independently retains session");
            }
            Throws<ArgumentException>(() => hid.OpenDevice("invalid\0path"), "HID path null rejected");
            Throws<SdlException>(() => { using var absent = hid.OpenDevice("sdl-sharp-nonexistent-hid-device"); }, "HID native open failure propagates");
            var native = new FakeHid();
            using var device = new HidDevice((N.SDL_hid_device*)0x51800001, hid.Lifetime, native);
            Throws<InvalidOperationException>(hid.Dispose, "Open HID device retains session");
            var info = device.Info;
            var props = device.Properties;
            check(info.Product == "pad 🕹" && info.SerialNumber == "serial é" && info.Path == "fake/path", "HID metadata copied from native strings");
            device.Nonblocking = true;
            check(device.Nonblocking && native.Nonblocking == 1, "HID nonblocking state reflects successful native call");
            Span<byte> bytes = stackalloc byte[8];
            check(device.Read(bytes) == 3 && bytes[1] == 11, "HID read reports actual length");
            native.ReadResult = 0;
            check(device.Read(bytes, TimeSpan.Zero) == 0 && native.Timeout == 0, "HID zero-byte poll result is not failure");
            native.ReadResult = 3;
            check(device.Read(bytes, TimeSpan.FromTicks(1)) == 3 && native.Timeout == 1, "HID timeout forwarded in milliseconds");
            device.Read(bytes, Timeout.InfiniteTimeSpan);
            check(native.Timeout == -1, "HID infinite timeout forwarded");
            check(device.Write([0, 1, 2]) == 3 && native.LastReport.SequenceEqual(new byte[] { 0, 1, 2 }), "HID output preserves zero report ID");
            check(device.SendFeatureReport([7, 8]) == 2 && native.LastReport[0] == 7, "HID feature output includes ID");
            check(device.GetFeatureReport(9, bytes) == 3 && bytes[0] == 9 && native.ReportId == 9, "HID feature input initializes ID");
            check(device.GetInputReport(4, bytes) == 3 && bytes[0] == 4 && native.ReportId == 4, "HID control input initializes ID");
            check(device.GetReportDescriptor(bytes) == 3 && bytes[0] == 10, "HID descriptor uses byte capacity");
            check(device.Manufacturer == "maker é" && device.Product == "product 🕹" && device.SerialNumber == "serial 中" && device.GetIndexedString(7) == "index 7", "All HID string APIs use native wchar width");
            foreach (var action in new Action[] { () => device.Read([]), () => device.Write([]), () => device.SendFeatureReport([]), () => device.GetFeatureReport(0, []), () => device.GetInputReport(0, []), () => device.GetReportDescriptor([]) })
                Throws<ArgumentException>(action, "Empty HID buffer rejected before transport");
            Parallel.For(0, 100, _ => device.Read(new byte[8]));
            check(native.MaxConcurrentReads == 1, "HID I/O is serialized across callers");
            native.ReadResult = -1;
            Throws<SdlException>(() => device.Read(new byte[8]), "Negative HID transfer result throws");
            native.ReadResult = 9;
            Throws<InvalidDataException>(() => device.Read(new byte[8]), "Impossible HID transfer count rejected");
            using (var lease = props.Lock()) Throws<InvalidOperationException>(device.Dispose, "HID property lock prevents close");
            device.Dispose(); device.Dispose();
            check(native.CloseCount == 1 && info.Product == "pad 🕹", "HID closes once; metadata survives native release");
            Throws<ObjectDisposedException>(() => props.GetNames(), "HID borrowed properties invalidate");
            Throws<ObjectDisposedException>(() => device.Read(new byte[8]), "Disposed HID read rejected");
            props.Dispose();
            var failedClose = new FakeHid { CloseResult = -1 };
            var failingDevice = new HidDevice((N.SDL_hid_device*)0x51800002, hid.Lifetime, failedClose);
            Throws<SdlException>(failingDevice.Dispose, "HID close error surfaces after invalidation");
            failingDevice.Dispose();
            check(failedClose.CloseCount == 1, "Failed HID close cannot close twice");
            hid.Dispose(); hid.Dispose();
            Throws<ObjectDisposedException>(() => hid.EnumerateDevices(), "Disposed HID session rejected");
        }

        var root = Directory.CreateTempSubdirectory("sdl-storage-");
        try
        {
            using (var storage = StorageContainer.OpenLocal(root.FullName))
            {
                check(storage.IsReady, "Local storage is ready");
                Throws<InvalidOperationException>(app.Dispose, "Storage prevents application shutdown");
                storage.CreateDirectory("saves");
                storage.WriteAllBytes("saves/é.bin", [1, 2, 3]);
                check(storage.GetFileSize("saves/é.bin") == 3 && storage.ReadAllBytes("saves/é.bin").SequenceEqual(new byte[] { 1, 2, 3 }), "Local storage Unicode whole-file round trip");
                check(storage.Exists("saves/é.bin") && !storage.Exists("missing"), "Local storage existence checks");
                check(storage.GetPathInfo("saves").Type == StoragePathType.Directory && storage.GetPathInfo("saves/é.bin").Size == 3, "Local storage path metadata");
                Throws<ArgumentException>(() => storage.ReadFile("saves/é.bin", new byte[2]), "Short storage read rejected");
                Throws<ArgumentException>(() => storage.ReadFile("saves/é.bin", new byte[4]), "Overlong storage read rejected");
                var data = new byte[3]; storage.ReadFile("saves/é.bin", data);
                check(data[2] == 3, "Exact-sized storage read");
                storage.WriteAllBytes("empty", []);
                check(storage.ReadAllBytes("empty").Length == 0, "Empty storage files use valid zero-length buffers");
                storage.ReadFile("empty", []);
                storage.CopyFile("saves/é.bin", "copy.bin"); storage.Rename("copy.bin", "renamed.BIN");
                check(storage.Glob("*.bin", ignoreCase: true).Contains("renamed.BIN"), "Native storage glob case-insensitive matching");
                check(storage.GetEntries("saves").Contains("saves/é.bin"), "Storage enumeration yields relative paths");
                var visited = 0; storage.VisitEntries(_ => { visited++; return false; });
                check(visited == 1, "Storage visitor stops early successfully");
                var callbackError = new IOException("visitor sentinel");
                try { storage.VisitEntries(_ => throw callbackError); check(false, "Storage visitor exception missing"); }
                catch (IOException e) { check(ReferenceEquals(e, callbackError), "Storage visitor preserves exception identity"); }
                Throws<InvalidOperationException>(() => storage.VisitEntries(_ => { storage.Dispose(); return true; }), "Visitor cannot dispose its storage");
                Throws<InvalidOperationException>(() => storage.VisitEntries(_ => storage.IsReady), "Visitor cannot reenter its storage");
                check(storage.IsReady, "Storage recovers after visitor failure");
                foreach (var invalid in new[] { "../escape", "a/../b", "./x", "/absolute", "bad\\path", "bad\0path" })
                    Throws<ArgumentException>(() => storage.WriteAllBytes(invalid, [1]), "Unsafe storage path rejected");
                _ = storage.SpaceRemaining;
                storage.Remove("renamed.BIN"); storage.Remove("saves/é.bin"); storage.Remove("saves");
                check(!storage.Exists("renamed.BIN"), "Storage copy/rename/remove complete");
                using var titleProperties = new PropertyGroup();
                using var title = StorageContainer.OpenTitle(root.FullName, titleProperties);
                check(title.ReadAllBytes("empty").Length == 0, "Title storage opens supplied root");
                Throws<SdlException>(() => title.WriteAllBytes("forbidden", [1]), "Title storage remains read-only");
                storage.Dispose(); storage.Dispose();
                Throws<ObjectDisposedException>(() => storage.GetEntries(), "Disposed storage enumeration rejected");
            }
        }
        finally { root.Delete(recursive: true); }

        using (var backend = new MemoryBackend())
        {
            using var storage = StorageContainer.Open(backend, leaveOpen: true);
            Throws<InvalidOperationException>(backend.Dispose, "Attached backend cannot be disposed by caller");
            Throws<InvalidOperationException>(() => { using var duplicate = StorageContainer.Open(backend); }, "Custom backend cannot be shared by two containers");
            backend.Ready = false;
            check(!storage.IsReady, "Custom readiness false is not an exception");
            backend.Ready = true;
            Parallel.For(0, 100, index => storage.WriteAllBytes("parallel-" + index, [(byte)index]));
            check(backend.Files.Count == 100 && backend.MaxConcurrentWrites == 1, "Storage serializes concurrent writes to managed backend");
            backend.Files.Clear();
            storage.WriteAllBytes("one", [3, 2, 1]);
            check(storage.ReadAllBytes("one").SequenceEqual(new byte[] { 3, 2, 1 }), "Custom storage callbacks round-trip spans through SDL");
            check(storage.SpaceRemaining == 12345 && storage.GetFileSize("one") == 3, "Custom storage capacity and metadata callbacks");
            storage.CreateDirectory("dir"); storage.CopyFile("one", "two"); storage.Rename("two", "three");
            check(storage.Exists("three") && !storage.Exists("missing"), "Custom info handles absent entries");
            check(storage.GetEntries().Contains("one") && storage.Glob("t*").Contains("three"), "Custom enumerate callback supports native enumeration and glob");
            var produced = 0; backend.OnYield = () => produced++;
            storage.VisitEntries(_ => false);
            check(produced == 1 && backend.IteratorsDisposed > 0, "Early stop disposes backend iterator immediately");
            backend.OnYield = null;
            storage.Remove("three");
            var sentinel = new IOException("backend sentinel");
            backend.Failure = sentinel;
            foreach (var action in new Action[] { () => _ = storage.IsReady, () => _ = storage.SpaceRemaining, () => storage.GetPathInfo("one"), () => storage.ReadAllBytes("one"), () => storage.WriteAllBytes("x", [1]), () => storage.CreateDirectory("x"), () => storage.Remove("x"), () => storage.Rename("one", "x"), () => storage.CopyFile("one", "x"), () => storage.GetEntries(), () => storage.Glob() })
            {
                try { action(); check(false, "Custom storage callback failure missing"); }
                catch (IOException error) { check(ReferenceEquals(error, sentinel), "Backend exception identity survives callback boundary"); }
            }
            backend.Failure = null;
            backend.ReadFailure = sentinel;
            try { storage.ReadFile("one", new byte[3]); check(false, "Read callback failure missing"); }
            catch (IOException error) { check(ReferenceEquals(error, sentinel), "Read callback error preserves identity"); }
            backend.ReadFailure = null;
            backend.SizeOverride = ulong.MaxValue;
            Throws<IOException>(() => storage.ReadAllBytes("one"), "Oversized custom files rejected before allocation");
            backend.SizeOverride = null;
            backend.OnYield = () => storage.Dispose();
            Throws<InvalidOperationException>(() => storage.GetEntries(), "Backend callback cannot close its container");
            backend.OnYield = null;
            backend.BadName = "../escape";
            Throws<InvalidDataException>(() => storage.Glob(), "Bad backend entry cannot escape glob traversal");
            backend.BadName = null;
            backend.IteratorFailure = new InvalidOperationException("iterator cleanup");
            var visitorFailure = new IOException("visitor first");
            try { storage.VisitEntries(_ => throw visitorFailure); check(false, "Combined enumeration failures missing"); }
            catch (IOException error) { check(ReferenceEquals(error, visitorFailure), "Visitor failure takes priority over iterator cleanup failure"); }
            backend.IteratorFailure = null;
            check(storage.IsReady, "Enumeration errors do not poison subsequent storage operations");
            GC.Collect(2, GCCollectionMode.Forced, true, true);
            check(storage.ReadAllBytes("one")[0] == 3, "Managed backend and callbacks stay rooted across GC");
            storage.Dispose(); storage.Dispose();
            check(backend.DisposeCount == 0, "leaveOpen preserves caller backend ownership");
            using var reopened = StorageContainer.Open(backend, leaveOpen: true);
            check(reopened.GetFileSize("one") == 3, "Backend reusable after leaveOpen container closes");
        }
        var owned = new MemoryBackend();
        using (var storage = StorageContainer.Open(owned)) { storage.Dispose(); storage.Dispose(); }
        owned.Dispose(); owned.Dispose();
        check(owned.DisposeCount == 1, "Owned backend disposed exactly once");
        Throws<ObjectDisposedException>(() => { using var reopened = StorageContainer.Open(owned); }, "Disposed backend cannot be reopened");
        var closeFailure = new MemoryBackend { CloseFailure = new IOException("close sentinel") };
        var failedStorage = StorageContainer.Open(closeFailure);
        try { failedStorage.Dispose(); check(false, "Close callback failure missing"); }
        catch (IOException error) { check(ReferenceEquals(error, closeFailure.CloseFailure), "Storage flush error preserves original exception"); }
        failedStorage.Dispose();
        closeFailure.Dispose();
        check(closeFailure.DisposeCount == 1, "Failed storage close releases exactly once");
        Throws<ObjectDisposedException>(() => _ = failedStorage.IsReady, "Failed close still invalidates storage");
        using (var unsupported = StorageContainer.Open(new UnsupportedBackend()))
            Throws<NotSupportedException>(() => unsupported.WriteAllBytes("x", []), "Unsupported managed backend operation propagated");
        Throws<ArgumentException>(() => StorageContainer.OpenUser("", "app"), "User storage requires organization");
        Throws<ArgumentException>(() => StorageContainer.OpenUser("org", "bad\0app"), "User storage rejects embedded null names");
        var oldUserDriver = SdlHints.Get(HintNames.StorageUserDriver);
        try
        {
            SdlHints.Set(HintNames.StorageUserDriver, "sdl-sharp-nonexistent-storage-driver");
            using var options = new PropertyGroup();
            Throws<SdlException>(() => { using var userStorage = StorageContainer.OpenUser("SdlSharp.Tests", "HidStorage", options); }, "User storage native backend failure propagates");
        }
        finally { SdlHints.Set(HintNames.StorageUserDriver, oldUserDriver); }
        check(CallbackHandle.Count == registrations, "HID/storage callbacks leave no registrations");
    }

    private sealed class MemoryBackend : StorageBackend
    {
        internal readonly Dictionary<string, byte[]> Files = new();
        internal bool Ready = true;
        private int _activeWrites;
        internal int MaxConcurrentWrites;
        internal int DisposeCount;
        internal int IteratorsDisposed;
        internal Exception? Failure;
        internal Exception? ReadFailure;
        internal Exception? CloseFailure;
        internal ulong? SizeOverride;
        internal Action? OnYield;
        internal Exception? IteratorFailure;
        internal string? BadName;
        private void Check() { if (Failure != null) throw Failure; }
        public override bool IsReady { get { Check(); return Ready; } }
        public override ulong SpaceRemaining { get { Check(); return 12345; } }
        public override StoragePathInfo GetPathInfo(string path)
        {
            Check();
            return path.Length == 0 ? new(StoragePathType.Directory, 0) : Files.TryGetValue(path, out var data) ? new(StoragePathType.File, SizeOverride ?? (ulong)data.Length) : new(StoragePathType.None, 0);
        }
        public override void ReadFile(string path, Span<byte> destination) { Check(); if (ReadFailure != null) throw ReadFailure; Files[path].CopyTo(destination); }
        public override void WriteFile(string path, ReadOnlySpan<byte> source)
        {
            var active = Interlocked.Increment(ref _activeWrites);
            MaxConcurrentWrites = Math.Max(MaxConcurrentWrites, active);
            try { Check(); Thread.SpinWait(1000); Files[path] = source.ToArray(); }
            finally { Interlocked.Decrement(ref _activeWrites); }
        }
        public override void CreateDirectory(string path) => Check();
        public override void Remove(string path) { Check(); Files.Remove(path); }
        public override void Rename(string oldPath, string newPath) { Check(); Files[newPath] = Files[oldPath]; Files.Remove(oldPath); }
        public override void CopyFile(string oldPath, string newPath) { Check(); Files[newPath] = Files[oldPath].ToArray(); }
        public override IEnumerable<string> EnumerateDirectory(string path)
        {
            try
            {
                Check();
                foreach (var name in Files.Keys) { OnYield?.Invoke(); yield return BadName ?? name; }
            }
            finally { IteratorsDisposed++; if (IteratorFailure != null) throw IteratorFailure; }
        }
        protected override void Dispose(bool disposing) { DisposeCount++; if (CloseFailure != null) throw CloseFailure; }
    }
    private sealed class UnsupportedBackend : StorageBackend;

    private sealed class FakeHid : HidDeviceApi
    {
        internal int ReadResult = 3;
        private int _activeReads;
        internal int MaxConcurrentReads;
        internal int Nonblocking;
        internal int Timeout;
        internal int CloseCount;
        internal int CloseResult;
        internal byte ReportId;
        internal byte[] LastReport = [];
        private readonly List<nint> _allocations = new();
        private readonly N.SDL_hid_device_info* _info;
        private readonly N.SDL_PropertiesID _properties = N.Properties.SDL_CreateProperties();
        internal FakeHid()
        {
            _info = (N.SDL_hid_device_info*)Allocate(new byte[sizeof(N.SDL_hid_device_info)]);
            _info->path = (byte*)Allocate("fake/path\0"u8.ToArray());
            _info->product_string = Allocate(HidText.Encode("pad 🕹", HidText.CharacterSize)!);
            _info->serial_number = Allocate(HidText.Encode("serial é", HidText.CharacterSize)!);
        }
        private void* Allocate(byte[] bytes)
        {
            var pointer = NativeMemory.Alloc((nuint)bytes.Length);
            bytes.CopyTo(new Span<byte>(pointer, bytes.Length));
            _allocations.Add((nint)pointer);
            return pointer;
        }
        internal override int Close(N.SDL_hid_device* device)
        {
            CloseCount++;
            N.Properties.SDL_DestroyProperties(_properties);
            foreach (var pointer in _allocations) NativeMemory.Free((void*)pointer);
            return CloseResult;
        }
        internal override N.SDL_PropertiesID GetProperties(N.SDL_hid_device* device) => _properties;
        internal override N.SDL_hid_device_info* GetInfo(N.SDL_hid_device* device) => _info;
        internal override int SetNonblocking(N.SDL_hid_device* device, int value) { Nonblocking = value; return 0; }
        internal override int Read(N.SDL_hid_device* device, byte* data, nuint length)
        {
            var active = Interlocked.Increment(ref _activeReads);
            MaxConcurrentReads = Math.Max(MaxConcurrentReads, active);
            try
            {
                Thread.SpinWait(1000);
                if (ReadResult > 0 && ReadResult <= (int)length) for (var i = 0; i < ReadResult; i++) data[i] = (byte)(10 + i);
                return ReadResult;
            }
            finally { Interlocked.Decrement(ref _activeReads); }
        }
        internal override int ReadTimeout(N.SDL_hid_device* device, byte* data, nuint length, int timeout) { Timeout = timeout; return Read(device, data, length); }
        internal override int Write(N.SDL_hid_device* device, byte* data, nuint length) { LastReport = new ReadOnlySpan<byte>(data, (int)length).ToArray(); return (int)length; }
        internal override int SendFeatureReport(N.SDL_hid_device* device, byte* data, nuint length) => Write(device, data, length);
        internal override int GetFeatureReport(N.SDL_hid_device* device, byte* data, nuint length) { ReportId = data[0]; data[1] = 42; data[2] = 43; return 3; }
        internal override int GetInputReport(N.SDL_hid_device* device, byte* data, nuint length) => GetFeatureReport(device, data, length);
        internal override int GetReportDescriptor(N.SDL_hid_device* device, byte* data, nuint length) => Read(device, data, length);
        private static int CopyString(void* text, nuint length, string value)
        {
            var bytes = HidText.Encode(value, HidText.CharacterSize)!;
            if ((nuint)bytes.Length > length * (nuint)HidText.CharacterSize) throw new InvalidOperationException("Bad wchar capacity");
            bytes.CopyTo(new Span<byte>(text, bytes.Length));
            return 0;
        }
        internal override int GetManufacturer(N.SDL_hid_device* device, void* text, nuint length) => CopyString(text, length, "maker é");
        internal override int GetProduct(N.SDL_hid_device* device, void* text, nuint length) => CopyString(text, length, "product 🕹");
        internal override int GetSerialNumber(N.SDL_hid_device* device, void* text, nuint length) => CopyString(text, length, "serial 中");
        internal override int GetIndexedString(N.SDL_hid_device* device, int index, void* text, nuint length) => CopyString(text, length, "index " + index);
    }
}
