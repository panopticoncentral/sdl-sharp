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

unsafe
{
    using var context = Context.Create(); // Also checks managed/native struct sizes.
    Io.DisplaySize = new Vec2(640, 480);
    Io.SetIniFilename(null);
    Io.BackendFlags |= BackendFlags.RendererHasTextures;
    IGSharp_NewFrame(); // Headless setup: no SDL window or GPU backend.
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
Console.WriteLine($"PASS: managed layout validation, drawing, cloning, frame reuse, disposal, and new exports ({path})");
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
