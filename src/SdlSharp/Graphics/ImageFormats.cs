namespace SdlSharp.Graphics
{
    /// <summary>
    /// Image formats
    /// </summary>
    [Flags]
    public enum ImageFormats
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// JPG
        /// </summary>
        Jpg = Native.IMG_InitFlags.IMG_INIT_JPG,

        /// <summary>
        /// PNG
        /// </summary>
        Png = Native.IMG_InitFlags.IMG_INIT_PNG,

        /// <summary>
        /// TIFF
        /// </summary>
        Tif = Native.IMG_InitFlags.IMG_INIT_TIF,

        /// <summary>
        /// WebP
        /// </summary>
        Webp = Native.IMG_InitFlags.IMG_INIT_WEBP,

        /// <summary>
        /// JXL
        /// </summary>
        Jxl = Native.IMG_InitFlags.IMG_INIT_JXL,

        /// <summary>
        /// AVIF
        /// </summary>
        Avif = Native.IMG_InitFlags.IMG_INIT_AVIF
    }
}
