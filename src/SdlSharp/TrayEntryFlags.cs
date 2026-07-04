namespace SdlSharp;

/// <summary>
/// Flags controlling the kind and initial state of a tray menu entry.
/// Exactly one of <see cref="Button"/>, <see cref="Checkbox"/>, or <see cref="Submenu"/> is required.
/// </summary>
[Flags]
public enum TrayEntryFlags : uint
{
    /// <summary>A simple clickable button.</summary>
    Button = Native.Tray.SDL_TRAYENTRY_BUTTON,
    /// <summary>A checkbox entry.</summary>
    Checkbox = Native.Tray.SDL_TRAYENTRY_CHECKBOX,
    /// <summary>An entry that will host a submenu.</summary>
    Submenu = Native.Tray.SDL_TRAYENTRY_SUBMENU,
    /// <summary>Create the entry disabled.</summary>
    Disabled = Native.Tray.SDL_TRAYENTRY_DISABLED,
    /// <summary>Create a checkbox entry checked.</summary>
    Checked = Native.Tray.SDL_TRAYENTRY_CHECKED,
}
