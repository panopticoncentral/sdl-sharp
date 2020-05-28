using System;

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
        Normal = 0x00,

        /// <summary>
        /// Bold
        /// </summary>
        Bold = 0x01,

        /// <summary>
        /// Italic
        /// </summary>
        Italic = 0x02,

        /// <summary>
        /// Underline
        /// </summary>
        Underline = 0x04,

        /// <summary>
        /// Strikethrough
        /// </summary>
        Strikethrough = 0x08
    }
}
