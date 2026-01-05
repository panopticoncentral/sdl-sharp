using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for BeginCombo().
/// </summary>
[Flags]
public enum ComboFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiComboFlags.None,

    /// <summary>
    /// Align the popup toward the left by default.
    /// </summary>
    PopupAlignLeft = ImGuiComboFlags.PopupAlignLeft,

    /// <summary>
    /// Max ~4 items visible. Tip: If you want your combo popup to be a specific size you can use SetNextWindowSizeConstraints() prior to calling BeginCombo().
    /// </summary>
    HeightSmall = ImGuiComboFlags.HeightSmall,

    /// <summary>
    /// Max ~8 items visible (default).
    /// </summary>
    HeightRegular = ImGuiComboFlags.HeightRegular,

    /// <summary>
    /// Max ~20 items visible.
    /// </summary>
    HeightLarge = ImGuiComboFlags.HeightLarge,

    /// <summary>
    /// As many fitting items as possible.
    /// </summary>
    HeightLargest = ImGuiComboFlags.HeightLargest,

    /// <summary>
    /// Display on the preview box without the square arrow button.
    /// </summary>
    NoArrowButton = ImGuiComboFlags.NoArrowButton,

    /// <summary>
    /// Display only a square arrow button.
    /// </summary>
    NoPreview = ImGuiComboFlags.NoPreview,

    /// <summary>
    /// Width dynamically calculated from preview contents.
    /// </summary>
    WidthFitPreview = ImGuiComboFlags.WidthFitPreview,

    /// <summary>
    /// A bitmask that includes all defined height values.
    /// </summary>
    /// <remarks>This value can be used to select or filter all height-related flags in bitwise
    /// operations.</remarks>
    HeightMask = HeightSmall | HeightRegular | HeightLarge | HeightLargest,
}
