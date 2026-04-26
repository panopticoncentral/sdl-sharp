using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Accessors for the global ImGui <c>ImGuiIO</c> struct — display size, timing,
/// mouse/keyboard state, metrics, and event injection for custom backends.
/// </summary>
public static unsafe class Io
{
    // --- Display ---

    /// <summary>Main display size in pixels. Must be set by the backend before <c>NewFrame</c>.</summary>
    public static Vec2 DisplaySize
    {
        get { var v = IGSharp_IO_GetDisplaySize(); return new Vec2(v.X, v.Y); }
        set => IGSharp_IO_SetDisplaySize(new IGSharp_Vec2(value.X, value.Y));
    }

    /// <summary>Ratio of framebuffer pixels to display units (for Retina / HiDPI displays).</summary>
    public static Vec2 DisplayFramebufferScale
    {
        get { var v = IGSharp_IO_GetDisplayFramebufferScale(); return new Vec2(v.X, v.Y); }
        set => IGSharp_IO_SetDisplayFramebufferScale(new IGSharp_Vec2(value.X, value.Y));
    }

    /// <summary>Seconds since the last frame (time step for animation / timing).</summary>
    public static float DeltaTime
    {
        get => IGSharp_IO_GetDeltaTime();
        set => IGSharp_IO_SetDeltaTime(value);
    }

    // --- Mouse / Keyboard state (read-only) ---

    /// <summary>Current mouse position in screen coordinates.</summary>
    public static Vec2 MousePos
    {
        get { var v = IGSharp_IO_GetMousePos(); return new Vec2(v.X, v.Y); }
    }

    /// <summary>Mouse position delta since the last frame.</summary>
    public static Vec2 MouseDelta
    {
        get { var v = IGSharp_IO_GetMouseDelta(); return new Vec2(v.X, v.Y); }
    }

    /// <summary>Vertical mouse wheel delta.</summary>
    public static float MouseWheel => IGSharp_IO_GetMouseWheel();

    /// <summary>Horizontal mouse wheel delta.</summary>
    public static float MouseWheelHorizontal => IGSharp_IO_GetMouseWheelH();

    /// <summary>True if Ctrl is held this frame.</summary>
    public static bool KeyCtrl => IGSharp_IO_GetKeyCtrl();

    /// <summary>True if Shift is held this frame.</summary>
    public static bool KeyShift => IGSharp_IO_GetKeyShift();

    /// <summary>True if Alt is held this frame.</summary>
    public static bool KeyAlt => IGSharp_IO_GetKeyAlt();

    /// <summary>True if Super (Windows / Cmd) is held this frame.</summary>
    public static bool KeySuper => IGSharp_IO_GetKeySuper();

    // --- "Want" flags (signals to the backend) ---

    /// <summary>True if ImGui wants to capture mouse input (the backend should not also act on it).</summary>
    public static bool WantCaptureMouse => IGSharp_IO_GetWantCaptureMouse();

    /// <summary>True if ImGui wants to capture keyboard input.</summary>
    public static bool WantCaptureKeyboard => IGSharp_IO_GetWantCaptureKeyboard();

    /// <summary>True if ImGui wants a text-input UI (e.g. on-screen keyboard on mobile).</summary>
    public static bool WantTextInput => IGSharp_IO_GetWantTextInput();

    /// <summary>True if ImGui wants the backend to reposition the mouse cursor.</summary>
    public static bool WantSetMousePos => IGSharp_IO_GetWantSetMousePos();

    /// <summary>True if ImGui has unsaved settings (backend should save the ini file). Clear after saving.</summary>
    public static bool WantSaveIniSettings
    {
        get => IGSharp_IO_GetWantSaveIniSettings();
        set => IGSharp_IO_SetWantSaveIniSettings(value);
    }

    // --- Navigation state ---

    /// <summary>True if keyboard/gamepad navigation is currently active.</summary>
    public static bool NavActive => IGSharp_IO_GetNavActive();

    /// <summary>True if the navigation highlight rectangle is currently visible.</summary>
    public static bool NavVisible => IGSharp_IO_GetNavVisible();

    // --- Timing / Config ---

    /// <summary>Frame rate (computed as a running average).</summary>
    public static float Framerate => IGSharp_IO_GetFramerate();

    /// <summary>Config flags (navigation, DPI awareness, etc.).</summary>
    public static ConfigFlags ConfigFlags
    {
        get => (ConfigFlags)IGSharp_IO_GetConfigFlags();
        set => IGSharp_IO_SetConfigFlags((int)value);
    }

    /// <summary>Backend capability flags (set by the imgui_impl_* backend).</summary>
    public static BackendFlags BackendFlags
    {
        get => (BackendFlags)IGSharp_IO_GetBackendFlags();
        set => IGSharp_IO_SetBackendFlags((int)value);
    }

    /// <summary>Sets the filename for saving/loading window layout. Pass null to disable persistence.</summary>
    public static void SetIniFilename(string? filename) => IGSharp_IO_SetIniFilename(ToUtf8(filename));

    // --- Metrics (read-only) ---

    /// <summary>Total vertex count submitted this frame.</summary>
    public static int MetricsRenderVertices => IGSharp_IO_GetMetricsRenderVertices();

    /// <summary>Total index count submitted this frame.</summary>
    public static int MetricsRenderIndices => IGSharp_IO_GetMetricsRenderIndices();

    /// <summary>Count of visible windows this frame.</summary>
    public static int MetricsRenderWindows => IGSharp_IO_GetMetricsRenderWindows();

    /// <summary>Count of active windows this frame.</summary>
    public static int MetricsActiveWindows => IGSharp_IO_GetMetricsActiveWindows();

    // --- Timing tunables ---

    /// <summary>Time in seconds for a double-click (default ~0.30).</summary>
    public static float MouseDoubleClickTime
    {
        get => IGSharp_IO_GetMouseDoubleClickTime();
        set => IGSharp_IO_SetMouseDoubleClickTime(value);
    }

    /// <summary>Maximum distance (in pixels) between two clicks to count as a double-click.</summary>
    public static float MouseDoubleClickMaxDist
    {
        get => IGSharp_IO_GetMouseDoubleClickMaxDist();
        set => IGSharp_IO_SetMouseDoubleClickMaxDist(value);
    }

    /// <summary>Drag distance threshold (in pixels) before a drag is registered.</summary>
    public static float MouseDragThreshold
    {
        get => IGSharp_IO_GetMouseDragThreshold();
        set => IGSharp_IO_SetMouseDragThreshold(value);
    }

    /// <summary>Delay (seconds) before a held key starts repeating.</summary>
    public static float KeyRepeatDelay
    {
        get => IGSharp_IO_GetKeyRepeatDelay();
        set => IGSharp_IO_SetKeyRepeatDelay(value);
    }

    /// <summary>Rate (seconds) at which a held key repeats after the initial delay.</summary>
    public static float KeyRepeatRate
    {
        get => IGSharp_IO_GetKeyRepeatRate();
        set => IGSharp_IO_SetKeyRepeatRate(value);
    }

    // --- Event injection (backends call these to forward OS events into ImGui) ---

    /// <summary>Queue a key down/up event.</summary>
    public static void AddKeyEvent(Key key, bool down) => IGSharp_IO_AddKeyEvent((int)key, down);

    /// <summary>Queue a key event with an analog value (e.g. gamepad trigger).</summary>
    public static void AddKeyAnalogEvent(Key key, bool down, float value) => IGSharp_IO_AddKeyAnalogEvent((int)key, down, value);

    /// <summary>Queue a mouse position event.</summary>
    public static void AddMousePosEvent(float x, float y) => IGSharp_IO_AddMousePosEvent(x, y);

    /// <summary>Queue a mouse button down/up event.</summary>
    public static void AddMouseButtonEvent(MouseButton button, bool down) => IGSharp_IO_AddMouseButtonEvent((int)button, down);

    /// <summary>Queue a mouse wheel scroll event (vertical + horizontal).</summary>
    public static void AddMouseWheelEvent(float wheelX, float wheelY) => IGSharp_IO_AddMouseWheelEvent(wheelX, wheelY);

    /// <summary>Queue a mouse source change (mouse / touch / pen). Call before <see cref="AddMousePosEvent"/>.</summary>
    public static void AddMouseSourceEvent(MouseSource source) => IGSharp_IO_AddMouseSourceEvent((int)source);

    /// <summary>Queue a window focus gain/loss event.</summary>
    public static void AddFocusEvent(bool focused) => IGSharp_IO_AddFocusEvent(focused);

    /// <summary>Queue a character input (Unicode scalar) for text widgets.</summary>
    public static void AddInputCharacter(uint c) => IGSharp_IO_AddInputCharacter(c);

    /// <summary>Queue a UTF-16 code unit input (combine surrogate pairs before calling).</summary>
    public static void AddInputCharacterUTF16(ushort c) => IGSharp_IO_AddInputCharacterUTF16(c);

    /// <summary>Queue a UTF-8 string input.</summary>
    public static void AddInputCharactersUTF8(string str) => IGSharp_IO_AddInputCharactersUTF8(ToUtf8(str));

    /// <summary>Enables or disables event acceptance. Pass false while the app is not focused to drop pending events.</summary>
    public static void SetAppAcceptingEvents(bool accepting) => IGSharp_IO_SetAppAcceptingEvents(accepting);

    /// <summary>Clears the pending event queue without processing.</summary>
    public static void ClearEventsQueue() => IGSharp_IO_ClearEventsQueue();

    /// <summary>Clears current keyboard state (useful when focus is lost).</summary>
    public static void ClearInputKeys() => IGSharp_IO_ClearInputKeys();

    /// <summary>Clears current mouse state.</summary>
    public static void ClearInputMouse() => IGSharp_IO_ClearInputMouse();
}
