using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Enumeration for PushStyleVar() / PopStyleVar() to temporarily modify the <see cref="Style"/> structure.
/// </summary>
/// <remarks>
/// The enum only refers to fields of <see cref="Style"/> which makes sense to be pushed/popped inside UI code.
/// During initialization or between frames, feel free to just poke into <see cref="Style"/> directly.
/// </remarks>
public enum StyleVariable
{
    /// <summary>
    /// Global alpha applies to everything in Dear ImGui. (float)
    /// </summary>
    Alpha = ImGuiStyleVar.Alpha,

    /// <summary>
    /// Additional alpha multiplier applied by BeginDisabled(). Multiply over current value of Alpha. (float)
    /// </summary>
    DisabledAlpha = ImGuiStyleVar.DisabledAlpha,

    /// <summary>
    /// Padding within a window. (ImVec2)
    /// </summary>
    WindowPadding = ImGuiStyleVar.WindowPadding,

    /// <summary>
    /// Radius of window corners rounding. Set to 0.0f to have rectangular windows. (float)
    /// </summary>
    WindowRounding = ImGuiStyleVar.WindowRounding,

    /// <summary>
    /// Thickness of border around windows. (float)
    /// </summary>
    WindowBorderSize = ImGuiStyleVar.WindowBorderSize,

    /// <summary>
    /// Minimum window size. (ImVec2)
    /// </summary>
    WindowMinSize = ImGuiStyleVar.WindowMinSize,

    /// <summary>
    /// Alignment for title bar text. (ImVec2)
    /// </summary>
    WindowTitleAlign = ImGuiStyleVar.WindowTitleAlign,

    /// <summary>
    /// Radius of child window corners rounding. (float)
    /// </summary>
    ChildRounding = ImGuiStyleVar.ChildRounding,

    /// <summary>
    /// Thickness of border around child windows. (float)
    /// </summary>
    ChildBorderSize = ImGuiStyleVar.ChildBorderSize,

    /// <summary>
    /// Radius of popup window corners rounding. (float)
    /// </summary>
    PopupRounding = ImGuiStyleVar.PopupRounding,

    /// <summary>
    /// Thickness of border around popup/tooltip windows. (float)
    /// </summary>
    PopupBorderSize = ImGuiStyleVar.PopupBorderSize,

    /// <summary>
    /// Padding within a framed rectangle (used by most widgets). (ImVec2)
    /// </summary>
    FramePadding = ImGuiStyleVar.FramePadding,

    /// <summary>
    /// Radius of frame corners rounding. (float)
    /// </summary>
    FrameRounding = ImGuiStyleVar.FrameRounding,

    /// <summary>
    /// Thickness of border around frames. (float)
    /// </summary>
    FrameBorderSize = ImGuiStyleVar.FrameBorderSize,

    /// <summary>
    /// Horizontal and vertical spacing between widgets/lines. (ImVec2)
    /// </summary>
    ItemSpacing = ImGuiStyleVar.ItemSpacing,

    /// <summary>
    /// Horizontal and vertical spacing between within elements of a composed widget (e.g. a slider and its label). (ImVec2)
    /// </summary>
    ItemInnerSpacing = ImGuiStyleVar.ItemInnerSpacing,

    /// <summary>
    /// Horizontal indentation when e.g. entering a tree node. (float)
    /// </summary>
    IndentSpacing = ImGuiStyleVar.IndentSpacing,

    /// <summary>
    /// Padding within a table cell. (ImVec2)
    /// </summary>
    CellPadding = ImGuiStyleVar.CellPadding,

    /// <summary>
    /// Width of the vertical scrollbar, Height of the horizontal scrollbar. (float)
    /// </summary>
    ScrollbarSize = ImGuiStyleVar.ScrollbarSize,

    /// <summary>
    /// Radius of grab corners for scrollbar. (float)
    /// </summary>
    ScrollbarRounding = ImGuiStyleVar.ScrollbarRounding,

    /// <summary>
    /// Padding from scrollbar edge to content. (float)
    /// </summary>
    ScrollbarPadding = ImGuiStyleVar.ScrollbarPadding,

    /// <summary>
    /// Minimum width/height of a grab box for slider/scrollbar. (float)
    /// </summary>
    GrabMinSize = ImGuiStyleVar.GrabMinSize,

    /// <summary>
    /// Radius of grabs corners rounding. (float)
    /// </summary>
    GrabRounding = ImGuiStyleVar.GrabRounding,

    /// <summary>
    /// Thickness of border around image in Image(). (float)
    /// </summary>
    ImageBorderSize = ImGuiStyleVar.ImageBorderSize,

    /// <summary>
    /// Radius of upper corners of a tab. (float)
    /// </summary>
    TabRounding = ImGuiStyleVar.TabRounding,

    /// <summary>
    /// Thickness of border around tabs. (float)
    /// </summary>
    TabBorderSize = ImGuiStyleVar.TabBorderSize,

    /// <summary>
    /// Minimum width for close button inside tabs. (float)
    /// </summary>
    TabMinWidthBase = ImGuiStyleVar.TabMinWidthBase,

    /// <summary>
    /// Minimum width for shrunk tabs. (float)
    /// </summary>
    TabMinWidthShrink = ImGuiStyleVar.TabMinWidthShrink,

    /// <summary>
    /// Thickness of tab-bar separator. (float)
    /// </summary>
    TabBarBorderSize = ImGuiStyleVar.TabBarBorderSize,

    /// <summary>
    /// Thickness of tab-bar overline. (float)
    /// </summary>
    TabBarOverlineSize = ImGuiStyleVar.TabBarOverlineSize,

    /// <summary>
    /// Angle of angled headers. (float)
    /// </summary>
    TableAngledHeadersAngle = ImGuiStyleVar.TableAngledHeadersAngle,

    /// <summary>
    /// Alignment of angled headers within column. (ImVec2)
    /// </summary>
    TableAngledHeadersTextAlign = ImGuiStyleVar.TableAngledHeadersTextAlign,

    /// <summary>
    /// Thickness of tree node lines. (float)
    /// </summary>
    TreeLinesSize = ImGuiStyleVar.TreeLinesSize,

    /// <summary>
    /// Radius of tree node lines corners rounding. (float)
    /// </summary>
    TreeLinesRounding = ImGuiStyleVar.TreeLinesRounding,

    /// <summary>
    /// Alignment of button text when button is larger than text. (ImVec2)
    /// </summary>
    ButtonTextAlign = ImGuiStyleVar.ButtonTextAlign,

    /// <summary>
    /// Alignment of selectable text. (ImVec2)
    /// </summary>
    SelectableTextAlign = ImGuiStyleVar.SelectableTextAlign,

    /// <summary>
    /// Thickkness of separator text border. (float)
    /// </summary>
    SeparatorTextBorderSize = ImGuiStyleVar.SeparatorTextBorderSize,

    /// <summary>
    /// Alignment of separator text. (ImVec2)
    /// </summary>
    SeparatorTextAlign = ImGuiStyleVar.SeparatorTextAlign,

    /// <summary>
    /// Padding around separator text. (ImVec2)
    /// </summary>
    SeparatorTextPadding = ImGuiStyleVar.SeparatorTextPadding
}
