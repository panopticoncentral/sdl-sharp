namespace SdlSharp.Graphics
{
    /// <summary>
    /// Conversion modes for YUV to RGB.
    /// </summary>
    public enum YuvConversionMode
    {
        /// <summary>
        /// Full range JPEG.
        /// </summary>
        Jpeg = Native.SDL_YUV_CONVERSION_MODE.SDL_YUV_CONVERSION_JPEG,

        /// <summary>
        /// BT.601 (the default).
        /// </summary>
        Bt601 = Native.SDL_YUV_CONVERSION_MODE.SDL_YUV_CONVERSION_BT601,

        /// <summary>
        /// BT.709.
        /// </summary>
        Bt709 = Native.SDL_YUV_CONVERSION_MODE.SDL_YUV_CONVERSION_BT709,

        /// <summary>
        /// BT.601 for SD content, BT.709 for HD content.
        /// </summary>
        Automatic = Native.SDL_YUV_CONVERSION_MODE.SDL_YUV_CONVERSION_AUTOMATIC
    }
}
