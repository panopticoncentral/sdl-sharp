namespace SdlSharp.Graphics
{
    /// <summary>
    /// Hinting levels for a font.
    /// </summary>
    public enum FontHinting
    {
        /// <summary>
        /// Normal
        /// </summary>
        Normal = Native.TTF_HINTING_NORMAL,

        /// <summary>
        /// Light
        /// </summary>
        Light = Native.TTF_HINTING_LIGHT,

        /// <summary>
        /// Mono
        /// </summary>
        Mono = Native.TTF_HINTING_MONO,

        /// <summary>
        /// None
        /// </summary>
        None = Native.TTF_HINTING_NONE,

        /// <summary>
        /// Light Subpixel
        /// </summary>
        LightSubPixel = Native.TTF_HINTING_LIGHT_SUBPIXEL
    }
}
