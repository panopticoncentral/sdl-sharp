using Sdl3Sharp.Graphics;

using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.Mouse;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp.Input;

/// <summary>
/// Provides mouse input functionality.
/// </summary>
public unsafe readonly record struct Mouse(uint Id)
{
    /// <summary>
    /// Gets whether a mouse is currently connected.
    /// </summary>
    public static bool HasMouse => SDL_HasMouse();

    /// <summary>
    /// Gets the name of the mouse.
    /// </summary>
    public string Name => CheckErrorNull(SDL_GetMouseNameForID(Id));

    /// <summary>
    /// Gets a list of currently connected mice.
    /// </summary>
    /// <returns>An array of mouse device information.</returns>
    public static Mouse[] GetMice()
    {
        int count;
        SDL_MouseID* mice = CheckErrorPointer(SDL_GetMice(&count));
        var result = new Mouse[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = new Mouse(mice[i]);
        }

        SDL_free(mice);
        return result;
    }

    /// <summary>
    /// Gets the window which currently has mouse focus.
    /// </summary>
    /// <returns>The window with mouse focus, or null if no window has focus.</returns>
    public static Window? GetFocusedWindow()
    {
        Native.Video.SDL_Window* window = SDL_GetMouseFocus();
        return window != null ? new Window(window) : null;
    }

    /// <summary>
    /// Gets the current mouse button state and position relative to the focused window.
    /// </summary>
    /// <returns>The current mouse state.</returns>
    public static MouseState GetState()
    {
        float x, y;
        SDL_MouseButtonFlags buttons = SDL_GetMouseState(&x, &y);
        return new MouseState(new PointF(x, y), (MousePressedButtons)buttons.Value);
    }

    /// <summary>
    /// Gets the current mouse button state and desktop-relative position.
    /// This queries the platform for immediate asynchronous state.
    /// </summary>
    /// <returns>The current global mouse state.</returns>
    public static MouseState GetGlobalState()
    {
        float x, y;
        SDL_MouseButtonFlags buttons = SDL_GetGlobalMouseState(&x, &y);
        return new MouseState(new PointF(x, y), (MousePressedButtons)buttons.Value);
    }

    /// <summary>
    /// Gets the accumulated mouse delta since the last call to this method.
    /// </summary>
    /// <returns>The relative mouse state with accumulated delta.</returns>
    public static MouseState GetRelativeState()
    {
        float x, y;
        SDL_MouseButtonFlags buttons = SDL_GetRelativeMouseState(&x, &y);
        return new MouseState(new PointF(x, y), (MousePressedButtons)buttons.Value);
    }

    /// <summary>
    /// Moves the mouse cursor to the given position in global screen space.
    /// This function generates a mouse motion event.
    /// </summary>
    /// <param name="position">The position to warp to.</param>
    public static void WarpGlobal(PointF position)
    {
        _ = CheckErrorBool(SDL_WarpMouseGlobal(position.X, position.Y));
    }

    /// <summary>
    /// Captures the mouse to track input outside an SDL window.
    /// </summary>
    /// <param name="enabled">True to enable capturing, false to disable.</param>
    /// <remarks>
    /// Capturing enables your app to obtain mouse events globally, instead of just within your window.
    /// Use this sparingly and in small bursts, such as while the user is dragging something.
    /// </remarks>
    public static void SetCapture(bool enabled)
    {
        _ = CheckErrorBool(SDL_CaptureMouse(enabled));
    }
}