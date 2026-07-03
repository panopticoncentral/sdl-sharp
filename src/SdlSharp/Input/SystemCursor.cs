namespace SdlSharp.Input;

/// <summary>
/// Standard system cursor shapes.
/// </summary>
public enum SystemCursor
{
    /// <summary>Default cursor (usually an arrow).</summary>
    Default = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_DEFAULT,
    /// <summary>Text selection (usually an I-beam).</summary>
    Text = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_TEXT,
    /// <summary>Wait cursor (usually an hourglass or spinner).</summary>
    Wait = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_WAIT,
    /// <summary>Crosshair.</summary>
    Crosshair = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_CROSSHAIR,
    /// <summary>Program is busy but still interactive.</summary>
    Progress = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_PROGRESS,
    /// <summary>Double arrow pointing northwest and southeast.</summary>
    NwseResize = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NWSE_RESIZE,
    /// <summary>Double arrow pointing northeast and southwest.</summary>
    NeswResize = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NESW_RESIZE,
    /// <summary>Double arrow pointing west and east.</summary>
    EwResize = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_EW_RESIZE,
    /// <summary>Double arrow pointing north and south.</summary>
    NsResize = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NS_RESIZE,
    /// <summary>Four-pointed move arrow.</summary>
    Move = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_MOVE,
    /// <summary>Action not permitted (usually a slashed circle).</summary>
    NotAllowed = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NOT_ALLOWED,
    /// <summary>Pointer that indicates a link (usually a pointing hand).</summary>
    Pointer = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_POINTER,
    /// <summary>Window resize top-left.</summary>
    NwResize = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NW_RESIZE,
    /// <summary>Window resize top.</summary>
    NResize = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_N_RESIZE,
    /// <summary>Window resize top-right.</summary>
    NeResize = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NE_RESIZE,
    /// <summary>Window resize right.</summary>
    EResize = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_E_RESIZE,
    /// <summary>Window resize bottom-right.</summary>
    SeResize = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SE_RESIZE,
    /// <summary>Window resize bottom.</summary>
    SResize = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_S_RESIZE,
    /// <summary>Window resize bottom-left.</summary>
    SwResize = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SW_RESIZE,
    /// <summary>Window resize left.</summary>
    WResize = (int)Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_W_RESIZE,
}
