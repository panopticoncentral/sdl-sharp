using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Pixels;
using static Sdl3Sharp.Native.Properties;
using static Sdl3Sharp.Native.Rect;
using static Sdl3Sharp.Native.Surface;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_video.h - Video subsystem for window management and display handling.
/// </summary>
public static unsafe partial class Video
{
    /// <summary>
    /// This is a unique ID for a display for the time it is connected to the system.
    /// The value 0 is an invalid ID.
    /// </summary>
    /// <param name="value">The display ID value.</param>
    public readonly struct SDL_DisplayID(uint value)
    {
        /// <summary>The underlying display ID value.</summary>
        public readonly uint Value = value;

        /// <summary>Implicitly converts an SDL_DisplayID to uint.</summary>
        /// <param name="id">The display ID to convert.</param>
        public static implicit operator uint(SDL_DisplayID id)
        {
            return id.Value;
        }

        /// <summary>Implicitly converts a uint to SDL_DisplayID.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_DisplayID(uint value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// This is a unique ID for a window. The value 0 is an invalid ID.
    /// </summary>
    /// <remarks>Creates a new SDL_WindowID from a uint value.</remarks>
    /// <param name="value">The window ID value.</param>
    public readonly struct SDL_WindowID(uint value)
    {
        /// <summary>The underlying window ID value.</summary>
        public readonly uint Value = value;

        /// <summary>Implicitly converts an SDL_WindowID to uint.</summary>
        /// <param name="id">The window ID to convert.</param>
        public static implicit operator uint(SDL_WindowID id)
        {
            return id.Value;
        }

        /// <summary>Implicitly converts a uint to SDL_WindowID.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_WindowID(uint value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// The pointer to the global wl_display object used by the Wayland video backend.
    /// </summary>
    public const string SDL_PROP_GLOBAL_VIDEO_WAYLAND_WL_DISPLAY_POINTER = "SDL.video.wayland.wl_display";

    /// <summary>
    /// System theme.
    /// </summary>
    public enum SDL_SystemTheme
    {
        /// <summary>Unknown system theme.</summary>
        SDL_SYSTEM_THEME_UNKNOWN,
        /// <summary>Light colored system theme.</summary>
        SDL_SYSTEM_THEME_LIGHT,
        /// <summary>Dark colored system theme.</summary>
        SDL_SYSTEM_THEME_DARK
    }

    /// <summary>
    /// Internal display mode data (opaque).
    /// </summary>
    public struct SDL_DisplayModeData { }

    /// <summary>
    /// The structure that defines a display mode.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_DisplayMode
    {
        /// <summary>The display this mode is associated with.</summary>
        public SDL_DisplayID displayID;
        /// <summary>Pixel format.</summary>
        public SDL_PixelFormat format;
        /// <summary>Width in pixels.</summary>
        public int w;
        /// <summary>Height in pixels.</summary>
        public int h;
        /// <summary>Scale converting size to pixels (e.g. a 1920x1080 mode with 2.0 scale would have 3840x2160 pixels).</summary>
        public float pixel_density;
        /// <summary>Refresh rate (or 0.0f for unspecified).</summary>
        public float refresh_rate;
        /// <summary>Precise refresh rate numerator (or 0 for unspecified).</summary>
        public int refresh_rate_numerator;
        /// <summary>Precise refresh rate denominator.</summary>
        public int refresh_rate_denominator;
        /// <summary>Private internal data.</summary>
        public SDL_DisplayModeData* @internal;
    }

    /// <summary>
    /// Display orientation values; the way a display is rotated.
    /// </summary>
    public enum SDL_DisplayOrientation
    {
        /// <summary>The display orientation can't be determined.</summary>
        SDL_ORIENTATION_UNKNOWN,
        /// <summary>The display is in landscape mode, with the right side up, relative to portrait mode.</summary>
        SDL_ORIENTATION_LANDSCAPE,
        /// <summary>The display is in landscape mode, with the left side up, relative to portrait mode.</summary>
        SDL_ORIENTATION_LANDSCAPE_FLIPPED,
        /// <summary>The display is in portrait mode.</summary>
        SDL_ORIENTATION_PORTRAIT,
        /// <summary>The display is in portrait mode, upside down.</summary>
        SDL_ORIENTATION_PORTRAIT_FLIPPED
    }

    /// <summary>
    /// The struct used as an opaque handle to a window.
    /// </summary>
    public struct SDL_Window { }

    /// <summary>
    /// The flags on a window.
    /// </summary>
    [Flags]
    public enum SDL_WindowFlags : ulong
    {
        /// <summary>Window is in fullscreen mode.</summary>
        SDL_WINDOW_FULLSCREEN = 0x0000000000000001UL,
        /// <summary>Window usable with OpenGL context.</summary>
        SDL_WINDOW_OPENGL = 0x0000000000000002UL,
        /// <summary>Window is occluded.</summary>
        SDL_WINDOW_OCCLUDED = 0x0000000000000004UL,
        /// <summary>Window is neither mapped onto the desktop nor shown in the taskbar/dock/window list; SDL_ShowWindow() is required for it to become visible.</summary>
        SDL_WINDOW_HIDDEN = 0x0000000000000008UL,
        /// <summary>No window decoration.</summary>
        SDL_WINDOW_BORDERLESS = 0x0000000000000010UL,
        /// <summary>Window can be resized.</summary>
        SDL_WINDOW_RESIZABLE = 0x0000000000000020UL,
        /// <summary>Window is minimized.</summary>
        SDL_WINDOW_MINIMIZED = 0x0000000000000040UL,
        /// <summary>Window is maximized.</summary>
        SDL_WINDOW_MAXIMIZED = 0x0000000000000080UL,
        /// <summary>Window has grabbed mouse input.</summary>
        SDL_WINDOW_MOUSE_GRABBED = 0x0000000000000100UL,
        /// <summary>Window has input focus.</summary>
        SDL_WINDOW_INPUT_FOCUS = 0x0000000000000200UL,
        /// <summary>Window has mouse focus.</summary>
        SDL_WINDOW_MOUSE_FOCUS = 0x0000000000000400UL,
        /// <summary>Window not created by SDL.</summary>
        SDL_WINDOW_EXTERNAL = 0x0000000000000800UL,
        /// <summary>Window is modal.</summary>
        SDL_WINDOW_MODAL = 0x0000000000001000UL,
        /// <summary>Window uses high pixel density back buffer if possible.</summary>
        SDL_WINDOW_HIGH_PIXEL_DENSITY = 0x0000000000002000UL,
        /// <summary>Window has mouse captured (unrelated to MOUSE_GRABBED).</summary>
        SDL_WINDOW_MOUSE_CAPTURE = 0x0000000000004000UL,
        /// <summary>Window has relative mode enabled.</summary>
        SDL_WINDOW_MOUSE_RELATIVE_MODE = 0x0000000000008000UL,
        /// <summary>Window should always be above others.</summary>
        SDL_WINDOW_ALWAYS_ON_TOP = 0x0000000000010000UL,
        /// <summary>Window should be treated as a utility window, not showing in the task bar and window list.</summary>
        SDL_WINDOW_UTILITY = 0x0000000000020000UL,
        /// <summary>Window should be treated as a tooltip and does not get mouse or keyboard focus, requires a parent window.</summary>
        SDL_WINDOW_TOOLTIP = 0x0000000000040000UL,
        /// <summary>Window should be treated as a popup menu, requires a parent window.</summary>
        SDL_WINDOW_POPUP_MENU = 0x0000000000080000UL,
        /// <summary>Window has grabbed keyboard input.</summary>
        SDL_WINDOW_KEYBOARD_GRABBED = 0x0000000000100000UL,
        /// <summary>Window usable for Vulkan surface.</summary>
        SDL_WINDOW_VULKAN = 0x0000000010000000UL,
        /// <summary>Window usable for Metal view.</summary>
        SDL_WINDOW_METAL = 0x0000000020000000UL,
        /// <summary>Window with transparent buffer.</summary>
        SDL_WINDOW_TRANSPARENT = 0x0000000040000000UL,
        /// <summary>Window should not be focusable.</summary>
        SDL_WINDOW_NOT_FOCUSABLE = 0x0000000080000000UL
    }

    /// <summary>
    /// A magic value used with SDL_WINDOWPOS_UNDEFINED.
    /// </summary>
    public const uint SDL_WINDOWPOS_UNDEFINED_MASK = 0x1FFF0000u;

    /// <summary>
    /// Used to indicate that you don't care what the window position is on the specified display.
    /// </summary>
    /// <param name="X">The SDL_DisplayID of the display to use.</param>
    /// <returns>The window position value.</returns>
    public static int SDL_WINDOWPOS_UNDEFINED_DISPLAY(SDL_DisplayID X)
    {
        return (int)(SDL_WINDOWPOS_UNDEFINED_MASK | X);
    }

    /// <summary>
    /// Used to indicate that you don't care what the window position is (uses primary display).
    /// </summary>
    public static readonly int SDL_WINDOWPOS_UNDEFINED = SDL_WINDOWPOS_UNDEFINED_DISPLAY(0);

    /// <summary>
    /// Test if the window position is marked as "undefined."
    /// </summary>
    /// <param name="X">The window position value.</param>
    /// <returns>True if the position is undefined.</returns>
    public static bool SDL_WINDOWPOS_ISUNDEFINED(int X)
    {
        return (X & 0xFFFF0000) == SDL_WINDOWPOS_UNDEFINED_MASK;
    }

    /// <summary>
    /// A magic value used with SDL_WINDOWPOS_CENTERED.
    /// </summary>
    public const uint SDL_WINDOWPOS_CENTERED_MASK = 0x2FFF0000u;

    /// <summary>
    /// Used to indicate that the window position should be centered on the specified display.
    /// </summary>
    /// <param name="X">The SDL_DisplayID of the display to use.</param>
    /// <returns>The window position value.</returns>
    public static int SDL_WINDOWPOS_CENTERED_DISPLAY(SDL_DisplayID X)
    {
        return (int)(SDL_WINDOWPOS_CENTERED_MASK | X);
    }

    /// <summary>
    /// Used to indicate that the window position should be centered (uses primary display).
    /// </summary>
    public static readonly int SDL_WINDOWPOS_CENTERED = SDL_WINDOWPOS_CENTERED_DISPLAY(0);

    /// <summary>
    /// Test if the window position is marked as "centered."
    /// </summary>
    /// <param name="X">The window position value.</param>
    /// <returns>True if the position is centered.</returns>
    public static bool SDL_WINDOWPOS_ISCENTERED(int X)
    {
        return (X & 0xFFFF0000) == SDL_WINDOWPOS_CENTERED_MASK;
    }

    /// <summary>
    /// Window flash operation.
    /// </summary>
    public enum SDL_FlashOperation
    {
        /// <summary>Cancel any window flash state.</summary>
        SDL_FLASH_CANCEL,
        /// <summary>Flash the window briefly to get attention.</summary>
        SDL_FLASH_BRIEFLY,
        /// <summary>Flash the window until it gets focus.</summary>
        SDL_FLASH_UNTIL_FOCUSED
    }

    /// <summary>
    /// An opaque handle to an OpenGL context.
    /// </summary>
    public struct SDL_GLContextState { }

    /// <summary>
    /// An opaque handle to an OpenGL context.
    /// </summary>
    /// <remarks>Creates a new SDL_GLContext from a pointer value.</remarks>
    /// <param name="value">The context pointer value.</param>
    public readonly struct SDL_GLContext(SDL_GLContextState* value)
    {
        /// <summary>The underlying OpenGL context pointer.</summary>
        public readonly SDL_GLContextState* Value = value;

        /// <summary>Implicitly converts an SDL_GLContext to a pointer.</summary>
        /// <param name="context">The context to convert.</param>
        public static implicit operator SDL_GLContextState*(SDL_GLContext context)
        {
            return context.Value;
        }

        /// <summary>Implicitly converts a pointer to SDL_GLContext.</summary>
        /// <param name="value">The pointer value to convert.</param>
        public static implicit operator SDL_GLContext(SDL_GLContextState* value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// Opaque type for an EGL display.
    /// </summary>
    /// <remarks>Creates a new SDL_EGLDisplay from a pointer value.</remarks>
    /// <param name="value">The display pointer value.</param>
    public readonly struct SDL_EGLDisplay(void* value)
    {
        /// <summary>The underlying EGL display pointer.</summary>
        public readonly void* Value = value;

        /// <summary>Implicitly converts an SDL_EGLDisplay to a void pointer.</summary>
        /// <param name="display">The display to convert.</param>
        public static implicit operator void*(SDL_EGLDisplay display)
        {
            return display.Value;
        }

        /// <summary>Implicitly converts a void pointer to SDL_EGLDisplay.</summary>
        /// <param name="value">The pointer value to convert.</param>
        public static implicit operator SDL_EGLDisplay(void* value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// Opaque type for an EGL config.
    /// </summary>
    /// <remarks>Creates a new SDL_EGLConfig from a pointer value.</remarks>
    /// <param name="value">The config pointer value.</param>
    public readonly struct SDL_EGLConfig(void* value)
    {
        /// <summary>The underlying EGL config pointer.</summary>
        public readonly void* Value = value;

        /// <summary>Implicitly converts an SDL_EGLConfig to a void pointer.</summary>
        /// <param name="config">The config to convert.</param>
        public static implicit operator void*(SDL_EGLConfig config)
        {
            return config.Value;
        }

        /// <summary>Implicitly converts a void pointer to SDL_EGLConfig.</summary>
        /// <param name="value">The pointer value to convert.</param>
        public static implicit operator SDL_EGLConfig(void* value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// Opaque type for an EGL surface.
    /// </summary>
    /// <remarks>Creates a new SDL_EGLSurface from a pointer value.</remarks>
    /// <param name="value">The surface pointer value.</param>
    public readonly struct SDL_EGLSurface(void* value)
    {
        /// <summary>The underlying EGL surface pointer.</summary>
        public readonly void* Value = value;

        /// <summary>Implicitly converts an SDL_EGLSurface to a void pointer.</summary>
        /// <param name="surface">The surface to convert.</param>
        public static implicit operator void*(SDL_EGLSurface surface)
        {
            return surface.Value;
        }

        /// <summary>Implicitly converts a void pointer to SDL_EGLSurface.</summary>
        /// <param name="value">The pointer value to convert.</param>
        public static implicit operator SDL_EGLSurface(void* value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// An EGL attribute, used when creating an EGL context.
    /// </summary>
    /// <remarks>Creates a new SDL_EGLAttrib from an nint value.</remarks>
    /// <param name="value">The attribute value.</param>
    public readonly struct SDL_EGLAttrib(nint value)
    {
        /// <summary>The underlying EGL attribute value.</summary>
        public readonly nint Value = value;

        /// <summary>Implicitly converts an SDL_EGLAttrib to nint.</summary>
        /// <param name="attrib">The attribute to convert.</param>
        public static implicit operator nint(SDL_EGLAttrib attrib)
        {
            return attrib.Value;
        }

        /// <summary>Implicitly converts an nint to SDL_EGLAttrib.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_EGLAttrib(nint value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// An EGL integer attribute, used when creating an EGL surface.
    /// </summary>
    /// <remarks>Creates a new SDL_EGLint from an int value.</remarks>
    /// <param name="value">The integer value.</param>
    public readonly struct SDL_EGLint(int value)
    {
        /// <summary>The underlying EGL integer value.</summary>
        public readonly int Value = value;

        /// <summary>Implicitly converts an SDL_EGLint to int.</summary>
        /// <param name="eglInt">The EGL integer to convert.</param>
        public static implicit operator int(SDL_EGLint eglInt)
        {
            return eglInt.Value;
        }

        /// <summary>Implicitly converts an int to SDL_EGLint.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_EGLint(int value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// An enumeration of OpenGL configuration attributes.
    /// </summary>
    public enum SDL_GLAttr
    {
        /// <summary>The minimum number of bits for the red channel of the color buffer; defaults to 8.</summary>
        SDL_GL_RED_SIZE,
        /// <summary>The minimum number of bits for the green channel of the color buffer; defaults to 8.</summary>
        SDL_GL_GREEN_SIZE,
        /// <summary>The minimum number of bits for the blue channel of the color buffer; defaults to 8.</summary>
        SDL_GL_BLUE_SIZE,
        /// <summary>The minimum number of bits for the alpha channel of the color buffer; defaults to 8.</summary>
        SDL_GL_ALPHA_SIZE,
        /// <summary>The minimum number of bits for frame buffer size; defaults to 0.</summary>
        SDL_GL_BUFFER_SIZE,
        /// <summary>Whether the output is single or double buffered; defaults to double buffering on.</summary>
        SDL_GL_DOUBLEBUFFER,
        /// <summary>The minimum number of bits in the depth buffer; defaults to 16.</summary>
        SDL_GL_DEPTH_SIZE,
        /// <summary>The minimum number of bits in the stencil buffer; defaults to 0.</summary>
        SDL_GL_STENCIL_SIZE,
        /// <summary>The minimum number of bits for the red channel of the accumulation buffer; defaults to 0.</summary>
        SDL_GL_ACCUM_RED_SIZE,
        /// <summary>The minimum number of bits for the green channel of the accumulation buffer; defaults to 0.</summary>
        SDL_GL_ACCUM_GREEN_SIZE,
        /// <summary>The minimum number of bits for the blue channel of the accumulation buffer; defaults to 0.</summary>
        SDL_GL_ACCUM_BLUE_SIZE,
        /// <summary>The minimum number of bits for the alpha channel of the accumulation buffer; defaults to 0.</summary>
        SDL_GL_ACCUM_ALPHA_SIZE,
        /// <summary>Whether the output is stereo 3D; defaults to off.</summary>
        SDL_GL_STEREO,
        /// <summary>The number of buffers used for multisample anti-aliasing; defaults to 0.</summary>
        SDL_GL_MULTISAMPLEBUFFERS,
        /// <summary>The number of samples used around the current pixel used for multisample anti-aliasing.</summary>
        SDL_GL_MULTISAMPLESAMPLES,
        /// <summary>Set to 1 to require hardware acceleration, set to 0 to force software rendering; defaults to allow either.</summary>
        SDL_GL_ACCELERATED_VISUAL,
        /// <summary>Not used (deprecated).</summary>
        SDL_GL_RETAINED_BACKING,
        /// <summary>OpenGL context major version.</summary>
        SDL_GL_CONTEXT_MAJOR_VERSION,
        /// <summary>OpenGL context minor version.</summary>
        SDL_GL_CONTEXT_MINOR_VERSION,
        /// <summary>Some combination of 0 or more of elements of the SDL_GLContextFlag enumeration; defaults to 0.</summary>
        SDL_GL_CONTEXT_FLAGS,
        /// <summary>Type of GL context (Core, Compatibility, ES). See SDL_GLProfile; default value depends on platform.</summary>
        SDL_GL_CONTEXT_PROFILE_MASK,
        /// <summary>OpenGL context sharing; defaults to 0.</summary>
        SDL_GL_SHARE_WITH_CURRENT_CONTEXT,
        /// <summary>Requests sRGB capable visual; defaults to 0.</summary>
        SDL_GL_FRAMEBUFFER_SRGB_CAPABLE,
        /// <summary>Sets context the release behavior. See SDL_GLContextReleaseFlag; defaults to FLUSH.</summary>
        SDL_GL_CONTEXT_RELEASE_BEHAVIOR,
        /// <summary>Set context reset notification. See SDL_GLContextResetNotification; defaults to NO_NOTIFICATION.</summary>
        SDL_GL_CONTEXT_RESET_NOTIFICATION,
        /// <summary>OpenGL context no error flag.</summary>
        SDL_GL_CONTEXT_NO_ERROR,
        /// <summary>OpenGL float buffers.</summary>
        SDL_GL_FLOATBUFFERS,
        /// <summary>EGL platform.</summary>
        SDL_GL_EGL_PLATFORM
    }

    /// <summary>OpenGL Core Profile context.</summary>
    public const uint SDL_GL_CONTEXT_PROFILE_CORE = 0x0001;
    /// <summary>OpenGL Compatibility Profile context.</summary>
    public const uint SDL_GL_CONTEXT_PROFILE_COMPATIBILITY = 0x0002;
    /// <summary>GLX_CONTEXT_ES2_PROFILE_BIT_EXT.</summary>
    public const uint SDL_GL_CONTEXT_PROFILE_ES = 0x0004;

    /// <summary>OpenGL context debug flag.</summary>
    public const uint SDL_GL_CONTEXT_DEBUG_FLAG = 0x0001;
    /// <summary>OpenGL context forward compatible flag.</summary>
    public const uint SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG = 0x0002;
    /// <summary>OpenGL context robust access flag.</summary>
    public const uint SDL_GL_CONTEXT_ROBUST_ACCESS_FLAG = 0x0004;
    /// <summary>OpenGL context reset isolation flag.</summary>
    public const uint SDL_GL_CONTEXT_RESET_ISOLATION_FLAG = 0x0008;

    /// <summary>No release behavior.</summary>
    public const uint SDL_GL_CONTEXT_RELEASE_BEHAVIOR_NONE = 0x0000;
    /// <summary>Flush on release behavior.</summary>
    public const uint SDL_GL_CONTEXT_RELEASE_BEHAVIOR_FLUSH = 0x0001;

    /// <summary>No reset notification.</summary>
    public const uint SDL_GL_CONTEXT_RESET_NO_NOTIFICATION = 0x0000;
    /// <summary>Lose context on reset notification.</summary>
    public const uint SDL_GL_CONTEXT_RESET_LOSE_CONTEXT = 0x0001;

    /// <summary>Window surface vsync disabled.</summary>
    public const int SDL_WINDOW_SURFACE_VSYNC_DISABLED = 0;
    /// <summary>Window surface vsync adaptive.</summary>
    public const int SDL_WINDOW_SURFACE_VSYNC_ADAPTIVE = -1;

    /// <summary>Property indicating whether the display supports HDR.</summary>
    public const string SDL_PROP_DISPLAY_HDR_ENABLED_BOOLEAN = "SDL.display.HDR_enabled";
    /// <summary>Property for KMSDRM panel orientation.</summary>
    public const string SDL_PROP_DISPLAY_KMSDRM_PANEL_ORIENTATION_NUMBER = "SDL.display.KMSDRM.panel_orientation";

    /// <summary>Property to set window always on top during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_ALWAYS_ON_TOP_BOOLEAN = "SDL.window.create.always_on_top";
    /// <summary>Property to set window borderless during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_BORDERLESS_BOOLEAN = "SDL.window.create.borderless";
    /// <summary>Property to constrain popup window during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_CONSTRAIN_POPUP_BOOLEAN = "SDL.window.create.constrain_popup";
    /// <summary>Property to set window focusable during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_FOCUSABLE_BOOLEAN = "SDL.window.create.focusable";
    /// <summary>Property to use external graphics context during window creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_EXTERNAL_GRAPHICS_CONTEXT_BOOLEAN = "SDL.window.create.external_graphics_context";
    /// <summary>Property to set window flags during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_FLAGS_NUMBER = "SDL.window.create.flags";
    /// <summary>Property to set window fullscreen during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_FULLSCREEN_BOOLEAN = "SDL.window.create.fullscreen";
    /// <summary>Property to set window height during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_HEIGHT_NUMBER = "SDL.window.create.height";
    /// <summary>Property to set window hidden during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_HIDDEN_BOOLEAN = "SDL.window.create.hidden";
    /// <summary>Property to set high pixel density during window creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_HIGH_PIXEL_DENSITY_BOOLEAN = "SDL.window.create.high_pixel_density";
    /// <summary>Property to set window maximized during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_MAXIMIZED_BOOLEAN = "SDL.window.create.maximized";
    /// <summary>Property to set window as menu during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_MENU_BOOLEAN = "SDL.window.create.menu";
    /// <summary>Property to enable Metal during window creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_METAL_BOOLEAN = "SDL.window.create.metal";
    /// <summary>Property to set window minimized during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_MINIMIZED_BOOLEAN = "SDL.window.create.minimized";
    /// <summary>Property to set window modal during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_MODAL_BOOLEAN = "SDL.window.create.modal";
    /// <summary>Property to set mouse grabbed during window creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_MOUSE_GRABBED_BOOLEAN = "SDL.window.create.mouse_grabbed";
    /// <summary>Property to enable OpenGL during window creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_OPENGL_BOOLEAN = "SDL.window.create.opengl";
    /// <summary>Property to set parent window during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_PARENT_POINTER = "SDL.window.create.parent";
    /// <summary>Property to set window resizable during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_RESIZABLE_BOOLEAN = "SDL.window.create.resizable";
    /// <summary>Property to set window title during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_TITLE_STRING = "SDL.window.create.title";
    /// <summary>Property to set window transparent during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_TRANSPARENT_BOOLEAN = "SDL.window.create.transparent";
    /// <summary>Property to set window as tooltip during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_TOOLTIP_BOOLEAN = "SDL.window.create.tooltip";
    /// <summary>Property to set window as utility during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_UTILITY_BOOLEAN = "SDL.window.create.utility";
    /// <summary>Property to enable Vulkan during window creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_VULKAN_BOOLEAN = "SDL.window.create.vulkan";
    /// <summary>Property to set window width during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WIDTH_NUMBER = "SDL.window.create.width";
    /// <summary>Property to set window X position during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_X_NUMBER = "SDL.window.create.x";
    /// <summary>Property to set window Y position during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_Y_NUMBER = "SDL.window.create.y";
    /// <summary>Property to wrap an existing Cocoa window during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_COCOA_WINDOW_POINTER = "SDL.window.create.cocoa.window";
    /// <summary>Property to wrap an existing Cocoa view during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_COCOA_VIEW_POINTER = "SDL.window.create.cocoa.view";
    /// <summary>Property to use custom Wayland surface role during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WAYLAND_SURFACE_ROLE_CUSTOM_BOOLEAN = "SDL.window.create.wayland.surface_role_custom";
    /// <summary>Property to create EGL window on Wayland during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WAYLAND_CREATE_EGL_WINDOW_BOOLEAN = "SDL.window.create.wayland.create_egl_window";
    /// <summary>Property to wrap an existing Wayland surface during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WAYLAND_WL_SURFACE_POINTER = "SDL.window.create.wayland.wl_surface";
    /// <summary>Property to wrap an existing Win32 HWND during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WIN32_HWND_POINTER = "SDL.window.create.win32.hwnd";
    /// <summary>Property to set pixel format HWND for Win32 window during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WIN32_PIXEL_FORMAT_HWND_POINTER = "SDL.window.create.win32.pixel_format_hwnd";
    /// <summary>Property to wrap an existing X11 window during creation.</summary>
    public const string SDL_PROP_WINDOW_CREATE_X11_WINDOW_NUMBER = "SDL.window.create.x11.window";

    /// <summary>Property to access window shape surface.</summary>
    public const string SDL_PROP_WINDOW_SHAPE_POINTER = "SDL.window.shape";
    /// <summary>Property indicating whether HDR is enabled for the window.</summary>
    public const string SDL_PROP_WINDOW_HDR_ENABLED_BOOLEAN = "SDL.window.HDR_enabled";
    /// <summary>Property for SDR white level of the window.</summary>
    public const string SDL_PROP_WINDOW_SDR_WHITE_LEVEL_FLOAT = "SDL.window.SDR_white_level";
    /// <summary>Property for HDR headroom of the window.</summary>
    public const string SDL_PROP_WINDOW_HDR_HEADROOM_FLOAT = "SDL.window.HDR_headroom";
    /// <summary>Property to access Android ANativeWindow.</summary>
    public const string SDL_PROP_WINDOW_ANDROID_WINDOW_POINTER = "SDL.window.android.window";
    /// <summary>Property to access Android EGLSurface.</summary>
    public const string SDL_PROP_WINDOW_ANDROID_SURFACE_POINTER = "SDL.window.android.surface";
    /// <summary>Property to access UIKit UIWindow.</summary>
    public const string SDL_PROP_WINDOW_UIKIT_WINDOW_POINTER = "SDL.window.uikit.window";
    /// <summary>Property for UIKit Metal view tag.</summary>
    public const string SDL_PROP_WINDOW_UIKIT_METAL_VIEW_TAG_NUMBER = "SDL.window.uikit.metal_view_tag";
    /// <summary>Property for UIKit OpenGL framebuffer.</summary>
    public const string SDL_PROP_WINDOW_UIKIT_OPENGL_FRAMEBUFFER_NUMBER = "SDL.window.uikit.opengl.framebuffer";
    /// <summary>Property for UIKit OpenGL renderbuffer.</summary>
    public const string SDL_PROP_WINDOW_UIKIT_OPENGL_RENDERBUFFER_NUMBER = "SDL.window.uikit.opengl.renderbuffer";
    /// <summary>Property for UIKit OpenGL resolve framebuffer.</summary>
    public const string SDL_PROP_WINDOW_UIKIT_OPENGL_RESOLVE_FRAMEBUFFER_NUMBER = "SDL.window.uikit.opengl.resolve_framebuffer";
    /// <summary>Property for KMSDRM device index.</summary>
    public const string SDL_PROP_WINDOW_KMSDRM_DEVICE_INDEX_NUMBER = "SDL.window.kmsdrm.dev_index";
    /// <summary>Property for KMSDRM DRM file descriptor.</summary>
    public const string SDL_PROP_WINDOW_KMSDRM_DRM_FD_NUMBER = "SDL.window.kmsdrm.drm_fd";
    /// <summary>Property for KMSDRM GBM device.</summary>
    public const string SDL_PROP_WINDOW_KMSDRM_GBM_DEVICE_POINTER = "SDL.window.kmsdrm.gbm_dev";
    /// <summary>Property to access Cocoa NSWindow.</summary>
    public const string SDL_PROP_WINDOW_COCOA_WINDOW_POINTER = "SDL.window.cocoa.window";
    /// <summary>Property for Cocoa Metal view tag.</summary>
    public const string SDL_PROP_WINDOW_COCOA_METAL_VIEW_TAG_NUMBER = "SDL.window.cocoa.metal_view_tag";
    /// <summary>Property for OpenVR overlay ID.</summary>
    public const string SDL_PROP_WINDOW_OPENVR_OVERLAY_ID = "SDL.window.openvr.overlay_id";
    /// <summary>Property to access Vivante display.</summary>
    public const string SDL_PROP_WINDOW_VIVANTE_DISPLAY_POINTER = "SDL.window.vivante.display";
    /// <summary>Property to access Vivante window.</summary>
    public const string SDL_PROP_WINDOW_VIVANTE_WINDOW_POINTER = "SDL.window.vivante.window";
    /// <summary>Property to access Vivante surface.</summary>
    public const string SDL_PROP_WINDOW_VIVANTE_SURFACE_POINTER = "SDL.window.vivante.surface";
    /// <summary>Property to access Win32 HWND.</summary>
    public const string SDL_PROP_WINDOW_WIN32_HWND_POINTER = "SDL.window.win32.hwnd";
    /// <summary>Property to access Win32 HDC.</summary>
    public const string SDL_PROP_WINDOW_WIN32_HDC_POINTER = "SDL.window.win32.hdc";
    /// <summary>Property to access Win32 HINSTANCE.</summary>
    public const string SDL_PROP_WINDOW_WIN32_INSTANCE_POINTER = "SDL.window.win32.instance";
    /// <summary>Property to access Wayland wl_display.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_DISPLAY_POINTER = "SDL.window.wayland.display";
    /// <summary>Property to access Wayland wl_surface.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_SURFACE_POINTER = "SDL.window.wayland.surface";
    /// <summary>Property to access Wayland viewport.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_VIEWPORT_POINTER = "SDL.window.wayland.viewport";
    /// <summary>Property to access Wayland EGL window.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_EGL_WINDOW_POINTER = "SDL.window.wayland.egl_window";
    /// <summary>Property to access Wayland XDG surface.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_XDG_SURFACE_POINTER = "SDL.window.wayland.xdg_surface";
    /// <summary>Property to access Wayland XDG toplevel.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_XDG_TOPLEVEL_POINTER = "SDL.window.wayland.xdg_toplevel";
    /// <summary>Property for Wayland XDG toplevel export handle.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_XDG_TOPLEVEL_EXPORT_HANDLE_STRING = "SDL.window.wayland.xdg_toplevel_export_handle";
    /// <summary>Property to access Wayland XDG popup.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_XDG_POPUP_POINTER = "SDL.window.wayland.xdg_popup";
    /// <summary>Property to access Wayland XDG positioner.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_XDG_POSITIONER_POINTER = "SDL.window.wayland.xdg_positioner";
    /// <summary>Property to access X11 Display.</summary>
    public const string SDL_PROP_WINDOW_X11_DISPLAY_POINTER = "SDL.window.x11.display";
    /// <summary>Property for X11 screen number.</summary>
    public const string SDL_PROP_WINDOW_X11_SCREEN_NUMBER = "SDL.window.x11.screen";
    /// <summary>Property for X11 window ID.</summary>
    public const string SDL_PROP_WINDOW_X11_WINDOW_NUMBER = "SDL.window.x11.window";

    /// <summary>
    /// Hit test results for custom window dragging and resizing.
    /// </summary>
    public enum SDL_HitTestResult
    {
        /// <summary>Region is normal. No special properties.</summary>
        SDL_HITTEST_NORMAL,
        /// <summary>Region can drag entire window.</summary>
        SDL_HITTEST_DRAGGABLE,
        /// <summary>Region is top-left resize handle.</summary>
        SDL_HITTEST_RESIZE_TOPLEFT,
        /// <summary>Region is top resize handle.</summary>
        SDL_HITTEST_RESIZE_TOP,
        /// <summary>Region is top-right resize handle.</summary>
        SDL_HITTEST_RESIZE_TOPRIGHT,
        /// <summary>Region is right resize handle.</summary>
        SDL_HITTEST_RESIZE_RIGHT,
        /// <summary>Region is bottom-right resize handle.</summary>
        SDL_HITTEST_RESIZE_BOTTOMRIGHT,
        /// <summary>Region is bottom resize handle.</summary>
        SDL_HITTEST_RESIZE_BOTTOM,
        /// <summary>Region is bottom-left resize handle.</summary>
        SDL_HITTEST_RESIZE_BOTTOMLEFT,
        /// <summary>Region is left resize handle.</summary>
        SDL_HITTEST_RESIZE_LEFT
    }

    /// <summary>
    /// Get the number of video drivers compiled into SDL.
    /// </summary>
    /// <returns>The number of built in video drivers.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetNumVideoDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumVideoDrivers();

    /// <summary>
    /// Get the name of a built in video driver.
    /// </summary>
    /// <param name="index">The index of a video driver.</param>
    /// <returns>The name of the video driver or NULL if index is out of range.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetVideoDriver", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? SDL_GetVideoDriver(int index);

    /// <summary>
    /// Get the name of the currently initialized video driver.
    /// </summary>
    /// <returns>The name of the current video driver or NULL if no driver has been initialized.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetCurrentVideoDriver", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? SDL_GetCurrentVideoDriver();

    /// <summary>
    /// Get the current system theme.
    /// </summary>
    /// <returns>The current system theme.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetSystemTheme")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_SystemTheme SDL_GetSystemTheme();

    /// <summary>
    /// Get a list of currently connected displays.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of displays returned.</param>
    /// <returns>A NULL-terminated array of display IDs or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetDisplays")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayID* SDL_GetDisplays(int* count);

    /// <summary>
    /// Return the primary display.
    /// </summary>
    /// <returns>The instance ID of the primary display on success or 0 on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetPrimaryDisplay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayID SDL_GetPrimaryDisplay();

    /// <summary>
    /// Get the properties associated with a display.
    /// </summary>
    /// <param name="displayID">The instance ID of the display to query.</param>
    /// <returns>A valid property ID on success or 0 on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetDisplayProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetDisplayProperties(SDL_DisplayID displayID);

    /// <summary>
    /// Get the name of a display.
    /// </summary>
    /// <param name="displayID">The instance ID of the display to query.</param>
    /// <returns>The name of a display or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetDisplayName", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? SDL_GetDisplayName(SDL_DisplayID displayID);

    /// <summary>
    /// Get the desktop area represented by a display.
    /// </summary>
    /// <param name="displayID">The instance ID of the display to query.</param>
    /// <param name="rect">The SDL_Rect structure filled in with the display bounds.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetDisplayBounds")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetDisplayBounds(SDL_DisplayID displayID, SDL_Rect* rect);

    /// <summary>
    /// Get the usable desktop area represented by a display.
    /// </summary>
    /// <param name="displayID">The instance ID of the display to query.</param>
    /// <param name="rect">The SDL_Rect structure filled in with the display usable bounds.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetDisplayUsableBounds")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetDisplayUsableBounds(SDL_DisplayID displayID, SDL_Rect* rect);

    /// <summary>
    /// Get the orientation of a display when it is unrotated.
    /// </summary>
    /// <param name="displayID">The instance ID of the display to query.</param>
    /// <returns>The natural orientation of the display.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetNaturalDisplayOrientation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayOrientation SDL_GetNaturalDisplayOrientation(SDL_DisplayID displayID);

    /// <summary>
    /// Get the orientation of a display.
    /// </summary>
    /// <param name="displayID">The instance ID of the display to query.</param>
    /// <returns>The current orientation of the display.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetCurrentDisplayOrientation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayOrientation SDL_GetCurrentDisplayOrientation(SDL_DisplayID displayID);

    /// <summary>
    /// Get the content scale of a display.
    /// </summary>
    /// <param name="displayID">The instance ID of the display to query.</param>
    /// <returns>The content scale of the display, or 0.0f on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetDisplayContentScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetDisplayContentScale(SDL_DisplayID displayID);

    /// <summary>
    /// Get a list of fullscreen display modes available on a display.
    /// </summary>
    /// <param name="displayID">The instance ID of the display to query.</param>
    /// <param name="count">A pointer filled in with the number of modes returned.</param>
    /// <returns>A NULL-terminated array of display mode pointers or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetFullscreenDisplayModes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayMode** SDL_GetFullscreenDisplayModes(SDL_DisplayID displayID, int* count);

    /// <summary>
    /// Get the closest match to the requested display mode.
    /// </summary>
    /// <param name="displayID">The instance ID of the display to query.</param>
    /// <param name="w">The width in pixels of the desired display mode.</param>
    /// <param name="h">The height in pixels of the desired display mode.</param>
    /// <param name="refresh_rate">The refresh rate of the desired display mode, or 0.0f for the desktop refresh rate.</param>
    /// <param name="include_high_density_modes">Boolean to include high density modes in the search.</param>
    /// <param name="closest">A pointer filled in with the closest display mode equal to or larger than the desired mode.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetClosestFullscreenDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetClosestFullscreenDisplayMode(SDL_DisplayID displayID, int w, int h, float refresh_rate, [MarshalAs(UnmanagedType.U1)] bool include_high_density_modes, SDL_DisplayMode* closest);

    /// <summary>
    /// Get information about the desktop's display mode.
    /// </summary>
    /// <param name="displayID">The instance ID of the display to query.</param>
    /// <returns>A pointer to the desktop display mode or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetDesktopDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayMode* SDL_GetDesktopDisplayMode(SDL_DisplayID displayID);

    /// <summary>
    /// Get information about the current display mode.
    /// </summary>
    /// <param name="displayID">The instance ID of the display to query.</param>
    /// <returns>A pointer to the current display mode or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetCurrentDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayMode* SDL_GetCurrentDisplayMode(SDL_DisplayID displayID);

    /// <summary>
    /// Get the display containing a point.
    /// </summary>
    /// <param name="point">The point to query.</param>
    /// <returns>The instance ID of the display containing the point or 0 on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetDisplayForPoint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayID SDL_GetDisplayForPoint(SDL_Point* point);

    /// <summary>
    /// Get the display primarily containing a rect.
    /// </summary>
    /// <param name="rect">The rect to query.</param>
    /// <returns>The instance ID of the display entirely containing the rect or closest to the center of the rect on success or 0 on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetDisplayForRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayID SDL_GetDisplayForRect(SDL_Rect* rect);

    /// <summary>
    /// Get the display associated with a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The instance ID of the display containing the center of the window on success or 0 on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetDisplayForWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayID SDL_GetDisplayForWindow(SDL_Window* window);

    /// <summary>
    /// Get the pixel density of a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The pixel density or 0.0f on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowPixelDensity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetWindowPixelDensity(SDL_Window* window);

    /// <summary>
    /// Get the content display scale relative to a window's pixel size.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The display scale, or 0.0f on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowDisplayScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetWindowDisplayScale(SDL_Window* window);

    /// <summary>
    /// Set the display mode to use when a window is visible and fullscreen.
    /// </summary>
    /// <param name="window">The window to affect.</param>
    /// <param name="mode">A pointer to the display mode to use, or NULL to use the window's dimensions and the desktop's format and refresh rate.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowFullscreenMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowFullscreenMode(SDL_Window* window, SDL_DisplayMode* mode);

    /// <summary>
    /// Query the display mode to use when a window is visible at fullscreen.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>A pointer to the fullscreen mode to use or NULL for borderless fullscreen desktop mode.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowFullscreenMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayMode* SDL_GetWindowFullscreenMode(SDL_Window* window);

    /// <summary>
    /// Get the raw ICC profile data for the screen the window is currently on.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="size">The size of the ICC profile in bytes.</param>
    /// <returns>The raw ICC profile data or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowICCProfile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_GetWindowICCProfile(SDL_Window* window, nuint* size);

    /// <summary>
    /// Get the pixel format associated with the window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The pixel format of the window on success or SDL_PIXELFORMAT_UNKNOWN on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowPixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PixelFormat SDL_GetWindowPixelFormat(SDL_Window* window);

    /// <summary>
    /// Get a list of valid windows.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of windows returned.</param>
    /// <returns>A NULL-terminated array of SDL_Window pointers or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindows")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Window** SDL_GetWindows(int* count);

    /// <summary>
    /// Create a window with the specified dimensions and flags.
    /// </summary>
    /// <param name="title">The title of the window, in UTF-8 encoding.</param>
    /// <param name="w">The width of the window.</param>
    /// <param name="h">The height of the window.</param>
    /// <param name="flags">0, or one or more SDL_WindowFlags OR'd together.</param>
    /// <returns>The window that was created or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreateWindow", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Window* SDL_CreateWindow(string title, int w, int h, SDL_WindowFlags flags);

    /// <summary>
    /// Create a child popup window of the specified parent window.
    /// </summary>
    /// <param name="parent">The parent of the window, must not be NULL.</param>
    /// <param name="offset_x">The x position of the popup window relative to the origin of the parent.</param>
    /// <param name="offset_y">The y position of the popup window relative to the origin of the parent.</param>
    /// <param name="w">The width of the window.</param>
    /// <param name="h">The height of the window.</param>
    /// <param name="flags">SDL_WINDOW_TOOLTIP or SDL_WINDOW_POPUP_MENU, and zero or more additional SDL_WindowFlags OR'd together.</param>
    /// <returns>The window that was created or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreatePopupWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Window* SDL_CreatePopupWindow(SDL_Window* parent, int offset_x, int offset_y, int w, int h, SDL_WindowFlags flags);

    /// <summary>
    /// Create a window with the specified properties.
    /// </summary>
    /// <param name="props">The properties to use.</param>
    /// <returns>The window that was created or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_CreateWindowWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Window* SDL_CreateWindowWithProperties(SDL_PropertiesID props);

    /// <summary>
    /// Get the numeric ID of a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The ID of the window on success or 0 on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_WindowID SDL_GetWindowID(SDL_Window* window);

    /// <summary>
    /// Get a window from a stored ID.
    /// </summary>
    /// <param name="id">The ID of the window.</param>
    /// <returns>The window associated with id or NULL if it doesn't exist.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Window* SDL_GetWindowFromID(SDL_WindowID id);

    /// <summary>
    /// Get the parent of a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The parent of the window on success or NULL if the window has no parent.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowParent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Window* SDL_GetWindowParent(SDL_Window* window);

    /// <summary>
    /// Get the properties associated with a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>A valid property ID on success or 0 on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetWindowProperties(SDL_Window* window);

    /// <summary>
    /// Get the window flags.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>A mask of the SDL_WindowFlags associated with window.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_WindowFlags SDL_GetWindowFlags(SDL_Window* window);

    /// <summary>
    /// Set the title of a window.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="title">The desired window title in UTF-8 format.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowTitle", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowTitle(SDL_Window* window, string title);

    /// <summary>
    /// Get the title of a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The title of the window in UTF-8 format or "" if there is no title.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowTitle", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string SDL_GetWindowTitle(SDL_Window* window);

    /// <summary>
    /// Set the icon for a window.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="icon">An SDL_Surface structure containing the icon for the window.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowIcon")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowIcon(SDL_Window* window, SDL_Surface* icon);

    /// <summary>
    /// Request that the window's position be set.
    /// </summary>
    /// <param name="window">The window to reposition.</param>
    /// <param name="x">The x coordinate of the window, or SDL_WINDOWPOS_CENTERED or SDL_WINDOWPOS_UNDEFINED.</param>
    /// <param name="y">The y coordinate of the window, or SDL_WINDOWPOS_CENTERED or SDL_WINDOWPOS_UNDEFINED.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowPosition(SDL_Window* window, int x, int y);

    /// <summary>
    /// Get the position of a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="x">A pointer filled in with the x position of the window, may be NULL.</param>
    /// <param name="y">A pointer filled in with the y position of the window, may be NULL.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetWindowPosition(SDL_Window* window, int* x, int* y);

    /// <summary>
    /// Request that the size of a window's client area be set.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="w">The width of the window, must be greater than 0.</param>
    /// <param name="h">The height of the window, must be greater than 0.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowSize(SDL_Window* window, int w, int h);

    /// <summary>
    /// Get the size of a window's client area.
    /// </summary>
    /// <param name="window">The window to query the width and height from.</param>
    /// <param name="w">A pointer filled in with the width of the window, may be NULL.</param>
    /// <param name="h">A pointer filled in with the height of the window, may be NULL.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetWindowSize(SDL_Window* window, int* w, int* h);

    /// <summary>
    /// Get the safe area for this window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="rect">A pointer filled in with the client area that is safe for interactive content.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowSafeArea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetWindowSafeArea(SDL_Window* window, SDL_Rect* rect);

    /// <summary>
    /// Request that the aspect ratio of a window's client area be set.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="min_aspect">The minimum aspect ratio of the window, or 0.0f for no limit.</param>
    /// <param name="max_aspect">The maximum aspect ratio of the window, or 0.0f for no limit.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowAspectRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowAspectRatio(SDL_Window* window, float min_aspect, float max_aspect);

    /// <summary>
    /// Get the size of a window's client area.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="min_aspect">A pointer filled in with the minimum aspect ratio of the window, may be NULL.</param>
    /// <param name="max_aspect">A pointer filled in with the maximum aspect ratio of the window, may be NULL.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowAspectRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetWindowAspectRatio(SDL_Window* window, float* min_aspect, float* max_aspect);

    /// <summary>
    /// Get the size of a window's borders (decorations) around the client area.
    /// </summary>
    /// <param name="window">The window to query the size values of the border (decorations) from.</param>
    /// <param name="top">Pointer to variable for storing the size of the top border, may be NULL.</param>
    /// <param name="left">Pointer to variable for storing the size of the left border, may be NULL.</param>
    /// <param name="bottom">Pointer to variable for storing the size of the bottom border, may be NULL.</param>
    /// <param name="right">Pointer to variable for storing the size of the right border, may be NULL.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowBordersSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetWindowBordersSize(SDL_Window* window, int* top, int* left, int* bottom, int* right);

    /// <summary>
    /// Get the size of a window's client area, in pixels.
    /// </summary>
    /// <param name="window">The window from which the drawable size should be queried.</param>
    /// <param name="w">A pointer to variable for storing the width in pixels, may be NULL.</param>
    /// <param name="h">A pointer to variable for storing the height in pixels, may be NULL.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowSizeInPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetWindowSizeInPixels(SDL_Window* window, int* w, int* h);

    /// <summary>
    /// Set the minimum size of a window's client area.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="min_w">The minimum width of the window, or 0 for no limit.</param>
    /// <param name="min_h">The minimum height of the window, or 0 for no limit.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowMinimumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowMinimumSize(SDL_Window* window, int min_w, int min_h);

    /// <summary>
    /// Get the minimum size of a window's client area.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="w">A pointer filled in with the minimum width of the window, may be NULL.</param>
    /// <param name="h">A pointer filled in with the minimum height of the window, may be NULL.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowMinimumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetWindowMinimumSize(SDL_Window* window, int* w, int* h);

    /// <summary>
    /// Set the maximum size of a window's client area.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="max_w">The maximum width of the window, or 0 for no limit.</param>
    /// <param name="max_h">The maximum height of the window, or 0 for no limit.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowMaximumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowMaximumSize(SDL_Window* window, int max_w, int max_h);

    /// <summary>
    /// Get the maximum size of a window's client area.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="w">A pointer filled in with the maximum width of the window, may be NULL.</param>
    /// <param name="h">A pointer filled in with the maximum height of the window, may be NULL.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowMaximumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetWindowMaximumSize(SDL_Window* window, int* w, int* h);

    /// <summary>
    /// Set the border state of a window.
    /// </summary>
    /// <param name="window">The window of which to change the border state.</param>
    /// <param name="bordered">False to remove border, true to add border.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowBordered")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowBordered(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool bordered);

    /// <summary>
    /// Set the user-resizable state of a window.
    /// </summary>
    /// <param name="window">The window of which to change the resizable state.</param>
    /// <param name="resizable">True to allow resizing, false to disallow.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowResizable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowResizable(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool resizable);

    /// <summary>
    /// Set the window to always be above the others.
    /// </summary>
    /// <param name="window">The window of which to change the always on top state.</param>
    /// <param name="on_top">True to set the window always on top, false otherwise.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowAlwaysOnTop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowAlwaysOnTop(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool on_top);

    /// <summary>
    /// Show a window.
    /// </summary>
    /// <param name="window">The window to show.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_ShowWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ShowWindow(SDL_Window* window);

    /// <summary>
    /// Hide a window.
    /// </summary>
    /// <param name="window">The window to hide.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_HideWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HideWindow(SDL_Window* window);

    /// <summary>
    /// Request that a window be raised above other windows and gain the input focus.
    /// </summary>
    /// <param name="window">The window to raise.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RaiseWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RaiseWindow(SDL_Window* window);

    /// <summary>
    /// Request that the window be made as large as possible.
    /// </summary>
    /// <param name="window">The window to maximize.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_MaximizeWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_MaximizeWindow(SDL_Window* window);

    /// <summary>
    /// Request that the window be minimized to an iconic representation.
    /// </summary>
    /// <param name="window">The window to minimize.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_MinimizeWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_MinimizeWindow(SDL_Window* window);

    /// <summary>
    /// Request that the size and position of a minimized or maximized window be restored.
    /// </summary>
    /// <param name="window">The window to restore.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_RestoreWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_RestoreWindow(SDL_Window* window);

    /// <summary>
    /// Request that the window's fullscreen state be changed.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="fullscreen">True for fullscreen mode, false for windowed mode.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowFullscreen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowFullscreen(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool fullscreen);

    /// <summary>
    /// Block until any pending window state is finalized.
    /// </summary>
    /// <param name="window">The window for which to wait for the pending state to be applied.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SyncWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SyncWindow(SDL_Window* window);

    /// <summary>
    /// Return whether the window has a surface associated with it.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>True if there is a surface associated with the window, or false otherwise.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_WindowHasSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WindowHasSurface(SDL_Window* window);

    /// <summary>
    /// Get the SDL surface associated with the window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The surface associated with the window, or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Surface* SDL_GetWindowSurface(SDL_Window* window);

    /// <summary>
    /// Set the VSync mode for a window.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="vsync">The vertical refresh sync interval.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowSurfaceVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowSurfaceVSync(SDL_Window* window, int vsync);

    /// <summary>
    /// Get VSync for the window surface.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="vsync">An int filled with the current vertical refresh sync interval.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowSurfaceVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetWindowSurfaceVSync(SDL_Window* window, int* vsync);

    /// <summary>
    /// Copy the window surface to the screen.
    /// </summary>
    /// <param name="window">The window to update.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_UpdateWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_UpdateWindowSurface(SDL_Window* window);

    /// <summary>
    /// Copy areas of the window surface to the screen.
    /// </summary>
    /// <param name="window">The window to update.</param>
    /// <param name="rects">An array of SDL_Rect structures representing areas of the surface to copy, in pixels.</param>
    /// <param name="numrects">The number of rectangles.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_UpdateWindowSurfaceRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_UpdateWindowSurfaceRects(SDL_Window* window, SDL_Rect* rects, int numrects);

    /// <summary>
    /// Destroy the surface associated with the window.
    /// </summary>
    /// <param name="window">The window to update.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_DestroyWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_DestroyWindowSurface(SDL_Window* window);

    /// <summary>
    /// Set a window's keyboard grab mode.
    /// </summary>
    /// <param name="window">The window for which the keyboard grab mode should be set.</param>
    /// <param name="grabbed">This is true to grab keyboard, and false to release.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowKeyboardGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowKeyboardGrab(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool grabbed);

    /// <summary>
    /// Set a window's mouse grab mode.
    /// </summary>
    /// <param name="window">The window for which the mouse grab mode should be set.</param>
    /// <param name="grabbed">This is true to grab mouse, and false to release.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowMouseGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowMouseGrab(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool grabbed);

    /// <summary>
    /// Get a window's keyboard grab mode.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>True if keyboard is grabbed, and false otherwise.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowKeyboardGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetWindowKeyboardGrab(SDL_Window* window);

    /// <summary>
    /// Get a window's mouse grab mode.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>True if mouse is grabbed, and false otherwise.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowMouseGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetWindowMouseGrab(SDL_Window* window);

    /// <summary>
    /// Get the window that currently has an input grab enabled.
    /// </summary>
    /// <returns>The window if input is grabbed or NULL otherwise.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetGrabbedWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Window* SDL_GetGrabbedWindow();

    /// <summary>
    /// Confines the cursor to the specified area of a window.
    /// </summary>
    /// <param name="window">The window that will be associated with the barrier.</param>
    /// <param name="rect">A rectangle area in window-relative coordinates. If NULL the barrier for the specified window will be destroyed.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowMouseRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowMouseRect(SDL_Window* window, SDL_Rect* rect);

    /// <summary>
    /// Get the mouse confinement rectangle of a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>A pointer to the mouse confinement rectangle of a window, or NULL if there isn't one.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowMouseRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Rect* SDL_GetWindowMouseRect(SDL_Window* window);

    /// <summary>
    /// Set the opacity for a window.
    /// </summary>
    /// <param name="window">The window which will be made transparent or opaque.</param>
    /// <param name="opacity">The opacity value (0.0f - transparent, 1.0f - opaque).</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowOpacity(SDL_Window* window, float opacity);

    /// <summary>
    /// Get the opacity of a window.
    /// </summary>
    /// <param name="window">The window to get the current opacity value from.</param>
    /// <returns>The opacity, (0.0f - transparent, 1.0f - opaque), or -1.0f on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetWindowOpacity(SDL_Window* window);

    /// <summary>
    /// Set the window as a child of a parent window.
    /// </summary>
    /// <param name="window">The window that should become the child of a parent.</param>
    /// <param name="parent">The new parent window for the child window.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowParent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowParent(SDL_Window* window, SDL_Window* parent);

    /// <summary>
    /// Toggle the state of the window as modal.
    /// </summary>
    /// <param name="window">The window on which to set the modal state.</param>
    /// <param name="modal">True to toggle modal status on, false to toggle it off.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowModal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowModal(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool modal);

    /// <summary>
    /// Set whether the window may have input focus.
    /// </summary>
    /// <param name="window">The window to set focusable state.</param>
    /// <param name="focusable">True to allow input focus, false to not allow input focus.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowFocusable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowFocusable(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool focusable);

    /// <summary>
    /// Display the system-level window menu.
    /// </summary>
    /// <param name="window">The window for which the menu will be displayed.</param>
    /// <param name="x">The x coordinate of the menu, relative to the origin (top-left) of the client area.</param>
    /// <param name="y">The y coordinate of the menu, relative to the origin (top-left) of the client area.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_ShowWindowSystemMenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ShowWindowSystemMenu(SDL_Window* window, int x, int y);

    /// <summary>
    /// Provide a callback that decides if a window region has special properties.
    /// </summary>
    /// <param name="window">The window to set hit-testing on.</param>
    /// <param name="callback">The function to call when doing a hit-test.</param>
    /// <param name="callback_data">An app-defined void pointer passed to callback.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowHitTest")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowHitTest(SDL_Window* window, delegate* unmanaged[Cdecl]<SDL_Window*, SDL_Point*, nuint, SDL_HitTestResult> callback, nuint callback_data);

    /// <summary>
    /// Set the shape of a transparent window.
    /// </summary>
    /// <param name="window">The window.</param>
    /// <param name="shape">The surface representing the shape of the window, or NULL to remove any current shape.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_SetWindowShape")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowShape(SDL_Window* window, SDL_Surface* shape);

    /// <summary>
    /// Request a window to demand attention from the user.
    /// </summary>
    /// <param name="window">The window to be flashed.</param>
    /// <param name="operation">The operation to perform.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_FlashWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_FlashWindow(SDL_Window* window, SDL_FlashOperation operation);

    /// <summary>
    /// Destroy a window.
    /// </summary>
    /// <param name="window">The window to destroy.</param>
    [LibraryImport(Sdl3, EntryPoint = "SDL_DestroyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyWindow(SDL_Window* window);

    /// <summary>
    /// Check whether the screensaver is currently enabled.
    /// </summary>
    /// <returns>True if the screensaver is enabled, false if it is disabled.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_ScreenSaverEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ScreenSaverEnabled();

    /// <summary>
    /// Allow the screen to be blanked by a screen saver.
    /// </summary>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_EnableScreenSaver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_EnableScreenSaver();

    /// <summary>
    /// Prevent the screen from being blanked by a screen saver.
    /// </summary>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_DisableScreenSaver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_DisableScreenSaver();

    /// <summary>
    /// Dynamically load an OpenGL library.
    /// </summary>
    /// <param name="path">The platform dependent OpenGL library name, or NULL to open the default OpenGL library.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_LoadLibrary", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GL_LoadLibrary(string? path);

    /// <summary>
    /// Get an OpenGL function by name.
    /// </summary>
    /// <param name="proc">The name of an OpenGL function.</param>
    /// <returns>A pointer to the named OpenGL function, or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_GetProcAddress", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint SDL_GL_GetProcAddress(string proc);

    /// <summary>
    /// Get an EGL library function by name.
    /// </summary>
    /// <param name="proc">The name of an EGL function.</param>
    /// <returns>A pointer to the named EGL function, or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_EGL_GetProcAddress", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint SDL_EGL_GetProcAddress(string proc);

    /// <summary>
    /// Unload the OpenGL library previously loaded by SDL_GL_LoadLibrary().
    /// </summary>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_UnloadLibrary")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_GL_UnloadLibrary();

    /// <summary>
    /// Check if an OpenGL extension is supported for the current context.
    /// </summary>
    /// <param name="extension">The name of the extension to check.</param>
    /// <returns>True if the extension is supported, false otherwise.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_ExtensionSupported", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GL_ExtensionSupported(string extension);

    /// <summary>
    /// Reset all previously set OpenGL context attributes to their default values.
    /// </summary>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_ResetAttributes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_GL_ResetAttributes();

    /// <summary>
    /// Set an OpenGL window attribute before window creation.
    /// </summary>
    /// <param name="attr">An SDL_GLAttr enum value specifying the OpenGL attribute to set.</param>
    /// <param name="value">The desired value for the attribute.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_SetAttribute")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GL_SetAttribute(SDL_GLAttr attr, int value);

    /// <summary>
    /// Get the actual value for an attribute from the current context.
    /// </summary>
    /// <param name="attr">An SDL_GLAttr enum value specifying the OpenGL attribute to get.</param>
    /// <param name="value">A pointer filled in with the current value of attr.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_GetAttribute")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GL_GetAttribute(SDL_GLAttr attr, int* value);

    /// <summary>
    /// Create an OpenGL context for an OpenGL window, and make it current.
    /// </summary>
    /// <param name="window">The window to associate with the context.</param>
    /// <returns>The OpenGL context associated with window or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_CreateContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GLContext SDL_GL_CreateContext(SDL_Window* window);

    /// <summary>
    /// Set up an OpenGL context for rendering into an OpenGL window.
    /// </summary>
    /// <param name="window">The window to associate with the context.</param>
    /// <param name="context">The OpenGL context to associate with the window.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_MakeCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GL_MakeCurrent(SDL_Window* window, SDL_GLContext context);

    /// <summary>
    /// Get the currently active OpenGL window.
    /// </summary>
    /// <returns>The currently active OpenGL window on success or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_GetCurrentWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Window* SDL_GL_GetCurrentWindow();

    /// <summary>
    /// Get the currently active OpenGL context.
    /// </summary>
    /// <returns>The currently active OpenGL context or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_GetCurrentContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_GLContext SDL_GL_GetCurrentContext();

    /// <summary>
    /// Get the currently active EGL display.
    /// </summary>
    /// <returns>The currently active EGL display or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_EGL_GetCurrentDisplay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_EGLDisplay SDL_EGL_GetCurrentDisplay();

    /// <summary>
    /// Get the currently active EGL config.
    /// </summary>
    /// <returns>The currently active EGL config or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_EGL_GetCurrentConfig")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_EGLConfig SDL_EGL_GetCurrentConfig();

    /// <summary>
    /// Get the EGL surface associated with the window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The EGLSurface pointer associated with the window, or NULL on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_EGL_GetWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_EGLSurface SDL_EGL_GetWindowSurface(SDL_Window* window);

    /// <summary>
    /// Set the callbacks for defining custom EGL attributes.
    /// </summary>
    /// <param name="platformAttribCallback">Callback for specifying attributes for eglGetPlatformDisplay.</param>
    /// <param name="surfaceAttribCallback">Callback for specifying attributes for eglCreateSurface.</param>
    /// <param name="contextAttribCallback">Callback for specifying attributes for eglCreateContext.</param>
    /// <param name="userdata">A pointer passed to each callback.</param>
    [LibraryImport(Sdl3, EntryPoint = "SDL_EGL_SetAttributeCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_EGL_SetAttributeCallbacks(delegate* unmanaged[Cdecl]<nuint, SDL_EGLAttrib*> platformAttribCallback, delegate* unmanaged[Cdecl]<nuint, SDL_EGLDisplay, SDL_EGLConfig, SDL_EGLint*> surfaceAttribCallback, delegate* unmanaged[Cdecl]<nuint, SDL_EGLDisplay, SDL_EGLConfig, SDL_EGLint*> contextAttribCallback, nuint userdata);

    /// <summary>
    /// Set the swap interval for the current OpenGL context.
    /// </summary>
    /// <param name="interval">0 for immediate updates, 1 for updates synchronized with the vertical retrace, -1 for adaptive vsync.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_SetSwapInterval")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GL_SetSwapInterval(int interval);

    /// <summary>
    /// Get the swap interval for the current OpenGL context.
    /// </summary>
    /// <param name="interval">Output for the swap interval, or 0 if there isn't a valid context.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_GetSwapInterval")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GL_GetSwapInterval(int* interval);

    /// <summary>
    /// Update a window with OpenGL rendering.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_SwapWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GL_SwapWindow(SDL_Window* window);

    /// <summary>
    /// Delete an OpenGL context.
    /// </summary>
    /// <param name="context">The OpenGL context to be deleted.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3, EntryPoint = "SDL_GL_DestroyContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GL_DestroyContext(SDL_GLContext context);
}
