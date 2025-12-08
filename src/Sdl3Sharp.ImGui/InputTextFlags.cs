using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for InputText().
/// </summary>
/// <remarks>
/// These are per-item flags. There are shared flags in <see cref="IO"/>:
/// <c>ConfigInputTextCursorBlink</c> and <c>ConfigInputTextEnterKeepActive</c>.
/// </remarks>
[Flags]
public enum InputTextFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiInputTextFlags.None,

    /// <summary>
    /// Allow 0123456789.+-*/
    /// </summary>
    CharsDecimal = ImGuiInputTextFlags.CharsDecimal,

    /// <summary>
    /// Allow 0123456789ABCDEFabcdef
    /// </summary>
    CharsHexadecimal = ImGuiInputTextFlags.CharsHexadecimal,

    /// <summary>
    /// Allow 0123456789.+-*/eE (Scientific notation input)
    /// </summary>
    CharsScientific = ImGuiInputTextFlags.CharsScientific,

    /// <summary>
    /// Turn a..z into A..Z
    /// </summary>
    CharsUppercase = ImGuiInputTextFlags.CharsUppercase,

    /// <summary>
    /// Filter out spaces, tabs.
    /// </summary>
    CharsNoBlank = ImGuiInputTextFlags.CharsNoBlank,

    /// <summary>
    /// Pressing TAB input a '\t' character into the text field.
    /// </summary>
    AllowTabInput = ImGuiInputTextFlags.AllowTabInput,

    /// <summary>
    /// Return 'true' when Enter is pressed (as opposed to every time the value was modified). Consider using IsItemDeactivatedAfterEdit() instead!
    /// </summary>
    EnterReturnsTrue = ImGuiInputTextFlags.EnterReturnsTrue,

    /// <summary>
    /// Escape key clears content if not empty, and deactivate otherwise (contrast to default behavior of Escape to revert).
    /// </summary>
    EscapeClearsAll = ImGuiInputTextFlags.EscapeClearsAll,

    /// <summary>
    /// In multi-line mode, validate with Enter, add new line with Ctrl+Enter (default is opposite: validate with Ctrl+Enter, add line with Enter).
    /// </summary>
    CtrlEnterForNewLine = ImGuiInputTextFlags.CtrlEnterForNewLine,

    /// <summary>
    /// Read-only mode.
    /// </summary>
    ReadOnly = ImGuiInputTextFlags.ReadOnly,

    /// <summary>
    /// Password mode, display all characters as '*', disable copy.
    /// </summary>
    Password = ImGuiInputTextFlags.Password,

    /// <summary>
    /// Overwrite mode.
    /// </summary>
    AlwaysOverwrite = ImGuiInputTextFlags.AlwaysOverwrite,

    /// <summary>
    /// Select entire text when first taking mouse focus.
    /// </summary>
    AutoSelectAll = ImGuiInputTextFlags.AutoSelectAll,

    /// <summary>
    /// InputFloat(), InputInt(), InputScalar() etc. only: parse empty string as zero value.
    /// </summary>
    ParseEmptyRefVal = ImGuiInputTextFlags.ParseEmptyRefVal,

    /// <summary>
    /// InputFloat(), InputInt(), InputScalar() etc. only: when value is zero, do not display it. Generally used with ParseEmptyRefVal.
    /// </summary>
    DisplayEmptyRefVal = ImGuiInputTextFlags.DisplayEmptyRefVal,

    /// <summary>
    /// Disable following the cursor horizontally.
    /// </summary>
    NoHorizontalScroll = ImGuiInputTextFlags.NoHorizontalScroll,

    /// <summary>
    /// Disable undo/redo. Note that input text owns the text data while active, if you want to provide your own undo/redo stack you need e.g. to call ClearActiveID().
    /// </summary>
    NoUndoRedo = ImGuiInputTextFlags.NoUndoRedo,

    /// <summary>
    /// When text doesn't fit, elide left side to ensure right side stays visible. Useful for path/filenames. Single-line only!
    /// </summary>
    ElideLeft = ImGuiInputTextFlags.ElideLeft,

    /// <summary>
    /// Callback on pressing TAB (for completion handling).
    /// </summary>
    CallbackCompletion = ImGuiInputTextFlags.CallbackCompletion,

    /// <summary>
    /// Callback on pressing Up/Down arrows (for history handling).
    /// </summary>
    CallbackHistory = ImGuiInputTextFlags.CallbackHistory,

    /// <summary>
    /// Callback on each iteration. User code may query cursor position, modify text buffer.
    /// </summary>
    CallbackAlways = ImGuiInputTextFlags.CallbackAlways,

    /// <summary>
    /// Callback on character inputs to replace or discard them. Modify 'EventChar' to replace or discard, or return 1 in callback to discard.
    /// </summary>
    CallbackCharFilter = ImGuiInputTextFlags.CallbackCharFilter,

    /// <summary>
    /// Callback on buffer capacity changes request (beyond 'buf_size' parameter value), allowing the string to grow.
    /// </summary>
    CallbackResize = ImGuiInputTextFlags.CallbackResize,

    /// <summary>
    /// Callback on any edit. Note that InputText() already returns true on edit + you can always use IsItemEdited(). The callback is useful to manipulate the underlying buffer while focus is active.
    /// </summary>
    CallbackEdit = ImGuiInputTextFlags.CallbackEdit,

    /// <summary>
    /// InputTextMultiline(): word-wrap lines that are too long.
    /// </summary>
    WordWrap = ImGuiInputTextFlags.WordWrap
}
