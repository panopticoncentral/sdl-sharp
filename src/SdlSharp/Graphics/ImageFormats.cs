using System;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// Image formats
    /// </summary>
    [Flags]
    public enum ImageFormats
    {
        Jpg = 0x00000001,
        Png = 0x00000002,
        Tif = 0x00000004,
        Webp = 0x00000008
    }
}
