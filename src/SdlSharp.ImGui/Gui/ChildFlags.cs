namespace SdlSharp.Gui;

/// <summary>Flags for <see cref="Gui.BeginChild"/>.</summary>
[Flags]
public enum ChildFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Show an outer border and enable WindowPadding.</summary>
    Borders = 1 << 0,
    /// <summary>Pad with style.WindowPadding even if no border are drawn.</summary>
    AlwaysUseWindowPadding = 1 << 1,
    /// <summary>Allow resize from right border (layout direction).</summary>
    ResizeX = 1 << 2,
    /// <summary>Allow resize from bottom border (layout direction).</summary>
    ResizeY = 1 << 3,
    /// <summary>Enable auto-resizing width.</summary>
    AutoResizeX = 1 << 4,
    /// <summary>Enable auto-resizing height.</summary>
    AutoResizeY = 1 << 5,
    /// <summary>Always measure size even when child is hidden, always return true, always disable clipping optimization.</summary>
    AlwaysAutoResize = 1 << 6,
    /// <summary>Style the child window like a framed item.</summary>
    FrameStyle = 1 << 7,
    /// <summary>[BETA] Share focus scope, allow keyboard/gamepad navigation to cross over parent border.</summary>
    NavFlattened = 1 << 8,
}
