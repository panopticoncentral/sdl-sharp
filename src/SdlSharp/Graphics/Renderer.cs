namespace SdlSharp.Graphics
{
    /// <summary>
    /// A graphics renderer.
    /// </summary>
    public sealed unsafe class Renderer : IDisposable
    {
        private readonly Native.SDL_Renderer* _renderer;

        /// <summary>
        /// The renderer drivers available.
        /// </summary>
        public static IReadOnlyList<RendererInfo> RenderDrivers => Native.GetIndexedCollection(i =>
        {
            Native.SDL_RendererInfo info;
            _ = Native.CheckError(Native.SDL_GetRenderDriverInfo(i, &info));
            return new RendererInfo(&info);
        }, Native.SDL_GetNumRenderDrivers);

        /// <summary>
        /// The ID of the renderer.
        /// </summary>
        public nuint Id => (nuint)_renderer;

        /// <summary>
        /// Information about this renderer.
        /// </summary>
        public RendererInfo Info
        {
            get
            {
                Native.SDL_RendererInfo info;
                _ = Native.CheckError(Native.SDL_GetRendererInfo(_renderer, &info));
                return new(&info);
            }
        }

        /// <summary>
        /// Gets the window associated with this renderer.
        /// </summary>
        public Window? Window =>
            new(Native.SDL_GetRenderGetWindow(_renderer));

        /// <summary>
        /// The output size of this renderer.
        /// </summary>
        public Size OutputSize
        {
            get
            {
                int w, h;
                _ = Native.CheckError(Native.SDL_GetRendererOutputSize(_renderer, &w, &h));
                return new(w, h);
            }
        }

        /// <summary>
        /// Whether the renderer supports a target.
        /// </summary>
        public bool TargetSupported =>
            Native.SDL_RenderTargetSupported(_renderer);

        /// <summary>
        /// The target of the renderer.
        /// </summary>
        public Texture? Target
        {
            get => new(Native.SDL_GetRenderTarget(_renderer));
            set => Native.CheckError(Native.SDL_SetRenderTarget(_renderer, value == null ? null : value.ToNative()));
        }

        /// <summary>
        /// The logical size of the renderer.
        /// </summary>
        public Size LogicalSize
        {
            get
            {
                int w, h;
                Native.SDL_RenderGetLogicalSize(_renderer, &w, &h);
                return new(w, h);
            }

            set => Native.CheckError(Native.SDL_RenderSetLogicalSize(_renderer, value.Width, value.Height));
        }

        /// <summary>
        /// Whether integer scales are forced for resolution-independent rendering.
        /// </summary>
        public bool IntegerScale
        {
            get => Native.SDL_RenderGetIntegerScale(_renderer);
            set => Native.CheckError(Native.SDL_RenderSetIntegerScale(_renderer, value));
        }

        /// <summary>
        /// The viewport of the renderer, if any.
        /// </summary>
        public Rectangle? Viewport
        {
            get
            {
                Native.SDL_Rect rect;
                Native.SDL_RenderGetViewport(_renderer, &rect);
                return Native.SDL_RectEmpty(&rect) ? null : new(rect);
            }

            set
            {
                Native.SDL_Rect rect;
                _ = Native.CheckError(Native.SDL_RenderSetViewport(_renderer, Rectangle.ToNative(value, &rect)));
            }
        }

        /// <summary>
        /// Whether clipping is enabled for the renderer.
        /// </summary>
        public bool ClippingEnabled =>
            Native.SDL_RenderIsClipEnabled(_renderer);

        /// <summary>
        /// The clipping rectangle of the renderer.
        /// </summary>
        public Rectangle? ClippingRectangle
        {
            get
            {
                Native.SDL_Rect rect;
                Native.SDL_RenderGetClipRect(_renderer, &rect);
                return Native.SDL_RectEmpty(&rect) ? null : new(rect);
            }

            set
            {
                Native.SDL_Rect rect;
                _ = Native.CheckError(Native.SDL_RenderSetClipRect(_renderer, Rectangle.ToNative(value, &rect)));
            }
        }

        /// <summary>
        /// The scaling of the renderer.
        /// </summary>
        public (float X, float Y) Scale
        {
            get
            {
                float x, y;
                Native.SDL_RenderGetScale(_renderer, &x, &y);
                return (x, y);
            }

            set => Native.CheckError(Native.SDL_RenderSetScale(_renderer, value.X, value.Y));
        }

        /// <summary>
        /// The drawing color for the renderer.
        /// </summary>
        public Color DrawColor
        {
            get
            {
                byte r, g, b, a;
                _ = Native.CheckError(Native.SDL_GetRenderDrawColor(_renderer, &r, &g, &b, &a));
                return new Color(r, g, b, a);
            }

            set => Native.CheckError(Native.SDL_SetRenderDrawColor(_renderer, value.Red, value.Green, value.Blue, value.Alpha));
        }

        /// <summary>
        /// The blending mode of the renderer.
        /// </summary>
        public BlendMode DrawBlendMode
        {
            get
            {
                Native.SDL_BlendMode mode;
                _ = Native.CheckError(Native.SDL_GetRenderDrawBlendMode(_renderer, &mode));
                return new(mode);
            }
            set => Native.CheckError(Native.SDL_SetRenderDrawBlendMode(_renderer, value.ToNative()));
        }

        internal Renderer(Native.SDL_Renderer* renderer)
        {
            _renderer = renderer;
        }

        /// <inheritdoc/>
        public void Dispose() => Native.SDL_DestroyRenderer(_renderer);

        /// <summary>
        /// Creates a new renderer.
        /// </summary>
        /// <param name="window">The window to render into.</param>
        /// <param name="index">The index of the renderer driver to use or -1 to pick the first one.</param>
        /// <param name="flags">Flags for the renderer.</param>
        /// <returns>The renderer.</returns>
        public static Renderer Create(Window window, int index, RendererOptions flags) =>
            new(Native.SDL_CreateRenderer(window.ToNative(), index, (uint)flags));

        /// <summary>
        /// Creates a software renderer for the surface.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <returns>The renderer.</returns>
        public static Renderer Create(Surface surface) =>
            new(Native.SDL_CreateSoftwareRenderer(surface.ToNative()));

        /// <summary>
        /// Clears the renderer.
        /// </summary>
        public void Clear() =>
            Native.CheckError(Native.SDL_RenderClear(_renderer));

        /// <summary>
        /// Get logical coordinates of point in renderer when given real coordinates of point in window.
        /// </summary>
        /// <param name="window">Real coordinates.</param>
        /// <returns>Logical coordinates.</returns>
        public PointF WindowToLogical(Point window)
        {
            float logicalX, logicalY;
            Native.SDL_RenderWindowToLogical(_renderer, window.X, window.Y, &logicalX, &logicalY);
            return new(logicalX, logicalY);
        }

        /// <summary>
        /// Get real coordinates of point in renderer when given logical coordinates of point in window.
        /// </summary>
        /// <param name="logical">Logical coordinates.</param>
        /// <returns>Real coordinates.</returns>
        public Point LogicalToWindow(PointF logical)
        {
            int windowX, windowY;
            Native.SDL_RenderLogicalToWindow(_renderer, logical.X, logical.Y, &windowX, &windowY);
            return new(windowX, windowY);
        }

        /// <summary>
        /// Draws a point on the renderer.
        /// </summary>
        /// <param name="p">The point.</param>
        public void DrawPoint(Point p) =>
            Native.CheckError(Native.SDL_RenderDrawPoint(_renderer, p.X, p.Y));

        /// <summary>
        /// Draws a point on the renderer.
        /// </summary>
        /// <param name="p">The point.</param>
        public void DrawPointF(PointF p) =>
            Native.CheckError(Native.SDL_RenderDrawPointF(_renderer, p.X, p.Y));

        /// <summary>
        /// Draws points on a renderer.
        /// </summary>
        /// <param name="points">The points.</param>
        public void DrawPoints(Point[] points)
        {
            fixed (Point* ptr = points)
            {
                _ = Native.CheckError(Native.SDL_RenderDrawPoints(_renderer, (Native.SDL_Point*)ptr, points.Length));
            }
        }

        /// <summary>
        /// Draws points on a renderer.
        /// </summary>
        /// <param name="points">The points.</param>
        public void DrawPointsF(PointF[] points)
        {
            fixed (PointF* ptr = points)
            {
                _ = Native.CheckError(Native.SDL_RenderDrawPointsF(_renderer, (Native.SDL_FPoint*)ptr, points.Length));
            }
        }

        /// <summary>
        /// Draws a line on a renderer.
        /// </summary>
        /// <param name="line">The line.</param>
        public void DrawLine(Line line) =>
            Native.CheckError(Native.SDL_RenderDrawLine(_renderer, line.Start.X, line.Start.Y, line.End.X, line.End.Y));

        /// <summary>
        /// Draws a line on a renderer.
        /// </summary>
        /// <param name="line">The line.</param>
        public void DrawLineF(LineF line) =>
            Native.CheckError(Native.SDL_RenderDrawLineF(_renderer, line.Start.X, line.Start.Y, line.End.X, line.End.Y));

        /// <summary>
        /// Draws multiple lines on a renderer.
        /// </summary>
        /// <param name="lines">The lines.</param>
        public void DrawLines(Line[] lines)
        {
            fixed (Line* ptr = lines)
            {
                _ = Native.CheckError(Native.SDL_RenderDrawLines(_renderer, (Native.SDL_Point*)ptr, lines.Length * 2));
            }
        }

        /// <summary>
        /// Draws multiple lines on a renderer.
        /// </summary>
        /// <param name="lines">The lines.</param>
        public void DrawLinesF(LineF[] lines)
        {
            fixed (LineF* ptr = lines)
            {
                _ = Native.CheckError(Native.SDL_RenderDrawLinesF(_renderer, (Native.SDL_FPoint*)ptr, lines.Length * 2));
            }
        }

        /// <summary>
        /// Draws a rectangle on a renderer.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public void DrawRectangle(Rectangle? rectangle)
        {
            Native.SDL_Rect rect;
            _ = Native.CheckError(Native.SDL_RenderDrawRect(_renderer, Rectangle.ToNative(rectangle, &rect)));
        }

        /// <summary>
        /// Draws a rectangle on a renderer.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public void DrawRectangleF(RectangleF? rectangle)
        {
            Native.SDL_FRect rect;
            _ = Native.CheckError(Native.SDL_RenderDrawRectF(_renderer, RectangleF.ToNative(rectangle, &rect)));
        }

        /// <summary>
        /// Draws rectangles on a renderer.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        public void DrawRectangles(Rectangle[] rectangles)
        {
            fixed (Rectangle* ptr = rectangles)
            {
                _ = Native.CheckError(Native.SDL_RenderDrawRects(_renderer, (Native.SDL_Rect*)ptr, rectangles.Length));
            }
        }

        /// <summary>
        /// Draws rectangles on a renderer.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        public void DrawRectanglesF(RectangleF[] rectangles)
        {
            fixed (RectangleF* ptr = rectangles)
            {
                _ = Native.CheckError(Native.SDL_RenderDrawRectsF(_renderer, (Native.SDL_FRect*)ptr, rectangles.Length));
            }
        }

        /// <summary>
        /// Fills a rectangle on a renderer.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public void FillRectangle(Rectangle? rectangle)
        {
            Native.SDL_Rect rect;
            _ = Native.CheckError(Native.SDL_RenderFillRect(_renderer, Rectangle.ToNative(rectangle, &rect)));
        }

        /// <summary>
        /// Fills a rectangle on a renderer.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public void FillRectangleF(RectangleF? rectangle)
        {
            Native.SDL_FRect rect;
            _ = Native.CheckError(Native.SDL_RenderFillRectF(_renderer, RectangleF.ToNative(rectangle, &rect)));
        }

        /// <summary>
        /// Fills rectangles on the renderer.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        public void FillRectangles(Rectangle[] rectangles)
        {
            fixed (Rectangle* ptr = rectangles)
            {
                _ = Native.CheckError(Native.SDL_RenderFillRects(_renderer, (Native.SDL_Rect*)ptr, rectangles.Length));
            }
        }

        /// <summary>
        /// Fills rectangles on the renderer.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        public void FillRectanglesF(RectangleF[] rectangles)
        {
            fixed (RectangleF* ptr = rectangles)
            {
                _ = Native.CheckError(Native.SDL_RenderFillRectsF(_renderer, (Native.SDL_FRect*)ptr, rectangles.Length));
            }
        }

        /// <summary>
        /// Copies a texture onto a renderer.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="source">The source rectangle.</param>
        /// <param name="destination">The destination rectangle.</param>
        public void Copy(Texture texture, Rectangle? source = null, Rectangle? destination = null)
        {
            Native.SDL_Rect sourceRect, destRect;
            _ = Native.CheckError(Native.SDL_RenderCopy(_renderer, texture.ToNative(), Rectangle.ToNative(source, &sourceRect), Rectangle.ToNative(destination, &destRect)));
        }

        /// <summary>
        /// Copies a texture onto a renderer.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="source">The source rectangle.</param>
        /// <param name="destination">The destination rectangle.</param>
        public void CopyF(Texture texture, Rectangle? source, RectangleF? destination)
        {
            Native.SDL_Rect sourceRect;
            Native.SDL_FRect destRect;
            _ = Native.CheckError(Native.SDL_RenderCopyF(_renderer, texture.ToNative(), Rectangle.ToNative(source, &sourceRect), RectangleF.ToNative(destination, &destRect)));
        }

        /// <summary>
        /// Copies a texture onto renderer.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="source">The source rectangle.</param>
        /// <param name="destination">The destination rectangle.</param>
        /// <param name="angle">The rotation angle.</param>
        /// <param name="center">The center.</param>
        /// <param name="flip">Whether to flip the texture.</param>
        public void Copy(Texture texture, Rectangle? source, Rectangle? destination, double angle, Point? center, RendererFlip flip)
        {
            Native.SDL_Rect sourceRect, destRect;
            Native.SDL_Point centerPoint;
            _ = Native.CheckError(Native.SDL_RenderCopyEx(_renderer, texture.ToNative(), Rectangle.ToNative(source, &sourceRect), Rectangle.ToNative(destination, &destRect), angle, Point.ToNative(center, &centerPoint), (Native.SDL_RendererFlip)flip));
        }

        /// <summary>
        /// Copies a texture onto renderer.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="source">The source rectangle.</param>
        /// <param name="destination">The destination rectangle.</param>
        /// <param name="angle">The rotation angle.</param>
        /// <param name="center">The center.</param>
        /// <param name="flip">Whether to flip the texture.</param>
        public void CopyF(Texture texture, Rectangle? source, RectangleF? destination, double angle, PointF? center, RendererFlip flip)
        {
            Native.SDL_Rect sourceRect;
            Native.SDL_FRect destRect;
            Native.SDL_FPoint centerPoint;
            _ = Native.CheckError(Native.SDL_RenderCopyExF(_renderer, texture.ToNative(), Rectangle.ToNative(source, &sourceRect), RectangleF.ToNative(destination, &destRect), angle, PointF.ToNative(center, &centerPoint), (Native.SDL_RendererFlip)flip));
        }

        /// <summary>
        /// Draw a list of triangles.
        /// </summary>
        /// <param name="texture">The texture to use, if any.</param>
        /// <param name="vertices">The vertices to draw.</param>
        /// <param name="indices">Optional ordering of vertices.</param>
        public void DrawGeometry(Texture? texture, Vertex[] vertices, int[]? indices)
        {
            fixed (Vertex* verticiesPtr = vertices)
            fixed (int* indicesPtr = indices)
            {
                _ = Native.CheckError(Native.SDL_RenderGeometry(_renderer, texture == null ? null : texture.ToNative(), (Native.SDL_Vertex*)verticiesPtr, vertices.Length, indicesPtr, indices?.Length ?? 0));
            }
        }

        // Currently leaving off RenderGeometryRaw since it involves interior pointers

        /// <summary>
        /// Reads pixels from the renderer.
        /// </summary>
        /// <param name="rectangle">The rectangle to read.</param>
        /// <param name="format">The format of the pixels.</param>
        /// <param name="pixels">A place to put the pixels.</param>
        /// <param name="pitch">The pitch.</param>
        public void ReadPixels(Rectangle? rectangle, EnumeratedPixelFormat format, Span<byte> pixels, int pitch)
        {
            Native.SDL_Rect rect;
            fixed (byte* pixelsPointer = pixels)
            {
                _ = Native.CheckError(Native.SDL_RenderReadPixels(_renderer, Rectangle.ToNative(rectangle, &rect), format.Value, pixelsPointer, pitch));
            }
        }

        /// <summary>
        /// Present the renderer on the window.
        /// </summary>
        public void Present() =>
            Native.SDL_RenderPresent(_renderer);

        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <param name="access">The access.</param>
        /// <param name="size">The size.</param>
        /// <returns>The texture.</returns>
        public Texture CreateTexture(EnumeratedPixelFormat format, TextureAccess access, Size size) =>
            new(Native.SDL_CreateTexture(_renderer, format.Value, (int)access, size.Width, size.Height));

        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <returns>The texture.</returns>
        public Texture CreateTexture(Surface surface) =>
            new(Native.SDL_CreateTextureFromSurface(_renderer, surface.ToNative()));

        /// <summary>
        /// Flushes all pending draws in the renderer.
        /// </summary>
        public void Flush() => Native.CheckError(Native.SDL_RenderFlush(_renderer));

        /// <summary>
        /// Sets the VSync of the renderer.
        /// </summary>
        /// <param name="vsync">Whether VSync is on.</param>
        public void SetVsync(bool vsync) => _ = Native.CheckError(Native.SDL_RenderSetVSync(_renderer, vsync ? 1 : 0));

        internal Native.SDL_Renderer* ToNative() => _renderer;
    }
}
