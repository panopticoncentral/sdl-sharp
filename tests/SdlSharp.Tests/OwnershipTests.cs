using SdlSharp;
using SdlSharp.Audio;
using SdlSharp.Graphics;
using SdlSharp.Graphics.Gpu;
using SdlSharp.Input;
using N = SdlSharp.Native;

internal static unsafe class OwnershipTests
{
    internal static void Run(Action<bool, string> check)
    {
        void Throws<T>(Action action, string name) where T : Exception
        {
            try { action(); check(false, name + " did not throw"); }
            catch (T) { check(true, name); }
        }
        SdlHints.Set(HintNames.VideoDriver, "dummy");
        SdlHints.Set(HintNames.AudioDriver, "dummy");
        Application? oldApplication = null;
        var staleQuitHandlerCalls = 0;
        using (var app = new Application(InitFlags.Video | InitFlags.Audio | InitFlags.Joystick | InitFlags.Events))
        {
            oldApplication = app;
            Application.Quit += _ => staleQuitHandlerCalls++;
            Throws<InvalidOperationException>(() => { using var second = new Application(0); }, "Reject overlapping application sessions");
            var window = Window.Create("ownership", 32, 32, WindowFlags.Hidden);
            var alias = Window.FromId(window.Id)!;
            var properties = window.Properties;
            Throws<InvalidOperationException>(app.Dispose, "Reject application shutdown with live resources");
            using (var locked = properties.Lock())
            {
                Throws<InvalidOperationException>(window.Dispose, "Property lock retains native owner");
                locked.Dispose(); locked.Dispose();
            }
            var surface = window.GetSurface();
            window.Dispose(); window.Dispose();
            Throws<ObjectDisposedException>(() => _ = alias.Size, "Borrowed window invalidates");
            Throws<ObjectDisposedException>(() => _ = surface.Width, "Window surface invalidates");
            Throws<ObjectDisposedException>(() => properties.GetNames(), "Borrowed properties invalidate");
            alias.Dispose(); surface.Dispose(); properties.Dispose();

            using (var filters = new FileDialog.FilterBuffer([new("Images", "png;jpg")]))
            {
                var address = (nint)filters.Pointer;
                GC.Collect(2, GCCollectionMode.Forced, blocking: true, compacting: true);
                check((nint)filters.Pointer == address && System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)filters.Pointer[0].pattern) == "png;jpg", "Dialog filter array and strings survive compacting GC");
                filters.Dispose(); filters.Dispose();
                Throws<ObjectDisposedException>(() => _ = (nint)filters.Pointer, "Disposed dialog filters reject access");
            }
            Throws<ArgumentNullException>(() => { using var filters = new FileDialog.FilterBuffer([new("valid", "*"), new(null!, "*")]); }, "Invalid later filter cleans earlier pins");
            using (var dialogProperties = new PropertyGroup())
            {
                dialogProperties.SetPointer("SDL.filedialog.filters", 1);
                Throws<ArgumentException>(() => FileDialog.ShowAsync(FileDialogType.OpenFile, dialogProperties), "Managed dialogs reject unowned filter pointers");
            }
            using (var indexed = Surface.Create(4, 4, PixelFormat.Index8))
            {
                using var oldPalette = indexed.CreatePalette();
                using var newPalette = indexed.CreatePalette();
                Throws<ObjectDisposedException>(() => _ = oldPalette.Count, "Recreating surface palette invalidates borrowed palette");
                using var replacementPalette = new Palette(256);
                indexed.SetPalette(replacementPalette);
                Throws<ObjectDisposedException>(() => _ = newPalette.Count, "Replacing surface palette invalidates borrowed palette");
                using var borrowed = indexed.GetPalette()!;
                check(borrowed.Count == 256, "Replacement palette remains usable");
            }
            using (var primary = Surface.Create(4, 4, PixelFormat.Rgba32))
            {
                var alternate = Surface.Create(8, 8, PixelFormat.Rgba32);
                primary.AddAlternateImage(alternate);
                alternate.Dispose();
                var versions = primary.GetImages();
                check(versions.Length == 2, "Alternate surface is retained by primary surface");
                using (var pixels = versions[1].Lock())
                    Throws<InvalidOperationException>(primary.RemoveAlternateImages, "Mapped alternate blocks reference removal");
                primary.RemoveAlternateImages();
                Throws<ObjectDisposedException>(() => _ = versions[1].Width, "Removing alternates invalidates borrowed images");
                check(versions[0].Width == 4, "Removing alternates preserves primary image");
                foreach (var version in versions) version.Dispose();
            }

            using (var target = Surface.Create(8, 8, PixelFormat.Rgba32))
            {
                var renderer = Renderer.CreateSoftware(target);
                Throws<InvalidOperationException>(target.Dispose, "Software renderer retains target surface");
                var texture = renderer.CreateTexture(PixelFormat.Rgba32, TextureAccess.Streaming, 4, 4);
                Throws<ArgumentException>(() => texture.Update(null, new byte[4], 16), "Short texture buffer rejected");
                Throws<ArgumentOutOfRangeException>(() => texture.Update(new Rectangle(-1, 0, 2, 2), new byte[16], 8), "Out of bounds texture area rejected");
                texture.Update(null, new byte[64], 16);
                using (var pixels = texture.Lock(new Rectangle(1, 1, 2, 2)))
                {
                    check(pixels.GetRowSpan(1).Length == 8, "Partial lock exposes only row pixels");
                    pixels.GetRowSpan(1).Fill(127);
                    Throws<ArgumentOutOfRangeException>(() => pixels.GetRowSpan(2), "Pixel row bounds checked");
                    Throws<InvalidOperationException>(texture.Dispose, "Pixel mapping retains texture");
                    Throws<InvalidOperationException>(renderer.Dispose, "Pixel mapping retains renderer hierarchy");
                    Throws<InvalidOperationException>(() => texture.Lock(), "Second texture lock rejected");
                    pixels.Dispose(); pixels.Dispose();
                    Throws<ObjectDisposedException>(() => pixels.GetRowSpan(0), "Disposed pixel mapping rejected");
                }
                using (var mapping = texture.LockToSurface())
                {
                    check(mapping.Surface.Width == 4, "Texture surface mapping dimensions");
                    mapping.Dispose(); mapping.Dispose();
                    Throws<ObjectDisposedException>(() => _ = mapping.Surface.Width, "Unlocked texture surface invalidates");
                }
                Throws<ArgumentException>(() => renderer.GeometryRaw(null, [0f], 8, [new FColor()], 16, [], 8, 2, []), "Geometry stride bounds checked");
                Throws<ArgumentOutOfRangeException>(() => renderer.GeometryRaw(null, [0f, 0f], 8, [new FColor()], 16, [], 8, 1, [1]), "Geometry indices checked");
                using (var yuv = renderer.CreateTexture(PixelFormat.Iyuv, TextureAccess.Streaming, 4, 4))
                {
                    Throws<ArgumentException>(() => yuv.Update(null, new byte[23], 4), "Contiguous planar update checks all planes");
                    Throws<ArgumentException>(() => yuv.UpdateYuv(null, new byte[16], 4, new byte[3], 2, new byte[4], 2), "Separate chroma plane bounds checked");
                    Throws<ArgumentException>(() => yuv.UpdateYuv(new Rectangle(1, 0, 2, 2), new byte[4], 2, new byte[1], 1, new byte[1], 1), "Planar origins enforce chroma alignment");
                    yuv.Update(null, new byte[24], 4);
                    check(true, "Complete planar update reaches SDL safely");
                }
                renderer.Dispose();
                Throws<ObjectDisposedException>(() => _ = texture.Width, "Renderer destroys texture aliases");
                texture.Dispose(); texture.Dispose();
            }
            using (var pixels = Surface.Create(2, 2, PixelFormat.Rgba32))
            {
                using (var mapping = pixels.Lock())
                {
                    mapping.GetRowSpan(0).Fill(255);
                    Throws<InvalidOperationException>(pixels.Dispose, "Surface mapping retains its owner");
                }
                using var memory = new MemoryStream();
                pixels.SaveBmp(memory);
                check(memory.CanWrite, "Saving leaves caller stream open");
                memory.Position = 0;
                using var decoded = Surface.LoadBmp(memory);
                check(decoded.Size == pixels.Size && memory.CanRead, "BMP stream round trip and leave-open");
                Throws<IOException>(() => Surface.LoadBmp(new ThrowingStream()), "Read callback exceptions preserve managed error");
                Throws<IOException>(() => pixels.SaveBmp(new ThrowingStream()), "Write callback exceptions preserve managed error");
            }
            using (var original = Surface.Create(2, 2, PixelFormat.Rgba32))
            using (var storage = new MemoryStream())
            {
                using (var output = new NonSeekableStream(storage)) original.SavePng(output);
                storage.Position = 0;
                using var input = new NonSeekableStream(storage);
                using var decoded = Surface.Load(input);
                check(decoded.Size == original.Size && storage.CanRead, "Non-seekable codec streams are buffered and left open");
            }
            var managedPixels = new byte[16];
            using (var pinned = Surface.CreateFrom(2, 2, PixelFormat.Rgba32, managedPixels.AsMemory(), 8))
            {
                GC.Collect(); GC.WaitForPendingFinalizers(); GC.Collect();
                using var pixels = pinned.Lock();
                pixels.GetRowSpan(1)[0] = 99;
                check(managedPixels[8] == 99, "Managed surface pixels stay pinned across compacting collections");
            }
            using (var callbackOwner = new Palette(2))
            using (SdlHints.Observe("SDL_SHARP_REENTRANCY", (_, _) => callbackOwner.Dispose()))
            {
                Throws<InvalidOperationException>(Application.ThrowPendingCallbackException, "Callback reentrancy cannot destroy an in-use native resource");
                check(callbackOwner.Count == 2, "Rejected callback disposal leaves resource usable");
            }
            var registrationCount = CallbackHandle.Count;
            using (var hitTestWindow = Window.Create("hit-test", 8, 8, WindowFlags.Hidden))
            {
                try { hitTestWindow.SetHitTest((_, _) => HitTestResult.Normal); }
                catch (SdlException) { /* Dummy video need not support hit testing. */ }
            }
            check(CallbackHandle.Count == registrationCount, "Hit-test registration failure or disposal releases callback state");
            var source = new byte[] { 10, 20, 30, 255 };
            var destination = new byte[4];
            PixelConverter.Convert(1, 1, PixelFormat.Rgba32, source, 4, PixelFormat.Rgba32, destination, 4);
            check(source.SequenceEqual(destination), "Pixel conversion");
            Throws<ArgumentException>(() => PixelConverter.Convert(2, 1, PixelFormat.Rgba32, source, 8, PixelFormat.Rgba32, destination, 8), "Pixel conversion rejects short buffers");

            using (var props = new PropertyGroup())
            {
                var cleaned = new List<nint>();
                props.SetPointer("value", 11, cleaned.Add);
                props.SetPointer("value", 12, cleaned.Add);
                check(cleaned.SequenceEqual(new nint[] { 11 }), "Replacing owned property cleans old value once");
                props.Dispose(); props.Dispose();
                check(cleaned.SequenceEqual(new nint[] { 11, 12 }), "Destroying owned property cleans final value once");
            }
            var changes = 0;
            using (var subscription = SdlHints.Observe("SDL_SHARP_OBSERVE", (_, _) => changes++))
            {
                SdlHints.Set("SDL_SHARP_OBSERVE", "1");
                check(changes == 2, "Hint subscription receives initial and changed value");
                Throws<InvalidOperationException>(app.Dispose, "Hint subscription retains SDL session");
                subscription.Dispose(); subscription.Dispose();
                SdlHints.Set("SDL_SHARP_OBSERVE", "2");
                check(changes == 2, "Disposed hint subscription stops callbacks");
            }
            using (SdlHints.Observe("SDL_SHARP_THROW", (_, _) => throw new FormatException("callback")))
                Throws<FormatException>(Application.ThrowPendingCallbackException, "Callback exception returned through managed channel");
            var token = CallbackHandle.Alloc(new object());
            var pointer = (nint)token;
            token.Free();
            var replacement = CallbackHandle.Alloc(new object());
            check((nint)replacement != pointer && CallbackHandle.FromIntPtr(pointer).Target == null, "Late callbacks cannot reach recycled handle targets");
            replacement.Free();

            Clipboard.Text = "héllo";
            check(Clipboard.Text == "héllo", "UTF8 clipboard text copied and freed");
            for (var i = 0; i < 3; i++)
            {
                var value = (byte)i;
                Clipboard.SetData(_ => new[] { value }, "application/x-sdl-sharp-test");
                check(Clipboard.GetData("application/x-sdl-sharp-test").SequenceEqual(new[] { value }), "Replacing clipboard offer preserves new registration");
            }
            Clipboard.ClearData();
            using (var stream = AudioStream.Create(new(AudioFormat.F32, 2, 48000), new(AudioFormat.F32, 2, 48000)))
            {
                Throws<ArgumentException>(() => stream.PutPlanarData([new byte[4], new byte[4]], 2), "Short planar audio rejected");
                Throws<ArgumentException>(() => stream.PutPlanarData([new byte[8]], 2), "Planar channel mismatch rejected");
                using (var locked = stream.Lock())
                {
                    Throws<InvalidOperationException>(stream.Dispose, "Audio lock retains stream");
                    locked.Dispose(); locked.Dispose();
                }
                var pending = stream.PutDataAsync(new byte[64]);
                stream.Clear();
                check(pending.IsCompletedSuccessfully, "No-copy audio pin releases when queue is cleared");
            }
            using (var joystick = VirtualJoystick.Create(new() { Name = "SDL regression", AxisCount = 2, ButtonCount = 2, HatCount = 1 }))
            {
                check(Joystick.IsVirtual(joystick.Id), "Virtual joystick attaches");
                using var duplicate = Joystick.Open(joystick.Id);
                duplicate.Dispose();
                check(joystick.Device.Name != null, "Closing one reference-counted joystick open preserves another");
                joystick.Device.SetVirtualAxis(0, 1234);
                joystick.Device.SetVirtualButton(1, true);
                Joystick.Update();
                check(joystick.Device.GetAxis(0) == 1234 && joystick.Device.GetButton(1), "Virtual joystick input round trip");
            }
            Keyboard.SetScancodeName(Scancode.A, "test-a");
            check(Keyboard.GetScancodeName(Scancode.A) == "test-a", "Scancode name storage persists");
            check(Keyboard.GetScancodeFromName("test-a") == Scancode.A, "Scancode lookup");
            _ = Keyboard.GetDevices(); _ = Mouse.GetDevices();
            check(SdlGuid.TryParse("03000000000000000000000000000000", out _), "Strict GUID accepts canonical hex");
            check(!SdlGuid.TryParse("bad", out _) && !SdlGuid.TryParse(new string('g', 32), out _), "Strict GUID rejects malformed input");
            Throws<FormatException>(() => SdlGuid.Parse("bad"), "GUID Parse throws on malformed input");
            var instant = DateTimeOffset.UnixEpoch.AddTicks(123456789);
            check(SdlDateTime.ToDateTimeOffset(SdlDateTime.FromDateTimeOffset(instant)) == instant, "Timestamp round trip preserves ticks");
            Throws<ArgumentException>(() => SdlDateTime.FromDateTime(new DateTime(2026, 1, 1)), "Unspecified timezone rejected");
            check(default(PixelFormatDetails).BitsPerPixel == 0, "Default pixel details are a safe value");
            Throws<InvalidOperationException>(() => default(PixelFormatDetails).MapRgb(0, 0, 0), "Default pixel mapping reports managed error");
            var threadWindow = Window.Create("thread", 8, 8, WindowFlags.Hidden);
            Exception? threadError = null;
            var thread = new Thread(() => { try { _ = threadWindow.Size; } catch (Exception e) { threadError = e; } });
            thread.Start(); thread.Join();
            check(threadError is InvalidOperationException, "Window thread affinity enforced");
            threadWindow.Dispose();
            // Real SDL surface plus an injected native-return operation: no physical camera is needed.
            var cameraOwner = NativeLifetime.Get(typeof(Camera), 9001, true);
            var image = N.Surface.SDL_CreateSurface(2, 2, N.SDL_PixelFormat.SDL_PIXELFORMAT_RGBA32);
            var returns = 0;
            var frame = new CameraFrame(cameraOwner, image, 42, () => returns++);
            check(frame.TimestampNanoseconds == 42 && frame.Surface.Width == 2, "Camera frame metadata");
            Throws<InvalidOperationException>(() => cameraOwner.Release(() => { }), "Outstanding camera frame prevents close");
            frame.Dispose(); frame.Dispose();
            check(returns == 1, "Camera frame returns to native owner once");
            Throws<ObjectDisposedException>(() => _ = frame.Surface.Width, "Returned camera surface invalidates");
            cameraOwner.Release(() => { });
            N.Surface.SDL_DestroySurface(image);

            // Tray node invalidation is exercised without opening operating-system tray UI.
            var tray = new Tray((N.SDL_Tray*)9010);
            var menu = new TrayMenu((N.SDL_TrayMenu*)9011, tray);
            var entry = new TrayEntry((N.SDL_TrayEntry*)9012, tray, menu.Lifetime);
            var entryAlias = new TrayEntry((N.SDL_TrayEntry*)9012, tray);
            var submenu = new TrayMenu((N.SDL_TrayMenu*)9013, tray, entry.Lifetime);
            var descendant = new TrayEntry((N.SDL_TrayEntry*)9014, tray, submenu.Lifetime);
            entry.Lifetime.Invalidate();
            Throws<ObjectDisposedException>(() => _ = entryAlias.Label, "Removed tray entry aliases invalidate");
            Throws<ObjectDisposedException>(() => _ = submenu.Entries, "Removed tray submenu invalidates");
            Throws<ObjectDisposedException>(() => _ = descendant.Label, "Removed tray descendants invalidate");
            tray.Lifetime.Invalidate(); tray.Dispose();
            Throws<FormatException>(() => Application.RunOnMainThread(() => throw new FormatException("dispatch")), "Synchronous main-thread action rethrows to caller");
            using (var temporary = Surface.Create(1, 1, PixelFormat.Rgba32))
                Application.RunOnMainThread(temporary.Dispose);
            check(true, "Main-thread dispatcher permits normal resource disposal");
            var concurrentOwner = NativeLifetime.Get(typeof(OwnershipTests), 1, true);
            Parallel.For(0, 1000, i =>
            {
                var child = NativeLifetime.Get(typeof(OwnershipTests), 100 + i, true, parent: concurrentOwner);
                child.Release(() => { });
            });
            concurrentOwner.Release(() => { });
            check(!concurrentOwner.IsAlive, "Concurrent child registration and release preserve lifetime tracking");
            SyntheticGpu(check, Throws<InvalidOperationException>, Throws<ObjectDisposedException>, Throws<ArgumentException>);
        }
        using var nextApp = new Application(InitFlags.Events);
        oldApplication.Dispose();
        check(Application.WasInit(InitFlags.Events) == InitFlags.Events, "Old application disposal cannot quit a new session");
        var quit = new N.SDL_Event { type = (uint)N.SDL_EventType.SDL_EVENT_QUIT };
        N.Common.Check(N.Events.SDL_PushEvent(&quit));
        Application.DispatchEvents();
        check(staleQuitHandlerCalls == 0, "Static event subscriptions are cleared between sessions");
        check(true, "SDL session restarts after clean shutdown");
        nextApp.Dispose(); nextApp.Dispose();
    }
    private static void SyntheticGpu(Action<bool,string> check, Action<Action,string> invalid, Action<Action,string> disposed, Action<Action,string> argument)
    {
        // No GPU driver or fake pointer is invoked: these operations must fail at the managed boundary.
        var device = new GpuDevice((N.SDL_GPUDevice*)1001);
        var other = new GpuDevice((N.SDL_GPUDevice*)1002);
        var command = new GpuCommandBuffer(device, (N.SDL_GPUCommandBuffer*)1003);
        var resource = new GpuBuffer(other, (N.SDL_GPUBuffer*)1004);
        argument(() => command.Use(resource), "Cross-device GPU resource rejected before native access");
        invalid(device.Dispose, "Active command buffer retains GPU device");
        var pass = new object();
        command.BeginPass(pass);
        invalid(command.Submit, "Submission with active pass rejected");
        invalid(() => command.BeginCopyPass(), "Overlapping GPU passes rejected");
        command.EndPass(pass);
        invalid(() => command.CheckPass(pass), "Ended GPU pass rejected");
        var texture = new GpuTexture(device, (N.SDL_GPUTexture*)1005, false, command.Lifetime) { OwnerCommand = command };
        var second = new GpuCommandBuffer(device, (N.SDL_GPUCommandBuffer*)1006);
        argument(() => second.Use(texture), "Swapchain texture confined to acquiring command buffer");
        command.Complete(); second.Complete(); command.Dispose(); second.Dispose();
        disposed(() => _ = texture.NativeHandle, "Swapchain texture invalidates after completion");
        resource.Lifetime.Invalidate(); device.Lifetime.Invalidate(); other.Lifetime.Invalidate();
        resource.Dispose(); device.Dispose(); other.Dispose(); texture.Dispose();
        check(true, "Managed GPU state cleanup completed without native fake-handle calls");
    }
    private sealed class NonSeekableStream(MemoryStream stream) : Stream
    {
        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => true;
        public override long Length => throw new NotSupportedException();
        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
        public override void Flush() => stream.Flush();
        public override int Read(byte[] buffer, int offset, int count) => stream.Read(buffer, offset, count);
        public override void Write(byte[] buffer, int offset, int count) => stream.Write(buffer, offset, count);
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
    }
    private sealed class ThrowingStream : Stream
    {
        public override bool CanRead => true;
        public override bool CanSeek => true;
        public override bool CanWrite => true;
        public override long Length => throw new IOException("length");
        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
        public override void Flush() => throw new IOException("flush");
        public override int Read(byte[] buffer, int offset, int count) => throw new IOException("read");
        public override int Read(Span<byte> buffer) => throw new IOException("read");
        public override void Write(byte[] buffer, int offset, int count) => throw new IOException("write");
        public override void Write(ReadOnlySpan<byte> buffer) => throw new IOException("write");
        public override long Seek(long offset, SeekOrigin origin) => throw new IOException("seek");
        public override void SetLength(long value) => throw new NotSupportedException();
    }
}
