using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>Sort specification for a single table column, obtained from <see cref="TableSortSpecs.GetSpec"/>.</summary>
public readonly unsafe struct TableColumnSortSpecs
{
    internal readonly void* Handle;

    internal TableColumnSortSpecs(void* handle) => Handle = handle;

    /// <summary>True if this handle refers to a valid spec.</summary>
    public bool IsValid => Handle != null;

    /// <summary>User-supplied column ID (from <see cref="ImGui.TableSetupColumn"/>'s <c>userId</c> parameter).</summary>
    public uint ColumnUserId => IGSharp_TableColumnSortSpecs_GetColumnUserID(Handle);

    /// <summary>Zero-based column index within the table.</summary>
    public int ColumnIndex => IGSharp_TableColumnSortSpecs_GetColumnIndex(Handle);

    /// <summary>Sort order within the multi-column sort (0 is primary sort).</summary>
    public int SortOrder => IGSharp_TableColumnSortSpecs_GetSortOrder(Handle);

    /// <summary>Ascending or descending.</summary>
    public SortDirection SortDirection => (SortDirection)IGSharp_TableColumnSortSpecs_GetSortDirection(Handle);
}
