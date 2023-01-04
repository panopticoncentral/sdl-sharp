namespace SdlSharp.Graphics
{
    /// <summary>
    /// A texture.
    /// </summary>
    public sealed unsafe class Texture : IDisposable
    {
        private readonly Native.SDL_Texture* _texture;

        /// <summary>
        /// The ID of the texture.
        /// </summary>
        public nuint Id => (nuint)_texture;

        /// <summary>
        /// The pixel format.
        /// </summary>
        public EnumeratedPixelFormat PixelFormat
        {
            get
            {
                uint format;
                _ = Native.CheckError(Native.SDL_QueryTexture(_texture, &format, null, null, null));
                return new(format);
            }
        }

        /// <summary>
        /// The texture access.
        /// </summary>
        public TextureAccess TextureAccess
        {
            get
            {
                int access;
                _ = Native.CheckError(Native.SDL_QueryTexture(_texture, null, &access, null, null));
                return (TextureAccess)access;
            }
        }

        /// <summary>
        /// The size.
        /// </summary>
        public Size Size
        {
            get
            {
                int width, height;
                _ = Native.CheckError(Native.SDL_QueryTexture(_texture, null, null, &width, &height));
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
                byte red, green, blue;
                _ = Native.CheckError(Native.SDL_GetTextureColorMod(_texture, &red, &green, &blue));
                return (red, green, blue);
            }
            set => _ = Native.CheckError(Native.SDL_SetTextureColorMod(_texture, value.Red, value.Green, value.Blue));
        }

        /// <summary>
        /// The alpha modulator.
        /// </summary>
        public byte AlphaMod
        {
            get
            {
                byte alpha;
                _ = Native.CheckError(Native.SDL_GetTextureAlphaMod(_texture, &alpha));
                return alpha;
            }
            set => _ = Native.CheckError(Native.SDL_SetTextureAlphaMod(_texture, value));
        }

        /// <summary>
        /// The blend mode.
        /// </summary>
        public BlendMode BlendMode
        {
            get
            {
                Native.SDL_BlendMode mode;
                _ = Native.CheckError(Native.SDL_GetTextureBlendMode(_texture, &mode));
                return new(mode);
            }

            set => Native.CheckError(Native.SDL_SetTextureBlendMode(_texture, value.ToNative()));
        }

        /// <summary>
        /// The scale mode.
        /// </summary>
        public ScaleMode ScaleMode
        {
            get
            {
                Native.SDL_ScaleMode mode;
                _ = Native.CheckError(Native.SDL_GetTextureScaleMode(_texture, &mode));
                return (ScaleMode)mode;
            }

            set => Native.CheckError(Native.SDL_SetTextureScaleMode(_texture, (Native.SDL_ScaleMode)value));
        }

        /// <summary>
        /// User data.
        /// </summary>
        public nint UserData
        {
            get => Native.SDL_GetTextureUserData(_texture);
            set => Native.CheckError(Native.SDL_SetTextureUserData(_texture, value));
        }

        internal Texture(Native.SDL_Texture* texture)
        {
            _texture = texture;
        }

        /// <inheritdoc/>
        public void Dispose() => Native.SDL_DestroyTexture(_texture);

        /// <summary>
        /// Updates the texture with RGB values.
        /// </summary>
        /// <param name="rectangle">The area to update.</param>
        /// <param name="pixels">The pixels to update with.</param>
        /// <param name="pitch">The pitch.</param>
        public void Update(Rectangle? rectangle, Span<byte> pixels, int pitch)
        {
            Native.SDL_Rect rect;
            fixed (byte* pixelsPointer = pixels)
            {
                _ = Native.CheckError(Native.SDL_UpdateTexture(_texture, Rectangle.ToNative(rectangle, &rect), pixelsPointer, pitch));
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
            Native.SDL_Rect rect;
            fixed (byte* yPixelsPointer = yPixels)
            fixed (byte* uPixelsPointer = uPixels)
            fixed (byte* vPixelsPointer = vPixels)
            {
                _ = Native.CheckError(Native.SDL_UpdateYUVTexture(_texture, Rectangle.ToNative(rectangle, &rect), yPixelsPointer, yPitch, uPixelsPointer, uPitch, vPixelsPointer, vPitch));
            }
        }

        /// <summary>
        /// Updates the texture with NV values.
        /// </summary>
        /// <param name="rectangle">The area to update.</param>
        /// <param name="yPixels">The Y plane pixels.</param>
        /// <param name="yPitch">The Y plane pitch.</param>
        /// <param name="uvPixels">The UV plane pixels.</param>
        /// <param name="uvPitch">The UV plane pitch.</param>
        public void Update(Rectangle? rectangle, Span<byte> yPixels, int yPitch, Span<byte> uvPixels, int uvPitch)
        {
            Native.SDL_Rect rect;
            fixed (byte* yPixelsPointer = yPixels)
            fixed (byte* uvPixelsPointer = uvPixels)
            {
                _ = Native.CheckError(Native.SDL_UpdateNVTexture(_texture, Rectangle.ToNative(rectangle, &rect), yPixelsPointer, yPitch, uvPixelsPointer, uvPitch));
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
            Native.SDL_Rect rect;
            var height = rectangle?.Size.Height ?? Size.Height;
            byte* pixels;
            int pitch;

            _ = Native.CheckError(Native.SDL_LockTexture(_texture, Rectangle.ToNative(rectangle, &rect), &pixels, &pitch));
            return Native.PixelsToSpan<T>(pixels, pitch, height);
        }

        /// <summary>
        /// Locks the texture for writing.
        /// </summary>
        /// <param name="rectangle">The area to lock.</param>
        /// <returns>A surface representing the pixels.</returns>
        public Surface LockAsSurface(Rectangle? rectangle)
        {
            Native.SDL_Rect rect;
            Native.SDL_Surface* surface;
            _ = Native.CheckError(Native.SDL_LockTextureToSurface(_texture, Rectangle.ToNative(rectangle, &rect), &surface));
            return new(surface);
        }

        /// <summary>
        /// Unlocks the texture.
        /// </summary>
        public void Unlock() =>
            Native.SDL_UnlockTexture(_texture);

        internal Native.SDL_Texture* ToNative() => _texture;

        internal static Native.SDL_Texture* ToNative(Texture? texture) =>
            texture == null ? (Native.SDL_Texture*)null : texture._texture;
    }
}
