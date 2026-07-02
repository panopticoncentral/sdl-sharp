using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// A read-only view over a single ImGui draw command (ImDrawCmd) — a range of indices in a
/// <see cref="DrawList"/> rendered with a single clip rectangle and texture.
/// Obtain via <see cref="DrawList.GetCmd"/> or inside a <see cref="DrawList.AddCallback"/> callback.
/// The underlying pointer is frame-transient: it is only valid until the draw list it came from
/// is modified or the next frame begins. Do not store instances across frames.
/// </summary>
public readonly unsafe struct DrawCmd
{
    internal readonly IGSharp_DrawCmd* Handle;

    internal DrawCmd(IGSharp_DrawCmd* handle) => Handle = handle;

    /// <summary>True if this handle refers to a valid draw command.</summary>
    public bool IsValid => Handle != null;

    /// <summary>Clip rectangle as (minX, minY, maxX, maxY), relative to the draw data's DisplayPos.</summary>
    public Vec4 ClipRect
    {
        get
        {
            var v = IGSharp_DrawCmd_GetClipRect(Handle);
            return new Vec4(v.X, v.Y, v.Z, v.W);
        }
    }

    /// <summary>Texture id to bind for this command. Backend-specific (SDL_GPU: SDL_GPUTexture*).</summary>
    public ulong TextureId => IGSharp_DrawCmd_GetTexID(Handle);

    /// <summary>Start offset into the vertex buffer (used when DrawListFlags.AllowVtxOffset is enabled for large meshes).</summary>
    public uint VtxOffset => IGSharp_DrawCmd_GetVtxOffset(Handle);

    /// <summary>Start offset into the index buffer.</summary>
    public uint IdxOffset => IGSharp_DrawCmd_GetIdxOffset(Handle);

    /// <summary>Number of indices (triangle count * 3) to render for this command.</summary>
    public uint ElemCount => IGSharp_DrawCmd_GetElemCount(Handle);

    /// <summary>True if this command carries a user callback (added via <see cref="DrawList.AddCallback"/>) instead of geometry.</summary>
    public bool HasUserCallback => IGSharp_DrawCmd_GetUserCallback(Handle) != null;

    /// <summary>True if this command carries user callback data.</summary>
    public bool HasUserCallbackData => IGSharp_DrawCmd_GetUserCallbackData(Handle) != null;

    /// <summary>Size in bytes of the callback data copied into the draw list (0 if the data was stored by pointer).</summary>
    public int UserCallbackDataSize => IGSharp_DrawCmd_GetUserCallbackDataSize(Handle);
}
