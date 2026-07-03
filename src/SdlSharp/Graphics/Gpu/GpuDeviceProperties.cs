namespace SdlSharp.Graphics.Gpu;

/// <summary>
/// Property names accepted by <see cref="GpuDevice.Create(PropertyGroup)"/>
/// (the SDL_PROP_GPU_DEVICE_CREATE_* constants).
/// </summary>
public static class GpuDeviceProperties
{
    /// <summary>Enable debug mode (boolean).</summary>
    public const string DebugMode = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_DEBUGMODE_BOOLEAN;
    /// <summary>Prefer a low-power GPU (boolean).</summary>
    public const string PreferLowPower = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_PREFERLOWPOWER_BOOLEAN;
    /// <summary>Automatically log GPU activity (boolean).</summary>
    public const string Verbose = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_VERBOSE_BOOLEAN;
    /// <summary>The name of the GPU driver to use (string).</summary>
    public const string Name = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_NAME_STRING;
    /// <summary>Enable Vulkan clip distance support (boolean).</summary>
    public const string FeatureClipDistance = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_FEATURE_CLIP_DISTANCE_BOOLEAN;
    /// <summary>Enable depth clamping support (boolean).</summary>
    public const string FeatureDepthClamping = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_FEATURE_DEPTH_CLAMPING_BOOLEAN;
    /// <summary>Enable indirect draw first-instance support (boolean).</summary>
    public const string FeatureIndirectDrawFirstInstance = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_FEATURE_INDIRECT_DRAW_FIRST_INSTANCE_BOOLEAN;
    /// <summary>Enable anisotropic filtering support (boolean).</summary>
    public const string FeatureAnisotropy = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_FEATURE_ANISOTROPY_BOOLEAN;
    /// <summary>The app can provide private (NDA) shaders (boolean).</summary>
    public const string ShadersPrivate = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_SHADERS_PRIVATE_BOOLEAN;
    /// <summary>The app can provide SPIR-V shaders (boolean).</summary>
    public const string ShadersSpirv = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_SHADERS_SPIRV_BOOLEAN;
    /// <summary>The app can provide DXBC shaders (boolean).</summary>
    public const string ShadersDxbc = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_SHADERS_DXBC_BOOLEAN;
    /// <summary>The app can provide DXIL shaders (boolean).</summary>
    public const string ShadersDxil = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_SHADERS_DXIL_BOOLEAN;
    /// <summary>The app can provide MSL shaders (boolean).</summary>
    public const string ShadersMsl = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_SHADERS_MSL_BOOLEAN;
    /// <summary>The app can provide Metal library shaders (boolean).</summary>
    public const string ShadersMetallib = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_SHADERS_METALLIB_BOOLEAN;
    /// <summary>Allow D3D12 tier-1 resource binding (boolean).</summary>
    public const string D3D12AllowFewerResourceSlots = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_D3D12_ALLOW_FEWER_RESOURCE_SLOTS_BOOLEAN;
    /// <summary>The D3D12 semantic name prefix (string).</summary>
    public const string D3D12SemanticName = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_D3D12_SEMANTIC_NAME_STRING;
    /// <summary>The D3D12 Agility SDK version (number).</summary>
    public const string D3D12AgilitySdkVersion = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_D3D12_AGILITY_SDK_VERSION_NUMBER;
    /// <summary>The D3D12 Agility SDK path (string).</summary>
    public const string D3D12AgilitySdkPath = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_D3D12_AGILITY_SDK_PATH_STRING;
    /// <summary>Require Vulkan hardware acceleration (boolean).</summary>
    public const string VulkanRequireHardwareAcceleration = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_VULKAN_REQUIRE_HARDWARE_ACCELERATION_BOOLEAN;
    /// <summary>A pointer to SDL_GPUVulkanOptions (pointer).</summary>
    public const string VulkanOptions = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_VULKAN_OPTIONS_POINTER;
    /// <summary>Allow the Metal Mac family 1 GPU tier (boolean).</summary>
    public const string MetalAllowMacFamily1 = Native.Gpu.SDL_PROP_GPU_DEVICE_CREATE_METAL_ALLOW_MACFAMILY1_BOOLEAN;
}
