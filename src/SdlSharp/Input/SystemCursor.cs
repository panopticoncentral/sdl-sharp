namespace SdlSharp.Input
{
    /// <summary>
    /// System cursors.
    /// </summary>
    public enum SystemCursor
    {
        /// <summary>
        /// Arrow cursor
        /// </summary>
        Arrow = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_ARROW,

        /// <summary>
        /// I-beam cursor
        /// </summary>
        IBeam = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_IBEAM,

        /// <summary>
        /// Wait cursor
        /// </summary>
        Wait = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_WAIT,

        /// <summary>
        /// Crosshair cursor
        /// </summary>
        Crosshair = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_CROSSHAIR,

        /// <summary>
        /// Wait arrow cursor
        /// </summary>
        WaitArrow = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_WAITARROW,

        /// <summary>
        /// Size NW/SE cursor
        /// </summary>
        SizeNWSE = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZENWSE,

        /// <summary>
        /// Size NE/SW cursor
        /// </summary>
        SizeNESW = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZENESW,

        /// <summary>
        /// Size W/E cursor
        /// </summary>
        SizeWE = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZEWE,

        /// <summary>
        /// Size N/S cursor
        /// </summary>
        SizeNS = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZENS,

        /// <summary>
        /// Size all cursor
        /// </summary>
        SizeAll = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZEALL,

        /// <summary>
        /// No cursor
        /// </summary>
        No = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NO,

        /// <summary>
        /// Hand cursor
        /// </summary>
        Hand = Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_HAND,

        /// <summary>
        /// Number of system cursors
        /// </summary>
        SystemCursorsCount = Native.SDL_SystemCursor.SDL_NUM_SYSTEM_CURSORS
    }
}
