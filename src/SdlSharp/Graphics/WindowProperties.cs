using static SdlSharp.Native.Video;

namespace SdlSharp.Graphics;

/// <summary>
/// Well-known property names for windows. This class covers two distinct
/// property groups that share the "SDL.window" namespace but are used at
/// different times: the <c>Create*</c>-prefixed members name properties
/// passed into window creation (see the window creation property group
/// accepted by <see cref="Window"/> factory methods), while the bare-named
/// members name read-only properties queried afterwards from
/// <see cref="Window.Properties"/> (platform handles, HDR state, and so on).
/// </summary>
public static class WindowProperties
{
    // --- Window creation properties (SDL_PROP_WINDOW_CREATE_*) ---

    /// <summary>True if the window should be always on top.</summary>
    public const string CreateAlwaysOnTop = SDL_PROP_WINDOW_CREATE_ALWAYS_ON_TOP_BOOLEAN;

    /// <summary>True if the window has no window decoration.</summary>
    public const string CreateBorderless = SDL_PROP_WINDOW_CREATE_BORDERLESS_BOOLEAN;

    /// <summary>True if "tooltip"/"menu" windows should be constrained to display bounds (default); false for no constraint.</summary>
    public const string CreateConstrainPopup = SDL_PROP_WINDOW_CREATE_CONSTRAIN_POPUP_BOOLEAN;

    /// <summary>True if the window will be used with an externally managed graphics context.</summary>
    public const string CreateExternalGraphicsContext = SDL_PROP_WINDOW_CREATE_EXTERNAL_GRAPHICS_CONTEXT_BOOLEAN;

    /// <summary>True if the window should accept keyboard input (defaults true).</summary>
    public const string CreateFocusable = SDL_PROP_WINDOW_CREATE_FOCUSABLE_BOOLEAN;

    /// <summary>The <see cref="WindowFlags"/> to create the window with, as a number.</summary>
    public const string CreateFlags = SDL_PROP_WINDOW_CREATE_FLAGS_NUMBER;

    /// <summary>True if the window should start in fullscreen mode at desktop resolution.</summary>
    public const string CreateFullscreen = SDL_PROP_WINDOW_CREATE_FULLSCREEN_BOOLEAN;

    /// <summary>The height of the window.</summary>
    public const string CreateHeight = SDL_PROP_WINDOW_CREATE_HEIGHT_NUMBER;

    /// <summary>True if the window should start hidden.</summary>
    public const string CreateHidden = SDL_PROP_WINDOW_CREATE_HIDDEN_BOOLEAN;

    /// <summary>True if the window uses a high pixel density buffer if possible.</summary>
    public const string CreateHighPixelDensity = SDL_PROP_WINDOW_CREATE_HIGH_PIXEL_DENSITY_BOOLEAN;

    /// <summary>True if the window should start maximized.</summary>
    public const string CreateMaximized = SDL_PROP_WINDOW_CREATE_MAXIMIZED_BOOLEAN;

    /// <summary>True if the window is a popup menu.</summary>
    public const string CreateMenu = SDL_PROP_WINDOW_CREATE_MENU_BOOLEAN;

    /// <summary>True if the window will be used with Metal rendering.</summary>
    public const string CreateMetal = SDL_PROP_WINDOW_CREATE_METAL_BOOLEAN;

    /// <summary>True if the window should start minimized.</summary>
    public const string CreateMinimized = SDL_PROP_WINDOW_CREATE_MINIMIZED_BOOLEAN;

    /// <summary>True if the window is modal to its parent.</summary>
    public const string CreateModal = SDL_PROP_WINDOW_CREATE_MODAL_BOOLEAN;

    /// <summary>True if the window starts with grabbed mouse focus.</summary>
    public const string CreateMouseGrabbed = SDL_PROP_WINDOW_CREATE_MOUSE_GRABBED_BOOLEAN;

    /// <summary>True if the window will be used with OpenGL rendering.</summary>
    public const string CreateOpenGl = SDL_PROP_WINDOW_CREATE_OPENGL_BOOLEAN;

    /// <summary>An <see cref="Window"/> that will be the parent of this window; required for "tooltip", "menu", and "modal" windows.</summary>
    public const string CreateParent = SDL_PROP_WINDOW_CREATE_PARENT_POINTER;

    /// <summary>True if the window should be resizable.</summary>
    public const string CreateResizable = SDL_PROP_WINDOW_CREATE_RESIZABLE_BOOLEAN;

    /// <summary>The title of the window, in UTF-8 encoding.</summary>
    public const string CreateTitle = SDL_PROP_WINDOW_CREATE_TITLE_STRING;

    /// <summary>True if the window shows transparent in areas with alpha of 0.</summary>
    public const string CreateTransparent = SDL_PROP_WINDOW_CREATE_TRANSPARENT_BOOLEAN;

    /// <summary>True if the window is a tooltip.</summary>
    public const string CreateTooltip = SDL_PROP_WINDOW_CREATE_TOOLTIP_BOOLEAN;

    /// <summary>True if the window is a utility window, not showing in the task bar and window list.</summary>
    public const string CreateUtility = SDL_PROP_WINDOW_CREATE_UTILITY_BOOLEAN;

    /// <summary>True if the window will be used with Vulkan rendering.</summary>
    public const string CreateVulkan = SDL_PROP_WINDOW_CREATE_VULKAN_BOOLEAN;

    /// <summary>The width of the window.</summary>
    public const string CreateWidth = SDL_PROP_WINDOW_CREATE_WIDTH_NUMBER;

    /// <summary>The x position of the window, or a centered/undefined sentinel; relative to the parent for "tooltip"/"menu" windows.</summary>
    public const string CreateX = SDL_PROP_WINDOW_CREATE_X_NUMBER;

    /// <summary>The y position of the window, or a centered/undefined sentinel; relative to the parent for "tooltip"/"menu" windows.</summary>
    public const string CreateY = SDL_PROP_WINDOW_CREATE_Y_NUMBER;

    /// <summary>(macOS) The existing (__unsafe_unretained) NSWindow to wrap.</summary>
    public const string CreateCocoaWindow = SDL_PROP_WINDOW_CREATE_COCOA_WINDOW_POINTER;

    /// <summary>(macOS) The existing (__unsafe_unretained) NSView to use; defaults to <c>[window contentView]</c>.</summary>
    public const string CreateCocoaView = SDL_PROP_WINDOW_CREATE_COCOA_VIEW_POINTER;

    /// <summary>(iOS/tvOS/visionOS) The (__unsafe_unretained) UIWindowScene to use; defaults to the active window scene.</summary>
    public const string CreateWindowScene = SDL_PROP_WINDOW_CREATE_WINDOWSCENE_POINTER;

    /// <summary>(Wayland) True to use the surface for a custom role rather than attaching it to an XDG toplevel window.</summary>
    public const string CreateWaylandSurfaceRoleCustom = SDL_PROP_WINDOW_CREATE_WAYLAND_SURFACE_ROLE_CUSTOM_BOOLEAN;

    /// <summary>(Wayland) True to create and attach an associated wl_egl_window object, even without OpenGL.</summary>
    public const string CreateWaylandCreateEglWindow = SDL_PROP_WINDOW_CREATE_WAYLAND_CREATE_EGL_WINDOW_BOOLEAN;

    /// <summary>(Wayland) The existing wl_surface to wrap.</summary>
    public const string CreateWaylandWlSurface = SDL_PROP_WINDOW_CREATE_WAYLAND_WL_SURFACE_POINTER;

    /// <summary>(Windows) The existing HWND to wrap.</summary>
    public const string CreateWin32Hwnd = SDL_PROP_WINDOW_CREATE_WIN32_HWND_POINTER;

    /// <summary>(Windows) Another window to share pixel format with; useful for OpenGL windows.</summary>
    public const string CreateWin32PixelFormatHwnd = SDL_PROP_WINDOW_CREATE_WIN32_PIXEL_FORMAT_HWND_POINTER;

    /// <summary>(X11) The existing X11 Window to wrap.</summary>
    public const string CreateX11Window = SDL_PROP_WINDOW_CREATE_X11_WINDOW_NUMBER;

    /// <summary>(Emscripten) The id given to the canvas element; should start with a '#'.</summary>
    public const string CreateEmscriptenCanvasId = SDL_PROP_WINDOW_CREATE_EMSCRIPTEN_CANVAS_ID_STRING;

    /// <summary>(Emscripten) Overrides the binding element for keyboard inputs for this canvas.</summary>
    public const string CreateEmscriptenKeyboardElement = SDL_PROP_WINDOW_CREATE_EMSCRIPTEN_KEYBOARD_ELEMENT_STRING;

    // --- Post-creation, read-only window properties (SDL_PROP_WINDOW_*, queried via Window.Properties) ---

    /// <summary>The surface associated with a shaped window.</summary>
    public const string Shape = SDL_PROP_WINDOW_SHAPE_POINTER;

    /// <summary>True if the window has HDR headroom above the SDR white point. Can change dynamically.</summary>
    public const string HdrEnabled = SDL_PROP_WINDOW_HDR_ENABLED_BOOLEAN;

    /// <summary>The value of SDR white in the sRGB linear colorspace. Can change dynamically.</summary>
    public const string SdrWhiteLevel = SDL_PROP_WINDOW_SDR_WHITE_LEVEL_FLOAT;

    /// <summary>The additional high dynamic range that can be displayed, in terms of the SDR white point (1.0 when HDR is off). Can change dynamically.</summary>
    public const string HdrHeadroom = SDL_PROP_WINDOW_HDR_HEADROOM_FLOAT;

    /// <summary>(Android) The ANativeWindow associated with the window.</summary>
    public const string AndroidWindow = SDL_PROP_WINDOW_ANDROID_WINDOW_POINTER;

    /// <summary>(Android) The EGLSurface associated with the window.</summary>
    public const string AndroidSurface = SDL_PROP_WINDOW_ANDROID_SURFACE_POINTER;

    /// <summary>(iOS) The (__unsafe_unretained) UIWindow associated with the window.</summary>
    public const string UikitWindow = SDL_PROP_WINDOW_UIKIT_WINDOW_POINTER;

    /// <summary>(iOS) The NSInteger tag associated with Metal views on the window.</summary>
    public const string UikitMetalViewTag = SDL_PROP_WINDOW_UIKIT_METAL_VIEW_TAG_NUMBER;

    /// <summary>(iOS) The OpenGL view's framebuffer object; must be bound when rendering with OpenGL.</summary>
    public const string UikitOpenGlFramebuffer = SDL_PROP_WINDOW_UIKIT_OPENGL_FRAMEBUFFER_NUMBER;

    /// <summary>(iOS) The OpenGL view's renderbuffer object; must be bound when SDL_GL_SwapWindow is called.</summary>
    public const string UikitOpenGlRenderbuffer = SDL_PROP_WINDOW_UIKIT_OPENGL_RENDERBUFFER_NUMBER;

    /// <summary>(iOS) The OpenGL view's resolve framebuffer, when MSAA is used.</summary>
    public const string UikitOpenGlResolveFramebuffer = SDL_PROP_WINDOW_UIKIT_OPENGL_RESOLVE_FRAMEBUFFER_NUMBER;

    /// <summary>(KMS/DRM) The device index associated with the window (the X in /dev/dri/cardX).</summary>
    public const string KmsdrmDeviceIndex = SDL_PROP_WINDOW_KMSDRM_DEVICE_INDEX_NUMBER;

    /// <summary>(KMS/DRM) The DRM FD associated with the window.</summary>
    public const string KmsdrmDrmFd = SDL_PROP_WINDOW_KMSDRM_DRM_FD_NUMBER;

    /// <summary>(KMS/DRM) The GBM device associated with the window.</summary>
    public const string KmsdrmGbmDevice = SDL_PROP_WINDOW_KMSDRM_GBM_DEVICE_POINTER;

    /// <summary>(macOS) The (__unsafe_unretained) NSWindow associated with the window.</summary>
    public const string CocoaWindow = SDL_PROP_WINDOW_COCOA_WINDOW_POINTER;

    /// <summary>(macOS) The NSInteger tag associated with Metal views on the window.</summary>
    public const string CocoaMetalViewTag = SDL_PROP_WINDOW_COCOA_METAL_VIEW_TAG_NUMBER;

    /// <summary>(OpenVR) The OpenVR Overlay Handle ID for the associated overlay window.</summary>
    public const string OpenVrOverlayId = SDL_PROP_WINDOW_OPENVR_OVERLAY_ID_NUMBER;

    /// <summary>(Vivante) The EGLNativeDisplayType associated with the window.</summary>
    public const string VivanteDisplay = SDL_PROP_WINDOW_VIVANTE_DISPLAY_POINTER;

    /// <summary>(Vivante) The EGLNativeWindowType associated with the window.</summary>
    public const string VivanteWindow = SDL_PROP_WINDOW_VIVANTE_WINDOW_POINTER;

    /// <summary>(Vivante) The EGLSurface associated with the window.</summary>
    public const string VivanteSurface = SDL_PROP_WINDOW_VIVANTE_SURFACE_POINTER;

    /// <summary>(Windows) The HWND associated with the window.</summary>
    public const string Win32Hwnd = SDL_PROP_WINDOW_WIN32_HWND_POINTER;

    /// <summary>(Windows) The HDC associated with the window.</summary>
    public const string Win32Hdc = SDL_PROP_WINDOW_WIN32_HDC_POINTER;

    /// <summary>(Windows) The HINSTANCE associated with the window.</summary>
    public const string Win32Instance = SDL_PROP_WINDOW_WIN32_INSTANCE_POINTER;

    /// <summary>(Wayland) The wl_display associated with the window.</summary>
    public const string WaylandDisplay = SDL_PROP_WINDOW_WAYLAND_DISPLAY_POINTER;

    /// <summary>(Wayland) The wl_surface associated with the window.</summary>
    public const string WaylandSurface = SDL_PROP_WINDOW_WAYLAND_SURFACE_POINTER;

    /// <summary>(Wayland) The wp_viewport associated with the window.</summary>
    public const string WaylandViewport = SDL_PROP_WINDOW_WAYLAND_VIEWPORT_POINTER;

    /// <summary>(Wayland) The wl_egl_window associated with the window.</summary>
    public const string WaylandEglWindow = SDL_PROP_WINDOW_WAYLAND_EGL_WINDOW_POINTER;

    /// <summary>(Wayland) The xdg_surface associated with the window.</summary>
    public const string WaylandXdgSurface = SDL_PROP_WINDOW_WAYLAND_XDG_SURFACE_POINTER;

    /// <summary>(Wayland) The xdg_toplevel role associated with the window.</summary>
    public const string WaylandXdgToplevel = SDL_PROP_WINDOW_WAYLAND_XDG_TOPLEVEL_POINTER;

    /// <summary>(Wayland) The export handle associated with the window.</summary>
    public const string WaylandXdgToplevelExportHandle = SDL_PROP_WINDOW_WAYLAND_XDG_TOPLEVEL_EXPORT_HANDLE_STRING;

    /// <summary>(Wayland) The xdg_popup role associated with the window.</summary>
    public const string WaylandXdgPopup = SDL_PROP_WINDOW_WAYLAND_XDG_POPUP_POINTER;

    /// <summary>(Wayland) The xdg_positioner associated with the window, in popup mode.</summary>
    public const string WaylandXdgPositioner = SDL_PROP_WINDOW_WAYLAND_XDG_POSITIONER_POINTER;

    /// <summary>(X11) The X11 Display associated with the window.</summary>
    public const string X11Display = SDL_PROP_WINDOW_X11_DISPLAY_POINTER;

    /// <summary>(X11) The screen number associated with the window.</summary>
    public const string X11Screen = SDL_PROP_WINDOW_X11_SCREEN_NUMBER;

    /// <summary>(X11) The X11 Window associated with the window.</summary>
    public const string X11Window = SDL_PROP_WINDOW_X11_WINDOW_NUMBER;

    /// <summary>(Emscripten) The id the canvas element will have.</summary>
    public const string EmscriptenCanvasId = SDL_PROP_WINDOW_EMSCRIPTEN_CANVAS_ID_STRING;

    /// <summary>(Emscripten) The keyboard element that associates keyboard events to this window.</summary>
    public const string EmscriptenKeyboardElement = SDL_PROP_WINDOW_EMSCRIPTEN_KEYBOARD_ELEMENT_STRING;
}
