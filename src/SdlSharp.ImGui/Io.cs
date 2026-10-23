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
    private static IGSharp_IO* CurrentHandle { get { Context.Current.CheckCurrent(); return IGSharp_GetIO(); } }

    private static readonly ConcurrentDictionary<nint, nint> IniFilenames = new();

    /// <summary>Default color editor options. Supply the complete desired flag combination.</summary>
    public static ColorEditFlags ConfigColorEditFlags
    {
        get => (ColorEditFlags)CurrentHandle->ConfigColorEditFlags;
        set => CurrentHandle->ConfigColorEditFlags = (int)value;
    }

    // --- Display ---

    /// <summary>Main display size in pixels. Must be set by the backend before <c>NewFrame</c>.</summary>
    public static Vec2 DisplaySize
    {
        get { var v = CurrentHandle->DisplaySize; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->DisplaySize = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Ratio of framebuffer pixels to display units (for Retina / HiDPI displays).</summary>
    public static Vec2 DisplayFramebufferScale
    {
        get { var v = CurrentHandle->DisplayFramebufferScale; return new Vec2(v.X, v.Y); }
        set => CurrentHandle->DisplayFramebufferScale = new IGSharp_Vec2(value.X, value.Y);
    }

    /// <summary>Seconds since the last frame (time step for animation / timing).</summary>
    public static float DeltaTime
    {
        get => CurrentHandle->DeltaTime;
        set => CurrentHandle->DeltaTime = value;
    }

    // --- Mouse / Keyboard state (read-only) ---

    /// <summary>Current mouse position in screen coordinates.</summary>
    public static Vec2 MousePos
    {
        get { var v = CurrentHandle->MousePos; return new Vec2(v.X, v.Y); }
    }

    /// <summary>Mouse position delta since the last frame.</summary>
    public static Vec2 MouseDelta
    {
        get { var v = CurrentHandle->MouseDelta; return new Vec2(v.X, v.Y); }
    }

    /// <summary>Vertical mouse wheel delta.</summary>
    public static float MouseWheel => CurrentHandle->MouseWheel;

    /// <summary>Horizontal mouse wheel delta.</summary>
    public static float MouseWheelHorizontal => CurrentHandle->MouseWheelH;

    /// <summary>Seconds the mouse button has been held, or -1 when it is up.</summary>
    public static float GetMouseDownDuration(MouseButton button)
    {
        var index = (int)button;
        if ((uint)index >= 5) throw new ArgumentOutOfRangeException(nameof(button));
        return CurrentHandle->MouseDownDuration[index];
    }

    /// <summary>Mouse position where the most recent click for this button began.</summary>
    public static Vec2 GetMouseClickedPosition(MouseButton button)
    {
        var index = (int)button;
        if ((uint)index >= 5) throw new ArgumentOutOfRangeException(nameof(button));
        var position = CurrentHandle->MouseClickedPos[index];
        return new Vec2(position.X, position.Y);
    }

    /// <summary>Unicode input characters queued for the current frame.</summary>
    public static string InputQueueCharacters
    {
        get
        {
            var io = CurrentHandle;
            return io->InputQueueCharacters_Data == null || io->InputQueueCharacters_Size == 0
                ? string.Empty
                : new string((char*)io->InputQueueCharacters_Data, 0, io->InputQueueCharacters_Size);
        }
    }

    /// <summary>True if Ctrl is held this frame.</summary>
    public static bool KeyCtrl => CurrentHandle->KeyCtrl;

    /// <summary>True if Shift is held this frame.</summary>
    public static bool KeyShift => CurrentHandle->KeyShift;

    /// <summary>True if Alt is held this frame.</summary>
    public static bool KeyAlt => CurrentHandle->KeyAlt;

    /// <summary>True if Super (Windows / Cmd) is held this frame.</summary>
    public static bool KeySuper => CurrentHandle->KeySuper;

    // --- "Want" flags (signals to the backend) ---

    /// <summary>True if ImGui wants to capture mouse input (the backend should not also act on it).</summary>
    public static bool WantCaptureMouse => CurrentHandle->WantCaptureMouse;

    /// <summary>Capture intent excluding the click that closes a popup.</summary>
    public static bool WantCaptureMouseUnlessPopupClose => CurrentHandle->WantCaptureMouseUnlessPopupClose;

    /// <summary>True if ImGui wants to capture keyboard input.</summary>
    public static bool WantCaptureKeyboard => CurrentHandle->WantCaptureKeyboard;

    /// <summary>True if ImGui wants a text-input UI (e.g. on-screen keyboard on mobile).</summary>
    public static bool WantTextInput => CurrentHandle->WantTextInput;

    /// <summary>True if ImGui wants the backend to reposition the mouse cursor.</summary>
    public static bool WantSetMousePos => CurrentHandle->WantSetMousePos;

    /// <summary>True if ImGui has unsaved settings (backend should save the ini file). Clear after saving.</summary>
    public static bool WantSaveIniSettings
    {
        get => CurrentHandle->WantSaveIniSettings;
        set => CurrentHandle->WantSaveIniSettings = value;
    }

    // --- Navigation state ---

    /// <summary>True if keyboard/gamepad navigation is currently active.</summary>
    public static bool NavActive => CurrentHandle->NavActive;

    /// <summary>True if the navigation highlight rectangle is currently visible.</summary>
    public static bool NavVisible => CurrentHandle->NavVisible;

    // --- Timing / Config ---

    /// <summary>Frame rate (computed as a running average).</summary>
    public static float Framerate => CurrentHandle->Framerate;

    /// <summary>Config flags (navigation, DPI awareness, etc.).</summary>
    public static ConfigFlags ConfigFlags
    {
        get => (ConfigFlags)CurrentHandle->ConfigFlags;
        set => CurrentHandle->ConfigFlags = (int)value;
    }

    /// <summary>Backend capability flags (set by the imgui_impl_* backend).</summary>
    public static BackendFlags BackendFlags
    {
        get => (BackendFlags)CurrentHandle->BackendFlags;
        set => CurrentHandle->BackendFlags = (int)value;
    }

    /// <summary>Sets the filename for saving/loading window layout. Pass null to disable persistence.</summary>
    public static void SetIniFilename(string? filename)
    {
        var context = (nint)IGSharp_GetCurrentContext();
        if (context == 0)
            throw new InvalidOperationException("No current ImGui context.");
        var ptr = Marshal.StringToCoTaskMemUTF8(filename);
        CurrentHandle->IniFilename = (byte*)ptr;
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
    public static int MetricsRenderVertices => CurrentHandle->MetricsRenderVertices;

    /// <summary>Total index count submitted this frame.</summary>
    public static int MetricsRenderIndices => CurrentHandle->MetricsRenderIndices;

    /// <summary>Count of visible windows this frame.</summary>
    public static int MetricsRenderWindows => CurrentHandle->MetricsRenderWindows;

    /// <summary>Count of active windows this frame.</summary>
    public static int MetricsActiveWindows => CurrentHandle->MetricsActiveWindows;

    // --- Timing tunables ---

    /// <summary>Time in seconds for a double-click (default ~0.30).</summary>
    public static float MouseDoubleClickTime
    {
        get => CurrentHandle->MouseDoubleClickTime;
        set => CurrentHandle->MouseDoubleClickTime = value;
    }

    /// <summary>Maximum distance (in pixels) between two clicks to count as a double-click.</summary>
    public static float MouseDoubleClickMaxDist
    {
        get => CurrentHandle->MouseDoubleClickMaxDist;
        set => CurrentHandle->MouseDoubleClickMaxDist = value;
    }

    /// <summary>Drag distance threshold (in pixels) before a drag is registered.</summary>
    public static float MouseDragThreshold
    {
        get => CurrentHandle->MouseDragThreshold;
        set => CurrentHandle->MouseDragThreshold = value;
    }

    /// <summary>Delay (seconds) before a held key starts repeating.</summary>
    public static float KeyRepeatDelay
    {
        get => CurrentHandle->KeyRepeatDelay;
        set => CurrentHandle->KeyRepeatDelay = value;
    }

    /// <summary>Rate (seconds) at which a held key repeats after the initial delay.</summary>
    public static float KeyRepeatRate
    {
        get => CurrentHandle->KeyRepeatRate;
        set => CurrentHandle->KeyRepeatRate = value;
    }

    // --- Event injection (backends call these to forward OS events into ImGui) ---

    /// <summary>Queue a key down/up event.</summary>
    public static void AddKeyEvent(Key key, bool down) => IGSharp_IO_AddKeyEvent(CurrentHandle, (int)key, down);

    /// <summary>Queue a key event with an analog value (e.g. gamepad trigger).</summary>
    public static void AddKeyAnalogEvent(Key key, bool down, float value) => IGSharp_IO_AddKeyAnalogEvent(CurrentHandle, (int)key, down, value);

    /// <summary>Queue a mouse position event.</summary>
    public static void AddMousePosEvent(float x, float y) => IGSharp_IO_AddMousePosEvent(CurrentHandle, x, y);

    /// <summary>Queue a mouse button down/up event.</summary>
    public static void AddMouseButtonEvent(MouseButton button, bool down) => IGSharp_IO_AddMouseButtonEvent(CurrentHandle, (int)button, down);

    /// <summary>Queue a mouse wheel scroll event (vertical + horizontal).</summary>
    public static void AddMouseWheelEvent(float wheelX, float wheelY) => IGSharp_IO_AddMouseWheelEvent(CurrentHandle, wheelX, wheelY);

    /// <summary>Queue a mouse source change (mouse / touch / pen). Call before <see cref="AddMousePosEvent"/>.</summary>
    public static void AddMouseSourceEvent(MouseSource source) => IGSharp_IO_AddMouseSourceEvent(CurrentHandle, (int)source);

    /// <summary>Queue a window focus gain/loss event.</summary>
    public static void AddFocusEvent(bool focused) => IGSharp_IO_AddFocusEvent(CurrentHandle, focused);

    /// <summary>Queue a character input (Unicode scalar) for text widgets.</summary>
    public static void AddInputCharacter(uint c) => IGSharp_IO_AddInputCharacter(CurrentHandle, c);

    /// <summary>Queue a UTF-16 code unit input (combine surrogate pairs before calling).</summary>
    public static void AddInputCharacterUTF16(ushort c) => IGSharp_IO_AddInputCharacterUTF16(CurrentHandle, c);

    /// <summary>Queue a UTF-8 string input.</summary>
    public static void AddInputCharactersUTF8(string str) => IGSharp_IO_AddInputCharactersUTF8(CurrentHandle, ToUtf8(str));

    /// <summary>Enables or disables event acceptance. Pass false while the app is not focused to drop pending events.</summary>
    public static void SetAppAcceptingEvents(bool accepting) => IGSharp_IO_SetAppAcceptingEvents(CurrentHandle, accepting);

    /// <summary>Clears the pending event queue without processing.</summary>
    public static void ClearEventsQueue() => IGSharp_IO_ClearEventsQueue(CurrentHandle);

    /// <summary>Clears current keyboard state (useful when focus is lost).</summary>
    public static void ClearInputKeys() => IGSharp_IO_ClearInputKeys(CurrentHandle);

    /// <summary>Clears current mouse state.</summary>
    public static void ClearInputMouse() => IGSharp_IO_ClearInputMouse(CurrentHandle);

    // --- Configuration options (direct ImGuiIO mirror fields) ---

    /// <summary>Mirror of <c>ImGuiIO::FontAllowUserScaling</c>.</summary>
    public static bool FontAllowUserScaling
    {
        get => CurrentHandle->FontAllowUserScaling;
        set => CurrentHandle->FontAllowUserScaling = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavSwapGamepadButtons</c>.</summary>
    public static bool ConfigNavSwapGamepadButtons
    {
        get => CurrentHandle->ConfigNavSwapGamepadButtons;
        set => CurrentHandle->ConfigNavSwapGamepadButtons = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavMoveSetMousePos</c>.</summary>
    public static bool ConfigNavMoveSetMousePos
    {
        get => CurrentHandle->ConfigNavMoveSetMousePos;
        set => CurrentHandle->ConfigNavMoveSetMousePos = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavCaptureKeyboard</c>.</summary>
    public static bool ConfigNavCaptureKeyboard
    {
        get => CurrentHandle->ConfigNavCaptureKeyboard;
        set => CurrentHandle->ConfigNavCaptureKeyboard = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavEscapeClearFocusItem</c>.</summary>
    public static bool ConfigNavEscapeClearFocusItem
    {
        get => CurrentHandle->ConfigNavEscapeClearFocusItem;
        set => CurrentHandle->ConfigNavEscapeClearFocusItem = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavEscapeClearFocusWindow</c>.</summary>
    public static bool ConfigNavEscapeClearFocusWindow
    {
        get => CurrentHandle->ConfigNavEscapeClearFocusWindow;
        set => CurrentHandle->ConfigNavEscapeClearFocusWindow = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavCursorVisibleAuto</c>.</summary>
    public static bool ConfigNavCursorVisibleAuto
    {
        get => CurrentHandle->ConfigNavCursorVisibleAuto;
        set => CurrentHandle->ConfigNavCursorVisibleAuto = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigNavCursorVisibleAlways</c>.</summary>
    public static bool ConfigNavCursorVisibleAlways
    {
        get => CurrentHandle->ConfigNavCursorVisibleAlways;
        set => CurrentHandle->ConfigNavCursorVisibleAlways = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::MouseDrawCursor</c>.</summary>
    public static bool MouseDrawCursor
    {
        get => CurrentHandle->MouseDrawCursor;
        set => CurrentHandle->MouseDrawCursor = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigMacOSXBehaviors</c>.</summary>
    public static bool ConfigMacOSXBehaviors
    {
        get => CurrentHandle->ConfigMacOSXBehaviors;
        set => CurrentHandle->ConfigMacOSXBehaviors = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigInputTrickleEventQueue</c>.</summary>
    public static bool ConfigInputTrickleEventQueue
    {
        get => CurrentHandle->ConfigInputTrickleEventQueue;
        set => CurrentHandle->ConfigInputTrickleEventQueue = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigInputTextCursorBlink</c>.</summary>
    public static bool ConfigInputTextCursorBlink
    {
        get => CurrentHandle->ConfigInputTextCursorBlink;
        set => CurrentHandle->ConfigInputTextCursorBlink = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigInputTextEnterKeepActive</c>.</summary>
    public static bool ConfigInputTextEnterKeepActive
    {
        get => CurrentHandle->ConfigInputTextEnterKeepActive;
        set => CurrentHandle->ConfigInputTextEnterKeepActive = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDragClickToInputText</c>.</summary>
    public static bool ConfigDragClickToInputText
    {
        get => CurrentHandle->ConfigDragClickToInputText;
        set => CurrentHandle->ConfigDragClickToInputText = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigWindowsResizeFromEdges</c>.</summary>
    public static bool ConfigWindowsResizeFromEdges
    {
        get => CurrentHandle->ConfigWindowsResizeFromEdges;
        set => CurrentHandle->ConfigWindowsResizeFromEdges = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigWindowsMoveFromTitleBarOnly</c>.</summary>
    public static bool ConfigWindowsMoveFromTitleBarOnly
    {
        get => CurrentHandle->ConfigWindowsMoveFromTitleBarOnly;
        set => CurrentHandle->ConfigWindowsMoveFromTitleBarOnly = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigWindowsCopyContentsWithCtrlC</c>.</summary>
    public static bool ConfigWindowsCopyContentsWithCtrlC
    {
        get => CurrentHandle->ConfigWindowsCopyContentsWithCtrlC;
        set => CurrentHandle->ConfigWindowsCopyContentsWithCtrlC = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigScrollbarScrollByPage</c>.</summary>
    public static bool ConfigScrollbarScrollByPage
    {
        get => CurrentHandle->ConfigScrollbarScrollByPage;
        set => CurrentHandle->ConfigScrollbarScrollByPage = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigErrorRecovery</c>.</summary>
    public static bool ConfigErrorRecovery
    {
        get => CurrentHandle->ConfigErrorRecovery;
        set => CurrentHandle->ConfigErrorRecovery = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigErrorRecoveryEnableAssert</c>.</summary>
    public static bool ConfigErrorRecoveryEnableAssert
    {
        get => CurrentHandle->ConfigErrorRecoveryEnableAssert;
        set => CurrentHandle->ConfigErrorRecoveryEnableAssert = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigErrorRecoveryEnableDebugLog</c>.</summary>
    public static bool ConfigErrorRecoveryEnableDebugLog
    {
        get => CurrentHandle->ConfigErrorRecoveryEnableDebugLog;
        set => CurrentHandle->ConfigErrorRecoveryEnableDebugLog = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigErrorRecoveryEnableTooltip</c>.</summary>
    public static bool ConfigErrorRecoveryEnableTooltip
    {
        get => CurrentHandle->ConfigErrorRecoveryEnableTooltip;
        set => CurrentHandle->ConfigErrorRecoveryEnableTooltip = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugIsDebuggerPresent</c>.</summary>
    public static bool ConfigDebugIsDebuggerPresent
    {
        get => CurrentHandle->ConfigDebugIsDebuggerPresent;
        set => CurrentHandle->ConfigDebugIsDebuggerPresent = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugHighlightIdConflicts</c>.</summary>
    public static bool ConfigDebugHighlightIdConflicts
    {
        get => CurrentHandle->ConfigDebugHighlightIdConflicts;
        set => CurrentHandle->ConfigDebugHighlightIdConflicts = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugHighlightIdConflictsShowItemPicker</c>.</summary>
    public static bool ConfigDebugHighlightIdConflictsShowItemPicker
    {
        get => CurrentHandle->ConfigDebugHighlightIdConflictsShowItemPicker;
        set => CurrentHandle->ConfigDebugHighlightIdConflictsShowItemPicker = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugBeginReturnValueOnce</c>.</summary>
    public static bool ConfigDebugBeginReturnValueOnce
    {
        get => CurrentHandle->ConfigDebugBeginReturnValueOnce;
        set => CurrentHandle->ConfigDebugBeginReturnValueOnce = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugBeginReturnValueLoop</c>.</summary>
    public static bool ConfigDebugBeginReturnValueLoop
    {
        get => CurrentHandle->ConfigDebugBeginReturnValueLoop;
        set => CurrentHandle->ConfigDebugBeginReturnValueLoop = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugIgnoreFocusLoss</c>.</summary>
    public static bool ConfigDebugIgnoreFocusLoss
    {
        get => CurrentHandle->ConfigDebugIgnoreFocusLoss;
        set => CurrentHandle->ConfigDebugIgnoreFocusLoss = value;
    }

    /// <summary>Mirror of <c>ImGuiIO::ConfigDebugIniSettings</c>.</summary>
    public static bool ConfigDebugIniSettings
    {
        get => CurrentHandle->ConfigDebugIniSettings;
        set => CurrentHandle->ConfigDebugIniSettings = value;
    }

    /// <summary>Minimum time between saving the .ini state, in seconds.</summary>
    public static float IniSavingRate
    {
        get => CurrentHandle->IniSavingRate;
        set => CurrentHandle->IniSavingRate = value;
    }

    /// <summary>Timer to free transient windows/tables memory buffers when unused (seconds).</summary>
    public static float ConfigMemoryCompactTimer
    {
        get => CurrentHandle->ConfigMemoryCompactTimer;
        set => CurrentHandle->ConfigMemoryCompactTimer = value;
    }

    /// <summary>Name of the platform backend, if set (e.g. "imgui_impl_sdl3").</summary>
    public static string? BackendPlatformName => Marshal.PtrToStringUTF8((IntPtr)CurrentHandle->BackendPlatformName);

    /// <summary>Name of the renderer backend, if set (e.g. "imgui_impl_sdlgpu3").</summary>
    public static string? BackendRendererName => Marshal.PtrToStringUTF8((IntPtr)CurrentHandle->BackendRendererName);
    /// <summary>ImGui IO.ConfigDebugDrawListDefaultsToStrokeLegacy configuration.</summary>
    public static bool ConfigDebugDrawListDefaultsToStrokeLegacy
    {
        get => CurrentHandle->ConfigDebugDrawListDefaultsToStrokeLegacy;
        set => CurrentHandle->ConfigDebugDrawListDefaultsToStrokeLegacy = value;
    }

    /// <summary>ImGui IO.MouseSingleClickDelay configuration.</summary>
    public static float MouseSingleClickDelay
    {
        get => CurrentHandle->MouseSingleClickDelay;
        set => CurrentHandle->MouseSingleClickDelay = value;
    }

    /// <summary>ImGui IO.ConfigIniSettingsAutoDiscardMonths configuration.</summary>
    public static int ConfigIniSettingsAutoDiscardMonths
    {
        get => CurrentHandle->ConfigIniSettingsAutoDiscardMonths;
        set => CurrentHandle->ConfigIniSettingsAutoDiscardMonths = value;
    }

    /// <summary>ImGui IO.ConfigIniSettingsSaveLastUsedDate configuration.</summary>
    public static bool ConfigIniSettingsSaveLastUsedDate
    {
        get => CurrentHandle->ConfigIniSettingsSaveLastUsedDate;
        set => CurrentHandle->ConfigIniSettingsSaveLastUsedDate = value;
    }

    /// <summary>Associates a submitted key event with native platform codes for legacy integrations.</summary>
    public static void SetKeyEventNativeData(Key key, int keycode, int scancode, int legacyIndex = -1)
        => IGSharp_IO_SetKeyEventNativeData(CurrentHandle, (int)key, keycode, scancode, legacyIndex);

}
