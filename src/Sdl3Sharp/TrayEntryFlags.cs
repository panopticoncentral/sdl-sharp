namespace Sdl3Sharp;

/// <summary>
/// Flags that control the creation of system tray entries.
/// </summary>
/// <remarks>
/// Some of these flags are required; exactly one of them must be specified at
/// the time a tray entry is created. Other flags are optional; zero or more of
/// those can be OR'ed together with the required flag.
/// </remarks>
[Flags]
public enum TrayEntryFlags : uint
{
    /// <summary>
    /// Make the entry a simple button. Required.
    /// </summary>
    Button = 0x00000001u,

    /// <summary>
    /// Make the entry a checkbox. Required.
    /// </summary>
    Checkbox = 0x00000002u,

    /// <summary>
    /// Prepare the entry to have a submenu. Required.
    /// </summary>
    Submenu = 0x00000004u,

    /// <summary>
    /// Make the entry disabled. Optional.
    /// </summary>
    Disabled = 0x80000000u,

    /// <summary>
    /// Make the entry checked. This is valid only for checkboxes. Optional.
    /// </summary>
    Checked = 0x40000000u
}
