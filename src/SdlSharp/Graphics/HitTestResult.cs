namespace SdlSharp.Graphics;

/// <summary>
/// Possible return values from a <see cref="Window.HitTestHandler"/>.
/// </summary>
public enum HitTestResult
{
    /// <summary>Region is normal. No special properties.</summary>
    Normal = (int)Native.SDL_HitTestResult.SDL_HITTEST_NORMAL,
    /// <summary>Region can drag the entire window.</summary>
    Draggable = (int)Native.SDL_HitTestResult.SDL_HITTEST_DRAGGABLE,
    /// <summary>Region is the resizable top-left corner border.</summary>
    ResizeTopLeft = (int)Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_TOPLEFT,
    /// <summary>Region is the resizable top border.</summary>
    ResizeTop = (int)Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_TOP,
    /// <summary>Region is the resizable top-right corner border.</summary>
    ResizeTopRight = (int)Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_TOPRIGHT,
    /// <summary>Region is the resizable right border.</summary>
    ResizeRight = (int)Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_RIGHT,
    /// <summary>Region is the resizable bottom-right corner border.</summary>
    ResizeBottomRight = (int)Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOMRIGHT,
    /// <summary>Region is the resizable bottom border.</summary>
    ResizeBottom = (int)Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOM,
    /// <summary>Region is the resizable bottom-left corner border.</summary>
    ResizeBottomLeft = (int)Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOMLEFT,
    /// <summary>Region is the resizable left border.</summary>
    ResizeLeft = (int)Native.SDL_HitTestResult.SDL_HITTEST_RESIZE_LEFT,
}
