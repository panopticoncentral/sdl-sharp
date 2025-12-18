using System.Runtime.InteropServices;
using System.Text;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Provides high-level wrapper methods for Dear ImGui functionality.
/// </summary>
public static unsafe class ImGui
{
    //
    // Context creation and access
    //

    /// <summary>
    /// Creates a new ImGui context.
    /// </summary>
    /// <param name="sharedFontAtlas">Optional shared font atlas to use across multiple contexts. If null, a new font atlas will be created.</param>
    /// <returns>The newly created ImGui context.</returns>
    public static Context CreateContext(FontAtlas? sharedFontAtlas = null)
    {
        return new Context(ImGui_CreateContext(sharedFontAtlas == null ? null : sharedFontAtlas.Value.Native));
    }

    /// <summary>
    /// Destroys an ImGui context.
    /// </summary>
    /// <param name="context">The context to destroy. If null, the current context will be destroyed.</param>
    public static void DestroyContext(Context? context)
    {
        ImGui_DestroyContext(context == null ? null : context.Value.Native);
    }

    /// <summary>
    /// Gets the current ImGui context.
    /// </summary>
    /// <returns>The current ImGui context.</returns>
    public static Context GetCurrentContext()
    {
        return new Context(ImGui_GetCurrentContext());
    }

    /// <summary>
    /// Sets the current ImGui context.
    /// </summary>
    /// <param name="context">The context to set as current. If null, no context will be current.</param>
    public static void SetCurrentContext(Context? context)
    {
        ImGui_SetCurrentContext(context == null ? null : context.Value.Native);
    }

    //
    // Main
    //

    /// <summary>
    /// Gets the IO configuration and state for the current context.
    /// </summary>
    /// <returns>The IO object containing input/output configuration and state.</returns>
    public static IO GetIO()
    {
        return new IO(ImGui_GetIO());
    }

    /// <summary>
    /// Gets the style settings for the current context.
    /// </summary>
    /// <returns>The style object containing visual styling configuration.</returns>
    public static Style GetStyle()
    {
        return new Style(ImGui_GetStyle());
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

    //
    // Demo, Debug, Information
    //

    /// <summary>
    /// Shows the ImGui demo window, which demonstrates most ImGui features.
    /// </summary>
    /// <param name="open">A reference to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowDemoWindow(StateRef<bool> open)
    {
        ImGui_ShowDemoWindow(open.Ptr);
    }

    /// <summary>
    /// Shows the ImGui metrics/debug window, displaying internal state information.
    /// </summary>
    /// <param name="open">A reference to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowMetricsWindow(StateRef<bool> open)
    {
        ImGui_ShowMetricsWindow(open.Ptr);
    }

    /// <summary>
    /// Shows the ImGui debug log window.
    /// </summary>
    /// <param name="open">A reference to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowDebugLogWindow(StateRef<bool> open)
    {
        ImGui_ShowDebugLogWindow(open.Ptr);
    }

    /// <summary>
    /// Shows the ImGui ID stack tool window, useful for debugging ID conflicts.
    /// </summary>
    /// <param name="open">A reference to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowIDStackToolWindowEx(StateRef<bool> open)
    {
        ImGui_ShowIDStackToolWindowEx(open.Ptr);
    }

    /// <summary>
    /// Shows the ImGui about window, displaying version and build information.
    /// </summary>
    /// <param name="open">A reference to a boolean controlling the window's open state. The window can be closed by the user.</param>
    public static void ShowAboutWindow(StateRef<bool> open)
    {
        ImGui_ShowAboutWindow(open.Ptr);
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
    public static bool ShowStyleSelector(Span<byte> label)
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
    public static void ShowFontSelector(Span<byte> label)
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
    /// Gets the version string of the Dear ImGui library.
    /// </summary>
    /// <returns>The version string (e.g., "1.90.1").</returns>
    public static string GetVersion()
    {
        var versionPtr = ImGui_GetVersion();
        return Marshal.PtrToStringUTF8((nint)versionPtr) ?? string.Empty;
    }

    //
    // Styles
    //

    /// <summary>
    /// Applies the dark color theme to the specified style.
    /// </summary>
    /// <param name="destination">The style object to apply the dark theme to.</param>
    public static void StyleColorsDark(Style destination)
    {
        ImGui_StyleColorsDark(destination.Native);
    }

    /// <summary>
    /// Applies the light color theme to the specified style.
    /// </summary>
    /// <param name="destination">The style object to apply the light theme to.</param>
    public static void StyleColorsLight(Style destination)
    {
        ImGui_StyleColorsLight(destination.Native);
    }

    /// <summary>
    /// Applies the classic (original) color theme to the specified style.
    /// </summary>
    /// <param name="destination">The style object to apply the classic theme to.</param>
    public static void StyleColorsClassic(Style destination)
    {
        ImGui_StyleColorsClassic(destination.Native);
    }

    //
    // Windows
    //

    /// <summary>
    /// Begins a new window. Must be paired with a call to <see cref="End"/>.
    /// </summary>
    /// <param name="name">The window name, used as a unique identifier. Use "##" to pass a label that isn't displayed.</param>
    /// <param name="open">Optional reference to a boolean controlling the window's open state. If provided, a close button is shown.</param>
    /// <param name="flags">Window behavior flags.</param>
    /// <returns>
    /// False if the window is collapsed or fully clipped (you can early out and skip submitting content).
    /// Always call <see cref="End"/> regardless of this return value.
    /// </returns>
    /// <remarks>
    /// You may append multiple times to the same window during the same frame by calling Begin()/End() pairs multiple times.
    /// Some information such as 'flags' or 'open' will only be considered by the first call to Begin().
    /// </remarks>
    public static bool Begin(Span<byte> name, StateRef<bool>? open = null, WindowFlags flags = WindowFlags.None)
    {
        fixed (byte* ptr = name)
        {
            return ImGui_Begin(ptr, open.HasValue ? open.Value.Ptr : null, (Native.ImGuiWindowFlags)flags);
        }
    }

    /// <summary>
    /// Ends the current window. Must be called for every <see cref="Begin"/> call, regardless of its return value.
    /// </summary>
    public static void End()
    {
        ImGui_End();
    }

    //
    // Child Windows
    //

    /// <summary>
    /// Begins a child window. Must be paired with a call to <see cref="EndChild"/>.
    /// </summary>
    /// <param name="id">The child window string ID.</param>
    /// <param name="size">
    /// The size of the child window. Use 0.0f for an axis to use remaining parent window size.
    /// Use values greater than 0.0f for explicit size. Use negative values to right/bottom-align.
    /// </param>
    /// <param name="childFlags">Child window behavior flags.</param>
    /// <param name="windowFlags">Window behavior flags.</param>
    /// <returns>
    /// False if the window is collapsed or fully clipped (you can early out and skip submitting content).
    /// Always call <see cref="EndChild"/> regardless of this return value.
    /// </returns>
    /// <remarks>
    /// Use child windows to create independent scrolling/clipping regions within a host window.
    /// Child windows can embed their own child windows.
    /// </remarks>
    public static bool BeginChild(Span<byte> id, Vec2 size = default, ChildFlags childFlags = ChildFlags.None, WindowFlags windowFlags = WindowFlags.None)
    {
        fixed (byte* ptr = id)
        {
            return ImGui_BeginChild(ptr, size.ToNative(), (Native.ImGuiChildFlags)childFlags, (Native.ImGuiWindowFlags)windowFlags);
        }
    }

    /// <summary>
    /// Begins a child window using an integer ID. Must be paired with a call to <see cref="EndChild"/>.
    /// </summary>
    /// <param name="id">The child window integer ID.</param>
    /// <param name="size">
    /// The size of the child window. Use 0.0f for an axis to use remaining parent window size.
    /// Use values greater than 0.0f for explicit size. Use negative values to right/bottom-align.
    /// </param>
    /// <param name="childFlags">Child window behavior flags.</param>
    /// <param name="windowFlags">Window behavior flags.</param>
    /// <returns>
    /// False if the window is collapsed or fully clipped (you can early out and skip submitting content).
    /// Always call <see cref="EndChild"/> regardless of this return value.
    /// </returns>
    /// <remarks>
    /// Use child windows to create independent scrolling/clipping regions within a host window.
    /// Child windows can embed their own child windows.
    /// </remarks>
    public static bool BeginChild(Id id, Vec2 size = default, ChildFlags childFlags = ChildFlags.None, WindowFlags windowFlags = WindowFlags.None)
    {
        return ImGui_BeginChildID(id.Value, size.ToNative(), (Native.ImGuiChildFlags)childFlags, (Native.ImGuiWindowFlags)windowFlags);
    }

    /// <summary>
    /// Ends a child window. Must be called for every <see cref="BeginChild"/> call, regardless of its return value.
    /// </summary>
    public static void EndChild()
    {
        ImGui_EndChild();
    }
}
