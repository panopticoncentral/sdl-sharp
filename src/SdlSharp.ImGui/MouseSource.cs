namespace SdlSharp.Gui;

/// <summary>Source of a mouse-like pointer event (for <c>Io.AddMouseSourceEvent</c>).</summary>
public enum MouseSource
{
    /// <summary>Input from an actual mouse.</summary>
    Mouse = 0,
    /// <summary>Input from a touch screen (no hovering before initial press).</summary>
    TouchScreen,
    /// <summary>Input from a stylus / pressure-sensitive pen.</summary>
    Pen,
}
