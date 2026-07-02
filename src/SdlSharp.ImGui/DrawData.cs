using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A handle to the frame's rendered draw data (ImDrawData) — everything ImGui produced for the
/// current frame, ready to be handed to a renderer backend. Obtain via <see cref="ImGui.GetDrawData"/>
/// after <see cref="ImGui.Render"/>. The handle is only valid until the next call to
/// <see cref="ImGui.Render"/> or <see cref="ImGuiBackend.NewFrame"/> — do not store across frames.
/// </summary>
public readonly unsafe struct DrawData
{
    internal DrawData(IGSharp_DrawData* handle) => Handle = handle;

    internal IGSharp_DrawData* Handle { get; }

    /// <summary>True if this handle is non-null and the draw data is valid (set after Render, cleared after NewFrame).</summary>
    public bool Valid => Handle != null && IGSharp_DrawData_GetValid(Handle);

    /// <summary>Number of draw lists to render.</summary>
    public int CmdListsCount => IGSharp_DrawData_GetCmdListsCount(Handle);

    /// <summary>Sum of all draw lists' index buffer sizes — useful for sizing a single index buffer upfront.</summary>
    public int TotalIdxCount => IGSharp_DrawData_GetTotalIdxCount(Handle);

    /// <summary>Sum of all draw lists' vertex buffer sizes — useful for sizing a single vertex buffer upfront.</summary>
    public int TotalVtxCount => IGSharp_DrawData_GetTotalVtxCount(Handle);

    /// <summary>Returns the draw list at <paramref name="index"/> (0 .. <see cref="CmdListsCount"/> - 1).</summary>
    public DrawList GetCmdList(int index) => new(IGSharp_DrawData_GetCmdList(Handle, index));

    /// <summary>Top-left position of the viewport to render (== <see cref="Viewport.Pos"/> for the main viewport).</summary>
    public Vec2 DisplayPos
    {
        get
        {
            var v = IGSharp_DrawData_GetDisplayPos(Handle);
            return new Vec2(v.X, v.Y);
        }
    }

    /// <summary>Size of the viewport to render (== <see cref="Viewport.Size"/> for the main viewport).</summary>
    public Vec2 DisplaySize
    {
        get
        {
            var v = IGSharp_DrawData_GetDisplaySize(Handle);
            return new Vec2(v.X, v.Y);
        }
    }

    /// <summary>Amount of pixels for each unit of DisplaySize (typically (1,1), or higher on retina displays).</summary>
    public Vec2 FramebufferScale
    {
        get
        {
            var v = IGSharp_DrawData_GetFramebufferScale(Handle);
            return new Vec2(v.X, v.Y);
        }
    }

    /// <summary>The viewport that owns this draw data.</summary>
    public Viewport OwnerViewport => new(IGSharp_DrawData_GetOwnerViewport(Handle));

    /// <summary>Number of textures the backend is expected to update this frame (backend concern; 0 if the textures list is unset).</summary>
    public int TexturesCount => IGSharp_DrawData_GetTexturesCount(Handle);

    /// <summary>Converts all draw lists' indexed buffers to non-indexed ones, for backends that cannot render indexed geometry. Slow!</summary>
    public void DeIndexAllBuffers() => IGSharp_DrawData_DeIndexAllBuffers(Handle);

    /// <summary>Scales all clip rectangles by <paramref name="fbScale"/> — for retina displays or when the final output buffer differs from DisplaySize.</summary>
    public void ScaleClipRects(Vec2 fbScale) => IGSharp_DrawData_ScaleClipRects(Handle, new IGSharp_Vec2(fbScale.X, fbScale.Y));
}
