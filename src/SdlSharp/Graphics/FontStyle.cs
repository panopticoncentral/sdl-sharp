using System;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// Font styles.
    /// </summary>
    [Flags]
    public enum FontStyle
    {
        Normal = 0x00,
        Bold = 0x01,
        Italic = 0x02,
        Underline = 0x04,
        Strikethrough = 0x08
    }
}
