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

/// <summary>
/// How a render target is loaded at the beginning of a render pass.
/// </summary>
public enum GpuLoadOp
{
    /// <summary>The previous contents of the texture will be preserved.</summary>
    Load = (int)SDL_GPULoadOp.SDL_GPU_LOADOP_LOAD,

    /// <summary>The contents of the texture will be cleared to a color.</summary>
    Clear = (int)SDL_GPULoadOp.SDL_GPU_LOADOP_CLEAR,

    /// <summary>The previous contents of the texture need not be preserved; the contents will be undefined.</summary>
    DontCare = (int)SDL_GPULoadOp.SDL_GPU_LOADOP_DONT_CARE,
}

/// <summary>
/// How a render target is stored at the end of a render pass.
/// </summary>
public enum GpuStoreOp
{
    /// <summary>The contents generated during the render pass will be written to memory.</summary>
    Store = (int)SDL_GPUStoreOp.SDL_GPU_STOREOP_STORE,

    /// <summary>The contents generated during the render pass are not needed and may be discarded; the contents will be undefined.</summary>
    DontCare = (int)SDL_GPUStoreOp.SDL_GPU_STOREOP_DONT_CARE,

    /// <summary>The multisample contents will be resolved to a non-multisample texture; the multisample contents may then be discarded and will be undefined.</summary>
    Resolve = (int)SDL_GPUStoreOp.SDL_GPU_STOREOP_RESOLVE,

    /// <summary>The multisample contents will be resolved to a non-multisample texture, and the multisample contents will also be written to memory.</summary>
    ResolveAndStore = (int)SDL_GPUStoreOp.SDL_GPU_STOREOP_RESOLVE_AND_STORE,
}

/// <summary>
/// Shader pipeline stage.
/// </summary>
public enum GpuShaderStage
{
    /// <summary>Vertex shader stage.</summary>
    Vertex = (int)SDL_GPUShaderStage.SDL_GPU_SHADERSTAGE_VERTEX,

    /// <summary>Fragment shader stage.</summary>
    Fragment = (int)SDL_GPUShaderStage.SDL_GPU_SHADERSTAGE_FRAGMENT,
}

/// <summary>
/// Transfer buffer usage.
/// </summary>
public enum GpuTransferBufferUsage
{
    /// <summary>Transfer buffer is used to upload data to the GPU.</summary>
    Upload = (int)SDL_GPUTransferBufferUsage.SDL_GPU_TRANSFERBUFFERUSAGE_UPLOAD,

    /// <summary>Transfer buffer is used to download data from the GPU.</summary>
    Download = (int)SDL_GPUTransferBufferUsage.SDL_GPU_TRANSFERBUFFERUSAGE_DOWNLOAD,
}

/// <summary>
/// Cube map face index.
/// </summary>
public enum GpuCubeMapFace
{
    /// <summary>The +X face of the cube map.</summary>
    PositiveX = (int)SDL_GPUCubeMapFace.SDL_GPU_CUBEMAPFACE_POSITIVEX,

    /// <summary>The -X face of the cube map.</summary>
    NegativeX = (int)SDL_GPUCubeMapFace.SDL_GPU_CUBEMAPFACE_NEGATIVEX,

    /// <summary>The +Y face of the cube map.</summary>
    PositiveY = (int)SDL_GPUCubeMapFace.SDL_GPU_CUBEMAPFACE_POSITIVEY,

    /// <summary>The -Y face of the cube map.</summary>
    NegativeY = (int)SDL_GPUCubeMapFace.SDL_GPU_CUBEMAPFACE_NEGATIVEY,

    /// <summary>The +Z face of the cube map.</summary>
    PositiveZ = (int)SDL_GPUCubeMapFace.SDL_GPU_CUBEMAPFACE_POSITIVEZ,

    /// <summary>The -Z face of the cube map.</summary>
    NegativeZ = (int)SDL_GPUCubeMapFace.SDL_GPU_CUBEMAPFACE_NEGATIVEZ,
}

/// <summary>
/// Vertex element data format.
/// </summary>
public enum GpuVertexElementFormat
{
    /// <summary>No valid vertex element format.</summary>
    Invalid = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_INVALID,

    // 32-bit
    /// <summary>One 32-bit signed integer.</summary>
    Int = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_INT,
    /// <summary>Two 32-bit signed integers.</summary>
    Int2 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_INT2,
    /// <summary>Three 32-bit signed integers.</summary>
    Int3 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_INT3,
    /// <summary>Four 32-bit signed integers.</summary>
    Int4 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_INT4,
    /// <summary>One 32-bit unsigned integer.</summary>
    Uint = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UINT,
    /// <summary>Two 32-bit unsigned integers.</summary>
    Uint2 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UINT2,
    /// <summary>Three 32-bit unsigned integers.</summary>
    Uint3 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UINT3,
    /// <summary>Four 32-bit unsigned integers.</summary>
    Uint4 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UINT4,
    /// <summary>One 32-bit float.</summary>
    Float = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_FLOAT,
    /// <summary>Two 32-bit floats.</summary>
    Float2 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_FLOAT2,
    /// <summary>Three 32-bit floats.</summary>
    Float3 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_FLOAT3,
    /// <summary>Four 32-bit floats.</summary>
    Float4 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_FLOAT4,

    // 8-bit
    /// <summary>Two 8-bit signed integers.</summary>
    Byte2 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_BYTE2,
    /// <summary>Four 8-bit signed integers.</summary>
    Byte4 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_BYTE4,
    /// <summary>Two 8-bit unsigned integers.</summary>
    Ubyte2 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2,
    /// <summary>Four 8-bit unsigned integers.</summary>
    Ubyte4 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4,
    /// <summary>Two 8-bit signed normalized integers.</summary>
    Byte2Norm = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_BYTE2_NORM,
    /// <summary>Four 8-bit signed normalized integers.</summary>
    Byte4Norm = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_BYTE4_NORM,
    /// <summary>Two 8-bit unsigned normalized integers.</summary>
    Ubyte2Norm = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2_NORM,
    /// <summary>Four 8-bit unsigned normalized integers.</summary>
    Ubyte4Norm = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4_NORM,

    // 16-bit
    /// <summary>Two 16-bit signed integers.</summary>
    Short2 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_SHORT2,
    /// <summary>Four 16-bit signed integers.</summary>
    Short4 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_SHORT4,
    /// <summary>Two 16-bit unsigned integers.</summary>
    Ushort2 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_USHORT2,
    /// <summary>Four 16-bit unsigned integers.</summary>
    Ushort4 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_USHORT4,
    /// <summary>Two 16-bit signed normalized integers.</summary>
    Short2Norm = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_SHORT2_NORM,
    /// <summary>Four 16-bit signed normalized integers.</summary>
    Short4Norm = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_SHORT4_NORM,
    /// <summary>Two 16-bit unsigned normalized integers.</summary>
    Ushort2Norm = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_USHORT2_NORM,
    /// <summary>Four 16-bit unsigned normalized integers.</summary>
    Ushort4Norm = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_USHORT4_NORM,

    // Half float
    /// <summary>Two 16-bit half-precision floats.</summary>
    Half2 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_HALF2,
    /// <summary>Four 16-bit half-precision floats.</summary>
    Half4 = (int)SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_HALF4,
}

/// <summary>
/// Vertex input rate.
/// </summary>
public enum GpuVertexInputRate
{
    /// <summary>Attribute addressing is a function of the vertex index.</summary>
    Vertex = (int)SDL_GPUVertexInputRate.SDL_GPU_VERTEXINPUTRATE_VERTEX,

    /// <summary>Attribute addressing is a function of the instance index.</summary>
    Instance = (int)SDL_GPUVertexInputRate.SDL_GPU_VERTEXINPUTRATE_INSTANCE,
}

/// <summary>
/// Polygon fill mode.
/// </summary>
public enum GpuFillMode
{
    /// <summary>Polygons will be rendered via rasterization.</summary>
    Fill = (int)SDL_GPUFillMode.SDL_GPU_FILLMODE_FILL,

    /// <summary>Polygon edges will be drawn as line segments.</summary>
    Line = (int)SDL_GPUFillMode.SDL_GPU_FILLMODE_LINE,
}

/// <summary>
/// Face culling mode.
/// </summary>
public enum GpuCullMode
{
    /// <summary>No triangles are culled.</summary>
    None = (int)SDL_GPUCullMode.SDL_GPU_CULLMODE_NONE,

    /// <summary>Front-facing triangles are culled.</summary>
    Front = (int)SDL_GPUCullMode.SDL_GPU_CULLMODE_FRONT,

    /// <summary>Back-facing triangles are culled.</summary>
    Back = (int)SDL_GPUCullMode.SDL_GPU_CULLMODE_BACK,
}

/// <summary>
/// Front face winding order.
/// </summary>
public enum GpuFrontFace
{
    /// <summary>A triangle with counter-clockwise vertex winding will be considered front-facing.</summary>
    CounterClockwise = (int)SDL_GPUFrontFace.SDL_GPU_FRONTFACE_COUNTER_CLOCKWISE,

    /// <summary>A triangle with clockwise vertex winding will be considered front-facing.</summary>
    Clockwise = (int)SDL_GPUFrontFace.SDL_GPU_FRONTFACE_CLOCKWISE,
}

/// <summary>
/// Comparison operator.
/// </summary>
public enum GpuCompareOp
{
    /// <summary>No valid comparison operator.</summary>
    Invalid = (int)SDL_GPUCompareOp.SDL_GPU_COMPAREOP_INVALID,

    /// <summary>The comparison always evaluates false.</summary>
    Never = (int)SDL_GPUCompareOp.SDL_GPU_COMPAREOP_NEVER,

    /// <summary>The comparison evaluates reference &lt; test.</summary>
    Less = (int)SDL_GPUCompareOp.SDL_GPU_COMPAREOP_LESS,

    /// <summary>The comparison evaluates reference == test.</summary>
    Equal = (int)SDL_GPUCompareOp.SDL_GPU_COMPAREOP_EQUAL,

    /// <summary>The comparison evaluates reference &lt;= test.</summary>
    LessOrEqual = (int)SDL_GPUCompareOp.SDL_GPU_COMPAREOP_LESS_OR_EQUAL,

    /// <summary>The comparison evaluates reference &gt; test.</summary>
    Greater = (int)SDL_GPUCompareOp.SDL_GPU_COMPAREOP_GREATER,

    /// <summary>The comparison evaluates reference != test.</summary>
    NotEqual = (int)SDL_GPUCompareOp.SDL_GPU_COMPAREOP_NOT_EQUAL,

    /// <summary>The comparison evaluates reference &gt;= test.</summary>
    GreaterOrEqual = (int)SDL_GPUCompareOp.SDL_GPU_COMPAREOP_GREATER_OR_EQUAL,

    /// <summary>The comparison always evaluates true.</summary>
    Always = (int)SDL_GPUCompareOp.SDL_GPU_COMPAREOP_ALWAYS,
}

/// <summary>
/// Stencil operation.
/// </summary>
public enum GpuStencilOp
{
    /// <summary>No valid stencil operation.</summary>
    Invalid = (int)SDL_GPUStencilOp.SDL_GPU_STENCILOP_INVALID,

    /// <summary>Keeps the current value.</summary>
    Keep = (int)SDL_GPUStencilOp.SDL_GPU_STENCILOP_KEEP,

    /// <summary>Sets the value to 0.</summary>
    Zero = (int)SDL_GPUStencilOp.SDL_GPU_STENCILOP_ZERO,

    /// <summary>Sets the value to reference.</summary>
    Replace = (int)SDL_GPUStencilOp.SDL_GPU_STENCILOP_REPLACE,

    /// <summary>Increments the current value and clamps to the maximum value.</summary>
    IncrementAndClamp = (int)SDL_GPUStencilOp.SDL_GPU_STENCILOP_INCREMENT_AND_CLAMP,

    /// <summary>Decrements the current value and clamps to 0.</summary>
    DecrementAndClamp = (int)SDL_GPUStencilOp.SDL_GPU_STENCILOP_DECREMENT_AND_CLAMP,

    /// <summary>Bitwise-inverts the current value.</summary>
    Invert = (int)SDL_GPUStencilOp.SDL_GPU_STENCILOP_INVERT,

    /// <summary>Increments the current value and wraps back to 0.</summary>
    IncrementAndWrap = (int)SDL_GPUStencilOp.SDL_GPU_STENCILOP_INCREMENT_AND_WRAP,

    /// <summary>Decrements the current value and wraps to the maximum value.</summary>
    DecrementAndWrap = (int)SDL_GPUStencilOp.SDL_GPU_STENCILOP_DECREMENT_AND_WRAP,
}

/// <summary>
/// Blend operation.
/// </summary>
public enum GpuBlendOp
{
    /// <summary>No valid blend operation.</summary>
    Invalid = (int)SDL_GPUBlendOp.SDL_GPU_BLENDOP_INVALID,

    /// <summary>(source * source_factor) + (destination * destination_factor).</summary>
    Add = (int)SDL_GPUBlendOp.SDL_GPU_BLENDOP_ADD,

    /// <summary>(source * source_factor) - (destination * destination_factor).</summary>
    Subtract = (int)SDL_GPUBlendOp.SDL_GPU_BLENDOP_SUBTRACT,

    /// <summary>(destination * destination_factor) - (source * source_factor).</summary>
    ReverseSubtract = (int)SDL_GPUBlendOp.SDL_GPU_BLENDOP_REVERSE_SUBTRACT,

    /// <summary>min(source, destination).</summary>
    Min = (int)SDL_GPUBlendOp.SDL_GPU_BLENDOP_MIN,

    /// <summary>max(source, destination).</summary>
    Max = (int)SDL_GPUBlendOp.SDL_GPU_BLENDOP_MAX,
}

/// <summary>
/// Blend factor.
/// </summary>
public enum GpuBlendFactor
{
    /// <summary>No valid blend factor.</summary>
    Invalid = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_INVALID,

    /// <summary>0.</summary>
    Zero = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ZERO,

    /// <summary>1.</summary>
    One = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ONE,

    /// <summary>Source color.</summary>
    SrcColor = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_SRC_COLOR,

    /// <summary>1 - source color.</summary>
    OneMinusSrcColor = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_COLOR,

    /// <summary>Destination color.</summary>
    DstColor = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_DST_COLOR,

    /// <summary>1 - destination color.</summary>
    OneMinusDstColor = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_COLOR,

    /// <summary>Source alpha.</summary>
    SrcAlpha = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_SRC_ALPHA,

    /// <summary>1 - source alpha.</summary>
    OneMinusSrcAlpha = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_ALPHA,

    /// <summary>Destination alpha.</summary>
    DstAlpha = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_DST_ALPHA,

    /// <summary>1 - destination alpha.</summary>
    OneMinusDstAlpha = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_ALPHA,

    /// <summary>Blend constant.</summary>
    ConstantColor = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_CONSTANT_COLOR,

    /// <summary>1 - blend constant.</summary>
    OneMinusConstantColor = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_ONE_MINUS_CONSTANT_COLOR,

    /// <summary>min(source alpha, 1 - destination alpha).</summary>
    SrcAlphaSaturate = (int)SDL_GPUBlendFactor.SDL_GPU_BLENDFACTOR_SRC_ALPHA_SATURATE,
}

/// <summary>
/// Texture filter mode.
/// </summary>
public enum GpuFilter
{
    /// <summary>Point filtering.</summary>
    Nearest = (int)SDL_GPUFilter.SDL_GPU_FILTER_NEAREST,

    /// <summary>Linear filtering.</summary>
    Linear = (int)SDL_GPUFilter.SDL_GPU_FILTER_LINEAR,
}

/// <summary>
/// Mipmap filter mode.
/// </summary>
public enum GpuSamplerMipmapMode
{
    /// <summary>Point filtering.</summary>
    Nearest = (int)SDL_GPUSamplerMipmapMode.SDL_GPU_SAMPLERMIPMAPMODE_NEAREST,

    /// <summary>Linear filtering.</summary>
    Linear = (int)SDL_GPUSamplerMipmapMode.SDL_GPU_SAMPLERMIPMAPMODE_LINEAR,
}

/// <summary>
/// Sampler address (wrap) mode.
/// </summary>
public enum GpuSamplerAddressMode
{
    /// <summary>Specifies that the coordinates will wrap around.</summary>
    Repeat = (int)SDL_GPUSamplerAddressMode.SDL_GPU_SAMPLERADDRESSMODE_REPEAT,

    /// <summary>Specifies that the coordinates will wrap around mirrored.</summary>
    MirroredRepeat = (int)SDL_GPUSamplerAddressMode.SDL_GPU_SAMPLERADDRESSMODE_MIRRORED_REPEAT,

    /// <summary>Specifies that the coordinates will clamp to the 0-1 range.</summary>
    ClampToEdge = (int)SDL_GPUSamplerAddressMode.SDL_GPU_SAMPLERADDRESSMODE_CLAMP_TO_EDGE,
}

/// <summary>
/// Color component write mask flags.
/// </summary>
[Flags]
public enum GpuColorComponentFlags : byte
{
    /// <summary>Red channel.</summary>
    R = (byte)SDL_GPUColorComponentFlags.SDL_GPU_COLORCOMPONENT_R,

    /// <summary>Green channel.</summary>
    G = (byte)SDL_GPUColorComponentFlags.SDL_GPU_COLORCOMPONENT_G,

    /// <summary>Blue channel.</summary>
    B = (byte)SDL_GPUColorComponentFlags.SDL_GPU_COLORCOMPONENT_B,

    /// <summary>Alpha channel.</summary>
    A = (byte)SDL_GPUColorComponentFlags.SDL_GPU_COLORCOMPONENT_A,
}
