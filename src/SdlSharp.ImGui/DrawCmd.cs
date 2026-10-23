using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>How a renderer should process a drawing callback.</summary>
public enum DrawCallbackKind
{
    /// <summary>Ordinary geometry command.</summary>
    None,
    /// <summary>A managed callback that can be invoked through its live list or snapshot.</summary>
    User,
    /// <summary>Restore the renderer's ImGui pipeline state.</summary>
    ResetRenderState,
    /// <summary>Select the renderer's linear sampler.</summary>
    SamplerLinear,
    /// <summary>Select the renderer's nearest-neighbor sampler.</summary>
    SamplerNearest,
    /// <summary>Select the sampler associated with the texture.</summary>
    SamplerFromTexture
}

/// <summary>
/// Copied draw-command metadata, unaffected by native command-buffer relocation.
/// Deferred texture references remain guarded by their frame or snapshot lifetime.
/// </summary>
public readonly unsafe struct DrawCmd
{
    private readonly IGSharp_DrawCmdInfo _info;
    private readonly bool _valid;
    /// <summary>Renderer action represented by this command.</summary>
    public DrawCallbackKind CallbackKind { get; }
    private readonly TextureReference _texture;

    internal DrawCmd(IGSharp_DrawCmd* handle, Lifetime? snapshot = null, Lifetime? textures = null) { _valid = handle != null; _info = default; if (_valid) { IGSharp_DrawCmdInfo info; IGSharp_DrawCmd_GetInfo(handle, &info); _info = info; } _texture = new(_info.Texture, snapshot, (textures ?? snapshot)?.GpuTextures.GetValueOrDefault(_info.Texture.ID));
        var callback = (nint)_info.UserCallback;
        CallbackKind = callback == 0 ? DrawCallbackKind.None : DrawCallbackKind.User;
        if (callback != 0)
        {
            var platform = IGSharp_GetPlatformIO();
            if (callback == (nint)IGSharp_PlatformIO_GetDrawCallbackResetRenderState(platform)) CallbackKind = DrawCallbackKind.ResetRenderState;
            else if (callback == (nint)IGSharp_PlatformIO_GetDrawCallbackSetSamplerLinear(platform)) CallbackKind = DrawCallbackKind.SamplerLinear;
            else if (callback == (nint)IGSharp_PlatformIO_GetDrawCallbackSetSamplerNearest(platform)) CallbackKind = DrawCallbackKind.SamplerNearest;
            else if (callback == (nint)IGSharp_PlatformIO_GetDrawCallbackSetSamplerFromTex(platform)) CallbackKind = DrawCallbackKind.SamplerFromTexture;
        }
    }

    /// <summary>True if this handle refers to a valid draw command.</summary>
    public bool IsValid => _valid;

    /// <summary>Clip rectangle as (minX, minY, maxX, maxY), relative to the draw data's DisplayPos.</summary>
    public Vec4 ClipRect
    {
        get
        {
            var v = _info.ClipRect;
            return new Vec4(v.X, v.Y, v.Z, v.W);
        }
    }

    /// <summary>Texture id to bind for this command. Backend-specific (SDL_GPU: SDL_GPUTexture*).</summary>
    public ulong TextureId => _texture.Id;

    /// <summary>Guarded texture reference copied from the command.</summary>
    public TextureReference Texture => _texture;

    /// <summary>Start offset into the vertex buffer (used when DrawFlags.UseVtxOffset is enabled for large meshes).</summary>
    public uint VtxOffset => _info.VtxOffset;

    /// <summary>Start offset into the index buffer.</summary>
    public uint IdxOffset => _info.IdxOffset;

    /// <summary>Number of indices (triangle count * 3) to render for this command.</summary>
    public uint ElemCount => _info.ElemCount;

    /// <summary>True if this command carries a user callback (added via <see cref="DrawList.AddCallback"/>) instead of geometry.</summary>
    public bool HasUserCallback => _info.UserCallback != null;

    /// <summary>True if this command carries user callback data.</summary>
    public bool HasUserCallbackData => _info.UserCallbackData != null;

    /// <summary>Size in bytes of the callback data copied into the draw list (0 if the data was stored by pointer).</summary>
    public int UserCallbackDataSize => _info.UserCallbackDataSize;
}
