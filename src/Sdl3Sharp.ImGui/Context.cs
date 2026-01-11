using Sdl3Sharp.ImGui.Native;
using System.Runtime.InteropServices;
using System.Text;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a Dear ImGui context that manages the state for a single ImGui instance.
/// </summary>
public unsafe readonly struct Context
{
    public const string VersionString = Native.ImGui.Version;
    public const int VersionNumber = VersionNum;

    internal readonly ImGuiContext* Value { get; init; }

    /// <summary>
    /// The current active context.
    /// </summary>
    public static Context? Current
    {
        get
        {
            ImGuiContext* ctx = ImGui_GetCurrentContext();
            return ctx == null ? null : new Context(ctx);
        }
        set => ImGui_SetCurrentContext(value == null ? null : value.Value.Value);
    }

    /// <summary>
    /// Gets the main viewport.
    /// </summary>
    public static Viewport MainViewport
    {
        get
        {
            ImGuiViewport* vp = ImGui_GetMainViewport();
            return new Viewport(vp);
        }
    }

    /// <summary>
    /// Gets the current ImGui IO configuration and state for the calling thread.
    /// </summary>
    public static IO IO => new(ImGui_GetIO());

    /// <summary>
    /// Gets the current global style settings for the ImGui user interface.
    /// </summary>
    public static Style Style => new(ImGui_GetStyle());

    /// <summary>
    /// Gets the version string of the Dear ImGui library.
    /// </summary>
    /// <returns>The version string (e.g., "1.90.1").</returns>
    public static string Version
    {
        get
        {
            var versionPtr = ImGui_GetVersion();
            return Marshal.PtrToStringUTF8((nint)versionPtr) ?? string.Empty;
        }
    }

    internal Context(ImGuiContext* native)
    {
        Value = native;
    }

    /// <summary>
    /// Initializes a new instance of the Context class, optionally using a specified font atlas.
    /// </summary>
    /// <param name="fontAtlas">An optional FontAtlas to use for font rendering. If null, a default font atlas is created and used.</param>
    public static Context Create(FontAtlas? fontAtlas = null)
    {
        return new Context(ImGui_CreateContext(fontAtlas == null ? null : fontAtlas.Value.Value));
    }

    /// <summary>
    /// Destroys an ImGui context.
    /// </summary>
    /// <param name="context">The context to destroy. If null, the current context will be destroyed.</param>
    public static void Destroy(Context? context = null)
    {
        ImGui_DestroyContext(context == null ? null : context.Value.Value);
    }

    /// <summary>
    /// Starts a new ImGui frame. Must be called once per frame before any ImGui commands.
    /// </summary>
    public static void NewFrame()
    {
        ImGui_NewFrame();
    }

    /// <summary>
    /// Ends the current ImGui frame. Automatically called by <see cref="Render"/>. You may call this to end a frame early without rendering.
    /// </summary>
    public static void EndFrame()
    {
        ImGui_EndFrame();
    }

    /// <summary>
    /// Ends the current frame and prepares the draw data for rendering. Must be called once per frame after all ImGui commands.
    /// </summary>
    public static void Render()
    {
        ImGui_Render();
    }

    /// <summary>
    /// Shows the ImGui demo window, which demonstrates most ImGui features.
    /// </summary>
    /// <param name="open">A pointer to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowDemoWindow()
    {
        ImGui_ShowDemoWindow(null);
    }

    /// <summary>
    /// Shows the ImGui demo window, which demonstrates most ImGui features.
    /// </summary>
    /// <param name="open">A pointer to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowDemoWindow(ref bool open)
    {
        fixed (bool* openPtr = &open)
        {
            ImGui_ShowDemoWindow(openPtr);
        }
    }

    /// <summary>
    /// Shows the ImGui metrics/debug window, displaying internal state information.
    /// </summary>
    /// <param name="open">A pointer to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowMetricsWindow()
    {
        ImGui_ShowMetricsWindow(null);
    }

    /// <summary>
    /// Shows the ImGui metrics/debug window, displaying internal state information.
    /// </summary>
    /// <param name="open">A pointer to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowMetricsWindow(ref bool open)
    {
        fixed (bool* openPtr = &open)
        {
            ImGui_ShowMetricsWindow(openPtr);
        }
    }

    /// <summary>
    /// Shows the ImGui debug log window.
    /// </summary>
    /// <param name="open">A pointer to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowDebugLogWindow()
    {
        ImGui_ShowDebugLogWindow(null);
    }

    /// <summary>
    /// Shows the ImGui debug log window.
    /// </summary>
    /// <param name="open">A pointer to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowDebugLogWindow(ref bool open)
    {
        fixed (bool* openPtr = &open)
        {
            ImGui_ShowDebugLogWindow(openPtr);
        }
    }

    /// <summary>
    /// Shows the ImGui ID stack tool window, useful for debugging ID conflicts.
    /// </summary>
    /// <param name="open">A pointer to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowIDStackToolWindow()
    {
        ImGui_ShowIDStackToolWindowEx(null);
    }

    /// <summary>
    /// Shows the ImGui ID stack tool window, useful for debugging ID conflicts.
    /// </summary>
    /// <param name="open">A pointer to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowIDStackToolWindow(ref bool open)
    {
        fixed (bool* openPtr = &open)
        {
            ImGui_ShowIDStackToolWindowEx(openPtr);
        }
    }

    /// <summary>
    /// Shows the ImGui about window, displaying version and build information.
    /// </summary>
    /// <param name="open">A pointer to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowAboutWindow()
    {
        ImGui_ShowAboutWindow(null);
    }

    /// <summary>
    /// Shows the ImGui about window, displaying version and build information.
    /// </summary>
    /// <param name="open">A pointer to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowAboutWindow(ref bool open)
    {
        fixed (bool* openPtr = &open)
        {
            ImGui_ShowAboutWindow(openPtr);
        }
    }

    /// <summary>
    /// Shows a style editor window for the given style, allowing interactive modification of style settings.
    /// </summary>
    /// <param name="style">The style object to edit.</param>
    public static void ShowStyleEditor(Style style)
    {
        ImGui_ShowStyleEditor(style.Native);
    }

    /// <summary>
    /// Shows a combo box to select between built-in style presets.
    /// </summary>
    /// <param name="label">The label for the combo box.</param>
    /// <returns>True if the style was changed, false otherwise.</returns>
    public static bool ShowStyleSelector(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_ShowStyleSelector(ptr);
        }
    }

    /// <summary>
    /// Shows a combo box to select between available fonts.
    /// </summary>
    /// <param name="label">The label for the combo box.</param>
    public static void ShowFontSelector(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            ImGui_ShowFontSelector(ptr);
        }
    }

    /// <summary>
    /// Shows a section with basic help and tips about using ImGui.
    /// </summary>
    public static void ShowUserGuide()
    {
        ImGui_ShowUserGuide();
    }

    /// <summary>
    /// Starts logging to tty (stdout).
    /// </summary>
    /// <param name="autoOpenDepth">Depth to auto-open tree nodes (-1 for default).</param>
    public static void LogToTTY(int autoOpenDepth = -1)
    {
        ImGui_LogToTTY(autoOpenDepth);
    }

    /// <summary>
    /// Starts logging to a file.
    /// </summary>
    /// <param name="autoOpenDepth">Depth to auto-open tree nodes (-1 for default).</param>
    /// <param name="filename">The filename to log to (null for default "imgui_log.txt").</param>
    public static void LogToFile(int autoOpenDepth = -1, ReadOnlySpan<byte> filename = default)
    {
        fixed (byte* ptr = filename)
        {
            ImGui_LogToFile(autoOpenDepth, ptr);
        }
    }

    /// <summary>
    /// Starts logging to the OS clipboard.
    /// </summary>
    /// <param name="autoOpenDepth">Depth to auto-open tree nodes (-1 for default).</param>
    public static void LogToClipboard(int autoOpenDepth = -1)
    {
        ImGui_LogToClipboard(autoOpenDepth);
    }

    /// <summary>
    /// Stops logging (closes file, etc.).
    /// </summary>
    public static void LogFinish()
    {
        ImGui_LogFinish();
    }

    /// <summary>
    /// Displays buttons for logging to tty/file/clipboard.
    /// </summary>
    public static void LogButtons()
    {
        ImGui_LogButtons();
    }

    /// <summary>
    /// Passes text data straight to the log without being displayed.
    /// </summary>
    /// <param name="text">The text to log.</param>
    public static void LogText(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_LogText(ptr);
        }
    }

    /// <summary>
    /// Alter visibility of keyboard/gamepad cursor. by default: show when using an arrow key, hide when clicking with mouse.
    /// </summary>
    public static void SetNavCursorVisible(bool visible)
    {
        ImGui_SetNavCursorVisible(visible);
    }

    /// <summary>
    /// Gets the global ImGui time, incremented by io.DeltaTime every frame.
    /// </summary>
    /// <returns>The global time in seconds.</returns>
    public static double GetTime()
    {
        return ImGui_GetTime();
    }

    /// <summary>
    /// Gets the global ImGui frame count, incremented by 1 every frame.
    /// </summary>
    /// <returns>The frame count.</returns>
    public static int GetFrameCount()
    {
        return ImGui_GetFrameCount();
    }

    /// <summary>
    /// Checks if a key is being held.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key is down.</returns>
    public static bool IsKeyDown(Key key)
    {
        return ImGui_IsKeyDown((ImGuiKey)key);
    }

    /// <summary>
    /// Checks if a key was pressed (went from !Down to Down).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key was pressed.</returns>
    public static bool IsKeyPressed(Key key)
    {
        return ImGui_IsKeyPressed((ImGuiKey)key);
    }

    /// <summary>
    /// Checks if a key was pressed (went from !Down to Down).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <param name="repeat">If true, uses io.KeyRepeatDelay / KeyRepeatRate.</param>
    /// <returns>True if the key was pressed.</returns>
    public static bool IsKeyPressed(Key key, bool repeat)
    {
        return ImGui_IsKeyPressedEx((ImGuiKey)key, repeat);
    }

    /// <summary>
    /// Checks if a key was released (went from Down to !Down).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key was released.</returns>
    public static bool IsKeyReleased(Key key)
    {
        return ImGui_IsKeyReleased((ImGuiKey)key);
    }

    /// <summary>
    /// Gets how many times a key was pressed using provided repeat rate/delay.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <param name="repeatDelay">The repeat delay.</param>
    /// <param name="rate">The repeat rate.</param>
    /// <returns>The press count (most often 0 or 1, but can be higher).</returns>
    public static int GetKeyPressedAmount(Key key, float repeatDelay, float rate)
    {
        return ImGui_GetKeyPressedAmount((ImGuiKey)key, repeatDelay, rate);
    }

    /// <summary>
    /// Overrides the io.WantCaptureKeyboard flag next frame.
    /// </summary>
    /// <param name="wantCaptureKeyboard">Whether to capture keyboard input.</param>
    public static void SetNextFrameWantCaptureKeyboard(bool wantCaptureKeyboard)
    {
        ImGui_SetNextFrameWantCaptureKeyboard(wantCaptureKeyboard);
    }

    /// <summary>
    /// Gets the English name of a key for debugging purposes.
    /// </summary>
    /// <param name="key">The key to get the name of.</param>
    /// <returns>The English name of the key.</returns>
    /// <remarks>
    /// These names are provided for debugging purpose and are not meant to be saved persistently nor compared.
    /// </remarks>
    public static string GetKeyName(Key key)
    {
        var namePtr = ImGui_GetKeyName((ImGuiKey)key);
        return Marshal.PtrToStringUTF8((nint)namePtr) ?? string.Empty;
    }

    /// <summary>
    /// Checks if a key chord was pressed.
    /// </summary>
    /// <param name="keyChord">The key chord to check (a key combined with optional modifiers using bitwise OR).</param>
    /// <returns>True if the key chord was pressed.</returns>
    /// <remarks>
    /// This function compares mods and calls IsKeyPressed() - it has no side-effects.
    /// For shortcut routing (where only one owner gets the shortcut), use <see cref="Shortcut"/> instead.
    /// </remarks>
    public static bool IsKeyChordPressed(Key keyChord)
    {
        return ImGui_IsKeyChordPressed((int)keyChord);
    }

    /// <summary>
    /// Tests for a shortcut with routing support.
    /// </summary>
    /// <param name="keyChord">The key chord to check (a key combined with optional modifiers using bitwise OR).</param>
    /// <param name="flags">Flags controlling the shortcut behavior and routing.</param>
    /// <returns>True if the shortcut was activated.</returns>
    /// <remarks>
    /// <para>
    /// Unlike <see cref="IsKeyChordPressed"/>, this function submits a route for the shortcut.
    /// Routes are resolved, and if the shortcut can currently be routed, it calls IsKeyChordPressed().
    /// This has desirable side-effects as it can prevent another caller from getting the route.
    /// </para>
    /// <para>
    /// The general idea is that several callers may register interest in a shortcut, and only one owner gets it.
    /// The system is order-independent, so if Child1 makes its calls before Parent, results will be identical.
    /// </para>
    /// <para>
    /// Visualize registered routes in 'Metrics/Debugger->Inputs'.
    /// </para>
    /// </remarks>
    public static bool Shortcut(Key keyChord, InputFlags flags = InputFlags.None)
    {
        return ImGui_Shortcut((int)keyChord, (ImGuiInputFlags)flags);
    }

    /// <summary>
    /// Sets a keyboard shortcut for the next item.
    /// </summary>
    /// <param name="keyChord">The key chord to assign (a key combined with optional modifiers using bitwise OR).</param>
    /// <param name="flags">Flags controlling the shortcut behavior.</param>
    /// <remarks>
    /// Call this before adding a widget to associate a keyboard shortcut with it.
    /// </remarks>
    public static void SetNextItemShortcut(Key keyChord, InputFlags flags = InputFlags.None)
    {
        ImGui_SetNextItemShortcut((int)keyChord, (ImGuiInputFlags)flags);
    }

    /// <summary>
    /// Sets the key owner to the last item's ID if it is hovered or active.
    /// </summary>
    /// <param name="key">The key to set ownership for.</param>
    /// <remarks>
    /// <para>
    /// This is equivalent to: if (IsItemHovered() || IsItemActive()) { SetKeyOwner(key, GetItemID()); }
    /// </para>
    /// <para>
    /// One common use case is to allow your items to disable standard input behaviors such as
    /// Tab or Alt key handling, Mouse Wheel scrolling, etc.
    /// </para>
    /// <para>
    /// Example: Button(...); SetItemKeyOwner(Key.MouseWheelY); // Makes hovering/activating the button disable wheel scrolling.
    /// </para>
    /// </remarks>
    public static void SetItemKeyOwner(Key key)
    {
        ImGui_SetItemKeyOwner((ImGuiKey)key);
    }

    /// <summary>
    /// Checks if a mouse button is held.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the mouse button is down.</returns>
    public static bool IsMouseDown(MouseButton button)
    {
        return ImGui_IsMouseDown((ImGuiMouseButton)button);
    }

    /// <summary>
    /// Checks if a mouse button was clicked (went from !Down to Down).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the mouse button was clicked.</returns>
    public static bool IsMouseClicked(MouseButton button)
    {
        return ImGui_IsMouseClicked((ImGuiMouseButton)button);
    }

    /// <summary>
    /// Checks if a mouse button was clicked (went from !Down to Down).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <param name="repeat">If true, uses io.KeyRepeatDelay / KeyRepeatRate.</param>
    /// <returns>True if the mouse button was clicked.</returns>
    public static bool IsMouseClicked(MouseButton button, bool repeat)
    {
        return ImGui_IsMouseClickedEx((ImGuiMouseButton)button, repeat);
    }

    /// <summary>
    /// Checks if a mouse button was released (went from Down to !Down).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the mouse button was released.</returns>
    public static bool IsMouseReleased(MouseButton button)
    {
        return ImGui_IsMouseReleased((ImGuiMouseButton)button);
    }

    /// <summary>
    /// Checks if a mouse button was double-clicked.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the mouse button was double-clicked.</returns>
    public static bool IsMouseDoubleClicked(MouseButton button)
    {
        return ImGui_IsMouseDoubleClicked((ImGuiMouseButton)button);
    }

    /// <summary>
    /// Gets the number of successive mouse clicks at the time of click (otherwise 0).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>The click count.</returns>
    public static int GetMouseClickedCount(MouseButton button)
    {
        return ImGui_GetMouseClickedCount((ImGuiMouseButton)button);
    }

    /// <summary>
    /// Checks if the mouse is hovering a given bounding rectangle.
    /// </summary>
    /// <param name="rMin">The upper-left corner of the rectangle.</param>
    /// <param name="rMax">The lower-right corner of the rectangle.</param>
    /// <param name="clip">If true, clip by current clipping settings.</param>
    /// <returns>True if the mouse is hovering the rectangle.</returns>
    public static bool IsMouseHoveringRect(Vec2 rMin, Vec2 rMax, bool clip = true)
    {
        return clip
            ? ImGui_IsMouseHoveringRect(rMin.Value, rMax.Value)
            : ImGui_IsMouseHoveringRectEx(rMin.Value, rMax.Value, false);
    }

    /// <summary>
    /// Checks if any mouse button is held.
    /// </summary>
    /// <returns>True if any mouse button is down.</returns>
    public static bool IsAnyMouseDown()
    {
        return ImGui_IsAnyMouseDown();
    }

    /// <summary>
    /// Gets the current mouse position.
    /// </summary>
    /// <returns>The mouse position in screen space.</returns>
    public static Vec2 GetMousePos()
    {
        return new(ImGui_GetMousePos());
    }

    /// <summary>
    /// Gets the mouse position at the time of opening the current popup.
    /// </summary>
    /// <returns>The mouse position when the popup was opened.</returns>
    public static Vec2 GetMousePosOnOpeningCurrentPopup()
    {
        return new(ImGui_GetMousePosOnOpeningCurrentPopup());
    }

    /// <summary>
    /// Checks if the mouse is dragging.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <param name="lockThreshold">The distance threshold (-1.0f uses io.MouseDraggingThreshold).</param>
    /// <returns>True if dragging.</returns>
    public static bool IsMouseDragging(MouseButton button, float lockThreshold = -1.0f)
    {
        return ImGui_IsMouseDragging((ImGuiMouseButton)button, lockThreshold);
    }

    /// <summary>
    /// Gets the delta from the initial clicking position while the mouse button is pressed.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <param name="lockThreshold">The distance threshold (-1.0f uses io.MouseDraggingThreshold).</param>
    /// <returns>The drag delta.</returns>
    public static Vec2 GetMouseDragDelta(MouseButton button = MouseButton.Left, float lockThreshold = -1.0f)
    {
        return new(ImGui_GetMouseDragDelta((ImGuiMouseButton)button, lockThreshold));
    }

    /// <summary>
    /// Resets the mouse drag delta.
    /// </summary>
    /// <param name="button">The mouse button to reset (defaults to left button).</param>
    public static void ResetMouseDragDelta(MouseButton button = MouseButton.Left)
    {
        if (button == MouseButton.Left)
        {
            ImGui_ResetMouseDragDelta();
        }
        else
        {
            ImGui_ResetMouseDragDeltaEx((ImGuiMouseButton)button);
        }
    }

    /// <summary>
    /// Gets the desired mouse cursor shape.
    /// </summary>
    /// <returns>The current mouse cursor.</returns>
    public static MouseCursor GetMouseCursor()
    {
        return (MouseCursor)ImGui_GetMouseCursor();
    }

    /// <summary>
    /// Sets the desired mouse cursor shape.
    /// </summary>
    /// <param name="cursorType">The cursor to set.</param>
    public static void SetMouseCursor(MouseCursor cursorType)
    {
        ImGui_SetMouseCursor((ImGuiMouseCursor)cursorType);
    }

    /// <summary>
    /// Overrides the io.WantCaptureMouse flag next frame.
    /// </summary>
    /// <param name="wantCaptureMouse">Whether to capture mouse input.</param>
    public static void SetNextFrameWantCaptureMouse(bool wantCaptureMouse)
    {
        ImGui_SetNextFrameWantCaptureMouse(wantCaptureMouse);
    }

    /// <summary>
    /// Gets the text from the clipboard.
    /// </summary>
    /// <returns>The clipboard text, or an empty string if empty.</returns>
    public static string GetClipboardText()
    {
        var ptr = ImGui_GetClipboardText();

        return ptr == null
            ? string.Empty
            : Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ptr));
    }

    /// <summary>
    /// Sets the clipboard text.
    /// </summary>
    /// <param name="text">The text to set.</param>
    public static void SetClipboardText(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_SetClipboardText(ptr);
        }
    }

    /// <summary>
    /// Loads settings from a .ini file on disk.
    /// </summary>
    /// <param name="iniFilename">The path to the .ini file.</param>
    /// <remarks>
    /// Call after CreateContext() and before the first call to NewFrame().
    /// NewFrame() automatically calls this with io.IniFilename if set.
    /// </remarks>
    public static void LoadIniSettingsFromDisk(ReadOnlySpan<byte> iniFilename)
    {
        fixed (byte* ptr = iniFilename)
        {
            ImGui_LoadIniSettingsFromDisk(ptr);
        }
    }

    /// <summary>
    /// Loads settings from a memory buffer.
    /// </summary>
    /// <param name="iniData">The .ini data to load.</param>
    /// <remarks>
    /// Call after CreateContext() and before the first call to NewFrame()
    /// to provide .ini data from your own data source.
    /// </remarks>
    public static void LoadIniSettingsFromMemory(ReadOnlySpan<byte> iniData)
    {
        fixed (byte* ptr = iniData)
        {
            ImGui_LoadIniSettingsFromMemory(ptr, (nuint)iniData.Length);
        }
    }

    /// <summary>
    /// Saves settings to a .ini file on disk.
    /// </summary>
    /// <param name="iniFilename">The path to the .ini file.</param>
    /// <remarks>
    /// This is automatically called (if io.IniFilename is not empty) a few seconds
    /// after any modification that should be reflected in the .ini file, and also by DestroyContext().
    /// </remarks>
    public static void SaveIniSettingsToDisk(ReadOnlySpan<byte> iniFilename)
    {
        fixed (byte* ptr = iniFilename)
        {
            ImGui_SaveIniSettingsToDisk(ptr);
        }
    }

    /// <summary>
    /// Saves settings to a string in memory.
    /// </summary>
    /// <returns>The .ini data as a string.</returns>
    /// <remarks>
    /// Call when io.WantSaveIniSettings is set, then save the data by your own means
    /// and clear io.WantSaveIniSettings.
    /// </remarks>
    public static string SaveIniSettingsToMemory()
    {
        nuint size;
        var ptr = ImGui_SaveIniSettingsToMemory(&size);

        return ptr == null
            ? string.Empty
            : Encoding.UTF8.GetString(ptr, (int)size);
    }
}