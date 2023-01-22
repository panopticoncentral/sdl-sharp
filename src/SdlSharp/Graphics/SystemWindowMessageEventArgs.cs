namespace SdlSharp.Graphics
{
    /// <summary>
    /// Information about a system window message.
    /// </summary>
    public sealed unsafe class SystemWindowMessageEventArgs : SdlEventArgs
    {
        /// <summary>
        /// The window's HWND.
        /// </summary>
        public nint Hwnd { get; }

        /// <summary>
        /// The message.
        /// </summary>
        public uint Msg { get; }

        /// <summary>
        /// The wParam of the message.
        /// </summary>
        public nuint WParam { get; }

        /// <summary>
        /// The lParam of the message.
        /// </summary>
        public nint LParam { get; }

        internal SystemWindowMessageEventArgs(Native.SDL_SysWMEvent e) : base(e.timestamp)
        {
            Hwnd = e.msg->win.hwnd;
            Msg = e.msg->win.msg;
            WParam = e.msg->win.wParam;
            LParam = e.msg->win.lParam;
        }
    }
}
