using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Enumeration for PushStyleColor() / PopStyleColor().
/// </summary>
public enum StyleColor
{
    /// <summary>
    /// Text color.
    /// </summary>
    Text = ImGuiCol.Text,

    /// <summary>
    /// Disabled text color.
    /// </summary>
    TextDisabled = ImGuiCol.TextDisabled,

    /// <summary>
    /// Background of normal windows.
    /// </summary>
    WindowBackground = ImGuiCol.WindowBg,

    /// <summary>
    /// Background of child windows.
    /// </summary>
    ChildBackground = ImGuiCol.ChildBg,

    /// <summary>
    /// Background of popups, menus, tooltips windows.
    /// </summary>
    PopupBackground = ImGuiCol.PopupBg,

    /// <summary>
    /// Border color.
    /// </summary>
    Border = ImGuiCol.Border,

    /// <summary>
    /// Border shadow color.
    /// </summary>
    BorderShadow = ImGuiCol.BorderShadow,

    /// <summary>
    /// Background of checkbox, radio button, plot, slider, text input.
    /// </summary>
    FrameBackground = ImGuiCol.FrameBg,

    /// <summary>
    /// Frame background when hovered.
    /// </summary>
    FrameBackgroundHovered = ImGuiCol.FrameBgHovered,

    /// <summary>
    /// Frame background when active.
    /// </summary>
    FrameBackgroundActive = ImGuiCol.FrameBgActive,

    /// <summary>
    /// Title bar color.
    /// </summary>
    TitleBackground = ImGuiCol.TitleBg,

    /// <summary>
    /// Title bar when focused.
    /// </summary>
    TitleBackgroundActive = ImGuiCol.TitleBgActive,

    /// <summary>
    /// Title bar when collapsed.
    /// </summary>
    TitleBackgroundCollapsed = ImGuiCol.TitleBgCollapsed,

    /// <summary>
    /// Menu bar background.
    /// </summary>
    MenuBarBackground = ImGuiCol.MenuBarBg,

    /// <summary>
    /// Scrollbar background.
    /// </summary>
    ScrollbarBackground = ImGuiCol.ScrollbarBg,

    /// <summary>
    /// Scrollbar grab color.
    /// </summary>
    ScrollbarGrab = ImGuiCol.ScrollbarGrab,

    /// <summary>
    /// Scrollbar grab when hovered.
    /// </summary>
    ScrollbarGrabHovered = ImGuiCol.ScrollbarGrabHovered,

    /// <summary>
    /// Scrollbar grab when active.
    /// </summary>
    ScrollbarGrabActive = ImGuiCol.ScrollbarGrabActive,

    /// <summary>
    /// Checkbox tick and RadioButton circle.
    /// </summary>
    CheckMark = ImGuiCol.CheckMark,

    /// <summary>
    /// Slider grab color.
    /// </summary>
    SliderGrab = ImGuiCol.SliderGrab,

    /// <summary>
    /// Slider grab when active.
    /// </summary>
    SliderGrabActive = ImGuiCol.SliderGrabActive,

    /// <summary>
    /// Button color.
    /// </summary>
    Button = ImGuiCol.Button,

    /// <summary>
    /// Button when hovered.
    /// </summary>
    ButtonHovered = ImGuiCol.ButtonHovered,

    /// <summary>
    /// Button when active.
    /// </summary>
    ButtonActive = ImGuiCol.ButtonActive,

    /// <summary>
    /// Header colors are used for CollapsingHeader, TreeNode, Selectable, MenuItem.
    /// </summary>
    Header = ImGuiCol.Header,

    /// <summary>
    /// Header when hovered.
    /// </summary>
    HeaderHovered = ImGuiCol.HeaderHovered,

    /// <summary>
    /// Header when active.
    /// </summary>
    HeaderActive = ImGuiCol.HeaderActive,

    /// <summary>
    /// Separator color.
    /// </summary>
    Separator = ImGuiCol.Separator,

    /// <summary>
    /// Separator when hovered.
    /// </summary>
    SeparatorHovered = ImGuiCol.SeparatorHovered,

    /// <summary>
    /// Separator when active.
    /// </summary>
    SeparatorActive = ImGuiCol.SeparatorActive,

    /// <summary>
    /// Resize grip in lower-right and lower-left corners of windows.
    /// </summary>
    ResizeGrip = ImGuiCol.ResizeGrip,

    /// <summary>
    /// Resize grip when hovered.
    /// </summary>
    ResizeGripHovered = ImGuiCol.ResizeGripHovered,

    /// <summary>
    /// Resize grip when active.
    /// </summary>
    ResizeGripActive = ImGuiCol.ResizeGripActive,

    /// <summary>
    /// InputText cursor/caret color.
    /// </summary>
    InputTextCursor = ImGuiCol.InputTextCursor,

    /// <summary>
    /// Tab background, when hovered.
    /// </summary>
    TabHovered = ImGuiCol.TabHovered,

    /// <summary>
    /// Tab background, when tab-bar is focused and tab is unselected.
    /// </summary>
    Tab = ImGuiCol.Tab,

    /// <summary>
    /// Tab background, when tab-bar is focused and tab is selected.
    /// </summary>
    TabSelected = ImGuiCol.TabSelected,

    /// <summary>
    /// Tab horizontal overline, when tab-bar is focused and tab is selected.
    /// </summary>
    TabSelectedOverline = ImGuiCol.TabSelectedOverline,

    /// <summary>
    /// Tab background, when tab-bar is unfocused and tab is unselected.
    /// </summary>
    TabDimmed = ImGuiCol.TabDimmed,

    /// <summary>
    /// Tab background, when tab-bar is unfocused and tab is selected.
    /// </summary>
    TabDimmedSelected = ImGuiCol.TabDimmedSelected,

    /// <summary>
    /// Tab horizontal overline, when tab-bar is unfocused and tab is selected.
    /// </summary>
    TabDimmedSelectedOverline = ImGuiCol.TabDimmedSelectedOverline,

    /// <summary>
    /// Plot lines color.
    /// </summary>
    PlotLines = ImGuiCol.PlotLines,

    /// <summary>
    /// Plot lines when hovered.
    /// </summary>
    PlotLinesHovered = ImGuiCol.PlotLinesHovered,

    /// <summary>
    /// Plot histogram color.
    /// </summary>
    PlotHistogram = ImGuiCol.PlotHistogram,

    /// <summary>
    /// Plot histogram when hovered.
    /// </summary>
    PlotHistogramHovered = ImGuiCol.PlotHistogramHovered,

    /// <summary>
    /// Table header background.
    /// </summary>
    TableHeaderBackground = ImGuiCol.TableHeaderBg,

    /// <summary>
    /// Table outer and header borders (prefer using Alpha=1.0 here).
    /// </summary>
    TableBorderStrong = ImGuiCol.TableBorderStrong,

    /// <summary>
    /// Table inner borders (prefer using Alpha=1.0 here).
    /// </summary>
    TableBorderLight = ImGuiCol.TableBorderLight,

    /// <summary>
    /// Table row background (even rows).
    /// </summary>
    TableRowBackground = ImGuiCol.TableRowBg,

    /// <summary>
    /// Table row background (odd rows).
    /// </summary>
    TableRowBackgroundAlt = ImGuiCol.TableRowBgAlt,

    /// <summary>
    /// Hyperlink color.
    /// </summary>
    TextLink = ImGuiCol.TextLink,

    /// <summary>
    /// Selected text inside an InputText.
    /// </summary>
    TextSelectedBackground = ImGuiCol.TextSelectedBg,

    /// <summary>
    /// Tree node hierarchy outlines when using TreeNodeFlags.DrawLines.
    /// </summary>
    TreeLines = ImGuiCol.TreeLines,

    /// <summary>
    /// Rectangle border highlighting a drop target.
    /// </summary>
    DragDropTarget = ImGuiCol.DragDropTarget,

    /// <summary>
    /// Rectangle background highlighting a drop target.
    /// </summary>
    DragDropTargetBackground = ImGuiCol.DragDropTargetBg,

    /// <summary>
    /// Unsaved Document marker (in window title and tabs).
    /// </summary>
    UnsavedMarker = ImGuiCol.UnsavedMarker,

    /// <summary>
    /// Color of keyboard/gamepad navigation cursor/rectangle, when visible.
    /// </summary>
    NavCursor = ImGuiCol.NavCursor,

    /// <summary>
    /// Highlight window when using Ctrl+Tab.
    /// </summary>
    NavWindowingHighlight = ImGuiCol.NavWindowingHighlight,

    /// <summary>
    /// Darken/colorize entire screen behind the Ctrl+Tab window list, when active.
    /// </summary>
    NavWindowingDimBackground = ImGuiCol.NavWindowingDimBg,

    /// <summary>
    /// Darken/colorize entire screen behind a modal window, when one is active.
    /// </summary>
    ModalWindowDimBackground = ImGuiCol.ModalWindowDimBg
}
