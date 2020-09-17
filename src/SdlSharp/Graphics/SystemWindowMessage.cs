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
        public readonly nint Hwnd;

        /// <summary>
        /// The message.
        /// </summary>
        public readonly uint Msg;

        /// <summary>
        /// The WParam parameter.
        /// </summary>
        public readonly nuint WParam;

        /// <summary>
        /// The LParam parameter.
        /// </summary>
        public readonly nint LParam;

        internal SystemWindowMessage(nint hwnd, uint msg, nuint wParam, nint lParam)
        {
            Hwnd = hwnd;
            Msg = msg;
            WParam = wParam;
            LParam = lParam;
        }
    }
}
