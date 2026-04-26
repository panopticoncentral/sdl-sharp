using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SdlSharp.Native;
// ReSharper disable InconsistentNaming

namespace SdlSharp.ImGui;

/// <summary>
/// Native P/Invoke bindings for the imgui_sharp C wrapper library,
/// including the SDL3 platform backend and SDL_GPU renderer backend.
/// </summary>
internal static unsafe partial class Native
{
    private const string ImGuiLib = "imgui_sharp";

    /// <summary>Opaque ImGui context handle.</summary>
    public struct ImGuiContext;

    /// <summary>Layout-compatible with ImVec2.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IGSharp_Vec2(float x, float y)
    {
        public float X = x, Y = y;
    }

    /// <summary>Layout-compatible with ImVec4.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IGSharp_Vec4(float x, float y, float z, float w)
    {
        public float X = x, Y = y, Z = z, W = w;
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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetWindowWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetWindowWidth();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetWindowHeight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetWindowHeight();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowPos(IGSharp_Vec2 pos, int cond, IGSharp_Vec2 pivot);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowSize(IGSharp_Vec2 size, int cond);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowCollapsed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowCollapsed([MarshalAs(UnmanagedType.U1)] bool collapsed, int cond);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetCursorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetCursorPos();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetCursorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetCursorPos(IGSharp_Vec2 local_pos);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetTextLineHeightWithSpacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetTextLineHeightWithSpacing();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetFrameHeight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetFrameHeight();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetFrameHeightWithSpacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetFrameHeightWithSpacing();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CalcTextSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_CalcTextSize(ReadOnlySpan<byte> text, byte* text_end, [MarshalAs(UnmanagedType.U1)] bool hide_text_after_double_hash, float wrap_width);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_LabelText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_LabelText(ReadOnlySpan<byte> label, ReadOnlySpan<byte> text);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextLink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TextLink(ReadOnlySpan<byte> label);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextLinkOpenURL")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextLinkOpenURL(ReadOnlySpan<byte> label, ReadOnlySpan<byte> url);

    // --- Widgets: Buttons ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Button")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Button(ReadOnlySpan<byte> label, IGSharp_Vec2 size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SmallButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SmallButton(ReadOnlySpan<byte> label);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InvisibleButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InvisibleButton(ReadOnlySpan<byte> str_id, IGSharp_Vec2 size, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ArrowButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ArrowButton(ReadOnlySpan<byte> str_id, int dir);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Bullet")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Bullet();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Checkbox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Checkbox(ReadOnlySpan<byte> label, bool* v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_RadioButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_RadioButton(ReadOnlySpan<byte> label, [MarshalAs(UnmanagedType.U1)] bool active);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_RadioButtonInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_RadioButtonInt(ReadOnlySpan<byte> label, int* v, int v_button);

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

    // --- Widgets: Drag ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragFloat(ReadOnlySpan<byte> label, float* v, float v_speed, float v_min, float v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragFloat2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragFloat2(ReadOnlySpan<byte> label, float* v, float v_speed, float v_min, float v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragFloat3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragFloat3(ReadOnlySpan<byte> label, float* v, float v_speed, float v_min, float v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragFloat4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragFloat4(ReadOnlySpan<byte> label, float* v, float v_speed, float v_min, float v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragInt(ReadOnlySpan<byte> label, int* v, float v_speed, int v_min, int v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragInt2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragInt2(ReadOnlySpan<byte> label, int* v, float v_speed, int v_min, int v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragInt3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragInt3(ReadOnlySpan<byte> label, int* v, float v_speed, int v_min, int v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragInt4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragInt4(ReadOnlySpan<byte> label, int* v, float v_speed, int v_min, int v_max, ReadOnlySpan<byte> format, int flags);

    // --- Widgets: Slider ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderFloat(ReadOnlySpan<byte> label, float* v, float v_min, float v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderInt(ReadOnlySpan<byte> label, int* v, int v_min, int v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderFloat2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderFloat2(ReadOnlySpan<byte> label, float* v, float v_min, float v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderFloat3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderFloat3(ReadOnlySpan<byte> label, float* v, float v_min, float v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderFloat4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderFloat4(ReadOnlySpan<byte> label, float* v, float v_min, float v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderInt2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderInt2(ReadOnlySpan<byte> label, int* v, int v_min, int v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderInt3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderInt3(ReadOnlySpan<byte> label, int* v, int v_min, int v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderInt4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderInt4(ReadOnlySpan<byte> label, int* v, int v_min, int v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderAngle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderAngle(ReadOnlySpan<byte> label, float* v_rad, float v_degrees_min, float v_degrees_max, ReadOnlySpan<byte> format, int flags);

    // --- Widgets: Input ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputText(ReadOnlySpan<byte> label, byte* buf, nuint buf_size, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextMultiline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextMultiline(ReadOnlySpan<byte> label, byte* buf, nuint buf_size, IGSharp_Vec2 size, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextWithHint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextWithHint(ReadOnlySpan<byte> label, ReadOnlySpan<byte> hint, byte* buf, nuint buf_size, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextEx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextEx(ReadOnlySpan<byte> label, byte* buf, nuint buf_size, int flags, delegate* unmanaged[Cdecl]<void*, int> callback, void* user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextMultilineEx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextMultilineEx(ReadOnlySpan<byte> label, byte* buf, nuint buf_size, IGSharp_Vec2 size, int flags, delegate* unmanaged[Cdecl]<void*, int> callback, void* user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextWithHintEx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextWithHintEx(ReadOnlySpan<byte> label, ReadOnlySpan<byte> hint, byte* buf, nuint buf_size, int flags, delegate* unmanaged[Cdecl]<void*, int> callback, void* user_data);

    // --- InputTextCallbackData: Field Accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetEventFlag")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetEventFlag(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetFlags(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_InputTextCallbackData_GetUserData(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetEventKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetEventKey(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetEventChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort IGSharp_InputTextCallbackData_GetEventChar(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetEventChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetEventChar(void* data, ushort c);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetEventActivated")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextCallbackData_GetEventActivated(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetBuf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_InputTextCallbackData_GetBuf(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetBufTextLen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetBufTextLen(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetBufTextLen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetBufTextLen(void* data, int v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetBufSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetBufSize(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetBufDirty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextCallbackData_GetBufDirty(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetBufDirty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetBufDirty(void* data, [MarshalAs(UnmanagedType.U1)] bool v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetCursorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetCursorPos(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetCursorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetCursorPos(void* data, int v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetSelectionStart")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetSelectionStart(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetSelectionStart")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetSelectionStart(void* data, int v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetSelectionEnd")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetSelectionEnd(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetSelectionEnd")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetSelectionEnd(void* data, int v);

    // --- InputTextCallbackData: Helper Methods ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_DeleteChars")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_DeleteChars(void* data, int pos, int bytes_count);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_InsertChars")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_InsertChars(void* data, int pos, ReadOnlySpan<byte> text, byte* text_end);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SelectAll")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SelectAll(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_ClearSelection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_ClearSelection(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_HasSelection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextCallbackData_HasSelection(void* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputFloat(ReadOnlySpan<byte> label, float* v, float step, float step_fast, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputInt(ReadOnlySpan<byte> label, int* v, int step, int step_fast, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputFloat2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputFloat2(ReadOnlySpan<byte> label, float* v, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputFloat3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputFloat3(ReadOnlySpan<byte> label, float* v, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputFloat4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputFloat4(ReadOnlySpan<byte> label, float* v, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputInt2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputInt2(ReadOnlySpan<byte> label, int* v, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputInt3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputInt3(ReadOnlySpan<byte> label, int* v, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputInt4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputInt4(ReadOnlySpan<byte> label, int* v, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputDouble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputDouble(ReadOnlySpan<byte> label, double* v, double step, double step_fast, ReadOnlySpan<byte> format, int flags);

    // --- Widgets: Color ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ColorEdit3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ColorEdit3(ReadOnlySpan<byte> label, float* col, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ColorEdit4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ColorEdit4(ReadOnlySpan<byte> label, float* col, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ColorPicker3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ColorPicker3(ReadOnlySpan<byte> label, float* col, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ColorPicker4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ColorPicker4(ReadOnlySpan<byte> label, float* col, int flags, float* ref_col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ColorButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ColorButton(ReadOnlySpan<byte> desc_id, IGSharp_Vec4 col, int flags, IGSharp_Vec2 size);

    // --- Widgets: Images ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Image")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Image(ulong tex_id, IGSharp_Vec2 image_size, IGSharp_Vec2 uv0, IGSharp_Vec2 uv1, IGSharp_Vec4 tint_col, IGSharp_Vec4 border_col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImageButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ImageButton(ReadOnlySpan<byte> str_id, ulong tex_id, IGSharp_Vec2 image_size, IGSharp_Vec2 uv0, IGSharp_Vec2 uv1, IGSharp_Vec4 bg_col, IGSharp_Vec4 tint_col);

    // --- Widgets: Trees ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreeNode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TreeNode(ReadOnlySpan<byte> label);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreeNodeEx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TreeNodeEx(ReadOnlySpan<byte> label, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreePop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TreePop();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetTreeNodeToLabelSpacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetTreeNodeToLabelSpacing();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreeNodeGetOpen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TreeNodeGetOpen(uint storage_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CollapsingHeader")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_CollapsingHeader(ReadOnlySpan<byte> label, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CollapsingHeaderClosable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_CollapsingHeaderClosable(ReadOnlySpan<byte> label, bool* p_visible, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextItemOpen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextItemOpen([MarshalAs(UnmanagedType.U1)] bool is_open, int cond);

    // --- Widgets: Selectable ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Selectable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Selectable(ReadOnlySpan<byte> label, [MarshalAs(UnmanagedType.U1)] bool selected, int flags, IGSharp_Vec2 size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectablePtr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SelectablePtr(ReadOnlySpan<byte> label, bool* p_selected, int flags, IGSharp_Vec2 size);

    // --- Widgets: List Box ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginListBox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginListBox(ReadOnlySpan<byte> label, IGSharp_Vec2 size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndListBox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndListBox();

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MenuItemPtr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_MenuItemPtr(ReadOnlySpan<byte> label, ReadOnlySpan<byte> shortcut, bool* p_selected, [MarshalAs(UnmanagedType.U1)] bool enabled);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginItemTooltip")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginItemTooltip();

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginPopupContextItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginPopupContextItem(ReadOnlySpan<byte> str_id, int popup_flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginPopupContextWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginPopupContextWindow(ReadOnlySpan<byte> str_id, int popup_flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsPopupOpen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsPopupOpen(ReadOnlySpan<byte> str_id, int flags);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableHeader")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableHeader(ReadOnlySpan<byte> label);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSetupScrollFreeze")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableSetupScrollFreeze(int cols, int rows);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TabItemButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TabItemButton(ReadOnlySpan<byte> label, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetTabItemClosed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetTabItemClosed(ReadOnlySpan<byte> tab_or_docked_window_label);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemActive();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemFocused")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemFocused();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemVisible")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemVisible();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemEdited")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemEdited();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemActivated")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemActivated();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemDeactivated")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemDeactivated();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemDeactivatedAfterEdit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemDeactivatedAfterEdit();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemToggledOpen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemToggledOpen();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetItemRectMin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetItemRectMin();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetItemRectMax")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetItemRectMax();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetItemRectSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetItemRectSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetItemDefaultFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetItemDefaultFocus();

    // --- Style Stack ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushStyleColorU32")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushStyleColorU32(int idx, uint col);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CalcItemWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_CalcItemWidth();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushTextWrapPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushTextWrapPos(float wrap_local_pos_x);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PopTextWrapPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PopTextWrapPos();

    // --- Color Utilities ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetColorU32")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_GetColorU32(int idx, float alpha_mul);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetColorU32Vec4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_GetColorU32Vec4(IGSharp_Vec4 col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetColorU32Packed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_GetColorU32Packed(uint col, float alpha_mul);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ColorConvertU32ToFloat4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec4 IGSharp_ColorConvertU32ToFloat4(uint @in);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ColorConvertFloat4ToU32")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_ColorConvertFloat4ToU32(IGSharp_Vec4 @in);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ColorConvertRGBtoHSV")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ColorConvertRGBtoHSV(float r, float g, float b, float* out_h, float* out_s, float* out_v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ColorConvertHSVtoRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ColorConvertHSVtoRGB(float h, float s, float v, float* out_r, float* out_g, float* out_b);

    // --- DrawList: Accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetWindowDrawList")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_GetWindowDrawList();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetBackgroundDrawList")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_GetBackgroundDrawList();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetForegroundDrawList")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_GetForegroundDrawList();

    // --- DrawList: Clipping ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PushClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PushClipRect(void* draw_list, IGSharp_Vec2 clip_rect_min, IGSharp_Vec2 clip_rect_max, [MarshalAs(UnmanagedType.U1)] bool intersect_with_current);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PushClipRectFullScreen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PushClipRectFullScreen(void* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PopClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PopClipRect(void* draw_list);

    // --- DrawList: Primitives ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddLine(void* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, uint col, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddRect(void* draw_list, IGSharp_Vec2 p_min, IGSharp_Vec2 p_max, uint col, float rounding, int flags, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddRectFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddRectFilled(void* draw_list, IGSharp_Vec2 p_min, IGSharp_Vec2 p_max, uint col, float rounding, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddRectFilledMultiColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddRectFilledMultiColor(void* draw_list, IGSharp_Vec2 p_min, IGSharp_Vec2 p_max, uint col_ul, uint col_ur, uint col_br, uint col_bl);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddQuad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddQuad(void* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, IGSharp_Vec2 p4, uint col, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddQuadFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddQuadFilled(void* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, IGSharp_Vec2 p4, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddTriangle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddTriangle(void* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, uint col, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddTriangleFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddTriangleFilled(void* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddCircle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddCircle(void* draw_list, IGSharp_Vec2 center, float radius, uint col, int num_segments, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddCircleFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddCircleFilled(void* draw_list, IGSharp_Vec2 center, float radius, uint col, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddNgon")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddNgon(void* draw_list, IGSharp_Vec2 center, float radius, uint col, int num_segments, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddNgonFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddNgonFilled(void* draw_list, IGSharp_Vec2 center, float radius, uint col, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddEllipse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddEllipse(void* draw_list, IGSharp_Vec2 center, IGSharp_Vec2 radius, uint col, float rot, int num_segments, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddEllipseFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddEllipseFilled(void* draw_list, IGSharp_Vec2 center, IGSharp_Vec2 radius, uint col, float rot, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddText(void* draw_list, IGSharp_Vec2 pos, uint col, ReadOnlySpan<byte> text_begin, byte* text_end);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddBezierCubic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddBezierCubic(void* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, IGSharp_Vec2 p4, uint col, float thickness, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddBezierQuadratic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddBezierQuadratic(void* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, uint col, float thickness, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddPolyline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddPolyline(void* draw_list, IGSharp_Vec2* points, int num_points, uint col, int flags, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddConvexPolyFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddConvexPolyFilled(void* draw_list, IGSharp_Vec2* points, int num_points, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddConcavePolyFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddConcavePolyFilled(void* draw_list, IGSharp_Vec2* points, int num_points, uint col);

    // --- DrawList: Images ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddImage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddImage(void* draw_list, ulong tex_id, IGSharp_Vec2 p_min, IGSharp_Vec2 p_max, IGSharp_Vec2 uv_min, IGSharp_Vec2 uv_max, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddImageQuad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddImageQuad(void* draw_list, ulong tex_id, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, IGSharp_Vec2 p4, IGSharp_Vec2 uv1, IGSharp_Vec2 uv2, IGSharp_Vec2 uv3, IGSharp_Vec2 uv4, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddImageRounded")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddImageRounded(void* draw_list, ulong tex_id, IGSharp_Vec2 p_min, IGSharp_Vec2 p_max, IGSharp_Vec2 uv_min, IGSharp_Vec2 uv_max, uint col, float rounding, int flags);

    // --- DrawList: Path API ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathClear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathClear(void* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathLineTo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathLineTo(void* draw_list, IGSharp_Vec2 pos);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathLineToMergeDuplicate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathLineToMergeDuplicate(void* draw_list, IGSharp_Vec2 pos);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathFillConvex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathFillConvex(void* draw_list, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathStroke")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathStroke(void* draw_list, uint col, int flags, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathArcTo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathArcTo(void* draw_list, IGSharp_Vec2 center, float radius, float a_min, float a_max, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathArcToFast")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathArcToFast(void* draw_list, IGSharp_Vec2 center, float radius, int a_min_of_12, int a_max_of_12);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathBezierCubicCurveTo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathBezierCubicCurveTo(void* draw_list, IGSharp_Vec2 p2, IGSharp_Vec2 p3, IGSharp_Vec2 p4, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathBezierQuadraticCurveTo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathBezierQuadraticCurveTo(void* draw_list, IGSharp_Vec2 p2, IGSharp_Vec2 p3, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathRect(void* draw_list, IGSharp_Vec2 rect_min, IGSharp_Vec2 rect_max, float rounding, int flags);

    // --- Keyboard / Mouse Input Queries ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsKeyDown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsKeyDown(int key);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsKeyPressed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsKeyPressed(int key, [MarshalAs(UnmanagedType.U1)] bool repeat);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsKeyReleased")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsKeyReleased(int key);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsKeyChordPressed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsKeyChordPressed(int key_chord);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsMouseDown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsMouseDown(int button);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsMouseClicked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsMouseClicked(int button, [MarshalAs(UnmanagedType.U1)] bool repeat);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsMouseReleased")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsMouseReleased(int button);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsMouseDoubleClicked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsMouseDoubleClicked(int button);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsMouseHoveringRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsMouseHoveringRect(IGSharp_Vec2 r_min, IGSharp_Vec2 r_max, [MarshalAs(UnmanagedType.U1)] bool clip);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsMousePosValid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsMousePosValid(IGSharp_Vec2* mouse_pos);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetMousePos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetMousePos();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsMouseDragging")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsMouseDragging(int button, float lock_threshold);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetMouseDragDelta")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetMouseDragDelta(int button, float lock_threshold);

    // --- Viewport ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetMainViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_GetMainViewport();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Viewport_GetPos(void* viewport);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Viewport_GetSize(void* viewport);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetWorkPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Viewport_GetWorkPos(void* viewport);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetWorkSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Viewport_GetWorkSize(void* viewport);

    // --- ListClipper ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_New")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_ListClipper_New();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_Delete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ListClipper_Delete(void* clipper);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_Begin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ListClipper_Begin(void* clipper, int items_count, float items_height);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_End")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ListClipper_End(void* clipper);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_Step")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ListClipper_Step(void* clipper);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_IncludeItemsByIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ListClipper_IncludeItemsByIndex(void* clipper, int item_begin, int item_end);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_SeekCursorForItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ListClipper_SeekCursorForItem(void* clipper, int item_index);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_GetDisplayStart")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_ListClipper_GetDisplayStart(void* clipper);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_GetDisplayEnd")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_ListClipper_GetDisplayEnd(void* clipper);

    // --- Plot Widgets ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlotLines")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlotLines(ReadOnlySpan<byte> label, float* values, int values_count, int values_offset, ReadOnlySpan<byte> overlay_text, float scale_min, float scale_max, IGSharp_Vec2 graph_size, int stride);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlotHistogram")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlotHistogram(ReadOnlySpan<byte> label, float* values, int values_count, int values_offset, ReadOnlySpan<byte> overlay_text, float scale_min, float scale_max, IGSharp_Vec2 graph_size, int stride);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlotLinesCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlotLinesCallback(ReadOnlySpan<byte> label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count, int values_offset, ReadOnlySpan<byte> overlay_text, float scale_min, float scale_max, IGSharp_Vec2 graph_size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlotHistogramCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlotHistogramCallback(ReadOnlySpan<byte> label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count, int values_offset, ReadOnlySpan<byte> overlay_text, float scale_min, float scale_max, IGSharp_Vec2 graph_size);

    // --- Fonts: Atlas Loading ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetFonts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_IO_GetFonts();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddFontDefault")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_FontAtlas_AddFontDefault(void* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddFontFromFileTTF")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_FontAtlas_AddFontFromFileTTF(void* atlas, ReadOnlySpan<byte> filename, float size_pixels);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddFontFromMemoryTTF")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_FontAtlas_AddFontFromMemoryTTF(void* atlas, void* font_data, int font_data_size, float size_pixels, [MarshalAs(UnmanagedType.U1)] bool transfer_ownership);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddFontFromMemoryCompressedTTF")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_FontAtlas_AddFontFromMemoryCompressedTTF(void* atlas, void* compressed_data, int compressed_size, float size_pixels);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_Build")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontAtlas_Build(void* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_Clear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_Clear(void* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_ClearFonts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_ClearFonts(void* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetFontCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontAtlas_GetFontCount(void* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetFont")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_FontAtlas_GetFont(void* atlas, int index);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetFontDefault")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetFontDefault(void* font);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetFontDefault")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_IO_GetFontDefault();

    // --- Fonts: Push/Pop/Query ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushFont")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushFont(void* font, float font_size_base_unscaled);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PopFont")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PopFont();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetFont")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_GetFont();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetFontSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetFontSize();

    // --- Drag and Drop ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginDragDropSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginDragDropSource(int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetDragDropPayload")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SetDragDropPayload(ReadOnlySpan<byte> type, void* data, nuint sz, int cond);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndDragDropSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndDragDropSource();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginDragDropTarget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginDragDropTarget();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_AcceptDragDropPayload")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_AcceptDragDropPayload(ReadOnlySpan<byte> type, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndDragDropTarget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndDragDropTarget();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetDragDropPayload")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_GetDragDropPayload();

    // --- Drag and Drop: Payload Accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Payload_GetData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_Payload_GetData(void* payload);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Payload_GetDataSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_Payload_GetDataSize(void* payload);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Payload_GetDataType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_Payload_GetDataType(void* payload);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Payload_IsDataType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Payload_IsDataType(void* payload, ReadOnlySpan<byte> type);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Payload_IsPreview")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Payload_IsPreview(void* payload);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Payload_IsDelivery")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Payload_IsDelivery(void* payload);

    // --- Multi-Select ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginMultiSelect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_BeginMultiSelect(int flags, int selection_size, int items_count);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndMultiSelect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_EndMultiSelect();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextItemSelectionUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextItemSelectionUserData(long selection_user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemToggledSelection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemToggledSelection();

    // --- MultiSelectIO Accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetRequestsCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_MultiSelectIO_GetRequestsCount(void* io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetRequest")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_MultiSelectIO_GetRequest(void* io, int index);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetRangeSrcItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long IGSharp_MultiSelectIO_GetRangeSrcItem(void* io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetNavIdItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long IGSharp_MultiSelectIO_GetNavIdItem(void* io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetNavIdSelected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_MultiSelectIO_GetNavIdSelected(void* io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetRangeSrcReset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_MultiSelectIO_GetRangeSrcReset(void* io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_SetRangeSrcReset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_MultiSelectIO_SetRangeSrcReset(void* io, [MarshalAs(UnmanagedType.U1)] bool v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetItemsCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_MultiSelectIO_GetItemsCount(void* io);

    // --- SelectionRequest Accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionRequest_GetType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_SelectionRequest_GetType(void* request);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionRequest_GetSelected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SelectionRequest_GetSelected(void* request);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionRequest_GetRangeDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_SelectionRequest_GetRangeDirection(void* request);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionRequest_GetRangeFirstItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long IGSharp_SelectionRequest_GetRangeFirstItem(void* request);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionRequest_GetRangeLastItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long IGSharp_SelectionRequest_GetRangeLastItem(void* request);

    // --- Table Sort Specs ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableGetSortSpecs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_TableGetSortSpecs();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSortSpecs_GetSpecsCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableSortSpecs_GetSpecsCount(void* specs);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSortSpecs_GetSpec")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_TableSortSpecs_GetSpec(void* specs, int index);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSortSpecs_GetSpecsDirty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TableSortSpecs_GetSpecsDirty(void* specs);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSortSpecs_SetSpecsDirty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableSortSpecs_SetSpecsDirty(void* specs, [MarshalAs(UnmanagedType.U1)] bool v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableColumnSortSpecs_GetColumnUserID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_TableColumnSortSpecs_GetColumnUserID(void* spec);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableColumnSortSpecs_GetColumnIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableColumnSortSpecs_GetColumnIndex(void* spec);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableColumnSortSpecs_GetSortOrder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableColumnSortSpecs_GetSortOrder(void* spec);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableColumnSortSpecs_GetSortDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableColumnSortSpecs_GetSortDirection(void* spec);

    // --- IO: Field Accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetDisplaySize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_IO_GetDisplaySize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetDisplaySize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetDisplaySize(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetDisplayFramebufferScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_IO_GetDisplayFramebufferScale();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetDisplayFramebufferScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetDisplayFramebufferScale(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetDeltaTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_IO_GetDeltaTime();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetDeltaTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetDeltaTime(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetMousePos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_IO_GetMousePos();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetMouseDelta")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_IO_GetMouseDelta();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetMouseWheel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_IO_GetMouseWheel();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetMouseWheelH")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_IO_GetMouseWheelH();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetKeyCtrl")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IO_GetKeyCtrl();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetKeyShift")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IO_GetKeyShift();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetKeyAlt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IO_GetKeyAlt();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetKeySuper")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IO_GetKeySuper();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetWantTextInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IO_GetWantTextInput();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetWantSetMousePos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IO_GetWantSetMousePos();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetWantSaveIniSettings")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IO_GetWantSaveIniSettings();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetWantSaveIniSettings")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetWantSaveIniSettings([MarshalAs(UnmanagedType.U1)] bool v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetNavActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IO_GetNavActive();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetNavVisible")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IO_GetNavVisible();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetMetricsRenderVertices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_IO_GetMetricsRenderVertices();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetMetricsRenderIndices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_IO_GetMetricsRenderIndices();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetMetricsRenderWindows")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_IO_GetMetricsRenderWindows();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetMetricsActiveWindows")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_IO_GetMetricsActiveWindows();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetBackendFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_IO_GetBackendFlags();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetBackendFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetBackendFlags(int v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetMouseDoubleClickTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_IO_GetMouseDoubleClickTime();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetMouseDoubleClickTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetMouseDoubleClickTime(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetMouseDoubleClickMaxDist")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_IO_GetMouseDoubleClickMaxDist();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetMouseDoubleClickMaxDist")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetMouseDoubleClickMaxDist(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetMouseDragThreshold")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_IO_GetMouseDragThreshold();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetMouseDragThreshold")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetMouseDragThreshold(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetKeyRepeatDelay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_IO_GetKeyRepeatDelay();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetKeyRepeatDelay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetKeyRepeatDelay(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_GetKeyRepeatRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_IO_GetKeyRepeatRate();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetKeyRepeatRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetKeyRepeatRate(float v);

    // --- IO: Event Queue ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddKeyEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddKeyEvent(int key, [MarshalAs(UnmanagedType.U1)] bool down);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddKeyAnalogEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddKeyAnalogEvent(int key, [MarshalAs(UnmanagedType.U1)] bool down, float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddMousePosEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddMousePosEvent(float x, float y);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddMouseButtonEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddMouseButtonEvent(int button, [MarshalAs(UnmanagedType.U1)] bool down);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddMouseWheelEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddMouseWheelEvent(float wheel_x, float wheel_y);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddMouseSourceEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddMouseSourceEvent(int source);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddFocusEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddFocusEvent([MarshalAs(UnmanagedType.U1)] bool focused);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddInputCharacter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddInputCharacter(uint c);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddInputCharacterUTF16")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddInputCharacterUTF16(ushort c);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddInputCharactersUTF8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddInputCharactersUTF8(ReadOnlySpan<byte> str);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetAppAcceptingEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetAppAcceptingEvents([MarshalAs(UnmanagedType.U1)] bool accepting);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_ClearEventsQueue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_ClearEventsQueue();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_ClearInputKeys")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_ClearInputKeys();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_ClearInputMouse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_ClearInputMouse();

    // --- Style: Scalar Fields ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetFontSizeBase")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetFontSizeBase();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetFontSizeBase")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetFontSizeBase(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetFontScaleMain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetFontScaleMain();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetFontScaleMain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetFontScaleMain(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetFontScaleDpi")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetFontScaleDpi();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetAlpha();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetAlpha(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetDisabledAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetDisabledAlpha();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetDisabledAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetDisabledAlpha(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetWindowRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetWindowRounding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetWindowRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetWindowRounding(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetWindowBorderSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetWindowBorderSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetWindowBorderSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetWindowBorderSize(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetChildRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetChildRounding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetChildRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetChildRounding(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetChildBorderSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetChildBorderSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetChildBorderSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetChildBorderSize(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetPopupRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetPopupRounding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetPopupRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetPopupRounding(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetPopupBorderSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetPopupBorderSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetPopupBorderSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetPopupBorderSize(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetFrameRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetFrameRounding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetFrameRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetFrameRounding(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetFrameBorderSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetFrameBorderSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetFrameBorderSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetFrameBorderSize(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetIndentSpacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetIndentSpacing();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetIndentSpacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetIndentSpacing(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetColumnsMinSpacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetColumnsMinSpacing();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetColumnsMinSpacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetColumnsMinSpacing(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetScrollbarSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetScrollbarSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetScrollbarSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetScrollbarSize(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetScrollbarRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetScrollbarRounding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetScrollbarRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetScrollbarRounding(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetGrabMinSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetGrabMinSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetGrabMinSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetGrabMinSize(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetGrabRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetGrabRounding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetGrabRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetGrabRounding(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetImageRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetImageRounding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetImageRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetImageRounding(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetImageBorderSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetImageBorderSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetImageBorderSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetImageBorderSize(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetTabRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetTabRounding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetTabRounding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetTabRounding(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetTabBorderSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetTabBorderSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetTabBorderSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetTabBorderSize(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetSeparatorSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetSeparatorSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetSeparatorSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetSeparatorSize(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetMouseCursorScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetMouseCursorScale();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetMouseCursorScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetMouseCursorScale(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetAntiAliasedLines")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Style_GetAntiAliasedLines();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetAntiAliasedLines")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetAntiAliasedLines([MarshalAs(UnmanagedType.U1)] bool v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetAntiAliasedFill")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Style_GetAntiAliasedFill();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetAntiAliasedFill")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetAntiAliasedFill([MarshalAs(UnmanagedType.U1)] bool v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetCurveTessellationTol")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetCurveTessellationTol();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetCurveTessellationTol")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetCurveTessellationTol(float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetCircleTessellationMaxError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Style_GetCircleTessellationMaxError();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetCircleTessellationMaxError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetCircleTessellationMaxError(float v);

    // --- Style: Vec2 Fields ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetWindowPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetWindowPadding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetWindowPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetWindowPadding(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetWindowMinSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetWindowMinSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetWindowMinSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetWindowMinSize(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetWindowTitleAlign")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetWindowTitleAlign();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetWindowTitleAlign")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetWindowTitleAlign(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetFramePadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetFramePadding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetFramePadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetFramePadding(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetItemSpacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetItemSpacing();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetItemSpacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetItemSpacing(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetItemInnerSpacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetItemInnerSpacing();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetItemInnerSpacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetItemInnerSpacing(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetCellPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetCellPadding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetCellPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetCellPadding(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetTouchExtraPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetTouchExtraPadding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetTouchExtraPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetTouchExtraPadding(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetButtonTextAlign")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetButtonTextAlign();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetButtonTextAlign")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetButtonTextAlign(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetSelectableTextAlign")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetSelectableTextAlign();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetSelectableTextAlign")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetSelectableTextAlign(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetSeparatorTextAlign")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetSeparatorTextAlign();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetSeparatorTextAlign")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetSeparatorTextAlign(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetSeparatorTextPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetSeparatorTextPadding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetSeparatorTextPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetSeparatorTextPadding(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetDisplayWindowPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetDisplayWindowPadding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetDisplayWindowPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetDisplayWindowPadding(IGSharp_Vec2 v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetDisplaySafeAreaPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Style_GetDisplaySafeAreaPadding();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetDisplaySafeAreaPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetDisplaySafeAreaPadding(IGSharp_Vec2 v);

    // --- Style: Colors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_GetColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec4 IGSharp_Style_GetColor(int idx);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_SetColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_SetColor(int idx, IGSharp_Vec4 col);

    // --- Scrolling ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetScrollX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetScrollX();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetScrollY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetScrollY();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetScrollX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetScrollX(float scroll_x);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetScrollY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetScrollY(float scroll_y);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetScrollMaxX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetScrollMaxX();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetScrollMaxY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetScrollMaxY();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetScrollHereX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetScrollHereX(float center_x_ratio);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetScrollHereY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetScrollHereY(float center_y_ratio);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetScrollFromPosX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetScrollFromPosX(float local_x, float center_x_ratio);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetScrollFromPosY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetScrollFromPosY(float local_y, float center_y_ratio);

    // --- Item Flags ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushItemFlag")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushItemFlag(int option, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PopItemFlag")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PopItemFlag();

    // --- Focus / Activation ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetKeyboardFocusHere")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetKeyboardFocusHere(int offset);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextItemAllowOverlap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextItemAllowOverlap();

    // --- Item Utilities (extra) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetItemID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_GetItemID();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsAnyItemHovered")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsAnyItemHovered();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsAnyItemActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsAnyItemActive();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsAnyItemFocused")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsAnyItemFocused();

    // --- Mouse Cursor ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetMouseCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_GetMouseCursor();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetMouseCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetMouseCursor(int cursor_type);

    // --- Window Manipulation (extra) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowSizeConstraints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowSizeConstraints(IGSharp_Vec2 size_min, IGSharp_Vec2 size_max);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowContentSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowContentSize(IGSharp_Vec2 size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowScroll")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowScroll(IGSharp_Vec2 scroll);

    // --- Tables (extra) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSetBgColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableSetBgColor(int target, uint color, int column_n);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableGetColumnCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableGetColumnCount();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableGetColumnIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableGetColumnIndex();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableGetRowIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableGetRowIndex();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableGetColumnName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_TableGetColumnName(int column_n);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableGetColumnFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableGetColumnFlags(int column_n);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSetColumnEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableSetColumnEnabled(int column_n, [MarshalAs(UnmanagedType.U1)] bool v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableGetHoveredColumn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableGetHoveredColumn();

    // --- Demo / Debug Windows (extra) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ShowDebugLogWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ShowDebugLogWindow(bool* p_open);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ShowIDStackToolWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ShowIDStackToolWindow(bool* p_open);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ShowAboutWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ShowAboutWindow(bool* p_open);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ShowStyleEditor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ShowStyleEditor();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ShowStyleSelector")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ShowStyleSelector(ReadOnlySpan<byte> label);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ShowFontSelector")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ShowFontSelector(ReadOnlySpan<byte> label);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ShowUserGuide")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ShowUserGuide();

    // --- InputTextCallbackData: Resize Helpers ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetBuf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetBuf(void* data, byte* buf);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetBufSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetBufSize(void* data, int size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_ResizeBuf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_ResizeBuf(void* data, byte* new_buf, int new_buf_size);

    // --- Widgets: Scalar (generic typed) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragScalar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragScalar(ReadOnlySpan<byte> label, int data_type, void* p_data, float v_speed, void* p_min, void* p_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragScalarN")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragScalarN(ReadOnlySpan<byte> label, int data_type, void* p_data, int components, float v_speed, void* p_min, void* p_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderScalar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderScalar(ReadOnlySpan<byte> label, int data_type, void* p_data, void* p_min, void* p_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderScalarN")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderScalarN(ReadOnlySpan<byte> label, int data_type, void* p_data, int components, void* p_min, void* p_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_VSliderScalar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_VSliderScalar(ReadOnlySpan<byte> label, IGSharp_Vec2 size, int data_type, void* p_data, void* p_min, void* p_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputScalar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputScalar(ReadOnlySpan<byte> label, int data_type, void* p_data, void* p_step, void* p_step_fast, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputScalarN")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputScalarN(ReadOnlySpan<byte> label, int data_type, void* p_data, int components, void* p_step, void* p_step_fast, ReadOnlySpan<byte> format, int flags);

    // ============================================================
    // SDL3 Platform Backend & SDL_GPU Renderer Backend
    // ============================================================

    // --- SDL3 Platform Backend ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDL3_InitForSDLGPU")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ImplSDL3_InitForSDLGPU(SDL_Window* window);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDL3_Shutdown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDL3_Shutdown();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDL3_NewFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDL3_NewFrame();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDL3_ProcessEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ImplSDL3_ProcessEvent(SDL_Event* sdl_event);

    // --- SDL_GPU Renderer Backend ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDLGPU3_Init")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ImplSDLGPU3_Init(SDL_GPUDevice* device, int color_target_format, int msaa_samples);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDLGPU3_Shutdown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDLGPU3_Shutdown();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDLGPU3_NewFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDLGPU3_NewFrame();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDLGPU3_PrepareDrawData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDLGPU3_PrepareDrawData(void* draw_data, SDL_GPUCommandBuffer* command_buffer);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDLGPU3_RenderDrawData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDLGPU3_RenderDrawData(void* draw_data, SDL_GPUCommandBuffer* command_buffer, SDL_GPURenderPass* render_pass);
}
