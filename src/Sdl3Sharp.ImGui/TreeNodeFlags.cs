using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for TreeNodeEx() and CollapsingHeader().
/// </summary>
[Flags]
public enum TreeNodeFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiTreeNodeFlags.None,

    /// <summary>
    /// Draw as selected.
    /// </summary>
    Selected = ImGuiTreeNodeFlags.Selected,

    /// <summary>
    /// Draw frame with background (e.g., for CollapsingHeader).
    /// </summary>
    Framed = ImGuiTreeNodeFlags.Framed,

    /// <summary>
    /// Hit testing to allow subsequent widgets to overlap this one.
    /// </summary>
    AllowOverlap = ImGuiTreeNodeFlags.AllowOverlap,

    /// <summary>
    /// Don't do a TreePush() when open (e.g., for CollapsingHeader) = no extra indent nor pushing on ID stack.
    /// </summary>
    NoTreePushOnOpen = ImGuiTreeNodeFlags.NoTreePushOnOpen,

    /// <summary>
    /// Don't automatically and temporarily open node when Logging is active
    /// (by default logging will automatically open tree nodes).
    /// </summary>
    NoAutoOpenOnLog = ImGuiTreeNodeFlags.NoAutoOpenOnLog,

    /// <summary>
    /// Default node to be open.
    /// </summary>
    DefaultOpen = ImGuiTreeNodeFlags.DefaultOpen,

    /// <summary>
    /// Open on double-click instead of simple click (default for multi-select unless any _OpenOnXXX behavior is set explicitly).
    /// Both behaviors may be combined.
    /// </summary>
    OpenOnDoubleClick = ImGuiTreeNodeFlags.OpenOnDoubleClick,

    /// <summary>
    /// Open when clicking on the arrow part (default for multi-select unless any _OpenOnXXX behavior is set explicitly).
    /// Both behaviors may be combined.
    /// </summary>
    OpenOnArrow = ImGuiTreeNodeFlags.OpenOnArrow,

    /// <summary>
    /// No collapsing, no arrow (use as a convenience for leaf nodes).
    /// </summary>
    Leaf = ImGuiTreeNodeFlags.Leaf,

    /// <summary>
    /// Display a bullet instead of arrow.
    /// IMPORTANT: node can still be marked open/close if you don't set the <see cref="Leaf"/> flag!
    /// </summary>
    Bullet = ImGuiTreeNodeFlags.Bullet,

    /// <summary>
    /// Use FramePadding (even for an unframed text node) to vertically align text baseline to regular widget height.
    /// Equivalent to calling AlignTextToFramePadding() before the node.
    /// </summary>
    FramePadding = ImGuiTreeNodeFlags.FramePadding,

    /// <summary>
    /// Extend hit box to the right-most edge, even if not framed.
    /// This is not the default in order to allow adding other items on the same line without using AllowOverlap mode.
    /// </summary>
    SpanAvailWidth = ImGuiTreeNodeFlags.SpanAvailWidth,

    /// <summary>
    /// Extend hit box to the left-most and right-most edges (cover the indent area).
    /// </summary>
    SpanFullWidth = ImGuiTreeNodeFlags.SpanFullWidth,

    /// <summary>
    /// Narrow hit box + narrow hovering highlight, will only cover the label text.
    /// </summary>
    SpanLabelWidth = ImGuiTreeNodeFlags.SpanLabelWidth,

    /// <summary>
    /// Frame will span all columns of its container table (label will still fit in current column).
    /// </summary>
    SpanAllColumns = ImGuiTreeNodeFlags.SpanAllColumns,

    /// <summary>
    /// Label will span all columns of its container table.
    /// </summary>
    LabelSpanAllColumns = ImGuiTreeNodeFlags.LabelSpanAllColumns,

    /// <summary>
    /// Nav: left arrow moves back to parent. This is processed in TreePop() when there's an unfulfilled Left nav request remaining.
    /// </summary>
    NavLeftJumpsToParent = ImGuiTreeNodeFlags.NavLeftJumpsToParent,

    /// <summary>
    /// Combination of <see cref="Framed"/> and <see cref="NoTreePushOnOpen"/> and <see cref="NoAutoOpenOnLog"/>.
    /// </summary>
    CollapsingHeader = ImGuiTreeNodeFlags.CollapsingHeader,

    /// <summary>
    /// No lines drawn.
    /// </summary>
    DrawLinesNone = ImGuiTreeNodeFlags.DrawLinesNone,

    /// <summary>
    /// Horizontal lines to child nodes. Vertical line drawn down to TreePop() position: cover full contents. Faster (for large trees).
    /// </summary>
    DrawLinesFull = ImGuiTreeNodeFlags.DrawLinesFull,

    /// <summary>
    /// Horizontal lines to child nodes. Vertical line drawn down to bottom-most child node. Slower (for large trees).
    /// </summary>
    DrawLinesToNodes = ImGuiTreeNodeFlags.DrawLinesToNodes
}
