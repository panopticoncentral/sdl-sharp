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
        public nint Hwnd { get; }

        /// <summary>
        /// The message.
        /// </summary>
        public uint Msg { get; }

        /// <summary>
        /// The WParam parameter.
        /// </summary>
        public nuint WParam { get; }

        /// <summary>
        /// The LParam parameter.
        /// </summary>
        public nint LParam { get; }

        internal SystemWindowMessage(nint hwnd, uint msg, nuint wParam, nint lParam)
        {
            Hwnd = hwnd;
            Msg = msg;
            WParam = wParam;
            LParam = lParam;
        }
    }
}
