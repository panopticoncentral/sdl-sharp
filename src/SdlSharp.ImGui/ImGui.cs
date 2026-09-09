using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using SdlSharp.Graphics.Gpu;
using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Static class providing Dear ImGui widget and layout functions.
/// Mirrors the ImGui:: C++ namespace.
/// </summary>
public static unsafe class ImGui
{
    private static readonly Lazy<int> VersionNumber = new(LoadVersionNumber);

    /// <summary>Raised when a persistent native callback catches an exception that cannot be rethrown at its call site.</summary>
    public static event Action<Exception>? UnhandledCallbackException;

    // --- Lifecycle ---

    /// <summary>Finalizes the frame and generates draw data.</summary>
    public static void Render() => IGSharp_Render();

    /// <summary>Ends the frame without rendering. Called automatically by <see cref="Render"/>; call this yourself only if you skip rendering for a frame.</summary>
    public static void EndFrame() => IGSharp_EndFrame();

    /// <summary>Gets the draw data for the current frame. Valid after <see cref="Render"/>.</summary>
    public static DrawData GetDrawData() => new(IGSharp_GetDrawData());

    /// <summary>Gets the ImGui version string.</summary>
    public static string? GetVersion() => Marshal.PtrToStringUTF8((nint)IGSharp_GetVersion());

    /// <summary>Gets the ImGui numeric version used to compile the native shim.</summary>
    public static int GetVersionNumber() => VersionNumber.Value;

    private static int LoadVersionNumber()
    {
        try
        {
            return IGSharp_GetVersionNumber();
        }
        catch (EntryPointNotFoundException)
        {
            // ImguiSharp.Redist releases before 0.3.0-preview.3 do not export
            // IMGUI_VERSION_NUM. Derive the same value from "1.xx.y ..." so
            // applications can update the managed package before the redist.
            var text = GetVersion();
            var suffix = text?.IndexOf(' ') ?? -1;
            if (suffix >= 0)
                text = text![..suffix];
            if (Version.TryParse(text, out var version))
                return checked(version.Major * 10_000 + version.Minor * 100 + Math.Max(version.Build, 0));
            throw new InvalidOperationException($"Could not parse the native ImGui version '{text ?? "<null>"}'.");
        }
    }

    // --- IO ---

    /// <summary>Returns true if ImGui wants to capture mouse input.</summary>
    public static bool WantCaptureMouse => IGSharp_GetIO()->WantCaptureMouse;

    /// <summary>Returns true if ImGui wants to capture keyboard input.</summary>
    public static bool WantCaptureKeyboard => IGSharp_GetIO()->WantCaptureKeyboard;

    /// <summary>Gets the current frame rate as computed by ImGui.</summary>
    public static float Framerate => IGSharp_GetIO()->Framerate;

    // --- Demo / Styles ---

    /// <summary>Shows the ImGui demo window.</summary>
    public static void ShowDemoWindow(ref bool open)
    {
        fixed (bool* p = &open) IGSharp_ShowDemoWindow(p);
    }

    /// <summary>Shows the ImGui demo window (no close button).</summary>
    public static void ShowDemoWindow() => IGSharp_ShowDemoWindow(null);

    /// <summary>Shows the ImGui metrics/debugger window.</summary>
    public static void ShowMetricsWindow(ref bool open)
    {
        fixed (bool* p = &open) IGSharp_ShowMetricsWindow(p);
    }

    /// <summary>Shows the debug log window.</summary>
    public static void ShowDebugLogWindow() => IGSharp_ShowDebugLogWindow(null);

    /// <summary>Shows the debug log window with a close button.</summary>
    public static void ShowDebugLogWindow(ref bool open)
    {
        fixed (bool* p = &open) IGSharp_ShowDebugLogWindow(p);
    }

    /// <summary>Shows the ID stack tool window for inspecting widget IDs.</summary>
    public static void ShowIDStackToolWindow() => IGSharp_ShowIDStackToolWindow(null);

    /// <summary>Shows the ID stack tool window with a close button.</summary>
    public static void ShowIDStackToolWindow(ref bool open)
    {
        fixed (bool* p = &open) IGSharp_ShowIDStackToolWindow(p);
    }

    /// <summary>Shows the ImGui about window (version, contributors, license).</summary>
    public static void ShowAboutWindow() => IGSharp_ShowAboutWindow(null);

    /// <summary>Shows the about window with a close button.</summary>
    public static void ShowAboutWindow(ref bool open)
    {
        fixed (bool* p = &open) IGSharp_ShowAboutWindow(p);
    }

    /// <summary>Shows the style editor (an inline form for tweaking the global ImGui style).</summary>
    public static void ShowStyleEditor() => IGSharp_ShowStyleEditor(null);

    /// <summary>Shows a combo to select a built-in style preset (Dark / Light / Classic). Returns true on selection change.</summary>
    public static bool ShowStyleSelector(string label) => IGSharp_ShowStyleSelector(ToUtf8(label));

    /// <summary>Shows a combo to select among the loaded fonts.</summary>
    public static void ShowFontSelector(string label) => IGSharp_ShowFontSelector(ToUtf8(label));

    /// <summary>Renders the user guide (default keyboard/mouse cheatsheet).</summary>
    public static void ShowUserGuide() => IGSharp_ShowUserGuide();

    /// <summary>Applies the dark color theme.</summary>
    public static void StyleColorsDark() => IGSharp_StyleColorsDark(null);

    /// <summary>Applies the light color theme.</summary>
    public static void StyleColorsLight() => IGSharp_StyleColorsLight(null);

    /// <summary>Applies the classic color theme.</summary>
    public static void StyleColorsClassic() => IGSharp_StyleColorsClassic(null);

    /// <summary>Scales all style sizes by the given factor.</summary>
    public static void ScaleAllSizes(float scale) => IGSharp_Style_ScaleAllSizes(IGSharp_GetStyle(), scale);

    /// <summary>Sets the DPI font scale.</summary>
    public static void SetFontScaleDpi(float scale) => IGSharp_GetStyle()->FontScaleDpi = scale;

    // --- Windows ---

    /// <summary>Begins a new window. Returns false if the window is collapsed.</summary>
    public static bool Begin(string name, WindowFlags flags = WindowFlags.None)
        => IGSharp_Begin(ToUtf8(name), null, (int)flags);

    /// <summary>Begins a new window with a close button.</summary>
    public static bool Begin(string name, ref bool open, WindowFlags flags = WindowFlags.None)
    {
        fixed (bool* p = &open) return IGSharp_Begin(ToUtf8(name), p, (int)flags);
    }

    /// <summary>Ends the current window.</summary>
    public static void End() => IGSharp_End();

    // --- Child Windows ---

    /// <summary>Begins a child region. Returns false if the region is clipped.</summary>
    public static bool BeginChild(string strId, float width = 0, float height = 0, ChildFlags childFlags = ChildFlags.None, WindowFlags windowFlags = WindowFlags.None)
        => IGSharp_BeginChild(ToUtf8(strId), new IGSharp_Vec2(width, height), (int)childFlags, (int)windowFlags);

    /// <summary>Begins a child region identified by an ID obtained from <see cref="GetID(string)"/>.</summary>
    public static bool BeginChild(uint id, float width = 0, float height = 0, ChildFlags childFlags = ChildFlags.None, WindowFlags windowFlags = WindowFlags.None)
        => IGSharp_BeginChildID(id, new IGSharp_Vec2(width, height), (int)childFlags, (int)windowFlags);

    /// <summary>Ends the current child region.</summary>
    public static void EndChild() => IGSharp_EndChild();

    // --- Window Queries ---

    /// <summary>Returns true if the current window is appearing this frame (just became visible).</summary>
    public static bool IsWindowAppearing() => IGSharp_IsWindowAppearing();

    /// <summary>Returns true if the current window is collapsed.</summary>
    public static bool IsWindowCollapsed() => IGSharp_IsWindowCollapsed();

    /// <summary>Returns true if the current window is focused.</summary>
    public static bool IsWindowFocused(FocusedFlags flags = FocusedFlags.None) => IGSharp_IsWindowFocused((int)flags);

    /// <summary>Returns true if the current window is hovered.</summary>
    public static bool IsWindowHovered(HoveredFlags flags = HoveredFlags.None) => IGSharp_IsWindowHovered((int)flags);

    /// <summary>Gets the current window position in screen coordinates.</summary>
    public static Vec2 GetWindowPos()
    {
        var v = IGSharp_GetWindowPos();
        return new Vec2(v.X, v.Y);
    }

    /// <summary>Gets the current window size.</summary>
    public static Vec2 GetWindowSize()
    {
        var v = IGSharp_GetWindowSize();
        return new Vec2(v.X, v.Y);
    }

    /// <summary>Gets the current window width.</summary>
    public static float GetWindowWidth() => IGSharp_GetWindowWidth();

    /// <summary>Gets the current window height.</summary>
    public static float GetWindowHeight() => IGSharp_GetWindowHeight();

    /// <summary>Sets the position of the next window.</summary>
    public static void SetNextWindowPos(float x, float y, Cond cond = Cond.None, float pivotX = 0, float pivotY = 0)
        => IGSharp_SetNextWindowPos(new IGSharp_Vec2(x, y), (int)cond, new IGSharp_Vec2(pivotX, pivotY));

    /// <summary>Sets the position of the next window.</summary>
    public static void SetNextWindowPos(Vec2 position, Cond cond = Cond.None, Vec2 pivot = default)
        => IGSharp_SetNextWindowPos(new IGSharp_Vec2(position.X, position.Y), (int)cond, new IGSharp_Vec2(pivot.X, pivot.Y));

    /// <summary>Sets the size of the next window.</summary>
    public static void SetNextWindowSize(float width, float height, Cond cond = Cond.None)
        => IGSharp_SetNextWindowSize(new IGSharp_Vec2(width, height), (int)cond);

    /// <summary>Sets the size of the next window.</summary>
    public static void SetNextWindowSize(Vec2 size, Cond cond = Cond.None)
        => IGSharp_SetNextWindowSize(new IGSharp_Vec2(size.X, size.Y), (int)cond);

    /// <summary>Sets the collapsed state of the next window.</summary>
    public static void SetNextWindowCollapsed(bool collapsed, Cond cond = Cond.None)
        => IGSharp_SetNextWindowCollapsed(collapsed, (int)cond);

    /// <summary>Brings the next window to the front and activates it.</summary>
    public static void SetNextWindowFocus() => IGSharp_SetNextWindowFocus();

    /// <summary>Overrides the background alpha for the next window.</summary>
    public static void SetNextWindowBgAlpha(float alpha) => IGSharp_SetNextWindowBgAlpha(alpha);

    // --- Layout ---

    /// <summary>Inserts a horizontal separator.</summary>
    public static void Separator() => IGSharp_Separator();

    /// <summary>Puts the next widget on the same line as the previous.</summary>
    public static void SameLine(float offsetFromStartX = 0, float spacing = -1)
        => IGSharp_SameLine(offsetFromStartX, spacing);

    /// <summary>Starts a new line.</summary>
    public static void NewLine() => IGSharp_NewLine();

    /// <summary>Adds vertical spacing.</summary>
    public static void Spacing() => IGSharp_Spacing();

    /// <summary>Adds an invisible dummy item of the given size (useful for spacing and layout).</summary>
    public static void Dummy(float width, float height) => IGSharp_Dummy(new IGSharp_Vec2(width, height));

    /// <summary>Adds an invisible dummy item of the given size.</summary>
    public static void Dummy(Vec2 size) => IGSharp_Dummy(new IGSharp_Vec2(size.X, size.Y));

    /// <summary>Begins a group.</summary>
    public static void BeginGroup() => IGSharp_BeginGroup();

    /// <summary>Ends a group.</summary>
    public static void EndGroup() => IGSharp_EndGroup();

    /// <summary>Gets the cursor position in local window coordinates.</summary>
    public static Vec2 GetCursorPos()
    {
        var v = IGSharp_GetCursorPos();
        return new Vec2(v.X, v.Y);
    }

    /// <summary>Sets the cursor position in local window coordinates.</summary>
    public static void SetCursorPos(float x, float y) => IGSharp_SetCursorPos(new IGSharp_Vec2(x, y));

    /// <summary>Sets the cursor position in local window coordinates.</summary>
    public static void SetCursorPos(Vec2 position) => IGSharp_SetCursorPos(new IGSharp_Vec2(position.X, position.Y));

    /// <summary>Gets the cursor X position in local window coordinates.</summary>
    public static float GetCursorPosX() => IGSharp_GetCursorPosX();

    /// <summary>Gets the cursor Y position in local window coordinates.</summary>
    public static float GetCursorPosY() => IGSharp_GetCursorPosY();

    /// <summary>Sets the cursor X position in local window coordinates.</summary>
    public static void SetCursorPosX(float localX) => IGSharp_SetCursorPosX(localX);

    /// <summary>Sets the cursor Y position in local window coordinates.</summary>
    public static void SetCursorPosY(float localY) => IGSharp_SetCursorPosY(localY);

    /// <summary>Gets the initial cursor position in local window coordinates (top-left of the content region).</summary>
    public static Vec2 GetCursorStartPos()
    {
        var v = IGSharp_GetCursorStartPos();
        return new Vec2(v.X, v.Y);
    }

    /// <summary>Gets the cursor position in screen coordinates.</summary>
    public static Vec2 GetCursorScreenPos()
    {
        var v = IGSharp_GetCursorScreenPos();
        return new Vec2(v.X, v.Y);
    }

    /// <summary>Sets the cursor position in screen coordinates.</summary>
    public static void SetCursorScreenPos(float x, float y) => IGSharp_SetCursorScreenPos(new IGSharp_Vec2(x, y));

    /// <summary>Sets the cursor position in screen coordinates.</summary>
    public static void SetCursorScreenPos(Vec2 position) => IGSharp_SetCursorScreenPos(new IGSharp_Vec2(position.X, position.Y));

    /// <summary>Gets the available content region size for the current layout.</summary>
    public static Vec2 GetContentRegionAvail()
    {
        var v = IGSharp_GetContentRegionAvail();
        return new Vec2(v.X, v.Y);
    }

    /// <summary>Aligns text so its baseline matches framed-widget baselines on the current line.</summary>
    public static void AlignTextToFramePadding() => IGSharp_AlignTextToFramePadding();

    /// <summary>Indents subsequent items by <paramref name="indentWidth"/> pixels (0 = use default).</summary>
    public static void Indent(float indentWidth = 0f) => IGSharp_Indent(indentWidth);

    /// <summary>Cancels a previous <see cref="Indent"/>.</summary>
    public static void Unindent(float indentWidth = 0f) => IGSharp_Unindent(indentWidth);

    // --- ID Stack ---

    /// <summary>Pushes a string ID onto the ID stack (used to disambiguate widgets with identical labels).</summary>
    public static void PushID(string strId) => IGSharp_PushIDStr(ToUtf8(strId));

    /// <summary>Pushes an integer ID onto the ID stack.</summary>
    public static void PushID(int intId) => IGSharp_PushIDInt(intId);

    /// <summary>Pushes a pointer-derived ID onto the ID stack (identity-based, e.g. from <see cref="GCHandle.ToIntPtr"/>).</summary>
    public static void PushID(nint ptrId) => IGSharp_PushIDPtr((void*)ptrId);

    /// <summary>Pops the last pushed ID.</summary>
    public static void PopID() => IGSharp_PopID();

    /// <summary>Computes the unique ID for the given string within the current ID stack.</summary>
    public static uint GetID(string strId) => IGSharp_GetIDStr(ToUtf8(strId));

    /// <summary>Computes the unique ID for the given integer within the current ID stack.</summary>
    public static uint GetID(int intId) => IGSharp_GetIDInt(intId);

    /// <summary>Computes the unique ID for the given pointer value within the current ID stack.</summary>
    public static uint GetID(nint ptrId) => IGSharp_GetIDPtr((void*)ptrId);

    /// <summary>Gets the text line height (font size).</summary>
    public static float GetTextLineHeight() => IGSharp_GetTextLineHeight();

    /// <summary>Gets the text line height + item spacing (distance in pixels between two consecutive lines).</summary>
    public static float GetTextLineHeightWithSpacing() => IGSharp_GetTextLineHeightWithSpacing();

    /// <summary>Gets the default framed widget height (font size + frame padding * 2).</summary>
    public static float GetFrameHeight() => IGSharp_GetFrameHeight();

    /// <summary>Gets the framed widget height + item spacing (distance between consecutive framed widgets).</summary>
    public static float GetFrameHeightWithSpacing() => IGSharp_GetFrameHeightWithSpacing();

    /// <summary>Computes the pixel size of the given text using the current font.</summary>
    public static (float Width, float Height) CalcTextSize(string text, bool hideTextAfterDoubleHash = false, float wrapWidth = -1f)
    {
        var v = IGSharp_CalcTextSize(ToUtf8(text), null, hideTextAfterDoubleHash, wrapWidth);
        return (v.X, v.Y);
    }

    // --- Widgets: Text ---

    /// <summary>Displays text (no formatting).</summary>
    public static void Text(string text) => IGSharp_Text(ToUtf8(text));

    /// <summary>Displays raw text without format processing (faster; text is never treated as a format string).</summary>
    public static void TextUnformatted(string text) => IGSharp_TextUnformatted(ToUtf8(text), default);

    /// <summary>Displays colored text.</summary>
    public static void TextColored(float r, float g, float b, float a, string text)
        => IGSharp_TextColored(new IGSharp_Vec4(r, g, b, a), ToUtf8(text));

    /// <summary>Displays grayed-out text.</summary>
    public static void TextDisabled(string text) => IGSharp_TextDisabled(ToUtf8(text));

    /// <summary>Displays word-wrapped text.</summary>
    public static void TextWrapped(string text) => IGSharp_TextWrapped(ToUtf8(text));

    /// <summary>Displays a bullet point followed by text.</summary>
    public static void BulletText(string text) => IGSharp_BulletText(ToUtf8(text));

    /// <summary>Displays a separator with centered text.</summary>
    public static void SeparatorText(string label) => IGSharp_SeparatorText(ToUtf8(label));

    /// <summary>Displays text aligned with a right-justified label (for key:value displays).</summary>
    public static void LabelText(string label, string text) => IGSharp_LabelText(ToUtf8(label), ToUtf8(text));

    /// <summary>Displays a clickable text link. Returns true when clicked.</summary>
    public static bool TextLink(string label) => IGSharp_TextLink(ToUtf8(label));

    /// <summary>Displays a clickable text link that opens a URL when clicked.</summary>
    public static void TextLinkOpenURL(string label, string url) => IGSharp_TextLinkOpenURL(ToUtf8(label), ToUtf8(url));

    /// <summary>Displays "prefix: value" (shorthand debug helper).</summary>
    public static void Value(string prefix, bool value) => IGSharp_ValueBool(ToUtf8(prefix), value);

    /// <summary>Displays "prefix: value" (shorthand debug helper).</summary>
    public static void Value(string prefix, int value) => IGSharp_ValueInt(ToUtf8(prefix), value);

    /// <summary>Displays "prefix: value" (shorthand debug helper).</summary>
    public static void Value(string prefix, uint value) => IGSharp_ValueUInt(ToUtf8(prefix), value);

    /// <summary>Displays "prefix: value" (shorthand debug helper). <paramref name="floatFormat"/> is a printf-style format (e.g. "%.2f").</summary>
    public static void Value(string prefix, float value, string? floatFormat = null)
        => IGSharp_ValueFloat(ToUtf8(prefix), value, ToUtf8(floatFormat));

    // --- Widgets: Buttons ---

    /// <summary>Creates a button. Returns true when clicked.</summary>
    public static bool Button(string label, float width = 0, float height = 0)
        => IGSharp_Button(ToUtf8(label), new IGSharp_Vec2(width, height));

    /// <summary>Creates a small button (with minimal padding).</summary>
    public static bool SmallButton(string label) => IGSharp_SmallButton(ToUtf8(label));

    /// <summary>Creates an invisible button that fills the given size, useful for custom behaviors.</summary>
    public static bool InvisibleButton(string strId, float width, float height, ButtonFlags flags = ButtonFlags.None)
        => IGSharp_InvisibleButton(ToUtf8(strId), new IGSharp_Vec2(width, height), (int)flags);

    /// <summary>Creates a small arrow button pointing in the given direction.</summary>
    public static bool ArrowButton(string strId, Dir dir) => IGSharp_ArrowButton(ToUtf8(strId), (int)dir);

    /// <summary>Draws a small bullet (without following text).</summary>
    public static void Bullet() => IGSharp_Bullet();

    /// <summary>Creates a checkbox. Returns true when the value changes.</summary>
    public static bool Checkbox(string label, ref bool v)
    {
        fixed (bool* p = &v) return IGSharp_Checkbox(ToUtf8(label), p);
    }

    /// <summary>Creates a checkbox that toggles the given bit(s) in an int flags value. Returns true when the value changes.</summary>
    public static bool CheckboxFlags(string label, ref int flags, int flagsValue)
    {
        fixed (int* p = &flags) return IGSharp_CheckboxFlags(ToUtf8(label), p, flagsValue);
    }

    /// <summary>Creates a checkbox that toggles the given bit(s) in a uint flags value. Returns true when the value changes.</summary>
    public static bool CheckboxFlags(string label, ref uint flags, uint flagsValue)
    {
        fixed (uint* p = &flags) return IGSharp_CheckboxFlagsUInt(ToUtf8(label), p, flagsValue);
    }

    /// <summary>Creates a radio button. Returns true when clicked.</summary>
    public static bool RadioButton(string label, bool active) => IGSharp_RadioButton(ToUtf8(label), active);

    /// <summary>Creates a radio button bound to an int value. Sets <paramref name="v"/> to <paramref name="vButton"/> when clicked.</summary>
    public static bool RadioButton(string label, ref int v, int vButton)
    {
        fixed (int* p = &v) return IGSharp_RadioButtonInt(ToUtf8(label), p, vButton);
    }

    // --- Widgets: Drag ---

    private static ReadOnlySpan<byte> DefaultFloatFormat => "%.3f"u8;
    private static ReadOnlySpan<byte> DefaultIntFormat => "%d"u8;

    /// <summary>Creates a float drag slider.</summary>
    public static bool DragFloat(string label, ref float v, float speed = 1f, float min = 0, float max = 0, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        fixed (float* p = &v)
            return IGSharp_DragFloat(ToUtf8(label), p, speed, min, max, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates a 2-component float drag slider. The span must contain at least 2 elements.</summary>
    public static bool DragFloat2(string label, Span<float> v, float speed = 1f, float min = 0, float max = 0, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        RequireLength(v, 2, nameof(v));
        fixed (float* p = v)
            return IGSharp_DragFloat2(ToUtf8(label), p, speed, min, max, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates a 3-component float drag slider. The span must contain at least 3 elements.</summary>
    public static bool DragFloat3(string label, Span<float> v, float speed = 1f, float min = 0, float max = 0, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        RequireLength(v, 3, nameof(v));
        fixed (float* p = v)
            return IGSharp_DragFloat3(ToUtf8(label), p, speed, min, max, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates a 4-component float drag slider. The span must contain at least 4 elements.</summary>
    public static bool DragFloat4(string label, Span<float> v, float speed = 1f, float min = 0, float max = 0, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        RequireLength(v, 4, nameof(v));
        fixed (float* p = v)
            return IGSharp_DragFloat4(ToUtf8(label), p, speed, min, max, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates an int drag slider.</summary>
    public static bool DragInt(string label, ref int v, float speed = 1f, int min = 0, int max = 0, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        fixed (int* p = &v)
            return IGSharp_DragInt(ToUtf8(label), p, speed, min, max, format != null ? ToUtf8(format) : DefaultIntFormat, (int)flags);
    }

    /// <summary>Creates a 2-component int drag slider. The span must contain at least 2 elements.</summary>
    public static bool DragInt2(string label, Span<int> v, float speed = 1f, int min = 0, int max = 0, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        RequireLength(v, 2, nameof(v));
        fixed (int* p = v)
            return IGSharp_DragInt2(ToUtf8(label), p, speed, min, max, format != null ? ToUtf8(format) : DefaultIntFormat, (int)flags);
    }

    /// <summary>Creates a 3-component int drag slider. The span must contain at least 3 elements.</summary>
    public static bool DragInt3(string label, Span<int> v, float speed = 1f, int min = 0, int max = 0, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        RequireLength(v, 3, nameof(v));
        fixed (int* p = v)
            return IGSharp_DragInt3(ToUtf8(label), p, speed, min, max, format != null ? ToUtf8(format) : DefaultIntFormat, (int)flags);
    }

    /// <summary>Creates a 4-component int drag slider. The span must contain at least 4 elements.</summary>
    public static bool DragInt4(string label, Span<int> v, float speed = 1f, int min = 0, int max = 0, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        RequireLength(v, 4, nameof(v));
        fixed (int* p = v)
            return IGSharp_DragInt4(ToUtf8(label), p, speed, min, max, format != null ? ToUtf8(format) : DefaultIntFormat, (int)flags);
    }

    /// <summary>Creates a two-handle float range drag slider. <paramref name="formatMax"/> null uses <paramref name="format"/> for both handles.</summary>
    public static bool DragFloatRange2(string label, ref float currentMin, ref float currentMax, float speed = 1f, float min = 0, float max = 0, string? format = null, string? formatMax = null, SliderFlags flags = SliderFlags.None)
    {
        fixed (float* pMin = &currentMin)
        fixed (float* pMax = &currentMax)
            return IGSharp_DragFloatRange2(ToUtf8(label), pMin, pMax, speed, min, max, format != null ? ToUtf8(format) : DefaultFloatFormat, ToUtf8(formatMax), (int)flags);
    }

    /// <summary>Creates a two-handle int range drag slider. <paramref name="formatMax"/> null uses <paramref name="format"/> for both handles.</summary>
    public static bool DragIntRange2(string label, ref int currentMin, ref int currentMax, float speed = 1f, int min = 0, int max = 0, string? format = null, string? formatMax = null, SliderFlags flags = SliderFlags.None)
    {
        fixed (int* pMin = &currentMin)
        fixed (int* pMax = &currentMax)
            return IGSharp_DragIntRange2(ToUtf8(label), pMin, pMax, speed, min, max, format != null ? ToUtf8(format) : DefaultIntFormat, ToUtf8(formatMax), (int)flags);
    }

    // --- Widgets: Slider ---

    /// <summary>Creates a float slider.</summary>
    public static bool SliderFloat(string label, ref float v, float min, float max, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        fixed (float* p = &v)
            return IGSharp_SliderFloat(ToUtf8(label), p, min, max, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates an int slider.</summary>
    public static bool SliderInt(string label, ref int v, int min, int max, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        fixed (int* p = &v)
            return IGSharp_SliderInt(ToUtf8(label), p, min, max, format != null ? ToUtf8(format) : DefaultIntFormat, (int)flags);
    }

    // --- Widgets: Scalar (generic typed; supports any unmanaged numeric T) ---

    /// <summary>Generic drag widget for any unmanaged numeric type (sbyte..ulong, float, double).</summary>
    public static bool Drag<T>(string label, ref T v, float speed = 1f, T min = default, T max = default, string? format = null, SliderFlags flags = SliderFlags.None) where T : unmanaged
    {
        var dataType = DataTypeOf<T>();
        fixed (T* p = &v)
            return IGSharp_DragScalar(ToUtf8(label), dataType, p, speed, &min, &max, format != null ? ToUtf8(format) : default, (int)flags);
    }

    /// <summary>Generic N-component drag widget. The span must contain at least 1 element.</summary>
    public static bool Drag<T>(string label, Span<T> values, float speed = 1f, T min = default, T max = default, string? format = null, SliderFlags flags = SliderFlags.None) where T : unmanaged
    {
        var dataType = DataTypeOf<T>();
        fixed (T* p = values)
            return IGSharp_DragScalarN(ToUtf8(label), dataType, p, values.Length, speed, &min, &max, format != null ? ToUtf8(format) : default, (int)flags);
    }

    /// <summary>Generic slider widget for any unmanaged numeric type.</summary>
    public static bool Slider<T>(string label, ref T v, T min, T max, string? format = null, SliderFlags flags = SliderFlags.None) where T : unmanaged
    {
        var dataType = DataTypeOf<T>();
        fixed (T* p = &v)
            return IGSharp_SliderScalar(ToUtf8(label), dataType, p, &min, &max, format != null ? ToUtf8(format) : default, (int)flags);
    }

    /// <summary>Generic N-component slider widget.</summary>
    public static bool Slider<T>(string label, Span<T> values, T min, T max, string? format = null, SliderFlags flags = SliderFlags.None) where T : unmanaged
    {
        var dataType = DataTypeOf<T>();
        fixed (T* p = values)
            return IGSharp_SliderScalarN(ToUtf8(label), dataType, p, values.Length, &min, &max, format != null ? ToUtf8(format) : default, (int)flags);
    }

    /// <summary>Generic vertical slider widget.</summary>
    public static bool VSlider<T>(string label, float width, float height, ref T v, T min, T max, string? format = null, SliderFlags flags = SliderFlags.None) where T : unmanaged
    {
        var dataType = DataTypeOf<T>();
        fixed (T* p = &v)
            return IGSharp_VSliderScalar(ToUtf8(label), new IGSharp_Vec2(width, height), dataType, p, &min, &max, format != null ? ToUtf8(format) : default, (int)flags);
    }

    /// <summary>Generic input widget for any unmanaged numeric type, with optional step buttons.</summary>
    public static bool Input<T>(string label, ref T v, T? step = null, T? stepFast = null, string? format = null, InputTextFlags flags = InputTextFlags.None) where T : unmanaged
    {
        var dataType = DataTypeOf<T>();
        var stepValue = step.GetValueOrDefault();
        var stepFastValue = stepFast.GetValueOrDefault();
        fixed (T* p = &v)
            return IGSharp_InputScalar(ToUtf8(label), dataType, p, step.HasValue ? &stepValue : null,
                stepFast.HasValue ? &stepFastValue : null, format != null ? ToUtf8(format) : default, (int)flags);
    }

    /// <summary>Generic N-component input widget.</summary>
    public static bool Input<T>(string label, Span<T> values, T? step = null, T? stepFast = null, string? format = null, InputTextFlags flags = InputTextFlags.None) where T : unmanaged
    {
        var dataType = DataTypeOf<T>();
        var stepValue = step.GetValueOrDefault();
        var stepFastValue = stepFast.GetValueOrDefault();
        fixed (T* p = values)
            return IGSharp_InputScalarN(ToUtf8(label), dataType, p, values.Length, step.HasValue ? &stepValue : null,
                stepFast.HasValue ? &stepFastValue : null, format != null ? ToUtf8(format) : default, (int)flags);
    }

    private static int DataTypeOf<T>() where T : unmanaged
    {
        if (typeof(T) == typeof(sbyte))  return (int)DataType.S8;
        if (typeof(T) == typeof(byte))   return (int)DataType.U8;
        if (typeof(T) == typeof(short))  return (int)DataType.S16;
        if (typeof(T) == typeof(ushort)) return (int)DataType.U16;
        if (typeof(T) == typeof(int))    return (int)DataType.S32;
        if (typeof(T) == typeof(uint))   return (int)DataType.U32;
        if (typeof(T) == typeof(long))   return (int)DataType.S64;
        if (typeof(T) == typeof(ulong))  return (int)DataType.U64;
        if (typeof(T) == typeof(float))  return (int)DataType.Float;
        if (typeof(T) == typeof(double)) return (int)DataType.Double;
        throw new NotSupportedException($"Type {typeof(T).Name} is not a supported scalar widget type. Use one of: sbyte, byte, short, ushort, int, uint, long, ulong, float, double.");
    }

    /// <summary>Creates a 2-component float slider. The span must contain at least 2 elements.</summary>
    public static bool SliderFloat2(string label, Span<float> v, float min, float max, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        RequireLength(v, 2, nameof(v));
        fixed (float* p = v)
            return IGSharp_SliderFloat2(ToUtf8(label), p, min, max, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates a 3-component float slider. The span must contain at least 3 elements.</summary>
    public static bool SliderFloat3(string label, Span<float> v, float min, float max, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        RequireLength(v, 3, nameof(v));
        fixed (float* p = v)
            return IGSharp_SliderFloat3(ToUtf8(label), p, min, max, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates a 4-component float slider. The span must contain at least 4 elements.</summary>
    public static bool SliderFloat4(string label, Span<float> v, float min, float max, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        RequireLength(v, 4, nameof(v));
        fixed (float* p = v)
            return IGSharp_SliderFloat4(ToUtf8(label), p, min, max, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates a 2-component int slider. The span must contain at least 2 elements.</summary>
    public static bool SliderInt2(string label, Span<int> v, int min, int max, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        RequireLength(v, 2, nameof(v));
        fixed (int* p = v)
            return IGSharp_SliderInt2(ToUtf8(label), p, min, max, format != null ? ToUtf8(format) : DefaultIntFormat, (int)flags);
    }

    /// <summary>Creates a 3-component int slider. The span must contain at least 3 elements.</summary>
    public static bool SliderInt3(string label, Span<int> v, int min, int max, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        RequireLength(v, 3, nameof(v));
        fixed (int* p = v)
            return IGSharp_SliderInt3(ToUtf8(label), p, min, max, format != null ? ToUtf8(format) : DefaultIntFormat, (int)flags);
    }

    /// <summary>Creates a 4-component int slider. The span must contain at least 4 elements.</summary>
    public static bool SliderInt4(string label, Span<int> v, int min, int max, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        RequireLength(v, 4, nameof(v));
        fixed (int* p = v)
            return IGSharp_SliderInt4(ToUtf8(label), p, min, max, format != null ? ToUtf8(format) : DefaultIntFormat, (int)flags);
    }

    private static ReadOnlySpan<byte> DefaultAngleFormat => "%.0f deg"u8;

    /// <summary>Creates an angle slider (value stored in radians, displayed in degrees).</summary>
    public static bool SliderAngle(string label, ref float vRad, float degreesMin = -360f, float degreesMax = 360f, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        fixed (float* p = &vRad)
            return IGSharp_SliderAngle(ToUtf8(label), p, degreesMin, degreesMax, format != null ? ToUtf8(format) : DefaultAngleFormat, (int)flags);
    }

    /// <summary>Creates a vertical float slider of the given size.</summary>
    public static bool VSliderFloat(string label, float width, float height, ref float v, float min, float max, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        fixed (float* p = &v)
            return IGSharp_VSliderFloat(ToUtf8(label), new IGSharp_Vec2(width, height), p, min, max, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates a vertical int slider of the given size.</summary>
    public static bool VSliderInt(string label, float width, float height, ref int v, int min, int max, string? format = null, SliderFlags flags = SliderFlags.None)
    {
        fixed (int* p = &v)
            return IGSharp_VSliderInt(ToUtf8(label), new IGSharp_Vec2(width, height), p, min, max, format != null ? ToUtf8(format) : DefaultIntFormat, (int)flags);
    }

    // --- Widgets: Input ---

    /// <summary>Creates a single-line text input. <paramref name="buf"/> is used as a fixed-size null-terminated UTF-8 buffer.</summary>
    public static bool InputText(string label, Span<byte> buf, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* p = buf)
            return IGSharp_InputText(ToUtf8(label), p, (nuint)buf.Length, (int)flags);
    }

    /// <summary>Creates a multi-line text input backed by a fixed-size null-terminated UTF-8 buffer.</summary>
    public static bool InputTextMultiline(string label, Span<byte> buf, float width = 0, float height = 0, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* p = buf)
            return IGSharp_InputTextMultiline(ToUtf8(label), p, (nuint)buf.Length, new IGSharp_Vec2(width, height), (int)flags);
    }

    /// <summary>Creates a single-line text input that shows <paramref name="hint"/> when empty.</summary>
    public static bool InputTextWithHint(string label, string hint, Span<byte> buf, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (byte* p = buf)
            return IGSharp_InputTextWithHint(ToUtf8(label), ToUtf8(hint), p, (nuint)buf.Length, (int)flags);
    }

    /// <summary>Creates a single-line text input with a callback. See <see cref="InputTextCallback"/> and <see cref="InputTextFlags"/>.</summary>
    public static bool InputText(string label, Span<byte> buf, InputTextFlags flags, InputTextCallback callback)
    {
        var state = new CallbackState<InputTextCallback>(callback);
        var handle = GCHandle.Alloc(state);
        try
        {
            fixed (byte* p = buf)
            {
                var result = IGSharp_InputTextEx(ToUtf8(label), p, (nuint)buf.Length, (int)flags, &InputTextCallbackThunk, (void*)GCHandle.ToIntPtr(handle));
                state.ThrowIfFailed();
                return result;
            }
        }
        finally { handle.Free(); }
    }

    /// <summary>Creates a multi-line text input with a callback.</summary>
    public static bool InputTextMultiline(string label, Span<byte> buf, float width, float height, InputTextFlags flags, InputTextCallback callback)
    {
        var state = new CallbackState<InputTextCallback>(callback);
        var handle = GCHandle.Alloc(state);
        try
        {
            fixed (byte* p = buf)
            {
                var result = IGSharp_InputTextMultilineEx(ToUtf8(label), p, (nuint)buf.Length, new IGSharp_Vec2(width, height), (int)flags, &InputTextCallbackThunk, (void*)GCHandle.ToIntPtr(handle));
                state.ThrowIfFailed();
                return result;
            }
        }
        finally { handle.Free(); }
    }

    /// <summary>Creates a single-line text input with a hint and callback.</summary>
    public static bool InputTextWithHint(string label, string hint, Span<byte> buf, InputTextFlags flags, InputTextCallback callback)
    {
        var state = new CallbackState<InputTextCallback>(callback);
        var handle = GCHandle.Alloc(state);
        try
        {
            fixed (byte* p = buf)
            {
                var result = IGSharp_InputTextWithHintEx(ToUtf8(label), ToUtf8(hint), p, (nuint)buf.Length, (int)flags, &InputTextCallbackThunk, (void*)GCHandle.ToIntPtr(handle));
                state.ThrowIfFailed();
                return result;
            }
        }
        finally { handle.Free(); }
    }

    /// <summary>
    /// Creates a single-line text input bound to a <see cref="string"/>. The buffer auto-grows
    /// as the user types — the string can be of unbounded length. <paramref name="initialBufferSize"/>
    /// is the starting capacity (defaults to a sensible small size).
    /// </summary>
    public static bool InputText(string label, ref string value, InputTextFlags flags = InputTextFlags.None, int initialBufferSize = 256)
    {
        using var state = new GrowingInputState(value, initialBufferSize);
        var stateHandle = GCHandle.Alloc(state);
        try
        {
            var augmented = (int)flags | (int)InputTextFlags.CallbackResize;
            var changed = IGSharp_InputTextEx(ToUtf8(label), state.Ptr, (nuint)state.Size, augmented, &GrowingInputCallback, (void*)GCHandle.ToIntPtr(stateHandle));
            state.ThrowIfFailed();
            if (changed)
            {
                value = state.ReadString();
                return true;
            }
            return false;
        }
        finally { stateHandle.Free(); }
    }

    /// <summary>Creates a multi-line text input bound to a <see cref="string"/> with auto-growing buffer.</summary>
    public static bool InputTextMultiline(string label, ref string value, float width = 0, float height = 0, InputTextFlags flags = InputTextFlags.None, int initialBufferSize = 1024)
    {
        using var state = new GrowingInputState(value, initialBufferSize);
        var stateHandle = GCHandle.Alloc(state);
        try
        {
            var augmented = (int)flags | (int)InputTextFlags.CallbackResize;
            var changed = IGSharp_InputTextMultilineEx(ToUtf8(label), state.Ptr, (nuint)state.Size, new IGSharp_Vec2(width, height), augmented, &GrowingInputCallback, (void*)GCHandle.ToIntPtr(stateHandle));
            state.ThrowIfFailed();
            if (changed)
            {
                value = state.ReadString();
                return true;
            }
            return false;
        }
        finally { stateHandle.Free(); }
    }

    /// <summary>Creates a single-line text input with a hint shown when empty, bound to a <see cref="string"/> with auto-growing buffer.</summary>
    public static bool InputTextWithHint(string label, string hint, ref string value, InputTextFlags flags = InputTextFlags.None, int initialBufferSize = 256)
    {
        using var state = new GrowingInputState(value, initialBufferSize);
        var stateHandle = GCHandle.Alloc(state);
        try
        {
            var augmented = (int)flags | (int)InputTextFlags.CallbackResize;
            var changed = IGSharp_InputTextWithHintEx(ToUtf8(label), ToUtf8(hint), state.Ptr, (nuint)state.Size, augmented, &GrowingInputCallback, (void*)GCHandle.ToIntPtr(stateHandle));
            state.ThrowIfFailed();
            if (changed)
            {
                value = state.ReadString();
                return true;
            }
            return false;
        }
        finally { stateHandle.Free(); }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static int GrowingInputCallback(IGSharp_InputTextCallbackData* data)
    {
        try
        {
            if (IGSharp_InputTextCallbackData_GetEventFlag(data) == (int)InputTextFlags.CallbackResize)
            {
                var userData = IGSharp_InputTextCallbackData_GetUserData(data);
                var handle = GCHandle.FromIntPtr((nint)userData);
                var state = (GrowingInputState)handle.Target!;
                var newSize = IGSharp_InputTextCallbackData_GetBufSize(data);
                state.Resize(newSize);
                IGSharp_InputTextCallbackData_ResizeBuf(data, state.Ptr, state.Size);
            }
        }
        catch (Exception ex)
        {
            var userData = IGSharp_InputTextCallbackData_GetUserData(data);
            var handle = GCHandle.FromIntPtr((nint)userData);
            ((GrowingInputState)handle.Target!).Capture(ex);
        }
        return 0;
    }

    private sealed class GrowingInputState : IDisposable
    {
        private nint _ptr;
        private ExceptionDispatchInfo? _exception;

        public byte* Ptr => (byte*)_ptr;
        public int Size { get; private set; }

        public GrowingInputState(string value, int initialSize)
        {
            var byteCount = System.Text.Encoding.UTF8.GetByteCount(value);
            Size = Math.Max(initialSize, byteCount + 1);
            _ptr = Marshal.AllocHGlobal(Size);
            var span = new Span<byte>(Ptr, Size);
            var written = System.Text.Encoding.UTF8.GetBytes(value, span);
            span[written] = 0;
        }

        public void Resize(int newSize)
        {
            if (newSize == Size) return;
            _ptr = Marshal.ReAllocHGlobal(_ptr, newSize);
            Size = newSize;
        }

        public string ReadString()
        {
            var span = new ReadOnlySpan<byte>(Ptr, Size);
            var len = span.IndexOf((byte)0);
            if (len < 0) len = span.Length;
            return System.Text.Encoding.UTF8.GetString(span[..len]);
        }

        public void Capture(Exception exception) => _exception ??= ExceptionDispatchInfo.Capture(exception);

        public void ThrowIfFailed() => _exception?.Throw();

        public void Dispose()
        {
            if (_ptr != 0)
            {
                Marshal.FreeHGlobal(_ptr);
                _ptr = 0;
            }
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static int InputTextCallbackThunk(IGSharp_InputTextCallbackData* data)
    {
        var userData = IGSharp_InputTextCallbackData_GetUserData(data);
        var handle = GCHandle.FromIntPtr((nint)userData);
        var state = (CallbackState<InputTextCallback>)handle.Target!;
        try { return state.Callback(new InputTextCallbackData(data)); }
        catch (Exception ex)
        {
            state.Capture(ex);
            return 0;
        }
    }


    /// <summary>Creates a float input with optional step buttons.</summary>
    public static bool InputFloat(string label, ref float v, float step = 0, float stepFast = 0, string? format = null, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (float* p = &v)
            return IGSharp_InputFloat(ToUtf8(label), p, step, stepFast, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates a 2-component float input. The span must contain at least 2 elements.</summary>
    public static bool InputFloat2(string label, Span<float> v, string? format = null, InputTextFlags flags = InputTextFlags.None)
    {
        RequireLength(v, 2, nameof(v));
        fixed (float* p = v)
            return IGSharp_InputFloat2(ToUtf8(label), p, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates a 3-component float input. The span must contain at least 3 elements.</summary>
    public static bool InputFloat3(string label, Span<float> v, string? format = null, InputTextFlags flags = InputTextFlags.None)
    {
        RequireLength(v, 3, nameof(v));
        fixed (float* p = v)
            return IGSharp_InputFloat3(ToUtf8(label), p, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates a 4-component float input. The span must contain at least 4 elements.</summary>
    public static bool InputFloat4(string label, Span<float> v, string? format = null, InputTextFlags flags = InputTextFlags.None)
    {
        RequireLength(v, 4, nameof(v));
        fixed (float* p = v)
            return IGSharp_InputFloat4(ToUtf8(label), p, format != null ? ToUtf8(format) : DefaultFloatFormat, (int)flags);
    }

    /// <summary>Creates an int input with optional step buttons.</summary>
    public static bool InputInt(string label, ref int v, int step = 1, int stepFast = 100, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (int* p = &v)
            return IGSharp_InputInt(ToUtf8(label), p, step, stepFast, (int)flags);
    }

    /// <summary>Creates a 2-component int input. The span must contain at least 2 elements.</summary>
    public static bool InputInt2(string label, Span<int> v, InputTextFlags flags = InputTextFlags.None)
    {
        RequireLength(v, 2, nameof(v));
        fixed (int* p = v)
            return IGSharp_InputInt2(ToUtf8(label), p, (int)flags);
    }

    /// <summary>Creates a 3-component int input. The span must contain at least 3 elements.</summary>
    public static bool InputInt3(string label, Span<int> v, InputTextFlags flags = InputTextFlags.None)
    {
        RequireLength(v, 3, nameof(v));
        fixed (int* p = v)
            return IGSharp_InputInt3(ToUtf8(label), p, (int)flags);
    }

    /// <summary>Creates a 4-component int input. The span must contain at least 4 elements.</summary>
    public static bool InputInt4(string label, Span<int> v, InputTextFlags flags = InputTextFlags.None)
    {
        RequireLength(v, 4, nameof(v));
        fixed (int* p = v)
            return IGSharp_InputInt4(ToUtf8(label), p, (int)flags);
    }

    private static ReadOnlySpan<byte> DefaultDoubleFormat => "%.6f"u8;

    /// <summary>Creates a double input with optional step buttons.</summary>
    public static bool InputDouble(string label, ref double v, double step = 0, double stepFast = 0, string? format = null, InputTextFlags flags = InputTextFlags.None)
    {
        fixed (double* p = &v)
            return IGSharp_InputDouble(ToUtf8(label), p, step, stepFast, format != null ? ToUtf8(format) : DefaultDoubleFormat, (int)flags);
    }

    // --- Widgets: Color ---

    /// <summary>Creates a color editor for 3 floats (RGB).</summary>
    public static bool ColorEdit3(string label, ref float r, ref float g, ref float b, ColorEditFlags flags = ColorEditFlags.None)
    {
        var col = stackalloc float[3];
        col[0] = r; col[1] = g; col[2] = b;
        var result = IGSharp_ColorEdit3(ToUtf8(label), col, (int)flags);
        r = col[0]; g = col[1]; b = col[2];
        return result;
    }

    /// <summary>Creates a color editor for 4 floats (RGBA). The span must contain at least 4 elements.</summary>
    public static bool ColorEdit4(string label, Span<float> rgba, ColorEditFlags flags = ColorEditFlags.None)
    {
        RequireLength(rgba, 4, nameof(rgba));
        fixed (float* p = rgba) return IGSharp_ColorEdit4(ToUtf8(label), p, (int)flags);
    }

    /// <summary>Creates a color picker for 3 floats (RGB). The span must contain at least 3 elements.</summary>
    public static bool ColorPicker3(string label, Span<float> rgb, ColorEditFlags flags = ColorEditFlags.None)
    {
        RequireLength(rgb, 3, nameof(rgb));
        fixed (float* p = rgb) return IGSharp_ColorPicker3(ToUtf8(label), p, (int)flags);
    }

    /// <summary>Creates a color picker for 4 floats (RGBA). The span must contain at least 4 elements.</summary>
    public static bool ColorPicker4(string label, Span<float> rgba, ColorEditFlags flags = ColorEditFlags.None)
    {
        RequireLength(rgba, 4, nameof(rgba));
        fixed (float* p = rgba) return IGSharp_ColorPicker4(ToUtf8(label), p, (int)flags, null);
    }

    /// <summary>Creates a color picker for 4 floats (RGBA) with a reference color. Both spans must contain at least 4 elements.</summary>
    public static bool ColorPicker4(string label, Span<float> rgba, ReadOnlySpan<float> refColor, ColorEditFlags flags = ColorEditFlags.None)
    {
        RequireLength(rgba, 4, nameof(rgba));
        RequireLength(refColor, 4, nameof(refColor));
        fixed (float* p = rgba)
        fixed (float* r = refColor)
            return IGSharp_ColorPicker4(ToUtf8(label), p, (int)flags, r);
    }

    /// <summary>Creates a color button that displays a color and opens a color picker when clicked.</summary>
    public static bool ColorButton(string descId, float r, float g, float b, float a, ColorEditFlags flags = ColorEditFlags.None, float width = 0, float height = 0)
        => IGSharp_ColorButton(ToUtf8(descId), new IGSharp_Vec4(r, g, b, a), (int)flags, new IGSharp_Vec2(width, height));

    /// <summary>Sets the default options for all subsequent color editors (e.g. display as HSV, alpha bar).</summary>
    public static void SetColorEditOptions(ColorEditFlags flags) => IGSharp_SetColorEditOptions((int)flags);

    // --- Widgets: Images ---

    /// <summary>
    /// Draws an image. <paramref name="textureId"/> is interpreted by the active backend
    /// (SDL_GPU backend: the value must be a <c>SDL_GPUTexture*</c> cast to <see cref="ulong"/>).
    /// </summary>
    public static void Image(ulong textureId, float width, float height)
        => IGSharp_Image(textureId, new IGSharp_Vec2(width, height), default, new IGSharp_Vec2(1, 1));

    /// <summary>Draws an image with custom UV coordinates, tint, and border.</summary>
    public static void Image(ulong textureId, float width, float height, Vec2 uv0, Vec2 uv1, float tintR = 1, float tintG = 1, float tintB = 1, float tintA = 1, float borderR = 0, float borderG = 0, float borderB = 0, float borderA = 0)
        // ImGui removed the border_col parameter; the closest equivalent is ImageWithBg's
        // background fill color, so the border color is now rendered as a background.
        => IGSharp_ImageWithBg(textureId, new IGSharp_Vec2(width, height),
            new IGSharp_Vec2(uv0.X, uv0.Y), new IGSharp_Vec2(uv1.X, uv1.Y),
            new IGSharp_Vec4(borderR, borderG, borderB, borderA),
            new IGSharp_Vec4(tintR, tintG, tintB, tintA));

    /// <summary>
    /// Creates a clickable image button. <paramref name="textureId"/> is backend-specific
    /// (SDL_GPU: <c>SDL_GPUTexture*</c>). Returns true when clicked.
    /// </summary>
    public static bool ImageButton(string strId, ulong textureId, float width, float height)
        => IGSharp_ImageButton(ToUtf8(strId), textureId, new IGSharp_Vec2(width, height), default, new IGSharp_Vec2(1, 1), default, new IGSharp_Vec4(1, 1, 1, 1));

    /// <summary>Creates an image button with custom UV coordinates, background, and tint.</summary>
    public static bool ImageButton(string strId, ulong textureId, float width, float height, Vec2 uv0, Vec2 uv1, float bgR = 0, float bgG = 0, float bgB = 0, float bgA = 0, float tintR = 1, float tintG = 1, float tintB = 1, float tintA = 1)
        => IGSharp_ImageButton(ToUtf8(strId), textureId, new IGSharp_Vec2(width, height),
            new IGSharp_Vec2(uv0.X, uv0.Y), new IGSharp_Vec2(uv1.X, uv1.Y),
            new IGSharp_Vec4(bgR, bgG, bgB, bgA),
            new IGSharp_Vec4(tintR, tintG, tintB, tintA));

    /// <summary>Draws an SDL_GPU texture as an image.</summary>
    public static void Image(GpuTexture texture, float width, float height)
        => Image((ulong)texture.NativeHandle, width, height);

    /// <summary>Draws an SDL_GPU texture as an image with custom UV, tint, and border.</summary>
    public static void Image(GpuTexture texture, float width, float height, Vec2 uv0, Vec2 uv1, float tintR = 1, float tintG = 1, float tintB = 1, float tintA = 1, float borderR = 0, float borderG = 0, float borderB = 0, float borderA = 0)
        => Image((ulong)texture.NativeHandle, width, height, uv0, uv1, tintR, tintG, tintB, tintA, borderR, borderG, borderB, borderA);

    /// <summary>Creates a clickable image button from an SDL_GPU texture.</summary>
    public static bool ImageButton(string strId, GpuTexture texture, float width, float height)
        => ImageButton(strId, (ulong)texture.NativeHandle, width, height);

    /// <summary>Creates a clickable image button from an SDL_GPU texture with custom UV, background, and tint.</summary>
    public static bool ImageButton(string strId, GpuTexture texture, float width, float height, Vec2 uv0, Vec2 uv1, float bgR = 0, float bgG = 0, float bgB = 0, float bgA = 0, float tintR = 1, float tintG = 1, float tintB = 1, float tintA = 1)
        => ImageButton(strId, (ulong)texture.NativeHandle, width, height, uv0, uv1, bgR, bgG, bgB, bgA, tintR, tintG, tintB, tintA);

    // --- Widgets: Plot ---

    /// <summary>Plots a line graph of the given values. Pass <see cref="float.MaxValue"/> for min/max to auto-fit.</summary>
    public static void PlotLines(string label, ReadOnlySpan<float> values, int valuesOffset = 0, string? overlay = null, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, float width = 0, float height = 0)
    {
        fixed (float* p = values)
            IGSharp_PlotLines(ToUtf8(label), p, values.Length, valuesOffset, ToUtf8(overlay), scaleMin, scaleMax, new IGSharp_Vec2(width, height), sizeof(float));
    }

    /// <summary>Plots a histogram of the given values. Pass <see cref="float.MaxValue"/> for min/max to auto-fit.</summary>
    public static void PlotHistogram(string label, ReadOnlySpan<float> values, int valuesOffset = 0, string? overlay = null, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, float width = 0, float height = 0)
    {
        fixed (float* p = values)
            IGSharp_PlotHistogram(ToUtf8(label), p, values.Length, valuesOffset, ToUtf8(overlay), scaleMin, scaleMax, new IGSharp_Vec2(width, height), sizeof(float));
    }

    /// <summary>Plots a line graph with values supplied by a callback (useful for sparse or computed data).</summary>
    public static void PlotLines(string label, PlotValuesGetter getter, int valuesCount, int valuesOffset = 0, string? overlay = null, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, float width = 0, float height = 0)
    {
        var state = new CallbackState<PlotValuesGetter>(getter);
        var handle = GCHandle.Alloc(state);
        try
        {
            IGSharp_PlotLinesCallback(ToUtf8(label), &PlotValuesGetterThunk, (void*)GCHandle.ToIntPtr(handle), valuesCount, valuesOffset, ToUtf8(overlay), scaleMin, scaleMax, new IGSharp_Vec2(width, height));
            state.ThrowIfFailed();
        }
        finally { handle.Free(); }
    }

    /// <summary>Plots a histogram with values supplied by a callback.</summary>
    public static void PlotHistogram(string label, PlotValuesGetter getter, int valuesCount, int valuesOffset = 0, string? overlay = null, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, float width = 0, float height = 0)
    {
        var state = new CallbackState<PlotValuesGetter>(getter);
        var handle = GCHandle.Alloc(state);
        try
        {
            IGSharp_PlotHistogramCallback(ToUtf8(label), &PlotValuesGetterThunk, (void*)GCHandle.ToIntPtr(handle), valuesCount, valuesOffset, ToUtf8(overlay), scaleMin, scaleMax, new IGSharp_Vec2(width, height));
            state.ThrowIfFailed();
        }
        finally { handle.Free(); }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static float PlotValuesGetterThunk(void* data, int idx)
    {
        var handle = GCHandle.FromIntPtr((nint)data);
        var state = (CallbackState<PlotValuesGetter>)handle.Target!;
        try { return state.Callback(idx); }
        catch (Exception ex)
        {
            state.Capture(ex);
            return 0;
        }
    }

    // --- Widgets: Progress ---

    /// <summary>
    /// Draws a progress bar. <paramref name="fraction"/> is in [0, 1] (negative = animated indeterminate).
    /// <paramref name="overlay"/> is optional centered text; when null, the percentage is shown.
    /// </summary>
    public static void ProgressBar(float fraction, float width = -float.Epsilon, float height = 0f, string? overlay = null)
        => IGSharp_ProgressBar(fraction, new IGSharp_Vec2(width, height), overlay != null ? ToUtf8(overlay) : default);

    // --- Widgets: Trees ---

    /// <summary>Creates a tree node. Returns true if the node is open.</summary>
    public static bool TreeNode(string label) => IGSharp_TreeNode(ToUtf8(label));

    /// <summary>Creates a tree node with flags. Returns true if the node is open.</summary>
    public static bool TreeNodeEx(string label, TreeNodeFlags flags = TreeNodeFlags.None)
        => IGSharp_TreeNodeEx(ToUtf8(label), (int)flags);

    /// <summary>Creates a tree node whose ID (<paramref name="strId"/>) is decoupled from the displayed <paramref name="text"/>.</summary>
    public static bool TreeNode(string strId, string text) => IGSharp_TreeNodeStr(ToUtf8(strId), ToUtf8(text));

    /// <summary>Creates a tree node identified by a pointer value, displaying <paramref name="text"/>.</summary>
    public static bool TreeNode(nint ptrId, string text) => IGSharp_TreeNodePtr((void*)ptrId, ToUtf8(text));

    /// <summary>Creates a tree node with flags whose ID (<paramref name="strId"/>) is decoupled from the displayed <paramref name="text"/>.</summary>
    public static bool TreeNodeEx(string strId, TreeNodeFlags flags, string text)
        => IGSharp_TreeNodeExStr(ToUtf8(strId), (int)flags, ToUtf8(text));

    /// <summary>Creates a tree node with flags identified by a pointer value, displaying <paramref name="text"/>.</summary>
    public static bool TreeNodeEx(nint ptrId, TreeNodeFlags flags, string text)
        => IGSharp_TreeNodeExPtr((void*)ptrId, (int)flags, ToUtf8(text));

    /// <summary>Pushes onto the ID stack and indents, as if entering an open tree node. Pair with <see cref="TreePop"/>.</summary>
    public static void TreePush(string strId) => IGSharp_TreePushStr(ToUtf8(strId));

    /// <summary>Pushes a pointer-derived ID onto the ID stack and indents. Pair with <see cref="TreePop"/>.</summary>
    public static void TreePush(nint ptrId) => IGSharp_TreePushPtr((void*)ptrId);

    /// <summary>Pops the tree node.</summary>
    public static void TreePop() => IGSharp_TreePop();

    /// <summary>Gets horizontal distance preceding the label when using tree nodes.</summary>
    public static float GetTreeNodeToLabelSpacing() => IGSharp_GetTreeNodeToLabelSpacing();

    /// <summary>Queries whether a tree node with the given storage ID is currently open.</summary>
    public static bool TreeNodeGetOpen(uint storageId) => IGSharp_TreeNodeGetOpen(storageId);

    /// <summary>Sets a tree node's open state in the current window storage.</summary>
    public static void TreeNodeSetOpen(uint storageId, bool open)
    {
        using var storage = GetStateStorage();
        storage.SetBool(storageId, open);
    }

    /// <summary>Sets whether the next tree node or collapsing header will be open.</summary>
    public static void SetNextItemOpen(bool isOpen, Cond cond = Cond.None)
        => IGSharp_SetNextItemOpen(isOpen, (int)cond);

    /// <summary>Overrides the storage ID used to remember the next tree node's open state (default is the item ID).</summary>
    public static void SetNextItemStorageID(uint storageId) => IGSharp_SetNextItemStorageID(storageId);

    /// <summary>Creates a collapsing header.</summary>
    public static bool CollapsingHeader(string label, TreeNodeFlags flags = TreeNodeFlags.None)
        => IGSharp_CollapsingHeader(ToUtf8(label), (int)flags);

    /// <summary>Creates a collapsing header with a close button that toggles <paramref name="visible"/>.</summary>
    public static bool CollapsingHeader(string label, ref bool visible, TreeNodeFlags flags = TreeNodeFlags.None)
    {
        fixed (bool* p = &visible)
            return IGSharp_CollapsingHeaderClosable(ToUtf8(label), p, (int)flags);
    }

    // --- Widgets: Selectable ---

    /// <summary>Creates a selectable item.</summary>
    public static bool Selectable(string label, bool selected = false, SelectableFlags flags = SelectableFlags.None, float width = 0, float height = 0)
        => IGSharp_Selectable(ToUtf8(label), selected, (int)flags, new IGSharp_Vec2(width, height));

    /// <summary>Creates a selectable item bound to a bool.</summary>
    public static bool Selectable(string label, ref bool selected, SelectableFlags flags = SelectableFlags.None, float width = 0, float height = 0)
    {
        fixed (bool* p = &selected)
            return IGSharp_SelectablePtr(ToUtf8(label), p, (int)flags, new IGSharp_Vec2(width, height));
    }

    // --- Widgets: List Box ---

    /// <summary>Begins a list box (scrollable region holding selectables). Pair with <see cref="EndListBox"/>.</summary>
    public static bool BeginListBox(string label, float width = 0, float height = 0)
        => IGSharp_BeginListBox(ToUtf8(label), new IGSharp_Vec2(width, height));

    /// <summary>Ends the current list box.</summary>
    public static void EndListBox() => IGSharp_EndListBox();

    /// <summary>Creates a list box from an array of items. Returns true when the selection changes.</summary>
    public static bool ListBox(string label, ref int currentItem, string[] items, int heightInItems = -1)
    {
        var ptrs = new nint[items.Length];
        try
        {
            for (var i = 0; i < items.Length; i++)
                ptrs[i] = Marshal.StringToCoTaskMemUTF8(items[i]);
            fixed (int* p = &currentItem)
            fixed (nint* pp = ptrs)
                return IGSharp_ListBox(ToUtf8(label), p, (byte**)pp, items.Length, heightInItems);
        }
        finally
        {
            foreach (var ptr in ptrs) Marshal.FreeCoTaskMem(ptr);
        }
    }

    /// <summary>Creates a list box with item text supplied by a callback (useful for large or computed lists).</summary>
    public static bool ListBox(string label, ref int currentItem, Func<int, string> itemGetter, int itemsCount, int heightInItems = -1)
    {
        using var state = new ItemsGetterState(itemGetter);
        var handle = GCHandle.Alloc(state);
        try
        {
            fixed (int* p = &currentItem)
            {
                var result = IGSharp_ListBoxCallback(ToUtf8(label), p, &ItemsGetterThunk, (void*)GCHandle.ToIntPtr(handle), itemsCount, heightInItems);
                state.ThrowIfFailed();
                return result;
            }
        }
        finally { handle.Free(); }
    }

    // --- Widgets: Combo ---

    /// <summary>Begins a combo box.</summary>
    public static bool BeginCombo(string label, string? previewValue, ComboFlags flags = ComboFlags.None)
        => IGSharp_BeginCombo(ToUtf8(label), ToUtf8(previewValue), (int)flags);

    /// <summary>Ends a combo box.</summary>
    public static void EndCombo() => IGSharp_EndCombo();

    /// <summary>Creates a combo box from an array of items. Returns true when the selection changes.</summary>
    public static bool Combo(string label, ref int currentItem, string[] items, int popupMaxHeightInItems = -1)
    {
        var ptrs = new nint[items.Length];
        try
        {
            for (var i = 0; i < items.Length; i++)
                ptrs[i] = Marshal.StringToCoTaskMemUTF8(items[i]);
            fixed (int* p = &currentItem)
            fixed (nint* pp = ptrs)
                return IGSharp_Combo(ToUtf8(label), p, (byte**)pp, items.Length, popupMaxHeightInItems);
        }
        finally
        {
            foreach (var ptr in ptrs) Marshal.FreeCoTaskMem(ptr);
        }
    }

    /// <summary>Creates a combo box with item text supplied by a callback (useful for large or computed lists).</summary>
    public static bool Combo(string label, ref int currentItem, Func<int, string> itemGetter, int itemsCount, int popupMaxHeightInItems = -1)
    {
        using var state = new ItemsGetterState(itemGetter);
        var handle = GCHandle.Alloc(state);
        try
        {
            fixed (int* p = &currentItem)
            {
                var result = IGSharp_ComboCallback(ToUtf8(label), p, &ItemsGetterThunk, (void*)GCHandle.ToIntPtr(handle), itemsCount, popupMaxHeightInItems);
                state.ThrowIfFailed();
                return result;
            }
        }
        finally { handle.Free(); }
    }

    // Shared state for Combo/ListBox callback overloads: item strings returned to native code
    // must stay valid for the duration of the widget call, so the UTF-8 copies are kept
    // alive here and freed when the wrapper returns.
    private sealed class ItemsGetterState : IDisposable
    {
        private readonly Func<int, string> _getter;
        private readonly List<nint> _allocations = [];
        private ExceptionDispatchInfo? _exception;

        public ItemsGetterState(Func<int, string> getter) => _getter = getter;

        public byte* GetItemText(int index)
        {
            var ptr = Marshal.StringToCoTaskMemUTF8(_getter(index));
            _allocations.Add(ptr);
            return (byte*)ptr;
        }

        public void Capture(Exception exception) => _exception ??= ExceptionDispatchInfo.Capture(exception);

        public void ThrowIfFailed() => _exception?.Throw();

        public void Dispose()
        {
            foreach (var ptr in _allocations)
                Marshal.FreeCoTaskMem(ptr);
            _allocations.Clear();
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte* ItemsGetterThunk(void* data, int idx)
    {
        var handle = GCHandle.FromIntPtr((nint)data);
        var state = (ItemsGetterState)handle.Target!;
        try { return state.GetItemText(idx); }
        catch (Exception ex)
        {
            state.Capture(ex);
            return null;
        }
    }

    // --- Widgets: Menus ---

    /// <summary>Begins a menu bar (inside a window with the MenuBar flag).</summary>
    public static bool BeginMenuBar() => IGSharp_BeginMenuBar();

    /// <summary>Ends a menu bar.</summary>
    public static void EndMenuBar() => IGSharp_EndMenuBar();

    /// <summary>Begins the main menu bar.</summary>
    public static bool BeginMainMenuBar() => IGSharp_BeginMainMenuBar();

    /// <summary>Ends the main menu bar.</summary>
    public static void EndMainMenuBar() => IGSharp_EndMainMenuBar();

    /// <summary>Begins a menu.</summary>
    public static bool BeginMenu(string label, bool enabled = true)
        => IGSharp_BeginMenu(ToUtf8(label), enabled);

    /// <summary>Ends a menu.</summary>
    public static void EndMenu() => IGSharp_EndMenu();

    /// <summary>Creates a menu item. Returns true when activated.</summary>
    public static bool MenuItem(string label, string? shortcut = null, bool selected = false, bool enabled = true)
        => IGSharp_MenuItem(ToUtf8(label), ToUtf8(shortcut), selected, enabled);

    /// <summary>Creates a menu item bound to a bool. Returns true when activated.</summary>
    public static bool MenuItem(string label, string? shortcut, ref bool selected, bool enabled = true)
    {
        fixed (bool* p = &selected)
            return IGSharp_MenuItemPtr(ToUtf8(label), ToUtf8(shortcut), p, enabled);
    }

    // --- Widgets: Tooltips ---

    /// <summary>Begins a tooltip.</summary>
    public static bool BeginTooltip() => IGSharp_BeginTooltip();

    /// <summary>Ends a tooltip.</summary>
    public static void EndTooltip() => IGSharp_EndTooltip();

    /// <summary>Sets a text tooltip (shorthand).</summary>
    public static void SetTooltip(string text) => IGSharp_SetTooltip(ToUtf8(text));

    /// <summary>Sets a text tooltip shown only when the last item is hovered (shorthand for BeginItemTooltip + Text + EndTooltip).</summary>
    public static void SetItemTooltip(string text) => IGSharp_SetItemTooltip(ToUtf8(text));

    /// <summary>Begins a tooltip that is shown only when the last item is hovered.</summary>
    public static bool BeginItemTooltip() => IGSharp_BeginItemTooltip();

    // --- Widgets: Popups ---

    /// <summary>Opens a popup by string ID.</summary>
    public static void OpenPopup(string strId, PopupFlags flags = PopupFlags.None)
        => IGSharp_OpenPopup(ToUtf8(strId), (int)flags);

    /// <summary>Opens a popup by ID obtained from <see cref="GetID(string)"/>.</summary>
    public static void OpenPopup(uint id, PopupFlags flags = PopupFlags.None)
        => IGSharp_OpenPopupID(id, (int)flags);

    /// <summary>Opens a popup when the last item is clicked (helper for context menus).</summary>
    public static void OpenPopupOnItemClick(string? strId = null, PopupFlags flags = PopupFlags.MouseButtonRight)
        => IGSharp_OpenPopupOnItemClick(ToUtf8(strId), (int)flags);

    /// <summary>Begins a popup.</summary>
    public static bool BeginPopup(string strId, WindowFlags flags = WindowFlags.None)
        => IGSharp_BeginPopup(ToUtf8(strId), (int)flags);

    /// <summary>Begins a modal popup (blocks interaction with the rest of the UI until closed).</summary>
    public static bool BeginPopupModal(string name, WindowFlags flags = WindowFlags.None)
        => IGSharp_BeginPopupModal(ToUtf8(name), null, (int)flags);

    /// <summary>Begins a modal popup with a close button.</summary>
    public static bool BeginPopupModal(string name, ref bool open, WindowFlags flags = WindowFlags.None)
    {
        fixed (bool* p = &open) return IGSharp_BeginPopupModal(ToUtf8(name), p, (int)flags);
    }

    /// <summary>Begins a context-menu popup attached to the previous item. Call inside an if().</summary>
    public static bool BeginPopupContextItem(string? strId = null, PopupFlags flags = PopupFlags.MouseButtonRight)
        => IGSharp_BeginPopupContextItem(ToUtf8(strId), (int)flags);

    /// <summary>Begins a context-menu popup attached to the current window. Call inside an if().</summary>
    public static bool BeginPopupContextWindow(string? strId = null, PopupFlags flags = PopupFlags.MouseButtonRight)
        => IGSharp_BeginPopupContextWindow(ToUtf8(strId), (int)flags);

    /// <summary>Begins a context-menu popup opened by clicking in the void (where there are no windows). Call inside an if().</summary>
    public static bool BeginPopupContextVoid(string? strId = null, PopupFlags flags = PopupFlags.MouseButtonRight)
        => IGSharp_BeginPopupContextVoid(ToUtf8(strId), (int)flags);

    /// <summary>Returns true if the popup with the given ID is currently open.</summary>
    public static bool IsPopupOpen(string strId, PopupFlags flags = PopupFlags.None)
        => IGSharp_IsPopupOpen(ToUtf8(strId), (int)flags);

    /// <summary>Ends a popup.</summary>
    public static void EndPopup() => IGSharp_EndPopup();

    /// <summary>Closes the current popup.</summary>
    public static void CloseCurrentPopup() => IGSharp_CloseCurrentPopup();

    // --- Widgets: Tables ---

    /// <summary>Begins a table.</summary>
    public static bool BeginTable(string strId, int columns, TableFlags flags = TableFlags.None, float outerWidth = 0, float outerHeight = 0, float innerWidth = 0)
        => IGSharp_BeginTable(ToUtf8(strId), columns, (int)flags, new IGSharp_Vec2(outerWidth, outerHeight), innerWidth);

    /// <summary>Ends a table.</summary>
    public static void EndTable() => IGSharp_EndTable();

    /// <summary>Begins the next table row.</summary>
    public static void TableNextRow(TableRowFlags rowFlags = TableRowFlags.None, float minRowHeight = 0)
        => IGSharp_TableNextRow((int)rowFlags, minRowHeight);

    /// <summary>Advances to the next table column.</summary>
    public static bool TableNextColumn() => IGSharp_TableNextColumn();

    /// <summary>Moves to the given column in the current row. Returns false if the column is not visible.</summary>
    public static bool TableSetColumnIndex(int columnN) => IGSharp_TableSetColumnIndex(columnN);

    /// <summary>Sets up a table column.</summary>
    public static void TableSetupColumn(string label, TableColumnFlags flags = TableColumnFlags.None, float initWidthOrWeight = 0, uint userId = 0)
        => IGSharp_TableSetupColumn(ToUtf8(label), (int)flags, initWidthOrWeight, userId);

    /// <summary>Submits header row for all table columns.</summary>
    public static void TableHeadersRow() => IGSharp_TableHeadersRow();

    /// <summary>Submits an angled header row for columns with the AngledHeader flag.</summary>
    public static void TableAngledHeadersRow() => IGSharp_TableAngledHeadersRow();

    /// <summary>Submits a single header cell (inside a table row) with the given label.</summary>
    public static void TableHeader(string label) => IGSharp_TableHeader(ToUtf8(label));

    /// <summary>Locks the given number of leading columns / rows from scrolling when the table scrolls.</summary>
    public static void TableSetupScrollFreeze(int cols, int rows) => IGSharp_TableSetupScrollFreeze(cols, rows);

    /// <summary>Overrides the background color of the current row or cell.</summary>
    /// <param name="target">Which background layer to override.</param>
    /// <param name="color">Packed RGBA color (use <see cref="GetColorU32(float, float, float, float)"/> to build).</param>
    /// <param name="columnN">Column index for cell-level targets, or -1 for row-level targets.</param>
    public static void TableSetBgColor(TableBgTarget target, uint color, int columnN = -1)
        => IGSharp_TableSetBgColor((int)target, color, columnN);

    /// <summary>Number of columns in the current table.</summary>
    public static int TableGetColumnCount() => IGSharp_TableGetColumnCount();

    /// <summary>Index of the column being submitted within the current row.</summary>
    public static int TableGetColumnIndex() => IGSharp_TableGetColumnIndex();

    /// <summary>Index of the current row.</summary>
    public static int TableGetRowIndex() => IGSharp_TableGetRowIndex();

    /// <summary>Returns the user-supplied label for a column (-1 = current column).</summary>
    public static string? TableGetColumnName(int columnN = -1)
        => Marshal.PtrToStringUTF8((nint)IGSharp_TableGetColumnName(columnN));

    /// <summary>Returns the runtime flags (input + status) for the given column.</summary>
    public static TableColumnFlags TableGetColumnFlags(int columnN = -1)
        => (TableColumnFlags)IGSharp_TableGetColumnFlags(columnN);

    /// <summary>Enables or disables a column at runtime.</summary>
    public static void TableSetColumnEnabled(int columnN, bool enabled)
        => IGSharp_TableSetColumnEnabled(columnN, enabled);

    /// <summary>Index of the column currently hovered, or -1 if none.</summary>
    public static int TableGetHoveredColumn() => IGSharp_TableGetHoveredColumn();

    // --- Widgets: Tabs ---

    /// <summary>Begins a tab bar.</summary>
    public static bool BeginTabBar(string strId, TabBarFlags flags = TabBarFlags.None)
        => IGSharp_BeginTabBar(ToUtf8(strId), (int)flags);

    /// <summary>Ends a tab bar.</summary>
    public static void EndTabBar() => IGSharp_EndTabBar();

    /// <summary>Begins a tab item.</summary>
    public static bool BeginTabItem(string label, TabItemFlags flags = TabItemFlags.None)
        => IGSharp_BeginTabItem(ToUtf8(label), null, (int)flags);

    /// <summary>Begins a tab item with a close button.</summary>
    public static bool BeginTabItem(string label, ref bool open, TabItemFlags flags = TabItemFlags.None)
    {
        fixed (bool* p = &open) return IGSharp_BeginTabItem(ToUtf8(label), p, (int)flags);
    }

    /// <summary>Ends a tab item.</summary>
    public static void EndTabItem() => IGSharp_EndTabItem();

    /// <summary>Creates a tab that behaves as a button (no state, does not display contents).</summary>
    public static bool TabItemButton(string label, TabItemFlags flags = TabItemFlags.None)
        => IGSharp_TabItemButton(ToUtf8(label), (int)flags);

    /// <summary>Notifies the tab bar that a tab was closed externally.</summary>
    public static void SetTabItemClosed(string tabOrDockedWindowLabel)
        => IGSharp_SetTabItemClosed(ToUtf8(tabOrDockedWindowLabel));

    // --- Columns (legacy; prefer Tables) ---

    /// <summary>Begins a legacy multi-column layout. Prefer <see cref="BeginTable"/> for new code.</summary>
    public static void Columns(int count = 1, string? id = null, bool borders = true)
        => IGSharp_Columns(count, ToUtf8(id), borders);

    /// <summary>Moves to the next column (legacy columns API).</summary>
    public static void NextColumn() => IGSharp_NextColumn();

    /// <summary>Gets the current column index (legacy columns API).</summary>
    public static int GetColumnIndex() => IGSharp_GetColumnIndex();

    /// <summary>Gets a column's width in pixels (-1 = current column; legacy columns API).</summary>
    public static float GetColumnWidth(int columnIndex = -1) => IGSharp_GetColumnWidth(columnIndex);

    /// <summary>Sets a column's width in pixels (-1 = current column; legacy columns API).</summary>
    public static void SetColumnWidth(int columnIndex, float width) => IGSharp_SetColumnWidth(columnIndex, width);

    /// <summary>Gets a column's left offset in pixels (-1 = current column; legacy columns API).</summary>
    public static float GetColumnOffset(int columnIndex = -1) => IGSharp_GetColumnOffset(columnIndex);

    /// <summary>Sets a column's left offset in pixels (-1 = current column; legacy columns API).</summary>
    public static void SetColumnOffset(int columnIndex, float offsetX) => IGSharp_SetColumnOffset(columnIndex, offsetX);

    /// <summary>Gets the number of columns in the current layout (legacy columns API).</summary>
    public static int GetColumnsCount() => IGSharp_GetColumnsCount();

    // --- Logging / Capture ---

    /// <summary>Starts logging all rendered text to the terminal (stdout). <paramref name="autoOpenDepth"/> auto-opens tree nodes up to that depth (-1 = unlimited).</summary>
    public static void LogToTTY(int autoOpenDepth = -1) => IGSharp_LogToTTY(autoOpenDepth);

    /// <summary>Starts logging all rendered text to a file (null uses io.LogFilename).</summary>
    public static void LogToFile(int autoOpenDepth = -1, string? filename = null) => IGSharp_LogToFile(autoOpenDepth, ToUtf8(filename));

    /// <summary>Starts logging all rendered text to the clipboard.</summary>
    public static void LogToClipboard(int autoOpenDepth = -1) => IGSharp_LogToClipboard(autoOpenDepth);

    /// <summary>Stops logging and closes the file/clipboard capture.</summary>
    public static void LogFinish() => IGSharp_LogFinish();

    /// <summary>Displays buttons for starting text capture to TTY, file, or clipboard.</summary>
    public static void LogButtons() => IGSharp_LogButtons();

    /// <summary>Writes text directly into the current log output.</summary>
    public static void LogText(string text) => IGSharp_LogText(ToUtf8(text));

    // --- Disabling ---

    /// <summary>Begins a disabled section.</summary>
    public static void BeginDisabled(bool disabled = true) => IGSharp_BeginDisabled(disabled);

    /// <summary>Ends a disabled section.</summary>
    public static void EndDisabled() => IGSharp_EndDisabled();

    // --- Clipping ---

    /// <summary>Pushes a clip rectangle in screen coordinates. Affects hit-testing and widget clipping. Pair with <see cref="PopClipRect"/>.</summary>
    public static void PushClipRect(Vec2 clipRectMin, Vec2 clipRectMax, bool intersectWithCurrentClipRect)
        => IGSharp_PushClipRect(new IGSharp_Vec2(clipRectMin.X, clipRectMin.Y), new IGSharp_Vec2(clipRectMax.X, clipRectMax.Y), intersectWithCurrentClipRect);

    /// <summary>Pops the last pushed clip rectangle.</summary>
    public static void PopClipRect() => IGSharp_PopClipRect();

    // --- Item Utilities ---

    /// <summary>Returns true if the last item is hovered.</summary>
    public static bool IsItemHovered(HoveredFlags flags = HoveredFlags.None) => IGSharp_IsItemHovered((int)flags);

    /// <summary>Returns true if the last item was clicked.</summary>
    public static bool IsItemClicked(MouseButton mouseButton = MouseButton.Left) => IGSharp_IsItemClicked((int)mouseButton);

    /// <summary>Returns true if the last item is active (e.g. held, edited).</summary>
    public static bool IsItemActive() => IGSharp_IsItemActive();

    /// <summary>Returns true if the last item is focused for keyboard/gamepad navigation.</summary>
    public static bool IsItemFocused() => IGSharp_IsItemFocused();

    /// <summary>Returns true if the last item is visible (not clipped).</summary>
    public static bool IsItemVisible() => IGSharp_IsItemVisible();

    /// <summary>Returns true if the last item's value was modified this frame.</summary>
    public static bool IsItemEdited() => IGSharp_IsItemEdited();

    /// <summary>Returns true if the last item just became active this frame.</summary>
    public static bool IsItemActivated() => IGSharp_IsItemActivated();

    /// <summary>Returns true if the last item just became inactive this frame.</summary>
    public static bool IsItemDeactivated() => IGSharp_IsItemDeactivated();

    /// <summary>Returns true if the last item just became inactive after an edit this frame.</summary>
    public static bool IsItemDeactivatedAfterEdit() => IGSharp_IsItemDeactivatedAfterEdit();

    /// <summary>Returns true if the last item's open state was toggled this frame (tree nodes, collapsing headers).</summary>
    public static bool IsItemToggledOpen() => IGSharp_IsItemToggledOpen();

    /// <summary>Gets the top-left of the last item's bounding rectangle in screen coordinates.</summary>
    public static Vec2 GetItemRectMin()
    {
        var v = IGSharp_GetItemRectMin();
        return new Vec2(v.X, v.Y);
    }

    /// <summary>Gets the bottom-right of the last item's bounding rectangle in screen coordinates.</summary>
    public static Vec2 GetItemRectMax()
    {
        var v = IGSharp_GetItemRectMax();
        return new Vec2(v.X, v.Y);
    }

    /// <summary>Gets the size of the last item's bounding rectangle.</summary>
    public static Vec2 GetItemRectSize()
    {
        var v = IGSharp_GetItemRectSize();
        return new Vec2(v.X, v.Y);
    }

    /// <summary>Marks the last item as the default focus target when the current window opens.</summary>
    public static void SetItemDefaultFocus() => IGSharp_SetItemDefaultFocus();

    // --- Style Stack ---

    /// <summary>Pushes a color onto the style stack (RGBA floats).</summary>
    public static void PushStyleColor(Col idx, float r, float g, float b, float a)
        => IGSharp_PushStyleColorVec4((int)idx, new IGSharp_Vec4(r, g, b, a));

    /// <summary>Pushes a color onto the style stack (packed 0xAABBGGRR).</summary>
    public static void PushStyleColor(Col idx, uint col) => IGSharp_PushStyleColorU32((int)idx, col);

    /// <summary>Pops style colors.</summary>
    public static void PopStyleColor(int count = 1) => IGSharp_PopStyleColor(count);

    /// <summary>Pushes a float style variable.</summary>
    public static void PushStyleVar(StyleVar idx, float val) => IGSharp_PushStyleVarFloat((int)idx, val);

    /// <summary>Pushes a Vec2 style variable.</summary>
    public static void PushStyleVar(StyleVar idx, float x, float y) => IGSharp_PushStyleVarVec2((int)idx, new IGSharp_Vec2(x, y));

    /// <summary>Pushes only the X component of a Vec2 style variable, keeping the current Y.</summary>
    public static void PushStyleVarX(StyleVar idx, float x) => IGSharp_PushStyleVarX((int)idx, x);

    /// <summary>Pushes only the Y component of a Vec2 style variable, keeping the current X.</summary>
    public static void PushStyleVarY(StyleVar idx, float y) => IGSharp_PushStyleVarY((int)idx, y);

    /// <summary>Pops style variables.</summary>
    public static void PopStyleVar(int count = 1) => IGSharp_PopStyleVar(count);

    /// <summary>Gets a style color as RGBA floats (unmodified by the current alpha; use <see cref="GetColorU32(Col, float)"/> for a render-ready color).</summary>
    public static Vec4 GetStyleColorVec4(Col idx)
    {
        var v = IGSharp_GetStyleColorVec4((int)idx);
        return new Vec4(v.X, v.Y, v.Z, v.W);
    }

    /// <summary>Gets the name of a style color (for display/debugging).</summary>
    public static string? GetStyleColorName(Col idx) => Marshal.PtrToStringUTF8((nint)IGSharp_GetStyleColorName((int)idx));

    /// <summary>Pushes an item width (0 = default, negative = relative to right edge).</summary>
    public static void PushItemWidth(float itemWidth) => IGSharp_PushItemWidth(itemWidth);

    /// <summary>Pops an item width.</summary>
    public static void PopItemWidth() => IGSharp_PopItemWidth();

    /// <summary>Sets the width of the next item.</summary>
    public static void SetNextItemWidth(float itemWidth) => IGSharp_SetNextItemWidth(itemWidth);

    /// <summary>Returns the width available to the current item (taking into account PushItemWidth/SetNextItemWidth).</summary>
    public static float CalcItemWidth() => IGSharp_CalcItemWidth();

    /// <summary>Pushes a word-wrap position for text. A value &lt; 0 disables wrapping; 0 uses the content region edge.</summary>
    public static void PushTextWrapPos(float wrapLocalPosX = 0f) => IGSharp_PushTextWrapPos(wrapLocalPosX);

    /// <summary>Pops the text wrap position.</summary>
    public static void PopTextWrapPos() => IGSharp_PopTextWrapPos();

    // --- Color Utilities ---

    /// <summary>Gets a packed 32-bit color from the style color palette, scaled by <paramref name="alphaMul"/>.</summary>
    public static uint GetColorU32(Col idx, float alphaMul = 1f) => IGSharp_GetColorU32((int)idx, alphaMul);

    /// <summary>Gets a packed 32-bit color from four floats (RGBA).</summary>
    public static uint GetColorU32(float r, float g, float b, float a) => IGSharp_GetColorU32Vec4(new IGSharp_Vec4(r, g, b, a));

    /// <summary>Re-multiplies a packed 32-bit color by <paramref name="alphaMul"/>.</summary>
    public static uint GetColorU32(uint col, float alphaMul = 1f) => IGSharp_GetColorU32Packed(col, alphaMul);

    /// <summary>Converts a packed 32-bit color into RGBA floats.</summary>
    public static (float R, float G, float B, float A) ColorConvertU32ToFloat4(uint col)
    {
        var v = IGSharp_ColorConvertU32ToFloat4(col);
        return (v.X, v.Y, v.Z, v.W);
    }

    /// <summary>Converts RGBA floats into a packed 32-bit color.</summary>
    public static uint ColorConvertFloat4ToU32(float r, float g, float b, float a)
        => IGSharp_ColorConvertFloat4ToU32(new IGSharp_Vec4(r, g, b, a));

    /// <summary>Converts an RGB color to HSV.</summary>
    public static (float H, float S, float V) ColorConvertRGBtoHSV(float r, float g, float b)
    {
        float h, s, v;
        IGSharp_ColorConvertRGBtoHSV(r, g, b, &h, &s, &v);
        return (h, s, v);
    }

    /// <summary>Converts an HSV color to RGB.</summary>
    public static (float R, float G, float B) ColorConvertHSVtoRGB(float h, float s, float v)
    {
        float r, g, b;
        IGSharp_ColorConvertHSVtoRGB(h, s, v, &r, &g, &b);
        return (r, g, b);
    }

    // --- DrawList Accessors ---

    /// <summary>Gets the current window's draw list — drawing happens between UI widgets and the window's background.</summary>
    public static DrawList GetWindowDrawList() => new(IGSharp_GetWindowDrawList());

    /// <summary>Gets the background draw list — drawing happens behind all windows, covering the full viewport.</summary>
    public static DrawList GetBackgroundDrawList() => new(IGSharp_GetBackgroundDrawList());

    /// <summary>Gets the foreground draw list — drawing happens in front of all windows, covering the full viewport.</summary>
    public static DrawList GetForegroundDrawList() => new(IGSharp_GetForegroundDrawList());

    // --- Scrolling ---

    /// <summary>Gets the current horizontal scroll position.</summary>
    public static float GetScrollX() => IGSharp_GetScrollX();

    /// <summary>Gets the current vertical scroll position.</summary>
    public static float GetScrollY() => IGSharp_GetScrollY();

    /// <summary>Sets the horizontal scroll position.</summary>
    public static void SetScrollX(float scrollX) => IGSharp_SetScrollX(scrollX);

    /// <summary>Sets the vertical scroll position.</summary>
    public static void SetScrollY(float scrollY) => IGSharp_SetScrollY(scrollY);

    /// <summary>Gets the maximum horizontal scroll position.</summary>
    public static float GetScrollMaxX() => IGSharp_GetScrollMaxX();

    /// <summary>Gets the maximum vertical scroll position.</summary>
    public static float GetScrollMaxY() => IGSharp_GetScrollMaxY();

    /// <summary>Adjusts horizontal scrolling so the cursor X position is centered (0.0 = left edge, 1.0 = right edge, 0.5 = center).</summary>
    public static void SetScrollHereX(float centerXRatio = 0.5f) => IGSharp_SetScrollHereX(centerXRatio);

    /// <summary>Adjusts vertical scrolling so the cursor Y position is centered.</summary>
    public static void SetScrollHereY(float centerYRatio = 0.5f) => IGSharp_SetScrollHereY(centerYRatio);

    /// <summary>Adjusts horizontal scrolling so the given local X position is centered.</summary>
    public static void SetScrollFromPosX(float localX, float centerXRatio = 0.5f) => IGSharp_SetScrollFromPosX(localX, centerXRatio);

    /// <summary>Adjusts vertical scrolling so the given local Y position is centered.</summary>
    public static void SetScrollFromPosY(float localY, float centerYRatio = 0.5f) => IGSharp_SetScrollFromPosY(localY, centerYRatio);

    // --- Item Flags ---

    /// <summary>Pushes an item flag for all subsequently submitted items in the current scope.</summary>
    public static void PushItemFlag(ItemFlags option, bool enabled) => IGSharp_PushItemFlag((int)option, enabled);

    /// <summary>Pops the last pushed item flag.</summary>
    public static void PopItemFlag() => IGSharp_PopItemFlag();

    /// <summary>Gets the item flags currently in effect (combination of pushed flags).</summary>
    public static ItemFlags GetItemFlags() => (ItemFlags)IGSharp_GetItemFlags();

    // --- Focus / Activation ---

    /// <summary>Requests keyboard focus on the next or previously submitted item. <paramref name="offset"/> 0 = next, -1 = previous.</summary>
    public static void SetKeyboardFocusHere(int offset = 0) => IGSharp_SetKeyboardFocusHere(offset);

    /// <summary>Allows the next item to be overlapped by following items (e.g. invisible button over a tree node).</summary>
    public static void SetNextItemAllowOverlap() => IGSharp_SetNextItemAllowOverlap();

    /// <summary>Shows or hides the keyboard/gamepad navigation cursor (highlight).</summary>
    public static void SetNavCursorVisible(bool visible) => IGSharp_SetNavCursorVisible(visible);

    // --- Item Utilities (extra) ---

    /// <summary>Gets the unique ID of the last submitted item.</summary>
    public static uint GetItemId() => IGSharp_GetItemID();

    /// <summary>True if any item is currently hovered.</summary>
    public static bool IsAnyItemHovered() => IGSharp_IsAnyItemHovered();

    /// <summary>True if any item is currently active (held / edited).</summary>
    public static bool IsAnyItemActive() => IGSharp_IsAnyItemActive();

    /// <summary>True if any item currently has keyboard/gamepad focus.</summary>
    public static bool IsAnyItemFocused() => IGSharp_IsAnyItemFocused();

    // --- Mouse Cursor ---

    /// <summary>Gets the desired mouse cursor shape for this frame (set by ImGui based on hovered widgets).</summary>
    public static MouseCursor GetMouseCursor() => (MouseCursor)IGSharp_GetMouseCursor();

    /// <summary>Sets the desired mouse cursor shape for this frame.</summary>
    public static void SetMouseCursor(MouseCursor cursor) => IGSharp_SetMouseCursor((int)cursor);

    // --- Window Manipulation (extra) ---

    /// <summary>Sets minimum and maximum size for the next window.</summary>
    public static void SetNextWindowSizeConstraints(float minWidth, float minHeight, float maxWidth, float maxHeight)
        => IGSharp_SetNextWindowSizeConstraints(new IGSharp_Vec2(minWidth, minHeight), new IGSharp_Vec2(maxWidth, maxHeight), null, null);

    /// <summary>Custom size-constraint callback: receives the window position and current size, and may adjust the desired size.</summary>
    public delegate void SizeConstraintCallback(Vec2 pos, Vec2 currentSize, ref Vec2 desiredSize);

    private static readonly ConcurrentDictionary<nint, SizeConstraintCallback> SizeConstraintCallbacks = new();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void SizeConstraintThunk(IGSharp_SizeCallbackData* data)
    {
        var context = (nint)IGSharp_GetCurrentContext();
        if (!SizeConstraintCallbacks.TryRemove(context, out var cb)) return;
        try
        {
            var p = IGSharp_SizeCallbackData_GetPos(data);
            var c = IGSharp_SizeCallbackData_GetCurrentSize(data);
            var d = IGSharp_SizeCallbackData_GetDesiredSize(data);
            var desired = new Vec2(d.X, d.Y);
            cb(new Vec2(p.X, p.Y), new Vec2(c.X, c.Y), ref desired);
            IGSharp_SizeCallbackData_SetDesiredSize(data, new IGSharp_Vec2(desired.X, desired.Y));
        }
        catch (Exception ex)
        {
            ReportUnhandledCallbackException(ex);
        }
    }

    /// <summary>Sets a custom size constraint for the next window (e.g. fixed aspect ratio or stepped sizes).</summary>
    public static void SetNextWindowSizeConstraints(Vec2 min, Vec2 max, SizeConstraintCallback callback)
    {
        var context = (nint)IGSharp_GetCurrentContext();
        if (context == 0) throw new InvalidOperationException("No current ImGui context.");
        SizeConstraintCallbacks[context] = callback;
        IGSharp_SetNextWindowSizeConstraints(new IGSharp_Vec2(min.X, min.Y), new IGSharp_Vec2(max.X, max.Y), &SizeConstraintThunk, null);
    }

    internal static void ReleaseContext(nint context) => SizeConstraintCallbacks.TryRemove(context, out _);

    /// <summary>Sets the content size used to compute scrollbar ranges for the next window.</summary>
    public static void SetNextWindowContentSize(float width, float height)
        => IGSharp_SetNextWindowContentSize(new IGSharp_Vec2(width, height));

    /// <summary>Sets the scrolling position for the next window. Negative components keep the existing value.</summary>
    public static void SetNextWindowScroll(float scrollX, float scrollY)
        => IGSharp_SetNextWindowScroll(new IGSharp_Vec2(scrollX, scrollY));

    /// <summary>Sets the current window's position. Prefer <see cref="SetNextWindowPos(Vec2, Cond, Vec2)"/> — calling this inside Begin/End causes a one-frame lag.</summary>
    public static void SetWindowPos(float x, float y, Cond cond = Cond.None)
        => IGSharp_SetWindowPos(new IGSharp_Vec2(x, y), (int)cond);

    /// <summary>Sets a named window's position.</summary>
    public static void SetWindowPos(string name, float x, float y, Cond cond = Cond.None)
        => IGSharp_SetWindowPosNamed(ToUtf8(name), new IGSharp_Vec2(x, y), (int)cond);

    /// <summary>Sets the current window's size. Prefer <see cref="SetNextWindowSize(Vec2, Cond)"/> — calling this inside Begin/End causes a one-frame lag.</summary>
    public static void SetWindowSize(float width, float height, Cond cond = Cond.None)
        => IGSharp_SetWindowSize(new IGSharp_Vec2(width, height), (int)cond);

    /// <summary>Sets a named window's size.</summary>
    public static void SetWindowSize(string name, float width, float height, Cond cond = Cond.None)
        => IGSharp_SetWindowSizeNamed(ToUtf8(name), new IGSharp_Vec2(width, height), (int)cond);

    /// <summary>Sets the current window's collapsed state. Prefer <see cref="SetNextWindowCollapsed"/>.</summary>
    public static void SetWindowCollapsed(bool collapsed, Cond cond = Cond.None)
        => IGSharp_SetWindowCollapsed(collapsed, (int)cond);

    /// <summary>Sets a named window's collapsed state.</summary>
    public static void SetWindowCollapsed(string name, bool collapsed, Cond cond = Cond.None)
        => IGSharp_SetWindowCollapsedNamed(ToUtf8(name), collapsed, (int)cond);

    /// <summary>Focuses the current window. Prefer <see cref="SetNextWindowFocus"/>.</summary>
    public static void SetWindowFocus() => IGSharp_SetWindowFocus();

    /// <summary>Focuses a named window.</summary>
    public static void SetWindowFocus(string name) => IGSharp_SetWindowFocusNamed(ToUtf8(name));

    // --- Viewport ---

    /// <summary>Gets the main viewport — covers the OS window client area.</summary>
    public static Viewport GetMainViewport() => new(IGSharp_GetMainViewport());

    // --- Input Queries: Keyboard ---

    /// <summary>Returns true while the given key is held down.</summary>
    public static bool IsKeyDown(Key key) => IGSharp_IsKeyDown((int)key);

    /// <summary>Returns true if the key was pressed this frame. With <paramref name="repeat"/> true, also returns true on key-repeat.</summary>
    public static bool IsKeyPressed(Key key, bool repeat = true) => IGSharp_IsKeyPressed((int)key, repeat);

    /// <summary>Returns true if the key was released this frame.</summary>
    public static bool IsKeyReleased(Key key) => IGSharp_IsKeyReleased((int)key);

    /// <summary>Returns true if the given key chord (a key bitwise-or'd with mod flags) was pressed this frame.</summary>
    public static bool IsKeyChordPressed(Key keyChord) => IGSharp_IsKeyChordPressed((int)keyChord);

    /// <summary>Gets the number of times the key was virtually pressed in the current frame, using the given repeat delay and rate.</summary>
    public static int GetKeyPressedAmount(Key key, float repeatDelay, float rate) => IGSharp_GetKeyPressedAmount((int)key, repeatDelay, rate);

    /// <summary>Gets a human-readable name for the key ("A", "Enter", ...).</summary>
    public static string? GetKeyName(Key key) => Marshal.PtrToStringUTF8((nint)IGSharp_GetKeyName((int)key));

    /// <summary>Overrides io.WantCaptureKeyboard for the next frame (e.g. to force capture or force pass-through).</summary>
    public static void SetNextFrameWantCaptureKeyboard(bool wantCaptureKeyboard) => IGSharp_SetNextFrameWantCaptureKeyboard(wantCaptureKeyboard);

    /// <summary>
    /// Returns true if the key chord was pressed this frame and is routed to the current window/item.
    /// <paramref name="flags"/> takes raw ImGuiInputFlags bits (0 = default routing).
    /// </summary>
    public static bool Shortcut(Key keyChord, InputFlags flags = InputFlags.None) => IGSharp_Shortcut((int)keyChord, (int)flags);

    /// <summary>
    /// Assigns a shortcut that activates the next item when pressed (also shown in tooltips).
    /// <paramref name="flags"/> takes raw ImGuiInputFlags bits (0 = default routing).
    /// </summary>
    public static void SetNextItemShortcut(Key keyChord, InputFlags flags = InputFlags.None) => IGSharp_SetNextItemShortcut((int)keyChord, (int)flags);

    /// <summary>Makes the last item the owner of the given key, preventing other code from reading it this frame.</summary>
    public static void SetItemKeyOwner(Key key) => IGSharp_SetItemKeyOwner((int)key);

    // --- Input Queries: Mouse ---

    /// <summary>Returns true while the given mouse button is held down.</summary>
    public static bool IsMouseDown(MouseButton button) => IGSharp_IsMouseDown((int)button);

    /// <summary>Returns true if the mouse button was clicked this frame. With <paramref name="repeat"/>, also returns true on click-repeat.</summary>
    public static bool IsMouseClicked(MouseButton button, bool repeat = false) => IGSharp_IsMouseClicked((int)button, repeat);

    /// <summary>Returns true if the mouse button was released this frame.</summary>
    public static bool IsMouseReleased(MouseButton button) => IGSharp_IsMouseReleased((int)button);

    /// <summary>Returns true if the mouse button was double-clicked this frame.</summary>
    public static bool IsMouseDoubleClicked(MouseButton button) => IGSharp_IsMouseDoubleClicked((int)button);

    /// <summary>Returns true if the mouse button was released this frame after being held for at least <paramref name="delay"/> seconds.</summary>
    public static bool IsMouseReleasedWithDelay(MouseButton button, float delay) => IGSharp_IsMouseReleasedWithDelay((int)button, delay);

    /// <summary>Gets the number of successive clicks of the mouse button this frame (1 = single, 2 = double, ...).</summary>
    public static int GetMouseClickedCount(MouseButton button) => IGSharp_GetMouseClickedCount((int)button);

    /// <summary>Returns true while any mouse button is held down.</summary>
    public static bool IsAnyMouseDown() => IGSharp_IsAnyMouseDown();

    /// <summary>Returns true if the mouse position is inside the given rectangle (optionally clipped against the current clip rect).</summary>
    public static bool IsMouseHoveringRect(Vec2 min, Vec2 max, bool clip = true)
        => IGSharp_IsMouseHoveringRect(new IGSharp_Vec2(min.X, min.Y), new IGSharp_Vec2(max.X, max.Y), clip);

    /// <summary>Returns true if the current mouse position is valid (inside the viewport and available).</summary>
    public static bool IsMousePosValid() => IGSharp_IsMousePosValid(null);

    /// <summary>Returns true if the given mouse position is valid.</summary>
    public static bool IsMousePosValid(Vec2 mousePos)
    {
        var v = new IGSharp_Vec2(mousePos.X, mousePos.Y);
        return IGSharp_IsMousePosValid(&v);
    }

    /// <summary>Gets the current mouse position in screen coordinates.</summary>
    public static Vec2 GetMousePos()
    {
        var v = IGSharp_GetMousePos();
        return new Vec2(v.X, v.Y);
    }

    /// <summary>Returns true if the mouse is being dragged with the given button. <paramref name="lockThreshold"/> of -1 uses io.MouseDragThreshold.</summary>
    public static bool IsMouseDragging(MouseButton button, float lockThreshold = -1f)
        => IGSharp_IsMouseDragging((int)button, lockThreshold);

    /// <summary>Gets the mouse drag delta since the drag started with the given button.</summary>
    public static Vec2 GetMouseDragDelta(MouseButton button = MouseButton.Left, float lockThreshold = -1f)
    {
        var v = IGSharp_GetMouseDragDelta((int)button, lockThreshold);
        return new Vec2(v.X, v.Y);
    }

    /// <summary>Resets the mouse drag delta for the given button (so <see cref="GetMouseDragDelta"/> measures from the current position).</summary>
    public static void ResetMouseDragDelta(MouseButton button = MouseButton.Left) => IGSharp_ResetMouseDragDelta((int)button);

    /// <summary>Gets the mouse position captured when the current popup was opened (useful for context-menu placement).</summary>
    public static Vec2 GetMousePosOnOpeningCurrentPopup()
    {
        var v = IGSharp_GetMousePosOnOpeningCurrentPopup();
        return new Vec2(v.X, v.Y);
    }

    /// <summary>Overrides io.WantCaptureMouse for the next frame (e.g. to force capture or force pass-through).</summary>
    public static void SetNextFrameWantCaptureMouse(bool wantCaptureMouse) => IGSharp_SetNextFrameWantCaptureMouse(wantCaptureMouse);

    // --- Fonts ---

    /// <summary>Gets the shared font atlas. Use this to load custom fonts before the first frame.</summary>
    public static FontAtlas GetFontAtlas() => new(IGSharp_GetIO()->Fonts);

    /// <summary>Sets the default font used when no <see cref="PushFont"/> is active.</summary>
    public static void SetDefaultFont(Font font) => IGSharp_GetIO()->FontDefault = (IGSharp_Font*)font.Handle;

    /// <summary>Gets the current default font.</summary>
    public static Font GetDefaultFont() => new(IGSharp_GetIO()->FontDefault);

    /// <summary>Pushes a font onto the font stack. Pair with <see cref="PopFont"/>.</summary>
    /// <param name="font">Font to activate.</param>
    /// <param name="sizeBaseUnscaled">Override base size in pixels (0 = use the font's declared size).</param>
    public static void PushFont(Font font, float sizeBaseUnscaled = 0f) => IGSharp_PushFont((IGSharp_Font*)font.Handle, sizeBaseUnscaled);

    /// <summary>Pops the last pushed font.</summary>
    public static void PopFont() => IGSharp_PopFont();

    /// <summary>Gets the currently active font.</summary>
    public static Font GetFont() => new(IGSharp_GetFont());

    /// <summary>Gets the active font's pixel size (including DPI scale).</summary>
    public static float GetFontSize() => IGSharp_GetFontSize();

    /// <summary>Gets the current font's baked data at the current size (frame-transient view).</summary>
    public static FontBaked GetFontBaked() => new(IGSharp_GetFontBaked());

    /// <summary>Gets the UV coordinate of a white pixel in the font atlas texture (useful for custom drawing).</summary>
    public static Vec2 GetFontTexUvWhitePixel()
    {
        var v = IGSharp_GetFontTexUvWhitePixel();
        return new Vec2(v.X, v.Y);
    }

    // --- Drag and Drop ---

    /// <summary>Starts a drag-and-drop source on the previously submitted item. Pair with <see cref="EndDragDropSource"/>.</summary>
    public static bool BeginDragDropSource(DragDropFlags flags = DragDropFlags.None)
        => IGSharp_BeginDragDropSource((int)flags);

    /// <summary>Sets the payload bytes for the current drag-and-drop source. Call between <see cref="BeginDragDropSource"/> and <see cref="EndDragDropSource"/>.</summary>
    public static bool SetDragDropPayload(string type, ReadOnlySpan<byte> data, Cond cond = Cond.None)
    {
        fixed (byte* p = data)
            return IGSharp_SetDragDropPayload(ToUtf8(type), p, (nuint)data.Length, (int)cond);
    }

    /// <summary>Sets a typed payload for the current drag-and-drop source. <typeparamref name="T"/> must be an unmanaged blittable type.</summary>
    public static bool SetDragDropPayload<T>(string type, T value, Cond cond = Cond.None) where T : unmanaged
        => IGSharp_SetDragDropPayload(ToUtf8(type), &value, (nuint)sizeof(T), (int)cond);

    /// <summary>Ends the current drag-and-drop source.</summary>
    public static void EndDragDropSource() => IGSharp_EndDragDropSource();

    /// <summary>Starts a drag-and-drop target on the previously submitted item. Pair with <see cref="EndDragDropTarget"/>.</summary>
    public static bool BeginDragDropTarget() => IGSharp_BeginDragDropTarget();

    /// <summary>Accepts a drop of the given type. Returns a valid payload on delivery (or earlier with <see cref="DragDropFlags.AcceptBeforeDelivery"/>).</summary>
    public static DragDropPayload AcceptDragDropPayload(string type, DragDropFlags flags = DragDropFlags.None)
        => new(IGSharp_AcceptDragDropPayload(ToUtf8(type), (int)flags));

    /// <summary>Accepts a drop and reinterprets it as <typeparamref name="T"/>. Returns true on successful delivery.</summary>
    public static bool AcceptDragDropPayload<T>(string type, out T value, DragDropFlags flags = DragDropFlags.None) where T : unmanaged
    {
        var payload = new DragDropPayload(IGSharp_AcceptDragDropPayload(ToUtf8(type), (int)flags));
        return payload.TryGetValue(out value);
    }

    /// <summary>Ends the current drag-and-drop target.</summary>
    public static void EndDragDropTarget() => IGSharp_EndDragDropTarget();

    /// <summary>Gets the payload currently being dragged (or an invalid handle if none).</summary>
    public static DragDropPayload GetDragDropPayload() => new(IGSharp_GetDragDropPayload());

    // --- Miscellaneous Utilities ---

    /// <summary>Returns true if a rectangle of the given size, starting at the cursor position, is visible (not clipped).</summary>
    public static bool IsRectVisible(Vec2 size) => IGSharp_IsRectVisible(new IGSharp_Vec2(size.X, size.Y));

    /// <summary>Returns true if the rectangle (in screen coordinates) is visible (not clipped).</summary>
    public static bool IsRectVisible(Vec2 rectMin, Vec2 rectMax)
        => IGSharp_IsRectVisibleRange(new IGSharp_Vec2(rectMin.X, rectMin.Y), new IGSharp_Vec2(rectMax.X, rectMax.Y));

    /// <summary>Gets the global ImGui time in seconds (incremented by io.DeltaTime every frame).</summary>
    public static double GetTime() => IGSharp_GetTime();

    /// <summary>Gets the global frame counter (incremented every frame).</summary>
    public static int GetFrameCount() => IGSharp_GetFrameCount();

    /// <summary>Gets the current window's state storage (per-window key/value store used for widget state).</summary>
    public static Storage GetStateStorage() => new(IGSharp_GetStateStorage());

    /// <summary>Replaces the current window's state storage (advanced).</summary>
    public static void SetStateStorage(Storage storage) => IGSharp_SetStateStorage(storage.Handle);

    // --- Multi-Select ---

    /// <summary>
    /// Begins a multi-selection scope. Apply the returned <see cref="MultiSelectIO"/>'s requests to your
    /// selection state before submitting items, and re-apply the one returned by <see cref="EndMultiSelect"/>
    /// after. <paramref name="selectionSize"/> is the current selection count (for shortcut handling);
    /// <paramref name="itemsCount"/> is the total number of items (-1 if unknown).
    /// </summary>
    public static MultiSelectIO BeginMultiSelect(MultiSelectFlags flags, int selectionSize, int itemsCount)
        => new(IGSharp_BeginMultiSelect((int)flags, selectionSize, itemsCount));

    /// <summary>Ends a multi-selection scope. Apply the returned <see cref="MultiSelectIO"/>'s requests to your selection state.</summary>
    public static MultiSelectIO EndMultiSelect() => new(IGSharp_EndMultiSelect());

    /// <summary>Associates a user-data value with the next submitted item, used by multi-select to identify items.</summary>
    public static void SetNextItemSelectionUserData(long selectionUserData)
        => IGSharp_SetNextItemSelectionUserData(selectionUserData);

    /// <summary>Returns true if the last item's selection was toggled this frame (within a multi-select scope).</summary>
    public static bool IsItemToggledSelection() => IGSharp_IsItemToggledSelection();

    // --- Table Sort Specs ---

    /// <summary>Gets the sort specs for the current sortable table. Returns an invalid handle if the table is not sortable.</summary>
    public static TableSortSpecs TableGetSortSpecs() => new(IGSharp_TableGetSortSpecs());

    // --- Clipboard ---

    /// <summary>Gets the clipboard text (via the platform backend).</summary>
    public static string? GetClipboardText() => Marshal.PtrToStringUTF8((nint)IGSharp_GetClipboardText());

    /// <summary>Sets the clipboard text (via the platform backend).</summary>
    public static void SetClipboardText(string text) => IGSharp_SetClipboardText(ToUtf8(text));

    // --- Settings / .Ini Utilities ---

    /// <summary>Loads window layout and settings from an .ini file. Call after <see cref="Context.Create"/> and before the first frame.</summary>
    public static void LoadIniSettingsFromDisk(string iniFilename) => IGSharp_LoadIniSettingsFromDisk(ToUtf8(iniFilename));

    /// <summary>Loads window layout and settings from an in-memory .ini string.</summary>
    public static void LoadIniSettingsFromMemory(string iniData)
    {
        var bytes = ToUtf8(iniData)!;
        IGSharp_LoadIniSettingsFromMemory(bytes, (nuint)(bytes.Length - 1));
    }

    /// <summary>Saves window layout and settings to an .ini file.</summary>
    public static void SaveIniSettingsToDisk(string iniFilename) => IGSharp_SaveIniSettingsToDisk(ToUtf8(iniFilename));

    /// <summary>Serializes window layout and settings to an .ini-format string.</summary>
    public static string? SaveIniSettingsToMemory()
    {
        nuint size;
        var ptr = IGSharp_SaveIniSettingsToMemory(&size);
        return ptr == null ? null : System.Text.Encoding.UTF8.GetString(ptr, (int)size);
    }

    // --- Debug Utilities ---

    /// <summary>Helper to diagnose text encoding problems: draws a breakdown of each byte/codepoint in the given text.</summary>
    public static void DebugTextEncoding(string text) => IGSharp_DebugTextEncoding(ToUtf8(text));

    /// <summary>Briefly flashes widgets using the given style color, to help locate where it is used.</summary>
    public static void DebugFlashStyleColor(Col idx) => IGSharp_DebugFlashStyleColor((int)idx);

    /// <summary>Starts the item picker: the next click on any item breaks into the debugger at its submission site.</summary>
    public static void DebugStartItemPicker() => IGSharp_DebugStartItemPicker();

    /// <summary>Writes a line to the ImGui debug log (visible in <see cref="ShowDebugLogWindow()"/>).</summary>
    public static void DebugLog(string text) => IGSharp_DebugLog(ToUtf8(text));

    private static void RequireLength<T>(ReadOnlySpan<T> values, int required, string paramName)
    {
        if (values.Length < required)
            throw new ArgumentException($"The span must contain at least {required} elements.", paramName);
    }

    internal static void ReportUnhandledCallbackException(Exception exception)
    {
        var handlers = UnhandledCallbackException;
        if (handlers == null) return;
        foreach (Action<Exception> handler in handlers.GetInvocationList())
        {
            try { handler(exception); }
            catch { }
        }
    }

    private sealed class CallbackState<T>(T callback)
    {
        private ExceptionDispatchInfo? _exception;
        public T Callback { get; } = callback;
        public void Capture(Exception exception) => _exception ??= ExceptionDispatchInfo.Capture(exception);
        public void ThrowIfFailed() => _exception?.Throw();
    }
}
