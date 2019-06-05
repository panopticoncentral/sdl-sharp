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
        Jpeg,

        /// <summary>
        /// BT.601 (the default).
        /// </summary>
        Bt601,

        /// <summary>
        /// BT.709.
        /// </summary>
        Bt709,

        /// <summary>
        /// BT.601 for SD content, BT.709 for HD content.
        /// </summary>
        Automatic
    }
}
