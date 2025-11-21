using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Surface;
using static Sdl3Sharp.Native.Video;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_mouse.h - Mouse input and cursor management.
/// </summary>
public static unsafe partial class Mouse
{
    /// <summary>
    /// This is a unique ID for a mouse for the time it is connected to the system.
    /// The value 0 is an invalid ID.
    /// </summary>
    /// <param name="value">The mouse ID value.</param>
    public readonly struct SDL_MouseID(uint value)
    {
        /// <summary>The underlying mouse ID value.</summary>
        public readonly uint Value = value;

        /// <summary>Implicitly converts an SDL_MouseID to uint.</summary>
        /// <param name="id">The mouse ID to convert.</param>
        public static implicit operator uint(SDL_MouseID id)
        {
            return id.Value;
        }

        /// <summary>Implicitly converts a uint to SDL_MouseID.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_MouseID(uint value)
        {
            return new(value);
        }
    }

    /// <summary>
    /// The structure used to identify an SDL cursor. This is opaque data.
    /// </summary>
    public struct SDL_Cursor { }

    /// <summary>
    /// Cursor types for SDL_CreateSystemCursor().
    /// </summary>
    public enum SDL_SystemCursor
    {
        /// <summary>Default cursor. Usually an arrow.</summary>
        SDL_SYSTEM_CURSOR_DEFAULT,
        /// <summary>Text selection. Usually an I-beam.</summary>
        SDL_SYSTEM_CURSOR_TEXT,
        /// <summary>Wait. Usually an hourglass or watch or spinning ball.</summary>
        SDL_SYSTEM_CURSOR_WAIT,
        /// <summary>Crosshair.</summary>
        SDL_SYSTEM_CURSOR_CROSSHAIR,
        /// <summary>Program is busy but still interactive. Usually it's WAIT with an arrow.</summary>
        SDL_SYSTEM_CURSOR_PROGRESS,
        /// <summary>Double arrow pointing northwest and southeast.</summary>
        SDL_SYSTEM_CURSOR_NWSE_RESIZE,
        /// <summary>Double arrow pointing northeast and southwest.</summary>
        SDL_SYSTEM_CURSOR_NESW_RESIZE,
        /// <summary>Double arrow pointing west and east.</summary>
        SDL_SYSTEM_CURSOR_EW_RESIZE,
        /// <summary>Double arrow pointing north and south.</summary>
        SDL_SYSTEM_CURSOR_NS_RESIZE,
        /// <summary>Four pointed arrow pointing north, south, east, and west.</summary>
        SDL_SYSTEM_CURSOR_MOVE,
        /// <summary>Not permitted. Usually a slashed circle or crossbones.</summary>
        SDL_SYSTEM_CURSOR_NOT_ALLOWED,
        /// <summary>Pointer that indicates a link. Usually a pointing hand.</summary>
        SDL_SYSTEM_CURSOR_POINTER,
        /// <summary>Window resize top-left. This may be a single arrow or a double arrow like NWSE_RESIZE.</summary>
        SDL_SYSTEM_CURSOR_NW_RESIZE,
        /// <summary>Window resize top. May be NS_RESIZE.</summary>
        SDL_SYSTEM_CURSOR_N_RESIZE,
        /// <summary>Window resize top-right. May be NESW_RESIZE.</summary>
        SDL_SYSTEM_CURSOR_NE_RESIZE,
        /// <summary>Window resize right. May be EW_RESIZE.</summary>
        SDL_SYSTEM_CURSOR_E_RESIZE,
        /// <summary>Window resize bottom-right. May be NWSE_RESIZE.</summary>
        SDL_SYSTEM_CURSOR_SE_RESIZE,
        /// <summary>Window resize bottom. May be NS_RESIZE.</summary>
        SDL_SYSTEM_CURSOR_S_RESIZE,
        /// <summary>Window resize bottom-left. May be NESW_RESIZE.</summary>
        SDL_SYSTEM_CURSOR_SW_RESIZE,
        /// <summary>Window resize left. May be EW_RESIZE.</summary>
        SDL_SYSTEM_CURSOR_W_RESIZE,
        /// <summary>Count of system cursors.</summary>
        SDL_SYSTEM_CURSOR_COUNT
    }

    /// <summary>
    /// Scroll direction types for the Scroll event.
    /// </summary>
    public enum SDL_MouseWheelDirection
    {
        /// <summary>The scroll direction is normal.</summary>
        SDL_MOUSEWHEEL_NORMAL,
        /// <summary>The scroll direction is flipped / natural.</summary>
        SDL_MOUSEWHEEL_FLIPPED
    }

    /// <summary>
    /// A bitmask of pressed mouse buttons, as reported by SDL_GetMouseState, etc.
    /// Button 1: Left mouse button, Button 2: Middle mouse button, Button 3: Right mouse button,
    /// Button 4: Side mouse button 1, Button 5: Side mouse button 2.
    /// </summary>
    public readonly struct SDL_MouseButtonFlags(uint value)
    {
        /// <summary>The underlying mouse button flags value.</summary>
        public readonly uint Value = value;

        /// <summary>Implicitly converts SDL_MouseButtonFlags to uint.</summary>
        /// <param name="flags">The flags to convert.</param>
        public static implicit operator uint(SDL_MouseButtonFlags flags)
        {
            return flags.Value;
        }

        /// <summary>Implicitly converts a uint to SDL_MouseButtonFlags.</summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator SDL_MouseButtonFlags(uint value)
        {
            return new(value);
        }
    }

    /// <summary>Left mouse button.</summary>
    public const int SDL_BUTTON_LEFT = 1;
    /// <summary>Middle mouse button.</summary>
    public const int SDL_BUTTON_MIDDLE = 2;
    /// <summary>Right mouse button.</summary>
    public const int SDL_BUTTON_RIGHT = 3;
    /// <summary>Side mouse button 1.</summary>
    public const int SDL_BUTTON_X1 = 4;
    /// <summary>Side mouse button 2.</summary>
    public const int SDL_BUTTON_X2 = 5;

    /// <summary>Helper to create a button mask from a button number.</summary>
    /// <param name="X">The button number (1-based).</param>
    /// <returns>The button mask.</returns>
    public static uint SDL_BUTTON_MASK(int X)
    {
        return 1u << (X - 1);
    }

    /// <summary>Left mouse button mask.</summary>
    public static readonly uint SDL_BUTTON_LMASK = SDL_BUTTON_MASK(SDL_BUTTON_LEFT);
    /// <summary>Middle mouse button mask.</summary>
    public static readonly uint SDL_BUTTON_MMASK = SDL_BUTTON_MASK(SDL_BUTTON_MIDDLE);
    /// <summary>Right mouse button mask.</summary>
    public static readonly uint SDL_BUTTON_RMASK = SDL_BUTTON_MASK(SDL_BUTTON_RIGHT);
    /// <summary>Side mouse button 1 mask.</summary>
    public static readonly uint SDL_BUTTON_X1MASK = SDL_BUTTON_MASK(SDL_BUTTON_X1);
    /// <summary>Side mouse button 2 mask.</summary>
    public static readonly uint SDL_BUTTON_X2MASK = SDL_BUTTON_MASK(SDL_BUTTON_X2);

    /// <summary>
    /// Return whether a mouse is currently connected.
    /// </summary>
    /// <returns>True if a mouse is connected, false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasMouse();

    /// <summary>
    /// Get a list of currently connected mice.
    /// Note that this will include any device or virtual driver that includes mouse functionality.
    /// </summary>
    /// <param name="count">A pointer filled in with the number of mice returned, may be NULL.</param>
    /// <returns>A 0 terminated array of mouse instance IDs or NULL on failure. This should be freed with SDL_free() when it is no longer needed.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_MouseID* SDL_GetMice(int* count);

    /// <summary>
    /// Get the name of a mouse.
    /// This function returns "" if the mouse doesn't have a name.
    /// </summary>
    /// <param name="instance_id">The mouse instance ID.</param>
    /// <returns>The name of the selected mouse, or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string? SDL_GetMouseNameForID(SDL_MouseID instance_id);

    /// <summary>
    /// Get the window which currently has mouse focus.
    /// </summary>
    /// <returns>The window with mouse focus.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Window* SDL_GetMouseFocus();

    /// <summary>
    /// Query SDL's cache for the synchronous mouse button state and the window-relative SDL-cursor position.
    /// </summary>
    /// <param name="x">A pointer to receive the SDL-cursor's x-position from the focused window's top left corner, can be NULL if unused.</param>
    /// <param name="y">A pointer to receive the SDL-cursor's y-position from the focused window's top left corner, can be NULL if unused.</param>
    /// <returns>A 32-bit bitmask of the button state that can be bitwise-compared against the SDL_BUTTON_MASK(X) macro.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_MouseButtonFlags SDL_GetMouseState(float* x, float* y);

    /// <summary>
    /// Query the platform for the asynchronous mouse button state and the desktop-relative platform-cursor position.
    /// </summary>
    /// <param name="x">A pointer to receive the platform-cursor's x-position from the desktop's top left corner, can be NULL if unused.</param>
    /// <param name="y">A pointer to receive the platform-cursor's y-position from the desktop's top left corner, can be NULL if unused.</param>
    /// <returns>A 32-bit bitmask of the button state that can be bitwise-compared against the SDL_BUTTON_MASK(X) macro.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_MouseButtonFlags SDL_GetGlobalMouseState(float* x, float* y);

    /// <summary>
    /// Query SDL's cache for the synchronous mouse button state and accumulated mouse delta since last call.
    /// </summary>
    /// <param name="x">A pointer to receive the x mouse delta accumulated since last call, can be NULL if unused.</param>
    /// <param name="y">A pointer to receive the y mouse delta accumulated since last call, can be NULL if unused.</param>
    /// <returns>A 32-bit bitmask of the button state that can be bitwise-compared against the SDL_BUTTON_MASK(X) macro.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_MouseButtonFlags SDL_GetRelativeMouseState(float* x, float* y);

    /// <summary>
    /// Move the mouse cursor to the given position within the window.
    /// This function generates a mouse motion event if relative mode is not enabled.
    /// </summary>
    /// <param name="window">The window to move the mouse into, or NULL for the current mouse focus.</param>
    /// <param name="x">The x coordinate within the window.</param>
    /// <param name="y">The y coordinate within the window.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_WarpMouseInWindow(SDL_Window* window, float x, float y);

    /// <summary>
    /// Move the mouse to the given position in global screen space.
    /// This function generates a mouse motion event.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_WarpMouseGlobal(float x, float y);

    /// <summary>
    /// Set relative mouse mode for a window.
    /// While the window has focus and relative mouse mode is enabled, the cursor is hidden,
    /// the mouse position is constrained to the window, and SDL will report continuous relative mouse motion.
    /// </summary>
    /// <param name="window">The window to change.</param>
    /// <param name="enabled">True to enable relative mode, false to disable.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetWindowRelativeMouseMode(SDL_Window* window, [MarshalAs(UnmanagedType.U1)] bool enabled);

    /// <summary>
    /// Query whether relative mouse mode is enabled for a window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>True if relative mode is enabled for a window or false otherwise.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_GetWindowRelativeMouseMode(SDL_Window* window);

    /// <summary>
    /// Capture the mouse and to track input outside an SDL window.
    /// Capturing enables your app to obtain mouse events globally, instead of just within your window.
    /// </summary>
    /// <param name="enabled">True to enable capturing, false to disable.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CaptureMouse([MarshalAs(UnmanagedType.U1)] bool enabled);

    /// <summary>
    /// Create a cursor using the specified bitmap data and mask (in MSB format).
    /// The cursor width must be a multiple of 8 bits. Cursors created with this function must be freed with SDL_DestroyCursor().
    /// </summary>
    /// <param name="data">The color value for each pixel of the cursor.</param>
    /// <param name="mask">The mask value for each pixel of the cursor.</param>
    /// <param name="w">The width of the cursor.</param>
    /// <param name="h">The height of the cursor.</param>
    /// <param name="hot_x">The x-axis offset from the left of the cursor image to the mouse x position, in the range of 0 to w - 1.</param>
    /// <param name="hot_y">The y-axis offset from the top of the cursor image to the mouse y position, in the range of 0 to h - 1.</param>
    /// <returns>A new cursor with the specified parameters on success or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Cursor* SDL_CreateCursor(byte* data, byte* mask, int w, int h, int hot_x, int hot_y);

    /// <summary>
    /// Create a color cursor.
    /// </summary>
    /// <param name="surface">An SDL_Surface structure representing the cursor image.</param>
    /// <param name="hot_x">The x position of the cursor hot spot.</param>
    /// <param name="hot_y">The y position of the cursor hot spot.</param>
    /// <returns>The new cursor on success or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Cursor* SDL_CreateColorCursor(SDL_Surface* surface, int hot_x, int hot_y);

    /// <summary>
    /// Create a system cursor.
    /// </summary>
    /// <param name="id">An SDL_SystemCursor enum value.</param>
    /// <returns>A cursor on success or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Cursor* SDL_CreateSystemCursor(SDL_SystemCursor id);

    /// <summary>
    /// Set the active cursor.
    /// This function sets the currently active cursor to the specified one.
    /// </summary>
    /// <param name="cursor">A cursor to make active.</param>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetCursor(SDL_Cursor* cursor);

    /// <summary>
    /// Get the active cursor.
    /// This function returns a pointer to the current cursor which is owned by the library.
    /// </summary>
    /// <returns>The active cursor or NULL if there is no mouse.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Cursor* SDL_GetCursor();

    /// <summary>
    /// Get the default cursor.
    /// You do not have to call SDL_DestroyCursor() on the return value, but it is safe to do so.
    /// </summary>
    /// <returns>The default cursor on success or NULL on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SDL_Cursor* SDL_GetDefaultCursor();

    /// <summary>
    /// Free a previously-created cursor.
    /// Use this function to free cursor resources created with SDL_CreateCursor(), SDL_CreateColorCursor() or SDL_CreateSystemCursor().
    /// </summary>
    /// <param name="cursor">The cursor to free.</param>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_DestroyCursor(SDL_Cursor* cursor);

    /// <summary>
    /// Show the cursor.
    /// </summary>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ShowCursor();

    /// <summary>
    /// Hide the cursor.
    /// </summary>
    /// <returns>True on success or false on failure.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HideCursor();

    /// <summary>
    /// Return whether the cursor is currently being shown.
    /// </summary>
    /// <returns>True if the cursor is being shown, or false if the cursor is hidden.</returns>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_CursorVisible();
}
