namespace SdlSharp.ImGui;

/// <summary>Flags for InputText, InputFloat/Int/Double, and related input widgets.</summary>
[Flags]
public enum InputTextFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Allow 0123456789.+-*/</summary>
    CharsDecimal = 1 << 0,
    /// <summary>Allow 0123456789ABCDEFabcdef.</summary>
    CharsHexadecimal = 1 << 1,
    /// <summary>Allow 0123456789.+-*/eE (scientific notation input).</summary>
    CharsScientific = 1 << 2,
    /// <summary>Turn a..z into A..Z.</summary>
    CharsUppercase = 1 << 3,
    /// <summary>Filter out spaces, tabs.</summary>
    CharsNoBlank = 1 << 4,
    /// <summary>Pressing TAB inputs a '\t' character into the text field.</summary>
    AllowTabInput = 1 << 5,
    /// <summary>Return 'true' when Enter is pressed.</summary>
    EnterReturnsTrue = 1 << 6,
    /// <summary>Escape key clears content if not empty, and deactivate otherwise.</summary>
    EscapeClearsAll = 1 << 7,
    /// <summary>Validate with Enter, add new line with Ctrl+Enter (multi-line).</summary>
    CtrlEnterForNewLine = 1 << 8,
    /// <summary>Read-only mode.</summary>
    ReadOnly = 1 << 9,
    /// <summary>Password mode, display all characters as '*'.</summary>
    Password = 1 << 10,
    /// <summary>Overwrite mode.</summary>
    AlwaysOverwrite = 1 << 11,
    /// <summary>Select entire text when first taking mouse focus.</summary>
    AutoSelectAll = 1 << 12,
    /// <summary>InputFloat/Int/Scalar: parse empty string as zero value.</summary>
    ParseEmptyRefVal = 1 << 13,
    /// <summary>InputFloat/Int/Scalar: when value is zero, do not display it.</summary>
    DisplayEmptyRefVal = 1 << 14,
    /// <summary>Disable following the cursor horizontally.</summary>
    NoHorizontalScroll = 1 << 15,
    /// <summary>Disable undo/redo.</summary>
    NoUndoRedo = 1 << 16,
    /// <summary>When text doesn't fit, elide left side.</summary>
    ElideLeft = 1 << 17,
    /// <summary>Callback on pressing TAB (for completion handling).</summary>
    CallbackCompletion = 1 << 18,
    /// <summary>Callback on pressing Up/Down arrows (for history handling).</summary>
    CallbackHistory = 1 << 19,
    /// <summary>Callback on each iteration.</summary>
    CallbackAlways = 1 << 20,
    /// <summary>Callback on character inputs to replace or discard them.</summary>
    CallbackCharFilter = 1 << 21,
    /// <summary>Callback on buffer capacity changes request.</summary>
    CallbackResize = 1 << 22,
    /// <summary>Callback on any edit.</summary>
    CallbackEdit = 1 << 23,
    /// <summary>InputTextMultiline: word-wrap lines that are too long.</summary>
    WordWrap = 1 << 24,
}
