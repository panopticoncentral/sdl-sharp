namespace SdlSharp.Graphics
{
    /// <summary>
    /// Native window manager information about a window.
    /// </summary>
    public readonly unsafe struct WindowManagerInfo
    {
        /// <summary>
        /// The window's HWND.
        /// </summary>
        public nuint Window { get; }

        /// <summary>
        /// The window's HDC.
        /// </summary>
        public nuint Hdc { get; }

        /// <summary>
        /// The window's HINSTANCE
        /// </summary>
        public nuint Hinstance { get; }

        internal WindowManagerInfo(Native.SDL_SysWMinfo* info)
        {
            Window = info->win.window;
            Hdc = info->win.hdc;
            Hinstance = info->win.hinstance;
        }
    }
}
