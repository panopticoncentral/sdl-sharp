namespace SdlSharp.Graphics
{
    /// <summary>
    /// Font styles.
    /// </summary>
    [Flags]
    public enum FontStyle
    {
        /// <summary>
        /// Normal
        /// </summary>
        Normal = Native.TTF_STYLE_NORMAL,

        /// <summary>
        /// Bold
        /// </summary>
        Bold = Native.TTF_STYLE_BOLD,

        /// <summary>
        /// Italic
        /// </summary>
        Italic = Native.TTF_STYLE_ITALIC,

        /// <summary>
        /// Underline
        /// </summary>
        Underline = Native.TTF_STYLE_UNDERLINE,

        /// <summary>
        /// Strikethrough
        /// </summary>
        Strikethrough = Native.TTF_STYLE_STRIKETHROUGH
    }
}
