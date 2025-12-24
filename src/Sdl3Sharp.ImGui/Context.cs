using Sdl3Sharp.ImGui.Native;
using System.Runtime.InteropServices;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a Dear ImGui context that manages the state for a single ImGui instance.
/// </summary>
public unsafe readonly struct Context: IDisposable
{
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

    /// <summary>
    /// Initializes a new instance of the Context class, optionally using a specified font atlas.
    /// </summary>
    /// <param name="fontAtlas">An optional FontAtlas to use for font rendering. If null, a default font atlas is created and used.</param>
    public static Context Create(FontAtlas? fontAtlas = null)
    {
        return new Context(ImGui_CreateContext(fontAtlas == null ? null : fontAtlas.Value.Value));
    }

    internal Context(ImGuiContext* native)
    {
        Value = native;
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
    /// <param name="open">A reference to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowDemoWindow(StateRef<bool>? open = null)
    {
        ImGui_ShowDemoWindow(open == null ? null : open.Value.Ptr);
    }

    /// <summary>
    /// Shows the ImGui metrics/debug window, displaying internal state information.
    /// </summary>
    /// <param name="open">A reference to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowMetricsWindow(StateRef<bool>? open = null)
    {
        ImGui_ShowMetricsWindow(open == null ? null : open.Value.Ptr);
    }

    /// <summary>
    /// Shows the ImGui debug log window.
    /// </summary>
    /// <param name="open">A reference to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowDebugLogWindow(StateRef<bool>? open = null)
    {
        ImGui_ShowDebugLogWindow(open == null ? null : open.Value.Ptr);
    }

    /// <summary>
    /// Shows the ImGui ID stack tool window, useful for debugging ID conflicts.
    /// </summary>
    /// <param name="open">A reference to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowIDStackToolWindow(StateRef<bool>? open = null)
    {
        // TODO: Why does this one get an "Ex" but the others don't?
        ImGui_ShowIDStackToolWindowEx(open == null ? null : open.Value.Ptr);
    }

    /// <summary>
    /// Shows the ImGui about window, displaying version and build information.
    /// </summary>
    /// <param name="open">A reference to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowAboutWindow(StateRef<bool>? open = null)
    {
        ImGui_ShowAboutWindow(open == null ? null : open.Value.Ptr);
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

    /// <inheritdoc/>
    public void Dispose()
    {
        ImGui_DestroyContext(Value);
    }
}