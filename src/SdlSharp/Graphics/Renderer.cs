using System.Runtime.InteropServices;

using SdlSharp.Native;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Render;

namespace SdlSharp.Graphics;

/// <summary>
/// A managed wrapper around an SDL 2D renderer (SDL_Renderer).
/// </summary>
public sealed unsafe class Renderer : IDisposable
{
    private readonly bool _ownsHandle;

    /// <summary>
    /// The underlying native SDL_Renderer pointer.
    /// </summary>
    internal SDL_Renderer* Handle { get; private set; }

    internal Renderer(SDL_Renderer* handle, bool ownsHandle = true)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Gets the number of 2D rendering drivers available for the current display.
    /// </summary>
    public static int NumDrivers => SDL_GetNumRenderDrivers();

    /// <summary>
    /// Gets the name of a built-in 2D rendering driver.
    /// </summary>
    /// <param name="index">The index of the rendering driver.</param>
    /// <returns>The name of the rendering driver.</returns>
    public static string? GetDriver(int index) =>
        Marshal.PtrToStringUTF8((nint)SDL_GetRenderDriver(index));

    /// <summary>
    /// The name of the software renderer.
    /// </summary>
    public const string SoftwareRenderer = "software";

    /// <summary>
    /// The name of the GPU renderer.
    /// </summary>
    public const string GpuRenderer = "gpu";

    // --- Create property names (for use with Create(PropertyGroup)) ---

    /// <summary>Property: the name of the rendering driver to create.</summary>
    public const string PropCreateName = Render.SDL_PROP_RENDERER_CREATE_NAME_STRING;
    /// <summary>Property: the window where rendering is displayed (pointer).</summary>
    public const string PropCreateWindow = Render.SDL_PROP_RENDERER_CREATE_WINDOW_POINTER;
    /// <summary>Property: the surface where rendering is displayed (pointer).</summary>
    public const string PropCreateSurface = Render.SDL_PROP_RENDERER_CREATE_SURFACE_POINTER;
    /// <summary>Property: an SDL_Colorspace value for the output colorspace (number).</summary>
    public const string PropCreateOutputColorspace = Render.SDL_PROP_RENDERER_CREATE_OUTPUT_COLORSPACE_NUMBER;
    /// <summary>Property: non-zero if you want present synchronized with the refresh rate (number).</summary>
    public const string PropCreatePresentVSync = Render.SDL_PROP_RENDERER_CREATE_PRESENT_VSYNC_NUMBER;
    /// <summary>Property: the GPU device to use for rendering (pointer).</summary>
    public const string PropCreateGpuDevice = Render.SDL_PROP_RENDERER_CREATE_GPU_DEVICE_POINTER;
    /// <summary>Property: the app is able to provide SPIR-V shaders (boolean).</summary>
    public const string PropCreateGpuShadersSpirvEnabled = Render.SDL_PROP_RENDERER_CREATE_GPU_SHADERS_SPIRV_BOOLEAN;
    /// <summary>Property: the app is able to provide DXIL shaders (boolean).</summary>
    public const string PropCreateGpuShadersDxilEnabled = Render.SDL_PROP_RENDERER_CREATE_GPU_SHADERS_DXIL_BOOLEAN;
    /// <summary>Property: the app is able to provide MSL shaders (boolean).</summary>
    public const string PropCreateGpuShadersMslEnabled = Render.SDL_PROP_RENDERER_CREATE_GPU_SHADERS_MSL_BOOLEAN;
    /// <summary>Property: the VkInstance to use with the renderer (pointer).</summary>
    public const string PropCreateVulkanInstance = Render.SDL_PROP_RENDERER_CREATE_VULKAN_INSTANCE_POINTER;
    /// <summary>Property: the VkSurfaceKHR to use with the renderer (number).</summary>
    public const string PropCreateVulkanSurface = Render.SDL_PROP_RENDERER_CREATE_VULKAN_SURFACE_NUMBER;
    /// <summary>Property: the VkPhysicalDevice to use with the renderer (pointer).</summary>
    public const string PropCreateVulkanPhysicalDevice = Render.SDL_PROP_RENDERER_CREATE_VULKAN_PHYSICAL_DEVICE_POINTER;
    /// <summary>Property: the VkDevice to use with the renderer (pointer).</summary>
    public const string PropCreateVulkanDevice = Render.SDL_PROP_RENDERER_CREATE_VULKAN_DEVICE_POINTER;
    /// <summary>Property: the queue family index for graphics operations (number).</summary>
    public const string PropCreateVulkanGraphicsQueueFamilyIndex = Render.SDL_PROP_RENDERER_CREATE_VULKAN_GRAPHICS_QUEUE_FAMILY_INDEX_NUMBER;
    /// <summary>Property: the queue family index for present operations (number).</summary>
    public const string PropCreateVulkanPresentQueueFamilyIndex = Render.SDL_PROP_RENDERER_CREATE_VULKAN_PRESENT_QUEUE_FAMILY_INDEX_NUMBER;

    // --- Renderer property names (for use with Properties) ---

    /// <summary>Property: the name of the rendering driver (string).</summary>
    public const string PropName = Render.SDL_PROP_RENDERER_NAME_STRING;
    /// <summary>Property: the window where rendering is displayed (pointer).</summary>
    public const string PropWindow = Render.SDL_PROP_RENDERER_WINDOW_POINTER;
    /// <summary>Property: the surface where rendering is displayed (pointer).</summary>
    public const string PropSurface = Render.SDL_PROP_RENDERER_SURFACE_POINTER;
    /// <summary>Property: the current VSync setting (number).</summary>
    public const string PropVSync = Render.SDL_PROP_RENDERER_VSYNC_NUMBER;
    /// <summary>Property: the maximum texture size (number).</summary>
    public const string PropMaxTextureSize = Render.SDL_PROP_RENDERER_MAX_TEXTURE_SIZE_NUMBER;
    /// <summary>Property: pointer to the array of supported texture formats (pointer).</summary>
    public const string PropTextureFormats = Render.SDL_PROP_RENDERER_TEXTURE_FORMATS_POINTER;
    /// <summary>Property: whether the renderer supports texture wrapping (boolean).</summary>
    public const string PropTextureWrapping = Render.SDL_PROP_RENDERER_TEXTURE_WRAPPING_BOOLEAN;
    /// <summary>Property: an SDL_Colorspace value for the output colorspace (number).</summary>
    public const string PropOutputColorspace = Render.SDL_PROP_RENDERER_OUTPUT_COLORSPACE_NUMBER;
    /// <summary>Property: whether HDR is enabled (boolean).</summary>
    public const string PropHdrEnabled = Render.SDL_PROP_RENDERER_HDR_ENABLED_BOOLEAN;
    /// <summary>Property: the SDR white point value (float).</summary>
    public const string PropSdrWhitePoint = Render.SDL_PROP_RENDERER_SDR_WHITE_POINT_FLOAT;
    /// <summary>Property: the HDR headroom value (float).</summary>
    public const string PropHdrHeadroom = Render.SDL_PROP_RENDERER_HDR_HEADROOM_FLOAT;
    /// <summary>Property: the GPU device pointer (pointer).</summary>
    public const string PropGpuDevice = Render.SDL_PROP_RENDERER_GPU_DEVICE_POINTER;

    /// <summary>
    /// Creates a 2D rendering context for a window.
    /// </summary>
    /// <param name="window">The window to create the renderer for.</param>
    /// <param name="name">The name of the rendering driver to initialize, or <c>null</c> for the first available.</param>
    /// <returns>A new renderer.</returns>
    public static Renderer Create(Window window, string? name = null) =>
        new(Check(SDL_CreateRenderer(window.Handle, ToUtf8(name))));

    /// <summary>
    /// Creates a 2D rendering context using properties to specify options.
    /// </summary>
    /// <param name="properties">The properties to create the renderer with.</param>
    /// <returns>A new renderer.</returns>
    public static Renderer Create(PropertyGroup properties) =>
        new(Check(SDL_CreateRendererWithProperties(properties.Id)));

    /// <summary>
    /// Creates a window and default renderer.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="w">The width of the window in screen coordinates.</param>
    /// <param name="h">The height of the window in screen coordinates.</param>
    /// <param name="flags">Window creation flags.</param>
    /// <returns>A tuple of the new window and renderer, both owning.</returns>
    public static (Window Window, Renderer Renderer) CreateWindowAndRenderer(string title, int w, int h, WindowFlags flags = 0)
    {
        Check(SDL_CreateWindowAndRenderer(ToUtf8(title), w, h, (SDL_WindowFlags)flags,
            out var window, out var renderer));
        return (new Window(window), new Renderer(renderer));
    }

    /// <summary>
    /// Gets the renderer associated with a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>A non-owning renderer, or <c>null</c> if the window has no renderer.</returns>
    public static Renderer? FromWindow(Window window)
    {
        var ptr = SDL_GetRenderer(window.Handle);
        return ptr == null ? null : new Renderer(ptr, ownsHandle: false);
    }

    /// <summary>
    /// Gets the name of the renderer.
    /// </summary>
    public string? Name => Marshal.PtrToStringUTF8((nint)SDL_GetRendererName(Handle));

    /// <summary>
    /// Gets the properties associated with this renderer.
    /// </summary>
    public PropertyGroup Properties =>
        new(CheckId(SDL_GetRendererProperties(Handle)), ownsHandle: false);

    /// <summary>
    /// Gets the output size in pixels of this renderer.
    /// </summary>
    public Size OutputSize
    {
        get
        {
            Check(SDL_GetRenderOutputSize(Handle, out var w, out var h));
            return new Size(w, h);
        }
    }

    /// <summary>
    /// Gets the current output size in pixels of this renderer.
    /// </summary>
    public Size CurrentOutputSize
    {
        get
        {
            Check(SDL_GetCurrentRenderOutputSize(Handle, out var w, out var h));
            return new Size(w, h);
        }
    }

    /// <summary>
    /// Gets the window associated with this renderer. The returned window is non-owning.
    /// </summary>
    public Window Window => new(Check(SDL_GetRenderWindow(Handle)), ownsHandle: false);

    /// <summary>
    /// Gets or sets the color used for drawing operations (Rect, Line and Clear).
    /// </summary>
    public Color DrawColor
    {
        get
        {
            Check(SDL_GetRenderDrawColor(Handle, out var r, out var g, out var b, out var a));
            return new Color(r, g, b, a);
        }
        set => Check(SDL_SetRenderDrawColor(Handle, value.R, value.G, value.B, value.A));
    }

    /// <summary>
    /// Gets or sets the color used for drawing operations with floating point precision.
    /// </summary>
    public FColor DrawColorFloat
    {
        get
        {
            Check(SDL_GetRenderDrawColorFloat(Handle, out var r, out var g, out var b, out var a));
            return new FColor(r, g, b, a);
        }
        set => Check(SDL_SetRenderDrawColorFloat(Handle, value.R, value.G, value.B, value.A));
    }

    /// <summary>
    /// Gets or sets the blend mode used for drawing operations.
    /// </summary>
    public BlendMode DrawBlendMode
    {
        get
        {
            Check(SDL_GetRenderDrawBlendMode(Handle, out var mode));
            return (BlendMode)(uint)mode;
        }
        set => Check(SDL_SetRenderDrawBlendMode(Handle, (Native.SDL_BlendMode)(uint)value));
    }

    /// <summary>
    /// Clears the current rendering target with the drawing color.
    /// </summary>
    public void Clear() => Check(SDL_RenderClear(Handle));

    /// <summary>
    /// Updates the screen with any rendering performed since the previous call.
    /// </summary>
    public void Present() => Check(SDL_RenderPresent(Handle));

    // --- Drawing ---

    /// <summary>
    /// Draws a point on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="x">The x coordinate of the point.</param>
    /// <param name="y">The y coordinate of the point.</param>
    public void DrawPoint(float x, float y) => Check(SDL_RenderPoint(Handle, x, y));

    /// <summary>
    /// Draws a series of points on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="points">The points to draw.</param>
    public void DrawPoints(ReadOnlySpan<FPoint> points)
    {
        fixed (FPoint* ptr = points)
        {
            Check(SDL_RenderPoints(Handle, (SDL_FPoint*)ptr, points.Length));
        }
    }

    /// <summary>
    /// Draws a line on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="x1">The x coordinate of the start point.</param>
    /// <param name="y1">The y coordinate of the start point.</param>
    /// <param name="x2">The x coordinate of the end point.</param>
    /// <param name="y2">The y coordinate of the end point.</param>
    public void DrawLine(float x1, float y1, float x2, float y2) =>
        Check(SDL_RenderLine(Handle, x1, y1, x2, y2));

    /// <summary>
    /// Draws a series of connected lines on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="points">The points along the lines.</param>
    public void DrawLines(ReadOnlySpan<FPoint> points)
    {
        fixed (FPoint* ptr = points)
        {
            Check(SDL_RenderLines(Handle, (SDL_FPoint*)ptr, points.Length));
        }
    }

    /// <summary>
    /// Draws a rectangle on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="rect">The destination rectangle, or <c>null</c> for the entire rendering target.</param>
    public void DrawRect(FRectangle? rect)
    {
        if (rect is { } r)
        {
            var native = r.ToNative();
            Check(SDL_RenderRect(Handle, &native));
        }
        else
        {
            Check(SDL_RenderRect(Handle, null));
        }
    }

    /// <summary>
    /// Draws a set of rectangles on the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="rects">The rectangles to draw.</param>
    public void DrawRects(ReadOnlySpan<FRectangle> rects)
    {
        fixed (FRectangle* ptr = rects)
        {
            Check(SDL_RenderRects(Handle, (SDL_FRect*)ptr, rects.Length));
        }
    }

    /// <summary>
    /// Fills a rectangle on the current rendering target with the drawing color at subpixel precision.
    /// </summary>
    /// <param name="rect">The destination rectangle, or <c>null</c> for the entire rendering target.</param>
    public void FillRect(FRectangle? rect)
    {
        if (rect is { } r)
        {
            var native = r.ToNative();
            Check(SDL_RenderFillRect(Handle, &native));
        }
        else
        {
            Check(SDL_RenderFillRect(Handle, null));
        }
    }

    /// <summary>
    /// Fills a set of rectangles on the current rendering target with the drawing color at subpixel precision.
    /// </summary>
    /// <param name="rects">The rectangles to fill.</param>
    public void FillRects(ReadOnlySpan<FRectangle> rects)
    {
        fixed (FRectangle* ptr = rects)
        {
            Check(SDL_RenderFillRects(Handle, (SDL_FRect*)ptr, rects.Length));
        }
    }

    /// <summary>
    /// Draws debug text to the renderer.
    /// </summary>
    /// <param name="x">The x coordinate where the text will be drawn.</param>
    /// <param name="y">The y coordinate where the text will be drawn.</param>
    /// <param name="text">The text to draw.</param>
    public void DrawDebugText(float x, float y, string text) =>
        Check(SDL_RenderDebugText(Handle, x, y, ToUtf8(text)));

    // --- Texture ---

    /// <summary>
    /// Creates a texture for this renderer.
    /// </summary>
    /// <param name="format">The pixel format.</param>
    /// <param name="access">The access pattern.</param>
    /// <param name="w">The width of the texture in pixels.</param>
    /// <param name="h">The height of the texture in pixels.</param>
    /// <returns>A new texture.</returns>
    public Texture CreateTexture(PixelFormat format, TextureAccess access, int w, int h) =>
        new(Check(SDL_CreateTexture(Handle, (SDL_PixelFormat)format, (SDL_TextureAccess)access, w, h)));

    /// <summary>
    /// Creates a texture from an existing surface.
    /// </summary>
    /// <param name="surface">The surface containing pixel data used to fill the texture.</param>
    /// <returns>A new texture.</returns>
    public Texture CreateTextureFromSurface(Surface surface) =>
        new(Check(SDL_CreateTextureFromSurface(Handle, surface.Handle)));

    /// <summary>
    /// Copies a portion of the texture to the current rendering target.
    /// </summary>
    /// <param name="texture">The source texture.</param>
    /// <param name="src">The source rectangle, or <c>null</c> for the entire texture.</param>
    /// <param name="dst">The destination rectangle, or <c>null</c> for the entire rendering target.</param>
    public void RenderTexture(Texture texture, FRectangle? src, FRectangle? dst)
    {
        SDL_FRect nSrc;
        SDL_FRect nDst;
        var pSrc = (SDL_FRect*)null;
        var pDst = (SDL_FRect*)null;

        if (src is { } s)
        {
            nSrc = s.ToNative();
            pSrc = &nSrc;
        }

        if (dst is { } d)
        {
            nDst = d.ToNative();
            pDst = &nDst;
        }

        Check(SDL_RenderTexture(Handle, texture.Handle, pSrc, pDst));
    }

    /// <summary>
    /// Copies a portion of the source texture to the current rendering target, with rotation and flipping.
    /// </summary>
    /// <param name="texture">The source texture.</param>
    /// <param name="src">The source rectangle, or <c>null</c> for the entire texture.</param>
    /// <param name="dst">The destination rectangle, or <c>null</c> for the entire rendering target.</param>
    /// <param name="angle">An angle in degrees that indicates the rotation that will be applied to dst, rotating it in a clockwise direction.</param>
    /// <param name="center">The point around which dst will be rotated, or <c>null</c> to rotate around the center.</param>
    /// <param name="flip">A flip mode value stating which flipping actions should be performed.</param>
    public void RenderTextureRotated(Texture texture, FRectangle? src, FRectangle? dst, double angle, FPoint? center, FlipMode flip)
    {
        SDL_FRect nSrc;
        SDL_FRect nDst;
        SDL_FPoint nCenter;
        var pSrc = (SDL_FRect*)null;
        var pDst = (SDL_FRect*)null;
        var pCenter = (SDL_FPoint*)null;

        if (src is { } s)
        {
            nSrc = s.ToNative();
            pSrc = &nSrc;
        }

        if (dst is { } d)
        {
            nDst = d.ToNative();
            pDst = &nDst;
        }

        if (center is { } c)
        {
            nCenter = c.ToNative();
            pCenter = &nCenter;
        }

        Check(SDL_RenderTextureRotated(Handle, texture.Handle, pSrc, pDst, angle, pCenter, (SDL_FlipMode)flip));
    }

    // --- Texture rendering (advanced) ---

    /// <summary>
    /// Copies a portion of the texture to the current rendering target with an affine transform.
    /// </summary>
    /// <param name="texture">The source texture.</param>
    /// <param name="src">The source rectangle, or <c>null</c> for the entire texture.</param>
    /// <param name="origin">The point in the rendering target where the origin of the source will be drawn.</param>
    /// <param name="right">The point in the rendering target where the right edge of the source will be drawn.</param>
    /// <param name="down">The point in the rendering target where the bottom edge of the source will be drawn.</param>
    public void RenderTextureAffine(Texture texture, FRectangle? src, FPoint? origin, FPoint? right, FPoint? down)
    {
        SDL_FRect nSrc;
        SDL_FPoint nOrigin, nRight, nDown;
        var pSrc = (SDL_FRect*)null;
        var pOrigin = (SDL_FPoint*)null;
        var pRight = (SDL_FPoint*)null;
        var pDown = (SDL_FPoint*)null;

        if (src is { } s) { nSrc = s.ToNative(); pSrc = &nSrc; }
        if (origin is { } o) { nOrigin = o.ToNative(); pOrigin = &nOrigin; }
        if (right is { } r) { nRight = r.ToNative(); pRight = &nRight; }
        if (down is { } d) { nDown = d.ToNative(); pDown = &nDown; }

        Check(SDL_RenderTextureAffine(Handle, texture.Handle, pSrc, pOrigin, pRight, pDown));
    }

    /// <summary>
    /// Tiles a portion of the texture to the current rendering target.
    /// </summary>
    /// <param name="texture">The source texture.</param>
    /// <param name="src">The source rectangle, or <c>null</c> for the entire texture.</param>
    /// <param name="scale">The scale used to transform the source texture region.</param>
    /// <param name="dst">The destination rectangle, or <c>null</c> for the entire rendering target.</param>
    public void RenderTextureTiled(Texture texture, FRectangle? src, float scale, FRectangle? dst)
    {
        SDL_FRect nSrc, nDst;
        var pSrc = (SDL_FRect*)null;
        var pDst = (SDL_FRect*)null;

        if (src is { } s) { nSrc = s.ToNative(); pSrc = &nSrc; }
        if (dst is { } d) { nDst = d.ToNative(); pDst = &nDst; }

        Check(SDL_RenderTextureTiled(Handle, texture.Handle, pSrc, scale, pDst));
    }

    /// <summary>
    /// Performs a scaled 9-grid blit of a texture to the current rendering target.
    /// </summary>
    /// <param name="texture">The source texture.</param>
    /// <param name="src">The source rectangle, or <c>null</c> for the entire texture.</param>
    /// <param name="leftWidth">The width of the left corners in source coordinates.</param>
    /// <param name="rightWidth">The width of the right corners in source coordinates.</param>
    /// <param name="topHeight">The height of the top corners in source coordinates.</param>
    /// <param name="bottomHeight">The height of the bottom corners in source coordinates.</param>
    /// <param name="scale">The scale used to transform the corner sizes.</param>
    /// <param name="dst">The destination rectangle, or <c>null</c> for the entire rendering target.</param>
    public void RenderTexture9Grid(Texture texture, FRectangle? src, float leftWidth, float rightWidth,
        float topHeight, float bottomHeight, float scale, FRectangle? dst)
    {
        SDL_FRect nSrc, nDst;
        var pSrc = (SDL_FRect*)null;
        var pDst = (SDL_FRect*)null;

        if (src is { } s) { nSrc = s.ToNative(); pSrc = &nSrc; }
        if (dst is { } d) { nDst = d.ToNative(); pDst = &nDst; }

        Check(SDL_RenderTexture9Grid(Handle, texture.Handle, pSrc, leftWidth, rightWidth, topHeight, bottomHeight, scale, pDst));
    }

    // --- Geometry ---

    /// <summary>
    /// Renders a list of triangles, optionally using a texture and indices into the vertex array.
    /// </summary>
    /// <param name="texture">The texture to use, or <c>null</c> for untextured geometry.</param>
    /// <param name="vertices">The vertices to render.</param>
    /// <param name="indices">The indices into the vertex array, or an empty span for sequential rendering.</param>
    public void RenderGeometry(Texture? texture, ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices)
    {
        fixed (Vertex* vPtr = vertices)
        fixed (int* iPtr = indices)
        {
            Check(SDL_RenderGeometry(Handle,
                texture is { } t ? t.Handle : null,
                (SDL_Vertex*)vPtr, vertices.Length,
                indices.Length > 0 ? iPtr : null, indices.Length));
        }
    }

    // --- Read pixels / flush ---

    /// <summary>
    /// Reads pixels from the current rendering target to a new surface.
    /// </summary>
    /// <param name="rect">The area to read, or <c>null</c> for the entire rendering target.</param>
    /// <returns>A new surface containing the pixel data.</returns>
    public Surface ReadPixels(Rectangle? rect)
    {
        if (rect is { } r)
        {
            var native = r.ToNative();
            return new Surface(Check(SDL_RenderReadPixels(Handle, &native)));
        }

        return new Surface(Check(SDL_RenderReadPixels(Handle, null)));
    }

    /// <summary>
    /// Forces the rendering context to flush any pending commands and state.
    /// </summary>
    public void Flush() => Check(SDL_FlushRenderer(Handle));

    // --- Coordinate conversion ---

    /// <summary>
    /// Gets the point in render coordinates associated with window coordinates.
    /// </summary>
    /// <param name="windowX">The X coordinate in window space.</param>
    /// <param name="windowY">The Y coordinate in window space.</param>
    /// <returns>The corresponding point in render coordinates.</returns>
    public FPoint CoordinatesFromWindow(float windowX, float windowY)
    {
        Check(SDL_RenderCoordinatesFromWindow(Handle, windowX, windowY, out var x, out var y));
        return new FPoint(x, y);
    }

    /// <summary>
    /// Gets the window coordinates associated with render coordinates.
    /// </summary>
    /// <param name="x">The X coordinate in render space.</param>
    /// <param name="y">The Y coordinate in render space.</param>
    /// <returns>The corresponding point in window coordinates.</returns>
    public FPoint CoordinatesToWindow(float x, float y)
    {
        Check(SDL_RenderCoordinatesToWindow(Handle, x, y, out var wx, out var wy));
        return new FPoint(wx, wy);
    }

    // --- Viewport / clipping ---

    /// <summary>
    /// Gets or sets the drawing area for rendering on the current target.
    /// Set to <c>null</c> to use the entire target.
    /// </summary>
    public Rectangle? Viewport
    {
        get
        {
            Check(SDL_GetRenderViewport(Handle, out var rect));
            // SDL returns an empty rect for "no viewport set" - check for that
            return rect.w == 0 && rect.h == 0 ? null : Rectangle.FromNative(rect);
        }
        set
        {
            if (value is { } v)
            {
                var native = v.ToNative();
                Check(SDL_SetRenderViewport(Handle, &native));
            }
            else
            {
                Check(SDL_SetRenderViewport(Handle, null));
            }
        }
    }

    /// <summary>
    /// Gets or sets the clip rectangle for rendering on the current target.
    /// Set to <c>null</c> to disable clipping.
    /// </summary>
    public Rectangle? ClipRect
    {
        get
        {
            if (!SDL_RenderClipEnabled(Handle))
            {
                return null;
            }

            Check(SDL_GetRenderClipRect(Handle, out var rect));
            return Rectangle.FromNative(rect);
        }
        set
        {
            if (value is { } v)
            {
                var native = v.ToNative();
                Check(SDL_SetRenderClipRect(Handle, &native));
            }
            else
            {
                Check(SDL_SetRenderClipRect(Handle, null));
            }
        }
    }

    /// <summary>
    /// Gets whether clipping is enabled on this renderer.
    /// </summary>
    public bool ClipEnabled => SDL_RenderClipEnabled(Handle);

    // --- Scale / target / logical / vsync ---

    /// <summary>
    /// Gets or sets the drawing scale for rendering on the current target.
    /// </summary>
    public (float X, float Y) Scale
    {
        get
        {
            Check(SDL_GetRenderScale(Handle, out var x, out var y));
            return (x, y);
        }
        set => Check(SDL_SetRenderScale(Handle, value.X, value.Y));
    }

    /// <summary>
    /// Gets or sets the current render target. Set to <c>null</c> for the default target.
    /// The returned texture is non-owning.
    /// </summary>
    public Texture? Target
    {
        get
        {
            var ptr = SDL_GetRenderTarget(Handle);
            return ptr == null ? null : new Texture(ptr, ownsHandle: false);
        }
        set => Check(SDL_SetRenderTarget(Handle, value is { } t ? t.Handle : null));
    }

    /// <summary>
    /// Sets a device independent resolution and presentation mode for rendering.
    /// </summary>
    /// <param name="w">The width of the logical resolution.</param>
    /// <param name="h">The height of the logical resolution.</param>
    /// <param name="mode">The presentation mode.</param>
    public void SetLogicalPresentation(int w, int h, LogicalPresentation mode) =>
        Check(SDL_SetRenderLogicalPresentation(Handle, w, h, (SDL_RendererLogicalPresentation)mode));

    /// <summary>
    /// Gets the device independent resolution and presentation mode for rendering.
    /// </summary>
    /// <returns>A tuple of width, height, and presentation mode.</returns>
    public (int W, int H, LogicalPresentation Mode) GetLogicalPresentation()
    {
        Check(SDL_GetRenderLogicalPresentation(Handle, out var w, out var h, out var mode));
        return (w, h, (LogicalPresentation)mode);
    }

    /// <summary>
    /// Gets or sets the texture address mode used for rendering.
    /// </summary>
    public (TextureAddressMode U, TextureAddressMode V) TextureAddressMode
    {
        get
        {
            Check(SDL_GetRenderTextureAddressMode(Handle, out var u, out var v));
            return ((TextureAddressMode)u, (TextureAddressMode)v);
        }
        set => Check(SDL_SetRenderTextureAddressMode(Handle, (SDL_TextureAddressMode)value.U, (SDL_TextureAddressMode)value.V));
    }

    /// <summary>
    /// Gets or sets VSync for this renderer.
    /// </summary>
    public int VSync
    {
        get
        {
            Check(SDL_GetRenderVSync(Handle, out var vsync));
            return vsync;
        }
        set => Check(SDL_SetRenderVSync(Handle, value));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHandle && Handle != null)
        {
            SDL_DestroyRenderer(Handle);
            Handle = null;
        }
    }
}
