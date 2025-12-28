using Sdl3Sharp.ImGui.Native;
using System.Runtime.InteropServices;
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
}