using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Skipped: SDL_CreateCursor (bitmap cursor — rarely used),
// SDL_CreateAnimatedCursor (SDL 3.4, niche),
// SDL_SetRelativeMouseTransform (callback — complex interop, niche),
// SDL_CursorFrameInfo struct (only used with animated cursors).

/// <summary>
/// Opaque cursor handle.
/// </summary>
public struct SDL_Cursor;

/// <summary>
/// Cursor types for SDL_CreateSystemCursor().
/// </summary>
public enum SDL_SystemCursor
{
    SDL_SYSTEM_CURSOR_DEFAULT,
    SDL_SYSTEM_CURSOR_TEXT,
    SDL_SYSTEM_CURSOR_WAIT,
    SDL_SYSTEM_CURSOR_CROSSHAIR,
    SDL_SYSTEM_CURSOR_PROGRESS,
    SDL_SYSTEM_CURSOR_NWSE_RESIZE,
    SDL_SYSTEM_CURSOR_NESW_RESIZE,
    SDL_SYSTEM_CURSOR_EW_RESIZE,
    SDL_SYSTEM_CURSOR_NS_RESIZE,
    SDL_SYSTEM_CURSOR_MOVE,
    SDL_SYSTEM_CURSOR_NOT_ALLOWED,
    SDL_SYSTEM_CURSOR_POINTER,
    SDL_SYSTEM_CURSOR_NW_RESIZE,
    SDL_SYSTEM_CURSOR_N_RESIZE,
    SDL_SYSTEM_CURSOR_NE_RESIZE,
    SDL_SYSTEM_CURSOR_E_RESIZE,
    SDL_SYSTEM_CURSOR_SE_RESIZE,
    SDL_SYSTEM_CURSOR_S_RESIZE,
    SDL_SYSTEM_CURSOR_SW_RESIZE,
    SDL_SYSTEM_CURSOR_W_RESIZE,
    SDL_SYSTEM_CURSOR_COUNT,
}

/// <summary>
/// Scroll direction types for the Scroll event.
/// </summary>
public enum SDL_MouseWheelDirection
{
    SDL_MOUSEWHEEL_NORMAL,
    SDL_MOUSEWHEEL_FLIPPED,
}

/// <summary>
/// Native bindings for SDL_mouse.h.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Mouse
{
    public const uint SDL_BUTTON_LEFT = 1;
    public const uint SDL_BUTTON_MIDDLE = 2;
    public const uint SDL_BUTTON_RIGHT = 3;
    public const uint SDL_BUTTON_X1 = 4;
    public const uint SDL_BUTTON_X2 = 5;

    public const uint SDL_TOUCH_MOUSEID = unchecked((uint)-1);
    public const uint SDL_PEN_MOUSEID = unchecked((uint)-2);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasMouse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasMouse();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetMice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint* SDL_GetMice(int* count);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetMouseNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetMouseNameForID(uint instance_id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetMouseFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Window* SDL_GetMouseFocus();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetMouseState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint SDL_GetMouseState(float* x, float* y);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetGlobalMouseState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint SDL_GetGlobalMouseState(float* x, float* y);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetRelativeMouseState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint SDL_GetRelativeMouseState(float* x, float* y);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WarpMouseInWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_WarpMouseInWindow(SDL_Window* window, float x, float y);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_WarpMouseGlobal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WarpMouseGlobal(float x, float y);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetWindowRelativeMouseMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetWindowRelativeMouseMode(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetWindowRelativeMouseMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_GetWindowRelativeMouseMode(SDL_Window* window);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CaptureMouse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CaptureMouse([MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateColorCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Cursor* SDL_CreateColorCursor(SDL_Surface* surface, int hot_x, int hot_y);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CreateSystemCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Cursor* SDL_CreateSystemCursor(SDL_SystemCursor id);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetCursor(SDL_Cursor* cursor);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Cursor* SDL_GetCursor();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetDefaultCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial SDL_Cursor* SDL_GetDefaultCursor();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_DestroyCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SDL_DestroyCursor(SDL_Cursor* cursor);

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ShowCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ShowCursor();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HideCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HideCursor();

    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_CursorVisible")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CursorVisible();
}
