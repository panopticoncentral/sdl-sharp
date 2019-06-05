using System;

namespace SdlSharp.Input
{
    /// <summary>
    /// Flags describing a hat's state.
    /// </summary>
    [Flags]
    public enum HatFlags : byte
    {
        /// <summary>
        /// Centered.
        /// </summary>
        Centered = 0x00,

        /// <summary>
        /// Up.
        /// </summary>
        Up = 0x01,

        /// <summary>
        /// Right.
        /// </summary>
        Right = 0x02,

        /// <summary>
        /// Right and up.
        /// </summary>
        RightUp = Right | Up,
        
        /// <summary>
        /// Down.
        /// </summary>
        Down = 0x04,

        /// <summary>
        /// Right and down.
        /// </summary>
        RightDown = Right | Down,

        /// <summary>
        /// Left.
        /// </summary>
        Left = 0x08,

        /// <summary>
        /// Left and up.
        /// </summary>
        LeftUp = Left | Up,

        /// <summary>
        /// Left and down.
        /// </summary>
        LeftDown = Left | Down
    }
}
