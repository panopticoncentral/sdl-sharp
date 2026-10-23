using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SdlSharp.ImGui;
using static SdlSharp.ImGui.Native;

internal static unsafe class HighLevelTests
{
    private static int _checks;
    private static int _nativeSizeCalls;
    internal static void Run()
    {
        WidgetsAndScopes();
        for (var variant = 0; variant < 6; variant++) TextEditing(variant);
        LifetimesAndSnapshots();
        SharedFontsAndMetadata();
        ResourcesAndRegistrations();
        PlatformLifetimes();
        Console.WriteLine($"PASS: {_checks} high-level regression checks");
    }
    private static Context Create()
    {
        var context = Context.Create(); Io.DisplaySize = new(800, 600); Io.SetIniFilename(null);
        Io.BackendFlags |= BackendFlags.RendererHasTextures; return context;
    }
    private static void Check(bool value, string message) { _checks++; if (!value) throw new Exception(message); }
    private static void Throws<T>(Action action) where T : Exception
    {
        try { action(); } catch (T) { _checks++; return; }
        throw new Exception($"Expected {typeof(T).Name}");
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void SizeCallback(IGSharp_SizeCallbackData* data) => _nativeSizeCalls++;
    private static void WidgetsAndScopes()
    {
        using var context = Create(); context.NewFrame();
        using (var window = ImGui.Window("Numeric defaults"))
        {
            float f = 0; int i = 0; double d = 0;
            ImGui.DragFloat("float", ref f); ImGui.DragInt("int", ref i);
            ImGui.InputDouble("double", ref d); ImGui.SliderAngle("angle", ref f);
            ImGui.InputFloat("input", ref f); ImGui.SliderFloat("slider", ref f, 0, 1);
            Check(true, "Default numeric format threw");
            Throws<ArgumentException>(() => ImGui.InputText("empty", Span<byte>.Empty));
            Throws<ArgumentException>(() => ImGui.InputText("unterminated", new byte[] { 1, 2 }));
            Throws<ArgumentException>(() => ImGui.InputText("resize", new byte[10], InputTextFlags.CallbackResize));
            Throws<ArgumentNullException>(() => ImGui.InputText("callback", new byte[10], InputTextFlags.CallbackAlways, null!));
            using var id = ImGui.IdScope("outer");
            Check(ImGui.GetID("child") == ImGui.GetID("child"u8), "UTF-8 ID differs");
            using var table = ImGui.Table("table", 2);
            if (table.Visible) { ImGui.TableNextRow(); ImGui.TableNextColumn(); ImGui.Text("cell"); }
        }
        var calls = 0;
        ImGui.SetNextWindowSizeConstraints(new(0, 0), new(500, 500), (Vec2 p, Vec2 c, ref Vec2 size) => calls++);
        using (ImGui.Window("managed size", WindowFlags.AlwaysAutoResize)) ImGui.Text("hello");
        _nativeSizeCalls = 0;
        IGSharp_SetNextWindowSizeConstraints(new(0, 0), new(500, 500), &SizeCallback, null);
        using (ImGui.Window("native size", WindowFlags.AlwaysAutoResize)) ImGui.Text("hello");
        var gate = new FrameGate(); Check(gate.TryEnter() && !gate.TryEnter(), "Frame gate admitted a frame twice");
        Check(calls > 1 && calls == _nativeSizeCalls, "Size callback was removed after its first invocation");
        ImGui.Render();
        var frameData = ImGui.GetDrawData(); var textureReference = ImGui.GetFontAtlas().TextureReference; var baked = ImGui.GetFontBaked(); context.NewFrame();
        Throws<InvalidOperationException>(() => { _ = textureReference.Id; });
        Throws<InvalidOperationException>(() => { _ = baked.Size; });
        Check(gate.TryEnter(), "Frame gate did not admit the next frame");
        Throws<InvalidOperationException>(() => { _ = frameData.TotalVtxCount; }); ImGui.EndFrame();
        using var filter = new TextFilter("é,-no");
        Check(filter.PassFilter("é") && !filter.PassFilter("é no"), "UTF-8 filter mismatch");
        Throws<ArgumentException>(() => filter.Text = new string('é', 128));
        Check(filter.Text == "é,-no", "Rejected filter mutation changed text");
        var style = StyleSnapshot.Capture(); var alpha = Style.Alpha; style.Alpha = 0.3f;
        Check(Style.Alpha == alpha, "Independent style mutated context"); style.Apply();
        Check(Style.Alpha == 0.3f, "Style snapshot failed to apply");
    }
    private static void TextEditing(int variant)
    {
        using var context = Create(); string text = "start";
        using var buffer = new TextBuffer("start", 6);
        var result = true;
        for (var frame = 0; frame < 5; frame++)
        {
            if (frame == 2) Io.AddInputCharactersUTF8("héllo longer text");
            context.NewFrame(); using (ImGui.Window("Editor"))
            {
                if (frame == 0) ImGui.SetKeyboardFocusHere();
                result = variant switch
                {
                    0 => ImGui.InputText("edit", ref text, InputTextFlags.EnterReturnsTrue),
                    1 => ImGui.InputTextMultiline("edit", ref text, 300, 100, InputTextFlags.EnterReturnsTrue),
                    2 => ImGui.InputTextWithHint("edit", "hint", ref text, InputTextFlags.EnterReturnsTrue),
                    3 => ImGui.InputText("edit", buffer, InputTextFlags.EnterReturnsTrue),
                    4 => ImGui.InputTextMultiline("edit", buffer, 300, 100, InputTextFlags.EnterReturnsTrue),
                    _ => ImGui.InputTextWithHint("edit", "hint", buffer, InputTextFlags.EnterReturnsTrue)
                };
            }
            ImGui.EndFrame();
        }
        Check(!result, "EnterReturnsTrue unexpectedly reported submission");
        Check((variant < 3 ? text : buffer.Text).Contains("héllo longer text"), $"Editing variant {variant} lost text when native returned false");
        if (variant >= 3) Check(buffer.Capacity > 6, "Reusable buffer did not grow");
    }
    private static void LifetimesAndSnapshots()
    {
        using var context = Create(); context.NewFrame();
        var first = 0; var second = 0;
        var owner = StandaloneDrawList.Create(); var list = owner.List;
        list.PushClipRectFullScreen(); list.PushTexture(1UL); list.AddCallback((_, _) => first++);
        list.AddLine(new(0, 0), new(100, 100), 0xffffffff);
        using var copy = list.CloneOutput(); var vertices = copy.CopyVertices();
        owner.Dispose(); Check(!list.IsValid, "Disposed draw view reports valid");
        Throws<ObjectDisposedException>(() => list.AddLine(new(), new(), 0));
        ImGui.EndFrame(); context.NewFrame();
        using var replacement = StandaloneDrawList.Create(); replacement.List.AddCallback((_, _) => second++);
        using (var callbackOnly = replacement.List.CloneOutput())
        using (var callbackSubmission = new OwnedDrawData())
        {
            var count = callbackOnly.CommandCount; callbackSubmission.Add(callbackOnly);
            Check(callbackOnly.CommandCount == count, "Submitting callback-only output mutated the snapshot");
        }
        Check(copy.TryInvokeCallback(0), "Snapshot callback was not invokable through the managed API");
        Check(first == 1 && second == 0, "Snapshot invoked a replacement callback");
        Check(copy.CopyVertices().SequenceEqual(vertices), "Snapshot geometry changed across frames");
        var commandsBeforeSubmission = copy.CommandCount;
        using var submission = new OwnedDrawData { DisplaySize = new(800, 600), FramebufferScale = new(1, 1), Valid = true };
        submission.Add(copy); Check(copy.CommandCount == commandsBeforeSubmission, "Submitting a snapshot changed its commands"); Check(submission.Data.CmdListsCount == 1, "Owned draw-data lost its list");
        Throws<InvalidOperationException>(copy.Dispose);
        Throws<InvalidOperationException>(() => submission.Data.GetCmdList(0).AddDrawCmd());
        var stale = submission.Data; submission.Clear(); Throws<InvalidOperationException>(() => { _ = stale.CmdListsCount; });
        copy.Dispose(); Throws<ObjectDisposedException>(() => copy.CopyVertices());
        ImGui.EndFrame();
        var retainedView = replacement.List;
        using var other = Create();
        Throws<InvalidOperationException>(() => replacement.List.AddDrawCmd());
        using (context.Enter()) Check(context.IsCurrent, "Enter did not activate context");
        Check(other.IsCurrent, "Enter did not restore previous context");
        Exception? failure = null;
        var thread = new Thread(() => { try { context.MakeCurrent(); } catch (Exception ex) { failure = ex; } }); thread.Start(); thread.Join();
        Check(failure is InvalidOperationException, "Cross-thread context access was accepted");
        context.MakeCurrent();
        using (context.Enter()) context.Dispose();
        Check(!retainedView.IsValid, "Context disposal did not invalidate a retained standalone view");
        Throws<InvalidOperationException>(() => ImGui.Text("no context"));
    }
    private static void SharedFontsAndMetadata()
    {
        using var atlas = new OwnedFontAtlas();
        using var first = Context.Create(atlas); Io.SetIniFilename(null);
        var font = atlas.Atlas.AddDefaultBitmapFont();
        var glyph = font.GetFontBaked(13).FindGlyph('A');
        Check(glyph.IsValid && glyph.Codepoint == 'A', "Glyph snapshot is invalid");
        using var second = Context.Create(atlas); Io.SetIniFilename(null);
        Check(atlas.Atlas.FontCount == 1 && font.IsLoaded, "Shared atlas cannot be used from another context");
        Throws<InvalidOperationException>(atlas.Dispose);
        first.Dispose(); Check(second.IsCurrent, "Disposing non-current context failed to restore current context");
        second.Dispose(); atlas.Atlas.RemoveFont(font);
        Check(!font.IsValid && glyph.Codepoint == 'A', "Removed font view or copied glyph has incorrect lifetime");
        Throws<ObjectDisposedException>(() => { _ = font.DebugName; });
        Throws<ArgumentOutOfRangeException>(() => atlas.Atlas.GetFont(0));
        using var config = new FontConfig { GlyphRanges = new ushort[] { 0x20, 0x7e } };
        for (var i = 0; i < 3; i++) Throws<InvalidOperationException>(() => atlas.Atlas.AddFontFromMemoryTTF(new byte[4], 13, config));
        Check(atlas.Atlas.FontCount == 0, "Failed loads added fonts");
        using var builder = new GlyphRangesBuilder(); builder.AddText("ABCé");
        Check(builder.Contains('é') && !builder.Contains('Z'), "Glyph range builder has wrong members");
        Check(builder.Build().SequenceEqual(new ushort[] { 65, 67, 233, 233 }), "Glyph range builder did not merge adjacent characters");
    }
    private static void PlatformLifetimes()
    {
        Check(SdlSharp.SdlHints.Set("SDL_VIDEO_DRIVER", "dummy", SdlSharp.HintPriority.Override), "Could not select dummy video driver");
        using var application = new SdlSharp.Application(SdlSharp.InitFlags.Video);
        using var context = Create();
        using var window = SdlSharp.Graphics.Window.Create("headless ownership test", 320, 240, SdlSharp.Graphics.WindowFlags.Hidden);
        using var alias = SdlSharp.Graphics.Window.FromId(window.Id)!;
        Throws<ArgumentException>(() => SdlPlatformBackend.Init(alias));
        using var backend = SdlPlatformBackend.Init(window);
        Throws<InvalidOperationException>(window.Dispose);
        using var other = Create();
        Throws<InvalidOperationException>(backend.NewFrame);
        using (context.Enter())
        {
            backend.NewFrame(); using (ImGui.Window("platform")) ImGui.Text("headless"); ImGui.EndFrame();
        }
        backend.Dispose(); Check(other.IsCurrent, "Backend shutdown did not restore the current context");
        window.Dispose();
    }
    private static void ResourcesAndRegistrations()
    {
        using var context = Create();
        var texture = new OwnedTextureData(TextureFormat.RGBA32, 4, 3); var view = texture.Texture;
        texture.Pixels.Fill(123); Check(view.CopyPixels().Length == 48 && view.CopyPixels()[0] == 123, "CPU texture data mismatch");
        using var textures = new TextureList(); textures.Add(view);
        using var data = new OwnedDrawData { Textures = textures };
        Throws<InvalidOperationException>(texture.Dispose); Throws<InvalidOperationException>(textures.Clear);
        data.Textures = null; textures.Clear(); texture.Dispose(); Check(!view.IsValid, "Disposed texture remains valid");
        Throws<ObjectDisposedException>(() => view.CopyPixels());
        var pio = IGSharp_GetPlatformIO(); var get = IGSharp_PlatformIO_GetPlatformGetClipboardTextFn(pio);
        var set = IGSharp_PlatformIO_GetPlatformSetClipboardTextFn(pio);
        using (PlatformIO.SetClipboardHandlers(() => "outer", _ => { }))
        {
            Check(ImGui.GetClipboardText() == "outer", "Clipboard override did not run");
            using (PlatformIO.SetClipboardHandlers(() => "inner", _ => { })) Check(ImGui.GetClipboardText() == "inner", "Nested clipboard override failed");
            Check(ImGui.GetClipboardText() == "outer", "Nested clipboard override did not restore prior delegate");
        }
        Check((nint)get == (nint)IGSharp_PlatformIO_GetPlatformGetClipboardTextFn(pio) && (nint)set == (nint)IGSharp_PlatformIO_GetPlatformSetClipboardTextFn(pio), "Platform handlers were not restored");
        using (PlatformIO.SetClipboardHandlers(() => throw new TestException(), null))
        {
            _ = ImGui.GetClipboardText(); Throws<AggregateException>(ImGui.ThrowPendingCallbackExceptions);
        }
        using (PlatformIO.SetClipboardHandlers(() => { context.Dispose(); return "invalid"; }, null))
        { _ = ImGui.GetClipboardText(); Throws<AggregateException>(ImGui.ThrowPendingCallbackExceptions); Check(!context.IsDisposed, "A callback disposed its context"); }
        var outer = PlatformIO.SetOpenInShellHandler(_ => true); var inner = PlatformIO.SetOpenInShellHandler(_ => false);
        Throws<InvalidOperationException>(outer.Dispose); inner.Dispose(); outer.Dispose();
        using var selection = new SelectionBasicStorage(); using var other = new SelectionBasicStorage();
        selection.SetIndexToStorageIdAdapter(index => (uint)index + 100); other.SetIndexToStorageIdAdapter(index => (uint)index + 200);
        selection.SetItemSelected(10, true); other.SetItemSelected(20, true); var selectedSnapshot = selection.SelectedItems; selection.Swap(other);
        Check(selectedSnapshot.SequenceEqual(new uint[] { 10 }), "Selection enumeration was not a value snapshot");
        Check(selection.GetStorageIdFromIndex(1) == 101 && other.GetStorageIdFromIndex(1) == 201, "Selection swap moved owner adapters");
        Check(selection.Contains(20) && other.Contains(10), "Selection swap failed");
        context.NewFrame();
        using (var owned = new OwnedTextureData(TextureFormat.RGBA32, 2, 2))
        using (var listOwner = StandaloneDrawList.Create())
        {
            var list = listOwner.List; list.PushClipRectFullScreen(); list.AddImage(owned.Texture.Reference, new(0, 0), new(2, 2));
            using var snapshot = list.CloneOutput();
            Check(owned.Texture.IsRetainedForRendering, "Snapshot did not retain its texture");
            Throws<InvalidOperationException>(owned.Dispose);
            var retainedTexture = owned.Texture; retainedTexture.Status = TextureStatus.WantDestroy;
            using (TextureRetention.DeferDestruction()) Check(owned.Texture.Status == TextureStatus.OK, "Renderer destruction was not deferred");
            Check(owned.Texture.Status == TextureStatus.WantDestroy, "Deferred destruction request was lost");
            snapshot.Dispose(); Check(owned.Texture.IsRetainedForRendering, "The source draw list lost its texture lease");
            listOwner.Dispose(); Check(!owned.Texture.IsRetainedForRendering, "Draw output leaked its texture lease");
        }
        ImGui.EndFrame();
        context.NewFrame();
        using var frameTexture = new OwnedTextureData(TextureFormat.Alpha8, 1, 1);
        using (ImGui.Window("owned image")) ImGui.Image(frameTexture.Texture.Reference, 10, 10);
        Throws<InvalidOperationException>(frameTexture.Dispose);
        ImGui.EndFrame(); context.NewFrame(); frameTexture.Dispose(); ImGui.EndFrame();
        var child = new OwnedTextureData(TextureFormat.Alpha8, 1, 1); var childView = child.Texture;
        context.NewFrame(); using (ImGui.Window("retained teardown")) ImGui.Image(child.Texture.Reference, 1, 1); ImGui.EndFrame();
        context.Dispose(); Check(!childView.IsValid, "Context did not dispose its owned children"); child.Dispose();
    }
}
