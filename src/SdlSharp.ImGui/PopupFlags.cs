namespace SdlSharp.Gui;

/// <summary>Flags for <see cref="Gui.OpenPopup"/>, BeginPopupContext*(), and IsPopupOpen().</summary>
[Flags]
public enum PopupFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>For BeginPopupContext*: open on Left Mouse release. Only one button allowed.</summary>
    MouseButtonLeft = 1 << 2,
    /// <summary>For BeginPopupContext*: open on Right Mouse release (default).</summary>
    MouseButtonRight = 2 << 2,
    /// <summary>For BeginPopupContext*: open on Middle Mouse release.</summary>
    MouseButtonMiddle = 3 << 2,
    /// <summary>Don't reopen same popup if already open.</summary>
    NoReopen = 1 << 5,
    /// <summary>Don't open if there's already a popup at the same level of the popup stack.</summary>
    NoOpenOverExistingPopup = 1 << 7,
    /// <summary>For BeginPopupContextWindow: don't return true when hovering items, only when hovering empty space.</summary>
    NoOpenOverItems = 1 << 8,
    /// <summary>For IsPopupOpen: ignore the ID parameter and test for any popup.</summary>
    AnyPopupId = 1 << 10,
    /// <summary>For IsPopupOpen: search/test at any level of the popup stack.</summary>
    AnyPopupLevel = 1 << 11,
    /// <summary>Combination: AnyPopupId | AnyPopupLevel.</summary>
    AnyPopup = AnyPopupId | AnyPopupLevel,
}
