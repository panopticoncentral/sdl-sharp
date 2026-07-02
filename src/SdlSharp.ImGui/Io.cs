using System.Runtime.InteropServices;
using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Accessors for the global ImGui <c>ImGuiIO</c> struct — display size, timing,
/// mouse/keyboard state, metrics, and event injection for custom backends.
/// </summary>
public static unsafe class Io
{
    // Keeps the unmanaged UTF-8 copy of the ini filename alive; ImGui stores the raw pointer.
    private static IntPtr _iniFilename;

    // --- Display ---

    /// <summary>Main display size in pixels. Must be set by the backend before <c>NewFrame</c>.</summary>
    public static Vec2 DisplaySize
    {
        get { var v = IGSharp_GetIO()->DisplaySize; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetIO()->DisplaySize = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Ratio of framebuffer pixels to display units (for Retina / HiDPI displays).</summary>
    public static Vec2 DisplayFramebufferScale
    {
        get { var v = IGSharp_GetIO()->DisplayFramebufferScale; return new Vec2(v.X, v.Y); }
        set => IGSharp_GetIO()->DisplayFramebufferScale = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Seconds since the last frame (time step for animation / timing).</summary>
    public static float DeltaTime
    {
        get => IGSharp_GetIO()->DeltaTime;
        set => IGSharp_GetIO()->DeltaTime = value;
    }

    // --- Mouse / Keyboard state (read-only) ---

    /// <summary>Current mouse position in screen coordinates.</summary>
    public static Vec2 MousePos
    {
        get { var v = IGSharp_GetIO()->MousePos; return new Vec2(v.X, v.Y); }
    }

    /// <summary>Mouse position delta since the last frame.</summary>
    public static Vec2 MouseDelta
    {
        get { var v = IGSharp_GetIO()->MouseDelta; return new Vec2(v.X, v.Y); }
    }

    /// <summary>Vertical mouse wheel delta.</summary>
    public static float MouseWheel => IGSharp_GetIO()->MouseWheel;

    /// <summary>Horizontal mouse wheel delta.</summary>
    public static float MouseWheelHorizontal => IGSharp_GetIO()->MouseWheelH;

    /// <summary>True if Ctrl is held this frame.</summary>
    public static bool KeyCtrl => IGSharp_GetIO()->KeyCtrl;

    /// <summary>True if Shift is held this frame.</summary>
    public static bool KeyShift => IGSharp_GetIO()->KeyShift;

    /// <summary>True if Alt is held this frame.</summary>
    public static bool KeyAlt => IGSharp_GetIO()->KeyAlt;

    /// <summary>True if Super (Windows / Cmd) is held this frame.</summary>
    public static bool KeySuper => IGSharp_GetIO()->KeySuper;

    // --- "Want" flags (signals to the backend) ---

    /// <summary>True if ImGui wants to capture mouse input (the backend should not also act on it).</summary>
    public static bool WantCaptureMouse => IGSharp_GetIO()->WantCaptureMouse;

    /// <summary>True if ImGui wants to capture keyboard input.</summary>
    public static bool WantCaptureKeyboard => IGSharp_GetIO()->WantCaptureKeyboard;

    /// <summary>True if ImGui wants a text-input UI (e.g. on-screen keyboard on mobile).</summary>
    public static bool WantTextInput => IGSharp_GetIO()->WantTextInput;

    /// <summary>True if ImGui wants the backend to reposition the mouse cursor.</summary>
    public static bool WantSetMousePos => IGSharp_GetIO()->WantSetMousePos;

    /// <summary>True if ImGui has unsaved settings (backend should save the ini file). Clear after saving.</summary>
    public static bool WantSaveIniSettings
    {
        get => IGSharp_GetIO()->WantSaveIniSettings;
        set => IGSharp_GetIO()->WantSaveIniSettings = value;
    }

    // --- Navigation state ---

    /// <summary>True if keyboard/gamepad navigation is currently active.</summary>
    public static bool NavActive => IGSharp_GetIO()->NavActive;

    /// <summary>True if the navigation highlight rectangle is currently visible.</summary>
    public static bool NavVisible => IGSharp_GetIO()->NavVisible;

    // --- Timing / Config ---

    /// <summary>Frame rate (computed as a running average).</summary>
    public static float Framerate => IGSharp_GetIO()->Framerate;

    /// <summary>Config flags (navigation, DPI awareness, etc.).</summary>
    public static ConfigFlags ConfigFlags
    {
        get => (ConfigFlags)IGSharp_GetIO()->ConfigFlags;
        set => IGSharp_GetIO()->ConfigFlags = (int)value;
    }

    /// <summary>Backend capability flags (set by the imgui_impl_* backend).</summary>
    public static BackendFlags BackendFlags
    {
        get => (BackendFlags)IGSharp_GetIO()->BackendFlags;
        set => IGSharp_GetIO()->BackendFlags = (int)value;
    }

    /// <summary>Sets the filename for saving/loading window layout. Pass null to disable persistence.</summary>
    public static void SetIniFilename(string? filename)
    {
        // ImGui stores the pointer (it does not copy the string), so keep the
        // unmanaged copy alive until it is replaced by a subsequent call.
        var ptr = Marshal.StringToCoTaskMemUTF8(filename);
        IGSharp_GetIO()->IniFilename = (byte*)ptr;
        if (_iniFilename != IntPtr.Zero)
        {
            Marshal.FreeCoTaskMem(_iniFilename);
        }
        _iniFilename = ptr;
    }

    // --- Metrics (read-only) ---

    /// <summary>Total vertex count submitted this frame.</summary>
    public static int MetricsRenderVertices => IGSharp_GetIO()->MetricsRenderVertices;

    /// <summary>Total index count submitted this frame.</summary>
    public static int MetricsRenderIndices => IGSharp_GetIO()->MetricsRenderIndices;

    /// <summary>Count of visible windows this frame.</summary>
    public static int MetricsRenderWindows => IGSharp_GetIO()->MetricsRenderWindows;

    /// <summary>Count of active windows this frame.</summary>
    public static int MetricsActiveWindows => IGSharp_GetIO()->MetricsActiveWindows;

    // --- Timing tunables ---

    /// <summary>Time in seconds for a double-click (default ~0.30).</summary>
    public static float MouseDoubleClickTime
    {
        get => IGSharp_GetIO()->MouseDoubleClickTime;
        set => IGSharp_GetIO()->MouseDoubleClickTime = value;
    }

    /// <summary>Maximum distance (in pixels) between two clicks to count as a double-click.</summary>
    public static float MouseDoubleClickMaxDist
    {
        get => IGSharp_GetIO()->MouseDoubleClickMaxDist;
        set => IGSharp_GetIO()->MouseDoubleClickMaxDist = value;
    }

    /// <summary>Drag distance threshold (in pixels) before a drag is registered.</summary>
    public static float MouseDragThreshold
    {
        get => IGSharp_GetIO()->MouseDragThreshold;
        set => IGSharp_GetIO()->MouseDragThreshold = value;
    }

    /// <summary>Delay (seconds) before a held key starts repeating.</summary>
    public static float KeyRepeatDelay
    {
        get => IGSharp_GetIO()->KeyRepeatDelay;
        set => IGSharp_GetIO()->KeyRepeatDelay = value;
    }

    /// <summary>Rate (seconds) at which a held key repeats after the initial delay.</summary>
    public static float KeyRepeatRate
    {
        get => IGSharp_GetIO()->KeyRepeatRate;
        set => IGSharp_GetIO()->KeyRepeatRate = value;
    }

    // --- Event injection (backends call these to forward OS events into ImGui) ---

    /// <summary>Queue a key down/up event.</summary>
    public static void AddKeyEvent(Key key, bool down) => IGSharp_IO_AddKeyEvent(IGSharp_GetIO(), (int)key, down);

    /// <summary>Queue a key event with an analog value (e.g. gamepad trigger).</summary>
    public static void AddKeyAnalogEvent(Key key, bool down, float value) => IGSharp_IO_AddKeyAnalogEvent(IGSharp_GetIO(), (int)key, down, value);

    /// <summary>Queue a mouse position event.</summary>
    public static void AddMousePosEvent(float x, float y) => IGSharp_IO_AddMousePosEvent(IGSharp_GetIO(), x, y);

    /// <summary>Queue a mouse button down/up event.</summary>
    public static void AddMouseButtonEvent(MouseButton button, bool down) => IGSharp_IO_AddMouseButtonEvent(IGSharp_GetIO(), (int)button, down);

    /// <summary>Queue a mouse wheel scroll event (vertical + horizontal).</summary>
    public static void AddMouseWheelEvent(float wheelX, float wheelY) => IGSharp_IO_AddMouseWheelEvent(IGSharp_GetIO(), wheelX, wheelY);

    /// <summary>Queue a mouse source change (mouse / touch / pen). Call before <see cref="AddMousePosEvent"/>.</summary>
    public static void AddMouseSourceEvent(MouseSource source) => IGSharp_IO_AddMouseSourceEvent(IGSharp_GetIO(), (int)source);

    /// <summary>Queue a window focus gain/loss event.</summary>
    public static void AddFocusEvent(bool focused) => IGSharp_IO_AddFocusEvent(IGSharp_GetIO(), focused);

    /// <summary>Queue a character input (Unicode scalar) for text widgets.</summary>
    public static void AddInputCharacter(uint c) => IGSharp_IO_AddInputCharacter(IGSharp_GetIO(), c);

    /// <summary>Queue a UTF-16 code unit input (combine surrogate pairs before calling).</summary>
    public static void AddInputCharacterUTF16(ushort c) => IGSharp_IO_AddInputCharacterUTF16(IGSharp_GetIO(), c);

    /// <summary>Queue a UTF-8 string input.</summary>
    public static void AddInputCharactersUTF8(string str) => IGSharp_IO_AddInputCharactersUTF8(IGSharp_GetIO(), ToUtf8(str));

    /// <summary>Enables or disables event acceptance. Pass false while the app is not focused to drop pending events.</summary>
    public static void SetAppAcceptingEvents(bool accepting) => IGSharp_IO_SetAppAcceptingEvents(IGSharp_GetIO(), accepting);

    /// <summary>Clears the pending event queue without processing.</summary>
    public static void ClearEventsQueue() => IGSharp_IO_ClearEventsQueue(IGSharp_GetIO());

    /// <summary>Clears current keyboard state (useful when focus is lost).</summary>
    public static void ClearInputKeys() => IGSharp_IO_ClearInputKeys(IGSharp_GetIO());

    /// <summary>Clears current mouse state.</summary>
    public static void ClearInputMouse() => IGSharp_IO_ClearInputMouse(IGSharp_GetIO());
}
