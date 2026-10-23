using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SdlSharp.Graphics.Gpu;
using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A handle to an ImGui draw list — used for custom drawing (lines, shapes, text, images)
/// on the current window, or on the full-viewport background / foreground overlays.
/// Obtain via <see cref="ImGui.GetWindowDrawList"/>, <see cref="ImGui.GetBackgroundDrawList"/>,
/// or <see cref="ImGui.GetForegroundDrawList"/>. The handle is only valid for the current frame.
/// </summary>
public readonly unsafe struct DrawList
{
    private readonly NativeView<IGSharp_DrawList> _view;
    internal IGSharp_DrawList* MutableHandle
    {
        get { var pointer = _view.Handle; if (Lifetime.ReadOnly) throw new InvalidOperationException("Render snapshots cannot be edited."); return pointer; }
    }
    internal IGSharp_DrawList* Handle => _view.Handle;
    internal Lifetime Lifetime => _view.Lifetime!;

    internal DrawList(IGSharp_DrawList* handle, Lifetime? lifetime = null, bool? frame = null) => _view = new(handle, lifetime, frame ?? true);

    /// <summary>True if this handle refers to a valid draw list.</summary>
    public bool IsValid => _view.IsValid;

    // --- Clipping ---

    /// <summary>Pushes a clipping rectangle. Drawing outside the rect will be clipped.</summary>
    public void PushClipRect(Vec2 min, Vec2 max, bool intersectWithCurrent = false)
        => IGSharp_DrawList_PushClipRect(MutableHandle, Reinterpret(min), Reinterpret(max), intersectWithCurrent);

    /// <summary>Pushes a full-screen clipping rectangle.</summary>
    public void PushClipRectFullScreen() => IGSharp_DrawList_PushClipRectFullScreen(MutableHandle);

    /// <summary>Pops the last clipping rectangle.</summary>
    public void PopClipRect() => IGSharp_DrawList_PopClipRect(MutableHandle);

    /// <summary>Top-left corner of the current clipping rectangle.</summary>
    public Vec2 ClipRectMin
    {
        get
        {
            var v = IGSharp_DrawList_GetClipRectMin(Handle);
            return new Vec2(v.X, v.Y);
        }
    }

    /// <summary>Bottom-right corner of the current clipping rectangle.</summary>
    public Vec2 ClipRectMax
    {
        get
        {
            var v = IGSharp_DrawList_GetClipRectMax(Handle);
            return new Vec2(v.X, v.Y);
        }
    }

    // --- Texture stack ---

    /// <summary>Pushes a texture onto the texture stack — subsequent primitives sample from it. <paramref name="textureId"/> is backend-specific (SDL_GPU: SDL_GPUTexture*).</summary>
    public void PushTexture(TextureReference textureId) => IGSharp_DrawList_PushTexture(MutableHandle, KeepTexture(textureId));

    /// <summary>Pushes an SDL_GPU texture onto the texture stack.</summary>
    public void PushTexture(GpuTexture texture) => PushTexture((TextureReference)texture);

    /// <summary>Pops the last texture off the texture stack.</summary>
    public void PopTexture() => IGSharp_DrawList_PopTexture(MutableHandle);

    // --- Flags ---

    /// <summary>Rendering flags for this draw list (anti-aliasing, vertex offsets). Initialized each frame from the ImGui style.</summary>
    public DrawFlags Flags
    {
        get => (DrawFlags)IGSharp_DrawList_GetFlags(Handle);
        set => IGSharp_DrawList_SetFlags(MutableHandle, (int)value);
    }

    // --- Primitives ---

    /// <summary>Draws a line between two points.</summary>
    public void AddLine(Vec2 p1, Vec2 p2, uint col, float thickness = 1f, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddLine(MutableHandle, Reinterpret(p1), Reinterpret(p2), col, thickness, (int)flags);

    /// <summary>Draws a rectangle outline.</summary>
    public void AddRect(Vec2 min, Vec2 max, uint col, float rounding = 0f, float thickness = 1f, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddRect(MutableHandle, Reinterpret(min), Reinterpret(max), col, rounding, thickness, (int)flags);

    /// <summary>Draws a filled rectangle.</summary>
    public void AddRectFilled(Vec2 min, Vec2 max, uint col, float rounding = 0f, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddRectFilled(MutableHandle, Reinterpret(min), Reinterpret(max), col, rounding, (int)flags);

    /// <summary>Draws a filled rectangle with per-corner colors (gradient).</summary>
    public void AddRectFilledMultiColor(Vec2 min, Vec2 max, uint colUpperLeft, uint colUpperRight, uint colBottomRight, uint colBottomLeft)
        => IGSharp_DrawList_AddRectFilledMultiColor(MutableHandle, Reinterpret(min), Reinterpret(max), colUpperLeft, colUpperRight, colBottomRight, colBottomLeft);

    /// <summary>Draws a quadrilateral outline (4 points in order).</summary>
    public void AddQuad(Vec2 p1, Vec2 p2, Vec2 p3, Vec2 p4, uint col, float thickness = 1f, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddQuad(MutableHandle, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), Reinterpret(p4), col, thickness, (int)flags);

    /// <summary>Draws a filled quadrilateral.</summary>
    public void AddQuadFilled(Vec2 p1, Vec2 p2, Vec2 p3, Vec2 p4, uint col)
        => IGSharp_DrawList_AddQuadFilled(MutableHandle, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), Reinterpret(p4), col);

    /// <summary>Draws a triangle outline.</summary>
    public void AddTriangle(Vec2 p1, Vec2 p2, Vec2 p3, uint col, float thickness = 1f, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddTriangle(MutableHandle, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), col, thickness, (int)flags);

    /// <summary>Draws a filled triangle.</summary>
    public void AddTriangleFilled(Vec2 p1, Vec2 p2, Vec2 p3, uint col)
        => IGSharp_DrawList_AddTriangleFilled(MutableHandle, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), col);

    /// <summary>Draws a circle outline. Pass 0 for <paramref name="numSegments"/> to auto-tessellate.</summary>
    public void AddCircle(Vec2 center, float radius, uint col, int numSegments = 0, float thickness = 1f, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddCircle(MutableHandle, Reinterpret(center), radius, col, numSegments, thickness, (int)flags);

    /// <summary>Draws a filled circle.</summary>
    public void AddCircleFilled(Vec2 center, float radius, uint col, int numSegments = 0)
        => IGSharp_DrawList_AddCircleFilled(MutableHandle, Reinterpret(center), radius, col, numSegments);

    /// <summary>Draws an n-gon outline with the given number of segments.</summary>
    public void AddNgon(Vec2 center, float radius, uint col, int numSegments, float thickness = 1f, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddNgon(MutableHandle, Reinterpret(center), radius, col, numSegments, thickness, (int)flags);

    /// <summary>Draws a filled n-gon.</summary>
    public void AddNgonFilled(Vec2 center, float radius, uint col, int numSegments)
        => IGSharp_DrawList_AddNgonFilled(MutableHandle, Reinterpret(center), radius, col, numSegments);

    /// <summary>Draws an ellipse outline.</summary>
    public void AddEllipse(Vec2 center, Vec2 radius, uint col, float rotation = 0f, int numSegments = 0, float thickness = 1f, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddEllipse(MutableHandle, Reinterpret(center), Reinterpret(radius), col, rotation, numSegments, thickness, (int)flags);

    /// <summary>Draws a filled ellipse.</summary>
    public void AddEllipseFilled(Vec2 center, Vec2 radius, uint col, float rotation = 0f, int numSegments = 0)
        => IGSharp_DrawList_AddEllipseFilled(MutableHandle, Reinterpret(center), Reinterpret(radius), col, rotation, numSegments);

    /// <summary>Draws text at the given position using the current font.</summary>
    public void AddText(Vec2 pos, uint col, string text)
        => IGSharp_DrawList_AddText(MutableHandle, Reinterpret(pos), col, ToUtf8Range(text));

    /// <summary>
    /// Draws text with an explicit font and size. Pass 0 for <paramref name="wrapWidth"/> to disable wrapping.
    /// </summary>
    public void AddText(Font font, float fontSize, Vec2 pos, uint col, string text, float wrapWidth = 0f)
        => IGSharp_DrawList_AddTextFont(MutableHandle, (IGSharp_Font*)font.Handle, fontSize, Reinterpret(pos), col, ToUtf8Range(text), wrapWidth, null);

    /// <summary>
    /// Draws text with an explicit font, size, and a fine (exact) CPU-side clip rectangle (minX, minY, maxX, maxY).
    /// </summary>
    public void AddText(Font font, float fontSize, Vec2 pos, uint col, string text, float wrapWidth, Vec4 cpuFineClipRect)
    {
        var clip = new IGSharp_Vec4(cpuFineClipRect.X, cpuFineClipRect.Y, cpuFineClipRect.Z, cpuFineClipRect.W);
        IGSharp_DrawList_AddTextFont(MutableHandle, (IGSharp_Font*)font.Handle, fontSize, Reinterpret(pos), col, ToUtf8Range(text), wrapWidth, &clip);
    }

    /// <summary>Draws a cubic Bezier curve.</summary>
    public void AddBezierCubic(Vec2 p1, Vec2 p2, Vec2 p3, Vec2 p4, uint col, float thickness = 1f, int numSegments = 0, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddBezierCubic(MutableHandle, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), Reinterpret(p4), col, thickness, numSegments, (int)flags);

    /// <summary>Draws a quadratic Bezier curve.</summary>
    public void AddBezierQuadratic(Vec2 p1, Vec2 p2, Vec2 p3, uint col, float thickness = 1f, int numSegments = 0, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddBezierQuadratic(MutableHandle, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), col, thickness, numSegments, (int)flags);

    /// <summary>Draws a connected series of line segments.</summary>
    public void AddPolyline(ReadOnlySpan<Vec2> points, uint col, float thickness = 1f, DrawFlags flags = DrawFlags.None)
    {
        fixed (Vec2* p = points)
            IGSharp_DrawList_AddPolyline(MutableHandle, (IGSharp_Vec2*)p, points.Length, col, thickness, (int)flags);
    }

    /// <summary>Fills a convex polygon.</summary>
    public void AddConvexPolyFilled(ReadOnlySpan<Vec2> points, uint col, DrawFlags flags = DrawFlags.None)
    {
        fixed (Vec2* p = points)
            IGSharp_DrawList_AddConvexPolyFilled(MutableHandle, (IGSharp_Vec2*)p, points.Length, col, (int)flags);
    }

    /// <summary>Fills a concave polygon (slower than convex).</summary>
    public void AddConcavePolyFilled(ReadOnlySpan<Vec2> points, uint col)
    {
        fixed (Vec2* p = points)
            IGSharp_DrawList_AddConcavePolyFilled(MutableHandle, (IGSharp_Vec2*)p, points.Length, col);
    }

    // --- Images ---

    /// <summary>Draws an axis-aligned textured rectangle using the full texture.</summary>
    public void AddImage(TextureReference textureId, Vec2 min, Vec2 max, uint col = 0xFFFFFFFFu)
        => AddImage(textureId, min, max, default, new Vec2(1, 1), col);

    /// <summary>Draws an axis-aligned textured rectangle with explicit UV coordinates. <paramref name="textureId"/> is backend-specific (SDL_GPU: SDL_GPUTexture*).</summary>
    public void AddImage(TextureReference textureId, Vec2 min, Vec2 max, Vec2 uvMin, Vec2 uvMax, uint col = 0xFFFFFFFFu)
        => IGSharp_DrawList_AddImage(MutableHandle, KeepTexture(textureId), Reinterpret(min), Reinterpret(max), Reinterpret(uvMin), Reinterpret(uvMax), col);

    /// <summary>Draws a textured quadrilateral.</summary>
    public void AddImageQuad(TextureReference textureId, Vec2 p1, Vec2 p2, Vec2 p3, Vec2 p4, Vec2 uv1, Vec2 uv2, Vec2 uv3, Vec2 uv4, uint col = 0xFFFFFFFFu)
        => IGSharp_DrawList_AddImageQuad(MutableHandle, KeepTexture(textureId), Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), Reinterpret(p4), Reinterpret(uv1), Reinterpret(uv2), Reinterpret(uv3), Reinterpret(uv4), col);

    /// <summary>Draws a textured rectangle with rounded corners.</summary>
    public void AddImageRounded(TextureReference textureId, Vec2 min, Vec2 max, Vec2 uvMin, Vec2 uvMax, uint col, float rounding, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddImageRounded(MutableHandle, KeepTexture(textureId), Reinterpret(min), Reinterpret(max), Reinterpret(uvMin), Reinterpret(uvMax), col, rounding, (int)flags);

    /// <summary>Draws an axis-aligned image using an SDL_GPU texture.</summary>
    public void AddImage(GpuTexture texture, Vec2 min, Vec2 max, uint col = 0xFFFFFFFFu)
        => AddImage((TextureReference)texture, min, max, col);

    /// <summary>Draws an axis-aligned image using an SDL_GPU texture and explicit UV coordinates.</summary>
    public void AddImage(GpuTexture texture, Vec2 min, Vec2 max, Vec2 uvMin, Vec2 uvMax, uint col = 0xFFFFFFFFu)
        => AddImage((TextureReference)texture, min, max, uvMin, uvMax, col);

    /// <summary>Draws a textured quadrilateral using an SDL_GPU texture.</summary>
    public void AddImageQuad(GpuTexture texture, Vec2 p1, Vec2 p2, Vec2 p3, Vec2 p4, Vec2 uv1, Vec2 uv2, Vec2 uv3, Vec2 uv4, uint col = 0xFFFFFFFFu)
        => AddImageQuad((TextureReference)texture, p1, p2, p3, p4, uv1, uv2, uv3, uv4, col);

    /// <summary>Draws a rounded-corner textured rectangle using an SDL_GPU texture.</summary>
    public void AddImageRounded(GpuTexture texture, Vec2 min, Vec2 max, Vec2 uvMin, Vec2 uvMax, uint col, float rounding, DrawFlags flags = DrawFlags.None)
        => AddImageRounded((TextureReference)texture, min, max, uvMin, uvMax, col, rounding, flags);

    // --- Path API ---

    /// <summary>Clears the current path.</summary>
    public void PathClear() => IGSharp_DrawList_PathClear(MutableHandle);

    /// <summary>Adds a straight line segment to the current path.</summary>
    public void PathLineTo(Vec2 pos) => IGSharp_DrawList_PathLineTo(MutableHandle, Reinterpret(pos));

    /// <summary>Adds a straight line segment, merging with the previous point if duplicate.</summary>
    public void PathLineToMergeDuplicate(Vec2 pos) => IGSharp_DrawList_PathLineToMergeDuplicate(MutableHandle, Reinterpret(pos));

    /// <summary>Fills the current path (must be convex) and clears it.</summary>
    public void PathFillConvex(uint col, DrawFlags flags = DrawFlags.None) => IGSharp_DrawList_PathFillConvex(MutableHandle, col, (int)flags);

    /// <summary>Fills the current path (may be concave; slower than convex) and clears it.</summary>
    public void PathFillConcave(uint col) => IGSharp_DrawList_PathFillConcave(MutableHandle, col);

    /// <summary>Strokes (outlines) the current path and clears it.</summary>
    public void PathStroke(uint col, float thickness = 1f, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_PathStroke(MutableHandle, col, thickness, (int)flags);

    /// <summary>Adds an arc to the current path (in radians).</summary>
    public void PathArcTo(Vec2 center, float radius, float angleMin, float angleMax, int numSegments = 0)
        => IGSharp_DrawList_PathArcTo(MutableHandle, Reinterpret(center), radius, angleMin, angleMax, numSegments);

    /// <summary>Adds an arc using precomputed 12-segment positions.</summary>
    public void PathArcToFast(Vec2 center, float radius, int angleMinOf12, int angleMaxOf12)
        => IGSharp_DrawList_PathArcToFast(MutableHandle, Reinterpret(center), radius, angleMinOf12, angleMaxOf12);

    /// <summary>Adds an elliptical arc to the current path (angles in radians, <paramref name="rotation"/> rotates the ellipse).</summary>
    public void PathEllipticalArcTo(Vec2 center, Vec2 radius, float rotation, float angleMin, float angleMax, int numSegments = 0)
        => IGSharp_DrawList_PathEllipticalArcTo(MutableHandle, Reinterpret(center), Reinterpret(radius), rotation, angleMin, angleMax, numSegments);

    /// <summary>Adds a cubic Bezier curve segment to the current path.</summary>
    public void PathBezierCubicCurveTo(Vec2 p2, Vec2 p3, Vec2 p4, int numSegments = 0)
        => IGSharp_DrawList_PathBezierCubicCurveTo(MutableHandle, Reinterpret(p2), Reinterpret(p3), Reinterpret(p4), numSegments);

    /// <summary>Adds a quadratic Bezier curve segment to the current path.</summary>
    public void PathBezierQuadraticCurveTo(Vec2 p2, Vec2 p3, int numSegments = 0)
        => IGSharp_DrawList_PathBezierQuadraticCurveTo(MutableHandle, Reinterpret(p2), Reinterpret(p3), numSegments);

    /// <summary>Adds a rectangle to the current path.</summary>
    public void PathRect(Vec2 min, Vec2 max, float rounding = 0f, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_PathRect(MutableHandle, Reinterpret(min), Reinterpret(max), rounding, (int)flags);

    // --- Channels ---

    /// <summary>
    /// Splits this draw list into <paramref name="count"/> layers (channels) that are drawn out of order
    /// and merged back in channel order by <see cref="ChannelsMerge"/>. Channel 0 is current after the split.
    /// Cannot be nested — use a standalone <see cref="DrawListSplitter"/> to interleave splits.
    /// </summary>
    public void ChannelsSplit(int count) => IGSharp_DrawList_ChannelsSplit(MutableHandle, count);

    /// <summary>Merges all channels back into the draw list in channel order.</summary>
    public void ChannelsMerge() => IGSharp_DrawList_ChannelsMerge(MutableHandle);

    /// <summary>Redirects subsequent drawing to the given channel.</summary>
    public void ChannelsSetCurrent(int channelIndex) => IGSharp_DrawList_ChannelsSetCurrent(MutableHandle, channelIndex);

    // --- Advanced ---

    /// <summary>
    /// Inserts a render callback into the draw command stream — invoked by the renderer backend when it
    /// reaches this point in the draw list, allowing custom native rendering between ImGui draw calls.
    /// Frame-list registrations live until the next managed frame; standalone-list registrations live
    /// until owner disposal. Clones retain independent registrations and may invoke them repeatedly.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The release token is owned by the draw-list lifetime or the per-context frame registry.")]
    public void AddCallback(Action<DrawList, DrawCmd> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        var native = MutableHandle;
        var token = Interlocked.Increment(ref _nextCallback);
        var registration = new CallbackRegistration(callback);
        lock (CallbackSync) Callbacks.Add(token, registration);
        var release = new ReleaseAction(() => ReleaseCallback(token));
        RegisterResource(release);
        IGSharp_DrawList_AddCallback(native, ManagedCallback, (void*)token, 0);
    }

    /// <summary>Forces a new draw command — useful after a callback, or to change rendering state manually.</summary>
    public void AddDrawCmd() => IGSharp_DrawList_AddDrawCmd(MutableHandle);

    /// <summary>
    /// Creates an owned deep copy of this draw list's output. Unlike the frame-transient draw list itself,
    /// the clone stays valid across frames until disposed.
    /// </summary>
    public ClonedDrawList CloneOutput() => new(Handle);

    /// <summary>Copies the current mesh vertices.</summary>
    public DrawVertex[] CopyVertices() => DrawGeometry.CopyVertices(Handle);
    /// <summary>Copies the current mesh indices.</summary>
    public ushort[] CopyIndices() => DrawGeometry.CopyIndices(Handle);
    // --- Geometry buffers (introspection) ---

    /// <summary>Number of draw commands in the command buffer.</summary>
    public int CmdBufferSize => IGSharp_DrawList_GetCmdBufferSize(Handle);

    /// <summary>Number of indices in the index buffer.</summary>
    public int IdxBufferSize => IGSharp_DrawList_GetIdxBufferSize(Handle);

    /// <summary>Number of vertices in the vertex buffer.</summary>
    public int VtxBufferSize => IGSharp_DrawList_GetVtxBufferSize(Handle);

    /// <summary>Returns the draw command at <paramref name="index"/> (0 .. <see cref="CmdBufferSize"/> - 1).</summary>
    public DrawCmd GetCmd(int index) { ArgumentOutOfRangeException.ThrowIfNegative(index); ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, CmdBufferSize); return new(IGSharp_DrawList_GetCmd(Handle, index), textures: Lifetime); }

    /// <summary>Invokes a managed user callback. Returns false for geometry and renderer-state commands; handle those using DrawCmd.CallbackKind.</summary>
    public bool TryInvokeCallback(int index) => InvokeCallback(Handle, index);
    internal static bool InvokeCallback(IGSharp_DrawList* list, int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index); ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, IGSharp_DrawList_GetCmdBufferSize(list));
        var command = IGSharp_DrawList_GetCmd(list, index);
        if (new DrawCmd(command).CallbackKind != DrawCallbackKind.User) return false;
        IGSharp_DrawCmd_GetUserCallback(command)(list, command);
        ImGui.ThrowPendingCallbackExceptions(); return true;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "RegisterResource transfers both leases to list-lifetime or frame cleanup.")]
    internal IGSharp_TextureRef KeepTexture(TextureReference texture)
    {
        _ = MutableHandle; var value = texture.Value;
        if (texture.GpuTexture is { } gpu && gpu.RetainFor(Lifetime) is { } gpuLease) RegisterResource(gpuLease);
        if (value.Data == null) return value;
        var lifetime = Lifetime; var pointer = (nint)value.Data;
        if (!lifetime.TextureReferences.Add(pointer)) return value;
        IDisposable lease;
        try { lease = TextureRetention.Retain(value.Data, lifetime.Owned ? lifetime : Context.Current.Lifetime); }
        catch { lifetime.TextureReferences.Remove(pointer); throw; }
        var release = new ReleaseAction(() => { lease.Dispose(); lifetime.TextureReferences.Remove(pointer); });
        RegisterResource(release); return value;
    }
    private void RegisterResource(IDisposable resource)
    {
        if (Lifetime.Owned) Lifetime.AddCleanup(resource.Dispose);
        else
        {
            var context = (nint)Context.Current.Handle;
            lock (CallbackSync)
            {
                if (!FrameCallbacks.TryGetValue(context, out var resources)) FrameCallbacks[context] = resources = [];
                resources.Add(resource);
            }
        }
    }

    // --- Callback plumbing ---

    private static readonly object CallbackSync = new();
    private static readonly delegate* unmanaged[Cdecl]<IGSharp_DrawList*, IGSharp_DrawCmd*, void> ManagedCallback = &CallbackTrampoline;
    private static long _nextCallback;
    private static readonly Dictionary<long, CallbackRegistration> Callbacks = [];
    private static readonly Dictionary<nint, List<IDisposable>> FrameCallbacks = [];
    private sealed class CallbackRegistration(Action<DrawList, DrawCmd> callback)
    {
        internal readonly Action<DrawList, DrawCmd> Callback = callback;
        internal int References = 1;
    }
    private static void ReleaseCallback(long token)
    {
        lock (CallbackSync)
            if (Callbacks.TryGetValue(token, out var registration) && --registration.References == 0) Callbacks.Remove(token);
    }
    internal static void RetainCallbacks(IGSharp_DrawList* list, Lifetime owner)
    {
        for (var i = 0; i < IGSharp_DrawList_GetCmdBufferSize(list); i++)
        {
            var cmd = IGSharp_DrawList_GetCmd(list, i);
            if ((nint)IGSharp_DrawCmd_GetUserCallback(cmd) != (nint)ManagedCallback) continue;
            var token = (long)IGSharp_DrawCmd_GetUserCallbackData(cmd);
            lock (CallbackSync)
            {
                if (!Callbacks.TryGetValue(token, out var registration)) throw new InvalidOperationException("The source callback expired.");
                registration.References++;
            }
            owner.AddCleanup(() => ReleaseCallback(token));
        }
    }
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void CallbackTrampoline(IGSharp_DrawList* drawList, IGSharp_DrawCmd* cmd)
    {
        try
        {
            var token = (long)IGSharp_DrawCmd_GetUserCallbackData(cmd);
            Action<DrawList, DrawCmd>? callback;
            lock (CallbackSync) callback = Callbacks.GetValueOrDefault(token)?.Callback;
            using var guard = Context.Current.EnterCallback();
            var lifetime = SdlSharp.ImGui.Lifetime.Capture((nint)drawList);
            using var lease = lifetime.Retain();
            var wasReadOnly = lifetime.ReadOnly; lifetime.ReadOnly = true;
            try { callback?.Invoke(new DrawList(drawList, lifetime, frame: false), new DrawCmd(cmd, lifetime.IsSnapshot ? lifetime : null, textures: lifetime)); }
            finally { lifetime.ReadOnly = wasReadOnly; }
        }
        catch (Exception ex) { ImGui.ReportUnhandledCallbackException(ex); }
    }
    internal static void ReleaseFrame(nint context)
    {
        List<IDisposable>? releases;
        lock (CallbackSync) FrameCallbacks.Remove(context, out releases);
        if (releases != null) foreach (var release in releases) release.Dispose();
    }
    internal static void ReleaseContext(nint context) => ReleaseFrame(context);

    private static IGSharp_Vec2 Reinterpret(Vec2 v) => new(v.X, v.Y);
    /// <summary>Pushes a draw flag override until PopDrawFlag.</summary>
    public void PushDrawFlag(DrawFlags flags, bool enabled) => IGSharp_DrawList_PushDrawFlag(MutableHandle, (int)flags, enabled);
    /// <summary>Restores the previous draw flag.</summary>
    public void PopDrawFlag() => IGSharp_DrawList_PopDrawFlag(MutableHandle);
    /// <summary>Overrides the pixel density used for this draw list's primitives.</summary>
    public void SetPixelDensity(float density) => IGSharp_DrawList_SetPixelDensity(MutableHandle, density);
    /// <summary>Adds a horizontal line.</summary>
    public void AddLineH(float minX, float maxX, float y, uint color, float thickness = 1, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddLineH(MutableHandle, minX, maxX, y, color, thickness, (int)flags);
    /// <summary>Adds a vertical line.</summary>
    public void AddLineV(float x, float minY, float maxY, uint color, float thickness = 1, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddLineV(MutableHandle, x, minY, maxY, color, thickness, (int)flags);
    /// <summary>Requests the active renderer to restore its state after custom rendering.</summary>
    public void AddResetRenderStateCallback()
    {
        var callback = IGSharp_PlatformIO_GetDrawCallbackResetRenderState(IGSharp_GetPlatformIO());
        if (callback == null) throw new InvalidOperationException("The renderer has no reset-state callback.");
        IGSharp_DrawList_AddCallback(MutableHandle, callback, null, 0);
    }

    /// <summary>Adds validated indexed triangles. Indices are relative to the supplied vertices; meshes are limited to 16-bit indices.</summary>
    public void AddMesh(ReadOnlySpan<DrawVertex> vertices, ReadOnlySpan<ushort> indices)
    {
        var handle = MutableHandle;
        if (indices.Length % 3 != 0) throw new ArgumentException("Triangle indices must be a multiple of three.", nameof(indices));
        var offset = VtxBufferSize;
        if (vertices.Length + (long)offset > ushort.MaxValue) throw new ArgumentException("This mesh exceeds the 16-bit draw-list limit.", nameof(vertices));
        foreach (var index in indices) if (index >= vertices.Length) throw new ArgumentException("An index is outside the vertex span.", nameof(indices));
        IGSharp_DrawList_PrimReserve(handle, indices.Length, vertices.Length);
        foreach (var vertex in vertices) IGSharp_DrawList_PrimWriteVtx(handle, new(vertex.Position.X, vertex.Position.Y), new(vertex.Uv.X, vertex.Uv.Y), vertex.Color);
        foreach (var index in indices) IGSharp_DrawList_PrimWriteIdx(handle, (ushort)(offset + index));
    }
    /// <summary>Adds a standard renderer sampler callback; false selects nearest-neighbor sampling.</summary>
    public void AddSamplerCallback(bool linear)
    {
        var callback = linear ? IGSharp_PlatformIO_GetDrawCallbackSetSamplerLinear(IGSharp_GetPlatformIO()) : IGSharp_PlatformIO_GetDrawCallbackSetSamplerNearest(IGSharp_GetPlatformIO());
        if (callback == null) throw new InvalidOperationException("The renderer does not provide a sampler callback.");
        IGSharp_DrawList_AddCallback(MutableHandle, callback, null, 0);
    }
    /// <summary>Restores the sampler associated with each texture.</summary>
    public void AddTextureSamplerCallback()
    {
        var callback = IGSharp_PlatformIO_GetDrawCallbackSetSamplerFromTex(IGSharp_GetPlatformIO());
        if (callback == null) throw new InvalidOperationException("The renderer does not provide a texture-sampler callback.");
        IGSharp_DrawList_AddCallback(MutableHandle, callback, null, 0);
    }

}
