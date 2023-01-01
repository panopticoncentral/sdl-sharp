namespace SdlSharp.Graphics
{
    /// <summary>
    /// The result of a hit test.
    /// </summary>
    public enum HitTestResult
    {
        /// <summary>
        /// Normal
        /// </summary>
        Normal = Native.SDL_HitTestResult.SDL_HITTEST_NORMAL,

        /// <summary>
        /// Draggable margin
        /// </summary>
        Draggable = Native.SDL_HitTestResult.SDL_HITTEST_DRAGGABLE,

        /// <summary>
        /// Resize margin top left
        /// </summary>
        ResizeTopLeft = Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_TOPLEFT,

        /// <summary>
        /// Resize margin top
        /// </summary>
        ResizeTop = Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_TOP,

        /// <summary>
        /// Resize margin top right
        /// </summary>
        ResizeTopRight = Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_TOPRIGHT,

        /// <summary>
        /// Resize margin right
        /// </summary>
        ResizeRight = Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_RIGHT,

        /// <summary>
        /// Resize margin bottom right
        /// </summary>
        ResizeBottomRight = Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOMRIGHT,

        /// <summary>
        /// Resize margin bottom
        /// </summary>
        ResizeBottom = Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOM,

        /// <summary>
        /// Resize margin bottom left
        /// </summary>
        ResizeBottomLeft = Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOMLEFT,

        /// <summary>
        /// Resize margin left
        /// </summary>
        ResizeLeft = Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_LEFT
    }
}
