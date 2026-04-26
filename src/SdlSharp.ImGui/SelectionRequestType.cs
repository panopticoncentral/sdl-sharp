namespace SdlSharp.ImGui;

/// <summary>Type of selection request emitted during multi-select.</summary>
public enum SelectionRequestType
{
    /// <summary>No request.</summary>
    None = 0,
    /// <summary>Clear selection (Selected==false) or select all items (Selected==true).</summary>
    SetAll,
    /// <summary>Select/unselect items in the range [RangeFirstItem, RangeLastItem] inclusive.</summary>
    SetRange,
}
