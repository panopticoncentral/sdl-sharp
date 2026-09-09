using System.Collections.Concurrent;
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
    private static readonly ConcurrentDictionary<nint, nint> IniFilenames = new();

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

    /// <summary>Seconds the mouse button has been held, or -1 when it is up.</summary>
    public static float GetMouseDownDuration(MouseButton button)
    {
        var index = (int)button;
        if ((uint)index >= 5) throw new ArgumentOutOfRangeException(nameof(button));
        return IGSharp_GetIO()->MouseDownDuration[index];
    }

    /// <summary>Mouse position where the most recent click for this button began.</summary>
    public static Vec2 GetMouseClickedPosition(MouseButton button)
    {
        var index = (int)button;
        if ((uint)index >= 5) throw new ArgumentOutOfRangeException(nameof(button));
        var position = IGSharp_GetIO()->MouseClickedPos[index];
        return new Vec2(position.X, position.Y);
    }

    /// <summary>Unicode input characters queued for the current frame.</summary>
    public static string InputQueueCharacters
    {
        get
        {
            var io = IGSharp_GetIO();
            return io->InputQueueCharacters_Data == null || io->InputQueueCharacters_Size == 0
                ? string.Empty
                : new string((char*)io->InputQueueCharacters_Data, 0, io->InputQueueCharacters_Size);
        }
    }

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

    /// <summary>Capture intent excluding the click that closes a popup.</summary>
    public static bool WantCaptureMouseUnlessPopupClose => IGSharp_GetIO()->WantCaptureMouseUnlessPopupClose;

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
        var context = (nint)IGSharp_GetCurrentContext();
        if (context == 0)
            throw new InvalidOperationException("No current ImGui context.");
        var ptr = Marshal.StringToCoTaskMemUTF8(filename);
        IGSharp_GetIO()->IniFilename = (byte*)ptr;
        if (IniFilenames.TryGetValue(context, out var previous) && previous != 0)
            Marshal.FreeCoTaskMem(previous);
        if (ptr == IntPtr.Zero)
            IniFilenames.TryRemove(context, out _);
        else
            IniFilenames[context] = ptr;
    }

    internal static void ReleaseContext(nint context)
    {
        if (IniFilenames.TryRemove(context, out var filename) && filename != 0)
            Marshal.FreeCoTaskMem(filename);
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

    // --- Configuration options (direct ImGuiIO mirror fields) ---

    /// <summary>Mirror of <c>ImGuiIO::FontAllowUserScaling</c>.</summary>
    public static bool FontAllowUserScaling
    {
        get => IGSharp_GetIO()->FontAllowUserScaling;
        set => IGSharp_GetIO()->FontAllowUserScaling = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavSwapGamepadButtons</c>.</summary>
    public static bool ConfigNavSwapGamepadButtons
    {
        get => IGSharp_GetIO()->ConfigNavSwapGamepadButtons;
        set => IGSharp_GetIO()->ConfigNavSwapGamepadButtons = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavMoveSetMousePos</c>.</summary>
    public static bool ConfigNavMoveSetMousePos
    {
        get => IGSharp_GetIO()->ConfigNavMoveSetMousePos;
        set => IGSharp_GetIO()->ConfigNavMoveSetMousePos = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavCaptureKeyboard</c>.</summary>
    public static bool ConfigNavCaptureKeyboard
    {
        get => IGSharp_GetIO()->ConfigNavCaptureKeyboard;
        set => IGSharp_GetIO()->ConfigNavCaptureKeyboard = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavEscapeClearFocusItem</c>.</summary>
    public static bool ConfigNavEscapeClearFocusItem
    {
        get => IGSharp_GetIO()->ConfigNavEscapeClearFocusItem;
        set => IGSharp_GetIO()->ConfigNavEscapeClearFocusItem = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavEscapeClearFocusWindow</c>.</summary>
    public static bool ConfigNavEscapeClearFocusWindow
    {
        get => IGSharp_GetIO()->ConfigNavEscapeClearFocusWindow;
        set => IGSharp_GetIO()->ConfigNavEscapeClearFocusWindow = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavCursorVisibleAuto</c>.</summary>
    public static bool ConfigNavCursorVisibleAuto
    {
        get => IGSharp_GetIO()->ConfigNavCursorVisibleAuto;
        set => IGSharp_GetIO()->ConfigNavCursorVisibleAuto = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavCursorVisibleAlways</c>.</summary>
    public static bool ConfigNavCursorVisibleAlways
    {
        get => IGSharp_GetIO()->ConfigNavCursorVisibleAlways;
        set => IGSharp_GetIO()->ConfigNavCursorVisibleAlways = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::MouseDrawCursor</c>.</summary>
    public static bool MouseDrawCursor
    {
        get => IGSharp_GetIO()->MouseDrawCursor;
        set => IGSharp_GetIO()->MouseDrawCursor = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigMacOSXBehaviors</c>.</summary>
    public static bool ConfigMacOSXBehaviors
    {
        get => IGSharp_GetIO()->ConfigMacOSXBehaviors;
        set => IGSharp_GetIO()->ConfigMacOSXBehaviors = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigInputTrickleEventQueue</c>.</summary>
    public static bool ConfigInputTrickleEventQueue
    {
        get => IGSharp_GetIO()->ConfigInputTrickleEventQueue;
        set => IGSharp_GetIO()->ConfigInputTrickleEventQueue = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigInputTextCursorBlink</c>.</summary>
    public static bool ConfigInputTextCursorBlink
    {
        get => IGSharp_GetIO()->ConfigInputTextCursorBlink;
        set => IGSharp_GetIO()->ConfigInputTextCursorBlink = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigInputTextEnterKeepActive</c>.</summary>
    public static bool ConfigInputTextEnterKeepActive
    {
        get => IGSharp_GetIO()->ConfigInputTextEnterKeepActive;
        set => IGSharp_GetIO()->ConfigInputTextEnterKeepActive = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDragClickToInputText</c>.</summary>
    public static bool ConfigDragClickToInputText
    {
        get => IGSharp_GetIO()->ConfigDragClickToInputText;
        set => IGSharp_GetIO()->ConfigDragClickToInputText = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigWindowsResizeFromEdges</c>.</summary>
    public static bool ConfigWindowsResizeFromEdges
    {
        get => IGSharp_GetIO()->ConfigWindowsResizeFromEdges;
        set => IGSharp_GetIO()->ConfigWindowsResizeFromEdges = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigWindowsMoveFromTitleBarOnly</c>.</summary>
    public static bool ConfigWindowsMoveFromTitleBarOnly
    {
        get => IGSharp_GetIO()->ConfigWindowsMoveFromTitleBarOnly;
        set => IGSharp_GetIO()->ConfigWindowsMoveFromTitleBarOnly = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigWindowsCopyContentsWithCtrlC</c>.</summary>
    public static bool ConfigWindowsCopyContentsWithCtrlC
    {
        get => IGSharp_GetIO()->ConfigWindowsCopyContentsWithCtrlC;
        set => IGSharp_GetIO()->ConfigWindowsCopyContentsWithCtrlC = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigScrollbarScrollByPage</c>.</summary>
    public static bool ConfigScrollbarScrollByPage
    {
        get => IGSharp_GetIO()->ConfigScrollbarScrollByPage;
        set => IGSharp_GetIO()->ConfigScrollbarScrollByPage = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigErrorRecovery</c>.</summary>
    public static bool ConfigErrorRecovery
    {
        get => IGSharp_GetIO()->ConfigErrorRecovery;
        set => IGSharp_GetIO()->ConfigErrorRecovery = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigErrorRecoveryEnableAssert</c>.</summary>
    public static bool ConfigErrorRecoveryEnableAssert
    {
        get => IGSharp_GetIO()->ConfigErrorRecoveryEnableAssert;
        set => IGSharp_GetIO()->ConfigErrorRecoveryEnableAssert = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigErrorRecoveryEnableDebugLog</c>.</summary>
    public static bool ConfigErrorRecoveryEnableDebugLog
    {
        get => IGSharp_GetIO()->ConfigErrorRecoveryEnableDebugLog;
        set => IGSharp_GetIO()->ConfigErrorRecoveryEnableDebugLog = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigErrorRecoveryEnableTooltip</c>.</summary>
    public static bool ConfigErrorRecoveryEnableTooltip
    {
        get => IGSharp_GetIO()->ConfigErrorRecoveryEnableTooltip;
        set => IGSharp_GetIO()->ConfigErrorRecoveryEnableTooltip = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugIsDebuggerPresent</c>.</summary>
    public static bool ConfigDebugIsDebuggerPresent
    {
        get => IGSharp_GetIO()->ConfigDebugIsDebuggerPresent;
        set => IGSharp_GetIO()->ConfigDebugIsDebuggerPresent = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugHighlightIdConflicts</c>.</summary>
    public static bool ConfigDebugHighlightIdConflicts
    {
        get => IGSharp_GetIO()->ConfigDebugHighlightIdConflicts;
        set => IGSharp_GetIO()->ConfigDebugHighlightIdConflicts = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugHighlightIdConflictsShowItemPicker</c>.</summary>
    public static bool ConfigDebugHighlightIdConflictsShowItemPicker
    {
        get => IGSharp_GetIO()->ConfigDebugHighlightIdConflictsShowItemPicker;
        set => IGSharp_GetIO()->ConfigDebugHighlightIdConflictsShowItemPicker = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugBeginReturnValueOnce</c>.</summary>
    public static bool ConfigDebugBeginReturnValueOnce
    {
        get => IGSharp_GetIO()->ConfigDebugBeginReturnValueOnce;
        set => IGSharp_GetIO()->ConfigDebugBeginReturnValueOnce = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugBeginReturnValueLoop</c>.</summary>
    public static bool ConfigDebugBeginReturnValueLoop
    {
        get => IGSharp_GetIO()->ConfigDebugBeginReturnValueLoop;
        set => IGSharp_GetIO()->ConfigDebugBeginReturnValueLoop = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugIgnoreFocusLoss</c>.</summary>
    public static bool ConfigDebugIgnoreFocusLoss
    {
        get => IGSharp_GetIO()->ConfigDebugIgnoreFocusLoss;
        set => IGSharp_GetIO()->ConfigDebugIgnoreFocusLoss = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugIniSettings</c>.</summary>
    public static bool ConfigDebugIniSettings
    {
        get => IGSharp_GetIO()->ConfigDebugIniSettings;
        set => IGSharp_GetIO()->ConfigDebugIniSettings = value;
    }

    /// <summary>Minimum time between saving the .ini state, in seconds.</summary>
    public static float IniSavingRate
    {
        get => IGSharp_GetIO()->IniSavingRate;
        set => IGSharp_GetIO()->IniSavingRate = value;
    }

    /// <summary>Timer to free transient windows/tables memory buffers when unused (seconds).</summary>
    public static float ConfigMemoryCompactTimer
    {
        get => IGSharp_GetIO()->ConfigMemoryCompactTimer;
        set => IGSharp_GetIO()->ConfigMemoryCompactTimer = value;
    }

    /// <summary>Name of the platform backend, if set (e.g. "imgui_impl_sdl3").</summary>
    public static string? BackendPlatformName => Marshal.PtrToStringUTF8((IntPtr)IGSharp_GetIO()->BackendPlatformName);

    /// <summary>Name of the renderer backend, if set (e.g. "imgui_impl_sdlgpu3").</summary>
    public static string? BackendRendererName => Marshal.PtrToStringUTF8((IntPtr)IGSharp_GetIO()->BackendRendererName);
}
