using System;

namespace SdlSharp.Graphics
{
    [Flags]
    public enum WindowFlags
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// Fullscreen window.
        /// </summary>
        Fullscreen = 0x00000001,

        /// <summary>
        /// Window usable with OpenGL context.
        /// </summary>
        OpenGl = 0x00000002,

        /// <summary>
        /// Window is visible.
        /// </summary>
        Shown = 0x00000004,

        /// <summary>
        /// Window is not visible.
        /// </summary>
        Hidden = 0x00000008,

        /// <summary>
        /// Window has no border.
        /// </summary>
        Borderless = 0x00000010,

        /// <summary>
        /// The window is resizable.
        /// </summary>
        Resizable = 0x00000020,

        /// <summary>
        /// The window is minimized.
        /// </summary>
        Minimized = 0x00000040,

        /// <summary>
        /// The window is maximized.
        /// </summary>
        Maximized = 0x00000080,

        /// <summary>
        /// The window has grabbed input focus.
        /// </summary>
        InputGrabbed = 0x00000100,

        /// <summary>
        /// The window has input focus.
        /// </summary>
        InputFocus = 0x00000200,

        /// <summary>
        /// The window has mouse focus.
        /// </summary>
        MouseFocus = 0x00000400,

        /// <summary>
        /// The window was not created by SDL.
        /// </summary>
        Foreign = 0x00000800,

        /// <summary>
        /// The window is relative to the desktop.
        /// </summary>
        Desktop = 0x00001000,

        /// <summary>
        /// The window is fullscreen desktop.
        /// </summary>
        FullscreenDesktop = Fullscreen | Desktop,

        /// <summary>
        /// High DPI is allowed on this window.
        /// </summary>
        AllowHighDpi = 0x00002000,

        /// <summary>
        /// Window has captured the mouse.
        /// </summary>
        MouseCapture = 0x00004000,

        /// <summary>
        /// The window is always on top.
        /// </summary>
        AlwaysOnTop = 0x00008000,

        /// <summary>
        /// The window should not be added to the taskbar.
        /// </summary>
        SkipTaskbar = 0x00010000,

        /// <summary>
        /// This is a utility window.
        /// </summary>
        Utility = 0x00020000,

        /// <summary>
        /// This is a tooltip window.
        /// </summary>
        Tooltip = 0x00040000,

        /// <summary>
        /// This is a popup menu window.
        /// </summary>
        PopupMenu = 0x00080000,

        /// <summary>
        /// The window is usable as a Vulkan surface.
        /// </summary>
        Vulkan = 0x10000000
    }
}
