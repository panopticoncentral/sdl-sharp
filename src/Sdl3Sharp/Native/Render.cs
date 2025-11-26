using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.BlendMode;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Events;
using static Sdl3Sharp.Native.Pixels;
using static Sdl3Sharp.Native.Properties;
using static Sdl3Sharp.Native.Rect;
using static Sdl3Sharp.Native.Surface;
using static Sdl3Sharp.Native.Video;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_render.h - 2D rendering functions for hardware-accelerated graphics.
/// </summary>
public static unsafe partial class Render
{
    /// <summary>
    /// The name of the software renderer.
    /// </summary>
    public const string SDL_SOFTWARE_RENDERER = "software";

    /// <summary>
    /// Synchronize immediately (no vsync).
    /// </summary>
    public const int SDL_RENDERER_VSYNC_DISABLED = 0;

    /// <summary>
    /// Synchronized with the vertical retrace, adaptive vsync if available.
    /// </summary>
    public const int SDL_RENDERER_VSYNC_ADAPTIVE = -1;

    /// <summary>
    /// The character size of the debug text font.
    /// </summary>
    public const int SDL_DEBUG_TEXT_FONT_CHARACTER_SIZE = 8;

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

        /// <summary>Normalized texture coordinates, if needed.</summary>
        public SDL_FPoint tex_coord;
    }

    /// <summary>
    /// The access pattern allowed for a texture.
    /// </summary>
    public enum SDL_TextureAccess
    {
        /// <summary>Changes rarely, not lockable.</summary>
        SDL_TEXTUREACCESS_STATIC,

        /// <summary>Changes frequently, lockable.</summary>
        SDL_TEXTUREACCESS_STREAMING,

        /// <summary>Texture can be used as a render target.</summary>
        SDL_TEXTUREACCESS_TARGET
    }

    /// <summary>
    /// How the logical size is mapped to the output.
    /// </summary>
    public enum SDL_RendererLogicalPresentation
    {
        /// <summary>There is no logical size in effect.</summary>
        SDL_LOGICAL_PRESENTATION_DISABLED,

        /// <summary>The rendered content is stretched to the output resolution.</summary>
        SDL_LOGICAL_PRESENTATION_STRETCH,

        /// <summary>The rendered content is fit to the largest dimension and the other dimension is letterboxed with black bars.</summary>
        SDL_LOGICAL_PRESENTATION_LETTERBOX,

        /// <summary>The rendered content is fit to the smallest dimension and the other dimension extends beyond the output bounds.</summary>
        SDL_LOGICAL_PRESENTATION_OVERSCAN,

        /// <summary>The rendered content is scaled up by integer multiples to fit the output resolution.</summary>
        SDL_LOGICAL_PRESENTATION_INTEGER_SCALE
    }

    /// <summary>
    /// A structure representing rendering state (opaque).
    /// </summary>
    public struct SDL_Renderer { }

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

        /// <summary>Application reference count, used when freeing texture.</summary>
        public int refcount;
    }

    // SDL_Event is defined in Events.cs

    /// <summary>
    /// Property name: the name of the rendering driver for renderer creation.
    /// </summary>
    public const string SDL_PROP_RENDERER_CREATE_NAME_STRING = "SDL.renderer.create.name";

    /// <summary>
    /// Property name: the window where rendering is displayed for renderer creation.
    /// </summary>
    public const string SDL_PROP_RENDERER_CREATE_WINDOW_POINTER = "SDL.renderer.create.window";

    /// <summary>
    /// Property name: the surface where rendering is displayed for software renderer creation.
    /// </summary>
    public const string SDL_PROP_RENDERER_CREATE_SURFACE_POINTER = "SDL.renderer.create.surface";

    /// <summary>
    /// Property name: an SDL_Colorspace value describing the colorspace for output to the display.
    /// </summary>
    public const string SDL_PROP_RENDERER_CREATE_OUTPUT_COLORSPACE_NUMBER = "SDL.renderer.create.output_colorspace";

    /// <summary>
    /// Property name: non-zero if you want present synchronized with the refresh rate.
    /// </summary>
    public const string SDL_PROP_RENDERER_CREATE_PRESENT_VSYNC_NUMBER = "SDL.renderer.create.present_vsync";

    /// <summary>
    /// Property name: the VkInstance to use with the vulkan renderer.
    /// </summary>
    public const string SDL_PROP_RENDERER_CREATE_VULKAN_INSTANCE_POINTER = "SDL.renderer.create.vulkan.instance";

    /// <summary>
    /// Property name: the VkSurfaceKHR to use with the vulkan renderer.
    /// </summary>
    public const string SDL_PROP_RENDERER_CREATE_VULKAN_SURFACE_NUMBER = "SDL.renderer.create.vulkan.surface";

    /// <summary>
    /// Property name: the VkPhysicalDevice to use with the vulkan renderer.
    /// </summary>
    public const string SDL_PROP_RENDERER_CREATE_VULKAN_PHYSICAL_DEVICE_POINTER = "SDL.renderer.create.vulkan.physical_device";

    /// <summary>
    /// Property name: the VkDevice to use with the vulkan renderer.
    /// </summary>
    public const string SDL_PROP_RENDERER_CREATE_VULKAN_DEVICE_POINTER = "SDL.renderer.create.vulkan.device";

    /// <summary>
    /// Property name: the queue family index used for rendering with the vulkan renderer.
    /// </summary>
    public const string SDL_PROP_RENDERER_CREATE_VULKAN_GRAPHICS_QUEUE_FAMILY_INDEX_NUMBER = "SDL.renderer.create.vulkan.graphics_queue_family_index";

    /// <summary>
    /// Property name: the queue family index used for presentation with the vulkan renderer.
    /// </summary>
    public const string SDL_PROP_RENDERER_CREATE_VULKAN_PRESENT_QUEUE_FAMILY_INDEX_NUMBER = "SDL.renderer.create.vulkan.present_queue_family_index";

    /// <summary>
    /// Property name: the name of the rendering driver.
    /// </summary>
    public const string SDL_PROP_RENDERER_NAME_STRING = "SDL.renderer.name";

    /// <summary>
    /// Property name: the window where rendering is displayed.
    /// </summary>
    public const string SDL_PROP_RENDERER_WINDOW_POINTER = "SDL.renderer.window";

    /// <summary>
    /// Property name: the surface where rendering is displayed for software renderer.
    /// </summary>
    public const string SDL_PROP_RENDERER_SURFACE_POINTER = "SDL.renderer.surface";

    /// <summary>
    /// Property name: the current vsync setting.
    /// </summary>
    public const string SDL_PROP_RENDERER_VSYNC_NUMBER = "SDL.renderer.vsync";

    /// <summary>
    /// Property name: the maximum texture width and height.
    /// </summary>
    public const string SDL_PROP_RENDERER_MAX_TEXTURE_SIZE_NUMBER = "SDL.renderer.max_texture_size";

    /// <summary>
    /// Property name: a (const SDL_PixelFormat *) array of pixel formats, terminated with SDL_PIXELFORMAT_UNKNOWN.
    /// </summary>
    public const string SDL_PROP_RENDERER_TEXTURE_FORMATS_POINTER = "SDL.renderer.texture_formats";

    /// <summary>
    /// Property name: an SDL_Colorspace value describing the colorspace for output to the display.
    /// </summary>
    public const string SDL_PROP_RENDERER_OUTPUT_COLORSPACE_NUMBER = "SDL.renderer.output_colorspace";

    /// <summary>
    /// Property name: true if the output colorspace is SDL_COLORSPACE_SRGB_LINEAR and HDR is enabled.
    /// </summary>
    public const string SDL_PROP_RENDERER_HDR_ENABLED_BOOLEAN = "SDL.renderer.HDR_enabled";

    /// <summary>
    /// Property name: the value of SDR white in the SDL_COLORSPACE_SRGB_LINEAR colorspace.
    /// </summary>
    public const string SDL_PROP_RENDERER_SDR_WHITE_POINT_FLOAT = "SDL.renderer.SDR_white_point";

    /// <summary>
    /// Property name: the additional high dynamic range that can be displayed.
    /// </summary>
    public const string SDL_PROP_RENDERER_HDR_HEADROOM_FLOAT = "SDL.renderer.HDR_headroom";

    /// <summary>
    /// Property name: the IDirect3DDevice9 associated with the renderer (Direct3D 9).
    /// </summary>
    public const string SDL_PROP_RENDERER_D3D9_DEVICE_POINTER = "SDL.renderer.d3d9.device";

    /// <summary>
    /// Property name: the ID3D11Device associated with the renderer (Direct3D 11).
    /// </summary>
    public const string SDL_PROP_RENDERER_D3D11_DEVICE_POINTER = "SDL.renderer.d3d11.device";

    /// <summary>
    /// Property name: the IDXGISwapChain1 associated with the renderer (Direct3D 11).
    /// </summary>
    public const string SDL_PROP_RENDERER_D3D11_SWAPCHAIN_POINTER = "SDL.renderer.d3d11.swap_chain";

    /// <summary>
    /// Property name: the ID3D12Device associated with the renderer (Direct3D 12).
    /// </summary>
    public const string SDL_PROP_RENDERER_D3D12_DEVICE_POINTER = "SDL.renderer.d3d12.device";

    /// <summary>
    /// Property name: the IDXGISwapChain4 associated with the renderer (Direct3D 12).
    /// </summary>
    public const string SDL_PROP_RENDERER_D3D12_SWAPCHAIN_POINTER = "SDL.renderer.d3d12.swap_chain";

    /// <summary>
    /// Property name: the ID3D12CommandQueue associated with the renderer (Direct3D 12).
    /// </summary>
    public const string SDL_PROP_RENDERER_D3D12_COMMAND_QUEUE_POINTER = "SDL.renderer.d3d12.command_queue";

    /// <summary>
    /// Property name: the VkInstance associated with the renderer (Vulkan).
    /// </summary>
    public const string SDL_PROP_RENDERER_VULKAN_INSTANCE_POINTER = "SDL.renderer.vulkan.instance";

    /// <summary>
    /// Property name: the VkSurfaceKHR associated with the renderer (Vulkan).
    /// </summary>
    public const string SDL_PROP_RENDERER_VULKAN_SURFACE_NUMBER = "SDL.renderer.vulkan.surface";

    /// <summary>
    /// Property name: the VkPhysicalDevice associated with the renderer (Vulkan).
    /// </summary>
    public const string SDL_PROP_RENDERER_VULKAN_PHYSICAL_DEVICE_POINTER = "SDL.renderer.vulkan.physical_device";

    /// <summary>
    /// Property name: the VkDevice associated with the renderer (Vulkan).
    /// </summary>
    public const string SDL_PROP_RENDERER_VULKAN_DEVICE_POINTER = "SDL.renderer.vulkan.device";

    /// <summary>
    /// Property name: the queue family index used for rendering (Vulkan).
    /// </summary>
    public const string SDL_PROP_RENDERER_VULKAN_GRAPHICS_QUEUE_FAMILY_INDEX_NUMBER = "SDL.renderer.vulkan.graphics_queue_family_index";

    /// <summary>
    /// Property name: the queue family index used for presentation (Vulkan).
    /// </summary>
    public const string SDL_PROP_RENDERER_VULKAN_PRESENT_QUEUE_FAMILY_INDEX_NUMBER = "SDL.renderer.vulkan.present_queue_family_index";

    /// <summary>
    /// Property name: the number of swapchain images, or potential frames in flight (Vulkan).
    /// </summary>
    public const string SDL_PROP_RENDERER_VULKAN_SWAPCHAIN_IMAGE_COUNT_NUMBER = "SDL.renderer.vulkan.swapchain_image_count";

    /// <summary>
    /// Property name: the SDL_GPUDevice associated with the renderer (GPU renderer).
    /// </summary>
    public const string SDL_PROP_RENDERER_GPU_DEVICE_POINTER = "SDL.renderer.gpu.device";

    /// <summary>
    /// Property name: an SDL_Colorspace value describing the texture colorspace.
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_COLORSPACE_NUMBER = "SDL.texture.create.colorspace";

    /// <summary>
    /// Property name: one of the enumerated values in SDL_PixelFormat for texture creation.
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_FORMAT_NUMBER = "SDL.texture.create.format";

    /// <summary>
    /// Property name: one of the enumerated values in SDL_TextureAccess for texture creation.
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_ACCESS_NUMBER = "SDL.texture.create.access";

    /// <summary>
    /// Property name: the width of the texture in pixels for texture creation.
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_WIDTH_NUMBER = "SDL.texture.create.width";

    /// <summary>
    /// Property name: the height of the texture in pixels for texture creation.
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_HEIGHT_NUMBER = "SDL.texture.create.height";

    /// <summary>
    /// Property name: for HDR10 and floating point textures, the value of 100% diffuse white.
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_SDR_WHITE_POINT_FLOAT = "SDL.texture.create.SDR_white_point";

    /// <summary>
    /// Property name: for HDR10 and floating point textures, the maximum dynamic range.
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_HDR_HEADROOM_FLOAT = "SDL.texture.create.HDR_headroom";

    /// <summary>
    /// Property name: the ID3D11Texture2D associated with the texture (Direct3D 11).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_POINTER = "SDL.texture.create.d3d11.texture";

    /// <summary>
    /// Property name: the ID3D11Texture2D associated with the U plane of a YUV texture (Direct3D 11).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_U_POINTER = "SDL.texture.create.d3d11.texture_u";

    /// <summary>
    /// Property name: the ID3D11Texture2D associated with the V plane of a YUV texture (Direct3D 11).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_V_POINTER = "SDL.texture.create.d3d11.texture_v";

    /// <summary>
    /// Property name: the ID3D12Resource associated with the texture (Direct3D 12).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_D3D12_TEXTURE_POINTER = "SDL.texture.create.d3d12.texture";

    /// <summary>
    /// Property name: the ID3D12Resource associated with the U plane of a YUV texture (Direct3D 12).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_D3D12_TEXTURE_U_POINTER = "SDL.texture.create.d3d12.texture_u";

    /// <summary>
    /// Property name: the ID3D12Resource associated with the V plane of a YUV texture (Direct3D 12).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_D3D12_TEXTURE_V_POINTER = "SDL.texture.create.d3d12.texture_v";

    /// <summary>
    /// Property name: the CVPixelBufferRef associated with the texture (Metal).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_METAL_PIXELBUFFER_POINTER = "SDL.texture.create.metal.pixelbuffer";

    /// <summary>
    /// Property name: the GLuint texture associated with the texture (OpenGL).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_NUMBER = "SDL.texture.create.opengl.texture";

    /// <summary>
    /// Property name: the GLuint texture associated with the UV plane of an NV12 texture (OpenGL).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_UV_NUMBER = "SDL.texture.create.opengl.texture_uv";

    /// <summary>
    /// Property name: the GLuint texture associated with the U plane of a YUV texture (OpenGL).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_U_NUMBER = "SDL.texture.create.opengl.texture_u";

    /// <summary>
    /// Property name: the GLuint texture associated with the V plane of a YUV texture (OpenGL).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_V_NUMBER = "SDL.texture.create.opengl.texture_v";

    /// <summary>
    /// Property name: the GLuint texture associated with the texture (OpenGL ES 2).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_NUMBER = "SDL.texture.create.opengles2.texture";

    /// <summary>
    /// Property name: the GLuint texture associated with the UV plane of an NV12 texture (OpenGL ES 2).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_UV_NUMBER = "SDL.texture.create.opengles2.texture_uv";

    /// <summary>
    /// Property name: the GLuint texture associated with the U plane of a YUV texture (OpenGL ES 2).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_U_NUMBER = "SDL.texture.create.opengles2.texture_u";

    /// <summary>
    /// Property name: the GLuint texture associated with the V plane of a YUV texture (OpenGL ES 2).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_V_NUMBER = "SDL.texture.create.opengles2.texture_v";

    /// <summary>
    /// Property name: the VkImage with layout VK_IMAGE_LAYOUT_SHADER_READ_ONLY_OPTIMAL (Vulkan).
    /// </summary>
    public const string SDL_PROP_TEXTURE_CREATE_VULKAN_TEXTURE_NUMBER = "SDL.texture.create.vulkan.texture";

    /// <summary>
    /// Property name: an SDL_Colorspace value describing the texture colorspace.
    /// </summary>
    public const string SDL_PROP_TEXTURE_COLORSPACE_NUMBER = "SDL.texture.colorspace";

    /// <summary>
    /// Property name: one of the enumerated values in SDL_PixelFormat.
    /// </summary>
    public const string SDL_PROP_TEXTURE_FORMAT_NUMBER = "SDL.texture.format";

    /// <summary>
    /// Property name: one of the enumerated values in SDL_TextureAccess.
    /// </summary>
    public const string SDL_PROP_TEXTURE_ACCESS_NUMBER = "SDL.texture.access";

    /// <summary>
    /// Property name: the width of the texture in pixels.
    /// </summary>
    public const string SDL_PROP_TEXTURE_WIDTH_NUMBER = "SDL.texture.width";

    /// <summary>
    /// Property name: the height of the texture in pixels.
    /// </summary>
    public const string SDL_PROP_TEXTURE_HEIGHT_NUMBER = "SDL.texture.height";

    /// <summary>
    /// Property name: for HDR10 and floating point textures, the value of 100% diffuse white.
    /// </summary>
    public const string SDL_PROP_TEXTURE_SDR_WHITE_POINT_FLOAT = "SDL.texture.SDR_white_point";

    /// <summary>
    /// Property name: for HDR10 and floating point textures, the maximum dynamic range.
    /// </summary>
    public const string SDL_PROP_TEXTURE_HDR_HEADROOM_FLOAT = "SDL.texture.HDR_headroom";

    /// <summary>
    /// Property name: the ID3D11Texture2D associated with the texture (Direct3D 11).
    /// </summary>
    public const string SDL_PROP_TEXTURE_D3D11_TEXTURE_POINTER = "SDL.texture.d3d11.texture";

    /// <summary>
    /// Property name: the ID3D11Texture2D associated with the U plane of a YUV texture (Direct3D 11).
    /// </summary>
    public const string SDL_PROP_TEXTURE_D3D11_TEXTURE_U_POINTER = "SDL.texture.d3d11.texture_u";

    /// <summary>
    /// Property name: the ID3D11Texture2D associated with the V plane of a YUV texture (Direct3D 11).
    /// </summary>
    public const string SDL_PROP_TEXTURE_D3D11_TEXTURE_V_POINTER = "SDL.texture.d3d11.texture_v";

    /// <summary>
    /// Property name: the ID3D12Resource associated with the texture (Direct3D 12).
    /// </summary>
    public const string SDL_PROP_TEXTURE_D3D12_TEXTURE_POINTER = "SDL.texture.d3d12.texture";

    /// <summary>
    /// Property name: the ID3D12Resource associated with the U plane of a YUV texture (Direct3D 12).
    /// </summary>
    public const string SDL_PROP_TEXTURE_D3D12_TEXTURE_U_POINTER = "SDL.texture.d3d12.texture_u";

    /// <summary>
    /// Property name: the ID3D12Resource associated with the V plane of a YUV texture (Direct3D 12).
    /// </summary>
    public const string SDL_PROP_TEXTURE_D3D12_TEXTURE_V_POINTER = "SDL.texture.d3d12.texture_v";

    /// <summary>
    /// Property name: the GLuint texture associated with the texture (OpenGL).
    /// </summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_NUMBER = "SDL.texture.opengl.texture";

    /// <summary>
    /// Property name: the GLuint texture associated with the UV plane of an NV12 texture (OpenGL).
    /// </summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_UV_NUMBER = "SDL.texture.opengl.texture_uv";

    /// <summary>
    /// Property name: the GLuint texture associated with the U plane of a YUV texture (OpenGL).
    /// </summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_U_NUMBER = "SDL.texture.opengl.texture_u";

    /// <summary>
    /// Property name: the GLuint texture associated with the V plane of a YUV texture (OpenGL).
    /// </summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_V_NUMBER = "SDL.texture.opengl.texture_v";

    /// <summary>
    /// Property name: the GLenum for the texture target (OpenGL).
    /// </summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_TARGET_NUMBER = "SDL.texture.opengl.target";

    /// <summary>
    /// Property name: the texture coordinate width of the texture (OpenGL).
    /// </summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEX_W_FLOAT = "SDL.texture.opengl.tex_w";

    /// <summary>
    /// Property name: the texture coordinate height of the texture (OpenGL).
    /// </summary>
    public const string SDL_PROP_TEXTURE_OPENGL_TEX_H_FLOAT = "SDL.texture.opengl.tex_h";

    /// <summary>
    /// Property name: the GLuint texture associated with the texture (OpenGL ES 2).
    /// </summary>
    public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_NUMBER = "SDL.texture.opengles2.texture";

    /// <summary>
    /// Property name: the GLuint texture associated with the UV plane of an NV12 texture (OpenGL ES 2).
    /// </summary>
    public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_UV_NUMBER = "SDL.texture.opengles2.texture_uv";

    /// <summary>
    /// Property name: the GLuint texture associated with the U plane of a YUV texture (OpenGL ES 2).
    /// </summary>
    public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_U_NUMBER = "SDL.texture.opengles2.texture_u";

    /// <summary>
    /// Property name: the GLuint texture associated with the V plane of a YUV texture (OpenGL ES 2).
    /// </summary>
    public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_V_NUMBER = "SDL.texture.opengles2.texture_v";

    /// <summary>
    /// Property name: the GLenum for the texture target (OpenGL ES 2).
    /// </summary>
    public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_TARGET_NUMBER = "SDL.texture.opengles2.target";

    /// <summary>
    /// Property name: the VkImage associated with the texture (Vulkan).
    /// </summary>
    public const string SDL_PROP_TEXTURE_VULKAN_TEXTURE_NUMBER = "SDL.texture.vulkan.texture";

    /// <summary>
    /// Get the number of 2D rendering drivers available for the current display.
    /// </summary>
    /// <returns>The number of built in render drivers.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetNumRenderDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumRenderDrivers();

    /// <summary>
    /// Use this function to get the name of a built in 2D rendering driver.
    /// </summary>
    /// <param name="index">The index of the rendering driver.</param>
    /// <returns>The name of the rendering driver at the requested index, or NULL if an invalid index was specified.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetRenderDriver(int index);

    /// <summary>
    /// Create a window and default renderer.
    /// </summary>
    /// <param name="title">The title of the window, in UTF-8 encoding.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="window_flags">The flags used to create the window.</param>
    /// <param name="window">A pointer filled with the window, or NULL on error.</param>
    /// <param name="renderer">A pointer filled with the renderer, or NULL on error.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreateWindowAndRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CreateWindowAndRenderer(
        [MarshalUsing(typeof(Utf8StringMarshaller))] string title,
        int width,
        int height,
        SDL_WindowFlags window_flags,
        SDL_Window** window,
        SDL_Renderer** renderer);

    /// <summary>
    /// Create a 2D rendering context for a window.
    /// </summary>
    /// <param name="window">The window where rendering is displayed.</param>
    /// <param name="name">The name of the rendering driver to initialize, or NULL to let SDL choose one.</param>
    /// <returns>A valid rendering context or NULL if there was an error; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreateRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Renderer* SDL_CreateRenderer(
        SDL_Window* window,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string? name);

    /// <summary>
    /// Create a 2D rendering context for a window, with the specified properties.
    /// </summary>
    /// <param name="props">The properties to use.</param>
    /// <returns>A valid rendering context or NULL if there was an error; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreateRendererWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Renderer* SDL_CreateRendererWithProperties(SDL_PropertiesID props);

    /// <summary>
    /// Create a 2D software rendering context for a surface.
    /// </summary>
    /// <param name="surface">The SDL_Surface structure representing the surface where rendering is done.</param>
    /// <returns>A valid rendering context or NULL if there was an error; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreateSoftwareRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Renderer* SDL_CreateSoftwareRenderer(SDL_Surface* surface);

    /// <summary>
    /// Get the renderer associated with a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The rendering context on success or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Renderer* SDL_GetRenderer(SDL_Window* window);

    /// <summary>
    /// Get the window associated with a renderer.
    /// </summary>
    /// <param name="renderer">The renderer to query.</param>
    /// <returns>The window on success or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Window* SDL_GetRenderWindow(SDL_Renderer* renderer);

    /// <summary>
    /// Get the name of a renderer.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <returns>The name of the selected renderer, or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRendererName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetRendererName(SDL_Renderer* renderer);

    /// <summary>
    /// Get the properties associated with a renderer.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <returns>A valid property ID on success or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRendererProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetRendererProperties(SDL_Renderer* renderer);

    /// <summary>
    /// Get the output size in pixels of a rendering context.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="w">A pointer filled in with the width in pixels.</param>
    /// <param name="h">A pointer filled in with the height in pixels.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderOutputSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRenderOutputSize(SDL_Renderer* renderer, int* w, int* h);

    /// <summary>
    /// Get the current output size in pixels of a rendering context.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="w">A pointer filled in with the current width.</param>
    /// <param name="h">A pointer filled in with the current height.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetCurrentRenderOutputSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetCurrentRenderOutputSize(SDL_Renderer* renderer, int* w, int* h);

    /// <summary>
    /// Create a texture for a rendering context.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="format">One of the enumerated values in SDL_PixelFormat.</param>
    /// <param name="access">One of the enumerated values in SDL_TextureAccess.</param>
    /// <param name="w">The width of the texture in pixels.</param>
    /// <param name="h">The height of the texture in pixels.</param>
    /// <returns>The created texture or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreateTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Texture* SDL_CreateTexture(
        SDL_Renderer* renderer,
        SDL_PixelFormat format,
        SDL_TextureAccess access,
        int w,
        int h);

    /// <summary>
    /// Create a texture from an existing surface.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="surface">The SDL_Surface structure containing pixel data used to fill the texture.</param>
    /// <returns>The created texture or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreateTextureFromSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Texture* SDL_CreateTextureFromSurface(SDL_Renderer* renderer, SDL_Surface* surface);

    /// <summary>
    /// Create a texture for a rendering context with the specified properties.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="props">The properties to use.</param>
    /// <returns>The created texture or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreateTextureWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Texture* SDL_CreateTextureWithProperties(SDL_Renderer* renderer, SDL_PropertiesID props);

    /// <summary>
    /// Get the properties associated with a texture.
    /// </summary>
    /// <param name="texture">The texture to query.</param>
    /// <returns>A valid property ID on success or 0 on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetTextureProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetTextureProperties(SDL_Texture* texture);

    /// <summary>
    /// Get the renderer that created an SDL_Texture.
    /// </summary>
    /// <param name="texture">The texture to query.</param>
    /// <returns>A pointer to the SDL_Renderer that created the texture, or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRendererFromTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Renderer* SDL_GetRendererFromTexture(SDL_Texture* texture);

    /// <summary>
    /// Get the size of a texture, as floating point values.
    /// </summary>
    /// <param name="texture">The texture to query.</param>
    /// <param name="w">A pointer filled in with the width of the texture in pixels.</param>
    /// <param name="h">A pointer filled in with the height of the texture in pixels.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetTextureSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetTextureSize(SDL_Texture* texture, float* w, float* h);

    /// <summary>
    /// Set an additional color value multiplied into render copy operations.
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="r">The red color value multiplied into copy operations.</param>
    /// <param name="g">The green color value multiplied into copy operations.</param>
    /// <param name="b">The blue color value multiplied into copy operations.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetTextureColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetTextureColorMod(SDL_Texture* texture, byte r, byte g, byte b);

    /// <summary>
    /// Set an additional color value multiplied into render copy operations (float version).
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="r">The red color value multiplied into copy operations.</param>
    /// <param name="g">The green color value multiplied into copy operations.</param>
    /// <param name="b">The blue color value multiplied into copy operations.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetTextureColorModFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetTextureColorModFloat(SDL_Texture* texture, float r, float g, float b);

    /// <summary>
    /// Get the additional color value multiplied into render copy operations.
    /// </summary>
    /// <param name="texture">The texture to query.</param>
    /// <param name="r">A pointer filled in with the current red color value.</param>
    /// <param name="g">A pointer filled in with the current green color value.</param>
    /// <param name="b">A pointer filled in with the current blue color value.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetTextureColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetTextureColorMod(SDL_Texture* texture, byte* r, byte* g, byte* b);

    /// <summary>
    /// Get the additional color value multiplied into render copy operations (float version).
    /// </summary>
    /// <param name="texture">The texture to query.</param>
    /// <param name="r">A pointer filled in with the current red color value.</param>
    /// <param name="g">A pointer filled in with the current green color value.</param>
    /// <param name="b">A pointer filled in with the current blue color value.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetTextureColorModFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetTextureColorModFloat(SDL_Texture* texture, float* r, float* g, float* b);

    /// <summary>
    /// Set an additional alpha value multiplied into render copy operations.
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="alpha">The source alpha value multiplied into copy operations.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetTextureAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetTextureAlphaMod(SDL_Texture* texture, byte alpha);

    /// <summary>
    /// Set an additional alpha value multiplied into render copy operations (float version).
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="alpha">The source alpha value multiplied into copy operations.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetTextureAlphaModFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetTextureAlphaModFloat(SDL_Texture* texture, float alpha);

    /// <summary>
    /// Get the additional alpha value multiplied into render copy operations.
    /// </summary>
    /// <param name="texture">The texture to query.</param>
    /// <param name="alpha">A pointer filled in with the current alpha value.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetTextureAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetTextureAlphaMod(SDL_Texture* texture, byte* alpha);

    /// <summary>
    /// Get the additional alpha value multiplied into render copy operations (float version).
    /// </summary>
    /// <param name="texture">The texture to query.</param>
    /// <param name="alpha">A pointer filled in with the current alpha value.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetTextureAlphaModFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetTextureAlphaModFloat(SDL_Texture* texture, float* alpha);

    /// <summary>
    /// Set the blend mode for a texture, used by SDL_RenderTexture().
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="blendMode">The SDL_BlendMode to use for texture blending.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetTextureBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetTextureBlendMode(SDL_Texture* texture, SDL_BlendMode blendMode);

    /// <summary>
    /// Get the blend mode used for texture copy operations.
    /// </summary>
    /// <param name="texture">The texture to query.</param>
    /// <param name="blendMode">A pointer filled in with the current SDL_BlendMode.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetTextureBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetTextureBlendMode(SDL_Texture* texture, SDL_BlendMode* blendMode);

    /// <summary>
    /// Set the scale mode used for texture scale operations.
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="scaleMode">The SDL_ScaleMode to use for texture scaling.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetTextureScaleMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetTextureScaleMode(SDL_Texture* texture, SDL_ScaleMode scaleMode);

    /// <summary>
    /// Get the scale mode used for texture scale operations.
    /// </summary>
    /// <param name="texture">The texture to query.</param>
    /// <param name="scaleMode">A pointer filled in with the current scale mode.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetTextureScaleMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetTextureScaleMode(SDL_Texture* texture, SDL_ScaleMode* scaleMode);

    /// <summary>
    /// Update the given texture rectangle with new pixel data.
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="rect">An SDL_Rect structure representing the area to update, or NULL to update the entire texture.</param>
    /// <param name="pixels">The raw pixel data in the format of the texture.</param>
    /// <param name="pitch">The number of bytes in a row of pixel data, including padding between lines.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_UpdateTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_UpdateTexture(SDL_Texture* texture, SDL_Rect* rect, void* pixels, int pitch);

    /// <summary>
    /// Update a rectangle within a planar YV12 or IYUV texture with new pixel data.
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="rect">A pointer to the rectangle of pixels to update, or NULL to update the entire texture.</param>
    /// <param name="Yplane">The raw pixel data for the Y plane.</param>
    /// <param name="Ypitch">The number of bytes between rows of pixel data for the Y plane.</param>
    /// <param name="Uplane">The raw pixel data for the U plane.</param>
    /// <param name="Upitch">The number of bytes between rows of pixel data for the U plane.</param>
    /// <param name="Vplane">The raw pixel data for the V plane.</param>
    /// <param name="Vpitch">The number of bytes between rows of pixel data for the V plane.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_UpdateYUVTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_UpdateYUVTexture(
        SDL_Texture* texture,
        SDL_Rect* rect,
        byte* Yplane,
        int Ypitch,
        byte* Uplane,
        int Upitch,
        byte* Vplane,
        int Vpitch);

    /// <summary>
    /// Update a rectangle within a planar NV12 or NV21 texture with new pixels.
    /// </summary>
    /// <param name="texture">The texture to update.</param>
    /// <param name="rect">A pointer to the rectangle of pixels to update, or NULL to update the entire texture.</param>
    /// <param name="Yplane">The raw pixel data for the Y plane.</param>
    /// <param name="Ypitch">The number of bytes between rows of pixel data for the Y plane.</param>
    /// <param name="UVplane">The raw pixel data for the UV plane.</param>
    /// <param name="UVpitch">The number of bytes between rows of pixel data for the UV plane.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_UpdateNVTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_UpdateNVTexture(
        SDL_Texture* texture,
        SDL_Rect* rect,
        byte* Yplane,
        int Ypitch,
        byte* UVplane,
        int UVpitch);

    /// <summary>
    /// Lock a portion of the texture for write-only pixel access.
    /// </summary>
    /// <param name="texture">The texture to lock for access, which was created with SDL_TEXTUREACCESS_STREAMING.</param>
    /// <param name="rect">An SDL_Rect structure representing the area to lock for access; NULL to lock the entire texture.</param>
    /// <param name="pixels">This is filled in with a pointer to the locked pixels, appropriately offset by the locked area.</param>
    /// <param name="pitch">This is filled in with the pitch of the locked pixels; the pitch is the length of one row in bytes.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_LockTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_LockTexture(SDL_Texture* texture, SDL_Rect* rect, void** pixels, int* pitch);

    /// <summary>
    /// Lock a portion of the texture for write-only pixel access, and expose it as a SDL surface.
    /// </summary>
    /// <param name="texture">The texture to lock for access, which must be created with SDL_TEXTUREACCESS_STREAMING.</param>
    /// <param name="rect">A pointer to the rectangle to lock for access. If the rect is NULL, the entire texture will be locked.</param>
    /// <param name="surface">A pointer to an SDL surface.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_LockTextureToSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_LockTextureToSurface(SDL_Texture* texture, SDL_Rect* rect, SDL_Surface** surface);

    /// <summary>
    /// Unlock a texture, uploading the changes to video memory, if needed.
    /// </summary>
    /// <param name="texture">A texture locked by SDL_LockTexture().</param>
    [LibraryImport(Sdl3, EntryPoint = "SDL_UnlockTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_UnlockTexture(SDL_Texture* texture);

    /// <summary>
    /// Set a texture as the current rendering target.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="texture">The targeted texture, which must be created with the SDL_TEXTUREACCESS_TARGET flag, or NULL to render to the window instead of a texture.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetRenderTarget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetRenderTarget(SDL_Renderer* renderer, SDL_Texture* texture);

    /// <summary>
    /// Get the current render target.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <returns>The current render target or NULL for the default render target.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderTarget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Texture* SDL_GetRenderTarget(SDL_Renderer* renderer);

    /// <summary>
    /// Set a device-independent resolution and presentation mode for rendering.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="w">The width of the logical resolution.</param>
    /// <param name="h">The height of the logical resolution.</param>
    /// <param name="mode">The presentation mode used.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetRenderLogicalPresentation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetRenderLogicalPresentation(
        SDL_Renderer* renderer,
        int w,
        int h,
        SDL_RendererLogicalPresentation mode);

    /// <summary>
    /// Get device independent resolution and presentation mode for rendering.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="w">An int to be filled with the width.</param>
    /// <param name="h">An int to be filled with the height.</param>
    /// <param name="mode">The presentation mode used.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderLogicalPresentation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRenderLogicalPresentation(
        SDL_Renderer* renderer,
        int* w,
        int* h,
        SDL_RendererLogicalPresentation* mode);

    /// <summary>
    /// Get the final presentation rectangle for rendering.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="rect">A pointer filled in with the final presentation rectangle, may be NULL.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderLogicalPresentationRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRenderLogicalPresentationRect(SDL_Renderer* renderer, SDL_FRect* rect);

    /// <summary>
    /// Get a point in render coordinates when given a point in window coordinates.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="window_x">The x coordinate in window coordinates.</param>
    /// <param name="window_y">The y coordinate in window coordinates.</param>
    /// <param name="x">A pointer filled with the x coordinate in render coordinates.</param>
    /// <param name="y">A pointer filled with the y coordinate in render coordinates.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderCoordinatesFromWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderCoordinatesFromWindow(
        SDL_Renderer* renderer,
        float window_x,
        float window_y,
        float* x,
        float* y);

    /// <summary>
    /// Get a point in window coordinates when given a point in render coordinates.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="x">The x coordinate in render coordinates.</param>
    /// <param name="y">The y coordinate in render coordinates.</param>
    /// <param name="window_x">A pointer filled with the x coordinate in window coordinates.</param>
    /// <param name="window_y">A pointer filled with the y coordinate in window coordinates.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderCoordinatesToWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderCoordinatesToWindow(
        SDL_Renderer* renderer,
        float x,
        float y,
        float* window_x,
        float* window_y);

    /// <summary>
    /// Convert the coordinates in an event to render coordinates.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="event">The event to modify.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_ConvertEventToRenderCoordinates")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ConvertEventToRenderCoordinates(SDL_Renderer* renderer, SDL_Event* @event);

    /// <summary>
    /// Set the drawing area for rendering on the current target.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="rect">The SDL_Rect structure representing the drawing area, or NULL to set the viewport to the entire target.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetRenderViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetRenderViewport(SDL_Renderer* renderer, SDL_Rect* rect);

    /// <summary>
    /// Get the drawing area for the current target.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="rect">An SDL_Rect structure filled in with the current drawing area.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRenderViewport(SDL_Renderer* renderer, SDL_Rect* rect);

    /// <summary>
    /// Return whether an explicit rectangle was set as the viewport.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <returns>True if the viewport was set to a specific rectangle, or false if it was set to NULL (the entire target).</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderViewportSet")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderViewportSet(SDL_Renderer* renderer);

    /// <summary>
    /// Get the safe area for rendering within the current viewport.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="rect">A pointer filled in with the area that is safe for interactive content.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderSafeArea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRenderSafeArea(SDL_Renderer* renderer, SDL_Rect* rect);

    /// <summary>
    /// Set the clip rectangle for rendering on the specified target.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="rect">An SDL_Rect structure representing the clip area, or NULL to disable clipping.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetRenderClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetRenderClipRect(SDL_Renderer* renderer, SDL_Rect* rect);

    /// <summary>
    /// Get the clip rectangle for the current target.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="rect">An SDL_Rect structure filled in with the current clipping area or an empty rectangle if clipping is disabled.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRenderClipRect(SDL_Renderer* renderer, SDL_Rect* rect);

    /// <summary>
    /// Get whether clipping is enabled on the given renderer.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <returns>True if clipping is enabled or false if not; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderClipEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderClipEnabled(SDL_Renderer* renderer);

    /// <summary>
    /// Set the drawing scale for rendering on the current target.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="scaleX">The horizontal scaling factor.</param>
    /// <param name="scaleY">The vertical scaling factor.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetRenderScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetRenderScale(SDL_Renderer* renderer, float scaleX, float scaleY);

    /// <summary>
    /// Get the drawing scale for the current target.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="scaleX">A pointer filled in with the horizontal scaling factor.</param>
    /// <param name="scaleY">A pointer filled in with the vertical scaling factor.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRenderScale(SDL_Renderer* renderer, float* scaleX, float* scaleY);

    /// <summary>
    /// Set the color used for drawing operations.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="r">The red value used to draw on the rendering target.</param>
    /// <param name="g">The green value used to draw on the rendering target.</param>
    /// <param name="b">The blue value used to draw on the rendering target.</param>
    /// <param name="a">The alpha value used to draw on the rendering target.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetRenderDrawColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetRenderDrawColor(SDL_Renderer* renderer, byte r, byte g, byte b, byte a);

    /// <summary>
    /// Set the color used for drawing operations (float version).
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="r">The red value used to draw on the rendering target.</param>
    /// <param name="g">The green value used to draw on the rendering target.</param>
    /// <param name="b">The blue value used to draw on the rendering target.</param>
    /// <param name="a">The alpha value used to draw on the rendering target.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetRenderDrawColorFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetRenderDrawColorFloat(SDL_Renderer* renderer, float r, float g, float b, float a);

    /// <summary>
    /// Get the color used for drawing operations (Rect, Line and Clear).
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="r">A pointer filled in with the red value used to draw on the rendering target.</param>
    /// <param name="g">A pointer filled in with the green value used to draw on the rendering target.</param>
    /// <param name="b">A pointer filled in with the blue value used to draw on the rendering target.</param>
    /// <param name="a">A pointer filled in with the alpha value used to draw on the rendering target.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderDrawColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRenderDrawColor(SDL_Renderer* renderer, byte* r, byte* g, byte* b, byte* a);

    /// <summary>
    /// Get the color used for drawing operations (Rect, Line and Clear) (float version).
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="r">A pointer filled in with the red value used to draw on the rendering target.</param>
    /// <param name="g">A pointer filled in with the green value used to draw on the rendering target.</param>
    /// <param name="b">A pointer filled in with the blue value used to draw on the rendering target.</param>
    /// <param name="a">A pointer filled in with the alpha value used to draw on the rendering target.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderDrawColorFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRenderDrawColorFloat(SDL_Renderer* renderer, float* r, float* g, float* b, float* a);

    /// <summary>
    /// Set the color scale used for render operations.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="scale">The color scale value.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetRenderColorScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetRenderColorScale(SDL_Renderer* renderer, float scale);

    /// <summary>
    /// Get the color scale used for render operations.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="scale">A pointer filled in with the current color scale value.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderColorScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRenderColorScale(SDL_Renderer* renderer, float* scale);

    /// <summary>
    /// Set the blend mode used for drawing operations (Fill and Line).
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="blendMode">The SDL_BlendMode to use for blending.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetRenderDrawBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetRenderDrawBlendMode(SDL_Renderer* renderer, SDL_BlendMode blendMode);

    /// <summary>
    /// Get the blend mode used for drawing operations.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="blendMode">A pointer filled in with the current SDL_BlendMode.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderDrawBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRenderDrawBlendMode(SDL_Renderer* renderer, SDL_BlendMode* blendMode);

    /// <summary>
    /// Clear the current rendering target with the drawing color.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderClear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderClear(SDL_Renderer* renderer);

    /// <summary>
    /// Draw a point on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="x">The x coordinate of the point.</param>
    /// <param name="y">The y coordinate of the point.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderPoint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderPoint(SDL_Renderer* renderer, float x, float y);

    /// <summary>
    /// Draw multiple points on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="points">The points to draw.</param>
    /// <param name="count">The number of points to draw.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderPoints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderPoints(SDL_Renderer* renderer, SDL_FPoint* points, int count);

    /// <summary>
    /// Draw a line on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="x1">The x coordinate of the start point.</param>
    /// <param name="y1">The y coordinate of the start point.</param>
    /// <param name="x2">The x coordinate of the end point.</param>
    /// <param name="y2">The y coordinate of the end point.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderLine(SDL_Renderer* renderer, float x1, float y1, float x2, float y2);

    /// <summary>
    /// Draw a series of connected lines on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="points">The points along the lines.</param>
    /// <param name="count">The number of points, drawing count-1 lines.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderLines")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderLines(SDL_Renderer* renderer, SDL_FPoint* points, int count);

    /// <summary>
    /// Draw a rectangle on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="rect">A pointer to the destination rectangle, or NULL to outline the entire rendering target.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderRect(SDL_Renderer* renderer, SDL_FRect* rect);

    /// <summary>
    /// Draw some number of rectangles on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="rects">A pointer to an array of destination rectangles.</param>
    /// <param name="count">The number of rectangles.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderRects(SDL_Renderer* renderer, SDL_FRect* rects, int count);

    /// <summary>
    /// Fill a rectangle on the current rendering target with the drawing color at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="rect">A pointer to the destination rectangle, or NULL for the entire rendering target.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderFillRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderFillRect(SDL_Renderer* renderer, SDL_FRect* rect);

    /// <summary>
    /// Fill some number of rectangles on the current rendering target with the drawing color at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="rects">A pointer to an array of destination rectangles.</param>
    /// <param name="count">The number of rectangles.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderFillRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderFillRects(SDL_Renderer* renderer, SDL_FRect* rects, int count);

    /// <summary>
    /// Copy a portion of the texture to the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="texture">The source texture.</param>
    /// <param name="srcrect">A pointer to the source rectangle, or NULL for the entire texture.</param>
    /// <param name="dstrect">A pointer to the destination rectangle, or NULL for the entire rendering target.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderTexture(
        SDL_Renderer* renderer,
        SDL_Texture* texture,
        SDL_FRect* srcrect,
        SDL_FRect* dstrect);

    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with rotation and flipping, at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="texture">The source texture.</param>
    /// <param name="srcrect">A pointer to the source rectangle, or NULL for the entire texture.</param>
    /// <param name="dstrect">A pointer to the destination rectangle, or NULL for the entire rendering target.</param>
    /// <param name="angle">An angle in degrees that indicates the rotation that will be applied to dstrect, rotating it in a clockwise direction.</param>
    /// <param name="center">A pointer to a point indicating the point around which dstrect will be rotated.</param>
    /// <param name="flip">An SDL_FlipMode value stating which flipping actions should be performed on the texture.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderTextureRotated")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderTextureRotated(
        SDL_Renderer* renderer,
        SDL_Texture* texture,
        SDL_FRect* srcrect,
        SDL_FRect* dstrect,
        double angle,
        SDL_FPoint* center,
        SDL_FlipMode flip);

    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with affine transformation, at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="texture">The source texture.</param>
    /// <param name="srcrect">A pointer to the source rectangle, or NULL for the entire texture.</param>
    /// <param name="origin">A pointer to a point indicating the origin of the transform, or NULL for the upper-left corner of dstrect.</param>
    /// <param name="right">A pointer to a point indicating the right edge of the destination rectangle.</param>
    /// <param name="down">A pointer to a point indicating the bottom edge of the destination rectangle.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderTextureAffine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderTextureAffine(
        SDL_Renderer* renderer,
        SDL_Texture* texture,
        SDL_FRect* srcrect,
        SDL_FPoint* origin,
        SDL_FPoint* right,
        SDL_FPoint* down);

    /// <summary>
    /// Tile a portion of the texture to the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="texture">The source texture.</param>
    /// <param name="srcrect">A pointer to the source rectangle, or NULL for the entire texture.</param>
    /// <param name="scale">The scale used to transform srcrect into the destination rectangle.</param>
    /// <param name="dstrect">A pointer to the destination rectangle, or NULL for the entire rendering target.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderTextureTiled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderTextureTiled(
        SDL_Renderer* renderer,
        SDL_Texture* texture,
        SDL_FRect* srcrect,
        float scale,
        SDL_FRect* dstrect);

    /// <summary>
    /// Perform a scaled copy using the 9-grid algorithm to the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="texture">The source texture.</param>
    /// <param name="srcrect">The SDL_Rect structure representing the rectangle to be used for the 9-grid, or NULL to use the entire texture.</param>
    /// <param name="left_width">The width, in pixels, of the left corners in srcrect.</param>
    /// <param name="right_width">The width, in pixels, of the right corners in srcrect.</param>
    /// <param name="top_height">The height, in pixels, of the top corners in srcrect.</param>
    /// <param name="bottom_height">The height, in pixels, of the bottom corners in srcrect.</param>
    /// <param name="scale">The scale used to transform the corner of srcrect into the corner of dstrect, or 0.0f for an unscaled copy.</param>
    /// <param name="dstrect">A pointer to the destination rectangle, or NULL for the entire rendering target.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderTexture9Grid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderTexture9Grid(
        SDL_Renderer* renderer,
        SDL_Texture* texture,
        SDL_FRect* srcrect,
        float left_width,
        float right_width,
        float top_height,
        float bottom_height,
        float scale,
        SDL_FRect* dstrect);

    /// <summary>
    /// Render a list of triangles, optionally using a texture and indices into the vertex array.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="texture">The SDL texture to use, or NULL for solid color.</param>
    /// <param name="vertices">Vertices.</param>
    /// <param name="num_vertices">Number of vertices.</param>
    /// <param name="indices">An array of integer indices into the 'vertices' array, or NULL to use all vertices in order.</param>
    /// <param name="num_indices">Number of indices into the 'vertices' array, or 0 to draw num_vertices triangles.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderGeometry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderGeometry(
        SDL_Renderer* renderer,
        SDL_Texture* texture,
        SDL_Vertex* vertices,
        int num_vertices,
        int* indices,
        int num_indices);

    /// <summary>
    /// Render a list of triangles, optionally using a texture and indices into the vertex arrays.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="texture">The SDL texture to use, or NULL for solid color.</param>
    /// <param name="xy">Vertex positions.</param>
    /// <param name="xy_stride">Byte size to move from one element to the next element.</param>
    /// <param name="color">Vertex colors (as SDL_FColor).</param>
    /// <param name="color_stride">Byte size to move from one element to the next element.</param>
    /// <param name="uv">Vertex normalized texture coordinates.</param>
    /// <param name="uv_stride">Byte size to move from one element to the next element.</param>
    /// <param name="num_vertices">Number of vertices.</param>
    /// <param name="indices">An array of indices into the 'vertices' arrays, or NULL to use all vertices in order.</param>
    /// <param name="num_indices">Number of indices, or 0 to draw num_vertices triangles.</param>
    /// <param name="size_indices">Index size: 1 (byte), 2 (short), or 4 (int).</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderGeometryRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderGeometryRaw(
        SDL_Renderer* renderer,
        SDL_Texture* texture,
        float* xy,
        int xy_stride,
        SDL_FColor* color,
        int color_stride,
        float* uv,
        int uv_stride,
        int num_vertices,
        void* indices,
        int num_indices,
        int size_indices);

    /// <summary>
    /// Read pixels from the current rendering target.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="rect">An SDL_Rect structure representing the area to read, or NULL for the entire render target.</param>
    /// <returns>A new SDL_Surface on success or NULL on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderReadPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Surface* SDL_RenderReadPixels(SDL_Renderer* renderer, SDL_Rect* rect);

    /// <summary>
    /// Update the screen with any rendering performed since the previous call.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderPresent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderPresent(SDL_Renderer* renderer);

    /// <summary>
    /// Destroy the specified texture.
    /// </summary>
    /// <param name="texture">The texture to destroy.</param>
    [LibraryImport(Sdl3, EntryPoint = "SDL_DestroyTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyTexture(SDL_Texture* texture);

    /// <summary>
    /// Destroy the rendering context for a window and free all associated textures.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    [LibraryImport(Sdl3, EntryPoint = "SDL_DestroyRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyRenderer(SDL_Renderer* renderer);

    /// <summary>
    /// Force the rendering context to flush any pending commands and state.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_FlushRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_FlushRenderer(SDL_Renderer* renderer);

    /// <summary>
    /// Get the CAMetalLayer associated with the given Metal renderer.
    /// </summary>
    /// <param name="renderer">The renderer to query.</param>
    /// <returns>A CAMetalLayer* on success, or NULL if the renderer isn't a Metal renderer.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderMetalLayer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_GetRenderMetalLayer(SDL_Renderer* renderer);

    /// <summary>
    /// Get the Metal command encoder for the current frame.
    /// </summary>
    /// <param name="renderer">The renderer to query.</param>
    /// <returns>An id&lt;MTLRenderCommandEncoder&gt; on success, or NULL if the renderer isn't a Metal renderer or there was an error.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderMetalCommandEncoder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_GetRenderMetalCommandEncoder(SDL_Renderer* renderer);

    /// <summary>
    /// Add a set of synchronization semaphores for the current frame.
    /// </summary>
    /// <param name="renderer">The rendering context.</param>
    /// <param name="wait_stage_mask">The VkPipelineStageFlags for the wait.</param>
    /// <param name="wait_semaphore">A VkSempahore to wait on before rendering the current frame, or 0 if not needed.</param>
    /// <param name="signal_semaphore">A VkSempahore that SDL will signal when rendering for the current frame is complete, or 0 if not needed.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_AddVulkanRenderSemaphores")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_AddVulkanRenderSemaphores(
        SDL_Renderer* renderer,
        uint wait_stage_mask,
        long wait_semaphore,
        long signal_semaphore);

    /// <summary>
    /// Toggle VSync of the given renderer.
    /// </summary>
    /// <param name="renderer">The renderer to toggle.</param>
    /// <param name="vsync">The vertical refresh sync interval.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetRenderVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetRenderVSync(SDL_Renderer* renderer, int vsync);

    /// <summary>
    /// Get VSync of the given renderer.
    /// </summary>
    /// <param name="renderer">The renderer to query.</param>
    /// <param name="vsync">An int filled with the current vertical refresh sync interval.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetRenderVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetRenderVSync(SDL_Renderer* renderer, int* vsync);

    /// <summary>
    /// Draw debug text to an SDL_Renderer.
    /// </summary>
    /// <param name="renderer">The renderer which should draw the debug text.</param>
    /// <param name="x">The x coordinate where the top-left corner of the text will draw.</param>
    /// <param name="y">The y coordinate where the top-left corner of the text will draw.</param>
    /// <param name="str">The UTF-8 string to draw.</param>
    /// <returns>True on success or false on failure; call SDL_GetError() for more information.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RenderDebugText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RenderDebugText(
        SDL_Renderer* renderer,
        float x,
        float y,
        [MarshalUsing(typeof(Utf8StringMarshaller))] string str);

    // SDL_RenderDebugTextFormat is not wrapped - it uses variadic arguments which cannot be directly wrapped in C# P/Invoke.
    // To use formatted debug text, format the string in C# using string interpolation or String.Format and pass to SDL_RenderDebugText.
}
