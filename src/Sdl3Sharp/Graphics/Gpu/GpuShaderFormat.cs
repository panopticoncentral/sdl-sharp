namespace Sdl3Sharp.Graphics.Gpu;

/// <summary>
/// Specifies the format of shader code.
/// </summary>
[Flags]
public enum GpuShaderFormat : uint
{
    /// <summary>Invalid shader format.</summary>
    Invalid = 0,

    /// <summary>Shaders for NDA'd platforms.</summary>
    Private = 1u << 0,

    /// <summary>SPIR-V shaders for Vulkan.</summary>
    SpirV = 1u << 1,

    /// <summary>DXBC SM5_1 shaders for D3D12.</summary>
    Dxbc = 1u << 2,

    /// <summary>DXIL SM6_0 shaders for D3D12.</summary>
    Dxil = 1u << 3,

    /// <summary>MSL shaders for Metal.</summary>
    Msl = 1u << 4,

    /// <summary>Precompiled metallib shaders for Metal.</summary>
    MetalLib = 1u << 5
}
