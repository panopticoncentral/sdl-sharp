namespace SdlSharp.ImGui;

/// <summary>Flags for <see cref="ImGui.BeginTabItem(string, TabItemFlags)"/>.</summary>
[Flags]
public enum TabItemFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Display a dot next to the title + set NoAssumedClosure.</summary>
    UnsavedDocument = 1 << 0,
    /// <summary>Trigger flag to programmatically make the tab selected.</summary>
    SetSelected = 1 << 1,
    /// <summary>Disable behavior of closing tabs with middle mouse button.</summary>
    NoCloseWithMiddleMouseButton = 1 << 2,
    /// <summary>Don't call PushID/PopID on BeginTabItem/EndTabItem.</summary>
    NoPushId = 1 << 3,
    /// <summary>Disable tooltip for the given tab.</summary>
    NoTooltip = 1 << 4,
    /// <summary>Disable reordering this tab or having another tab cross over this tab.</summary>
    NoReorder = 1 << 5,
    /// <summary>Enforce the tab position to the left of the tab bar.</summary>
    Leading = 1 << 6,
    /// <summary>Enforce the tab position to the right of the tab bar.</summary>
    Trailing = 1 << 7,
    /// <summary>Tab is selected when trying to close + closure is not immediately assumed.</summary>
    NoAssumedClosure = 1 << 8,
}
