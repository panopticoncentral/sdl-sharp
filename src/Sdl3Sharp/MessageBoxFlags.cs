namespace Sdl3Sharp;

/// <summary>
/// Flags that control the appearance and behavior of message boxes.
/// </summary>
[Flags]
public enum MessageBoxFlags : uint
{
    /// <summary>
    /// No special flags.
    /// </summary>
    None = 0,

    /// <summary>
    /// Display an error icon.
    /// </summary>
    Error = 0x00000010u,

    /// <summary>
    /// Display a warning icon.
    /// </summary>
    Warning = 0x00000020u,

    /// <summary>
    /// Display an informational icon.
    /// </summary>
    Information = 0x00000040u,

    /// <summary>
    /// Buttons are placed left to right.
    /// </summary>
    ButtonsLeftToRight = 0x00000080u,

    /// <summary>
    /// Buttons are placed right to left.
    /// </summary>
    ButtonsRightToLeft = 0x00000100u
}
