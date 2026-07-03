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
