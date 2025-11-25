namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies the pixel format of a texture.
/// </summary>
public enum GpuTextureFormat
{
    /// <summary>Invalid texture format.</summary>
    Invalid,

    /// <summary>A8 unsigned normalized format.</summary>
    A8UNorm,
    /// <summary>R8 unsigned normalized format.</summary>
    R8UNorm,
    /// <summary>R8G8 unsigned normalized format.</summary>
    R8G8UNorm,
    /// <summary>R8G8B8A8 unsigned normalized format.</summary>
    R8G8B8A8UNorm,
    /// <summary>R16 unsigned normalized format.</summary>
    R16UNorm,
    /// <summary>R16G16 unsigned normalized format.</summary>
    R16G16UNorm,
    /// <summary>R16G16B16A16 unsigned normalized format.</summary>
    R16G16B16A16UNorm,
    /// <summary>R10G10B10A2 unsigned normalized format.</summary>
    R10G10B10A2UNorm,
    /// <summary>B5G6R5 unsigned normalized format.</summary>
    B5G6R5UNorm,
    /// <summary>B5G5R5A1 unsigned normalized format.</summary>
    B5G5R5A1UNorm,
    /// <summary>B4G4R4A4 unsigned normalized format.</summary>
    B4G4R4A4UNorm,
    /// <summary>B8G8R8A8 unsigned normalized format.</summary>
    B8G8R8A8UNorm,

    /// <summary>BC1 RGBA unsigned normalized format.</summary>
    BC1RgbaUNorm,
    /// <summary>BC2 RGBA unsigned normalized format.</summary>
    BC2RgbaUNorm,
    /// <summary>BC3 RGBA unsigned normalized format.</summary>
    BC3RgbaUNorm,
    /// <summary>BC4 R unsigned normalized format.</summary>
    BC4RUNorm,
    /// <summary>BC5 RG unsigned normalized format.</summary>
    BC5RgUNorm,
    /// <summary>BC7 RGBA unsigned normalized format.</summary>
    BC7RgbaUNorm,

    /// <summary>BC6H RGB signed float format.</summary>
    BC6HRgbFloat,
    /// <summary>BC6H RGB unsigned float format.</summary>
    BC6HRgbUFloat,

    /// <summary>R8 signed normalized format.</summary>
    R8SNorm,
    /// <summary>R8G8 signed normalized format.</summary>
    R8G8SNorm,
    /// <summary>R8G8B8A8 signed normalized format.</summary>
    R8G8B8A8SNorm,
    /// <summary>R16 signed normalized format.</summary>
    R16SNorm,
    /// <summary>R16G16 signed normalized format.</summary>
    R16G16SNorm,
    /// <summary>R16G16B16A16 signed normalized format.</summary>
    R16G16B16A16SNorm,

    /// <summary>R16 float format.</summary>
    R16Float,
    /// <summary>R16G16 float format.</summary>
    R16G16Float,
    /// <summary>R16G16B16A16 float format.</summary>
    R16G16B16A16Float,
    /// <summary>R32 float format.</summary>
    R32Float,
    /// <summary>R32G32 float format.</summary>
    R32G32Float,
    /// <summary>R32G32B32A32 float format.</summary>
    R32G32B32A32Float,

    /// <summary>R11G11B10 unsigned float format.</summary>
    R11G11B10UFloat,

    /// <summary>R8 unsigned integer format.</summary>
    R8UInt,
    /// <summary>R8G8 unsigned integer format.</summary>
    R8G8UInt,
    /// <summary>R8G8B8A8 unsigned integer format.</summary>
    R8G8B8A8UInt,
    /// <summary>R16 unsigned integer format.</summary>
    R16UInt,
    /// <summary>R16G16 unsigned integer format.</summary>
    R16G16UInt,
    /// <summary>R16G16B16A16 unsigned integer format.</summary>
    R16G16B16A16UInt,
    /// <summary>R32 unsigned integer format.</summary>
    R32UInt,
    /// <summary>R32G32 unsigned integer format.</summary>
    R32G32UInt,
    /// <summary>R32G32B32A32 unsigned integer format.</summary>
    R32G32B32A32UInt,

    /// <summary>R8 signed integer format.</summary>
    R8Int,
    /// <summary>R8G8 signed integer format.</summary>
    R8G8Int,
    /// <summary>R8G8B8A8 signed integer format.</summary>
    R8G8B8A8Int,
    /// <summary>R16 signed integer format.</summary>
    R16Int,
    /// <summary>R16G16 signed integer format.</summary>
    R16G16Int,
    /// <summary>R16G16B16A16 signed integer format.</summary>
    R16G16B16A16Int,
    /// <summary>R32 signed integer format.</summary>
    R32Int,
    /// <summary>R32G32 signed integer format.</summary>
    R32G32Int,
    /// <summary>R32G32B32A32 signed integer format.</summary>
    R32G32B32A32Int,

    /// <summary>R8G8B8A8 unsigned normalized sRGB format.</summary>
    R8G8B8A8UNormSrgb,
    /// <summary>B8G8R8A8 unsigned normalized sRGB format.</summary>
    B8G8R8A8UNormSrgb,

    /// <summary>BC1 RGBA unsigned normalized sRGB format.</summary>
    BC1RgbaUNormSrgb,
    /// <summary>BC2 RGBA unsigned normalized sRGB format.</summary>
    BC2RgbaUNormSrgb,
    /// <summary>BC3 RGBA unsigned normalized sRGB format.</summary>
    BC3RgbaUNormSrgb,
    /// <summary>BC7 RGBA unsigned normalized sRGB format.</summary>
    BC7RgbaUNormSrgb,

    /// <summary>D16 unsigned normalized depth format.</summary>
    D16UNorm,
    /// <summary>D24 unsigned normalized depth format.</summary>
    D24UNorm,
    /// <summary>D32 float depth format.</summary>
    D32Float,
    /// <summary>D24 unsigned normalized depth with S8 unsigned integer stencil format.</summary>
    D24UNormS8UInt,
    /// <summary>D32 float depth with S8 unsigned integer stencil format.</summary>
    D32FloatS8UInt,

    /// <summary>ASTC 4x4 unsigned normalized format.</summary>
    Astc4x4UNorm,
    /// <summary>ASTC 5x4 unsigned normalized format.</summary>
    Astc5x4UNorm,
    /// <summary>ASTC 5x5 unsigned normalized format.</summary>
    Astc5x5UNorm,
    /// <summary>ASTC 6x5 unsigned normalized format.</summary>
    Astc6x5UNorm,
    /// <summary>ASTC 6x6 unsigned normalized format.</summary>
    Astc6x6UNorm,
    /// <summary>ASTC 8x5 unsigned normalized format.</summary>
    Astc8x5UNorm,
    /// <summary>ASTC 8x6 unsigned normalized format.</summary>
    Astc8x6UNorm,
    /// <summary>ASTC 8x8 unsigned normalized format.</summary>
    Astc8x8UNorm,
    /// <summary>ASTC 10x5 unsigned normalized format.</summary>
    Astc10x5UNorm,
    /// <summary>ASTC 10x6 unsigned normalized format.</summary>
    Astc10x6UNorm,
    /// <summary>ASTC 10x8 unsigned normalized format.</summary>
    Astc10x8UNorm,
    /// <summary>ASTC 10x10 unsigned normalized format.</summary>
    Astc10x10UNorm,
    /// <summary>ASTC 12x10 unsigned normalized format.</summary>
    Astc12x10UNorm,
    /// <summary>ASTC 12x12 unsigned normalized format.</summary>
    Astc12x12UNorm,

    /// <summary>ASTC 4x4 unsigned normalized sRGB format.</summary>
    Astc4x4UNormSrgb,
    /// <summary>ASTC 5x4 unsigned normalized sRGB format.</summary>
    Astc5x4UNormSrgb,
    /// <summary>ASTC 5x5 unsigned normalized sRGB format.</summary>
    Astc5x5UNormSrgb,
    /// <summary>ASTC 6x5 unsigned normalized sRGB format.</summary>
    Astc6x5UNormSrgb,
    /// <summary>ASTC 6x6 unsigned normalized sRGB format.</summary>
    Astc6x6UNormSrgb,
    /// <summary>ASTC 8x5 unsigned normalized sRGB format.</summary>
    Astc8x5UNormSrgb,
    /// <summary>ASTC 8x6 unsigned normalized sRGB format.</summary>
    Astc8x6UNormSrgb,
    /// <summary>ASTC 8x8 unsigned normalized sRGB format.</summary>
    Astc8x8UNormSrgb,
    /// <summary>ASTC 10x5 unsigned normalized sRGB format.</summary>
    Astc10x5UNormSrgb,
    /// <summary>ASTC 10x6 unsigned normalized sRGB format.</summary>
    Astc10x6UNormSrgb,
    /// <summary>ASTC 10x8 unsigned normalized sRGB format.</summary>
    Astc10x8UNormSrgb,
    /// <summary>ASTC 10x10 unsigned normalized sRGB format.</summary>
    Astc10x10UNormSrgb,
    /// <summary>ASTC 12x10 unsigned normalized sRGB format.</summary>
    Astc12x10UNormSrgb,
    /// <summary>ASTC 12x12 unsigned normalized sRGB format.</summary>
    Astc12x12UNormSrgb,

    /// <summary>ASTC 4x4 float format.</summary>
    Astc4x4Float,
    /// <summary>ASTC 5x4 float format.</summary>
    Astc5x4Float,
    /// <summary>ASTC 5x5 float format.</summary>
    Astc5x5Float,
    /// <summary>ASTC 6x5 float format.</summary>
    Astc6x5Float,
    /// <summary>ASTC 6x6 float format.</summary>
    Astc6x6Float,
    /// <summary>ASTC 8x5 float format.</summary>
    Astc8x5Float,
    /// <summary>ASTC 8x6 float format.</summary>
    Astc8x6Float,
    /// <summary>ASTC 8x8 float format.</summary>
    Astc8x8Float,
    /// <summary>ASTC 10x5 float format.</summary>
    Astc10x5Float,
    /// <summary>ASTC 10x6 float format.</summary>
    Astc10x6Float,
    /// <summary>ASTC 10x8 float format.</summary>
    Astc10x8Float,
    /// <summary>ASTC 10x10 float format.</summary>
    Astc10x10Float,
    /// <summary>ASTC 12x10 float format.</summary>
    Astc12x10Float,
    /// <summary>ASTC 12x12 float format.</summary>
    Astc12x12Float
}
