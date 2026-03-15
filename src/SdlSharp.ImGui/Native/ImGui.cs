using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.ImGui.Native;

/// <summary>
/// Native P/Invoke bindings for the imgui_sharp C wrapper library.
/// </summary>
public static unsafe partial class ImGui
{
    public const string ImGuiLib = "imgui_sharp";

    /// <summary>Opaque ImGui context handle.</summary>
    public struct ImGuiContext;

    /// <summary>Layout-compatible with ImVec2.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IGSharp_Vec2
    {
        public float X, Y;
        public IGSharp_Vec2(float x, float y) { X = x; Y = y; }
    }

    /// <summary>Layout-compatible with ImVec4.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IGSharp_Vec4
    {
        public float X, Y, Z, W;
        public IGSharp_Vec4(float x, float y, float z, float w) { X = x; Y = y; Z = z; W = w; }
    }

    // --- Context & Lifecycle ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CreateContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ImGuiContext* IGSharp_CreateContext();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DestroyContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DestroyContext(ImGuiContext* ctx);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetCurrentContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ImGuiContext* IGSharp_GetCurrentContext();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetCurrentContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetCurrentContext(ImGuiContext* ctx);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_NewFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_NewFrame();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Render")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Render();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndFrame();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetDrawData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_GetDrawData();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_GetVersion();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CheckVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_CheckVersion();

    // --- IO Accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetWantCaptureMouse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IO_GetWantCaptureMouse();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetWantCaptureKeyboard")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IO_GetWantCaptureKeyboard();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetConfigFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetConfigFlags(int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetConfigFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_IO_GetConfigFlags();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetIniFilename")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetIniFilename(ReadOnlySpan<byte> filename);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetFramerate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_IO_GetFramerate();

    // --- Demo / Styles ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ShowDemoWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ShowDemoWindow(bool* p_open);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ShowMetricsWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ShowMetricsWindow(bool* p_open);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_StyleColorsDark")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_StyleColorsDark();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_StyleColorsLight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_StyleColorsLight();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_StyleColorsClassic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_StyleColorsClassic();

    // --- Style Scale ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_ScaleAllSizes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_ScaleAllSizes(float scale);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetFontScaleDpi")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetFontScaleDpi(float scale);

    // --- Windows ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Begin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Begin(ReadOnlySpan<byte> name, bool* p_open, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_End")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_End();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginChild")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginChild(ReadOnlySpan<byte> str_id, IGSharp_Vec2 size, int child_flags, int window_flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndChild")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndChild();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsWindowAppearing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsWindowAppearing();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsWindowCollapsed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsWindowCollapsed();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsWindowFocused")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsWindowFocused(int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsWindowHovered")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsWindowHovered(int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetWindowPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetWindowPos();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetWindowSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowPos(IGSharp_Vec2 pos, int cond, IGSharp_Vec2 pivot);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowSize(IGSharp_Vec2 size, int cond);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowFocus();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowBgAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowBgAlpha(float alpha);

    // --- Layout ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Separator")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Separator();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SameLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SameLine(float offset_from_start_x, float spacing);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_NewLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_NewLine();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Spacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Spacing();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Dummy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Dummy(IGSharp_Vec2 size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Indent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Indent(float indent_w);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Unindent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Unindent(float indent_w);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginGroup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_BeginGroup();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndGroup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndGroup();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetCursorScreenPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetCursorScreenPos();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetCursorScreenPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetCursorScreenPos(IGSharp_Vec2 screen_pos);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetContentRegionAvail")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetContentRegionAvail();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_AlignTextToFramePadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_AlignTextToFramePadding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetTextLineHeight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetTextLineHeight();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetFrameHeight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetFrameHeight();

    // --- ID Stack ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushIDStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushIDStr(ReadOnlySpan<byte> str_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushIDInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushIDInt(int int_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PopID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PopID();

    // --- Widgets: Text ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextUnformatted")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextUnformatted(ReadOnlySpan<byte> text, byte* text_end);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Text")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Text(ReadOnlySpan<byte> text);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextColored")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextColored(IGSharp_Vec4 col, ReadOnlySpan<byte> text);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextDisabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextDisabled(ReadOnlySpan<byte> text);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextWrapped")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextWrapped(ReadOnlySpan<byte> text);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BulletText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_BulletText(ReadOnlySpan<byte> text);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SeparatorText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SeparatorText(ReadOnlySpan<byte> label);

    // --- Widgets: Buttons ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Button")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Button(ReadOnlySpan<byte> label, IGSharp_Vec2 size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SmallButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SmallButton(ReadOnlySpan<byte> label);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Checkbox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Checkbox(ReadOnlySpan<byte> label, bool* v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_RadioButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_RadioButton(ReadOnlySpan<byte> label, [MarshalAs(UnmanagedType.U1)] bool active);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ProgressBar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ProgressBar(float fraction, IGSharp_Vec2 size_arg, ReadOnlySpan<byte> overlay);

    // --- Widgets: Combo ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginCombo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginCombo(ReadOnlySpan<byte> label, ReadOnlySpan<byte> preview_value, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndCombo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndCombo();

    // --- Widgets: Slider ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderFloat(ReadOnlySpan<byte> label, float* v, float v_min, float v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderInt(ReadOnlySpan<byte> label, int* v, int v_min, int v_max, ReadOnlySpan<byte> format, int flags);

    // --- Widgets: Input ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputText(ReadOnlySpan<byte> label, byte* buf, nuint buf_size, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputFloat(ReadOnlySpan<byte> label, float* v, float step, float step_fast, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputInt(ReadOnlySpan<byte> label, int* v, int step, int step_fast, int flags);

    // --- Widgets: Color ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ColorEdit3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ColorEdit3(ReadOnlySpan<byte> label, float* col, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ColorEdit4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ColorEdit4(ReadOnlySpan<byte> label, float* col, int flags);

    // --- Widgets: Trees ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreeNode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TreeNode(ReadOnlySpan<byte> label);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreePop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TreePop();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CollapsingHeader")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_CollapsingHeader(ReadOnlySpan<byte> label, int flags);

    // --- Widgets: Selectable ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Selectable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Selectable(ReadOnlySpan<byte> label, [MarshalAs(UnmanagedType.U1)] bool selected, int flags, IGSharp_Vec2 size);

    // --- Widgets: Menus ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginMenuBar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginMenuBar();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndMenuBar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndMenuBar();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginMainMenuBar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginMainMenuBar();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndMainMenuBar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndMainMenuBar();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginMenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginMenu(ReadOnlySpan<byte> label, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndMenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndMenu();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MenuItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_MenuItem(ReadOnlySpan<byte> label, ReadOnlySpan<byte> shortcut, [MarshalAs(UnmanagedType.U1)] bool selected, [MarshalAs(UnmanagedType.U1)] bool enabled);

    // --- Widgets: Tooltips ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginTooltip")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginTooltip();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndTooltip")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndTooltip();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetTooltip")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetTooltip(ReadOnlySpan<byte> text);

    // --- Widgets: Popups ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginPopup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginPopup(ReadOnlySpan<byte> str_id, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginPopupModal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginPopupModal(ReadOnlySpan<byte> name, bool* p_open, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndPopup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndPopup();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_OpenPopup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_OpenPopup(ReadOnlySpan<byte> str_id, int popup_flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CloseCurrentPopup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_CloseCurrentPopup();

    // --- Widgets: Tables ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginTable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginTable(ReadOnlySpan<byte> str_id, int columns, int flags, IGSharp_Vec2 outer_size, float inner_width);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndTable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndTable();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableNextRow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableNextRow(int row_flags, float min_row_height);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableNextColumn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TableNextColumn();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSetColumnIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TableSetColumnIndex(int column_n);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSetupColumn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableSetupColumn(ReadOnlySpan<byte> label, int flags, float init_width_or_weight, uint user_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableHeadersRow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableHeadersRow();

    // --- Widgets: Tabs ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginTabBar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginTabBar(ReadOnlySpan<byte> str_id, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndTabBar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndTabBar();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginTabItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginTabItem(ReadOnlySpan<byte> label, bool* p_open, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndTabItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndTabItem();

    // --- Disabling ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginDisabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_BeginDisabled([MarshalAs(UnmanagedType.U1)] bool disabled);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndDisabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndDisabled();

    // --- Item Utilities ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemHovered")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemHovered(int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemClicked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemClicked(int mouse_button);

    // --- Style Stack ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushStyleColorVec4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushStyleColorVec4(int idx, IGSharp_Vec4 col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PopStyleColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PopStyleColor(int count);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushStyleVarFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushStyleVarFloat(int idx, float val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushStyleVarVec2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushStyleVarVec2(int idx, IGSharp_Vec2 val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PopStyleVar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PopStyleVar(int count);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushItemWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushItemWidth(float item_width);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PopItemWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PopItemWidth();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextItemWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextItemWidth(float item_width);
}
