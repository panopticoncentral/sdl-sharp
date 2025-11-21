namespace Sdl3Sharp.Input;

/// <summary>
/// System cursor types for creating standard platform cursors.
/// </summary>
public enum SystemCursor
{
    /// <summary>Default cursor. Usually an arrow.</summary>
    Default,
    /// <summary>Text selection. Usually an I-beam.</summary>
    Text,
    /// <summary>Wait. Usually an hourglass or watch or spinning ball.</summary>
    Wait,
    /// <summary>Crosshair.</summary>
    Crosshair,
    /// <summary>Program is busy but still interactive. Usually it's Wait with an arrow.</summary>
    Progress,
    /// <summary>Double arrow pointing northwest and southeast.</summary>
    NwseResize,
    /// <summary>Double arrow pointing northeast and southwest.</summary>
    NeswResize,
    /// <summary>Double arrow pointing west and east.</summary>
    EwResize,
    /// <summary>Double arrow pointing north and south.</summary>
    NsResize,
    /// <summary>Four pointed arrow pointing north, south, east, and west.</summary>
    Move,
    /// <summary>Not permitted. Usually a slashed circle or crossbones.</summary>
    NotAllowed,
    /// <summary>Pointer that indicates a link. Usually a pointing hand.</summary>
    Pointer,
    /// <summary>Window resize top-left.</summary>
    NwResize,
    /// <summary>Window resize top.</summary>
    NResize,
    /// <summary>Window resize top-right.</summary>
    NeResize,
    /// <summary>Window resize right.</summary>
    EResize,
    /// <summary>Window resize bottom-right.</summary>
    SeResize,
    /// <summary>Window resize bottom.</summary>
    SResize,
    /// <summary>Window resize bottom-left.</summary>
    SwResize,
    /// <summary>Window resize left.</summary>
    WResize
}
