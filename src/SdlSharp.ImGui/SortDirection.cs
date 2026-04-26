namespace SdlSharp.Gui;

/// <summary>A sorting direction.</summary>
public enum SortDirection : byte
{
    /// <summary>No sort direction.</summary>
    None = 0,
    /// <summary>Ascending (0→9, A→Z).</summary>
    Ascending = 1,
    /// <summary>Descending (9→0, Z→A).</summary>
    Descending = 2,
}
