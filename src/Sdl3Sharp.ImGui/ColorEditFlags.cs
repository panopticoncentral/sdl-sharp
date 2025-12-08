using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for ColorEdit3() / ColorEdit4() / ColorPicker3() / ColorPicker4() / ColorButton().
/// </summary>
[Flags]
public enum ColorEditFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiColorEditFlags.None,

    /// <summary>
    /// ColorEdit, ColorPicker, ColorButton: ignore Alpha component (will only read 3 components from the input pointer).
    /// </summary>
    NoAlpha = ImGuiColorEditFlags.NoAlpha,

    /// <summary>
    /// ColorEdit: disable picker when clicking on color square.
    /// </summary>
    NoPicker = ImGuiColorEditFlags.NoPicker,

    /// <summary>
    /// ColorEdit: disable toggling options menu when right-clicking on inputs/small preview.
    /// </summary>
    NoOptions = ImGuiColorEditFlags.NoOptions,

    /// <summary>
    /// ColorEdit, ColorPicker: disable color square preview next to the inputs. (e.g. to show only the inputs)
    /// </summary>
    NoSmallPreview = ImGuiColorEditFlags.NoSmallPreview,

    /// <summary>
    /// ColorEdit, ColorPicker: disable inputs sliders/text widgets (e.g. to show only the small preview color square).
    /// </summary>
    NoInputs = ImGuiColorEditFlags.NoInputs,

    /// <summary>
    /// ColorEdit, ColorPicker, ColorButton: disable tooltip when hovering the preview.
    /// </summary>
    NoTooltip = ImGuiColorEditFlags.NoTooltip,

    /// <summary>
    /// ColorEdit, ColorPicker: disable display of inline text label (the label is still forwarded to the tooltip and picker).
    /// </summary>
    NoLabel = ImGuiColorEditFlags.NoLabel,

    /// <summary>
    /// ColorPicker: disable bigger color preview on right side of the picker, use small color square preview instead.
    /// </summary>
    NoSidePreview = ImGuiColorEditFlags.NoSidePreview,

    /// <summary>
    /// ColorEdit: disable drag and drop target. ColorButton: disable drag and drop source.
    /// </summary>
    NoDragDrop = ImGuiColorEditFlags.NoDragDrop,

    /// <summary>
    /// ColorButton: disable border (which is enforced by default).
    /// </summary>
    NoBorder = ImGuiColorEditFlags.NoBorder,

    /// <summary>
    /// ColorEdit, ColorPicker, ColorButton: disable alpha in the preview. Contrary to NoAlpha it may still be edited when calling ColorEdit4()/ColorPicker4(). For ColorButton() this does the same as NoAlpha.
    /// </summary>
    AlphaOpaque = ImGuiColorEditFlags.AlphaOpaque,

    /// <summary>
    /// ColorEdit, ColorPicker, ColorButton: disable rendering a checkerboard background behind transparent color.
    /// </summary>
    AlphaNoBg = ImGuiColorEditFlags.AlphaNoBg,

    /// <summary>
    /// ColorEdit, ColorPicker, ColorButton: display half opaque / half transparent preview.
    /// </summary>
    AlphaPreviewHalf = ImGuiColorEditFlags.AlphaPreviewHalf,

    /// <summary>
    /// ColorEdit, ColorPicker: show vertical alpha bar/gradient in picker.
    /// </summary>
    AlphaBar = ImGuiColorEditFlags.AlphaBar,

    /// <summary>
    /// (WIP) ColorEdit: Currently only disable 0.0f..1.0f limits in RGBA edition (note: you probably want to use Float flag as well).
    /// </summary>
    HDR = ImGuiColorEditFlags.HDR,

    /// <summary>
    /// [Display] ColorEdit: override display type among RGB/HSV/Hex. ColorPicker: select any combination using one or more of RGB/HSV/Hex.
    /// </summary>
    DisplayRGB = ImGuiColorEditFlags.DisplayRGB,

    /// <summary>
    /// [Display] Display as HSV.
    /// </summary>
    DisplayHSV = ImGuiColorEditFlags.DisplayHSV,

    /// <summary>
    /// [Display] Display as Hex.
    /// </summary>
    DisplayHex = ImGuiColorEditFlags.DisplayHex,

    /// <summary>
    /// [DataType] ColorEdit, ColorPicker, ColorButton: display values formatted as 0..255.
    /// </summary>
    Uint8 = ImGuiColorEditFlags.Uint8,

    /// <summary>
    /// [DataType] ColorEdit, ColorPicker, ColorButton: display values formatted as 0.0f..1.0f floats instead of 0..255 integers. No round-trip of value via integers.
    /// </summary>
    Float = ImGuiColorEditFlags.Float,

    /// <summary>
    /// [Picker] ColorPicker: bar for Hue, rectangle for Sat/Value.
    /// </summary>
    PickerHueBar = ImGuiColorEditFlags.PickerHueBar,

    /// <summary>
    /// [Picker] ColorPicker: wheel for Hue, triangle for Sat/Value.
    /// </summary>
    PickerHueWheel = ImGuiColorEditFlags.PickerHueWheel,

    /// <summary>
    /// [Input] ColorEdit, ColorPicker: input and output data in RGB format.
    /// </summary>
    InputRGB = ImGuiColorEditFlags.InputRGB,

    /// <summary>
    /// [Input] ColorEdit, ColorPicker: input and output data in HSV format.
    /// </summary>
    InputHSV = ImGuiColorEditFlags.InputHSV
}
