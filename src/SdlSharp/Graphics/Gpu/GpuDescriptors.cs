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
