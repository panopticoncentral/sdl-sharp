namespace SdlSharp.Graphics
{
    /// <summary>
    /// Flags for windows.
    /// </summary>
    [Flags]
    public enum WindowOptions
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Fullscreen window.
        /// </summary>
        Fullscreen = Native.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN,

        /// <summary>
        /// Window usable with OpenGL context.
        /// </summary>
        OpenGl = Native.SDL_WindowFlags.SDL_WINDOW_OPENGL,

        /// <summary>
        /// Window is visible.
        /// </summary>
        Shown = Native.SDL_WindowFlags.SDL_WINDOW_SHOWN,

        /// <summary>
        /// Window is not visible.
        /// </summary>
        Hidden = Native.SDL_WindowFlags.SDL_WINDOW_HIDDEN,

        /// <summary>
        /// Window has no border.
        /// </summary>
        Borderless = Native.SDL_WindowFlags.SDL_WINDOW_BORDERLESS,

        /// <summary>
        /// The window is resizable.
        /// </summary>
        Resizable = Native.SDL_WindowFlags.SDL_WINDOW_RESIZABLE,

        /// <summary>
        /// The window is minimized.
        /// </summary>
        Minimized = Native.SDL_WindowFlags.SDL_WINDOW_MINIMIZED,

        /// <summary>
        /// The window is maximized.
        /// </summary>
        Maximized = Native.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED,

        /// <summary>
        /// The window has grabbed mouse focus.
        /// </summary>
        MouseGrabbed = Native.SDL_WindowFlags.SDL_WINDOW_MOUSE_GRABBED,

        /// <summary>
        /// The window has input focus.
        /// </summary>
        InputFocus = Native.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS,

        /// <summary>
        /// The window has mouse focus.
        /// </summary>
        MouseFocus = Native.SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS,

        /// <summary>
        /// The window is relative to the desktop.
        /// </summary>
        FullScreenDesktop = Native.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP,

        /// <summary>
        /// The window was not created by SDL.
        /// </summary>
        Foreign = Native.SDL_WindowFlags.SDL_WINDOW_FOREIGN,

        /// <summary>
        /// High DPI is allowed on this window.
        /// </summary>
        AllowHighDpi = Native.SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI,

        /// <summary>
        /// Window has captured the mouse.
        /// </summary>
        MouseCapture = Native.SDL_WindowFlags.SDL_WINDOW_MOUSE_CAPTURE,

        /// <summary>
        /// The window is always on top.
        /// </summary>
        AlwaysOnTop = Native.SDL_WindowFlags.SDL_WINDOW_ALWAYS_ON_TOP,

        /// <summary>
        /// The window should not be added to the taskbar.
        /// </summary>
        SkipTaskbar = Native.SDL_WindowFlags.SDL_WINDOW_SKIP_TASKBAR,

        /// <summary>
        /// This is a utility window.
        /// </summary>
        Utility = Native.SDL_WindowFlags.SDL_WINDOW_UTILITY,

        /// <summary>
        /// This is a tooltip window.
        /// </summary>
        Tooltip = Native.SDL_WindowFlags.SDL_WINDOW_TOOLTIP,

        /// <summary>
        /// This is a popup menu window.
        /// </summary>
        PopupMenu = Native.SDL_WindowFlags.SDL_WINDOW_POPUP_MENU,

        /// <summary>
        /// Window has captured the keyboard.
        /// </summary>
        KeyboardGrabbed = Native.SDL_WindowFlags.SDL_WINDOW_KEYBOARD_GRABBED,

        /// <summary>
        /// The window is usable as a Vulkan surface.
        /// </summary>
        Vulkan = Native.SDL_WindowFlags.SDL_WINDOW_VULKAN,

        /// <summary>
        /// The window is usable as a Metal surface.
        /// </summary>
        Metal = Native.SDL_WindowFlags.SDL_WINDOW_METAL
    }
}
