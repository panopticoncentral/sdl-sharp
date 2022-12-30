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
        public required string? Name { get; init; }

        /// <summary>
        /// The flags of the renderer.
        /// </summary>
        public required RendererOptions Flags { get; init; }

        /// <summary>
        /// The texture formats supported.
        /// </summary>
        public required IReadOnlyList<EnumeratedPixelFormat> TextureFormats { get; init; }

        /// <summary>
        /// The maximum texture size.
        /// </summary>
        public required Size MaxTextureSize { get; init; }

        internal static RendererInfo FromNative(Native.SDL_RendererInfo* info)
        {
            var formats = new EnumeratedPixelFormat[(int)info->num_texture_formats];
            for (var index = 0; index < (int)info->num_texture_formats; index++)
            {
                formats[index] = new EnumeratedPixelFormat(info->texture_formats[index]);
            }

            return new RendererInfo
            {
                Name = Native.Utf8ToString(info->name),
                Flags = (RendererOptions)info->flags,
                TextureFormats = formats,
                MaxTextureSize = (info->max_texture_width, info->max_texture_height)
            };
        }
    }
}
