using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Enum for TableSetBgColor().
/// </summary>
/// <remarks>
/// <para>Background colors are rendering in 3 layers:</para>
/// <list type="bullet">
/// <item>Layer 0: draw with RowBg0 color if set, otherwise draw with ColumnBg0 if set.</item>
/// <item>Layer 1: draw with RowBg1 color if set, otherwise draw with ColumnBg1 if set.</item>
/// <item>Layer 2: draw with CellBg color if set.</item>
/// </list>
/// <para>The purpose of the two row/columns layers is to let you decide if a background color change should override or blend with the existing color.
/// When using TableFlags.RowBg on the table, each row has the RowBg0 color automatically set for odd/even rows.
/// If you set the color of RowBg0 target, your color will override the existing RowBg0 color.
/// If you set the color of RowBg1 or ColumnBg1 target, your color will blend over the RowBg0 color.</para>
/// </remarks>
public enum TableBgTarget
{
    /// <summary>
    /// No target.
    /// </summary>
    None = ImGuiTableBgTarget.None,

    /// <summary>
    /// Set row background color 0 (generally used for background, automatically set when TableFlags.RowBg is used).
    /// </summary>
    RowBg0 = ImGuiTableBgTarget.RowBg0,

    /// <summary>
    /// Set row background color 1 (generally used for selection marking).
    /// </summary>
    RowBg1 = ImGuiTableBgTarget.RowBg1,

    /// <summary>
    /// Set cell background color (top-most color).
    /// </summary>
    CellBg = ImGuiTableBgTarget.CellBg
}
