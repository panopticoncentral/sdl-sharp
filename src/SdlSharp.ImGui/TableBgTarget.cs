namespace SdlSharp.ImGui;

/// <summary>Target layer for <see cref="ImGui.TableSetBgColor"/>.</summary>
public enum TableBgTarget
{
    /// <summary>No target.</summary>
    None = 0,
    /// <summary>Row background, layer 0 (auto-set when <see cref="TableFlags.RowBg"/> is on).</summary>
    RowBg0 = 1,
    /// <summary>Row background, layer 1 (typically used for selection highlight).</summary>
    RowBg1 = 2,
    /// <summary>Cell background (top-most layer).</summary>
    CellBg = 3,
}
