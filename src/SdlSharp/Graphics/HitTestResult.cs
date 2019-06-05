namespace SdlSharp.Graphics
{
    /// <summary>
    /// The result of a hit test.
    /// </summary>
    public enum HitTestResult
    {
        Normal,
        Draggable,
        ResizeTopLeft,
        ResizeTop,
        ResizeTopRight,
        ResizeRight,
        ResizeBottomRight,
        ResizeBottom,
        ResizeBottomLeft,
        ResizeLeft
    }
}
