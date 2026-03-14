namespace SdlSharp.Graphics;

/// <summary>
/// Window state flags.
/// </summary>
[Flags]
public enum WindowFlags : ulong
{
    /// <summary>No flags.</summary>
    None = 0,

    /// <summary>Window is in fullscreen mode.</summary>
    Fullscreen = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN,

    /// <summary>Window usable with OpenGL context.</summary>
    OpenGL = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_OPENGL,

    /// <summary>Window is occluded.</summary>
    Occluded = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_OCCLUDED,

    /// <summary>Window is hidden.</summary>
    Hidden = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_HIDDEN,

    /// <summary>No window decoration.</summary>
    Borderless = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_BORDERLESS,

    /// <summary>Window can be resized.</summary>
    Resizable = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_RESIZABLE,

    /// <summary>Window is minimized.</summary>
    Minimized = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_MINIMIZED,

    /// <summary>Window is maximized.</summary>
    Maximized = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED,

    /// <summary>Window has grabbed mouse input.</summary>
    MouseGrabbed = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_MOUSE_GRABBED,

    /// <summary>Window has input focus.</summary>
    InputFocus = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS,

    /// <summary>Window has mouse focus.</summary>
    MouseFocus = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS,

    /// <summary>Window not created by SDL.</summary>
    External = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_EXTERNAL,

    /// <summary>Window is modal.</summary>
    Modal = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_MODAL,

    /// <summary>Window uses high pixel density back buffer if possible.</summary>
    HighPixelDensity = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_HIGH_PIXEL_DENSITY,

    /// <summary>Window has mouse captured.</summary>
    MouseCapture = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_MOUSE_CAPTURE,

    /// <summary>Window has relative mode enabled.</summary>
    MouseRelativeMode = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_MOUSE_RELATIVE_MODE,

    /// <summary>Window should always be above others.</summary>
    AlwaysOnTop = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_ALWAYS_ON_TOP,

    /// <summary>Window should be treated as a utility window.</summary>
    Utility = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_UTILITY,

    /// <summary>Window should be treated as a tooltip.</summary>
    Tooltip = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_TOOLTIP,

    /// <summary>Window should be treated as a popup menu.</summary>
    PopupMenu = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_POPUP_MENU,

    /// <summary>Window has grabbed keyboard input.</summary>
    KeyboardGrabbed = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_KEYBOARD_GRABBED,

    /// <summary>Window is in fill-document mode (Emscripten only).</summary>
    FillDocument = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_FILL_DOCUMENT,

    /// <summary>Window usable for Vulkan surface.</summary>
    Vulkan = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_VULKAN,

    /// <summary>Window usable for Metal view.</summary>
    Metal = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_METAL,

    /// <summary>Window with transparent buffer.</summary>
    Transparent = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_TRANSPARENT,

    /// <summary>Window should not be focusable.</summary>
    NotFocusable = (ulong)Native.SDL_WindowFlags.SDL_WINDOW_NOT_FOCUSABLE,
}
