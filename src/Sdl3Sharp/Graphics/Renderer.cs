using static Sdl3Sharp.Native.BlendMode;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Rect;
using static Sdl3Sharp.Native.Render;
using static Sdl3Sharp.Native.Surface;
using static Sdl3Sharp.Native.Video;

namespace Sdl3Sharp.Graphics;

/// <summary>
/// Represents an SDL 2D rendering context.
/// </summary>
public sealed unsafe class Renderer : IDisposable
{
    /// <summary>
    /// Occurs when render targets have been reset and their contents need to be updated.
    /// </summary>
    public static event EventHandler<RenderEventArgs>? TargetsReset;

    /// <summary>
    /// Occurs when the render device has been reset and all textures need to be recreated.
    /// </summary>
    public static event EventHandler<RenderEventArgs>? DeviceReset;

    /// <summary>
    /// Occurs when the render device has been lost and can't be recovered.
    /// </summary>
    public static event EventHandler<RenderEventArgs>? DeviceLost;

    internal static void DispatchEvent(Event e)
    {
        switch (e.Type)
        {
            case EventType.RenderTargetsReset:
                TargetsReset?.Invoke(null, (RenderEventArgs)e.TranslateEvent());
                break;
            case EventType.RenderDeviceReset:
                DeviceReset?.Invoke(null, (RenderEventArgs)e.TranslateEvent());
                break;
            case EventType.RenderDeviceLost:
                DeviceLost?.Invoke(null, (RenderEventArgs)e.TranslateEvent());
                break;
        }
    }

    private bool _disposed;
    private readonly bool _ownsHandle;

    /// <summary>
    /// Gets the underlying SDL_Renderer pointer.
    /// </summary>
    public SDL_Renderer* Handle { get; private set; }

    /// <summary>
    /// Creates a new renderer wrapping an existing SDL_Renderer pointer.
    /// </summary>
    /// <param name="handle">The SDL_Renderer pointer.</param>
    /// <param name="ownsHandle">Whether this instance owns the handle and should free it on disposal.</param>
    internal Renderer(SDL_Renderer* handle, bool ownsHandle)
    {
        Handle = handle;
        _ownsHandle = ownsHandle;
    }

    /// <summary>
    /// Creates a 2D rendering context for a window.
    /// </summary>
    /// <param name="window">The window where rendering is displayed.</param>
    /// <param name="name">The name of the rendering driver to use, or null to let SDL choose.</param>
    public Renderer(Window window, string? name = null)
    {
        ArgumentNullException.ThrowIfNull(window);
        Handle = CheckErrorPointer(SDL_CreateRenderer(window.Handle, name));
        _ownsHandle = true;
    }

    /// <summary>
    /// Creates a 2D rendering context for a window with specified properties.
    /// </summary>
    /// <param name="properties">The properties to use.</param>
    public Renderer(PropertyGroup properties)
    {
        ArgumentNullException.ThrowIfNull(properties);
        Handle = CheckErrorPointer(SDL_CreateRendererWithProperties(properties.Id));
        _ownsHandle = true;
    }

    /// <summary>
    /// Creates a 2D software rendering context for a surface.
    /// </summary>
    /// <param name="surface">The surface where rendering is done.</param>
    public Renderer(Surface surface)
    {
        ArgumentNullException.ThrowIfNull(surface);
        Handle = CheckErrorPointer(SDL_CreateSoftwareRenderer(surface.Handle));
        _ownsHandle = true;
    }

    /// <summary>
    /// Gets the number of 2D rendering drivers available.
    /// </summary>
    public static int DriverCount => SDL_GetNumRenderDrivers();

    /// <summary>
    /// Gets the name of a rendering driver by index.
    /// </summary>
    /// <param name="index">The driver index.</param>
    /// <returns>The driver name, or null if the index is invalid.</returns>
    public static string? GetDriverName(int index)
    {
        return SDL_GetRenderDriver(index);
    }

    /// <summary>
    /// Creates a window and default renderer.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="size">The size of the window.</param>
    /// <param name="flags">The window flags.</param>
    /// <returns>A tuple containing the window and renderer.</returns>
    public static (Window Window, Renderer Renderer) CreateWindowAndRenderer(string title, Size size, WindowFlags flags = WindowFlags.None)
    {
        SDL_Window* window;
        SDL_Renderer* renderer;
        _ = CheckErrorBool(SDL_CreateWindowAndRenderer(title, size.Width, size.Height, (SDL_WindowFlags)flags, &window, &renderer));
        return (new Window(window), new Renderer(renderer, ownsHandle: true));
    }

    /// <summary>
    /// Gets the renderer associated with a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The renderer, or null if none is associated.</returns>
    public static Renderer? GetRenderer(Window window)
    {
        ArgumentNullException.ThrowIfNull(window);
        SDL_Renderer* renderer = SDL_GetRenderer(window.Handle);
        return renderer != null ? new Renderer(renderer, ownsHandle: false) : null;
    }

    /// <summary>
    /// Gets the name of this renderer.
    /// </summary>
    public string? Name
    {
        get
        {
            ThrowIfDisposed();
            return SDL_GetRendererName(Handle);
        }
    }

    /// <summary>
    /// Gets the properties associated with this renderer.
    /// </summary>
    public PropertyGroup Properties
    {
        get
        {
            ThrowIfDisposed();
            return new(CheckErrorZero(SDL_GetRendererProperties(Handle)), ownsHandle: false);
        }
    }

    /// <summary>
    /// Gets the window associated with this renderer.
    /// </summary>
    public Window? Window
    {
        get
        {
            ThrowIfDisposed();
            SDL_Window* window = SDL_GetRenderWindow(Handle);
            return window != null ? new Window(window) : null;
        }
    }

    /// <summary>
    /// Gets the output size in pixels of the rendering context.
    /// </summary>
    public Size OutputSize
    {
        get
        {
            ThrowIfDisposed();
            int w, h;
            _ = CheckErrorBool(SDL_GetRenderOutputSize(Handle, &w, &h));
            return new(w, h);
        }
    }

    /// <summary>
    /// Gets the current output size in pixels, taking into account render targets and logical presentation.
    /// </summary>
    public Size CurrentOutputSize
    {
        get
        {
            ThrowIfDisposed();
            int w, h;
            _ = CheckErrorBool(SDL_GetCurrentRenderOutputSize(Handle, &w, &h));
            return new(w, h);
        }
    }

    /// <summary>
    /// Creates a texture for this renderer.
    /// </summary>
    /// <param name="format">The pixel format.</param>
    /// <param name="access">The texture access pattern.</param>
    /// <param name="size">The size in pixels.</param>
    /// <returns>A new texture.</returns>
    public Texture CreateTexture(PixelFormat format, TextureAccess access, Size size)
    {
        ThrowIfDisposed();
        return new Texture(CheckErrorPointer(SDL_CreateTexture(Handle, format.Format, (SDL_TextureAccess)access, size.Width, size.Height)), ownsHandle: true);
    }

    /// <summary>
    /// Creates a texture from a surface.
    /// </summary>
    /// <param name="surface">The surface containing pixel data.</param>
    /// <returns>A new texture.</returns>
    public Texture CreateTextureFromSurface(Surface surface)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(surface);
        return new Texture(CheckErrorPointer(SDL_CreateTextureFromSurface(Handle, surface.Handle)), ownsHandle: true);
    }

    /// <summary>
    /// Creates a texture with specified properties.
    /// </summary>
    /// <param name="properties">The properties to use.</param>
    /// <returns>A new texture.</returns>
    public Texture CreateTextureWithProperties(PropertyGroup properties)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(properties);
        return new Texture(CheckErrorPointer(SDL_CreateTextureWithProperties(Handle, properties.Id)), ownsHandle: true);
    }

    /// <summary>
    /// Gets or sets the current render target. Null means the default render target (the window).
    /// </summary>
    public Texture? RenderTarget
    {
        get
        {
            ThrowIfDisposed();
            SDL_Texture* texture = SDL_GetRenderTarget(Handle);
            return texture != null ? new Texture(texture, ownsHandle: false) : null;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetRenderTarget(Handle, value != null ? value.Handle : null));
        }
    }

    /// <summary>
    /// Sets a device-independent resolution and presentation mode for rendering.
    /// </summary>
    /// <param name="size">The logical resolution size.</param>
    /// <param name="mode">The presentation mode.</param>
    public void SetLogicalPresentation(Size size, LogicalPresentation mode)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_SetRenderLogicalPresentation(Handle, size.Width, size.Height, (SDL_RendererLogicalPresentation)mode));
    }

    /// <summary>
    /// Gets the logical presentation settings.
    /// </summary>
    /// <param name="size">Receives the logical resolution size.</param>
    /// <param name="mode">Receives the presentation mode.</param>
    public void GetLogicalPresentation(out Size size, out LogicalPresentation mode)
    {
        ThrowIfDisposed();
        int w, h;
        SDL_RendererLogicalPresentation m;
        _ = CheckErrorBool(SDL_GetRenderLogicalPresentation(Handle, &w, &h, &m));
        size = new(w, h);
        mode = (LogicalPresentation)m;
    }

    /// <summary>
    /// Gets the final presentation rectangle for rendering.
    /// </summary>
    public RectangleF LogicalPresentationRect
    {
        get
        {
            ThrowIfDisposed();
            SDL_FRect rect;
            _ = CheckErrorBool(SDL_GetRenderLogicalPresentationRect(Handle, &rect));
            return new(new PointF(rect.x, rect.y), new SizeF(rect.w, rect.h));
        }
    }

    /// <summary>
    /// Converts window coordinates to render coordinates.
    /// </summary>
    /// <param name="windowPoint">The point in window coordinates.</param>
    /// <returns>The point in render coordinates.</returns>
    public PointF CoordinatesFromWindow(PointF windowPoint)
    {
        ThrowIfDisposed();
        float x, y;
        _ = CheckErrorBool(SDL_RenderCoordinatesFromWindow(Handle, windowPoint.X, windowPoint.Y, &x, &y));
        return new(x, y);
    }

    /// <summary>
    /// Converts render coordinates to window coordinates.
    /// </summary>
    /// <param name="renderPoint">The point in render coordinates.</param>
    /// <returns>The point in window coordinates.</returns>
    public PointF CoordinatesToWindow(PointF renderPoint)
    {
        ThrowIfDisposed();
        float x, y;
        _ = CheckErrorBool(SDL_RenderCoordinatesToWindow(Handle, renderPoint.X, renderPoint.Y, &x, &y));
        return new(x, y);
    }

    /// <summary>
    /// Gets or sets the drawing area for rendering.
    /// </summary>
    public Rectangle? Viewport
    {
        get
        {
            ThrowIfDisposed();
            SDL_Rect rect;
            _ = CheckErrorBool(SDL_GetRenderViewport(Handle, &rect));
            return new Rectangle(rect);
        }
        set
        {
            ThrowIfDisposed();
            SDL_Rect nativeRect;
            _ = CheckErrorBool(SDL_SetRenderViewport(Handle, Rectangle.ToNative(value, &nativeRect)));
        }
    }

    /// <summary>
    /// Gets whether an explicit viewport was set.
    /// </summary>
    public bool ViewportSet
    {
        get
        {
            ThrowIfDisposed();
            return SDL_RenderViewportSet(Handle);
        }
    }

    /// <summary>
    /// Gets the safe area for rendering within the current viewport.
    /// </summary>
    public Rectangle SafeArea
    {
        get
        {
            ThrowIfDisposed();
            SDL_Rect rect;
            _ = CheckErrorBool(SDL_GetRenderSafeArea(Handle, &rect));
            return new Rectangle(rect);
        }
    }

    /// <summary>
    /// Gets or sets the clip rectangle for rendering.
    /// </summary>
    public Rectangle? ClipRect
    {
        get
        {
            ThrowIfDisposed();
            SDL_Rect rect;
            _ = CheckErrorBool(SDL_GetRenderClipRect(Handle, &rect));
            return new Rectangle(rect);
        }
        set
        {
            ThrowIfDisposed();
            SDL_Rect nativeRect;
            _ = CheckErrorBool(SDL_SetRenderClipRect(Handle, Rectangle.ToNative(value, &nativeRect)));
        }
    }

    /// <summary>
    /// Gets whether clipping is enabled.
    /// </summary>
    public bool ClipEnabled
    {
        get
        {
            ThrowIfDisposed();
            return SDL_RenderClipEnabled(Handle);
        }
    }

    /// <summary>
    /// Gets or sets the drawing scale.
    /// </summary>
    public (float X, float Y) Scale
    {
        get
        {
            ThrowIfDisposed();
            float x, y;
            _ = CheckErrorBool(SDL_GetRenderScale(Handle, &x, &y));
            return (x, y);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetRenderScale(Handle, value.X, value.Y));
        }
    }

    /// <summary>
    /// Gets or sets the draw color.
    /// </summary>
    public Color DrawColor
    {
        get
        {
            ThrowIfDisposed();
            byte r, g, b, a;
            _ = CheckErrorBool(SDL_GetRenderDrawColor(Handle, &r, &g, &b, &a));
            return new(r, g, b, a);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetRenderDrawColor(Handle, value.Red, value.Green, value.Blue, value.Alpha));
        }
    }

    /// <summary>
    /// Gets or sets the draw color as floating point values.
    /// </summary>
    public ColorF DrawColorFloat
    {
        get
        {
            ThrowIfDisposed();
            float r, g, b, a;
            _ = CheckErrorBool(SDL_GetRenderDrawColorFloat(Handle, &r, &g, &b, &a));
            return new(r, g, b, a);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetRenderDrawColorFloat(Handle, value.Red, value.Green, value.Blue, value.Alpha));
        }
    }

    /// <summary>
    /// Gets or sets the color scale used for render operations.
    /// </summary>
    public float ColorScale
    {
        get
        {
            ThrowIfDisposed();
            float scale;
            _ = CheckErrorBool(SDL_GetRenderColorScale(Handle, &scale));
            return scale;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetRenderColorScale(Handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the blend mode for drawing operations.
    /// </summary>
    public BlendMode DrawBlendMode
    {
        get
        {
            ThrowIfDisposed();
            SDL_BlendMode mode;
            _ = CheckErrorBool(SDL_GetRenderDrawBlendMode(Handle, &mode));
            return new BlendMode(mode);
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetRenderDrawBlendMode(Handle, value.ToNative()));
        }
    }

    /// <summary>
    /// Clears the current rendering target with the draw color.
    /// </summary>
    public void Clear()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_RenderClear(Handle));
    }

    /// <summary>
    /// Draws a point.
    /// </summary>
    /// <param name="point">The point to draw.</param>
    public void DrawPoint(PointF point)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_RenderPoint(Handle, point.X, point.Y));
    }

    /// <summary>
    /// Draws multiple points.
    /// </summary>
    /// <param name="points">The points to draw.</param>
    public void DrawPoints(ReadOnlySpan<PointF> points)
    {
        ThrowIfDisposed();
        if (points.Length == 0)
        {
            return;
        }

        fixed (PointF* pointsPtr = points)
        {
            _ = CheckErrorBool(SDL_RenderPoints(Handle, (SDL_FPoint*)pointsPtr, points.Length));
        }
    }

    /// <summary>
    /// Draws a line.
    /// </summary>
    /// <param name="start">The start point.</param>
    /// <param name="end">The end point.</param>
    public void DrawLine(PointF start, PointF end)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_RenderLine(Handle, start.X, start.Y, end.X, end.Y));
    }

    /// <summary>
    /// Draws a line.
    /// </summary>
    /// <param name="line">The line to draw.</param>
    public void DrawLine(LineF line)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_RenderLine(Handle, line.Start.X, line.Start.Y, line.End.X, line.End.Y));
    }

    /// <summary>
    /// Draws a series of connected lines.
    /// </summary>
    /// <param name="points">The points along the lines.</param>
    public void DrawLines(ReadOnlySpan<PointF> points)
    {
        ThrowIfDisposed();
        if (points.Length == 0)
        {
            return;
        }

        fixed (PointF* pointsPtr = points)
        {
            _ = CheckErrorBool(SDL_RenderLines(Handle, (SDL_FPoint*)pointsPtr, points.Length));
        }
    }

    /// <summary>
    /// Draws a rectangle outline.
    /// </summary>
    /// <param name="rect">The rectangle to draw, or null to outline the entire target.</param>
    public void DrawRect(RectangleF? rect)
    {
        ThrowIfDisposed();
        SDL_FRect nativeRect;
        _ = CheckErrorBool(SDL_RenderRect(Handle, RectangleF.ToNative(rect, &nativeRect)));
    }

    /// <summary>
    /// Draws multiple rectangle outlines.
    /// </summary>
    /// <param name="rects">The rectangles to draw.</param>
    public void DrawRects(ReadOnlySpan<RectangleF> rects)
    {
        ThrowIfDisposed();
        if (rects.Length == 0)
        {
            return;
        }

        fixed (RectangleF* rectsPtr = rects)
        {
            _ = CheckErrorBool(SDL_RenderRects(Handle, (SDL_FRect*)rectsPtr, rects.Length));
        }
    }

    /// <summary>
    /// Fills a rectangle with the draw color.
    /// </summary>
    /// <param name="rect">The rectangle to fill, or null to fill the entire target.</param>
    public void FillRect(RectangleF? rect)
    {
        ThrowIfDisposed();
        SDL_FRect nativeRect;
        _ = CheckErrorBool(SDL_RenderFillRect(Handle, RectangleF.ToNative(rect, &nativeRect)));
    }

    /// <summary>
    /// Fills multiple rectangles with the draw color.
    /// </summary>
    /// <param name="rects">The rectangles to fill.</param>
    public void FillRects(ReadOnlySpan<RectangleF> rects)
    {
        ThrowIfDisposed();
        if (rects.Length == 0)
        {
            return;
        }

        fixed (RectangleF* rectsPtr = rects)
        {
            _ = CheckErrorBool(SDL_RenderFillRects(Handle, (SDL_FRect*)rectsPtr, rects.Length));
        }
    }

    /// <summary>
    /// Copies a texture to the rendering target.
    /// </summary>
    /// <param name="texture">The source texture.</param>
    /// <param name="srcRect">The source rectangle, or null for the entire texture.</param>
    /// <param name="dstRect">The destination rectangle, or null for the entire target.</param>
    public void RenderTexture(Texture texture, RectangleF? srcRect = null, RectangleF? dstRect = null)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(texture);
        SDL_FRect srcNative, dstNative;
        _ = CheckErrorBool(SDL_RenderTexture(Handle, texture.Handle, RectangleF.ToNative(srcRect, &srcNative), RectangleF.ToNative(dstRect, &dstNative)));
    }

    /// <summary>
    /// Copies a texture to the rendering target with rotation and flipping.
    /// </summary>
    /// <param name="texture">The source texture.</param>
    /// <param name="srcRect">The source rectangle, or null for the entire texture.</param>
    /// <param name="dstRect">The destination rectangle, or null for the entire target.</param>
    /// <param name="angle">The rotation angle in degrees (clockwise).</param>
    /// <param name="center">The rotation center, or null for the center of dstRect.</param>
    /// <param name="flip">The flip mode.</param>
    public void RenderTextureRotated(Texture texture, RectangleF? srcRect, RectangleF? dstRect, double angle, PointF? center = null, FlipMode flip = FlipMode.None)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(texture);
        SDL_FRect srcNative, dstNative;
        SDL_FPoint centerNative;
        SDL_FPoint* centerPtr = null;
        if (center.HasValue)
        {
            centerNative = new SDL_FPoint { x = center.Value.X, y = center.Value.Y };
            centerPtr = &centerNative;
        }

        _ = CheckErrorBool(SDL_RenderTextureRotated(Handle, texture.Handle, RectangleF.ToNative(srcRect, &srcNative), RectangleF.ToNative(dstRect, &dstNative), angle, centerPtr, (SDL_FlipMode)flip));
    }

    /// <summary>
    /// Copies a texture with affine transformation.
    /// </summary>
    /// <param name="texture">The source texture.</param>
    /// <param name="srcRect">The source rectangle, or null for the entire texture.</param>
    /// <param name="origin">The origin of the transform.</param>
    /// <param name="right">The right edge direction.</param>
    /// <param name="down">The down edge direction.</param>
    public void RenderTextureAffine(Texture texture, RectangleF? srcRect, PointF origin, PointF right, PointF down)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(texture);
        SDL_FRect srcNative;
        SDL_FPoint originNative = new() { x = origin.X, y = origin.Y };
        SDL_FPoint rightNative = new() { x = right.X, y = right.Y };
        SDL_FPoint downNative = new() { x = down.X, y = down.Y };
        _ = CheckErrorBool(SDL_RenderTextureAffine(Handle, texture.Handle, RectangleF.ToNative(srcRect, &srcNative), &originNative, &rightNative, &downNative));
    }

    /// <summary>
    /// Tiles a texture to fill the destination rectangle.
    /// </summary>
    /// <param name="texture">The source texture.</param>
    /// <param name="srcRect">The source rectangle, or null for the entire texture.</param>
    /// <param name="scale">The scale factor.</param>
    /// <param name="dstRect">The destination rectangle, or null for the entire target.</param>
    public void RenderTextureTiled(Texture texture, RectangleF? srcRect, float scale, RectangleF? dstRect)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(texture);
        SDL_FRect srcNative, dstNative;
        _ = CheckErrorBool(SDL_RenderTextureTiled(Handle, texture.Handle, RectangleF.ToNative(srcRect, &srcNative), scale, RectangleF.ToNative(dstRect, &dstNative)));
    }

    /// <summary>
    /// Renders using the 9-grid algorithm.
    /// </summary>
    /// <param name="texture">The source texture.</param>
    /// <param name="srcRect">The source rectangle for the 9-grid.</param>
    /// <param name="leftWidth">The width of the left corners.</param>
    /// <param name="rightWidth">The width of the right corners.</param>
    /// <param name="topHeight">The height of the top corners.</param>
    /// <param name="bottomHeight">The height of the bottom corners.</param>
    /// <param name="scale">The scale for the corners.</param>
    /// <param name="dstRect">The destination rectangle.</param>
    public void RenderTexture9Grid(Texture texture, RectangleF? srcRect, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, RectangleF? dstRect)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(texture);
        SDL_FRect srcNative, dstNative;
        _ = CheckErrorBool(SDL_RenderTexture9Grid(Handle, texture.Handle, RectangleF.ToNative(srcRect, &srcNative), leftWidth, rightWidth, topHeight, bottomHeight, scale, RectangleF.ToNative(dstRect, &dstNative)));
    }

    /// <summary>
    /// Renders a list of triangles.
    /// </summary>
    /// <param name="texture">The texture to use, or null for solid color.</param>
    /// <param name="vertices">The vertices.</param>
    /// <param name="indices">The indices into the vertex array, or null to use vertices in order.</param>
    public void RenderGeometry(Texture? texture, ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices = default)
    {
        ThrowIfDisposed();
        fixed (Vertex* verticesPtr = vertices)
        fixed (int* indicesPtr = indices)
        {
            _ = CheckErrorBool(SDL_RenderGeometry(Handle, texture != null ? texture.Handle : null, (SDL_Vertex*)verticesPtr, vertices.Length, indicesPtr, indices.Length));
        }
    }

    /// <summary>
    /// Reads pixels from the current rendering target.
    /// </summary>
    /// <param name="rect">The area to read, or null for the entire target.</param>
    /// <returns>A new surface containing the pixels.</returns>
    public Surface ReadPixels(Rectangle? rect = null)
    {
        ThrowIfDisposed();
        SDL_Rect nativeRect;
        return new Surface(CheckErrorPointer(SDL_RenderReadPixels(Handle, Rectangle.ToNative(rect, &nativeRect))), ownsHandle: true);
    }

    /// <summary>
    /// Updates the screen with any rendering performed since the previous call.
    /// </summary>
    public void Present()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_RenderPresent(Handle));
    }

    /// <summary>
    /// Forces any pending commands and state to be flushed.
    /// </summary>
    public void Flush()
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_FlushRenderer(Handle));
    }

    /// <summary>
    /// Gets or sets the VSync setting.
    /// </summary>
    public int VSync
    {
        get
        {
            ThrowIfDisposed();
            int vsync;
            _ = CheckErrorBool(SDL_GetRenderVSync(Handle, &vsync));
            return vsync;
        }
        set
        {
            ThrowIfDisposed();
            _ = CheckErrorBool(SDL_SetRenderVSync(Handle, value));
        }
    }

    /// <summary>
    /// Draws debug text to the renderer.
    /// </summary>
    /// <param name="position">The position to draw at.</param>
    /// <param name="text">The text to draw.</param>
    public void DrawDebugText(PointF position, string text)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_RenderDebugText(Handle, position.X, position.Y, text));
    }

    /// <summary>
    /// Gets the CAMetalLayer associated with a Metal renderer.
    /// </summary>
    /// <returns>The CAMetalLayer pointer, or null if not a Metal renderer.</returns>
    public void* GetMetalLayer()
    {
        ThrowIfDisposed();
        return SDL_GetRenderMetalLayer(Handle);
    }

    /// <summary>
    /// Gets the Metal command encoder for the current frame.
    /// </summary>
    /// <returns>The command encoder pointer, or null if not a Metal renderer.</returns>
    public void* GetMetalCommandEncoder()
    {
        ThrowIfDisposed();
        return SDL_GetRenderMetalCommandEncoder(Handle);
    }

    /// <summary>
    /// Adds Vulkan synchronization semaphores for the current frame.
    /// </summary>
    /// <param name="waitStageMask">The VkPipelineStageFlags for the wait.</param>
    /// <param name="waitSemaphore">A VkSemaphore to wait on, or 0 if not needed.</param>
    /// <param name="signalSemaphore">A VkSemaphore that SDL will signal when rendering is complete, or 0 if not needed.</param>
    public void AddVulkanRenderSemaphores(uint waitStageMask, long waitSemaphore, long signalSemaphore)
    {
        ThrowIfDisposed();
        _ = CheckErrorBool(SDL_AddVulkanRenderSemaphores(Handle, waitStageMask, waitSemaphore, signalSemaphore));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_ownsHandle && Handle != null)
        {
            SDL_DestroyRenderer(Handle);
        }

        Handle = null;
        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
