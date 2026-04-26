namespace SdlSharp.Gui;

/// <summary>Mouse cursor identifier (for <see cref="Gui.GetMouseCursor"/> / <see cref="Gui.SetMouseCursor"/>).</summary>
public enum MouseCursor
{
    /// <summary>No cursor.</summary>
    None = -1,
    /// <summary>Default arrow.</summary>
    Arrow = 0,
    /// <summary>I-beam (text input).</summary>
    TextInput,
    /// <summary>Resize in any direction (unused by ImGui itself).</summary>
    ResizeAll,
    /// <summary>Resize vertically (over a horizontal border).</summary>
    ResizeNS,
    /// <summary>Resize horizontally (over a vertical border).</summary>
    ResizeEW,
    /// <summary>Resize bottom-left / top-right diagonal.</summary>
    ResizeNESW,
    /// <summary>Resize top-left / bottom-right diagonal.</summary>
    ResizeNWSE,
    /// <summary>Pointing hand (used for hyperlinks).</summary>
    Hand,
    /// <summary>Hourglass / busy.</summary>
    Wait,
    /// <summary>In-progress (busy but interactive).</summary>
    Progress,
    /// <summary>Crossed-out / not-allowed.</summary>
    NotAllowed,
}
