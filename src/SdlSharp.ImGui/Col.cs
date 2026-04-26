namespace SdlSharp.Gui;

/// <summary>Color identifier for PushStyleColor / PopStyleColor.</summary>
public enum Col
{
    /// <summary>Text color.</summary>
    Text,
    /// <summary>Disabled text color.</summary>
    TextDisabled,
    /// <summary>Background of normal windows.</summary>
    WindowBg,
    /// <summary>Background of child windows.</summary>
    ChildBg,
    /// <summary>Background of popups, menus, tooltips windows.</summary>
    PopupBg,
    /// <summary>Border color.</summary>
    Border,
    /// <summary>Border shadow color.</summary>
    BorderShadow,
    /// <summary>Background of checkbox, radio button, plot, slider, text input.</summary>
    FrameBg,
    /// <summary>Frame background, hovered.</summary>
    FrameBgHovered,
    /// <summary>Frame background, active.</summary>
    FrameBgActive,
    /// <summary>Title bar.</summary>
    TitleBg,
    /// <summary>Title bar when focused.</summary>
    TitleBgActive,
    /// <summary>Title bar when collapsed.</summary>
    TitleBgCollapsed,
    /// <summary>Menu bar background.</summary>
    MenuBarBg,
    /// <summary>Scrollbar background.</summary>
    ScrollbarBg,
    /// <summary>Scrollbar grab.</summary>
    ScrollbarGrab,
    /// <summary>Scrollbar grab, hovered.</summary>
    ScrollbarGrabHovered,
    /// <summary>Scrollbar grab, active.</summary>
    ScrollbarGrabActive,
    /// <summary>Checkbox tick and RadioButton circle.</summary>
    CheckMark,
    /// <summary>Slider grab.</summary>
    SliderGrab,
    /// <summary>Slider grab, active.</summary>
    SliderGrabActive,
    /// <summary>Button.</summary>
    Button,
    /// <summary>Button, hovered.</summary>
    ButtonHovered,
    /// <summary>Button, active.</summary>
    ButtonActive,
    /// <summary>Header (used for CollapsingHeader, TreeNode, Selectable, MenuItem).</summary>
    Header,
    /// <summary>Header, hovered.</summary>
    HeaderHovered,
    /// <summary>Header, active.</summary>
    HeaderActive,
    /// <summary>Separator.</summary>
    Separator,
    /// <summary>Separator, hovered.</summary>
    SeparatorHovered,
    /// <summary>Separator, active.</summary>
    SeparatorActive,
    /// <summary>Resize grip.</summary>
    ResizeGrip,
    /// <summary>Resize grip, hovered.</summary>
    ResizeGripHovered,
    /// <summary>Resize grip, active.</summary>
    ResizeGripActive,
    /// <summary>InputText cursor/caret.</summary>
    InputTextCursor,
    /// <summary>Tab background, when hovered.</summary>
    TabHovered,
    /// <summary>Tab background, when tab-bar is focused and tab is unselected.</summary>
    Tab,
    /// <summary>Tab background, when tab-bar is focused and tab is selected.</summary>
    TabSelected,
    /// <summary>Tab horizontal overline, when selected.</summary>
    TabSelectedOverline,
    /// <summary>Tab background, when tab-bar is unfocused and tab is unselected.</summary>
    TabDimmed,
    /// <summary>Tab background, when tab-bar is unfocused and tab is selected.</summary>
    TabDimmedSelected,
    /// <summary>Tab horizontal overline, when tab-bar is unfocused and tab is selected.</summary>
    TabDimmedSelectedOverline,
    /// <summary>Plot lines.</summary>
    PlotLines,
    /// <summary>Plot lines, hovered.</summary>
    PlotLinesHovered,
    /// <summary>Plot histogram.</summary>
    PlotHistogram,
    /// <summary>Plot histogram, hovered.</summary>
    PlotHistogramHovered,
    /// <summary>Table header background.</summary>
    TableHeaderBg,
    /// <summary>Table outer and header borders.</summary>
    TableBorderStrong,
    /// <summary>Table inner borders.</summary>
    TableBorderLight,
    /// <summary>Table row background (even rows).</summary>
    TableRowBg,
    /// <summary>Table row background (odd rows).</summary>
    TableRowBgAlt,
    /// <summary>Hyperlink color.</summary>
    TextLink,
    /// <summary>Selected text inside an InputText.</summary>
    TextSelectedBg,
    /// <summary>Tree node hierarchy outlines.</summary>
    TreeLines,
    /// <summary>Rectangle border highlighting a drop target.</summary>
    DragDropTarget,
    /// <summary>Rectangle background highlighting a drop target.</summary>
    DragDropTargetBg,
    /// <summary>Unsaved Document marker (in window title and tabs).</summary>
    UnsavedMarker,
    /// <summary>Color of keyboard/gamepad navigation cursor/rectangle.</summary>
    NavCursor,
    /// <summary>Highlight window when using Ctrl+Tab.</summary>
    NavWindowingHighlight,
    /// <summary>Darken/colorize entire screen behind the Ctrl+Tab window list.</summary>
    NavWindowingDimBg,
    /// <summary>Darken/colorize entire screen behind a modal window.</summary>
    ModalWindowDimBg,
}
