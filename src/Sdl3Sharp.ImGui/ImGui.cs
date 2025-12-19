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
    // Windows Utilities
    //

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

    /// <summary>
    /// Gets the current window position in screen space.
    /// </summary>
    /// <returns>The current window position.</returns>
    /// <remarks>
    /// It is unlikely you ever need to use this. Consider using <see cref="GetCursorScreenPos"/> and <see cref="GetContentRegionAvail"/> instead.
    /// </remarks>
    public static Vec2 GetWindowPos()
    {
        return Vec2.FromNative(ImGui_GetWindowPos());
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
        return Vec2.FromNative(ImGui_GetWindowSize());
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

    //
    // Window Manipulation
    //

    /// <summary>
    /// Sets the next window position. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="pos">The position in screen coordinates.</param>
    /// <param name="cond">Condition for applying the position.</param>
    /// <remarks>
    /// Prefer using SetNextWindow*** functions (before Begin) rather than SetWindow*** functions (after Begin).
    /// </remarks>
    public static void SetNextWindowPos(Vec2 pos, Cond cond = Cond.None)
    {
        ImGui_SetNextWindowPos(pos.ToNative(), (Native.ImGuiCond)cond);
    }

    /// <summary>
    /// Sets the next window position with a pivot point. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="pos">The position in screen coordinates.</param>
    /// <param name="cond">Condition for applying the position.</param>
    /// <param name="pivot">The pivot point (0,0) = top-left, (0.5,0.5) = center, (1,1) = bottom-right.</param>
    /// <remarks>
    /// Use pivot=(0.5f, 0.5f) to center on the given point.
    /// </remarks>
    public static void SetNextWindowPos(Vec2 pos, Cond cond, Vec2 pivot)
    {
        ImGui_SetNextWindowPosEx(pos.ToNative(), (Native.ImGuiCond)cond, pivot.ToNative());
    }

    /// <summary>
    /// Sets the next window size. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="size">The window size. Set an axis to 0.0f to force auto-fit on that axis.</param>
    /// <param name="cond">Condition for applying the size.</param>
    public static void SetNextWindowSize(Vec2 size, Cond cond = Cond.None)
    {
        ImGui_SetNextWindowSize(size.ToNative(), (Native.ImGuiCond)cond);
    }

    /// <summary>
    /// Sets the next window size constraints. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="sizeMin">Minimum window size. Use 0.0f for no minimum. Use -1 for both min and max of same axis to preserve current size.</param>
    /// <param name="sizeMax">Maximum window size. Use float.MaxValue for no maximum. Use -1 for both min and max of same axis to preserve current size.</param>
    public static void SetNextWindowSizeConstraints(Vec2 sizeMin, Vec2 sizeMax)
    {
        ImGui_SetNextWindowSizeConstraints(sizeMin.ToNative(), sizeMax.ToNative(), null, 0);
    }

    /// <summary>
    /// Sets the next window content size (scrollable client area). Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="size">The content size. Does not include window decorations (title bar, menu bar, etc.) or WindowPadding. Set an axis to 0.0f to leave it automatic.</param>
    public static void SetNextWindowContentSize(Vec2 size)
    {
        ImGui_SetNextWindowContentSize(size.ToNative());
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
        ImGui_SetNextWindowScroll(scroll.ToNative());
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
        ImGui_SetWindowPos(pos.ToNative(), (Native.ImGuiCond)cond);
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
        ImGui_SetWindowSize(size.ToNative(), (Native.ImGuiCond)cond);
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
    public static void SetWindowPos(Span<byte> name, Vec2 pos, Cond cond = Cond.None)
    {
        fixed (byte* ptr = name)
        {
            ImGui_SetWindowPosStr(ptr, pos.ToNative(), (Native.ImGuiCond)cond);
        }
    }

    /// <summary>
    /// Sets a named window's size.
    /// </summary>
    /// <param name="name">The window name.</param>
    /// <param name="size">The window size. Set an axis to 0.0f to force auto-fit on that axis.</param>
    /// <param name="cond">Condition for applying the size.</param>
    public static void SetWindowSize(Span<byte> name, Vec2 size, Cond cond = Cond.None)
    {
        fixed (byte* ptr = name)
        {
            ImGui_SetWindowSizeStr(ptr, size.ToNative(), (Native.ImGuiCond)cond);
        }
    }

    /// <summary>
    /// Sets a named window's collapsed state.
    /// </summary>
    /// <param name="name">The window name.</param>
    /// <param name="collapsed">Whether the window should be collapsed.</param>
    /// <param name="cond">Condition for applying the collapsed state.</param>
    public static void SetWindowCollapsed(Span<byte> name, bool collapsed, Cond cond = Cond.None)
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
    public static void SetWindowFocus(Span<byte> name)
    {
        fixed (byte* ptr = name)
        {
            ImGui_SetWindowFocusStr(ptr);
        }
    }

    //
    // Windows Scrolling
    //

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

    //
    // Parameters stacks (font)
    //

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
        ImGui_PushFontFloat(font.HasValue ? font.Value.Native : null, fontSizeBaseUnscaled);
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

    //
    // Parameters stacks (shared)
    //

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
    /// Pops the most recently pushed style color from the stack.
    /// </summary>
    public static void PopStyleColor()
    {
        ImGui_PopStyleColor();
    }

    /// <summary>
    /// Pops multiple style colors from the stack.
    /// </summary>
    /// <param name="count">The number of style colors to pop.</param>
    public static void PopStyleColor(int count)
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
        ImGui_PushStyleVarImVec2((Native.ImGuiStyleVar)idx, val.ToNative());
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
    /// Pops the most recently pushed style variable from the stack.
    /// </summary>
    public static void PopStyleVar()
    {
        ImGui_PopStyleVar();
    }

    /// <summary>
    /// Pops multiple style variables from the stack.
    /// </summary>
    /// <param name="count">The number of style variables to pop.</param>
    public static void PopStyleVar(int count)
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

    //
    // Parameters stacks (current window)
    //

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

    //
    // Style read access
    //

    /// <summary>
    /// Gets the UV coordinate for a white pixel, useful to draw custom shapes via the DrawList API.
    /// </summary>
    /// <returns>The UV coordinate for a white pixel in the font texture.</returns>
    public static Vec2 GetFontTexUvWhitePixel()
    {
        return Vec2.FromNative(ImGui_GetFontTexUvWhitePixel());
    }

    /// <summary>
    /// Gets a style color as a 32-bit packed value suitable for DrawList.
    /// </summary>
    /// <param name="idx">The color index.</param>
    /// <returns>The color as a 32-bit packed RGBA value with style alpha applied.</returns>
    public static uint GetColorU32(Col idx)
    {
        return ImGui_GetColorU32((Native.ImGuiCol)idx);
    }

    /// <summary>
    /// Gets a style color as a 32-bit packed value with an additional alpha multiplier.
    /// </summary>
    /// <param name="idx">The color index.</param>
    /// <param name="alphaMul">Additional alpha multiplier (0.0 to 1.0).</param>
    /// <returns>The color as a 32-bit packed RGBA value with style alpha and multiplier applied.</returns>
    public static uint GetColorU32(Col idx, float alphaMul)
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
    /// Gets a 32-bit color with style alpha applied.
    /// </summary>
    /// <param name="col">The color as a 32-bit packed RGBA value.</param>
    /// <returns>The color with style alpha applied.</returns>
    public static uint GetColorU32(uint col)
    {
        return ImGui_GetColorU32ImU32(col);
    }

    /// <summary>
    /// Gets a 32-bit color with style alpha and an additional alpha multiplier applied.
    /// </summary>
    /// <param name="col">The color as a 32-bit packed RGBA value.</param>
    /// <param name="alphaMul">Additional alpha multiplier (0.0 to 1.0).</param>
    /// <returns>The color with style alpha and multiplier applied.</returns>
    public static uint GetColorU32(uint col, float alphaMul)
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

    //
    // Layout cursor positioning
    //

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
        return Vec2.FromNative(ImGui_GetCursorScreenPos());
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
        ImGui_SetCursorScreenPos(pos.ToNative());
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
        return Vec2.FromNative(ImGui_GetContentRegionAvail());
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
        return Vec2.FromNative(ImGui_GetCursorPos());
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
        ImGui_SetCursorPos(localPos.ToNative());
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
        return Vec2.FromNative(ImGui_GetCursorStartPos());
    }

    //
    // Other layout functions
    //

    /// <summary>
    /// Draws a separator, generally horizontal. Inside a menu bar or in horizontal layout mode, this becomes a vertical separator.
    /// </summary>
    public static void Separator()
    {
        ImGui_Separator();
    }

    /// <summary>
    /// Calls between widgets or groups to layout them horizontally.
    /// </summary>
    public static void SameLine()
    {
        ImGui_SameLine();
    }

    /// <summary>
    /// Calls between widgets or groups to layout them horizontally with custom positioning.
    /// </summary>
    /// <param name="offsetFromStartX">X position from window start in window coordinates. 0.0f to use current position.</param>
    /// <param name="spacing">Spacing between the previous widget and current position. -1.0f to use default spacing.</param>
    public static void SameLine(float offsetFromStartX, float spacing = -1.0f)
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
        ImGui_Dummy(size.ToNative());
    }

    /// <summary>
    /// Moves content position toward the right by style.IndentSpacing.
    /// </summary>
    public static void Indent()
    {
        ImGui_Indent();
    }

    /// <summary>
    /// Moves content position toward the right by the specified amount.
    /// </summary>
    /// <param name="indentW">The indentation amount. If less than or equal to 0, uses style.IndentSpacing.</param>
    public static void Indent(float indentW)
    {
        ImGui_IndentEx(indentW);
    }

    /// <summary>
    /// Moves content position back to the left by style.IndentSpacing.
    /// </summary>
    public static void Unindent()
    {
        ImGui_Unindent();
    }

    /// <summary>
    /// Moves content position back to the left by the specified amount.
    /// </summary>
    /// <param name="indentW">The unindentation amount. If less than or equal to 0, uses style.IndentSpacing.</param>
    public static void Unindent(float indentW)
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
