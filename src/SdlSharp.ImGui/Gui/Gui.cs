using System.Runtime.InteropServices;
using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native.ImGui;

namespace SdlSharp.Gui;

/// <summary>
/// Static class providing Dear ImGui widget and layout functions.
/// Mirrors the ImGui:: C++ namespace.
/// </summary>
public static unsafe class Gui
{
    // --- Lifecycle ---

    /// <summary>Finalizes the frame and generates draw data.</summary>
    public static void Render() => IGSharp_Render();

    /// <summary>Gets the opaque draw data pointer for the current frame. Valid after <see cref="Render"/>.</summary>
    public static void* GetDrawData() => IGSharp_GetDrawData();

    /// <summary>Gets the ImGui version string.</summary>
    public static string? GetVersion() => Marshal.PtrToStringUTF8((nint)IGSharp_GetVersion());

    // --- IO ---

    /// <summary>Returns true if ImGui wants to capture mouse input.</summary>
    public static bool WantCaptureMouse => IGSharp_IO_GetWantCaptureMouse();

    /// <summary>Returns true if ImGui wants to capture keyboard input.</summary>
    public static bool WantCaptureKeyboard => IGSharp_IO_GetWantCaptureKeyboard();

    /// <summary>Gets the current frame rate as computed by ImGui.</summary>
    public static float Framerate => IGSharp_IO_GetFramerate();

    /// <summary>Sets the INI filename for saving/loading layout. Pass null to disable.</summary>
    public static void SetIniFilename(string? filename) => IGSharp_IO_SetIniFilename(ToUtf8(filename));

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

    /// <summary>Applies the dark color theme.</summary>
    public static void StyleColorsDark() => IGSharp_StyleColorsDark();

    /// <summary>Applies the light color theme.</summary>
    public static void StyleColorsLight() => IGSharp_StyleColorsLight();

    /// <summary>Applies the classic color theme.</summary>
    public static void StyleColorsClassic() => IGSharp_StyleColorsClassic();

    /// <summary>Scales all style sizes by the given factor.</summary>
    public static void ScaleAllSizes(float scale) => IGSharp_Style_ScaleAllSizes(scale);

    /// <summary>Sets the DPI font scale.</summary>
    public static void SetFontScaleDpi(float scale) => IGSharp_Style_SetFontScaleDpi(scale);

    // --- Windows ---

    /// <summary>Begins a new window. Returns false if the window is collapsed.</summary>
    public static bool Begin(string name, int flags = 0)
        => IGSharp_Begin(ToUtf8(name), null, flags);

    /// <summary>Begins a new window with a close button.</summary>
    public static bool Begin(string name, ref bool open, int flags = 0)
    {
        fixed (bool* p = &open) return IGSharp_Begin(ToUtf8(name), p, flags);
    }

    /// <summary>Ends the current window.</summary>
    public static void End() => IGSharp_End();

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

    /// <summary>Begins a group.</summary>
    public static void BeginGroup() => IGSharp_BeginGroup();

    /// <summary>Ends a group.</summary>
    public static void EndGroup() => IGSharp_EndGroup();

    // --- Widgets: Text ---

    /// <summary>Displays text (no formatting).</summary>
    public static void Text(string text) => IGSharp_Text(ToUtf8(text));

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

    // --- Widgets: Buttons ---

    /// <summary>Creates a button. Returns true when clicked.</summary>
    public static bool Button(string label, float width = 0, float height = 0)
        => IGSharp_Button(ToUtf8(label), new IGSharp_Vec2(width, height));

    /// <summary>Creates a small button (with minimal padding).</summary>
    public static bool SmallButton(string label) => IGSharp_SmallButton(ToUtf8(label));

    /// <summary>Creates a checkbox. Returns true when the value changes.</summary>
    public static bool Checkbox(string label, ref bool v)
    {
        fixed (bool* p = &v) return IGSharp_Checkbox(ToUtf8(label), p);
    }

    /// <summary>Creates a radio button. Returns true when clicked.</summary>
    public static bool RadioButton(string label, bool active) => IGSharp_RadioButton(ToUtf8(label), active);

    // --- Widgets: Slider ---

    private static ReadOnlySpan<byte> DefaultFloatFormat => "%.3f"u8;
    private static ReadOnlySpan<byte> DefaultIntFormat => "%d"u8;

    /// <summary>Creates a float slider.</summary>
    public static bool SliderFloat(string label, ref float v, float min, float max, string? format = null, int flags = 0)
    {
        fixed (float* p = &v)
            return IGSharp_SliderFloat(ToUtf8(label), p, min, max, format != null ? ToUtf8(format) : DefaultFloatFormat, flags);
    }

    /// <summary>Creates an int slider.</summary>
    public static bool SliderInt(string label, ref int v, int min, int max, string? format = null, int flags = 0)
    {
        fixed (int* p = &v)
            return IGSharp_SliderInt(ToUtf8(label), p, min, max, format != null ? ToUtf8(format) : DefaultIntFormat, flags);
    }

    // --- Widgets: Color ---

    /// <summary>Creates a color editor for 3 floats (RGB).</summary>
    public static bool ColorEdit3(string label, ref float r, ref float g, ref float b, int flags = 0)
    {
        var col = stackalloc float[3];
        col[0] = r; col[1] = g; col[2] = b;
        var result = IGSharp_ColorEdit3(ToUtf8(label), col, flags);
        r = col[0]; g = col[1]; b = col[2];
        return result;
    }

    // --- Widgets: Trees ---

    /// <summary>Creates a tree node. Returns true if the node is open.</summary>
    public static bool TreeNode(string label) => IGSharp_TreeNode(ToUtf8(label));

    /// <summary>Pops the tree node.</summary>
    public static void TreePop() => IGSharp_TreePop();

    /// <summary>Creates a collapsing header.</summary>
    public static bool CollapsingHeader(string label, int flags = 0)
        => IGSharp_CollapsingHeader(ToUtf8(label), flags);

    // --- Widgets: Selectable ---

    /// <summary>Creates a selectable item.</summary>
    public static bool Selectable(string label, bool selected = false, int flags = 0, float width = 0, float height = 0)
        => IGSharp_Selectable(ToUtf8(label), selected, flags, new IGSharp_Vec2(width, height));

    // --- Widgets: Combo ---

    /// <summary>Begins a combo box.</summary>
    public static bool BeginCombo(string label, string? previewValue, int flags = 0)
        => IGSharp_BeginCombo(ToUtf8(label), ToUtf8(previewValue), flags);

    /// <summary>Ends a combo box.</summary>
    public static void EndCombo() => IGSharp_EndCombo();

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

    // --- Widgets: Tooltips ---

    /// <summary>Begins a tooltip.</summary>
    public static bool BeginTooltip() => IGSharp_BeginTooltip();

    /// <summary>Ends a tooltip.</summary>
    public static void EndTooltip() => IGSharp_EndTooltip();

    /// <summary>Sets a text tooltip (shorthand).</summary>
    public static void SetTooltip(string text) => IGSharp_SetTooltip(ToUtf8(text));

    // --- Widgets: Popups ---

    /// <summary>Opens a popup by string ID.</summary>
    public static void OpenPopup(string strId, int flags = 0)
        => IGSharp_OpenPopup(ToUtf8(strId), flags);

    /// <summary>Begins a popup.</summary>
    public static bool BeginPopup(string strId, int flags = 0)
        => IGSharp_BeginPopup(ToUtf8(strId), flags);

    /// <summary>Ends a popup.</summary>
    public static void EndPopup() => IGSharp_EndPopup();

    /// <summary>Closes the current popup.</summary>
    public static void CloseCurrentPopup() => IGSharp_CloseCurrentPopup();

    // --- Widgets: Tables ---

    /// <summary>Begins a table.</summary>
    public static bool BeginTable(string strId, int columns, int flags = 0, float outerWidth = 0, float outerHeight = 0, float innerWidth = 0)
        => IGSharp_BeginTable(ToUtf8(strId), columns, flags, new IGSharp_Vec2(outerWidth, outerHeight), innerWidth);

    /// <summary>Ends a table.</summary>
    public static void EndTable() => IGSharp_EndTable();

    /// <summary>Begins the next table row.</summary>
    public static void TableNextRow(int rowFlags = 0, float minRowHeight = 0)
        => IGSharp_TableNextRow(rowFlags, minRowHeight);

    /// <summary>Advances to the next table column.</summary>
    public static bool TableNextColumn() => IGSharp_TableNextColumn();

    /// <summary>Sets up a table column.</summary>
    public static void TableSetupColumn(string label, int flags = 0, float initWidthOrWeight = 0, uint userId = 0)
        => IGSharp_TableSetupColumn(ToUtf8(label), flags, initWidthOrWeight, userId);

    /// <summary>Submits header row for all table columns.</summary>
    public static void TableHeadersRow() => IGSharp_TableHeadersRow();

    // --- Widgets: Tabs ---

    /// <summary>Begins a tab bar.</summary>
    public static bool BeginTabBar(string strId, int flags = 0)
        => IGSharp_BeginTabBar(ToUtf8(strId), flags);

    /// <summary>Ends a tab bar.</summary>
    public static void EndTabBar() => IGSharp_EndTabBar();

    /// <summary>Begins a tab item.</summary>
    public static bool BeginTabItem(string label, int flags = 0)
        => IGSharp_BeginTabItem(ToUtf8(label), null, flags);

    /// <summary>Begins a tab item with a close button.</summary>
    public static bool BeginTabItem(string label, ref bool open, int flags = 0)
    {
        fixed (bool* p = &open) return IGSharp_BeginTabItem(ToUtf8(label), p, flags);
    }

    /// <summary>Ends a tab item.</summary>
    public static void EndTabItem() => IGSharp_EndTabItem();

    // --- Disabling ---

    /// <summary>Begins a disabled section.</summary>
    public static void BeginDisabled(bool disabled = true) => IGSharp_BeginDisabled(disabled);

    /// <summary>Ends a disabled section.</summary>
    public static void EndDisabled() => IGSharp_EndDisabled();

    // --- Item Utilities ---

    /// <summary>Returns true if the last item is hovered.</summary>
    public static bool IsItemHovered(int flags = 0) => IGSharp_IsItemHovered(flags);

    /// <summary>Returns true if the last item was clicked.</summary>
    public static bool IsItemClicked(int mouseButton = 0) => IGSharp_IsItemClicked(mouseButton);

    // --- Style Stack ---

    /// <summary>Pushes a float style variable.</summary>
    public static void PushStyleVar(int idx, float val) => IGSharp_PushStyleVarFloat(idx, val);

    /// <summary>Pops style variables.</summary>
    public static void PopStyleVar(int count = 1) => IGSharp_PopStyleVar(count);

    /// <summary>Sets the width of the next item.</summary>
    public static void SetNextItemWidth(float itemWidth) => IGSharp_SetNextItemWidth(itemWidth);
}
