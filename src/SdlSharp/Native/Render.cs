using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred:
// SDL_ConvertEventToRenderCoordinates (needs SDL_Event — Phase 3),
// SDL_GetRenderMetalLayer, SDL_GetRenderMetalCommandEncoder (Metal-specific),
// SDL_AddVulkanRenderSemaphores (Vulkan-specific),
// SDL_RenderDebugTextFormat (printf-style variadic — use SDL_RenderDebugText instead; C# formatting is done on the managed side),
// SDL_SetRenderGPUState (GPU renderer-specific).

/// <summary>
/// The texture address mode for texture coordinates.
/// </summary>
public enum SDL_TextureAddressMode
{
    /// <summary>Invalid address mode.</summary>
    SDL_TEXTURE_ADDRESS_INVALID = -1,
    /// <summary>Wrapping is enabled if texture coordinates are outside [0, 1], this is the default.</summary>
    SDL_TEXTURE_ADDRESS_AUTO,
    /// <summary>Texture coordinates are clamped to the [0, 1] range.</summary>
    SDL_TEXTURE_ADDRESS_CLAMP,
    /// <summary>The texture is repeated (tiled).</summary>
    SDL_TEXTURE_ADDRESS_WRAP,
}

/// <summary>
/// The access pattern allowed for a texture.
/// </summary>
public enum SDL_TextureAccess
{
    /// <summary>Changes rarely, not lockable.</summary>
    SDL_TEXTUREACCESS_STATIC = 0,
    /// <summary>Changes frequently, lockable.</summary>
    SDL_TEXTUREACCESS_STREAMING = 1,
    /// <summary>Texture can be used as a render target.</summary>
    SDL_TEXTUREACCESS_TARGET = 2,
}

/// <summary>
/// How the logical size is mapped to the output.
/// </summary>
public enum SDL_RendererLogicalPresentation
{
    /// <summary>There is no logical size in effect.</summary>
    SDL_LOGICAL_PRESENTATION_DISABLED = 0,
    /// <summary>The rendered content is stretched to the output resolution.</summary>
    SDL_LOGICAL_PRESENTATION_STRETCH = 1,
    /// <summary>The rendered content is fit to the largest dimension and letterboxed.</summary>
    SDL_LOGICAL_PRESENTATION_LETTERBOX = 2,
    /// <summary>The rendered content is fit to the smallest dimension and extends beyond bounds.</summary>
    SDL_LOGICAL_PRESENTATION_OVERSCAN = 3,
    /// <summary>The rendered content is scaled up by integer multiples.</summary>
    SDL_LOGICAL_PRESENTATION_INTEGER_SCALE = 4,
}

/// <summary>
/// Opaque renderer handle.
/// </summary>
public struct SDL_Renderer;

/// <summary>
/// An efficient driver-specific representation of pixel data.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_Texture
{
    /// <summary>The format of the texture, read-only.</summary>
    public SDL_PixelFormat format;
    /// <summary>The width of the texture, read-only.</summary>
    public int w;
    /// <summary>The height of the texture, read-only.</summary>
    public int h;
    /// <summary>Application reference count.</summary>
    public int refcount;
}

/// <summary>
/// Vertex structure for geometry rendering.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SDL_Vertex
{
    /// <summary>Vertex position, in SDL_Renderer coordinates.</summary>
    public SDL_FPoint position;
    /// <summary>Vertex color.</summary>
    public SDL_FColor color;
    /// <summary>Normalized texture coordinates.</summary>
    public SDL_FPoint tex_coord;
}

/// <summary>
/// Native bindings for SDL_render.h — 2D rendering functions.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Render
{
    /// <summary>The name of the software renderer.</summary>
    public static ReadOnlySpan<byte> SDL_SOFTWARE_RENDERER => "software"u8;

    /// <summary>The name of the GPU renderer.</summary>
    public static ReadOnlySpan<byte> SDL_GPU_RENDERER => "gpu"u8;

    // --- Renderer create property names ---

    /// <summary>The name of the rendering driver to create.</summary>
    public const string SDL_PROP_RENDERER_CREATE_NAME_STRING = "SDL.renderer.create.name";
    /// <summary>The window where rendering is displayed.</summary>
    public const string SDL_PROP_RENDERER_CREATE_WINDOW_POINTER = "SDL.renderer.create.window";
    /// <summary>The surface where rendering is displayed.</summary>
    public const string SDL_PROP_RENDERER_CREATE_SURFACE_POINTER = "SDL.renderer.create.surface";
    /// <summary>An SDL_Colorspace value for the output colorspace.</summary>
    public const string SDL_PROP_RENDERER_CREATE_OUTPUT_COLORSPACE_NUMBER = "SDL.renderer.create.output_colorspace";
    /// <summary>Non-zero if you want present synchronized with the refresh rate.</summary>
    public const string SDL_PROP_RENDERER_CREATE_PRESENT_VSYNC_NUMBER = "SDL.renderer.create.present_vsync";
    /// <summary>The GPU device to use for rendering.</summary>
    public const string SDL_PROP_RENDERER_CREATE_GPU_DEVICE_POINTER = "SDL.renderer.create.gpu.device";
    /// <summary>The app is able to provide SPIR-V shaders.</summary>
    public const string SDL_PROP_RENDERER_CREATE_GPU_SHADERS_SPIRV_BOOLEAN = "SDL.renderer.create.gpu.shaders_spirv";
    /// <summary>The app is able to provide DXIL shaders.</summary>
    public const string SDL_PROP_RENDERER_CREATE_GPU_SHADERS_DXIL_BOOLEAN = "SDL.renderer.create.gpu.shaders_dxil";
    /// <summary>The app is able to provide MSL shaders.</summary>
    public const string SDL_PROP_RENDERER_CREATE_GPU_SHADERS_MSL_BOOLEAN = "SDL.renderer.create.gpu.shaders_msl";
    /// <summary>The VkInstance to use with the renderer.</summary>
    public const string SDL_PROP_RENDERER_CREATE_VULKAN_INSTANCE_POINTER = "SDL.renderer.create.vulkan.instance";
    /// <summary>The VkSurfaceKHR to use with the renderer.</summary>
    public const string SDL_PROP_RENDERER_CREATE_VULKAN_SURFACE_NUMBER = "SDL.renderer.create.vulkan.surface";
    /// <summary>The VkPhysicalDevice to use with the renderer.</summary>
    public const string SDL_PROP_RENDERER_CREATE_VULKAN_PHYSICAL_DEVICE_POINTER = "SDL.renderer.create.vulkan.physical_device";
    /// <summary>The VkDevice to use with the renderer.</summary>
    public const string SDL_PROP_RENDERER_CREATE_VULKAN_DEVICE_POINTER = "SDL.renderer.create.vulkan.device";
    /// <summary>The queue family index for graphics operations.</summary>
    public const string SDL_PROP_RENDERER_CREATE_VULKAN_GRAPHICS_QUEUE_FAMILY_INDEX_NUMBER = "SDL.renderer.create.vulkan.graphics_queue_family_index";
    /// <summary>The queue family index for present operations.</summary>
    public const string SDL_PROP_RENDERER_CREATE_VULKAN_PRESENT_QUEUE_FAMILY_INDEX_NUMBER = "SDL.renderer.create.vulkan.present_queue_family_index";

    // --- Renderer property names ---

    /// <summary>The name of the rendering driver.</summary>
    public const string SDL_PROP_RENDERER_NAME_STRING = "SDL.renderer.name";
    /// <summary>The window where rendering is displayed.</summary>
    public const string SDL_PROP_RENDERER_WINDOW_POINTER = "SDL.renderer.window";
    /// <summary>The surface where rendering is displayed.</summary>
    public const string SDL_PROP_RENDERER_SURFACE_POINTER = "SDL.renderer.surface";
    /// <summary>The current VSync setting.</summary>
    public const string SDL_PROP_RENDERER_VSYNC_NUMBER = "SDL.renderer.vsync";
    /// <summary>The maximum texture size.</summary>
    public const string SDL_PROP_RENDERER_MAX_TEXTURE_SIZE_NUMBER = "SDL.renderer.max_texture_size";
    /// <summary>A pointer to the array of supported texture formats.</summary>
    public const string SDL_PROP_RENDERER_TEXTURE_FORMATS_POINTER = "SDL.renderer.texture_formats";
    /// <summary>Whether the renderer supports texture wrapping.</summary>
    public const string SDL_PROP_RENDERER_TEXTURE_WRAPPING_BOOLEAN = "SDL.renderer.texture_wrapping";
    /// <summary>An SDL_Colorspace value for the output colorspace.</summary>
    public const string SDL_PROP_RENDERER_OUTPUT_COLORSPACE_NUMBER = "SDL.renderer.output_colorspace";
    /// <summary>Whether HDR is enabled.</summary>
    public const string SDL_PROP_RENDERER_HDR_ENABLED_BOOLEAN = "SDL.renderer.HDR_enabled";
    /// <summary>The SDR white point value.</summary>
    public const string SDL_PROP_RENDERER_SDR_WHITE_POINT_FLOAT = "SDL.renderer.SDR_white_point";
    /// <summary>The HDR headroom value.</summary>
    public const string SDL_PROP_RENDERER_HDR_HEADROOM_FLOAT = "SDL.renderer.HDR_headroom";
    /// <summary>The D3D9 device pointer.</summary>
    public const string SDL_PROP_RENDERER_D3D9_DEVICE_POINTER = "SDL.renderer.d3d9.device";
    /// <summary>The D3D11 device pointer.</summary>
    public const string SDL_PROP_RENDERER_D3D11_DEVICE_POINTER = "SDL.renderer.d3d11.device";
    /// <summary>The D3D11 swap chain pointer.</summary>
    public const string SDL_PROP_RENDERER_D3D11_SWAPCHAIN_POINTER = "SDL.renderer.d3d11.swap_chain";
    /// <summary>The D3D12 device pointer.</summary>
    public const string SDL_PROP_RENDERER_D3D12_DEVICE_POINTER = "SDL.renderer.d3d12.device";
    /// <summary>The D3D12 swap chain pointer.</summary>
    public const string SDL_PROP_RENDERER_D3D12_SWAPCHAIN_POINTER = "SDL.renderer.d3d12.swap_chain";
    /// <summary>The D3D12 command queue pointer.</summary>
    public const string SDL_PROP_RENDERER_D3D12_COMMAND_QUEUE_POINTER = "SDL.renderer.d3d12.command_queue";
    /// <summary>The Vulkan instance pointer.</summary>
    public const string SDL_PROP_RENDERER_VULKAN_INSTANCE_POINTER = "SDL.renderer.vulkan.instance";
    /// <summary>The Vulkan surface number.</summary>
    public const string SDL_PROP_RENDERER_VULKAN_SURFACE_NUMBER = "SDL.renderer.vulkan.surface";
    /// <summary>The Vulkan physical device pointer.</summary>
    public const string SDL_PROP_RENDERER_VULKAN_PHYSICAL_DEVICE_POINTER = "SDL.renderer.vulkan.physical_device";
    /// <summary>The Vulkan device pointer.</summary>
    public const string SDL_PROP_RENDERER_VULKAN_DEVICE_POINTER = "SDL.renderer.vulkan.device";
    /// <summary>The Vulkan graphics queue family index.</summary>
    public const string SDL_PROP_RENDERER_VULKAN_GRAPHICS_QUEUE_FAMILY_INDEX_NUMBER = "SDL.renderer.vulkan.graphics_queue_family_index";
    /// <summary>The Vulkan present queue family index.</summary>
    public const string SDL_PROP_RENDERER_VULKAN_PRESENT_QUEUE_FAMILY_INDEX_NUMBER = "SDL.renderer.vulkan.present_queue_family_index";
    /// <summary>The Vulkan swapchain image count.</summary>
    public const string SDL_PROP_RENDERER_VULKAN_SWAPCHAIN_IMAGE_COUNT_NUMBER = "SDL.renderer.vulkan.swapchain_image_count";
    /// <summary>The GPU device pointer.</summary>
    public const string SDL_PROP_RENDERER_GPU_DEVICE_POINTER = "SDL.renderer.gpu.device";

    // --- Texture create property names ---

    /// <summary>The colorspace for the texture.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_COLORSPACE_NUMBER = "SDL.texture.create.colorspace";
    /// <summary>The pixel format for the texture.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_FORMAT_NUMBER = "SDL.texture.create.format";
    /// <summary>The access pattern for the texture.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_ACCESS_NUMBER = "SDL.texture.create.access";
    /// <summary>The width of the texture.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_WIDTH_NUMBER = "SDL.texture.create.width";
    /// <summary>The height of the texture.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_HEIGHT_NUMBER = "SDL.texture.create.height";
    /// <summary>The palette for the texture.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_PALETTE_POINTER = "SDL.texture.create.palette";
    /// <summary>The SDR white point for the texture.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_SDR_WHITE_POINT_FLOAT = "SDL.texture.create.SDR_white_point";
    /// <summary>The HDR headroom for the texture.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_HDR_HEADROOM_FLOAT = "SDL.texture.create.HDR_headroom";
    /// <summary>The D3D11 texture pointer.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_POINTER = "SDL.texture.create.d3d11.texture";
    /// <summary>The D3D11 texture U pointer.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_U_POINTER = "SDL.texture.create.d3d11.texture_u";
    /// <summary>The D3D11 texture V pointer.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_V_POINTER = "SDL.texture.create.d3d11.texture_v";
    /// <summary>The D3D12 texture pointer.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_D3D12_TEXTURE_POINTER = "SDL.texture.create.d3d12.texture";
    /// <summary>The D3D12 texture U pointer.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_D3D12_TEXTURE_U_POINTER = "SDL.texture.create.d3d12.texture_u";
    /// <summary>The D3D12 texture V pointer.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_D3D12_TEXTURE_V_POINTER = "SDL.texture.create.d3d12.texture_v";
    /// <summary>The Metal pixel buffer pointer.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_METAL_PIXELBUFFER_POINTER = "SDL.texture.create.metal.pixelbuffer";
    /// <summary>The OpenGL texture number.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_NUMBER = "SDL.texture.create.opengl.texture";
    /// <summary>The OpenGL texture UV number.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_UV_NUMBER = "SDL.texture.create.opengl.texture_uv";
    /// <summary>The OpenGL texture U number.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_U_NUMBER = "SDL.texture.create.opengl.texture_u";
    /// <summary>The OpenGL texture V number.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_V_NUMBER = "SDL.texture.create.opengl.texture_v";
    /// <summary>The OpenGL ES 2 texture number.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_NUMBER = "SDL.texture.create.opengles2.texture";
    /// <summary>The OpenGL ES 2 texture UV number.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_UV_NUMBER = "SDL.texture.create.opengles2.texture_uv";
    /// <summary>The OpenGL ES 2 texture U number.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_U_NUMBER = "SDL.texture.create.opengles2.texture_u";
    /// <summary>The OpenGL ES 2 texture V number.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_V_NUMBER = "SDL.texture.create.opengles2.texture_v";
    /// <summary>The Vulkan texture number.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_VULKAN_TEXTURE_NUMBER = "SDL.texture.create.vulkan.texture";
    /// <summary>The Vulkan texture layout number.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_VULKAN_LAYOUT_NUMBER = "SDL.texture.create.vulkan.layout";
    /// <summary>The GPU texture pointer.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_GPU_TEXTURE_POINTER = "SDL.texture.create.gpu.texture";
    /// <summary>The GPU texture UV pointer.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_GPU_TEXTURE_UV_POINTER = "SDL.texture.create.gpu.texture_uv";
    /// <summary>The GPU texture U pointer.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_GPU_TEXTURE_U_POINTER = "SDL.texture.create.gpu.texture_u";
    /// <summary>The GPU texture V pointer.</summary>
    public const string SDL_PROP_TEXTURE_CREATE_GPU_TEXTURE_V_POINTER = "SDL.texture.create.gpu.texture_v";

    // --- Texture property names ---

    /// <summary>The colorspace of the texture.</summary>
    public const string SDL_PROP_TEXTURE_COLORSPACE_NUMBER = "SDL.texture.colorspace";
    /// <summary>The pixel format of the texture.</summary>
    public const string SDL_PROP_TEXTURE_FORMAT_NUMBER = "SDL.texture.format";
    /// <summary>The access pattern of the texture.</summary>
    public const string SDL_PROP_TEXTURE_ACCESS_NUMBER = "SDL.texture.access";
    /// <summary>The width of the texture.</summary>
    public const string SDL_PROP_TEXTURE_WIDTH_NUMBER = "SDL.texture.width";
    /// <summary>The height of the texture.</summary>
    public const string SDL_PROP_TEXTURE_HEIGHT_NUMBER = "SDL.texture.height";
    /// <summary>The SDR white point of the texture.</summary>
    public const string SDL_PROP_TEXTURE_SDR_WHITE_POINT_FLOAT = "SDL.texture.SDR_white_point";
    /// <summary>The HDR headroom of the texture.</summary>
    public const string SDL_PROP_TEXTURE_HDR_HEADROOM_FLOAT = "SDL.texture.HDR_headroom";
    /// <summary>The D3D11 texture pointer.</summary>
    public const string SDL_PROP_TEXTURE_D3D11_TEXTURE_POINTER = "SDL.texture.d3d11.texture";
    /// <summary>The D3D11 texture U pointer.</summary>
    public const string SDL_PROP_TEXTURE_D3D11_TEXTURE_U_POINTER = "SDL.texture.d3d11.texture_u";
    /// <summary>The D3D11 texture V pointer.</summary>
    public const string SDL_PROP_TEXTURE_D3D11_TEXTURE_V_POINTER = "SDL.texture.d3d11.texture_v";
    /// <summary>The D3D12 texture pointer.</summary>
    public const string SDL_PROP_TEXTURE_D3D12_TEXTURE_POINTER = "SDL.texture.d3d12.texture";
    /// <summary>The D3D12 texture U pointer.</summary>
    public const string SDL_PROP_TEXTURE_D3D12_TEXTURE_U_POINTER = "SDL.texture.d3d12.texture_u";
    /// <summary>The D3D12 texture V pointer.</summary>
    public const string SDL_PROP_TEXTURE_D3D12_TEXTURE_V_POINTER = "SDL.texture.d3d12.texture_v";
    /// <summary>The OpenGL texture number.</summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_NUMBER = "SDL.texture.opengl.texture";
    /// <summary>The OpenGL texture UV number.</summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_UV_NUMBER = "SDL.texture.opengl.texture_uv";
    /// <summary>The OpenGL texture U number.</summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_U_NUMBER = "SDL.texture.opengl.texture_u";
    /// <summary>The OpenGL texture V number.</summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_V_NUMBER = "SDL.texture.opengl.texture_v";
    /// <summary>The OpenGL texture target number.</summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_TARGET_NUMBER = "SDL.texture.opengl.target";
    /// <summary>The OpenGL texture width as float.</summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEX_W_FLOAT = "SDL.texture.opengl.tex_w";
    /// <summary>The OpenGL texture height as float.</summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEX_H_FLOAT = "SDL.texture.opengl.tex_h";
    /// <summary>The OpenGL ES 2 texture number.</summary>
    public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_NUMBER = "SDL.texture.opengles2.texture";
    /// <summary>The OpenGL ES 2 texture UV number.</summary>
    public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_UV_NUMBER = "SDL.texture.opengles2.texture_uv";
    /// <summary>The OpenGL ES 2 texture U number.</summary>
    public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_U_NUMBER = "SDL.texture.opengles2.texture_u";
    /// <summary>The OpenGL ES 2 texture V number.</summary>
    public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_V_NUMBER = "SDL.texture.opengles2.texture_v";
    /// <summary>The OpenGL ES 2 texture target number.</summary>
    public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_TARGET_NUMBER = "SDL.texture.opengles2.target";
    /// <summary>The Vulkan texture number.</summary>
    public const string SDL_PROP_TEXTURE_VULKAN_TEXTURE_NUMBER = "SDL.texture.vulkan.texture";
    /// <summary>The GPU texture pointer.</summary>
    public const string SDL_PROP_TEXTURE_GPU_TEXTURE_POINTER = "SDL.texture.gpu.texture";
    /// <summary>The GPU texture UV pointer.</summary>
    public const string SDL_PROP_TEXTURE_GPU_TEXTURE_UV_POINTER = "SDL.texture.gpu.texture_uv";
    /// <summary>The GPU texture U pointer.</summary>
    public const string SDL_PROP_TEXTURE_GPU_TEXTURE_U_POINTER = "SDL.texture.gpu.texture_u";
    /// <summary>The GPU texture V pointer.</summary>
    public const string SDL_PROP_TEXTURE_GPU_TEXTURE_V_POINTER = "SDL.texture.gpu.texture_v";

    // --- Renderer lifecycle ---

    /// <summary>
    /// Get the number of 2D rendering drivers available for the current display.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumRenderDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumRenderDrivers();

    /// <summary>
    /// Get the name of a built-in 2D rendering driver.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetRenderDriver(int index);

    /// <summary>
    /// Create a 2D rendering context for a window, using properties to specify options.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateRendererWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Renderer* SDL_CreateRendererWithProperties(SDL_PropertiesID props);

    /// <summary>
    /// Create a 2D rendering context for a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Renderer* SDL_CreateRenderer(SDL_Window* window, ReadOnlySpan<byte> name);

    /// <summary>
    /// Create a window and default renderer.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateWindowAndRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_CreateWindowAndRenderer(
        ReadOnlySpan<byte> title, int width, int height, SDL_WindowFlags flags,
        out SDL_Window* window, out SDL_Renderer* renderer);

    /// <summary>
    /// Destroy the rendering context for a window and free all associated textures.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DestroyRenderer(SDL_Renderer* renderer);

    /// <summary>
    /// Get the name of a renderer.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRendererName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetRendererName(SDL_Renderer* renderer);

    /// <summary>
    /// Get the properties associated with a renderer.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRendererProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PropertiesID SDL_GetRendererProperties(SDL_Renderer* renderer);

    /// <summary>
    /// Get the window associated with a renderer.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window* SDL_GetRenderWindow(SDL_Renderer* renderer);

    /// <summary>
    /// Get the renderer associated with a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Renderer* SDL_GetRenderer(SDL_Window* window);

    /// <summary>
    /// Get the output size in pixels of a rendering context.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderOutputSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRenderOutputSize(SDL_Renderer* renderer, out int w, out int h);

    /// <summary>
    /// Get the current output size in pixels of a rendering context.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCurrentRenderOutputSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetCurrentRenderOutputSize(SDL_Renderer* renderer, out int w, out int h);

    // --- Texture ---

    /// <summary>
    /// Create a texture for a rendering context.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Texture* SDL_CreateTexture(SDL_Renderer* renderer, SDL_PixelFormat format, SDL_TextureAccess access, int w, int h);

    /// <summary>
    /// Create a texture from an existing surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateTextureFromSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Texture* SDL_CreateTextureFromSurface(SDL_Renderer* renderer, SDL_Surface* surface);

    /// <summary>
    /// Destroy the specified texture.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DestroyTexture(SDL_Texture* texture);

    /// <summary>
    /// Get the size of a texture, as floating point values.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTextureSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetTextureSize(SDL_Texture* texture, out float w, out float h);

    /// <summary>
    /// Set an additional color value multiplied into texture copy operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTextureColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetTextureColorMod(SDL_Texture* texture, byte r, byte g, byte b);

    /// <summary>
    /// Get the additional color value multiplied into texture copy operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTextureColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetTextureColorMod(SDL_Texture* texture, out byte r, out byte g, out byte b);

    /// <summary>
    /// Set an additional alpha value multiplied into texture copy operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTextureAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetTextureAlphaMod(SDL_Texture* texture, byte alpha);

    /// <summary>
    /// Get the additional alpha value multiplied into texture copy operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTextureAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetTextureAlphaMod(SDL_Texture* texture, out byte alpha);

    /// <summary>
    /// Set the blend mode for a texture, used by SDL_RenderTexture.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTextureBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetTextureBlendMode(SDL_Texture* texture, SDL_BlendMode blendMode);

    /// <summary>
    /// Get the blend mode used for texture copy operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTextureBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetTextureBlendMode(SDL_Texture* texture, out SDL_BlendMode blendMode);

    /// <summary>
    /// Set the scale mode used for texture scale operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetTextureScaleMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetTextureScaleMode(SDL_Texture* texture, SDL_ScaleMode scaleMode);

    /// <summary>
    /// Get the scale mode used for texture scale operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetTextureScaleMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetTextureScaleMode(SDL_Texture* texture, out SDL_ScaleMode scaleMode);

    /// <summary>
    /// Update the given texture rectangle with new pixel data.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UpdateTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_UpdateTexture(SDL_Texture* texture, SDL_Rect* rect, void* pixels, int pitch);

    /// <summary>
    /// Lock a portion of the texture for write-only pixel access.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_LockTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_LockTexture(SDL_Texture* texture, SDL_Rect* rect, out void* pixels, out int pitch);

    /// <summary>
    /// Unlock a texture, uploading the changes to video memory, if needed.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UnlockTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_UnlockTexture(SDL_Texture* texture);

    // --- Drawing state ---

    /// <summary>
    /// Set the color used for drawing operations (Rect, Line and Clear).
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetRenderDrawColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetRenderDrawColor(SDL_Renderer* renderer, byte r, byte g, byte b, byte a);

    /// <summary>
    /// Set the color used for drawing operations with floating point precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetRenderDrawColorFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetRenderDrawColorFloat(SDL_Renderer* renderer, float r, float g, float b, float a);

    /// <summary>
    /// Get the color used for drawing operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderDrawColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRenderDrawColor(SDL_Renderer* renderer, out byte r, out byte g, out byte b, out byte a);

    /// <summary>
    /// Get the color used for drawing operations with floating point precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderDrawColorFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRenderDrawColorFloat(SDL_Renderer* renderer, out float r, out float g, out float b, out float a);

    /// <summary>
    /// Set the blend mode used for drawing operations (Fill and Line).
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetRenderDrawBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetRenderDrawBlendMode(SDL_Renderer* renderer, SDL_BlendMode blendMode);

    /// <summary>
    /// Get the blend mode used for drawing operations.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderDrawBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRenderDrawBlendMode(SDL_Renderer* renderer, out SDL_BlendMode blendMode);

    // --- Drawing ---

    /// <summary>
    /// Clear the current rendering target with the drawing color.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderClear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderClear(SDL_Renderer* renderer);

    /// <summary>
    /// Draw a point on the current rendering target at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderPoint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderPoint(SDL_Renderer* renderer, float x, float y);

    /// <summary>
    /// Draw a series of points on the current rendering target at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderPoints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderPoints(SDL_Renderer* renderer, SDL_FPoint* points, int count);

    /// <summary>
    /// Draw a line on the current rendering target at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderLine(SDL_Renderer* renderer, float x1, float y1, float x2, float y2);

    /// <summary>
    /// Draw a series of connected lines on the current rendering target at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderLines")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderLines(SDL_Renderer* renderer, SDL_FPoint* points, int count);

    /// <summary>
    /// Draw a rectangle on the current rendering target at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderRect(SDL_Renderer* renderer, SDL_FRect* rect);

    /// <summary>
    /// Draw some number of rectangles on the current rendering target at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderRects(SDL_Renderer* renderer, SDL_FRect* rects, int count);

    /// <summary>
    /// Fill a rectangle on the current rendering target with the drawing color at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderFillRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderFillRect(SDL_Renderer* renderer, SDL_FRect* rect);

    /// <summary>
    /// Fill some number of rectangles on the current rendering target with the drawing color at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderFillRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderFillRects(SDL_Renderer* renderer, SDL_FRect* rects, int count);

    /// <summary>
    /// Copy a portion of the texture to the current rendering target at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderTexture(SDL_Renderer* renderer, SDL_Texture* texture, SDL_FRect* srcrect, SDL_FRect* dstrect);

    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with affine transform, at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderTextureAffine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderTextureAffine(SDL_Renderer* renderer, SDL_Texture* texture,
        SDL_FRect* srcrect, SDL_FPoint* origin, SDL_FPoint* right, SDL_FPoint* down);

    /// <summary>
    /// Tile a portion of the texture to the current rendering target at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderTextureTiled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderTextureTiled(SDL_Renderer* renderer, SDL_Texture* texture,
        SDL_FRect* srcrect, float scale, SDL_FRect* dstrect);

    /// <summary>
    /// Perform a scaled 9-grid blit of a texture to the current rendering target at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderTexture9Grid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderTexture9Grid(SDL_Renderer* renderer, SDL_Texture* texture,
        SDL_FRect* srcrect, float left_width, float right_width, float top_height, float bottom_height,
        float scale, SDL_FRect* dstrect);

    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with rotation and flipping, at subpixel precision.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderTextureRotated")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderTextureRotated(SDL_Renderer* renderer, SDL_Texture* texture,
        SDL_FRect* srcrect, SDL_FRect* dstrect, double angle, SDL_FPoint* center, SDL_FlipMode flip);

    /// <summary>
    /// Update the screen with any rendering performed since the previous call.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderPresent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderPresent(SDL_Renderer* renderer);

    /// <summary>
    /// Draw debug text to an SDL_Renderer.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderDebugText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderDebugText(SDL_Renderer* renderer, float x, float y, ReadOnlySpan<byte> str);

    // --- Geometry ---

    /// <summary>
    /// Render a list of triangles, optionally using a texture and indices into the vertex array.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderGeometry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderGeometry(SDL_Renderer* renderer, SDL_Texture* texture,
        SDL_Vertex* vertices, int num_vertices, int* indices, int num_indices);

    /// <summary>
    /// Render a list of triangles, optionally using a texture and indices into the vertex arrays.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderGeometryRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderGeometryRaw(SDL_Renderer* renderer, SDL_Texture* texture,
        float* xy, int xy_stride, SDL_FColor* color, int color_stride, float* uv, int uv_stride,
        int num_vertices, void* indices, int num_indices, int size_indices);

    // --- Read pixels / flush ---

    /// <summary>
    /// Read pixels from the current rendering target to a new surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderReadPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_RenderReadPixels(SDL_Renderer* renderer, SDL_Rect* rect);

    /// <summary>
    /// Force the rendering context to flush any pending commands and state to the underlying rendering API.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_FlushRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_FlushRenderer(SDL_Renderer* renderer);

    // --- Coordinate conversion ---

    /// <summary>
    /// Get the point in render coordinates associated with window coordinates.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderCoordinatesFromWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderCoordinatesFromWindow(SDL_Renderer* renderer, float window_x, float window_y, out float x, out float y);

    /// <summary>
    /// Get the window coordinates associated with render coordinates.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderCoordinatesToWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderCoordinatesToWindow(SDL_Renderer* renderer, float x, float y, out float window_x, out float window_y);

    // --- Viewport / clipping ---

    /// <summary>
    /// Set the drawing area for rendering on the current target.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetRenderViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetRenderViewport(SDL_Renderer* renderer, SDL_Rect* rect);

    /// <summary>
    /// Get the drawing area for the current target.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRenderViewport(SDL_Renderer* renderer, out SDL_Rect rect);

    /// <summary>
    /// Set the clip rectangle for rendering on the specified target.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetRenderClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetRenderClipRect(SDL_Renderer* renderer, SDL_Rect* rect);

    /// <summary>
    /// Get the clip rectangle for the current target.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRenderClipRect(SDL_Renderer* renderer, out SDL_Rect rect);

    /// <summary>
    /// Get whether clipping is enabled on the given renderer.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RenderClipEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RenderClipEnabled(SDL_Renderer* renderer);

    // --- Scale / target / logical / vsync ---

    /// <summary>
    /// Set the drawing scale for rendering on the current target.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetRenderScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetRenderScale(SDL_Renderer* renderer, float scaleX, float scaleY);

    /// <summary>
    /// Get the drawing scale for the current target.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRenderScale(SDL_Renderer* renderer, out float scaleX, out float scaleY);

    /// <summary>
    /// Set a texture as the current rendering target.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetRenderTarget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetRenderTarget(SDL_Renderer* renderer, SDL_Texture* texture);

    /// <summary>
    /// Get the current render target.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderTarget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Texture* SDL_GetRenderTarget(SDL_Renderer* renderer);

    /// <summary>
    /// Set a device independent resolution and presentation mode for rendering.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetRenderLogicalPresentation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetRenderLogicalPresentation(SDL_Renderer* renderer, int w, int h, SDL_RendererLogicalPresentation mode);

    /// <summary>
    /// Get device independent resolution and presentation mode for rendering.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderLogicalPresentation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRenderLogicalPresentation(SDL_Renderer* renderer, out int w, out int h, out SDL_RendererLogicalPresentation mode);

    /// <summary>
    /// Toggle VSync of the given renderer.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetRenderVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetRenderVSync(SDL_Renderer* renderer, int vsync);

    /// <summary>
    /// Get VSync of the given renderer.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRenderVSync(SDL_Renderer* renderer, out int vsync);

    /// <summary>
    /// Set the texture address mode used for rendering.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetRenderTextureAddressMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetRenderTextureAddressMode(SDL_Renderer* renderer, SDL_TextureAddressMode u_mode, SDL_TextureAddressMode v_mode);

    /// <summary>
    /// Get the texture address mode used for rendering.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRenderTextureAddressMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetRenderTextureAddressMode(SDL_Renderer* renderer, out SDL_TextureAddressMode u_mode, out SDL_TextureAddressMode v_mode);
}
