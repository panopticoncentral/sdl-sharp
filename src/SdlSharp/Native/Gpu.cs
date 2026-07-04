using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: SDL_GDKSuspendGPU/SDL_GDKResumeGPU (Xbox GDK only),
// SDL_GPUVulkanOptions (advanced Vulkan-specific configuration),
// SDL_GPUSupportsProperties (rarely needed vs SDL_GPUSupportsShaderFormats),
// SDL_GetPixelFormatFromGPUTextureFormat / SDL_GetGPUTextureFormatFromPixelFormat
// (niche 2D-render <-> GPU pixel-format conversion helpers, SDL 3.4).

// ──────────────────────────────────────────────────────────────
//  Opaque types
// ──────────────────────────────────────────────────────────────

/// <summary>Opaque handle for a GPU device context.</summary>
public struct SDL_GPUDevice;
/// <summary>Opaque handle for a GPU buffer.</summary>
public struct SDL_GPUBuffer;
/// <summary>Opaque handle for a GPU transfer buffer.</summary>
public struct SDL_GPUTransferBuffer;
/// <summary>Opaque handle for a GPU texture.</summary>
public struct SDL_GPUTexture;
/// <summary>Opaque handle for a GPU sampler.</summary>
public struct SDL_GPUSampler;
/// <summary>Opaque handle for a compiled GPU shader.</summary>
public struct SDL_GPUShader;
/// <summary>Opaque handle for a compute pipeline.</summary>
public struct SDL_GPUComputePipeline;
/// <summary>Opaque handle for a graphics pipeline.</summary>
public struct SDL_GPUGraphicsPipeline;
/// <summary>Opaque handle for a command buffer.</summary>
public struct SDL_GPUCommandBuffer;
/// <summary>Opaque handle for a render pass (transient).</summary>
public struct SDL_GPURenderPass;
/// <summary>Opaque handle for a compute pass (transient).</summary>
public struct SDL_GPUComputePass;
/// <summary>Opaque handle for a copy pass (transient).</summary>
public struct SDL_GPUCopyPass;
/// <summary>Opaque handle for a GPU fence.</summary>
public struct SDL_GPUFence;

// ──────────────────────────────────────────────────────────────
//  Enums
// ──────────────────────────────────────────────────────────────

/// <summary>Primitive topology types.</summary>
public enum SDL_GPUPrimitiveType
{
    SDL_GPU_PRIMITIVETYPE_TRIANGLELIST,
    SDL_GPU_PRIMITIVETYPE_TRIANGLESTRIP,
    SDL_GPU_PRIMITIVETYPE_LINELIST,
    SDL_GPU_PRIMITIVETYPE_LINESTRIP,
    SDL_GPU_PRIMITIVETYPE_POINTLIST,
}

/// <summary>How a render target is loaded at the beginning of a render pass.</summary>
public enum SDL_GPULoadOp
{
    SDL_GPU_LOADOP_LOAD,
    SDL_GPU_LOADOP_CLEAR,
    SDL_GPU_LOADOP_DONT_CARE,
}

/// <summary>How a render target is stored at the end of a render pass.</summary>
public enum SDL_GPUStoreOp
{
    SDL_GPU_STOREOP_STORE,
    SDL_GPU_STOREOP_DONT_CARE,
    SDL_GPU_STOREOP_RESOLVE,
    SDL_GPU_STOREOP_RESOLVE_AND_STORE,
}

/// <summary>Index buffer element size.</summary>
public enum SDL_GPUIndexElementSize
{
    SDL_GPU_INDEXELEMENTSIZE_16BIT = 0,
    SDL_GPU_INDEXELEMENTSIZE_32BIT = 1,
}

/// <summary>GPU texture formats.</summary>
public enum SDL_GPUTextureFormat
{
    SDL_GPU_TEXTUREFORMAT_INVALID = 0,

    // Unsigned Normalized
    SDL_GPU_TEXTUREFORMAT_A8_UNORM,
    SDL_GPU_TEXTUREFORMAT_R8_UNORM,
    SDL_GPU_TEXTUREFORMAT_R8G8_UNORM,
    SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UNORM,
    SDL_GPU_TEXTUREFORMAT_R16_UNORM,
    SDL_GPU_TEXTUREFORMAT_R16G16_UNORM,
    SDL_GPU_TEXTUREFORMAT_R16G16B16A16_UNORM,
    SDL_GPU_TEXTUREFORMAT_R10G10B10A2_UNORM,
    SDL_GPU_TEXTUREFORMAT_B5G6R5_UNORM,
    SDL_GPU_TEXTUREFORMAT_B5G5R5A1_UNORM,
    SDL_GPU_TEXTUREFORMAT_B4G4R4A4_UNORM,
    SDL_GPU_TEXTUREFORMAT_B8G8R8A8_UNORM,

    // Compressed Unsigned Normalized
    SDL_GPU_TEXTUREFORMAT_BC1_RGBA_UNORM,
    SDL_GPU_TEXTUREFORMAT_BC2_RGBA_UNORM,
    SDL_GPU_TEXTUREFORMAT_BC3_RGBA_UNORM,
    SDL_GPU_TEXTUREFORMAT_BC4_R_UNORM,
    SDL_GPU_TEXTUREFORMAT_BC5_RG_UNORM,
    SDL_GPU_TEXTUREFORMAT_BC7_RGBA_UNORM,

    // Compressed Signed/Unsigned Float
    SDL_GPU_TEXTUREFORMAT_BC6H_RGB_FLOAT,
    SDL_GPU_TEXTUREFORMAT_BC6H_RGB_UFLOAT,

    // Signed Normalized
    SDL_GPU_TEXTUREFORMAT_R8_SNORM,
    SDL_GPU_TEXTUREFORMAT_R8G8_SNORM,
    SDL_GPU_TEXTUREFORMAT_R8G8B8A8_SNORM,
    SDL_GPU_TEXTUREFORMAT_R16_SNORM,
    SDL_GPU_TEXTUREFORMAT_R16G16_SNORM,
    SDL_GPU_TEXTUREFORMAT_R16G16B16A16_SNORM,

    // Signed Float
    SDL_GPU_TEXTUREFORMAT_R16_FLOAT,
    SDL_GPU_TEXTUREFORMAT_R16G16_FLOAT,
    SDL_GPU_TEXTUREFORMAT_R16G16B16A16_FLOAT,
    SDL_GPU_TEXTUREFORMAT_R32_FLOAT,
    SDL_GPU_TEXTUREFORMAT_R32G32_FLOAT,
    SDL_GPU_TEXTUREFORMAT_R32G32B32A32_FLOAT,

    // Unsigned Float
    SDL_GPU_TEXTUREFORMAT_R11G11B10_UFLOAT,

    // Unsigned Integer
    SDL_GPU_TEXTUREFORMAT_R8_UINT,
    SDL_GPU_TEXTUREFORMAT_R8G8_UINT,
    SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UINT,
    SDL_GPU_TEXTUREFORMAT_R16_UINT,
    SDL_GPU_TEXTUREFORMAT_R16G16_UINT,
    SDL_GPU_TEXTUREFORMAT_R16G16B16A16_UINT,
    SDL_GPU_TEXTUREFORMAT_R32_UINT,
    SDL_GPU_TEXTUREFORMAT_R32G32_UINT,
    SDL_GPU_TEXTUREFORMAT_R32G32B32A32_UINT,

    // Signed Integer
    SDL_GPU_TEXTUREFORMAT_R8_INT,
    SDL_GPU_TEXTUREFORMAT_R8G8_INT,
    SDL_GPU_TEXTUREFORMAT_R8G8B8A8_INT,
    SDL_GPU_TEXTUREFORMAT_R16_INT,
    SDL_GPU_TEXTUREFORMAT_R16G16_INT,
    SDL_GPU_TEXTUREFORMAT_R16G16B16A16_INT,
    SDL_GPU_TEXTUREFORMAT_R32_INT,
    SDL_GPU_TEXTUREFORMAT_R32G32_INT,
    SDL_GPU_TEXTUREFORMAT_R32G32B32A32_INT,

    // sRGB
    SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_B8G8R8A8_UNORM_SRGB,

    // Compressed sRGB
    SDL_GPU_TEXTUREFORMAT_BC1_RGBA_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_BC2_RGBA_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_BC3_RGBA_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_BC7_RGBA_UNORM_SRGB,

    // Depth
    SDL_GPU_TEXTUREFORMAT_D16_UNORM,
    SDL_GPU_TEXTUREFORMAT_D24_UNORM,
    SDL_GPU_TEXTUREFORMAT_D32_FLOAT,
    SDL_GPU_TEXTUREFORMAT_D24_UNORM_S8_UINT,
    SDL_GPU_TEXTUREFORMAT_D32_FLOAT_S8_UINT,

    // ASTC Normalized
    SDL_GPU_TEXTUREFORMAT_ASTC_4x4_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_5x4_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_5x5_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_6x5_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_6x6_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_8x5_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_8x6_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_8x8_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_10x5_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_10x6_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_10x8_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_10x10_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_12x10_UNORM,
    SDL_GPU_TEXTUREFORMAT_ASTC_12x12_UNORM,

    // ASTC sRGB
    SDL_GPU_TEXTUREFORMAT_ASTC_4x4_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_5x4_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_5x5_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_6x5_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_6x6_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_8x5_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_8x6_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_8x8_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_10x5_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_10x6_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_10x8_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_10x10_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_12x10_UNORM_SRGB,
    SDL_GPU_TEXTUREFORMAT_ASTC_12x12_UNORM_SRGB,

    // ASTC Float
    SDL_GPU_TEXTUREFORMAT_ASTC_4x4_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_5x4_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_5x5_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_6x5_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_6x6_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_8x5_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_8x6_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_8x8_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_10x5_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_10x6_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_10x8_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_10x10_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_12x10_FLOAT,
    SDL_GPU_TEXTUREFORMAT_ASTC_12x12_FLOAT,
}

/// <summary>Texture dimensionality type.</summary>
public enum SDL_GPUTextureType
{
    SDL_GPU_TEXTURETYPE_2D = 0,
    SDL_GPU_TEXTURETYPE_2D_ARRAY = 1,
    SDL_GPU_TEXTURETYPE_3D = 2,
    SDL_GPU_TEXTURETYPE_CUBE = 3,
    SDL_GPU_TEXTURETYPE_CUBE_ARRAY = 4,
}

/// <summary>Multisample sample count.</summary>
public enum SDL_GPUSampleCount
{
    SDL_GPU_SAMPLECOUNT_1 = 0,
    SDL_GPU_SAMPLECOUNT_2 = 1,
    SDL_GPU_SAMPLECOUNT_4 = 2,
    SDL_GPU_SAMPLECOUNT_8 = 3,
}

/// <summary>Cube map face index.</summary>
public enum SDL_GPUCubeMapFace
{
    SDL_GPU_CUBEMAPFACE_POSITIVEX = 0,
    SDL_GPU_CUBEMAPFACE_NEGATIVEX = 1,
    SDL_GPU_CUBEMAPFACE_POSITIVEY = 2,
    SDL_GPU_CUBEMAPFACE_NEGATIVEY = 3,
    SDL_GPU_CUBEMAPFACE_POSITIVEZ = 4,
    SDL_GPU_CUBEMAPFACE_NEGATIVEZ = 5,
}

/// <summary>Transfer buffer usage.</summary>
public enum SDL_GPUTransferBufferUsage
{
    SDL_GPU_TRANSFERBUFFERUSAGE_UPLOAD = 0,
    SDL_GPU_TRANSFERBUFFERUSAGE_DOWNLOAD = 1,
}

/// <summary>Shader pipeline stage.</summary>
public enum SDL_GPUShaderStage
{
    SDL_GPU_SHADERSTAGE_VERTEX = 0,
    SDL_GPU_SHADERSTAGE_FRAGMENT = 1,
}

/// <summary>Vertex element data format.</summary>
public enum SDL_GPUVertexElementFormat
{
    SDL_GPU_VERTEXELEMENTFORMAT_INVALID = 0,

    // 32-bit
    SDL_GPU_VERTEXELEMENTFORMAT_INT,
    SDL_GPU_VERTEXELEMENTFORMAT_INT2,
    SDL_GPU_VERTEXELEMENTFORMAT_INT3,
    SDL_GPU_VERTEXELEMENTFORMAT_INT4,
    SDL_GPU_VERTEXELEMENTFORMAT_UINT,
    SDL_GPU_VERTEXELEMENTFORMAT_UINT2,
    SDL_GPU_VERTEXELEMENTFORMAT_UINT3,
    SDL_GPU_VERTEXELEMENTFORMAT_UINT4,
    SDL_GPU_VERTEXELEMENTFORMAT_FLOAT,
    SDL_GPU_VERTEXELEMENTFORMAT_FLOAT2,
    SDL_GPU_VERTEXELEMENTFORMAT_FLOAT3,
    SDL_GPU_VERTEXELEMENTFORMAT_FLOAT4,

    // 8-bit
    SDL_GPU_VERTEXELEMENTFORMAT_BYTE2,
    SDL_GPU_VERTEXELEMENTFORMAT_BYTE4,
    SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2,
    SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4,
    SDL_GPU_VERTEXELEMENTFORMAT_BYTE2_NORM,
    SDL_GPU_VERTEXELEMENTFORMAT_BYTE4_NORM,
    SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2_NORM,
    SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4_NORM,

    // 16-bit
    SDL_GPU_VERTEXELEMENTFORMAT_SHORT2,
    SDL_GPU_VERTEXELEMENTFORMAT_SHORT4,
    SDL_GPU_VERTEXELEMENTFORMAT_USHORT2,
    SDL_GPU_VERTEXELEMENTFORMAT_USHORT4,
    SDL_GPU_VERTEXELEMENTFORMAT_SHORT2_NORM,
    SDL_GPU_VERTEXELEMENTFORMAT_SHORT4_NORM,
    SDL_GPU_VERTEXELEMENTFORMAT_USHORT2_NORM,
    SDL_GPU_VERTEXELEMENTFORMAT_USHORT4_NORM,

    // Half float
    SDL_GPU_VERTEXELEMENTFORMAT_HALF2,
    SDL_GPU_VERTEXELEMENTFORMAT_HALF4,
}

/// <summary>Vertex input rate.</summary>
public enum SDL_GPUVertexInputRate
{
    SDL_GPU_VERTEXINPUTRATE_VERTEX = 0,
    SDL_GPU_VERTEXINPUTRATE_INSTANCE = 1,
}

/// <summary>Polygon fill mode.</summary>
public enum SDL_GPUFillMode
{
    SDL_GPU_FILLMODE_FILL = 0,
    SDL_GPU_FILLMODE_LINE = 1,
}

/// <summary>Face culling mode.</summary>
public enum SDL_GPUCullMode
{
    SDL_GPU_CULLMODE_NONE = 0,
    SDL_GPU_CULLMODE_FRONT = 1,
    SDL_GPU_CULLMODE_BACK = 2,
}

/// <summary>Front face winding order.</summary>
public enum SDL_GPUFrontFace
{
    SDL_GPU_FRONTFACE_COUNTER_CLOCKWISE = 0,
    SDL_GPU_FRONTFACE_CLOCKWISE = 1,
}

/// <summary>Comparison operator.</summary>
public enum SDL_GPUCompareOp
{
    SDL_GPU_COMPAREOP_INVALID = 0,
    SDL_GPU_COMPAREOP_NEVER,
    SDL_GPU_COMPAREOP_LESS,
    SDL_GPU_COMPAREOP_EQUAL,
    SDL_GPU_COMPAREOP_LESS_OR_EQUAL,
    SDL_GPU_COMPAREOP_GREATER,
    SDL_GPU_COMPAREOP_NOT_EQUAL,
    SDL_GPU_COMPAREOP_GREATER_OR_EQUAL,
    SDL_GPU_COMPAREOP_ALWAYS,
}

/// <summary>Stencil operation.</summary>
public enum SDL_GPUStencilOp
{
    SDL_GPU_STENCILOP_INVALID = 0,
    SDL_GPU_STENCILOP_KEEP,
    SDL_GPU_STENCILOP_ZERO,
    SDL_GPU_STENCILOP_REPLACE,
    SDL_GPU_STENCILOP_INCREMENT_AND_CLAMP,
    SDL_GPU_STENCILOP_DECREMENT_AND_CLAMP,
    SDL_GPU_STENCILOP_INVERT,
    SDL_GPU_STENCILOP_INCREMENT_AND_WRAP,
    SDL_GPU_STENCILOP_DECREMENT_AND_WRAP,
}

/// <summary>Blend operation.</summary>
public enum SDL_GPUBlendOp
{
    SDL_GPU_BLENDOP_INVALID = 0,
    SDL_GPU_BLENDOP_ADD,
    SDL_GPU_BLENDOP_SUBTRACT,
    SDL_GPU_BLENDOP_REVERSE_SUBTRACT,
    SDL_GPU_BLENDOP_MIN,
    SDL_GPU_BLENDOP_MAX,
}

/// <summary>Blend factor.</summary>
public enum SDL_GPUBlendFactor
{
    SDL_GPU_BLENDFACTOR_INVALID = 0,
    SDL_GPU_BLENDFACTOR_ZERO,
    SDL_GPU_BLENDFACTOR_ONE,
    SDL_GPU_BLENDFACTOR_SRC_COLOR,
    SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_COLOR,
    SDL_GPU_BLENDFACTOR_DST_COLOR,
    SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_COLOR,
    SDL_GPU_BLENDFACTOR_SRC_ALPHA,
    SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_ALPHA,
    SDL_GPU_BLENDFACTOR_DST_ALPHA,
    SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_ALPHA,
    SDL_GPU_BLENDFACTOR_CONSTANT_COLOR,
    SDL_GPU_BLENDFACTOR_ONE_MINUS_CONSTANT_COLOR,
    SDL_GPU_BLENDFACTOR_SRC_ALPHA_SATURATE,
}

/// <summary>Texture filter mode.</summary>
public enum SDL_GPUFilter
{
    SDL_GPU_FILTER_NEAREST = 0,
    SDL_GPU_FILTER_LINEAR = 1,
}

/// <summary>Mipmap filter mode.</summary>
public enum SDL_GPUSamplerMipmapMode
{
    SDL_GPU_SAMPLERMIPMAPMODE_NEAREST = 0,
    SDL_GPU_SAMPLERMIPMAPMODE_LINEAR = 1,
}

/// <summary>Sampler address (wrap) mode.</summary>
public enum SDL_GPUSamplerAddressMode
{
    SDL_GPU_SAMPLERADDRESSMODE_REPEAT = 0,
    SDL_GPU_SAMPLERADDRESSMODE_MIRRORED_REPEAT = 1,
    SDL_GPU_SAMPLERADDRESSMODE_CLAMP_TO_EDGE = 2,
}

/// <summary>Presentation/vsync mode.</summary>
public enum SDL_GPUPresentMode
{
    SDL_GPU_PRESENTMODE_VSYNC = 0,
    SDL_GPU_PRESENTMODE_IMMEDIATE = 1,
    SDL_GPU_PRESENTMODE_MAILBOX = 2,
}

/// <summary>Swapchain color space composition.</summary>
public enum SDL_GPUSwapchainComposition
{
    SDL_GPU_SWAPCHAINCOMPOSITION_SDR = 0,
    SDL_GPU_SWAPCHAINCOMPOSITION_SDR_LINEAR = 1,
    SDL_GPU_SWAPCHAINCOMPOSITION_HDR_EXTENDED_LINEAR = 2,
    SDL_GPU_SWAPCHAINCOMPOSITION_HDR10_ST2084 = 3,
}

// ──────────────────────────────────────────────────────────────
//  Flag enums (defined as Uint32/#define in C)
// ──────────────────────────────────────────────────────────────

/// <summary>Texture usage flags.</summary>
[Flags]
public enum SDL_GPUTextureUsageFlags : uint
{
    SDL_GPU_TEXTUREUSAGE_SAMPLER = 1u << 0,
    SDL_GPU_TEXTUREUSAGE_COLOR_TARGET = 1u << 1,
    SDL_GPU_TEXTUREUSAGE_DEPTH_STENCIL_TARGET = 1u << 2,
    SDL_GPU_TEXTUREUSAGE_GRAPHICS_STORAGE_READ = 1u << 3,
    SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_READ = 1u << 4,
    SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_WRITE = 1u << 5,
    SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_SIMULTANEOUS_READ_WRITE = 1u << 6,
}

/// <summary>Buffer usage flags.</summary>
[Flags]
public enum SDL_GPUBufferUsageFlags : uint
{
    SDL_GPU_BUFFERUSAGE_VERTEX = 1u << 0,
    SDL_GPU_BUFFERUSAGE_INDEX = 1u << 1,
    SDL_GPU_BUFFERUSAGE_INDIRECT = 1u << 2,
    SDL_GPU_BUFFERUSAGE_GRAPHICS_STORAGE_READ = 1u << 3,
    SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_READ = 1u << 4,
    SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_WRITE = 1u << 5,
}

/// <summary>Shader format flags.</summary>
[Flags]
public enum SDL_GPUShaderFormat : uint
{
    SDL_GPU_SHADERFORMAT_INVALID = 0,
    SDL_GPU_SHADERFORMAT_PRIVATE = 1u << 0,
    SDL_GPU_SHADERFORMAT_SPIRV = 1u << 1,
    SDL_GPU_SHADERFORMAT_DXBC = 1u << 2,
    SDL_GPU_SHADERFORMAT_DXIL = 1u << 3,
    SDL_GPU_SHADERFORMAT_MSL = 1u << 4,
    SDL_GPU_SHADERFORMAT_METALLIB = 1u << 5,
}

/// <summary>Color component write mask flags.</summary>
[Flags]
public enum SDL_GPUColorComponentFlags : byte
{
    SDL_GPU_COLORCOMPONENT_R = 1 << 0,
    SDL_GPU_COLORCOMPONENT_G = 1 << 1,
    SDL_GPU_COLORCOMPONENT_B = 1 << 2,
    SDL_GPU_COLORCOMPONENT_A = 1 << 3,
}

// ──────────────────────────────────────────────────────────────
//  Structs
// ──────────────────────────────────────────────────────────────

/// <summary>GPU viewport.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUViewport
{
    public float x, y, w, h;
    public float min_depth, max_depth;
}

/// <summary>Source info for texture transfer operations.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUTextureTransferInfo
{
    public SDL_GPUTransferBuffer* transfer_buffer;
    public uint offset;
    public uint pixels_per_row;
    public uint rows_per_layer;
}

/// <summary>Location within a transfer buffer.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUTransferBufferLocation
{
    public SDL_GPUTransferBuffer* transfer_buffer;
    public uint offset;
}

/// <summary>Location within a texture.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUTextureLocation
{
    public SDL_GPUTexture* texture;
    public uint mip_level;
    public uint layer;
    public uint x, y, z;
}

/// <summary>Region within a texture.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUTextureRegion
{
    public SDL_GPUTexture* texture;
    public uint mip_level;
    public uint layer;
    public uint x, y, z;
    public uint w, h, d;
}

/// <summary>Region for blit operations.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUBlitRegion
{
    public SDL_GPUTexture* texture;
    public uint mip_level;
    public uint layer_or_depth_plane;
    public uint x, y, w, h;
}

/// <summary>Location within a buffer.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUBufferLocation
{
    public SDL_GPUBuffer* buffer;
    public uint offset;
}

/// <summary>Region within a buffer.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUBufferRegion
{
    public SDL_GPUBuffer* buffer;
    public uint offset;
    public uint size;
}

/// <summary>Indirect draw command arguments.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUIndirectDrawCommand
{
    public uint num_vertices;
    public uint num_instances;
    public uint first_vertex;
    public uint first_instance;
}

/// <summary>Indexed indirect draw command arguments.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUIndexedIndirectDrawCommand
{
    public uint num_indices;
    public uint num_instances;
    public uint first_index;
    public int vertex_offset;
    public uint first_instance;
}

/// <summary>Indirect dispatch command arguments.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUIndirectDispatchCommand
{
    public uint groupcount_x;
    public uint groupcount_y;
    public uint groupcount_z;
}

/// <summary>Sampler creation parameters.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUSamplerCreateInfo
{
    public SDL_GPUFilter min_filter;
    public SDL_GPUFilter mag_filter;
    public SDL_GPUSamplerMipmapMode mipmap_mode;
    public SDL_GPUSamplerAddressMode address_mode_u;
    public SDL_GPUSamplerAddressMode address_mode_v;
    public SDL_GPUSamplerAddressMode address_mode_w;
    public float mip_lod_bias;
    public float max_anisotropy;
    public SDL_GPUCompareOp compare_op;
    public float min_lod;
    public float max_lod;
    public byte enable_anisotropy;
    public byte enable_compare;
    public byte padding1;
    public byte padding2;
    public SDL_PropertiesID props;
}

/// <summary>Vertex buffer description.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUVertexBufferDescription
{
    public uint slot;
    public uint pitch;
    public SDL_GPUVertexInputRate input_rate;
    public uint instance_step_rate;
}

/// <summary>Vertex attribute description.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUVertexAttribute
{
    public uint location;
    public uint buffer_slot;
    public SDL_GPUVertexElementFormat format;
    public uint offset;
}

/// <summary>Vertex input state for graphics pipeline creation.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUVertexInputState
{
    public SDL_GPUVertexBufferDescription* vertex_buffer_descriptions;
    public uint num_vertex_buffers;
    public SDL_GPUVertexAttribute* vertex_attributes;
    public uint num_vertex_attributes;
}

/// <summary>Stencil operation state.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUStencilOpState
{
    public SDL_GPUStencilOp fail_op;
    public SDL_GPUStencilOp pass_op;
    public SDL_GPUStencilOp depth_fail_op;
    public SDL_GPUCompareOp compare_op;
}

/// <summary>Color target blend state.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUColorTargetBlendState
{
    public SDL_GPUBlendFactor src_color_blendfactor;
    public SDL_GPUBlendFactor dst_color_blendfactor;
    public SDL_GPUBlendOp color_blend_op;
    public SDL_GPUBlendFactor src_alpha_blendfactor;
    public SDL_GPUBlendFactor dst_alpha_blendfactor;
    public SDL_GPUBlendOp alpha_blend_op;
    public SDL_GPUColorComponentFlags color_write_mask;
    public byte enable_blend;
    public byte enable_color_write_mask;
    public byte padding1;
    public byte padding2;
}

/// <summary>Shader creation parameters.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUShaderCreateInfo
{
    public nuint code_size;
    public byte* code;
    public byte* entrypoint;
    public SDL_GPUShaderFormat format;
    public SDL_GPUShaderStage stage;
    public uint num_samplers;
    public uint num_storage_textures;
    public uint num_storage_buffers;
    public uint num_uniform_buffers;
    public SDL_PropertiesID props;
}

/// <summary>Texture creation parameters.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUTextureCreateInfo
{
    public SDL_GPUTextureType type;
    public SDL_GPUTextureFormat format;
    public SDL_GPUTextureUsageFlags usage;
    public uint width;
    public uint height;
    public uint layer_count_or_depth;
    public uint num_levels;
    public SDL_GPUSampleCount sample_count;
    public SDL_PropertiesID props;
}

/// <summary>Buffer creation parameters.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUBufferCreateInfo
{
    public SDL_GPUBufferUsageFlags usage;
    public uint size;
    public SDL_PropertiesID props;
}

/// <summary>Transfer buffer creation parameters.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUTransferBufferCreateInfo
{
    public SDL_GPUTransferBufferUsage usage;
    public uint size;
    public SDL_PropertiesID props;
}

/// <summary>Rasterizer state.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPURasterizerState
{
    public SDL_GPUFillMode fill_mode;
    public SDL_GPUCullMode cull_mode;
    public SDL_GPUFrontFace front_face;
    public float depth_bias_constant_factor;
    public float depth_bias_clamp;
    public float depth_bias_slope_factor;
    public byte enable_depth_bias;
    public byte enable_depth_clip;
    public byte padding1;
    public byte padding2;
}

/// <summary>Multisample state.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUMultisampleState
{
    public SDL_GPUSampleCount sample_count;
    public uint sample_mask;
    public byte enable_mask;
    public byte enable_alpha_to_coverage;
    public byte padding2;
    public byte padding3;
}

/// <summary>Depth/stencil state.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUDepthStencilState
{
    public SDL_GPUCompareOp compare_op;
    public SDL_GPUStencilOpState back_stencil_state;
    public SDL_GPUStencilOpState front_stencil_state;
    public byte compare_mask;
    public byte write_mask;
    public byte enable_depth_test;
    public byte enable_depth_write;
    public byte enable_stencil_test;
    public byte padding1;
    public byte padding2;
    public byte padding3;
}

/// <summary>Color target description for pipeline creation.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUColorTargetDescription
{
    public SDL_GPUTextureFormat format;
    public SDL_GPUColorTargetBlendState blend_state;
}

/// <summary>Pipeline target info for graphics pipeline creation.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUGraphicsPipelineTargetInfo
{
    public SDL_GPUColorTargetDescription* color_target_descriptions;
    public uint num_color_targets;
    public SDL_GPUTextureFormat depth_stencil_format;
    public byte has_depth_stencil_target;
    public byte padding1;
    public byte padding2;
    public byte padding3;
}

/// <summary>Graphics pipeline creation parameters.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUGraphicsPipelineCreateInfo
{
    public SDL_GPUShader* vertex_shader;
    public SDL_GPUShader* fragment_shader;
    public SDL_GPUVertexInputState vertex_input_state;
    public SDL_GPUPrimitiveType primitive_type;
    public SDL_GPURasterizerState rasterizer_state;
    public SDL_GPUMultisampleState multisample_state;
    public SDL_GPUDepthStencilState depth_stencil_state;
    public SDL_GPUGraphicsPipelineTargetInfo target_info;
    public SDL_PropertiesID props;
}

/// <summary>Compute pipeline creation parameters.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUComputePipelineCreateInfo
{
    public nuint code_size;
    public byte* code;
    public byte* entrypoint;
    public SDL_GPUShaderFormat format;
    public uint num_samplers;
    public uint num_readonly_storage_textures;
    public uint num_readonly_storage_buffers;
    public uint num_readwrite_storage_textures;
    public uint num_readwrite_storage_buffers;
    public uint num_uniform_buffers;
    public uint threadcount_x;
    public uint threadcount_y;
    public uint threadcount_z;
    public SDL_PropertiesID props;
}

/// <summary>Color target info for render pass creation.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUColorTargetInfo
{
    public SDL_GPUTexture* texture;
    public uint mip_level;
    public uint layer_or_depth_plane;
    public SDL_FColor clear_color;
    public SDL_GPULoadOp load_op;
    public SDL_GPUStoreOp store_op;
    public SDL_GPUTexture* resolve_texture;
    public uint resolve_mip_level;
    public uint resolve_layer;
    public byte cycle;
    public byte cycle_resolve_texture;
    public byte padding1;
    public byte padding2;
}

/// <summary>Depth/stencil target info for render pass creation.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUDepthStencilTargetInfo
{
    public SDL_GPUTexture* texture;
    public float clear_depth;
    public SDL_GPULoadOp load_op;
    public SDL_GPUStoreOp store_op;
    public SDL_GPULoadOp stencil_load_op;
    public SDL_GPUStoreOp stencil_store_op;
    public byte cycle;
    public byte clear_stencil;
    public byte mip_level;
    public byte layer;
}

/// <summary>Blit operation info.</summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_GPUBlitInfo
{
    public SDL_GPUBlitRegion source;
    public SDL_GPUBlitRegion destination;
    public SDL_GPULoadOp load_op;
    public SDL_FColor clear_color;
    public SDL_FlipMode flip_mode;
    public SDL_GPUFilter filter;
    public byte cycle;
    public byte padding1;
    public byte padding2;
    public byte padding3;
}

/// <summary>Buffer binding for vertex/index buffers.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUBufferBinding
{
    public SDL_GPUBuffer* buffer;
    public uint offset;
}

/// <summary>Texture-sampler pair binding.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUTextureSamplerBinding
{
    public SDL_GPUTexture* texture;
    public SDL_GPUSampler* sampler;
}

/// <summary>Storage buffer read-write binding.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUStorageBufferReadWriteBinding
{
    public SDL_GPUBuffer* buffer;
    public byte cycle;
    public byte padding1;
    public byte padding2;
    public byte padding3;
}

/// <summary>Storage texture read-write binding.</summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SDL_GPUStorageTextureReadWriteBinding
{
    public SDL_GPUTexture* texture;
    public uint mip_level;
    public uint layer;
    public byte cycle;
    public byte padding1;
    public byte padding2;
    public byte padding3;
}

// ──────────────────────────────────────────────────────────────
//  Functions
// ──────────────────────────────────────────────────────────────

/// <summary>
/// Native bindings for SDL_gpu.h — GPU rendering API.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Gpu
{
    // --- Device property constants ---

    /// <summary>GPU device name property.</summary>
    public static ReadOnlySpan<byte> SDL_PROP_GPU_DEVICE_NAME_STRING => "SDL.gpu.device.name"u8;
    /// <summary>GPU driver name property.</summary>
    public static ReadOnlySpan<byte> SDL_PROP_GPU_DEVICE_DRIVER_NAME_STRING => "SDL.gpu.device.driver_name"u8;
    /// <summary>GPU driver version property.</summary>
    public static ReadOnlySpan<byte> SDL_PROP_GPU_DEVICE_DRIVER_VERSION_STRING => "SDL.gpu.device.driver_version"u8;
    /// <summary>GPU driver info property.</summary>
    public static ReadOnlySpan<byte> SDL_PROP_GPU_DEVICE_DRIVER_INFO_STRING => "SDL.gpu.device.driver_info"u8;

    // --- Device create property names ---

    /// <summary>Enable debug mode properties and validations.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_DEBUGMODE_BOOLEAN = "SDL.gpu.device.create.debugmode";
    /// <summary>Prefer energy efficiency over maximum GPU performance.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_PREFERLOWPOWER_BOOLEAN = "SDL.gpu.device.create.preferlowpower";
    /// <summary>Automatically log useful debug information on device creation.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_VERBOSE_BOOLEAN = "SDL.gpu.device.create.verbose";
    /// <summary>The name of the GPU driver to use.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_NAME_STRING = "SDL.gpu.device.create.name";
    /// <summary>Enable Vulkan clip distance support.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_FEATURE_CLIP_DISTANCE_BOOLEAN = "SDL.gpu.device.create.feature.clip_distance";
    /// <summary>Enable depth clamping support.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_FEATURE_DEPTH_CLAMPING_BOOLEAN = "SDL.gpu.device.create.feature.depth_clamping";
    /// <summary>Enable indirect draw first-instance support.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_FEATURE_INDIRECT_DRAW_FIRST_INSTANCE_BOOLEAN = "SDL.gpu.device.create.feature.indirect_draw_first_instance";
    /// <summary>Enable anisotropic filtering support.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_FEATURE_ANISOTROPY_BOOLEAN = "SDL.gpu.device.create.feature.anisotropy";
    /// <summary>The app is able to provide private (NDA) shaders.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_PRIVATE_BOOLEAN = "SDL.gpu.device.create.shaders.private";
    /// <summary>The app is able to provide SPIR-V shaders.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_SPIRV_BOOLEAN = "SDL.gpu.device.create.shaders.spirv";
    /// <summary>The app is able to provide DXBC shaders.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_DXBC_BOOLEAN = "SDL.gpu.device.create.shaders.dxbc";
    /// <summary>The app is able to provide DXIL shaders.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_DXIL_BOOLEAN = "SDL.gpu.device.create.shaders.dxil";
    /// <summary>The app is able to provide MSL shaders.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_MSL_BOOLEAN = "SDL.gpu.device.create.shaders.msl";
    /// <summary>The app is able to provide Metal library shaders.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_METALLIB_BOOLEAN = "SDL.gpu.device.create.shaders.metallib";
    /// <summary>Allow D3D12 tier-1 resource binding hardware.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_D3D12_ALLOW_FEWER_RESOURCE_SLOTS_BOOLEAN = "SDL.gpu.device.create.d3d12.allowtier1resourcebinding";
    /// <summary>The D3D12 semantic name prefix for vertex attributes.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_D3D12_SEMANTIC_NAME_STRING = "SDL.gpu.device.create.d3d12.semantic";
    /// <summary>The D3D12 Agility SDK version.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_D3D12_AGILITY_SDK_VERSION_NUMBER = "SDL.gpu.device.create.d3d12.agility_sdk_version";
    /// <summary>The D3D12 Agility SDK path.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_D3D12_AGILITY_SDK_PATH_STRING = "SDL.gpu.device.create.d3d12.agility_sdk_path";
    /// <summary>Require Vulkan hardware acceleration.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_VULKAN_REQUIRE_HARDWARE_ACCELERATION_BOOLEAN = "SDL.gpu.device.create.vulkan.requirehardwareacceleration";
    /// <summary>A pointer to an SDL_GPUVulkanOptions structure.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_VULKAN_OPTIONS_POINTER = "SDL.gpu.device.create.vulkan.options";
    /// <summary>Allow the Metal MTLGPUFamilyMac1 hardware tier.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_METAL_ALLOW_MACFAMILY1_BOOLEAN = "SDL.gpu.device.create.metal.allowmacfamily1";

    // --- Device creation ---

    /// <summary>Checks if a GPU backend supports given shader formats.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GPUSupportsShaderFormats")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GPUSupportsShaderFormats(SDL_GPUShaderFormat format_flags, ReadOnlySpan<byte> name);

    /// <summary>Create a GPU device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateGPUDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUDevice* SDL_CreateGPUDevice(SDL_GPUShaderFormat format_flags, [MarshalAs(UnmanagedType.U1)] bool debug_mode, ReadOnlySpan<byte> name);

    /// <summary>Create a GPU device with properties.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateGPUDeviceWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUDevice* SDL_CreateGPUDeviceWithProperties(SDL_PropertiesID props);

    /// <summary>Destroy a GPU device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyGPUDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DestroyGPUDevice(SDL_GPUDevice* device);

    /// <summary>Get the number of GPU drivers compiled into SDL.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumGPUDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumGPUDrivers();

    /// <summary>Get the name of a built-in GPU driver.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGPUDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGPUDriver(int index);

    /// <summary>Get the name of the backend used by a GPU device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGPUDeviceDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetGPUDeviceDriver(SDL_GPUDevice* device);

    /// <summary>Get the supported shader formats for a GPU device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGPUShaderFormats")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUShaderFormat SDL_GetGPUShaderFormats(SDL_GPUDevice* device);

    /// <summary>Get the properties of a GPU device.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGPUDeviceProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PropertiesID SDL_GetGPUDeviceProperties(SDL_GPUDevice* device);

    // --- Pipeline creation ---

    /// <summary>Create a compute pipeline.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateGPUComputePipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUComputePipeline* SDL_CreateGPUComputePipeline(SDL_GPUDevice* device, SDL_GPUComputePipelineCreateInfo* createinfo);

    /// <summary>Create a graphics pipeline.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateGPUGraphicsPipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUGraphicsPipeline* SDL_CreateGPUGraphicsPipeline(SDL_GPUDevice* device, SDL_GPUGraphicsPipelineCreateInfo* createinfo);

    /// <summary>Create a sampler.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateGPUSampler")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUSampler* SDL_CreateGPUSampler(SDL_GPUDevice* device, SDL_GPUSamplerCreateInfo* createinfo);

    /// <summary>Create a shader.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateGPUShader")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUShader* SDL_CreateGPUShader(SDL_GPUDevice* device, SDL_GPUShaderCreateInfo* createinfo);

    // --- Resource creation ---

    /// <summary>Create a texture.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateGPUTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUTexture* SDL_CreateGPUTexture(SDL_GPUDevice* device, SDL_GPUTextureCreateInfo* createinfo);

    /// <summary>Create a buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateGPUBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUBuffer* SDL_CreateGPUBuffer(SDL_GPUDevice* device, SDL_GPUBufferCreateInfo* createinfo);

    /// <summary>Create a transfer buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateGPUTransferBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUTransferBuffer* SDL_CreateGPUTransferBuffer(SDL_GPUDevice* device, SDL_GPUTransferBufferCreateInfo* createinfo);

    // --- Resource release ---

    /// <summary>Release a texture.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReleaseGPUTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ReleaseGPUTexture(SDL_GPUDevice* device, SDL_GPUTexture* texture);

    /// <summary>Release a sampler.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReleaseGPUSampler")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ReleaseGPUSampler(SDL_GPUDevice* device, SDL_GPUSampler* sampler);

    /// <summary>Release a buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReleaseGPUBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ReleaseGPUBuffer(SDL_GPUDevice* device, SDL_GPUBuffer* buffer);

    /// <summary>Release a transfer buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReleaseGPUTransferBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ReleaseGPUTransferBuffer(SDL_GPUDevice* device, SDL_GPUTransferBuffer* transfer_buffer);

    /// <summary>Release a compute pipeline.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReleaseGPUComputePipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ReleaseGPUComputePipeline(SDL_GPUDevice* device, SDL_GPUComputePipeline* compute_pipeline);

    /// <summary>Release a shader.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReleaseGPUShader")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ReleaseGPUShader(SDL_GPUDevice* device, SDL_GPUShader* shader);

    /// <summary>Release a graphics pipeline.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReleaseGPUGraphicsPipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ReleaseGPUGraphicsPipeline(SDL_GPUDevice* device, SDL_GPUGraphicsPipeline* graphics_pipeline);

    // --- Command buffer ---

    /// <summary>Acquire a command buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AcquireGPUCommandBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUCommandBuffer* SDL_AcquireGPUCommandBuffer(SDL_GPUDevice* device);

    // --- Uniform data ---

    /// <summary>Push vertex shader uniform data.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PushGPUVertexUniformData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_PushGPUVertexUniformData(SDL_GPUCommandBuffer* command_buffer, uint slot_index, void* data, uint length);

    /// <summary>Push fragment shader uniform data.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PushGPUFragmentUniformData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_PushGPUFragmentUniformData(SDL_GPUCommandBuffer* command_buffer, uint slot_index, void* data, uint length);

    /// <summary>Push compute shader uniform data.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PushGPUComputeUniformData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_PushGPUComputeUniformData(SDL_GPUCommandBuffer* command_buffer, uint slot_index, void* data, uint length);

    // --- Render pass ---

    /// <summary>Begin a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BeginGPURenderPass")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPURenderPass* SDL_BeginGPURenderPass(SDL_GPUCommandBuffer* command_buffer, SDL_GPUColorTargetInfo* color_target_infos, uint num_color_targets, SDL_GPUDepthStencilTargetInfo* depth_stencil_target_info);

    /// <summary>Bind a graphics pipeline to a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUGraphicsPipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUGraphicsPipeline(SDL_GPURenderPass* render_pass, SDL_GPUGraphicsPipeline* graphics_pipeline);

    /// <summary>Set the viewport on a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGPUViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetGPUViewport(SDL_GPURenderPass* render_pass, SDL_GPUViewport* viewport);

    /// <summary>Set the scissor rect on a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGPUScissor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetGPUScissor(SDL_GPURenderPass* render_pass, SDL_Rect* scissor);

    /// <summary>Set blend constants on a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGPUBlendConstants")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetGPUBlendConstants(SDL_GPURenderPass* render_pass, SDL_FColor blend_constants);

    /// <summary>Set the stencil reference value on a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGPUStencilReference")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetGPUStencilReference(SDL_GPURenderPass* render_pass, byte reference);

    /// <summary>Bind vertex buffers to a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUVertexBuffers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUVertexBuffers(SDL_GPURenderPass* render_pass, uint first_slot, SDL_GPUBufferBinding* bindings, uint num_bindings);

    /// <summary>Bind an index buffer to a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUIndexBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUIndexBuffer(SDL_GPURenderPass* render_pass, SDL_GPUBufferBinding* binding, SDL_GPUIndexElementSize index_element_size);

    /// <summary>Bind vertex samplers to a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUVertexSamplers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUVertexSamplers(SDL_GPURenderPass* render_pass, uint first_slot, SDL_GPUTextureSamplerBinding* texture_sampler_bindings, uint num_bindings);

    /// <summary>Bind fragment samplers to a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUFragmentSamplers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUFragmentSamplers(SDL_GPURenderPass* render_pass, uint first_slot, SDL_GPUTextureSamplerBinding* texture_sampler_bindings, uint num_bindings);

    /// <summary>Bind vertex storage textures to a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUVertexStorageTextures")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUVertexStorageTextures(SDL_GPURenderPass* render_pass, uint first_slot, SDL_GPUTexture** storage_textures, uint num_bindings);

    /// <summary>Bind fragment storage textures to a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUFragmentStorageTextures")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUFragmentStorageTextures(SDL_GPURenderPass* render_pass, uint first_slot, SDL_GPUTexture** storage_textures, uint num_bindings);

    /// <summary>Bind vertex storage buffers to a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUVertexStorageBuffers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUVertexStorageBuffers(SDL_GPURenderPass* render_pass, uint first_slot, SDL_GPUBuffer** storage_buffers, uint num_bindings);

    /// <summary>Bind fragment storage buffers to a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUFragmentStorageBuffers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUFragmentStorageBuffers(SDL_GPURenderPass* render_pass, uint first_slot, SDL_GPUBuffer** storage_buffers, uint num_bindings);

    // --- Draw commands ---

    /// <summary>Draw non-indexed primitives.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DrawGPUPrimitives")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DrawGPUPrimitives(SDL_GPURenderPass* render_pass, uint num_vertices, uint num_instances, uint first_vertex, uint first_instance);

    /// <summary>Draw indexed primitives.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DrawGPUIndexedPrimitives")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DrawGPUIndexedPrimitives(SDL_GPURenderPass* render_pass, uint num_indices, uint num_instances, uint first_index, int vertex_offset, uint first_instance);

    /// <summary>Draw primitives with indirect arguments.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DrawGPUPrimitivesIndirect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DrawGPUPrimitivesIndirect(SDL_GPURenderPass* render_pass, SDL_GPUBuffer* buffer, uint offset, uint draw_count);

    /// <summary>Draw indexed primitives with indirect arguments.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DrawGPUIndexedPrimitivesIndirect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DrawGPUIndexedPrimitivesIndirect(SDL_GPURenderPass* render_pass, SDL_GPUBuffer* buffer, uint offset, uint draw_count);

    /// <summary>End a render pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_EndGPURenderPass")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_EndGPURenderPass(SDL_GPURenderPass* render_pass);

    // --- Compute pass ---

    /// <summary>Begin a compute pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BeginGPUComputePass")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUComputePass* SDL_BeginGPUComputePass(SDL_GPUCommandBuffer* command_buffer, SDL_GPUStorageTextureReadWriteBinding* storage_texture_bindings, uint num_storage_texture_bindings, SDL_GPUStorageBufferReadWriteBinding* storage_buffer_bindings, uint num_storage_buffer_bindings);

    /// <summary>Bind a compute pipeline to a compute pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUComputePipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUComputePipeline(SDL_GPUComputePass* compute_pass, SDL_GPUComputePipeline* compute_pipeline);

    /// <summary>Bind compute samplers to a compute pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUComputeSamplers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUComputeSamplers(SDL_GPUComputePass* compute_pass, uint first_slot, SDL_GPUTextureSamplerBinding* texture_sampler_bindings, uint num_bindings);

    /// <summary>Bind compute storage textures to a compute pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUComputeStorageTextures")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUComputeStorageTextures(SDL_GPUComputePass* compute_pass, uint first_slot, SDL_GPUTexture** storage_textures, uint num_bindings);

    /// <summary>Bind compute storage buffers to a compute pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BindGPUComputeStorageBuffers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BindGPUComputeStorageBuffers(SDL_GPUComputePass* compute_pass, uint first_slot, SDL_GPUBuffer** storage_buffers, uint num_bindings);

    /// <summary>Dispatch a compute workgroup.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DispatchGPUCompute")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DispatchGPUCompute(SDL_GPUComputePass* compute_pass, uint groupcount_x, uint groupcount_y, uint groupcount_z);

    /// <summary>Dispatch a compute workgroup with indirect arguments.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DispatchGPUComputeIndirect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DispatchGPUComputeIndirect(SDL_GPUComputePass* compute_pass, SDL_GPUBuffer* buffer, uint offset);

    /// <summary>End a compute pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_EndGPUComputePass")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_EndGPUComputePass(SDL_GPUComputePass* compute_pass);

    // --- Transfer buffer ---

    /// <summary>Map a transfer buffer for CPU access.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_MapGPUTransferBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void* SDL_MapGPUTransferBuffer(SDL_GPUDevice* device, SDL_GPUTransferBuffer* transfer_buffer, [MarshalAs(UnmanagedType.U1)] bool cycle);

    /// <summary>Unmap a transfer buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UnmapGPUTransferBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_UnmapGPUTransferBuffer(SDL_GPUDevice* device, SDL_GPUTransferBuffer* transfer_buffer);

    // --- Copy pass ---

    /// <summary>Begin a copy pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BeginGPUCopyPass")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUCopyPass* SDL_BeginGPUCopyPass(SDL_GPUCommandBuffer* command_buffer);

    /// <summary>Upload data to a GPU texture.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UploadToGPUTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_UploadToGPUTexture(SDL_GPUCopyPass* copy_pass, SDL_GPUTextureTransferInfo* source, SDL_GPUTextureRegion* destination, [MarshalAs(UnmanagedType.U1)] bool cycle);

    /// <summary>Upload data to a GPU buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UploadToGPUBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_UploadToGPUBuffer(SDL_GPUCopyPass* copy_pass, SDL_GPUTransferBufferLocation* source, SDL_GPUBufferRegion* destination, [MarshalAs(UnmanagedType.U1)] bool cycle);

    /// <summary>Copy data between GPU textures.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CopyGPUTextureToTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_CopyGPUTextureToTexture(SDL_GPUCopyPass* copy_pass, SDL_GPUTextureLocation* source, SDL_GPUTextureLocation* destination, uint w, uint h, uint d, [MarshalAs(UnmanagedType.U1)] bool cycle);

    /// <summary>Copy data between GPU buffers.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CopyGPUBufferToBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_CopyGPUBufferToBuffer(SDL_GPUCopyPass* copy_pass, SDL_GPUBufferLocation* source, SDL_GPUBufferLocation* destination, uint size, [MarshalAs(UnmanagedType.U1)] bool cycle);

    /// <summary>Download data from a GPU texture.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DownloadFromGPUTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DownloadFromGPUTexture(SDL_GPUCopyPass* copy_pass, SDL_GPUTextureRegion* source, SDL_GPUTextureTransferInfo* destination);

    /// <summary>Download data from a GPU buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DownloadFromGPUBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DownloadFromGPUBuffer(SDL_GPUCopyPass* copy_pass, SDL_GPUBufferRegion* source, SDL_GPUTransferBufferLocation* destination);

    /// <summary>End a copy pass.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_EndGPUCopyPass")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_EndGPUCopyPass(SDL_GPUCopyPass* copy_pass);

    // --- Texture operations ---

    /// <summary>Generate mipmaps for a texture.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GenerateMipmapsForGPUTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_GenerateMipmapsForGPUTexture(SDL_GPUCommandBuffer* command_buffer, SDL_GPUTexture* texture);

    /// <summary>Blit from one texture region to another.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_BlitGPUTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_BlitGPUTexture(SDL_GPUCommandBuffer* command_buffer, SDL_GPUBlitInfo* info);

    // --- Swapchain ---

    /// <summary>Check if a window supports a swapchain composition.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WindowSupportsGPUSwapchainComposition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_WindowSupportsGPUSwapchainComposition(SDL_GPUDevice* device, SDL_Window* window, SDL_GPUSwapchainComposition swapchain_composition);

    /// <summary>Check if a window supports a present mode.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WindowSupportsGPUPresentMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_WindowSupportsGPUPresentMode(SDL_GPUDevice* device, SDL_Window* window, SDL_GPUPresentMode present_mode);

    /// <summary>Claim a window for GPU presentation.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ClaimWindowForGPUDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ClaimWindowForGPUDevice(SDL_GPUDevice* device, SDL_Window* window);

    /// <summary>Release a window from GPU presentation.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReleaseWindowFromGPUDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ReleaseWindowFromGPUDevice(SDL_GPUDevice* device, SDL_Window* window);

    /// <summary>Set swapchain parameters for a window.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGPUSwapchainParameters")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetGPUSwapchainParameters(SDL_GPUDevice* device, SDL_Window* window, SDL_GPUSwapchainComposition swapchain_composition, SDL_GPUPresentMode present_mode);

    /// <summary>Set the maximum number of frames in flight.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGPUAllowedFramesInFlight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetGPUAllowedFramesInFlight(SDL_GPUDevice* device, uint allowed_frames_in_flight);

    /// <summary>Get the texture format used by the swapchain.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGPUSwapchainTextureFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUTextureFormat SDL_GetGPUSwapchainTextureFormat(SDL_GPUDevice* device, SDL_Window* window);

    /// <summary>Acquire a swapchain texture for rendering.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_AcquireGPUSwapchainTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_AcquireGPUSwapchainTexture(SDL_GPUCommandBuffer* command_buffer, SDL_Window* window, SDL_GPUTexture** swapchain_texture, uint* swapchain_texture_width, uint* swapchain_texture_height);

    /// <summary>Wait until a swapchain texture is available.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WaitForGPUSwapchain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_WaitForGPUSwapchain(SDL_GPUDevice* device, SDL_Window* window);

    /// <summary>Wait for and acquire a swapchain texture.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WaitAndAcquireGPUSwapchainTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_WaitAndAcquireGPUSwapchainTexture(SDL_GPUCommandBuffer* command_buffer, SDL_Window* window, SDL_GPUTexture** swapchain_texture, uint* swapchain_texture_width, uint* swapchain_texture_height);

    // --- Submission & fences ---

    /// <summary>Submit a command buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SubmitGPUCommandBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SubmitGPUCommandBuffer(SDL_GPUCommandBuffer* command_buffer);

    /// <summary>Submit a command buffer and acquire a fence.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SubmitGPUCommandBufferAndAcquireFence")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_GPUFence* SDL_SubmitGPUCommandBufferAndAcquireFence(SDL_GPUCommandBuffer* command_buffer);

    /// <summary>Cancel a command buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CancelGPUCommandBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_CancelGPUCommandBuffer(SDL_GPUCommandBuffer* command_buffer);

    /// <summary>Wait for the GPU to be idle.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WaitForGPUIdle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_WaitForGPUIdle(SDL_GPUDevice* device);

    /// <summary>Wait for GPU fences.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WaitForGPUFences")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_WaitForGPUFences(SDL_GPUDevice* device, [MarshalAs(UnmanagedType.U1)] bool wait_all, SDL_GPUFence** fences, uint num_fences);

    /// <summary>Query whether a fence has been signaled.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_QueryGPUFence")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_QueryGPUFence(SDL_GPUDevice* device, SDL_GPUFence* fence);

    /// <summary>Release a fence.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ReleaseGPUFence")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_ReleaseGPUFence(SDL_GPUDevice* device, SDL_GPUFence* fence);

    // --- Format queries ---

    /// <summary>Get the texel block size of a texture format in bytes.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GPUTextureFormatTexelBlockSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_GPUTextureFormatTexelBlockSize(SDL_GPUTextureFormat format);

    /// <summary>Check if a texture format is supported.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GPUTextureSupportsFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GPUTextureSupportsFormat(SDL_GPUDevice* device, SDL_GPUTextureFormat format, SDL_GPUTextureType type, SDL_GPUTextureUsageFlags usage);

    /// <summary>Check if a texture format supports a sample count.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GPUTextureSupportsSampleCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GPUTextureSupportsSampleCount(SDL_GPUDevice* device, SDL_GPUTextureFormat format, SDL_GPUSampleCount sample_count);

    /// <summary>Calculate the byte size needed for a texture format.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CalculateGPUTextureFormatSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_CalculateGPUTextureFormatSize(SDL_GPUTextureFormat format, uint width, uint height, uint depth_or_layer_count);

    // --- Debug naming ---

    /// <summary>Set a debug name for a buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGPUBufferName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetGPUBufferName(SDL_GPUDevice* device, SDL_GPUBuffer* buffer, ReadOnlySpan<byte> text);

    /// <summary>Set a debug name for a texture.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetGPUTextureName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_SetGPUTextureName(SDL_GPUDevice* device, SDL_GPUTexture* texture, ReadOnlySpan<byte> text);

    /// <summary>Insert a debug label into a command buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_InsertGPUDebugLabel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_InsertGPUDebugLabel(SDL_GPUCommandBuffer* command_buffer, ReadOnlySpan<byte> text);

    /// <summary>Push a debug group onto the command buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PushGPUDebugGroup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_PushGPUDebugGroup(SDL_GPUCommandBuffer* command_buffer, ReadOnlySpan<byte> name);

    /// <summary>Pop a debug group from the command buffer.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_PopGPUDebugGroup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_PopGPUDebugGroup(SDL_GPUCommandBuffer* command_buffer);
}
