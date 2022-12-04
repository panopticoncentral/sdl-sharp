namespace SdlSharp.Graphics
{
    /// <summary>
    /// A graphics renderer.
    /// </summary>
    public sealed unsafe class Renderer : NativePointerBase<Native.SDL_Renderer, Renderer>
    {
        private static ItemCollection<RendererInfo>? s_renderDrivers;

        /// <summary>
        /// The renderer drivers available.
        /// </summary>
        public static IReadOnlyList<RendererInfo> RenderDrivers => s_renderDrivers ??= new ItemCollection<RendererInfo>(
            index =>
            {
                _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_GetRenderDriverInfo(index, out var info));
                return info;
            },
            SdlSharp.Native.SDL_GetNumRenderDrivers);

        /// <summary>
        /// Information about this renderer.
        /// </summary>
        public RendererInfo Driver
        {
            get
            {
                _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_GetRendererInfo(Native, out var info));
                return info;
            }
        }

        /// <summary>
        /// The output size of this renderer.
        /// </summary>
        public Size OutputSize
        {
            get
            {
                _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_GetRendererOutputSize(Native, out var w, out var h));
                return (w, h);
            }
        }

        /// <summary>
        /// Whether the renderer supports a target.
        /// </summary>
        public bool TargetSupported =>
            SdlSharp.Native.SDL_RenderTargetSupported(Native);

        /// <summary>
        /// The target of the renderer.
        /// </summary>
        public Texture? Target
        {
            get => Texture.PointerToInstance(SdlSharp.Native.SDL_GetRenderTarget(Native));
            set => SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SetRenderTarget(Native, value == null ? null : value.Native));
        }

        /// <summary>
        /// The logical size of the renderer.
        /// </summary>
        public Size LogicalSize
        {
            get
            {
                SdlSharp.Native.SDL_RenderGetLogicalSize(Native, out var w, out var h);
                return (w, h);
            }

            set => SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderSetLogicalSize(Native, value.Width, value.Height));
        }

        /// <summary>
        /// Whether integer scales are forced for resolution-independent rendering.
        /// </summary>
        public bool IntegerScale
        {
            get => SdlSharp.Native.SDL_RenderGetIntegerScale(Native);
            set => SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderSetIntegerScale(Native, value));
        }

        /// <summary>
        /// The viewport of the renderer, if any.
        /// </summary>
        public Rectangle? Viewport
        {
            get
            {
                SdlSharp.Native.SDL_RenderGetViewport(Native, out var rect);
                return rect.IsEmpty ? null : rect;
            }

            set
            {
                var rectPointer = (Rectangle*)null;
                Rectangle rect;

                if (value.HasValue)
                {
                    rect = value.Value;
                    rectPointer = &rect;
                }

                _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderSetViewport(Native, rectPointer));
            }
        }

        /// <summary>
        /// Whether clipping is enabled for the renderer.
        /// </summary>
        public bool ClippingEnabled =>
            SdlSharp.Native.SDL_RenderIsClipEnabled(Native);

        /// <summary>
        /// The clipping rectangle of the renderer.
        /// </summary>
        public Rectangle? ClippingRectangle
        {
            get
            {
                SdlSharp.Native.SDL_RenderGetClipRect(Native, out var rect);
                return rect.IsEmpty ? null : rect;
            }

            set
            {
                var rectPointer = (Rectangle*)null;
                Rectangle rect;

                if (value.HasValue)
                {
                    rect = value.Value;
                    rectPointer = &rect;
                }

                _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderSetClipRect(Native, rectPointer));
            }
        }

        /// <summary>
        /// The scaling of the renderer.
        /// </summary>
        public (float X, float Y) Scale
        {
            get
            {
                SdlSharp.Native.SDL_RenderGetScale(Native, out var x, out var y);
                return (x, y);
            }

            set => SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderSetScale(Native, value.X, value.Y));
        }

        /// <summary>
        /// The drawing color for the renderer.
        /// </summary>
        public Color DrawColor
        {
            get
            {
                _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_GetRenderDrawColor(Native, out var r, out var g, out var b, out var a));
                return new Color(r, g, b, a);
            }

            set => SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SetRenderDrawColor(Native, value.Red, value.Green, value.Blue, value.Alpha));
        }

        /// <summary>
        /// The blending mode of the renderer.
        /// </summary>
        public BlendMode DrawBlendMode
        {
            get
            {
                _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_GetRenderDrawBlendMode(Native, out var mode));
                return mode;
            }
            set => SdlSharp.Native.CheckError(SdlSharp.Native.SDL_SetRenderDrawBlendMode(Native, value));
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            SdlSharp.Native.SDL_DestroyRenderer(Native);
            base.Dispose();
        }

        /// <summary>
        /// Creates a new renderer.
        /// </summary>
        /// <param name="window">The window to render into.</param>
        /// <param name="index">The index of the renderer driver to use or -1 to pick the first one.</param>
        /// <param name="flags">Flags for the renderer.</param>
        /// <returns>The renderer.</returns>
        public static Renderer Create(Window window, int index, RendererOptions flags) =>
            PointerToInstanceNotNull(SdlSharp.Native.SDL_CreateRenderer(window.Native, index, flags));

        /// <summary>
        /// Creates a software renderer for the surface.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <returns>The renderer.</returns>
        public static Renderer Create(Surface surface) =>
            PointerToInstanceNotNull(SdlSharp.Native.SDL_CreateSoftwareRenderer(surface.Native));

        /// <summary>
        /// Clears the renderer.
        /// </summary>
        public void Clear() =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderClear(Native));

        /// <summary>
        /// Draws a point on the renderer.
        /// </summary>
        /// <param name="p">The point.</param>
        public void DrawPoint(Point p) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderDrawPoint(Native, p.X, p.Y));

        /// <summary>
        /// Draws a point on the renderer.
        /// </summary>
        /// <param name="p">The point.</param>
        public void DrawPoint(PointF p) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderDrawPointF(Native, p.X, p.Y));

        /// <summary>
        /// Draws points on a renderer.
        /// </summary>
        /// <param name="points">The points.</param>
        public void DrawPoints(Point[] points) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderDrawPoints(Native, points, points.Length));

        /// <summary>
        /// Draws points on a renderer.
        /// </summary>
        /// <param name="points">The points.</param>
        public void DrawPoints(PointF[] points) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderDrawPointsF(Native, points, points.Length));

        /// <summary>
        /// Draws a line on a renderer.
        /// </summary>
        /// <param name="p1">The beginning of the line.</param>
        /// <param name="p2">The end of the line.</param>
        public void DrawLine(Point p1, Point p2) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderDrawLine(Native, p1.X, p1.Y, p2.X, p2.Y));

        /// <summary>
        /// Draws a line on a renderer.
        /// </summary>
        /// <param name="p1">The beginning of the line.</param>
        /// <param name="p2">The end of the line.</param>
        public void DrawLine(PointF p1, PointF p2) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderDrawLineF(Native, p1.X, p1.Y, p2.X, p2.Y));

        /// <summary>
        /// Draws multiple lines on a renderer.
        /// </summary>
        /// <param name="points">The beginning/end points of the lines.</param>
        public void DrawLines(Point[] points) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderDrawLines(Native, points, points.Length));

        /// <summary>
        /// Draws multiple lines on a renderer.
        /// </summary>
        /// <param name="points">The beginning/end points of the lines.</param>
        public void DrawLines(PointF[] points) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderDrawLinesF(Native, points, points.Length));

        /// <summary>
        /// Draws a rectangle on a renderer.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public void DrawRectangle(Rectangle? rectangle)
        {
            var rectPointer = (Rectangle*)null;

            if (rectangle.HasValue)
            {
                var rect = rectangle.Value;
                rectPointer = &rect;
            }

            _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderDrawRect(Native, rectPointer));
        }

        /// <summary>
        /// Draws a rectangle on a renderer.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public void DrawRectangle(RectangleF? rectangle)
        {
            var rectPointer = (RectangleF*)null;

            if (rectangle.HasValue)
            {
                var rect = rectangle.Value;
                rectPointer = &rect;
            }

            _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderDrawRectF(Native, rectPointer));
        }

        /// <summary>
        /// Draws rectangles on a renderer.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        public void DrawRectangles(Rectangle[] rectangles) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderDrawRects(Native, rectangles, rectangles.Length));

        /// <summary>
        /// Draws rectangles on a renderer.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        public void DrawRectangles(RectangleF[] rectangles) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderDrawRectsF(Native, rectangles, rectangles.Length));

        /// <summary>
        /// Fills a rectangle on a renderer.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public void FillRectangle(Rectangle? rectangle)
        {
            var rectPointer = (Rectangle*)null;

            if (rectangle.HasValue)
            {
                var rect = rectangle.Value;
                rectPointer = &rect;
            }

            _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderFillRect(Native, rectPointer));
        }

        /// <summary>
        /// Fills a rectangle on a renderer.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public void FillRectangle(RectangleF? rectangle)
        {
            var rectPointer = (RectangleF*)null;

            if (rectangle.HasValue)
            {
                var rect = rectangle.Value;
                rectPointer = &rect;
            }

            _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderFillRectF(Native, rectPointer));
        }

        /// <summary>
        /// Fills rectangles on the renderer.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        public void FillRectangles(Rectangle[] rectangles) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderFillRects(Native, rectangles, rectangles.Length));

        /// <summary>
        /// Fills rectangles on the renderer.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        public void FillRectangles(RectangleF[] rectangles) =>
            SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderFillRectsF(Native, rectangles, rectangles.Length));

        /// <summary>
        /// Copies a texture onto a renderer.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="source">The source rectangle.</param>
        /// <param name="destination">The destination rectangle.</param>
        public void Copy(Texture texture, Rectangle? source = null, Rectangle? destination = null)
        {
            var sourcePointer = (Rectangle*)null;
            var destPointer = (Rectangle*)null;

            if (source.HasValue)
            {
                var sourceRect = source.Value;
                sourcePointer = &sourceRect;
            }

            if (destination.HasValue)
            {
                var destRect = destination.Value;
                destPointer = &destRect;
            }

            _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderCopy(Native, texture.Native, sourcePointer, destPointer));
        }

        /// <summary>
        /// Copies a texture onto a renderer.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="source">The source rectangle.</param>
        /// <param name="destination">The destination rectangle.</param>
        public void Copy(Texture texture, Rectangle? source, RectangleF? destination)
        {
            var sourcePointer = (Rectangle*)null;
            var destPointer = (RectangleF*)null;

            if (source.HasValue)
            {
                var sourceRect = source.Value;
                sourcePointer = &sourceRect;
            }

            if (destination.HasValue)
            {
                var destRect = destination.Value;
                destPointer = &destRect;
            }

            _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderCopyF(Native, texture.Native, sourcePointer, destPointer));
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
            var sourcePointer = (Rectangle*)null;
            var destPointer = (Rectangle*)null;
            var centerPointer = (Point*)null;

            if (source.HasValue)
            {
                var sourceRect = source.Value;
                sourcePointer = &sourceRect;
            }

            if (destination.HasValue)
            {
                var destRect = destination.Value;
                destPointer = &destRect;
            }

            if (center.HasValue)
            {
                var centerRect = center.Value;
                centerPointer = &centerRect;
            }

            _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderCopy(Native, texture.Native, sourcePointer, destPointer, angle, centerPointer, flip));
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
        public void Copy(Texture texture, Rectangle? source, RectangleF? destination, double angle, PointF? center, RendererFlip flip)
        {
            var sourcePointer = (Rectangle*)null;
            var destPointer = (RectangleF*)null;
            var centerPointer = (PointF*)null;

            if (source.HasValue)
            {
                var sourceRect = source.Value;
                sourcePointer = &sourceRect;
            }

            if (destination.HasValue)
            {
                var destRect = destination.Value;
                destPointer = &destRect;
            }

            if (center.HasValue)
            {
                var centerRect = center.Value;
                centerPointer = &centerRect;
            }

            _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderCopyExF(Native, texture.Native, sourcePointer, destPointer, angle, centerPointer, flip));
        }

        /// <summary>
        /// Reads pixels from the renderer.
        /// </summary>
        /// <param name="rectangle">The rectangle to read.</param>
        /// <param name="format">The format of the pixels.</param>
        /// <param name="pixels">A place to put the pixels.</param>
        /// <param name="pitch">The pitch.</param>
        public void ReadPixels(Rectangle? rectangle, EnumeratedPixelFormat format, Span<byte> pixels, int pitch)
        {
            var rectPointer = (Rectangle*)null;

            if (rectangle.HasValue)
            {
                var rect = rectangle.Value;
                rectPointer = &rect;
            }

            fixed (byte* pixelsPointer = pixels)
            {
                _ = SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderReadPixels(Native, rectPointer, format, pixelsPointer, pitch));
            }
        }

        /// <summary>
        /// Present the renderer on the window.
        /// </summary>
        public void Present() =>
            SdlSharp.Native.SDL_RenderPresent(Native);

        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <param name="access">The access.</param>
        /// <param name="size">The size.</param>
        /// <returns>The texture.</returns>
        public Texture CreateTexture(EnumeratedPixelFormat format, TextureAccess access, Size size) =>
            Texture.PointerToInstanceNotNull(SdlSharp.Native.SDL_CreateTexture(Native, format, access, size.Width, size.Height));

        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <returns>The texture.</returns>
        public Texture CreateTexture(Surface surface) =>
            Texture.PointerToInstanceNotNull(SdlSharp.Native.SDL_CreateTextureFromSurface(Native, surface.Native));

        /// <summary>
        /// Flushes all pending draws in the renderer.
        /// </summary>
        public void Flush() => SdlSharp.Native.CheckError(SdlSharp.Native.SDL_RenderFlush(Native));
    }
}
