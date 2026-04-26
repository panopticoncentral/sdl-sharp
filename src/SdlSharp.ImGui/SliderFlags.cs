namespace SdlSharp.ImGui;

/// <summary>Flags for Drag/Slider widgets (DragFloat, SliderFloat, etc.).</summary>
[Flags]
public enum SliderFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Make the widget logarithmic.</summary>
    Logarithmic = 1 << 5,
    /// <summary>Disable rounding underlying value to match precision of the display format string.</summary>
    NoRoundToFormat = 1 << 6,
    /// <summary>Disable Ctrl+Click or Enter key allowing to input text directly into the widget.</summary>
    NoInput = 1 << 7,
    /// <summary>Enable wrapping around from max to min and from min to max (Drag only).</summary>
    WrapAround = 1 << 8,
    /// <summary>Clamp value to min/max bounds when input manually with Ctrl+Click.</summary>
    ClampOnInput = 1 << 9,
    /// <summary>Clamp even if min==max==0.</summary>
    ClampZeroRange = 1 << 10,
    /// <summary>Disable keyboard modifiers altering tweak speed.</summary>
    NoSpeedTweaks = 1 << 11,
    /// <summary>DragScalarN/SliderScalarN only: draw R/G/B/A color markers on each component.</summary>
    ColorMarkers = 1 << 12,
    /// <summary>Combination: ClampOnInput | ClampZeroRange.</summary>
    AlwaysClamp = ClampOnInput | ClampZeroRange,
}
