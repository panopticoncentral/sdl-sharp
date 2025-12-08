using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for BeginTable().
/// </summary>
/// <remarks>
/// <para>Important! Sizing policies have complex and subtle side effects, much more so than you would expect.
/// Read comments/demos carefully + experiment with live demos to get acquainted with them.</para>
/// <para>The DEFAULT sizing policies are:</para>
/// <list type="bullet">
/// <item>Default to SizingFixedFit if ScrollX is on, or if host window has AlwaysAutoResize.</item>
/// <item>Default to SizingStretchSame if ScrollX is off.</item>
/// </list>
/// </remarks>
[Flags]
public enum TableFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiTableFlags.None,

    /// <summary>
    /// Enable resizing columns.
    /// </summary>
    Resizable = ImGuiTableFlags.Resizable,

    /// <summary>
    /// Enable reordering columns in header row (need calling TableSetupColumn() + TableHeadersRow() to display headers).
    /// </summary>
    Reorderable = ImGuiTableFlags.Reorderable,

    /// <summary>
    /// Enable hiding/disabling columns in context menu.
    /// </summary>
    Hideable = ImGuiTableFlags.Hideable,

    /// <summary>
    /// Enable sorting. Call TableGetSortSpecs() to obtain sort specs. Also see SortMulti and SortTristate.
    /// </summary>
    Sortable = ImGuiTableFlags.Sortable,

    /// <summary>
    /// Disable persisting columns order, width and sort settings in the .ini file.
    /// </summary>
    NoSavedSettings = ImGuiTableFlags.NoSavedSettings,

    /// <summary>
    /// Right-click on columns body/contents will display table context menu. By default it is available in TableHeadersRow().
    /// </summary>
    ContextMenuInBody = ImGuiTableFlags.ContextMenuInBody,

    /// <summary>
    /// Set each RowBg color with Col.TableRowBg or Col.TableRowBgAlt (equivalent of calling TableSetBgColor with RowBg0 on each row manually).
    /// </summary>
    RowBg = ImGuiTableFlags.RowBg,

    /// <summary>
    /// Draw horizontal borders between rows.
    /// </summary>
    BordersInnerH = ImGuiTableFlags.BordersInnerH,

    /// <summary>
    /// Draw horizontal borders at the top and bottom.
    /// </summary>
    BordersOuterH = ImGuiTableFlags.BordersOuterH,

    /// <summary>
    /// Draw vertical borders between columns.
    /// </summary>
    BordersInnerV = ImGuiTableFlags.BordersInnerV,

    /// <summary>
    /// Draw vertical borders on the left and right sides.
    /// </summary>
    BordersOuterV = ImGuiTableFlags.BordersOuterV,

    /// <summary>
    /// Draw horizontal borders.
    /// </summary>
    BordersH = ImGuiTableFlags.BordersH,

    /// <summary>
    /// Draw vertical borders.
    /// </summary>
    BordersV = ImGuiTableFlags.BordersV,

    /// <summary>
    /// Draw inner borders.
    /// </summary>
    BordersInner = ImGuiTableFlags.BordersInner,

    /// <summary>
    /// Draw outer borders.
    /// </summary>
    BordersOuter = ImGuiTableFlags.BordersOuter,

    /// <summary>
    /// Draw all borders.
    /// </summary>
    Borders = ImGuiTableFlags.Borders,

    /// <summary>
    /// [ALPHA] Disable vertical borders in columns Body (borders will always appear in Headers). May move to style.
    /// </summary>
    NoBordersInBody = ImGuiTableFlags.NoBordersInBody,

    /// <summary>
    /// [ALPHA] Disable vertical borders in columns Body until hovered for resize (borders will always appear in Headers). May move to style.
    /// </summary>
    NoBordersInBodyUntilResize = ImGuiTableFlags.NoBordersInBodyUntilResize,

    /// <summary>
    /// Columns default to WidthFixed or WidthAuto (if resizable or not resizable), matching contents width.
    /// </summary>
    SizingFixedFit = ImGuiTableFlags.SizingFixedFit,

    /// <summary>
    /// Columns default to WidthFixed or WidthAuto (if resizable or not resizable), matching the maximum contents width of all columns. Implicitly enable NoKeepColumnsVisible.
    /// </summary>
    SizingFixedSame = ImGuiTableFlags.SizingFixedSame,

    /// <summary>
    /// Columns default to WidthStretch with default weights proportional to each columns contents widths.
    /// </summary>
    SizingStretchProp = ImGuiTableFlags.SizingStretchProp,

    /// <summary>
    /// Columns default to WidthStretch with default weights all equal, unless overridden by TableSetupColumn().
    /// </summary>
    SizingStretchSame = ImGuiTableFlags.SizingStretchSame,

    /// <summary>
    /// Make outer width auto-fit to columns, overriding outer_size.x value. Only available when ScrollX/ScrollY are disabled and Stretch columns are not used.
    /// </summary>
    NoHostExtendX = ImGuiTableFlags.NoHostExtendX,

    /// <summary>
    /// Make outer height stop exactly at outer_size.y (prevent auto-extending table past the limit). Only available when ScrollX/ScrollY are disabled. Data below the limit will be clipped and not visible.
    /// </summary>
    NoHostExtendY = ImGuiTableFlags.NoHostExtendY,

    /// <summary>
    /// Disable keeping column always minimally visible when ScrollX is off and table gets too small. Not recommended if columns are resizable.
    /// </summary>
    NoKeepColumnsVisible = ImGuiTableFlags.NoKeepColumnsVisible,

    /// <summary>
    /// Disable distributing remainder width to stretched columns (width allocation on a 100-wide table with 3 columns: Without this flag: 33,33,34. With this flag: 33,33,33).
    /// </summary>
    PreciseWidths = ImGuiTableFlags.PreciseWidths,

    /// <summary>
    /// Disable clipping rectangle for every individual columns (reduce draw command count, items will be able to overflow into other columns). Generally incompatible with TableSetupScrollFreeze().
    /// </summary>
    NoClip = ImGuiTableFlags.NoClip,

    /// <summary>
    /// Default if BordersOuterV is on. Enable outermost padding. Generally desirable if you have headers.
    /// </summary>
    PadOuterX = ImGuiTableFlags.PadOuterX,

    /// <summary>
    /// Default if BordersOuterV is off. Disable outermost padding.
    /// </summary>
    NoPadOuterX = ImGuiTableFlags.NoPadOuterX,

    /// <summary>
    /// Disable inner padding between columns (double inner padding if BordersOuterV is on, single inner padding if BordersOuterV is off).
    /// </summary>
    NoPadInnerX = ImGuiTableFlags.NoPadInnerX,

    /// <summary>
    /// Enable horizontal scrolling. Require 'outer_size' parameter of BeginTable() to specify the container size. Changes default sizing policy.
    /// </summary>
    ScrollX = ImGuiTableFlags.ScrollX,

    /// <summary>
    /// Enable vertical scrolling. Require 'outer_size' parameter of BeginTable() to specify the container size.
    /// </summary>
    ScrollY = ImGuiTableFlags.ScrollY,

    /// <summary>
    /// Hold shift when clicking headers to sort on multiple column. TableGetSortSpecs() may return specs where (SpecsCount > 1).
    /// </summary>
    SortMulti = ImGuiTableFlags.SortMulti,

    /// <summary>
    /// Allow no sorting, disable default sorting. TableGetSortSpecs() may return specs where (SpecsCount == 0).
    /// </summary>
    SortTristate = ImGuiTableFlags.SortTristate,

    /// <summary>
    /// Highlight column headers when hovered (may evolve into a fuller highlight).
    /// </summary>
    HighlightHoveredColumn = ImGuiTableFlags.HighlightHoveredColumn
}
