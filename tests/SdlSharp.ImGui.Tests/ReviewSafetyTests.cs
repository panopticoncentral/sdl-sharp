using System.Buffers.Binary;
using System.Text;
using SdlSharp.Graphics.Gpu;
using SdlSharp.ImGui;
using N = SdlSharp.Native;
using static SdlSharp.ImGui.Native;

internal static unsafe class ReviewSafetyTests
{
    private static int _checks;
    internal static void Run()
    {
        FontInputs(); RangesPlotsAndIds(); StorageScopes(); FontOptions(); GpuOwnership(); RejectedContextDisposal(); InternalWindowStorage();
        Console.WriteLine($"PASS: {_checks} final-review regression checks");
    }
    private static void Check(bool condition, string message)
    { _checks++; if (!condition) throw new Exception(message); }
    private static void Throws<T>(Action action) where T : Exception
    {
        try { action(); } catch (T) { _checks++; return; }
        throw new Exception($"Expected {typeof(T).Name}");
    }
    private static Context Create()
    {
        var context = Context.Create(); Io.SetIniFilename(null); Io.DisplaySize = new(800, 600);
        Io.BackendFlags |= BackendFlags.RendererHasTextures; return context;
    }
    private static byte[] FontFixture()
    {
        using var source = typeof(ReviewSafetyTests).Assembly.GetManifestResourceStream("SdlSharp.ImGui.Tests.Fixtures.ProggyClean.stb")!;
        using var bytes = new MemoryStream(); source.CopyTo(bytes); return bytes.ToArray();
    }
    private static void FontInputs()
    {
        using var context = Create(); var atlas = ImGui.GetFontAtlas();
        var compressed = FontFixture();
        Check(CompressedFont.Decode(compressed).Length == 41208, "Known font decompressed size");
        Check(atlas.AddFontFromMemoryCompressedTTF(compressed, 13).IsLoaded, "Valid compressed font loads");
        var encoded = Encode85(compressed);
        using var config = new FontConfig { GlyphRanges = [32, 126] };
        Check(atlas.AddFontFromMemoryCompressedBase85TTF(encoded, 13, config).IsLoaded, "Valid Base85 font loads with copied configuration");
        Throws<InvalidDataException>(() => atlas.AddFontFromMemoryCompressedTTF([], 13));
        Throws<InvalidDataException>(() => atlas.AddFontFromMemoryCompressedBase85TTF("", 13));
        Throws<ArgumentNullException>(() => atlas.AddFontFromMemoryCompressedBase85TTF(null!, 13));
        foreach (var text in new[] { "abc", "!!!!!", "xxxxx", "##\\##", "##\0##" })
            Throws<InvalidDataException>(() => atlas.AddFontFromMemoryCompressedBase85TTF(text, 13));
        foreach (var length in new[] { 0, 1, 8, 15, 16, 17, 32, compressed.Length - 1 })
            Throws<InvalidDataException>(() => CompressedFont.Decode(compressed.AsSpan(0, length)));
        Throws<InvalidDataException>(() => CompressedFont.Decode(compressed, 100));
        Throws<ArgumentOutOfRangeException>(() => CompressedFont.Decode(compressed, 0));
        var bad = (byte[])compressed.Clone(); bad[^1] ^= 1;
        Throws<InvalidDataException>(() => CompressedFont.Decode(bad));
        bad = (byte[])compressed.Clone(); bad[0] = 0;
        Throws<InvalidDataException>(() => CompressedFont.Decode(bad));
        bad = (byte[])compressed.Clone(); BinaryPrimitives.WriteUInt32BigEndian(bad.AsSpan(8), uint.MaxValue);
        Throws<InvalidDataException>(() => CompressedFont.Decode(bad));
        // Each match encoding must support overlap and reject references before output.
        foreach (var match in new byte[][] { [0x82, 0], [0x40, 0, 2], [0x18, 0, 0, 2], [0x10, 0, 0, 0, 2], [6, 0, 0, 0, 2], [4, 0, 0, 0, 0, 2] })
        {
            Check(CompressedFont.Decode(Stream([0x20, (byte)'A', .. match], "AAAA"u8)).AsSpan().SequenceEqual("AAAA"u8), "Overlapping backreference");
            Throws<InvalidDataException>(() => CompressedFont.Decode(Stream(match, "AAA"u8)));
        }
        foreach (var literal in new byte[][] { [0x22, 65, 66, 67], [8, 2, 65, 66, 67], [7, 0, 2, 65, 66, 67] })
            Check(CompressedFont.Decode(Stream(literal, "ABC"u8)).AsSpan().SequenceEqual("ABC"u8), "Literal token variants");
        Throws<InvalidDataException>(() => CompressedFont.Decode(Stream([0x23, 65, 66, 67, 68], "ABC"u8)));
        Throws<InvalidDataException>(() => CompressedFont.Decode(Stream([3], "ABC"u8)));
        Throws<InvalidOperationException>(() => atlas.AddFontFromMemoryCompressedTTF(Stream([0x22, 65, 66, 67], "ABC"u8), 13));
        Check(atlas.FontCount == 2, "Rejected compressed fonts never reach the atlas");
    }
    private static byte[] Stream(byte[] tokens, ReadOnlySpan<byte> expected)
    {
        var bytes = new byte[16 + tokens.Length + 6];
        BinaryPrimitives.WriteUInt32BigEndian(bytes, 0x57bc0000);
        BinaryPrimitives.WriteUInt32BigEndian(bytes.AsSpan(8), (uint)expected.Length);
        tokens.CopyTo(bytes, 16); bytes[^6] = 5; bytes[^5] = 0xfa;
        uint first = 1, second = 0;
        foreach (var b in expected) { first = (first + b) % 65521; second = (second + first) % 65521; }
        BinaryPrimitives.WriteUInt32BigEndian(bytes.AsSpan(bytes.Length - 4), (second << 16) | first);
        return bytes;
    }
    private static string Encode85(byte[] bytes)
    {
        var padded = new byte[(bytes.Length + 3) / 4 * 4]; bytes.CopyTo(padded, 0);
        var text = new StringBuilder();
        for (var offset = 0; offset < padded.Length; offset += 4)
        {
            var value = BinaryPrimitives.ReadUInt32LittleEndian(padded.AsSpan(offset));
            for (var i = 0; i < 5; i++, value /= 85)
            { var c = (char)(value % 85 + 35); if (c >= '\\') c++; text.Append(c); }
        }
        return text.ToString();
    }
    private static void RangesPlotsAndIds()
    {
        using (var builder = new GlyphRangesBuilder())
        {
            builder.AddRanges([]); Check(builder.Build().Length == 0, "Empty ranges are a no-op");
            builder.Add('A'); builder.AddRanges(default); Check(builder.Build().SequenceEqual(new ushort[] { 65, 65 }), "Empty ranges preserve existing entries");
            Throws<ArgumentException>(() => builder.AddRanges([1]));
            builder.Dispose(); Throws<ObjectDisposedException>(() => builder.AddRanges([]));
        }
        using (var context = Create())
        {
            context.NewFrame();
            ImGui.SetNextWindowSize(600, 400);
            using (ImGui.Window("ranges, plots, IDs"))
            {
                foreach (var offset in new[] { -1, 3, int.MaxValue })
                {
                    Throws<ArgumentOutOfRangeException>(() => ImGui.PlotLines("bad", new float[3], valuesOffset: offset));
                    Throws<ArgumentOutOfRangeException>(() => ImGui.PlotHistogram("bad", new float[3], valuesOffset: offset));
                    Throws<ArgumentOutOfRangeException>(() => ImGui.PlotLines("bad", _ => throw new Exception("Called invalid getter"), 3, valuesOffset: offset));
                    Throws<ArgumentOutOfRangeException>(() => ImGui.PlotHistogram("bad", _ => throw new Exception("Called invalid getter"), 3, valuesOffset: offset));
                }
                Throws<ArgumentOutOfRangeException>(() => ImGui.PlotLines("count", _ => 0, -1));
                Throws<ArgumentOutOfRangeException>(() => ImGui.PlotHistogram("empty", [], valuesOffset: 1));
                ImGui.PlotLines("empty", []); ImGui.PlotHistogram("empty2", []);
                var indices = new List<int>();
                ImGui.PlotLines("valid", i => { indices.Add(i); return i; }, 3, valuesOffset: 2, width: 100, height: 30);
                Check(indices.Count > 0 && indices.All(i => i >= 0 && i < 3), "Valid ring offset remains in bounds");
                Check(ImGui.GetID("é") == ImGui.GetID("é"u8), "UTF-8 IDs remain equivalent");
                Task.Run(() =>
                {
                    Throws<InvalidOperationException>(() => ImGui.GetID("wrong"u8));
                    Throws<InvalidOperationException>(() => ImGui.PushID("wrong"u8));
                }).GetAwaiter().GetResult();
            }
            ImGui.EndFrame();
        }
        Throws<InvalidOperationException>(() => ImGui.GetID("no context"u8));
        Throws<InvalidOperationException>(() => ImGui.PushID("no context"u8));
    }
    private static void StorageScopes()
    {
        using var context = Create(); using var first = new Storage(); using var second = new Storage();
        context.NewFrame();
        using (ImGui.Window("storage"))
        {
            using var original = ImGui.GetStateStorage();
            for (var i = 0; i < 100; i++)
            {
                using (ImGui.UseStateStorage(first))
                {
                    Check(ImGui.GetStateStorage().Handle == first.Handle, "Storage selected");
                    Throws<InvalidOperationException>(first.Dispose);
                }
                Check(first.Lifetime.Retainers == 0 && original.Lifetime.Retainers == 0, "Scope releases replacement and previous storage leases");
            }
            var outer = ImGui.UseStateStorage(first);
            using (var inner = ImGui.UseStateStorage(second))
            {
                Throws<InvalidOperationException>(outer.Dispose);
                Throws<InvalidOperationException>(ImGui.End);
                Throws<InvalidOperationException>(ImGui.EndMainMenuBar);
                Throws<InvalidOperationException>(ImGui.EndMenu);
                Throws<InvalidOperationException>(ImGui.EndFrame);
                Throws<InvalidOperationException>(context.NewFrame);
                Throws<InvalidOperationException>(context.Dispose);
                Task.Run(() => Throws<InvalidOperationException>(inner.Dispose)).GetAwaiter().GetResult();
            }
            Check(ImGui.GetStateStorage().Handle == first.Handle, "Nested storage restores outer value");
            outer.Dispose(); outer.Dispose();
            Check(ImGui.GetStateStorage().Handle == original.Handle, "Outer scope restores window storage");
            first.Dispose(); second.Dispose();
            Throws<ObjectDisposedException>(() => ImGui.UseStateStorage(first));
        }
        ImGui.EndFrame();
        Check(context.Lifetime.Retainers == 0, "Storage scopes do not accumulate context leases");
    }
    private static void FontOptions()
    {
        using var context = Create();
        var font = ImGui.GetFontAtlas().AddDefaultBitmapFont();
        var width = font.CalcTextSize(13, "é").X;
        var size = font.CalcTextSize(13, "éAA", out var chars, width + 0.5f);
        var utf8Size = font.CalcTextSize(13, "éAA"u8, out var bytes, width + 0.5f);
        Check(chars == 1 && bytes == 2 && size == utf8Size, "Consumed offsets use UTF-16 for strings and UTF-8 for bytes");
        font.CalcTextSize(13, "éAA", out chars); Check(chars == 3, "Full measurement consumes all text");
        font.CalcTextSize(13, "", out chars); Check(chars == 0, "Empty measurement returns zero consumed characters");
        context.NewFrame(); using var owner = StandaloneDrawList.Create();
        var list = owner.List; list.PushClipRectFullScreen(); list.PushTexture(ImGui.GetFontAtlas().TextureReference);
        font.RenderCharacter(list, 13, new(0, 0), uint.MaxValue, 'A');
        var original = list.CopyVertices(); Check(original.Length == 4, "Character generates a quad");
        var minX = original.Min(v => v.Position.X); var maxX = original.Max(v => v.Position.X); var midpoint = (minX + maxX) / 2;
        font.RenderCharacter(list, 13, new(0, 0), uint.MaxValue, 'A', new Vec4(midpoint, -100, 100, 100));
        var clipped = list.CopyVertices()[4..];
        Check(clipped.Length == 4 && clipped.All(v => v.Position.X >= midpoint), "Fine clipping clips character geometry");
        Check(clipped.Min(v => v.Uv.X) > original.Min(v => v.Uv.X), "Fine clipping adjusts character UVs");
        font.RenderCharacter(list, 13, new(0, 0), uint.MaxValue, 'A', new Vec4(100, 100, 200, 200));
        Check(list.VtxBufferSize == 8, "Fully clipped character emits no vertices");
        ImGui.EndFrame();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "Synthetic native identities are invalidated explicitly; calling native destruction on them would be invalid.")]
    private static void GpuOwnership()
    {
        using var context = Create(); context.NewFrame();
        using var device = new GpuDevice((N.SDL_GPUDevice*)0x720001, false);
        using var otherDevice = new GpuDevice((N.SDL_GPUDevice*)0x720002, false);
        using var texture = new GpuTexture(device, (N.SDL_GPUTexture*)0x720003, false);
        var textureLife = texture.Lifetime;
        var command = new GpuCommandBuffer(device, (N.SDL_GPUCommandBuffer*)0x720004);
        var otherCommand = new GpuCommandBuffer(device, (N.SDL_GPUCommandBuffer*)0x720005);
        var wrongCommand = new GpuCommandBuffer(otherDevice, (N.SDL_GPUCommandBuffer*)0x720009);
        var pipeline = new GpuGraphicsPipeline(device, (N.SDL_GPUGraphicsPipeline*)0x720006);
        var wrongPipeline = new GpuGraphicsPipeline(otherDevice, (N.SDL_GPUGraphicsPipeline*)0x720007);
        try
        {
            ImGuiBackend.ValidatePreparation(device, command);
            Throws<ArgumentException>(() => ImGuiBackend.ValidatePreparation(otherDevice, command));
            var pass = new GpuRenderPass(command, (N.SDL_GPURenderPass*)0x720008);
            Throws<InvalidOperationException>(() => ImGuiBackend.ValidatePreparation(device, command));
            Throws<ArgumentException>(() => ImGuiBackend.ValidateRendering(device, otherCommand, pass, null));
            Throws<ArgumentException>(() => ImGuiBackend.ValidateRendering(device, command, pass, wrongPipeline));
            ImGuiBackend.ValidateRendering(device, command, pass, pipeline);
            Throws<InvalidOperationException>(pipeline.Lifetime.EnsureCanDestroy);
            command.EndPass(pass);
            Throws<InvalidOperationException>(() => ImGuiBackend.ValidateRendering(device, command, pass, null));

            using var owner = StandaloneDrawList.Create();
            owner.List.PushClipRectFullScreen(); owner.List.AddImage(texture, new(0, 0), new(10, 10));
            var metadata = owner.List.GetCmd(0);
            Throws<InvalidOperationException>(textureLife.EnsureCanDestroy);
            using var clone = owner.List.CloneOutput();
            using var transformed = clone.Transform(new(2, 2));
            texture.Dispose(); // Borrowed alias disposal does not end the native identity.
            Check(metadata.TextureId == 0x720003 && transformed.GetCommand(0).TextureId == 0x720003, "Typed metadata preserves native identity after alias disposal");
            owner.Dispose(); clone.Dispose();
            Throws<InvalidOperationException>(textureLife.EnsureCanDestroy);
            using var data = new OwnedDrawData(); data.Add(transformed);
            ImGuiBackend.RetainTextures(data.Data, command);
            var backendLife = new Lifetime(0, context, context.Lifetime);
            command.RetainDependency(backendLife, backendLife.Retain);
            command.RetainDependency(backendLife, backendLife.Retain);
            Check(backendLife.Retainers == 1, "Command deduplicates backend retention");
            Throws<InvalidOperationException>(backendLife.Dispose);
            Throws<ArgumentException>(() => ImGuiBackend.RetainTextures(data.Data, wrongCommand));
            data.Clear(); transformed.Dispose();
            Throws<InvalidOperationException>(textureLife.EnsureCanDestroy);
            command.Complete(); command.Dispose();
            textureLife.EnsureCanDestroy(); pipeline.Lifetime.EnsureCanDestroy();
            Check(backendLife.Retainers == 0, "Command completion releases backend retention"); backendLife.Dispose();
            Check(true, "Submission retains textures and pipeline until command completion");
            textureLife.Invalidate(); Throws<ObjectDisposedException>(() => _ = metadata.TextureId);
            using var swapchain = new GpuTexture(device, (N.SDL_GPUTexture*)0x720010, false) { OwnerCommand = otherCommand };
            Throws<ArgumentException>(() => { TextureReference reference = swapchain; });
            swapchain.Lifetime.Invalidate();
            using var frameTexture = new GpuTexture(device, (N.SDL_GPUTexture*)0x720011, false);
            using (ImGui.Window("frame texture")) ImGui.Image(frameTexture, 10, 10);
            Throws<InvalidOperationException>(frameTexture.Lifetime.EnsureCanDestroy);
            ImGui.EndFrame(); context.NewFrame(); frameTexture.Lifetime.EnsureCanDestroy();
            Check(true, "Frame cleanup releases typed texture leases");
            frameTexture.Lifetime.Invalidate(); ImGui.EndFrame();
        }
        finally
        {
            command.Complete(); otherCommand.Complete(); wrongCommand.Complete();
            pipeline.Lifetime.Invalidate(); wrongPipeline.Lifetime.Invalidate();
            pipeline.Dispose(); wrongPipeline.Dispose();
            device.Lifetime.Invalidate(); otherDevice.Lifetime.Invalidate();
        }
    }
    private static void RejectedContextDisposal()
    {
        using var context = Create(); using var storage = new Storage();
        using var cpu = new OwnedTextureData(TextureFormat.RGBA32, 2, 2);
        var cpuView = cpu.Texture;
        using var device = new GpuDevice((N.SDL_GPUDevice*)0x740001, false);
        using var texture = new GpuTexture(device, (N.SDL_GPUTexture*)0x740002, false);
        context.NewFrame(); ImGui.SetNextWindowSize(500, 400);
        ImGui.Begin("rejected disposal"); ImGui.Image(texture, 20, 20);
        var list = ImGui.GetWindowDrawList();
        list.AddImage(cpu.Texture.Reference, new(0, 0), new(2, 2));
        var callbacks = 0; list.AddCallback((_, _) => callbacks++);
        using (ImGui.UseStateStorage(storage))
        {
            Throws<InvalidOperationException>(context.Dispose);
            Check(!context.IsDisposed && cpuView.IsValid, "Rejected disposal preserves context and CPU texture");
            Throws<InvalidOperationException>(texture.Lifetime.EnsureCanDestroy);
            Throws<InvalidOperationException>(cpu.Dispose);
            for (var i = 0; i < list.CmdBufferSize; i++) list.TryInvokeCallback(i);
            Check(callbacks == 1, "Rejected disposal preserves frame callback registration");
        }
        ImGui.End(); ImGui.Render();
        // An external submission can retain a descendant even when the context
        // itself has no direct retainers. Teardown must remain all-or-nothing.
        var backend = new Lifetime(0, context, context.Lifetime);
        var child = new Lifetime(0, context, backend);
        var command = new GpuCommandBuffer(device, (N.SDL_GPUCommandBuffer*)0x740003);
        command.RetainDependency(child, child.Retain);
        try
        {
            Throws<InvalidOperationException>(context.Dispose);
            Check(backend.Alive && child.Alive && cpuView.IsValid, "External descendant lease blocks teardown before any child is destroyed");
            Throws<InvalidOperationException>(texture.Lifetime.EnsureCanDestroy);
            Throws<InvalidOperationException>(cpu.Dispose);
            for (var i = 0; i < list.CmdBufferSize; i++) list.TryInvokeCallback(i);
            Check(callbacks == 2, "External lease rejection preserves callbacks");
        }
        finally { command.Complete(); command.Dispose(); }
        using var clone = list.CloneOutput();
        using var data = new OwnedDrawData(); data.Add(clone);
        using var textures = new TextureList(); textures.Add(cpu.Texture); data.Textures = textures;
        context.Dispose();
        Check(context.IsDisposed && !cpuView.IsValid, "Context tears down internal containers, snapshots and textures in dependency order");
        texture.Lifetime.EnsureCanDestroy();
        texture.Lifetime.Invalidate(); device.Lifetime.Invalidate();
    }
    private static void InternalWindowStorage()
    {
        using var context = Create(); using var storage = new Storage();
        context.NewFrame(); ImGui.SetNextWindowSize(600, 500);
        using (ImGui.Window("internal storage windows"))
        {
            using var parentStorage = ImGui.UseStateStorage(storage);
            foreach (var flags in new[] { TableFlags.ScrollX, TableFlags.ScrollY })
            {
                Check(ImGui.BeginTable(flags.ToString(), 1, flags, 250, 80), "Scrolling table is active");
                using var replacement = new Storage();
                var scope = ImGui.UseStateStorage(replacement);
                var selected = ImGui.GetStateStorage().Handle;
                ImGui.TableNextRow(); ImGui.TableNextColumn();
                Check(ImGui.BeginTable("nested plain", 1), "Nested table is active");
                ImGui.EndTable();
                Throws<InvalidOperationException>(ImGui.EndTable);
                Check(ImGui.GetStateStorage().Handle == selected, "Rejected table end leaves the storage and window unchanged");
                scope.Dispose(); ImGui.EndTable();
                Check(ImGui.GetStateStorage().Handle == storage.Handle, "Closing a scrolling table restores its parent window");
            }
            // No window transition: a parent storage scope may span ordinary tables.
            Check(ImGui.BeginTable("plain", 1), "Non-scrolling table is active");
            using (var inner = ImGui.UseStateStorage(storage)) ImGui.EndTable();
            Check(ImGui.GetStateStorage().Handle == storage.Handle, "Ordinary table end permits storage scopes on its host window");
            Check(ImGui.BeginDragDropSource(DragDropFlags.SourceExtern), "External drag source opens a preview tooltip");
            using (var tooltipStorage = ImGui.UseStateStorage(storage))
            {
                Throws<InvalidOperationException>(ImGui.EndDragDropSource);
                Check(ImGui.GetStateStorage().Handle == storage.Handle, "Rejected drag end preserves the tooltip storage");
                // Leave the payload unset so this test drag ends before changing source flags.
            }
            ImGui.EndDragDropSource();
            Check(ImGui.GetStateStorage().Handle == storage.Handle, "Drag end returns to its parent window");
            Check(ImGui.BeginDragDropSource(DragDropFlags.SourceExtern | DragDropFlags.SourceNoPreviewTooltip), "Drag source without tooltip is active");
            using (var hostStorage = ImGui.UseStateStorage(storage)) ImGui.EndDragDropSource();
            Check(context.StorageScopes.Count == 1, "Only the parent storage scope remains");
        }
        ImGui.EndFrame(); context.NewFrame(); ImGui.EndFrame();
        Check(storage.Lifetime.Retainers == 0 && context.Lifetime.Retainers == 0, "Internal-window guards allow recovery and release all leases");
    }

}
