using Sdl3Sharp.ImGui.Native;
using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public static unsafe class Window
{
    /// <summary>
    /// Returns true if the current window is appearing (was just created or unhidden this frame).
    /// </summary>
    /// <returns>True if the current window is appearing.</returns>
    public static bool IsAppearing => ImGui_IsWindowAppearing();

    /// <summary>
    /// Returns true if the current window is collapsed.
    /// </summary>
    /// <returns>True if the current window is collapsed.</returns>
    public static bool IsCollapsed => ImGui_IsWindowCollapsed();

    // Not wrapping ImGui_GetWindowDrawList at this time

    /// <summary>
    /// Gets the current window position in screen space.
    /// </summary>
    /// <returns>The current window position.</returns>
    public static Point Position => new(ImGui_GetWindowPos());

    /// <summary>
    /// Gets the current window size.
    /// </summary>
    /// <returns>The current window size.</returns>
    public static Size Size => new(ImGui_GetWindowSize());

    /// <summary>
    /// Gets the current window width.
    /// </summary>
    /// <returns>The current window width.</returns>
    public static float Width => ImGui_GetWindowWidth();

    /// <summary>
    /// Gets the current window height.
    /// </summary>
    /// <returns>The current window height.</returns>
    public static float Height => ImGui_GetWindowHeight();

    /// <summary>
    /// Gets or sets the horizontal scrolling amount [0 .. <see cref="ScrollMaxX"/>].
    /// </summary>
    /// <remarks>
    /// Any change of scroll will be applied at the beginning of next frame in the first call to <see cref="Window(ReadOnlySpan{byte}, StateRef{bool}?, WindowFlags)"/>.
    /// You may use <see cref="SetNextWindowScroll"/> prior to calling the constructor to avoid this delay.
    /// </remarks>
    public static float ScrollX
    {
        get => ImGui_GetScrollX();
        set => ImGui_SetScrollX(value);
    }

    /// <summary>
    /// Gets or sets the vertical scrolling amount [0 .. <see cref="ScrollMaxY"/>].
    /// </summary>
    /// <remarks>
    /// Any change of scroll will be applied at the beginning of next frame in the first call to <see cref="Window(ReadOnlySpan{byte}, StateRef{bool}?, WindowFlags)"/>.
    /// You may use <see cref="SetNextWindowScroll"/> prior to calling the constructor to avoid this delay.
    /// </remarks>
    public static float ScrollY
    {
        get => ImGui_GetScrollY();
        set => ImGui_SetScrollY(value);
    }

    /// <summary>
    /// Gets the maximum horizontal scrolling amount (~~ ContentSize.X - WindowSize.X - DecorationsSize.X).
    /// </summary>
    public static float ScrollMaxX => ImGui_GetScrollMaxX();

    /// <summary>
    /// Gets the maximum vertical scrolling amount (~~ ContentSize.Y - WindowSize.Y - DecorationsSize.Y).
    /// </summary>
    public static float ScrollMaxY => ImGui_GetScrollMaxY();

    /// <summary>
    /// Gets or sets the cursor position in absolute screen coordinates.
    /// </summary>
    /// <remarks>
    /// This is your best friend! Prefer using this rather than <see cref="CursorPos"/>.
    /// Also more useful when working with the DrawList API.
    /// </remarks>
    public static Point CursorScreenPosition
    {
        get => new(ImGui_GetCursorScreenPos());
        set => ImGui_SetCursorScreenPos(value.Value);
    }

    /// <summary>
    /// Gets the available space from the current cursor position.
    /// </summary>
    /// <remarks>
    /// This is your best friend!
    /// </remarks>
    public static Size ContentRegionAvail => new(ImGui_GetContentRegionAvail());

    /// <summary>
    /// Gets or sets the cursor position in window-local coordinates.
    /// </summary>
    /// <remarks>
    /// This is not your best friend. Prefer <see cref="CursorScreenPos"/>.
    /// </remarks>
    public static Point CursorPosition
    {
        get => new(ImGui_GetCursorPos());
        set => ImGui_SetCursorPos(value.Value);
    }

    /// <summary>
    /// Gets or sets the cursor X position in window-local coordinates.
    /// </summary>
    public static float CursorPositionX
    {
        get => ImGui_GetCursorPosX();
        set => ImGui_SetCursorPosX(value);
    }

    /// <summary>
    /// Gets or sets the cursor Y position in window-local coordinates.
    /// </summary>
    public static float CursorPositionY
    {
        get => ImGui_GetCursorPosY();
        set => ImGui_SetCursorPosY(value);
    }

    /// <summary>
    /// Gets the initial cursor position in window-local coordinates.
    /// </summary>
    /// <remarks>
    /// Call <see cref="CursorScreenPos"/> after Begin to get the absolute coordinates version.
    /// </remarks>
    public static Point CursorStartPosition => new(ImGui_GetCursorStartPos());

    /// <summary>
    /// Gets the draw list associated with the current window.
    /// </summary>
    public static DrawList? DrawList
    {
        get
        {
            ImDrawList* drawList = ImGui_GetWindowDrawList();
            return drawList != null ? new DrawList(drawList) : null;
        }
    }

    /// <summary>
    /// Begins a new window.
    /// </summary>
    /// <param name="name">The window name, used as a unique identifier. Use "##" to pass a label that isn't displayed.</param>
    /// <param name="open">Optional reference to a boolean controlling the window's open state. If provided, a close button is shown.</param>
    /// <param name="flags">Window behavior flags.</param>
    /// <returns>
    /// False if the window is collapsed or fully clipped (you can early out and skip submitting content).
    /// Always call <see cref="Dispose"/> regardless of this return value.
    /// </returns>
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

    /// <summary>
    /// Returns true if the current window is focused, based on the specified flags.
    /// </summary>
    /// <param name="flags">Flags controlling which windows to consider when checking focus.</param>
    /// <returns>True if the current window is focused according to the specified flags.</returns>
    public static bool IsFocused(FocusedFlags flags = FocusedFlags.None)
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
    public static bool IsHovered(HoveredFlags flags = HoveredFlags.None)
    {
        return ImGui_IsWindowHovered((Native.ImGuiHoveredFlags)flags);
    }

    /// <summary>
    /// Sets the next window position with a pivot point. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="position">The position in screen coordinates.</param>
    /// <param name="cond">Condition for applying the position.</param>
    /// <param name="pivot">The pivot point (0,0) = top-left, (0.5,0.5) = center, (1,1) = bottom-right.</param>
    /// <remarks>
    /// Use pivot=(0.5f, 0.5f) to center on the given point.
    /// </remarks>
    public static void SetNextWindowPos(Point position, Condition cond = Condition.None, Point pivot = default)
    {
        ImGui_SetNextWindowPosEx(position.Value, (Native.ImGuiCond)cond, pivot.Value);
    }

    /// <summary>
    /// Sets the next window size. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="size">The window size. Set an axis to 0.0f to force auto-fit on that axis.</param>
    /// <param name="cond">Condition for applying the size.</param>
    public static void SetNextWindowSize(Size size, Condition cond = Condition.None)
    {
        ImGui_SetNextWindowSize(size.Value, (Native.ImGuiCond)cond);
    }

    /// <summary>
    /// Sets the next window size constraints. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="sizeMin">Minimum window size. Use 0.0f for no minimum. Use -1 for both min and max of same axis to preserve current size.</param>
    /// <param name="sizeMax">Maximum window size. Use float.MaxValue for no maximum. Use -1 for both min and max of same axis to preserve current size.</param>
    public static void SetNextWindowSizeConstraints(Size sizeMin, Size sizeMax)
    {
        // TODO: We're not exposing the custom size callback for now
        ImGui_SetNextWindowSizeConstraints(sizeMin.Value, sizeMax.Value, null, null);
    }

    /// <summary>
    /// Sets the next window content size (scrollable client area). Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="size">The content size. Does not include window decorations (title bar, menu bar, etc.) or WindowPadding. Set an axis to 0.0f to leave it automatic.</param>
    public static void SetNextWindowContentSize(Size size)
    {
        ImGui_SetNextWindowContentSize(size.Value);
    }

    /// <summary>
    /// Sets the next window collapsed state. Call before <see cref="Begin"/>.
    /// </summary>
    /// <param name="collapsed">Whether the window should be collapsed.</param>
    /// <param name="cond">Condition for applying the collapsed state.</param>
    public static void SetNextWindowCollapsed(bool collapsed, Condition cond = Condition.None)
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
    /// Not recommended. Prefer using <see cref="SetNextWindowPos(Vec2, Condition)"/> as this may incur tearing and side-effects.
    /// </remarks>
    public static void SetWindowPos(Vec2 pos, Condition cond = Condition.None)
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
    public static void SetWindowSize(Vec2 size, Condition cond = Condition.None)
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
    public static void SetWindowCollapsed(bool collapsed, Condition cond = Condition.None)
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
    public static void SetWindowPos(ReadOnlySpan<byte> name, Vec2 pos, Condition cond = Condition.None)
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
    public static void SetWindowSize(ReadOnlySpan<byte> name, Vec2 size, Condition cond = Condition.None)
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
    public static void SetWindowCollapsed(ReadOnlySpan<byte> name, bool collapsed, Condition cond = Condition.None)
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
}
