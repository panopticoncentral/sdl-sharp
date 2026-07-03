using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred to a later task (this file, OpenGL/EGL batch): all SDL_GL_* and SDL_EGL_* functions,
// the SDL_GLContext/SDL_GLAttr/SDL_GLProfile/SDL_GLContextFlag/SDL_GLContextReleaseFlag/
// SDL_GLContextResetNotification types.
//
// Permanently skipped: SDL_GetWindowICCProfile (returns a raw ICC profile blob via SDL_free'd
// void*/size_t out-param; niche colour-management use case with no natural high-level wrapper).

/// <summary>
/// Opaque window handle.
/// </summary>
public struct SDL_Window;

/// <summary>
/// A unique ID for a window.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct SDL_WindowID(uint Value);

/// <summary>
/// A unique ID for a display.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct SDL_DisplayID(uint Value);

/// <summary>
/// Window flags.
/// </summary>
[Flags]
public enum SDL_WindowFlags : ulong
{
    /// <summary>Window is in fullscreen mode.</summary>
    SDL_WINDOW_FULLSCREEN = 0x0000000000000001,
    /// <summary>Window usable with OpenGL context.</summary>
    SDL_WINDOW_OPENGL = 0x0000000000000002,
    /// <summary>Window is occluded.</summary>
    SDL_WINDOW_OCCLUDED = 0x0000000000000004,
    /// <summary>Window is hidden.</summary>
    SDL_WINDOW_HIDDEN = 0x0000000000000008,
    /// <summary>No window decoration.</summary>
    SDL_WINDOW_BORDERLESS = 0x0000000000000010,
    /// <summary>Window can be resized.</summary>
    SDL_WINDOW_RESIZABLE = 0x0000000000000020,
    /// <summary>Window is minimized.</summary>
    SDL_WINDOW_MINIMIZED = 0x0000000000000040,
    /// <summary>Window is maximized.</summary>
    SDL_WINDOW_MAXIMIZED = 0x0000000000000080,
    /// <summary>Window has grabbed mouse input.</summary>
    SDL_WINDOW_MOUSE_GRABBED = 0x0000000000000100,
    /// <summary>Window has input focus.</summary>
    SDL_WINDOW_INPUT_FOCUS = 0x0000000000000200,
    /// <summary>Window has mouse focus.</summary>
    SDL_WINDOW_MOUSE_FOCUS = 0x0000000000000400,
    /// <summary>Window not created by SDL.</summary>
    SDL_WINDOW_EXTERNAL = 0x0000000000000800,
    /// <summary>Window is modal.</summary>
    SDL_WINDOW_MODAL = 0x0000000000001000,
    /// <summary>Window uses high pixel density back buffer if possible.</summary>
    SDL_WINDOW_HIGH_PIXEL_DENSITY = 0x0000000000002000,
    /// <summary>Window has mouse captured.</summary>
    SDL_WINDOW_MOUSE_CAPTURE = 0x0000000000004000,
    /// <summary>Window has relative mode enabled.</summary>
    SDL_WINDOW_MOUSE_RELATIVE_MODE = 0x0000000000008000,
    /// <summary>Window should always be above others.</summary>
    SDL_WINDOW_ALWAYS_ON_TOP = 0x0000000000010000,
    /// <summary>Window should be treated as a utility window.</summary>
    SDL_WINDOW_UTILITY = 0x0000000000020000,
    /// <summary>Window should be treated as a tooltip.</summary>
    SDL_WINDOW_TOOLTIP = 0x0000000000040000,
    /// <summary>Window should be treated as a popup menu.</summary>
    SDL_WINDOW_POPUP_MENU = 0x0000000000080000,
    /// <summary>Window has grabbed keyboard input.</summary>
    SDL_WINDOW_KEYBOARD_GRABBED = 0x0000000000100000,
    /// <summary>Window is in fill-document mode (Emscripten only).</summary>
    SDL_WINDOW_FILL_DOCUMENT = 0x0000000000200000,
    /// <summary>Window usable for Vulkan surface.</summary>
    SDL_WINDOW_VULKAN = 0x0000000010000000,
    /// <summary>Window usable for Metal view.</summary>
    SDL_WINDOW_METAL = 0x0000000020000000,
    /// <summary>Window with transparent buffer.</summary>
    SDL_WINDOW_TRANSPARENT = 0x0000000040000000,
    /// <summary>Window should not be focusable.</summary>
    SDL_WINDOW_NOT_FOCUSABLE = 0x0000000080000000,
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
    SDL_FLASH_UNTIL_FOCUSED,
}

/// <summary>
/// Display orientation values.
/// </summary>
public enum SDL_DisplayOrientation
{
    /// <summary>The display orientation can't be determined.</summary>
    SDL_ORIENTATION_UNKNOWN,
    /// <summary>The display is in landscape mode, with the right side up.</summary>
    SDL_ORIENTATION_LANDSCAPE,
    /// <summary>The display is in landscape mode, with the left side up.</summary>
    SDL_ORIENTATION_LANDSCAPE_FLIPPED,
    /// <summary>The display is in portrait mode.</summary>
    SDL_ORIENTATION_PORTRAIT,
    /// <summary>The display is in portrait mode, upside down.</summary>
    SDL_ORIENTATION_PORTRAIT_FLIPPED,
}

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
    SDL_SYSTEM_THEME_DARK,
}

/// <summary>
/// Window progress state.
/// </summary>
public enum SDL_ProgressState
{
    /// <summary>An invalid progress state indicating an error; check SDL_GetError().</summary>
    SDL_PROGRESS_STATE_INVALID = -1,
    /// <summary>No progress bar is shown.</summary>
    SDL_PROGRESS_STATE_NONE,
    /// <summary>The progress bar is shown in a indeterminate state.</summary>
    SDL_PROGRESS_STATE_INDETERMINATE,
    /// <summary>The progress bar is shown in a normal state.</summary>
    SDL_PROGRESS_STATE_NORMAL,
    /// <summary>The progress bar is shown in a paused state.</summary>
    SDL_PROGRESS_STATE_PAUSED,
    /// <summary>The progress bar is shown in a state indicating the application had an error.</summary>
    SDL_PROGRESS_STATE_ERROR,
}

/// <summary>
/// Possible return values from the SDL_HitTest callback.
/// </summary>
public enum SDL_HitTestResult
{
    /// <summary>Region is normal. No special properties.</summary>
    SDL_HITTEST_NORMAL,
    /// <summary>Region can drag entire window.</summary>
    SDL_HITTEST_DRAGGABLE,
    /// <summary>Region is the resizable top-left corner border.</summary>
    SDL_HITTEST_RESIZE_TOPLEFT,
    /// <summary>Region is the resizable top border.</summary>
    SDL_HITTEST_RESIZE_TOP,
    /// <summary>Region is the resizable top-right corner border.</summary>
    SDL_HITTEST_RESIZE_TOPRIGHT,
    /// <summary>Region is the resizable right border.</summary>
    SDL_HITTEST_RESIZE_RIGHT,
    /// <summary>Region is the resizable bottom-right corner border.</summary>
    SDL_HITTEST_RESIZE_BOTTOMRIGHT,
    /// <summary>Region is the resizable bottom border.</summary>
    SDL_HITTEST_RESIZE_BOTTOM,
    /// <summary>Region is the resizable bottom-left corner border.</summary>
    SDL_HITTEST_RESIZE_BOTTOMLEFT,
    /// <summary>Region is the resizable left border.</summary>
    SDL_HITTEST_RESIZE_LEFT,
}

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
    /// <summary>Width.</summary>
    public int w;
    /// <summary>Height.</summary>
    public int h;
    /// <summary>Scale converting size to pixels.</summary>
    public float pixel_density;
    /// <summary>Refresh rate (or 0.0f for unspecified).</summary>
    public float refresh_rate;
    /// <summary>Precise refresh rate numerator (or 0 for unspecified).</summary>
    public int refresh_rate_numerator;
    /// <summary>Precise refresh rate denominator.</summary>
    public int refresh_rate_denominator;
    /// <summary>Private internal data.</summary>
    public nint @internal;
}

/// <summary>
/// Native bindings for SDL_video.h — window and display management.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Video
{
    /// <summary>Undefined window position mask.</summary>
    public const int SDL_WINDOWPOS_UNDEFINED_MASK = unchecked((int)0x1FFF0000u);

    /// <summary>Undefined window position.</summary>
    public const int SDL_WINDOWPOS_UNDEFINED = SDL_WINDOWPOS_UNDEFINED_MASK | 0;

    /// <summary>Centered window position mask.</summary>
    public const int SDL_WINDOWPOS_CENTERED_MASK = unchecked((int)0x2FFF0000u);

    /// <summary>Centered window position.</summary>
    public const int SDL_WINDOWPOS_CENTERED = SDL_WINDOWPOS_CENTERED_MASK | 0;

    /// <summary>Adaptive vsync is disabled for the window surface.</summary>
    public const int SDL_WINDOW_SURFACE_VSYNC_DISABLED = 0;

    /// <summary>Late swap tearing (adaptive vsync) for the window surface.</summary>
    public const int SDL_WINDOW_SURFACE_VSYNC_ADAPTIVE = -1;

    // --- SDL_CreateWindowWithProperties property names ---

    /// <summary>True if the window should be always on top.</summary>
    public const string SDL_PROP_WINDOW_CREATE_ALWAYS_ON_TOP_BOOLEAN = "SDL.window.create.always_on_top";
    /// <summary>True if the window has no window decoration.</summary>
    public const string SDL_PROP_WINDOW_CREATE_BORDERLESS_BOOLEAN = "SDL.window.create.borderless";
    /// <summary>True if the "tooltip" and "menu" window types should be constrained to display bounds (default); false for no positional constraint.</summary>
    public const string SDL_PROP_WINDOW_CREATE_CONSTRAIN_POPUP_BOOLEAN = "SDL.window.create.constrain_popup";
    /// <summary>True if the window will be used with an externally managed graphics context.</summary>
    public const string SDL_PROP_WINDOW_CREATE_EXTERNAL_GRAPHICS_CONTEXT_BOOLEAN = "SDL.window.create.external_graphics_context";
    /// <summary>True if the window should accept keyboard input (defaults true).</summary>
    public const string SDL_PROP_WINDOW_CREATE_FOCUSABLE_BOOLEAN = "SDL.window.create.focusable";
    /// <summary>The window creation flags, as used with SDL_CreateWindow.</summary>
    public const string SDL_PROP_WINDOW_CREATE_FLAGS_NUMBER = "SDL.window.create.flags";
    /// <summary>True if the window should start in fullscreen mode at desktop resolution.</summary>
    public const string SDL_PROP_WINDOW_CREATE_FULLSCREEN_BOOLEAN = "SDL.window.create.fullscreen";
    /// <summary>The height of the window.</summary>
    public const string SDL_PROP_WINDOW_CREATE_HEIGHT_NUMBER = "SDL.window.create.height";
    /// <summary>True if the window should start hidden.</summary>
    public const string SDL_PROP_WINDOW_CREATE_HIDDEN_BOOLEAN = "SDL.window.create.hidden";
    /// <summary>True if the window uses a high pixel density buffer if possible.</summary>
    public const string SDL_PROP_WINDOW_CREATE_HIGH_PIXEL_DENSITY_BOOLEAN = "SDL.window.create.high_pixel_density";
    /// <summary>True if the window should start maximized.</summary>
    public const string SDL_PROP_WINDOW_CREATE_MAXIMIZED_BOOLEAN = "SDL.window.create.maximized";
    /// <summary>True if the window is a popup menu.</summary>
    public const string SDL_PROP_WINDOW_CREATE_MENU_BOOLEAN = "SDL.window.create.menu";
    /// <summary>True if the window will be used with Metal rendering.</summary>
    public const string SDL_PROP_WINDOW_CREATE_METAL_BOOLEAN = "SDL.window.create.metal";
    /// <summary>True if the window should start minimized.</summary>
    public const string SDL_PROP_WINDOW_CREATE_MINIMIZED_BOOLEAN = "SDL.window.create.minimized";
    /// <summary>True if the window is modal to its parent.</summary>
    public const string SDL_PROP_WINDOW_CREATE_MODAL_BOOLEAN = "SDL.window.create.modal";
    /// <summary>True if the window starts with grabbed mouse focus.</summary>
    public const string SDL_PROP_WINDOW_CREATE_MOUSE_GRABBED_BOOLEAN = "SDL.window.create.mouse_grabbed";
    /// <summary>True if the window will be used with OpenGL rendering.</summary>
    public const string SDL_PROP_WINDOW_CREATE_OPENGL_BOOLEAN = "SDL.window.create.opengl";
    /// <summary>An SDL_Window that will be the parent of this window; required for "tooltip", "menu", and "modal" windows.</summary>
    public const string SDL_PROP_WINDOW_CREATE_PARENT_POINTER = "SDL.window.create.parent";
    /// <summary>True if the window should be resizable.</summary>
    public const string SDL_PROP_WINDOW_CREATE_RESIZABLE_BOOLEAN = "SDL.window.create.resizable";
    /// <summary>The title of the window, in UTF-8 encoding.</summary>
    public const string SDL_PROP_WINDOW_CREATE_TITLE_STRING = "SDL.window.create.title";
    /// <summary>True if the window shows transparent in the areas with alpha of 0.</summary>
    public const string SDL_PROP_WINDOW_CREATE_TRANSPARENT_BOOLEAN = "SDL.window.create.transparent";
    /// <summary>True if the window is a tooltip.</summary>
    public const string SDL_PROP_WINDOW_CREATE_TOOLTIP_BOOLEAN = "SDL.window.create.tooltip";
    /// <summary>True if the window is a utility window, not shown in the task bar and window list.</summary>
    public const string SDL_PROP_WINDOW_CREATE_UTILITY_BOOLEAN = "SDL.window.create.utility";
    /// <summary>True if the window will be used with Vulkan rendering.</summary>
    public const string SDL_PROP_WINDOW_CREATE_VULKAN_BOOLEAN = "SDL.window.create.vulkan";
    /// <summary>The width of the window.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WIDTH_NUMBER = "SDL.window.create.width";
    /// <summary>The x position of the window, or SDL_WINDOWPOS_CENTERED; defaults to SDL_WINDOWPOS_UNDEFINED.</summary>
    public const string SDL_PROP_WINDOW_CREATE_X_NUMBER = "SDL.window.create.x";
    /// <summary>The y position of the window, or SDL_WINDOWPOS_CENTERED; defaults to SDL_WINDOWPOS_UNDEFINED.</summary>
    public const string SDL_PROP_WINDOW_CREATE_Y_NUMBER = "SDL.window.create.y";
    /// <summary>macOS: the NSWindow associated with the window, if wrapping an existing window.</summary>
    public const string SDL_PROP_WINDOW_CREATE_COCOA_WINDOW_POINTER = "SDL.window.create.cocoa.window";
    /// <summary>macOS: the NSView associated with the window; defaults to [window contentView].</summary>
    public const string SDL_PROP_WINDOW_CREATE_COCOA_VIEW_POINTER = "SDL.window.create.cocoa.view";
    /// <summary>iOS/tvOS/visionOS: the UIWindowScene associated with the window; defaults to the active window scene.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WINDOWSCENE_POINTER = "SDL.window.create.uikit.windowscene";
    /// <summary>Wayland: true if the application wants to use the Wayland surface for a custom role, not attached to an XDG toplevel window.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WAYLAND_SURFACE_ROLE_CUSTOM_BOOLEAN = "SDL.window.create.wayland.surface_role_custom";
    /// <summary>Wayland: true if the application wants an associated wl_egl_window object created and attached, even without the OpenGL property/flag.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WAYLAND_CREATE_EGL_WINDOW_BOOLEAN = "SDL.window.create.wayland.create_egl_window";
    /// <summary>Wayland: the wl_surface associated with the window, if wrapping an existing window.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WAYLAND_WL_SURFACE_POINTER = "SDL.window.create.wayland.wl_surface";
    /// <summary>Windows: the HWND associated with the window, if wrapping an existing window.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WIN32_HWND_POINTER = "SDL.window.create.win32.hwnd";
    /// <summary>Windows: another window to share pixel format with; useful for OpenGL windows.</summary>
    public const string SDL_PROP_WINDOW_CREATE_WIN32_PIXEL_FORMAT_HWND_POINTER = "SDL.window.create.win32.pixel_format_hwnd";
    /// <summary>X11: the X11 Window associated with the window, if wrapping an existing window.</summary>
    public const string SDL_PROP_WINDOW_CREATE_X11_WINDOW_NUMBER = "SDL.window.create.x11.window";
    /// <summary>Emscripten: the id given to the canvas element; should start with a '#' sign.</summary>
    public const string SDL_PROP_WINDOW_CREATE_EMSCRIPTEN_CANVAS_ID_STRING = "SDL.window.create.emscripten.canvas_id";
    /// <summary>Emscripten: overrides the binding element for keyboard inputs for this canvas.</summary>
    public const string SDL_PROP_WINDOW_CREATE_EMSCRIPTEN_KEYBOARD_ELEMENT_STRING = "SDL.window.create.emscripten.keyboard_element";

    // --- SDL_GetWindowProperties property names ---

    /// <summary>The surface associated with a shaped window, set via SDL_SetWindowShape.</summary>
    public const string SDL_PROP_WINDOW_SHAPE_POINTER = "SDL.window.shape";
    /// <summary>True if the window has HDR headroom above the SDR white point.</summary>
    public const string SDL_PROP_WINDOW_HDR_ENABLED_BOOLEAN = "SDL.window.HDR_enabled";
    /// <summary>The value of SDR white in the SDR to HDR extended range colorspace.</summary>
    public const string SDL_PROP_WINDOW_SDR_WHITE_LEVEL_FLOAT = "SDL.window.SDR_white_level";
    /// <summary>The additional high dynamic range that can be displayed, in terms of the SDR white point.</summary>
    public const string SDL_PROP_WINDOW_HDR_HEADROOM_FLOAT = "SDL.window.HDR_headroom";
    /// <summary>Android: the ANativeWindow associated with the window.</summary>
    public const string SDL_PROP_WINDOW_ANDROID_WINDOW_POINTER = "SDL.window.android.window";
    /// <summary>Android: the EGLSurface associated with the window.</summary>
    public const string SDL_PROP_WINDOW_ANDROID_SURFACE_POINTER = "SDL.window.android.surface";
    /// <summary>iOS: the UIWindow associated with the window.</summary>
    public const string SDL_PROP_WINDOW_UIKIT_WINDOW_POINTER = "SDL.window.uikit.window";
    /// <summary>iOS: the NSInteger tag associated with metal views on the window.</summary>
    public const string SDL_PROP_WINDOW_UIKIT_METAL_VIEW_TAG_NUMBER = "SDL.window.uikit.metal_view_tag";
    /// <summary>iOS: the OpenGL view's framebuffer object; must be bound when rendering to the screen using OpenGL.</summary>
    public const string SDL_PROP_WINDOW_UIKIT_OPENGL_FRAMEBUFFER_NUMBER = "SDL.window.uikit.opengl.framebuffer";
    /// <summary>iOS: the OpenGL view's renderbuffer object; must be bound when SDL_GL_SwapWindow is called.</summary>
    public const string SDL_PROP_WINDOW_UIKIT_OPENGL_RENDERBUFFER_NUMBER = "SDL.window.uikit.opengl.renderbuffer";
    /// <summary>iOS: the OpenGL view's resolve framebuffer, when MSAA is used.</summary>
    public const string SDL_PROP_WINDOW_UIKIT_OPENGL_RESOLVE_FRAMEBUFFER_NUMBER = "SDL.window.uikit.opengl.resolve_framebuffer";
    /// <summary>KMS/DRM: the device index associated with the window (e.g. the X in /dev/dri/cardX).</summary>
    public const string SDL_PROP_WINDOW_KMSDRM_DEVICE_INDEX_NUMBER = "SDL.window.kmsdrm.dev_index";
    /// <summary>KMS/DRM: the DRM FD associated with the window.</summary>
    public const string SDL_PROP_WINDOW_KMSDRM_DRM_FD_NUMBER = "SDL.window.kmsdrm.drm_fd";
    /// <summary>KMS/DRM: the GBM device associated with the window.</summary>
    public const string SDL_PROP_WINDOW_KMSDRM_GBM_DEVICE_POINTER = "SDL.window.kmsdrm.gbm_dev";
    /// <summary>macOS: the NSWindow associated with the window.</summary>
    public const string SDL_PROP_WINDOW_COCOA_WINDOW_POINTER = "SDL.window.cocoa.window";
    /// <summary>macOS: the NSInteger tag associated with metal views on the window.</summary>
    public const string SDL_PROP_WINDOW_COCOA_METAL_VIEW_TAG_NUMBER = "SDL.window.cocoa.metal_view_tag";
    /// <summary>OpenVR: the OpenVR Overlay Handle ID for the associated overlay window.</summary>
    public const string SDL_PROP_WINDOW_OPENVR_OVERLAY_ID_NUMBER = "SDL.window.openvr.overlay_id";
    /// <summary>Vivante: the EGLNativeDisplayType associated with the window.</summary>
    public const string SDL_PROP_WINDOW_VIVANTE_DISPLAY_POINTER = "SDL.window.vivante.display";
    /// <summary>Vivante: the EGLNativeWindowType associated with the window.</summary>
    public const string SDL_PROP_WINDOW_VIVANTE_WINDOW_POINTER = "SDL.window.vivante.window";
    /// <summary>Vivante: the EGLSurface associated with the window.</summary>
    public const string SDL_PROP_WINDOW_VIVANTE_SURFACE_POINTER = "SDL.window.vivante.surface";
    /// <summary>Windows: the HWND associated with the window.</summary>
    public const string SDL_PROP_WINDOW_WIN32_HWND_POINTER = "SDL.window.win32.hwnd";
    /// <summary>Windows: the HDC associated with the window.</summary>
    public const string SDL_PROP_WINDOW_WIN32_HDC_POINTER = "SDL.window.win32.hdc";
    /// <summary>Windows: the HINSTANCE associated with the window.</summary>
    public const string SDL_PROP_WINDOW_WIN32_INSTANCE_POINTER = "SDL.window.win32.instance";
    /// <summary>Wayland: the wl_display associated with the window.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_DISPLAY_POINTER = "SDL.window.wayland.display";
    /// <summary>Wayland: the wl_surface associated with the window.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_SURFACE_POINTER = "SDL.window.wayland.surface";
    /// <summary>Wayland: the wp_viewport associated with the window.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_VIEWPORT_POINTER = "SDL.window.wayland.viewport";
    /// <summary>Wayland: the wl_egl_window associated with the window.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_EGL_WINDOW_POINTER = "SDL.window.wayland.egl_window";
    /// <summary>Wayland: the xdg_surface associated with the window.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_XDG_SURFACE_POINTER = "SDL.window.wayland.xdg_surface";
    /// <summary>Wayland: the xdg_toplevel role associated with the window.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_XDG_TOPLEVEL_POINTER = "SDL.window.wayland.xdg_toplevel";
    /// <summary>Wayland: the export handle associated with the window.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_XDG_TOPLEVEL_EXPORT_HANDLE_STRING = "SDL.window.wayland.xdg_toplevel_export_handle";
    /// <summary>Wayland: the xdg_popup role associated with the window.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_XDG_POPUP_POINTER = "SDL.window.wayland.xdg_popup";
    /// <summary>Wayland: the xdg_positioner associated with the window, in popup mode.</summary>
    public const string SDL_PROP_WINDOW_WAYLAND_XDG_POSITIONER_POINTER = "SDL.window.wayland.xdg_positioner";
    /// <summary>X11: the X11 Display associated with the window.</summary>
    public const string SDL_PROP_WINDOW_X11_DISPLAY_POINTER = "SDL.window.x11.display";
    /// <summary>X11: the screen number associated with the window.</summary>
    public const string SDL_PROP_WINDOW_X11_SCREEN_NUMBER = "SDL.window.x11.screen";
    /// <summary>X11: the X11 Window associated with the window.</summary>
    public const string SDL_PROP_WINDOW_X11_WINDOW_NUMBER = "SDL.window.x11.window";
    /// <summary>Emscripten: the id the canvas element will have.</summary>
    public const string SDL_PROP_WINDOW_EMSCRIPTEN_CANVAS_ID_STRING = "SDL.window.emscripten.canvas_id";
    /// <summary>Emscripten: the keyboard element that associates keyboard events to this window.</summary>
    public const string SDL_PROP_WINDOW_EMSCRIPTEN_KEYBOARD_ELEMENT_STRING = "SDL.window.emscripten.keyboard_element";

    // --- SDL_GetDisplayProperties property names ---

    /// <summary>True if the display has HDR headroom above the SDR white point.</summary>
    public const string SDL_PROP_DISPLAY_HDR_ENABLED_BOOLEAN = "SDL.display.HDR_enabled";
    /// <summary>KMS/DRM: the "panel orientation" property for the display in degrees of clockwise rotation (a hint only).</summary>
    public const string SDL_PROP_DISPLAY_KMSDRM_PANEL_ORIENTATION_NUMBER = "SDL.display.KMSDRM.panel_orientation";
    /// <summary>Wayland: the wl_output associated with the display.</summary>
    public const string SDL_PROP_DISPLAY_WAYLAND_WL_OUTPUT_POINTER = "SDL.display.wayland.wl_output";
    /// <summary>Windows: the monitor handle (HMONITOR) associated with the display.</summary>
    public const string SDL_PROP_DISPLAY_WINDOWS_HMONITOR_POINTER = "SDL.display.windows.hmonitor";

    // --- Window lifecycle ---

    /// <summary>
    /// Create a window with the specified title, dimensions, and flags.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window* SDL_CreateWindow(ReadOnlySpan<byte> title, int w, int h, SDL_WindowFlags flags);

    /// <summary>
    /// Create a window with the specified properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateWindowWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window* SDL_CreateWindowWithProperties(SDL_PropertiesID props);

    /// <summary>
    /// Destroy a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DestroyWindow(SDL_Window* window);

    /// <summary>
    /// Get the numeric ID of a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_WindowID SDL_GetWindowID(SDL_Window* window);

    /// <summary>
    /// Get a window from a stored ID.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window* SDL_GetWindowFromID(SDL_WindowID id);

    /// <summary>
    /// Get the properties associated with a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PropertiesID SDL_GetWindowProperties(SDL_Window* window);

    /// <summary>
    /// Get the window flags.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_WindowFlags SDL_GetWindowFlags(SDL_Window* window);

    // --- Window attributes ---

    /// <summary>
    /// Set the title of a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowTitle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowTitle(SDL_Window* window, ReadOnlySpan<byte> title);

    /// <summary>
    /// Get the title of a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowTitle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetWindowTitle(SDL_Window* window);

    /// <summary>
    /// Set the position of a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowPosition(SDL_Window* window, int x, int y);

    /// <summary>
    /// Get the position of a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetWindowPosition(SDL_Window* window, out int x, out int y);

    /// <summary>
    /// Set the size of a window's client area.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowSize(SDL_Window* window, int w, int h);

    /// <summary>
    /// Get the size of a window's client area.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetWindowSize(SDL_Window* window, out int w, out int h);

    /// <summary>
    /// Get the size of a window's client area in pixels.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowSizeInPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetWindowSizeInPixels(SDL_Window* window, out int w, out int h);

    /// <summary>
    /// Set the minimum size of a window's client area.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowMinimumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowMinimumSize(SDL_Window* window, int min_w, int min_h);

    /// <summary>
    /// Get the minimum size of a window's client area.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowMinimumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetWindowMinimumSize(SDL_Window* window, out int w, out int h);

    /// <summary>
    /// Set the maximum size of a window's client area.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowMaximumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowMaximumSize(SDL_Window* window, int max_w, int max_h);

    /// <summary>
    /// Get the maximum size of a window's client area.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowMaximumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetWindowMaximumSize(SDL_Window* window, out int w, out int h);

    /// <summary>
    /// Set the border state of a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowBordered")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowBordered(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool bordered);

    /// <summary>
    /// Set the user-resizable state of a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowResizable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowResizable(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool resizable);

    /// <summary>
    /// Request that the window's fullscreen state be changed.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowFullscreen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowFullscreen(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool fullscreen);

    /// <summary>
    /// Request that the window be made as large as possible.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ShowWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ShowWindow(SDL_Window* window);

    /// <summary>
    /// Hide a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HideWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_HideWindow(SDL_Window* window);

    /// <summary>
    /// Request that a window be raised above other windows and gain the input focus.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RaiseWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RaiseWindow(SDL_Window* window);

    /// <summary>
    /// Request that the window be made as large as possible.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_MaximizeWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_MaximizeWindow(SDL_Window* window);

    /// <summary>
    /// Request that the window be minimized to an iconic representation.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_MinimizeWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_MinimizeWindow(SDL_Window* window);

    /// <summary>
    /// Request that the size and position of a minimized or maximized window be restored.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_RestoreWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_RestoreWindow(SDL_Window* window);

    /// <summary>
    /// Set the opacity for a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowOpacity(SDL_Window* window, float opacity);

    /// <summary>
    /// Get the opacity of a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial float SDL_GetWindowOpacity(SDL_Window* window);

    /// <summary>
    /// Set the icon for a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowIcon")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowIcon(SDL_Window* window, SDL_Surface* icon);

    /// <summary>
    /// Request a window to demand attention from the user.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_FlashWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_FlashWindow(SDL_Window* window, SDL_FlashOperation operation);

    /// <summary>
    /// Get the pixel density of a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowPixelDensity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial float SDL_GetWindowPixelDensity(SDL_Window* window);

    /// <summary>
    /// Get the content display scale relative to a window's pixel size.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowDisplayScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial float SDL_GetWindowDisplayScale(SDL_Window* window);

    // --- Display ---

    /// <summary>
    /// Get a list of currently connected displays.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDisplays")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_DisplayID* SDL_GetDisplays(out int count);

    /// <summary>
    /// Return the primary display.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetPrimaryDisplay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayID SDL_GetPrimaryDisplay();

    /// <summary>
    /// Get the name of a display in UTF-8 encoding.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDisplayName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetDisplayName(SDL_DisplayID displayID);

    /// <summary>
    /// Get the desktop area represented by a display.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDisplayBounds")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetDisplayBounds(SDL_DisplayID displayID, out SDL_Rect rect);

    /// <summary>
    /// Get the usable desktop area represented by a display, in screen coordinates.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDisplayUsableBounds")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetDisplayUsableBounds(SDL_DisplayID displayID, out SDL_Rect rect);

    /// <summary>
    /// Get the content scale of a display.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDisplayContentScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float SDL_GetDisplayContentScale(SDL_DisplayID displayID);

    /// <summary>
    /// Get information about the desktop's display mode.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDesktopDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_DisplayMode* SDL_GetDesktopDisplayMode(SDL_DisplayID displayID);

    /// <summary>
    /// Get information about the current display mode.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCurrentDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_DisplayMode* SDL_GetCurrentDisplayMode(SDL_DisplayID displayID);

    /// <summary>
    /// Get the display associated with a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDisplayForWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_DisplayID SDL_GetDisplayForWindow(SDL_Window* window);

    // --- Video driver ---

    /// <summary>
    /// Get the number of video drivers compiled into SDL.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNumVideoDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int SDL_GetNumVideoDrivers();

    /// <summary>
    /// Get the name of a built-in video driver.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetVideoDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetVideoDriver(int index);

    /// <summary>
    /// Get the name of the currently initialized video driver.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCurrentVideoDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetCurrentVideoDriver();

    // --- Window surface ---

    /// <summary>
    /// Get the SDL surface associated with the window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Surface* SDL_GetWindowSurface(SDL_Window* window);

    /// <summary>
    /// Copy the window surface to the screen.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UpdateWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_UpdateWindowSurface(SDL_Window* window);

    /// <summary>
    /// Copy areas of the window surface to the screen.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_UpdateWindowSurfaceRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_UpdateWindowSurfaceRects(SDL_Window* window, SDL_Rect* rects, int numrects);

    /// <summary>
    /// Get whether the window has a surface associated with it.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WindowHasSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_WindowHasSurface(SDL_Window* window);

    /// <summary>
    /// Set the VSync for the window surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowSurfaceVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowSurfaceVSync(SDL_Window* window, int vsync);

    /// <summary>
    /// Get the VSync for the window surface.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowSurfaceVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetWindowSurfaceVSync(SDL_Window* window, out int vsync);

    /// <summary>
    /// Destroy the surface associated with the window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_DestroyWindowSurface(SDL_Window* window);

    // --- System theme ---

    /// <summary>
    /// Get the current system theme.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetSystemTheme")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_SystemTheme SDL_GetSystemTheme();

    // --- Fullscreen display modes ---

    /// <summary>
    /// Get a list of fullscreen display modes available on a display.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetFullscreenDisplayModes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_DisplayMode** SDL_GetFullscreenDisplayModes(SDL_DisplayID displayID, out int count);

    /// <summary>
    /// Get the closest match to the requested display mode.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetClosestFullscreenDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetClosestFullscreenDisplayMode(SDL_DisplayID displayID, int w, int h, float refresh_rate, [MarshalAs(UnmanagedType.U1)] bool include_high_density_modes, out SDL_DisplayMode closest);

    /// <summary>
    /// Set the display mode to use when a window is visible and fullscreen.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowFullscreenMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowFullscreenMode(SDL_Window* window, SDL_DisplayMode* mode);

    /// <summary>
    /// Query the display mode to use when a window is visible at fullscreen.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowFullscreenMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_DisplayMode* SDL_GetWindowFullscreenMode(SDL_Window* window);

    // --- Display orientation ---

    /// <summary>
    /// Get the orientation of a display when it is unrotated.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetNaturalDisplayOrientation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayOrientation SDL_GetNaturalDisplayOrientation(SDL_DisplayID displayID);

    /// <summary>
    /// Get the orientation of a display.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCurrentDisplayOrientation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_DisplayOrientation SDL_GetCurrentDisplayOrientation(SDL_DisplayID displayID);

    // --- Display lookup and properties ---

    /// <summary>
    /// Get the display containing a point.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDisplayForPoint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_DisplayID SDL_GetDisplayForPoint(SDL_Point* point);

    /// <summary>
    /// Get the display primarily containing a rect.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDisplayForRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_DisplayID SDL_GetDisplayForRect(SDL_Rect* rect);

    /// <summary>
    /// Get the properties associated with a display.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDisplayProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_PropertiesID SDL_GetDisplayProperties(SDL_DisplayID displayID);

    // --- Window lifecycle: popup, parent, modal, focusable ---

    /// <summary>
    /// Create a child popup window of the specified parent window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreatePopupWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window* SDL_CreatePopupWindow(SDL_Window* parent, int offset_x, int offset_y, int w, int h, SDL_WindowFlags flags);

    /// <summary>
    /// Get parent of a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowParent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window* SDL_GetWindowParent(SDL_Window* window);

    /// <summary>
    /// Set the window as a child of a parent window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowParent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowParent(SDL_Window* window, SDL_Window* parent);

    /// <summary>
    /// Toggle the state of the window as modal.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowModal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowModal(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool modal);

    /// <summary>
    /// Set whether the window may have input focus.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowFocusable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowFocusable(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool focusable);

    // --- Window layout: always-on-top, aspect ratio, sync, borders, safe area, fill document ---

    /// <summary>
    /// Set the window to always be above the others.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowAlwaysOnTop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowAlwaysOnTop(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool on_top);

    /// <summary>
    /// Request that the aspect ratio of a window's client area be set.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowAspectRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowAspectRatio(SDL_Window* window, float min_aspect, float max_aspect);

    /// <summary>
    /// Get the size of a window's client area.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowAspectRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetWindowAspectRatio(SDL_Window* window, out float min_aspect, out float max_aspect);

    /// <summary>
    /// Get the size of a window's borders (decorations) around the client area.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowBordersSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetWindowBordersSize(SDL_Window* window, out int top, out int left, out int bottom, out int right);

    /// <summary>
    /// Get the safe area for this window, in window coordinates.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowSafeArea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetWindowSafeArea(SDL_Window* window, out SDL_Rect rect);

    /// <summary>
    /// Request that the window fill the document (Emscripten only).
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowFillDocument")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowFillDocument(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool fill);

    /// <summary>
    /// Block until any pending window state is finalized.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SyncWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SyncWindow(SDL_Window* window);

    /// <summary>
    /// Get the pixel format associated with the window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowPixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_PixelFormat SDL_GetWindowPixelFormat(SDL_Window* window);

    // --- Mouse/keyboard grab and confinement ---

    /// <summary>
    /// Set a window's mouse grab mode.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowMouseGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowMouseGrab(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool grabbed);

    /// <summary>
    /// Get a window's mouse grab mode.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowMouseGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetWindowMouseGrab(SDL_Window* window);

    /// <summary>
    /// Set a window's keyboard grab mode.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowKeyboardGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowKeyboardGrab(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool grabbed);

    /// <summary>
    /// Get a window's keyboard grab mode.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowKeyboardGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetWindowKeyboardGrab(SDL_Window* window);

    /// <summary>
    /// Confines the cursor to the specified area of a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowMouseRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowMouseRect(SDL_Window* window, SDL_Rect* rect);

    /// <summary>
    /// Get the mouse confinement rectangle of a window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowMouseRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Rect* SDL_GetWindowMouseRect(SDL_Window* window);

    /// <summary>
    /// Get the window that currently has an input grab enabled.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGrabbedWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window* SDL_GetGrabbedWindow();

    // --- Window shape, system menu, progress ---

    /// <summary>
    /// Set the shape of a transparent window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowShape")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowShape(SDL_Window* window, SDL_Surface* shape);

    /// <summary>
    /// Display the system-level window menu.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ShowWindowSystemMenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_ShowWindowSystemMenu(SDL_Window* window, int x, int y);

    /// <summary>
    /// Set the progress state for the given window's taskbar icon.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowProgressState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowProgressState(SDL_Window* window, SDL_ProgressState state);

    /// <summary>
    /// Get the progress state for the given window's taskbar icon.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowProgressState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_ProgressState SDL_GetWindowProgressState(SDL_Window* window);

    /// <summary>
    /// Set the value for the given window's progress bar.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowProgressValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowProgressValue(SDL_Window* window, float value);

    /// <summary>
    /// Get the value for the given window's progress bar.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowProgressValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial float SDL_GetWindowProgressValue(SDL_Window* window);

    // --- Hit test ---

    /// <summary>
    /// Provide a callback that decides if a window region has special properties.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowHitTest")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowHitTest(SDL_Window* window, delegate* unmanaged[Cdecl]<SDL_Window*, SDL_Point*, void*, SDL_HitTestResult> callback, void* callback_data);

    // --- Screen saver ---

    /// <summary>
    /// Check whether the screen saver is currently enabled.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ScreenSaverEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ScreenSaverEnabled();

    /// <summary>
    /// Allow the screen to be blanked by a screen saver.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_EnableScreenSaver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_EnableScreenSaver();

    /// <summary>
    /// Prevent the screen from being blanked by a screen saver.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DisableScreenSaver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_DisableScreenSaver();

    // --- Multiple windows ---

    /// <summary>
    /// Get a list of valid windows.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindows")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window** SDL_GetWindows(out int count);
}
