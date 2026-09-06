#!/usr/bin/env python3
"""Generate SdlSharp.ImGui/Native.cs from imgui_sharp.h.
Preserves the hand-written SDL3 backend section from the existing file."""
import re, sys

HDR = '/Users/paulv/Projects/imgui-sharp-native/src/imgui_sharp.h'
OLD = '/Users/paulv/Projects/sdl-sharp/src/SdlSharp.ImGui/Native.cs'
OUT = '/Users/paulv/Projects/sdl-sharp/src/SdlSharp.ImGui/Native.cs'

# ---- opaque handle names (from the header's consumer branch) ----
h = open(HDR).read()
consumer = re.search(r'#else\n((?:typedef struct IGSharp_\w+\s+IGSharp_\w+;\n)+)#endif', h)
HANDLES = re.findall(r'typedef struct (IGSharp_\w+)', consumer.group(1))
assert len(HANDLES) == 30, HANDLES

MIRRORS = {'IGSharp_IO', 'IGSharp_Style', 'IGSharp_KeyData', 'IGSharp_PlatformImeData',
           'IGSharp_DrawVert', 'IGSharp_FontAtlasRect', 'IGSharp_Vec2', 'IGSharp_Vec4'}

CALLBACKS = {
    'IGSharp_InputTextCallback': 'delegate* unmanaged[Cdecl]<IGSharp_InputTextCallbackData*, int>',
    'IGSharp_SizeCallback': 'delegate* unmanaged[Cdecl]<IGSharp_SizeCallbackData*, void>',
    'IGSharp_MemAllocFunc': 'delegate* unmanaged[Cdecl]<nuint, void*, void*>',
    'IGSharp_MemFreeFunc': 'delegate* unmanaged[Cdecl]<void*, void*, void>',
    'IGSharp_DrawCallback': 'delegate* unmanaged[Cdecl]<IGSharp_DrawList*, IGSharp_DrawCmd*, void>',
    'IGSharp_SelectionBasicStorageAdapter': 'delegate* unmanaged[Cdecl]<IGSharp_SelectionBasicStorage*, int, uint>',
    'IGSharp_SelectionExternalStorageAdapter': 'delegate* unmanaged[Cdecl]<IGSharp_SelectionExternalStorage*, int, byte, void>',
    'IGSharp_Platform_GetClipboardTextFn': 'delegate* unmanaged[Cdecl]<IGSharp_Context*, byte*>',
    'IGSharp_Platform_SetClipboardTextFn': 'delegate* unmanaged[Cdecl]<IGSharp_Context*, byte*, void>',
    'IGSharp_Platform_OpenInShellFn': 'delegate* unmanaged[Cdecl]<IGSharp_Context*, byte*, byte>',
    'IGSharp_Platform_SetImeDataFn': 'delegate* unmanaged[Cdecl]<IGSharp_Context*, IGSharp_Viewport*, IGSharp_PlatformImeData*, void>',
}

CS_KEYWORDS = {'ref','out','in','params','string','object','base','event','lock','byte',
               'char','bool','int','float','double','fixed','new','this','value'}

SCALARS = {
    'void': 'void', 'bool': 'bool', 'int': 'int', 'float': 'float', 'double': 'double',
    'short': 'short', 'char': 'byte',
    'unsigned int': 'uint', 'unsigned short': 'ushort', 'unsigned char': 'byte',
    'unsigned long long': 'ulong', 'long long': 'long', 'size_t': 'nuint',
}

def norm(t):
    return re.sub(r'\s*\*', '*', re.sub(r'\s+', ' ', t)).strip()

def cstype(ctype, is_param):
    t = norm(ctype)
    const = t.startswith('const ')
    if const: t = t[6:]
    stars = len(t) - len(t.rstrip('*'))
    base = t.rstrip('*').strip()
    if stars == 0:
        if base in SCALARS: return SCALARS[base]
        if base in CALLBACKS: return CALLBACKS[base]
        if base in ('IGSharp_Vec2', 'IGSharp_Vec4'): return base
        raise ValueError(f'unmapped value type: {ctype}')
    if base == 'char':
        if stars == 1:
            return 'ReadOnlySpan<byte>' if (is_param and const) else 'byte*'
        return 'byte' + '*'*stars
    if base == 'void': return 'void' + '*'*stars
    if base in SCALARS: return SCALARS[base] + '*'*stars
    if base in HANDLES or base in MIRRORS: return base + '*'*stars
    if base in CALLBACKS: return CALLBACKS[base] + '*'*stars
    raise ValueError(f'unmapped pointer type: {ctype}')

def split_params(s):
    out, depth, cur = [], 0, ''
    for ch in s:
        if ch == ',' and depth == 0: out.append(cur); cur = ''
        else:
            if ch == '(': depth += 1
            if ch == ')': depth -= 1
            cur += ch
    if cur.strip(): out.append(cur)
    return [p.strip() for p in out if p.strip()]

FNPTR = re.compile(r'^(.*?)\(\*\s*(\w+)\)\s*\((.*)\)$')

def map_param(p):
    """C param decl -> (attrs, cstype, name)"""
    m = FNPTR.match(p.strip())
    if m:
        ret, name, args = m.groups()
        argts = [cstype(re.match(r'^(.*?)([A-Za-z_]\w*)$', a.strip()).group(1), True)
                 for a in split_params(args)] if args.strip() != 'void' else []
        # spans not allowed inside fn ptrs
        argts = ['byte*' if a == 'ReadOnlySpan<byte>' else a for a in argts]
        r = cstype(ret, False)
        r = 'byte' if r == 'bool' else ('byte*' if r == 'ReadOnlySpan<byte>' else r)
        sig = ', '.join(argts + [r]) if True else ''
        return ('', f'delegate* unmanaged[Cdecl]<{sig}>', name)
    am = re.match(r'^(.*?)([A-Za-z_]\w*)\s*\[\d*\]$', p.strip())
    if am:
        base = am.group(1).strip()
        base = re.sub(r'\bconst\s*$', '', base).strip()  # 'const char* const' -> 'const char*'
        cst = cstype(base + '*', False)  # array decays to pointer; never a span
        cst = 'byte**' if cst == 'ReadOnlySpan<byte>*' else cst
        name = am.group(2)
        if name in CS_KEYWORDS: name = '@' + name
        return ('', cst, name)
    pm = re.match(r'^(.*?)([A-Za-z_]\w*)$', p.strip())
    ctype, name = pm.group(1).strip(), pm.group(2)
    cst = cstype(ctype, True)
    attrs = '[MarshalAs(UnmanagedType.U1)] ' if cst == 'bool' else ''
    if name in CS_KEYWORDS: name = '@' + name
    return (attrs, cst, name)

# ---- parse header declarations, tracking sections & block comments ----
lines = h.split('\n')
items = []   # ('section', title) | ('comment', text) | ('fn', ret, name, params, trail)
prev_blank = True
last_comment = None
for i, line in enumerate(lines):
    s = line.strip()
    m = re.match(r'^// \[SECTION\] (.*)$', line)
    if m and i > 30:
        items.append(('section', m.group(1))); last_comment = None; continue
    if s.startswith('//') and not s.startswith('//---'):
        if prev_blank: last_comment = s[2:].strip()
        prev_blank = False
        continue
    if s == '':
        prev_blank = True; last_comment = None; continue
    prev_blank = False
    fm = re.match(r'^IGSHARP_API\s+(.*?)\s*\b(IGSharp_\w+)\((.*?)\);(.*)$', line)
    if fm:
        if last_comment:
            items.append(('comment', last_comment)); last_comment = None
        ret, name, params, trail = fm.groups()
        items.append(('fn', ret, name, params, trail.strip()))

# ---- emit ----
out = []
A = out.append
A('''using System.Runtime.CompilerServices;
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
''')
for hd in HANDLES:
    A(f'    public struct {hd};')
A('''
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
''')

count = 0
for it in items:
    if it[0] == 'section':
        if it[1].startswith('Obsolete'): continue
        A(f'\n    // ============ [SECTION] {it[1]} ============')
    elif it[0] == 'comment':
        A(f'\n    // --- {it[1]} ---')
    else:
        _, ret, name, params, trail = it
        rett = cstype(ret, False)
        plist = [] if norm(params) == 'void' else split_params(params)
        ps = []
        for p in plist:
            attrs, t, n = map_param(p)
            ps.append(f'{attrs}{t} {n}')
        tc = f' // {trail.lstrip("/ ").strip()}' if trail else ''
        A('')
        A(f'    [LibraryImport(ImGuiLib, EntryPoint = "{name}")]')
        A('    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]')
        if rett == 'bool':
            A('    [return: MarshalAs(UnmanagedType.U1)]')
        A(f'    public static partial {rett} {name}({", ".join(ps)});{tc}')
        count += 1

# ---- splice backend section from old file ----
old = open(OLD).read()
bi = old.find('    // ' + '='*10)
bm = re.search(r'^    // ={5,}\n    // SDL3 Platform Backend.*$', old, re.M)
if not bm:
    bm = re.search(r'^\s*// --- SDL3 Platform Backend ---.*$', old, re.M)
start = old.rfind('\n', 0, old.find('// SDL3 Platform Backend'))
# find enclosing separator line before that comment
seg = old[old.find('// SDL3 Platform Backend'):]
# capture from the separator comment block above 'SDL3 Platform Backend' to final closing brace
pre = old[:old.find('// SDL3 Platform Backend')]
sep_start = pre.rfind('\n    //')
backend = old[sep_start:].rstrip()
assert backend.endswith('}'), 'backend splice failed'
backend = backend[:backend.rfind('}')].rstrip()  # drop class closing brace, re-added below

A('')
A(backend.lstrip('\n'))
A('}')
open(OUT, 'w').write('\n'.join(out) + '\n')
print(f'generated {count} imports + backend splice -> {OUT}')
