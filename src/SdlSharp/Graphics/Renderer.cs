using System;
using System.Collections.Generic;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// A graphics renderer.
    /// </summary>
    public unsafe sealed class Renderer : NativePointerBase<Native.SDL_Renderer, Renderer>
    {
        private static ItemCollection<RendererInfo> s_renderDrivers;

        /// <summary>
        /// The renderer drivers available.
        /// </summary>
        public static IReadOnlyList<RendererInfo> RenderDrivers = s_renderDrivers ?? (s_renderDrivers = new ItemCollection<RendererInfo>(
            index =>
            {
                _ = Native.CheckError(Native.SDL_GetRenderDriverInfo(index, out var info));
                return info;
            },
            Native.SDL_GetNumRenderDrivers));

        /// <summary>
        /// The driver for this renderer.
        /// </summary>
        public RendererInfo Driver
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetRendererInfo(Pointer, out var info));
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
                _ = Native.CheckError(Native.SDL_GetRendererOutputSize(Pointer, out var w, out var h));
                return (w, h);
            }
        }

        /// <summary>
        /// Whether the renderer supports a taret.
        /// </summary>
        public bool TargetSupported =>
            Native.SDL_RenderTargetSupported(Pointer);

        /// <summary>
        /// The target of the renderer.
        /// </summary>
        public Texture Target
        {
            get => Texture.PointerToInstanceNotNull(Native.SDL_GetRenderTarget(Pointer));
            set => Native.CheckError(Native.SDL_SetRenderTarget(Pointer, value.Pointer));
        }

        /// <summary>
        /// The logical size of the renderer.
        /// </summary>
        public Size LogicalSize
        {
            get
            {
                Native.SDL_RenderGetLogicalSize(Pointer, out var w, out var h);
                return (w, h);
            }

            set => Native.CheckError(Native.SDL_RenderSetLogicalSize(Pointer, value.Width, value.Height));
        }

        /// <summary>
        /// Whether integer scales are forced for resolution-independent rendering.
        /// </summary>
        public bool IntegerScale
        {
            get => Native.SDL_RenderGetIntegerScale(Pointer);
            set => Native.CheckError(Native.SDL_RenderSetIntegerScale(Pointer, value));
        }

        /// <summary>
        /// The viewport of the renderer, if any.
        /// </summary>
        public Rectangle? Viewport
        {
            get
            {
                Native.SDL_RenderGetViewport(Pointer, out var rect);
                return rect.IsEmpty ? null : (Rectangle?)rect;
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

                _ = Native.CheckError(Native.SDL_RenderSetViewport(Pointer, rectPointer));
            }
        }

        /// <summary>
        /// Whether clipping is enabled for the renderer.
        /// </summary>
        public bool ClippingEnabled =>
            Native.SDL_RenderIsClipEnabled(Pointer);

        /// <summary>
        /// The clipping rectangle of the renderer.
        /// </summary>
        public Rectangle? ClippingRectangle
        {
            get
            {
                Native.SDL_RenderGetClipRect(Pointer, out var rect);
                return rect.IsEmpty ? null : (Rectangle?)rect;
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

                _ = Native.CheckError(Native.SDL_RenderSetClipRect(Pointer, rectPointer));
            }
        }

        /// <summary>
        /// The scaling of the renderer.
        /// </summary>
        public (float X, float Y) Scale
        {
            get
            {
                Native.SDL_RenderGetScale(Pointer, out var x, out var y);
                return (x, y);
            }

            set => Native.CheckError(Native.SDL_RenderSetScale(Pointer, value.X, value.Y));
        }

        /// <summary>
        /// The drawing color for the renderer.
        /// </summary>
        public Color DrawColor
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetRenderDrawColor(Pointer, out var r, out var g, out var b, out var a));
                return new Color(r, g, b, a);
            }

            set => Native.CheckError(Native.SDL_SetRenderDrawColor(Pointer, value.Red, value.Green, value.Blue, value.Alpha));
        }

        /// <summary>
        /// The blending mode of the renderer.
        /// </summary>
        public BlendMode DrawBlendMode
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetRenderDrawBlendMode(Pointer, out var mode));
                return mode;
            }
            set => Native.CheckError(Native.SDL_SetRenderDrawBlendMode(Pointer, value));
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Native.SDL_DestroyRenderer(Pointer);
            base.Dispose();
        }

        /// <summary>
        /// Creates a new renderer.
        /// </summary>
        /// <param name="window">The window to render into.</param>
        /// <param name="index">The index of the renderer driver to use or -1 to pick the first one.</param>
        /// <param name="flags">Flags for the renderer.</param>
        /// <returns>The renderer.</returns>
        public static Renderer Create(Window window, int index, RendererFlags flags) =>
            PointerToInstanceNotNull(Native.SDL_CreateRenderer(window.Pointer, index, flags));

        /// <summary>
        /// Creates a software renderer for the surface.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <returns>The renderer.</returns>
        public static Renderer Create(Surface surface) =>
            PointerToInstanceNotNull(Native.SDL_CreateSoftwareRenderer(surface.Pointer));

        /// <summary>
        /// Clears the renderer.
        /// </summary>
        public void Clear() =>
            Native.CheckError(Native.SDL_RenderClear(Pointer));

        /// <summary>
        /// Draws a point on the renderer.
        /// </summary>
        /// <param name="p">The point.</param>
        public void DrawPoint(Point p) =>
            Native.CheckError(Native.SDL_RenderDrawPoint(Pointer, p.X, p.Y));

        /// <summary>
        /// Draws points on a renderer.
        /// </summary>
        /// <param name="points">The points.</param>
        public void DrawPoints(Point[] points) =>
            Native.CheckError(Native.SDL_RenderDrawPoints(Pointer, points, points.Length));

        /// <summary>
        /// Draws a line on a renderer.
        /// </summary>
        /// <param name="p1">The beginning of the line.</param>
        /// <param name="p2">The end of the line.</param>
        public void DrawLine(Point p1, Point p2) =>
            Native.CheckError(Native.SDL_RenderDrawLine(Pointer, p1.X, p1.Y, p2.X, p2.Y));

        /// <summary>
        /// Draws multiple lines on a renderer.
        /// </summary>
        /// <param name="points">The beginning/end points of the lines.</param>
        public void DrawLines(Point[] points) =>
            Native.CheckError(Native.SDL_RenderDrawLines(Pointer, points, points.Length));

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

            _ = Native.CheckError(Native.SDL_RenderDrawRect(Pointer, rectPointer));
        }

        /// <summary>
        /// Draws rectangles on a renderer.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        public void DrawRectangles(Rectangle[] rectangles) =>
            Native.CheckError(Native.SDL_RenderDrawRects(Pointer, rectangles, rectangles.Length));

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

            _ = Native.CheckError(Native.SDL_RenderFillRect(Pointer, rectPointer));
        }

        /// <summary>
        /// Fills rectangles on the renderer.
        /// </summary>
        /// <param name="rectangles">The rectangles.</param>
        public void FillRectangles(Rectangle[] rectangles) =>
            Native.CheckError(Native.SDL_RenderFillRects(Pointer, rectangles, rectangles.Length));

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

            _ = Native.CheckError(Native.SDL_RenderCopy(Pointer, texture.Pointer, sourcePointer, destPointer));
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

            _ = Native.CheckError(Native.SDL_RenderCopyEx(Pointer, texture.Pointer, sourcePointer, destPointer, angle, centerPointer, flip));
        }

        /// <summary>
        /// Reads pixels from the renderer.
        /// </summary>
        /// <param name="rectangle">The rectangle to read.</param>
        /// <param name="format">The format of the </param>
        /// <param name="plane"></param>
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
                _ = Native.CheckError(Native.SDL_RenderReadPixels(Pointer, rectPointer, format, pixelsPointer, pitch));
            }
        }

        /// <summary>
        /// Present the renderer on the window.
        /// </summary>
        public void Present() =>
            Native.SDL_RenderPresent(Pointer);

        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="format">The pixel format.</param>
        /// <param name="access">The access.</param>
        /// <param name="size">The size.</param>
        /// <returns>The texture.</returns>
        public Texture CreateTexture(EnumeratedPixelFormat format, TextureAccess access, Size size) =>
            Texture.PointerToInstanceNotNull(Native.SDL_CreateTexture(Pointer, format, access, size.Width, size.Height));

        /// <summary>
        /// Creates a texture.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <returns>The texture.</returns>
        public Texture CreateTexture(Surface surface) =>
            Texture.PointerToInstanceNotNull(Native.SDL_CreateTextureFromSurface(Pointer, surface.Pointer));
    }
}
