using System;

namespace SdlSharp.Input
{
    /// <summary>
    /// A mouse button.
    /// </summary>
    [Flags]
    public enum MouseButton
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Left
        /// </summary>
        Left = 0x1,

        /// <summary>
        /// Middle
        /// </summary>
        Middle = 0x2,
        
        /// <summary>
        /// Right
        /// </summary>
        Right = 0x4,
        
        /// <summary>
        /// X1
        /// </summary>
        X1 = 0x8,

        /// <summary>
        /// X2
        /// </summary>
        X2 = 0x10
    }
}
