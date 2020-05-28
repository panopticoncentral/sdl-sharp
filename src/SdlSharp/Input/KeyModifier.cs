using System;

namespace SdlSharp.Input
{
    /// <summary>
    /// Modifiers to keys on the keyboard.
    /// </summary>
    [Flags]
    public enum KeyModifier : ushort
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// Left shift is pressed.
        /// </summary>
        LeftShift = 0x0001,

        /// <summary>
        /// Right shift is pressed.
        /// </summary>
        RightShift = 0x0002,

        /// <summary>
        /// A shift key is pressed.
        /// </summary>
        Shift = LeftShift | RightShift,

        /// <summary>
        /// Left ctrl is pressed.
        /// </summary>
        LeftCtrl = 0x0040,

        /// <summary>
        /// Right ctrl is pressed.
        /// </summary>
        RightCtrl = 0x0080,

        /// <summary>
        /// A ctrl key is pressed.
        /// </summary>
        Ctrl = LeftCtrl | RightCtrl,

        /// <summary>
        /// Left alt is pressed.
        /// </summary>
        LeftAlt = 0x0100,

        /// <summary>
        /// Right alt is pressed.
        /// </summary>
        RightAlt = 0x0200,

        /// <summary>
        /// An alt key is pressed.
        /// </summary>
        Alt = LeftAlt | RightAlt,

        /// <summary>
        /// Left GUI key is pressed.
        /// </summary>
        LeftGui = 0x0400,

        /// <summary>
        /// Right GUI key is pressed.
        /// </summary>
        RightGui = 0x0800,

        /// <summary>
        /// A GUI key is pressed.
        /// </summary>
        Gui = LeftGui | RightGui,

        /// <summary>
        /// NumLock key is active.
        /// </summary>
        Num = 0x1000,

        /// <summary>
        /// CapsLock key is active.
        /// </summary>
        Caps = 0x2000,

        /// <summary>
        /// Mode key is active.
        /// </summary>
        Mode = 0x4000,

        /// <summary>
        /// Reserved.
        /// </summary>
        Reserved = 0x8000
    }
}
