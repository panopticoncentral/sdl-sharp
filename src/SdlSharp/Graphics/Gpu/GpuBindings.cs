using SdlSharp.Native;

namespace SdlSharp.Graphics.Gpu;

/// <summary>A vertex or index buffer binding.</summary>
public readonly record struct GpuBufferBinding(GpuBuffer Buffer, uint Offset = 0)
{
    internal unsafe SDL_GPUBufferBinding ToNative() => new() { buffer = Buffer.Handle, offset = Offset };
}

/// <summary>A texture-sampler pair binding.</summary>
public readonly record struct GpuTextureSamplerBinding(GpuTexture Texture, GpuSampler Sampler)
{
    internal unsafe SDL_GPUTextureSamplerBinding ToNative() => new() { texture = Texture.Handle, sampler = Sampler.Handle };
}

/// <summary>A read-write storage buffer binding for compute passes.</summary>
public readonly record struct GpuStorageBufferReadWriteBinding(GpuBuffer Buffer, bool Cycle = false)
{
    internal unsafe SDL_GPUStorageBufferReadWriteBinding ToNative() =>
        new() { buffer = Buffer.Handle, cycle = (byte)(Cycle ? 1 : 0) };
}

/// <summary>A read-write storage texture binding for compute passes.</summary>
public readonly record struct GpuStorageTextureReadWriteBinding
{
    /// <summary>The texture to bind.</summary>
    public required GpuTexture Texture { get; init; }
    /// <summary>The mip level to bind.</summary>
    public uint MipLevel { get; init; }
    /// <summary>The layer to bind.</summary>
    public uint Layer { get; init; }
    /// <summary>Whether to cycle the texture to avoid stalls.</summary>
    public bool Cycle { get; init; }

    internal unsafe SDL_GPUStorageTextureReadWriteBinding ToNative() =>
        new() { texture = Texture.Handle, mip_level = MipLevel, layer = Layer, cycle = (byte)(Cycle ? 1 : 0) };
}

/// <summary>A color render target for a render pass.</summary>
public readonly record struct GpuColorTargetInfo
{
    /// <summary>The texture to render to.</summary>
    public required GpuTexture Texture { get; init; }
    /// <summary>The mip level to render to.</summary>
    public uint MipLevel { get; init; }
    /// <summary>The layer index or depth plane to render to.</summary>
    public uint LayerOrDepthPlane { get; init; }
    /// <summary>The clear color, used when <see cref="LoadOp"/> is <see cref="GpuLoadOp.Clear"/>.</summary>
    public FColor ClearColor { get; init; }
    /// <summary>How the target is loaded at the beginning of the pass.</summary>
    public GpuLoadOp LoadOp { get; init; }
    /// <summary>How the target is stored at the end of the pass.</summary>
    public GpuStoreOp StoreOp { get; init; }
    /// <summary>The MSAA resolve target, when <see cref="StoreOp"/> resolves.</summary>
    public GpuTexture? ResolveTexture { get; init; }
    /// <summary>The mip level of the resolve target.</summary>
    public uint ResolveMipLevel { get; init; }
    /// <summary>The layer of the resolve target.</summary>
    public uint ResolveLayer { get; init; }
    /// <summary>Whether to cycle the texture to avoid stalls.</summary>
    public bool Cycle { get; init; }
    /// <summary>Whether to cycle the resolve texture to avoid stalls.</summary>
    public bool CycleResolveTexture { get; init; }

    internal unsafe SDL_GPUColorTargetInfo ToNative() => new()
    {
        texture = Texture.Handle,
        mip_level = MipLevel,
        layer_or_depth_plane = LayerOrDepthPlane,
        clear_color = ClearColor.ToNative(),
        load_op = (SDL_GPULoadOp)LoadOp,
        store_op = (SDL_GPUStoreOp)StoreOp,
        resolve_texture = ResolveTexture is { } resolve ? resolve.Handle : null,
        resolve_mip_level = ResolveMipLevel,
        resolve_layer = ResolveLayer,
        cycle = (byte)(Cycle ? 1 : 0),
        cycle_resolve_texture = (byte)(CycleResolveTexture ? 1 : 0),
    };
}

/// <summary>The depth/stencil render target for a render pass.</summary>
public readonly record struct GpuDepthStencilTargetInfo
{
    /// <summary>The texture to use as the depth/stencil target.</summary>
    public required GpuTexture Texture { get; init; }
    /// <summary>The clear depth value, used when <see cref="LoadOp"/> is <see cref="GpuLoadOp.Clear"/>.</summary>
    public float ClearDepth { get; init; }
    /// <summary>How the depth is loaded at the beginning of the pass.</summary>
    public GpuLoadOp LoadOp { get; init; }
    /// <summary>How the depth is stored at the end of the pass.</summary>
    public GpuStoreOp StoreOp { get; init; }
    /// <summary>How the stencil is loaded at the beginning of the pass.</summary>
    public GpuLoadOp StencilLoadOp { get; init; }
    /// <summary>How the stencil is stored at the end of the pass.</summary>
    public GpuStoreOp StencilStoreOp { get; init; }
    /// <summary>Whether to cycle the texture to avoid stalls.</summary>
    public bool Cycle { get; init; }
    /// <summary>The clear stencil value, used when <see cref="StencilLoadOp"/> is <see cref="GpuLoadOp.Clear"/>.</summary>
    public byte ClearStencil { get; init; }
    /// <summary>The mip level to use.</summary>
    public byte MipLevel { get; init; }
    /// <summary>The layer to use.</summary>
    public byte Layer { get; init; }

    internal unsafe SDL_GPUDepthStencilTargetInfo ToNative() => new()
    {
        texture = Texture.Handle,
        clear_depth = ClearDepth,
        load_op = (SDL_GPULoadOp)LoadOp,
        store_op = (SDL_GPUStoreOp)StoreOp,
        stencil_load_op = (SDL_GPULoadOp)StencilLoadOp,
        stencil_store_op = (SDL_GPUStoreOp)StencilStoreOp,
        cycle = (byte)(Cycle ? 1 : 0),
        clear_stencil = ClearStencil,
        mip_level = MipLevel,
        layer = Layer,
    };
}

/// <summary>A viewport for a render pass.</summary>
public readonly record struct GpuViewport(float X, float Y, float W, float H, float MinDepth = 0f, float MaxDepth = 1f)
{
    internal SDL_GPUViewport ToNative() => new() { x = X, y = Y, w = W, h = H, min_depth = MinDepth, max_depth = MaxDepth };
}

/// <summary>Transfer-buffer-side parameters of a texture upload/download.</summary>
public readonly record struct GpuTextureTransferInfo(GpuTransferBuffer TransferBuffer, uint Offset = 0, uint PixelsPerRow = 0, uint RowsPerLayer = 0)
{
    internal unsafe SDL_GPUTextureTransferInfo ToNative() =>
        new() { transfer_buffer = TransferBuffer.Handle, offset = Offset, pixels_per_row = PixelsPerRow, rows_per_layer = RowsPerLayer };
}

/// <summary>A location in a transfer buffer.</summary>
public readonly record struct GpuTransferBufferLocation(GpuTransferBuffer TransferBuffer, uint Offset = 0)
{
    internal unsafe SDL_GPUTransferBufferLocation ToNative() => new() { transfer_buffer = TransferBuffer.Handle, offset = Offset };
}

/// <summary>A location in a texture.</summary>
public readonly record struct GpuTextureLocation(GpuTexture Texture, uint MipLevel = 0, uint Layer = 0, uint X = 0, uint Y = 0, uint Z = 0)
{
    internal unsafe SDL_GPUTextureLocation ToNative() =>
        new() { texture = Texture.Handle, mip_level = MipLevel, layer = Layer, x = X, y = Y, z = Z };
}

/// <summary>A region of a texture.</summary>
public readonly record struct GpuTextureRegion
{
    /// <summary>Creates a texture region. <see cref="D"/> defaults to 1.</summary>
    public GpuTextureRegion() { }

    /// <summary>The texture.</summary>
    public required GpuTexture Texture { get; init; }
    /// <summary>The mip level.</summary>
    public uint MipLevel { get; init; }
    /// <summary>The layer.</summary>
    public uint Layer { get; init; }
    /// <summary>Left edge of the region.</summary>
    public uint X { get; init; }
    /// <summary>Top edge of the region.</summary>
    public uint Y { get; init; }
    /// <summary>Front edge of the region (3D textures).</summary>
    public uint Z { get; init; }
    /// <summary>Width of the region.</summary>
    public uint W { get; init; }
    /// <summary>Height of the region.</summary>
    public uint H { get; init; }
    /// <summary>Depth of the region.</summary>
    public uint D { get; init; } = 1;

    internal unsafe SDL_GPUTextureRegion ToNative() =>
        new() { texture = Texture.Handle, mip_level = MipLevel, layer = Layer, x = X, y = Y, z = Z, w = W, h = H, d = D };
}

/// <summary>A blit source or destination region.</summary>
public readonly record struct GpuBlitRegion
{
    /// <summary>The texture.</summary>
    public required GpuTexture Texture { get; init; }
    /// <summary>The mip level.</summary>
    public uint MipLevel { get; init; }
    /// <summary>The layer index or depth plane.</summary>
    public uint LayerOrDepthPlane { get; init; }
    /// <summary>Left edge of the region.</summary>
    public uint X { get; init; }
    /// <summary>Top edge of the region.</summary>
    public uint Y { get; init; }
    /// <summary>Width of the region.</summary>
    public uint W { get; init; }
    /// <summary>Height of the region.</summary>
    public uint H { get; init; }

    internal unsafe SDL_GPUBlitRegion ToNative() =>
        new() { texture = Texture.Handle, mip_level = MipLevel, layer_or_depth_plane = LayerOrDepthPlane, x = X, y = Y, w = W, h = H };
}

/// <summary>A location in a GPU buffer.</summary>
public readonly record struct GpuBufferLocation(GpuBuffer Buffer, uint Offset = 0)
{
    internal unsafe SDL_GPUBufferLocation ToNative() => new() { buffer = Buffer.Handle, offset = Offset };
}

/// <summary>A region of a GPU buffer.</summary>
public readonly record struct GpuBufferRegion(GpuBuffer Buffer, uint Offset, uint Size)
{
    internal unsafe SDL_GPUBufferRegion ToNative() => new() { buffer = Buffer.Handle, offset = Offset, size = Size };
}

/// <summary>Parameters for a texture blit.</summary>
public readonly record struct GpuBlitInfo
{
    /// <summary>The source region.</summary>
    public required GpuBlitRegion Source { get; init; }
    /// <summary>The destination region.</summary>
    public required GpuBlitRegion Destination { get; init; }
    /// <summary>How the destination is loaded before the blit.</summary>
    public GpuLoadOp LoadOp { get; init; }
    /// <summary>The clear color, used when <see cref="LoadOp"/> is <see cref="GpuLoadOp.Clear"/>.</summary>
    public FColor ClearColor { get; init; }
    /// <summary>Optional flip applied during the blit.</summary>
    public FlipMode FlipMode { get; init; }
    /// <summary>The filter used when scaling.</summary>
    public GpuFilter Filter { get; init; }
    /// <summary>Whether to cycle the destination texture to avoid stalls.</summary>
    public bool Cycle { get; init; }

    internal unsafe SDL_GPUBlitInfo ToNative() => new()
    {
        source = Source.ToNative(),
        destination = Destination.ToNative(),
        load_op = (SDL_GPULoadOp)LoadOp,
        clear_color = ClearColor.ToNative(),
        flip_mode = (SDL_FlipMode)FlipMode,
        filter = (SDL_GPUFilter)Filter,
        cycle = (byte)(Cycle ? 1 : 0),
    };
}
