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
    internal readonly IGSharp_DrawList* Handle;

    internal DrawList(IGSharp_DrawList* handle) => Handle = handle;

    /// <summary>True if this handle refers to a valid draw list.</summary>
    public bool IsValid => Handle != null;

    // --- Clipping ---

    /// <summary>Pushes a clipping rectangle. Drawing outside the rect will be clipped.</summary>
    public void PushClipRect(Vec2 min, Vec2 max, bool intersectWithCurrent = false)
        => IGSharp_DrawList_PushClipRect(Handle, Reinterpret(min), Reinterpret(max), intersectWithCurrent);

    /// <summary>Pushes a full-screen clipping rectangle.</summary>
    public void PushClipRectFullScreen() => IGSharp_DrawList_PushClipRectFullScreen(Handle);

    /// <summary>Pops the last clipping rectangle.</summary>
    public void PopClipRect() => IGSharp_DrawList_PopClipRect(Handle);

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
    public void PushTexture(ulong textureId) => IGSharp_DrawList_PushTexture(Handle, textureId);

    /// <summary>Pushes an SDL_GPU texture onto the texture stack.</summary>
    public void PushTexture(GpuTexture texture) => PushTexture((ulong)texture.NativeHandle);

    /// <summary>Pops the last texture off the texture stack.</summary>
    public void PopTexture() => IGSharp_DrawList_PopTexture(Handle);

    // --- Flags ---

    /// <summary>Rendering flags for this draw list (anti-aliasing, vertex offsets). Initialized each frame from the ImGui style.</summary>
    public DrawListFlags Flags
    {
        get => (DrawListFlags)IGSharp_DrawList_GetFlags(Handle);
        set => IGSharp_DrawList_SetFlags(Handle, (int)value);
    }

    // --- Primitives ---

    /// <summary>Draws a line between two points.</summary>
    public void AddLine(Vec2 p1, Vec2 p2, uint col, float thickness = 1f)
        => IGSharp_DrawList_AddLine(Handle, Reinterpret(p1), Reinterpret(p2), col, thickness);

    /// <summary>Draws a rectangle outline.</summary>
    public void AddRect(Vec2 min, Vec2 max, uint col, float rounding = 0f, DrawFlags flags = DrawFlags.None, float thickness = 1f)
        => IGSharp_DrawList_AddRect(Handle, Reinterpret(min), Reinterpret(max), col, rounding, (int)flags, thickness);

    /// <summary>Draws a filled rectangle.</summary>
    public void AddRectFilled(Vec2 min, Vec2 max, uint col, float rounding = 0f, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddRectFilled(Handle, Reinterpret(min), Reinterpret(max), col, rounding, (int)flags);

    /// <summary>Draws a filled rectangle with per-corner colors (gradient).</summary>
    public void AddRectFilledMultiColor(Vec2 min, Vec2 max, uint colUpperLeft, uint colUpperRight, uint colBottomRight, uint colBottomLeft)
        => IGSharp_DrawList_AddRectFilledMultiColor(Handle, Reinterpret(min), Reinterpret(max), colUpperLeft, colUpperRight, colBottomRight, colBottomLeft);

    /// <summary>Draws a quadrilateral outline (4 points in order).</summary>
    public void AddQuad(Vec2 p1, Vec2 p2, Vec2 p3, Vec2 p4, uint col, float thickness = 1f)
        => IGSharp_DrawList_AddQuad(Handle, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), Reinterpret(p4), col, thickness);

    /// <summary>Draws a filled quadrilateral.</summary>
    public void AddQuadFilled(Vec2 p1, Vec2 p2, Vec2 p3, Vec2 p4, uint col)
        => IGSharp_DrawList_AddQuadFilled(Handle, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), Reinterpret(p4), col);

    /// <summary>Draws a triangle outline.</summary>
    public void AddTriangle(Vec2 p1, Vec2 p2, Vec2 p3, uint col, float thickness = 1f)
        => IGSharp_DrawList_AddTriangle(Handle, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), col, thickness);

    /// <summary>Draws a filled triangle.</summary>
    public void AddTriangleFilled(Vec2 p1, Vec2 p2, Vec2 p3, uint col)
        => IGSharp_DrawList_AddTriangleFilled(Handle, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), col);

    /// <summary>Draws a circle outline. Pass 0 for <paramref name="numSegments"/> to auto-tessellate.</summary>
    public void AddCircle(Vec2 center, float radius, uint col, int numSegments = 0, float thickness = 1f)
        => IGSharp_DrawList_AddCircle(Handle, Reinterpret(center), radius, col, numSegments, thickness);

    /// <summary>Draws a filled circle.</summary>
    public void AddCircleFilled(Vec2 center, float radius, uint col, int numSegments = 0)
        => IGSharp_DrawList_AddCircleFilled(Handle, Reinterpret(center), radius, col, numSegments);

    /// <summary>Draws an n-gon outline with the given number of segments.</summary>
    public void AddNgon(Vec2 center, float radius, uint col, int numSegments, float thickness = 1f)
        => IGSharp_DrawList_AddNgon(Handle, Reinterpret(center), radius, col, numSegments, thickness);

    /// <summary>Draws a filled n-gon.</summary>
    public void AddNgonFilled(Vec2 center, float radius, uint col, int numSegments)
        => IGSharp_DrawList_AddNgonFilled(Handle, Reinterpret(center), radius, col, numSegments);

    /// <summary>Draws an ellipse outline.</summary>
    public void AddEllipse(Vec2 center, Vec2 radius, uint col, float rotation = 0f, int numSegments = 0, float thickness = 1f)
        => IGSharp_DrawList_AddEllipse(Handle, Reinterpret(center), Reinterpret(radius), col, rotation, numSegments, thickness);

    /// <summary>Draws a filled ellipse.</summary>
    public void AddEllipseFilled(Vec2 center, Vec2 radius, uint col, float rotation = 0f, int numSegments = 0)
        => IGSharp_DrawList_AddEllipseFilled(Handle, Reinterpret(center), Reinterpret(radius), col, rotation, numSegments);

    /// <summary>Draws text at the given position using the current font.</summary>
    public void AddText(Vec2 pos, uint col, string text)
        => IGSharp_DrawList_AddText(Handle, Reinterpret(pos), col, ToUtf8(text), null);

    /// <summary>
    /// Draws text with an explicit font and size. Pass 0 for <paramref name="wrapWidth"/> to disable wrapping.
    /// </summary>
    public void AddText(Font font, float fontSize, Vec2 pos, uint col, string text, float wrapWidth = 0f)
        => IGSharp_DrawList_AddTextFont(Handle, (IGSharp_Font*)font.Handle, fontSize, Reinterpret(pos), col, ToUtf8(text), null, wrapWidth, null);

    /// <summary>
    /// Draws text with an explicit font, size, and a fine (exact) CPU-side clip rectangle (minX, minY, maxX, maxY).
    /// </summary>
    public void AddText(Font font, float fontSize, Vec2 pos, uint col, string text, float wrapWidth, Vec4 cpuFineClipRect)
    {
        var clip = new IGSharp_Vec4(cpuFineClipRect.X, cpuFineClipRect.Y, cpuFineClipRect.Z, cpuFineClipRect.W);
        IGSharp_DrawList_AddTextFont(Handle, (IGSharp_Font*)font.Handle, fontSize, Reinterpret(pos), col, ToUtf8(text), null, wrapWidth, &clip);
    }

    /// <summary>Draws a cubic Bezier curve.</summary>
    public void AddBezierCubic(Vec2 p1, Vec2 p2, Vec2 p3, Vec2 p4, uint col, float thickness = 1f, int numSegments = 0)
        => IGSharp_DrawList_AddBezierCubic(Handle, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), Reinterpret(p4), col, thickness, numSegments);

    /// <summary>Draws a quadratic Bezier curve.</summary>
    public void AddBezierQuadratic(Vec2 p1, Vec2 p2, Vec2 p3, uint col, float thickness = 1f, int numSegments = 0)
        => IGSharp_DrawList_AddBezierQuadratic(Handle, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), col, thickness, numSegments);

    /// <summary>Draws a connected series of line segments.</summary>
    public void AddPolyline(ReadOnlySpan<Vec2> points, uint col, DrawFlags flags = DrawFlags.None, float thickness = 1f)
    {
        fixed (Vec2* p = points)
            IGSharp_DrawList_AddPolyline(Handle, (IGSharp_Vec2*)p, points.Length, col, (int)flags, thickness);
    }

    /// <summary>Fills a convex polygon.</summary>
    public void AddConvexPolyFilled(ReadOnlySpan<Vec2> points, uint col)
    {
        fixed (Vec2* p = points)
            IGSharp_DrawList_AddConvexPolyFilled(Handle, (IGSharp_Vec2*)p, points.Length, col);
    }

    /// <summary>Fills a concave polygon (slower than convex).</summary>
    public void AddConcavePolyFilled(ReadOnlySpan<Vec2> points, uint col)
    {
        fixed (Vec2* p = points)
            IGSharp_DrawList_AddConcavePolyFilled(Handle, (IGSharp_Vec2*)p, points.Length, col);
    }

    // --- Images ---

    /// <summary>Draws an axis-aligned textured rectangle. <paramref name="textureId"/> is backend-specific (SDL_GPU: SDL_GPUTexture*).</summary>
    public void AddImage(ulong textureId, Vec2 min, Vec2 max, Vec2 uvMin = default, Vec2 uvMax = default, uint col = 0xFFFFFFFFu)
    {
        var defaultMax = uvMax == default ? new Vec2(1, 1) : uvMax;
        IGSharp_DrawList_AddImage(Handle, textureId, Reinterpret(min), Reinterpret(max), Reinterpret(uvMin), Reinterpret(defaultMax), col);
    }

    /// <summary>Draws a textured quadrilateral.</summary>
    public void AddImageQuad(ulong textureId, Vec2 p1, Vec2 p2, Vec2 p3, Vec2 p4, Vec2 uv1, Vec2 uv2, Vec2 uv3, Vec2 uv4, uint col = 0xFFFFFFFFu)
        => IGSharp_DrawList_AddImageQuad(Handle, textureId, Reinterpret(p1), Reinterpret(p2), Reinterpret(p3), Reinterpret(p4), Reinterpret(uv1), Reinterpret(uv2), Reinterpret(uv3), Reinterpret(uv4), col);

    /// <summary>Draws a textured rectangle with rounded corners.</summary>
    public void AddImageRounded(ulong textureId, Vec2 min, Vec2 max, Vec2 uvMin, Vec2 uvMax, uint col, float rounding, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_AddImageRounded(Handle, textureId, Reinterpret(min), Reinterpret(max), Reinterpret(uvMin), Reinterpret(uvMax), col, rounding, (int)flags);

    /// <summary>Draws an axis-aligned image using an SDL_GPU texture.</summary>
    public void AddImage(GpuTexture texture, Vec2 min, Vec2 max, Vec2 uvMin = default, Vec2 uvMax = default, uint col = 0xFFFFFFFFu)
        => AddImage((ulong)texture.NativeHandle, min, max, uvMin, uvMax, col);

    /// <summary>Draws a textured quadrilateral using an SDL_GPU texture.</summary>
    public void AddImageQuad(GpuTexture texture, Vec2 p1, Vec2 p2, Vec2 p3, Vec2 p4, Vec2 uv1, Vec2 uv2, Vec2 uv3, Vec2 uv4, uint col = 0xFFFFFFFFu)
        => AddImageQuad((ulong)texture.NativeHandle, p1, p2, p3, p4, uv1, uv2, uv3, uv4, col);

    /// <summary>Draws a rounded-corner textured rectangle using an SDL_GPU texture.</summary>
    public void AddImageRounded(GpuTexture texture, Vec2 min, Vec2 max, Vec2 uvMin, Vec2 uvMax, uint col, float rounding, DrawFlags flags = DrawFlags.None)
        => AddImageRounded((ulong)texture.NativeHandle, min, max, uvMin, uvMax, col, rounding, flags);

    // --- Path API ---

    /// <summary>Clears the current path.</summary>
    public void PathClear() => IGSharp_DrawList_PathClear(Handle);

    /// <summary>Adds a straight line segment to the current path.</summary>
    public void PathLineTo(Vec2 pos) => IGSharp_DrawList_PathLineTo(Handle, Reinterpret(pos));

    /// <summary>Adds a straight line segment, merging with the previous point if duplicate.</summary>
    public void PathLineToMergeDuplicate(Vec2 pos) => IGSharp_DrawList_PathLineToMergeDuplicate(Handle, Reinterpret(pos));

    /// <summary>Fills the current path (must be convex) and clears it.</summary>
    public void PathFillConvex(uint col) => IGSharp_DrawList_PathFillConvex(Handle, col);

    /// <summary>Fills the current path (may be concave; slower than convex) and clears it.</summary>
    public void PathFillConcave(uint col) => IGSharp_DrawList_PathFillConcave(Handle, col);

    /// <summary>Strokes (outlines) the current path and clears it.</summary>
    public void PathStroke(uint col, DrawFlags flags = DrawFlags.None, float thickness = 1f)
        => IGSharp_DrawList_PathStroke(Handle, col, (int)flags, thickness);

    /// <summary>Adds an arc to the current path (in radians).</summary>
    public void PathArcTo(Vec2 center, float radius, float angleMin, float angleMax, int numSegments = 0)
        => IGSharp_DrawList_PathArcTo(Handle, Reinterpret(center), radius, angleMin, angleMax, numSegments);

    /// <summary>Adds an arc using precomputed 12-segment positions.</summary>
    public void PathArcToFast(Vec2 center, float radius, int angleMinOf12, int angleMaxOf12)
        => IGSharp_DrawList_PathArcToFast(Handle, Reinterpret(center), radius, angleMinOf12, angleMaxOf12);

    /// <summary>Adds an elliptical arc to the current path (angles in radians, <paramref name="rotation"/> rotates the ellipse).</summary>
    public void PathEllipticalArcTo(Vec2 center, Vec2 radius, float rotation, float angleMin, float angleMax, int numSegments = 0)
        => IGSharp_DrawList_PathEllipticalArcTo(Handle, Reinterpret(center), Reinterpret(radius), rotation, angleMin, angleMax, numSegments);

    /// <summary>Adds a cubic Bezier curve segment to the current path.</summary>
    public void PathBezierCubicCurveTo(Vec2 p2, Vec2 p3, Vec2 p4, int numSegments = 0)
        => IGSharp_DrawList_PathBezierCubicCurveTo(Handle, Reinterpret(p2), Reinterpret(p3), Reinterpret(p4), numSegments);

    /// <summary>Adds a quadratic Bezier curve segment to the current path.</summary>
    public void PathBezierQuadraticCurveTo(Vec2 p2, Vec2 p3, int numSegments = 0)
        => IGSharp_DrawList_PathBezierQuadraticCurveTo(Handle, Reinterpret(p2), Reinterpret(p3), numSegments);

    /// <summary>Adds a rectangle to the current path.</summary>
    public void PathRect(Vec2 min, Vec2 max, float rounding = 0f, DrawFlags flags = DrawFlags.None)
        => IGSharp_DrawList_PathRect(Handle, Reinterpret(min), Reinterpret(max), rounding, (int)flags);

    // --- Channels ---

    /// <summary>
    /// Splits this draw list into <paramref name="count"/> layers (channels) that are drawn out of order
    /// and merged back in channel order by <see cref="ChannelsMerge"/>. Channel 0 is current after the split.
    /// Cannot be nested — use a standalone <see cref="DrawListSplitter"/> to interleave splits.
    /// </summary>
    public void ChannelsSplit(int count) => IGSharp_DrawList_ChannelsSplit(Handle, count);

    /// <summary>Merges all channels back into the draw list in channel order.</summary>
    public void ChannelsMerge() => IGSharp_DrawList_ChannelsMerge(Handle);

    /// <summary>Redirects subsequent drawing to the given channel.</summary>
    public void ChannelsSetCurrent(int channelIndex) => IGSharp_DrawList_ChannelsSetCurrent(Handle, channelIndex);

    // --- Advanced ---

    /// <summary>
    /// Inserts a render callback into the draw command stream — invoked by the renderer backend when it
    /// reaches this point in the draw list, allowing custom native rendering between ImGui draw calls.
    /// The delegate is kept alive for the current frame only; it is released when a callback is added
    /// in a later frame. Do not rely on it being invoked more than once.
    /// </summary>
    public void AddCallback(Action<DrawList, DrawCmd> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        var frame = IGSharp_GetFrameCount();
        if (frame != s_callbacksFrame)
        {
            s_callbacks.Clear();
            s_callbacksFrame = frame;
        }
        int index = s_callbacks.Count;
        s_callbacks.Add(callback);
        // ImGui copies the userdata (size > 0) into the draw list, so the local's address is safe here.
        IGSharp_DrawList_AddCallback(Handle, &CallbackTrampoline, &index, (nuint)sizeof(int));
    }

    /// <summary>Forces a new draw command — useful after a callback, or to change rendering state manually.</summary>
    public void AddDrawCmd() => IGSharp_DrawList_AddDrawCmd(Handle);

    /// <summary>
    /// Creates an owned deep copy of this draw list's output. Unlike the frame-transient draw list itself,
    /// the clone stays valid across frames until disposed.
    /// </summary>
    public ClonedDrawList CloneOutput() => new(IGSharp_DrawList_CloneOutput(Handle));

    // --- Geometry buffers (introspection) ---

    /// <summary>Number of draw commands in the command buffer.</summary>
    public int CmdBufferSize => IGSharp_DrawList_GetCmdBufferSize(Handle);

    /// <summary>Number of indices in the index buffer.</summary>
    public int IdxBufferSize => IGSharp_DrawList_GetIdxBufferSize(Handle);

    /// <summary>Number of vertices in the vertex buffer.</summary>
    public int VtxBufferSize => IGSharp_DrawList_GetVtxBufferSize(Handle);

    /// <summary>Returns the draw command at <paramref name="index"/> (0 .. <see cref="CmdBufferSize"/> - 1).</summary>
    public DrawCmd GetCmd(int index) => new(IGSharp_DrawList_GetCmd(Handle, index));

    // --- Callback plumbing ---

    // ImGui is single-threaded; delegates are rooted here for the frame they were added in and
    // replaced wholesale when AddCallback is first called in a later frame.
    private static readonly List<Action<DrawList, DrawCmd>> s_callbacks = [];
    private static int s_callbacksFrame = -1;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void CallbackTrampoline(IGSharp_DrawList* drawList, IGSharp_DrawCmd* cmd)
    {
        try
        {
            var index = *(int*)IGSharp_DrawCmd_GetUserCallbackData(cmd);
            if ((uint)index < (uint)s_callbacks.Count)
                s_callbacks[index](new DrawList(drawList), new DrawCmd(cmd));
        }
        catch
        {
            // Never let a managed exception escape into native rendering code.
        }
    }

    private static IGSharp_Vec2 Reinterpret(Vec2 v) => new(v.X, v.Y);
}

/// <summary>
/// An owned deep copy of a <see cref="DrawList"/>'s output, created via <see cref="DrawList.CloneOutput"/>.
/// Stays valid across frames until disposed.
/// </summary>
public sealed unsafe class ClonedDrawList : IDisposable
{
    private IGSharp_DrawList* _handle;

    internal ClonedDrawList(IGSharp_DrawList* handle) => _handle = handle;

    /// <summary>A draw list view over the cloned output.</summary>
    public DrawList List
    {
        get
        {
            if (_handle == null) throw new ObjectDisposedException(nameof(ClonedDrawList));
            return new DrawList(_handle);
        }
    }

    /// <summary>Releases the unmanaged draw list clone.</summary>
    public void Dispose()
    {
        if (_handle != null)
        {
            IGSharp_DrawList_Destroy(_handle);
            _handle = null;
        }
    }
}
