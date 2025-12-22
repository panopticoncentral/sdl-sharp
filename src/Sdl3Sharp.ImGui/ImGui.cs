using System.Runtime.InteropServices;
using System.Text;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Provides high-level wrapper methods for Dear ImGui functionality.
/// </summary>
public static unsafe class ImGui
{
    #region * Context creation and access

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
    public static void DestroyContext(Context? context = null)
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

    #endregion

    #region * Main

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

    // Not wrapping GetDrawData at this time

    #endregion

    #region * Demo, Debug, Information

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

    /// <summary>
    /// Gets the version string of the Dear ImGui library.
    /// </summary>
    /// <returns>The version string (e.g., "1.90.1").</returns>
    public static string GetVersion()
    {
        var versionPtr = ImGui_GetVersion();
        return Marshal.PtrToStringUTF8((nint)versionPtr) ?? string.Empty;
    }

    #endregion

    #region * Styles

    /// <summary>
    /// Applies the dark color theme to the specified style.
    /// </summary>
    /// <param name="destination">The style object to apply the dark theme to.</param>
    public static void StyleColorsDark(Style? destination = null)
    {
        ImGui_StyleColorsDark(destination == null ? null : destination.Native);
    }

    /// <summary>
    /// Applies the light color theme to the specified style.
    /// </summary>
    /// <param name="destination">The style object to apply the light theme to.</param>
    public static void StyleColorsLight(Style? destination = null)
    {
        ImGui_StyleColorsLight(destination == null ? null : destination.Native);
    }

    /// <summary>
    /// Applies the classic (original) color theme to the specified style.
    /// </summary>
    /// <param name="destination">The style object to apply the classic theme to.</param>
    public static void StyleColorsClassic(Style? destination = null)
    {
        ImGui_StyleColorsClassic(destination == null ? null : destination.Native);
    }

    #endregion

    #region * Windows

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
    public static bool Begin(ReadOnlySpan<byte> name, StateRef<bool>? open = null, WindowFlags flags = WindowFlags.None)
    {
        fixed (byte* ptr = name)
        {
            return ImGui_Begin(ptr, open == null ? null : open.Value.Ptr, (Native.ImGuiWindowFlags)flags);
        }
    }

    /// <summary>
    /// Ends the current window. Must be called for every <see cref="Begin"/> call, regardless of its return value.
    /// </summary>
    public static void End()
    {
        ImGui_End();
    }

    #endregion

    #region * Child Windows

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
    public static bool BeginChild(ReadOnlySpan<byte> id, Vec2 size = default, ChildFlags childFlags = ChildFlags.None, WindowFlags windowFlags = WindowFlags.None)
    {
        fixed (byte* ptr = id)
        {
            return ImGui_BeginChild(ptr, size.Value, (Native.ImGuiChildFlags)childFlags, (Native.ImGuiWindowFlags)windowFlags);
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
        return ImGui_BeginChildID(id.Value, size.Value, (Native.ImGuiChildFlags)childFlags, (Native.ImGuiWindowFlags)windowFlags);
    }

    /// <summary>
    /// Ends a child window. Must be called for every <see cref="BeginChild"/> call, regardless of its return value.
    /// </summary>
    public static void EndChild()
    {
        ImGui_EndChild();
    }

    #endregion

    #region * Windows Utilities

    /// <summary>
    /// Returns true if the current window is appearing (was just created or unhidden this frame).
    /// </summary>
    /// <returns>True if the current window is appearing.</returns>
    public static bool IsWindowAppearing()
    {
        return ImGui_IsWindowAppearing();
    }

    /// <summary>
    /// Returns true if the current window is collapsed.
    /// </summary>
    /// <returns>True if the current window is collapsed.</returns>
    public static bool IsWindowCollapsed()
    {
        return ImGui_IsWindowCollapsed();
    }

    /// <summary>
    /// Returns true if the current window is focused, based on the specified flags.
    /// </summary>
    /// <param name="flags">Flags controlling which windows to consider when checking focus.</param>
    /// <returns>True if the current window is focused according to the specified flags.</returns>
    public static bool IsWindowFocused(FocusedFlags flags = FocusedFlags.None)
    {
        return ImGui_IsWindowFocused((Native.ImGuiFocusedFlags)flags);
    }

    /// <summary>
    /// Returns true if the current window is hovered and hoverable (e.g., not blocked by a popup/modal).
    /// </summary>
    /// <param name="flags">Flags controlling hover behavior and which windows to consider.</param>
    /// <returns>True if the current window is hovered according to the specified flags.</returns>
    /// <remarks>
    /// If you are trying to check whether your mouse should be dispatched to Dear ImGui or to your underlying app,
    /// you should not use this function! Use <see cref="IO.WantCaptureMouse"/> instead.
    /// </remarks>
    public static bool IsWindowHovered(HoveredFlags flags = HoveredFlags.None)
    {
        return ImGui_IsWindowHovered((Native.ImGuiHoveredFlags)flags);
    }

    // Not wrapping ImGui_GetWindowDrawList at this time

    /// <summary>
    /// Gets the current window position in screen space.
    /// </summary>
    /// <returns>The current window position.</returns>
    /// <remarks>
    /// It is unlikely you ever need to use this. Consider using <see cref="GetCursorScreenPos"/> and <see cref="GetContentRegionAvail"/> instead.
    /// </remarks>
    public static Vec2 GetWindowPos()
    {
        return new(ImGui_GetWindowPos());
    }

    /// <summary>
    /// Gets the current window size.
    /// </summary>
    /// <returns>The current window size.</returns>
    /// <remarks>
    /// It is unlikely you ever need to use this. Consider using <see cref="GetCursorScreenPos"/> and <see cref="GetContentRegionAvail"/> instead.
    /// </remarks>
    public static Vec2 GetWindowSize()
    {
        return new(ImGui_GetWindowSize());
    }

    /// <summary>
    /// Gets the current window width.
    /// </summary>
    /// <returns>The current window width.</returns>
    /// <remarks>
    /// It is unlikely you ever need to use this. Shortcut for <see cref="GetWindowSize"/>.X.
    /// </remarks>
    public static float GetWindowWidth()
    {
        return ImGui_GetWindowWidth();
    }

    /// <summary>
    /// Gets the current window height.
    /// </summary>
    /// <returns>The current window height.</returns>
    /// <remarks>
    /// It is unlikely you ever need to use this. Shortcut for <see cref="GetWindowSize"/>.Y.
    /// </remarks>
    public static float GetWindowHeight()
    {
        return ImGui_GetWindowHeight();
    }

    #endregion

    #region * Window Manipulation

    /// <summary>
    /// Sets the next window position with a pivot point. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="pos">The position in screen coordinates.</param>
    /// <param name="cond">Condition for applying the position.</param>
    /// <param name="pivot">The pivot point (0,0) = top-left, (0.5,0.5) = center, (1,1) = bottom-right.</param>
    /// <remarks>
    /// Use pivot=(0.5f, 0.5f) to center on the given point.
    /// </remarks>
    public static void SetNextWindowPos(Vec2 pos, Cond cond = Cond.None, Vec2 pivot = default)
    {
        ImGui_SetNextWindowPosEx(pos.Value, (Native.ImGuiCond)cond, pivot.Value);
    }

    /// <summary>
    /// Sets the next window size. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="size">The window size. Set an axis to 0.0f to force auto-fit on that axis.</param>
    /// <param name="cond">Condition for applying the size.</param>
    public static void SetNextWindowSize(Vec2 size, Cond cond = Cond.None)
    {
        ImGui_SetNextWindowSize(size.Value, (Native.ImGuiCond)cond);
    }

    /// <summary>
    /// Sets the next window size constraints. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="sizeMin">Minimum window size. Use 0.0f for no minimum. Use -1 for both min and max of same axis to preserve current size.</param>
    /// <param name="sizeMax">Maximum window size. Use float.MaxValue for no maximum. Use -1 for both min and max of same axis to preserve current size.</param>
    public static void SetNextWindowSizeConstraints(Vec2 sizeMin, Vec2 sizeMax)
    {
        // TODO: We're not exposing the custom size callback for now
        ImGui_SetNextWindowSizeConstraints(sizeMin.Value, sizeMax.Value, null, 0);
    }

    /// <summary>
    /// Sets the next window content size (scrollable client area). Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="size">The content size. Does not include window decorations (title bar, menu bar, etc.) or WindowPadding. Set an axis to 0.0f to leave it automatic.</param>
    public static void SetNextWindowContentSize(Vec2 size)
    {
        ImGui_SetNextWindowContentSize(size.Value);
    }

    /// <summary>
    /// Sets the next window collapsed state. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="collapsed">Whether the window should be collapsed.</param>
    /// <param name="cond">Condition for applying the collapsed state.</param>
    public static void SetNextWindowCollapsed(bool collapsed, Cond cond = Cond.None)
    {
        ImGui_SetNextWindowCollapsed(collapsed, (Native.ImGuiCond)cond);
    }

    /// <summary>
    /// Sets the next window to be focused / top-most. Call before <see cref="Begin"/>.
    /// </summary>
    public static void SetNextWindowFocus()
    {
        ImGui_SetNextWindowFocus();
    }

    /// <summary>
    /// Sets the next window scrolling value. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="scroll">The scroll position. Use a value less than 0.0f to not affect a given axis.</param>
    public static void SetNextWindowScroll(Vec2 scroll)
    {
        ImGui_SetNextWindowScroll(scroll.Value);
    }

    /// <summary>
    /// Sets the next window background color alpha.
    /// </summary>
    /// <param name="alpha">The alpha value for the background color.</param>
    /// <remarks>
    /// Helper to easily override the Alpha component of ImGuiCol.WindowBg/ChildBg/PopupBg.
    /// You may also use <see cref="WindowFlags.NoBackground"/>.
    /// </remarks>
    public static void SetNextWindowBgAlpha(float alpha)
    {
        ImGui_SetNextWindowBgAlpha(alpha);
    }

    /// <summary>
    /// Sets the current window position. Call within <see cref="Begin"/>/<see cref="End"/>.
    /// </summary>
    /// <param name="pos">The position in screen coordinates.</param>
    /// <param name="cond">Condition for applying the position.</param>
    /// <remarks>
    /// Not recommended. Prefer using <see cref="SetNextWindowPos(Vec2, Cond)"/> as this may incur tearing and side-effects.
    /// </remarks>
    public static void SetWindowPos(Vec2 pos, Cond cond = Cond.None)
    {
        ImGui_SetWindowPos(pos.Value, (Native.ImGuiCond)cond);
    }

    /// <summary>
    /// Sets the current window size. Call within <see cref="Begin"/>/<see cref="End"/>.
    /// </summary>
    /// <param name="size">The window size. Set to (0, 0) to force auto-fit.</param>
    /// <param name="cond">Condition for applying the size.</param>
    /// <remarks>
    /// Not recommended. Prefer using <see cref="SetNextWindowSize"/> as this may incur tearing and minor side-effects.
    /// </remarks>
    public static void SetWindowSize(Vec2 size, Cond cond = Cond.None)
    {
        ImGui_SetWindowSize(size.Value, (Native.ImGuiCond)cond);
    }

    /// <summary>
    /// Sets the current window collapsed state. Call within <see cref="Begin"/>/<see cref="End"/>.
    /// </summary>
    /// <param name="collapsed">Whether the window should be collapsed.</param>
    /// <param name="cond">Condition for applying the collapsed state.</param>
    /// <remarks>
    /// Not recommended. Prefer using <see cref="SetNextWindowCollapsed"/>.
    /// </remarks>
    public static void SetWindowCollapsed(bool collapsed, Cond cond = Cond.None)
    {
        ImGui_SetWindowCollapsed(collapsed, (Native.ImGuiCond)cond);
    }

    /// <summary>
    /// Sets the current window to be focused / top-most. Call within <see cref="Begin"/>/<see cref="End"/>.
    /// </summary>
    /// <remarks>
    /// Not recommended. Prefer using <see cref="SetNextWindowFocus"/>.
    /// </remarks>
    public static void SetWindowFocus()
    {
        ImGui_SetWindowFocus();
    }

    /// <summary>
    /// Sets a named window's position.
    /// </summary>
    /// <param name="name">The window name.</param>
    /// <param name="pos">The position in screen coordinates.</param>
    /// <param name="cond">Condition for applying the position.</param>
    public static void SetWindowPos(ReadOnlySpan<byte> name, Vec2 pos, Cond cond = Cond.None)
    {
        fixed (byte* ptr = name)
        {
            ImGui_SetWindowPosStr(ptr, pos.Value, (Native.ImGuiCond)cond);
        }
    }

    /// <summary>
    /// Sets a named window's size.
    /// </summary>
    /// <param name="name">The window name.</param>
    /// <param name="size">The window size. Set an axis to 0.0f to force auto-fit on that axis.</param>
    /// <param name="cond">Condition for applying the size.</param>
    public static void SetWindowSize(ReadOnlySpan<byte> name, Vec2 size, Cond cond = Cond.None)
    {
        fixed (byte* ptr = name)
        {
            ImGui_SetWindowSizeStr(ptr, size.Value, (Native.ImGuiCond)cond);
        }
    }

    /// <summary>
    /// Sets a named window's collapsed state.
    /// </summary>
    /// <param name="name">The window name.</param>
    /// <param name="collapsed">Whether the window should be collapsed.</param>
    /// <param name="cond">Condition for applying the collapsed state.</param>
    public static void SetWindowCollapsed(ReadOnlySpan<byte> name, bool collapsed, Cond cond = Cond.None)
    {
        fixed (byte* ptr = name)
        {
            ImGui_SetWindowCollapsedStr(ptr, collapsed, (Native.ImGuiCond)cond);
        }
    }

    /// <summary>
    /// Sets a named window to be focused / top-most.
    /// </summary>
    /// <param name="name">The window name. Use null to remove focus.</param>
    public static void SetWindowFocus(ReadOnlySpan<byte> name)
    {
        fixed (byte* ptr = name)
        {
            ImGui_SetWindowFocusStr(ptr);
        }
    }

    #endregion

    #region * Windows Scrolling

    /// <summary>
    /// Gets the horizontal scrolling amount [0 .. <see cref="GetScrollMaxX"/>].
    /// </summary>
    /// <returns>The current horizontal scroll position.</returns>
    /// <remarks>
    /// Any change of scroll will be applied at the beginning of next frame in the first call to <see cref="Begin"/>.
    /// You may use <see cref="SetNextWindowScroll"/> prior to calling <see cref="Begin"/> to avoid this delay.
    /// </remarks>
    public static float GetScrollX()
    {
        return ImGui_GetScrollX();
    }

    /// <summary>
    /// Gets the vertical scrolling amount [0 .. <see cref="GetScrollMaxY"/>].
    /// </summary>
    /// <returns>The current vertical scroll position.</returns>
    /// <remarks>
    /// Any change of scroll will be applied at the beginning of next frame in the first call to <see cref="Begin"/>.
    /// You may use <see cref="SetNextWindowScroll"/> prior to calling <see cref="Begin"/> to avoid this delay.
    /// </remarks>
    public static float GetScrollY()
    {
        return ImGui_GetScrollY();
    }

    /// <summary>
    /// Sets the horizontal scrolling amount [0 .. <see cref="GetScrollMaxX"/>].
    /// </summary>
    /// <param name="scrollX">The horizontal scroll position to set.</param>
    public static void SetScrollX(float scrollX)
    {
        ImGui_SetScrollX(scrollX);
    }

    /// <summary>
    /// Sets the vertical scrolling amount [0 .. <see cref="GetScrollMaxY"/>].
    /// </summary>
    /// <param name="scrollY">The vertical scroll position to set.</param>
    public static void SetScrollY(float scrollY)
    {
        ImGui_SetScrollY(scrollY);
    }

    /// <summary>
    /// Gets the maximum horizontal scrolling amount (~~ ContentSize.X - WindowSize.X - DecorationsSize.X).
    /// </summary>
    /// <returns>The maximum horizontal scroll value.</returns>
    public static float GetScrollMaxX()
    {
        return ImGui_GetScrollMaxX();
    }

    /// <summary>
    /// Gets the maximum vertical scrolling amount (~~ ContentSize.Y - WindowSize.Y - DecorationsSize.Y).
    /// </summary>
    /// <returns>The maximum vertical scroll value.</returns>
    public static float GetScrollMaxY()
    {
        return ImGui_GetScrollMaxY();
    }

    /// <summary>
    /// Adjusts horizontal scrolling amount to make the current cursor position visible.
    /// </summary>
    /// <param name="centerXRatio">Where to position the cursor: 0.0 = left, 0.5 = center, 1.0 = right.</param>
    /// <remarks>
    /// When using to make a "default/current item" visible, consider using SetItemDefaultFocus() instead.
    /// </remarks>
    public static void SetScrollHereX(float centerXRatio = 0.5f)
    {
        ImGui_SetScrollHereX(centerXRatio);
    }

    /// <summary>
    /// Adjusts vertical scrolling amount to make the current cursor position visible.
    /// </summary>
    /// <param name="centerYRatio">Where to position the cursor: 0.0 = top, 0.5 = center, 1.0 = bottom.</param>
    /// <remarks>
    /// When using to make a "default/current item" visible, consider using SetItemDefaultFocus() instead.
    /// </remarks>
    public static void SetScrollHereY(float centerYRatio = 0.5f)
    {
        ImGui_SetScrollHereY(centerYRatio);
    }

    /// <summary>
    /// Adjusts horizontal scrolling amount to make the given position visible.
    /// </summary>
    /// <param name="localX">The local X position (generally GetCursorStartPos() + offset).</param>
    /// <param name="centerXRatio">Where to position: 0.0 = left, 0.5 = center, 1.0 = right.</param>
    public static void SetScrollFromPosX(float localX, float centerXRatio = 0.5f)
    {
        ImGui_SetScrollFromPosX(localX, centerXRatio);
    }

    /// <summary>
    /// Adjusts vertical scrolling amount to make the given position visible.
    /// </summary>
    /// <param name="localY">The local Y position (generally GetCursorStartPos() + offset).</param>
    /// <param name="centerYRatio">Where to position: 0.0 = top, 0.5 = center, 1.0 = bottom.</param>
    public static void SetScrollFromPosY(float localY, float centerYRatio = 0.5f)
    {
        ImGui_SetScrollFromPosY(localY, centerYRatio);
    }

    #endregion

    #region * Parameters stacks (font)

    /// <summary>
    /// Pushes a font and/or font size onto the stack.
    /// </summary>
    /// <param name="font">The font to use, or null to keep the current font.</param>
    /// <param name="fontSizeBaseUnscaled">The base font size before global scaling, or 0.0f to keep the current size.</param>
    /// <remarks>
    /// <para>Examples:</para>
    /// <list type="bullet">
    /// <item>PushFont(font, 0.0f) - Change font and keep current size</item>
    /// <item>PushFont(null, 20.0f) - Keep font and change current size</item>
    /// <item>PushFont(font, 20.0f) - Change font and set size to 20.0f</item>
    /// </list>
    /// <para>
    /// Global scale factors (style.FontScaleMain, style.FontScaleDpi) are applied over the provided size.
    /// Do NOT pass GetFontSize() to this function as it would apply global scaling twice.
    /// </para>
    /// </remarks>
    public static void PushFont(Font? font, float fontSizeBaseUnscaled = 0.0f)
    {
        ImGui_PushFontFloat(font == null ? null : font.Value.Native, fontSizeBaseUnscaled);
    }

    /// <summary>
    /// Pops the most recently pushed font from the stack.
    /// </summary>
    public static void PopFont()
    {
        ImGui_PopFont();
    }

    /// <summary>
    /// Gets the current font.
    /// </summary>
    /// <returns>The current font.</returns>
    public static Font GetFont()
    {
        return new Font(ImGui_GetFont());
    }

    /// <summary>
    /// Gets the current scaled font size (height in pixels) after global scale factors are applied.
    /// </summary>
    /// <returns>The current font size in pixels.</returns>
    /// <remarks>
    /// Do NOT pass this value to <see cref="PushFont"/>! Use Style.FontSizeBase to get the value before global scale factors.
    /// </remarks>
    public static float GetFontSize()
    {
        return ImGui_GetFontSize();
    }

    /// <summary>
    /// Gets the current font baked at the current size.
    /// </summary>
    /// <returns>The current font baked instance.</returns>
    /// <remarks>
    /// This is equivalent to <c>GetFont().GetFontBaked(GetFontSize())</c>.
    /// Pointers to FontBaked are only valid for the current frame.
    /// </remarks>
    public static FontBaked GetFontBaked()
    {
        return new FontBaked(ImGui_GetFontBaked());
    }

    #endregion

    #region * Parameters stacks (shared)

    /// <summary>
    /// Pushes a style color modification onto the stack using a 32-bit color value.
    /// </summary>
    /// <param name="idx">The color index to modify.</param>
    /// <param name="col">The color value as a 32-bit packed RGBA value.</param>
    /// <remarks>
    /// Always use this if you modify the style after <see cref="NewFrame"/>.
    /// </remarks>
    public static void PushStyleColor(Col idx, uint col)
    {
        ImGui_PushStyleColor((Native.ImGuiCol)idx, col);
    }

    /// <summary>
    /// Pushes a style color modification onto the stack using a Vec4 color value.
    /// </summary>
    /// <param name="idx">The color index to modify.</param>
    /// <param name="col">The color value as a Vec4 (RGBA, each component 0-1).</param>
    /// <remarks>
    /// Always use this if you modify the style after <see cref="NewFrame"/>.
    /// </remarks>
    public static void PushStyleColor(Col idx, Vec4 col)
    {
        ImGui_PushStyleColorImVec4((Native.ImGuiCol)idx, col.ToNative());
    }

    /// <summary>
    /// Pops multiple style colors from the stack.
    /// </summary>
    /// <param name="count">The number of style colors to pop.</param>
    public static void PopStyleColor(int count = 1)
    {
        ImGui_PopStyleColorEx(count);
    }

    /// <summary>
    /// Pushes a style variable modification onto the stack using a float value.
    /// </summary>
    /// <param name="idx">The style variable to modify.</param>
    /// <param name="val">The new float value.</param>
    /// <remarks>
    /// Always use this if you modify the style after <see cref="NewFrame"/>.
    /// </remarks>
    public static void PushStyleVar(StyleVar idx, float val)
    {
        ImGui_PushStyleVar((Native.ImGuiStyleVar)idx, val);
    }

    /// <summary>
    /// Pushes a style variable modification onto the stack using a Vec2 value.
    /// </summary>
    /// <param name="idx">The style variable to modify.</param>
    /// <param name="val">The new Vec2 value.</param>
    /// <remarks>
    /// Always use this if you modify the style after <see cref="NewFrame"/>.
    /// </remarks>
    public static void PushStyleVar(StyleVar idx, Vec2 val)
    {
        ImGui_PushStyleVarImVec2((Native.ImGuiStyleVar)idx, val.Value);
    }

    /// <summary>
    /// Pushes a modification to the X component of a style Vec2 variable.
    /// </summary>
    /// <param name="idx">The style variable to modify.</param>
    /// <param name="valX">The new X component value.</param>
    public static void PushStyleVarX(StyleVar idx, float valX)
    {
        ImGui_PushStyleVarX((Native.ImGuiStyleVar)idx, valX);
    }

    /// <summary>
    /// Pushes a modification to the Y component of a style Vec2 variable.
    /// </summary>
    /// <param name="idx">The style variable to modify.</param>
    /// <param name="valY">The new Y component value.</param>
    public static void PushStyleVarY(StyleVar idx, float valY)
    {
        ImGui_PushStyleVarY((Native.ImGuiStyleVar)idx, valY);
    }

    /// <summary>
    /// Pops multiple style variables from the stack.
    /// </summary>
    /// <param name="count">The number of style variables to pop.</param>
    public static void PopStyleVar(int count = 1)
    {
        ImGui_PopStyleVarEx(count);
    }

    /// <summary>
    /// Pushes an item flag modification onto the stack.
    /// </summary>
    /// <param name="option">The item flag to modify.</param>
    /// <param name="enabled">Whether the flag should be enabled.</param>
    public static void PushItemFlag(ItemFlags option, bool enabled)
    {
        ImGui_PushItemFlag((Native.ImGuiItemFlags)option, enabled);
    }

    /// <summary>
    /// Pops the most recently pushed item flag from the stack.
    /// </summary>
    public static void PopItemFlag()
    {
        ImGui_PopItemFlag();
    }

    #endregion

    #region * Parameters stacks (current window)

    /// <summary>
    /// Pushes the width of items for common large "item+label" widgets.
    /// </summary>
    /// <param name="itemWidth">
    /// The item width in pixels. Greater than 0.0f for explicit width.
    /// Less than 0.0f to align that many pixels from the right of the window.
    /// Use -float.Epsilon to always align to the right side.
    /// </param>
    public static void PushItemWidth(float itemWidth)
    {
        ImGui_PushItemWidth(itemWidth);
    }

    /// <summary>
    /// Pops the most recently pushed item width from the stack.
    /// </summary>
    public static void PopItemWidth()
    {
        ImGui_PopItemWidth();
    }

    /// <summary>
    /// Sets the width of the next common large "item+label" widget.
    /// </summary>
    /// <param name="itemWidth">
    /// The item width in pixels. Greater than 0.0f for explicit width.
    /// Less than 0.0f to align that many pixels from the right of the window.
    /// Use -float.Epsilon to always align to the right side.
    /// </param>
    public static void SetNextItemWidth(float itemWidth)
    {
        ImGui_SetNextItemWidth(itemWidth);
    }

    /// <summary>
    /// Calculates the width of an item given pushed settings and current cursor position.
    /// </summary>
    /// <returns>The calculated item width.</returns>
    /// <remarks>
    /// This is NOT necessarily the width of the last item unlike most 'Item' functions.
    /// </remarks>
    public static float CalcItemWidth()
    {
        return ImGui_CalcItemWidth();
    }

    /// <summary>
    /// Pushes a word-wrapping position for Text*() commands.
    /// </summary>
    /// <param name="wrapLocalPosX">
    /// The wrap position in window local space.
    /// Less than 0.0f: no wrapping.
    /// 0.0f: wrap to end of window (or column).
    /// Greater than 0.0f: wrap at the specified X position.
    /// </param>
    public static void PushTextWrapPos(float wrapLocalPosX = 0.0f)
    {
        ImGui_PushTextWrapPos(wrapLocalPosX);
    }

    /// <summary>
    /// Pops the most recently pushed text wrap position from the stack.
    /// </summary>
    public static void PopTextWrapPos()
    {
        ImGui_PopTextWrapPos();
    }

    #endregion

    #region * Style read access

    /// <summary>
    /// Gets the UV coordinate for a white pixel, useful to draw custom shapes via the DrawList API.
    /// </summary>
    /// <returns>The UV coordinate for a white pixel in the font texture.</returns>
    public static Vec2 GetFontTexUvWhitePixel()
    {
        return new(ImGui_GetFontTexUvWhitePixel());
    }
    /// <summary>
    /// Gets a style color as a 32-bit packed value with an additional alpha multiplier.
    /// </summary>
    /// <param name="idx">The color index.</param>
    /// <param name="alphaMul">Additional alpha multiplier (0.0 to 1.0).</param>
    /// <returns>The color as a 32-bit packed RGBA value with style alpha and multiplier applied.</returns>
    public static uint GetColorU32(Col idx, float alphaMul = 1.0f)
    {
        return ImGui_GetColorU32Ex((Native.ImGuiCol)idx, alphaMul);
    }

    /// <summary>
    /// Gets a Vec4 color as a 32-bit packed value with style alpha applied.
    /// </summary>
    /// <param name="col">The color as a Vec4.</param>
    /// <returns>The color as a 32-bit packed RGBA value.</returns>
    public static uint GetColorU32(Vec4 col)
    {
        return ImGui_GetColorU32ImVec4(col.ToNative());
    }

    /// <summary>
    /// Gets a 32-bit color with style alpha and an additional alpha multiplier applied.
    /// </summary>
    /// <param name="col">The color as a 32-bit packed RGBA value.</param>
    /// <param name="alphaMul">Additional alpha multiplier (0.0 to 1.0).</param>
    /// <returns>The color with style alpha and multiplier applied.</returns>
    public static uint GetColorU32(uint col, float alphaMul = 1.0f)
    {
        return ImGui_GetColorU32ImU32Ex(col, alphaMul);
    }

    /// <summary>
    /// Gets a style color as stored in the Style structure.
    /// </summary>
    /// <param name="idx">The color index.</param>
    /// <returns>The color as a Vec4.</returns>
    /// <remarks>
    /// Use this to feed back into <see cref="PushStyleColor(Col, Vec4)"/>.
    /// Otherwise use <see cref="GetColorU32(Col)"/> to get style color with style alpha baked in.
    /// </remarks>
    public static Vec4 GetStyleColorVec4(Col idx)
    {
        return Vec4.FromNative(*ImGui_GetStyleColorVec4((Native.ImGuiCol)idx));
    }

    #endregion

    #region * Layout cursor positioning

    /// <summary>
    /// Gets the cursor position in absolute screen coordinates.
    /// </summary>
    /// <returns>The cursor position in screen coordinates.</returns>
    /// <remarks>
    /// This is your best friend! Prefer using this rather than <see cref="GetCursorPos"/>.
    /// Also more useful when working with the DrawList API.
    /// </remarks>
    public static Vec2 GetCursorScreenPos()
    {
        return new(ImGui_GetCursorScreenPos());
    }

    /// <summary>
    /// Sets the cursor position in absolute screen coordinates.
    /// </summary>
    /// <param name="pos">The position in screen coordinates.</param>
    /// <remarks>
    /// This is your best friend!
    /// </remarks>
    public static void SetCursorScreenPos(Vec2 pos)
    {
        ImGui_SetCursorScreenPos(pos.Value);
    }

    /// <summary>
    /// Gets the available space from the current cursor position.
    /// </summary>
    /// <returns>The available content region size.</returns>
    /// <remarks>
    /// This is your best friend!
    /// </remarks>
    public static Vec2 GetContentRegionAvail()
    {
        return new(ImGui_GetContentRegionAvail());
    }

    /// <summary>
    /// Gets the cursor position in window-local coordinates.
    /// </summary>
    /// <returns>The cursor position in window-local coordinates.</returns>
    /// <remarks>
    /// This is not your best friend. Prefer <see cref="GetCursorScreenPos"/>.
    /// </remarks>
    public static Vec2 GetCursorPos()
    {
        return new(ImGui_GetCursorPos());
    }

    /// <summary>
    /// Gets the cursor X position in window-local coordinates.
    /// </summary>
    /// <returns>The cursor X position.</returns>
    public static float GetCursorPosX()
    {
        return ImGui_GetCursorPosX();
    }

    /// <summary>
    /// Gets the cursor Y position in window-local coordinates.
    /// </summary>
    /// <returns>The cursor Y position.</returns>
    public static float GetCursorPosY()
    {
        return ImGui_GetCursorPosY();
    }

    /// <summary>
    /// Sets the cursor position in window-local coordinates.
    /// </summary>
    /// <param name="localPos">The position in window-local coordinates.</param>
    public static void SetCursorPos(Vec2 localPos)
    {
        ImGui_SetCursorPos(localPos.Value);
    }

    /// <summary>
    /// Sets the cursor X position in window-local coordinates.
    /// </summary>
    /// <param name="localX">The X position in window-local coordinates.</param>
    public static void SetCursorPosX(float localX)
    {
        ImGui_SetCursorPosX(localX);
    }

    /// <summary>
    /// Sets the cursor Y position in window-local coordinates.
    /// </summary>
    /// <param name="localY">The Y position in window-local coordinates.</param>
    public static void SetCursorPosY(float localY)
    {
        ImGui_SetCursorPosY(localY);
    }

    /// <summary>
    /// Gets the initial cursor position in window-local coordinates.
    /// </summary>
    /// <returns>The initial cursor position when the window was created.</returns>
    /// <remarks>
    /// Call <see cref="GetCursorScreenPos"/> after <see cref="Begin"/> to get the absolute coordinates version.
    /// </remarks>
    public static Vec2 GetCursorStartPos()
    {
        return new(ImGui_GetCursorStartPos());
    }

    #endregion

    #region * Other layout functions

    /// <summary>
    /// Draws a separator, generally horizontal. Inside a menu bar or in horizontal layout mode, this becomes a vertical separator.
    /// </summary>
    public static void Separator()
    {
        ImGui_Separator();
    }

    /// <summary>
    /// Calls between widgets or groups to layout them horizontally with custom positioning.
    /// </summary>
    /// <param name="offsetFromStartX">X position from window start in window coordinates. 0.0f to use current position.</param>
    /// <param name="spacing">Spacing between the previous widget and current position. -1.0f to use default spacing.</param>
    public static void SameLine(float offsetFromStartX = 0.0f, float spacing = -1.0f)
    {
        ImGui_SameLineEx(offsetFromStartX, spacing);
    }

    /// <summary>
    /// Undoes a <see cref="SameLine"/> or forces a new line when in a horizontal-layout context.
    /// </summary>
    public static void NewLine()
    {
        ImGui_NewLine();
    }

    /// <summary>
    /// Adds vertical spacing.
    /// </summary>
    public static void Spacing()
    {
        ImGui_Spacing();
    }

    /// <summary>
    /// Adds a dummy item of the given size.
    /// </summary>
    /// <param name="size">The size of the dummy item.</param>
    /// <remarks>
    /// Unlike InvisibleButton(), Dummy() won't take mouse clicks or be navigable.
    /// </remarks>
    public static void Dummy(Vec2 size)
    {
        ImGui_Dummy(size.Value);
    }

    /// <summary>
    /// Moves content position toward the right by the specified amount.
    /// </summary>
    /// <param name="indentW">The indentation amount. If less than or equal to 0, uses style.IndentSpacing.</param>
    public static void Indent(float indentW = 0.0f)
    {
        ImGui_IndentEx(indentW);
    }

    /// <summary>
    /// Moves content position back to the left by the specified amount.
    /// </summary>
    /// <param name="indentW">The unindentation amount. If less than or equal to 0, uses style.IndentSpacing.</param>
    public static void Unindent(float indentW = 0.0f)
    {
        ImGui_UnindentEx(indentW);
    }

    /// <summary>
    /// Locks the horizontal starting position for a group of items.
    /// </summary>
    /// <remarks>
    /// Must be paired with <see cref="EndGroup"/>.
    /// </remarks>
    public static void BeginGroup()
    {
        ImGui_BeginGroup();
    }

    /// <summary>
    /// Unlocks the horizontal starting position and captures the whole group bounding box into one "item".
    /// </summary>
    /// <remarks>
    /// After calling this, you can use IsItemHovered() or layout primitives such as SameLine() on the whole group.
    /// </remarks>
    public static void EndGroup()
    {
        ImGui_EndGroup();
    }

    /// <summary>
    /// Vertically aligns upcoming text baseline to FramePadding.Y.
    /// </summary>
    /// <remarks>
    /// Call this if you have text on a line before a framed item, so that it will align properly.
    /// </remarks>
    public static void AlignTextToFramePadding()
    {
        ImGui_AlignTextToFramePadding();
    }

    /// <summary>
    /// Gets the height of a line of text (~~ FontSize).
    /// </summary>
    /// <returns>The text line height in pixels.</returns>
    public static float GetTextLineHeight()
    {
        return ImGui_GetTextLineHeight();
    }

    /// <summary>
    /// Gets the height of a line of text with spacing (~~ FontSize + style.ItemSpacing.Y).
    /// </summary>
    /// <returns>The distance in pixels between two consecutive lines of text.</returns>
    public static float GetTextLineHeightWithSpacing()
    {
        return ImGui_GetTextLineHeightWithSpacing();
    }

    /// <summary>
    /// Gets the height of a framed widget (~~ FontSize + style.FramePadding.Y * 2).
    /// </summary>
    /// <returns>The frame height in pixels.</returns>
    public static float GetFrameHeight()
    {
        return ImGui_GetFrameHeight();
    }

    /// <summary>
    /// Gets the height of a framed widget with spacing (~~ FontSize + style.FramePadding.Y * 2 + style.ItemSpacing.Y).
    /// </summary>
    /// <returns>The distance in pixels between two consecutive lines of framed widgets.</returns>
    public static float GetFrameHeightWithSpacing()
    {
        return ImGui_GetFrameHeightWithSpacing();
    }

    #endregion

    #region * ID stack/scopes

    /// <summary>
    /// Pushes a string into the ID stack (will hash string).
    /// </summary>
    /// <param name="strId">The string ID to push.</param>
    /// <remarks>
    /// <para>
    /// Read the FAQ for more details about how IDs are handled in Dear ImGui.
    /// </para>
    /// <para>
    /// IDs are hashes of the entire ID stack. If you are creating widgets in a loop,
    /// you most likely want to push a unique identifier (e.g., object pointer, loop index)
    /// to uniquely differentiate them.
    /// </para>
    /// <para>
    /// You can also use the "Label##foobar" syntax within widget labels to distinguish them from each other.
    /// </para>
    /// </remarks>
    public static void PushID(ReadOnlySpan<byte> strId)
    {
        fixed (byte* ptr = strId)
        {
            ImGui_PushID(ptr);
        }
    }

    /// <summary>
    /// Pushes a pointer into the ID stack (will hash pointer).
    /// </summary>
    /// <param name="ptrId">The pointer value to push as an ID.</param>
    public static void PushID(nint ptrId)
    {
        ImGui_PushIDPtr(ptrId);
    }

    /// <summary>
    /// Pushes an integer into the ID stack (will hash integer).
    /// </summary>
    /// <param name="intId">The integer ID to push.</param>
    public static void PushID(int intId)
    {
        ImGui_PushIDInt(intId);
    }

    /// <summary>
    /// Pops from the ID stack.
    /// </summary>
    public static void PopID()
    {
        ImGui_PopID();
    }

    /// <summary>
    /// Calculates a unique ID (hash of whole ID stack + given parameter).
    /// </summary>
    /// <param name="strId">The string to include in the hash.</param>
    /// <returns>The calculated unique ID.</returns>
    /// <remarks>
    /// Use this if you want to query into ImGuiStorage yourself.
    /// </remarks>
    public static Id GetID(ReadOnlySpan<byte> strId)
    {
        fixed (byte* ptr = strId)
        {
            return new Id(ImGui_GetID(ptr));
        }
    }

    /// <summary>
    /// Calculates a unique ID from a pointer (hash of whole ID stack + given parameter).
    /// </summary>
    /// <param name="ptrId">The pointer value to include in the hash.</param>
    /// <returns>The calculated unique ID.</returns>
    public static Id GetID(nint ptrId)
    {
        return new Id(ImGui_GetIDPtr(ptrId));
    }

    /// <summary>
    /// Calculates a unique ID from an integer (hash of whole ID stack + given parameter).
    /// </summary>
    /// <param name="intId">The integer to include in the hash.</param>
    /// <returns>The calculated unique ID.</returns>
    public static Id GetID(int intId)
    {
        return new Id(ImGui_GetIDInt(intId));
    }

    #endregion

    #region * Widgets: Text

    /// <summary>
    /// Displays raw text without formatting.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <remarks>
    /// This is faster than formatted text functions, with no memory copy or buffer size limits.
    /// Recommended for long chunks of text.
    /// </remarks>
    public static void TextUnformatted(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_TextUnformatted(ptr);
        }
    }

    /// <summary>
    /// Displays formatted text.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <remarks>
    /// The text is passed directly to ImGui. Any '%' characters in the text are escaped
    /// to prevent them from being interpreted as format specifiers.
    /// </remarks>
    public static void Text(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_Text(ptr);
        }
    }

    /// <summary>
    /// Displays colored text.
    /// </summary>
    /// <param name="color">The text color as RGBA values (0-1 range).</param>
    /// <param name="text">The text to display.</param>
    /// <remarks>
    /// Shortcut for PushStyleColor(ImGuiCol_Text, col); Text(text); PopStyleColor();
    /// </remarks>
    public static void TextColored(Vec4 color, ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_TextColored(color.ToNative(), ptr);
        }
    }

    /// <summary>
    /// Displays text in the disabled color.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <remarks>
    /// Shortcut for PushStyleColor(ImGuiCol_Text, style.Colors[ImGuiCol_TextDisabled]); Text(text); PopStyleColor();
    /// </remarks>
    public static void TextDisabled(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_TextDisabled(ptr);
        }
    }

    /// <summary>
    /// Displays text with automatic word wrapping.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <remarks>
    /// Shortcut for PushTextWrapPos(0.0f); Text(text); PopTextWrapPos();
    /// Note that this won't work on an auto-resizing window if there's no other widgets
    /// to extend the window width. You may need to set a size using SetNextWindowSize().
    /// </remarks>
    public static void TextWrapped(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_TextWrapped(ptr);
        }
    }

    /// <summary>
    /// Displays a label and text aligned the same way as value+label widgets.
    /// </summary>
    /// <param name="label">The label text.</param>
    /// <param name="text">The value text to display.</param>
    public static void LabelText(ReadOnlySpan<byte> label, ReadOnlySpan<byte> text)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* textPtr = text)
        {
            ImGui_LabelText(labelPtr, textPtr);
        }
    }

    /// <summary>
    /// Displays a bullet point followed by text.
    /// </summary>
    /// <param name="text">The text to display after the bullet.</param>
    /// <remarks>
    /// Shortcut for Bullet()+Text().
    /// </remarks>
    public static void BulletText(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_BulletText(ptr);
        }
    }

    /// <summary>
    /// Displays text with a horizontal separator line.
    /// </summary>
    /// <param name="label">The text label to display.</param>
    public static void SeparatorText(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            ImGui_SeparatorText(ptr);
        }
    }

    #endregion

    #region * Widgets: Main

    /// <summary>
    /// Creates a button widget with explicit size.
    /// </summary>
    /// <param name="label">The button label. ID is derived from the label (use ## to append non-visible ID).</param>
    /// <param name="size">The button size. Use (0,0) for automatic sizing.</param>
    /// <returns>True when clicked.</returns>
    public static bool Button(ReadOnlySpan<byte> label, Vec2 size = default)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_ButtonEx(ptr, size.Value);
        }
    }

    /// <summary>
    /// Creates a small button with FramePadding.y == 0, easily embeddable within text.
    /// </summary>
    /// <param name="label">The button label.</param>
    /// <returns>True when clicked.</returns>
    public static bool SmallButton(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SmallButton(ptr);
        }
    }

    /// <summary>
    /// Creates a flexible button behavior without visuals.
    /// </summary>
    /// <param name="strId">The string ID.</param>
    /// <param name="size">The clickable area size.</param>
    /// <param name="flags">Button behavior flags.</param>
    /// <returns>True when clicked.</returns>
    /// <remarks>
    /// Useful for building custom behaviors using the public API (along with IsItemActive, IsItemHovered, etc.).
    /// </remarks>
    public static bool InvisibleButton(ReadOnlySpan<byte> strId, Vec2 size, ButtonFlags flags = ButtonFlags.None)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_InvisibleButton(ptr, size.Value, (Native.ImGuiButtonFlags)flags);
        }
    }

    /// <summary>
    /// Creates a square button with an arrow shape.
    /// </summary>
    /// <param name="strId">The string ID.</param>
    /// <param name="dir">The arrow direction.</param>
    /// <returns>True when clicked.</returns>
    public static bool ArrowButton(ReadOnlySpan<byte> strId, Dir dir)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_ArrowButton(ptr, (Native.ImGuiDir)dir);
        }
    }

    /// <summary>
    /// Creates a checkbox widget.
    /// </summary>
    /// <param name="label">The checkbox label.</param>
    /// <param name="v">Reference to the boolean value.</param>
    /// <returns>True when the value has been changed.</returns>
    public static bool Checkbox(ReadOnlySpan<byte> label, StateRef<bool> v)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_Checkbox(ptr, v.Ptr);
        }
    }

    /// <summary>
    /// Creates a checkbox widget for signed integer flags.
    /// </summary>
    /// <param name="label">The checkbox label.</param>
    /// <param name="flags">Reference to the flags value.</param>
    /// <param name="flagsValue">The flag bit(s) to toggle.</param>
    /// <returns>True when the value has been changed.</returns>
    public static bool CheckboxFlags(ReadOnlySpan<byte> label, StateRef<int> flags, int flagsValue)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_CheckboxFlagsIntPtr(ptr, flags.Ptr, flagsValue);
        }
    }

    /// <summary>
    /// Creates a checkbox widget for unsigned integer flags.
    /// </summary>
    /// <param name="label">The checkbox label.</param>
    /// <param name="flags">Reference to the flags value.</param>
    /// <param name="flagsValue">The flag bit(s) to toggle.</param>
    /// <returns>True when the value has been changed.</returns>
    public static bool CheckboxFlags(ReadOnlySpan<byte> label, StateRef<uint> flags, uint flagsValue)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_CheckboxFlagsUintPtr(ptr, flags.Ptr, flagsValue);
        }
    }

    /// <summary>
    /// Creates a radio button widget.
    /// </summary>
    /// <param name="label">The radio button label.</param>
    /// <param name="active">Whether this radio button is currently active.</param>
    /// <returns>True when clicked.</returns>
    /// <remarks>
    /// Use with e.g. if (RadioButton("one", myValue == 1)) { myValue = 1; }
    /// </remarks>
    public static bool RadioButton(ReadOnlySpan<byte> label, bool active)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_RadioButton(ptr, active);
        }
    }

    /// <summary>
    /// Creates a radio button widget that modifies an integer value.
    /// </summary>
    /// <param name="label">The radio button label.</param>
    /// <param name="v">Reference to the current value.</param>
    /// <param name="vButton">The value this button represents.</param>
    /// <returns>True when clicked (and value changed).</returns>
    /// <remarks>
    /// Shortcut to handle the RadioButton pattern when the value is an integer.
    /// </remarks>
    public static bool RadioButton(ReadOnlySpan<byte> label, StateRef<int> v, int vButton)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_RadioButtonIntPtr(ptr, v.Ptr, vButton);
        }
    }

    /// <summary>
    /// Creates a progress bar widget.
    /// </summary>
    /// <param name="fraction">The progress value between 0.0f and 1.0f.</param>
    /// <param name="sizeArg">The bar size. Use (-FLT_MIN, 0) for default size.</param>
    /// <param name="overlay">Optional overlay text.</param>
    public static void ProgressBar(float fraction, Vec2 sizeArg = default, ReadOnlySpan<byte> overlay = default)
    {
        fixed (byte* overlayPtr = overlay)
        {
            ImGui_ProgressBar(fraction, sizeArg.Value, overlayPtr);
        }
    }

    /// <summary>
    /// Draws a small circle (bullet) and keeps the cursor on the same line.
    /// </summary>
    /// <remarks>
    /// Advances cursor x position by GetTreeNodeToLabelSpacing(), same distance that TreeNode() uses.
    /// </remarks>
    public static void Bullet()
    {
        ImGui_Bullet();
    }

    /// <summary>
    /// Creates a hyperlink text button.
    /// </summary>
    /// <param name="label">The link text.</param>
    /// <returns>True when clicked.</returns>
    public static bool TextLink(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_TextLink(ptr);
        }
    }

    /// <summary>
    /// Creates a hyperlink text button that opens a URL when clicked.
    /// </summary>
    /// <param name="label">The link text (also used as the URL if url is not specified).</param>
    /// <returns>True when clicked.</returns>
    public static bool TextLinkOpenURL(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_TextLinkOpenURL(ptr);
        }
    }

    /// <summary>
    /// Creates a hyperlink text button that opens a specified URL when clicked.
    /// </summary>
    /// <param name="label">The link text.</param>
    /// <param name="url">The URL to open.</param>
    /// <returns>True when clicked.</returns>
    public static bool TextLinkOpenURL(ReadOnlySpan<byte> label, ReadOnlySpan<byte> url)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* urlPtr = url)
        {
            return ImGui_TextLinkOpenURLEx(labelPtr, urlPtr);
        }
    }

    #endregion

    #region * Widgets: Images

    /// <summary>
    /// Displays an image.
    /// </summary>
    /// <param name="texRef">The texture reference.</param>
    /// <param name="imageSize">The size of the image to display.</param>
    /// <remarks>
    /// Image() adds style.ImageBorderSize on each side.
    /// Read about ImTextureID/ImTextureRef here: https://github.com/ocornut/imgui/wiki/Image-Loading-and-Displaying-Examples
    /// </remarks>
    public static void Image(TextureRef texRef, Vec2 imageSize)
    {
        ImGui_Image(texRef.Native, imageSize.Value);
    }

    /// <summary>
    /// Displays an image with explicit UV coordinates.
    /// </summary>
    /// <param name="texRef">The texture reference.</param>
    /// <param name="imageSize">The size of the image to display.</param>
    /// <param name="uv0">The UV coordinate of the top-left corner.</param>
    /// <param name="uv1">The UV coordinate of the bottom-right corner.</param>
    public static void Image(TextureRef texRef, Vec2 imageSize, Vec2 uv0, Vec2 uv1)
    {
        ImGui_ImageEx(texRef.Native, imageSize.Value, uv0.Value, uv1.Value);
    }

    /// <summary>
    /// Displays an image with background and tint color support.
    /// </summary>
    /// <param name="texRef">The texture reference.</param>
    /// <param name="imageSize">The size of the image to display.</param>
    public static void ImageWithBg(TextureRef texRef, Vec2 imageSize)
    {
        ImGui_ImageWithBg(texRef.Native, imageSize.Value);
    }

    /// <summary>
    /// Displays an image with background and tint color support, with explicit UV coordinates and colors.
    /// </summary>
    /// <param name="texRef">The texture reference.</param>
    /// <param name="imageSize">The size of the image to display.</param>
    /// <param name="uv0">The UV coordinate of the top-left corner.</param>
    /// <param name="uv1">The UV coordinate of the bottom-right corner.</param>
    /// <param name="bgCol">The background color.</param>
    /// <param name="tintCol">The tint color to apply to the image.</param>
    public static void ImageWithBg(TextureRef texRef, Vec2 imageSize, Vec2 uv0, Vec2 uv1, Vec4 bgCol, Vec4 tintCol)
    {
        ImGui_ImageWithBgEx(texRef.Native, imageSize.Value, uv0.Value, uv1.Value, bgCol.ToNative(), tintCol.ToNative());
    }

    /// <summary>
    /// Creates an image button.
    /// </summary>
    /// <param name="strId">The string ID for the button.</param>
    /// <param name="texRef">The texture reference.</param>
    /// <param name="imageSize">The size of the image.</param>
    /// <returns>True when clicked.</returns>
    /// <remarks>
    /// ImageButton() adds style.FramePadding on each side and draws a background based on regular Button() color.
    /// </remarks>
    public static bool ImageButton(ReadOnlySpan<byte> strId, TextureRef texRef, Vec2 imageSize)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_ImageButton(ptr, texRef.Native, imageSize.Value);
        }
    }

    /// <summary>
    /// Creates an image button with explicit UV coordinates and colors.
    /// </summary>
    /// <param name="strId">The string ID for the button.</param>
    /// <param name="texRef">The texture reference.</param>
    /// <param name="imageSize">The size of the image.</param>
    /// <param name="uv0">The UV coordinate of the top-left corner.</param>
    /// <param name="uv1">The UV coordinate of the bottom-right corner.</param>
    /// <param name="bgCol">The background color.</param>
    /// <param name="tintCol">The tint color to apply to the image.</param>
    /// <returns>True when clicked.</returns>
    public static bool ImageButton(ReadOnlySpan<byte> strId, TextureRef texRef, Vec2 imageSize, Vec2 uv0, Vec2 uv1, Vec4 bgCol, Vec4 tintCol)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_ImageButtonEx(ptr, texRef.Native, imageSize.Value, uv0.Value, uv1.Value, bgCol.ToNative(), tintCol.ToNative());
        }
    }

    #endregion

    #region * Widgets: Combo Box (Dropdown)

    /// <summary>
    /// Begins a combo box (dropdown). Must be followed by <see cref="EndCombo"/> if this returns true.
    /// </summary>
    /// <param name="label">The label for the combo box.</param>
    /// <param name="previewValue">The preview value displayed when closed.</param>
    /// <param name="flags">Combo box behavior flags.</param>
    /// <returns>True if the combo box is open and items should be rendered.</returns>
    /// <remarks>
    /// The BeginCombo()/EndCombo() API allows you to manage your contents and selection state however you want,
    /// by creating e.g. Selectable() items.
    /// </remarks>
    public static bool BeginCombo(ReadOnlySpan<byte> label, ReadOnlySpan<byte> previewValue, ComboFlags flags = ComboFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* previewPtr = previewValue)
        {
            return ImGui_BeginCombo(labelPtr, previewPtr, (Native.ImGuiComboFlags)flags);
        }
    }

    /// <summary>
    /// Ends a combo box. Only call this if <see cref="BeginCombo"/> returned true.
    /// </summary>
    public static void EndCombo()
    {
        ImGui_EndCombo();
    }

    /// <summary>
    /// Creates a combo box with items separated by null characters and explicit popup height.
    /// </summary>
    /// <param name="label">The label for the combo box.</param>
    /// <param name="currentItem">Reference to the current selected item index.</param>
    /// <param name="itemsSeparatedByZeros">Items separated by \0, ending with \0\0. e.g. "One\0Two\0Three\0"</param>
    /// <param name="popupMaxHeightInItems">Maximum height in items. Use -1 for default.</param>
    /// <returns>True if the selection changed.</returns>
    public static bool Combo(ReadOnlySpan<byte> label, StateRef<int> currentItem, ReadOnlySpan<byte> itemsSeparatedByZeros, int popupMaxHeightInItems = -1)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* itemsPtr = itemsSeparatedByZeros)
        {
            return ImGui_ComboEx(labelPtr, currentItem.Ptr, itemsPtr, popupMaxHeightInItems);
        }
    }

    #endregion

    #region Widgets: Drag Sliders

    /// <summary>
    /// Creates a drag slider for a float value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool DragFloat(ReadOnlySpan<byte> label, StateRef<float> v, float vSpeed, float vMin = 0.0f, float vMax = 0.0f, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragFloatEx(labelPtr, v.Ptr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for 2 float values.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Span containing 2 float values to edit.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool DragFloat2(ReadOnlySpan<byte> label, Span<float> v, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (float* vPtr = v)
        {
            return ImGui_DragFloat2Ex(labelPtr, vPtr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for 3 float values.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Span containing 3 float values to edit.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool DragFloat3(ReadOnlySpan<byte> label, Span<float> v, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (float* vPtr = v)
        {
            return ImGui_DragFloat3Ex(labelPtr, vPtr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for 4 float values.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Span containing 4 float values to edit.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if any value changed.</returns>
    public static bool DragFloat4(ReadOnlySpan<byte> label, Span<float> v, float vSpeed = 1.0f, float vMin = 0.0f, float vMax = 0.0f, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (float* vPtr = v)
        {
            return ImGui_DragFloat4Ex(labelPtr, vPtr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for a float range (min/max pair).
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="vCurrentMin">Reference to the minimum value.</param>
    /// <param name="vCurrentMax">Reference to the maximum value.</param>
    /// <returns>True if either value changed.</returns>
    public static bool DragFloatRange2(ReadOnlySpan<byte> label, StateRef<float> vCurrentMin, StateRef<float> vCurrentMax)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_DragFloatRange2(ptr, vCurrentMin.Ptr, vCurrentMax.Ptr);
        }
    }

    /// <summary>
    /// Creates a drag slider for a float range with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="vCurrentMin">Reference to the minimum value.</param>
    /// <param name="vCurrentMax">Reference to the maximum value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum allowed value.</param>
    /// <param name="vMax">Maximum allowed value.</param>
    /// <param name="format">Printf format string for the min value.</param>
    /// <param name="formatMax">Printf format string for the max value. If null, uses format.</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if either value changed.</returns>
    public static bool DragFloatRange2(ReadOnlySpan<byte> label, StateRef<float> vCurrentMin, StateRef<float> vCurrentMax, float vSpeed, float vMin = 0.0f, float vMax = 0.0f, ReadOnlySpan<byte> format = default, ReadOnlySpan<byte> formatMax = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* formatMaxPtr = formatMax)
        {
            return ImGui_DragFloatRange2Ex(labelPtr, vCurrentMin.Ptr, vCurrentMax.Ptr, vSpeed, vMin, vMax, formatPtr, formatMaxPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an integer value.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <returns>True if the value changed.</returns>
    public static bool DragInt(ReadOnlySpan<byte> label, StateRef<int> v)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_DragInt(ptr, v.Ptr);
        }
    }

    /// <summary>
    /// Creates a drag slider for an integer value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="vMax">Maximum value. If vMin >= vMax, there is no bound.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool DragInt(ReadOnlySpan<byte> label, StateRef<int> v, float vSpeed, int vMin = 0, int vMax = 0, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_DragIntEx(labelPtr, v.Ptr, vSpeed, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a drag slider for an integer range (min/max pair).
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="vCurrentMin">Reference to the minimum value.</param>
    /// <param name="vCurrentMax">Reference to the maximum value.</param>
    /// <returns>True if either value changed.</returns>
    public static bool DragIntRange2(ReadOnlySpan<byte> label, StateRef<int> vCurrentMin, StateRef<int> vCurrentMax)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_DragIntRange2(ptr, vCurrentMin.Ptr, vCurrentMax.Ptr);
        }
    }

    /// <summary>
    /// Creates a drag slider for an integer range with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="vCurrentMin">Reference to the minimum value.</param>
    /// <param name="vCurrentMax">Reference to the maximum value.</param>
    /// <param name="vSpeed">The speed of value change per pixel of mouse movement.</param>
    /// <param name="vMin">Minimum allowed value.</param>
    /// <param name="vMax">Maximum allowed value.</param>
    /// <param name="format">Printf format string for the min value.</param>
    /// <param name="formatMax">Printf format string for the max value. If null, uses format.</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if either value changed.</returns>
    public static bool DragIntRange2(ReadOnlySpan<byte> label, StateRef<int> vCurrentMin, StateRef<int> vCurrentMax, float vSpeed, int vMin = 0, int vMax = 0, ReadOnlySpan<byte> format = default, ReadOnlySpan<byte> formatMax = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        fixed (byte* formatMaxPtr = formatMax)
        {
            return ImGui_DragIntRange2Ex(labelPtr, vCurrentMin.Ptr, vCurrentMax.Ptr, vSpeed, vMin, vMax, formatPtr, formatMaxPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    #endregion

    #region Widgets: Regular Sliders

    /// <summary>
    /// Creates a slider for a float value.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <returns>True if the value changed.</returns>
    /// <remarks>
    /// Ctrl+Click on any slider to turn it into an input box.
    /// Manually input values aren't clamped by default. Use SliderFlags.AlwaysClamp to always clamp.
    /// </remarks>
    public static bool SliderFloat(ReadOnlySpan<byte> label, StateRef<float> v, float vMin, float vMax)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SliderFloat(ptr, v.Ptr, vMin, vMax);
        }
    }

    /// <summary>
    /// Creates a slider for a float value with explicit format and flags.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool SliderFloat(ReadOnlySpan<byte> label, StateRef<float> v, float vMin, float vMax, ReadOnlySpan<byte> format, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderFloatEx(labelPtr, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates an angle slider for a float value (in radians, displayed in degrees).
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="vRad">Reference to the value in radians.</param>
    /// <returns>True if the value changed.</returns>
    public static bool SliderAngle(ReadOnlySpan<byte> label, StateRef<float> vRad)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SliderAngle(ptr, vRad.Ptr);
        }
    }

    /// <summary>
    /// Creates an angle slider for a float value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="vRad">Reference to the value in radians.</param>
    /// <param name="vDegreesMin">Minimum value in degrees.</param>
    /// <param name="vDegreesMax">Maximum value in degrees.</param>
    /// <param name="format">Printf format string for display (e.g., "%.0f deg").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool SliderAngle(ReadOnlySpan<byte> label, StateRef<float> vRad, float vDegreesMin = -360.0f, float vDegreesMax = 360.0f, ReadOnlySpan<byte> format = default, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderAngleEx(labelPtr, vRad.Ptr, vDegreesMin, vDegreesMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a slider for an integer value.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <returns>True if the value changed.</returns>
    public static bool SliderInt(ReadOnlySpan<byte> label, StateRef<int> v, int vMin, int vMax)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SliderInt(ptr, v.Ptr, vMin, vMax);
        }
    }

    /// <summary>
    /// Creates a slider for an integer value with explicit format and flags.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool SliderInt(ReadOnlySpan<byte> label, StateRef<int> v, int vMin, int vMax, ReadOnlySpan<byte> format, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_SliderIntEx(labelPtr, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a vertical slider for a float value.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSliderFloat(ReadOnlySpan<byte> label, Vec2 size, StateRef<float> v, float vMin, float vMax)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_VSliderFloat(ptr, size.Value, v.Ptr, vMin, vMax);
        }
    }

    /// <summary>
    /// Creates a vertical slider for a float value with explicit format and flags.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%.3f").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSliderFloat(ReadOnlySpan<byte> label, Vec2 size, StateRef<float> v, float vMin, float vMax, ReadOnlySpan<byte> format, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_VSliderFloatEx(labelPtr, size.Value, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    /// <summary>
    /// Creates a vertical slider for an integer value.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSliderInt(ReadOnlySpan<byte> label, Vec2 size, StateRef<int> v, int vMin, int vMax)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_VSliderInt(ptr, size.Value, v.Ptr, vMin, vMax);
        }
    }

    /// <summary>
    /// Creates a vertical slider for an integer value with explicit format and flags.
    /// </summary>
    /// <param name="label">The label for the slider.</param>
    /// <param name="size">The size of the slider.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="vMin">Minimum value.</param>
    /// <param name="vMax">Maximum value.</param>
    /// <param name="format">Printf format string for display (e.g., "%d").</param>
    /// <param name="flags">Slider behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool VSliderInt(ReadOnlySpan<byte> label, Vec2 size, StateRef<int> v, int vMin, int vMax, ReadOnlySpan<byte> format, SliderFlags flags = SliderFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_VSliderIntEx(labelPtr, size.Value, v.Ptr, vMin, vMax, formatPtr, (Native.ImGuiSliderFlags)flags);
        }
    }

    #endregion

    #region Widgets: Input with Keyboard

    /// <summary>
    /// Creates a single-line text input widget.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="buf">The buffer to edit.</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool InputText(ReadOnlySpan<byte> label, Span<byte> buf, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* bufPtr = buf)
        {
            return ImGui_InputText(labelPtr, bufPtr, (nuint)buf.Length, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates a multi-line text input widget.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="buf">The buffer to edit.</param>
    /// <returns>True if the value changed.</returns>
    public static bool InputTextMultiline(ReadOnlySpan<byte> label, Span<byte> buf)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* bufPtr = buf)
        {
            return ImGui_InputTextMultiline(labelPtr, bufPtr, (nuint)buf.Length);
        }
    }

    /// <summary>
    /// Creates a multi-line text input widget with explicit size and flags.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="buf">The buffer to edit.</param>
    /// <param name="size">The size of the input area.</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool InputTextMultiline(ReadOnlySpan<byte> label, Span<byte> buf, Vec2 size, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* bufPtr = buf)
        {
            return ImGui_InputTextMultilineEx(labelPtr, bufPtr, (nuint)buf.Length, size.Value, (Native.ImGuiInputTextFlags)flags, null, 0);
        }
    }

    /// <summary>
    /// Creates a single-line text input widget with a hint.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="hint">The hint text displayed when empty.</param>
    /// <param name="buf">The buffer to edit.</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool InputTextWithHint(ReadOnlySpan<byte> label, ReadOnlySpan<byte> hint, Span<byte> buf, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* hintPtr = hint)
        fixed (byte* bufPtr = buf)
        {
            return ImGui_InputTextWithHint(labelPtr, hintPtr, bufPtr, (nuint)buf.Length, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates an input widget for a float value.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <returns>True if the value changed.</returns>
    public static bool InputFloat(ReadOnlySpan<byte> label, StateRef<float> v)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_InputFloat(ptr, v.Ptr);
        }
    }

    /// <summary>
    /// Creates an input widget for a float value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons.</param>
    /// <param name="stepFast">Fast step value when holding button.</param>
    /// <param name="format">Printf format string for display.</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool InputFloat(ReadOnlySpan<byte> label, StateRef<float> v, float step, float stepFast = 0.0f, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputFloatEx(labelPtr, v.Ptr, step, stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates an input widget for an integer value.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <returns>True if the value changed.</returns>
    public static bool InputInt(ReadOnlySpan<byte> label, StateRef<int> v)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_InputInt(ptr, v.Ptr);
        }
    }

    /// <summary>
    /// Creates an input widget for an integer value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons.</param>
    /// <param name="stepFast">Fast step value when holding button.</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool InputInt(ReadOnlySpan<byte> label, StateRef<int> v, int step, int stepFast = 100, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_InputIntEx(ptr, v.Ptr, step, stepFast, (Native.ImGuiInputTextFlags)flags);
        }
    }

    /// <summary>
    /// Creates an input widget for a double value.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <returns>True if the value changed.</returns>
    public static bool InputDouble(ReadOnlySpan<byte> label, StateRef<double> v)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_InputDouble(ptr, v.Ptr);
        }
    }

    /// <summary>
    /// Creates an input widget for a double value with explicit parameters.
    /// </summary>
    /// <param name="label">The label for the input.</param>
    /// <param name="v">Reference to the value.</param>
    /// <param name="step">Step value for +/- buttons.</param>
    /// <param name="stepFast">Fast step value when holding button.</param>
    /// <param name="format">Printf format string for display.</param>
    /// <param name="flags">Input text behavior flags.</param>
    /// <returns>True if the value changed.</returns>
    public static bool InputDouble(ReadOnlySpan<byte> label, StateRef<double> v, double step, double stepFast = 0.0, ReadOnlySpan<byte> format = default, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* formatPtr = format)
        {
            return ImGui_InputDoubleEx(labelPtr, v.Ptr, step, stepFast, formatPtr, (Native.ImGuiInputTextFlags)flags);
        }
    }

    #endregion

    #region Widgets: Color Editor/Picker

    /// <summary>
    /// Displays a color button that opens a color picker when clicked.
    /// </summary>
    /// <param name="descId">Description ID for the button.</param>
    /// <param name="col">The color to display.</param>
    /// <param name="flags">Color edit behavior flags.</param>
    /// <returns>True when clicked.</returns>
    public static bool ColorButton(ReadOnlySpan<byte> descId, Vec4 col, ColorEditFlags flags = ColorEditFlags.None)
    {
        fixed (byte* ptr = descId)
        {
            return ImGui_ColorButton(ptr, col.ToNative(), (Native.ImGuiColorEditFlags)flags);
        }
    }

    /// <summary>
    /// Displays a color button with explicit size that opens a color picker when clicked.
    /// </summary>
    /// <param name="descId">Description ID for the button.</param>
    /// <param name="col">The color to display.</param>
    /// <param name="flags">Color edit behavior flags.</param>
    /// <param name="size">The button size.</param>
    /// <returns>True when clicked.</returns>
    public static bool ColorButton(ReadOnlySpan<byte> descId, Vec4 col, ColorEditFlags flags, Vec2 size)
    {
        fixed (byte* ptr = descId)
        {
            return ImGui_ColorButtonEx(ptr, col.ToNative(), (Native.ImGuiColorEditFlags)flags, size.Value);
        }
    }

    /// <summary>
    /// Sets the default color edit options.
    /// </summary>
    /// <param name="flags">Color edit behavior flags to use as defaults.</param>
    /// <remarks>
    /// Initialize current options (generally on application startup) if you want to select
    /// a default format, picker type, etc. User will be able to change many settings,
    /// unless you pass the NoOptions flag to your calls.
    /// </remarks>
    public static void SetColorEditOptions(ColorEditFlags flags)
    {
        ImGui_SetColorEditOptions((Native.ImGuiColorEditFlags)flags);
    }

    #endregion

    #region Widgets: Trees

    /// <summary>
    /// Creates a tree node.
    /// </summary>
    /// <param name="label">The node label.</param>
    /// <returns>True if the node is open. Call <see cref="TreePop"/> when done if this returns true.</returns>
    public static bool TreeNode(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_TreeNode(ptr);
        }
    }

    /// <summary>
    /// Creates a tree node with flags.
    /// </summary>
    /// <param name="label">The node label.</param>
    /// <param name="flags">Tree node behavior flags.</param>
    /// <returns>True if the node is open. Call <see cref="TreePop"/> when done if this returns true.</returns>
    public static bool TreeNode(ReadOnlySpan<byte> label, TreeNodeFlags flags)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_TreeNodeEx(ptr, (Native.ImGuiTreeNodeFlags)flags);
        }
    }

    /// <summary>
    /// Pushes a tree node onto the stack (equivalent to Indent + PushID).
    /// </summary>
    /// <param name="strId">The string ID to push.</param>
    /// <remarks>
    /// Already called by TreeNode() when returning true, but you can call TreePush/TreePop yourself if desired.
    /// </remarks>
    public static void TreePush(ReadOnlySpan<byte> strId)
    {
        fixed (byte* ptr = strId)
        {
            ImGui_TreePush(ptr);
        }
    }

    /// <summary>
    /// Pushes a tree node onto the stack using a pointer ID.
    /// </summary>
    /// <param name="ptrId">The pointer ID to push.</param>
    public static void TreePush(nint ptrId)
    {
        ImGui_TreePushPtr(ptrId);
    }

    /// <summary>
    /// Pops a tree node from the stack (equivalent to Unindent + PopID).
    /// </summary>
    public static void TreePop()
    {
        ImGui_TreePop();
    }

    /// <summary>
    /// Gets the horizontal distance preceding a label when using TreeNode or Bullet.
    /// </summary>
    /// <returns>The spacing in pixels.</returns>
    public static float GetTreeNodeToLabelSpacing()
    {
        return ImGui_GetTreeNodeToLabelSpacing();
    }

    /// <summary>
    /// Creates a collapsing header.
    /// </summary>
    /// <param name="label">The header label.</param>
    /// <param name="flags">Tree node behavior flags.</param>
    /// <returns>True if the header is open.</returns>
    /// <remarks>
    /// Doesn't indent or push onto the ID stack. User doesn't have to call TreePop().
    /// </remarks>
    public static bool CollapsingHeader(ReadOnlySpan<byte> label, TreeNodeFlags flags = TreeNodeFlags.None)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_CollapsingHeader(ptr, (Native.ImGuiTreeNodeFlags)flags);
        }
    }

    /// <summary>
    /// Creates a collapsing header with a close button.
    /// </summary>
    /// <param name="label">The header label.</param>
    /// <param name="pVisible">Reference to visibility state. If false, header is not displayed.</param>
    /// <param name="flags">Tree node behavior flags.</param>
    /// <returns>True if the header is open.</returns>
    public static bool CollapsingHeader(ReadOnlySpan<byte> label, StateRef<bool> pVisible, TreeNodeFlags flags = TreeNodeFlags.None)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_CollapsingHeaderBoolPtr(ptr, pVisible.Ptr, (Native.ImGuiTreeNodeFlags)flags);
        }
    }

    /// <summary>
    /// Sets the next TreeNode/CollapsingHeader open state.
    /// </summary>
    /// <param name="isOpen">Whether the node should be open.</param>
    /// <param name="cond">Condition for applying the state.</param>
    public static void SetNextItemOpen(bool isOpen, Cond cond = Cond.None)
    {
        ImGui_SetNextItemOpen(isOpen, (Native.ImGuiCond)cond);
    }

    /// <summary>
    /// Sets the ID to use for open/close storage (default is same as item ID).
    /// </summary>
    /// <param name="storageId">The storage ID.</param>
    public static void SetNextItemStorageID(Id storageId)
    {
        ImGui_SetNextItemStorageID(storageId.Value);
    }

    #endregion

    #region Widgets: Selectables

    /// <summary>
    /// Creates a selectable item.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <returns>True when clicked.</returns>
    public static bool Selectable(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_Selectable(ptr);
        }
    }

    /// <summary>
    /// Creates a selectable item with explicit parameters.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <param name="selected">Whether the item is currently selected (read-only).</param>
    /// <param name="flags">Selectable behavior flags.</param>
    /// <param name="size">The item size.</param>
    /// <returns>True when clicked.</returns>
    public static bool Selectable(ReadOnlySpan<byte> label, bool selected, SelectableFlags flags = SelectableFlags.None, Vec2 size = default)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SelectableEx(ptr, selected, (Native.ImGuiSelectableFlags)flags, size.Value);
        }
    }

    /// <summary>
    /// Creates a selectable item with mutable selection state.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <param name="pSelected">Reference to selection state (read-write).</param>
    /// <param name="flags">Selectable behavior flags.</param>
    /// <returns>True when clicked.</returns>
    public static bool Selectable(ReadOnlySpan<byte> label, StateRef<bool> pSelected, SelectableFlags flags = SelectableFlags.None)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SelectableBoolPtr(ptr, pSelected.Ptr, (Native.ImGuiSelectableFlags)flags);
        }
    }

    /// <summary>
    /// Creates a selectable item with mutable selection state and explicit size.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <param name="pSelected">Reference to selection state (read-write).</param>
    /// <param name="flags">Selectable behavior flags.</param>
    /// <param name="size">The item size.</param>
    /// <returns>True when clicked.</returns>
    public static bool Selectable(ReadOnlySpan<byte> label, StateRef<bool> pSelected, SelectableFlags flags, Vec2 size)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_SelectableBoolPtrEx(ptr, pSelected.Ptr, (Native.ImGuiSelectableFlags)flags, size.Value);
        }
    }

    #endregion

    #region Widgets: List Boxes

    /// <summary>
    /// Begins a list box. Must be followed by <see cref="EndListBox"/> if this returns true.
    /// </summary>
    /// <param name="label">The label for the list box.</param>
    /// <param name="size">The size of the list box.</param>
    /// <returns>True if the list box is open and items should be rendered.</returns>
    public static bool BeginListBox(ReadOnlySpan<byte> label, Vec2 size = default)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_BeginListBox(ptr, size.Value);
        }
    }

    /// <summary>
    /// Ends a list box. Only call this if <see cref="BeginListBox"/> returned true.
    /// </summary>
    public static void EndListBox()
    {
        ImGui_EndListBox();
    }

    #endregion

    #region Widgets: Data Plotting

    /// <summary>
    /// Plots a line graph from an array of values.
    /// </summary>
    /// <param name="label">The label for the plot.</param>
    /// <param name="values">The array of values to plot.</param>
    public static void PlotLines(ReadOnlySpan<byte> label, ReadOnlySpan<float> values)
    {
        fixed (byte* labelPtr = label)
        fixed (float* valuesPtr = values)
        {
            ImGui_PlotLines(labelPtr, valuesPtr, values.Length);
        }
    }

    /// <summary>
    /// Plots a line graph from an array of values with extended options.
    /// </summary>
    /// <param name="label">The label for the plot.</param>
    /// <param name="values">The array of values to plot.</param>
    /// <param name="valuesOffset">Index offset into the values array.</param>
    /// <param name="overlayText">Text to overlay on the graph.</param>
    /// <param name="scaleMin">The minimum scale value (float.MaxValue for auto).</param>
    /// <param name="scaleMax">The maximum scale value (float.MaxValue for auto).</param>
    /// <param name="graphSize">The size of the graph (0,0 for default).</param>
    public static void PlotLines(ReadOnlySpan<byte> label, ReadOnlySpan<float> values, int valuesOffset, ReadOnlySpan<byte> overlayText, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, Vec2 graphSize = default)
    {
        fixed (byte* labelPtr = label)
        fixed (float* valuesPtr = values)
        fixed (byte* overlayPtr = overlayText)
        {
            ImGui_PlotLinesEx(labelPtr, valuesPtr, values.Length, valuesOffset, overlayPtr, scaleMin, scaleMax, graphSize.Value, sizeof(float));
        }
    }

    /// <summary>
    /// Plots a histogram from an array of values.
    /// </summary>
    /// <param name="label">The label for the plot.</param>
    /// <param name="values">The array of values to plot.</param>
    public static void PlotHistogram(ReadOnlySpan<byte> label, ReadOnlySpan<float> values)
    {
        fixed (byte* labelPtr = label)
        fixed (float* valuesPtr = values)
        {
            ImGui_PlotHistogram(labelPtr, valuesPtr, values.Length);
        }
    }

    /// <summary>
    /// Plots a histogram from an array of values with extended options.
    /// </summary>
    /// <param name="label">The label for the plot.</param>
    /// <param name="values">The array of values to plot.</param>
    /// <param name="valuesOffset">Index offset into the values array.</param>
    /// <param name="overlayText">Text to overlay on the graph.</param>
    /// <param name="scaleMin">The minimum scale value (float.MaxValue for auto).</param>
    /// <param name="scaleMax">The maximum scale value (float.MaxValue for auto).</param>
    /// <param name="graphSize">The size of the graph (0,0 for default).</param>
    public static void PlotHistogram(ReadOnlySpan<byte> label, ReadOnlySpan<float> values, int valuesOffset, ReadOnlySpan<byte> overlayText, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, Vec2 graphSize = default)
    {
        fixed (byte* labelPtr = label)
        fixed (float* valuesPtr = values)
        fixed (byte* overlayPtr = overlayText)
        {
            ImGui_PlotHistogramEx(labelPtr, valuesPtr, values.Length, valuesOffset, overlayPtr, scaleMin, scaleMax, graphSize.Value, sizeof(float));
        }
    }

    #endregion

    #region Widgets: Menus

    /// <summary>
    /// Begins appending to a menu bar of the current window.
    /// </summary>
    /// <returns>True if the menu bar is visible. Only call <see cref="EndMenuBar"/> if this returns true.</returns>
    /// <remarks>
    /// Requires the parent window to have the MenuBar window flag set.
    /// </remarks>
    public static bool BeginMenuBar()
    {
        return ImGui_BeginMenuBar();
    }

    /// <summary>
    /// Ends appending to the menu bar. Only call if <see cref="BeginMenuBar"/> returned true.
    /// </summary>
    public static void EndMenuBar()
    {
        ImGui_EndMenuBar();
    }

    /// <summary>
    /// Creates and appends to a full-screen menu bar.
    /// </summary>
    /// <returns>True if the main menu bar is visible. Only call <see cref="EndMainMenuBar"/> if this returns true.</returns>
    public static bool BeginMainMenuBar()
    {
        return ImGui_BeginMainMenuBar();
    }

    /// <summary>
    /// Ends the main menu bar. Only call if <see cref="BeginMainMenuBar"/> returned true.
    /// </summary>
    public static void EndMainMenuBar()
    {
        ImGui_EndMainMenuBar();
    }

    /// <summary>
    /// Creates a sub-menu entry.
    /// </summary>
    /// <param name="label">The menu label.</param>
    /// <returns>True if the menu is open. Only call <see cref="EndMenu"/> if this returns true.</returns>
    public static bool BeginMenu(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_BeginMenu(ptr);
        }
    }

    /// <summary>
    /// Creates a sub-menu entry with explicit enabled state.
    /// </summary>
    /// <param name="label">The menu label.</param>
    /// <param name="enabled">Whether the menu is enabled.</param>
    /// <returns>True if the menu is open. Only call <see cref="EndMenu"/> if this returns true.</returns>
    public static bool BeginMenu(ReadOnlySpan<byte> label, bool enabled)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_BeginMenuEx(ptr, enabled);
        }
    }

    /// <summary>
    /// Ends a menu. Only call if <see cref="BeginMenu"/> returned true.
    /// </summary>
    public static void EndMenu()
    {
        ImGui_EndMenu();
    }

    /// <summary>
    /// Creates a menu item.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <returns>True when activated.</returns>
    public static bool MenuItem(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_MenuItem(ptr);
        }
    }

    /// <summary>
    /// Creates a menu item with explicit parameters.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <param name="shortcut">Optional shortcut text displayed on the right.</param>
    /// <param name="selected">Whether to show a check mark.</param>
    /// <param name="enabled">Whether the item is enabled.</param>
    /// <returns>True when activated.</returns>
    public static bool MenuItem(ReadOnlySpan<byte> label, ReadOnlySpan<byte> shortcut, bool selected = false, bool enabled = true)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* shortcutPtr = shortcut)
        {
            return ImGui_MenuItemEx(labelPtr, shortcutPtr, selected, enabled);
        }
    }

    /// <summary>
    /// Creates a menu item with mutable selection state.
    /// </summary>
    /// <param name="label">The item label.</param>
    /// <param name="shortcut">Optional shortcut text displayed on the right.</param>
    /// <param name="pSelected">Reference to selection state (toggles on activation).</param>
    /// <param name="enabled">Whether the item is enabled.</param>
    /// <returns>True when activated.</returns>
    public static bool MenuItem(ReadOnlySpan<byte> label, ReadOnlySpan<byte> shortcut, StateRef<bool> pSelected, bool enabled = true)
    {
        fixed (byte* labelPtr = label)
        fixed (byte* shortcutPtr = shortcut)
        {
            return ImGui_MenuItemBoolPtr(labelPtr, shortcutPtr, pSelected.Ptr, enabled);
        }
    }

    #endregion

    #region Tooltips

    /// <summary>
    /// Begins a tooltip window.
    /// </summary>
    /// <returns>True if the tooltip is visible. Only call <see cref="EndTooltip"/> if this returns true.</returns>
    public static bool BeginTooltip()
    {
        return ImGui_BeginTooltip();
    }

    /// <summary>
    /// Ends a tooltip window. Only call if <see cref="BeginTooltip"/> or <see cref="BeginItemTooltip"/> returned true.
    /// </summary>
    public static void EndTooltip()
    {
        ImGui_EndTooltip();
    }

    /// <summary>
    /// Begins a tooltip window if the preceding item was hovered.
    /// </summary>
    /// <returns>True if the tooltip is visible. Only call <see cref="EndTooltip"/> if this returns true.</returns>
    /// <remarks>
    /// Shortcut for: if (IsItemHovered(HoveredFlags.ForTooltip) &amp;&amp; BeginTooltip())
    /// </remarks>
    public static bool BeginItemTooltip()
    {
        return ImGui_BeginItemTooltip();
    }

    /// <summary>
    /// Sets a text-only tooltip.
    /// </summary>
    /// <param name="text">The tooltip text.</param>
    /// <remarks>
    /// Often used after an IsItemHovered() check. Overrides any previous call to SetTooltip().
    /// </remarks>
    public static void SetTooltip(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            ImGui_SetTooltip(ptr);
        }
    }

    #endregion

    #region Popups, Modals

    /// <summary>
    /// Begins a popup window.
    /// </summary>
    /// <param name="strId">The popup string ID.</param>
    /// <param name="flags">Window behavior flags.</param>
    /// <returns>True if the popup is open. Only call <see cref="EndPopup"/> if this returns true.</returns>
    public static bool BeginPopup(ReadOnlySpan<byte> strId, WindowFlags flags = WindowFlags.None)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginPopup(ptr, (Native.ImGuiWindowFlags)flags);
        }
    }

    /// <summary>
    /// Begins a modal popup window.
    /// </summary>
    /// <param name="name">The modal name.</param>
    /// <param name="pOpen">Optional reference to open state. If provided, shows a close button.</param>
    /// <param name="flags">Window behavior flags.</param>
    /// <returns>True if the modal is open. Only call <see cref="EndPopup"/> if this returns true.</returns>
    /// <remarks>
    /// Modal windows block all interaction behind them and cannot be closed by clicking outside.
    /// </remarks>
    public static bool BeginPopupModal(ReadOnlySpan<byte> name, StateRef<bool>? pOpen = null, WindowFlags flags = WindowFlags.None)
    {
        fixed (byte* ptr = name)
        {
            return ImGui_BeginPopupModal(ptr, pOpen.HasValue ? pOpen.Value.Ptr : null, (Native.ImGuiWindowFlags)flags);
        }
    }

    /// <summary>
    /// Ends a popup window. Only call if BeginPopup/BeginPopupModal returned true.
    /// </summary>
    public static void EndPopup()
    {
        ImGui_EndPopup();
    }

    /// <summary>
    /// Opens a popup by string ID.
    /// </summary>
    /// <param name="strId">The popup string ID.</param>
    /// <param name="popupFlags">Popup behavior flags.</param>
    /// <remarks>
    /// Call to mark popup as open (don't call every frame!).
    /// </remarks>
    public static void OpenPopup(ReadOnlySpan<byte> strId, PopupFlags popupFlags = PopupFlags.None)
    {
        fixed (byte* ptr = strId)
        {
            ImGui_OpenPopup(ptr, (Native.ImGuiPopupFlags)popupFlags);
        }
    }

    /// <summary>
    /// Opens a popup by ID.
    /// </summary>
    /// <param name="id">The popup ID.</param>
    /// <param name="popupFlags">Popup behavior flags.</param>
    public static void OpenPopup(Id id, PopupFlags popupFlags = PopupFlags.None)
    {
        ImGui_OpenPopupID(id.Value, (Native.ImGuiPopupFlags)popupFlags);
    }

    /// <summary>
    /// Helper to open a popup when the last item was clicked.
    /// </summary>
    /// <param name="strId">The popup string ID. Use null to associate with previous item.</param>
    /// <param name="popupFlags">Popup behavior flags. Defaults to right mouse button.</param>
    public static void OpenPopupOnItemClick(ReadOnlySpan<byte> strId = default, PopupFlags popupFlags = PopupFlags.MouseButtonRight)
    {
        fixed (byte* ptr = strId)
        {
            ImGui_OpenPopupOnItemClick(ptr, (Native.ImGuiPopupFlags)popupFlags);
        }
    }

    /// <summary>
    /// Manually closes the current popup.
    /// </summary>
    public static void CloseCurrentPopup()
    {
        ImGui_CloseCurrentPopup();
    }

    /// <summary>
    /// Opens and begins a popup when the last item was clicked.
    /// </summary>
    /// <returns>True if the popup is open.</returns>
    public static bool BeginPopupContextItem()
    {
        return ImGui_BeginPopupContextItem();
    }

    /// <summary>
    /// Opens and begins a popup when the last item was clicked, with explicit parameters.
    /// </summary>
    /// <param name="strId">The popup string ID. Use null to associate with previous item.</param>
    /// <param name="popupFlags">Popup behavior flags. Defaults to right mouse button.</param>
    /// <returns>True if the popup is open.</returns>
    public static bool BeginPopupContextItem(ReadOnlySpan<byte> strId, PopupFlags popupFlags = PopupFlags.MouseButtonRight)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginPopupContextItemEx(ptr, (Native.ImGuiPopupFlags)popupFlags);
        }
    }

    /// <summary>
    /// Opens and begins a popup when the current window was clicked.
    /// </summary>
    /// <returns>True if the popup is open.</returns>
    public static bool BeginPopupContextWindow()
    {
        return ImGui_BeginPopupContextWindow();
    }

    /// <summary>
    /// Opens and begins a popup when the current window was clicked, with explicit parameters.
    /// </summary>
    /// <param name="strId">The popup string ID.</param>
    /// <param name="popupFlags">Popup behavior flags. Defaults to right mouse button.</param>
    /// <returns>True if the popup is open.</returns>
    public static bool BeginPopupContextWindow(ReadOnlySpan<byte> strId, PopupFlags popupFlags = PopupFlags.MouseButtonRight)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginPopupContextWindowEx(ptr, (Native.ImGuiPopupFlags)popupFlags);
        }
    }

    /// <summary>
    /// Opens and begins a popup when clicking in void (where there are no windows).
    /// </summary>
    /// <returns>True if the popup is open.</returns>
    public static bool BeginPopupContextVoid()
    {
        return ImGui_BeginPopupContextVoid();
    }

    /// <summary>
    /// Opens and begins a popup when clicking in void, with explicit parameters.
    /// </summary>
    /// <param name="strId">The popup string ID.</param>
    /// <param name="popupFlags">Popup behavior flags. Defaults to right mouse button.</param>
    /// <returns>True if the popup is open.</returns>
    public static bool BeginPopupContextVoid(ReadOnlySpan<byte> strId, PopupFlags popupFlags = PopupFlags.MouseButtonRight)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginPopupContextVoidEx(ptr, (Native.ImGuiPopupFlags)popupFlags);
        }
    }

    /// <summary>
    /// Checks if a popup is open.
    /// </summary>
    /// <param name="strId">The popup string ID.</param>
    /// <param name="flags">Popup flags for query behavior.</param>
    /// <returns>True if the popup is open.</returns>
    public static bool IsPopupOpen(ReadOnlySpan<byte> strId, PopupFlags flags = PopupFlags.None)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_IsPopupOpen(ptr, (Native.ImGuiPopupFlags)flags);
        }
    }

    #endregion

    #region Tables

    /// <summary>
    /// Begins a table.
    /// </summary>
    /// <param name="strId">The table string ID.</param>
    /// <param name="columns">The number of columns.</param>
    /// <param name="flags">Table behavior flags.</param>
    /// <returns>True if the table is visible. Only call <see cref="EndTable"/> if this returns true.</returns>
    public static bool BeginTable(ReadOnlySpan<byte> strId, int columns, TableFlags flags = TableFlags.None)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginTable(ptr, columns, (Native.ImGuiTableFlags)flags);
        }
    }

    /// <summary>
    /// Begins a table with explicit size parameters.
    /// </summary>
    /// <param name="strId">The table string ID.</param>
    /// <param name="columns">The number of columns.</param>
    /// <param name="flags">Table behavior flags.</param>
    /// <param name="outerSize">The outer size of the table.</param>
    /// <param name="innerWidth">The inner width for scrolling.</param>
    /// <returns>True if the table is visible. Only call <see cref="EndTable"/> if this returns true.</returns>
    public static bool BeginTable(ReadOnlySpan<byte> strId, int columns, TableFlags flags, Vec2 outerSize, float innerWidth = 0.0f)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginTableEx(ptr, columns, (Native.ImGuiTableFlags)flags, outerSize.Value, innerWidth);
        }
    }

    /// <summary>
    /// Ends a table. Only call if <see cref="BeginTable"/> returned true.
    /// </summary>
    public static void EndTable()
    {
        ImGui_EndTable();
    }

    /// <summary>
    /// Appends into the first cell of a new row.
    /// </summary>
    public static void TableNextRow()
    {
        ImGui_TableNextRow();
    }

    /// <summary>
    /// Appends into the first cell of a new row with explicit parameters.
    /// </summary>
    /// <param name="rowFlags">Row behavior flags.</param>
    /// <param name="minRowHeight">Minimum row height.</param>
    public static void TableNextRow(TableRowFlags rowFlags, float minRowHeight = 0.0f)
    {
        ImGui_TableNextRowEx((Native.ImGuiTableRowFlags)rowFlags, minRowHeight);
    }

    /// <summary>
    /// Appends into the next column (or first column of next row if currently in last column).
    /// </summary>
    /// <returns>True when the column is visible.</returns>
    public static bool TableNextColumn()
    {
        return ImGui_TableNextColumn();
    }

    /// <summary>
    /// Appends into the specified column.
    /// </summary>
    /// <param name="columnN">The column index.</param>
    /// <returns>True when the column is visible.</returns>
    public static bool TableSetColumnIndex(int columnN)
    {
        return ImGui_TableSetColumnIndex(columnN);
    }

    /// <summary>
    /// Sets up a column for the table.
    /// </summary>
    /// <param name="label">The column label.</param>
    /// <param name="flags">Column behavior flags.</param>
    public static void TableSetupColumn(ReadOnlySpan<byte> label, TableColumnFlags flags = TableColumnFlags.None)
    {
        fixed (byte* ptr = label)
        {
            ImGui_TableSetupColumn(ptr, (Native.ImGuiTableColumnFlags)flags);
        }
    }

    /// <summary>
    /// Sets up a column for the table with explicit parameters.
    /// </summary>
    /// <param name="label">The column label.</param>
    /// <param name="flags">Column behavior flags.</param>
    /// <param name="initWidthOrWeight">Initial width or weight depending on flags.</param>
    /// <param name="userId">User ID for the column.</param>
    public static void TableSetupColumn(ReadOnlySpan<byte> label, TableColumnFlags flags, float initWidthOrWeight, Id userId = default)
    {
        fixed (byte* ptr = label)
        {
            ImGui_TableSetupColumnEx(ptr, (Native.ImGuiTableColumnFlags)flags, initWidthOrWeight, userId.Value);
        }
    }

    /// <summary>
    /// Locks columns/rows so they stay visible when scrolled.
    /// </summary>
    /// <param name="cols">Number of columns to freeze.</param>
    /// <param name="rows">Number of rows to freeze.</param>
    public static void TableSetupScrollFreeze(int cols, int rows)
    {
        ImGui_TableSetupScrollFreeze(cols, rows);
    }

    /// <summary>
    /// Submits one header cell manually.
    /// </summary>
    /// <param name="label">The header label.</param>
    public static void TableHeader(ReadOnlySpan<byte> label)
    {
        fixed (byte* ptr = label)
        {
            ImGui_TableHeader(ptr);
        }
    }

    /// <summary>
    /// Submits a row with header cells based on data provided to <see cref="TableSetupColumn"/>.
    /// </summary>
    public static void TableHeadersRow()
    {
        ImGui_TableHeadersRow();
    }

    /// <summary>
    /// Submits a row with angled headers for every column with the AngledHeader flag.
    /// </summary>
    /// <remarks>
    /// Must be the first row.
    /// </remarks>
    public static void TableAngledHeadersRow()
    {
        ImGui_TableAngledHeadersRow();
    }

    /// <summary>
    /// Gets the number of columns in the current table.
    /// </summary>
    /// <returns>The column count.</returns>
    public static int TableGetColumnCount()
    {
        return ImGui_TableGetColumnCount();
    }

    /// <summary>
    /// Gets the current column index.
    /// </summary>
    /// <returns>The current column index.</returns>
    public static int TableGetColumnIndex()
    {
        return ImGui_TableGetColumnIndex();
    }

    /// <summary>
    /// Gets the current row index.
    /// </summary>
    /// <returns>The current row index.</returns>
    public static int TableGetRowIndex()
    {
        return ImGui_TableGetRowIndex();
    }

    /// <summary>
    /// Gets the column flags for a column.
    /// </summary>
    /// <param name="columnN">The column index. Use -1 for current column.</param>
    /// <returns>The column flags.</returns>
    public static TableColumnFlags TableGetColumnFlags(int columnN = -1)
    {
        return (TableColumnFlags)ImGui_TableGetColumnFlags(columnN);
    }

    /// <summary>
    /// Sets the enabled state of a column.
    /// </summary>
    /// <param name="columnN">The column index.</param>
    /// <param name="v">Whether the column is enabled.</param>
    public static void TableSetColumnEnabled(int columnN, bool v)
    {
        ImGui_TableSetColumnEnabled(columnN, v);
    }

    /// <summary>
    /// Gets the hovered column index.
    /// </summary>
    /// <returns>The hovered column index, or -1 if table is not hovered.</returns>
    public static int TableGetHoveredColumn()
    {
        return ImGui_TableGetHoveredColumn();
    }

    #endregion

    #region Tab Bars, Tabs

    /// <summary>
    /// Creates and appends into a tab bar.
    /// </summary>
    /// <param name="strId">The tab bar string ID.</param>
    /// <param name="flags">Tab bar behavior flags.</param>
    /// <returns>True if the tab bar is visible. Only call <see cref="EndTabBar"/> if this returns true.</returns>
    public static bool BeginTabBar(ReadOnlySpan<byte> strId, TabBarFlags flags = TabBarFlags.None)
    {
        fixed (byte* ptr = strId)
        {
            return ImGui_BeginTabBar(ptr, (Native.ImGuiTabBarFlags)flags);
        }
    }

    /// <summary>
    /// Ends a tab bar. Only call if <see cref="BeginTabBar"/> returned true.
    /// </summary>
    public static void EndTabBar()
    {
        ImGui_EndTabBar();
    }

    /// <summary>
    /// Creates a tab.
    /// </summary>
    /// <param name="label">The tab label.</param>
    /// <param name="pOpen">Optional reference to open state. If provided, shows a close button.</param>
    /// <param name="flags">Tab item behavior flags.</param>
    /// <returns>True if the tab is selected. Only call <see cref="EndTabItem"/> if this returns true.</returns>
    public static bool BeginTabItem(ReadOnlySpan<byte> label, StateRef<bool>? pOpen = null, TabItemFlags flags = TabItemFlags.None)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_BeginTabItem(ptr, pOpen.HasValue ? pOpen.Value.Ptr : null, (Native.ImGuiTabItemFlags)flags);
        }
    }

    /// <summary>
    /// Ends a tab item. Only call if <see cref="BeginTabItem"/> returned true.
    /// </summary>
    public static void EndTabItem()
    {
        ImGui_EndTabItem();
    }

    /// <summary>
    /// Creates a tab button (cannot be selected, returns true when clicked).
    /// </summary>
    /// <param name="label">The button label.</param>
    /// <param name="flags">Tab item behavior flags.</param>
    /// <returns>True when clicked.</returns>
    public static bool TabItemButton(ReadOnlySpan<byte> label, TabItemFlags flags = TabItemFlags.None)
    {
        fixed (byte* ptr = label)
        {
            return ImGui_TabItemButton(ptr, (Native.ImGuiTabItemFlags)flags);
        }
    }

    /// <summary>
    /// Notifies the tab bar of a closed tab/window ahead of time.
    /// </summary>
    /// <param name="tabOrDockedWindowLabel">The tab or window label.</param>
    public static void SetTabItemClosed(ReadOnlySpan<byte> tabOrDockedWindowLabel)
    {
        fixed (byte* ptr = tabOrDockedWindowLabel)
        {
            ImGui_SetTabItemClosed(ptr);
        }
    }

    #endregion

    #region Logging/Capture

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

    #endregion

    #region Drag and Drop

    /// <summary>
    /// Begins a drag-and-drop source. Call after submitting an item which may be dragged.
    /// </summary>
    /// <param name="flags">Drag-and-drop flags.</param>
    /// <returns>True if drag source is active; call SetDragDropPayload() + EndDragDropSource().</returns>
    public static bool BeginDragDropSource(DragDropFlags flags = DragDropFlags.None)
    {
        return ImGui_BeginDragDropSource((Native.ImGuiDragDropFlags)flags);
    }

    /// <summary>
    /// Sets the payload data for the current drag-and-drop operation.
    /// </summary>
    /// <param name="type">A user-defined string type (max 32 characters).</param>
    /// <param name="data">The data to be copied and held by ImGui.</param>
    /// <param name="cond">Condition for setting the payload.</param>
    /// <returns>True when payload has been accepted.</returns>
    public static bool SetDragDropPayload(ReadOnlySpan<byte> type, ReadOnlySpan<byte> data, Cond cond = Cond.None)
    {
        fixed (byte* typePtr = type)
        fixed (byte* dataPtr = data)
        {
            return ImGui_SetDragDropPayload(typePtr, (nint)dataPtr, (nuint)data.Length, (Native.ImGuiCond)cond);
        }
    }

    /// <summary>
    /// Ends the drag-and-drop source. Only call if BeginDragDropSource() returned true.
    /// </summary>
    public static void EndDragDropSource()
    {
        ImGui_EndDragDropSource();
    }

    /// <summary>
    /// Begins a drag-and-drop target. Call after submitting an item that may receive a payload.
    /// </summary>
    /// <returns>True if can accept payload; call AcceptDragDropPayload() + EndDragDropTarget().</returns>
    public static bool BeginDragDropTarget()
    {
        return ImGui_BeginDragDropTarget();
    }

    /// <summary>
    /// Ends the drag-and-drop target. Only call if BeginDragDropTarget() returned true.
    /// </summary>
    public static void EndDragDropTarget()
    {
        ImGui_EndDragDropTarget();
    }

    #endregion

    #region Clipping

    /// <summary>
    /// Pushes a clipping rectangle for both ImGui logic (hit testing) and rendering.
    /// </summary>
    /// <param name="clipRectMin">The minimum corner of the clip rectangle.</param>
    /// <param name="clipRectMax">The maximum corner of the clip rectangle.</param>
    /// <param name="intersectWithCurrentClipRect">Whether to intersect with the current clip rectangle.</param>
    public static void PushClipRect(Vec2 clipRectMin, Vec2 clipRectMax, bool intersectWithCurrentClipRect)
    {
        ImGui_PushClipRect(clipRectMin.Value, clipRectMax.Value, intersectWithCurrentClipRect);
    }

    /// <summary>
    /// Pops the last clip rectangle.
    /// </summary>
    public static void PopClipRect()
    {
        ImGui_PopClipRect();
    }

    #endregion

    #region Focus, Activation

    /// <summary>
    /// Makes the last item the default focused item of a newly appearing window.
    /// </summary>
    public static void SetItemDefaultFocus()
    {
        ImGui_SetItemDefaultFocus();
    }

    /// <summary>
    /// Focuses keyboard on the next widget.
    /// </summary>
    public static void SetKeyboardFocusHere()
    {
        ImGui_SetKeyboardFocusHere();
    }

    /// <summary>
    /// Focuses keyboard on a widget relative to current position.
    /// </summary>
    /// <param name="offset">Use positive offset to access sub components, -1 for previous widget.</param>
    public static void SetKeyboardFocusHere(int offset)
    {
        ImGui_SetKeyboardFocusHereEx(offset);
    }

    #endregion

    #region Keyboard/Gamepad Navigation

    #endregion

    #region Overlapping mode

    /// <summary>
    /// Allows the next item to be overlapped by a subsequent item.
    /// </summary>
    public static void SetNextItemAllowOverlap()
    {
        ImGui_SetNextItemAllowOverlap();
    }

    #endregion

    #region Item/Widgets Utilities and Query Functions

    /// <summary>
    /// Checks if the last item is hovered.
    /// </summary>
    /// <param name="flags">Hover behavior flags.</param>
    /// <returns>True if the item is hovered.</returns>
    public static bool IsItemHovered(HoveredFlags flags = HoveredFlags.None)
    {
        return ImGui_IsItemHovered((Native.ImGuiHoveredFlags)flags);
    }

    /// <summary>
    /// Checks if the last item is active (e.g., button being held, text field being edited).
    /// </summary>
    /// <returns>True if the item is active.</returns>
    public static bool IsItemActive()
    {
        return ImGui_IsItemActive();
    }

    /// <summary>
    /// Checks if the last item is focused for keyboard/gamepad navigation.
    /// </summary>
    /// <returns>True if the item is focused.</returns>
    public static bool IsItemFocused()
    {
        return ImGui_IsItemFocused();
    }

    /// <summary>
    /// Checks if the last item was clicked with the left mouse button.
    /// </summary>
    /// <returns>True if the item was clicked.</returns>
    public static bool IsItemClicked()
    {
        return ImGui_IsItemClicked();
    }

    /// <summary>
    /// Checks if the last item was clicked with a specific mouse button.
    /// </summary>
    /// <param name="mouseButton">The mouse button to check.</param>
    /// <returns>True if the item was clicked.</returns>
    public static bool IsItemClicked(MouseButton mouseButton)
    {
        return ImGui_IsItemClickedEx((Native.ImGuiMouseButton)mouseButton);
    }

    /// <summary>
    /// Checks if the last item is visible (not clipped/scrolled out of view).
    /// </summary>
    /// <returns>True if the item is visible.</returns>
    public static bool IsItemVisible()
    {
        return ImGui_IsItemVisible();
    }

    /// <summary>
    /// Checks if the last item modified its underlying value this frame.
    /// </summary>
    /// <returns>True if the item was edited.</returns>
    public static bool IsItemEdited()
    {
        return ImGui_IsItemEdited();
    }

    /// <summary>
    /// Checks if the last item was just made active (was previously inactive).
    /// </summary>
    /// <returns>True if the item was activated.</returns>
    public static bool IsItemActivated()
    {
        return ImGui_IsItemActivated();
    }

    /// <summary>
    /// Checks if the last item was just made inactive (was previously active).
    /// </summary>
    /// <returns>True if the item was deactivated.</returns>
    public static bool IsItemDeactivated()
    {
        return ImGui_IsItemDeactivated();
    }

    /// <summary>
    /// Checks if the last item was just made inactive and made a value change when active.
    /// </summary>
    /// <returns>True if the item was deactivated after edit.</returns>
    public static bool IsItemDeactivatedAfterEdit()
    {
        return ImGui_IsItemDeactivatedAfterEdit();
    }

    /// <summary>
    /// Checks if the last item's open state was toggled (set by TreeNode).
    /// </summary>
    /// <returns>True if the item was toggled open.</returns>
    public static bool IsItemToggledOpen()
    {
        return ImGui_IsItemToggledOpen();
    }

    /// <summary>
    /// Checks if any item is hovered.
    /// </summary>
    /// <returns>True if any item is hovered.</returns>
    public static bool IsAnyItemHovered()
    {
        return ImGui_IsAnyItemHovered();
    }

    /// <summary>
    /// Checks if any item is active.
    /// </summary>
    /// <returns>True if any item is active.</returns>
    public static bool IsAnyItemActive()
    {
        return ImGui_IsAnyItemActive();
    }

    /// <summary>
    /// Checks if any item is focused.
    /// </summary>
    /// <returns>True if any item is focused.</returns>
    public static bool IsAnyItemFocused()
    {
        return ImGui_IsAnyItemFocused();
    }

    /// <summary>
    /// Gets the ID of the last item.
    /// </summary>
    /// <returns>The ID of the last item.</returns>
    public static Id GetItemID()
    {
        return new(ImGui_GetItemID());
    }

    /// <summary>
    /// Gets the upper-left bounding rectangle of the last item (screen space).
    /// </summary>
    /// <returns>The minimum bounding rectangle position.</returns>
    public static Vec2 GetItemRectMin()
    {
        return new(ImGui_GetItemRectMin());
    }

    /// <summary>
    /// Gets the lower-right bounding rectangle of the last item (screen space).
    /// </summary>
    /// <returns>The maximum bounding rectangle position.</returns>
    public static Vec2 GetItemRectMax()
    {
        return new(ImGui_GetItemRectMax());
    }

    /// <summary>
    /// Gets the size of the last item.
    /// </summary>
    /// <returns>The size of the last item.</returns>
    public static Vec2 GetItemRectSize()
    {
        return new(ImGui_GetItemRectSize());
    }

    #endregion

    #region Miscellaneous Utilities

    /// <summary>
    /// Tests if a rectangle of given size starting from cursor position is visible/not clipped.
    /// </summary>
    /// <param name="size">The size of the rectangle to test.</param>
    /// <returns>True if the rectangle is visible.</returns>
    public static bool IsRectVisible(Vec2 size)
    {
        return ImGui_IsRectVisibleBySize(size.Value);
    }

    /// <summary>
    /// Tests if a rectangle in screen space is visible/not clipped.
    /// </summary>
    /// <param name="rectMin">The upper-left corner of the rectangle.</param>
    /// <param name="rectMax">The lower-right corner of the rectangle.</param>
    /// <returns>True if the rectangle is visible.</returns>
    public static bool IsRectVisible(Vec2 rectMin, Vec2 rectMax)
    {
        return ImGui_IsRectVisible(rectMin.Value, rectMax.Value);
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

    #endregion

    #region Text Utilities

    /// <summary>
    /// Calculates the size of text.
    /// </summary>
    /// <param name="text">The text to measure.</param>
    /// <returns>The calculated text size.</returns>
    public static Vec2 CalcTextSize(ReadOnlySpan<byte> text)
    {
        fixed (byte* ptr = text)
        {
            return new(ImGui_CalcTextSize(ptr));
        }
    }

    /// <summary>
    /// Calculates the size of text with extended options.
    /// </summary>
    /// <param name="text">The text to measure.</param>
    /// <param name="hideTextAfterDoubleHash">If true, stop measuring at ##.</param>
    /// <param name="wrapWidth">The wrap width (-1.0f for no wrapping).</param>
    /// <returns>The calculated text size.</returns>
    public static Vec2 CalcTextSize(ReadOnlySpan<byte> text, bool hideTextAfterDoubleHash, float wrapWidth = -1.0f)
    {
        fixed (byte* ptr = text)
        {
            return new(ImGui_CalcTextSizeEx(ptr, null, hideTextAfterDoubleHash, wrapWidth));
        }
    }

    #endregion

    #region Color Utilities

    /// <summary>
    /// Converts a 32-bit color value to a Vec4 float color.
    /// </summary>
    /// <param name="color">The 32-bit color value (0xRRGGBBAA or ImU32 format).</param>
    /// <returns>The color as a Vec4 (RGBA, 0-1 range).</returns>
    public static Vec4 ColorConvertU32ToFloat4(uint color)
    {
        return Vec4.FromNative(ImGui_ColorConvertU32ToFloat4(color));
    }

    /// <summary>
    /// Converts a Vec4 float color to a 32-bit color value.
    /// </summary>
    /// <param name="color">The color as a Vec4 (RGBA, 0-1 range).</param>
    /// <returns>The 32-bit color value.</returns>
    public static uint ColorConvertFloat4ToU32(Vec4 color)
    {
        return ImGui_ColorConvertFloat4ToU32(color.ToNative());
    }

    /// <summary>
    /// Converts RGB color values to HSV.
    /// </summary>
    /// <param name="r">Red component (0-1).</param>
    /// <param name="g">Green component (0-1).</param>
    /// <param name="b">Blue component (0-1).</param>
    /// <returns>A tuple containing (H, S, V) values.</returns>
    public static (float H, float S, float V) ColorConvertRGBtoHSV(float r, float g, float b)
    {
        float h, s, v;
        ImGui_ColorConvertRGBtoHSV(r, g, b, &h, &s, &v);
        return (h, s, v);
    }

    /// <summary>
    /// Converts HSV color values to RGB.
    /// </summary>
    /// <param name="h">Hue component (0-1).</param>
    /// <param name="s">Saturation component (0-1).</param>
    /// <param name="v">Value component (0-1).</param>
    /// <returns>A tuple containing (R, G, B) values.</returns>
    public static (float R, float G, float B) ColorConvertHSVtoRGB(float h, float s, float v)
    {
        float r, g, b;
        ImGui_ColorConvertHSVtoRGB(h, s, v, &r, &g, &b);
        return (r, g, b);
    }

    #endregion

    #region Inputs Utilities: Keyboard/Mouse/Gamepad

    /// <summary>
    /// Checks if a key is being held.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key is down.</returns>
    public static bool IsKeyDown(Key key)
    {
        return ImGui_IsKeyDown((Native.ImGuiKey)key);
    }

    /// <summary>
    /// Checks if a key was pressed (went from !Down to Down).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key was pressed.</returns>
    public static bool IsKeyPressed(Key key)
    {
        return ImGui_IsKeyPressed((Native.ImGuiKey)key);
    }

    /// <summary>
    /// Checks if a key was pressed (went from !Down to Down).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <param name="repeat">If true, uses io.KeyRepeatDelay / KeyRepeatRate.</param>
    /// <returns>True if the key was pressed.</returns>
    public static bool IsKeyPressed(Key key, bool repeat)
    {
        return ImGui_IsKeyPressedEx((Native.ImGuiKey)key, repeat);
    }

    /// <summary>
    /// Checks if a key was released (went from Down to !Down).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key was released.</returns>
    public static bool IsKeyReleased(Key key)
    {
        return ImGui_IsKeyReleased((Native.ImGuiKey)key);
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
        return ImGui_GetKeyPressedAmount((Native.ImGuiKey)key, repeatDelay, rate);
    }

    /// <summary>
    /// Overrides the io.WantCaptureKeyboard flag next frame.
    /// </summary>
    /// <param name="wantCaptureKeyboard">Whether to capture keyboard input.</param>
    public static void SetNextFrameWantCaptureKeyboard(bool wantCaptureKeyboard)
    {
        ImGui_SetNextFrameWantCaptureKeyboard(wantCaptureKeyboard);
    }

    #endregion

    #region Inputs Utilities: Mouse

    /// <summary>
    /// Checks if a mouse button is held.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the mouse button is down.</returns>
    public static bool IsMouseDown(MouseButton button)
    {
        return ImGui_IsMouseDown((Native.ImGuiMouseButton)button);
    }

    /// <summary>
    /// Checks if a mouse button was clicked (went from !Down to Down).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the mouse button was clicked.</returns>
    public static bool IsMouseClicked(MouseButton button)
    {
        return ImGui_IsMouseClicked((Native.ImGuiMouseButton)button);
    }

    /// <summary>
    /// Checks if a mouse button was clicked (went from !Down to Down).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <param name="repeat">If true, uses io.KeyRepeatDelay / KeyRepeatRate.</param>
    /// <returns>True if the mouse button was clicked.</returns>
    public static bool IsMouseClicked(MouseButton button, bool repeat)
    {
        return ImGui_IsMouseClickedEx((Native.ImGuiMouseButton)button, repeat);
    }

    /// <summary>
    /// Checks if a mouse button was released (went from Down to !Down).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the mouse button was released.</returns>
    public static bool IsMouseReleased(MouseButton button)
    {
        return ImGui_IsMouseReleased((Native.ImGuiMouseButton)button);
    }

    /// <summary>
    /// Checks if a mouse button was double-clicked.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the mouse button was double-clicked.</returns>
    public static bool IsMouseDoubleClicked(MouseButton button)
    {
        return ImGui_IsMouseDoubleClicked((Native.ImGuiMouseButton)button);
    }

    /// <summary>
    /// Gets the number of successive mouse clicks at the time of click (otherwise 0).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>The click count.</returns>
    public static int GetMouseClickedCount(MouseButton button)
    {
        return ImGui_GetMouseClickedCount((Native.ImGuiMouseButton)button);
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
        return ImGui_IsMouseDragging((Native.ImGuiMouseButton)button, lockThreshold);
    }

    /// <summary>
    /// Gets the delta from the initial clicking position while the mouse button is pressed.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <param name="lockThreshold">The distance threshold (-1.0f uses io.MouseDraggingThreshold).</param>
    /// <returns>The drag delta.</returns>
    public static Vec2 GetMouseDragDelta(MouseButton button = MouseButton.Left, float lockThreshold = -1.0f)
    {
        return new(ImGui_GetMouseDragDelta((Native.ImGuiMouseButton)button, lockThreshold));
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
            ImGui_ResetMouseDragDeltaEx((Native.ImGuiMouseButton)button);
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
        ImGui_SetMouseCursor((Native.ImGuiMouseCursor)cursorType);
    }

    /// <summary>
    /// Overrides the io.WantCaptureMouse flag next frame.
    /// </summary>
    /// <param name="wantCaptureMouse">Whether to capture mouse input.</param>
    public static void SetNextFrameWantCaptureMouse(bool wantCaptureMouse)
    {
        ImGui_SetNextFrameWantCaptureMouse(wantCaptureMouse);
    }

    #endregion

    #region Clipboard Utilities

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

    #endregion

    #region Settings/.Ini Utilities

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

    #endregion
}
