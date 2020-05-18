using System;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// A texture.
    /// </summary>
    public sealed unsafe class Texture : NativePointerBase<Native.SDL_Texture, Texture>
    {
        /// <summary>
        /// The pixel format.
        /// </summary>
        public EnumeratedPixelFormat PixelFormat
        {
            get
            {
                _ = Native.CheckError(Native.SDL_QueryTexture(Pointer, out var format, out _, out _, out _));
                return format;
            }
        }

        /// <summary>
        /// The texture access.
        /// </summary>
        public TextureAccess TextureAccess
        {
            get
            {
                _ = Native.CheckError(Native.SDL_QueryTexture(Pointer, out _, out var access, out _, out _));
                return access;
            }
        }

        /// <summary>
        /// The size.
        /// </summary>
        public Size Size
        {
            get
            {
                _ = Native.CheckError(Native.SDL_QueryTexture(Pointer, out _, out _, out var width, out var height));
                return (width, height);
            }
        }

        /// <summary>
        /// The color modulator.
        /// </summary>
        public (byte Red, byte Green, byte Blue) ColorMod
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetTextureColorMod(Pointer, out var red, out var green, out var blue));
                return (red, green, blue);
            }
            set => _ = Native.CheckError(Native.SDL_SetTextureColorMod(Pointer, value.Red, value.Green, value.Blue));
        }

        /// <summary>
        /// The alpha modulator.
        /// </summary>
        public byte AlphaMod
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetTextureAlphaMod(Pointer, out var alpha));
                return alpha;
            }
            set => _ = Native.CheckError(Native.SDL_SetTextureAlphaMod(Pointer, value));
        }

        /// <summary>
        /// The blend mode.
        /// </summary>
        public BlendMode BlendMode
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetTextureBlendMode(Pointer, out var mode));
                return mode;
            }

            set => Native.CheckError(Native.SDL_SetTextureBlendMode(Pointer, value));
        }

        /// <summary>
        /// The scale mode.
        /// </summary>
        public ScaleMode ScaleMode
        {
            get
            {
                _ = Native.CheckError(Native.SDL_GetTextureScaleMode(Pointer, out var mode));
                return mode;
            }

            set => Native.CheckError(Native.SDL_SetTextureScaleMode(Pointer, value));
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Native.SDL_DestroyTexture(Pointer);
            base.Dispose();
        }

        /// <summary>
        /// Updates the texture with RGB values.
        /// </summary>
        /// <param name="rectangle">The area to update.</param>
        /// <param name="pixels">The pixels to update with.</param>
        /// <param name="pitch">The pitch.</param>
        public void Update(Rectangle? rectangle, Span<byte> pixels, int pitch)
        {
            var rectPointer = (Rectangle*)null;
            Rectangle rect;

            if (rectangle.HasValue)
            {
                rect = rectangle.Value;
                rectPointer = &rect;
            }

            fixed (byte* pixelsPointer = pixels)
            {
                _ = Native.CheckError(Native.SDL_UpdateTexture(Pointer, rectPointer, pixelsPointer, pitch));
            }
        }

        /// <summary>
        /// Updates the texture with YUV values.
        /// </summary>
        /// <param name="rectangle">The area to update.</param>
        /// <param name="yPixels">The Y plane pixels.</param>
        /// <param name="yPitch">The Y plane pitch.</param>
        /// <param name="uPixels">The U plane pixels.</param>
        /// <param name="uPitch">The U plane pitch.</param>
        /// <param name="vPixels">The V plane pixels.</param>
        /// <param name="vPitch">The V plane pitch.</param>
        public void Update(Rectangle? rectangle, Span<byte> yPixels, int yPitch, Span<byte> uPixels, int uPitch, Span<byte> vPixels, int vPitch)
        {
            var rectPointer = (Rectangle*)null;
            Rectangle rect;

            if (rectangle.HasValue)
            {
                rect = rectangle.Value;
                rectPointer = &rect;
            }

            fixed (byte* yPixelsPointer = yPixels)
            {
                fixed (byte* uPixelsPointer = uPixels)
                {
                    fixed (byte* vPixelsPointer = vPixels)
                    {
                        _ = Native.CheckError(Native.SDL_UpdateYUVTexture(Pointer, rectPointer, yPixelsPointer, yPitch, uPixelsPointer, uPitch, vPixelsPointer, vPitch));
                    }
                }
            }
        }

        /// <summary>
        /// Locks the texture for writing.
        /// </summary>
        /// <typeparam name="T">The type of the pixel.</typeparam>
        /// <param name="rectangle">The area to lock.</param>
        /// <returns>The pixels.</returns>
        public Span<T> Lock<T>(Rectangle? rectangle)
        {
            var rectPointer = (Rectangle*)null;
            Rectangle rect;
            int height;

            if (rectangle.HasValue)
            {
                rect = rectangle.Value;
                rectPointer = &rect;
                height = rect.Size.Height;
            }
            else
            {
                height = Size.Height;
            }

            _ = Native.CheckError(Native.SDL_LockTexture(Pointer, rectPointer, out var pixelsPointer, out var pitch));
            return Native.PixelsToSpan<T>(pixelsPointer, pitch, height);
        }

        /// <summary>
        /// Locks the texture for writing.
        /// </summary>
        /// <param name="rectangle">The area to lock.</param>
        /// <returns>A surface representing the pixels.</returns>
        public Surface Lock(Rectangle? rectangle)
        {
            var rectPointer = (Rectangle*)null;
            Rectangle rect;

            if (rectangle.HasValue)
            {
                rect = rectangle.Value;
                rectPointer = &rect;
            }

            _ = Native.CheckError(Native.SDL_LockTextureToSurface(Pointer, rectPointer, out var surfacePointer));
            return Surface.PointerToInstanceNotNull(surfacePointer);
        }

        /// <summary>
        /// Unlocks the texture.
        /// </summary>
        public void Unlock() =>
            Native.SDL_UnlockTexture(Pointer);
    }
}
