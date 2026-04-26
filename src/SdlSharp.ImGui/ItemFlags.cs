namespace SdlSharp.ImGui;

/// <summary>Item flags pushed via <see cref="ImGui.PushItemFlag"/>; affect all subsequently submitted items.</summary>
[Flags]
public enum ItemFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Disable keyboard tabbing (lighter version of NoNav).</summary>
    NoTabStop = 1 << 0,
    /// <summary>Disable any form of focusing (keyboard/gamepad navigation and SetKeyboardFocusHere).</summary>
    NoNav = 1 << 1,
    /// <summary>Disable item being a candidate for default focus.</summary>
    NoNavDefaultFocus = 1 << 2,
    /// <summary>Buttons repeat while held (uses <c>io.KeyRepeatDelay</c> / <c>KeyRepeatRate</c>).</summary>
    ButtonRepeat = 1 << 3,
    /// <summary>MenuItem / Selectable automatically close their parent popup window.</summary>
    AutoClosePopups = 1 << 4,
    /// <summary>Allow submitting items with the same identifier without a warning.</summary>
    AllowDuplicateId = 1 << 5,
}
