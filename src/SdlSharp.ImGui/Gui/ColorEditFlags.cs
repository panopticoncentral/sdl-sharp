namespace SdlSharp.Gui;

/// <summary>Flags for ColorEdit3/4, ColorPicker3/4, ColorButton.</summary>
[Flags]
public enum ColorEditFlags
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Ignore alpha component (will only read 3 components from the input pointer).</summary>
    NoAlpha = 1 << 1,
    /// <summary>ColorEdit: disable picker when clicking on color square.</summary>
    NoPicker = 1 << 2,
    /// <summary>ColorEdit: disable toggling options menu when right-clicking.</summary>
    NoOptions = 1 << 3,
    /// <summary>Disable color square preview next to the inputs.</summary>
    NoSmallPreview = 1 << 4,
    /// <summary>Disable inputs sliders/text widgets.</summary>
    NoInputs = 1 << 5,
    /// <summary>Disable tooltip when hovering the preview.</summary>
    NoTooltip = 1 << 6,
    /// <summary>Disable display of inline text label.</summary>
    NoLabel = 1 << 7,
    /// <summary>ColorPicker: disable bigger color preview on right side of the picker.</summary>
    NoSidePreview = 1 << 8,
    /// <summary>ColorEdit: disable drag and drop target/source.</summary>
    NoDragDrop = 1 << 9,
    /// <summary>ColorButton: disable border.</summary>
    NoBorder = 1 << 10,
    /// <summary>ColorEdit: disable rendering R/G/B/A color marker.</summary>
    NoColorMarkers = 1 << 11,
    /// <summary>Disable alpha in the preview.</summary>
    AlphaOpaque = 1 << 12,
    /// <summary>Disable rendering a checkerboard background behind transparent color.</summary>
    AlphaNoBg = 1 << 13,
    /// <summary>Display half opaque / half transparent preview.</summary>
    AlphaPreviewHalf = 1 << 14,
    /// <summary>ColorEdit, ColorPicker: show vertical alpha bar/gradient in picker.</summary>
    AlphaBar = 1 << 18,
    /// <summary>ColorEdit: disable 0.0..1.0 limits in RGBA edition.</summary>
    HDR = 1 << 19,
    /// <summary>ColorEdit: display as RGB.</summary>
    DisplayRGB = 1 << 20,
    /// <summary>ColorEdit: display as HSV.</summary>
    DisplayHSV = 1 << 21,
    /// <summary>ColorEdit: display as Hex.</summary>
    DisplayHex = 1 << 22,
    /// <summary>Display values formatted as 0..255.</summary>
    Uint8 = 1 << 23,
    /// <summary>Display values formatted as 0.0..1.0 floats.</summary>
    Float = 1 << 24,
    /// <summary>ColorPicker: bar for Hue, rectangle for Sat/Value.</summary>
    PickerHueBar = 1 << 25,
    /// <summary>ColorPicker: wheel for Hue, triangle for Sat/Value.</summary>
    PickerHueWheel = 1 << 26,
    /// <summary>Input and output data in RGB format.</summary>
    InputRGB = 1 << 27,
    /// <summary>Input and output data in HSV format.</summary>
    InputHSV = 1 << 28,
    /// <summary>Default options combination.</summary>
    DefaultOptions = Uint8 | DisplayRGB | InputRGB | PickerHueBar,
}
