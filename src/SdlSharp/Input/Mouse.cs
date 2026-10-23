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
    /// <summary>Gets a snapshot of connected mouse instance IDs.</summary>
    public static uint[] GetDevices()
    {
        int count;
        var ids = Check(SDL_GetMice(&count));
        try { return new ReadOnlySpan<uint>(ids, count).ToArray(); }
        finally { SDL_free(ids); }
    }
    /// <summary>Gets a connected mouse's name.</summary>
    public static string? GetName(uint id) => System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)SDL_GetMouseNameForID(id));
    /// <summary>Gets a borrowed view of the mouse focus window.</summary>
    public static Window? Focus
    {
        get { var handle = SDL_GetMouseFocus(); return handle == null ? null : new Window(handle, false); }
    }
    /// <summary>Enables or disables relative motion for a window.</summary>
    public static void SetRelativeMode(Window window, bool enabled) => Check(SDL_SetWindowRelativeMouseMode(window.Handle, enabled));
    /// <summary>Gets whether a window uses relative mouse motion.</summary>
    public static bool GetRelativeMode(Window window) => SDL_GetWindowRelativeMouseMode(window.Handle);
    /// <summary>Transforms relative mouse motion. The window is borrowed and the timestamp is in nanoseconds.</summary>
    public delegate (float X, float Y) MotionTransform(ulong timestamp, Window? window, uint mouseId, float x, float y);
    private static CallbackHandle _transform;
    /// <summary>Sets or clears the relative motion transform. Exceptions preserve the original motion and can be retrieved from Application.</summary>
    public static void SetRelativeTransform(MotionTransform? transform)
    {
        CallbackErrors.EnsureOutsideCallback();
        var next = transform == null ? default : CallbackHandle.Alloc(transform);
        try { Check(SDL_SetRelativeMouseTransform(transform == null ? null : &TransformMotion, (void*)(nint)next)); }
        catch { next.Free(); throw; }
        _transform.Free(); _transform = next;
    }
    [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static void TransformMotion(void* data, ulong timestamp, Native.SDL_Window* window, uint id, float* x, float* y)
    {
        try
        {
            using var scope = CallbackErrors.Enter();
            if (CallbackHandle.FromIntPtr((nint)data).Target is MotionTransform transform)
            {
                using var view = window == null ? null : new Window(window, false);
                var result = transform(timestamp, view, id, *x, *y);
                *x = result.X; *y = result.Y;
            }
        }
        catch (Exception error) { CallbackErrors.Capture(error); }
    }
}
