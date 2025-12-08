using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Enumeration for GetMouseCursor().
/// </summary>
/// <remarks>
/// User code may request backend to display given cursor by calling SetMouseCursor(), which is why we have some cursors that are marked unused here.
/// </remarks>
public enum MouseCursor
{
    /// <summary>
    /// No cursor.
    /// </summary>
    None = ImGuiMouseCursor.None,

    /// <summary>
    /// Arrow cursor (default).
    /// </summary>
    Arrow = ImGuiMouseCursor.Arrow,

    /// <summary>
    /// Text input cursor. When hovering over InputText, etc.
    /// </summary>
    TextInput = ImGuiMouseCursor.TextInput,

    /// <summary>
    /// Resize all directions cursor (Unused by Dear ImGui functions).
    /// </summary>
    ResizeAll = ImGuiMouseCursor.ResizeAll,

    /// <summary>
    /// Resize North-South cursor. When hovering over a horizontal border.
    /// </summary>
    ResizeNS = ImGuiMouseCursor.ResizeNS,

    /// <summary>
    /// Resize East-West cursor. When hovering over a vertical border or a column.
    /// </summary>
    ResizeEW = ImGuiMouseCursor.ResizeEW,

    /// <summary>
    /// Resize Northeast-Southwest cursor. When hovering over the bottom-left corner of a window.
    /// </summary>
    ResizeNESW = ImGuiMouseCursor.ResizeNESW,

    /// <summary>
    /// Resize Northwest-Southeast cursor. When hovering over the bottom-right corner of a window.
    /// </summary>
    ResizeNWSE = ImGuiMouseCursor.ResizeNWSE,

    /// <summary>
    /// Hand cursor (Unused by Dear ImGui functions. Use for e.g. hyperlinks).
    /// </summary>
    Hand = ImGuiMouseCursor.Hand,

    /// <summary>
    /// Wait cursor. When waiting for something to process/load.
    /// </summary>
    Wait = ImGuiMouseCursor.Wait,

    /// <summary>
    /// Progress cursor. When waiting for something to process/load, but application is still interactive.
    /// </summary>
    Progress = ImGuiMouseCursor.Progress,

    /// <summary>
    /// Not allowed cursor. When hovering something with disallowed interaction. Usually a crossed circle.
    /// </summary>
    NotAllowed = ImGuiMouseCursor.NotAllowed
}
