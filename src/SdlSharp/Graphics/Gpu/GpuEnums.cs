using SdlSharp.Native;

namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// Shader format flags indicating which shader bytecode formats a GPU device supports.
/// </summary>
[Flags]
public enum GpuShaderFormat : uint
{
    /// <summary>No valid shader format.</summary>
    Invalid = (uint)SDL_GPUShaderFormat.SDL_GPU_SHADERFORMAT_INVALID,

    /// <summary>Vendor-specific private shader format.</summary>
    Private = (uint)SDL_GPUShaderFormat.SDL_GPU_SHADERFORMAT_PRIVATE,

    /// <summary>SPIR-V shader format.</summary>
    Spirv = (uint)SDL_GPUShaderFormat.SDL_GPU_SHADERFORMAT_SPIRV,

    /// <summary>DXBC (DirectX Bytecode) shader format.</summary>
    Dxbc = (uint)SDL_GPUShaderFormat.SDL_GPU_SHADERFORMAT_DXBC,

    /// <summary>DXIL (DirectX Intermediate Language) shader format.</summary>
    Dxil = (uint)SDL_GPUShaderFormat.SDL_GPU_SHADERFORMAT_DXIL,

    /// <summary>MSL (Metal Shading Language) source shader format.</summary>
    Msl = (uint)SDL_GPUShaderFormat.SDL_GPU_SHADERFORMAT_MSL,

    /// <summary>Precompiled Metal library shader format.</summary>
    MetalLib = (uint)SDL_GPUShaderFormat.SDL_GPU_SHADERFORMAT_METALLIB,
}

/// <summary>
/// Presentation/vsync mode for the GPU swapchain.
/// </summary>
public enum GpuPresentMode
{
    /// <summary>Waits for vertical sync before presenting.</summary>
    VSync = (int)SDL_GPUPresentMode.SDL_GPU_PRESENTMODE_VSYNC,

    /// <summary>Presents immediately without waiting for vsync.</summary>
    Immediate = (int)SDL_GPUPresentMode.SDL_GPU_PRESENTMODE_IMMEDIATE,

    /// <summary>Uses a mailbox queue, replacing queued frames with the latest.</summary>
    Mailbox = (int)SDL_GPUPresentMode.SDL_GPU_PRESENTMODE_MAILBOX,
}

/// <summary>
/// Color space composition mode for the GPU swapchain.
/// </summary>
public enum GpuSwapchainComposition
{
    /// <summary>Standard dynamic range (SDR) output.</summary>
    Sdr = (int)SDL_GPUSwapchainComposition.SDL_GPU_SWAPCHAINCOMPOSITION_SDR,

    /// <summary>SDR with linear color space.</summary>
    SdrLinear = (int)SDL_GPUSwapchainComposition.SDL_GPU_SWAPCHAINCOMPOSITION_SDR_LINEAR,

    /// <summary>HDR with extended linear color space.</summary>
    HdrExtendedLinear = (int)SDL_GPUSwapchainComposition.SDL_GPU_SWAPCHAINCOMPOSITION_HDR_EXTENDED_LINEAR,

    /// <summary>HDR10 with ST.2084 (PQ) transfer function.</summary>
    Hdr10St2084 = (int)SDL_GPUSwapchainComposition.SDL_GPU_SWAPCHAINCOMPOSITION_HDR10_ST2084,
}

/// <summary>
/// GPU texture pixel formats. Values match the native <see cref="SDL_GPUTextureFormat"/> enum
/// numerically, so casting between them is safe. Individual members are not listed here due to
/// the large number of formats; use the native enum member names as a reference.
/// </summary>
public enum GpuTextureFormat
{
    /// <summary>Invalid/unspecified format.</summary>
    Invalid = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_INVALID,

    // Unsigned Normalized
    A8Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_A8_UNORM,
    R8Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8_UNORM,
    R8G8Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8G8_UNORM,
    R8G8B8A8Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UNORM,
    R16Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16_UNORM,
    R16G16Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16G16_UNORM,
    R16G16B16A16Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16G16B16A16_UNORM,
    R10G10B10A2Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R10G10B10A2_UNORM,
    B5G6R5Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_B5G6R5_UNORM,
    B5G5R5A1Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_B5G5R5A1_UNORM,
    B4G4R4A4Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_B4G4R4A4_UNORM,
    B8G8R8A8Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_B8G8R8A8_UNORM,

    // Compressed Unsigned Normalized
    Bc1RgbaUnorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_BC1_RGBA_UNORM,
    Bc2RgbaUnorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_BC2_RGBA_UNORM,
    Bc3RgbaUnorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_BC3_RGBA_UNORM,
    Bc4RUnorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_BC4_R_UNORM,
    Bc5RgUnorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_BC5_RG_UNORM,
    Bc7RgbaUnorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_BC7_RGBA_UNORM,

    // Compressed Signed/Unsigned Float
    Bc6hRgbFloat = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_BC6H_RGB_FLOAT,
    Bc6hRgbUfloat = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_BC6H_RGB_UFLOAT,

    // Signed Normalized
    R8Snorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8_SNORM,
    R8G8Snorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8G8_SNORM,
    R8G8B8A8Snorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8G8B8A8_SNORM,
    R16Snorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16_SNORM,
    R16G16Snorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16G16_SNORM,
    R16G16B16A16Snorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16G16B16A16_SNORM,

    // Signed Float
    R16Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16_FLOAT,
    R16G16Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16G16_FLOAT,
    R16G16B16A16Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16G16B16A16_FLOAT,
    R32Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R32_FLOAT,
    R32G32Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R32G32_FLOAT,
    R32G32B32A32Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R32G32B32A32_FLOAT,

    // Unsigned Float
    R11G11B10Ufloat = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R11G11B10_UFLOAT,

    // Unsigned Integer
    R8Uint = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8_UINT,
    R8G8Uint = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8G8_UINT,
    R8G8B8A8Uint = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UINT,
    R16Uint = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16_UINT,
    R16G16Uint = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16G16_UINT,
    R16G16B16A16Uint = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16G16B16A16_UINT,
    R32Uint = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R32_UINT,
    R32G32Uint = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R32G32_UINT,
    R32G32B32A32Uint = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R32G32B32A32_UINT,

    // Signed Integer
    R8Int = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8_INT,
    R8G8Int = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8G8_INT,
    R8G8B8A8Int = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8G8B8A8_INT,
    R16Int = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16_INT,
    R16G16Int = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16G16_INT,
    R16G16B16A16Int = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R16G16B16A16_INT,
    R32Int = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R32_INT,
    R32G32Int = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R32G32_INT,
    R32G32B32A32Int = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R32G32B32A32_INT,

    // sRGB
    R8G8B8A8UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UNORM_SRGB,
    B8G8R8A8UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_B8G8R8A8_UNORM_SRGB,

    // Compressed sRGB
    Bc1RgbaUnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_BC1_RGBA_UNORM_SRGB,
    Bc2RgbaUnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_BC2_RGBA_UNORM_SRGB,
    Bc3RgbaUnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_BC3_RGBA_UNORM_SRGB,
    Bc7RgbaUnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_BC7_RGBA_UNORM_SRGB,

    // Depth
    D16Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_D16_UNORM,
    D24Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_D24_UNORM,
    D32Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_D32_FLOAT,
    D24UnormS8Uint = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_D24_UNORM_S8_UINT,
    D32FloatS8Uint = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_D32_FLOAT_S8_UINT,

    // ASTC Normalized
    Astc4x4Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_4x4_UNORM,
    Astc5x4Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_5x4_UNORM,
    Astc5x5Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_5x5_UNORM,
    Astc6x5Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_6x5_UNORM,
    Astc6x6Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_6x6_UNORM,
    Astc8x5Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_8x5_UNORM,
    Astc8x6Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_8x6_UNORM,
    Astc8x8Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_8x8_UNORM,
    Astc10x5Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_10x5_UNORM,
    Astc10x6Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_10x6_UNORM,
    Astc10x8Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_10x8_UNORM,
    Astc10x10Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_10x10_UNORM,
    Astc12x10Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_12x10_UNORM,
    Astc12x12Unorm = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_12x12_UNORM,

    // ASTC sRGB
    Astc4x4UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_4x4_UNORM_SRGB,
    Astc5x4UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_5x4_UNORM_SRGB,
    Astc5x5UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_5x5_UNORM_SRGB,
    Astc6x5UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_6x5_UNORM_SRGB,
    Astc6x6UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_6x6_UNORM_SRGB,
    Astc8x5UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_8x5_UNORM_SRGB,
    Astc8x6UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_8x6_UNORM_SRGB,
    Astc8x8UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_8x8_UNORM_SRGB,
    Astc10x5UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_10x5_UNORM_SRGB,
    Astc10x6UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_10x6_UNORM_SRGB,
    Astc10x8UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_10x8_UNORM_SRGB,
    Astc10x10UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_10x10_UNORM_SRGB,
    Astc12x10UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_12x10_UNORM_SRGB,
    Astc12x12UnormSrgb = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_12x12_UNORM_SRGB,

    // ASTC Float
    Astc4x4Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_4x4_FLOAT,
    Astc5x4Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_5x4_FLOAT,
    Astc5x5Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_5x5_FLOAT,
    Astc6x5Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_6x5_FLOAT,
    Astc6x6Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_6x6_FLOAT,
    Astc8x5Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_8x5_FLOAT,
    Astc8x6Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_8x6_FLOAT,
    Astc8x8Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_8x8_FLOAT,
    Astc10x5Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_10x5_FLOAT,
    Astc10x6Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_10x6_FLOAT,
    Astc10x8Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_10x8_FLOAT,
    Astc10x10Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_10x10_FLOAT,
    Astc12x10Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_12x10_FLOAT,
    Astc12x12Float = (int)SDL_GPUTextureFormat.SDL_GPU_TEXTUREFORMAT_ASTC_12x12_FLOAT,
}

/// <summary>
/// Texture dimensionality type.
/// </summary>
public enum GpuTextureType
{
    Texture2D = (int)SDL_GPUTextureType.SDL_GPU_TEXTURETYPE_2D,
    Texture2DArray = (int)SDL_GPUTextureType.SDL_GPU_TEXTURETYPE_2D_ARRAY,
    Texture3D = (int)SDL_GPUTextureType.SDL_GPU_TEXTURETYPE_3D,
    Cube = (int)SDL_GPUTextureType.SDL_GPU_TEXTURETYPE_CUBE,
    CubeArray = (int)SDL_GPUTextureType.SDL_GPU_TEXTURETYPE_CUBE_ARRAY,
}

/// <summary>
/// Texture usage flags indicating how a texture will be used.
/// </summary>
[Flags]
public enum GpuTextureUsage : uint
{
    /// <summary>Texture can be used as a sampler source.</summary>
    Sampler = (uint)SDL_GPUTextureUsageFlags.SDL_GPU_TEXTUREUSAGE_SAMPLER,

    /// <summary>Texture can be used as a color render target.</summary>
    ColorTarget = (uint)SDL_GPUTextureUsageFlags.SDL_GPU_TEXTUREUSAGE_COLOR_TARGET,

    /// <summary>Texture can be used as a depth/stencil render target.</summary>
    DepthStencilTarget = (uint)SDL_GPUTextureUsageFlags.SDL_GPU_TEXTUREUSAGE_DEPTH_STENCIL_TARGET,

    /// <summary>Texture can be read from in a graphics shader.</summary>
    GraphicsStorageRead = (uint)SDL_GPUTextureUsageFlags.SDL_GPU_TEXTUREUSAGE_GRAPHICS_STORAGE_READ,

    /// <summary>Texture can be read from in a compute shader.</summary>
    ComputeStorageRead = (uint)SDL_GPUTextureUsageFlags.SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_READ,

    /// <summary>Texture can be written to in a compute shader.</summary>
    ComputeStorageWrite = (uint)SDL_GPUTextureUsageFlags.SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_WRITE,

    /// <summary>Texture supports simultaneous read-write in compute shaders.</summary>
    ComputeStorageSimultaneousReadWrite = (uint)SDL_GPUTextureUsageFlags.SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_SIMULTANEOUS_READ_WRITE,
}

/// <summary>
/// Multisample sample count for textures and render targets.
/// </summary>
public enum GpuSampleCount
{
    One = (int)SDL_GPUSampleCount.SDL_GPU_SAMPLECOUNT_1,
    Two = (int)SDL_GPUSampleCount.SDL_GPU_SAMPLECOUNT_2,
    Four = (int)SDL_GPUSampleCount.SDL_GPU_SAMPLECOUNT_4,
    Eight = (int)SDL_GPUSampleCount.SDL_GPU_SAMPLECOUNT_8,
}

/// <summary>
/// Primitive topology type for draw calls.
/// </summary>
public enum GpuPrimitiveType
{
    TriangleList = (int)SDL_GPUPrimitiveType.SDL_GPU_PRIMITIVETYPE_TRIANGLELIST,
    TriangleStrip = (int)SDL_GPUPrimitiveType.SDL_GPU_PRIMITIVETYPE_TRIANGLESTRIP,
    LineList = (int)SDL_GPUPrimitiveType.SDL_GPU_PRIMITIVETYPE_LINELIST,
    LineStrip = (int)SDL_GPUPrimitiveType.SDL_GPU_PRIMITIVETYPE_LINESTRIP,
    PointList = (int)SDL_GPUPrimitiveType.SDL_GPU_PRIMITIVETYPE_POINTLIST,
}

/// <summary>
/// Index buffer element size.
/// </summary>
public enum GpuIndexElementSize
{
    Sixteen = (int)SDL_GPUIndexElementSize.SDL_GPU_INDEXELEMENTSIZE_16BIT,
    ThirtyTwo = (int)SDL_GPUIndexElementSize.SDL_GPU_INDEXELEMENTSIZE_32BIT,
}

/// <summary>
/// Buffer usage flags indicating how a buffer will be used.
/// </summary>
[Flags]
public enum GpuBufferUsage : uint
{
    /// <summary>Buffer can be used as a vertex buffer.</summary>
    Vertex = (uint)SDL_GPUBufferUsageFlags.SDL_GPU_BUFFERUSAGE_VERTEX,

    /// <summary>Buffer can be used as an index buffer.</summary>
    Index = (uint)SDL_GPUBufferUsageFlags.SDL_GPU_BUFFERUSAGE_INDEX,

    /// <summary>Buffer can be used for indirect draw/dispatch arguments.</summary>
    Indirect = (uint)SDL_GPUBufferUsageFlags.SDL_GPU_BUFFERUSAGE_INDIRECT,

    /// <summary>Buffer can be read from in a graphics shader.</summary>
    GraphicsStorageRead = (uint)SDL_GPUBufferUsageFlags.SDL_GPU_BUFFERUSAGE_GRAPHICS_STORAGE_READ,

    /// <summary>Buffer can be read from in a compute shader.</summary>
    ComputeStorageRead = (uint)SDL_GPUBufferUsageFlags.SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_READ,

    /// <summary>Buffer can be written to in a compute shader.</summary>
    ComputeStorageWrite = (uint)SDL_GPUBufferUsageFlags.SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_WRITE,
}
