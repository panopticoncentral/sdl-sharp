using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: popup windows (SDL_CreatePopupWindow), modal (SDL_SetWindowModal, SDL_SetWindowParent),
// hit test (SDL_SetWindowHitTest, SDL_HitTestResult, SDL_HitTest), shape (SDL_SetWindowShape),
// progress (SDL_SetWindowProgressState, SDL_ProgressState), sync (SDL_SyncWindow),
// safe area (SDL_GetWindowSafeArea), aspect ratio (SDL_SetWindowAspectRatio, SDL_GetWindowAspectRatio),
// border size (SDL_GetWindowBordersSize), always-on-top (SDL_SetWindowAlwaysOnTop),
// mouse/keyboard grab (SDL_SetWindowMouseGrab, SDL_GetWindowMouseGrab, SDL_SetWindowKeyboardGrab,
// SDL_GetWindowKeyboardGrab, SDL_SetWindowMouseRect, SDL_GetWindowMouseRect, SDL_GetGrabbedWindow),
// screen saver (SDL_ScreenSaverEnabled, SDL_EnableScreenSaver, SDL_DisableScreenSaver),
// all GL/EGL functions (SDL_GL_*), ICC profile (SDL_GetWindowICCProfile),
// surface vsync (SDL_SetWindowSurfaceVSync, SDL_GetWindowSurfaceVSync),
// fullscreen display modes (SDL_GetFullscreenDisplayModes, SDL_GetClosestFullscreenDisplayMode,
// SDL_SetWindowFullscreenMode, SDL_GetWindowFullscreenMode),
// natural display orientation (SDL_GetNaturalDisplayOrientation, SDL_GetCurrentDisplayOrientation),
// system theme (SDL_GetSystemTheme),
// property string constants (SDL_PROP_WINDOW_*, SDL_PROP_DISPLAY_*).

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
    /// Destroy the surface associated with the window.
    /// </summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_DestroyWindowSurface(SDL_Window* window);
}
