using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>Sort specification for a single table column, obtained from <see cref="TableSortSpecs.GetSpec"/>.</summary>
public readonly unsafe struct TableColumnSortSpecs
{

    internal TableColumnSortSpecs(IGSharp_TableColumnSortSpecs* handle)
    {
        this = default;
        if (handle == null) return;
        IsValid = true;
        ColumnUserId = IGSharp_TableColumnSortSpecs_GetColumnUserID(handle);
        ColumnIndex = IGSharp_TableColumnSortSpecs_GetColumnIndex(handle);
        SortOrder = IGSharp_TableColumnSortSpecs_GetSortOrder(handle);
        SortDirection = (SortDirection)IGSharp_TableColumnSortSpecs_GetSortDirection(handle);
    }

    /// <summary>True if this handle refers to a valid spec.</summary>
    public bool IsValid { get; }

    /// <summary>User-supplied column ID (from <see cref="ImGui.TableSetupColumn"/>'s <c>userId</c> parameter).</summary>
    public uint ColumnUserId { get; }

    /// <summary>Zero-based column index within the table.</summary>
    public int ColumnIndex { get; }

    /// <summary>Sort order within the multi-column sort (0 is primary sort).</summary>
    public int SortOrder { get; }

    /// <summary>Ascending or descending.</summary>
    public SortDirection SortDirection { get; }
}
