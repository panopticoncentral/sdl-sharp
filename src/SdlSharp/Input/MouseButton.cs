using System;

namespace SdlSharp.Input
{
    /// <summary>
    /// A mouse button.
    /// </summary>
    [Flags]
    public enum MouseButton
    {
        None = 0x0,
        Left = 0x1,
        Middle = 0x2,
        Right = 0x4,
        X1 = 0x8,
        X2 = 0x10
    }
}
