using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Pixels;
using static Sdl3Sharp.Native.Properties;
using static Sdl3Sharp.Native.Rect;
using static Sdl3Sharp.Native.Surface;
using static Sdl3Sharp.Native.Video;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_gpu.h - GPU rendering API for 3D graphics and compute.
/// </summary>
public static unsafe partial class Gpu
{
    // Opaque handle types

    /// <summary>
    /// An opaque handle representing the SDL_GPU context.
    /// </summary>
    public struct SDL_GPUDevice { }

    /// <summary>
    /// An opaque handle representing a buffer.
    /// </summary>
    public struct SDL_GPUBuffer { }

    /// <summary>
    /// An opaque handle representing a transfer buffer.
    /// </summary>
    public struct SDL_GPUTransferBuffer { }

    /// <summary>
    /// An opaque handle representing a texture.
    /// </summary>
    public struct SDL_GPUTexture { }

    /// <summary>
    /// An opaque handle representing a sampler.
    /// </summary>
    public struct SDL_GPUSampler { }

    /// <summary>
    /// An opaque handle representing a compiled shader object.
    /// </summary>
    public struct SDL_GPUShader { }

    /// <summary>
    /// An opaque handle representing a compute pipeline.
    /// </summary>
    public struct SDL_GPUComputePipeline { }

    /// <summary>
    /// An opaque handle representing a graphics pipeline.
    /// </summary>
    public struct SDL_GPUGraphicsPipeline { }

    /// <summary>
    /// An opaque handle representing a command buffer.
    /// </summary>
    public struct SDL_GPUCommandBuffer { }

    /// <summary>
    /// An opaque handle representing a render pass.
    /// </summary>
    public struct SDL_GPURenderPass { }

    /// <summary>
    /// An opaque handle representing a compute pass.
    /// </summary>
    public struct SDL_GPUComputePass { }

    /// <summary>
    /// An opaque handle representing a copy pass.
    /// </summary>
    public struct SDL_GPUCopyPass { }

    /// <summary>
    /// An opaque handle representing a fence.
    /// </summary>
    public struct SDL_GPUFence { }

    // Enums

    /// <summary>
    /// Specifies the primitive topology of a graphics pipeline.
    /// </summary>
    public enum SDL_GPUPrimitiveType
    {
        /// <summary>A series of separate triangles.</summary>
        SDL_GPU_PRIMITIVETYPE_TRIANGLELIST,

        /// <summary>A series of connected triangles.</summary>
        SDL_GPU_PRIMITIVETYPE_TRIANGLESTRIP,

        /// <summary>A series of separate lines.</summary>
        SDL_GPU_PRIMITIVETYPE_LINELIST,

        /// <summary>A series of connected lines.</summary>
        SDL_GPU_PRIMITIVETYPE_LINESTRIP,

        /// <summary>A series of separate points.</summary>
        SDL_GPU_PRIMITIVETYPE_POINTLIST
    }

    /// <summary>
    /// Specifies how the contents of a texture attached to a render pass are treated at the beginning of the render pass.
    /// </summary>
    public enum SDL_GPULoadOp
    {
        /// <summary>The previous contents of the texture will be preserved.</summary>
        SDL_GPU_LOADOP_LOAD,

        /// <summary>The contents of the texture will be cleared to a color.</summary>
        SDL_GPU_LOADOP_CLEAR,

        /// <summary>The previous contents of the texture need not be preserved. The contents will be undefined.</summary>
        SDL_GPU_LOADOP_DONT_CARE
    }

    /// <summary>
    /// Specifies how the contents of a texture attached to a render pass are treated at the end of the render pass.
    /// </summary>
    public enum SDL_GPUStoreOp
    {
        /// <summary>The contents generated during the render pass will be written to memory.</summary>
        SDL_GPU_STOREOP_STORE,

        /// <summary>The contents generated during the render pass are not needed and may be discarded.</summary>
        SDL_GPU_STOREOP_DONT_CARE,

        /// <summary>The multisample contents generated during the render pass will be resolved to a non-multisample texture.</summary>
        SDL_GPU_STOREOP_RESOLVE,

        /// <summary>The multisample contents generated during the render pass will be resolved to a non-multisample texture. The contents in the multisample texture will be written to memory.</summary>
        SDL_GPU_STOREOP_RESOLVE_AND_STORE
    }

    /// <summary>
    /// Specifies the size of elements in an index buffer.
    /// </summary>
    public enum SDL_GPUIndexElementSize
    {
        /// <summary>The index elements are 16-bit.</summary>
        SDL_GPU_INDEXELEMENTSIZE_16BIT,

        /// <summary>The index elements are 32-bit.</summary>
        SDL_GPU_INDEXELEMENTSIZE_32BIT
    }

    /// <summary>
    /// Specifies the pixel format of a texture.
    /// </summary>
    public enum SDL_GPUTextureFormat
    {
        /// <summary>Invalid texture format.</summary>
        SDL_GPU_TEXTUREFORMAT_INVALID,

        // Unsigned Normalized Float Color Formats
        /// <summary>A8 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_A8_UNORM,
        /// <summary>R8 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8_UNORM,
        /// <summary>R8G8 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8G8_UNORM,
        /// <summary>R8G8B8A8 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UNORM,
        /// <summary>R16 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16_UNORM,
        /// <summary>R16G16 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16G16_UNORM,
        /// <summary>R16G16B16A16 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16G16B16A16_UNORM,
        /// <summary>R10G10B10A2 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R10G10B10A2_UNORM,
        /// <summary>B5G6R5 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_B5G6R5_UNORM,
        /// <summary>B5G5R5A1 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_B5G5R5A1_UNORM,
        /// <summary>B4G4R4A4 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_B4G4R4A4_UNORM,
        /// <summary>B8G8R8A8 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_B8G8R8A8_UNORM,

        // Compressed Unsigned Normalized Float Color Formats
        /// <summary>BC1 RGBA unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_BC1_RGBA_UNORM,
        /// <summary>BC2 RGBA unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_BC2_RGBA_UNORM,
        /// <summary>BC3 RGBA unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_BC3_RGBA_UNORM,
        /// <summary>BC4 R unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_BC4_R_UNORM,
        /// <summary>BC5 RG unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_BC5_RG_UNORM,
        /// <summary>BC7 RGBA unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_BC7_RGBA_UNORM,

        // Compressed Signed Float Color Formats
        /// <summary>BC6H RGB signed float format.</summary>
        SDL_GPU_TEXTUREFORMAT_BC6H_RGB_FLOAT,

        // Compressed Unsigned Float Color Formats
        /// <summary>BC6H RGB unsigned float format.</summary>
        SDL_GPU_TEXTUREFORMAT_BC6H_RGB_UFLOAT,

        // Signed Normalized Float Color Formats
        /// <summary>R8 signed normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8_SNORM,
        /// <summary>R8G8 signed normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8G8_SNORM,
        /// <summary>R8G8B8A8 signed normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8G8B8A8_SNORM,
        /// <summary>R16 signed normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16_SNORM,
        /// <summary>R16G16 signed normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16G16_SNORM,
        /// <summary>R16G16B16A16 signed normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16G16B16A16_SNORM,

        // Signed Float Color Formats
        /// <summary>R16 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16_FLOAT,
        /// <summary>R16G16 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16G16_FLOAT,
        /// <summary>R16G16B16A16 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16G16B16A16_FLOAT,
        /// <summary>R32 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_R32_FLOAT,
        /// <summary>R32G32 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_R32G32_FLOAT,
        /// <summary>R32G32B32A32 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_R32G32B32A32_FLOAT,

        // Unsigned Float Color Formats
        /// <summary>R11G11B10 unsigned float format.</summary>
        SDL_GPU_TEXTUREFORMAT_R11G11B10_UFLOAT,

        // Unsigned Integer Color Formats
        /// <summary>R8 unsigned integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8_UINT,
        /// <summary>R8G8 unsigned integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8G8_UINT,
        /// <summary>R8G8B8A8 unsigned integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UINT,
        /// <summary>R16 unsigned integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16_UINT,
        /// <summary>R16G16 unsigned integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16G16_UINT,
        /// <summary>R16G16B16A16 unsigned integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16G16B16A16_UINT,
        /// <summary>R32 unsigned integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R32_UINT,
        /// <summary>R32G32 unsigned integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R32G32_UINT,
        /// <summary>R32G32B32A32 unsigned integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R32G32B32A32_UINT,

        // Signed Integer Color Formats
        /// <summary>R8 signed integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8_INT,
        /// <summary>R8G8 signed integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8G8_INT,
        /// <summary>R8G8B8A8 signed integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8G8B8A8_INT,
        /// <summary>R16 signed integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16_INT,
        /// <summary>R16G16 signed integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16G16_INT,
        /// <summary>R16G16B16A16 signed integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R16G16B16A16_INT,
        /// <summary>R32 signed integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R32_INT,
        /// <summary>R32G32 signed integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R32G32_INT,
        /// <summary>R32G32B32A32 signed integer format.</summary>
        SDL_GPU_TEXTUREFORMAT_R32G32B32A32_INT,

        // SRGB Unsigned Normalized Color Formats
        /// <summary>R8G8B8A8 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UNORM_SRGB,
        /// <summary>B8G8R8A8 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_B8G8R8A8_UNORM_SRGB,

        // Compressed SRGB Unsigned Normalized Color Formats
        /// <summary>BC1 RGBA unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_BC1_RGBA_UNORM_SRGB,
        /// <summary>BC2 RGBA unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_BC2_RGBA_UNORM_SRGB,
        /// <summary>BC3 RGBA unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_BC3_RGBA_UNORM_SRGB,
        /// <summary>BC7 RGBA unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_BC7_RGBA_UNORM_SRGB,

        // Depth Formats
        /// <summary>D16 unsigned normalized depth format.</summary>
        SDL_GPU_TEXTUREFORMAT_D16_UNORM,
        /// <summary>D24 unsigned normalized depth format.</summary>
        SDL_GPU_TEXTUREFORMAT_D24_UNORM,
        /// <summary>D32 float depth format.</summary>
        SDL_GPU_TEXTUREFORMAT_D32_FLOAT,
        /// <summary>D24 unsigned normalized depth with S8 unsigned integer stencil format.</summary>
        SDL_GPU_TEXTUREFORMAT_D24_UNORM_S8_UINT,
        /// <summary>D32 float depth with S8 unsigned integer stencil format.</summary>
        SDL_GPU_TEXTUREFORMAT_D32_FLOAT_S8_UINT,

        // Compressed ASTC Normalized Float Color Formats
        /// <summary>ASTC 4x4 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_4x4_UNORM,
        /// <summary>ASTC 5x4 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_5x4_UNORM,
        /// <summary>ASTC 5x5 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_5x5_UNORM,
        /// <summary>ASTC 6x5 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_6x5_UNORM,
        /// <summary>ASTC 6x6 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_6x6_UNORM,
        /// <summary>ASTC 8x5 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_8x5_UNORM,
        /// <summary>ASTC 8x6 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_8x6_UNORM,
        /// <summary>ASTC 8x8 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_8x8_UNORM,
        /// <summary>ASTC 10x5 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_10x5_UNORM,
        /// <summary>ASTC 10x6 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_10x6_UNORM,
        /// <summary>ASTC 10x8 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_10x8_UNORM,
        /// <summary>ASTC 10x10 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_10x10_UNORM,
        /// <summary>ASTC 12x10 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_12x10_UNORM,
        /// <summary>ASTC 12x12 unsigned normalized format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_12x12_UNORM,

        // Compressed SRGB ASTC Normalized Float Color Formats
        /// <summary>ASTC 4x4 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_4x4_UNORM_SRGB,
        /// <summary>ASTC 5x4 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_5x4_UNORM_SRGB,
        /// <summary>ASTC 5x5 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_5x5_UNORM_SRGB,
        /// <summary>ASTC 6x5 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_6x5_UNORM_SRGB,
        /// <summary>ASTC 6x6 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_6x6_UNORM_SRGB,
        /// <summary>ASTC 8x5 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_8x5_UNORM_SRGB,
        /// <summary>ASTC 8x6 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_8x6_UNORM_SRGB,
        /// <summary>ASTC 8x8 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_8x8_UNORM_SRGB,
        /// <summary>ASTC 10x5 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_10x5_UNORM_SRGB,
        /// <summary>ASTC 10x6 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_10x6_UNORM_SRGB,
        /// <summary>ASTC 10x8 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_10x8_UNORM_SRGB,
        /// <summary>ASTC 10x10 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_10x10_UNORM_SRGB,
        /// <summary>ASTC 12x10 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_12x10_UNORM_SRGB,
        /// <summary>ASTC 12x12 unsigned normalized sRGB format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_12x12_UNORM_SRGB,

        // Compressed ASTC Signed Float Color Formats
        /// <summary>ASTC 4x4 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_4x4_FLOAT,
        /// <summary>ASTC 5x4 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_5x4_FLOAT,
        /// <summary>ASTC 5x5 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_5x5_FLOAT,
        /// <summary>ASTC 6x5 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_6x5_FLOAT,
        /// <summary>ASTC 6x6 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_6x6_FLOAT,
        /// <summary>ASTC 8x5 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_8x5_FLOAT,
        /// <summary>ASTC 8x6 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_8x6_FLOAT,
        /// <summary>ASTC 8x8 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_8x8_FLOAT,
        /// <summary>ASTC 10x5 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_10x5_FLOAT,
        /// <summary>ASTC 10x6 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_10x6_FLOAT,
        /// <summary>ASTC 10x8 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_10x8_FLOAT,
        /// <summary>ASTC 10x10 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_10x10_FLOAT,
        /// <summary>ASTC 12x10 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_12x10_FLOAT,
        /// <summary>ASTC 12x12 float format.</summary>
        SDL_GPU_TEXTUREFORMAT_ASTC_12x12_FLOAT
    }

    /// <summary>
    /// Specifies how a texture is intended to be used by the client.
    /// </summary>
    [Flags]
    public enum SDL_GPUTextureUsageFlags : uint
    {
        /// <summary>Texture supports sampling.</summary>
        SDL_GPU_TEXTUREUSAGE_SAMPLER = 1u << 0,

        /// <summary>Texture is a color render target.</summary>
        SDL_GPU_TEXTUREUSAGE_COLOR_TARGET = 1u << 1,

        /// <summary>Texture is a depth stencil target.</summary>
        SDL_GPU_TEXTUREUSAGE_DEPTH_STENCIL_TARGET = 1u << 2,

        /// <summary>Texture supports storage reads in graphics stages.</summary>
        SDL_GPU_TEXTUREUSAGE_GRAPHICS_STORAGE_READ = 1u << 3,

        /// <summary>Texture supports storage reads in the compute stage.</summary>
        SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_READ = 1u << 4,

        /// <summary>Texture supports storage writes in the compute stage.</summary>
        SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_WRITE = 1u << 5,

        /// <summary>Texture supports reads and writes in the same compute shader.</summary>
        SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_SIMULTANEOUS_READ_WRITE = 1u << 6
    }

    /// <summary>
    /// Specifies the type of a texture.
    /// </summary>
    public enum SDL_GPUTextureType
    {
        /// <summary>The texture is a 2-dimensional image.</summary>
        SDL_GPU_TEXTURETYPE_2D,

        /// <summary>The texture is a 2-dimensional array image.</summary>
        SDL_GPU_TEXTURETYPE_2D_ARRAY,

        /// <summary>The texture is a 3-dimensional image.</summary>
        SDL_GPU_TEXTURETYPE_3D,

        /// <summary>The texture is a cube image.</summary>
        SDL_GPU_TEXTURETYPE_CUBE,

        /// <summary>The texture is a cube array image.</summary>
        SDL_GPU_TEXTURETYPE_CUBE_ARRAY
    }

    /// <summary>
    /// Specifies the sample count of a texture.
    /// </summary>
    public enum SDL_GPUSampleCount
    {
        /// <summary>No multisampling.</summary>
        SDL_GPU_SAMPLECOUNT_1,

        /// <summary>MSAA 2x.</summary>
        SDL_GPU_SAMPLECOUNT_2,

        /// <summary>MSAA 4x.</summary>
        SDL_GPU_SAMPLECOUNT_4,

        /// <summary>MSAA 8x.</summary>
        SDL_GPU_SAMPLECOUNT_8
    }

    /// <summary>
    /// Specifies the face of a cube map.
    /// </summary>
    public enum SDL_GPUCubeMapFace
    {
        /// <summary>Positive X face.</summary>
        SDL_GPU_CUBEMAPFACE_POSITIVEX,

        /// <summary>Negative X face.</summary>
        SDL_GPU_CUBEMAPFACE_NEGATIVEX,

        /// <summary>Positive Y face.</summary>
        SDL_GPU_CUBEMAPFACE_POSITIVEY,

        /// <summary>Negative Y face.</summary>
        SDL_GPU_CUBEMAPFACE_NEGATIVEY,

        /// <summary>Positive Z face.</summary>
        SDL_GPU_CUBEMAPFACE_POSITIVEZ,

        /// <summary>Negative Z face.</summary>
        SDL_GPU_CUBEMAPFACE_NEGATIVEZ
    }

    /// <summary>
    /// Specifies how a buffer is intended to be used by the client.
    /// </summary>
    [Flags]
    public enum SDL_GPUBufferUsageFlags : uint
    {
        /// <summary>Buffer is a vertex buffer.</summary>
        SDL_GPU_BUFFERUSAGE_VERTEX = 1u << 0,

        /// <summary>Buffer is an index buffer.</summary>
        SDL_GPU_BUFFERUSAGE_INDEX = 1u << 1,

        /// <summary>Buffer is an indirect buffer.</summary>
        SDL_GPU_BUFFERUSAGE_INDIRECT = 1u << 2,

        /// <summary>Buffer supports storage reads in graphics stages.</summary>
        SDL_GPU_BUFFERUSAGE_GRAPHICS_STORAGE_READ = 1u << 3,

        /// <summary>Buffer supports storage reads in the compute stage.</summary>
        SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_READ = 1u << 4,

        /// <summary>Buffer supports storage writes in the compute stage.</summary>
        SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_WRITE = 1u << 5
    }

    /// <summary>
    /// Specifies how a transfer buffer is intended to be used by the client.
    /// </summary>
    public enum SDL_GPUTransferBufferUsage
    {
        /// <summary>Transfer buffer is used for uploading data to the GPU.</summary>
        SDL_GPU_TRANSFERBUFFERUSAGE_UPLOAD,

        /// <summary>Transfer buffer is used for downloading data from the GPU.</summary>
        SDL_GPU_TRANSFERBUFFERUSAGE_DOWNLOAD
    }

    /// <summary>
    /// Specifies which stage a shader program corresponds to.
    /// </summary>
    public enum SDL_GPUShaderStage
    {
        /// <summary>Vertex shader stage.</summary>
        SDL_GPU_SHADERSTAGE_VERTEX,

        /// <summary>Fragment shader stage.</summary>
        SDL_GPU_SHADERSTAGE_FRAGMENT
    }

    /// <summary>
    /// Specifies the format of shader code.
    /// </summary>
    [Flags]
    public enum SDL_GPUShaderFormat : uint
    {
        /// <summary>Invalid shader format.</summary>
        SDL_GPU_SHADERFORMAT_INVALID = 0,

        /// <summary>Shaders for NDA'd platforms.</summary>
        SDL_GPU_SHADERFORMAT_PRIVATE = 1u << 0,

        /// <summary>SPIR-V shaders for Vulkan.</summary>
        SDL_GPU_SHADERFORMAT_SPIRV = 1u << 1,

        /// <summary>DXBC SM5_1 shaders for D3D12.</summary>
        SDL_GPU_SHADERFORMAT_DXBC = 1u << 2,

        /// <summary>DXIL SM6_0 shaders for D3D12.</summary>
        SDL_GPU_SHADERFORMAT_DXIL = 1u << 3,

        /// <summary>MSL shaders for Metal.</summary>
        SDL_GPU_SHADERFORMAT_MSL = 1u << 4,

        /// <summary>Precompiled metallib shaders for Metal.</summary>
        SDL_GPU_SHADERFORMAT_METALLIB = 1u << 5
    }

    /// <summary>
    /// Specifies the format of a vertex attribute.
    /// </summary>
    public enum SDL_GPUVertexElementFormat
    {
        /// <summary>Invalid format.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_INVALID,

        // 32-bit Signed Integers
        /// <summary>Single 32-bit signed integer.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_INT,
        /// <summary>Two 32-bit signed integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_INT2,
        /// <summary>Three 32-bit signed integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_INT3,
        /// <summary>Four 32-bit signed integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_INT4,

        // 32-bit Unsigned Integers
        /// <summary>Single 32-bit unsigned integer.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_UINT,
        /// <summary>Two 32-bit unsigned integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_UINT2,
        /// <summary>Three 32-bit unsigned integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_UINT3,
        /// <summary>Four 32-bit unsigned integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_UINT4,

        // 32-bit Floats
        /// <summary>Single 32-bit float.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_FLOAT,
        /// <summary>Two 32-bit floats.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_FLOAT2,
        /// <summary>Three 32-bit floats.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_FLOAT3,
        /// <summary>Four 32-bit floats.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_FLOAT4,

        // 8-bit Signed Integers
        /// <summary>Two 8-bit signed integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_BYTE2,
        /// <summary>Four 8-bit signed integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_BYTE4,

        // 8-bit Unsigned Integers
        /// <summary>Two 8-bit unsigned integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2,
        /// <summary>Four 8-bit unsigned integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4,

        // 8-bit Signed Normalized
        /// <summary>Two 8-bit signed normalized values.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_BYTE2_NORM,
        /// <summary>Four 8-bit signed normalized values.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_BYTE4_NORM,

        // 8-bit Unsigned Normalized
        /// <summary>Two 8-bit unsigned normalized values.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2_NORM,
        /// <summary>Four 8-bit unsigned normalized values.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4_NORM,

        // 16-bit Signed Integers
        /// <summary>Two 16-bit signed integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_SHORT2,
        /// <summary>Four 16-bit signed integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_SHORT4,

        // 16-bit Unsigned Integers
        /// <summary>Two 16-bit unsigned integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_USHORT2,
        /// <summary>Four 16-bit unsigned integers.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_USHORT4,

        // 16-bit Signed Normalized
        /// <summary>Two 16-bit signed normalized values.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_SHORT2_NORM,
        /// <summary>Four 16-bit signed normalized values.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_SHORT4_NORM,

        // 16-bit Unsigned Normalized
        /// <summary>Two 16-bit unsigned normalized values.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_USHORT2_NORM,
        /// <summary>Four 16-bit unsigned normalized values.</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_USHORT4_NORM,

        // 16-bit Floats
        /// <summary>Two 16-bit floats (half precision).</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_HALF2,
        /// <summary>Four 16-bit floats (half precision).</summary>
        SDL_GPU_VERTEXELEMENTFORMAT_HALF4
    }

    /// <summary>
    /// Specifies the rate at which vertex attributes are pulled from buffers.
    /// </summary>
    public enum SDL_GPUVertexInputRate
    {
        /// <summary>Attribute addressing is a function of the vertex index.</summary>
        SDL_GPU_VERTEXINPUTRATE_VERTEX,

        /// <summary>Attribute addressing is a function of the instance index.</summary>
        SDL_GPU_VERTEXINPUTRATE_INSTANCE
    }

    /// <summary>
    /// Specifies the fill mode of the graphics pipeline.
    /// </summary>
    public enum SDL_GPUFillMode
    {
        /// <summary>Polygons will be rendered via rasterization.</summary>
        SDL_GPU_FILLMODE_FILL,

        /// <summary>Polygon edges will be drawn as line segments.</summary>
        SDL_GPU_FILLMODE_LINE
    }

    /// <summary>
    /// Specifies the facing direction in which triangle faces will be culled.
    /// </summary>
    public enum SDL_GPUCullMode
    {
        /// <summary>No triangles are culled.</summary>
        SDL_GPU_CULLMODE_NONE,

        /// <summary>Front-facing triangles are culled.</summary>
        SDL_GPU_CULLMODE_FRONT,

        /// <summary>Back-facing triangles are culled.</summary>
        SDL_GPU_CULLMODE_BACK
    }

    /// <summary>
    /// Specifies the vertex winding that will cause a triangle to be determined to be front-facing.
    /// </summary>
    public enum SDL_GPUFrontFace
    {
        /// <summary>A triangle with counter-clockwise vertex winding will be considered front-facing.</summary>
        SDL_GPU_FRONTFACE_COUNTER_CLOCKWISE,

        /// <summary>A triangle with clockwise vertex winding will be considered front-facing.</summary>
        SDL_GPU_FRONTFACE_CLOCKWISE
    }

    /// <summary>
    /// Specifies a comparison operator for depth, stencil and sampler operations.
    /// </summary>
    public enum SDL_GPUCompareOp
    {
        /// <summary>Invalid compare operation.</summary>
        SDL_GPU_COMPAREOP_INVALID,

        /// <summary>The comparison always evaluates false.</summary>
        SDL_GPU_COMPAREOP_NEVER,

        /// <summary>The comparison evaluates reference less than test.</summary>
        SDL_GPU_COMPAREOP_LESS,

        /// <summary>The comparison evaluates reference equal to test.</summary>
        SDL_GPU_COMPAREOP_EQUAL,

        /// <summary>The comparison evaluates reference less than or equal to test.</summary>
        SDL_GPU_COMPAREOP_LESS_OR_EQUAL,

        /// <summary>The comparison evaluates reference greater than test.</summary>
        SDL_GPU_COMPAREOP_GREATER,

        /// <summary>The comparison evaluates reference not equal to test.</summary>
        SDL_GPU_COMPAREOP_NOT_EQUAL,

        /// <summary>The comparison evaluates reference greater than or equal to test.</summary>
        SDL_GPU_COMPAREOP_GREATER_OR_EQUAL,

        /// <summary>The comparison always evaluates true.</summary>
        SDL_GPU_COMPAREOP_ALWAYS
    }

    /// <summary>
    /// Specifies what happens to a stored stencil value if stencil tests fail or pass.
    /// </summary>
    public enum SDL_GPUStencilOp
    {
        /// <summary>Invalid stencil operation.</summary>
        SDL_GPU_STENCILOP_INVALID,

        /// <summary>Keeps the current value.</summary>
        SDL_GPU_STENCILOP_KEEP,

        /// <summary>Sets the value to 0.</summary>
        SDL_GPU_STENCILOP_ZERO,

        /// <summary>Sets the value to reference.</summary>
        SDL_GPU_STENCILOP_REPLACE,

        /// <summary>Increments the current value and clamps to the maximum value.</summary>
        SDL_GPU_STENCILOP_INCREMENT_AND_CLAMP,

        /// <summary>Decrements the current value and clamps to 0.</summary>
        SDL_GPU_STENCILOP_DECREMENT_AND_CLAMP,

        /// <summary>Bitwise-inverts the current value.</summary>
        SDL_GPU_STENCILOP_INVERT,

        /// <summary>Increments the current value and wraps back to 0.</summary>
        SDL_GPU_STENCILOP_INCREMENT_AND_WRAP,

        /// <summary>Decrements the current value and wraps to the maximum value.</summary>
        SDL_GPU_STENCILOP_DECREMENT_AND_WRAP
    }

    /// <summary>
    /// Specifies the operator to be used when pixels in a render target are blended with existing pixels in the texture.
    /// </summary>
    public enum SDL_GPUBlendOp
    {
        /// <summary>Invalid blend operation.</summary>
        SDL_GPU_BLENDOP_INVALID,

        /// <summary>(source * source_factor) + (destination * destination_factor).</summary>
        SDL_GPU_BLENDOP_ADD,

        /// <summary>(source * source_factor) - (destination * destination_factor).</summary>
        SDL_GPU_BLENDOP_SUBTRACT,

        /// <summary>(destination * destination_factor) - (source * source_factor).</summary>
        SDL_GPU_BLENDOP_REVERSE_SUBTRACT,

        /// <summary>min(source, destination).</summary>
        SDL_GPU_BLENDOP_MIN,

        /// <summary>max(source, destination).</summary>
        SDL_GPU_BLENDOP_MAX
    }

    /// <summary>
    /// Specifies a blending factor to be used when pixels in a render target are blended with existing pixels in the texture.
    /// </summary>
    public enum SDL_GPUBlendFactor
    {
        /// <summary>Invalid blend factor.</summary>
        SDL_GPU_BLENDFACTOR_INVALID,

        /// <summary>Factor is 0.</summary>
        SDL_GPU_BLENDFACTOR_ZERO,

        /// <summary>Factor is 1.</summary>
        SDL_GPU_BLENDFACTOR_ONE,

        /// <summary>Factor is source color.</summary>
        SDL_GPU_BLENDFACTOR_SRC_COLOR,

        /// <summary>Factor is 1 - source color.</summary>
        SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_COLOR,

        /// <summary>Factor is destination color.</summary>
        SDL_GPU_BLENDFACTOR_DST_COLOR,

        /// <summary>Factor is 1 - destination color.</summary>
        SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_COLOR,

        /// <summary>Factor is source alpha.</summary>
        SDL_GPU_BLENDFACTOR_SRC_ALPHA,

        /// <summary>Factor is 1 - source alpha.</summary>
        SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_ALPHA,

        /// <summary>Factor is destination alpha.</summary>
        SDL_GPU_BLENDFACTOR_DST_ALPHA,

        /// <summary>Factor is 1 - destination alpha.</summary>
        SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_ALPHA,

        /// <summary>Factor is blend constant.</summary>
        SDL_GPU_BLENDFACTOR_CONSTANT_COLOR,

        /// <summary>Factor is 1 - blend constant.</summary>
        SDL_GPU_BLENDFACTOR_ONE_MINUS_CONSTANT_COLOR,

        /// <summary>Factor is min(source alpha, 1 - destination alpha).</summary>
        SDL_GPU_BLENDFACTOR_SRC_ALPHA_SATURATE
    }

    /// <summary>
    /// Specifies which color components are written in a graphics pipeline.
    /// </summary>
    [Flags]
    public enum SDL_GPUColorComponentFlags : byte
    {
        /// <summary>The red component.</summary>
        SDL_GPU_COLORCOMPONENT_R = 1 << 0,

        /// <summary>The green component.</summary>
        SDL_GPU_COLORCOMPONENT_G = 1 << 1,

        /// <summary>The blue component.</summary>
        SDL_GPU_COLORCOMPONENT_B = 1 << 2,

        /// <summary>The alpha component.</summary>
        SDL_GPU_COLORCOMPONENT_A = 1 << 3
    }

    /// <summary>
    /// Specifies a filter operation used by a sampler.
    /// </summary>
    public enum SDL_GPUFilter
    {
        /// <summary>Point filtering.</summary>
        SDL_GPU_FILTER_NEAREST,

        /// <summary>Linear filtering.</summary>
        SDL_GPU_FILTER_LINEAR
    }

    /// <summary>
    /// Specifies a mipmap mode used by a sampler.
    /// </summary>
    public enum SDL_GPUSamplerMipmapMode
    {
        /// <summary>Point filtering.</summary>
        SDL_GPU_SAMPLERMIPMAPMODE_NEAREST,

        /// <summary>Linear filtering.</summary>
        SDL_GPU_SAMPLERMIPMAPMODE_LINEAR
    }

    /// <summary>
    /// Specifies behavior of texture sampling when the coordinates exceed the 0-1 range.
    /// </summary>
    public enum SDL_GPUSamplerAddressMode
    {
        /// <summary>Specifies that the coordinates will wrap around.</summary>
        SDL_GPU_SAMPLERADDRESSMODE_REPEAT,

        /// <summary>Specifies that the coordinates will wrap around mirrored.</summary>
        SDL_GPU_SAMPLERADDRESSMODE_MIRRORED_REPEAT,

        /// <summary>Specifies that the coordinates will clamp to the 0-1 range.</summary>
        SDL_GPU_SAMPLERADDRESSMODE_CLAMP_TO_EDGE
    }

    /// <summary>
    /// Specifies the timing that will be used to present swapchain textures to the OS.
    /// </summary>
    public enum SDL_GPUPresentMode
    {
        /// <summary>Waits for vblank before presenting. No tearing is possible.</summary>
        SDL_GPU_PRESENTMODE_VSYNC,

        /// <summary>Immediately presents. Lowest latency option, but tearing may occur.</summary>
        SDL_GPU_PRESENTMODE_IMMEDIATE,

        /// <summary>Waits for vblank before presenting. Similar to VSYNC, but with reduced visual latency.</summary>
        SDL_GPU_PRESENTMODE_MAILBOX
    }

    /// <summary>
    /// Specifies the texture format and colorspace of the swapchain textures.
    /// </summary>
    public enum SDL_GPUSwapchainComposition
    {
        /// <summary>B8G8R8A8 or R8G8B8A8 swapchain. Pixel values are in sRGB encoding.</summary>
        SDL_GPU_SWAPCHAINCOMPOSITION_SDR,

        /// <summary>B8G8R8A8_SRGB or R8G8B8A8_SRGB swapchain. Pixel values are stored in sRGB encoding but accessed in linear sRGB.</summary>
        SDL_GPU_SWAPCHAINCOMPOSITION_SDR_LINEAR,

        /// <summary>R16G16B16A16_FLOAT swapchain. Pixel values are in extended linear sRGB encoding.</summary>
        SDL_GPU_SWAPCHAINCOMPOSITION_HDR_EXTENDED_LINEAR,

        /// <summary>A2R10G10B10 or A2B10G10R10 swapchain. Pixel values are in BT.2020 ST2084 (PQ) encoding.</summary>
        SDL_GPU_SWAPCHAINCOMPOSITION_HDR10_ST2084
    }

    // Structs

    /// <summary>
    /// A structure specifying a viewport.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUViewport
    {
        /// <summary>The left offset of the viewport.</summary>
        public float x;

        /// <summary>The top offset of the viewport.</summary>
        public float y;

        /// <summary>The width of the viewport.</summary>
        public float w;

        /// <summary>The height of the viewport.</summary>
        public float h;

        /// <summary>The minimum depth of the viewport.</summary>
        public float min_depth;

        /// <summary>The maximum depth of the viewport.</summary>
        public float max_depth;
    }

    /// <summary>
    /// A structure specifying parameters related to transferring data to or from a texture.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTextureTransferInfo
    {
        /// <summary>The transfer buffer used in the transfer operation.</summary>
        public SDL_GPUTransferBuffer* transfer_buffer;

        /// <summary>The starting byte of the image data in the transfer buffer.</summary>
        public uint offset;

        /// <summary>The number of pixels from one row to the next.</summary>
        public uint pixels_per_row;

        /// <summary>The number of rows from one layer/depth-slice to the next.</summary>
        public uint rows_per_layer;
    }

    /// <summary>
    /// A structure specifying a location in a transfer buffer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTransferBufferLocation
    {
        /// <summary>The transfer buffer used in the transfer operation.</summary>
        public SDL_GPUTransferBuffer* transfer_buffer;

        /// <summary>The starting byte of the buffer data in the transfer buffer.</summary>
        public uint offset;
    }

    /// <summary>
    /// A structure specifying a location in a texture.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTextureLocation
    {
        /// <summary>The texture used in the copy operation.</summary>
        public SDL_GPUTexture* texture;

        /// <summary>The mip level index of the location.</summary>
        public uint mip_level;

        /// <summary>The layer index of the location.</summary>
        public uint layer;

        /// <summary>The left offset of the location.</summary>
        public uint x;

        /// <summary>The top offset of the location.</summary>
        public uint y;

        /// <summary>The front offset of the location.</summary>
        public uint z;
    }

    /// <summary>
    /// A structure specifying a region of a texture.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTextureRegion
    {
        /// <summary>The texture used in the copy operation.</summary>
        public SDL_GPUTexture* texture;

        /// <summary>The mip level index to transfer.</summary>
        public uint mip_level;

        /// <summary>The layer index to transfer.</summary>
        public uint layer;

        /// <summary>The left offset of the region.</summary>
        public uint x;

        /// <summary>The top offset of the region.</summary>
        public uint y;

        /// <summary>The front offset of the region.</summary>
        public uint z;

        /// <summary>The width of the region.</summary>
        public uint w;

        /// <summary>The height of the region.</summary>
        public uint h;

        /// <summary>The depth of the region.</summary>
        public uint d;
    }

    /// <summary>
    /// A structure specifying a region of a texture used in the blit operation.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUBlitRegion
    {
        /// <summary>The texture.</summary>
        public SDL_GPUTexture* texture;

        /// <summary>The mip level index of the region.</summary>
        public uint mip_level;

        /// <summary>The layer index or depth plane of the region.</summary>
        public uint layer_or_depth_plane;

        /// <summary>The left offset of the region.</summary>
        public uint x;

        /// <summary>The top offset of the region.</summary>
        public uint y;

        /// <summary>The width of the region.</summary>
        public uint w;

        /// <summary>The height of the region.</summary>
        public uint h;
    }

    /// <summary>
    /// A structure specifying a location in a buffer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUBufferLocation
    {
        /// <summary>The buffer.</summary>
        public SDL_GPUBuffer* buffer;

        /// <summary>The starting byte within the buffer.</summary>
        public uint offset;
    }

    /// <summary>
    /// A structure specifying a region of a buffer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUBufferRegion
    {
        /// <summary>The buffer.</summary>
        public SDL_GPUBuffer* buffer;

        /// <summary>The starting byte within the buffer.</summary>
        public uint offset;

        /// <summary>The size in bytes of the region.</summary>
        public uint size;
    }

    /// <summary>
    /// A structure specifying the parameters of an indirect draw command.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUIndirectDrawCommand
    {
        /// <summary>The number of vertices to draw.</summary>
        public uint num_vertices;

        /// <summary>The number of instances to draw.</summary>
        public uint num_instances;

        /// <summary>The index of the first vertex to draw.</summary>
        public uint first_vertex;

        /// <summary>The ID of the first instance to draw.</summary>
        public uint first_instance;
    }

    /// <summary>
    /// A structure specifying the parameters of an indexed indirect draw command.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUIndexedIndirectDrawCommand
    {
        /// <summary>The number of indices to draw per instance.</summary>
        public uint num_indices;

        /// <summary>The number of instances to draw.</summary>
        public uint num_instances;

        /// <summary>The base index within the index buffer.</summary>
        public uint first_index;

        /// <summary>The value added to the vertex index before indexing into the vertex buffer.</summary>
        public int vertex_offset;

        /// <summary>The ID of the first instance to draw.</summary>
        public uint first_instance;
    }

    /// <summary>
    /// A structure specifying the parameters of an indirect dispatch command.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUIndirectDispatchCommand
    {
        /// <summary>The number of local workgroups to dispatch in the X dimension.</summary>
        public uint groupcount_x;

        /// <summary>The number of local workgroups to dispatch in the Y dimension.</summary>
        public uint groupcount_y;

        /// <summary>The number of local workgroups to dispatch in the Z dimension.</summary>
        public uint groupcount_z;
    }

    /// <summary>
    /// A structure specifying the parameters of a sampler.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUSamplerCreateInfo
    {
        /// <summary>The minification filter to apply to lookups.</summary>
        public SDL_GPUFilter min_filter;

        /// <summary>The magnification filter to apply to lookups.</summary>
        public SDL_GPUFilter mag_filter;

        /// <summary>The mipmap filter to apply to lookups.</summary>
        public SDL_GPUSamplerMipmapMode mipmap_mode;

        /// <summary>The addressing mode for U coordinates outside [0, 1).</summary>
        public SDL_GPUSamplerAddressMode address_mode_u;

        /// <summary>The addressing mode for V coordinates outside [0, 1).</summary>
        public SDL_GPUSamplerAddressMode address_mode_v;

        /// <summary>The addressing mode for W coordinates outside [0, 1).</summary>
        public SDL_GPUSamplerAddressMode address_mode_w;

        /// <summary>The bias to be added to mipmap LOD calculation.</summary>
        public float mip_lod_bias;

        /// <summary>The anisotropy value clamp used by the sampler.</summary>
        public float max_anisotropy;

        /// <summary>The comparison operator to apply to fetched data before filtering.</summary>
        public SDL_GPUCompareOp compare_op;

        /// <summary>Clamps the minimum of the computed LOD value.</summary>
        public float min_lod;

        /// <summary>Clamps the maximum of the computed LOD value.</summary>
        public float max_lod;

        /// <summary>True to enable anisotropic filtering.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool enable_anisotropy;

        /// <summary>True to enable comparison against a reference value during lookups.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool enable_compare;

        private byte padding1;
        private byte padding2;

        /// <summary>A properties ID for extensions. Should be 0 if no extensions are needed.</summary>
        public SDL_PropertiesID props;
    }

    /// <summary>
    /// A structure specifying the parameters of vertex buffers used in a graphics pipeline.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUVertexBufferDescription
    {
        /// <summary>The binding slot of the vertex buffer.</summary>
        public uint slot;

        /// <summary>The size of a single element + the offset between elements.</summary>
        public uint pitch;

        /// <summary>Whether attribute addressing is a function of the vertex index or instance index.</summary>
        public SDL_GPUVertexInputRate input_rate;

        /// <summary>Reserved for future use. Must be set to 0.</summary>
        public uint instance_step_rate;
    }

    /// <summary>
    /// A structure specifying a vertex attribute.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUVertexAttribute
    {
        /// <summary>The shader input location index.</summary>
        public uint location;

        /// <summary>The binding slot of the associated vertex buffer.</summary>
        public uint buffer_slot;

        /// <summary>The size and type of the attribute data.</summary>
        public SDL_GPUVertexElementFormat format;

        /// <summary>The byte offset of this attribute relative to the start of the vertex element.</summary>
        public uint offset;
    }

    /// <summary>
    /// A structure specifying the parameters of a graphics pipeline vertex input state.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUVertexInputState
    {
        /// <summary>A pointer to an array of vertex buffer descriptions.</summary>
        public SDL_GPUVertexBufferDescription* vertex_buffer_descriptions;

        /// <summary>The number of vertex buffer descriptions in the above array.</summary>
        public uint num_vertex_buffers;

        /// <summary>A pointer to an array of vertex attribute descriptions.</summary>
        public SDL_GPUVertexAttribute* vertex_attributes;

        /// <summary>The number of vertex attribute descriptions in the above array.</summary>
        public uint num_vertex_attributes;
    }

    /// <summary>
    /// A structure specifying the stencil operation state of a graphics pipeline.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUStencilOpState
    {
        /// <summary>The action performed on samples that fail the stencil test.</summary>
        public SDL_GPUStencilOp fail_op;

        /// <summary>The action performed on samples that pass the depth and stencil tests.</summary>
        public SDL_GPUStencilOp pass_op;

        /// <summary>The action performed on samples that pass the stencil test and fail the depth test.</summary>
        public SDL_GPUStencilOp depth_fail_op;

        /// <summary>The comparison operator used in the stencil test.</summary>
        public SDL_GPUCompareOp compare_op;
    }

    /// <summary>
    /// A structure specifying the blend state of a color target.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUColorTargetBlendState
    {
        /// <summary>The value to be multiplied by the source RGB value.</summary>
        public SDL_GPUBlendFactor src_color_blendfactor;

        /// <summary>The value to be multiplied by the destination RGB value.</summary>
        public SDL_GPUBlendFactor dst_color_blendfactor;

        /// <summary>The blend operation for the RGB components.</summary>
        public SDL_GPUBlendOp color_blend_op;

        /// <summary>The value to be multiplied by the source alpha.</summary>
        public SDL_GPUBlendFactor src_alpha_blendfactor;

        /// <summary>The value to be multiplied by the destination alpha.</summary>
        public SDL_GPUBlendFactor dst_alpha_blendfactor;

        /// <summary>The blend operation for the alpha component.</summary>
        public SDL_GPUBlendOp alpha_blend_op;

        /// <summary>A bitmask specifying which of the RGBA components are enabled for writing.</summary>
        public SDL_GPUColorComponentFlags color_write_mask;

        /// <summary>Whether blending is enabled for the color target.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool enable_blend;

        /// <summary>Whether the color write mask is enabled.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool enable_color_write_mask;

        private byte padding1;
        private byte padding2;
    }

    /// <summary>
    /// A structure specifying code and metadata for creating a shader object.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUShaderCreateInfo
    {
        /// <summary>The size in bytes of the code pointed to.</summary>
        public nuint code_size;

        /// <summary>A pointer to shader code.</summary>
        public byte* code;

        /// <summary>A pointer to a null-terminated UTF-8 string specifying the entry point function name for the shader.</summary>
        public byte* entrypoint;

        /// <summary>The format of the shader code.</summary>
        public SDL_GPUShaderFormat format;

        /// <summary>The stage the shader program corresponds to.</summary>
        public SDL_GPUShaderStage stage;

        /// <summary>The number of samplers defined in the shader.</summary>
        public uint num_samplers;

        /// <summary>The number of storage textures defined in the shader.</summary>
        public uint num_storage_textures;

        /// <summary>The number of storage buffers defined in the shader.</summary>
        public uint num_storage_buffers;

        /// <summary>The number of uniform buffers defined in the shader.</summary>
        public uint num_uniform_buffers;

        /// <summary>A properties ID for extensions. Should be 0 if no extensions are needed.</summary>
        public SDL_PropertiesID props;
    }

    /// <summary>
    /// A structure specifying the parameters of a texture.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTextureCreateInfo
    {
        /// <summary>The base dimensionality of the texture.</summary>
        public SDL_GPUTextureType type;

        /// <summary>The pixel format of the texture.</summary>
        public SDL_GPUTextureFormat format;

        /// <summary>How the texture is intended to be used by the client.</summary>
        public SDL_GPUTextureUsageFlags usage;

        /// <summary>The width of the texture.</summary>
        public uint width;

        /// <summary>The height of the texture.</summary>
        public uint height;

        /// <summary>The layer count or depth of the texture.</summary>
        public uint layer_count_or_depth;

        /// <summary>The number of mip levels in the texture.</summary>
        public uint num_levels;

        /// <summary>The number of samples per texel.</summary>
        public SDL_GPUSampleCount sample_count;

        /// <summary>A properties ID for extensions. Should be 0 if no extensions are needed.</summary>
        public SDL_PropertiesID props;
    }

    /// <summary>
    /// A structure specifying the parameters of a buffer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUBufferCreateInfo
    {
        /// <summary>How the buffer is intended to be used by the client.</summary>
        public SDL_GPUBufferUsageFlags usage;

        /// <summary>The size in bytes of the buffer.</summary>
        public uint size;

        /// <summary>A properties ID for extensions. Should be 0 if no extensions are needed.</summary>
        public SDL_PropertiesID props;
    }

    /// <summary>
    /// A structure specifying the parameters of a transfer buffer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTransferBufferCreateInfo
    {
        /// <summary>How the transfer buffer is intended to be used by the client.</summary>
        public SDL_GPUTransferBufferUsage usage;

        /// <summary>The size in bytes of the transfer buffer.</summary>
        public uint size;

        /// <summary>A properties ID for extensions. Should be 0 if no extensions are needed.</summary>
        public SDL_PropertiesID props;
    }

    /// <summary>
    /// A structure specifying the parameters of the graphics pipeline rasterizer state.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPURasterizerState
    {
        /// <summary>Whether polygons will be filled in or drawn as lines.</summary>
        public SDL_GPUFillMode fill_mode;

        /// <summary>The facing direction in which triangles will be culled.</summary>
        public SDL_GPUCullMode cull_mode;

        /// <summary>The vertex winding that will cause a triangle to be determined as front-facing.</summary>
        public SDL_GPUFrontFace front_face;

        /// <summary>A scalar factor controlling the depth value added to each fragment.</summary>
        public float depth_bias_constant_factor;

        /// <summary>The maximum depth bias of a fragment.</summary>
        public float depth_bias_clamp;

        /// <summary>A scalar factor applied to a fragment's slope in depth calculations.</summary>
        public float depth_bias_slope_factor;

        /// <summary>True to bias fragment depth values.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool enable_depth_bias;

        /// <summary>True to enable depth clip, false to enable depth clamp.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool enable_depth_clip;

        private byte padding1;
        private byte padding2;
    }

    /// <summary>
    /// A structure specifying the parameters of the graphics pipeline multisample state.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUMultisampleState
    {
        /// <summary>The number of samples to be used in rasterization.</summary>
        public SDL_GPUSampleCount sample_count;

        /// <summary>Reserved for future use. Must be set to 0.</summary>
        public uint sample_mask;

        /// <summary>Reserved for future use. Must be set to false.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool enable_mask;

        private byte padding1;
        private byte padding2;
        private byte padding3;
    }

    /// <summary>
    /// A structure specifying the parameters of the graphics pipeline depth stencil state.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUDepthStencilState
    {
        /// <summary>The comparison operator used for depth testing.</summary>
        public SDL_GPUCompareOp compare_op;

        /// <summary>The stencil op state for back-facing triangles.</summary>
        public SDL_GPUStencilOpState back_stencil_state;

        /// <summary>The stencil op state for front-facing triangles.</summary>
        public SDL_GPUStencilOpState front_stencil_state;

        /// <summary>Selects the bits of the stencil values participating in the stencil test.</summary>
        public byte compare_mask;

        /// <summary>Selects the bits of the stencil values updated by the stencil test.</summary>
        public byte write_mask;

        /// <summary>True enables the depth test.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool enable_depth_test;

        /// <summary>True enables depth writes.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool enable_depth_write;

        /// <summary>True enables the stencil test.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool enable_stencil_test;

        private byte padding1;
        private byte padding2;
        private byte padding3;
    }

    /// <summary>
    /// A structure specifying the parameters of color targets used in a graphics pipeline.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUColorTargetDescription
    {
        /// <summary>The pixel format of the texture to be used as a color target.</summary>
        public SDL_GPUTextureFormat format;

        /// <summary>The blend state to be used for the color target.</summary>
        public SDL_GPUColorTargetBlendState blend_state;
    }

    /// <summary>
    /// A structure specifying the descriptions of render targets used in a graphics pipeline.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUGraphicsPipelineTargetInfo
    {
        /// <summary>A pointer to an array of color target descriptions.</summary>
        public SDL_GPUColorTargetDescription* color_target_descriptions;

        /// <summary>The number of color target descriptions in the above array.</summary>
        public uint num_color_targets;

        /// <summary>The pixel format of the depth-stencil target.</summary>
        public SDL_GPUTextureFormat depth_stencil_format;

        /// <summary>True specifies that the pipeline uses a depth-stencil target.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool has_depth_stencil_target;

        private byte padding1;
        private byte padding2;
        private byte padding3;
    }

    /// <summary>
    /// A structure specifying the parameters of a graphics pipeline state.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUGraphicsPipelineCreateInfo
    {
        /// <summary>The vertex shader used by the graphics pipeline.</summary>
        public SDL_GPUShader* vertex_shader;

        /// <summary>The fragment shader used by the graphics pipeline.</summary>
        public SDL_GPUShader* fragment_shader;

        /// <summary>The vertex layout of the graphics pipeline.</summary>
        public SDL_GPUVertexInputState vertex_input_state;

        /// <summary>The primitive topology of the graphics pipeline.</summary>
        public SDL_GPUPrimitiveType primitive_type;

        /// <summary>The rasterizer state of the graphics pipeline.</summary>
        public SDL_GPURasterizerState rasterizer_state;

        /// <summary>The multisample state of the graphics pipeline.</summary>
        public SDL_GPUMultisampleState multisample_state;

        /// <summary>The depth-stencil state of the graphics pipeline.</summary>
        public SDL_GPUDepthStencilState depth_stencil_state;

        /// <summary>Formats and blend modes for the render targets of the graphics pipeline.</summary>
        public SDL_GPUGraphicsPipelineTargetInfo target_info;

        /// <summary>A properties ID for extensions. Should be 0 if no extensions are needed.</summary>
        public SDL_PropertiesID props;
    }

    /// <summary>
    /// A structure specifying the parameters of a compute pipeline state.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUComputePipelineCreateInfo
    {
        /// <summary>The size in bytes of the compute shader code pointed to.</summary>
        public nuint code_size;

        /// <summary>A pointer to compute shader code.</summary>
        public byte* code;

        /// <summary>A pointer to a null-terminated UTF-8 string specifying the entry point function name for the shader.</summary>
        public byte* entrypoint;

        /// <summary>The format of the compute shader code.</summary>
        public SDL_GPUShaderFormat format;

        /// <summary>The number of samplers defined in the shader.</summary>
        public uint num_samplers;

        /// <summary>The number of readonly storage textures defined in the shader.</summary>
        public uint num_readonly_storage_textures;

        /// <summary>The number of readonly storage buffers defined in the shader.</summary>
        public uint num_readonly_storage_buffers;

        /// <summary>The number of read-write storage textures defined in the shader.</summary>
        public uint num_readwrite_storage_textures;

        /// <summary>The number of read-write storage buffers defined in the shader.</summary>
        public uint num_readwrite_storage_buffers;

        /// <summary>The number of uniform buffers defined in the shader.</summary>
        public uint num_uniform_buffers;

        /// <summary>The number of threads in the X dimension.</summary>
        public uint threadcount_x;

        /// <summary>The number of threads in the Y dimension.</summary>
        public uint threadcount_y;

        /// <summary>The number of threads in the Z dimension.</summary>
        public uint threadcount_z;

        /// <summary>A properties ID for extensions. Should be 0 if no extensions are needed.</summary>
        public SDL_PropertiesID props;
    }

    /// <summary>
    /// A structure specifying the parameters of a color target used by a render pass.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUColorTargetInfo
    {
        /// <summary>The texture that will be used as a color target by a render pass.</summary>
        public SDL_GPUTexture* texture;

        /// <summary>The mip level to use as a color target.</summary>
        public uint mip_level;

        /// <summary>The layer index or depth plane to use as a color target.</summary>
        public uint layer_or_depth_plane;

        /// <summary>The color to clear the color target to at the start of the render pass.</summary>
        public SDL_FColor clear_color;

        /// <summary>What is done with the contents of the color target at the beginning of the render pass.</summary>
        public SDL_GPULoadOp load_op;

        /// <summary>What is done with the results of the render pass.</summary>
        public SDL_GPUStoreOp store_op;

        /// <summary>The texture that will receive the results of a multisample resolve operation.</summary>
        public SDL_GPUTexture* resolve_texture;

        /// <summary>The mip level of the resolve texture to use for the resolve operation.</summary>
        public uint resolve_mip_level;

        /// <summary>The layer index of the resolve texture to use for the resolve operation.</summary>
        public uint resolve_layer;

        /// <summary>True cycles the texture if the texture is bound and load_op is not LOAD.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool cycle;

        /// <summary>True cycles the resolve texture if the resolve texture is bound.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool cycle_resolve_texture;

        private byte padding1;
        private byte padding2;
    }

    /// <summary>
    /// A structure specifying the parameters of a depth-stencil target used by a render pass.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUDepthStencilTargetInfo
    {
        /// <summary>The texture that will be used as the depth stencil target by the render pass.</summary>
        public SDL_GPUTexture* texture;

        /// <summary>The value to clear the depth component to at the beginning of the render pass.</summary>
        public float clear_depth;

        /// <summary>What is done with the depth contents at the beginning of the render pass.</summary>
        public SDL_GPULoadOp load_op;

        /// <summary>What is done with the depth results of the render pass.</summary>
        public SDL_GPUStoreOp store_op;

        /// <summary>What is done with the stencil contents at the beginning of the render pass.</summary>
        public SDL_GPULoadOp stencil_load_op;

        /// <summary>What is done with the stencil results of the render pass.</summary>
        public SDL_GPUStoreOp stencil_store_op;

        /// <summary>True cycles the texture if the texture is bound and any load ops are not LOAD.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool cycle;

        /// <summary>The value to clear the stencil component to at the beginning of the render pass.</summary>
        public byte clear_stencil;

        private byte padding1;
        private byte padding2;
    }

    /// <summary>
    /// A structure containing parameters for a blit command.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUBlitInfo
    {
        /// <summary>The source region for the blit.</summary>
        public SDL_GPUBlitRegion source;

        /// <summary>The destination region for the blit.</summary>
        public SDL_GPUBlitRegion destination;

        /// <summary>What is done with the contents of the destination before the blit.</summary>
        public SDL_GPULoadOp load_op;

        /// <summary>The color to clear the destination region to before the blit.</summary>
        public SDL_FColor clear_color;

        /// <summary>The flip mode for the source region.</summary>
        public SDL_FlipMode flip_mode;

        /// <summary>The filter mode used when blitting.</summary>
        public SDL_GPUFilter filter;

        /// <summary>True cycles the destination texture if it is already bound.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool cycle;

        private byte padding1;
        private byte padding2;
        private byte padding3;
    }

    /// <summary>
    /// A structure specifying parameters in a buffer binding call.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUBufferBinding
    {
        /// <summary>The buffer to bind.</summary>
        public SDL_GPUBuffer* buffer;

        /// <summary>The starting byte of the data to bind in the buffer.</summary>
        public uint offset;
    }

    /// <summary>
    /// A structure specifying parameters in a sampler binding call.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUTextureSamplerBinding
    {
        /// <summary>The texture to bind.</summary>
        public SDL_GPUTexture* texture;

        /// <summary>The sampler to bind.</summary>
        public SDL_GPUSampler* sampler;
    }

    /// <summary>
    /// A structure specifying parameters related to binding buffers in a compute pass.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUStorageBufferReadWriteBinding
    {
        /// <summary>The buffer to bind.</summary>
        public SDL_GPUBuffer* buffer;

        /// <summary>True cycles the buffer if it is already bound.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool cycle;

        private byte padding1;
        private byte padding2;
        private byte padding3;
    }

    /// <summary>
    /// A structure specifying parameters related to binding textures in a compute pass.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GPUStorageTextureReadWriteBinding
    {
        /// <summary>The texture to bind.</summary>
        public SDL_GPUTexture* texture;

        /// <summary>The mip level index to bind.</summary>
        public uint mip_level;

        /// <summary>The layer index to bind.</summary>
        public uint layer;

        /// <summary>True cycles the texture if it is already bound.</summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool cycle;

        private byte padding1;
        private byte padding2;
        private byte padding3;
    }

    // Property name constants

    /// <summary>Property name: enable debug mode properties and validations.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_DEBUGMODE_BOOLEAN = "SDL.gpu.device.create.debugmode";

    /// <summary>Property name: enable to prefer energy efficiency over maximum GPU performance.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_PREFERLOWPOWER_BOOLEAN = "SDL.gpu.device.create.preferlowpower";

    /// <summary>Property name: the name of the GPU driver to use.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_NAME_STRING = "SDL.gpu.device.create.name";

    /// <summary>Property name: the app is able to provide shaders for an NDA platform.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_PRIVATE_BOOLEAN = "SDL.gpu.device.create.shaders.private";

    /// <summary>Property name: the app is able to provide SPIR-V shaders if applicable.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_SPIRV_BOOLEAN = "SDL.gpu.device.create.shaders.spirv";

    /// <summary>Property name: the app is able to provide DXBC shaders if applicable.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_DXBC_BOOLEAN = "SDL.gpu.device.create.shaders.dxbc";

    /// <summary>Property name: the app is able to provide DXIL shaders if applicable.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_DXIL_BOOLEAN = "SDL.gpu.device.create.shaders.dxil";

    /// <summary>Property name: the app is able to provide MSL shaders if applicable.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_MSL_BOOLEAN = "SDL.gpu.device.create.shaders.msl";

    /// <summary>Property name: the app is able to provide Metal shader libraries if applicable.</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_METALLIB_BOOLEAN = "SDL.gpu.device.create.shaders.metallib";

    /// <summary>Property name: the prefix to use for all vertex semantics (D3D12).</summary>
    public const string SDL_PROP_GPU_DEVICE_CREATE_D3D12_SEMANTIC_NAME_STRING = "SDL.gpu.device.create.d3d12.semantic";

    /// <summary>Property name: a name that can be displayed in debugging tools (compute pipeline).</summary>
    public const string SDL_PROP_GPU_COMPUTEPIPELINE_CREATE_NAME_STRING = "SDL.gpu.computepipeline.create.name";

    /// <summary>Property name: a name that can be displayed in debugging tools (graphics pipeline).</summary>
    public const string SDL_PROP_GPU_GRAPHICSPIPELINE_CREATE_NAME_STRING = "SDL.gpu.graphicspipeline.create.name";

    /// <summary>Property name: a name that can be displayed in debugging tools (sampler).</summary>
    public const string SDL_PROP_GPU_SAMPLER_CREATE_NAME_STRING = "SDL.gpu.sampler.create.name";

    /// <summary>Property name: a name that can be displayed in debugging tools (shader).</summary>
    public const string SDL_PROP_GPU_SHADER_CREATE_NAME_STRING = "SDL.gpu.shader.create.name";

    /// <summary>Property name: clear the texture to a color with this red intensity (D3D12).</summary>
    public const string SDL_PROP_GPU_TEXTURE_CREATE_D3D12_CLEAR_R_FLOAT = "SDL.gpu.texture.create.d3d12.clear.r";

    /// <summary>Property name: clear the texture to a color with this green intensity (D3D12).</summary>
    public const string SDL_PROP_GPU_TEXTURE_CREATE_D3D12_CLEAR_G_FLOAT = "SDL.gpu.texture.create.d3d12.clear.g";

    /// <summary>Property name: clear the texture to a color with this blue intensity (D3D12).</summary>
    public const string SDL_PROP_GPU_TEXTURE_CREATE_D3D12_CLEAR_B_FLOAT = "SDL.gpu.texture.create.d3d12.clear.b";

    /// <summary>Property name: clear the texture to a color with this alpha intensity (D3D12).</summary>
    public const string SDL_PROP_GPU_TEXTURE_CREATE_D3D12_CLEAR_A_FLOAT = "SDL.gpu.texture.create.d3d12.clear.a";

    /// <summary>Property name: clear the texture to a depth of this value (D3D12).</summary>
    public const string SDL_PROP_GPU_TEXTURE_CREATE_D3D12_CLEAR_DEPTH_FLOAT = "SDL.gpu.texture.create.d3d12.clear.depth";

    /// <summary>Property name: clear the texture to a stencil of this value (D3D12).</summary>
    public const string SDL_PROP_GPU_TEXTURE_CREATE_D3D12_CLEAR_STENCIL_NUMBER = "SDL.gpu.texture.create.d3d12.clear.stencil";

    /// <summary>Property name: a name that can be displayed in debugging tools (texture).</summary>
    public const string SDL_PROP_GPU_TEXTURE_CREATE_NAME_STRING = "SDL.gpu.texture.create.name";

    /// <summary>Property name: a name that can be displayed in debugging tools (buffer).</summary>
    public const string SDL_PROP_GPU_BUFFER_CREATE_NAME_STRING = "SDL.gpu.buffer.create.name";

    /// <summary>Property name: a name that can be displayed in debugging tools (transfer buffer).</summary>
    public const string SDL_PROP_GPU_TRANSFERBUFFER_CREATE_NAME_STRING = "SDL.gpu.transferbuffer.create.name";

    // Device functions

    /// <summary>
    /// Checks for GPU runtime support.
    /// </summary>
    /// <param name="format_flags">A bitmask of shader formats you can provide.</param>
    /// <param name="name">The preferred GPU driver, or NULL to let SDL pick the optimal driver.</param>
    /// <returns>True if supported, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GPUSupportsShaderFormats(
        SDL_GPUShaderFormat format_flags,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? name);

    /// <summary>
    /// Checks for GPU runtime support.
    /// </summary>
    /// <param name="props">The properties to use.</param>
    /// <returns>True if supported, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GPUSupportsProperties(SDL_PropertiesID props);

    /// <summary>
    /// Creates a GPU context.
    /// </summary>
    /// <param name="format_flags">A bitmask of shader formats you can provide.</param>
    /// <param name="debug_mode">Enable debug mode properties and validations.</param>
    /// <param name="name">The preferred GPU driver, or NULL to let SDL pick the optimal driver.</param>
    /// <returns>A GPU context on success or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUDevice* SDL_CreateGPUDevice(
        SDL_GPUShaderFormat format_flags,
        [MarshalAs(UnmanagedType.U1)] bool debug_mode,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? name);

    /// <summary>
    /// Creates a GPU context.
    /// </summary>
    /// <param name="props">The properties to use.</param>
    /// <returns>A GPU context on success or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUDevice* SDL_CreateGPUDeviceWithProperties(SDL_PropertiesID props);

    /// <summary>
    /// Destroys a GPU context previously returned by SDL_CreateGPUDevice.
    /// </summary>
    /// <param name="device">A GPU Context to destroy.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyGPUDevice(SDL_GPUDevice* device);

    /// <summary>
    /// Get the number of GPU drivers compiled into SDL.
    /// </summary>
    /// <returns>The number of built-in GPU drivers.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumGPUDrivers();

    /// <summary>
    /// Get the name of a built-in GPU driver.
    /// </summary>
    /// <param name="index">The index of a GPU driver.</param>
    /// <returns>The name of the GPU driver with the given index.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetGPUDriver(int index);

    /// <summary>
    /// Returns the name of the backend used to create this GPU context.
    /// </summary>
    /// <param name="device">A GPU context to query.</param>
    /// <returns>The name of the device's driver, or NULL on error.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetGPUDeviceDriver(SDL_GPUDevice* device);

    /// <summary>
    /// Returns the supported shader formats for this GPU context.
    /// </summary>
    /// <param name="device">A GPU context to query.</param>
    /// <returns>A bitmask of the supported shader formats.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUShaderFormat SDL_GetGPUShaderFormats(SDL_GPUDevice* device);

    // Resource creation functions

    /// <summary>
    /// Creates a compute pipeline object to be used in a compute workflow.
    /// </summary>
    /// <param name="device">A GPU Context.</param>
    /// <param name="createinfo">A struct describing the state of the compute pipeline to create.</param>
    /// <returns>A compute pipeline object on success, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUComputePipeline* SDL_CreateGPUComputePipeline(
        SDL_GPUDevice* device,
        SDL_GPUComputePipelineCreateInfo* createinfo);

    /// <summary>
    /// Creates a pipeline object to be used in a graphics workflow.
    /// </summary>
    /// <param name="device">A GPU Context.</param>
    /// <param name="createinfo">A struct describing the state of the graphics pipeline to create.</param>
    /// <returns>A graphics pipeline object on success, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUGraphicsPipeline* SDL_CreateGPUGraphicsPipeline(
        SDL_GPUDevice* device,
        SDL_GPUGraphicsPipelineCreateInfo* createinfo);

    /// <summary>
    /// Creates a sampler object to be used when binding textures in a graphics workflow.
    /// </summary>
    /// <param name="device">A GPU Context.</param>
    /// <param name="createinfo">A struct describing the state of the sampler to create.</param>
    /// <returns>A sampler object on success, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUSampler* SDL_CreateGPUSampler(
        SDL_GPUDevice* device,
        SDL_GPUSamplerCreateInfo* createinfo);

    /// <summary>
    /// Creates a shader to be used when creating a graphics pipeline.
    /// </summary>
    /// <param name="device">A GPU Context.</param>
    /// <param name="createinfo">A struct describing the state of the shader to create.</param>
    /// <returns>A shader object on success, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUShader* SDL_CreateGPUShader(
        SDL_GPUDevice* device,
        SDL_GPUShaderCreateInfo* createinfo);

    /// <summary>
    /// Creates a texture object to be used in graphics or compute workflows.
    /// </summary>
    /// <param name="device">A GPU Context.</param>
    /// <param name="createinfo">A struct describing the state of the texture to create.</param>
    /// <returns>A texture object on success, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUTexture* SDL_CreateGPUTexture(
        SDL_GPUDevice* device,
        SDL_GPUTextureCreateInfo* createinfo);

    /// <summary>
    /// Creates a buffer object to be used in graphics or compute workflows.
    /// </summary>
    /// <param name="device">A GPU Context.</param>
    /// <param name="createinfo">A struct describing the state of the buffer to create.</param>
    /// <returns>A buffer object on success, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUBuffer* SDL_CreateGPUBuffer(
        SDL_GPUDevice* device,
        SDL_GPUBufferCreateInfo* createinfo);

    /// <summary>
    /// Creates a transfer buffer to be used when uploading to or downloading from graphics resources.
    /// </summary>
    /// <param name="device">A GPU Context.</param>
    /// <param name="createinfo">A struct describing the state of the transfer buffer to create.</param>
    /// <returns>A transfer buffer on success, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUTransferBuffer* SDL_CreateGPUTransferBuffer(
        SDL_GPUDevice* device,
        SDL_GPUTransferBufferCreateInfo* createinfo);

    // Debug naming functions

    /// <summary>
    /// Sets an arbitrary string constant to label a buffer.
    /// </summary>
    /// <param name="device">A GPU Context.</param>
    /// <param name="buffer">A buffer to attach the name to.</param>
    /// <param name="text">A UTF-8 string constant to mark as the name of the buffer.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetGPUBufferName(
        SDL_GPUDevice* device,
        SDL_GPUBuffer* buffer,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string text);

    /// <summary>
    /// Sets an arbitrary string constant to label a texture.
    /// </summary>
    /// <param name="device">A GPU Context.</param>
    /// <param name="texture">A texture to attach the name to.</param>
    /// <param name="text">A UTF-8 string constant to mark as the name of the texture.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetGPUTextureName(
        SDL_GPUDevice* device,
        SDL_GPUTexture* texture,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string text);

    /// <summary>
    /// Inserts an arbitrary string label into the command buffer callstream.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <param name="text">A UTF-8 string constant to insert as the label.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_InsertGPUDebugLabel(
        SDL_GPUCommandBuffer* command_buffer,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string text);

    /// <summary>
    /// Begins a debug group with an arbitrary name.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <param name="name">A UTF-8 string constant that names the group.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_PushGPUDebugGroup(
        SDL_GPUCommandBuffer* command_buffer,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string name);

    /// <summary>
    /// Ends the most-recently pushed debug group.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_PopGPUDebugGroup(SDL_GPUCommandBuffer* command_buffer);

    // Disposal functions

    /// <summary>
    /// Frees the given texture as soon as it is safe to do so.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="texture">A texture to be destroyed.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ReleaseGPUTexture(SDL_GPUDevice* device, SDL_GPUTexture* texture);

    /// <summary>
    /// Frees the given sampler as soon as it is safe to do so.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="sampler">A sampler to be destroyed.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ReleaseGPUSampler(SDL_GPUDevice* device, SDL_GPUSampler* sampler);

    /// <summary>
    /// Frees the given buffer as soon as it is safe to do so.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="buffer">A buffer to be destroyed.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ReleaseGPUBuffer(SDL_GPUDevice* device, SDL_GPUBuffer* buffer);

    /// <summary>
    /// Frees the given transfer buffer as soon as it is safe to do so.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="transfer_buffer">A transfer buffer to be destroyed.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ReleaseGPUTransferBuffer(SDL_GPUDevice* device, SDL_GPUTransferBuffer* transfer_buffer);

    /// <summary>
    /// Frees the given compute pipeline as soon as it is safe to do so.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="compute_pipeline">A compute pipeline to be destroyed.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ReleaseGPUComputePipeline(SDL_GPUDevice* device, SDL_GPUComputePipeline* compute_pipeline);

    /// <summary>
    /// Frees the given shader as soon as it is safe to do so.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="shader">A shader to be destroyed.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ReleaseGPUShader(SDL_GPUDevice* device, SDL_GPUShader* shader);

    /// <summary>
    /// Frees the given graphics pipeline as soon as it is safe to do so.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="graphics_pipeline">A graphics pipeline to be destroyed.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ReleaseGPUGraphicsPipeline(SDL_GPUDevice* device, SDL_GPUGraphicsPipeline* graphics_pipeline);

    // Command buffer functions

    /// <summary>
    /// Acquire a command buffer.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <returns>A command buffer, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUCommandBuffer* SDL_AcquireGPUCommandBuffer(SDL_GPUDevice* device);

    // Uniform data functions

    /// <summary>
    /// Pushes data to a vertex uniform slot on the command buffer.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <param name="slot_index">The vertex uniform slot to push data to.</param>
    /// <param name="data">Client data to write.</param>
    /// <param name="length">The length of the data to write.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_PushGPUVertexUniformData(
        SDL_GPUCommandBuffer* command_buffer,
        uint slot_index,
        void* data,
        uint length);

    /// <summary>
    /// Pushes data to a fragment uniform slot on the command buffer.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <param name="slot_index">The fragment uniform slot to push data to.</param>
    /// <param name="data">Client data to write.</param>
    /// <param name="length">The length of the data to write.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_PushGPUFragmentUniformData(
        SDL_GPUCommandBuffer* command_buffer,
        uint slot_index,
        void* data,
        uint length);

    /// <summary>
    /// Pushes data to a uniform slot on the command buffer.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <param name="slot_index">The uniform slot to push data to.</param>
    /// <param name="data">Client data to write.</param>
    /// <param name="length">The length of the data to write.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_PushGPUComputeUniformData(
        SDL_GPUCommandBuffer* command_buffer,
        uint slot_index,
        void* data,
        uint length);

    // Render pass functions

    /// <summary>
    /// Begins a render pass on a command buffer.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <param name="color_target_infos">An array of texture subresources with corresponding clear values and load/store ops.</param>
    /// <param name="num_color_targets">The number of color targets in the color_target_infos array.</param>
    /// <param name="depth_stencil_target_info">A texture subresource with corresponding clear value and load/store ops, may be NULL.</param>
    /// <returns>A render pass handle.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPURenderPass* SDL_BeginGPURenderPass(
        SDL_GPUCommandBuffer* command_buffer,
        SDL_GPUColorTargetInfo* color_target_infos,
        uint num_color_targets,
        SDL_GPUDepthStencilTargetInfo* depth_stencil_target_info);

    /// <summary>
    /// Binds a graphics pipeline on a render pass to be used in rendering.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="graphics_pipeline">The graphics pipeline to bind.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUGraphicsPipeline(
        SDL_GPURenderPass* render_pass,
        SDL_GPUGraphicsPipeline* graphics_pipeline);

    /// <summary>
    /// Sets the current viewport state on a command buffer.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="viewport">The viewport to set.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetGPUViewport(SDL_GPURenderPass* render_pass, SDL_GPUViewport* viewport);

    /// <summary>
    /// Sets the current scissor state on a command buffer.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="scissor">The scissor area to set.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetGPUScissor(SDL_GPURenderPass* render_pass, SDL_Rect* scissor);

    /// <summary>
    /// Sets the current blend constants on a command buffer.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="blend_constants">The blend constant color.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetGPUBlendConstants(SDL_GPURenderPass* render_pass, SDL_FColor blend_constants);

    /// <summary>
    /// Sets the current stencil reference value on a command buffer.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="reference">The stencil reference value to set.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetGPUStencilReference(SDL_GPURenderPass* render_pass, byte reference);

    /// <summary>
    /// Binds vertex buffers on a command buffer for use with subsequent draw calls.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="first_slot">The vertex buffer slot to begin binding from.</param>
    /// <param name="bindings">An array of SDL_GPUBufferBinding structs containing vertex buffers and offset values.</param>
    /// <param name="num_bindings">The number of bindings in the bindings array.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUVertexBuffers(
        SDL_GPURenderPass* render_pass,
        uint first_slot,
        SDL_GPUBufferBinding* bindings,
        uint num_bindings);

    /// <summary>
    /// Binds an index buffer on a command buffer for use with subsequent draw calls.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="binding">A pointer to a struct containing an index buffer and offset.</param>
    /// <param name="index_element_size">Whether the index values in the buffer are 16- or 32-bit.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUIndexBuffer(
        SDL_GPURenderPass* render_pass,
        SDL_GPUBufferBinding* binding,
        SDL_GPUIndexElementSize index_element_size);

    /// <summary>
    /// Binds texture-sampler pairs for use on the vertex shader.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="first_slot">The vertex sampler slot to begin binding from.</param>
    /// <param name="texture_sampler_bindings">An array of texture-sampler binding structs.</param>
    /// <param name="num_bindings">The number of texture-sampler pairs to bind from the array.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUVertexSamplers(
        SDL_GPURenderPass* render_pass,
        uint first_slot,
        SDL_GPUTextureSamplerBinding* texture_sampler_bindings,
        uint num_bindings);

    /// <summary>
    /// Binds storage textures for use on the vertex shader.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="first_slot">The vertex storage texture slot to begin binding from.</param>
    /// <param name="storage_textures">An array of storage textures.</param>
    /// <param name="num_bindings">The number of storage texture to bind from the array.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUVertexStorageTextures(
        SDL_GPURenderPass* render_pass,
        uint first_slot,
        SDL_GPUTexture** storage_textures,
        uint num_bindings);

    /// <summary>
    /// Binds storage buffers for use on the vertex shader.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="first_slot">The vertex storage buffer slot to begin binding from.</param>
    /// <param name="storage_buffers">An array of buffers.</param>
    /// <param name="num_bindings">The number of buffers to bind from the array.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUVertexStorageBuffers(
        SDL_GPURenderPass* render_pass,
        uint first_slot,
        SDL_GPUBuffer** storage_buffers,
        uint num_bindings);

    /// <summary>
    /// Binds texture-sampler pairs for use on the fragment shader.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="first_slot">The fragment sampler slot to begin binding from.</param>
    /// <param name="texture_sampler_bindings">An array of texture-sampler binding structs.</param>
    /// <param name="num_bindings">The number of texture-sampler pairs to bind from the array.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUFragmentSamplers(
        SDL_GPURenderPass* render_pass,
        uint first_slot,
        SDL_GPUTextureSamplerBinding* texture_sampler_bindings,
        uint num_bindings);

    /// <summary>
    /// Binds storage textures for use on the fragment shader.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="first_slot">The fragment storage texture slot to begin binding from.</param>
    /// <param name="storage_textures">An array of storage textures.</param>
    /// <param name="num_bindings">The number of storage textures to bind from the array.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUFragmentStorageTextures(
        SDL_GPURenderPass* render_pass,
        uint first_slot,
        SDL_GPUTexture** storage_textures,
        uint num_bindings);

    /// <summary>
    /// Binds storage buffers for use on the fragment shader.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="first_slot">The fragment storage buffer slot to begin binding from.</param>
    /// <param name="storage_buffers">An array of storage buffers.</param>
    /// <param name="num_bindings">The number of storage buffers to bind from the array.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUFragmentStorageBuffers(
        SDL_GPURenderPass* render_pass,
        uint first_slot,
        SDL_GPUBuffer** storage_buffers,
        uint num_bindings);

    // Drawing functions

    /// <summary>
    /// Draws data using bound graphics state with an index buffer and instancing enabled.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="num_indices">The number of indices to draw per instance.</param>
    /// <param name="num_instances">The number of instances to draw.</param>
    /// <param name="first_index">The starting index within the index buffer.</param>
    /// <param name="vertex_offset">Value added to vertex index before indexing into the vertex buffer.</param>
    /// <param name="first_instance">The ID of the first instance to draw.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DrawGPUIndexedPrimitives(
        SDL_GPURenderPass* render_pass,
        uint num_indices,
        uint num_instances,
        uint first_index,
        int vertex_offset,
        uint first_instance);

    /// <summary>
    /// Draws data using bound graphics state.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="num_vertices">The number of vertices to draw.</param>
    /// <param name="num_instances">The number of instances that will be drawn.</param>
    /// <param name="first_vertex">The index of the first vertex to draw.</param>
    /// <param name="first_instance">The ID of the first instance to draw.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DrawGPUPrimitives(
        SDL_GPURenderPass* render_pass,
        uint num_vertices,
        uint num_instances,
        uint first_vertex,
        uint first_instance);

    /// <summary>
    /// Draws data using bound graphics state and with draw parameters set from a buffer.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="buffer">A buffer containing draw parameters.</param>
    /// <param name="offset">The offset to start reading from the draw buffer.</param>
    /// <param name="draw_count">The number of draw parameter sets that should be read from the draw buffer.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DrawGPUPrimitivesIndirect(
        SDL_GPURenderPass* render_pass,
        SDL_GPUBuffer* buffer,
        uint offset,
        uint draw_count);

    /// <summary>
    /// Draws data using bound graphics state with an index buffer enabled and with draw parameters set from a buffer.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    /// <param name="buffer">A buffer containing draw parameters.</param>
    /// <param name="offset">The offset to start reading from the draw buffer.</param>
    /// <param name="draw_count">The number of draw parameter sets that should be read from the draw buffer.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DrawGPUIndexedPrimitivesIndirect(
        SDL_GPURenderPass* render_pass,
        SDL_GPUBuffer* buffer,
        uint offset,
        uint draw_count);

    /// <summary>
    /// Ends the given render pass.
    /// </summary>
    /// <param name="render_pass">A render pass handle.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_EndGPURenderPass(SDL_GPURenderPass* render_pass);

    // Compute pass functions

    /// <summary>
    /// Begins a compute pass on a command buffer.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <param name="storage_texture_bindings">An array of writeable storage texture binding structs.</param>
    /// <param name="num_storage_texture_bindings">The number of storage textures to bind from the array.</param>
    /// <param name="storage_buffer_bindings">An array of writeable storage buffer binding structs.</param>
    /// <param name="num_storage_buffer_bindings">The number of storage buffers to bind from the array.</param>
    /// <returns>A compute pass handle.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUComputePass* SDL_BeginGPUComputePass(
        SDL_GPUCommandBuffer* command_buffer,
        SDL_GPUStorageTextureReadWriteBinding* storage_texture_bindings,
        uint num_storage_texture_bindings,
        SDL_GPUStorageBufferReadWriteBinding* storage_buffer_bindings,
        uint num_storage_buffer_bindings);

    /// <summary>
    /// Binds a compute pipeline on a command buffer for use in compute dispatch.
    /// </summary>
    /// <param name="compute_pass">A compute pass handle.</param>
    /// <param name="compute_pipeline">A compute pipeline to bind.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUComputePipeline(
        SDL_GPUComputePass* compute_pass,
        SDL_GPUComputePipeline* compute_pipeline);

    /// <summary>
    /// Binds texture-sampler pairs for use on the compute shader.
    /// </summary>
    /// <param name="compute_pass">A compute pass handle.</param>
    /// <param name="first_slot">The compute sampler slot to begin binding from.</param>
    /// <param name="texture_sampler_bindings">An array of texture-sampler binding structs.</param>
    /// <param name="num_bindings">The number of texture-sampler bindings to bind from the array.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUComputeSamplers(
        SDL_GPUComputePass* compute_pass,
        uint first_slot,
        SDL_GPUTextureSamplerBinding* texture_sampler_bindings,
        uint num_bindings);

    /// <summary>
    /// Binds storage textures as readonly for use on the compute pipeline.
    /// </summary>
    /// <param name="compute_pass">A compute pass handle.</param>
    /// <param name="first_slot">The compute storage texture slot to begin binding from.</param>
    /// <param name="storage_textures">An array of storage textures.</param>
    /// <param name="num_bindings">The number of storage textures to bind from the array.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUComputeStorageTextures(
        SDL_GPUComputePass* compute_pass,
        uint first_slot,
        SDL_GPUTexture** storage_textures,
        uint num_bindings);

    /// <summary>
    /// Binds storage buffers as readonly for use on the compute pipeline.
    /// </summary>
    /// <param name="compute_pass">A compute pass handle.</param>
    /// <param name="first_slot">The compute storage buffer slot to begin binding from.</param>
    /// <param name="storage_buffers">An array of storage buffer binding structs.</param>
    /// <param name="num_bindings">The number of storage buffers to bind from the array.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BindGPUComputeStorageBuffers(
        SDL_GPUComputePass* compute_pass,
        uint first_slot,
        SDL_GPUBuffer** storage_buffers,
        uint num_bindings);

    /// <summary>
    /// Dispatches compute work.
    /// </summary>
    /// <param name="compute_pass">A compute pass handle.</param>
    /// <param name="groupcount_x">Number of local workgroups to dispatch in the X dimension.</param>
    /// <param name="groupcount_y">Number of local workgroups to dispatch in the Y dimension.</param>
    /// <param name="groupcount_z">Number of local workgroups to dispatch in the Z dimension.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DispatchGPUCompute(
        SDL_GPUComputePass* compute_pass,
        uint groupcount_x,
        uint groupcount_y,
        uint groupcount_z);

    /// <summary>
    /// Dispatches compute work with parameters set from a buffer.
    /// </summary>
    /// <param name="compute_pass">A compute pass handle.</param>
    /// <param name="buffer">A buffer containing dispatch parameters.</param>
    /// <param name="offset">The offset to start reading from the dispatch buffer.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DispatchGPUComputeIndirect(
        SDL_GPUComputePass* compute_pass,
        SDL_GPUBuffer* buffer,
        uint offset);

    /// <summary>
    /// Ends the current compute pass.
    /// </summary>
    /// <param name="compute_pass">A compute pass handle.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_EndGPUComputePass(SDL_GPUComputePass* compute_pass);

    // Transfer buffer data functions

    /// <summary>
    /// Maps a transfer buffer into application address space.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="transfer_buffer">A transfer buffer.</param>
    /// <param name="cycle">If true, cycles the transfer buffer if it is already bound.</param>
    /// <returns>The address of the mapped transfer buffer memory, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_MapGPUTransferBuffer(
        SDL_GPUDevice* device,
        SDL_GPUTransferBuffer* transfer_buffer,
        [MarshalAs(UnmanagedType.U1)] bool cycle);

    /// <summary>
    /// Unmaps a previously mapped transfer buffer.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="transfer_buffer">A previously mapped transfer buffer.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnmapGPUTransferBuffer(
        SDL_GPUDevice* device,
        SDL_GPUTransferBuffer* transfer_buffer);

    // Copy pass functions

    /// <summary>
    /// Begins a copy pass on a command buffer.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <returns>A copy pass handle.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUCopyPass* SDL_BeginGPUCopyPass(SDL_GPUCommandBuffer* command_buffer);

    /// <summary>
    /// Uploads data from a transfer buffer to a texture.
    /// </summary>
    /// <param name="copy_pass">A copy pass handle.</param>
    /// <param name="source">The source transfer buffer with image layout information.</param>
    /// <param name="destination">The destination texture region.</param>
    /// <param name="cycle">If true, cycles the texture if the texture is bound, otherwise overwrites the data.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UploadToGPUTexture(
        SDL_GPUCopyPass* copy_pass,
        SDL_GPUTextureTransferInfo* source,
        SDL_GPUTextureRegion* destination,
        [MarshalAs(UnmanagedType.U1)] bool cycle);

    /// <summary>
    /// Uploads data from a transfer buffer to a buffer.
    /// </summary>
    /// <param name="copy_pass">A copy pass handle.</param>
    /// <param name="source">The source transfer buffer with offset.</param>
    /// <param name="destination">The destination buffer with offset and size.</param>
    /// <param name="cycle">If true, cycles the buffer if it is already bound, otherwise overwrites the data.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UploadToGPUBuffer(
        SDL_GPUCopyPass* copy_pass,
        SDL_GPUTransferBufferLocation* source,
        SDL_GPUBufferRegion* destination,
        [MarshalAs(UnmanagedType.U1)] bool cycle);

    /// <summary>
    /// Performs a texture-to-texture copy.
    /// </summary>
    /// <param name="copy_pass">A copy pass handle.</param>
    /// <param name="source">A source texture region.</param>
    /// <param name="destination">A destination texture region.</param>
    /// <param name="w">The width of the region to copy.</param>
    /// <param name="h">The height of the region to copy.</param>
    /// <param name="d">The depth of the region to copy.</param>
    /// <param name="cycle">If true, cycles the destination texture if the destination texture is bound, otherwise overwrites the data.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_CopyGPUTextureToTexture(
        SDL_GPUCopyPass* copy_pass,
        SDL_GPUTextureLocation* source,
        SDL_GPUTextureLocation* destination,
        uint w,
        uint h,
        uint d,
        [MarshalAs(UnmanagedType.U1)] bool cycle);

    /// <summary>
    /// Performs a buffer-to-buffer copy.
    /// </summary>
    /// <param name="copy_pass">A copy pass handle.</param>
    /// <param name="source">The buffer and offset to copy from.</param>
    /// <param name="destination">The buffer and offset to copy to.</param>
    /// <param name="size">The length of the buffer to copy.</param>
    /// <param name="cycle">If true, cycles the destination buffer if it is already bound, otherwise overwrites the data.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_CopyGPUBufferToBuffer(
        SDL_GPUCopyPass* copy_pass,
        SDL_GPUBufferLocation* source,
        SDL_GPUBufferLocation* destination,
        uint size,
        [MarshalAs(UnmanagedType.U1)] bool cycle);

    /// <summary>
    /// Copies data from a texture to a transfer buffer on the GPU timeline.
    /// </summary>
    /// <param name="copy_pass">A copy pass handle.</param>
    /// <param name="source">The source texture region.</param>
    /// <param name="destination">The destination transfer buffer with image layout information.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DownloadFromGPUTexture(
        SDL_GPUCopyPass* copy_pass,
        SDL_GPUTextureRegion* source,
        SDL_GPUTextureTransferInfo* destination);

    /// <summary>
    /// Copies data from a buffer to a transfer buffer on the GPU timeline.
    /// </summary>
    /// <param name="copy_pass">A copy pass handle.</param>
    /// <param name="source">The source buffer with offset and size.</param>
    /// <param name="destination">The destination transfer buffer with offset.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DownloadFromGPUBuffer(
        SDL_GPUCopyPass* copy_pass,
        SDL_GPUBufferRegion* source,
        SDL_GPUTransferBufferLocation* destination);

    /// <summary>
    /// Ends the current copy pass.
    /// </summary>
    /// <param name="copy_pass">A copy pass handle.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_EndGPUCopyPass(SDL_GPUCopyPass* copy_pass);

    /// <summary>
    /// Generates mipmaps for the given texture.
    /// </summary>
    /// <param name="command_buffer">A command_buffer.</param>
    /// <param name="texture">A texture with more than 1 mip level.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_GenerateMipmapsForGPUTexture(
        SDL_GPUCommandBuffer* command_buffer,
        SDL_GPUTexture* texture);

    /// <summary>
    /// Blits from a source texture region to a destination texture region.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <param name="info">The blit info struct containing the blit parameters.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_BlitGPUTexture(
        SDL_GPUCommandBuffer* command_buffer,
        SDL_GPUBlitInfo* info);

    // Submission/Presentation functions

    /// <summary>
    /// Determines whether a swapchain composition is supported by the window.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="window">An SDL_Window.</param>
    /// <param name="swapchain_composition">The swapchain composition to check.</param>
    /// <returns>True if supported, false if unsupported.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WindowSupportsGPUSwapchainComposition(
        SDL_GPUDevice* device,
        SDL_Window* window,
        SDL_GPUSwapchainComposition swapchain_composition);

    /// <summary>
    /// Determines whether a presentation mode is supported by the window.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="window">An SDL_Window.</param>
    /// <param name="present_mode">The presentation mode to check.</param>
    /// <returns>True if supported, false if unsupported.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WindowSupportsGPUPresentMode(
        SDL_GPUDevice* device,
        SDL_Window* window,
        SDL_GPUPresentMode present_mode);

    /// <summary>
    /// Claims a window, creating a swapchain structure for it.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="window">An SDL_Window.</param>
    /// <returns>True on success, or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ClaimWindowForGPUDevice(SDL_GPUDevice* device, SDL_Window* window);

    /// <summary>
    /// Unclaims a window, destroying its swapchain structure.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="window">An SDL_Window that has been claimed.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ReleaseWindowFromGPUDevice(SDL_GPUDevice* device, SDL_Window* window);

    /// <summary>
    /// Changes the swapchain parameters for the given claimed window.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="window">An SDL_Window that has been claimed.</param>
    /// <param name="swapchain_composition">The desired composition of the swapchain.</param>
    /// <param name="present_mode">The desired present mode for the swapchain.</param>
    /// <returns>True if successful, false on error; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetGPUSwapchainParameters(
        SDL_GPUDevice* device,
        SDL_Window* window,
        SDL_GPUSwapchainComposition swapchain_composition,
        SDL_GPUPresentMode present_mode);

    /// <summary>
    /// Configures the maximum allowed number of frames in flight.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="allowed_frames_in_flight">The maximum number of frames that can be pending on the GPU.</param>
    /// <returns>True if successful, false on error; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetGPUAllowedFramesInFlight(SDL_GPUDevice* device, uint allowed_frames_in_flight);

    /// <summary>
    /// Obtains the texture format of the swapchain for the given window.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="window">An SDL_Window that has been claimed.</param>
    /// <returns>The texture format of the swapchain.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUTextureFormat SDL_GetGPUSwapchainTextureFormat(SDL_GPUDevice* device, SDL_Window* window);

    /// <summary>
    /// Acquire a texture to use in presentation.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <param name="window">A window that has been claimed.</param>
    /// <param name="swapchain_texture">A pointer filled in with a swapchain texture handle.</param>
    /// <param name="swapchain_texture_width">A pointer filled in with the swapchain texture width, may be NULL.</param>
    /// <param name="swapchain_texture_height">A pointer filled in with the swapchain texture height, may be NULL.</param>
    /// <returns>True on success, false on error; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_AcquireGPUSwapchainTexture(
        SDL_GPUCommandBuffer* command_buffer,
        SDL_Window* window,
        SDL_GPUTexture** swapchain_texture,
        uint* swapchain_texture_width,
        uint* swapchain_texture_height);

    /// <summary>
    /// Blocks the thread until a swapchain texture is available to be acquired.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="window">A window that has been claimed.</param>
    /// <returns>True on success, false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WaitForGPUSwapchain(SDL_GPUDevice* device, SDL_Window* window);

    /// <summary>
    /// Blocks the thread until a swapchain texture is available to be acquired, and then acquires it.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <param name="window">A window that has been claimed.</param>
    /// <param name="swapchain_texture">A pointer filled in with a swapchain texture handle.</param>
    /// <param name="swapchain_texture_width">A pointer filled in with the swapchain texture width, may be NULL.</param>
    /// <param name="swapchain_texture_height">A pointer filled in with the swapchain texture height, may be NULL.</param>
    /// <returns>True on success, false on error; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WaitAndAcquireGPUSwapchainTexture(
        SDL_GPUCommandBuffer* command_buffer,
        SDL_Window* window,
        SDL_GPUTexture** swapchain_texture,
        uint* swapchain_texture_width,
        uint* swapchain_texture_height);

    /// <summary>
    /// Submits a command buffer so its commands can be processed on the GPU.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <returns>True on success, false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SubmitGPUCommandBuffer(SDL_GPUCommandBuffer* command_buffer);

    /// <summary>
    /// Submits a command buffer so its commands can be processed on the GPU, and acquires a fence associated with the command buffer.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <returns>A fence associated with the command buffer, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GPUFence* SDL_SubmitGPUCommandBufferAndAcquireFence(SDL_GPUCommandBuffer* command_buffer);

    /// <summary>
    /// Cancels a command buffer.
    /// </summary>
    /// <param name="command_buffer">A command buffer.</param>
    /// <returns>True on success, false on error; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CancelGPUCommandBuffer(SDL_GPUCommandBuffer* command_buffer);

    /// <summary>
    /// Blocks the thread until the GPU is completely idle.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <returns>True on success, false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WaitForGPUIdle(SDL_GPUDevice* device);

    /// <summary>
    /// Blocks the thread until the given fences are signaled.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="wait_all">If 0, wait for any fence to be signaled, if 1, wait for all fences to be signaled.</param>
    /// <param name="fences">An array of fences to wait on.</param>
    /// <param name="num_fences">The number of fences in the fences array.</param>
    /// <returns>True on success, false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WaitForGPUFences(
        SDL_GPUDevice* device,
        [MarshalAs(UnmanagedType.U1)] bool wait_all,
        SDL_GPUFence** fences,
        uint num_fences);

    /// <summary>
    /// Checks the status of a fence.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="fence">A fence.</param>
    /// <returns>True if the fence is signaled, false if it is not.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_QueryGPUFence(SDL_GPUDevice* device, SDL_GPUFence* fence);

    /// <summary>
    /// Releases a fence obtained from SDL_SubmitGPUCommandBufferAndAcquireFence.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="fence">A fence.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_ReleaseGPUFence(SDL_GPUDevice* device, SDL_GPUFence* fence);

    // Format info functions

    /// <summary>
    /// Obtains the texel block size for a texture format.
    /// </summary>
    /// <param name="format">The texture format you want to know the texel size of.</param>
    /// <returns>The texel block size of the texture format.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_GPUTextureFormatTexelBlockSize(SDL_GPUTextureFormat format);

    /// <summary>
    /// Determines whether a texture format is supported for a given type and usage.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="format">The texture format to check.</param>
    /// <param name="type">The type of texture (2D, 3D, Cube).</param>
    /// <param name="usage">A bitmask of all usage scenarios to check.</param>
    /// <returns>Whether the texture format is supported for this type and usage.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GPUTextureSupportsFormat(
        SDL_GPUDevice* device,
        SDL_GPUTextureFormat format,
        SDL_GPUTextureType type,
        SDL_GPUTextureUsageFlags usage);

    /// <summary>
    /// Determines if a sample count for a texture format is supported.
    /// </summary>
    /// <param name="device">A GPU context.</param>
    /// <param name="format">The texture format to check.</param>
    /// <param name="sample_count">The sample count to check.</param>
    /// <returns>Whether the sample count is supported for this texture format.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GPUTextureSupportsSampleCount(
        SDL_GPUDevice* device,
        SDL_GPUTextureFormat format,
        SDL_GPUSampleCount sample_count);

    /// <summary>
    /// Calculate the size in bytes of a texture format with dimensions.
    /// </summary>
    /// <param name="format">A texture format.</param>
    /// <param name="width">Width in pixels.</param>
    /// <param name="height">Height in pixels.</param>
    /// <param name="depth_or_layer_count">Depth for 3D textures or layer count otherwise.</param>
    /// <returns>The size of a texture with this format and dimensions.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint SDL_CalculateGPUTextureFormatSize(
        SDL_GPUTextureFormat format,
        uint width,
        uint height,
        uint depth_or_layer_count);

    // SDL_GDKSuspendGPU and SDL_GDKResumeGPU are Xbox-specific and not wrapped
}
