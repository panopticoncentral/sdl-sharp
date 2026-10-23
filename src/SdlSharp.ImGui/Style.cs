using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Accessors for the global ImGui <c>ImGuiStyle</c> struct — all rounding, padding,
/// alignment, color, and tessellation fields. Changes apply immediately and persist
/// until reset or overwritten (e.g. by <see cref="ImGui.StyleColorsDark"/>).
/// </summary>
public static unsafe class Style
{
    private static IGSharp_Style* CurrentHandle { get { Context.Current.CheckCurrent(); return IGSharp_GetStyle(); } }

    // --- Font / Alpha ---

    /// <summary>Base font size in pixels (pre-scaling).</summary>
    public static float FontSizeBase
    {
        get => CurrentHandle->FontSizeBase;
        set => CurrentHandle->FontSizeBase = value;
    }

    /// <summary>Main font scale (multiplier on <see cref="FontSizeBase"/>).</summary>
    public static float FontScaleMain
    {
        get => CurrentHandle->FontScaleMain;
        set => CurrentHandle->FontScaleMain = value;
    }

    /// <summary>DPI font scale (read-only; set via <see cref="ImGui.SetFontScaleDpi"/>).</summary>
    public static float FontScaleDpi => CurrentHandle->FontScaleDpi;

    /// <summary>Global alpha multiplier (1.0 opaque, 0.0 invisible).</summary>
    public static float Alpha
    {
        get => CurrentHandle->Alpha;
        set => CurrentHandle->Alpha = value;
    }

    /// <summary>Alpha multiplier for disabled widgets.</summary>
    public static float DisabledAlpha
    {
        get => CurrentHandle->DisabledAlpha;
        set => CurrentHandle->DisabledAlpha = value;
    }

    // --- Window ---

    /// <summary>Rounding radius of window corners.</summary>
    public static float WindowRounding
    {
        get => CurrentHandle->WindowRounding;
        set => CurrentHandle->WindowRounding = value;
    }

    /// <summary>Thickness of window border (0 = no border).</summary>
    public static float WindowBorderSize
    {
        get => CurrentHandle->WindowBorderSize;
        set => CurrentHandle->WindowBorderSize = value;
    }

    /// <summary>Padding inside a window.</summary>
    public static Vec2 WindowPadding
    {
        get { var v = CurrentHandle->WindowPadding; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->WindowPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Minimum window size.</summary>
    public static Vec2 WindowMinSize
    {
        get { var v = CurrentHandle->WindowMinSize; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->WindowMinSize = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Window title alignment (0.0 = left, 1.0 = right, 0.5 = centered).</summary>
    public static Vec2 WindowTitleAlign
    {
        get { var v = CurrentHandle->WindowTitleAlign; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->WindowTitleAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    // --- Child / Popup ---

    /// <summary>Rounding radius of child window corners.</summary>
    public static float ChildRounding
    {
        get => CurrentHandle->ChildRounding;
        set => CurrentHandle->ChildRounding = value;
    }

    /// <summary>Thickness of child window border.</summary>
    public static float ChildBorderSize
    {
        get => CurrentHandle->ChildBorderSize;
        set => CurrentHandle->ChildBorderSize = value;
    }

    /// <summary>Rounding radius of popup window corners.</summary>
    public static float PopupRounding
    {
        get => CurrentHandle->PopupRounding;
        set => CurrentHandle->PopupRounding = value;
    }

    /// <summary>Thickness of popup window border.</summary>
    public static float PopupBorderSize
    {
        get => CurrentHandle->PopupBorderSize;
        set => CurrentHandle->PopupBorderSize = value;
    }

    // --- Frame (checkbox, slider, input, etc.) ---

    /// <summary>Rounding radius of framed widget corners.</summary>
    public static float FrameRounding
    {
        get => CurrentHandle->FrameRounding;
        set => CurrentHandle->FrameRounding = value;
    }

    /// <summary>Thickness of framed widget border.</summary>
    public static float FrameBorderSize
    {
        get => CurrentHandle->FrameBorderSize;
        set => CurrentHandle->FrameBorderSize = value;
    }

    /// <summary>Padding within a framed widget.</summary>
    public static Vec2 FramePadding
    {
        get { var v = CurrentHandle->FramePadding; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->FramePadding = new IGSharp_Vec2(value.X, value.Y);
    }

    // --- Layout spacing ---

    /// <summary>Horizontal and vertical spacing between widgets.</summary>
    public static Vec2 ItemSpacing
    {
        get { var v = CurrentHandle->ItemSpacing; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->ItemSpacing = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Horizontal and vertical spacing between sub-components within a widget.</summary>
    public static Vec2 ItemInnerSpacing
    {
        get { var v = CurrentHandle->ItemInnerSpacing; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->ItemInnerSpacing = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Padding inside table cells.</summary>
    public static Vec2 CellPadding
    {
        get { var v = CurrentHandle->CellPadding; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->CellPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Extra padding around items for touch input.</summary>
    public static Vec2 TouchExtraPadding
    {
        get { var v = CurrentHandle->TouchExtraPadding; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->TouchExtraPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Indent amount for TreeNode / Indent.</summary>
    public static float IndentSpacing
    {
        get => CurrentHandle->IndentSpacing;
        set => CurrentHandle->IndentSpacing = value;
    }

    /// <summary>Minimum spacing between columns in legacy Columns API.</summary>
    public static float ColumnsMinSpacing
    {
        get => CurrentHandle->ColumnsMinSpacing;
        set => CurrentHandle->ColumnsMinSpacing = value;
    }

    // --- Scrollbar / Grab / Image / Tab / Separator ---

    /// <summary>Width of the scrollbar.</summary>
    public static float ScrollbarSize
    {
        get => CurrentHandle->ScrollbarSize;
        set => CurrentHandle->ScrollbarSize = value;
    }

    /// <summary>Rounding radius of scrollbar grab corners.</summary>
    public static float ScrollbarRounding
    {
        get => CurrentHandle->ScrollbarRounding;
        set => CurrentHandle->ScrollbarRounding = value;
    }

    /// <summary>Minimum size of a slider/scrollbar grab.</summary>
    public static float GrabMinSize
    {
        get => CurrentHandle->GrabMinSize;
        set => CurrentHandle->GrabMinSize = value;
    }

    /// <summary>Rounding radius of slider grab corners.</summary>
    public static float GrabRounding
    {
        get => CurrentHandle->GrabRounding;
        set => CurrentHandle->GrabRounding = value;
    }

    /// <summary>Rounding radius of image corners.</summary>
    public static float ImageRounding
    {
        get => CurrentHandle->ImageRounding;
        set => CurrentHandle->ImageRounding = value;
    }

    /// <summary>Thickness of image border.</summary>
    public static float ImageBorderSize
    {
        get => CurrentHandle->ImageBorderSize;
        set => CurrentHandle->ImageBorderSize = value;
    }

    /// <summary>Rounding radius of tab corners.</summary>
    public static float TabRounding
    {
        get => CurrentHandle->TabRounding;
        set => CurrentHandle->TabRounding = value;
    }

    /// <summary>Thickness of tab border.</summary>
    public static float TabBorderSize
    {
        get => CurrentHandle->TabBorderSize;
        set => CurrentHandle->TabBorderSize = value;
    }

    /// <summary>Thickness of separator lines.</summary>
    public static float SeparatorSize
    {
        get => CurrentHandle->SeparatorSize;
        set => CurrentHandle->SeparatorSize = value;
    }

    // --- Text alignment ---

    /// <summary>Alignment of text inside a button (default 0.5, 0.5 = centered).</summary>
    public static Vec2 ButtonTextAlign
    {
        get { var v = CurrentHandle->ButtonTextAlign; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->ButtonTextAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Alignment of text inside a selectable.</summary>
    public static Vec2 SelectableTextAlign
    {
        get { var v = CurrentHandle->SelectableTextAlign; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->SelectableTextAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Alignment of separator text.</summary>
    public static Vec2 SeparatorTextAlign
    {
        get { var v = CurrentHandle->SeparatorTextAlign; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->SeparatorTextAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Padding around separator text.</summary>
    public static Vec2 SeparatorTextPadding
    {
        get { var v = CurrentHandle->SeparatorTextPadding; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->SeparatorTextPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    // --- Display padding ---

    /// <summary>Window padding at the display edge (for windows flush to the edge).</summary>
    public static Vec2 DisplayWindowPadding
    {
        get { var v = CurrentHandle->DisplayWindowPadding; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->DisplayWindowPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Safe-area padding (for TVs and notched displays).</summary>
    public static Vec2 DisplaySafeAreaPadding
    {
        get { var v = CurrentHandle->DisplaySafeAreaPadding; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->DisplaySafeAreaPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    // --- Mouse cursor / tessellation ---

    /// <summary>Scale factor applied to software-drawn mouse cursor.</summary>
    public static float MouseCursorScale
    {
        get => CurrentHandle->MouseCursorScale;
        set => CurrentHandle->MouseCursorScale = value;
    }

    /// <summary>Enable anti-aliased lines (expensive for custom drawing).</summary>
    public static bool AntiAliasedLines
    {
        get => CurrentHandle->AntiAliasedLines;
        set => CurrentHandle->AntiAliasedLines = value;
    }

    /// <summary>Enable anti-aliased filled shapes.</summary>
    public static bool AntiAliasedFill
    {
        get => CurrentHandle->AntiAliasedFill;
        set => CurrentHandle->AntiAliasedFill = value;
    }

    /// <summary>Tessellation tolerance for curves (lower = more segments).</summary>
    public static float CurveTessellationMaxError
    {
        get => CurrentHandle->CurveTessellationMaxError;
        set => CurrentHandle->CurveTessellationMaxError = value;
    }

    /// <summary>Maximum error in pixels for circle tessellation.</summary>
    public static float CircleTessellationMaxError
    {
        get => CurrentHandle->CircleTessellationMaxError;
        set => CurrentHandle->CircleTessellationMaxError = value;
    }

    // --- Colors ---

    /// <summary>Gets the color for the given palette index.</summary>
    public static Vec4 GetColor(Col idx)
    {
        var v = CurrentHandle->Colors[ColorIndex(idx)];
        return new Vec4(v.X, v.Y, v.Z, v.W);
    }

    /// <summary>Sets the color for the given palette index.</summary>
    public static void SetColor(Col idx, Vec4 col)
        => CurrentHandle->Colors[ColorIndex(idx)] = new IGSharp_Vec4(col.X, col.Y, col.Z, col.W);

    /// <summary>Sets the color for the given palette index from RGBA floats.</summary>
    public static void SetColor(Col idx, float r, float g, float b, float a)
        => CurrentHandle->Colors[ColorIndex(idx)] = new IGSharp_Vec4(r, g, b, a);

    // --- Additional ImGuiStyle mirror fields ---

    /// <summary>Mirror of <c>ImGuiStyle::WindowBorderHoverPadding</c>.</summary>
    public static float WindowBorderHoverPadding
    {
        get => CurrentHandle->WindowBorderHoverPadding;
        set => CurrentHandle->WindowBorderHoverPadding = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::ScrollbarPadding</c>.</summary>
    public static float ScrollbarPadding
    {
        get => CurrentHandle->ScrollbarPadding;
        set => CurrentHandle->ScrollbarPadding = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::LogSliderDeadzone</c>.</summary>
    public static float LogSliderDeadzone
    {
        get => CurrentHandle->LogSliderDeadzone;
        set => CurrentHandle->LogSliderDeadzone = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TabMinWidthBase</c>.</summary>
    public static float TabMinWidthBase
    {
        get => CurrentHandle->TabMinWidthBase;
        set => CurrentHandle->TabMinWidthBase = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TabMinWidthShrink</c>.</summary>
    public static float TabMinWidthShrink
    {
        get => CurrentHandle->TabMinWidthShrink;
        set => CurrentHandle->TabMinWidthShrink = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TabCloseButtonMinWidthSelected</c>.</summary>
    public static float TabCloseButtonMinWidthSelected
    {
        get => CurrentHandle->TabCloseButtonMinWidthSelected;
        set => CurrentHandle->TabCloseButtonMinWidthSelected = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TabCloseButtonMinWidthUnselected</c>.</summary>
    public static float TabCloseButtonMinWidthUnselected
    {
        get => CurrentHandle->TabCloseButtonMinWidthUnselected;
        set => CurrentHandle->TabCloseButtonMinWidthUnselected = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TabBarBorderSize</c>.</summary>
    public static float TabBarBorderSize
    {
        get => CurrentHandle->TabBarBorderSize;
        set => CurrentHandle->TabBarBorderSize = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TabBarOverlineSize</c>.</summary>
    public static float TabBarOverlineSize
    {
        get => CurrentHandle->TabBarOverlineSize;
        set => CurrentHandle->TabBarOverlineSize = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TableAngledHeadersAngle</c>.</summary>
    public static float TableAngledHeadersAngle
    {
        get => CurrentHandle->TableAngledHeadersAngle;
        set => CurrentHandle->TableAngledHeadersAngle = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TreeLinesSize</c>.</summary>
    public static float TreeLinesSize
    {
        get => CurrentHandle->TreeLinesSize;
        set => CurrentHandle->TreeLinesSize = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TreeLinesRounding</c>.</summary>
    public static float TreeLinesRounding
    {
        get => CurrentHandle->TreeLinesRounding;
        set => CurrentHandle->TreeLinesRounding = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::DragDropTargetRounding</c>.</summary>
    public static float DragDropTargetRounding
    {
        get => CurrentHandle->DragDropTargetRounding;
        set => CurrentHandle->DragDropTargetRounding = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::DragDropTargetBorderSize</c>.</summary>
    public static float DragDropTargetBorderSize
    {
        get => CurrentHandle->DragDropTargetBorderSize;
        set => CurrentHandle->DragDropTargetBorderSize = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::DragDropTargetPadding</c>.</summary>
    public static float DragDropTargetPadding
    {
        get => CurrentHandle->DragDropTargetPadding;
        set => CurrentHandle->DragDropTargetPadding = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::ColorMarkerSize</c>.</summary>
    public static float ColorMarkerSize
    {
        get => CurrentHandle->ColorMarkerSize;
        set => CurrentHandle->ColorMarkerSize = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::SeparatorTextBorderSize</c>.</summary>
    public static float SeparatorTextBorderSize
    {
        get => CurrentHandle->SeparatorTextBorderSize;
        set => CurrentHandle->SeparatorTextBorderSize = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::HoverStationaryDelay</c>.</summary>
    public static float HoverStationaryDelay
    {
        get => CurrentHandle->HoverStationaryDelay;
        set => CurrentHandle->HoverStationaryDelay = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::HoverDelayShort</c>.</summary>
    public static float HoverDelayShort
    {
        get => CurrentHandle->HoverDelayShort;
        set => CurrentHandle->HoverDelayShort = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::HoverDelayNormal</c>.</summary>
    public static float HoverDelayNormal
    {
        get => CurrentHandle->HoverDelayNormal;
        set => CurrentHandle->HoverDelayNormal = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::AntiAliasedLineEnds</c>.</summary>
    public static bool AntiAliasedLineEnds
    {
        get => CurrentHandle->AntiAliasedLineEnds;
        set => CurrentHandle->AntiAliasedLineEnds = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TableAngledHeadersTextAlign</c>.</summary>
    public static Vec2 TableAngledHeadersTextAlign
    {
        get { var v = CurrentHandle->TableAngledHeadersTextAlign; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->TableAngledHeadersTextAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Mirror of <c>ImGuiStyle::WindowMenuButtonPosition</c>.</summary>
    public static Dir WindowMenuButtonPosition
    {
        get => (Dir)CurrentHandle->WindowMenuButtonPosition;
        set => CurrentHandle->WindowMenuButtonPosition = (int)value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::ColorButtonPosition</c>.</summary>
    public static Dir ColorButtonPosition
    {
        get => (Dir)CurrentHandle->ColorButtonPosition;
        set => CurrentHandle->ColorButtonPosition = (int)value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TreeLinesFlags</c>.</summary>
    public static TreeNodeFlags TreeLinesFlags
    {
        get => (TreeNodeFlags)CurrentHandle->TreeLinesFlags;
        set => CurrentHandle->TreeLinesFlags = (int)value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::HoverFlagsForTooltipMouse</c>.</summary>
    public static HoveredFlags HoverFlagsForTooltipMouse
    {
        get => (HoveredFlags)CurrentHandle->HoverFlagsForTooltipMouse;
        set => CurrentHandle->HoverFlagsForTooltipMouse = (int)value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::HoverFlagsForTooltipNav</c>.</summary>
    public static HoveredFlags HoverFlagsForTooltipNav
    {
        get => (HoveredFlags)CurrentHandle->HoverFlagsForTooltipNav;
        set => CurrentHandle->HoverFlagsForTooltipNav = (int)value;
    }

    /// <summary>ImGui Style.InputTextCursorSize configuration.</summary>
    public static float InputTextCursorSize
    {
        get => CurrentHandle->InputTextCursorSize;
        set => CurrentHandle->InputTextCursorSize = value;
    }

    /// <summary>ImGui Style.SelectableRounding configuration.</summary>
    public static float SelectableRounding
    {
        get => CurrentHandle->SelectableRounding;
        set => CurrentHandle->SelectableRounding = value;
    }

    /// <summary>ImGui Style.MenuItemRounding configuration.</summary>
    public static float MenuItemRounding
    {
        get => CurrentHandle->MenuItemRounding;
        set => CurrentHandle->MenuItemRounding = value;
    }

    private static int ColorIndex(Col value) { var index = (int)value; ArgumentOutOfRangeException.ThrowIfNegative(index); ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, (int)Col.COUNT); return index; }

}
