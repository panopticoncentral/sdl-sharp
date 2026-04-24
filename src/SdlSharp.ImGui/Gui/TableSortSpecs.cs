using static SdlSharp.ImGui.Native.ImGui;

namespace SdlSharp.Gui;

/// <summary>
/// Sort specs for a sortable table (<see cref="TableFlags.Sortable"/>). Obtain via
/// <see cref="Gui.TableGetSortSpecs"/>. When <see cref="SpecsDirty"/> is true, re-sort your
/// data and clear the flag.
/// </summary>
public readonly unsafe struct TableSortSpecs
{
    internal readonly void* Handle;

    internal TableSortSpecs(void* handle) => Handle = handle;

    /// <summary>True if the table currently has sort specs (false outside a sortable table).</summary>
    public bool IsValid => Handle != null;

    /// <summary>Number of columns currently contributing to the sort (usually 1; &gt; 1 with <see cref="TableFlags.SortMulti"/>).</summary>
    public int SpecsCount => IGSharp_TableSortSpecs_GetSpecsCount(Handle);

    /// <summary>Gets the per-column spec at the given index.</summary>
    public TableColumnSortSpecs GetSpec(int index) => new(IGSharp_TableSortSpecs_GetSpec(Handle, index));

    /// <summary>
    /// True when the sort specs changed since last read. Re-sort your data and set to false.
    /// Leaving this true causes ImGui to keep setting it every frame.
    /// </summary>
    public bool SpecsDirty
    {
        get => IGSharp_TableSortSpecs_GetSpecsDirty(Handle);
        set => IGSharp_TableSortSpecs_SetSpecsDirty(Handle, value);
    }
}
