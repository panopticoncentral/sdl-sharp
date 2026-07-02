using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Accessors for the global ImGui <c>ImGuiStyle</c> struct — all rounding, padding,
/// alignment, color, and tessellation fields. Changes apply immediately and persist
/// until reset or overwritten (e.g. by <see cref="ImGui.StyleColorsDark"/>).
/// </summary>
public static unsafe class Style
{
    // --- Font / Alpha ---

    /// <summary>Base font size in pixels (pre-scaling).</summary>
    public static float FontSizeBase
    {
        get => IGSharp_GetStyle()->FontSizeBase;
        set => IGSharp_GetStyle()->FontSizeBase = value;
    }

    /// <summary>Main font scale (multiplier on <see cref="FontSizeBase"/>).</summary>
    public static float FontScaleMain
    {
        get => IGSharp_GetStyle()->FontScaleMain;
        set => IGSharp_GetStyle()->FontScaleMain = value;
    }

    /// <summary>DPI font scale (read-only; set via <see cref="ImGui.SetFontScaleDpi"/>).</summary>
    public static float FontScaleDpi => IGSharp_GetStyle()->FontScaleDpi;

    /// <summary>Global alpha multiplier (1.0 opaque, 0.0 invisible).</summary>
    public static float Alpha
    {
        get => IGSharp_GetStyle()->Alpha;
        set => IGSharp_GetStyle()->Alpha = value;
    }

    /// <summary>Alpha multiplier for disabled widgets.</summary>
    public static float DisabledAlpha
    {
        get => IGSharp_GetStyle()->DisabledAlpha;
        set => IGSharp_GetStyle()->DisabledAlpha = value;
    }

    // --- Window ---

    /// <summary>Rounding radius of window corners.</summary>
    public static float WindowRounding
    {
        get => IGSharp_GetStyle()->WindowRounding;
        set => IGSharp_GetStyle()->WindowRounding = value;
    }

    /// <summary>Thickness of window border (0 = no border).</summary>
    public static float WindowBorderSize
    {
        get => IGSharp_GetStyle()->WindowBorderSize;
        set => IGSharp_GetStyle()->WindowBorderSize = value;
    }

    /// <summary>Padding inside a window.</summary>
    public static Vec2 WindowPadding
    {
        get { var v = IGSharp_GetStyle()->WindowPadding; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->WindowPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Minimum window size.</summary>
    public static Vec2 WindowMinSize
    {
        get { var v = IGSharp_GetStyle()->WindowMinSize; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->WindowMinSize = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Window title alignment (0.0 = left, 1.0 = right, 0.5 = centered).</summary>
    public static Vec2 WindowTitleAlign
    {
        get { var v = IGSharp_GetStyle()->WindowTitleAlign; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->WindowTitleAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    // --- Child / Popup ---

    /// <summary>Rounding radius of child window corners.</summary>
    public static float ChildRounding
    {
        get => IGSharp_GetStyle()->ChildRounding;
        set => IGSharp_GetStyle()->ChildRounding = value;
    }

    /// <summary>Thickness of child window border.</summary>
    public static float ChildBorderSize
    {
        get => IGSharp_GetStyle()->ChildBorderSize;
        set => IGSharp_GetStyle()->ChildBorderSize = value;
    }

    /// <summary>Rounding radius of popup window corners.</summary>
    public static float PopupRounding
    {
        get => IGSharp_GetStyle()->PopupRounding;
        set => IGSharp_GetStyle()->PopupRounding = value;
    }

    /// <summary>Thickness of popup window border.</summary>
    public static float PopupBorderSize
    {
        get => IGSharp_GetStyle()->PopupBorderSize;
        set => IGSharp_GetStyle()->PopupBorderSize = value;
    }

    // --- Frame (checkbox, slider, input, etc.) ---

    /// <summary>Rounding radius of framed widget corners.</summary>
    public static float FrameRounding
    {
        get => IGSharp_GetStyle()->FrameRounding;
        set => IGSharp_GetStyle()->FrameRounding = value;
    }

    /// <summary>Thickness of framed widget border.</summary>
    public static float FrameBorderSize
    {
        get => IGSharp_GetStyle()->FrameBorderSize;
        set => IGSharp_GetStyle()->FrameBorderSize = value;
    }

    /// <summary>Padding within a framed widget.</summary>
    public static Vec2 FramePadding
    {
        get { var v = IGSharp_GetStyle()->FramePadding; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->FramePadding = new IGSharp_Vec2(value.X, value.Y);
    }

    // --- Layout spacing ---

    /// <summary>Horizontal and vertical spacing between widgets.</summary>
    public static Vec2 ItemSpacing
    {
        get { var v = IGSharp_GetStyle()->ItemSpacing; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->ItemSpacing = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Horizontal and vertical spacing between sub-components within a widget.</summary>
    public static Vec2 ItemInnerSpacing
    {
        get { var v = IGSharp_GetStyle()->ItemInnerSpacing; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->ItemInnerSpacing = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Padding inside table cells.</summary>
    public static Vec2 CellPadding
    {
        get { var v = IGSharp_GetStyle()->CellPadding; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->CellPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Extra padding around items for touch input.</summary>
    public static Vec2 TouchExtraPadding
    {
        get { var v = IGSharp_GetStyle()->TouchExtraPadding; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->TouchExtraPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Indent amount for TreeNode / Indent.</summary>
    public static float IndentSpacing
    {
        get => IGSharp_GetStyle()->IndentSpacing;
        set => IGSharp_GetStyle()->IndentSpacing = value;
    }

    /// <summary>Minimum spacing between columns in legacy Columns API.</summary>
    public static float ColumnsMinSpacing
    {
        get => IGSharp_GetStyle()->ColumnsMinSpacing;
        set => IGSharp_GetStyle()->ColumnsMinSpacing = value;
    }

    // --- Scrollbar / Grab / Image / Tab / Separator ---

    /// <summary>Width of the scrollbar.</summary>
    public static float ScrollbarSize
    {
        get => IGSharp_GetStyle()->ScrollbarSize;
        set => IGSharp_GetStyle()->ScrollbarSize = value;
    }

    /// <summary>Rounding radius of scrollbar grab corners.</summary>
    public static float ScrollbarRounding
    {
        get => IGSharp_GetStyle()->ScrollbarRounding;
        set => IGSharp_GetStyle()->ScrollbarRounding = value;
    }

    /// <summary>Minimum size of a slider/scrollbar grab.</summary>
    public static float GrabMinSize
    {
        get => IGSharp_GetStyle()->GrabMinSize;
        set => IGSharp_GetStyle()->GrabMinSize = value;
    }

    /// <summary>Rounding radius of slider grab corners.</summary>
    public static float GrabRounding
    {
        get => IGSharp_GetStyle()->GrabRounding;
        set => IGSharp_GetStyle()->GrabRounding = value;
    }

    /// <summary>Rounding radius of image corners.</summary>
    public static float ImageRounding
    {
        get => IGSharp_GetStyle()->ImageRounding;
        set => IGSharp_GetStyle()->ImageRounding = value;
    }

    /// <summary>Thickness of image border.</summary>
    public static float ImageBorderSize
    {
        get => IGSharp_GetStyle()->ImageBorderSize;
        set => IGSharp_GetStyle()->ImageBorderSize = value;
    }

    /// <summary>Rounding radius of tab corners.</summary>
    public static float TabRounding
    {
        get => IGSharp_GetStyle()->TabRounding;
        set => IGSharp_GetStyle()->TabRounding = value;
    }

    /// <summary>Thickness of tab border.</summary>
    public static float TabBorderSize
    {
        get => IGSharp_GetStyle()->TabBorderSize;
        set => IGSharp_GetStyle()->TabBorderSize = value;
    }

    /// <summary>Thickness of separator lines.</summary>
    public static float SeparatorSize
    {
        get => IGSharp_GetStyle()->SeparatorSize;
        set => IGSharp_GetStyle()->SeparatorSize = value;
    }

    // --- Text alignment ---

    /// <summary>Alignment of text inside a button (default 0.5, 0.5 = centered).</summary>
    public static Vec2 ButtonTextAlign
    {
        get { var v = IGSharp_GetStyle()->ButtonTextAlign; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->ButtonTextAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Alignment of text inside a selectable.</summary>
    public static Vec2 SelectableTextAlign
    {
        get { var v = IGSharp_GetStyle()->SelectableTextAlign; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->SelectableTextAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Alignment of separator text.</summary>
    public static Vec2 SeparatorTextAlign
    {
        get { var v = IGSharp_GetStyle()->SeparatorTextAlign; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->SeparatorTextAlign = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Padding around separator text.</summary>
    public static Vec2 SeparatorTextPadding
    {
        get { var v = IGSharp_GetStyle()->SeparatorTextPadding; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->SeparatorTextPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    // --- Display padding ---

    /// <summary>Window padding at the display edge (for windows flush to the edge).</summary>
    public static Vec2 DisplayWindowPadding
    {
        get { var v = IGSharp_GetStyle()->DisplayWindowPadding; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->DisplayWindowPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Safe-area padding (for TVs and notched displays).</summary>
    public static Vec2 DisplaySafeAreaPadding
    {
        get { var v = IGSharp_GetStyle()->DisplaySafeAreaPadding; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetStyle()->DisplaySafeAreaPadding = new IGSharp_Vec2(value.X, value.Y);
    }

    // --- Mouse cursor / tessellation ---

    /// <summary>Scale factor applied to software-drawn mouse cursor.</summary>
    public static float MouseCursorScale
    {
        get => IGSharp_GetStyle()->MouseCursorScale;
        set => IGSharp_GetStyle()->MouseCursorScale = value;
    }

    /// <summary>Enable anti-aliased lines (expensive for custom drawing).</summary>
    public static bool AntiAliasedLines
    {
        get => IGSharp_GetStyle()->AntiAliasedLines;
        set => IGSharp_GetStyle()->AntiAliasedLines = value;
    }

    /// <summary>Enable anti-aliased filled shapes.</summary>
    public static bool AntiAliasedFill
    {
        get => IGSharp_GetStyle()->AntiAliasedFill;
        set => IGSharp_GetStyle()->AntiAliasedFill = value;
    }

    /// <summary>Tessellation tolerance for curves (lower = more segments).</summary>
    public static float CurveTessellationTol
    {
        get => IGSharp_GetStyle()->CurveTessellationTol;
        set => IGSharp_GetStyle()->CurveTessellationTol = value;
    }

    /// <summary>Maximum error in pixels for circle tessellation.</summary>
    public static float CircleTessellationMaxError
    {
        get => IGSharp_GetStyle()->CircleTessellationMaxError;
        set => IGSharp_GetStyle()->CircleTessellationMaxError = value;
    }

    // --- Colors ---

    /// <summary>Gets the color for the given palette index.</summary>
    public static Vec4 GetColor(Col idx)
    {
        var v = IGSharp_GetStyle()->Colors[(int)idx];
        return new Vec4(v.X, v.Y, v.Z, v.W);
    }

    /// <summary>Sets the color for the given palette index.</summary>
    public static void SetColor(Col idx, Vec4 col)
        => IGSharp_GetStyle()->Colors[(int)idx] = new IGSharp_Vec4(col.X, col.Y, col.Z, col.W);

    /// <summary>Sets the color for the given palette index from RGBA floats.</summary>
    public static void SetColor(Col idx, float r, float g, float b, float a)
        => IGSharp_GetStyle()->Colors[(int)idx] = new IGSharp_Vec4(r, g, b, a);
}
