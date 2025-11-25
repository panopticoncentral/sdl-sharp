namespace Sdl3Sharp;

/// <summary>
/// Flags that control the behavior of message box buttons.
/// </summary>
[Flags]
public enum MessageBoxButtonFlags : uint
{
    /// <summary>
    /// No special flags.
    /// </summary>
    None = 0,

    /// <summary>
    /// This button is the default when the Return key is pressed.
    /// </summary>
    ReturnKeyDefault = 0x00000001u,

    /// <summary>
    /// This button is the default when the Escape key is pressed.
    /// </summary>
    EscapeKeyDefault = 0x00000002u
}
