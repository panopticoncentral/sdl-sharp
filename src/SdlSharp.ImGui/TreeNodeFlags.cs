namespace SdlSharp.ImGui;

/// <summary>Flags for <see cref="ImGui.CollapsingHeader(string, TreeNodeFlags)"/> and <see cref="ImGui.TreeNodeEx(string, TreeNodeFlags)"/>.</summary>
[Flags]
public enum TreeNodeFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Draw as selected.</summary>
    Selected = 1 << 0,
    /// <summary>Draw frame with background (e.g. for CollapsingHeader).</summary>
    Framed = 1 << 1,
    /// <summary>Hit testing will allow subsequent widgets to overlap this one.</summary>
    AllowOverlap = 1 << 2,
    /// <summary>Don't do a TreePush() when open (no extra indent nor pushing on ID stack).</summary>
    NoTreePushOnOpen = 1 << 3,
    /// <summary>Don't automatically and temporarily open node when Logging is active.</summary>
    NoAutoOpenOnLog = 1 << 4,
    /// <summary>Default node to be open.</summary>
    DefaultOpen = 1 << 5,
    /// <summary>Open on double-click instead of simple click.</summary>
    OpenOnDoubleClick = 1 << 6,
    /// <summary>Open when clicking on the arrow part.</summary>
    OpenOnArrow = 1 << 7,
    /// <summary>No collapsing, no arrow (use as a convenience for leaf nodes).</summary>
    Leaf = 1 << 8,
    /// <summary>Display a bullet instead of arrow.</summary>
    Bullet = 1 << 9,
    /// <summary>Use FramePadding to vertically align text baseline to regular widget height.</summary>
    FramePadding = 1 << 10,
    /// <summary>Extend hit box to the right-most edge, even if not framed.</summary>
    SpanAvailWidth = 1 << 11,
    /// <summary>Extend hit box to the left-most and right-most edges (cover the indent area).</summary>
    SpanFullWidth = 1 << 12,
    /// <summary>Narrow hit box + narrow hovering highlight, will only cover the label text.</summary>
    SpanLabelWidth = 1 << 13,
    /// <summary>Frame will span all columns of its container table.</summary>
    SpanAllColumns = 1 << 14,
    /// <summary>Label will span all columns of its container table.</summary>
    LabelSpanAllColumns = 1 << 15,
    /// <summary>Nav: left arrow moves back to parent.</summary>
    NavLeftJumpsToParent = 1 << 17,
    /// <summary>Shortcut combination: Framed | NoTreePushOnOpen | NoAutoOpenOnLog.</summary>
    CollapsingHeader = Framed | NoTreePushOnOpen | NoAutoOpenOnLog,
    /// <summary>No lines drawn.</summary>
    DrawLinesNone = 1 << 18,
    /// <summary>Horizontal lines to child nodes, vertical line drawn down to TreePop() position.</summary>
    DrawLinesFull = 1 << 19,
    /// <summary>Horizontal lines to child nodes, vertical line drawn down to bottom-most child node.</summary>
    DrawLinesToNodes = 1 << 20,
}
