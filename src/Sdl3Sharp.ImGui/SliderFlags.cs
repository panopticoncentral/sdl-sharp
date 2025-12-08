using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for DragFloat(), DragInt(), SliderFloat(), SliderInt() etc.
/// </summary>
/// <remarks>
/// We use the same sets of flags for DragXXX() and SliderXXX() functions as the features are the same and it makes it easier to swap them.
/// These are per-item flags. There is shared behavior flag too: <see cref="IO"/>.ConfigDragClickToInputText.
/// </remarks>
[Flags]
public enum SliderFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiSliderFlags.None,

    /// <summary>
    /// Make the widget logarithmic (linear otherwise). Consider using NoRoundToFormat with this if using a format-string with small amount of digits.
    /// </summary>
    Logarithmic = ImGuiSliderFlags.Logarithmic,

    /// <summary>
    /// Disable rounding underlying value to match precision of the display format string (e.g. %.3f values are rounded to those 3 digits).
    /// </summary>
    NoRoundToFormat = ImGuiSliderFlags.NoRoundToFormat,

    /// <summary>
    /// Disable Ctrl+Click or Enter key allowing to input text directly into the widget.
    /// </summary>
    NoInput = ImGuiSliderFlags.NoInput,

    /// <summary>
    /// Enable wrapping around from max to min and from min to max. Only supported by DragXXX() functions for now.
    /// </summary>
    WrapAround = ImGuiSliderFlags.WrapAround,

    /// <summary>
    /// Clamp value to min/max bounds when input manually with Ctrl+Click. By default Ctrl+Click allows going out of bounds.
    /// </summary>
    ClampOnInput = ImGuiSliderFlags.ClampOnInput,

    /// <summary>
    /// Clamp even if min==max==0.0f. Otherwise due to legacy reason DragXXX functions don't clamp with those values. When your clamping limits are dynamic you almost always want to use it.
    /// </summary>
    ClampZeroRange = ImGuiSliderFlags.ClampZeroRange,

    /// <summary>
    /// Disable keyboard modifiers altering tweak speed. Useful if you want to alter tweak speed yourself based on your own logic.
    /// </summary>
    NoSpeedTweaks = ImGuiSliderFlags.NoSpeedTweaks,

    /// <summary>
    /// Combination of ClampOnInput | ClampZeroRange.
    /// </summary>
    AlwaysClamp = ImGuiSliderFlags.AlwaysClamp
}
