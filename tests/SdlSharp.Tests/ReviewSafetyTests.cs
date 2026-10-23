using System.Buffers;
using System.Runtime.InteropServices;
using SdlSharp;
using SdlSharp.Audio;
using SdlSharp.Graphics;
using SdlSharp.Input;
using N = SdlSharp.Native;

internal static unsafe class ReviewSafetyTests
{
    internal static void Run(Action<bool, string> check)
    {
        PackedPixelBounds(check);
        StreamCallbackReentrancy(check);
        BorrowedWindowDisposal(check);
        AudioCompletionReentrancy(check);
        DevicePropertyLifetimes(check);
        ClipboardCallbackReentrancy(check);

        using (var memory = new TrackedMemory())
        using (var first = Surface.Create(2, 2, PixelFormat.Rgba32))
        using (var second = Surface.Create(2, 2, PixelFormat.Rgba32))
        {
            var baseline = CallbackHandle.Count;
            var alternate = Surface.CreateFrom(2, 2, PixelFormat.Rgba32, memory.Memory, 8);
            first.AddAlternateImage(alternate); second.AddAlternateImage(alternate);
            alternate.Dispose(); alternate.Dispose();
            check(memory.Pins == 1, "Alternate images keep managed pixels pinned after wrapper disposal");
            GC.Collect(2, GCCollectionMode.Forced, true, true);
            var images = second.GetImages();
            using (var mapping = images[1].Lock()) mapping.GetRowSpan(0).Fill(42);
            check(memory.GetSpan()[0] == 42, "Retained alternate pixels survive compacting GC");
            foreach (var image in images) image.Dispose();
            first.RemoveAlternateImages();
            check(memory.Pins == 1, "One remaining native reference retains the pin");
            second.Dispose();
            check(memory.Pins == 0 && memory.Unpins == 1, "Final native destruction unpins exactly once");
            check(CallbackHandle.Count == baseline, "Final surface destruction frees the cleanup registration");
        }
        using (var memory = new TrackedMemory())
        {
            using (var surface = Surface.CreateFrom(2, 2, PixelFormat.Rgba32, memory.Memory, 8)) { }
            check(memory.Unpins == 1, "Unshared surface releases its pin");
            Throws<ArgumentException>(() => Surface.CreateFrom(4, 4, PixelFormat.Rgba32, memory.Memory, 16), check);
            check(memory.Pins == 0, "Invalid surface input never pins memory");
        }

        ProtectedSurfaceProperties(check);

        // Synthetic native identities exercise the real open/close ownership path
        // without opening or sending effects to any attached hardware.
        var joystick = new Joystick((N.SDL_Joystick*)0x710001);
        var closes = 0;
        try
        {
            using var first = Haptic.OpenFromJoystickCore(joystick, _ => 0x710002, _ => closes++);
            using var second = Haptic.OpenFromJoystickCore(joystick, _ => 0x710002, _ => closes++);
            Throws<InvalidOperationException>(joystick.Dispose, check);
            first.Dispose();
            Throws<InvalidOperationException>(joystick.Dispose, check);
            second.Dispose(); second.Dispose();
            joystick.Lifetime.EnsureCanDestroy();
            check(closes == 2, "Repeated haptic opens retain the joystick until the last native close");
            Throws<InvalidOperationException>(() => Haptic.OpenFromJoystickCore(joystick, _ => throw new InvalidOperationException(), _ => closes++), check);
            joystick.Lifetime.EnsureCanDestroy();
            check(closes == 2, "Failed haptic open releases its joystick lease without closing a nonexistent handle");
        }
        finally { joystick.Lifetime.Release(() => { }); joystick.Dispose(); }
    }
    private static void PackedPixelBounds(Action<bool, string> check)
    {
        PixelFormat[] formats = [PixelFormat.Xrgb4444, PixelFormat.Xrgb1555, PixelFormat.Xrgb8888, PixelFormat.Rgbx8888, PixelFormat.Xrgb2101010];
        foreach (var format in formats)
        {
            const int width = 8;
            var rowBytes = width * PixelFormatDetails.Get(format).BytesPerPixel;
            // Creating and disposing a surface never reads the short backing buffer.
            Throws<ArgumentException>(() => { using var invalid = Surface.CreateFrom(width, 1, format, new byte[rowBytes - 1], rowBytes); }, check);
            using var surface = Surface.CreateFrom(width, 2, format, new byte[rowBytes * 2], rowBytes);
            using var pixels = surface.Lock();
            check(pixels.RowBytes == rowBytes && pixels.GetRowSpan(1).Length == rowBytes, $"{format} mappings include padding bits within each pixel");
        }
        using var indexed = Surface.Create(9, 2, PixelFormat.Index1Msb);
        using var indexedPixels = indexed.Lock();
        check(indexedPixels.RowBytes == 2, "Sub-byte indexed rows round up to whole bytes");

        using var target = Surface.Create(16, 16, PixelFormat.Rgba32);
        using var renderer = Renderer.CreateSoftware(target);
        foreach (var format in formats)
        {
            using var texture = renderer.CreateTexture(format, TextureAccess.Streaming, 8, 2);
            var rowBytes = 8 * PixelFormatDetails.Get(format).BytesPerPixel;
            Throws<ArgumentException>(() => texture.Update(null, new byte[2 * rowBytes - 1], rowBytes), check);
            texture.Update(null, new byte[2 * rowBytes], rowBytes);
            using var pixels = texture.Lock();
            check(pixels.GetRowSpan(1).Length == rowBytes, $"{format} texture mappings expose the full stored row");
        }
    }
    private static void StreamCallbackReentrancy(Action<bool, string> check)
    {
        using var surface = Surface.Create(2, 2, PixelFormat.Rgba32);
        // Exercise the disposal preflight without freeing memory if the guard regresses.
        using var output = new CallbackStream(surface.Lifetime.EnsureCanDestroy);
        Throws<InvalidOperationException>(() => surface.SaveBmp(output), check);
        check(surface.Width == 2, "Failed codec callback leaves the surface usable");
        using var valid = new MemoryStream();
        surface.SaveBmp(valid);
        valid.Position = 0;
        using var input = new CallbackStream(surface.Lifetime.EnsureCanDestroy, valid.ToArray());
        Throws<InvalidOperationException>(() => { using var loaded = Surface.LoadBmp(input); }, check);
        surface.Lifetime.EnsureCanDestroy();
    }
    private static void BorrowedWindowDisposal(Action<bool, string> check)
    {
        SdlHints.Set(HintNames.VideoDriver, "dummy");
        using var app = new Application(InitFlags.Video);
        using var owner = Window.Create("borrowed disposal", 16, 16, WindowFlags.Hidden);
        using var alias = Window.FromId(owner.Id)!;
        using (owner.Lifetime.Retain())
        {
            alias.Dispose();
            check(owner.Size.W == 16, "Disposing a borrowed window does not require releasing native leases");
        }
        using var callbackAlias = Window.FromId(owner.Id)!;
        using (CallbackErrors.Enter()) callbackAlias.Dispose();
        check(owner.Size.W == 16, "Callback-local window views can be disposed without destroying their native owner");
    }
    private static void AudioCompletionReentrancy(Action<bool, string> check)
    {
        using var stream = AudioStream.Create(new(AudioFormat.F32, 1, 48000), new(AudioFormat.F32, 1, 48000));
        using var memory = new TrackedMemory { OnUnpin = stream.Lifetime.EnsureCanDestroy };
        var completion = stream.PutDataAsync(memory.Memory);
        stream.Clear();
        Throws<InvalidOperationException>(() => completion.GetAwaiter().GetResult(), check);
        check(memory.Pins == 0 && memory.Unpins == 1, "Audio completion releases memory once even when its unpin callback throws");
        stream.Lifetime.EnsureCanDestroy();
    }
    private static void DevicePropertyLifetimes(Action<bool, string> check)
    {
        using var app = new Application(InitFlags.Gamepad);
        using var virtualDevice = VirtualJoystick.Create(new()
        {
            Name = "property lifetime regression", Type = JoystickType.Gamepad,
            AxisCount = 2, ButtonCount = 1, AxisMask = 3, ButtonMask = 1
        });
        using var gamepad = Gamepad.Open(virtualDevice.Id);
        // Query through the gamepad first to verify that both views bind to the
        // actual joystick owner, regardless of the order they are acquired.
        using var gamepadProperties = gamepad.Properties;
        using var joystickProperties = virtualDevice.Device.Properties;
        joystickProperties.SetNumber("test.retained-value", 42);
        using (joystickProperties.Lock()) Throws<InvalidOperationException>(virtualDevice.Device.Dispose, check);
        using (gamepadProperties.Lock()) Throws<InvalidOperationException>(virtualDevice.Device.Dispose, check);
        gamepad.Dispose();
        check(gamepadProperties.Id == joystickProperties.Id, "Gamepad and joystick share a native property group");
        check(joystickProperties.GetNumber("test.retained-value") == 42, "Closing gamepad leaves independently opened joystick properties usable");
        virtualDevice.Dispose();
        Throws<ObjectDisposedException>(() => joystickProperties.GetNames(), check);
        Throws<ObjectDisposedException>(() => gamepadProperties.GetNames(), check);

        using var secondVirtual = VirtualJoystick.Create(new() { Type = JoystickType.Gamepad, ButtonCount = 1, ButtonMask = 1 });
        using var secondGamepad = Gamepad.Open(secondVirtual.Id);
        using var secondProperties = secondVirtual.Device.Properties;
        secondProperties.SetNumber("test.gamepad-reference", 7);
        using var secondGamepadProperties = secondGamepad.Properties;
        using var borrowedJoystick = secondGamepad.GetJoystick();
        secondVirtual.Device.Dispose();
        check(secondGamepadProperties.GetNumber("test.gamepad-reference") == 7 && borrowedJoystick.Name != null,
            "A gamepad's implicit joystick reference preserves property and joystick views after direct close");
        using (secondGamepadProperties.Lock()) Throws<InvalidOperationException>(secondGamepad.Dispose, check);
        using var reopenedJoystick = Joystick.Open(secondVirtual.Id);
        secondGamepad.Dispose();
        check(reopenedJoystick.Name != null,
            "An independently opened joystick outlives the gamepad that first supplied its borrowed identity");
        Throws<ObjectDisposedException>(() => _ = borrowedJoystick.Name, check);
        check(secondProperties.GetNumber("test.gamepad-reference") == 7, "Reopened joystick preserves the shared native property identity");
        reopenedJoystick.Dispose();
        Throws<ObjectDisposedException>(() => secondProperties.GetNames(), check);

        // A root native reference can be opened after first seeing a resource as
        // a borrow. Destroying that original parent must not destroy the new open.
        var parent = NativeLifetime.Get(typeof(ReviewSafetyTests), 0x710010, true);
        var borrowed = NativeLifetime.Get(typeof(ReviewSafetyTests), 0x710011, false, parent: parent);
        var independent = NativeLifetime.Get(typeof(ReviewSafetyTests), 0x710011, true);
        try
        {
            parent.Release(static () => { });
            independent.Check();
            check(ReferenceEquals(borrowed, independent), "Promoting a borrow preserves identity while removing the old parent dependency");
        }
        finally { independent.Release(static () => { }); parent.Release(static () => { }); }
    }
    private static void ClipboardCallbackReentrancy(Action<bool, string> check)
    {
        SdlHints.Set(HintNames.VideoDriver, "dummy");
        using var app = new Application(InitFlags.Video);
        var registrations = CallbackHandle.Count;
        const string mimeType = "application/x-sdl-sharp-reentrant-text";
        Clipboard.SetData(type =>
        {
            if (type != null) Clipboard.Text = "replacement during data callback";
            return new byte[] { 42 };
        }, mimeType);
        _ = Clipboard.GetData(mimeType);
        Throws<InvalidOperationException>(Application.ThrowPendingCallbackException, check);
        Clipboard.ClearData();
        check(CallbackHandle.Count == registrations, "Rejected clipboard replacement leaves no abandoned registration");
        Clipboard.Text = "replacement after callback";
        check(Clipboard.Text == "replacement after callback", "Clipboard replacement works after callback unwinds");
    }
    private sealed class CallbackStream(Action callback, byte[]? bytes = null) : MemoryStream(bytes ?? new byte[1024], writable: true)
    {
        public override int Read(Span<byte> buffer) { callback(); return base.Read(buffer); }
        public override void Write(ReadOnlySpan<byte> buffer) { callback(); base.Write(buffer); }
    }
    private static void ProtectedSurfaceProperties(Action<bool, string> check)
    {
        const string key = "SdlSharp.Surface.PinnedPixels";
        using var memory = new TrackedMemory();
        using var parent = Surface.Create(2, 2, PixelFormat.Rgba32);
        using var surface = Surface.CreateFrom(2, 2, PixelFormat.Rgba32, memory.Memory, 8);
        using var properties = surface.Properties;
        using var alias = surface.Properties;
        using var source = new PropertyGroup();
        var cleanupCount = 0;
        var registrations = CallbackHandle.Count;
        Action[] mutations = [
            () => alias.Clear(key), () => alias.SetString(key, "replace"),
            () => alias.SetNumber(key, 2), () => alias.SetFloat(key, 2),
            () => alias.SetBoolean(key, true), () => alias.SetPointer(key, 2),
            () => alias.SetPointer(key, 2, _ => cleanupCount++),
            () => alias.Clear(key + "\0suffix"), () => source.SetNumber(key, 2)
        ];
        foreach (var mutate in mutations) Throws<ArgumentException>(mutate, check);
        check(CallbackHandle.Count == registrations && cleanupCount == 0 && memory.Pins == 1,
            "Rejected internal-property mutations retain the pin and do not transfer cleanup ownership");
        properties.SetNumber("application.value", 7);
        foreach (var name in properties.GetNames()) properties.Clear(name);
        check(!properties.Has("application.value") && memory.Pins == 1, "Public property enumeration and clearing preserve internal pin metadata");
        // Copying a native producer's reserved key must not bypass managed protection.
        SdlSharp.Native.Properties.SDL_SetNumberProperty(source.Id, "SdlSharp.Surface.PinnedPixels"u8, 9);
        source.SetNumber("application.value", 42);
        source.CopyTo(properties);
        check(properties.GetNumber("application.value") == 42 && memory.Pins == 1, "Copy filters reserved source keys while copying application values");
        using var destination = new PropertyGroup(); properties.CopyTo(destination);
        check(!destination.Has(key) && destination.GetNumber("application.value") == 42, "Surface property copy omits ownership registrations");
        source.SetPointer("application.cleanup", 1, _ => cleanupCount++);
        source.CopyTo(destination);
        check(!destination.Has("application.cleanup") && cleanupCount == 0, "Copy preserves SDL's cleanup-property exclusion");
        destination.SetPointer("application.value", 1, _ => cleanupCount++);
        source.CopyTo(destination);
        check(cleanupCount == 1, "Copy invokes replaced destination cleanup exactly once");
        parent.AddAlternateImage(surface); surface.Dispose();
        GC.Collect(2, GCCollectionMode.Forced, true, true);
        check(memory.Pins == 1, "Protected pin survives wrapper disposal and compacting GC");
        parent.RemoveAlternateImages();
        check(memory.Pins == 0 && memory.Unpins == 1, "Final native reference still unpins exactly once");
    }
    private static void Throws<T>(Action action, Action<bool, string> check) where T : Exception
    {
        try { action(); check(false, $"Expected {typeof(T).Name}"); }
        catch (T) { check(true, $"Rejected with {typeof(T).Name}"); }
    }
    private sealed class TrackedMemory : MemoryManager<byte>
    {
        private readonly byte[] _bytes = new byte[16];
        private GCHandle _pin;
        internal int Pins;
        internal int Unpins;
        internal Action? OnUnpin;
        public override Span<byte> GetSpan() => _bytes;
        public override MemoryHandle Pin(int elementIndex = 0)
        {
            _pin = GCHandle.Alloc(_bytes, GCHandleType.Pinned); Pins++;
            return new MemoryHandle((byte*)_pin.AddrOfPinnedObject() + elementIndex, default, this);
        }
        public override void Unpin() { _pin.Free(); Pins--; Unpins++; OnUnpin?.Invoke(); }
        protected override void Dispose(bool disposing) { }
    }
}
