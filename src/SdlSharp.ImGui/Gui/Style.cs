using static SdlSharp.ImGui.Native;

namespace SdlSharp.Gui;

/// <summary>
/// Accessors for the global ImGui <c>ImGuiStyle</c> struct — all rounding, padding,
/// alignment, color, and tessellation fields. Changes apply immediately and persist
/// until reset or overwritten (e.g. by <see cref="Gui.StyleColorsDark"/>).
/// </summary>
public static class Style
{
    // --- Font / Alpha ---

    /// <summary>Base font size in pixels (pre-scaling).</summary>
    public static float FontSizeBase
    {
        get => IGSharp_Style_GetFontSizeBase();
        set => IGSharp_Style_SetFontSizeBase(value);
    }

    /// <summary>Main font scale (multiplier on <see cref="FontSizeBase"/>).</summary>
    public static float FontScaleMain
    {
        get => IGSharp_Style_GetFontScaleMain();
        set => IGSharp_Style_SetFontScaleMain(value);
    }

    /// <summary>DPI font scale (read-only; set via <see cref="Gui.SetFontScaleDpi"/>).</summary>
    public static float FontScaleDpi => IGSharp_Style_GetFontScaleDpi();

    /// <summary>Global alpha multiplier (1.0 opaque, 0.0 invisible).</summary>
    public static float Alpha
    {
        get => IGSharp_Style_GetAlpha();
        set => IGSharp_Style_SetAlpha(value);
    }

    /// <summary>Alpha multiplier for disabled widgets.</summary>
    public static float DisabledAlpha
    {
        get => IGSharp_Style_GetDisabledAlpha();
        set => IGSharp_Style_SetDisabledAlpha(value);
    }

    // --- Window ---

    /// <summary>Rounding radius of window corners.</summary>
    public static float WindowRounding
    {
        get => IGSharp_Style_GetWindowRounding();
        set => IGSharp_Style_SetWindowRounding(value);
    }

    /// <summary>Thickness of window border (0 = no border).</summary>
    public static float WindowBorderSize
    {
        get => IGSharp_Style_GetWindowBorderSize();
        set => IGSharp_Style_SetWindowBorderSize(value);
    }

    /// <summary>Padding inside a window.</summary>
    public static Vec2 WindowPadding
    {
        get { var v = IGSharp_Style_GetWindowPadding(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetWindowPadding(new IGSharp_Vec2(value.X, value.Y));
    }

    /// <summary>Minimum window size.</summary>
    public static Vec2 WindowMinSize
    {
        get { var v = IGSharp_Style_GetWindowMinSize(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetWindowMinSize(new IGSharp_Vec2(value.X, value.Y));
    }

    /// <summary>Window title alignment (0.0 = left, 1.0 = right, 0.5 = centered).</summary>
    public static Vec2 WindowTitleAlign
    {
        get { var v = IGSharp_Style_GetWindowTitleAlign(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetWindowTitleAlign(new IGSharp_Vec2(value.X, value.Y));
    }

    // --- Child / Popup ---

    /// <summary>Rounding radius of child window corners.</summary>
    public static float ChildRounding
    {
        get => IGSharp_Style_GetChildRounding();
        set => IGSharp_Style_SetChildRounding(value);
    }

    /// <summary>Thickness of child window border.</summary>
    public static float ChildBorderSize
    {
        get => IGSharp_Style_GetChildBorderSize();
        set => IGSharp_Style_SetChildBorderSize(value);
    }

    /// <summary>Rounding radius of popup window corners.</summary>
    public static float PopupRounding
    {
        get => IGSharp_Style_GetPopupRounding();
        set => IGSharp_Style_SetPopupRounding(value);
    }

    /// <summary>Thickness of popup window border.</summary>
    public static float PopupBorderSize
    {
        get => IGSharp_Style_GetPopupBorderSize();
        set => IGSharp_Style_SetPopupBorderSize(value);
    }

    // --- Frame (checkbox, slider, input, etc.) ---

    /// <summary>Rounding radius of framed widget corners.</summary>
    public static float FrameRounding
    {
        get => IGSharp_Style_GetFrameRounding();
        set => IGSharp_Style_SetFrameRounding(value);
    }

    /// <summary>Thickness of framed widget border.</summary>
    public static float FrameBorderSize
    {
        get => IGSharp_Style_GetFrameBorderSize();
        set => IGSharp_Style_SetFrameBorderSize(value);
    }

    /// <summary>Padding within a framed widget.</summary>
    public static Vec2 FramePadding
    {
        get { var v = IGSharp_Style_GetFramePadding(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetFramePadding(new IGSharp_Vec2(value.X, value.Y));
    }

    // --- Layout spacing ---

    /// <summary>Horizontal and vertical spacing between widgets.</summary>
    public static Vec2 ItemSpacing
    {
        get { var v = IGSharp_Style_GetItemSpacing(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetItemSpacing(new IGSharp_Vec2(value.X, value.Y));
    }

    /// <summary>Horizontal and vertical spacing between sub-components within a widget.</summary>
    public static Vec2 ItemInnerSpacing
    {
        get { var v = IGSharp_Style_GetItemInnerSpacing(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetItemInnerSpacing(new IGSharp_Vec2(value.X, value.Y));
    }

    /// <summary>Padding inside table cells.</summary>
    public static Vec2 CellPadding
    {
        get { var v = IGSharp_Style_GetCellPadding(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetCellPadding(new IGSharp_Vec2(value.X, value.Y));
    }

    /// <summary>Extra padding around items for touch input.</summary>
    public static Vec2 TouchExtraPadding
    {
        get { var v = IGSharp_Style_GetTouchExtraPadding(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetTouchExtraPadding(new IGSharp_Vec2(value.X, value.Y));
    }

    /// <summary>Indent amount for TreeNode / Indent.</summary>
    public static float IndentSpacing
    {
        get => IGSharp_Style_GetIndentSpacing();
        set => IGSharp_Style_SetIndentSpacing(value);
    }

    /// <summary>Minimum spacing between columns in legacy Columns API.</summary>
    public static float ColumnsMinSpacing
    {
        get => IGSharp_Style_GetColumnsMinSpacing();
        set => IGSharp_Style_SetColumnsMinSpacing(value);
    }

    // --- Scrollbar / Grab / Image / Tab / Separator ---

    /// <summary>Width of the scrollbar.</summary>
    public static float ScrollbarSize
    {
        get => IGSharp_Style_GetScrollbarSize();
        set => IGSharp_Style_SetScrollbarSize(value);
    }

    /// <summary>Rounding radius of scrollbar grab corners.</summary>
    public static float ScrollbarRounding
    {
        get => IGSharp_Style_GetScrollbarRounding();
        set => IGSharp_Style_SetScrollbarRounding(value);
    }

    /// <summary>Minimum size of a slider/scrollbar grab.</summary>
    public static float GrabMinSize
    {
        get => IGSharp_Style_GetGrabMinSize();
        set => IGSharp_Style_SetGrabMinSize(value);
    }

    /// <summary>Rounding radius of slider grab corners.</summary>
    public static float GrabRounding
    {
        get => IGSharp_Style_GetGrabRounding();
        set => IGSharp_Style_SetGrabRounding(value);
    }

    /// <summary>Rounding radius of image corners.</summary>
    public static float ImageRounding
    {
        get => IGSharp_Style_GetImageRounding();
        set => IGSharp_Style_SetImageRounding(value);
    }

    /// <summary>Thickness of image border.</summary>
    public static float ImageBorderSize
    {
        get => IGSharp_Style_GetImageBorderSize();
        set => IGSharp_Style_SetImageBorderSize(value);
    }

    /// <summary>Rounding radius of tab corners.</summary>
    public static float TabRounding
    {
        get => IGSharp_Style_GetTabRounding();
        set => IGSharp_Style_SetTabRounding(value);
    }

    /// <summary>Thickness of tab border.</summary>
    public static float TabBorderSize
    {
        get => IGSharp_Style_GetTabBorderSize();
        set => IGSharp_Style_SetTabBorderSize(value);
    }

    /// <summary>Thickness of separator lines.</summary>
    public static float SeparatorSize
    {
        get => IGSharp_Style_GetSeparatorSize();
        set => IGSharp_Style_SetSeparatorSize(value);
    }

    // --- Text alignment ---

    /// <summary>Alignment of text inside a button (default 0.5, 0.5 = centered).</summary>
    public static Vec2 ButtonTextAlign
    {
        get { var v = IGSharp_Style_GetButtonTextAlign(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetButtonTextAlign(new IGSharp_Vec2(value.X, value.Y));
    }

    /// <summary>Alignment of text inside a selectable.</summary>
    public static Vec2 SelectableTextAlign
    {
        get { var v = IGSharp_Style_GetSelectableTextAlign(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetSelectableTextAlign(new IGSharp_Vec2(value.X, value.Y));
    }

    /// <summary>Alignment of separator text.</summary>
    public static Vec2 SeparatorTextAlign
    {
        get { var v = IGSharp_Style_GetSeparatorTextAlign(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetSeparatorTextAlign(new IGSharp_Vec2(value.X, value.Y));
    }

    /// <summary>Padding around separator text.</summary>
    public static Vec2 SeparatorTextPadding
    {
        get { var v = IGSharp_Style_GetSeparatorTextPadding(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetSeparatorTextPadding(new IGSharp_Vec2(value.X, value.Y));
    }

    // --- Display padding ---

    /// <summary>Window padding at the display edge (for windows flush to the edge).</summary>
    public static Vec2 DisplayWindowPadding
    {
        get { var v = IGSharp_Style_GetDisplayWindowPadding(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetDisplayWindowPadding(new IGSharp_Vec2(value.X, value.Y));
    }

    /// <summary>Safe-area padding (for TVs and notched displays).</summary>
    public static Vec2 DisplaySafeAreaPadding
    {
        get { var v = IGSharp_Style_GetDisplaySafeAreaPadding(); return new Vec2(v.X, v.Y); }
        set => IGSharp_Style_SetDisplaySafeAreaPadding(new IGSharp_Vec2(value.X, value.Y));
    }

    // --- Mouse cursor / tessellation ---

    /// <summary>Scale factor applied to software-drawn mouse cursor.</summary>
    public static float MouseCursorScale
    {
        get => IGSharp_Style_GetMouseCursorScale();
        set => IGSharp_Style_SetMouseCursorScale(value);
    }

    /// <summary>Enable anti-aliased lines (expensive for custom drawing).</summary>
    public static bool AntiAliasedLines
    {
        get => IGSharp_Style_GetAntiAliasedLines();
        set => IGSharp_Style_SetAntiAliasedLines(value);
    }

    /// <summary>Enable anti-aliased filled shapes.</summary>
    public static bool AntiAliasedFill
    {
        get => IGSharp_Style_GetAntiAliasedFill();
        set => IGSharp_Style_SetAntiAliasedFill(value);
    }

    /// <summary>Tessellation tolerance for curves (lower = more segments).</summary>
    public static float CurveTessellationTol
    {
        get => IGSharp_Style_GetCurveTessellationTol();
        set => IGSharp_Style_SetCurveTessellationTol(value);
    }

    /// <summary>Maximum error in pixels for circle tessellation.</summary>
    public static float CircleTessellationMaxError
    {
        get => IGSharp_Style_GetCircleTessellationMaxError();
        set => IGSharp_Style_SetCircleTessellationMaxError(value);
    }

    // --- Colors ---

    /// <summary>Gets the color for the given palette index.</summary>
    public static Vec4 GetColor(Col idx)
    {
        var v = IGSharp_Style_GetColor((int)idx);
        return new Vec4(v.X, v.Y, v.Z, v.W);
    }

    /// <summary>Sets the color for the given palette index.</summary>
    public static void SetColor(Col idx, Vec4 col)
        => IGSharp_Style_SetColor((int)idx, new IGSharp_Vec4(col.X, col.Y, col.Z, col.W));

    /// <summary>Sets the color for the given palette index from RGBA floats.</summary>
    public static void SetColor(Col idx, float r, float g, float b, float a)
        => IGSharp_Style_SetColor((int)idx, new IGSharp_Vec4(r, g, b, a));
}
