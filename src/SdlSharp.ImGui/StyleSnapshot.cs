using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>An independent managed copy of every style field and palette color. Capture, edit, and Apply to a context.</summary>
public sealed unsafe class StyleSnapshot
{
    private IGSharp_Style _value;
    /// <summary>Creates an independent managed style initialized with upstream defaults.</summary>
    public StyleSnapshot() { fixed (IGSharp_Style* pointer = &_value) IGSharp_Style_Init(pointer); }
    /// <summary>Copies the current context's complete style into managed storage.</summary>
    public static StyleSnapshot Capture()
    {
        Context.Current.CheckCurrent(); return new StyleSnapshot { _value = *IGSharp_GetStyle() };
    }
    /// <summary>Applies this complete style to the current context.</summary>
    public void Apply() { Context.Current.CheckCurrent(); *IGSharp_GetStyle() = _value; }

    // --- Font / Alpha ---

    /// <summary>Base font size in pixels (pre-scaling).</summary>
    public float FontSizeBase
    {
        get => _value.FontSizeBase;
        set => _value.FontSizeBase = value;
    }

    /// <summary>Main font scale (multiplier on <see cref="FontSizeBase"/>).</summary>
    public float FontScaleMain
    {
        get => _value.FontScaleMain;
        set => _value.FontScaleMain = value;
    }

    /// <summary>DPI font scale (read-only; set via <see cref="ImGui.SetFontScaleDpi"/>).</summary>
    public float FontScaleDpi => _value.FontScaleDpi;

    /// <summary>Global alpha multiplier (1.0 opaque, 0.0 invisible).</summary>
    public float Alpha
    {
        get => _value.Alpha;
        set => _value.Alpha = value;
    }

    /// <summary>Alpha multiplier for disabled widgets.</summary>
    public float DisabledAlpha
    {
        get => _value.DisabledAlpha;
        set => _value.DisabledAlpha = value;
    }

    // --- Window ---

    /// <summary>Rounding radius of window corners.</summary>
    public float WindowRounding
    {
        get => _value.WindowRounding;
        set => _value.WindowRounding = value;
    }

    /// <summary>Thickness of window border (0 = no border).</summary>
    public float WindowBorderSize
    {
        get => _value.WindowBorderSize;
        set => _value.WindowBorderSize = value;
    }

    /// <summary>Padding inside a window.</summary>
    public Vec2 WindowPadding
    {
        get { var v = _value.WindowPadding; return new Vec2(v.X, v.Y); }
        set => _value.WindowPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Minimum window size.</summary>
    public Vec2 WindowMinSize
    {
        get { var v = _value.WindowMinSize; return new Vec2(v.X, v.Y); }
        set => _value.WindowMinSize = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Window title alignment (0.0 = left, 1.0 = right, 0.5 = centered).</summary>
    public Vec2 WindowTitleAlign
    {
        get { var v = _value.WindowTitleAlign; return new Vec2(v.X, v.Y); }
        set => _value.WindowTitleAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    // --- Child / Popup ---

    /// <summary>Rounding radius of child window corners.</summary>
    public float ChildRounding
    {
        get => _value.ChildRounding;
        set => _value.ChildRounding = value;
    }

    /// <summary>Thickness of child window border.</summary>
    public float ChildBorderSize
    {
        get => _value.ChildBorderSize;
        set => _value.ChildBorderSize = value;
    }

    /// <summary>Rounding radius of popup window corners.</summary>
    public float PopupRounding
    {
        get => _value.PopupRounding;
        set => _value.PopupRounding = value;
    }

    /// <summary>Thickness of popup window border.</summary>
    public float PopupBorderSize
    {
        get => _value.PopupBorderSize;
        set => _value.PopupBorderSize = value;
    }

    // --- Frame (checkbox, slider, input, etc.) ---

    /// <summary>Rounding radius of framed widget corners.</summary>
    public float FrameRounding
    {
        get => _value.FrameRounding;
        set => _value.FrameRounding = value;
    }

    /// <summary>Thickness of framed widget border.</summary>
    public float FrameBorderSize
    {
        get => _value.FrameBorderSize;
        set => _value.FrameBorderSize = value;
    }

    /// <summary>Padding within a framed widget.</summary>
    public Vec2 FramePadding
    {
        get { var v = _value.FramePadding; return new Vec2(v.X, v.Y); }
        set => _value.FramePadding = new IGSharp_Vec2(value.X, value.Y);
    }

    // --- Layout spacing ---

    /// <summary>Horizontal and vertical spacing between widgets.</summary>
    public Vec2 ItemSpacing
    {
        get { var v = _value.ItemSpacing; return new Vec2(v.X, v.Y); }
        set => _value.ItemSpacing = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Horizontal and vertical spacing between sub-components within a widget.</summary>
    public Vec2 ItemInnerSpacing
    {
        get { var v = _value.ItemInnerSpacing; return new Vec2(v.X, v.Y); }
        set => _value.ItemInnerSpacing = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Padding inside table cells.</summary>
    public Vec2 CellPadding
    {
        get { var v = _value.CellPadding; return new Vec2(v.X, v.Y); }
        set => _value.CellPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Extra padding around items for touch input.</summary>
    public Vec2 TouchExtraPadding
    {
        get { var v = _value.TouchExtraPadding; return new Vec2(v.X, v.Y); }
        set => _value.TouchExtraPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Indent amount for TreeNode / Indent.</summary>
    public float IndentSpacing
    {
        get => _value.IndentSpacing;
        set => _value.IndentSpacing = value;
    }

    /// <summary>Minimum spacing between columns in legacy Columns API.</summary>
    public float ColumnsMinSpacing
    {
        get => _value.ColumnsMinSpacing;
        set => _value.ColumnsMinSpacing = value;
    }

    // --- Scrollbar / Grab / Image / Tab / Separator ---

    /// <summary>Width of the scrollbar.</summary>
    public float ScrollbarSize
    {
        get => _value.ScrollbarSize;
        set => _value.ScrollbarSize = value;
    }

    /// <summary>Rounding radius of scrollbar grab corners.</summary>
    public float ScrollbarRounding
    {
        get => _value.ScrollbarRounding;
        set => _value.ScrollbarRounding = value;
    }

    /// <summary>Minimum size of a slider/scrollbar grab.</summary>
    public float GrabMinSize
    {
        get => _value.GrabMinSize;
        set => _value.GrabMinSize = value;
    }

    /// <summary>Rounding radius of slider grab corners.</summary>
    public float GrabRounding
    {
        get => _value.GrabRounding;
        set => _value.GrabRounding = value;
    }

    /// <summary>Rounding radius of image corners.</summary>
    public float ImageRounding
    {
        get => _value.ImageRounding;
        set => _value.ImageRounding = value;
    }

    /// <summary>Thickness of image border.</summary>
    public float ImageBorderSize
    {
        get => _value.ImageBorderSize;
        set => _value.ImageBorderSize = value;
    }

    /// <summary>Rounding radius of tab corners.</summary>
    public float TabRounding
    {
        get => _value.TabRounding;
        set => _value.TabRounding = value;
    }

    /// <summary>Thickness of tab border.</summary>
    public float TabBorderSize
    {
        get => _value.TabBorderSize;
        set => _value.TabBorderSize = value;
    }

    /// <summary>Thickness of separator lines.</summary>
    public float SeparatorSize
    {
        get => _value.SeparatorSize;
        set => _value.SeparatorSize = value;
    }

    // --- Text alignment ---

    /// <summary>Alignment of text inside a button (default 0.5, 0.5 = centered).</summary>
    public Vec2 ButtonTextAlign
    {
        get { var v = _value.ButtonTextAlign; return new Vec2(v.X, v.Y); }
        set => _value.ButtonTextAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Alignment of text inside a selectable.</summary>
    public Vec2 SelectableTextAlign
    {
        get { var v = _value.SelectableTextAlign; return new Vec2(v.X, v.Y); }
        set => _value.SelectableTextAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Alignment of separator text.</summary>
    public Vec2 SeparatorTextAlign
    {
        get { var v = _value.SeparatorTextAlign; return new Vec2(v.X, v.Y); }
        set => _value.SeparatorTextAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Padding around separator text.</summary>
    public Vec2 SeparatorTextPadding
    {
        get { var v = _value.SeparatorTextPadding; return new Vec2(v.X, v.Y); }
        set => _value.SeparatorTextPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    // --- Display padding ---

    /// <summary>Window padding at the display edge (for windows flush to the edge).</summary>
    public Vec2 DisplayWindowPadding
    {
        get { var v = _value.DisplayWindowPadding; return new Vec2(v.X, v.Y); }
        set => _value.DisplayWindowPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Safe-area padding (for TVs and notched displays).</summary>
    public Vec2 DisplaySafeAreaPadding
    {
        get { var v = _value.DisplaySafeAreaPadding; return new Vec2(v.X, v.Y); }
        set => _value.DisplaySafeAreaPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    // --- Mouse cursor / tessellation ---

    /// <summary>Scale factor applied to software-drawn mouse cursor.</summary>
    public float MouseCursorScale
    {
        get => _value.MouseCursorScale;
        set => _value.MouseCursorScale = value;
    }

    /// <summary>Enable anti-aliased lines (expensive for custom drawing).</summary>
    public bool AntiAliasedLines
    {
        get => _value.AntiAliasedLines;
        set => _value.AntiAliasedLines = value;
    }

    /// <summary>Enable anti-aliased filled shapes.</summary>
    public bool AntiAliasedFill
    {
        get => _value.AntiAliasedFill;
        set => _value.AntiAliasedFill = value;
    }

    /// <summary>Tessellation tolerance for curves (lower = more segments).</summary>
    public float CurveTessellationMaxError
    {
        get => _value.CurveTessellationMaxError;
        set => _value.CurveTessellationMaxError = value;
    }

    /// <summary>Maximum error in pixels for circle tessellation.</summary>
    public float CircleTessellationMaxError
    {
        get => _value.CircleTessellationMaxError;
        set => _value.CircleTessellationMaxError = value;
    }

    // --- Colors ---

    /// <summary>Gets the color for the given palette index.</summary>
    public Vec4 GetColor(Col idx)
    {
        var v = _value.Colors[ColorIndex(idx)];
        return new Vec4(v.X, v.Y, v.Z, v.W);
    }

    /// <summary>Sets the color for the given palette index.</summary>
    public void SetColor(Col idx, Vec4 col)
        => _value.Colors[ColorIndex(idx)] = new IGSharp_Vec4(col.X, col.Y, col.Z, col.W);

    /// <summary>Sets the color for the given palette index from RGBA floats.</summary>
    public void SetColor(Col idx, float r, float g, float b, float a)
        => _value.Colors[ColorIndex(idx)] = new IGSharp_Vec4(r, g, b, a);

    // --- Additional ImGuiStyle mirror fields ---

    /// <summary>Mirror of <c>ImGuiStyle::WindowBorderHoverPadding</c>.</summary>
    public float WindowBorderHoverPadding
    {
        get => _value.WindowBorderHoverPadding;
        set => _value.WindowBorderHoverPadding = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::ScrollbarPadding</c>.</summary>
    public float ScrollbarPadding
    {
        get => _value.ScrollbarPadding;
        set => _value.ScrollbarPadding = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::LogSliderDeadzone</c>.</summary>
    public float LogSliderDeadzone
    {
        get => _value.LogSliderDeadzone;
        set => _value.LogSliderDeadzone = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TabMinWidthBase</c>.</summary>
    public float TabMinWidthBase
    {
        get => _value.TabMinWidthBase;
        set => _value.TabMinWidthBase = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TabMinWidthShrink</c>.</summary>
    public float TabMinWidthShrink
    {
        get => _value.TabMinWidthShrink;
        set => _value.TabMinWidthShrink = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TabCloseButtonMinWidthSelected</c>.</summary>
    public float TabCloseButtonMinWidthSelected
    {
        get => _value.TabCloseButtonMinWidthSelected;
        set => _value.TabCloseButtonMinWidthSelected = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TabCloseButtonMinWidthUnselected</c>.</summary>
    public float TabCloseButtonMinWidthUnselected
    {
        get => _value.TabCloseButtonMinWidthUnselected;
        set => _value.TabCloseButtonMinWidthUnselected = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TabBarBorderSize</c>.</summary>
    public float TabBarBorderSize
    {
        get => _value.TabBarBorderSize;
        set => _value.TabBarBorderSize = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TabBarOverlineSize</c>.</summary>
    public float TabBarOverlineSize
    {
        get => _value.TabBarOverlineSize;
        set => _value.TabBarOverlineSize = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TableAngledHeadersAngle</c>.</summary>
    public float TableAngledHeadersAngle
    {
        get => _value.TableAngledHeadersAngle;
        set => _value.TableAngledHeadersAngle = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TreeLinesSize</c>.</summary>
    public float TreeLinesSize
    {
        get => _value.TreeLinesSize;
        set => _value.TreeLinesSize = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TreeLinesRounding</c>.</summary>
    public float TreeLinesRounding
    {
        get => _value.TreeLinesRounding;
        set => _value.TreeLinesRounding = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::DragDropTargetRounding</c>.</summary>
    public float DragDropTargetRounding
    {
        get => _value.DragDropTargetRounding;
        set => _value.DragDropTargetRounding = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::DragDropTargetBorderSize</c>.</summary>
    public float DragDropTargetBorderSize
    {
        get => _value.DragDropTargetBorderSize;
        set => _value.DragDropTargetBorderSize = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::DragDropTargetPadding</c>.</summary>
    public float DragDropTargetPadding
    {
        get => _value.DragDropTargetPadding;
        set => _value.DragDropTargetPadding = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::ColorMarkerSize</c>.</summary>
    public float ColorMarkerSize
    {
        get => _value.ColorMarkerSize;
        set => _value.ColorMarkerSize = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::SeparatorTextBorderSize</c>.</summary>
    public float SeparatorTextBorderSize
    {
        get => _value.SeparatorTextBorderSize;
        set => _value.SeparatorTextBorderSize = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::HoverStationaryDelay</c>.</summary>
    public float HoverStationaryDelay
    {
        get => _value.HoverStationaryDelay;
        set => _value.HoverStationaryDelay = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::HoverDelayShort</c>.</summary>
    public float HoverDelayShort
    {
        get => _value.HoverDelayShort;
        set => _value.HoverDelayShort = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::HoverDelayNormal</c>.</summary>
    public float HoverDelayNormal
    {
        get => _value.HoverDelayNormal;
        set => _value.HoverDelayNormal = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::AntiAliasedLineEnds</c>.</summary>
    public bool AntiAliasedLineEnds
    {
        get => _value.AntiAliasedLineEnds;
        set => _value.AntiAliasedLineEnds = value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TableAngledHeadersTextAlign</c>.</summary>
    public Vec2 TableAngledHeadersTextAlign
    {
        get { var v = _value.TableAngledHeadersTextAlign; return new Vec2(v.X, v.Y); }
        set => _value.TableAngledHeadersTextAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Mirror of <c>ImGuiStyle::WindowMenuButtonPosition</c>.</summary>
    public Dir WindowMenuButtonPosition
    {
        get => (Dir)_value.WindowMenuButtonPosition;
        set => _value.WindowMenuButtonPosition = (int)value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::ColorButtonPosition</c>.</summary>
    public Dir ColorButtonPosition
    {
        get => (Dir)_value.ColorButtonPosition;
        set => _value.ColorButtonPosition = (int)value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::TreeLinesFlags</c>.</summary>
    public TreeNodeFlags TreeLinesFlags
    {
        get => (TreeNodeFlags)_value.TreeLinesFlags;
        set => _value.TreeLinesFlags = (int)value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::HoverFlagsForTooltipMouse</c>.</summary>
    public HoveredFlags HoverFlagsForTooltipMouse
    {
        get => (HoveredFlags)_value.HoverFlagsForTooltipMouse;
        set => _value.HoverFlagsForTooltipMouse = (int)value;
    }

    /// <summary>Mirror of <c>ImGuiStyle::HoverFlagsForTooltipNav</c>.</summary>
    public HoveredFlags HoverFlagsForTooltipNav
    {
        get => (HoveredFlags)_value.HoverFlagsForTooltipNav;
        set => _value.HoverFlagsForTooltipNav = (int)value;
    }

    /// <summary>ImGui Style.InputTextCursorSize configuration.</summary>
    public float InputTextCursorSize
    {
        get => _value.InputTextCursorSize;
        set => _value.InputTextCursorSize = value;
    }

    /// <summary>ImGui Style.SelectableRounding configuration.</summary>
    public float SelectableRounding
    {
        get => _value.SelectableRounding;
        set => _value.SelectableRounding = value;
    }

    /// <summary>ImGui Style.MenuItemRounding configuration.</summary>
    public float MenuItemRounding
    {
        get => _value.MenuItemRounding;
        set => _value.MenuItemRounding = value;
    }

    private static int ColorIndex(Col value) { var index = (int)value; ArgumentOutOfRangeException.ThrowIfNegative(index); ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, (int)Col.COUNT); return index; }

}
