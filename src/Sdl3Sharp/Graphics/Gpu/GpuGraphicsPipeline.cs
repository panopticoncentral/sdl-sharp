using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Gpu;

namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Represents a GPU graphics pipeline for rendering operations.
/// </summary>
public sealed unsafe class GpuGraphicsPipeline : IDisposable
{
    private bool _disposed;
    private readonly GpuDevice _device;

    /// <summary>
    /// Gets the underlying SDL_GPUGraphicsPipeline pointer.
    /// </summary>
    public SDL_GPUGraphicsPipeline* Handle { get; private set; }

    /// <summary>
    /// Initializes a new instance wrapping an existing SDL_GPUGraphicsPipeline pointer.
    /// </summary>
    /// <param name="handle">The SDL_GPUGraphicsPipeline pointer.</param>
    /// <param name="device">The GPU device that created this pipeline.</param>
    internal GpuGraphicsPipeline(SDL_GPUGraphicsPipeline* handle, GpuDevice device)
    {
        Handle = handle;
        _device = device;
    }

    /// <summary>
    /// Creates a graphics pipeline object.
    /// </summary>
    /// <param name="device">The GPU device.</param>
    /// <param name="createInfo">The graphics pipeline creation info.</param>
    /// <returns>A new graphics pipeline.</returns>
    public static GpuGraphicsPipeline Create(GpuDevice device, GpuGraphicsPipelineCreateInfo createInfo)
    {
        fixed (GpuVertexBufferDescription* vertexBuffersPtr = createInfo.VertexBufferDescriptions)
        fixed (GpuVertexAttribute* vertexAttributesPtr = createInfo.VertexAttributes)
        fixed (GpuColorTargetDescription* colorTargetsPtr = createInfo.ColorTargetDescriptions)
        {
            var nativeInfo = new SDL_GPUGraphicsPipelineCreateInfo
            {
                vertex_shader = createInfo.VertexShader != null ? createInfo.VertexShader.Handle : null,
                fragment_shader = createInfo.FragmentShader != null ? createInfo.FragmentShader.Handle : null,
                vertex_input_state = new SDL_GPUVertexInputState
                {
                    vertex_buffer_descriptions = (SDL_GPUVertexBufferDescription*)vertexBuffersPtr,
                    num_vertex_buffers = (uint)(createInfo.VertexBufferDescriptions?.Length ?? 0),
                    vertex_attributes = (SDL_GPUVertexAttribute*)vertexAttributesPtr,
                    num_vertex_attributes = (uint)(createInfo.VertexAttributes?.Length ?? 0)
                },
                primitive_type = (SDL_GPUPrimitiveType)createInfo.PrimitiveType,
                rasterizer_state = new SDL_GPURasterizerState
                {
                    fill_mode = (SDL_GPUFillMode)createInfo.RasterizerState.FillMode,
                    cull_mode = (SDL_GPUCullMode)createInfo.RasterizerState.CullMode,
                    front_face = (SDL_GPUFrontFace)createInfo.RasterizerState.FrontFace,
                    depth_bias_constant_factor = createInfo.RasterizerState.DepthBiasConstantFactor,
                    depth_bias_clamp = createInfo.RasterizerState.DepthBiasClamp,
                    depth_bias_slope_factor = createInfo.RasterizerState.DepthBiasSlopeFactor,
                    enable_depth_bias = createInfo.RasterizerState.EnableDepthBias,
                    enable_depth_clip = createInfo.RasterizerState.EnableDepthClip
                },
                multisample_state = new SDL_GPUMultisampleState
                {
                    sample_count = (SDL_GPUSampleCount)createInfo.MultisampleState.SampleCount,
                    sample_mask = createInfo.MultisampleState.SampleMask,
                    enable_mask = createInfo.MultisampleState.EnableMask
                },
                depth_stencil_state = new SDL_GPUDepthStencilState
                {
                    compare_op = (SDL_GPUCompareOp)createInfo.DepthStencilState.CompareOp,
                    back_stencil_state = new SDL_GPUStencilOpState
                    {
                        fail_op = (SDL_GPUStencilOp)createInfo.DepthStencilState.BackStencilState.FailOp,
                        pass_op = (SDL_GPUStencilOp)createInfo.DepthStencilState.BackStencilState.PassOp,
                        depth_fail_op = (SDL_GPUStencilOp)createInfo.DepthStencilState.BackStencilState.DepthFailOp,
                        compare_op = (SDL_GPUCompareOp)createInfo.DepthStencilState.BackStencilState.CompareOp
                    },
                    front_stencil_state = new SDL_GPUStencilOpState
                    {
                        fail_op = (SDL_GPUStencilOp)createInfo.DepthStencilState.FrontStencilState.FailOp,
                        pass_op = (SDL_GPUStencilOp)createInfo.DepthStencilState.FrontStencilState.PassOp,
                        depth_fail_op = (SDL_GPUStencilOp)createInfo.DepthStencilState.FrontStencilState.DepthFailOp,
                        compare_op = (SDL_GPUCompareOp)createInfo.DepthStencilState.FrontStencilState.CompareOp
                    },
                    compare_mask = createInfo.DepthStencilState.CompareMask,
                    write_mask = createInfo.DepthStencilState.WriteMask,
                    enable_depth_test = createInfo.DepthStencilState.EnableDepthTest,
                    enable_depth_write = createInfo.DepthStencilState.EnableDepthWrite,
                    enable_stencil_test = createInfo.DepthStencilState.EnableStencilTest
                },
                target_info = new SDL_GPUGraphicsPipelineTargetInfo
                {
                    color_target_descriptions = (SDL_GPUColorTargetDescription*)colorTargetsPtr,
                    num_color_targets = (uint)(createInfo.ColorTargetDescriptions?.Length ?? 0),
                    depth_stencil_format = (SDL_GPUTextureFormat)createInfo.DepthStencilFormat,
                    has_depth_stencil_target = createInfo.HasDepthStencilTarget
                },
                props = 0
            };
            return new GpuGraphicsPipeline(CheckErrorPointer(SDL_CreateGPUGraphicsPipeline(device.Handle, &nativeInfo)), device);
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (Handle != null)
        {
            SDL_ReleaseGPUGraphicsPipeline(_device.Handle, Handle);
        }

        Handle = null;
        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}

/// <summary>
/// Describes the parameters for creating a GPU graphics pipeline.
/// </summary>
public struct GpuGraphicsPipelineCreateInfo
{
    /// <summary>
    /// The vertex shader used by the graphics pipeline.
    /// </summary>
    public GpuShader? VertexShader { get; set; }

    /// <summary>
    /// The fragment shader used by the graphics pipeline.
    /// </summary>
    public GpuShader? FragmentShader { get; set; }

    /// <summary>
    /// The vertex buffer descriptions.
    /// </summary>
    public GpuVertexBufferDescription[]? VertexBufferDescriptions { get; set; }

    /// <summary>
    /// The vertex attribute descriptions.
    /// </summary>
    public GpuVertexAttribute[]? VertexAttributes { get; set; }

    /// <summary>
    /// The primitive topology of the graphics pipeline.
    /// </summary>
    public GpuPrimitiveType PrimitiveType { get; set; }

    /// <summary>
    /// The rasterizer state of the graphics pipeline.
    /// </summary>
    public GpuRasterizerState RasterizerState { get; set; }

    /// <summary>
    /// The multisample state of the graphics pipeline.
    /// </summary>
    public GpuMultisampleState MultisampleState { get; set; }

    /// <summary>
    /// The depth-stencil state of the graphics pipeline.
    /// </summary>
    public GpuDepthStencilState DepthStencilState { get; set; }

    /// <summary>
    /// The color target descriptions for the pipeline.
    /// </summary>
    public GpuColorTargetDescription[]? ColorTargetDescriptions { get; set; }

    /// <summary>
    /// The pixel format of the depth-stencil target.
    /// </summary>
    public GpuTextureFormat DepthStencilFormat { get; set; }

    /// <summary>
    /// True specifies that the pipeline uses a depth-stencil target.
    /// </summary>
    public bool HasDepthStencilTarget { get; set; }
}

/// <summary>
/// Describes a vertex buffer used in a graphics pipeline.
/// </summary>
public struct GpuVertexBufferDescription
{
    /// <summary>
    /// The binding slot of the vertex buffer.
    /// </summary>
    public uint Slot { get; set; }

    /// <summary>
    /// The size of a single element + the offset between elements.
    /// </summary>
    public uint Pitch { get; set; }

    /// <summary>
    /// Whether attribute addressing is a function of the vertex index or instance index.
    /// </summary>
    public GpuVertexInputRate InputRate { get; set; }

    /// <summary>
    /// Reserved for future use. Must be set to 0.
    /// </summary>
    public uint InstanceStepRate { get; set; }
}

/// <summary>
/// Describes a vertex attribute in a graphics pipeline.
/// </summary>
public struct GpuVertexAttribute
{
    /// <summary>
    /// The shader input location index.
    /// </summary>
    public uint Location { get; set; }

    /// <summary>
    /// The binding slot of the associated vertex buffer.
    /// </summary>
    public uint BufferSlot { get; set; }

    /// <summary>
    /// The size and type of the attribute data.
    /// </summary>
    public GpuVertexElementFormat Format { get; set; }

    /// <summary>
    /// The byte offset of this attribute relative to the start of the vertex element.
    /// </summary>
    public uint Offset { get; set; }
}

/// <summary>
/// Describes the rasterizer state of a graphics pipeline.
/// </summary>
public struct GpuRasterizerState
{
    /// <summary>
    /// Whether polygons will be filled or drawn as lines.
    /// </summary>
    public GpuFillMode FillMode { get; set; }

    /// <summary>
    /// The facing direction in which triangles will be culled.
    /// </summary>
    public GpuCullMode CullMode { get; set; }

    /// <summary>
    /// The vertex winding that will cause a triangle to be determined as front-facing.
    /// </summary>
    public GpuFrontFace FrontFace { get; set; }

    /// <summary>
    /// A scalar factor controlling the depth value added to each fragment.
    /// </summary>
    public float DepthBiasConstantFactor { get; set; }

    /// <summary>
    /// The maximum depth bias of a fragment.
    /// </summary>
    public float DepthBiasClamp { get; set; }

    /// <summary>
    /// A scalar factor applied to a fragment's slope in depth calculations.
    /// </summary>
    public float DepthBiasSlopeFactor { get; set; }

    /// <summary>
    /// True to bias fragment depth values.
    /// </summary>
    public bool EnableDepthBias { get; set; }

    /// <summary>
    /// True to enable depth clip, false to enable depth clamp.
    /// </summary>
    public bool EnableDepthClip { get; set; }
}

/// <summary>
/// Describes the multisample state of a graphics pipeline.
/// </summary>
public struct GpuMultisampleState
{
    /// <summary>
    /// The number of samples to be used in rasterization.
    /// </summary>
    public GpuSampleCount SampleCount { get; set; }

    /// <summary>
    /// Reserved for future use. Must be set to 0.
    /// </summary>
    public uint SampleMask { get; set; }

    /// <summary>
    /// Reserved for future use. Must be set to false.
    /// </summary>
    public bool EnableMask { get; set; }
}

/// <summary>
/// Describes the depth-stencil state of a graphics pipeline.
/// </summary>
public struct GpuDepthStencilState
{
    /// <summary>
    /// The comparison operator used for depth testing.
    /// </summary>
    public GpuCompareOp CompareOp { get; set; }

    /// <summary>
    /// The stencil op state for back-facing triangles.
    /// </summary>
    public GpuStencilOpState BackStencilState { get; set; }

    /// <summary>
    /// The stencil op state for front-facing triangles.
    /// </summary>
    public GpuStencilOpState FrontStencilState { get; set; }

    /// <summary>
    /// Selects the bits of the stencil values participating in the stencil test.
    /// </summary>
    public byte CompareMask { get; set; }

    /// <summary>
    /// Selects the bits of the stencil values updated by the stencil test.
    /// </summary>
    public byte WriteMask { get; set; }

    /// <summary>
    /// True enables the depth test.
    /// </summary>
    public bool EnableDepthTest { get; set; }

    /// <summary>
    /// True enables depth writes.
    /// </summary>
    public bool EnableDepthWrite { get; set; }

    /// <summary>
    /// True enables the stencil test.
    /// </summary>
    public bool EnableStencilTest { get; set; }
}

/// <summary>
/// Describes the stencil operation state.
/// </summary>
public struct GpuStencilOpState
{
    /// <summary>
    /// The action performed on samples that fail the stencil test.
    /// </summary>
    public GpuStencilOp FailOp { get; set; }

    /// <summary>
    /// The action performed on samples that pass the depth and stencil tests.
    /// </summary>
    public GpuStencilOp PassOp { get; set; }

    /// <summary>
    /// The action performed on samples that pass the stencil test and fail the depth test.
    /// </summary>
    public GpuStencilOp DepthFailOp { get; set; }

    /// <summary>
    /// The comparison operator used in the stencil test.
    /// </summary>
    public GpuCompareOp CompareOp { get; set; }
}

/// <summary>
/// Describes a color target in a graphics pipeline.
/// </summary>
public struct GpuColorTargetDescription
{
    /// <summary>
    /// The pixel format of the texture to be used as a color target.
    /// </summary>
    public GpuTextureFormat Format { get; set; }

    /// <summary>
    /// The blend state to be used for the color target.
    /// </summary>
    public GpuColorTargetBlendState BlendState { get; set; }
}

/// <summary>
/// Describes the blend state of a color target.
/// </summary>
public struct GpuColorTargetBlendState
{
    /// <summary>
    /// The value to be multiplied by the source RGB value.
    /// </summary>
    public GpuBlendFactor SrcColorBlendFactor { get; set; }

    /// <summary>
    /// The value to be multiplied by the destination RGB value.
    /// </summary>
    public GpuBlendFactor DstColorBlendFactor { get; set; }

    /// <summary>
    /// The blend operation for the RGB components.
    /// </summary>
    public GpuBlendOp ColorBlendOp { get; set; }

    /// <summary>
    /// The value to be multiplied by the source alpha.
    /// </summary>
    public GpuBlendFactor SrcAlphaBlendFactor { get; set; }

    /// <summary>
    /// The value to be multiplied by the destination alpha.
    /// </summary>
    public GpuBlendFactor DstAlphaBlendFactor { get; set; }

    /// <summary>
    /// The blend operation for the alpha component.
    /// </summary>
    public GpuBlendOp AlphaBlendOp { get; set; }

    /// <summary>
    /// A bitmask specifying which of the RGBA components are enabled for writing.
    /// </summary>
    public GpuColorComponent ColorWriteMask { get; set; }

    /// <summary>
    /// Whether blending is enabled for the color target.
    /// </summary>
    public bool EnableBlend { get; set; }

    /// <summary>
    /// Whether the color write mask is enabled.
    /// </summary>
    public bool EnableColorWriteMask { get; set; }
}
