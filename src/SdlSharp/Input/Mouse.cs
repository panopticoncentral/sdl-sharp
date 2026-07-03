using SdlSharp.Graphics;
using static SdlSharp.Native.Common;
using static SdlSharp.Native.Mouse;

namespace SdlSharp.Input;

/// <summary>
/// Provides mouse state queries and cursor management.
/// </summary>
public static unsafe class Mouse
{
    /// <summary>
    /// Returns whether any mouse is currently connected.
    /// </summary>
    public static bool HasMouse() => SDL_HasMouse();

    /// <summary>
    /// Gets the synchronous mouse state and window-relative cursor position.
    /// </summary>
    /// <param name="x">Receives the cursor X position.</param>
    /// <param name="y">Receives the cursor Y position.</param>
    /// <returns>A bitmask of pressed buttons.</returns>
    public static uint GetState(out float x, out float y)
    {
        float px, py;
        var buttons = SDL_GetMouseState(&px, &py);
        x = px;
        y = py;
        return buttons;
    }

    /// <summary>
    /// Gets the global (desktop-relative) mouse state.
    /// </summary>
    /// <param name="x">Receives the cursor X position.</param>
    /// <param name="y">Receives the cursor Y position.</param>
    /// <returns>A bitmask of pressed buttons.</returns>
    public static uint GetGlobalState(out float x, out float y)
    {
        float px, py;
        var buttons = SDL_GetGlobalMouseState(&px, &py);
        x = px;
        y = py;
        return buttons;
    }

    /// <summary>
    /// Gets the relative mouse motion since the last call.
    /// </summary>
    /// <param name="x">Receives the X delta.</param>
    /// <param name="y">Receives the Y delta.</param>
    /// <returns>A bitmask of pressed buttons.</returns>
    public static uint GetRelativeState(out float x, out float y)
    {
        float px, py;
        var buttons = SDL_GetRelativeMouseState(&px, &py);
        x = px;
        y = py;
        return buttons;
    }

    /// <summary>
    /// Moves the mouse to a position within the specified window.
    /// </summary>
    /// <param name="window">The target window, or null for the focused window.</param>
    /// <param name="x">The X coordinate within the window.</param>
    /// <param name="y">The Y coordinate within the window.</param>
    public static void WarpInWindow(Window? window, float x, float y) =>
        SDL_WarpMouseInWindow(window is not null ? window.Handle : null, x, y);

    /// <summary>
    /// Moves the mouse to a position in global screen space.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    public static void WarpGlobal(float x, float y) =>
        Check(SDL_WarpMouseGlobal(x, y));

    /// <summary>
    /// Shows the mouse cursor.
    /// </summary>
    /// <seealso cref="Cursor.Current"/>
    public static void ShowCursor() => Check(SDL_ShowCursor());

    /// <summary>
    /// Hides the mouse cursor.
    /// </summary>
    /// <seealso cref="Cursor.Current"/>
    public static void HideCursor() => Check(SDL_HideCursor());

    /// <summary>
    /// Returns whether the cursor is currently visible.
    /// </summary>
    public static bool IsCursorVisible() => SDL_CursorVisible();

    /// <summary>
    /// Enables or disables mouse capture.
    /// </summary>
    /// <param name="enabled">True to enable capture.</param>
    public static void Capture(bool enabled) => Check(SDL_CaptureMouse(enabled));

    /// <summary>
    /// Returns true if the specified button is pressed, based on a button bitmask.
    /// </summary>
    /// <param name="buttonMask">The bitmask from GetState.</param>
    /// <param name="button">The button to check.</param>
    /// <returns>True if the button is pressed.</returns>
    public static bool IsButtonPressed(uint buttonMask, MouseButton button) =>
        (buttonMask & (1u << ((int)button - 1))) != 0;
}
