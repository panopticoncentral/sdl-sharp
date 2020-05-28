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
        Normal,

        /// <summary>
        /// Draggable margin
        /// </summary>
        Draggable,

        /// <summary>
        /// Resize margin top left
        /// </summary>
        ResizeTopLeft,

        /// <summary>
        /// Resize margin top
        /// </summary>
        ResizeTop,

        /// <summary>
        /// Resize margin top right
        /// </summary>
        ResizeTopRight,

        /// <summary>
        /// Resize margin right
        /// </summary>
        ResizeRight,

        /// <summary>
        /// Resize margin bottom right
        /// </summary>
        ResizeBottomRight,

        /// <summary>
        /// Resize margin bottom
        /// </summary>
        ResizeBottom,

        /// <summary>
        /// Resize margin bottom left
        /// </summary>
        ResizeBottomLeft,

        /// <summary>
        /// Resize margin left
        /// </summary>
        ResizeLeft
    }
}
