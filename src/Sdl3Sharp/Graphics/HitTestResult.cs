namespace Sdl3Sharp.Graphics;

/// <summary>
/// Hit test results for custom window dragging and resizing.
/// </summary>
public enum HitTestResult
{
    /// <summary>Region is normal. No special properties.</summary>
    Normal,
    /// <summary>Region can drag entire window.</summary>
    Draggable,
    /// <summary>Region is top-left resize handle.</summary>
    ResizeTopLeft,
    /// <summary>Region is top resize handle.</summary>
    ResizeTop,
    /// <summary>Region is top-right resize handle.</summary>
    ResizeTopRight,
    /// <summary>Region is right resize handle.</summary>
    ResizeRight,
    /// <summary>Region is bottom-right resize handle.</summary>
    ResizeBottomRight,
    /// <summary>Region is bottom resize handle.</summary>
    ResizeBottom,
    /// <summary>Region is bottom-left resize handle.</summary>
    ResizeBottomLeft,
    /// <summary>Region is left resize handle.</summary>
    ResizeLeft
}
