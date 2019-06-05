using System;

namespace SdlSharp.Input
{
    /// <summary>
    /// Modifiers to keys on the keyboard.
    /// </summary>
    [Flags]
    public enum KeyModifier : ushort
    {
        None = 0x0000,
        LeftShift = 0x0001,
        RightShift = 0x0002,
        Shift = LeftShift | RightShift,
        LeftCtrl = 0x0040,
        RightCtrl = 0x0080,
        Ctrl = LeftCtrl | RightCtrl,
        LeftAlt = 0x0100,
        RightAlt = 0x0200,
        Alt = LeftAlt | RightAlt,
        LeftGui = 0x0400,
        RightGui = 0x0800,
        Gui = LeftGui | RightGui,
        Num = 0x1000,
        Caps = 0x2000,
        Mode = 0x4000,
        Reserved = 0x8000
    }
}
