using System;

namespace SdlSharp.Graphics
{
    /// <summary>
    /// A system window message.
    /// </summary>
    public readonly struct SystemWindowMessage
    {
        /// <summary>
        /// The HWND.
        /// </summary>
        public readonly IntPtr Hwnd;

        /// <summary>
        /// The message.
        /// </summary>
        public readonly uint Msg;

        /// <summary>
        /// The WParam parameter.
        /// </summary>
        public readonly UIntPtr WParam;

        /// <summary>
        /// The LParam parameter.
        /// </summary>
        public readonly IntPtr LParam;

        internal SystemWindowMessage(IntPtr hwnd, uint msg, UIntPtr wParam, IntPtr lParam)
        {
            Hwnd = hwnd;
            Msg = msg;
            WParam = wParam;
            LParam = lParam;
        }
    }
}
