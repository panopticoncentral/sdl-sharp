# GPU Managed Descriptor Layer Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Remove every `SdlSharp.Native` type from the public `SdlSharp.Graphics.Gpu` API by introducing managed descriptor/binding structs, a non-owning swapchain `GpuTexture`, and managed wrappers for the 8 native-only GPU functions.

**Architecture:** Managed `record struct`s mirror the SDL structs 1:1 (spec: `docs/superpowers/specs/2026-07-02-gpu-managed-descriptors-design.md`). Each has an `internal ToNative()` (same pattern as `Rectangle.ToNative()` / `FColor.ToNative()`). Hot-path methods take `ReadOnlySpan<T>` and stackalloc the native arrays; cold-path pipeline creation pins arrays with `fixed`. The old pointer-based public methods are deleted outright.

**Tech Stack:** C# 13 / .NET 10, `LibraryImport` P/Invoke (already in `src/SdlSharp/Native/Gpu.cs` — this plan adds **no** native bindings, only managed surface).

**Testing:** This repo has no unit-test project; the established verification gates (CLAUDE.md) are: `dotnet build` with 0 warnings/0 errors, running the samples, and — specific to this phase — a grep gate proving no `SDL_`-prefixed type appears in any public signature under `src/SdlSharp/Graphics/Gpu/`. Where a step says "Build", run the exact command shown and require `0 Warning(s), 0 Error(s)` in the output.

**Build commands:**
- Library only (tasks 1–9): `dotnet build src/SdlSharp/SdlSharp.csproj`
- Whole solution (task 10 on): `dotnet build SdlSharp.slnx`

Intermediate tasks build only the library because `Samples/ImGuiDemo` uses the old signatures until task 10 migrates it.

**Git:** Commit after every task with the message shown. Do NOT add a Co-Authored-By line (user preference).

---

### Task 1: Public GPU enums

**Files:**
- Modify: `src/SdlSharp/Graphics/Gpu/GpuEnums.cs` (append at end of file)

- [ ] **Step 1.1: Append 18 public enums to `GpuEnums.cs`**

Follow the file's existing pattern (each member cast from the `Native` member; match the doc-comment style of the existing enums in that file). Append:

```csharp
/// <summary>How a render target is loaded at the beginning of a render pass.</summary>
public enum GpuLoadOp
{
    Load = (int)Native.SDL_GPULoadOp.SDL_GPU_LOADOP_LOAD,
    Clear = (int)Native.SDL_GPULoadOp.SDL_GPU_LOADOP_CLEAR,
    DontCare = (int)Native.SDL_GPULoadOp.SDL_GPU_LOADOP_DONT_CARE,
}

/// <summary>How a render target is stored at the end of a render pass.</summary>
public enum GpuStoreOp
{
    Store = (int)Native.SDL_GPUStoreOp.SDL_GPU_STOREOP_STORE,
    DontCare = (int)Native.SDL_GPUStoreOp.SDL_GPU_STOREOP_DONT_CARE,
    Resolve = (int)Native.SDL_GPUStoreOp.SDL_GPU_STOREOP_RESOLVE,
    ResolveAndStore = (int)Native.SDL_GPUStoreOp.SDL_GPU_STOREOP_RESOLVE_AND_STORE,
}

/// <summary>Shader pipeline stage.</summary>
public enum GpuShaderStage
{
    Vertex = (int)Native.SDL_GPUShaderStage.SDL_GPU_SHADERSTAGE_VERTEX,
    Fragment = (int)Native.SDL_GPUShaderStage.SDL_GPU_SHADERSTAGE_FRAGMENT,
}

/// <summary>Transfer buffer usage.</summary>
public enum GpuTransferBufferUsage
{
    Upload = (int)Native.SDL_GPUTransferBufferUsage.SDL_GPU_TRANSFERBUFFERUSAGE_UPLOAD,
    Download = (int)Native.SDL_GPUTransferBufferUsage.SDL_GPU_TRANSFERBUFFERUSAGE_DOWNLOAD,
}

/// <summary>Cube map face index.</summary>
public enum GpuCubeMapFace
{
    PositiveX = (int)Native.SDL_GPUCubeMapFace.SDL_GPU_CUBEMAPFACE_POSITIVEX,
    NegativeX = (int)Native.SDL_GPUCubeMapFace.SDL_GPU_CUBEMAPFACE_NEGATIVEX,
    PositiveY = (int)Native.SDL_GPUCubeMapFace.SDL_GPU_CUBEMAPFACE_POSITIVEY,
    NegativeY = (int)Native.SDL_GPUCubeMapFace.SDL_GPU_CUBEMAPFACE_NEGATIVEY,
    PositiveZ = (int)Native.SDL_GPUCubeMapFace.SDL_GPU_CUBEMAPFACE_POSITIVEZ,
    NegativeZ = (int)Native.SDL_GPUCubeMapFace.SDL_GPU_CUBEMAPFACE_NEGATIVEZ,
}

/// <summary>Vertex element data format.</summary>
public enum GpuVertexElementFormat
{
    Invalid = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_INVALID,
    Int = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_INT,
    Int2 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_INT2,
    Int3 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_INT3,
    Int4 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_INT4,
    Uint = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UINT,
    Uint2 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UINT2,
    Uint3 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UINT3,
    Uint4 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UINT4,
    Float = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_FLOAT,
    Float2 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_FLOAT2,
    Float3 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_FLOAT3,
    Float4 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_FLOAT4,
    Byte2 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_BYTE2,
    Byte4 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_BYTE4,
    Ubyte2 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2,
    Ubyte4 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4,
    Byte2Norm = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_BYTE2_NORM,
    Byte4Norm = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_BYTE4_NORM,
    Ubyte2Norm = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2_NORM,
    Ubyte4Norm = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4_NORM,
    Short2 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_SHORT2,
    Short4 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_SHORT4,
    Ushort2 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_USHORT2,
    Ushort4 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_USHORT4,
    Short2Norm = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_SHORT2_NORM,
    Short4Norm = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_SHORT4_NORM,
    Ushort2Norm = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_USHORT2_NORM,
    Ushort4Norm = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_USHORT4_NORM,
    Half2 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_HALF2,
    Half4 = (int)Native.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_HALF4,
}

/// <summary>Vertex input rate.</summary>
public enum GpuVertexInputRate
{
    Vertex = (int)Native.SDL_GPUVertexInputRate.SDL_GPU_VERTEXINPUTRATE_VERTEX,
    Instance = (int)Native.SDL_GPUVertexInputRate.SDL_GPU_VERTEXINPUTRATE_INSTANCE,
}

/// <summary>Polygon fill mode.</summary>
public enum GpuFillMode
{
    Fill = (int)Native.SDL_GPUFillMode.SDL_GPU_FILLMODE_FILL,
    Line = (int)Native.SDL_GPUFillMode.SDL_GPU_FILLMODE_LINE,
}

/// <summary>Face culling mode.</summary>
public enum GpuCullMode
{
    None = (int)Native.SDL_GPUCullMode.SDL_GPU_CULLMODE_NONE,
    Front = (int)Native.SDL_GPUCullMode.SDL_GPU_CULLMODE_FRONT,
    Back = (int)Native.SDL_GPUCullMode.SDL_GPU_CULLMODE_BACK,
}

/// <summary>Front face winding order.</summary>
public enum GpuFrontFace
{
    CounterClockwise = (int)Native.SDL_GPUFrontFace.SDL_GPU_FRONTFACE_COUNTER_CLOCKWISE,
    Clockwise = (int)Native.SDL_GPUFrontFace.SDL_GPU_FRONTFACE_CLOCKWISE,
}

/// <summary>Comparison operator.</summary>
public enum GpuCompareOp
{
    Invalid = (int)Native.SDL_GPUCompareOp.SDL_GPU_COMPAREOP_INVALID,
    Never = (int)Native.SDL_GPUCompareOp.SDL_GPU_COMPAREOP_NEVER,
    Less = (int)Native.SDL_GPUCompareOp.SDL_GPU_COMPAREOP_LESS,
    Equal = (int)Native.SDL_GPUCompareOp.SDL_GPU_COMPAREOP_EQUAL,
    LessOrEqual = (int)Native.SDL_GPUCompareOp.SDL_GPU_COMPAREOP_LESS_OR_EQUAL,
    Greater = (int)Native.SDL_GPUCompareOp.SDL_GPU_COMPAREOP_GREATER,
    NotEqual = (int)Native.SDL_GPUCompareOp.SDL_GPU_COMPAREOP_NOT_EQUAL,
    GreaterOrEqual = (int)Native.SDL_GPUCompareOp.SDL_GPU_COMPAREOP_GREATER_OR_EQUAL,
    Always = (int)Native.SDL_GPUCompareOp.SDL_GPU_COMPAREOP_ALWAYS,
}

/// <summary>Stencil operation.</summary>
public enum GpuStencilOp
{
    Invalid = (int)Native.SDL_GPUStencilOp.SDL_GPU_STENCILOP_INVALID,
    Keep = (int)Native.SDL_GPUStencilOp.SDL_GPU_STENCILOP_KEEP,
    Zero = (int)Native.SDL_GPUStencilOp.SDL_GPU_STENCILOP_ZERO,
    Replace = (int)Native.SDL_GPUStencilOp.SDL_GPU_STENCILOP_REPLACE,
    IncrementAndClamp = (int)Native.SDL_GPUStencilOp.SDL_GPU_STENCILOP_INCREMENT_AND_CLAMP,
    DecrementAndClamp = (int)Native.SDL_GPUStencilOp.SDL_GPU_STENCILOP_DECREMENT_AND_CLAMP,
    Invert = (int)Native.SDL_GPUStencilOp.SDL_GPU_STENCILOP_INVERT,
    IncrementAndWrap = (int)Native.SDL_GPUStencilOp.SDL_GPU_STENCILOP_INCREMENT_AND_WRAP,
    DecrementAndWrap = (int)Native.SDL_GPUStencilOp.SDL_GPU_STENCILOP_DECREMENT_AND_WRAP,
}

/// <summary>Blend operation.</summary>
public enum GpuBlendOp
{
    Invalid = (int)Native.SDL_GPUBlendOp.SDL_GPU_BLENDOP_INVALID,
    Add = (int)Native.SDL_GPUBlendOp.SDL_GPU_BLENDOP_ADD,
    Subtract = (int)Native.SDL_GPUBlendOp.SDL_GPU_BLENDOP_SUBTRACT,
    ReverseSubtract = (int)Native.SDL_GPUBlendOp.SDL_GPU_BLENDOP_REVERSE_SUBTRACT,
    Min = (int)Native.SDL_GPUBlendOp.SDL_GPU_BLENDOP_MIN,
    Max = (int)Native.SDL_GPUBlendOp.SDL_GPU_BLENDOP_MAX,
}

/// <summary>Blend factor.</summary>
public enum GpuBlendFactor
{
    Invalid = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_INVALID,
    Zero = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ZERO,
    One = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ONE,
    SrcColor = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_SRC_COLOR,
    OneMinusSrcColor = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_COLOR,
    DstColor = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_DST_COLOR,
    OneMinusDstColor = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_COLOR,
    SrcAlpha = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_SRC_ALPHA,
    OneMinusSrcAlpha = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_ALPHA,
    DstAlpha = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_DST_ALPHA,
    OneMinusDstAlpha = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_ALPHA,
    ConstantColor = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_CONSTANT_COLOR,
    OneMinusConstantColor = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ONE_MINUS_CONSTANT_COLOR,
    SrcAlphaSaturate = (int)Native.SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_SRC_ALPHA_SATURATE,
}

/// <summary>Texture filter mode.</summary>
public enum GpuFilter
{
    Nearest = (int)Native.SDL_GPUFilter.SDL_GPU_FILTER_NEAREST,
    Linear = (int)Native.SDL_GPUFilter.SDL_GPU_FILTER_LINEAR,
}

/// <summary>Mipmap filter mode.</summary>
public enum GpuSamplerMipmapMode
{
    Nearest = (int)Native.SDL_GPUSamplerMipmapMode.SDL_GPU_SAMPLERMIPMAPMODE_NEAREST,
    Linear = (int)Native.SDL_GPUSamplerMipmapMode.SDL_GPU_SAMPLERMIPMAPMODE_LINEAR,
}

/// <summary>Sampler address (wrap) mode.</summary>
public enum GpuSamplerAddressMode
{
    Repeat = (int)Native.SDL_GPUSamplerAddressMode.SDL_GPU_SAMPLERADDRESSMODE_REPEAT,
    MirroredRepeat = (int)Native.SDL_GPUSamplerAddressMode.SDL_GPU_SAMPLERADDRESSMODE_MIRRORED_REPEAT,
    ClampToEdge = (int)Native.SDL_GPUSamplerAddressMode.SDL_GPU_SAMPLERADDRESSMODE_CLAMP_TO_EDGE,
}

/// <summary>Color component write mask flags.</summary>
[Flags]
public enum GpuColorComponentFlags : byte
{
    R = (byte)Native.SDL_GPUColorComponentFlags.SDL_GPU_COLORCOMPONENT_R,
    G = (byte)Native.SDL_GPUColorComponentFlags.SDL_GPU_COLORCOMPONENT_G,
    B = (byte)Native.SDL_GPUColorComponentFlags.SDL_GPU_COLORCOMPONENT_B,
    A = (byte)Native.SDL_GPUColorComponentFlags.SDL_GPU_COLORCOMPONENT_A,
}
```

Note: the `(int)`/`(byte)` casts are required — enum members cannot reference another enum's members without one. This matches the existing pattern in `GpuEnums.cs` and CLAUDE.md.

- [ ] **Step 1.2: Build**

Run: `dotnet build src/SdlSharp/SdlSharp.csproj`
Expected: `0 Warning(s), 0 Error(s)`

- [ ] **Step 1.3: Commit**

```bash
git add src/SdlSharp/Graphics/Gpu/GpuEnums.cs
git commit -m "Add public GPU enums for descriptor/binding structs"
```

---

### Task 2: Hot-path binding structs (`GpuBindings.cs`)

**Files:**
- Create: `src/SdlSharp/Graphics/Gpu/GpuBindings.cs`

- [ ] **Step 2.1: Create the file with this exact content**

```csharp
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
```

Note: `FlipMode` is the existing public enum in `src/SdlSharp/Graphics/FlipMode.cs`; check the cast it uses to reach `SDL_FlipMode` and match it. If `FColor.ToNative()` has a different accessibility, adjust — it exists at `src/SdlSharp/Graphics/FColor.cs:15` as `internal`.

- [ ] **Step 2.2: Build**

Run: `dotnet build src/SdlSharp/SdlSharp.csproj`
Expected: `0 Warning(s), 0 Error(s)`

- [ ] **Step 2.3: Commit**

```bash
git add src/SdlSharp/Graphics/Gpu/GpuBindings.cs
git commit -m "Add managed hot-path GPU binding structs"
```

---

### Task 3: Creation-time descriptor structs (`GpuDescriptors.cs`)

**Files:**
- Create: `src/SdlSharp/Graphics/Gpu/GpuDescriptors.cs`

- [ ] **Step 3.1: Create the file with this exact content**

```csharp
using System.Runtime.InteropServices;
using SdlSharp.Native;

namespace SdlSharp.Graphics.Gpu;

/// <summary>Sampler creation parameters.</summary>
public readonly record struct GpuSamplerCreateInfo
{
    /// <summary>The minification filter.</summary>
    public GpuFilter MinFilter { get; init; }
    /// <summary>The magnification filter.</summary>
    public GpuFilter MagFilter { get; init; }
    /// <summary>The mipmap filter.</summary>
    public GpuSamplerMipmapMode MipmapMode { get; init; }
    /// <summary>The U (horizontal) address mode.</summary>
    public GpuSamplerAddressMode AddressModeU { get; init; }
    /// <summary>The V (vertical) address mode.</summary>
    public GpuSamplerAddressMode AddressModeV { get; init; }
    /// <summary>The W (depth) address mode.</summary>
    public GpuSamplerAddressMode AddressModeW { get; init; }
    /// <summary>The mip LOD bias.</summary>
    public float MipLodBias { get; init; }
    /// <summary>The maximum anisotropy, used when <see cref="EnableAnisotropy"/> is true.</summary>
    public float MaxAnisotropy { get; init; }
    /// <summary>The comparison operator, used when <see cref="EnableCompare"/> is true.</summary>
    public GpuCompareOp CompareOp { get; init; }
    /// <summary>The minimum LOD.</summary>
    public float MinLod { get; init; }
    /// <summary>The maximum LOD.</summary>
    public float MaxLod { get; init; }
    /// <summary>Whether anisotropic filtering is enabled.</summary>
    public bool EnableAnisotropy { get; init; }
    /// <summary>Whether depth comparison is enabled.</summary>
    public bool EnableCompare { get; init; }
    /// <summary>Optional extra properties.</summary>
    public PropertyGroup? Props { get; init; }

    internal SDL_GPUSamplerCreateInfo ToNative() => new()
    {
        min_filter = (SDL_GPUFilter)MinFilter,
        mag_filter = (SDL_GPUFilter)MagFilter,
        mipmap_mode = (SDL_GPUSamplerMipmapMode)MipmapMode,
        address_mode_u = (SDL_GPUSamplerAddressMode)AddressModeU,
        address_mode_v = (SDL_GPUSamplerAddressMode)AddressModeV,
        address_mode_w = (SDL_GPUSamplerAddressMode)AddressModeW,
        mip_lod_bias = MipLodBias,
        max_anisotropy = MaxAnisotropy,
        compare_op = (SDL_GPUCompareOp)CompareOp,
        min_lod = MinLod,
        max_lod = MaxLod,
        enable_anisotropy = (byte)(EnableAnisotropy ? 1 : 0),
        enable_compare = (byte)(EnableCompare ? 1 : 0),
        props = Props?.Id ?? default,
    };
}

/// <summary>Texture creation parameters.</summary>
public readonly record struct GpuTextureCreateInfo
{
    /// <summary>Creates texture creation info. <see cref="LayerCountOrDepth"/> and <see cref="NumLevels"/> default to 1.</summary>
    public GpuTextureCreateInfo() { }

    /// <summary>The texture dimensionality type.</summary>
    public GpuTextureType Type { get; init; }
    /// <summary>The pixel format.</summary>
    public required GpuTextureFormat Format { get; init; }
    /// <summary>The intended usage flags.</summary>
    public required GpuTextureUsage Usage { get; init; }
    /// <summary>The width in pixels.</summary>
    public required uint Width { get; init; }
    /// <summary>The height in pixels.</summary>
    public required uint Height { get; init; }
    /// <summary>The layer count (array textures) or depth (3D textures).</summary>
    public uint LayerCountOrDepth { get; init; } = 1;
    /// <summary>The number of mip levels.</summary>
    public uint NumLevels { get; init; } = 1;
    /// <summary>The multisample count (render targets only).</summary>
    public GpuSampleCount SampleCount { get; init; }
    /// <summary>Optional extra properties.</summary>
    public PropertyGroup? Props { get; init; }

    internal SDL_GPUTextureCreateInfo ToNative() => new()
    {
        type = (SDL_GPUTextureType)Type,
        format = (SDL_GPUTextureFormat)Format,
        usage = (SDL_GPUTextureUsageFlags)Usage,
        width = Width,
        height = Height,
        layer_count_or_depth = LayerCountOrDepth,
        num_levels = NumLevels,
        sample_count = (SDL_GPUSampleCount)SampleCount,
        props = Props?.Id ?? default,
    };
}

/// <summary>Buffer creation parameters.</summary>
public readonly record struct GpuBufferCreateInfo(GpuBufferUsage Usage, uint Size)
{
    /// <summary>Optional extra properties.</summary>
    public PropertyGroup? Props { get; init; }

    internal SDL_GPUBufferCreateInfo ToNative() =>
        new() { usage = (SDL_GPUBufferUsageFlags)Usage, size = Size, props = Props?.Id ?? default };
}

/// <summary>Transfer buffer creation parameters.</summary>
public readonly record struct GpuTransferBufferCreateInfo(GpuTransferBufferUsage Usage, uint Size)
{
    /// <summary>Optional extra properties.</summary>
    public PropertyGroup? Props { get; init; }

    internal SDL_GPUTransferBufferCreateInfo ToNative() =>
        new() { usage = (SDL_GPUTransferBufferUsage)Usage, size = Size, props = Props?.Id ?? default };
}

/// <summary>Shader creation parameters.</summary>
public readonly record struct GpuShaderCreateInfo
{
    /// <summary>The compiled shader code.</summary>
    public required ReadOnlyMemory<byte> Code { get; init; }
    /// <summary>The shader entry point; defaults to "main" when null.</summary>
    public string? EntryPoint { get; init; }
    /// <summary>The format of <see cref="Code"/>.</summary>
    public required GpuShaderFormat Format { get; init; }
    /// <summary>The pipeline stage this shader is for.</summary>
    public required GpuShaderStage Stage { get; init; }
    /// <summary>The number of sampler slots the shader uses.</summary>
    public uint NumSamplers { get; init; }
    /// <summary>The number of storage texture slots the shader uses.</summary>
    public uint NumStorageTextures { get; init; }
    /// <summary>The number of storage buffer slots the shader uses.</summary>
    public uint NumStorageBuffers { get; init; }
    /// <summary>The number of uniform buffer slots the shader uses.</summary>
    public uint NumUniformBuffers { get; init; }
    /// <summary>Optional extra properties.</summary>
    public PropertyGroup? Props { get; init; }
}

/// <summary>Compute pipeline creation parameters.</summary>
public readonly record struct GpuComputePipelineCreateInfo
{
    /// <summary>The compiled compute shader code.</summary>
    public required ReadOnlyMemory<byte> Code { get; init; }
    /// <summary>The shader entry point; defaults to "main" when null.</summary>
    public string? EntryPoint { get; init; }
    /// <summary>The format of <see cref="Code"/>.</summary>
    public required GpuShaderFormat Format { get; init; }
    /// <summary>The number of sampler slots the shader uses.</summary>
    public uint NumSamplers { get; init; }
    /// <summary>The number of read-only storage texture slots.</summary>
    public uint NumReadonlyStorageTextures { get; init; }
    /// <summary>The number of read-only storage buffer slots.</summary>
    public uint NumReadonlyStorageBuffers { get; init; }
    /// <summary>The number of read-write storage texture slots.</summary>
    public uint NumReadwriteStorageTextures { get; init; }
    /// <summary>The number of read-write storage buffer slots.</summary>
    public uint NumReadwriteStorageBuffers { get; init; }
    /// <summary>The number of uniform buffer slots.</summary>
    public uint NumUniformBuffers { get; init; }
    /// <summary>The workgroup size in the X dimension.</summary>
    public required uint ThreadcountX { get; init; }
    /// <summary>The workgroup size in the Y dimension.</summary>
    public required uint ThreadcountY { get; init; }
    /// <summary>The workgroup size in the Z dimension.</summary>
    public required uint ThreadcountZ { get; init; }
    /// <summary>Optional extra properties.</summary>
    public PropertyGroup? Props { get; init; }
}

/// <summary>A vertex buffer slot description for pipeline creation.</summary>
public readonly record struct GpuVertexBufferDescription(uint Slot, uint Pitch, GpuVertexInputRate InputRate = GpuVertexInputRate.Vertex, uint InstanceStepRate = 0)
{
    internal SDL_GPUVertexBufferDescription ToNative() =>
        new() { slot = Slot, pitch = Pitch, input_rate = (SDL_GPUVertexInputRate)InputRate, instance_step_rate = InstanceStepRate };
}

/// <summary>A vertex attribute description for pipeline creation.</summary>
public readonly record struct GpuVertexAttribute(uint Location, uint BufferSlot, GpuVertexElementFormat Format, uint Offset)
{
    internal SDL_GPUVertexAttribute ToNative() =>
        new() { location = Location, buffer_slot = BufferSlot, format = (SDL_GPUVertexElementFormat)Format, offset = Offset };
}

/// <summary>Vertex input state for pipeline creation.</summary>
public readonly record struct GpuVertexInputState
{
    /// <summary>The vertex buffer slot descriptions.</summary>
    public GpuVertexBufferDescription[]? VertexBufferDescriptions { get; init; }
    /// <summary>The vertex attribute descriptions.</summary>
    public GpuVertexAttribute[]? VertexAttributes { get; init; }
}

/// <summary>Per-face stencil operation state.</summary>
public readonly record struct GpuStencilOpState(GpuStencilOp FailOp, GpuStencilOp PassOp, GpuStencilOp DepthFailOp, GpuCompareOp CompareOp)
{
    internal SDL_GPUStencilOpState ToNative() => new()
    {
        fail_op = (SDL_GPUStencilOp)FailOp,
        pass_op = (SDL_GPUStencilOp)PassOp,
        depth_fail_op = (SDL_GPUStencilOp)DepthFailOp,
        compare_op = (SDL_GPUCompareOp)CompareOp,
    };
}

/// <summary>Color target blend state for pipeline creation.</summary>
public readonly record struct GpuColorTargetBlendState
{
    /// <summary>The blend factor applied to the source color.</summary>
    public GpuBlendFactor SrcColorBlendFactor { get; init; }
    /// <summary>The blend factor applied to the destination color.</summary>
    public GpuBlendFactor DstColorBlendFactor { get; init; }
    /// <summary>The blend operation for color.</summary>
    public GpuBlendOp ColorBlendOp { get; init; }
    /// <summary>The blend factor applied to the source alpha.</summary>
    public GpuBlendFactor SrcAlphaBlendFactor { get; init; }
    /// <summary>The blend factor applied to the destination alpha.</summary>
    public GpuBlendFactor DstAlphaBlendFactor { get; init; }
    /// <summary>The blend operation for alpha.</summary>
    public GpuBlendOp AlphaBlendOp { get; init; }
    /// <summary>The color component write mask, used when <see cref="EnableColorWriteMask"/> is true.</summary>
    public GpuColorComponentFlags ColorWriteMask { get; init; }
    /// <summary>Whether blending is enabled.</summary>
    public bool EnableBlend { get; init; }
    /// <summary>Whether the color write mask is applied.</summary>
    public bool EnableColorWriteMask { get; init; }

    /// <summary>No blending; source overwrites destination.</summary>
    public static GpuColorTargetBlendState Disabled => default;

    /// <summary>Standard alpha blending (src alpha, one-minus-src-alpha).</summary>
    public static GpuColorTargetBlendState AlphaBlend => new()
    {
        EnableBlend = true,
        SrcColorBlendFactor = GpuBlendFactor.SrcAlpha,
        DstColorBlendFactor = GpuBlendFactor.OneMinusSrcAlpha,
        ColorBlendOp = GpuBlendOp.Add,
        SrcAlphaBlendFactor = GpuBlendFactor.One,
        DstAlphaBlendFactor = GpuBlendFactor.OneMinusSrcAlpha,
        AlphaBlendOp = GpuBlendOp.Add,
    };

    /// <summary>Premultiplied alpha blending (one, one-minus-src-alpha).</summary>
    public static GpuColorTargetBlendState PremultipliedAlpha => new()
    {
        EnableBlend = true,
        SrcColorBlendFactor = GpuBlendFactor.One,
        DstColorBlendFactor = GpuBlendFactor.OneMinusSrcAlpha,
        ColorBlendOp = GpuBlendOp.Add,
        SrcAlphaBlendFactor = GpuBlendFactor.One,
        DstAlphaBlendFactor = GpuBlendFactor.OneMinusSrcAlpha,
        AlphaBlendOp = GpuBlendOp.Add,
    };

    internal SDL_GPUColorTargetBlendState ToNative() => new()
    {
        src_color_blendfactor = (SDL_GPUBlendFactor)SrcColorBlendFactor,
        dst_color_blendfactor = (SDL_GPUBlendFactor)DstColorBlendFactor,
        color_blend_op = (SDL_GPUBlendOp)ColorBlendOp,
        src_alpha_blendfactor = (SDL_GPUBlendFactor)SrcAlphaBlendFactor,
        dst_alpha_blendfactor = (SDL_GPUBlendFactor)DstAlphaBlendFactor,
        alpha_blend_op = (SDL_GPUBlendOp)AlphaBlendOp,
        color_write_mask = (SDL_GPUColorComponentFlags)ColorWriteMask,
        enable_blend = (byte)(EnableBlend ? 1 : 0),
        enable_color_write_mask = (byte)(EnableColorWriteMask ? 1 : 0),
    };
}

/// <summary>Rasterizer state for pipeline creation.</summary>
public readonly record struct GpuRasterizerState
{
    /// <summary>The polygon fill mode.</summary>
    public GpuFillMode FillMode { get; init; }
    /// <summary>The face culling mode.</summary>
    public GpuCullMode CullMode { get; init; }
    /// <summary>The front face winding order.</summary>
    public GpuFrontFace FrontFace { get; init; }
    /// <summary>The constant depth bias factor.</summary>
    public float DepthBiasConstantFactor { get; init; }
    /// <summary>The maximum depth bias.</summary>
    public float DepthBiasClamp { get; init; }
    /// <summary>The slope-scaled depth bias factor.</summary>
    public float DepthBiasSlopeFactor { get; init; }
    /// <summary>Whether depth biasing is enabled.</summary>
    public bool EnableDepthBias { get; init; }
    /// <summary>Whether depth clipping is enabled.</summary>
    public bool EnableDepthClip { get; init; }

    /// <summary>Filled polygons, no culling, counter-clockwise front faces.</summary>
    public static GpuRasterizerState Default => default;

    internal SDL_GPURasterizerState ToNative() => new()
    {
        fill_mode = (SDL_GPUFillMode)FillMode,
        cull_mode = (SDL_GPUCullMode)CullMode,
        front_face = (SDL_GPUFrontFace)FrontFace,
        depth_bias_constant_factor = DepthBiasConstantFactor,
        depth_bias_clamp = DepthBiasClamp,
        depth_bias_slope_factor = DepthBiasSlopeFactor,
        enable_depth_bias = (byte)(EnableDepthBias ? 1 : 0),
        enable_depth_clip = (byte)(EnableDepthClip ? 1 : 0),
    };
}

/// <summary>Multisample state for pipeline creation.</summary>
public readonly record struct GpuMultisampleState
{
    /// <summary>The sample count.</summary>
    public GpuSampleCount SampleCount { get; init; }
    /// <summary>The sample mask, used when <see cref="EnableMask"/> is true.</summary>
    public uint SampleMask { get; init; }
    /// <summary>Whether the sample mask is applied.</summary>
    public bool EnableMask { get; init; }
    /// <summary>Whether alpha-to-coverage is enabled.</summary>
    public bool EnableAlphaToCoverage { get; init; }

    /// <summary>Single-sample rendering (no MSAA).</summary>
    public static GpuMultisampleState None => default;

    internal SDL_GPUMultisampleState ToNative() => new()
    {
        sample_count = (SDL_GPUSampleCount)SampleCount,
        sample_mask = SampleMask,
        enable_mask = (byte)(EnableMask ? 1 : 0),
        enable_alpha_to_coverage = (byte)(EnableAlphaToCoverage ? 1 : 0),
    };
}

/// <summary>Depth/stencil state for pipeline creation.</summary>
public readonly record struct GpuDepthStencilState
{
    /// <summary>The depth comparison operator.</summary>
    public GpuCompareOp CompareOp { get; init; }
    /// <summary>The stencil state for back faces.</summary>
    public GpuStencilOpState BackStencilState { get; init; }
    /// <summary>The stencil state for front faces.</summary>
    public GpuStencilOpState FrontStencilState { get; init; }
    /// <summary>The stencil comparison mask.</summary>
    public byte CompareMask { get; init; }
    /// <summary>The stencil write mask.</summary>
    public byte WriteMask { get; init; }
    /// <summary>Whether depth testing is enabled.</summary>
    public bool EnableDepthTest { get; init; }
    /// <summary>Whether depth writing is enabled.</summary>
    public bool EnableDepthWrite { get; init; }
    /// <summary>Whether stencil testing is enabled.</summary>
    public bool EnableStencilTest { get; init; }

    /// <summary>Depth and stencil testing disabled.</summary>
    public static GpuDepthStencilState Disabled => default;

    internal SDL_GPUDepthStencilState ToNative() => new()
    {
        compare_op = (SDL_GPUCompareOp)CompareOp,
        back_stencil_state = BackStencilState.ToNative(),
        front_stencil_state = FrontStencilState.ToNative(),
        compare_mask = CompareMask,
        write_mask = WriteMask,
        enable_depth_test = (byte)(EnableDepthTest ? 1 : 0),
        enable_depth_write = (byte)(EnableDepthWrite ? 1 : 0),
        enable_stencil_test = (byte)(EnableStencilTest ? 1 : 0),
    };
}

/// <summary>A color target description for pipeline creation.</summary>
public readonly record struct GpuColorTargetDescription(GpuTextureFormat Format, GpuColorTargetBlendState BlendState = default)
{
    internal SDL_GPUColorTargetDescription ToNative() =>
        new() { format = (SDL_GPUTextureFormat)Format, blend_state = BlendState.ToNative() };
}

/// <summary>Render target formats for pipeline creation.</summary>
public readonly record struct GpuGraphicsPipelineTargetInfo
{
    /// <summary>The color target descriptions.</summary>
    public GpuColorTargetDescription[]? ColorTargetDescriptions { get; init; }
    /// <summary>The depth/stencil format, or null for no depth/stencil target.</summary>
    public GpuTextureFormat? DepthStencilFormat { get; init; }
}

/// <summary>Graphics pipeline creation parameters.</summary>
public readonly record struct GpuGraphicsPipelineCreateInfo
{
    /// <summary>The vertex shader.</summary>
    public required GpuShader VertexShader { get; init; }
    /// <summary>The fragment shader.</summary>
    public required GpuShader FragmentShader { get; init; }
    /// <summary>The vertex input layout.</summary>
    public GpuVertexInputState VertexInputState { get; init; }
    /// <summary>The primitive topology.</summary>
    public GpuPrimitiveType PrimitiveType { get; init; }
    /// <summary>The rasterizer state.</summary>
    public GpuRasterizerState RasterizerState { get; init; }
    /// <summary>The multisample state.</summary>
    public GpuMultisampleState MultisampleState { get; init; }
    /// <summary>The depth/stencil state.</summary>
    public GpuDepthStencilState DepthStencilState { get; init; }
    /// <summary>The render target formats.</summary>
    public required GpuGraphicsPipelineTargetInfo TargetInfo { get; init; }
    /// <summary>Optional extra properties.</summary>
    public PropertyGroup? Props { get; init; }
}

/// <summary>Arguments for an indirect draw, laid out exactly as the GPU expects in a buffer.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct GpuIndirectDrawCommand
{
    /// <summary>The number of vertices to draw.</summary>
    public uint NumVertices;
    /// <summary>The number of instances to draw.</summary>
    public uint NumInstances;
    /// <summary>The index of the first vertex.</summary>
    public uint FirstVertex;
    /// <summary>The index of the first instance.</summary>
    public uint FirstInstance;
}

/// <summary>Arguments for an indexed indirect draw, laid out exactly as the GPU expects in a buffer.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct GpuIndexedIndirectDrawCommand
{
    /// <summary>The number of indices to draw.</summary>
    public uint NumIndices;
    /// <summary>The number of instances to draw.</summary>
    public uint NumInstances;
    /// <summary>The index of the first index in the index buffer.</summary>
    public uint FirstIndex;
    /// <summary>The value added to each index before fetching the vertex.</summary>
    public int VertexOffset;
    /// <summary>The index of the first instance.</summary>
    public uint FirstInstance;
}

/// <summary>Arguments for an indirect compute dispatch, laid out exactly as the GPU expects in a buffer.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct GpuIndirectDispatchCommand
{
    /// <summary>The number of work groups in the X dimension.</summary>
    public uint GroupCountX;
    /// <summary>The number of work groups in the Y dimension.</summary>
    public uint GroupCountY;
    /// <summary>The number of work groups in the Z dimension.</summary>
    public uint GroupCountZ;
}
```

Note: `GpuShaderCreateInfo`, `GpuComputePipelineCreateInfo`, and `GpuGraphicsPipelineCreateInfo` have **no** `ToNative()` — their conversions need pinning, so they happen inside the `GpuDevice.Create*` methods (Task 8). Compare the field lists against the native structs in `src/SdlSharp/Native/Gpu.cs:595-820` before building.

- [ ] **Step 3.2: Build**

Run: `dotnet build src/SdlSharp/SdlSharp.csproj`
Expected: `0 Warning(s), 0 Error(s)`

- [ ] **Step 3.3: Commit**

```bash
git add src/SdlSharp/Graphics/Gpu/GpuDescriptors.cs
git commit -m "Add managed GPU creation-time descriptor structs"
```

---

### Task 4: Rewrite `GpuRenderPass` public surface

**Files:**
- Modify: `src/SdlSharp/Graphics/Gpu/GpuRenderPass.cs`

- [ ] **Step 4.1: Replace the six leaking methods**

Replace `SetViewport`, `SetScissor`, `BindVertexBuffers`, `BindIndexBuffer`, `BindVertexSamplers`, `BindFragmentSamplers`, `BindVertexStorageTextures`, `BindFragmentStorageTextures`, `BindVertexStorageBuffers`, `BindFragmentStorageBuffers` with (keep all other members unchanged; keep the file's doc-comment style — add `<param>` tags matching the existing ones):

```csharp
public void SetViewport(in GpuViewport viewport)
{
    var native = viewport.ToNative();
    SDL_SetGPUViewport(Handle, &native);
}

public void SetScissor(Rectangle scissor)
{
    var native = scissor.ToNative();
    SDL_SetGPUScissor(Handle, &native);
}

public void BindVertexBuffers(uint firstSlot, ReadOnlySpan<GpuBufferBinding> bindings)
{
    Span<SDL_GPUBufferBinding> native = stackalloc SDL_GPUBufferBinding[bindings.Length];
    for (var i = 0; i < bindings.Length; i++) native[i] = bindings[i].ToNative();
    fixed (SDL_GPUBufferBinding* p = native)
        SDL_BindGPUVertexBuffers(Handle, firstSlot, p, (uint)bindings.Length);
}

public void BindIndexBuffer(in GpuBufferBinding binding, GpuIndexElementSize elementSize)
{
    var native = binding.ToNative();
    SDL_BindGPUIndexBuffer(Handle, &native, (SDL_GPUIndexElementSize)elementSize);
}

public void BindVertexSamplers(uint firstSlot, ReadOnlySpan<GpuTextureSamplerBinding> bindings)
{
    Span<SDL_GPUTextureSamplerBinding> native = stackalloc SDL_GPUTextureSamplerBinding[bindings.Length];
    for (var i = 0; i < bindings.Length; i++) native[i] = bindings[i].ToNative();
    fixed (SDL_GPUTextureSamplerBinding* p = native)
        SDL_BindGPUVertexSamplers(Handle, firstSlot, p, (uint)bindings.Length);
}

public void BindFragmentSamplers(uint firstSlot, ReadOnlySpan<GpuTextureSamplerBinding> bindings)
{
    Span<SDL_GPUTextureSamplerBinding> native = stackalloc SDL_GPUTextureSamplerBinding[bindings.Length];
    for (var i = 0; i < bindings.Length; i++) native[i] = bindings[i].ToNative();
    fixed (SDL_GPUTextureSamplerBinding* p = native)
        SDL_BindGPUFragmentSamplers(Handle, firstSlot, p, (uint)bindings.Length);
}

public void BindVertexStorageTextures(uint firstSlot, ReadOnlySpan<GpuTexture> textures)
{
    var pointers = stackalloc SDL_GPUTexture*[textures.Length];
    for (var i = 0; i < textures.Length; i++) pointers[i] = textures[i].Handle;
    SDL_BindGPUVertexStorageTextures(Handle, firstSlot, pointers, (uint)textures.Length);
}

public void BindFragmentStorageTextures(uint firstSlot, ReadOnlySpan<GpuTexture> textures)
{
    var pointers = stackalloc SDL_GPUTexture*[textures.Length];
    for (var i = 0; i < textures.Length; i++) pointers[i] = textures[i].Handle;
    SDL_BindGPUFragmentStorageTextures(Handle, firstSlot, pointers, (uint)textures.Length);
}

public void BindVertexStorageBuffers(uint firstSlot, ReadOnlySpan<GpuBuffer> buffers)
{
    var pointers = stackalloc SDL_GPUBuffer*[buffers.Length];
    for (var i = 0; i < buffers.Length; i++) pointers[i] = buffers[i].Handle;
    SDL_BindGPUVertexStorageBuffers(Handle, firstSlot, pointers, (uint)buffers.Length);
}

public void BindFragmentStorageBuffers(uint firstSlot, ReadOnlySpan<GpuBuffer> buffers)
{
    var pointers = stackalloc SDL_GPUBuffer*[buffers.Length];
    for (var i = 0; i < buffers.Length; i++) pointers[i] = buffers[i].Handle;
    SDL_BindGPUFragmentStorageBuffers(Handle, firstSlot, pointers, (uint)buffers.Length);
}
```

Also change `SetBlendConstants(SDL_FColor constants)` to:

```csharp
public void SetBlendConstants(FColor constants) =>
    SDL_SetGPUBlendConstants(Handle, constants.ToNative());
```

- [ ] **Step 4.2: Build and grep-check this file**

Run: `dotnet build src/SdlSharp/SdlSharp.csproj && grep -n "public.*SDL_" src/SdlSharp/Graphics/Gpu/GpuRenderPass.cs`
Expected: build `0 Warning(s), 0 Error(s)`; grep produces no output.

- [ ] **Step 4.3: Commit**

```bash
git add src/SdlSharp/Graphics/Gpu/GpuRenderPass.cs
git commit -m "Replace native types in GpuRenderPass public surface"
```

---

### Task 5: Rewrite `GpuComputePass` public surface

**Files:**
- Modify: `src/SdlSharp/Graphics/Gpu/GpuComputePass.cs`

- [ ] **Step 5.1: Replace the three leaking methods**

```csharp
public void BindSamplers(uint firstSlot, ReadOnlySpan<GpuTextureSamplerBinding> bindings)
{
    Span<SDL_GPUTextureSamplerBinding> native = stackalloc SDL_GPUTextureSamplerBinding[bindings.Length];
    for (var i = 0; i < bindings.Length; i++) native[i] = bindings[i].ToNative();
    fixed (SDL_GPUTextureSamplerBinding* p = native)
        SDL_BindGPUComputeSamplers(Handle, firstSlot, p, (uint)bindings.Length);
}

public void BindStorageTextures(uint firstSlot, ReadOnlySpan<GpuTexture> textures)
{
    var pointers = stackalloc SDL_GPUTexture*[textures.Length];
    for (var i = 0; i < textures.Length; i++) pointers[i] = textures[i].Handle;
    SDL_BindGPUComputeStorageTextures(Handle, firstSlot, pointers, (uint)textures.Length);
}

public void BindStorageBuffers(uint firstSlot, ReadOnlySpan<GpuBuffer> buffers)
{
    var pointers = stackalloc SDL_GPUBuffer*[buffers.Length];
    for (var i = 0; i < buffers.Length; i++) pointers[i] = buffers[i].Handle;
    SDL_BindGPUComputeStorageBuffers(Handle, firstSlot, pointers, (uint)buffers.Length);
}
```

- [ ] **Step 5.2: Build and grep-check**

Run: `dotnet build src/SdlSharp/SdlSharp.csproj && grep -n "public.*SDL_" src/SdlSharp/Graphics/Gpu/GpuComputePass.cs`
Expected: build clean; grep empty.

- [ ] **Step 5.3: Commit**

```bash
git add src/SdlSharp/Graphics/Gpu/GpuComputePass.cs
git commit -m "Replace native types in GpuComputePass public surface"
```

---

### Task 6: Rewrite `GpuCopyPass` public surface

**Files:**
- Modify: `src/SdlSharp/Graphics/Gpu/GpuCopyPass.cs`

- [ ] **Step 6.1: Replace all six transfer methods**

```csharp
public void UploadToTexture(in GpuTextureTransferInfo source, in GpuTextureRegion destination, bool cycle = false)
{
    var s = source.ToNative();
    var d = destination.ToNative();
    SDL_UploadToGPUTexture(Handle, &s, &d, cycle);
}

public void UploadToBuffer(in GpuTransferBufferLocation source, in GpuBufferRegion destination, bool cycle = false)
{
    var s = source.ToNative();
    var d = destination.ToNative();
    SDL_UploadToGPUBuffer(Handle, &s, &d, cycle);
}

public void CopyTextureToTexture(in GpuTextureLocation source, in GpuTextureLocation destination, uint w, uint h, uint d, bool cycle = false)
{
    var s = source.ToNative();
    var dst = destination.ToNative();
    SDL_CopyGPUTextureToTexture(Handle, &s, &dst, w, h, d, cycle);
}

public void CopyBufferToBuffer(in GpuBufferLocation source, in GpuBufferLocation destination, uint size, bool cycle = false)
{
    var s = source.ToNative();
    var dst = destination.ToNative();
    SDL_CopyGPUBufferToBuffer(Handle, &s, &dst, size, cycle);
}

public void DownloadFromTexture(in GpuTextureRegion source, in GpuTextureTransferInfo destination)
{
    var s = source.ToNative();
    var d = destination.ToNative();
    SDL_DownloadFromGPUTexture(Handle, &s, &d);
}

public void DownloadFromBuffer(in GpuBufferRegion source, in GpuTransferBufferLocation destination)
{
    var s = source.ToNative();
    var d = destination.ToNative();
    SDL_DownloadFromGPUBuffer(Handle, &s, &d);
}
```

- [ ] **Step 6.2: Build and grep-check**

Run: `dotnet build src/SdlSharp/SdlSharp.csproj && grep -n "public.*SDL_" src/SdlSharp/Graphics/Gpu/GpuCopyPass.cs`
Expected: build clean; grep empty.

- [ ] **Step 6.3: Commit**

```bash
git add src/SdlSharp/Graphics/Gpu/GpuCopyPass.cs
git commit -m "Replace native types in GpuCopyPass public surface"
```

---

### Task 7: Rewrite `GpuCommandBuffer` + non-owning `GpuTexture`

**Files:**
- Modify: `src/SdlSharp/Graphics/Gpu/GpuTexture.cs`
- Modify: `src/SdlSharp/Graphics/Gpu/GpuCommandBuffer.cs`

- [ ] **Step 7.1: Add `_ownsHandle` to `GpuTexture`**

Change the constructor and `Dispose` in `src/SdlSharp/Graphics/Gpu/GpuTexture.cs`:

```csharp
private readonly bool _ownsHandle;

internal GpuTexture(GpuDevice device, SDL_GPUTexture* handle, bool ownsHandle = true)
{
    _device = device;
    Handle = handle;
    _ownsHandle = ownsHandle;
}

public void Dispose()
{
    if (_ownsHandle && Handle != null)
    {
        SDL_ReleaseGPUTexture(_device.Handle, Handle);
    }
    Handle = null;
}
```

- [ ] **Step 7.2: Replace `BeginRenderPass`, `BeginComputePass`, `Blit`, and the swapchain methods in `GpuCommandBuffer.cs`**

Add `using SdlSharp.Graphics;` if not already resolvable (the file is in `SdlSharp.Graphics.Gpu`, so `Size` from `SdlSharp.Graphics` resolves without a new using). Replace the four leaking members with:

```csharp
/// <summary>
/// Begins a render pass with a single color target and no depth/stencil target.
/// </summary>
public GpuRenderPass BeginRenderPass(in GpuColorTargetInfo colorTarget)
{
    var native = colorTarget.ToNative();
    return new(Check(SDL_BeginGPURenderPass(Handle, &native, 1, null)));
}

/// <summary>
/// Begins a render pass with the specified color targets and no depth/stencil target.
/// </summary>
public GpuRenderPass BeginRenderPass(ReadOnlySpan<GpuColorTargetInfo> colorTargets)
{
    Span<SDL_GPUColorTargetInfo> native = stackalloc SDL_GPUColorTargetInfo[colorTargets.Length];
    for (var i = 0; i < colorTargets.Length; i++) native[i] = colorTargets[i].ToNative();
    fixed (SDL_GPUColorTargetInfo* p = native)
        return new(Check(SDL_BeginGPURenderPass(Handle, p, (uint)colorTargets.Length, null)));
}

/// <summary>
/// Begins a render pass with the specified color targets and a depth/stencil target.
/// </summary>
public GpuRenderPass BeginRenderPass(ReadOnlySpan<GpuColorTargetInfo> colorTargets, in GpuDepthStencilTargetInfo depthStencilTarget)
{
    Span<SDL_GPUColorTargetInfo> native = stackalloc SDL_GPUColorTargetInfo[colorTargets.Length];
    for (var i = 0; i < colorTargets.Length; i++) native[i] = colorTargets[i].ToNative();
    var nativeDepth = depthStencilTarget.ToNative();
    fixed (SDL_GPUColorTargetInfo* p = native)
        return new(Check(SDL_BeginGPURenderPass(Handle, p, (uint)colorTargets.Length, &nativeDepth)));
}

/// <summary>
/// Begins a compute pass with the specified read-write storage bindings.
/// </summary>
public GpuComputePass BeginComputePass(ReadOnlySpan<GpuStorageTextureReadWriteBinding> storageTextures, ReadOnlySpan<GpuStorageBufferReadWriteBinding> storageBuffers)
{
    Span<SDL_GPUStorageTextureReadWriteBinding> nativeTextures = stackalloc SDL_GPUStorageTextureReadWriteBinding[storageTextures.Length];
    for (var i = 0; i < storageTextures.Length; i++) nativeTextures[i] = storageTextures[i].ToNative();
    Span<SDL_GPUStorageBufferReadWriteBinding> nativeBuffers = stackalloc SDL_GPUStorageBufferReadWriteBinding[storageBuffers.Length];
    for (var i = 0; i < storageBuffers.Length; i++) nativeBuffers[i] = storageBuffers[i].ToNative();
    fixed (SDL_GPUStorageTextureReadWriteBinding* pt = nativeTextures)
    fixed (SDL_GPUStorageBufferReadWriteBinding* pb = nativeBuffers)
        return new(Check(SDL_BeginGPUComputePass(Handle, pt, (uint)storageTextures.Length, pb, (uint)storageBuffers.Length)));
}

/// <summary>
/// Blits (copies with potential scaling/filtering) between texture regions.
/// </summary>
public void Blit(in GpuBlitInfo info)
{
    var native = info.ToNative();
    SDL_BlitGPUTexture(Handle, &native);
}

/// <summary>
/// Acquires a swapchain texture for rendering to a window.
/// Returns null when no texture is available this frame (too many frames in flight) — skip rendering.
/// The returned texture is owned by the swapchain; disposing it is a no-op.
/// </summary>
public GpuTexture? AcquireSwapchainTexture(Window window, out Size size)
{
    SDL_GPUTexture* tex;
    uint w, h;
    Check(SDL_AcquireGPUSwapchainTexture(Handle, window.Handle, &tex, &w, &h));
    size = new Size((int)w, (int)h);
    return tex == null ? null : new GpuTexture(_device, tex, ownsHandle: false);
}

/// <summary>
/// Waits for and acquires a swapchain texture for rendering to a window.
/// Returns null when no texture is available — skip rendering.
/// The returned texture is owned by the swapchain; disposing it is a no-op.
/// </summary>
public GpuTexture? WaitAndAcquireSwapchainTexture(Window window, out Size size)
{
    SDL_GPUTexture* tex;
    uint w, h;
    Check(SDL_WaitAndAcquireGPUSwapchainTexture(Handle, window.Handle, &tex, &w, &h));
    size = new Size((int)w, (int)h);
    return tex == null ? null : new GpuTexture(_device, tex, ownsHandle: false);
}
```

Note the swapchain methods previously threw `SdlException` manually; `Check(bool)` does the same — verify `Common.Check(bool)` exists (it is used throughout, e.g. `GpuDevice.ClaimWindow`).

- [ ] **Step 7.3: Build and grep-check**

Run: `dotnet build src/SdlSharp/SdlSharp.csproj && grep -n "public.*SDL_" src/SdlSharp/Graphics/Gpu/GpuCommandBuffer.cs`
Expected: build clean; grep empty.

- [ ] **Step 7.4: Commit**

```bash
git add src/SdlSharp/Graphics/Gpu/GpuTexture.cs src/SdlSharp/Graphics/Gpu/GpuCommandBuffer.cs
git commit -m "Managed render/compute pass begin, blit, and swapchain acquire"
```

---

### Task 8: Rewrite `GpuDevice` create methods

**Files:**
- Modify: `src/SdlSharp/Graphics/Gpu/GpuDevice.cs`

- [ ] **Step 8.1: Replace the seven `Create*` methods that take native create infos**

```csharp
public GpuShader CreateShader(in GpuShaderCreateInfo createInfo)
{
    fixed (byte* code = createInfo.Code.Span)
    fixed (byte* entryPoint = ToUtf8(createInfo.EntryPoint ?? "main"))
    {
        var native = new SDL_GPUShaderCreateInfo
        {
            code_size = (nuint)createInfo.Code.Length,
            code = code,
            entrypoint = entryPoint,
            format = (SDL_GPUShaderFormat)createInfo.Format,
            stage = (SDL_GPUShaderStage)createInfo.Stage,
            num_samplers = createInfo.NumSamplers,
            num_storage_textures = createInfo.NumStorageTextures,
            num_storage_buffers = createInfo.NumStorageBuffers,
            num_uniform_buffers = createInfo.NumUniformBuffers,
            props = createInfo.Props?.Id ?? default,
        };
        return new GpuShader(this, Check(SDL_CreateGPUShader(Handle, &native)));
    }
}

public GpuGraphicsPipeline CreateGraphicsPipeline(in GpuGraphicsPipelineCreateInfo createInfo)
{
    var vertexBufferDescs = createInfo.VertexInputState.VertexBufferDescriptions ?? [];
    var vertexAttributes = createInfo.VertexInputState.VertexAttributes ?? [];
    var colorTargetDescs = createInfo.TargetInfo.ColorTargetDescriptions ?? [];

    Span<SDL_GPUVertexBufferDescription> nativeVertexBuffers = stackalloc SDL_GPUVertexBufferDescription[vertexBufferDescs.Length];
    for (var i = 0; i < vertexBufferDescs.Length; i++) nativeVertexBuffers[i] = vertexBufferDescs[i].ToNative();
    Span<SDL_GPUVertexAttribute> nativeAttributes = stackalloc SDL_GPUVertexAttribute[vertexAttributes.Length];
    for (var i = 0; i < vertexAttributes.Length; i++) nativeAttributes[i] = vertexAttributes[i].ToNative();
    Span<SDL_GPUColorTargetDescription> nativeColorTargets = stackalloc SDL_GPUColorTargetDescription[colorTargetDescs.Length];
    for (var i = 0; i < colorTargetDescs.Length; i++) nativeColorTargets[i] = colorTargetDescs[i].ToNative();

    fixed (SDL_GPUVertexBufferDescription* pVertexBuffers = nativeVertexBuffers)
    fixed (SDL_GPUVertexAttribute* pAttributes = nativeAttributes)
    fixed (SDL_GPUColorTargetDescription* pColorTargets = nativeColorTargets)
    {
        var native = new SDL_GPUGraphicsPipelineCreateInfo
        {
            vertex_shader = createInfo.VertexShader.Handle,
            fragment_shader = createInfo.FragmentShader.Handle,
            vertex_input_state = new SDL_GPUVertexInputState
            {
                vertex_buffer_descriptions = pVertexBuffers,
                num_vertex_buffers = (uint)vertexBufferDescs.Length,
                vertex_attributes = pAttributes,
                num_vertex_attributes = (uint)vertexAttributes.Length,
            },
            primitive_type = (SDL_GPUPrimitiveType)createInfo.PrimitiveType,
            rasterizer_state = createInfo.RasterizerState.ToNative(),
            multisample_state = createInfo.MultisampleState.ToNative(),
            depth_stencil_state = createInfo.DepthStencilState.ToNative(),
            target_info = new SDL_GPUGraphicsPipelineTargetInfo
            {
                color_target_descriptions = pColorTargets,
                num_color_targets = (uint)colorTargetDescs.Length,
                depth_stencil_format = (SDL_GPUTextureFormat)(createInfo.TargetInfo.DepthStencilFormat ?? GpuTextureFormat.Invalid),
                has_depth_stencil_target = (byte)(createInfo.TargetInfo.DepthStencilFormat is not null ? 1 : 0),
            },
            props = createInfo.Props?.Id ?? default,
        };
        return new GpuGraphicsPipeline(this, Check(SDL_CreateGPUGraphicsPipeline(Handle, &native)));
    }
}

public GpuComputePipeline CreateComputePipeline(in GpuComputePipelineCreateInfo createInfo)
{
    fixed (byte* code = createInfo.Code.Span)
    fixed (byte* entryPoint = ToUtf8(createInfo.EntryPoint ?? "main"))
    {
        var native = new SDL_GPUComputePipelineCreateInfo
        {
            code_size = (nuint)createInfo.Code.Length,
            code = code,
            entrypoint = entryPoint,
            format = (SDL_GPUShaderFormat)createInfo.Format,
            num_samplers = createInfo.NumSamplers,
            num_readonly_storage_textures = createInfo.NumReadonlyStorageTextures,
            num_readonly_storage_buffers = createInfo.NumReadonlyStorageBuffers,
            num_readwrite_storage_textures = createInfo.NumReadwriteStorageTextures,
            num_readwrite_storage_buffers = createInfo.NumReadwriteStorageBuffers,
            num_uniform_buffers = createInfo.NumUniformBuffers,
            threadcount_x = createInfo.ThreadcountX,
            threadcount_y = createInfo.ThreadcountY,
            threadcount_z = createInfo.ThreadcountZ,
            props = createInfo.Props?.Id ?? default,
        };
        return new GpuComputePipeline(this, Check(SDL_CreateGPUComputePipeline(Handle, &native)));
    }
}

public GpuSampler CreateSampler(in GpuSamplerCreateInfo createInfo)
{
    var native = createInfo.ToNative();
    return new GpuSampler(this, Check(SDL_CreateGPUSampler(Handle, &native)));
}

public GpuTexture CreateTexture(in GpuTextureCreateInfo createInfo)
{
    var native = createInfo.ToNative();
    return new GpuTexture(this, Check(SDL_CreateGPUTexture(Handle, &native)));
}

public GpuBuffer CreateBuffer(in GpuBufferCreateInfo createInfo)
{
    var native = createInfo.ToNative();
    return new GpuBuffer(this, Check(SDL_CreateGPUBuffer(Handle, &native)));
}

public GpuTransferBuffer CreateTransferBuffer(in GpuTransferBufferCreateInfo createInfo)
{
    var native = createInfo.ToNative();
    return new GpuTransferBuffer(this, Check(SDL_CreateGPUTransferBuffer(Handle, &native)));
}
```

Note: `ToUtf8` returns `byte[]?`; `fixed` over it is valid. Keep the existing XML doc comments on each method (update `<param>` text where it mentioned native types).

- [ ] **Step 8.2: Build and grep-check**

Run: `dotnet build src/SdlSharp/SdlSharp.csproj && grep -rn "public.*SDL_" src/SdlSharp/Graphics/Gpu/`
Expected: build clean; grep empty — this is the moment the whole `Graphics/Gpu` directory goes native-free.

- [ ] **Step 8.3: Commit**

```bash
git add src/SdlSharp/Graphics/Gpu/GpuDevice.cs
git commit -m "Managed create-info structs for GPU resource creation"
```

---

### Task 9: Expose the 8 native-only GPU functions

**Files:**
- Modify: `src/SdlSharp/Graphics/Gpu/GpuFence.cs`
- Modify: `src/SdlSharp/Graphics/Gpu/GpuDevice.cs`
- Create: `src/SdlSharp/Graphics/Gpu/GpuDeviceProperties.cs`
- Create: `src/SdlSharp/Graphics/Gpu/GpuTextureFormatExtensions.cs`

- [ ] **Step 9.1: Add fence waiting to `GpuFence.cs`**

Add inside the class (needs `using static SdlSharp.Native.Common;` at the top of the file for `Check`):

```csharp
/// <summary>
/// Blocks until all of the given fences are signaled.
/// </summary>
/// <param name="device">The device the fences belong to.</param>
/// <param name="fences">The fences to wait for.</param>
public static void WaitAll(GpuDevice device, ReadOnlySpan<GpuFence> fences) => Wait(device, waitAll: true, fences);

/// <summary>
/// Blocks until at least one of the given fences is signaled.
/// </summary>
/// <param name="device">The device the fences belong to.</param>
/// <param name="fences">The fences to wait for.</param>
public static void WaitAny(GpuDevice device, ReadOnlySpan<GpuFence> fences) => Wait(device, waitAll: false, fences);

private static void Wait(GpuDevice device, bool waitAll, ReadOnlySpan<GpuFence> fences)
{
    var pointers = stackalloc SDL_GPUFence*[fences.Length];
    for (var i = 0; i < fences.Length; i++) pointers[i] = fences[i].Handle;
    Check(SDL_WaitForGPUFences(device.Handle, waitAll, pointers, (uint)fences.Length));
}
```

- [ ] **Step 9.2: Add device queries to `GpuDevice.cs`**

```csharp
/// <summary>
/// Creates a new GPU device from a property group. Property names are in <see cref="GpuDeviceProperties"/>.
/// </summary>
/// <param name="props">The creation properties.</param>
/// <returns>A new GPU device.</returns>
public static GpuDevice Create(PropertyGroup props) =>
    new(Check(SDL_CreateGPUDeviceWithProperties(props.Id)));

/// <summary>
/// Checks whether a GPU device with the given shader format support can be created.
/// </summary>
/// <param name="shaderFormats">The required shader formats.</param>
/// <param name="preferredBackend">An optional GPU backend name, or null for any.</param>
/// <returns>True if a device supporting the formats can be created.</returns>
public static bool SupportsShaderFormats(GpuShaderFormat shaderFormats, string? preferredBackend = null) =>
    SDL_GPUSupportsShaderFormats((SDL_GPUShaderFormat)shaderFormats, ToUtf8(preferredBackend));

/// <summary>
/// Gets the properties of this device (name, driver info). The returned group is owned by SDL.
/// </summary>
public PropertyGroup Properties => new(SDL_GetGPUDeviceProperties(Handle), ownsHandle: false);

/// <summary>
/// Checks whether a window's swapchain supports the given present mode.
/// </summary>
/// <param name="window">The claimed window.</param>
/// <param name="presentMode">The present mode to check.</param>
/// <returns>True if supported.</returns>
public bool WindowSupportsPresentMode(Window window, GpuPresentMode presentMode) =>
    SDL_WindowSupportsGPUPresentMode(Handle, window.Handle, (SDL_GPUPresentMode)presentMode);

/// <summary>
/// Checks whether a window's swapchain supports the given composition.
/// </summary>
/// <param name="window">The claimed window.</param>
/// <param name="composition">The swapchain composition to check.</param>
/// <returns>True if supported.</returns>
public bool WindowSupportsSwapchainComposition(Window window, GpuSwapchainComposition composition) =>
    SDL_WindowSupportsGPUSwapchainComposition(Handle, window.Handle, (SDL_GPUSwapchainComposition)composition);
```

- [ ] **Step 9.3: Create `GpuDeviceProperties.cs`**

```csharp
namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// Property names accepted by <see cref="GpuDevice.Create(PropertyGroup)"/>
/// (the SDL_PROP_GPU_DEVICE_CREATE_* constants).
/// </summary>
public static class GpuDeviceProperties
{
    /// <summary>Enable debug mode (boolean).</summary>
    public const string DebugMode = "SDL.gpu.device.create.debugmode";
    /// <summary>Prefer a low-power GPU (boolean).</summary>
    public const string PreferLowPower = "SDL.gpu.device.create.preferlowpower";
    /// <summary>Automatically log GPU activity (boolean).</summary>
    public const string Verbose = "SDL.gpu.device.create.verbose";
    /// <summary>The name of the GPU driver to use (string).</summary>
    public const string Name = "SDL.gpu.device.create.name";
    /// <summary>Enable Vulkan clip distance support (boolean).</summary>
    public const string FeatureClipDistance = "SDL.gpu.device.create.feature.clip_distance";
    /// <summary>Enable depth clamping support (boolean).</summary>
    public const string FeatureDepthClamping = "SDL.gpu.device.create.feature.depth_clamping";
    /// <summary>Enable indirect draw first-instance support (boolean).</summary>
    public const string FeatureIndirectDrawFirstInstance = "SDL.gpu.device.create.feature.indirect_draw_first_instance";
    /// <summary>Enable anisotropic filtering support (boolean).</summary>
    public const string FeatureAnisotropy = "SDL.gpu.device.create.feature.anisotropy";
    /// <summary>The app can provide private (NDA) shaders (boolean).</summary>
    public const string ShadersPrivate = "SDL.gpu.device.create.shaders.private";
    /// <summary>The app can provide SPIR-V shaders (boolean).</summary>
    public const string ShadersSpirv = "SDL.gpu.device.create.shaders.spirv";
    /// <summary>The app can provide DXBC shaders (boolean).</summary>
    public const string ShadersDxbc = "SDL.gpu.device.create.shaders.dxbc";
    /// <summary>The app can provide DXIL shaders (boolean).</summary>
    public const string ShadersDxil = "SDL.gpu.device.create.shaders.dxil";
    /// <summary>The app can provide MSL shaders (boolean).</summary>
    public const string ShadersMsl = "SDL.gpu.device.create.shaders.msl";
    /// <summary>The app can provide Metal library shaders (boolean).</summary>
    public const string ShadersMetallib = "SDL.gpu.device.create.shaders.metallib";
    /// <summary>Allow D3D12 tier-1 resource binding (boolean).</summary>
    public const string D3D12AllowFewerResourceSlots = "SDL.gpu.device.create.d3d12.allowtier1resourcebinding";
    /// <summary>The D3D12 semantic name prefix (string).</summary>
    public const string D3D12SemanticName = "SDL.gpu.device.create.d3d12.semantic";
    /// <summary>The D3D12 Agility SDK version (number).</summary>
    public const string D3D12AgilitySdkVersion = "SDL.gpu.device.create.d3d12.agility_sdk_version";
    /// <summary>The D3D12 Agility SDK path (string).</summary>
    public const string D3D12AgilitySdkPath = "SDL.gpu.device.create.d3d12.agility_sdk_path";
    /// <summary>Require Vulkan hardware acceleration (boolean).</summary>
    public const string VulkanRequireHardwareAcceleration = "SDL.gpu.device.create.vulkan.requirehardwareacceleration";
    /// <summary>A pointer to SDL_GPUVulkanOptions (pointer).</summary>
    public const string VulkanOptions = "SDL.gpu.device.create.vulkan.options";
    /// <summary>Allow the Metal Mac family 1 GPU tier (boolean).</summary>
    public const string MetalAllowMacFamily1 = "SDL.gpu.device.create.metal.allowmacfamily1";
}
```

- [ ] **Step 9.4: Create `GpuTextureFormatExtensions.cs`**

```csharp
using SdlSharp.Native;
using static SdlSharp.Native.Gpu;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// Size queries for GPU texture formats.
/// </summary>
public static class GpuTextureFormatExtensions
{
    /// <summary>
    /// Gets the texel block size of a format in bytes.
    /// </summary>
    /// <param name="format">The texture format.</param>
    /// <returns>The block size in bytes.</returns>
    public static uint TexelBlockSize(this GpuTextureFormat format) =>
        SDL_GPUTextureFormatTexelBlockSize((SDL_GPUTextureFormat)format);

    /// <summary>
    /// Calculates the total size in bytes of a texture with the given format and dimensions.
    /// </summary>
    /// <param name="format">The texture format.</param>
    /// <param name="width">The width in pixels.</param>
    /// <param name="height">The height in pixels.</param>
    /// <param name="depthOrLayerCount">The depth or layer count.</param>
    /// <returns>The size in bytes.</returns>
    public static uint CalculateSize(this GpuTextureFormat format, uint width, uint height, uint depthOrLayerCount = 1) =>
        SDL_CalculateGPUTextureFormatSize((SDL_GPUTextureFormat)format, width, height, depthOrLayerCount);
}
```

Note: `PropertyGroup`'s constructor is `internal PropertyGroup(SDL_PropertiesID id, bool ownsHandle = true)` (`src/SdlSharp/PropertyGroup.cs:22`), so `Properties` compiles as written. Remove the stale "skipped: rarely needed" comment for `SDL_GPUSupportsProperties`/`SDL_CreateGPUDeviceWithProperties` in `src/SdlSharp/Native/Gpu.cs:10` **only if** it contradicts what is now wrapped — update the comment text to reflect that `SDL_CreateGPUDeviceWithProperties` is now exposed.

- [ ] **Step 9.5: Build**

Run: `dotnet build src/SdlSharp/SdlSharp.csproj`
Expected: `0 Warning(s), 0 Error(s)`

- [ ] **Step 9.6: Commit**

```bash
git add src/SdlSharp/Graphics/Gpu/
git commit -m "Expose fence waits, device properties, swapchain support queries, format sizes"
```

---

### Task 10: Migrate the ImGuiDemo sample; full-solution build + grep gate

**Files:**
- Modify: `Samples/ImGuiDemo/Program.cs:56-80`

- [ ] **Step 10.1: Update the render loop**

Replace the block at `Samples/ImGuiDemo/Program.cs:56-80` (currently builds `SDL_GPUColorTargetInfo` by hand) with:

```csharp
// GPU rendering
var cmdBuf = device.AcquireCommandBuffer();

if (cmdBuf.WaitAndAcquireSwapchainTexture(window, out _) is { } swapTex)
{
    // Upload vertex/index buffers BEFORE starting the render pass
    ImGuiBackend.PrepareDrawData(drawData, cmdBuf);

    var colorTarget = new GpuColorTargetInfo
    {
        Texture = swapTex,
        ClearColor = new FColor(0.45f, 0.55f, 0.60f, 1.0f),
        LoadOp = GpuLoadOp.Clear,
        StoreOp = GpuStoreOp.Store,
    };

    var renderPass = cmdBuf.BeginRenderPass(colorTarget);

    // Render ImGui draw commands inside the render pass
    ImGuiBackend.RenderDrawData(drawData, cmdBuf, renderPass);

    renderPass.End();
}

cmdBuf.Submit();
```

Remove any now-unused `using SdlSharp.Native;` / `using static SdlSharp.Native.Gpu;` directives from the top of `Program.cs`, and add `using SdlSharp.Graphics;` if `FColor` doesn't already resolve.

- [ ] **Step 10.2: Full solution build**

Run: `dotnet build SdlSharp.slnx`
Expected: `0 Warning(s), 0 Error(s)` across all projects (SdlSharp, SdlSharp.ImGui, Samples).

- [ ] **Step 10.3: The grep gate (phase 7.2 acceptance criterion)**

Run: `grep -rn "public.*SDL_" src/SdlSharp/Graphics/Gpu/`
Expected: **no output**. If anything appears, fix it before proceeding.

- [ ] **Step 10.4: Run the samples**

Run: `dotnet run --project Samples/GpuInfo`
Expected: prints driver list, device backend name, shader formats, and per-format support table; exits 0.

Run: `dotnet run --project Samples/ImGuiDemo` (this opens a window — close it after confirming the demo renders and responds to input; if running unattended, ask the user to verify).
Expected: ImGui demo window renders on the GPU backend exactly as before the change.

- [ ] **Step 10.5: Commit**

```bash
git add Samples/ImGuiDemo/Program.cs
git commit -m "Migrate ImGuiDemo to managed GPU descriptor API"
```

---

### Task 11: Update INVENTORY.md and TODO.md

**Files:**
- Modify: `INVENTORY.md` (the `## SDL_gpu.h` section)
- Modify: `TODO.md` (Phase 7.2 checkbox)

- [ ] **Step 11.1: Update the `SDL_gpu.h` inventory section**

In the `## SDL_gpu.h` section of `INVENTORY.md`, update the **Managed Wrapper** column for the rows this change affected. The specific fixes:
- `SDL_CreateGPUDeviceWithProperties` → `GpuDevice.Create(PropertyGroup)`
- `SDL_GPUSupportsShaderFormats` → `GpuDevice.SupportsShaderFormats`
- `SDL_GetGPUDeviceProperties` → `GpuDevice.Properties`
- `SDL_WindowSupportsGPUPresentMode` → `GpuDevice.WindowSupportsPresentMode`
- `SDL_WindowSupportsGPUSwapchainComposition` → `GpuDevice.WindowSupportsSwapchainComposition`
- `SDL_WaitForGPUFences` → `GpuFence.WaitAll` / `GpuFence.WaitAny`
- `SDL_GPUTextureFormatTexelBlockSize` → `GpuTextureFormatExtensions.TexelBlockSize`
- `SDL_CalculateGPUTextureFormatSize` → `GpuTextureFormatExtensions.CalculateSize`
- The 21 `SDL_PROP_GPU_DEVICE_CREATE_*` grouped row → `GpuDeviceProperties` constants
- Every struct row previously marked with a native-only wrapper (e.g. `SDL_GPUColorTargetInfo`, `SDL_GPUShaderCreateInfo`, all binding/region/location structs) → add its new managed type name (`GpuColorTargetInfo`, `GpuShaderCreateInfo`, …)
- Enum rows for the 18 newly wrapped enums → add the managed enum names (`GpuLoadOp`, `GpuStoreOp`, …)

Cross-check each name against the actual code before writing it (the whole point of the last inventory pass was killing rows that name nonexistent wrappers).

- [ ] **Step 11.2: Check off phase 7.2 in TODO.md**

Change the `- [ ] 7.2 GPU managed descriptor layer: …` entry under "Phase 7: Truth and rules" to `- [x]`, leaving the text intact.

- [ ] **Step 11.3: Final build + commit**

Run: `dotnet build SdlSharp.slnx`
Expected: `0 Warning(s), 0 Error(s)`

```bash
git add INVENTORY.md TODO.md
git commit -m "Record GPU managed descriptor layer in INVENTORY.md; complete phase 7.2"
```
