using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents the ImGui IO configuration and state wrapper.
/// </summary>
public unsafe sealed class IO
{
    internal ImGuiIO* Native { get; }

    internal IO(ImGuiIO* native)
    {
        Native = native;
    }

    /// <summary>
    /// Gets or sets the configuration flags.
    /// </summary>
    /// <seealso cref="ConfigFlags"/>
    public ConfigFlags ConfigFlags
    {
        get => (ConfigFlags)Native->ConfigFlags;
        set => Native->ConfigFlags = (ImGuiConfigFlags)value;
    }

    /// <summary>
    /// Gets or sets the backend flags.
    /// </summary>
    /// <seealso cref="BackendFlags"/>
    public BackendFlags BackendFlags
    {
        get => (BackendFlags)Native->BackendFlags;
        set => Native->BackendFlags = (ImGuiBackendFlags)value;
    }

    /// <summary>
    /// Gets or sets the main display size in pixels.
    /// </summary>
    public Vec2 DisplaySize
    {
        get => Vec2.FromNative(Native->DisplaySize);
        set => Native->DisplaySize = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the display framebuffer scale for retina displays.
    /// </summary>
    public Vec2 DisplayFramebufferScale
    {
        get => Vec2.FromNative(Native->DisplayFramebufferScale);
        set => Native->DisplayFramebufferScale = value.ToNative();
    }

    /// <summary>
    /// Gets or sets the time elapsed since last frame, in seconds.
    /// </summary>
    public float DeltaTime
    {
        get => Native->DeltaTime;
        set => Native->DeltaTime = value;
    }

    /// <summary>
    /// Gets or sets the minimum time between saving positions/sizes to .ini file, in seconds.
    /// </summary>
    public float IniSavingRate
    {
        get => Native->IniSavingRate;
        set => Native->IniSavingRate = value;
    }

    /// <summary>
    /// Gets or sets custom user data for the IO.
    /// </summary>
    public nint UserData
    {
        get => Native->UserData;
        set => Native->UserData = value;
    }

    /// <summary>
    /// Gets the font atlas.
    /// </summary>
    public FontAtlas Fonts => new(Native->Fonts);

    /// <summary>
    /// Gets or sets whether to allow user scaling text of individual windows with Ctrl+Wheel.
    /// </summary>
    public bool FontAllowUserScaling
    {
        get => Native->FontAllowUserScaling;
        set => Native->FontAllowUserScaling = value;
    }

    /// <summary>
    /// Gets or sets whether to swap Activate/Cancel (A/B) buttons for gamepad navigation.
    /// </summary>
    public bool ConfigNavSwapGamepadButtons
    {
        get => Native->ConfigNavSwapGamepadButtons;
        set => Native->ConfigNavSwapGamepadButtons = value;
    }

    /// <summary>
    /// Gets or sets whether directional/tabbing navigation teleports the mouse cursor.
    /// </summary>
    public bool ConfigNavMoveSetMousePos
    {
        get => Native->ConfigNavMoveSetMousePos;
        set => Native->ConfigNavMoveSetMousePos = value;
    }

    /// <summary>
    /// Gets or sets whether io.WantCaptureKeyboard is set when io.NavActive is set.
    /// </summary>
    public bool ConfigNavCaptureKeyboard
    {
        get => Native->ConfigNavCaptureKeyboard;
        set => Native->ConfigNavCaptureKeyboard = value;
    }

    /// <summary>
    /// Gets or sets whether pressing Escape can clear focused item and navigation id/highlight.
    /// </summary>
    public bool ConfigNavEscapeClearFocusItem
    {
        get => Native->ConfigNavEscapeClearFocusItem;
        set => Native->ConfigNavEscapeClearFocusItem = value;
    }

    /// <summary>
    /// Gets or sets whether pressing Escape can clear focused window as well.
    /// </summary>
    public bool ConfigNavEscapeClearFocusWindow
    {
        get => Native->ConfigNavEscapeClearFocusWindow;
        set => Native->ConfigNavEscapeClearFocusWindow = value;
    }

    /// <summary>
    /// Gets or sets whether using directional navigation key makes the cursor visible.
    /// </summary>
    public bool ConfigNavCursorVisibleAuto
    {
        get => Native->ConfigNavCursorVisibleAuto;
        set => Native->ConfigNavCursorVisibleAuto = value;
    }

    /// <summary>
    /// Gets or sets whether the navigation cursor is always visible.
    /// </summary>
    public bool ConfigNavCursorVisibleAlways
    {
        get => Native->ConfigNavCursorVisibleAlways;
        set => Native->ConfigNavCursorVisibleAlways = value;
    }

    /// <summary>
    /// Gets or sets whether ImGui should draw a mouse cursor for you.
    /// </summary>
    public bool MouseDrawCursor
    {
        get => Native->MouseDrawCursor;
        set => Native->MouseDrawCursor = value;
    }

    /// <summary>
    /// Gets or sets whether to use macOS-style behaviors (Cmd/Ctrl swap, Alt text editing, etc.).
    /// </summary>
    public bool ConfigMacOSXBehaviors
    {
        get => Native->ConfigMacOSXBehaviors;
        set => Native->ConfigMacOSXBehaviors = value;
    }

    /// <summary>
    /// Gets or sets whether to enable input queue trickling.
    /// </summary>
    public bool ConfigInputTrickleEventQueue
    {
        get => Native->ConfigInputTrickleEventQueue;
        set => Native->ConfigInputTrickleEventQueue = value;
    }

    /// <summary>
    /// Gets or sets whether to enable blinking cursor.
    /// </summary>
    public bool ConfigInputTextCursorBlink
    {
        get => Native->ConfigInputTextCursorBlink;
        set => Native->ConfigInputTextCursorBlink = value;
    }

    /// <summary>
    /// Gets or sets whether pressing Enter keeps item active and selects contents (single-line only).
    /// </summary>
    public bool ConfigInputTextEnterKeepActive
    {
        get => Native->ConfigInputTextEnterKeepActive;
        set => Native->ConfigInputTextEnterKeepActive = value;
    }

    /// <summary>
    /// Gets or sets whether clicking on DragXXX widgets turns them into text input.
    /// </summary>
    public bool ConfigDragClickToInputText
    {
        get => Native->ConfigDragClickToInputText;
        set => Native->ConfigDragClickToInputText = value;
    }

    /// <summary>
    /// Gets or sets whether to enable resizing of windows from their edges.
    /// </summary>
    public bool ConfigWindowsResizeFromEdges
    {
        get => Native->ConfigWindowsResizeFromEdges;
        set => Native->ConfigWindowsResizeFromEdges = value;
    }

    /// <summary>
    /// Gets or sets whether windows can only be moved by clicking on their title bar.
    /// </summary>
    public bool ConfigWindowsMoveFromTitleBarOnly
    {
        get => Native->ConfigWindowsMoveFromTitleBarOnly;
        set => Native->ConfigWindowsMoveFromTitleBarOnly = value;
    }

    /// <summary>
    /// Gets or sets whether Ctrl+C copies the contents of focused window into the clipboard.
    /// </summary>
    public bool ConfigWindowsCopyContentsWithCtrlC
    {
        get => Native->ConfigWindowsCopyContentsWithCtrlC;
        set => Native->ConfigWindowsCopyContentsWithCtrlC = value;
    }

    /// <summary>
    /// Gets or sets whether to enable scrolling page by page when clicking outside the scrollbar grab.
    /// </summary>
    public bool ConfigScrollbarScrollByPage
    {
        get => Native->ConfigScrollbarScrollByPage;
        set => Native->ConfigScrollbarScrollByPage = value;
    }

    /// <summary>
    /// Gets or sets the timer (in seconds) to free transient windows/tables memory buffers when unused.
    /// </summary>
    public float ConfigMemoryCompactTimer
    {
        get => Native->ConfigMemoryCompactTimer;
        set => Native->ConfigMemoryCompactTimer = value;
    }

    /// <summary>
    /// Gets or sets the time for a double-click, in seconds.
    /// </summary>
    public float MouseDoubleClickTime
    {
        get => Native->MouseDoubleClickTime;
        set => Native->MouseDoubleClickTime = value;
    }

    /// <summary>
    /// Gets or sets the distance threshold to stay in to validate a double-click, in pixels.
    /// </summary>
    public float MouseDoubleClickMaxDist
    {
        get => Native->MouseDoubleClickMaxDist;
        set => Native->MouseDoubleClickMaxDist = value;
    }

    /// <summary>
    /// Gets or sets the distance threshold before considering we are dragging.
    /// </summary>
    public float MouseDragThreshold
    {
        get => Native->MouseDragThreshold;
        set => Native->MouseDragThreshold = value;
    }

    /// <summary>
    /// Gets or sets the time before key/button repeat starts, in seconds.
    /// </summary>
    public float KeyRepeatDelay
    {
        get => Native->KeyRepeatDelay;
        set => Native->KeyRepeatDelay = value;
    }

    /// <summary>
    /// Gets or sets the rate at which key/button repeats, in seconds.
    /// </summary>
    public float KeyRepeatRate
    {
        get => Native->KeyRepeatRate;
        set => Native->KeyRepeatRate = value;
    }

    /// <summary>
    /// Gets or sets whether to enable error recovery support.
    /// </summary>
    public bool ConfigErrorRecovery
    {
        get => Native->ConfigErrorRecovery;
        set => Native->ConfigErrorRecovery = value;
    }

    /// <summary>
    /// Gets or sets whether to enable asserts on recoverable errors.
    /// </summary>
    public bool ConfigErrorRecoveryEnableAssert
    {
        get => Native->ConfigErrorRecoveryEnableAssert;
        set => Native->ConfigErrorRecoveryEnableAssert = value;
    }

    /// <summary>
    /// Gets or sets whether to enable debug log output on recoverable errors.
    /// </summary>
    public bool ConfigErrorRecoveryEnableDebugLog
    {
        get => Native->ConfigErrorRecoveryEnableDebugLog;
        set => Native->ConfigErrorRecoveryEnableDebugLog = value;
    }

    /// <summary>
    /// Gets or sets whether to enable tooltip on recoverable errors.
    /// </summary>
    public bool ConfigErrorRecoveryEnableTooltip
    {
        get => Native->ConfigErrorRecoveryEnableTooltip;
        set => Native->ConfigErrorRecoveryEnableTooltip = value;
    }

    /// <summary>
    /// Gets or sets whether a debugger is present.
    /// </summary>
    public bool ConfigDebugIsDebuggerPresent
    {
        get => Native->ConfigDebugIsDebuggerPresent;
        set => Native->ConfigDebugIsDebuggerPresent = value;
    }

    /// <summary>
    /// Gets or sets whether to highlight and show error message popup when multiple items have conflicting identifiers.
    /// </summary>
    public bool ConfigDebugHighlightIdConflicts
    {
        get => Native->ConfigDebugHighlightIdConflicts;
        set => Native->ConfigDebugHighlightIdConflicts = value;
    }

    /// <summary>
    /// Gets or sets whether to show "Item Picker" button in ID conflict popup.
    /// </summary>
    public bool ConfigDebugHighlightIdConflictsShowItemPicker
    {
        get => Native->ConfigDebugHighlightIdConflictsShowItemPicker;
        set => Native->ConfigDebugHighlightIdConflictsShowItemPicker = value;
    }

    /// <summary>
    /// Gets or sets whether first-time calls to Begin()/BeginChild() will return false.
    /// </summary>
    public bool ConfigDebugBeginReturnValueOnce
    {
        get => Native->ConfigDebugBeginReturnValueOnce;
        set => Native->ConfigDebugBeginReturnValueOnce = value;
    }

    /// <summary>
    /// Gets or sets whether some calls to Begin()/BeginChild() will return false (cycles through window depths).
    /// </summary>
    public bool ConfigDebugBeginReturnValueLoop
    {
        get => Native->ConfigDebugBeginReturnValueLoop;
        set => Native->ConfigDebugBeginReturnValueLoop = value;
    }

    /// <summary>
    /// Gets or sets whether to ignore io.AddFocusEvent(false).
    /// </summary>
    public bool ConfigDebugIgnoreFocusLoss
    {
        get => Native->ConfigDebugIgnoreFocusLoss;
        set => Native->ConfigDebugIgnoreFocusLoss = value;
    }

    /// <summary>
    /// Gets or sets whether to save .ini data with extra comments.
    /// </summary>
    public bool ConfigDebugIniSettings
    {
        get => Native->ConfigDebugIniSettings;
        set => Native->ConfigDebugIniSettings = value;
    }

    /// <summary>
    /// Gets the backend platform name.
    /// </summary>
    public string? BackendPlatformName => Marshal.PtrToStringUTF8((nint)Native->BackendPlatformName);

    /// <summary>
    /// Gets the backend renderer name.
    /// </summary>
    public string? BackendRendererName => Marshal.PtrToStringUTF8((nint)Native->BackendRendererName);

    /// <summary>
    /// Gets or sets the user data for the platform backend.
    /// </summary>
    public nint BackendPlatformUserData
    {
        get => Native->BackendPlatformUserData;
        set => Native->BackendPlatformUserData = value;
    }

    /// <summary>
    /// Gets or sets the user data for the renderer backend.
    /// </summary>
    public nint BackendRendererUserData
    {
        get => Native->BackendRendererUserData;
        set => Native->BackendRendererUserData = value;
    }

    /// <summary>
    /// Gets or sets the user data for non-C++ programming language backend.
    /// </summary>
    public nint BackendLanguageUserData
    {
        get => Native->BackendLanguageUserData;
        set => Native->BackendLanguageUserData = value;
    }

    /// <summary>
    /// Gets whether Dear ImGui will use mouse inputs.
    /// </summary>
    public bool WantCaptureMouse => Native->WantCaptureMouse;

    /// <summary>
    /// Gets whether Dear ImGui will use keyboard inputs.
    /// </summary>
    public bool WantCaptureKeyboard => Native->WantCaptureKeyboard;

    /// <summary>
    /// Gets whether an on-screen keyboard should be displayed.
    /// </summary>
    public bool WantTextInput => Native->WantTextInput;

    /// <summary>
    /// Gets whether the mouse position has been altered.
    /// </summary>
    public bool WantSetMousePos => Native->WantSetMousePos;

    /// <summary>
    /// Gets or sets whether .ini settings should be saved.
    /// </summary>
    public bool WantSaveIniSettings
    {
        get => Native->WantSaveIniSettings;
        set => Native->WantSaveIniSettings = value;
    }

    /// <summary>
    /// Gets whether keyboard/gamepad navigation is currently allowed.
    /// </summary>
    public bool NavActive => Native->NavActive;

    /// <summary>
    /// Gets whether keyboard/gamepad navigation highlight is visible and allowed.
    /// </summary>
    public bool NavVisible => Native->NavVisible;

    /// <summary>
    /// Gets the estimate of application framerate (rolling average over 60 frames).
    /// </summary>
    public float Framerate => Native->Framerate;

    /// <summary>
    /// Gets the number of vertices output during last call to Render().
    /// </summary>
    public int MetricsRenderVertices => Native->MetricsRenderVertices;

    /// <summary>
    /// Gets the number of indices output during last call to Render().
    /// </summary>
    public int MetricsRenderIndices => Native->MetricsRenderIndices;

    /// <summary>
    /// Gets the number of visible windows.
    /// </summary>
    public int MetricsRenderWindows => Native->MetricsRenderWindows;

    /// <summary>
    /// Gets the number of active windows.
    /// </summary>
    public int MetricsActiveWindows => Native->MetricsActiveWindows;

    /// <summary>
    /// Gets the mouse delta.
    /// </summary>
    public Vec2 MouseDelta => Vec2.FromNative(Native->MouseDelta);

    /// <summary>
    /// Queues a new key down/up event.
    /// </summary>
    /// <param name="key">The key that was pressed or released.</param>
    /// <param name="down">True if the key is down; false if up.</param>
    public void AddKeyEvent(Key key, bool down)
    {
        ImGuiIO.AddKeyEvent(Native, (ImGuiKey)key, down);
    }

    /// <summary>
    /// Queues a new key down/up event for analog values (e.g., gamepad values).
    /// </summary>
    /// <param name="key">The key that was pressed or released.</param>
    /// <param name="down">True if the key is down; false if up.</param>
    /// <param name="analogValue">The analog value.</param>
    public void AddKeyAnalogEvent(Key key, bool down, float analogValue)
    {
        ImGuiIO.AddKeyAnalogEvent(Native, (ImGuiKey)key, down, analogValue);
    }

    /// <summary>
    /// Queues a mouse position update.
    /// </summary>
    /// <param name="x">The mouse X position. Use -FLT_MAX to signify no mouse.</param>
    /// <param name="y">The mouse Y position. Use -FLT_MAX to signify no mouse.</param>
    public void AddMousePosEvent(float x, float y)
    {
        ImGuiIO.AddMousePosEvent(Native, x, y);
    }

    /// <summary>
    /// Queues a mouse button change.
    /// </summary>
    /// <param name="button">The mouse button index (0=left, 1=right, 2=middle, etc.).</param>
    /// <param name="down">True if the button is down; false if up.</param>
    public void AddMouseButtonEvent(int button, bool down)
    {
        ImGuiIO.AddMouseButtonEvent(Native, button, down);
    }

    /// <summary>
    /// Queues a mouse wheel update.
    /// </summary>
    /// <param name="wheelX">Horizontal wheel movement (negative=right, positive=left).</param>
    /// <param name="wheelY">Vertical wheel movement (negative=down, positive=up).</param>
    public void AddMouseWheelEvent(float wheelX, float wheelY)
    {
        ImGuiIO.AddMouseWheelEvent(Native, wheelX, wheelY);
    }

    /// <summary>
    /// Queues a mouse source change.
    /// </summary>
    /// <param name="source">The mouse source (Mouse, TouchScreen, or Pen).</param>
    public void AddMouseSourceEvent(MouseSource source)
    {
        ImGuiIO.AddMouseSourceEvent(Native, (ImGuiMouseSource)source);
    }

    /// <summary>
    /// Queues a gain/loss of focus for the application.
    /// </summary>
    /// <param name="focused">True if the application gained focus; false if lost.</param>
    public void AddFocusEvent(bool focused)
    {
        ImGuiIO.AddFocusEvent(Native, focused);
    }

    /// <summary>
    /// Queues a new character input.
    /// </summary>
    /// <param name="c">The Unicode codepoint.</param>
    public void AddInputCharacter(char c)
    {
        ImGuiIO.AddInputCharacter(Native, c);
    }

    /// <summary>
    /// Queues a new character input from a UTF-16 character.
    /// </summary>
    /// <param name="c">The UTF-16 character (can be a surrogate).</param>
    public void AddInputCharacterUtf16(char c)
    {
        ImGuiIO.AddInputCharacterUTF16(Native, c);
    }

    /// <summary>
    /// Queues new characters input from a UTF-8 string.
    /// </summary>
    /// <param name="str">The UTF-8 string.</param>
    public void AddInputCharactersUtf8(string str)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(str + '\0');
        fixed (byte* ptr = bytes)
        {
            ImGuiIO.AddInputCharactersUTF8(Native, ptr);
        }
    }

    /// <summary>
    /// Sets native key data for legacy IsKeyXXX() functions.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="nativeKeycode">The native keycode.</param>
    /// <param name="nativeScancode">The native scancode.</param>
    /// <param name="nativeLegacyIndex">The native legacy index (default -1).</param>
    public void SetKeyEventNativeData(Key key, int nativeKeycode, int nativeScancode, int nativeLegacyIndex = -1)
    {
        ImGuiIO.SetKeyEventNativeData(Native, (ImGuiKey)key, nativeKeycode, nativeScancode, nativeLegacyIndex);
    }

    /// <summary>
    /// Sets the master flag for accepting key/mouse/text events.
    /// </summary>
    /// <param name="acceptingEvents">True to accept events; false to block them.</param>
    public void SetAppAcceptingEvents(bool acceptingEvents)
    {
        ImGuiIO.SetAppAcceptingEvents(Native, acceptingEvents);
    }

    /// <summary>
    /// Clears all incoming events.
    /// </summary>
    public void ClearEventsQueue()
    {
        ImGuiIO.ClearEventsQueue(Native);
    }

    /// <summary>
    /// Clears current keyboard/gamepad state and current frame text input buffer.
    /// </summary>
    public void ClearInputKeys()
    {
        ImGuiIO.ClearInputKeys(Native);
    }

    /// <summary>
    /// Clears current mouse state.
    /// </summary>
    public void ClearInputMouse()
    {
        ImGuiIO.ClearInputMouse(Native);
    }
}
