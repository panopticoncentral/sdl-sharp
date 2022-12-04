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
        None = 0x00000000,

        /// <summary>
        /// JPG
        /// </summary>
        Jpg = 0x00000001,

        /// <summary>
        /// PNG
        /// </summary>
        Png = 0x00000002,

        /// <summary>
        /// TIFF
        /// </summary>
        Tif = 0x00000004,

        /// <summary>
        /// WebP
        /// </summary>
        Webp = 0x00000008
    }
}
