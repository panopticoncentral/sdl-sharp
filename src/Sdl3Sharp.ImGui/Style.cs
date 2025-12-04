using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents the ImGui style configuration wrapper.
/// </summary>
/// <remarks>
/// This wrapper provides access to style settings that control the visual appearance of Dear ImGui.
/// Access the current context's style via <see cref="Context.Style"/>.
/// </remarks>
public readonly unsafe struct Style
{
    internal ImGuiStyle* Native { get; }

    internal Style(ImGuiStyle* native)
    {
        Native = native;
    }

    /// <summary>
    /// Gets or sets the current base font size before external global factors are applied.
    /// </summary>
    /// <remarks>
    /// Use PushFont(NULL, size) to modify. Use GetFontSize() to obtain scaled value.
    /// Recap: GetFontSize() == FontSizeBase * (FontScaleMain * FontScaleDpi * other_scaling_factors)
    /// </remarks>
    public float FontSizeBase
    {
        get => Native->FontSizeBase;
        set => Native->FontSizeBase = value;
    }

    /// <summary>
    /// Gets or sets the main global scale factor.
    /// </summary>
    /// <remarks>
    /// May be set by application once, or exposed to end-user.
    /// </remarks>
    public float FontScaleMain
    {
        get => Native->FontScaleMain;
        set => Native->FontScaleMain = value;
    }

    /// <summary>
    /// Gets or sets the additional global scale factor from viewport/monitor contents scale.
    /// </summary>
    /// <remarks>
    /// When io.ConfigDpiScaleFonts is enabled, this is automatically overwritten when changing monitor DPI.
    /// </remarks>
    public float FontScaleDpi
    {
        get => Native->FontScaleDpi;
        set => Native->FontScaleDpi = value;
    }

    /// <summary>
    /// Gets or sets the global alpha that applies to everything in Dear ImGui.
    /// </summary>
    public float Alpha
    {
        get => Native->Alpha;
        set => Native->Alpha = value;
    }

    /// <summary>
    /// Gets or sets the additional alpha multiplier applied by BeginDisabled().
    /// </summary>
    /// <remarks>
    /// Multiplies over current value of Alpha.
    /// </remarks>
    public float DisabledAlpha
    {
        get => Native->DisabledAlpha;
        set => Native->DisabledAlpha = value;
    }

    /// <summary>
    /// Gets or sets the padding within a window.
    /// </summary>
    public Vec2 WindowPadding
    {
        get => Vec2.FromNative(Native->WindowPadding);
        set => Native->WindowPadding = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the radius of window corners rounding.
    /// </summary>
    /// <remarks>
    /// Set to 0.0f to have rectangular windows. Large values tend to lead to variety of artifacts and are not recommended.
    /// </remarks>
    public float WindowRounding
    {
        get => Native->WindowRounding;
        set => Native->WindowRounding = value;
    }

    /// <summary>
    /// Gets or sets the thickness of border around windows.
    /// </summary>
    /// <remarks>
    /// Generally set to 0.0f or 1.0f. Other values are not well tested and more CPU/GPU costly.
    /// </remarks>
    public float WindowBorderSize
    {
        get => Native->WindowBorderSize;
        set => Native->WindowBorderSize = value;
    }

    /// <summary>
    /// Gets or sets the hit-testing extent outside/inside resizing border.
    /// </summary>
    /// <remarks>
    /// Also extends determination of hovered window. Generally meaningfully larger than WindowBorderSize
    /// to make it easy to reach borders.
    /// </remarks>
    public float WindowBorderHoverPadding
    {
        get => Native->WindowBorderHoverPadding;
        set => Native->WindowBorderHoverPadding = value;
    }

    /// <summary>
    /// Gets or sets the minimum window size.
    /// </summary>
    /// <remarks>
    /// This is a global setting. If you want to constrain individual windows, use SetNextWindowSizeConstraints().
    /// </remarks>
    public Vec2 WindowMinSize
    {
        get => Vec2.FromNative(Native->WindowMinSize);
        set => Native->WindowMinSize = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the alignment for title bar text.
    /// </summary>
    /// <remarks>
    /// Defaults to (0.0f, 0.5f) for left-aligned, vertically centered.
    /// </remarks>
    public Vec2 WindowTitleAlign
    {
        get => Vec2.FromNative(Native->WindowTitleAlign);
        set => Native->WindowTitleAlign = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the side of the collapsing/docking button in the title bar (None/Left/Right).
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="Dir.Left"/>.
    /// </remarks>
    public Dir WindowMenuButtonPosition
    {
        get => (Dir)Native->WindowMenuButtonPosition;
        set => Native->WindowMenuButtonPosition = (ImGuiDir)value;
    }

    /// <summary>
    /// Gets or sets the radius of child window corners rounding.
    /// </summary>
    /// <remarks>
    /// Set to 0.0f to have rectangular windows.
    /// </remarks>
    public float ChildRounding
    {
        get => Native->ChildRounding;
        set => Native->ChildRounding = value;
    }

    /// <summary>
    /// Gets or sets the thickness of border around child windows.
    /// </summary>
    /// <remarks>
    /// Generally set to 0.0f or 1.0f. Other values are not well tested and more CPU/GPU costly.
    /// </remarks>
    public float ChildBorderSize
    {
        get => Native->ChildBorderSize;
        set => Native->ChildBorderSize = value;
    }

    /// <summary>
    /// Gets or sets the radius of popup window corners rounding.
    /// </summary>
    /// <remarks>
    /// Note that tooltip windows use WindowRounding.
    /// </remarks>
    public float PopupRounding
    {
        get => Native->PopupRounding;
        set => Native->PopupRounding = value;
    }

    /// <summary>
    /// Gets or sets the thickness of border around popup/tooltip windows.
    /// </summary>
    /// <remarks>
    /// Generally set to 0.0f or 1.0f. Other values are not well tested and more CPU/GPU costly.
    /// </remarks>
    public float PopupBorderSize
    {
        get => Native->PopupBorderSize;
        set => Native->PopupBorderSize = value;
    }

    /// <summary>
    /// Gets or sets the padding within a framed rectangle (used by most widgets).
    /// </summary>
    public Vec2 FramePadding
    {
        get => Vec2.FromNative(Native->FramePadding);
        set => Native->FramePadding = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the radius of frame corners rounding.
    /// </summary>
    /// <remarks>
    /// Set to 0.0f to have rectangular frames (used by most widgets).
    /// </remarks>
    public float FrameRounding
    {
        get => Native->FrameRounding;
        set => Native->FrameRounding = value;
    }

    /// <summary>
    /// Gets or sets the thickness of border around frames.
    /// </summary>
    /// <remarks>
    /// Generally set to 0.0f or 1.0f. Other values are not well tested and more CPU/GPU costly.
    /// </remarks>
    public float FrameBorderSize
    {
        get => Native->FrameBorderSize;
        set => Native->FrameBorderSize = value;
    }

    /// <summary>
    /// Gets or sets the horizontal and vertical spacing between widgets/lines.
    /// </summary>
    public Vec2 ItemSpacing
    {
        get => Vec2.FromNative(Native->ItemSpacing);
        set => Native->ItemSpacing = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the horizontal and vertical spacing between within elements of a composed widget
    /// (e.g., a slider and its label).
    /// </summary>
    public Vec2 ItemInnerSpacing
    {
        get => Vec2.FromNative(Native->ItemInnerSpacing);
        set => Native->ItemInnerSpacing = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the padding within a table cell.
    /// </summary>
    /// <remarks>
    /// CellPadding.X is locked for entire table. CellPadding.Y may be altered between different rows.
    /// </remarks>
    public Vec2 CellPadding
    {
        get => Vec2.FromNative(Native->CellPadding);
        set => Native->CellPadding = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the expand reactive bounding box for touch-based systems.
    /// </summary>
    /// <remarks>
    /// Touch position is not accurate enough. Unfortunately we don't sort widgets so priority
    /// on overlap will always be given to the first widget. So don't grow this too much!
    /// </remarks>
    public Vec2 TouchExtraPadding
    {
        get => Vec2.FromNative(Native->TouchExtraPadding);
        set => Native->TouchExtraPadding = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the horizontal indentation when entering a tree node.
    /// </summary>
    /// <remarks>
    /// Generally == (FontSize + FramePadding.X * 2).
    /// </remarks>
    public float IndentSpacing
    {
        get => Native->IndentSpacing;
        set => Native->IndentSpacing = value;
    }

    /// <summary>
    /// Gets or sets the minimum horizontal spacing between two columns.
    /// </summary>
    /// <remarks>
    /// Preferably > (FramePadding.X + 1).
    /// </remarks>
    public float ColumnsMinSpacing
    {
        get => Native->ColumnsMinSpacing;
        set => Native->ColumnsMinSpacing = value;
    }

    /// <summary>
    /// Gets or sets the width of the vertical scrollbar, Height of the horizontal scrollbar.
    /// </summary>
    public float ScrollbarSize
    {
        get => Native->ScrollbarSize;
        set => Native->ScrollbarSize = value;
    }

    /// <summary>
    /// Gets or sets the radius of grab corners for scrollbar.
    /// </summary>
    public float ScrollbarRounding
    {
        get => Native->ScrollbarRounding;
        set => Native->ScrollbarRounding = value;
    }

    /// <summary>
    /// Gets or sets the padding of scrollbar grab within its frame (same for both axes).
    /// </summary>
    public float ScrollbarPadding
    {
        get => Native->ScrollbarPadding;
        set => Native->ScrollbarPadding = value;
    }

    /// <summary>
    /// Gets or sets the minimum width/height of a grab box for slider/scrollbar.
    /// </summary>
    public float GrabMinSize
    {
        get => Native->GrabMinSize;
        set => Native->GrabMinSize = value;
    }

    /// <summary>
    /// Gets or sets the radius of grabs corners rounding.
    /// </summary>
    /// <remarks>
    /// Set to 0.0f to have rectangular slider grabs.
    /// </remarks>
    public float GrabRounding
    {
        get => Native->GrabRounding;
        set => Native->GrabRounding = value;
    }

    /// <summary>
    /// Gets or sets the size in pixels of the dead-zone around zero on logarithmic sliders that cross zero.
    /// </summary>
    public float LogSliderDeadzone
    {
        get => Native->LogSliderDeadzone;
        set => Native->LogSliderDeadzone = value;
    }

    /// <summary>
    /// Gets or sets the thickness of border around Image() calls.
    /// </summary>
    public float ImageBorderSize
    {
        get => Native->ImageBorderSize;
        set => Native->ImageBorderSize = value;
    }

    /// <summary>
    /// Gets or sets the radius of upper corners of a tab.
    /// </summary>
    /// <remarks>
    /// Set to 0.0f to have rectangular tabs.
    /// </remarks>
    public float TabRounding
    {
        get => Native->TabRounding;
        set => Native->TabRounding = value;
    }

    /// <summary>
    /// Gets or sets the thickness of border around tabs.
    /// </summary>
    public float TabBorderSize
    {
        get => Native->TabBorderSize;
        set => Native->TabBorderSize = value;
    }

    /// <summary>
    /// Gets or sets the minimum tab width, to make tabs larger than their contents.
    /// </summary>
    /// <remarks>
    /// TabBar buttons are not affected.
    /// </remarks>
    public float TabMinWidthBase
    {
        get => Native->TabMinWidthBase;
        set => Native->TabMinWidthBase = value;
    }

    /// <summary>
    /// Gets or sets the minimum tab width after shrinking, when using FittingPolicyMixed policy.
    /// </summary>
    public float TabMinWidthShrink
    {
        get => Native->TabMinWidthShrink;
        set => Native->TabMinWidthShrink = value;
    }

    /// <summary>
    /// Gets or sets when the close button is visible for selected tabs.
    /// </summary>
    /// <remarks>
    /// -1: always visible. 0.0f: visible when hovered. > 0.0f: visible when hovered if minimum width.
    /// </remarks>
    public float TabCloseButtonMinWidthSelected
    {
        get => Native->TabCloseButtonMinWidthSelected;
        set => Native->TabCloseButtonMinWidthSelected = value;
    }

    /// <summary>
    /// Gets or sets when the close button is visible for unselected tabs.
    /// </summary>
    /// <remarks>
    /// -1: always visible. 0.0f: visible when hovered. > 0.0f: visible when hovered if minimum width.
    /// float.MaxValue: never show close button when unselected.
    /// </remarks>
    public float TabCloseButtonMinWidthUnselected
    {
        get => Native->TabCloseButtonMinWidthUnselected;
        set => Native->TabCloseButtonMinWidthUnselected = value;
    }

    /// <summary>
    /// Gets or sets the thickness of tab-bar separator, which takes on the tab active color to denote focus.
    /// </summary>
    public float TabBarBorderSize
    {
        get => Native->TabBarBorderSize;
        set => Native->TabBarBorderSize = value;
    }

    /// <summary>
    /// Gets or sets the thickness of tab-bar overline, which highlights the selected tab-bar.
    /// </summary>
    public float TabBarOverlineSize
    {
        get => Native->TabBarOverlineSize;
        set => Native->TabBarOverlineSize = value;
    }

    /// <summary>
    /// Gets or sets the angle of angled headers (supported values range from -50.0f degrees to +50.0f degrees).
    /// </summary>
    public float TableAngledHeadersAngle
    {
        get => Native->TableAngledHeadersAngle;
        set => Native->TableAngledHeadersAngle = value;
    }

    /// <summary>
    /// Gets or sets the alignment of angled headers within the cell.
    /// </summary>
    public Vec2 TableAngledHeadersTextAlign
    {
        get => Vec2.FromNative(Native->TableAngledHeadersTextAlign);
        set => Native->TableAngledHeadersTextAlign = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the default way to draw lines connecting TreeNode hierarchy.
    /// </summary>
    /// <remarks>
    /// Use <see cref="TreeNodeFlags.DrawLinesNone"/>, <see cref="TreeNodeFlags.DrawLinesFull"/>,
    /// or <see cref="TreeNodeFlags.DrawLinesToNodes"/>.
    /// </remarks>
    public TreeNodeFlags TreeLinesFlags
    {
        get => (TreeNodeFlags)Native->TreeLinesFlags;
        set => Native->TreeLinesFlags = (ImGuiTreeNodeFlags)value;
    }

    /// <summary>
    /// Gets or sets the thickness of outlines when using DrawLines flags.
    /// </summary>
    public float TreeLinesSize
    {
        get => Native->TreeLinesSize;
        set => Native->TreeLinesSize = value;
    }

    /// <summary>
    /// Gets or sets the radius of lines connecting child nodes to the vertical line.
    /// </summary>
    public float TreeLinesRounding
    {
        get => Native->TreeLinesRounding;
        set => Native->TreeLinesRounding = value;
    }

    /// <summary>
    /// Gets or sets the radius of the drag and drop target frame.
    /// </summary>
    public float DragDropTargetRounding
    {
        get => Native->DragDropTargetRounding;
        set => Native->DragDropTargetRounding = value;
    }

    /// <summary>
    /// Gets or sets the thickness of the drag and drop target border.
    /// </summary>
    public float DragDropTargetBorderSize
    {
        get => Native->DragDropTargetBorderSize;
        set => Native->DragDropTargetBorderSize = value;
    }

    /// <summary>
    /// Gets or sets the size to expand the drag and drop target from actual target item size.
    /// </summary>
    public float DragDropTargetPadding
    {
        get => Native->DragDropTargetPadding;
        set => Native->DragDropTargetPadding = value;
    }

    /// <summary>
    /// Gets or sets the side of the color button in the ColorEdit4 widget (left/right).
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="Dir.Right"/>.
    /// </remarks>
    public Dir ColorButtonPosition
    {
        get => (Dir)Native->ColorButtonPosition;
        set => Native->ColorButtonPosition = (ImGuiDir)value;
    }

    /// <summary>
    /// Gets or sets the alignment of button text when button is larger than text.
    /// </summary>
    /// <remarks>
    /// Defaults to (0.5f, 0.5f) (centered).
    /// </remarks>
    public Vec2 ButtonTextAlign
    {
        get => Vec2.FromNative(Native->ButtonTextAlign);
        set => Native->ButtonTextAlign = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the alignment of selectable text.
    /// </summary>
    /// <remarks>
    /// Defaults to (0.0f, 0.0f) (top-left aligned). It's generally important to keep this left-aligned
    /// if you want to lay multiple items on a same line.
    /// </remarks>
    public Vec2 SelectableTextAlign
    {
        get => Vec2.FromNative(Native->SelectableTextAlign);
        set => Native->SelectableTextAlign = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the thickness of border in SeparatorText().
    /// </summary>
    public float SeparatorTextBorderSize
    {
        get => Native->SeparatorTextBorderSize;
        set => Native->SeparatorTextBorderSize = value;
    }

    /// <summary>
    /// Gets or sets the alignment of text within the separator.
    /// </summary>
    /// <remarks>
    /// Defaults to (0.0f, 0.5f) (left aligned, center).
    /// </remarks>
    public Vec2 SeparatorTextAlign
    {
        get => Vec2.FromNative(Native->SeparatorTextAlign);
        set => Native->SeparatorTextAlign = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the horizontal offset of text from each edge of the separator + spacing on other axis.
    /// </summary>
    /// <remarks>
    /// Generally small values. Y is recommended to be == FramePadding.Y.
    /// </remarks>
    public Vec2 SeparatorTextPadding
    {
        get => Vec2.FromNative(Native->SeparatorTextPadding);
        set => Native->SeparatorTextPadding = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the amount enforced to keep visible when moving near edges of your screen.
    /// </summary>
    /// <remarks>
    /// Apply to regular windows.
    /// </remarks>
    public Vec2 DisplayWindowPadding
    {
        get => Vec2.FromNative(Native->DisplayWindowPadding);
        set => Native->DisplayWindowPadding = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the amount where we avoid displaying contents.
    /// </summary>
    /// <remarks>
    /// Apply to every windows, menus, popups, tooltips. Adjust if you cannot see the edges of your screen
    /// (e.g., on a TV where scaling has not been configured).
    /// </remarks>
    public Vec2 DisplaySafeAreaPadding
    {
        get => Vec2.FromNative(Native->DisplaySafeAreaPadding);
        set => Native->DisplaySafeAreaPadding = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the scale for software rendered mouse cursor.
    /// </summary>
    /// <remarks>
    /// When io.MouseDrawCursor is enabled. We apply per-monitor DPI scaling over this scale. May be removed later.
    /// </remarks>
    public float MouseCursorScale
    {
        get => Native->MouseCursorScale;
        set => Native->MouseCursorScale = value;
    }

    /// <summary>
    /// Gets or sets whether to enable anti-aliased lines/borders.
    /// </summary>
    /// <remarks>
    /// Disable if you are really tight on CPU/GPU. Latched at the beginning of the frame (copied to ImDrawList).
    /// </remarks>
    public bool AntiAliasedLines
    {
        get => Native->AntiAliasedLines;
        set => Native->AntiAliasedLines = value;
    }

    /// <summary>
    /// Gets or sets whether to enable anti-aliased lines/borders using textures where possible.
    /// </summary>
    /// <remarks>
    /// Requires backend to render with bilinear filtering (NOT point/nearest filtering).
    /// Latched at the beginning of the frame (copied to ImDrawList).
    /// </remarks>
    public bool AntiAliasedLinesUseTex
    {
        get => Native->AntiAliasedLinesUseTex;
        set => Native->AntiAliasedLinesUseTex = value;
    }

    /// <summary>
    /// Gets or sets whether to enable anti-aliased edges around filled shapes (rounded rectangles, circles, etc.).
    /// </summary>
    /// <remarks>
    /// Disable if you are really tight on CPU/GPU. Latched at the beginning of the frame (copied to ImDrawList).
    /// </remarks>
    public bool AntiAliasedFill
    {
        get => Native->AntiAliasedFill;
        set => Native->AntiAliasedFill = value;
    }

    /// <summary>
    /// Gets or sets the tessellation tolerance when using PathBezierCurveTo() without a specific number of segments.
    /// </summary>
    /// <remarks>
    /// Decrease for highly tessellated curves (higher quality, more polygons), increase to reduce quality.
    /// </remarks>
    public float CurveTessellationTol
    {
        get => Native->CurveTessellationTol;
        set => Native->CurveTessellationTol = value;
    }

    /// <summary>
    /// Gets or sets the maximum error (in pixels) allowed when using AddCircle()/AddCircleFilled()
    /// or drawing rounded corner rectangles with no explicit segment count specified.
    /// </summary>
    /// <remarks>
    /// Decrease for higher quality but more geometry.
    /// </remarks>
    public float CircleTessellationMaxError
    {
        get => Native->CircleTessellationMaxError;
        set => Native->CircleTessellationMaxError = value;
    }

    /// <summary>
    /// Gets or sets the delay for IsItemHovered(Stationary).
    /// </summary>
    /// <remarks>
    /// Time required to consider mouse stationary.
    /// </remarks>
    public float HoverStationaryDelay
    {
        get => Native->HoverStationaryDelay;
        set => Native->HoverStationaryDelay = value;
    }

    /// <summary>
    /// Gets or sets the delay for IsItemHovered(DelayShort).
    /// </summary>
    /// <remarks>
    /// Usually used along with HoverStationaryDelay.
    /// </remarks>
    public float HoverDelayShort
    {
        get => Native->HoverDelayShort;
        set => Native->HoverDelayShort = value;
    }

    /// <summary>
    /// Gets or sets the delay for IsItemHovered(DelayNormal).
    /// </summary>
    public float HoverDelayNormal
    {
        get => Native->HoverDelayNormal;
        set => Native->HoverDelayNormal = value;
    }

    /// <summary>
    /// Gets or sets the default flags when using IsItemHovered(ForTooltip) or BeginItemTooltip()/SetItemTooltip()
    /// while using mouse.
    /// </summary>
    public HoveredFlags HoverFlagsForTooltipMouse
    {
        get => (HoveredFlags)Native->HoverFlagsForTooltipMouse;
        set => Native->HoverFlagsForTooltipMouse = (ImGuiHoveredFlags)value;
    }

    /// <summary>
    /// Gets or sets the default flags when using IsItemHovered(ForTooltip) or BeginItemTooltip()/SetItemTooltip()
    /// while using keyboard/gamepad.
    /// </summary>
    public HoveredFlags HoverFlagsForTooltipNav
    {
        get => (HoveredFlags)Native->HoverFlagsForTooltipNav;
        set => Native->HoverFlagsForTooltipNav = (ImGuiHoveredFlags)value;
    }

    /// <summary>
    /// Scales all spacing/padding/thickness values by the given factor.
    /// </summary>
    /// <param name="scaleFactor">The scale factor to apply.</param>
    /// <remarks>
    /// Does not scale fonts.
    /// </remarks>
    public void ScaleAllSizes(float scaleFactor)
    {
        ImGuiStyle.ScaleAllSizes(Native, scaleFactor);
    }
}
