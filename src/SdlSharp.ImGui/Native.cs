using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SdlSharp.Native;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace SdlSharp.ImGui;

/// <summary>
/// Native P/Invoke bindings for the imgui_sharp C wrapper library,
/// including the SDL3 platform backend and SDL_GPU renderer backend.
/// GENERATED from imgui_sharp.h by scripts/gen-imgui-native.py — edit the generator, not this file
/// (except the hand-written backend section at the bottom).
/// </summary>
internal static unsafe partial class Native
{
    private const string ImGuiLib = "imgui_sharp";

    // --- Opaque handle types (mirror the imgui_sharp.h forward declarations) ---

    public struct IGSharp_DrawCmd;
    public struct IGSharp_DrawData;
    public struct IGSharp_DrawList;
    public struct IGSharp_DrawListSharedData;
    public struct IGSharp_DrawListSplitter;
    public struct IGSharp_Font;
    public struct IGSharp_FontAtlas;
    public struct IGSharp_FontBaked;
    public struct IGSharp_FontConfig;
    public struct IGSharp_FontGlyph;
    public struct IGSharp_FontGlyphRangesBuilder;
    public struct IGSharp_FontLoader;
    public struct IGSharp_TextureData;
    public struct IGSharp_Context;
    public struct IGSharp_InputTextCallbackData;
    public struct IGSharp_ListClipper;
    public struct IGSharp_MultiSelectIO;
    public struct IGSharp_OnceUponAFrame;
    public struct IGSharp_Payload;
    public struct IGSharp_PlatformIO;
    public struct IGSharp_SelectionBasicStorage;
    public struct IGSharp_SelectionExternalStorage;
    public struct IGSharp_SelectionRequest;
    public struct IGSharp_SizeCallbackData;
    public struct IGSharp_Storage;
    public struct IGSharp_TableColumnSortSpecs;
    public struct IGSharp_TableSortSpecs;
    public struct IGSharp_TextBuffer;
    public struct IGSharp_TextFilter;
    public struct IGSharp_Viewport;

    // --- Value structs (layout-compatible mirrors; validated at startup via IGSharp_ValidateLayouts) ---

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

    /// <summary>Layout-compatible with IGSharp_KeyData (ImGuiKeyData).</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IGSharp_KeyData
    {
        public bool Down;
        public float DownDuration;
        public float DownDurationPrev;
        public float AnalogValue;
    }

    /// <summary>Layout-compatible with IGSharp_DrawVert (ImDrawVert).</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IGSharp_DrawVert
    {
        public IGSharp_Vec2 Pos;
        public IGSharp_Vec2 Uv;
        public uint Col;
    }

    /// <summary>Layout-compatible with IGSharp_FontAtlasRect (ImFontAtlasRect).</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IGSharp_FontAtlasRect
    {
        public ushort X, Y;
        public ushort W, H;
        public IGSharp_Vec2 Uv0, Uv1;
    }

    /// <summary>Layout-compatible with IGSharp_PlatformImeData (ImGuiPlatformImeData).</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IGSharp_PlatformImeData
    {
        public bool WantVisible;
        public bool WantTextInput;
        public IGSharp_Vec2 InputPos;
        public float InputLineHeight;
        public uint ViewportId;
    }

    [InlineArray(5)]
    public struct IGSharp_Vec2x5 { private IGSharp_Vec2 _element0; }

    [InlineArray(60)]  // IGSharp_Col_COUNT
    public struct IGSharp_Vec4x60 { private IGSharp_Vec4 _element0; }

    [InlineArray(155)] // IGSharp_Key_NamedKey_COUNT
    public struct IGSharp_KeyDataX155 { private IGSharp_KeyData _element0; }

    /// <summary>Layout-compatible with IGSharp_Style (ImGuiStyle). Access via IGSharp_GetStyle().</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IGSharp_Style
    {
        public float FontSizeBase;
        public float FontScaleMain;
        public float FontScaleDpi;
        public float Alpha;
        public float DisabledAlpha;
        public IGSharp_Vec2 WindowPadding;
        public float WindowRounding;
        public float WindowBorderSize;
        public float WindowBorderHoverPadding;
        public IGSharp_Vec2 WindowMinSize;
        public IGSharp_Vec2 WindowTitleAlign;
        public int WindowMenuButtonPosition;
        public float ChildRounding;
        public float ChildBorderSize;
        public float PopupRounding;
        public float PopupBorderSize;
        public IGSharp_Vec2 FramePadding;
        public float FrameRounding;
        public float FrameBorderSize;
        public IGSharp_Vec2 ItemSpacing;
        public IGSharp_Vec2 ItemInnerSpacing;
        public IGSharp_Vec2 CellPadding;
        public IGSharp_Vec2 TouchExtraPadding;
        public float IndentSpacing;
        public float ColumnsMinSpacing;
        public float ScrollbarSize;
        public float ScrollbarRounding;
        public float ScrollbarPadding;
        public float GrabMinSize;
        public float GrabRounding;
        public float LogSliderDeadzone;
        public float ImageRounding;
        public float ImageBorderSize;
        public float TabRounding;
        public float TabBorderSize;
        public float TabMinWidthBase;
        public float TabMinWidthShrink;
        public float TabCloseButtonMinWidthSelected;
        public float TabCloseButtonMinWidthUnselected;
        public float TabBarBorderSize;
        public float TabBarOverlineSize;
        public float TableAngledHeadersAngle;
        public IGSharp_Vec2 TableAngledHeadersTextAlign;
        public int TreeLinesFlags;
        public float TreeLinesSize;
        public float TreeLinesRounding;
        public float DragDropTargetRounding;
        public float DragDropTargetBorderSize;
        public float DragDropTargetPadding;
        public float ColorMarkerSize;
        public int ColorButtonPosition;
        public IGSharp_Vec2 ButtonTextAlign;
        public IGSharp_Vec2 SelectableTextAlign;
        public float SeparatorSize;
        public float SeparatorTextBorderSize;
        public IGSharp_Vec2 SeparatorTextAlign;
        public IGSharp_Vec2 SeparatorTextPadding;
        public IGSharp_Vec2 DisplayWindowPadding;
        public IGSharp_Vec2 DisplaySafeAreaPadding;
        public float MouseCursorScale;
        public bool AntiAliasedLines;
        public bool AntiAliasedLinesUseTex;
        public bool AntiAliasedFill;
        public float CurveTessellationTol;
        public float CircleTessellationMaxError;
        public IGSharp_Vec4x60 Colors;
        public float HoverStationaryDelay;
        public float HoverDelayShort;
        public float HoverDelayNormal;
        public int HoverFlagsForTooltipMouse;
        public int HoverFlagsForTooltipNav;
        // [Internal] maintained by Dear ImGui
        public float _MainScale;
        public float _NextFrameFontSizeBase;
    }

    /// <summary>Layout-compatible with IGSharp_IO (ImGuiIO). Access via IGSharp_GetIO().</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IGSharp_IO
    {
        public int ConfigFlags;
        public int BackendFlags;
        public IGSharp_Vec2 DisplaySize;
        public IGSharp_Vec2 DisplayFramebufferScale;
        public float DeltaTime;
        public float IniSavingRate;
        public byte* IniFilename;
        public byte* LogFilename;
        public void* UserData;
        public IGSharp_FontAtlas* Fonts;
        public IGSharp_Font* FontDefault;
        public bool FontAllowUserScaling;
        public bool ConfigNavSwapGamepadButtons;
        public bool ConfigNavMoveSetMousePos;
        public bool ConfigNavCaptureKeyboard;
        public bool ConfigNavEscapeClearFocusItem;
        public bool ConfigNavEscapeClearFocusWindow;
        public bool ConfigNavCursorVisibleAuto;
        public bool ConfigNavCursorVisibleAlways;
        public bool MouseDrawCursor;
        public bool ConfigMacOSXBehaviors;
        public bool ConfigInputTrickleEventQueue;
        public bool ConfigInputTextCursorBlink;
        public bool ConfigInputTextEnterKeepActive;
        public bool ConfigDragClickToInputText;
        public bool ConfigWindowsResizeFromEdges;
        public bool ConfigWindowsMoveFromTitleBarOnly;
        public bool ConfigWindowsCopyContentsWithCtrlC;
        public bool ConfigScrollbarScrollByPage;
        public float ConfigMemoryCompactTimer;
        public float MouseDoubleClickTime;
        public float MouseDoubleClickMaxDist;
        public float MouseDragThreshold;
        public float KeyRepeatDelay;
        public float KeyRepeatRate;
        public bool ConfigErrorRecovery;
        public bool ConfigErrorRecoveryEnableAssert;
        public bool ConfigErrorRecoveryEnableDebugLog;
        public bool ConfigErrorRecoveryEnableTooltip;
        public bool ConfigDebugIsDebuggerPresent;
        public bool ConfigDebugHighlightIdConflicts;
        public bool ConfigDebugHighlightIdConflictsShowItemPicker;
        public bool ConfigDebugBeginReturnValueOnce;
        public bool ConfigDebugBeginReturnValueLoop;
        public bool ConfigDebugIgnoreFocusLoss;
        public bool ConfigDebugIniSettings;
        public byte* BackendPlatformName;
        public byte* BackendRendererName;
        public void* BackendPlatformUserData;
        public void* BackendRendererUserData;
        public void* BackendLanguageUserData;
        public bool WantCaptureMouse;
        public bool WantCaptureKeyboard;
        public bool WantTextInput;
        public bool WantSetMousePos;
        public bool WantSaveIniSettings;
        public bool NavActive;
        public bool NavVisible;
        public float Framerate;
        public int MetricsRenderVertices;
        public int MetricsRenderIndices;
        public int MetricsRenderWindows;
        public int MetricsActiveWindows;
        public IGSharp_Vec2 MouseDelta;
        // [Internal] maintained by Dear ImGui
        public IGSharp_Context* Ctx;
        public IGSharp_Vec2 MousePos;
        public fixed bool MouseDown[5];
        public float MouseWheel;
        public float MouseWheelH;
        public int MouseSource;
        public bool KeyCtrl;
        public bool KeyShift;
        public bool KeyAlt;
        public bool KeySuper;
        public int KeyMods;
        public IGSharp_KeyDataX155 KeysData;
        public bool WantCaptureMouseUnlessPopupClose;
        public IGSharp_Vec2 MousePosPrev;
        public IGSharp_Vec2x5 MouseClickedPos;
        public fixed double MouseClickedTime[5];
        public fixed bool MouseClicked[5];
        public fixed bool MouseDoubleClicked[5];
        public fixed ushort MouseClickedCount[5];
        public fixed ushort MouseClickedLastCount[5];
        public fixed bool MouseReleased[5];
        public fixed double MouseReleasedTime[5];
        public fixed bool MouseDownOwned[5];
        public fixed bool MouseDownOwnedUnlessPopupClose[5];
        public bool MouseWheelRequestAxisSwap;
        public bool MouseCtrlLeftAsRightClick;
        public fixed float MouseDownDuration[5];
        public fixed float MouseDownDurationPrev[5];
        public fixed float MouseDragMaxDistanceSqr[5];
        public float PenPressure;
        public bool AppFocusLost;
        public bool AppAcceptingEvents;
        public ushort InputQueueSurrogate;
        public int InputQueueCharacters_Size;
        public int InputQueueCharacters_Capacity;
        public ushort* InputQueueCharacters_Data;
    }


    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CheckVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_CheckVersion();

    // --- Returns true when the caller's sizes for the mirrored structs match this library's ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ValidateLayouts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ValidateLayouts(nuint sizeof_io, nuint sizeof_style, nuint sizeof_key_data, nuint sizeof_platform_ime_data, nuint sizeof_draw_vert, nuint sizeof_font_atlas_rect);

    // ============ [SECTION] Forward declarations and basic types ============

    // ============ [SECTION] Texture identifiers (ImTextureID, ImTextureRef) ============

    // ============ [SECTION] Dear ImGui end-user API functions ============

    // --- Context creation and access ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CreateContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Context* IGSharp_CreateContext(IGSharp_FontAtlas* shared_font_atlas); // shared_font_atlas: ImFontAtlas* (NULL = context creates & owns its own)

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DestroyContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DestroyContext(IGSharp_Context* ctx);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetCurrentContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Context* IGSharp_GetCurrentContext();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetCurrentContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetCurrentContext(IGSharp_Context* ctx);

    // --- Main ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_IO* IGSharp_GetIO();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetPlatformIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_PlatformIO* IGSharp_GetPlatformIO();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetStyle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Style* IGSharp_GetStyle();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_NewFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_NewFrame();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndFrame();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Render")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Render();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetDrawData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_DrawData* IGSharp_GetDrawData();

    // --- Demo, Debug, Information ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ShowDemoWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ShowDemoWindow(bool* p_open);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ShowMetricsWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ShowMetricsWindow(bool* p_open);

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
    public static partial void IGSharp_ShowStyleEditor(IGSharp_Style* @ref); // ref: reference style to compare/revert/save to (NULL = default style)

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_GetVersion();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetVersionNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_GetVersionNumber();

    // --- Styles ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_StyleColorsDark")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_StyleColorsDark(IGSharp_Style* dst);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_StyleColorsLight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_StyleColorsLight(IGSharp_Style* dst);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_StyleColorsClassic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_StyleColorsClassic(IGSharp_Style* dst);

    // --- Windows ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Begin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Begin(ReadOnlySpan<byte> name, bool* p_open, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_End")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_End();

    // --- Child Windows ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginChild")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginChild(ReadOnlySpan<byte> str_id, IGSharp_Vec2 size, int child_flags, int window_flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginChildID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginChildID(uint id, IGSharp_Vec2 size, int child_flags, int window_flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndChild")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndChild();

    // --- Windows Utilities ---

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetWindowDrawList")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_DrawList* IGSharp_GetWindowDrawList();

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

    // --- Window manipulation ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowPos(IGSharp_Vec2 pos, int cond, IGSharp_Vec2 pivot);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowSize(IGSharp_Vec2 size, int cond);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowSizeConstraints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowSizeConstraints(IGSharp_Vec2 size_min, IGSharp_Vec2 size_max, delegate* unmanaged[Cdecl]<IGSharp_SizeCallbackData*, void> custom_callback, void* custom_callback_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowContentSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowContentSize(IGSharp_Vec2 size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowCollapsed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowCollapsed([MarshalAs(UnmanagedType.U1)] bool collapsed, int cond);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowFocus();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowScroll")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowScroll(IGSharp_Vec2 scroll);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextWindowBgAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextWindowBgAlpha(float alpha);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetWindowPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetWindowPos(IGSharp_Vec2 pos, int cond);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetWindowSize(IGSharp_Vec2 size, int cond);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetWindowCollapsed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetWindowCollapsed([MarshalAs(UnmanagedType.U1)] bool collapsed, int cond);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetWindowFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetWindowFocus();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetWindowPosNamed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetWindowPosNamed(ReadOnlySpan<byte> name, IGSharp_Vec2 pos, int cond);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetWindowSizeNamed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetWindowSizeNamed(ReadOnlySpan<byte> name, IGSharp_Vec2 size, int cond);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetWindowCollapsedNamed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetWindowCollapsedNamed(ReadOnlySpan<byte> name, [MarshalAs(UnmanagedType.U1)] bool collapsed, int cond);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetWindowFocusNamed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetWindowFocusNamed(ReadOnlySpan<byte> name);

    // --- Windows Scrolling ---

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

    // --- Parameters stacks (font) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushFont")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushFont(IGSharp_Font* font, float font_size_base_unscaled);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PopFont")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PopFont();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetFont")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Font* IGSharp_GetFont();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetFontSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetFontSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetFontBaked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_FontBaked* IGSharp_GetFontBaked();

    // --- Parameters stacks (shared) ---

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushStyleVarX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushStyleVarX(int idx, float val_x);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushStyleVarY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushStyleVarY(int idx, float val_y);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PopStyleVar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PopStyleVar(int count);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushItemFlag")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushItemFlag(int option, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PopItemFlag")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PopItemFlag();

    // --- Parameters stacks (current window) ---

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

    // --- Style read access ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetFontTexUvWhitePixel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetFontTexUvWhitePixel();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetColorU32")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_GetColorU32(int idx, float alpha_mul);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetColorU32Vec4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_GetColorU32Vec4(IGSharp_Vec4 col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetColorU32Packed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_GetColorU32Packed(uint col, float alpha_mul);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetStyleColorVec4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec4 IGSharp_GetStyleColorVec4(int idx);

    // --- Layout cursor positioning ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetCursorScreenPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetCursorScreenPos();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetCursorScreenPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetCursorScreenPos(IGSharp_Vec2 screen_pos);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetContentRegionAvail")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetContentRegionAvail();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetCursorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetCursorPos();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetCursorPosX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetCursorPosX();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetCursorPosY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetCursorPosY();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetCursorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetCursorPos(IGSharp_Vec2 local_pos);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetCursorPosX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetCursorPosX(float local_x);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetCursorPosY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetCursorPosY(float local_y);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetCursorStartPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetCursorStartPos();

    // --- Other layout functions ---

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

    // --- ID stack/scopes ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushIDStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushIDStr(ReadOnlySpan<byte> str_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushIDStrRange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushIDStrRange(ReadOnlySpan<byte> str_id_begin, ReadOnlySpan<byte> str_id_end);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushIDPtr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushIDPtr(void* ptr_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushIDInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushIDInt(int int_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PopID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PopID();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetIDStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_GetIDStr(ReadOnlySpan<byte> str_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetIDStrRange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_GetIDStrRange(ReadOnlySpan<byte> str_id_begin, ReadOnlySpan<byte> str_id_end);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetIDPtr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_GetIDPtr(void* ptr_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetIDInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_GetIDInt(int int_id);

    // --- Widgets: Text ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextUnformatted")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextUnformatted(ReadOnlySpan<byte> text, ReadOnlySpan<byte> text_end);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_LabelText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_LabelText(ReadOnlySpan<byte> label, ReadOnlySpan<byte> text);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BulletText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_BulletText(ReadOnlySpan<byte> text);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SeparatorText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SeparatorText(ReadOnlySpan<byte> label);

    // --- Widgets: Main ---

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Checkbox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Checkbox(ReadOnlySpan<byte> label, bool* v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CheckboxFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_CheckboxFlags(ReadOnlySpan<byte> label, int* flags, int flags_value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CheckboxFlagsUInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_CheckboxFlagsUInt(ReadOnlySpan<byte> label, uint* flags, uint flags_value);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Bullet")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Bullet();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextLink")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TextLink(ReadOnlySpan<byte> label);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextLinkOpenURL")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TextLinkOpenURL(ReadOnlySpan<byte> label, ReadOnlySpan<byte> url);

    // --- Widgets: Images ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Image")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Image(ulong tex_id, IGSharp_Vec2 image_size, IGSharp_Vec2 uv0, IGSharp_Vec2 uv1);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImageWithBg")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImageWithBg(ulong tex_id, IGSharp_Vec2 image_size, IGSharp_Vec2 uv0, IGSharp_Vec2 uv1, IGSharp_Vec4 bg_col, IGSharp_Vec4 tint_col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImageButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ImageButton(ReadOnlySpan<byte> str_id, ulong tex_id, IGSharp_Vec2 image_size, IGSharp_Vec2 uv0, IGSharp_Vec2 uv1, IGSharp_Vec4 bg_col, IGSharp_Vec4 tint_col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImageTextureData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImageTextureData(IGSharp_TextureData* tex_data, IGSharp_Vec2 image_size, IGSharp_Vec2 uv0, IGSharp_Vec2 uv1);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImageWithBgTextureData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImageWithBgTextureData(IGSharp_TextureData* tex_data, IGSharp_Vec2 image_size, IGSharp_Vec2 uv0, IGSharp_Vec2 uv1, IGSharp_Vec4 bg_col, IGSharp_Vec4 tint_col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImageButtonTextureData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ImageButtonTextureData(ReadOnlySpan<byte> str_id, IGSharp_TextureData* tex_data, IGSharp_Vec2 image_size, IGSharp_Vec2 uv0, IGSharp_Vec2 uv1, IGSharp_Vec4 bg_col, IGSharp_Vec4 tint_col);

    // --- Widgets: Combo Box (Dropdown) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginCombo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginCombo(ReadOnlySpan<byte> label, ReadOnlySpan<byte> preview_value, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndCombo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndCombo();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Combo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Combo(ReadOnlySpan<byte> label, int* current_item, byte** items, int items_count, int popup_max_height_in_items);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ComboStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ComboStr(ReadOnlySpan<byte> label, int* current_item, ReadOnlySpan<byte> items_separated_by_zeros, int popup_max_height_in_items);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ComboCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ComboCallback(ReadOnlySpan<byte> label, int* current_item, delegate* unmanaged[Cdecl]<void*, int, byte*> getter, void* user_data, int items_count, int popup_max_height_in_items);

    // --- Widgets: Drag Sliders ---

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragFloatRange2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragFloatRange2(ReadOnlySpan<byte> label, float* v_current_min, float* v_current_max, float v_speed, float v_min, float v_max, ReadOnlySpan<byte> format, ReadOnlySpan<byte> format_max, int flags);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragIntRange2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragIntRange2(ReadOnlySpan<byte> label, int* v_current_min, int* v_current_max, float v_speed, int v_min, int v_max, ReadOnlySpan<byte> format, ReadOnlySpan<byte> format_max, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragScalar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragScalar(ReadOnlySpan<byte> label, int data_type, void* p_data, float v_speed, void* p_min, void* p_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DragScalarN")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DragScalarN(ReadOnlySpan<byte> label, int data_type, void* p_data, int components, float v_speed, void* p_min, void* p_max, ReadOnlySpan<byte> format, int flags);

    // --- Widgets: Regular Sliders ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderFloat(ReadOnlySpan<byte> label, float* v, float v_min, float v_max, ReadOnlySpan<byte> format, int flags);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderAngle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderAngle(ReadOnlySpan<byte> label, float* v_rad, float v_degrees_min, float v_degrees_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderInt(ReadOnlySpan<byte> label, int* v, int v_min, int v_max, ReadOnlySpan<byte> format, int flags);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderScalar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderScalar(ReadOnlySpan<byte> label, int data_type, void* p_data, void* p_min, void* p_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SliderScalarN")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SliderScalarN(ReadOnlySpan<byte> label, int data_type, void* p_data, int components, void* p_min, void* p_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_VSliderFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_VSliderFloat(ReadOnlySpan<byte> label, IGSharp_Vec2 size, float* v, float v_min, float v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_VSliderInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_VSliderInt(ReadOnlySpan<byte> label, IGSharp_Vec2 size, int* v, int v_min, int v_max, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_VSliderScalar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_VSliderScalar(ReadOnlySpan<byte> label, IGSharp_Vec2 size, int data_type, void* p_data, void* p_min, void* p_max, ReadOnlySpan<byte> format, int flags);

    // --- Widgets: Input with Keyboard ---

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
    public static partial bool IGSharp_InputTextEx(ReadOnlySpan<byte> label, byte* buf, nuint buf_size, int flags, delegate* unmanaged[Cdecl]<IGSharp_InputTextCallbackData*, int> callback, void* user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextMultilineEx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextMultilineEx(ReadOnlySpan<byte> label, byte* buf, nuint buf_size, IGSharp_Vec2 size, int flags, delegate* unmanaged[Cdecl]<IGSharp_InputTextCallbackData*, int> callback, void* user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextWithHintEx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextWithHintEx(ReadOnlySpan<byte> label, ReadOnlySpan<byte> hint, byte* buf, nuint buf_size, int flags, delegate* unmanaged[Cdecl]<IGSharp_InputTextCallbackData*, int> callback, void* user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputFloat(ReadOnlySpan<byte> label, float* v, float step, float step_fast, ReadOnlySpan<byte> format, int flags);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputInt(ReadOnlySpan<byte> label, int* v, int step, int step_fast, int flags);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputScalar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputScalar(ReadOnlySpan<byte> label, int data_type, void* p_data, void* p_step, void* p_step_fast, ReadOnlySpan<byte> format, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputScalarN")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputScalarN(ReadOnlySpan<byte> label, int data_type, void* p_data, int components, void* p_step, void* p_step_fast, ReadOnlySpan<byte> format, int flags);

    // --- Widgets: Color Editor/Picker (tip: the ColorEdit* functions have a little color square that can be left-clicked to open a picker, and right-clicked to open an option menu.) ---

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetColorEditOptions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetColorEditOptions(int flags); // flags = ImGuiColorEditFlags

    // --- Widgets: Trees ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreeNode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TreeNode(ReadOnlySpan<byte> label);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreeNodeStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TreeNodeStr(ReadOnlySpan<byte> str_id, ReadOnlySpan<byte> text); // decorrelate id from displayed text

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreeNodePtr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TreeNodePtr(void* ptr_id, ReadOnlySpan<byte> text);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreeNodeEx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TreeNodeEx(ReadOnlySpan<byte> label, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreeNodeExStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TreeNodeExStr(ReadOnlySpan<byte> str_id, int flags, ReadOnlySpan<byte> text);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreeNodeExPtr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TreeNodeExPtr(void* ptr_id, int flags, ReadOnlySpan<byte> text);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreePushStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TreePushStr(ReadOnlySpan<byte> str_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreePushPtr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TreePushPtr(void* ptr_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreePop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TreePop();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetTreeNodeToLabelSpacing")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetTreeNodeToLabelSpacing();

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextItemStorageID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextItemStorageID(uint storage_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TreeNodeGetOpen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TreeNodeGetOpen(uint storage_id);

    // --- Widgets: Selectables ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Selectable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Selectable(ReadOnlySpan<byte> label, [MarshalAs(UnmanagedType.U1)] bool selected, int flags, IGSharp_Vec2 size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectablePtr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SelectablePtr(ReadOnlySpan<byte> label, bool* p_selected, int flags, IGSharp_Vec2 size);

    // --- Multi-selection system for Selectable(), Checkbox(), TreeNode() functions [BETA] ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginMultiSelect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_MultiSelectIO* IGSharp_BeginMultiSelect(int flags, int selection_size, int items_count);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndMultiSelect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_MultiSelectIO* IGSharp_EndMultiSelect();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextItemSelectionUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextItemSelectionUserData(long selection_user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemToggledSelection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemToggledSelection();

    // --- Widgets: List Boxes ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginListBox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginListBox(ReadOnlySpan<byte> label, IGSharp_Vec2 size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndListBox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndListBox();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListBox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ListBox(ReadOnlySpan<byte> label, int* current_item, byte** items, int items_count, int height_in_items);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListBoxCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ListBoxCallback(ReadOnlySpan<byte> label, int* current_item, delegate* unmanaged[Cdecl]<void*, int, byte*> getter, void* user_data, int items_count, int height_in_items);

    // --- Widgets: Data Plotting ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlotLines")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlotLines(ReadOnlySpan<byte> label, float* values, int values_count, int values_offset, ReadOnlySpan<byte> overlay_text, float scale_min, float scale_max, IGSharp_Vec2 graph_size, int stride);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlotLinesCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlotLinesCallback(ReadOnlySpan<byte> label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count, int values_offset, ReadOnlySpan<byte> overlay_text, float scale_min, float scale_max, IGSharp_Vec2 graph_size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlotHistogram")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlotHistogram(ReadOnlySpan<byte> label, float* values, int values_count, int values_offset, ReadOnlySpan<byte> overlay_text, float scale_min, float scale_max, IGSharp_Vec2 graph_size, int stride);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlotHistogramCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlotHistogramCallback(ReadOnlySpan<byte> label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count, int values_offset, ReadOnlySpan<byte> overlay_text, float scale_min, float scale_max, IGSharp_Vec2 graph_size);

    // --- Widgets: Value() Helpers. Output single value in "name: value" format ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ValueBool")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ValueBool(ReadOnlySpan<byte> prefix, [MarshalAs(UnmanagedType.U1)] bool b);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ValueInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ValueInt(ReadOnlySpan<byte> prefix, int v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ValueUInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ValueUInt(ReadOnlySpan<byte> prefix, uint v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ValueFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ValueFloat(ReadOnlySpan<byte> prefix, float v, ReadOnlySpan<byte> float_format);

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

    // --- Tooltips ---

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

    // --- Tooltips: helpers for showing a tooltip when hovering an item ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginItemTooltip")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginItemTooltip();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetItemTooltip")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetItemTooltip(ReadOnlySpan<byte> text);

    // --- Popups, Modals ---

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

    // --- Popups: open/close functions ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_OpenPopup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_OpenPopup(ReadOnlySpan<byte> str_id, int popup_flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_OpenPopupID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_OpenPopupID(uint id, int popup_flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_OpenPopupOnItemClick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_OpenPopupOnItemClick(ReadOnlySpan<byte> str_id, int popup_flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CloseCurrentPopup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_CloseCurrentPopup();

    // --- Popups: Open+Begin popup combined functions helpers to create context menus. ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginPopupContextItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginPopupContextItem(ReadOnlySpan<byte> str_id, int popup_flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginPopupContextWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginPopupContextWindow(ReadOnlySpan<byte> str_id, int popup_flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginPopupContextVoid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_BeginPopupContextVoid(ReadOnlySpan<byte> str_id, int popup_flags);

    // --- Popups: query functions ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsPopupOpen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsPopupOpen(ReadOnlySpan<byte> str_id, int flags);

    // --- Tables ---

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

    // --- Tables: Headers & Columns declaration ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSetupColumn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableSetupColumn(ReadOnlySpan<byte> label, int flags, float init_width_or_weight, uint user_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSetupScrollFreeze")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableSetupScrollFreeze(int cols, int rows);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableHeader")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableHeader(ReadOnlySpan<byte> label);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableHeadersRow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableHeadersRow();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableAngledHeadersRow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableAngledHeadersRow();

    // --- Tables: Sorting & Miscellaneous functions ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableGetSortSpecs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_TableSortSpecs* IGSharp_TableGetSortSpecs();

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSetBgColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableSetBgColor(int target, uint color, int column_n);

    // --- Legacy Columns API (prefer using Tables!) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Columns")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Columns(int count, ReadOnlySpan<byte> id, [MarshalAs(UnmanagedType.U1)] bool borders);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_NextColumn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_NextColumn();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetColumnIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_GetColumnIndex();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetColumnWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetColumnWidth(int column_index);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetColumnWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetColumnWidth(int column_index, float width);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetColumnOffset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_GetColumnOffset(int column_index);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetColumnOffset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetColumnOffset(int column_index, float offset_x);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetColumnsCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_GetColumnsCount();

    // --- Tab Bars, Tabs ---

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

    // --- Logging/Capture ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_LogToTTY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_LogToTTY(int auto_open_depth);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_LogToFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_LogToFile(int auto_open_depth, ReadOnlySpan<byte> filename);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_LogToClipboard")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_LogToClipboard(int auto_open_depth);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_LogFinish")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_LogFinish();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_LogButtons")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_LogButtons();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_LogText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_LogText(ReadOnlySpan<byte> text);

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
    public static partial IGSharp_Payload* IGSharp_AcceptDragDropPayload(ReadOnlySpan<byte> type, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndDragDropTarget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndDragDropTarget();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetDragDropPayload")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Payload* IGSharp_GetDragDropPayload();

    // --- Disabling [BETA API] ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_BeginDisabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_BeginDisabled([MarshalAs(UnmanagedType.U1)] bool disabled);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_EndDisabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_EndDisabled();

    // --- Clipping ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PushClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PushClipRect(IGSharp_Vec2 clip_rect_min, IGSharp_Vec2 clip_rect_max, [MarshalAs(UnmanagedType.U1)] bool intersect_with_current_clip_rect);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PopClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PopClipRect();

    // --- Focus, Activation ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetItemDefaultFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetItemDefaultFocus();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetKeyboardFocusHere")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetKeyboardFocusHere(int offset);

    // --- Keyboard/Gamepad Navigation ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNavCursorVisible")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNavCursorVisible([MarshalAs(UnmanagedType.U1)] bool visible);

    // --- Overlapping mode ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextItemAllowOverlap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextItemAllowOverlap();

    // --- Item/Widgets Utilities and Query Functions ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemHovered")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemHovered(int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemActive();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemFocused")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemFocused();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsItemClicked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsItemClicked(int mouse_button);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetItemID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_GetItemID();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetItemRectMin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetItemRectMin();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetItemRectMax")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetItemRectMax();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetItemRectSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetItemRectSize();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetItemFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_GetItemFlags(); // ImGuiItemFlags

    // --- Viewports ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetMainViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Viewport* IGSharp_GetMainViewport();

    // --- Background/Foreground Draw Lists ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetBackgroundDrawList")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_DrawList* IGSharp_GetBackgroundDrawList();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetForegroundDrawList")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_DrawList* IGSharp_GetForegroundDrawList();

    // --- Miscellaneous Utilities ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsRectVisible")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsRectVisible(IGSharp_Vec2 size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsRectVisibleRange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsRectVisibleRange(IGSharp_Vec2 rect_min, IGSharp_Vec2 rect_max);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double IGSharp_GetTime();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetFrameCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_GetFrameCount();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetDrawListSharedData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_DrawListSharedData* IGSharp_GetDrawListSharedData();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetStyleColorName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_GetStyleColorName(int idx);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetStateStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetStateStorage(IGSharp_Storage* storage);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetStateStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Storage* IGSharp_GetStateStorage();

    // --- Text Utilities ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_CalcTextSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_CalcTextSize(ReadOnlySpan<byte> text, ReadOnlySpan<byte> text_end, [MarshalAs(UnmanagedType.U1)] bool hide_text_after_double_hash, float wrap_width);

    // --- Color Utilities ---

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

    // --- Inputs Utilities: Raw Keyboard/Mouse/Gamepad Access ---

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetKeyPressedAmount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_GetKeyPressedAmount(int key, float repeat_delay, float rate);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetKeyName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_GetKeyName(int key);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextFrameWantCaptureKeyboard")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextFrameWantCaptureKeyboard([MarshalAs(UnmanagedType.U1)] bool want_capture_keyboard);

    // --- Inputs Utilities: Shortcut Testing & Routing ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Shortcut")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Shortcut(int key_chord, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextItemShortcut")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextItemShortcut(int key_chord, int flags);

    // --- Inputs Utilities: Key/Input Ownership ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetItemKeyOwner")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetItemKeyOwner(int key);

    // --- Inputs Utilities: Mouse ---

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsMouseReleasedWithDelay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsMouseReleasedWithDelay(int button, float delay);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetMouseClickedCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_GetMouseClickedCount(int button);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsMouseHoveringRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsMouseHoveringRect(IGSharp_Vec2 r_min, IGSharp_Vec2 r_max, [MarshalAs(UnmanagedType.U1)] bool clip);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsMousePosValid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsMousePosValid(IGSharp_Vec2* mouse_pos);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsAnyMouseDown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsAnyMouseDown();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetMousePos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetMousePos();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetMousePosOnOpeningCurrentPopup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetMousePosOnOpeningCurrentPopup();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IsMouseDragging")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_IsMouseDragging(int button, float lock_threshold);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetMouseDragDelta")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_GetMouseDragDelta(int button, float lock_threshold);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ResetMouseDragDelta")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ResetMouseDragDelta(int button);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetMouseCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_GetMouseCursor();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetMouseCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetMouseCursor(int cursor_type);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetNextFrameWantCaptureMouse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetNextFrameWantCaptureMouse([MarshalAs(UnmanagedType.U1)] bool want_capture_mouse);

    // --- Clipboard Utilities ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetClipboardText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_GetClipboardText();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetClipboardText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetClipboardText(ReadOnlySpan<byte> text);

    // --- Settings/.Ini Utilities ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_LoadIniSettingsFromDisk")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_LoadIniSettingsFromDisk(ReadOnlySpan<byte> ini_filename);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_LoadIniSettingsFromMemory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_LoadIniSettingsFromMemory(ReadOnlySpan<byte> ini_data, nuint ini_size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SaveIniSettingsToDisk")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SaveIniSettingsToDisk(ReadOnlySpan<byte> ini_filename);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SaveIniSettingsToMemory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_SaveIniSettingsToMemory(nuint* out_ini_size);

    // --- Debug Utilities ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DebugTextEncoding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DebugTextEncoding(ReadOnlySpan<byte> text);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DebugFlashStyleColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DebugFlashStyleColor(int idx); // ImGuiCol

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DebugStartItemPicker")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DebugStartItemPicker();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DebugCheckVersionAndDataLayout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DebugCheckVersionAndDataLayout(ReadOnlySpan<byte> version_str, nuint sz_io, nuint sz_style, nuint sz_vec2, nuint sz_vec4, nuint sz_drawvert, nuint sz_drawidx);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DebugLog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DebugLog(ReadOnlySpan<byte> text);

    // --- Memory Allocators (see IGSharp_MemAllocFunc/IGSharp_MemFreeFunc typedefs in the forward declarations section) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SetAllocatorFunctions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SetAllocatorFunctions(delegate* unmanaged[Cdecl]<nuint, void*, void*> alloc_func, delegate* unmanaged[Cdecl]<void*, void*, void> free_func, void* user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_GetAllocatorFunctions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_GetAllocatorFunctions(delegate* unmanaged[Cdecl]<nuint, void*, void*>* p_alloc_func, delegate* unmanaged[Cdecl]<void*, void*, void>* p_free_func, void** p_user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MemAlloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_MemAlloc(nuint size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MemFree")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_MemFree(void* ptr);

    // ============ [SECTION] Flags & Enumerations ============

    // ============ [SECTION] Tables API flags and structures (ImGuiTableFlags, ImGuiTableColumnFlags, ImGuiTableRowFlags, ImGuiTableBgTarget, ImGuiTableSortSpecs, ImGuiTableColumnSortSpecs) ============

    // --- ImGuiTableSortSpecs (opaque; obtain via IGSharp_TableGetSortSpecs()) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSortSpecs_GetSpecsCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableSortSpecs_GetSpecsCount(IGSharp_TableSortSpecs* specs);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSortSpecs_GetSpec")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_TableColumnSortSpecs* IGSharp_TableSortSpecs_GetSpec(IGSharp_TableSortSpecs* specs, int index);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSortSpecs_GetSpecsDirty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TableSortSpecs_GetSpecsDirty(IGSharp_TableSortSpecs* specs);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableSortSpecs_SetSpecsDirty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TableSortSpecs_SetSpecsDirty(IGSharp_TableSortSpecs* specs, [MarshalAs(UnmanagedType.U1)] bool v);

    // --- ImGuiTableColumnSortSpecs (opaque; obtain via IGSharp_TableSortSpecs_GetSpec()) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableColumnSortSpecs_GetColumnUserID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_TableColumnSortSpecs_GetColumnUserID(IGSharp_TableColumnSortSpecs* spec);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableColumnSortSpecs_GetColumnIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableColumnSortSpecs_GetColumnIndex(IGSharp_TableColumnSortSpecs* spec);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableColumnSortSpecs_GetSortOrder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableColumnSortSpecs_GetSortOrder(IGSharp_TableColumnSortSpecs* spec);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TableColumnSortSpecs_GetSortDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TableColumnSortSpecs_GetSortDirection(IGSharp_TableColumnSortSpecs* spec);

    // ============ [SECTION] Helpers: Debug log, memory allocations macros, ImVector<> ============

    // ============ [SECTION] ImGuiStyle ============

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Style_ScaleAllSizes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Style_ScaleAllSizes(IGSharp_Style* style, float scale);

    // ============ [SECTION] ImGuiIO ============

    // --- Wrap ImGuiIO's C++ member functions; pass the IGSharp_IO* you got from IGSharp_GetIO(). ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddKeyEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddKeyEvent(IGSharp_IO* io, int key, [MarshalAs(UnmanagedType.U1)] bool down);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddKeyAnalogEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddKeyAnalogEvent(IGSharp_IO* io, int key, [MarshalAs(UnmanagedType.U1)] bool down, float v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddMousePosEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddMousePosEvent(IGSharp_IO* io, float x, float y);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddMouseButtonEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddMouseButtonEvent(IGSharp_IO* io, int button, [MarshalAs(UnmanagedType.U1)] bool down);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddMouseWheelEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddMouseWheelEvent(IGSharp_IO* io, float wheel_x, float wheel_y);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddMouseSourceEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddMouseSourceEvent(IGSharp_IO* io, int source);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddFocusEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddFocusEvent(IGSharp_IO* io, [MarshalAs(UnmanagedType.U1)] bool focused);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddInputCharacter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddInputCharacter(IGSharp_IO* io, uint c);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddInputCharacterUTF16")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddInputCharacterUTF16(IGSharp_IO* io, ushort c);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_AddInputCharactersUTF8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_AddInputCharactersUTF8(IGSharp_IO* io, ReadOnlySpan<byte> str);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetKeyEventNativeData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetKeyEventNativeData(IGSharp_IO* io, int key, int native_keycode, int native_scancode, int native_legacy_index);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_SetAppAcceptingEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_SetAppAcceptingEvents(IGSharp_IO* io, [MarshalAs(UnmanagedType.U1)] bool accepting);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_ClearEventsQueue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_ClearEventsQueue(IGSharp_IO* io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_ClearInputKeys")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_ClearInputKeys(IGSharp_IO* io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_IO_ClearInputMouse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_IO_ClearInputMouse(IGSharp_IO* io);

    // ============ [SECTION] Misc data structures (ImGuiInputTextCallbackData, ImGuiSizeCallbackData, ImGuiPayload) ============

    // --- ImGuiInputTextCallbackData: Field Accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetCtx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Context* IGSharp_InputTextCallbackData_GetCtx(IGSharp_InputTextCallbackData* data); // ImGuiContext* (-> IGSharp_Context*; for IGSharp_SetCurrentContext in multi-context apps)

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetEventFlag")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetEventFlag(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetFlags(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_InputTextCallbackData_GetUserData(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_InputTextCallbackData_GetID(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetEventKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetEventKey(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetEventChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort IGSharp_InputTextCallbackData_GetEventChar(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetEventChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetEventChar(IGSharp_InputTextCallbackData* data, ushort c);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetEventActivated")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextCallbackData_GetEventActivated(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetBufDirty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextCallbackData_GetBufDirty(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetBufDirty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetBufDirty(IGSharp_InputTextCallbackData* data, [MarshalAs(UnmanagedType.U1)] bool v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetBuf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_InputTextCallbackData_GetBuf(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetBufTextLen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetBufTextLen(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetBufTextLen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetBufTextLen(IGSharp_InputTextCallbackData* data, int v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetBufSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetBufSize(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetCursorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetCursorPos(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetCursorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetCursorPos(IGSharp_InputTextCallbackData* data, int v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetSelectionStart")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetSelectionStart(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetSelectionStart")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetSelectionStart(IGSharp_InputTextCallbackData* data, int v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_GetSelectionEnd")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_InputTextCallbackData_GetSelectionEnd(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetSelectionEnd")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetSelectionEnd(IGSharp_InputTextCallbackData* data, int v);

    // --- ImGuiInputTextCallbackData: Helper Methods ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_DeleteChars")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_DeleteChars(IGSharp_InputTextCallbackData* data, int pos, int bytes_count);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_InsertChars")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_InsertChars(IGSharp_InputTextCallbackData* data, int pos, ReadOnlySpan<byte> text, ReadOnlySpan<byte> text_end);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SelectAll")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SelectAll(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetSelection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetSelection(IGSharp_InputTextCallbackData* data, int s, int e);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_ClearSelection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_ClearSelection(IGSharp_InputTextCallbackData* data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_HasSelection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_InputTextCallbackData_HasSelection(IGSharp_InputTextCallbackData* data);

    // --- ImGuiInputTextCallbackData: Resize Helpers ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetBuf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetBuf(IGSharp_InputTextCallbackData* data, byte* buf);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_SetBufSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_SetBufSize(IGSharp_InputTextCallbackData* data, int size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_InputTextCallbackData_ResizeBuf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_InputTextCallbackData_ResizeBuf(IGSharp_InputTextCallbackData* data, byte* new_buf, int new_buf_size);

    // --- ImGuiSizeCallbackData accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SizeCallbackData_GetUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_SizeCallbackData_GetUserData(IGSharp_SizeCallbackData* data); // Read-only.

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SizeCallbackData_GetPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_SizeCallbackData_GetPos(IGSharp_SizeCallbackData* data); // Read-only.

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SizeCallbackData_GetCurrentSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_SizeCallbackData_GetCurrentSize(IGSharp_SizeCallbackData* data); // Read-only.

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SizeCallbackData_GetDesiredSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_SizeCallbackData_GetDesiredSize(IGSharp_SizeCallbackData* data); // Read-write.

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SizeCallbackData_SetDesiredSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SizeCallbackData_SetDesiredSize(IGSharp_SizeCallbackData* data, IGSharp_Vec2 v);

    // --- ImGuiPayload accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Payload_GetData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_Payload_GetData(IGSharp_Payload* payload);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Payload_GetDataSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_Payload_GetDataSize(IGSharp_Payload* payload);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Payload_GetDataType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_Payload_GetDataType(IGSharp_Payload* payload);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Payload_IsDataType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Payload_IsDataType(IGSharp_Payload* payload, ReadOnlySpan<byte> type);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Payload_IsPreview")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Payload_IsPreview(IGSharp_Payload* payload);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Payload_IsDelivery")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Payload_IsDelivery(IGSharp_Payload* payload);

    // ============ [SECTION] Helpers (ImGuiOnceUponAFrame, ImGuiTextFilter, ImGuiTextBuffer, ImGuiStorage, ImGuiListClipper, Math Operators, ImColor) ============

    // --- ImGuiOnceUponAFrame ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_OnceUponAFrame_New")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_OnceUponAFrame* IGSharp_OnceUponAFrame_New();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_OnceUponAFrame_Delete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_OnceUponAFrame_Delete(IGSharp_OnceUponAFrame* oaf);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_OnceUponAFrame_Check")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_OnceUponAFrame_Check(IGSharp_OnceUponAFrame* oaf); // invokes operator bool(): true at most once per frame

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_OnceUponAFrame_GetRefFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_OnceUponAFrame_GetRefFrame(IGSharp_OnceUponAFrame* oaf);

    // --- ImGuiTextFilter ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextFilter_New")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_TextFilter* IGSharp_TextFilter_New(ReadOnlySpan<byte> default_filter);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextFilter_Delete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextFilter_Delete(IGSharp_TextFilter* filter);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextFilter_Draw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TextFilter_Draw(IGSharp_TextFilter* filter, ReadOnlySpan<byte> label, float width);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextFilter_PassFilter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TextFilter_PassFilter(IGSharp_TextFilter* filter, ReadOnlySpan<byte> text, ReadOnlySpan<byte> text_end);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextFilter_Build")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextFilter_Build(IGSharp_TextFilter* filter);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextFilter_Clear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextFilter_Clear(IGSharp_TextFilter* filter);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextFilter_IsActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TextFilter_IsActive(IGSharp_TextFilter* filter);

    // --- ImGuiTextBuffer (~string builder over ImVector<char>). Opaque handle. ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextBuffer_New")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_TextBuffer* IGSharp_TextBuffer_New();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextBuffer_Delete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextBuffer_Delete(IGSharp_TextBuffer* buf);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextBuffer_CStr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_TextBuffer_CStr(IGSharp_TextBuffer* buf);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextBuffer_Size")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TextBuffer_Size(IGSharp_TextBuffer* buf);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextBuffer_Empty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TextBuffer_Empty(IGSharp_TextBuffer* buf);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextBuffer_Clear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextBuffer_Clear(IGSharp_TextBuffer* buf);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextBuffer_Resize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextBuffer_Resize(IGSharp_TextBuffer* buf, int size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextBuffer_Reserve")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextBuffer_Reserve(IGSharp_TextBuffer* buf, int capacity);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextBuffer_Append")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextBuffer_Append(IGSharp_TextBuffer* buf, ReadOnlySpan<byte> str, ReadOnlySpan<byte> str_end);

    // --- ImGuiStorage (opaque handle; sorted key->value container, ref-returning methods) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_New")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Storage* IGSharp_Storage_New();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_Delete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Storage_Delete(IGSharp_Storage* storage);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_Clear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Storage_Clear(IGSharp_Storage* storage);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_GetInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_Storage_GetInt(IGSharp_Storage* storage, uint key, int default_val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_SetInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Storage_SetInt(IGSharp_Storage* storage, uint key, int val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_GetBool")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Storage_GetBool(IGSharp_Storage* storage, uint key, [MarshalAs(UnmanagedType.U1)] bool default_val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_SetBool")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Storage_SetBool(IGSharp_Storage* storage, uint key, [MarshalAs(UnmanagedType.U1)] bool val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_GetFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Storage_GetFloat(IGSharp_Storage* storage, uint key, float default_val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_SetFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Storage_SetFloat(IGSharp_Storage* storage, uint key, float val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_GetVoidPtr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_Storage_GetVoidPtr(IGSharp_Storage* storage, uint key);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_SetVoidPtr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Storage_SetVoidPtr(IGSharp_Storage* storage, uint key, void* val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_GetIntRef")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int* IGSharp_Storage_GetIntRef(IGSharp_Storage* storage, uint key, int default_val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_GetBoolRef")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial bool* IGSharp_Storage_GetBoolRef(IGSharp_Storage* storage, uint key, [MarshalAs(UnmanagedType.U1)] bool default_val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_GetFloatRef")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float* IGSharp_Storage_GetFloatRef(IGSharp_Storage* storage, uint key, float default_val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_GetVoidPtrRef")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void** IGSharp_Storage_GetVoidPtrRef(IGSharp_Storage* storage, uint key, void* default_val);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_BuildSortByKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Storage_BuildSortByKey(IGSharp_Storage* storage);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Storage_SetAllInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Storage_SetAllInt(IGSharp_Storage* storage, int val);

    // --- ImGuiListClipper ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_New")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_ListClipper* IGSharp_ListClipper_New();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_Delete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ListClipper_Delete(IGSharp_ListClipper* clipper);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_Begin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ListClipper_Begin(IGSharp_ListClipper* clipper, int items_count, float items_height);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_End")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ListClipper_End(IGSharp_ListClipper* clipper);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_Step")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_ListClipper_Step(IGSharp_ListClipper* clipper);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_IncludeItemsByIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ListClipper_IncludeItemsByIndex(IGSharp_ListClipper* clipper, int item_begin, int item_end);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_SeekCursorForItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ListClipper_SeekCursorForItem(IGSharp_ListClipper* clipper, int item_index);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_GetDisplayStart")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_ListClipper_GetDisplayStart(IGSharp_ListClipper* clipper);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_GetDisplayEnd")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_ListClipper_GetDisplayEnd(IGSharp_ListClipper* clipper);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_GetUserIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_ListClipper_GetUserIndex(IGSharp_ListClipper* clipper);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ListClipper_SetUserIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ListClipper_SetUserIndex(IGSharp_ListClipper* clipper, int user_index);

    // ============ [SECTION] Multi-Select API flags and structures (ImGuiMultiSelectFlags, ImGuiSelectionRequestType, ImGuiSelectionRequest, ImGuiMultiSelectIO, ImGuiSelectionBasicStorage) ============

    // --- ImGuiMultiSelectIO accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetRequestsCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_MultiSelectIO_GetRequestsCount(IGSharp_MultiSelectIO* io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetRequest")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_SelectionRequest* IGSharp_MultiSelectIO_GetRequest(IGSharp_MultiSelectIO* io, int index);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetRangeSrcItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long IGSharp_MultiSelectIO_GetRangeSrcItem(IGSharp_MultiSelectIO* io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetNavIdItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long IGSharp_MultiSelectIO_GetNavIdItem(IGSharp_MultiSelectIO* io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetNavIdSelected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_MultiSelectIO_GetNavIdSelected(IGSharp_MultiSelectIO* io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetRangeSrcReset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_MultiSelectIO_GetRangeSrcReset(IGSharp_MultiSelectIO* io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_SetRangeSrcReset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_MultiSelectIO_SetRangeSrcReset(IGSharp_MultiSelectIO* io, [MarshalAs(UnmanagedType.U1)] bool v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_MultiSelectIO_GetItemsCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_MultiSelectIO_GetItemsCount(IGSharp_MultiSelectIO* io);

    // --- ImGuiSelectionRequest accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionRequest_GetType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_SelectionRequest_GetType(IGSharp_SelectionRequest* request);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionRequest_GetSelected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SelectionRequest_GetSelected(IGSharp_SelectionRequest* request);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionRequest_GetRangeDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_SelectionRequest_GetRangeDirection(IGSharp_SelectionRequest* request);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionRequest_GetRangeFirstItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long IGSharp_SelectionRequest_GetRangeFirstItem(IGSharp_SelectionRequest* request);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionRequest_GetRangeLastItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long IGSharp_SelectionRequest_GetRangeLastItem(IGSharp_SelectionRequest* request);

    // --- ImGuiSelectionBasicStorage (opaque handle + accessors) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_Create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_SelectionBasicStorage* IGSharp_SelectionBasicStorage_Create();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_Destroy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SelectionBasicStorage_Destroy(IGSharp_SelectionBasicStorage* storage);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_ApplyRequests")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SelectionBasicStorage_ApplyRequests(IGSharp_SelectionBasicStorage* storage, IGSharp_MultiSelectIO* ms_io);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_Contains")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SelectionBasicStorage_Contains(IGSharp_SelectionBasicStorage* storage, uint id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_Clear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SelectionBasicStorage_Clear(IGSharp_SelectionBasicStorage* storage);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_SetItemSelected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SelectionBasicStorage_SetItemSelected(IGSharp_SelectionBasicStorage* storage, uint id, [MarshalAs(UnmanagedType.U1)] bool selected);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_GetNextSelectedItem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SelectionBasicStorage_GetNextSelectedItem(IGSharp_SelectionBasicStorage* storage, void** opaque_it, uint* out_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_GetStorageIdFromIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_SelectionBasicStorage_GetStorageIdFromIndex(IGSharp_SelectionBasicStorage* storage, int idx);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_GetSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_SelectionBasicStorage_GetSize(IGSharp_SelectionBasicStorage* storage);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_GetPreserveOrder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_SelectionBasicStorage_GetPreserveOrder(IGSharp_SelectionBasicStorage* storage);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_SetPreserveOrder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SelectionBasicStorage_SetPreserveOrder(IGSharp_SelectionBasicStorage* storage, [MarshalAs(UnmanagedType.U1)] bool v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_GetUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_SelectionBasicStorage_GetUserData(IGSharp_SelectionBasicStorage* storage);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_SetUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SelectionBasicStorage_SetUserData(IGSharp_SelectionBasicStorage* storage, void* v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionBasicStorage_SetAdapterIndexToStorageId")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SelectionBasicStorage_SetAdapterIndexToStorageId(IGSharp_SelectionBasicStorage* storage, delegate* unmanaged[Cdecl]<IGSharp_SelectionBasicStorage*, int, uint> adapter); // NULL restores the default (id == idx) adapter

    // --- ImGuiSelectionExternalStorage (opaque; helper to apply selection requests to your own storage) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionExternalStorage_Create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_SelectionExternalStorage* IGSharp_SelectionExternalStorage_Create();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionExternalStorage_Destroy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SelectionExternalStorage_Destroy(IGSharp_SelectionExternalStorage* storage);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionExternalStorage_GetUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_SelectionExternalStorage_GetUserData(IGSharp_SelectionExternalStorage* storage);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionExternalStorage_SetUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SelectionExternalStorage_SetUserData(IGSharp_SelectionExternalStorage* storage, void* user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionExternalStorage_SetAdapterSetItemSelected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SelectionExternalStorage_SetAdapterSetItemSelected(IGSharp_SelectionExternalStorage* storage, delegate* unmanaged[Cdecl]<IGSharp_SelectionExternalStorage*, int, byte, void> adapter);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_SelectionExternalStorage_ApplyRequests")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_SelectionExternalStorage_ApplyRequests(IGSharp_SelectionExternalStorage* storage, IGSharp_MultiSelectIO* ms_io); // ms_io = ImGuiMultiSelectIO*

    // ============ [SECTION] Drawing API (ImDrawCmd, ImDrawIdx, ImDrawVert, ImDrawChannel, ImDrawListSplitter, ImDrawListFlags, ImDrawList, ImDrawData) ============

    // --- ImDrawCmd: accessors (not mirrored: contains ImTextureRef and requires the GetTexID() getter) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawCmd_GetClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec4 IGSharp_DrawCmd_GetClipRect(IGSharp_DrawCmd* draw_cmd);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawCmd_GetTexID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong IGSharp_DrawCmd_GetTexID(IGSharp_DrawCmd* draw_cmd);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawCmd_GetVtxOffset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_DrawCmd_GetVtxOffset(IGSharp_DrawCmd* draw_cmd);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawCmd_GetIdxOffset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_DrawCmd_GetIdxOffset(IGSharp_DrawCmd* draw_cmd);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawCmd_GetElemCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_DrawCmd_GetElemCount(IGSharp_DrawCmd* draw_cmd);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawCmd_GetUserCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<IGSharp_DrawList*, IGSharp_DrawCmd*, void> IGSharp_DrawCmd_GetUserCallback(IGSharp_DrawCmd* draw_cmd);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawCmd_GetUserCallbackData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_DrawCmd_GetUserCallbackData(IGSharp_DrawCmd* draw_cmd);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawCmd_GetUserCallbackDataSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_DrawCmd_GetUserCallbackDataSize(IGSharp_DrawCmd* draw_cmd);

    // --- ImDrawListSplitter (accessors) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawListSplitter_Create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_DrawListSplitter* IGSharp_DrawListSplitter_Create();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawListSplitter_Destroy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawListSplitter_Destroy(IGSharp_DrawListSplitter* splitter);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawListSplitter_Clear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawListSplitter_Clear(IGSharp_DrawListSplitter* splitter);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawListSplitter_ClearFreeMemory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawListSplitter_ClearFreeMemory(IGSharp_DrawListSplitter* splitter);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawListSplitter_Split")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawListSplitter_Split(IGSharp_DrawListSplitter* splitter, IGSharp_DrawList* draw_list, int count);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawListSplitter_Merge")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawListSplitter_Merge(IGSharp_DrawListSplitter* splitter, IGSharp_DrawList* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawListSplitter_SetCurrentChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawListSplitter_SetCurrentChannel(IGSharp_DrawListSplitter* splitter, IGSharp_DrawList* draw_list, int channel_idx);

    // --- ImDrawList: Clipping ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PushClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PushClipRect(IGSharp_DrawList* draw_list, IGSharp_Vec2 clip_rect_min, IGSharp_Vec2 clip_rect_max, [MarshalAs(UnmanagedType.U1)] bool intersect_with_current);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PushClipRectFullScreen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PushClipRectFullScreen(IGSharp_DrawList* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PopClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PopClipRect(IGSharp_DrawList* draw_list);

    // --- ImDrawList: Texture state ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PushTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PushTexture(IGSharp_DrawList* draw_list, ulong tex_id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PushTextureData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PushTextureData(IGSharp_DrawList* draw_list, IGSharp_TextureData* tex_data); // ImTextureData* (preserves deferred resolution; NULL == no texture)

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PopTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PopTexture(IGSharp_DrawList* draw_list);

    // --- ImDrawList: Clip rect query ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_GetClipRectMin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_DrawList_GetClipRectMin(IGSharp_DrawList* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_GetClipRectMax")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_DrawList_GetClipRectMax(IGSharp_DrawList* draw_list);

    // --- ImDrawList: Primitives ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddLine(IGSharp_DrawList* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, uint col, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddRect(IGSharp_DrawList* draw_list, IGSharp_Vec2 p_min, IGSharp_Vec2 p_max, uint col, float rounding, int flags, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddRectFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddRectFilled(IGSharp_DrawList* draw_list, IGSharp_Vec2 p_min, IGSharp_Vec2 p_max, uint col, float rounding, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddRectFilledMultiColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddRectFilledMultiColor(IGSharp_DrawList* draw_list, IGSharp_Vec2 p_min, IGSharp_Vec2 p_max, uint col_ul, uint col_ur, uint col_br, uint col_bl);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddQuad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddQuad(IGSharp_DrawList* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, IGSharp_Vec2 p4, uint col, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddQuadFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddQuadFilled(IGSharp_DrawList* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, IGSharp_Vec2 p4, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddTriangle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddTriangle(IGSharp_DrawList* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, uint col, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddTriangleFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddTriangleFilled(IGSharp_DrawList* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddCircle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddCircle(IGSharp_DrawList* draw_list, IGSharp_Vec2 center, float radius, uint col, int num_segments, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddCircleFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddCircleFilled(IGSharp_DrawList* draw_list, IGSharp_Vec2 center, float radius, uint col, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddNgon")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddNgon(IGSharp_DrawList* draw_list, IGSharp_Vec2 center, float radius, uint col, int num_segments, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddNgonFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddNgonFilled(IGSharp_DrawList* draw_list, IGSharp_Vec2 center, float radius, uint col, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddEllipse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddEllipse(IGSharp_DrawList* draw_list, IGSharp_Vec2 center, IGSharp_Vec2 radius, uint col, float rot, int num_segments, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddEllipseFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddEllipseFilled(IGSharp_DrawList* draw_list, IGSharp_Vec2 center, IGSharp_Vec2 radius, uint col, float rot, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddText(IGSharp_DrawList* draw_list, IGSharp_Vec2 pos, uint col, ReadOnlySpan<byte> text_begin, ReadOnlySpan<byte> text_end);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddTextFont")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddTextFont(IGSharp_DrawList* draw_list, IGSharp_Font* font, float font_size, IGSharp_Vec2 pos, uint col, ReadOnlySpan<byte> text_begin, ReadOnlySpan<byte> text_end, float wrap_width, IGSharp_Vec4* cpu_fine_clip_rect);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddBezierCubic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddBezierCubic(IGSharp_DrawList* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, IGSharp_Vec2 p4, uint col, float thickness, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddBezierQuadratic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddBezierQuadratic(IGSharp_DrawList* draw_list, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, uint col, float thickness, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddPolyline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddPolyline(IGSharp_DrawList* draw_list, IGSharp_Vec2* points, int num_points, uint col, int flags, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddConvexPolyFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddConvexPolyFilled(IGSharp_DrawList* draw_list, IGSharp_Vec2* points, int num_points, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddConcavePolyFilled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddConcavePolyFilled(IGSharp_DrawList* draw_list, IGSharp_Vec2* points, int num_points, uint col);

    // --- ImDrawList: Images ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddImage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddImage(IGSharp_DrawList* draw_list, ulong tex_id, IGSharp_Vec2 p_min, IGSharp_Vec2 p_max, IGSharp_Vec2 uv_min, IGSharp_Vec2 uv_max, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddImageQuad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddImageQuad(IGSharp_DrawList* draw_list, ulong tex_id, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, IGSharp_Vec2 p4, IGSharp_Vec2 uv1, IGSharp_Vec2 uv2, IGSharp_Vec2 uv3, IGSharp_Vec2 uv4, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddImageRounded")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddImageRounded(IGSharp_DrawList* draw_list, ulong tex_id, IGSharp_Vec2 p_min, IGSharp_Vec2 p_max, IGSharp_Vec2 uv_min, IGSharp_Vec2 uv_max, uint col, float rounding, int flags);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddImageTextureData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddImageTextureData(IGSharp_DrawList* draw_list, IGSharp_TextureData* tex_data, IGSharp_Vec2 p_min, IGSharp_Vec2 p_max, IGSharp_Vec2 uv_min, IGSharp_Vec2 uv_max, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddImageQuadTextureData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddImageQuadTextureData(IGSharp_DrawList* draw_list, IGSharp_TextureData* tex_data, IGSharp_Vec2 p1, IGSharp_Vec2 p2, IGSharp_Vec2 p3, IGSharp_Vec2 p4, IGSharp_Vec2 uv1, IGSharp_Vec2 uv2, IGSharp_Vec2 uv3, IGSharp_Vec2 uv4, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddImageRoundedTextureData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddImageRoundedTextureData(IGSharp_DrawList* draw_list, IGSharp_TextureData* tex_data, IGSharp_Vec2 p_min, IGSharp_Vec2 p_max, IGSharp_Vec2 uv_min, IGSharp_Vec2 uv_max, uint col, float rounding, int flags);

    // --- ImDrawList: Path API ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathClear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathClear(IGSharp_DrawList* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathLineTo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathLineTo(IGSharp_DrawList* draw_list, IGSharp_Vec2 pos);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathLineToMergeDuplicate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathLineToMergeDuplicate(IGSharp_DrawList* draw_list, IGSharp_Vec2 pos);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathFillConvex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathFillConvex(IGSharp_DrawList* draw_list, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathFillConcave")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathFillConcave(IGSharp_DrawList* draw_list, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathStroke")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathStroke(IGSharp_DrawList* draw_list, uint col, int flags, float thickness);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathArcTo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathArcTo(IGSharp_DrawList* draw_list, IGSharp_Vec2 center, float radius, float a_min, float a_max, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathArcToFast")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathArcToFast(IGSharp_DrawList* draw_list, IGSharp_Vec2 center, float radius, int a_min_of_12, int a_max_of_12);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathEllipticalArcTo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathEllipticalArcTo(IGSharp_DrawList* draw_list, IGSharp_Vec2 center, IGSharp_Vec2 radius, float rot, float a_min, float a_max, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathBezierCubicCurveTo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathBezierCubicCurveTo(IGSharp_DrawList* draw_list, IGSharp_Vec2 p2, IGSharp_Vec2 p3, IGSharp_Vec2 p4, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathBezierQuadraticCurveTo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathBezierQuadraticCurveTo(IGSharp_DrawList* draw_list, IGSharp_Vec2 p2, IGSharp_Vec2 p3, int num_segments);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PathRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PathRect(IGSharp_DrawList* draw_list, IGSharp_Vec2 rect_min, IGSharp_Vec2 rect_max, float rounding, int flags);

    // --- ImDrawList: Advanced (callbacks, draw commands, cloning) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddCallback(IGSharp_DrawList* draw_list, delegate* unmanaged[Cdecl]<IGSharp_DrawList*, IGSharp_DrawCmd*, void> callback, void* userdata, nuint userdata_size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_AddDrawCmd")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_AddDrawCmd(IGSharp_DrawList* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_CloneOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_DrawList* IGSharp_DrawList_CloneOutput(IGSharp_DrawList* draw_list); // returns a heap ImDrawList*; free with IGSharp_DrawList_Destroy

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_Create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_DrawList* IGSharp_DrawList_Create(IGSharp_DrawListSharedData* shared_data); // ImDrawListSharedData*

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_ResetForNewFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_ResetForNewFrame(IGSharp_DrawList* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_Destroy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_Destroy(IGSharp_DrawList* draw_list);

    // --- ImDrawList: Channels splitting/merging ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_ChannelsSplit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_ChannelsSplit(IGSharp_DrawList* draw_list, int count);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_ChannelsMerge")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_ChannelsMerge(IGSharp_DrawList* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_ChannelsSetCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_ChannelsSetCurrent(IGSharp_DrawList* draw_list, int n);

    // --- ImDrawList: Advanced - Primitives allocations (for custom mesh generation) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PrimReserve")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PrimReserve(IGSharp_DrawList* draw_list, int idx_count, int vtx_count);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PrimUnreserve")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PrimUnreserve(IGSharp_DrawList* draw_list, int idx_count, int vtx_count);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PrimRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PrimRect(IGSharp_DrawList* draw_list, IGSharp_Vec2 a, IGSharp_Vec2 b, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PrimRectUV")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PrimRectUV(IGSharp_DrawList* draw_list, IGSharp_Vec2 a, IGSharp_Vec2 b, IGSharp_Vec2 uv_a, IGSharp_Vec2 uv_b, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PrimQuadUV")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PrimQuadUV(IGSharp_DrawList* draw_list, IGSharp_Vec2 a, IGSharp_Vec2 b, IGSharp_Vec2 c, IGSharp_Vec2 d, IGSharp_Vec2 uv_a, IGSharp_Vec2 uv_b, IGSharp_Vec2 uv_c, IGSharp_Vec2 uv_d, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PrimWriteVtx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PrimWriteVtx(IGSharp_DrawList* draw_list, IGSharp_Vec2 pos, IGSharp_Vec2 uv, uint col);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PrimWriteIdx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PrimWriteIdx(IGSharp_DrawList* draw_list, ushort idx);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_PrimVtx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_PrimVtx(IGSharp_DrawList* draw_list, IGSharp_Vec2 pos, IGSharp_Vec2 uv, uint col);

    // --- ImDrawList: Buffer / flag access (ImDrawList is not mirrored; required for a .NET renderer) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_GetFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_DrawList_GetFlags(IGSharp_DrawList* draw_list); // ImDrawListFlags

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_SetFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawList_SetFlags(IGSharp_DrawList* draw_list, int flags); // ImDrawListFlags

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_GetCmdBufferSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_DrawList_GetCmdBufferSize(IGSharp_DrawList* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_GetCmdBufferData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_DrawCmd* IGSharp_DrawList_GetCmdBufferData(IGSharp_DrawList* draw_list); // ImDrawCmd* (base; element stride is opaque — use IGSharp_DrawList_GetCmd to index)

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_GetCmd")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_DrawCmd* IGSharp_DrawList_GetCmd(IGSharp_DrawList* draw_list, int index); // ImDrawCmd* at index (feed to IGSharp_DrawCmd_* accessors)

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_GetIdxBufferSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_DrawList_GetIdxBufferSize(IGSharp_DrawList* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_GetIdxBufferData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort* IGSharp_DrawList_GetIdxBufferData(IGSharp_DrawList* draw_list); // ImDrawIdx* (unsigned short)

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_GetVtxBufferSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_DrawList_GetVtxBufferSize(IGSharp_DrawList* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawList_GetVtxBufferData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_DrawVert* IGSharp_DrawList_GetVtxBufferData(IGSharp_DrawList* draw_list); // ImDrawVert*

    // --- ImDrawData (accessors; obtain via IGSharp_GetDrawData()) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_GetValid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_DrawData_GetValid(IGSharp_DrawData* draw_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_GetCmdListsCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_DrawData_GetCmdListsCount(IGSharp_DrawData* draw_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_GetTotalIdxCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_DrawData_GetTotalIdxCount(IGSharp_DrawData* draw_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_GetTotalVtxCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_DrawData_GetTotalVtxCount(IGSharp_DrawData* draw_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_GetCmdList")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_DrawList* IGSharp_DrawData_GetCmdList(IGSharp_DrawData* draw_data, int index); // -> ImDrawList*

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_GetDisplayPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_DrawData_GetDisplayPos(IGSharp_DrawData* draw_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_GetDisplaySize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_DrawData_GetDisplaySize(IGSharp_DrawData* draw_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_GetFramebufferScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_DrawData_GetFramebufferScale(IGSharp_DrawData* draw_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_GetOwnerViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Viewport* IGSharp_DrawData_GetOwnerViewport(IGSharp_DrawData* draw_data); // -> ImGuiViewport*

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_GetTexturesCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_DrawData_GetTexturesCount(IGSharp_DrawData* draw_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_GetTexturesData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_TextureData** IGSharp_DrawData_GetTexturesData(IGSharp_DrawData* draw_data); // -> ImTextureData**

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_GetTextures")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_DrawData_GetTextures(IGSharp_DrawData* draw_data); // -> ImVector<ImTextureData*>* (may be NULL)

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_SetTextures")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawData_SetTextures(IGSharp_DrawData* draw_data, void* textures); // override or set NULL to manage textures yourself

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_Clear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawData_Clear(IGSharp_DrawData* draw_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_AddDrawList")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawData_AddDrawList(IGSharp_DrawData* draw_data, IGSharp_DrawList* draw_list);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_DeIndexAllBuffers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawData_DeIndexAllBuffers(IGSharp_DrawData* draw_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_DrawData_ScaleClipRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_DrawData_ScaleClipRects(IGSharp_DrawData* draw_data, IGSharp_Vec2 fb_scale);

    // ============ [SECTION] Texture API (ImTextureFormat, ImTextureStatus, ImTextureRect, ImTextureData) ============

    // --- ImTextureData accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetUniqueID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TextureData_GetUniqueID(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetStatus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TextureData_GetStatus(IGSharp_TextureData* tex_data); // ImTextureStatus

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetBackendUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_TextureData_GetBackendUserData(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_SetBackendUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextureData_SetBackendUserData(IGSharp_TextureData* tex_data, void* backend_user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetTexID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong IGSharp_TextureData_GetTexID(IGSharp_TextureData* tex_data); // ImTextureID

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TextureData_GetFormat(IGSharp_TextureData* tex_data); // ImTextureFormat

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TextureData_GetWidth(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetHeight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TextureData_GetHeight(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetBytesPerPixel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TextureData_GetBytesPerPixel(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_TextureData_GetPixels(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetUsedRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextureData_GetUsedRect(IGSharp_TextureData* tex_data, ushort* x, ushort* y, ushort* w, ushort* h);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetUpdateRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextureData_GetUpdateRect(IGSharp_TextureData* tex_data, ushort* x, ushort* y, ushort* w, ushort* h);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetUpdatesCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TextureData_GetUpdatesCount(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetUpdate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextureData_GetUpdate(IGSharp_TextureData* tex_data, int index, ushort* x, ushort* y, ushort* w, ushort* h);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetUnusedFrames")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TextureData_GetUnusedFrames(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetRefCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort IGSharp_TextureData_GetRefCount(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetUseColors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_TextureData_GetUseColors(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_Create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextureData_Create(IGSharp_TextureData* tex_data, int format, int w, int h); // format = ImTextureFormat

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_DestroyPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextureData_DestroyPixels(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetPixelsPtr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_TextureData_GetPixelsPtr(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetPixelsAt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_TextureData_GetPixelsAt(IGSharp_TextureData* tex_data, int x, int y);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetSizeInBytes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TextureData_GetSizeInBytes(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_GetPitch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_TextureData_GetPitch(IGSharp_TextureData* tex_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_SetTexID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextureData_SetTexID(IGSharp_TextureData* tex_data, ulong tex_id); // tex_id = ImTextureID

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_TextureData_SetStatus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_TextureData_SetStatus(IGSharp_TextureData* tex_data, int status); // status = ImTextureStatus

    // ============ [SECTION] Font API (ImFontConfig, ImFontGlyph, ImFontAtlasFlags, ImFontAtlas, ImFontGlyphRangesBuilder, ImFont) ============

    // --- ImFontConfig (accessors) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_Create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_FontConfig* IGSharp_FontConfig_Create();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_Destroy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_Destroy(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_FontConfig_GetName(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetName(IGSharp_FontConfig* cfg, ReadOnlySpan<byte> name);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetFontData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_FontConfig_GetFontData(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetFontData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetFontData(IGSharp_FontConfig* cfg, void* font_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetFontDataSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontConfig_GetFontDataSize(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetFontDataSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetFontDataSize(IGSharp_FontConfig* cfg, int font_data_size);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetFontDataOwnedByAtlas")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontConfig_GetFontDataOwnedByAtlas(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetFontDataOwnedByAtlas")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetFontDataOwnedByAtlas(IGSharp_FontConfig* cfg, [MarshalAs(UnmanagedType.U1)] bool @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetMergeMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontConfig_GetMergeMode(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetMergeMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetMergeMode(IGSharp_FontConfig* cfg, [MarshalAs(UnmanagedType.U1)] bool @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetPixelSnapH")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontConfig_GetPixelSnapH(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetPixelSnapH")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetPixelSnapH(IGSharp_FontConfig* cfg, [MarshalAs(UnmanagedType.U1)] bool @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetOversampleH")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontConfig_GetOversampleH(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetOversampleH")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetOversampleH(IGSharp_FontConfig* cfg, int @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetOversampleV")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontConfig_GetOversampleV(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetOversampleV")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetOversampleV(IGSharp_FontConfig* cfg, int @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetEllipsisChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort IGSharp_FontConfig_GetEllipsisChar(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetEllipsisChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetEllipsisChar(IGSharp_FontConfig* cfg, ushort @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetSizePixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontConfig_GetSizePixels(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetSizePixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetSizePixels(IGSharp_FontConfig* cfg, float @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetGlyphRanges")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort* IGSharp_FontConfig_GetGlyphRanges(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetGlyphRanges")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetGlyphRanges(IGSharp_FontConfig* cfg, ushort* ranges);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetGlyphExcludeRanges")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort* IGSharp_FontConfig_GetGlyphExcludeRanges(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetGlyphExcludeRanges")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetGlyphExcludeRanges(IGSharp_FontConfig* cfg, ushort* ranges);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetGlyphOffset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_FontConfig_GetGlyphOffset(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetGlyphOffset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetGlyphOffset(IGSharp_FontConfig* cfg, IGSharp_Vec2 @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetGlyphMinAdvanceX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontConfig_GetGlyphMinAdvanceX(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetGlyphMinAdvanceX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetGlyphMinAdvanceX(IGSharp_FontConfig* cfg, float @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetGlyphMaxAdvanceX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontConfig_GetGlyphMaxAdvanceX(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetGlyphMaxAdvanceX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetGlyphMaxAdvanceX(IGSharp_FontConfig* cfg, float @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetGlyphExtraAdvanceX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontConfig_GetGlyphExtraAdvanceX(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetGlyphExtraAdvanceX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetGlyphExtraAdvanceX(IGSharp_FontConfig* cfg, float @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetFontNo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_FontConfig_GetFontNo(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetFontNo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetFontNo(IGSharp_FontConfig* cfg, uint @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetFontLoaderFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_FontConfig_GetFontLoaderFlags(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetFontLoaderFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetFontLoaderFlags(IGSharp_FontConfig* cfg, uint @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetRasterizerMultiply")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontConfig_GetRasterizerMultiply(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetRasterizerMultiply")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetRasterizerMultiply(IGSharp_FontConfig* cfg, float @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetRasterizerDensity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontConfig_GetRasterizerDensity(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetRasterizerDensity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetRasterizerDensity(IGSharp_FontConfig* cfg, float @value);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_GetExtraSizeScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontConfig_GetExtraSizeScale(IGSharp_FontConfig* cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontConfig_SetExtraSizeScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontConfig_SetExtraSizeScale(IGSharp_FontConfig* cfg, float @value);

    // --- ImFontGlyph (accessors) ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetColored")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontGlyph_GetColored(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetVisible")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontGlyph_GetVisible(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetSourceIdx")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontGlyph_GetSourceIdx(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetCodepoint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_FontGlyph_GetCodepoint(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetAdvanceX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontGlyph_GetAdvanceX(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetX0")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontGlyph_GetX0(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetY0")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontGlyph_GetY0(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetX1")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontGlyph_GetX1(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetY1")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontGlyph_GetY1(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetU0")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontGlyph_GetU0(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetV0")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontGlyph_GetV0(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetU1")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontGlyph_GetU1(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetV1")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontGlyph_GetV1(IGSharp_FontGlyph* glyph);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyph_GetPackId")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontGlyph_GetPackId(IGSharp_FontGlyph* glyph); // ImFontAtlasRectId (-1 if none); pass to IGSharp_FontAtlas_GetCustomRect to refresh UVs

    // --- ImFontGlyphRangesBuilder ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyphRangesBuilder_New")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_FontGlyphRangesBuilder* IGSharp_FontGlyphRangesBuilder_New();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyphRangesBuilder_Delete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontGlyphRangesBuilder_Delete(IGSharp_FontGlyphRangesBuilder* builder);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyphRangesBuilder_Clear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontGlyphRangesBuilder_Clear(IGSharp_FontGlyphRangesBuilder* builder);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyphRangesBuilder_GetBit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontGlyphRangesBuilder_GetBit(IGSharp_FontGlyphRangesBuilder* builder, nuint n);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyphRangesBuilder_SetBit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontGlyphRangesBuilder_SetBit(IGSharp_FontGlyphRangesBuilder* builder, nuint n);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyphRangesBuilder_AddChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontGlyphRangesBuilder_AddChar(IGSharp_FontGlyphRangesBuilder* builder, ushort c);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyphRangesBuilder_AddText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontGlyphRangesBuilder_AddText(IGSharp_FontGlyphRangesBuilder* builder, ReadOnlySpan<byte> text, ReadOnlySpan<byte> text_end);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyphRangesBuilder_AddRanges")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontGlyphRangesBuilder_AddRanges(IGSharp_FontGlyphRangesBuilder* builder, ushort* ranges);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontGlyphRangesBuilder_BuildRanges")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontGlyphRangesBuilder_BuildRanges(IGSharp_FontGlyphRangesBuilder* builder, ushort* out_ranges, int out_ranges_capacity);

    // --- ImFontAtlas ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_Create")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_FontAtlas* IGSharp_FontAtlas_Create();

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_Destroy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_Destroy(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddFont")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Font* IGSharp_FontAtlas_AddFont(IGSharp_FontAtlas* atlas, IGSharp_FontConfig* font_cfg); // font_cfg: const ImFontConfig*

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddFontDefault")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Font* IGSharp_FontAtlas_AddFontDefault(IGSharp_FontAtlas* atlas, IGSharp_FontConfig* font_cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddFontDefaultVector")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Font* IGSharp_FontAtlas_AddFontDefaultVector(IGSharp_FontAtlas* atlas, IGSharp_FontConfig* font_cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddFontDefaultBitmap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Font* IGSharp_FontAtlas_AddFontDefaultBitmap(IGSharp_FontAtlas* atlas, IGSharp_FontConfig* font_cfg);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddFontFromFileTTF")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Font* IGSharp_FontAtlas_AddFontFromFileTTF(IGSharp_FontAtlas* atlas, ReadOnlySpan<byte> filename, float size_pixels, IGSharp_FontConfig* font_cfg, ushort* glyph_ranges);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddFontFromMemoryTTF")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Font* IGSharp_FontAtlas_AddFontFromMemoryTTF(IGSharp_FontAtlas* atlas, void* font_data, int font_data_size, float size_pixels, IGSharp_FontConfig* font_cfg, ushort* glyph_ranges); // NOTE: deviates from upstream default — when font_cfg is NULL the atlas does NOT take ownership of font_data (caller keeps/frees it). To transfer ownership, pass a config with IGSharp_FontConfig_SetFontDataOwnedByAtlas(cfg, true) and allocate font_data with IGSharp_MemAlloc.

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddFontFromMemoryCompressedTTF")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Font* IGSharp_FontAtlas_AddFontFromMemoryCompressedTTF(IGSharp_FontAtlas* atlas, void* compressed_data, int compressed_size, float size_pixels, IGSharp_FontConfig* font_cfg, ushort* glyph_ranges);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddFontFromMemoryCompressedBase85TTF")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Font* IGSharp_FontAtlas_AddFontFromMemoryCompressedBase85TTF(IGSharp_FontAtlas* atlas, ReadOnlySpan<byte> compressed_data_base85, float size_pixels, IGSharp_FontConfig* font_cfg, ushort* glyph_ranges);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_RemoveFont")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_RemoveFont(IGSharp_FontAtlas* atlas, IGSharp_Font* font);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_Clear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_Clear(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_CompactCache")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_CompactCache(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_SetFontLoader")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_SetFontLoader(IGSharp_FontAtlas* atlas, IGSharp_FontLoader* font_loader); // font_loader: const ImFontLoader*

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_ClearInputData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_ClearInputData(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_ClearFonts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_ClearFonts(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_ClearTexData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_ClearTexData(IGSharp_FontAtlas* atlas);

    // --- --- Glyph Ranges --- ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetGlyphRangesDefault")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort* IGSharp_FontAtlas_GetGlyphRangesDefault(IGSharp_FontAtlas* atlas); // const ImWchar*

    // --- --- Fonts: Custom rectangles (ImFontAtlasRectId is int; -1 == invalid) --- ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_AddCustomRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontAtlas_AddCustomRect(IGSharp_FontAtlas* atlas, int width, int height, IGSharp_FontAtlasRect* out_r);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_RemoveCustomRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_RemoveCustomRect(IGSharp_FontAtlas* atlas, int id);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetCustomRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontAtlas_GetCustomRect(IGSharp_FontAtlas* atlas, int id, IGSharp_FontAtlasRect* out_r);

    // --- ImFontAtlas member field accessors ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontAtlas_GetFlags(IGSharp_FontAtlas* atlas); // ImFontAtlasFlags

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_SetFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_SetFlags(IGSharp_FontAtlas* atlas, int flags); // ImFontAtlasFlags

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetTexDesiredFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontAtlas_GetTexDesiredFormat(IGSharp_FontAtlas* atlas); // ImTextureFormat

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_SetTexDesiredFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_SetTexDesiredFormat(IGSharp_FontAtlas* atlas, int format); // ImTextureFormat

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetTexGlyphPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontAtlas_GetTexGlyphPadding(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_SetTexGlyphPadding")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_SetTexGlyphPadding(IGSharp_FontAtlas* atlas, int padding);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetTexMinWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontAtlas_GetTexMinWidth(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_SetTexMinWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_SetTexMinWidth(IGSharp_FontAtlas* atlas, int width);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetTexMinHeight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontAtlas_GetTexMinHeight(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_SetTexMinHeight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_SetTexMinHeight(IGSharp_FontAtlas* atlas, int height);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetTexMaxWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontAtlas_GetTexMaxWidth(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_SetTexMaxWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_SetTexMaxWidth(IGSharp_FontAtlas* atlas, int width);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetTexMaxHeight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontAtlas_GetTexMaxHeight(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_SetTexMaxHeight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_SetTexMaxHeight(IGSharp_FontAtlas* atlas, int height);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_FontAtlas_GetUserData(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_SetUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_SetUserData(IGSharp_FontAtlas* atlas, void* user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetTexID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong IGSharp_FontAtlas_GetTexID(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetTexData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_TextureData* IGSharp_FontAtlas_GetTexData(IGSharp_FontAtlas* atlas); // ImTextureData* (preserves deferred resolution)

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetTexPixelsUseColors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontAtlas_GetTexPixelsUseColors(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_SetTexPixelsUseColors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_SetTexPixelsUseColors(IGSharp_FontAtlas* atlas, [MarshalAs(UnmanagedType.U1)] bool v); // set true when rendering colored output into custom rects

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetTexUvScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_FontAtlas_GetTexUvScale(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetTexUvWhitePixel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_FontAtlas_GetTexUvWhitePixel(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetFontCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontAtlas_GetFontCount(IGSharp_FontAtlas* atlas); // == Fonts.Size

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetFont")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Font* IGSharp_FontAtlas_GetFont(IGSharp_FontAtlas* atlas, int index); // -> ImFont* (Fonts[index])

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetTexIsBuilt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontAtlas_GetTexIsBuilt(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetLocked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontAtlas_GetLocked(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetRendererHasTextures")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontAtlas_GetRendererHasTextures(IGSharp_FontAtlas* atlas);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetFontLoaderName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_FontAtlas_GetFontLoaderName(IGSharp_FontAtlas* atlas); // == FontLoader->Name

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_GetFontLoaderFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_FontAtlas_GetFontLoaderFlags(IGSharp_FontAtlas* atlas); // shared font-loader flags (e.g. FreeType) for all fonts

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontAtlas_SetFontLoaderFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontAtlas_SetFontLoaderFlags(IGSharp_FontAtlas* atlas, uint flags);

    // --- ImFontBaked accessors & methods ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontBaked_FindGlyph")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_FontGlyph* IGSharp_FontBaked_FindGlyph(IGSharp_FontBaked* baked, ushort c); // ImWchar; returns ImFontGlyph* (U+FFFD fallback glyph if missing)

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontBaked_FindGlyphNoFallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_FontGlyph* IGSharp_FontBaked_FindGlyphNoFallback(IGSharp_FontBaked* baked, ushort c); // ImWchar; returns ImFontGlyph*, NULL if glyph doesn't exist

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontBaked_GetCharAdvance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontBaked_GetCharAdvance(IGSharp_FontBaked* baked, ushort c); // ImWchar

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontBaked_IsGlyphLoaded")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_FontBaked_IsGlyphLoaded(IGSharp_FontBaked* baked, ushort c); // ImWchar

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontBaked_GetSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontBaked_GetSize(IGSharp_FontBaked* baked);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontBaked_GetAscent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontBaked_GetAscent(IGSharp_FontBaked* baked);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontBaked_GetDescent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontBaked_GetDescent(IGSharp_FontBaked* baked);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontBaked_GetFallbackAdvanceX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontBaked_GetFallbackAdvanceX(IGSharp_FontBaked* baked);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontBaked_GetRasterizerDensity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_FontBaked_GetRasterizerDensity(IGSharp_FontBaked* baked); // density this baked size is baked at

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontBaked_GetGlyphsCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_FontBaked_GetGlyphsCount(IGSharp_FontBaked* baked); // enumerate all baked glyphs (Glyphs[])

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontBaked_GetGlyph")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_FontGlyph* IGSharp_FontBaked_GetGlyph(IGSharp_FontBaked* baked, int index); // ImFontGlyph* at index

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_FontBaked_ClearOutputData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_FontBaked_ClearOutputData(IGSharp_FontBaked* baked); // [Internal] Don't use unless you know what you're doing

    // --- ImFont accessors & methods ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_IsGlyphInFont")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Font_IsGlyphInFont(IGSharp_Font* font, ushort c);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_IsLoaded")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Font_IsLoaded(IGSharp_Font* font);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_GetDebugName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_Font_GetDebugName(IGSharp_Font* font);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_GetFontBaked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_FontBaked* IGSharp_Font_GetFontBaked(IGSharp_Font* font, float font_size, float density); // returns ImFontBaked*

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_CalcTextSizeA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Font_CalcTextSizeA(IGSharp_Font* font, float size, float max_width, float wrap_width, ReadOnlySpan<byte> text_begin, ReadOnlySpan<byte> text_end, byte** out_remaining);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_CalcWordWrapPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* IGSharp_Font_CalcWordWrapPosition(IGSharp_Font* font, float size, ReadOnlySpan<byte> text, ReadOnlySpan<byte> text_end, float wrap_width);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_RenderChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Font_RenderChar(IGSharp_Font* font, IGSharp_DrawList* draw_list, float size, IGSharp_Vec2 pos, uint col, ushort c, IGSharp_Vec4* cpu_fine_clip);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_RenderText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Font_RenderText(IGSharp_Font* font, IGSharp_DrawList* draw_list, float size, IGSharp_Vec2 pos, uint col, IGSharp_Vec4 clip_rect, ReadOnlySpan<byte> text_begin, ReadOnlySpan<byte> text_end, float wrap_width, int flags); // flags: ImDrawTextFlags

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_AddRemapChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Font_AddRemapChar(IGSharp_Font* font, ushort from_codepoint, ushort to_codepoint);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_IsGlyphRangeUnused")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Font_IsGlyphRangeUnused(IGSharp_Font* font, uint c_begin, uint c_last);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_ClearOutputData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Font_ClearOutputData(IGSharp_Font* font); // [Internal] Don't use unless you know what you're doing

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_GetOwnerAtlas")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_FontAtlas* IGSharp_Font_GetOwnerAtlas(IGSharp_Font* font); // ImFontAtlas* this font was loaded into

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_GetFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_Font_GetFlags(IGSharp_Font* font); // ImFontFlags

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_SetFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Font_SetFlags(IGSharp_Font* font, int flags); // ImFontFlags

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_GetFallbackChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort IGSharp_Font_GetFallbackChar(IGSharp_Font* font);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_SetFallbackChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Font_SetFallbackChar(IGSharp_Font* font, ushort c);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_GetEllipsisChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort IGSharp_Font_GetEllipsisChar(IGSharp_Font* font);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_SetEllipsisChar")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Font_SetEllipsisChar(IGSharp_Font* font, ushort c);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_GetEllipsisAutoBake")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool IGSharp_Font_GetEllipsisAutoBake(IGSharp_Font* font);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_SetEllipsisAutoBake")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Font_SetEllipsisAutoBake(IGSharp_Font* font, [MarshalAs(UnmanagedType.U1)] bool v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_GetLegacySize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float IGSharp_Font_GetLegacySize(IGSharp_Font* font);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Font_SetLegacySize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Font_SetLegacySize(IGSharp_Font* font, float size);

    // ============ [SECTION] Viewports ============

    // --- ImGuiViewport ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint IGSharp_Viewport_GetID(IGSharp_Viewport* viewport);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_Viewport_GetFlags(IGSharp_Viewport* viewport); // ImGuiViewportFlags

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Viewport_GetPos(IGSharp_Viewport* viewport);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Viewport_GetSize(IGSharp_Viewport* viewport);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetFramebufferScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Viewport_GetFramebufferScale(IGSharp_Viewport* viewport);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetWorkPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Viewport_GetWorkPos(IGSharp_Viewport* viewport);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetWorkSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Viewport_GetWorkSize(IGSharp_Viewport* viewport);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetPlatformHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_Viewport_GetPlatformHandle(IGSharp_Viewport* viewport);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_SetPlatformHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Viewport_SetPlatformHandle(IGSharp_Viewport* viewport, void* handle);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetPlatformHandleRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_Viewport_GetPlatformHandleRaw(IGSharp_Viewport* viewport);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_SetPlatformHandleRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_Viewport_SetPlatformHandleRaw(IGSharp_Viewport* viewport, void* handle);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetCenter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Viewport_GetCenter(IGSharp_Viewport* viewport);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_Viewport_GetWorkCenter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_Vec2 IGSharp_Viewport_GetWorkCenter(IGSharp_Viewport* viewport);

    // ============ [SECTION] Platform Dependent Interfaces ============

    // --- Override setters for the platform handler function pointers, each followed by the ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_SetPlatformGetClipboardTextFn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_SetPlatformGetClipboardTextFn(IGSharp_PlatformIO* pio, delegate* unmanaged[Cdecl]<IGSharp_Context*, byte*> fn);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_SetPlatformSetClipboardTextFn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_SetPlatformSetClipboardTextFn(IGSharp_PlatformIO* pio, delegate* unmanaged[Cdecl]<IGSharp_Context*, byte*, void> fn);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_GetPlatformClipboardUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_PlatformIO_GetPlatformClipboardUserData(IGSharp_PlatformIO* pio);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_SetPlatformClipboardUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_SetPlatformClipboardUserData(IGSharp_PlatformIO* pio, void* user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_SetPlatformOpenInShellFn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_SetPlatformOpenInShellFn(IGSharp_PlatformIO* pio, delegate* unmanaged[Cdecl]<IGSharp_Context*, byte*, byte> fn);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_GetPlatformOpenInShellUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_PlatformIO_GetPlatformOpenInShellUserData(IGSharp_PlatformIO* pio);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_SetPlatformOpenInShellUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_SetPlatformOpenInShellUserData(IGSharp_PlatformIO* pio, void* user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_SetPlatformSetImeDataFn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_SetPlatformSetImeDataFn(IGSharp_PlatformIO* pio, delegate* unmanaged[Cdecl]<IGSharp_Context*, IGSharp_Viewport*, IGSharp_PlatformImeData*, void> fn);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_GetPlatformImeUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_PlatformIO_GetPlatformImeUserData(IGSharp_PlatformIO* pio);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_SetPlatformImeUserData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_SetPlatformImeUserData(IGSharp_PlatformIO* pio, void* user_data);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_GetPlatformLocaleDecimalPoint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ushort IGSharp_PlatformIO_GetPlatformLocaleDecimalPoint(IGSharp_PlatformIO* pio); // ImWchar

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_SetPlatformLocaleDecimalPoint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_SetPlatformLocaleDecimalPoint(IGSharp_PlatformIO* pio, ushort c); // ImWchar

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_GetRendererTextureMaxWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_PlatformIO_GetRendererTextureMaxWidth(IGSharp_PlatformIO* pio);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_SetRendererTextureMaxWidth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_SetRendererTextureMaxWidth(IGSharp_PlatformIO* pio, int v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_GetRendererTextureMaxHeight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_PlatformIO_GetRendererTextureMaxHeight(IGSharp_PlatformIO* pio);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_SetRendererTextureMaxHeight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_SetRendererTextureMaxHeight(IGSharp_PlatformIO* pio, int v);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_GetRendererRenderState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* IGSharp_PlatformIO_GetRendererRenderState(IGSharp_PlatformIO* pio);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_SetRendererRenderState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_SetRendererRenderState(IGSharp_PlatformIO* pio, void* render_state);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_GetTexturesCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int IGSharp_PlatformIO_GetTexturesCount(IGSharp_PlatformIO* pio);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_GetTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IGSharp_TextureData* IGSharp_PlatformIO_GetTexture(IGSharp_PlatformIO* pio, int index);

    // --- Member functions. ---

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_ClearPlatformHandlers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_ClearPlatformHandlers(IGSharp_PlatformIO* pio);

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_PlatformIO_ClearRendererHandlers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_PlatformIO_ClearRendererHandlers(IGSharp_PlatformIO* pio);

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

    [LibraryImport(ImGuiLib, EntryPoint = "IGSharp_ImplSDLGPU3_RenderDrawDataWithPipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IGSharp_ImplSDLGPU3_RenderDrawDataWithPipeline(void* draw_data, SDL_GPUCommandBuffer* command_buffer, SDL_GPURenderPass* render_pass, SDL_GPUGraphicsPipeline* pipeline);
}
