using System.Runtime.InteropServices;
using SdlSharp.ImGui;
using static SdlSharp.ImGui.Native;

if (args.Length > 1)
    throw new ArgumentException("Pass an optional absolute native library path; omit it to test NuGet resolution.");
var assembly = typeof(Context).Assembly;
var path = args.Length == 1 ? Path.GetFullPath(args[0]) : "NuGet runtime resolution";
var library = args.Length == 1
    ? NativeLibrary.Load(path)
    : NativeLibrary.Load("imgui_sharp", assembly, null);
if (args.Length == 1)
{
    // A local-build test overrides resolution; package tests deliberately do not.
    NativeLibrary.SetDllImportResolver(assembly,
        (name, requestingAssembly, searchPath) => name == "imgui_sharp" ? library : IntPtr.Zero);
}
NativeLibrary.GetExport(library, "IGSharp_DrawList_ResetForNewFrame");
NativeLibrary.GetExport(library, "IGSharp_ImplSDLGPU3_RenderDrawDataWithPipeline");
if (args.Length == 1)
    NativeLibrary.GetExport(library, "IGSharp_GetVersionNumber");

unsafe
{
    using var context = Context.Create(); // Also checks managed/native struct sizes.
    Check(ImGui.GetVersionNumber() > 0, "Native ImGui version number was not exposed");
    Io.DisplaySize = new Vec2(640, 480);
    Io.SetIniFilename("context-a.ini");
    using (var otherContext = Context.Create())
    {
        Check(otherContext.IsCurrent, "Context.Create did not make the new context current");
        Io.SetIniFilename("context-b.ini");
        context.MakeCurrent();
        var contextAFilename = Marshal.PtrToStringUTF8((nint)IGSharp_GetIO()->IniFilename);
        Check(contextAFilename == "context-a.ini",
            $"INI filename storage leaked across contexts: {contextAFilename ?? "<null>"}");
        otherContext.MakeCurrent();
    }
    context.MakeCurrent();
    Io.SetIniFilename(null);

    ExpectArgumentException(() => ImGui.DragFloat4("short", new float[3]));
    ExpectArgumentException(() => ImGui.ColorPicker4("short", new float[4], new float[3]));

    Font font;
    using (var config = new FontConfig())
    {
        config.GlyphRanges = [0x20, 0x7e];
        using (var temporaryContext = Context.Create())
            ImGui.GetFontAtlas().AddDefaultBitmapFont(config);
        context.MakeCurrent();
        font = ImGui.GetFontAtlas().AddDefaultBitmapFont(config);
    }
    Check(font.GetFontBaked(13).FindGlyph('A').IsValid,
        "Disposing FontConfig invalidated atlas-owned glyph ranges");

    using (var filter = new TextFilter("include"))
    {
        Check(filter.Text == "include" && filter.PassFilter("include this"), "TextFilter initial text was not applied");
        filter.Text = "-exclude";
        Check(!filter.PassFilter("exclude this"), "TextFilter text replacement was not rebuilt");
    }

    Io.BackendFlags |= BackendFlags.RendererHasTextures;
    IGSharp_NewFrame(); // Headless setup: no SDL window or GPU backend.
    ImGui.Begin("callback tests");
    ExpectTestException(() => ImGui.PlotLines("throwing plot", _ => throw new TestException(), 1));
    ImGui.End();
    using var owner = StandaloneDrawList.Create();
    var list = owner.List;
    Check(IGSharp_DrawList_GetCmdBufferSize(list.Handle) == 1, "Missing initial draw command");
    Draw(list);
    int vertices = IGSharp_DrawList_GetVtxBufferSize(list.Handle);
    int indices = IGSharp_DrawList_GetIdxBufferSize(list.Handle);
    Check(vertices > 0 && indices > 0, "Drawing produced no geometry");
    using (var clone = list.CloneOutput())
        Check(IGSharp_DrawList_GetVtxBufferSize(clone.List.Handle) == vertices, "Clone lost geometry");
    ImGui.EndFrame();

    IGSharp_NewFrame();
    owner.ResetForNewFrame();
    Check(IGSharp_DrawList_GetVtxBufferSize(owner.List.Handle) == 0, "Reset retained vertices");
    Check(IGSharp_DrawList_GetIdxBufferSize(owner.List.Handle) == 0, "Reset retained indices");
    Draw(owner.List);
    Check(IGSharp_DrawList_GetVtxBufferSize(owner.List.Handle) == vertices, "Reuse changed vertex count");
    Check(IGSharp_DrawList_GetIdxBufferSize(owner.List.Handle) == indices, "Reuse changed index count");
    ImGui.EndFrame();
    owner.Dispose();
    owner.Dispose();
    ExpectDisposed(() => owner.ResetForNewFrame());
    ExpectDisposed(() => { _ = owner.List; });
}
Console.WriteLine($"PASS: layouts, context isolation, span guards, callbacks, font lifetimes, drawing, cloning, reuse, disposal, and exports ({path})");
// Keep the library loaded until process exit: generated P/Invokes cache its entry points.

static void Draw(DrawList list)
{
    list.PushClipRectFullScreen();
    list.PushTexture(1UL);
    list.AddLine(new Vec2(0, 0), new Vec2(100, 100), 0xffffffffu);
}
static void Check(bool ok, string message)
{
    if (!ok) throw new InvalidOperationException(message);
}
static void ExpectDisposed(Action action)
{
    try { action(); }
    catch (ObjectDisposedException) { return; }
    throw new InvalidOperationException("Disposed owner remained usable");
}

static void ExpectArgumentException(Action action)
{
    try { action(); }
    catch (ArgumentException) { return; }
    throw new InvalidOperationException("Invalid span length was accepted");
}

static void ExpectTestException(Action action)
{
    try { action(); }
    catch (TestException) { return; }
    throw new InvalidOperationException("Managed callback exception was not rethrown");
}

sealed class TestException : Exception;
