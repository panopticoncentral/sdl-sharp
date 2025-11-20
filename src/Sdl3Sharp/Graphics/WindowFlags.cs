namespace Sdl3Sharp.Graphics;

/// <summary>
/// The flags on a window.
/// </summary>
[Flags]
public enum WindowFlags : ulong
{
    /// <summary>No flags.</summary>
    None = 0,
    /// <summary>Window is in fullscreen mode.</summary>
    Fullscreen = 0x0000000000000001UL,
    /// <summary>Window usable with OpenGL context.</summary>
    OpenGL = 0x0000000000000002UL,
    /// <summary>Window is occluded.</summary>
    Occluded = 0x0000000000000004UL,
    /// <summary>Window is neither mapped onto the desktop nor shown in the taskbar/dock/window list; Show() is required for it to become visible.</summary>
    Hidden = 0x0000000000000008UL,
    /// <summary>No window decoration.</summary>
    Borderless = 0x0000000000000010UL,
    /// <summary>Window can be resized.</summary>
    Resizable = 0x0000000000000020UL,
    /// <summary>Window is minimized.</summary>
    Minimized = 0x0000000000000040UL,
    /// <summary>Window is maximized.</summary>
    Maximized = 0x0000000000000080UL,
    /// <summary>Window has grabbed mouse input.</summary>
    MouseGrabbed = 0x0000000000000100UL,
    /// <summary>Window has input focus.</summary>
    InputFocus = 0x0000000000000200UL,
    /// <summary>Window has mouse focus.</summary>
    MouseFocus = 0x0000000000000400UL,
    /// <summary>Window not created by SDL.</summary>
    External = 0x0000000000000800UL,
    /// <summary>Window is modal.</summary>
    Modal = 0x0000000000001000UL,
    /// <summary>Window uses high pixel density back buffer if possible.</summary>
    HighPixelDensity = 0x0000000000002000UL,
    /// <summary>Window has mouse captured (unrelated to MouseGrabbed).</summary>
    MouseCapture = 0x0000000000004000UL,
    /// <summary>Window has relative mode enabled.</summary>
    MouseRelativeMode = 0x0000000000008000UL,
    /// <summary>Window should always be above others.</summary>
    AlwaysOnTop = 0x0000000000010000UL,
    /// <summary>Window should be treated as a utility window, not showing in the task bar and window list.</summary>
    Utility = 0x0000000000020000UL,
    /// <summary>Window should be treated as a tooltip and does not get mouse or keyboard focus, requires a parent window.</summary>
    Tooltip = 0x0000000000040000UL,
    /// <summary>Window should be treated as a popup menu, requires a parent window.</summary>
    PopupMenu = 0x0000000000080000UL,
    /// <summary>Window has grabbed keyboard input.</summary>
    KeyboardGrabbed = 0x0000000000100000UL,
    /// <summary>Window usable for Vulkan surface.</summary>
    Vulkan = 0x0000000010000000UL,
    /// <summary>Window usable for Metal view.</summary>
    Metal = 0x0000000020000000UL,
    /// <summary>Window with transparent buffer.</summary>
    Transparent = 0x0000000040000000UL,
    /// <summary>Window should not be focusable.</summary>
    NotFocusable = 0x0000000080000000UL
}
