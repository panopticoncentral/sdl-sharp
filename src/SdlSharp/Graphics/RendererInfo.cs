namespace SdlSharp.Graphics
{
    /// <summary>
    /// Information about a renderer.
    /// </summary>
    public sealed unsafe class RendererInfo
    {
        /// <summary>
        /// The name of the renderer.
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// The flags of the renderer.
        /// </summary>
        public RendererOptions Flags { get; }

        /// <summary>
        /// The texture formats supported.
        /// </summary>
        public IReadOnlyList<EnumeratedPixelFormat> TextureFormats { get; }

        /// <summary>
        /// The maximum texture size.
        /// </summary>
        public Size MaxTextureSize { get; }

        internal RendererInfo(Native.SDL_RendererInfo* info)
        {
            var formats = new EnumeratedPixelFormat[(int)info->num_texture_formats];
            for (var index = 0; index < (int)info->num_texture_formats; index++)
            {
                formats[index] = new EnumeratedPixelFormat(info->texture_formats[index]);
            }

            Name = Native.Utf8ToString(info->name);
            Flags = (RendererOptions)info->flags;
            TextureFormats = formats;
            MaxTextureSize = (info->max_texture_width, info->max_texture_height);
        }
    }
}
