using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents the ImGui IO configuration and state wrapper.
/// </summary>
public unsafe readonly struct IO
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
    public ref ConfigFlags ConfigFlags => ref Unsafe.As<ImGuiConfigFlags, ConfigFlags>(ref Native->ConfigFlags);

    /// <summary>
    /// Gets or sets the backend flags.
    /// </summary>
    /// <seealso cref="BackendFlags"/>
    public ref BackendFlags BackendFlags => ref Unsafe.As<ImGuiBackendFlags, BackendFlags>(ref Native->BackendFlags);

    /// <summary>
    /// Gets or sets the main display size in pixels.
    /// </summary>
    public ref Vec2 DisplaySize => ref Unsafe.As<ImVec2, Vec2>(ref Native->DisplaySize);

    /// <summary>
    /// Gets or sets the display framebuffer scale for retina displays.
    /// </summary>
    public ref Vec2 DisplayFramebufferScale => ref Unsafe.As<ImVec2, Vec2>(ref Native->DisplayFramebufferScale);

    /// <summary>
    /// Gets or sets the time elapsed since last frame, in seconds.
    /// </summary>
    public ref float DeltaTime => ref Native->DeltaTime;

    /// <summary>
    /// Gets or sets the minimum time between saving positions/sizes to .ini file, in seconds.
    /// </summary>
    public ref float IniSavingRate => ref Native->IniSavingRate;

    /// <summary>
    /// Gets or sets custom user data for the IO.
    /// </summary>
    public ref nint UserData => ref Unsafe.AsRef<nint>(&Native->UserData);

    /// <summary>
    /// Gets the font atlas.
    /// </summary>
    public readonly ref FontAtlas Fonts => ref Unsafe.AsRef<FontAtlas>(&Native->Fonts);

    /// <summary>
    /// Gets or sets whether to allow user scaling text of individual windows with Ctrl+Wheel.
    /// </summary>
    public ref bool FontAllowUserScaling => ref Native->FontAllowUserScaling;

    /// <summary>
    /// Gets or sets whether to swap Activate/Cancel (A/B) buttons for gamepad navigation.
    /// </summary>
    public ref bool ConfigNavSwapGamepadButtons => ref Native->ConfigNavSwapGamepadButtons;

    /// <summary>
    /// Gets or sets whether directional/tabbing navigation teleports the mouse cursor.
    /// </summary>
    public ref bool ConfigNavMoveSetMousePos => ref Native->ConfigNavMoveSetMousePos;

    /// <summary>
    /// Gets or sets whether io.WantCaptureKeyboard is set when io.NavActive is set.
    /// </summary>
    public ref bool ConfigNavCaptureKeyboard => ref Native->ConfigNavCaptureKeyboard;

    /// <summary>
    /// Gets or sets whether pressing Escape can clear focused item and navigation id/highlight.
    /// </summary>
    public ref bool ConfigNavEscapeClearFocusItem => ref Native->ConfigNavEscapeClearFocusItem;

    /// <summary>
    /// Gets or sets whether pressing Escape can clear focused window as well.
    /// </summary>
    public ref bool ConfigNavEscapeClearFocusWindow => ref Native->ConfigNavEscapeClearFocusWindow;

    /// <summary>
    /// Gets or sets whether using directional navigation key makes the cursor visible.
    /// </summary>
    public ref bool ConfigNavCursorVisibleAuto => ref Native->ConfigNavCursorVisibleAuto;

    /// <summary>
    /// Gets or sets whether the navigation cursor is always visible.
    /// </summary>
    public ref bool ConfigNavCursorVisibleAlways => ref Native->ConfigNavCursorVisibleAlways;

    /// <summary>
    /// Gets or sets whether ImGui should draw a mouse cursor for you.
    /// </summary>
    public ref bool MouseDrawCursor => ref Native->MouseDrawCursor;

    /// <summary>
    /// Gets or sets whether to use macOS-style behaviors (Cmd/Ctrl swap, Alt text editing, etc.).
    /// </summary>
    public ref bool ConfigMacOSXBehaviors => ref Native->ConfigMacOSXBehaviors;

    /// <summary>
    /// Gets or sets whether to enable input queue trickling.
    /// </summary>
    public ref bool ConfigInputTrickleEventQueue => ref Native->ConfigInputTrickleEventQueue;

    /// <summary>
    /// Gets or sets whether to enable blinking cursor.
    /// </summary>
    public ref bool ConfigInputTextCursorBlink => ref Native->ConfigInputTextCursorBlink;

    /// <summary>
    /// Gets or sets whether pressing Enter keeps item active and selects contents (single-line only).
    /// </summary>
    public ref bool ConfigInputTextEnterKeepActive => ref Native->ConfigInputTextEnterKeepActive;

    /// <summary>
    /// Gets or sets whether clicking on DragXXX widgets turns them into text input.
    /// </summary>
    public ref bool ConfigDragClickToInputText => ref Native->ConfigDragClickToInputText;

    /// <summary>
    /// Gets or sets whether to enable resizing of windows from their edges.
    /// </summary>
    public ref bool ConfigWindowsResizeFromEdges => ref Native->ConfigWindowsResizeFromEdges;

    /// <summary>
    /// Gets or sets whether windows can only be moved by clicking on their title bar.
    /// </summary>
    public ref bool ConfigWindowsMoveFromTitleBarOnly => ref Native->ConfigWindowsMoveFromTitleBarOnly;

    /// <summary>
    /// Gets or sets whether Ctrl+C copies the contents of focused window into the clipboard.
    /// </summary>
    public ref bool ConfigWindowsCopyContentsWithCtrlC => ref Native->ConfigWindowsCopyContentsWithCtrlC;

    /// <summary>
    /// Gets or sets whether to enable scrolling page by page when clicking outside the scrollbar grab.
    /// </summary>
    public ref bool ConfigScrollbarScrollByPage => ref Native->ConfigScrollbarScrollByPage;

    /// <summary>
    /// Gets or sets the timer (in seconds) to free transient windows/tables memory buffers when unused.
    /// </summary>
    public ref float ConfigMemoryCompactTimer => ref Native->ConfigMemoryCompactTimer;

    /// <summary>
    /// Gets or sets the time for a double-click, in seconds.
    /// </summary>
    public ref float MouseDoubleClickTime => ref Native->MouseDoubleClickTime;

    /// <summary>
    /// Gets or sets the distance threshold to stay in to validate a double-click, in pixels.
    /// </summary>
    public ref float MouseDoubleClickMaxDist => ref Native->MouseDoubleClickMaxDist;

    /// <summary>
    /// Gets or sets the distance threshold before considering we are dragging.
    /// </summary>
    public ref float MouseDragThreshold => ref Native->MouseDragThreshold;

    /// <summary>
    /// Gets or sets the time before key/button repeat starts, in seconds.
    /// </summary>
    public ref float KeyRepeatDelay => ref Native->KeyRepeatDelay;

    /// <summary>
    /// Gets or sets the rate at which key/button repeats, in seconds.
    /// </summary>
    public ref float KeyRepeatRate => ref Native->KeyRepeatRate;

    /// <summary>
    /// Gets or sets whether to enable error recovery support.
    /// </summary>
    public ref bool ConfigErrorRecovery => ref Native->ConfigErrorRecovery;

    /// <summary>
    /// Gets or sets whether to enable asserts on recoverable errors.
    /// </summary>
    public ref bool ConfigErrorRecoveryEnableAssert => ref Native->ConfigErrorRecoveryEnableAssert;

    /// <summary>
    /// Gets or sets whether to enable debug log output on recoverable errors.
    /// </summary>
    public ref bool ConfigErrorRecoveryEnableDebugLog => ref Native->ConfigErrorRecoveryEnableDebugLog;

    /// <summary>
    /// Gets or sets whether to enable tooltip on recoverable errors.
    /// </summary>
    public ref bool ConfigErrorRecoveryEnableTooltip => ref Native->ConfigErrorRecoveryEnableTooltip;

    /// <summary>
    /// Gets or sets whether a debugger is present.
    /// </summary>
    public ref bool ConfigDebugIsDebuggerPresent => ref Native->ConfigDebugIsDebuggerPresent;

    /// <summary>
    /// Gets or sets whether to highlight and show error message popup when multiple items have conflicting identifiers.
    /// </summary>
    public ref bool ConfigDebugHighlightIdConflicts => ref Native->ConfigDebugHighlightIdConflicts;

    /// <summary>
    /// Gets or sets whether to show "Item Picker" button in ID conflict popup.
    /// </summary>
    public ref bool ConfigDebugHighlightIdConflictsShowItemPicker => ref Native->ConfigDebugHighlightIdConflictsShowItemPicker;

    /// <summary>
    /// Gets or sets whether first-time calls to Begin()/BeginChild() will return false.
    /// </summary>
    public ref bool ConfigDebugBeginReturnValueOnce => ref Native->ConfigDebugBeginReturnValueOnce;

    /// <summary>
    /// Gets or sets whether some calls to Begin()/BeginChild() will return false (cycles through window depths).
    /// </summary>
    public ref bool ConfigDebugBeginReturnValueLoop => ref Native->ConfigDebugBeginReturnValueLoop;

    /// <summary>
    /// Gets or sets whether to ignore io.AddFocusEvent(false).
    /// </summary>
    public ref bool ConfigDebugIgnoreFocusLoss => ref Native->ConfigDebugIgnoreFocusLoss;

    /// <summary>
    /// Gets or sets whether to save .ini data with extra comments.
    /// </summary>
    public ref bool ConfigDebugIniSettings => ref Native->ConfigDebugIniSettings;

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
    public ref nint BackendPlatformUserData => ref Unsafe.AsRef<nint>(&Native->BackendPlatformUserData);

    /// <summary>
    /// Gets or sets the user data for the renderer backend.
    /// </summary>
    public ref nint BackendRendererUserData => ref Unsafe.AsRef<nint>(&Native->BackendRendererUserData);

    /// <summary>
    /// Gets or sets the user data for non-C++ programming language backend.
    /// </summary>
    public ref nint BackendLanguageUserData => ref Unsafe.AsRef<nint>(&Native->BackendLanguageUserData);

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
    public ref bool WantSaveIniSettings => ref Native->WantSaveIniSettings;

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
    public Vec2 MouseDelta => new(Native->MouseDelta);

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
