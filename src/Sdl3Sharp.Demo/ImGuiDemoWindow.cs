using Sdl3Sharp.ImGui;
using Sdl3Sharp.ImGui.Native;
using System.Runtime.CompilerServices;

namespace Sdl3Sharp.Demo;

public static unsafe class ImGuiDemoWindow
{
    private static ImGuiDemoWindowData _demoData;

    // Demonstrate the various window flags. Typically you would just use the default!
    private static bool _noTitlebar = false;
    private static bool _noScrollbar = false;
    private static bool _noMenu = false;
    private static bool _noMove = false;
    private static bool _noResize = false;
    private static bool _noCollapse = false;
    private static bool _noClose = false;
    private static bool _noNav = false;
    private static bool _noBackground = false;
    private static bool _noBringToFront = false;
    private static bool _unsavedDocument = false;

    private static bool _options_enabled = true;
    private static float _options_f = 0.5f;
    private static int _options_n = 0;
    private static bool _options_b = true;

    // Widget Demo Section
    // State Fields - Basic Section

    private static int _basicClicked;
    private static bool _basicCheck = true;
    private static int _basicRadio;
    private static int _basicCounter;
    private static readonly byte[] _basicStr0 = new byte[128];
    private static readonly byte[] _basicStr1 = new byte[128];
    private static int _basicInputInt = 123;
    private static float _basicInputFloat = 0.001f;
    private static double _basicInputDouble = 999999.00000001;
    private static float _basicInputScientific = 1.0e10f;
    private static readonly float[] _basicVec4a = [0.10f, 0.20f, 0.30f, 0.44f];
    private static int _basicDragInt1 = 50;
    private static int _basicDragInt2 = 42;
    private static int _basicDragInt3 = 128;
    private static float _basicDragFloat1 = 1.00f;
    private static float _basicDragFloat2 = 0.0067f;
    private static int _basicSliderInt = 0;
    private static float _basicSliderFloat1 = 0.123f;
    private static float _basicSliderFloat2 = 0.0f;
    private static float _basicSliderAngle = 0.0f;
    private static int _basicSliderEnum = 0;
    private static ColorRGB _basicCol1 = (1.0f, 0.0f, 0.2f);
    private static Color _basicCol2 = (0.4f, 0.7f, 0.0f, 0.5f);
    private static int _basicComboItem;
    private static int _basicListBoxItem = 1;

    // State Fields - Collapsing Headers Section

    private static bool _collapsingClosableGroup = true;

    // State Fields - Combo Boxes Section

    private static byte[][] _comboItems = [
            "AAAA"u8.ToArray(),
            "BBBB"u8.ToArray(),
            "CCCC"u8.ToArray(),
            "DDDD"u8.ToArray(),
            "EEEE"u8.ToArray(),
            "FFFF"u8.ToArray(),
            "GGGG"u8.ToArray(),
            "HHHH"u8.ToArray(),
            "IIII"u8.ToArray(),
            "JJJJ"u8.ToArray(),
            "KKKK"u8.ToArray(),
            "LLLLLLL"u8.ToArray(),
            "MMMM"u8.ToArray(),
            "OOOOOOO"u8.ToArray()];
    private static int _comboItemCurrent;
    private static TextFilter _comboTextFilter;
    private static int _comboItemCurrent2;
    private static int _comboItemCurrent3 = -1;
    private static int _comboItemCurrent4;
    private static ComboFlags _comboFlags = ComboFlags.None;

    // State Fields - Color and Pickers Section

    private static ColorEditFlags _colorBaseFlags = ColorEditFlags.None;
    private static Color _colorColor = (114.0f / 255.0f, 144.0f / 255.0f, 154.0f / 255.0f, 200.0f / 255.0f);
    private static bool _colorNoBorder;
    private static Color _colorBackupColor;
    private static readonly Color[] _colorSavedPalette = new Color[32];
    private static bool _colorRefColor = false;
    private static Color _colorRefColorV = (1.0f, 0.0f, 1.0f, 0.5f);
    private static ColorEditFlags _colorColorPickerFlags = ColorEditFlags.AlphaBar;
    private static int _colorPickerMode = 0;
    private static int _colorDisplayMode = 0;
    private readonly static float[] _colorColorHsv = [0.23f, 1.0f, 1.0f, 1.0f];

    // State Fields - Data Types Section

    private static bool _dataDragClamp;
    private static sbyte _dataS8 = 127;
    private static byte _dataU8 = 255;
    private static short _dataS16 = 32767;
    private static ushort _dataU16 = 65535;
    private static int _dataS32 = -1;
    private static uint _dataU32 = 0xFFFFFFFF;
    private static long _dataS64 = -1;
    private static ulong _dataU64 = 0xFFFFFFFFFFFFFFFF;
    private static float _dataF32 = 0.123f;
    private static double _dataF64 = 90000.01234567890123456789;
    private static bool _dataInputsStep = true;
    private static InputTextFlags _dataFlags = InputTextFlags.None;

    // State Fields - Disable Blocks Section

    // DisableSections is a public property below

    // State Fields - Drag and Drop Section

    private static readonly int[] _dragDropMode1Col1 = [1, 0, 2];
    private static readonly int[] _dragDropMode1Col2 = [3, 4, 5];
    private static int _dragDropMode;

    // State Fields - Drags and Sliders Section

    private static float _dragsClampingValue1 = 0.5f;
    private static float _dragsClampingValue2 = 0.5f;
    private static float _dragsClampingValue3 = 0.5f;
    private static SliderFlags _dragsClampsFlags = SliderFlags.None;

    // State Fields - Progress Bars Section

    private static float _progressProgress;
    private static float _progressProgressDir = 1.0f;
    private static bool _progressAnimate = true;

    // State Fields - Querying Statuses Section

    private static int _queryingItemType = 1;
    private static bool _queryingB;
    private static float _queryingF = 1.0f;
    private static float[] _queryingFArray = [1.0f, 0.5f, 0.0f];
    private static Color _queryingCol4f = (1.0f, 0.5f, 0.0f, 1.0f);
    private static readonly byte[] _queryingStr = new byte[16];
    private static int _queryingCurrent1 = 1;
    private static int _queryingCurrent2;
    private static bool _queryingEmbedAllInsideAChildWindow;
    private static bool _queryingTestWindow;

    // State Fields - Selectables Section

    private static readonly bool[] _selectablesBasic = [false, true, false, false];
    private static readonly bool[] _selectablesSelected = new bool[5];
    private static int _selectablesSelectedSingleOnly = -1;
    private static readonly bool[] _selectablesSelectedSameLineFirst = new bool[3];
    private static readonly bool[] _selectablesSelectedSameLineSecond = new bool[3];
    private static readonly bool[] _selectablesSelectedSameLineThird = new bool[3];
    private static readonly bool[] _selectablesGrid = new bool[16];
    private static readonly bool[] _selectablesAlignment = [true, false, true, false, true, false, true, false, true];

    // State Fields - Tabs Section

    private static TabBarFlags _tabsFlags = TabBarFlags.Reorderable;
    private static bool _tabsFittingResizeDown = true;
    private static bool _tabsFittingScroll;
    private static readonly bool[] _tabsOpened = [true, true, true, true];
    private static int _tabsNextTabId;
    private static readonly List<int> _tabsActiveTabs = [];

    // State Fields - Text Section

    private static bool _textWordWrappingEnabled = true;
    private static bool _textWordSpacingEnabled;
    private static float _textWordSpacing = 8.0f;

    // State Fields - Text Input Section

    private static readonly byte[] _textInputStr0 = new byte[64];
    private static readonly byte[] _textInputStr1 = new byte[64];
    private static int _textInputI0;
    private static float _textInputF0;
    private static double _textInputD0;
    private static readonly byte[] _textInputBuf1 = new byte[64];
    private static readonly byte[] _textInputBuf2 = new byte[64];
    private static readonly byte[] _textInputBuf3 = new byte[64];
    private static readonly byte[] _textInputBuf4 = new byte[128];
    private static readonly byte[] _textInputBuf5 = new byte[128];
    private static readonly byte[] _textInputBufMultiline = new byte[1024 * 16];
    private static readonly byte[] _textInputPassword = new byte[64];
    private static InputTextFlags _textInputFlags = InputTextFlags.AllowTabInput;

    // State Fields - Tooltips Section

    private static int _tooltipsAlwaysOn;
    private static int _tooltipsOnDisabledItems;
    private static bool _tooltipsDisabledItem;
    private static bool _tooltipsWrap = true;
    private static HoveredFlags _tooltipsHoveredFlags = HoveredFlags.None;

    // State Fields - Tree Nodes Section

    private static TreeNodeFlags _treeBaseFlags = TreeNodeFlags.OpenOnArrow | TreeNodeFlags.OpenOnDoubleClick | TreeNodeFlags.SpanAvailWidth;
    private static bool _treeAlignLabelWithCurrentXPosition;
    private static bool _treeTestDragAndDrop;
    private static int _treeSelectionMask = 1 << 2;

    // State Fields - Vertical Sliders Section

    private static float _vslidersSize = 18.0f;
    private static int _vslidersIntValue;
    private static readonly float[] _vslidersValues = [0.0f, 0.60f, 0.35f, 0.9f, 0.70f, 0.20f, 0.0f];
    private static readonly float[] _vslidersValues2 = [0.20f, 0.80f, 0.40f, 0.25f];

    // State Fields - List Boxes Section

    private static int _listBoxItemCurrent;

    // State Fields - Images Section

    private static bool _imageUseTextColor;
    private static int _imageZoomImagePressedCount;

    // State Fields - Multi-Components Section

    private static readonly float[] _multiVec4f = [0.10f, 0.20f, 0.30f, 0.44f];
    private static readonly int[] _multiVec4i = [1, 5, 100, 255];

    // State Fields - Plotting Section

    private static bool _plotAnimate = true;
    private static readonly float[] _plotArrSin = new float[120];
    private static readonly float[] _plotArrCos = new float[120];
    private static int _plotValuesOffset;
    private static double _plotRefreshTime;
    private static float _plotPhase;
    private static PlotType _plotFuncType;
    private static int _plotDisplayCount = 70;

    // Public Properties

    /// <summary>
    /// Gets or sets whether to disable all widget sections.
    /// </summary>
    public static bool DisableSections { get; set; }

    // Shared Data

    /// <summary>
    /// Example names used throughout demos (vegetable names).
    /// </summary>
    private static readonly string[] ExampleNames =
    [
        "Artichoke", "Arugula", "Asparagus", "Avocado", "Bamboo Shoots", "Bean Sprouts", "Beans",
        "Beet", "Belgian Endive", "Bell Pepper", "Bitter Gourd", "Bok Choy", "Broccoli", "Brussels Sprouts",
        "Burdock Root", "Cabbage", "Calabash", "Capers", "Carrot", "Cassava", "Cauliflower", "Celery",
        "Celery Root", "Chard", "Chayote", "Chinese Broccoli", "Corn", "Cucumber"
    ];

    /// <summary>
    /// Initializes a new instance of the <see cref="ImGuiDemoWindow"/> class.
    /// </summary>
    static ImGuiDemoWindow()
    {
        // Initialize text buffers with default content
        "Hello, world!"u8.CopyTo(_basicStr0);
        "password123"u8.CopyTo(_textInputPassword);

        // Generate a default palette
        for (var n = 0; n < 32; n++)
        {
            (var r, var g, var b) = Color.ColorConvertHSVtoRGB(n / 31.0f, 0.8f, 0.8f);
            _colorSavedPalette[n] = new(r, g, b, 1.0f);
        }

        // Initialize plotting arrays
        for (var i = 0; i < _plotArrSin.Length; i++)
        {
            _plotArrSin[i] = MathF.Sin(i * 0.2f);
            _plotArrCos[i] = MathF.Cos(i * 0.2f);
        }

        // Initialize active tabs
        _tabsActiveTabs.Add(0);
        _tabsActiveTabs.Add(1);
        _tabsActiveTabs.Add(2);
        _tabsNextTabId = 3;

        // Initialize querying statuses string
        "Test"u8.CopyTo(_queryingStr);

        // Initialize text input
        "Hello, world!"u8.CopyTo(_textInputStr0);
        "hello"u8.CopyTo(_textInputBuf1);
        "hello"u8.CopyTo(_textInputBuf2);
        "Enter text here"u8.CopyTo(_textInputBuf3);
        "Dear ImGui\r\n\r\nWelcome to the demo!\r\n"u8.CopyTo(_textInputBufMultiline);
    }

    // Helper to display a little (?) mark which shows a tooltip when hovered.
    // In your own code you may want to display an actual icon if you are using a merged icon fonts (see docs/FONTS.md)
    private static void HelpMarker(ReadOnlySpan<byte> desc)
    {
        Widgets.TextDisabled("(?)"u8);
        if (Widgets.BeginItemTooltip())
        {
            Style.PushTextWrapPosition(Font.GetSize() * 35.0f);
            Widgets.TextUnformatted(desc);
            Style.PopTextWrapPosition();
            Widgets.EndTooltip();
        }
    }

    public static void ShowDemoWindow(ref bool open)
    {
        WindowFlags windowFlags = 0;
        if (_noTitlebar)
        {
            windowFlags |= WindowFlags.NoTitleBar;
        }

        if (_noScrollbar)
        {
            windowFlags |= WindowFlags.NoScrollbar;
        }

        if (!_noMenu)
        {
            windowFlags |= WindowFlags.MenuBar;
        }

        if (_noMove)
        {
            windowFlags |= WindowFlags.NoMove;
        }

        if (_noResize)
        {
            windowFlags |= WindowFlags.NoResize;
        }

        if (_noCollapse)
        {
            windowFlags |= WindowFlags.NoCollapse;
        }

        if (_noNav)
        {
            windowFlags |= WindowFlags.NoNav;
        }

        if (_noBackground)
        {
            windowFlags |= WindowFlags.NoBackground;
        }

        if (_noBringToFront)
        {
            windowFlags |= WindowFlags.NoBringToFrontOnFocus;
        }

        if (_unsavedDocument)
        {
            windowFlags |= WindowFlags.UnsavedDocument;
        }

        // We specify a default position/size in case there's no data in the .ini file.
        // We only do it to make the demo applications a little more welcoming, but typically this isn't required.
        Viewport mainViewport = Context.MainViewport;
        Window.SetNextWindowPos((mainViewport.WorkPosition.X + 50, mainViewport.WorkPosition.Y + 20), Condition.FirstUseEver);
        Window.SetNextWindowSize((550, 680), Condition.FirstUseEver);

        if (_noClose 
            ? !Window.Begin("Dear ImGui Demo (managed)"u8, windowFlags) 
            : !Window.Begin("Dear ImGui Demo (managed)"u8, ref open, windowFlags))
        {
            return;
        }

        // Most framed widgets share a common width settings. Remaining width is used for the label.
        // The width of the frame may be changed with PushItemWidth() or SetNextItemWidth().
        // - Positive value for absolute size, negative value for right-alignment.
        // - The default value is about GetWindowWidth() * 0.65f.
        // - See 'Demo->Layout->Widgets Width' for details.
        // Here we change the frame width based on how much width we want to give to the label.
        var labelWidthBase = Font.GetSize() * 12; // Some amount of width for label, based on font size.
        var labelWidthMax = Window.ContentRegionAvail.Width * 0.40f; // ...but always leave some room for framed widgets.
        var labelWidth = Math.Min(labelWidthBase, labelWidthMax);
        Style.PushItemWidth(-labelWidth); // Right-align: framed items will leave 'label_width' available for the label.

        DemoWindowMenuBar();

        Widgets.Text($"dear imgui says hello! ({Context.VersionString}) ({Context.VersionNumber})".ToUtf8());
        Widgets.Spacing();

        if (Widgets.CollapsingHeader("Help"u8))
        {
            Widgets.SeparatorText("ABOUT THIS DEMO:"u8);
            Widgets.BulletText("Sections below are demonstrating many aspects of the library."u8);
            Widgets.BulletText("The \"u8Examples\" menu above leads to more demo contents."u8);
            Widgets.BulletText("The \"u8Tools\" menu above gives access to: About Box, Style Editor,\n"u8 +
                               "and Metrics/Debugger (general purpose Dear ImGui debugging tool)."u8);

            Widgets.SeparatorText("PROGRAMMER GUIDE:"u8);
            Widgets.BulletText("See the ShowDemoWindow() code in imgui_demo.cpp. <- you are here!"u8);
            Widgets.BulletText("See comments in imgui.cpp."u8);
            Widgets.BulletText("See example applications in the examples/ folder."u8);
            Widgets.BulletText("Read the FAQ at "u8);
            Widgets.SameLine(0, 0);
            _ = Widgets.TextLinkOpenURL("https://www.dearimgui.com/faq/"u8);
            Widgets.BulletText("Set 'io.ConfigFlags |= NavEnableKeyboard' for keyboard controls."u8);
            Widgets.BulletText("Set 'io.ConfigFlags |= NavEnableGamepad' for gamepad controls."u8);

            Widgets.SeparatorText("USER GUIDE:"u8);
            ShowUserGuide();
        }

        if (Widgets.CollapsingHeader("Configuration"u8))
        {
            IO io = Context.IO;

            if (Widgets.TreeNode("Configuration##2"u8))
            {
                Widgets.SeparatorText("General"u8);
                _ = Widgets.CheckboxFlags("io.ConfigFlags: NavEnableKeyboard"u8, ref io.ConfigFlags, ConfigFlags.NavEnableKeyboard);
                Widgets.SameLine();
                HelpMarker("Enable keyboard controls."u8);
                _ = Widgets.CheckboxFlags("io.ConfigFlags: NavEnableGamepad"u8, ref io.ConfigFlags, ConfigFlags.NavEnableGamepad);
                Widgets.SameLine();
                HelpMarker("Enable gamepad controls. Require backend to set io.BackendFlags |= ImGuiBackendFlags_HasGamepad.\n\nRead instructions in imgui.cpp for details."u8);
                _ = Widgets.CheckboxFlags("io.ConfigFlags: NoMouse"u8, ref io.ConfigFlags, ConfigFlags.NoMouse);
                Widgets.SameLine();
                HelpMarker("Instruct dear imgui to disable mouse inputs and interactions."u8);

                // The "NoMouse"u8 option can get us stuck with a disabled mouse! Let's provide an alternative way to fix it:
                if (io.ConfigFlags.HasFlag(ConfigFlags.NoMouse))
                {
                    if (((float)Context.GetTime() % 0.40f) < 0.20f)
                    {
                        Widgets.SameLine();
                        Widgets.Text("<<PRESS SPACE TO DISABLE>>"u8);
                    }

                    // Prevent both being checked
                    if (Context.IsKeyPressed(Key.Space) || io.ConfigFlags.HasFlag(ConfigFlags.NoKeyboard))
                    {
                        io.ConfigFlags &= ~ConfigFlags.NoMouse;
                    }
                }

                _ = Widgets.CheckboxFlags("io.ConfigFlags: NoMouseCursorChange"u8, ref io.ConfigFlags, ConfigFlags.NoMouseCursorChange);
                Widgets.SameLine();
                HelpMarker("Instruct backend to not alter mouse cursor shape and visibility."u8);
                _ = Widgets.CheckboxFlags("io.ConfigFlags: NoKeyboard"u8, ref io.ConfigFlags, ConfigFlags.NoKeyboard);
                Widgets.SameLine();
                HelpMarker("Instruct dear imgui to disable keyboard inputs and interactions."u8);

                _ = Widgets.Checkbox("io.ConfigInputTrickleEventQueue"u8, ref io.ConfigInputTrickleEventQueue);
                Widgets.SameLine();
                HelpMarker("Enable input queue trickling: some types of events submitted during the same frame (e.g. button down + up) will be spread over multiple frames, improving interactions with low framerates."u8);
                _ = Widgets.Checkbox("io.MouseDrawCursor"u8, ref io.MouseDrawCursor);
                Widgets.SameLine();
                HelpMarker("Instruct Dear ImGui to render a mouse cursor itself. Note that a mouse cursor rendered via your application GPU rendering path will feel more laggy than hardware cursor, but will be more in sync with your other visuals.\n\nSome desktop applications may use both kinds of cursors (e.g. enable software cursor only when resizing/dragging something)."u8);

                Widgets.SeparatorText("Keyboard/Gamepad Navigation"u8);
                _ = Widgets.Checkbox("io.ConfigNavSwapGamepadButtons"u8, ref io.ConfigNavSwapGamepadButtons);
                _ = Widgets.Checkbox("io.ConfigNavMoveSetMousePos"u8, ref io.ConfigNavMoveSetMousePos);
                Widgets.SameLine();
                HelpMarker("Directional/tabbing navigation teleports the mouse cursor. May be useful on TV/console systems where moving a virtual mouse is difficult"u8);
                _ = Widgets.Checkbox("io.ConfigNavCaptureKeyboard"u8, ref io.ConfigNavCaptureKeyboard);
                _ = Widgets.Checkbox("io.ConfigNavEscapeClearFocusItem"u8, ref io.ConfigNavEscapeClearFocusItem);
                Widgets.SameLine();
                HelpMarker("Pressing Escape clears focused item."u8);
                _ = Widgets.Checkbox("io.ConfigNavEscapeClearFocusWindow"u8, ref io.ConfigNavEscapeClearFocusWindow);
                Widgets.SameLine();
                HelpMarker("Pressing Escape clears focused window."u8);
                _ = Widgets.Checkbox("io.ConfigNavCursorVisibleAuto"u8, ref io.ConfigNavCursorVisibleAuto);
                Widgets.SameLine();
                HelpMarker("Using directional navigation key makes the cursor visible. Mouse click hides the cursor."u8);
                _ = Widgets.Checkbox("io.ConfigNavCursorVisibleAlways"u8, ref io.ConfigNavCursorVisibleAlways);
                Widgets.SameLine();
                HelpMarker("Navigation cursor is always visible."u8);

                Widgets.SeparatorText("Windows"u8);
                _ = Widgets.Checkbox("io.ConfigWindowsResizeFromEdges"u8, ref io.ConfigWindowsResizeFromEdges);
                Widgets.SameLine();
                HelpMarker("Enable resizing of windows from their edges and from the lower-left corner.\nThis requires ImGuiBackendFlags_HasMouseCursors for better mouse cursor feedback."u8);
                _ = Widgets.Checkbox("io.ConfigWindowsMoveFromTitleBarOnly"u8, ref io.ConfigWindowsMoveFromTitleBarOnly);
                _ = Widgets.Checkbox("io.ConfigWindowsCopyContentsWithCtrlC"u8, ref io.ConfigWindowsCopyContentsWithCtrlC); // [EXPERIMENTAL]
                Widgets.SameLine();
                HelpMarker("*EXPERIMENTAL* Ctrl+C copy the contents of focused window into the clipboard.\n\nExperimental because:\n- (1) has known issues with nested Begin/End pairs.\n- (2) text output quality varies.\n- (3) text output is in submission order rather than spatial order."u8);
                _ = Widgets.Checkbox("io.ConfigScrollbarScrollByPage"u8, ref io.ConfigScrollbarScrollByPage);
                Widgets.SameLine();
                HelpMarker("Enable scrolling page by page when clicking outside the scrollbar grab.\nWhen disabled, always scroll to clicked location.\nWhen enabled, Shift+Click scrolls to clicked location."u8);

                Widgets.SeparatorText("Widgets"u8);
                _ = Widgets.Checkbox("io.ConfigInputTextCursorBlink"u8, ref io.ConfigInputTextCursorBlink);
                Widgets.SameLine();
                HelpMarker("Enable blinking cursor (optional as some users consider it to be distracting)."u8);
                _ = Widgets.Checkbox("io.ConfigInputTextEnterKeepActive"u8, ref io.ConfigInputTextEnterKeepActive);
                Widgets.SameLine();
                HelpMarker("Pressing Enter will keep item active and select contents (single-line only)."u8);
                _ = Widgets.Checkbox("io.ConfigDragClickToInputText"u8, ref io.ConfigDragClickToInputText);
                Widgets.SameLine();
                HelpMarker("Enable turning DragXXX widgets into text input with a simple mouse click-release (without moving)."u8);
                _ = Widgets.Checkbox("io.ConfigMacOSXBehaviors"u8, ref io.ConfigMacOSXBehaviors);
                Widgets.SameLine();
                HelpMarker("Swap Cmd<>Ctrl keys, enable various MacOS style behaviors."u8);
                Widgets.Text("Also see Style->Rendering for rendering options."u8);

                // Also read: https://github.com/ocornut/imgui/wiki/Error-Handling
                Widgets.SeparatorText("Error Handling"u8);

                _ = Widgets.Checkbox("io.ConfigErrorRecovery"u8, ref io.ConfigErrorRecovery);
                Widgets.SameLine();
                HelpMarker(
                    "Options to configure how we handle recoverable errors.\n"u8 +
                    "- Error recovery is not perfect nor guaranteed! It is a feature to ease development.\n"u8 +
                    "- You not are not supposed to rely on it in the course of a normal application run.\n"u8 +
                    "- Possible usage: facilitate recovery from errors triggered from a scripting language or after specific exceptions handlers.\n"u8 +
                    "- Always ensure that on programmers seat you have at minimum Asserts or Tooltips enabled when making direct imgui API call! "u8 +
                    "Otherwise it would severely hinder your ability to catch and correct mistakes!"u8);
                _ = Widgets.Checkbox("io.ConfigErrorRecoveryEnableAssert"u8, ref io.ConfigErrorRecoveryEnableAssert);
                _ = Widgets.Checkbox("io.ConfigErrorRecoveryEnableDebugLog"u8, ref io.ConfigErrorRecoveryEnableDebugLog);
                _ = Widgets.Checkbox("io.ConfigErrorRecoveryEnableTooltip"u8, ref io.ConfigErrorRecoveryEnableTooltip);
                if (!io.ConfigErrorRecoveryEnableAssert && !io.ConfigErrorRecoveryEnableDebugLog && !io.ConfigErrorRecoveryEnableTooltip)
                {
                    io.ConfigErrorRecoveryEnableAssert = io.ConfigErrorRecoveryEnableDebugLog = io.ConfigErrorRecoveryEnableTooltip = true;
                }

                // Also read: https://github.com/ocornut/imgui/wiki/Debug-Tools
                Widgets.SeparatorText("Debug"u8);
                _ = Widgets.Checkbox("io.ConfigDebugIsDebuggerPresent"u8, ref io.ConfigDebugIsDebuggerPresent);
                Widgets.SameLine();
                HelpMarker("Enable various tools calling IM_DEBUG_BREAK().\n\nRequires a debugger being attached, otherwise IM_DEBUG_BREAK() options will appear to crash your application."u8);
                _ = Widgets.Checkbox("io.ConfigDebugHighlightIdConflicts"u8, ref io.ConfigDebugHighlightIdConflicts);
                Widgets.SameLine();
                HelpMarker("Highlight and show an error message when multiple items have conflicting identifiers."u8);
                Widgets.BeginDisabled();
                _ = Widgets.Checkbox("io.ConfigDebugBeginReturnValueOnce"u8, ref io.ConfigDebugBeginReturnValueOnce);
                Widgets.EndDisabled();
                Widgets.SameLine();
                HelpMarker("First calls to Begin()/BeginChild() will return false.\n\nTHIS OPTION IS DISABLED because it needs to be set at application boot-time to make sense. Showing the disabled option is a way to make this feature easier to discover."u8);
                _ = Widgets.Checkbox("io.ConfigDebugBeginReturnValueLoop"u8, ref io.ConfigDebugBeginReturnValueLoop);
                Widgets.SameLine();
                HelpMarker("Some calls to Begin()/BeginChild() will return false.\n\nWill cycle through window depths then repeat. Windows should be flickering while running."u8);
                _ = Widgets.Checkbox("io.ConfigDebugIgnoreFocusLoss"u8, ref io.ConfigDebugIgnoreFocusLoss);
                Widgets.SameLine();
                HelpMarker("Option to deactivate io.AddFocusEvent(false) handling. May facilitate interactions with a debugger when focus loss leads to clearing inputs data."u8);
                _ = Widgets.Checkbox("io.ConfigDebugIniSettings"u8, ref io.ConfigDebugIniSettings);
                Widgets.SameLine();
                HelpMarker("Option to save .ini data with extra comments (particularly helpful for Docking, but makes saving slower)."u8);

                Widgets.TreePop();
                Widgets.Spacing();
            }

            if (Widgets.TreeNode("Backend Flags"u8))
            {
                HelpMarker(
                    "Those flags are set by the backends (imgui_impl_xxx files) to specify their capabilities.\n"u8 +
                    "Here we expose them as read-only fields to avoid breaking interactions with your backend."u8);

                // FIXME: Maybe we need a BeginReadonly() equivalent to keep label bright?
                Widgets.BeginDisabled();
                _ = Widgets.CheckboxFlags("io.BackendFlags: HasGamepad"u8, ref io.BackendFlags, BackendFlags.HasGamepad);
                _ = Widgets.CheckboxFlags("io.BackendFlags: HasMouseCursors"u8, ref io.BackendFlags, BackendFlags.HasMouseCursors);
                _ = Widgets.CheckboxFlags("io.BackendFlags: HasSetMousePos"u8, ref io.BackendFlags, BackendFlags.HasSetMousePos);
                _ = Widgets.CheckboxFlags("io.BackendFlags: RendererHasVtxOffset"u8, ref io.BackendFlags, BackendFlags.RendererHasVtxOffset);
                _ = Widgets.CheckboxFlags("io.BackendFlags: RendererHasTextures"u8, ref io.BackendFlags, BackendFlags.RendererHasTextures);
                Widgets.EndDisabled();

                Widgets.TreePop();
                Widgets.Spacing();
            }

            if (Widgets.TreeNode("Style, Fonts"u8))
            {
                _ = Widgets.Checkbox("Style Editor"u8, ref _demoData.ShowStyleEditor);
                Widgets.SameLine();
                HelpMarker("The same contents can be accessed in 'Tools->Style Editor' or by calling the ShowStyleEditor() function."u8);
                Widgets.TreePop();
                Widgets.Spacing();
            }

            if (Widgets.TreeNode("Capture/Logging"u8))
            {
                HelpMarker(
                    "The logging API redirects all text output so you can easily capture the content of "u8 +
                    "a window or a block. Tree nodes can be automatically expanded.\n"u8 +
                    "Try opening any of the contents below in this window and then click one of the \"u8Log To\" button."u8);
                Context.LogButtons();

                HelpMarker("You can also call ImGui::LogText() to output directly to the log without a visual output."u8);
                if (Widgets.Button("Copy \"Hello, world!\" to clipboard"u8))
                {
                    Context.LogToClipboard();
                    Context.LogText("Hello, world!"u8);
                    Context.LogFinish();
                }

                Widgets.TreePop();
            }
        }

        if (Widgets.CollapsingHeader("Window options"u8))
        {
            if (Widgets.BeginTable("split"u8, 3))
            {
                _ = Widgets.TableNextColumn();
                _ = Widgets.Checkbox("No titlebar"u8, ref _noTitlebar);
                _ = Widgets.TableNextColumn();
                _ = Widgets.Checkbox("No scrollbar"u8, ref _noScrollbar);
                _ = Widgets.TableNextColumn();
                _ = Widgets.Checkbox("No menu"u8, ref _noMenu);
                _ = Widgets.TableNextColumn();
                _ = Widgets.Checkbox("No move"u8, ref _noMove);
                _ = Widgets.TableNextColumn();
                _ = Widgets.Checkbox("No resize"u8, ref _noResize);
                _ = Widgets.TableNextColumn();
                _ = Widgets.Checkbox("No collapse"u8, ref _noCollapse);
                _ = Widgets.TableNextColumn();
                _ = Widgets.Checkbox("No close"u8, ref _noClose);
                _ = Widgets.TableNextColumn();
                _ = Widgets.Checkbox("No nav"u8, ref _noNav);
                _ = Widgets.TableNextColumn();
                _ = Widgets.Checkbox("No background"u8, ref _noBackground);
                _ = Widgets.TableNextColumn();
                _ = Widgets.Checkbox("No bring to front"u8, ref _noBringToFront);
                _ = Widgets.TableNextColumn();
                _ = Widgets.Checkbox("Unsaved document"u8, ref _unsavedDocument);
                Widgets.EndTable();
            }
        }

        DemoWindowWidgets();
        //DemoWindowLayout();
        //DemoWindowPopups();
        //DemoWindowTables();
        //DemoWindowInputs();

        Style.PopItemWidth();
        Window.End();
    }

    private static void DemoWindowMenuBar()
    {
        if (!Widgets.BeginMenuBar())
        {
            return;
        }

        if (Widgets.BeginMenu("Menu"u8))
        {
            ShowExampleMenuFile();
            Widgets.EndMenu();
        }

        if (Widgets.BeginMenu("Examples"u8))
        {
            Widgets.MenuItem("Main menu bar"u8, null, ref _demoData.ShowMainMenuBar);

            Widgets.SeparatorText("Mini apps"u8);
            Widgets.MenuItem("Assets Browser"u8, null, ref _demoData.ShowAppAssetsBrowser);
            Widgets.MenuItem("Console"u8, null, ref _demoData.ShowAppConsole);
            Widgets.MenuItem("Custom rendering"u8, null, ref _demoData.ShowAppCustomRendering);
            Widgets.MenuItem("Documents"u8, null, ref _demoData.ShowAppDocuments);
            Widgets.MenuItem("Log"u8, null, ref _demoData.ShowAppLog);
            Widgets.MenuItem("Property editor"u8, null, ref _demoData.ShowAppPropertyEditor);
            Widgets.MenuItem("Simple layout"u8, null, ref _demoData.ShowAppLayout);
            Widgets.MenuItem("Simple overlay"u8, null, ref _demoData.ShowAppSimpleOverlay);

            Widgets.SeparatorText("Concepts"u8);
            Widgets.MenuItem("Auto-resizing window"u8, null, ref _demoData.ShowAppAutoResize);
            Widgets.MenuItem("Constrained-resizing window"u8, null, ref _demoData.ShowAppConstrainedResize);
            Widgets.MenuItem("Fullscreen window"u8, null, ref _demoData.ShowAppFullscreen);
            Widgets.MenuItem("Long text display"u8, null, ref _demoData.ShowAppLongText);
            Widgets.MenuItem("Manipulating window titles"u8, null, ref _demoData.ShowAppWindowTitles);

            Widgets.EndMenu();
        }

        if (Widgets.BeginMenu("Tools"u8))
        {
            IO io = Context.IO;
#if !IMGUI_DISABLE_DEBUG_TOOLS
            const bool hasDebugTools = true;
#else
            const bool hasDebugTools = false;
#endif
            Widgets.MenuItem("Metrics/Debugger"u8, null, ref _demoData.ShowMetrics, hasDebugTools);
            if (Widgets.BeginMenu("Debug Options"u8))
            {
                Widgets.BeginDisabled(!hasDebugTools);
                Widgets.Checkbox("Highlight ID Conflicts"u8, ref io.ConfigDebugHighlightIdConflicts);
                Widgets.EndDisabled();
                Widgets.Checkbox("Assert on error recovery"u8, ref io.ConfigErrorRecoveryEnableAssert);
                Widgets.TextDisabled("(see Demo->Configuration for details & more)"u8);
                Widgets.EndMenu();
            }

            Widgets.MenuItem("Debug Log"u8, null, ref _demoData.ShowDebugLog, hasDebugTools);
            Widgets.MenuItem("ID Stack Tool"u8, null, ref _demoData.ShowIDStackTool, hasDebugTools);
            var isDebuggerPresent = io.ConfigDebugIsDebuggerPresent;
            if (Widgets.MenuItem("Item Picker"u8, null, false, hasDebugTools))// && isDebuggerPresent))
            {
                // TODO: Widgets.DebugStartItemPicker();
            }

            if (!isDebuggerPresent)
            {
                Widgets.SetItemTooltip("Requires io.ConfigDebugIsDebuggerPresent=true to be set.\n\nWe otherwise disable some extra features to avoid casual users crashing the application."u8);
            }

            Widgets.MenuItem("Style Editor"u8, null, ref _demoData.ShowStyleEditor);
            Widgets.MenuItem("About Dear ImGui"u8, null, ref _demoData.ShowAbout);

            Widgets.EndMenu();
        }

        Widgets.EndMenuBar();
    }

    private static void DemoWindowWidgetsBasic()
    {
        if (!Widgets.TreeNode("Basic"u8))
        {
            return;
        }

        Widgets.SeparatorText("General"u8);

        // Button
        if (Widgets.Button("Button"u8))
        {
            _basicClicked++;
        }

        if ((_basicClicked & 1) != 0)
        {
            Widgets.SameLine();
            Widgets.Text("Thanks for clicking me!"u8);
        }

        // Checkbox
        _ = Widgets.Checkbox("checkbox"u8, ref _basicCheck);

        // Radio buttons
        _ = Widgets.RadioButton("radio a"u8, ref _basicRadio, 0);
        Widgets.SameLine();
        _ = Widgets.RadioButton("radio b"u8, ref _basicRadio, 1);
        Widgets.SameLine();
        _ = Widgets.RadioButton("radio c"u8, ref _basicRadio, 2);

        // Hyperlink
        Font.AlignTextToFramePadding();
        _ = Widgets.TextLinkOpenURL("Hyperlink"u8, "https://github.com/ocornut/imgui/wiki/Error-Handling"u8);

        // Colored buttons
        for (var i = 0; i < 7; i++)
        {
            if (i > 0)
            {
                Widgets.SameLine();
            }

            Id.Push(i);
            Style.PushStyleColor(StyleColor.Button, Color.FromHSV(i / 7.0f, 0.6f, 0.6f));
            Style.PushStyleColor(StyleColor.ButtonHovered, Color.FromHSV(i / 7.0f, 0.7f, 0.7f));
            Style.PushStyleColor(StyleColor.ButtonActive, Color.FromHSV(i / 7.0f, 0.8f, 0.8f));
            _ = Widgets.Button("Click"u8);
            Style.PopStyleColor(3);
            Id.Pop();
        }

        // Use AlignTextToFramePadding() to align text baseline to the baseline of framed widgets elements
        // (otherwise a Text+SameLine+Button sequence will have the text a little too high by default!)
        // See 'Demo->Layout->Text Baseline Alignment' for details.
        Font.AlignTextToFramePadding();
        Widgets.Text("Hold to repeat:"u8);
        Widgets.SameLine();

        var spacing = Context.Style.ItemInnerSpacing.X;
        Style.PushItemFlag(ItemFlags.ButtonRepeat, true);
        if (Widgets.ArrowButton("##left"u8, Dir.Left))
        {
            _basicCounter--;
        }

        Widgets.SameLine(0.0f, spacing);
        if (Widgets.ArrowButton("##right"u8, Dir.Right))
        {
            _basicCounter++;
        }

        Style.PopItemFlag();
        Widgets.SameLine();
        Widgets.Text(_basicCounter.ToString().ToUtf8());

        _ = Widgets.Button("Tooltip"u8);
        Widgets.SetItemTooltip("I am a tooltip"u8);
        Widgets.LabelText("label"u8, "Value"u8);

        Widgets.SeparatorText("Inputs"u8);

        // Input text
        {
            _ = Widgets.InputText("input text"u8, _basicStr0);
            Widgets.SameLine();
            HelpMarker(@"USER:
Hold Shift or use mouse to select text.
Ctrl+Left/Right to word jump.
Ctrl+A or Double-Click to select all.
Ctrl+X,Ctrl+C,Ctrl+V for clipboard.
Ctrl+Z to undo, Ctrl+Y/Ctrl+Shift+Z to redo.
Escape to revert.

PROGRAMMER:
You can use the ImGuiInputTextFlags_CallbackResize facility if you need to wire InputText() to a dynamic string type."u8);
            _ = Widgets.InputTextWithHint("input text (w/ hint)"u8, "enter text here"u8, _basicStr1);
            _ = Widgets.Input("input int"u8, ref _basicInputInt);
            _ = Widgets.Input("input float"u8, ref _basicInputFloat, 0.01f, 1.0f, "%.3f"u8);
            _ = Widgets.Input("input double"u8, ref _basicInputDouble, 0.01, 1.0, "%.8f"u8);
            _ = Widgets.Input("input scientific"u8, ref _basicInputScientific, 0.0f, 0.0f, "%e"u8);
            Widgets.SameLine();
            HelpMarker(@"You can input value using the scientific notation,
  e.g. ""1e+8"" becomes ""100000000""."u8);
            _ = Widgets.Input("input float3"u8, _basicVec4a);
        }

        {
            Widgets.SeparatorText("Drags"u8);
            _ = Widgets.Drag("drag int"u8, ref _basicDragInt1, 1);
            Widgets.SameLine();
            HelpMarker(@"Click and drag to edit value.
Hold Shift/Alt for faster/slower edit.
Double-Click or Ctrl+Click to input value."u8);
            _ = Widgets.Drag("drag int 0..100"u8, ref _basicDragInt2, 1, 0, 100, "%d%%"u8, SliderFlags.AlwaysClamp);
            _ = Widgets.Drag("drag int wrap 100..200"u8, ref _basicDragInt3, 1, 100, 200, "%d"u8, SliderFlags.WrapAround);
            _ = Widgets.Drag("drag float"u8, ref _basicDragFloat1, 0.005f);
            _ = Widgets.Drag("drag small float"u8, ref _basicDragFloat2, 0.0001f, 0.0f, 0.0f, "%.06f ns"u8);

            Widgets.SeparatorText("Sliders"u8);
            _ = Widgets.Slider("slider int"u8, ref _basicSliderInt, -1, 3);
            Widgets.SameLine();
            HelpMarker("Ctrl+Click to input value."u8);
            _ = Widgets.Slider("slider float"u8, ref _basicSliderFloat1, 0.0f, 1.0f, "ratio = %.3f"u8);
            _ = Widgets.Slider("slider float (log)"u8, ref _basicSliderFloat2, -10.0f, 10.0f, "%.4f"u8, SliderFlags.Logarithmic);
            _ = Widgets.SliderAngle("slider angle"u8, ref _basicSliderAngle);
            _ = Widgets.Slider("slider enum"u8, ref _basicSliderEnum, 0, 3, ((Element)_basicSliderEnum).ToString().ToUtf8());
            Widgets.SameLine();
            HelpMarker("Using the format string parameter to display a name instead of the underlying integer."u8);
        }

        {
            Widgets.SeparatorText("Selectors/Pickers"u8);
            _ = Widgets.ColorEdit("color 1"u8, ref _basicCol1);
            Widgets.SameLine();
            HelpMarker(@"Click on the color square to open a color picker.
Click and hold to use drag and drop.
Right-Click on the color square to show options.
Ctrl+Click on individual component to input value.
"u8);
            _ = Widgets.ColorEdit("color 2"u8, ref _basicCol2);
        }

        {
            // TODO: Basic combos
            _ = Widgets.Combo("combo"u8, ref _basicComboItem, "AAAA\0BBBB\0CCCC\0DDDD\0EEEE\0FFFF\0GGGG\0HHHH\0IIIIIII\0JJJJ\0KKKKKKK\0"u8);
            Widgets.SameLine();
            HelpMarker("Using the simplified one-liner Combo API here.\nRefer to the \"Combo\" section below for an explanation of how to use the more flexible and general BeginCombo/EndCombo API."u8);
        }

        {
            // TODO: Basic list box
            if (Widgets.BeginListBox("listbox"u8, new Vec2(0, 4 * Font.GetTextLineHeightWithSpacing())))
            {
                string[] listBoxItems = ["Apple", "Banana", "Cherry", "Kiwi", "Mango", "Orange", "Pineapple", "Strawberry", "Watermelon"];
                for (var i = 0; i < listBoxItems.Length; i++)
                {
                    var isSelected = _basicListBoxItem == i;
                    if (Widgets.Selectable(System.Text.Encoding.UTF8.GetBytes(listBoxItems[i]), isSelected))
                    {
                        _basicListBoxItem = i;
                    }

                    if (isSelected)
                    {
                        Widgets.SetItemDefaultFocus();
                    }
                }

                Widgets.EndListBox();
            }

            Widgets.SameLine();
            HelpMarker("Using the simplified one-liner ListBox API here.\nRefer to the \"List boxes\" section below for an explanation of how to use the more flexible and general BeginListBox/EndListBox API."u8);
        }

        Widgets.TreePop();
    }

    private static void DemoWindowWidgetsBullets()
    {
        if (!Widgets.TreeNode("Bullets"u8))
        {
            return;
        }

        Widgets.BulletText("Bullet point 1"u8);
        Widgets.BulletText("Bullet point 2\nOn multiple lines"u8);
        if (Widgets.TreeNode("Tree node"u8))
        {
            Widgets.BulletText("Another bullet point"u8);
            Widgets.TreePop();
        }

        Widgets.Bullet();
        Widgets.Text("Bullet point 3 (two calls)"u8);
        Widgets.Bullet();
        _ = Widgets.SmallButton("Button"u8);
        Widgets.TreePop();
    }

    private static void DemoWindowWidgetsCollapsingHeaders()
    {
        if (!Widgets.TreeNode("Collapsing Headers"u8))
        {
            return;
        }

        _ = Widgets.Checkbox("Show 2nd header"u8, ref _collapsingClosableGroup);
        if (Widgets.CollapsingHeader("Header"u8, TreeNodeFlags.None))
        {
            Widgets.Text($"IsItemHovered: {Widgets.IsItemHovered()}".ToUtf8());
            for (var i = 0; i < 5; i++)
            {
                Widgets.Text($"Some content {i}".ToUtf8());
            }
        }

        if (_collapsingClosableGroup)
        {
            if (Widgets.CollapsingHeader("Header with a close button"u8, ref _collapsingClosableGroup))
            {
                Widgets.Text($"IsItemHovered: {Widgets.IsItemHovered()}".ToUtf8());
                for (var i = 0; i < 5; i++)
                {
                    Widgets.Text($"More content {i}".ToUtf8());
                }
            }
        }

        Widgets.TreePop();
    }

    private static void DemoWindowWidgetsColorAndPickers()
    {
        if (!Widgets.TreeNode("Color/Picker Widgets"u8))
        {
            return;
        }

        Widgets.SeparatorText("Options"u8);
        _ = Widgets.CheckboxFlags("ColorEditFlags.NoAlpha"u8, ref _colorBaseFlags, ColorEditFlags.NoAlpha);
        _ = Widgets.CheckboxFlags("ColorEditFlags.AlphaOpaque"u8, ref _colorBaseFlags, ColorEditFlags.AlphaOpaque);
        _ = Widgets.CheckboxFlags("ColorEditFlags.AlphaNoBackground"u8, ref _colorBaseFlags, ColorEditFlags.AlphaNoBackground);
        _ = Widgets.CheckboxFlags("ColorEditFlags.AlphaPreviewHalf"u8, ref _colorBaseFlags, ColorEditFlags.AlphaPreviewHalf);
        _ = Widgets.CheckboxFlags("ColorEditFlags.NoDragDrop"u8, ref _colorBaseFlags, ColorEditFlags.NoDragDrop);
        _ = Widgets.CheckboxFlags("ColorEditFlags.NoOptions"u8, ref _colorBaseFlags, ColorEditFlags.NoOptions);
        _ = Widgets.CheckboxFlags("ColorEditFlags.HDR"u8, ref _colorBaseFlags, ColorEditFlags.HDR);

        Widgets.SeparatorText("Inline color editor"u8);
        Widgets.Text("Color widget:"u8);
        Widgets.SameLine();
        HelpMarker(@"Click on the color square to open a color picker.
Ctrl+Click on individual component to input value.
"u8);
        _ = Widgets.ColorEdit("MyColor##1"u8, ref _colorColor, _colorBaseFlags);

        Widgets.Text("Color widget HSV with Alpha:"u8);
        _ = Widgets.ColorEdit("MyColor##2"u8, ref _colorColor, ColorEditFlags.DisplayHSV | _colorBaseFlags);

        Widgets.Text("Color widget with Float Display:"u8);
        _ = Widgets.ColorEdit("MyColor##2f"u8, ref _colorColor, ColorEditFlags.Float | _colorBaseFlags);

        Widgets.Text("Color button with Picker:"u8);
        Widgets.SameLine();
        HelpMarker(@"With the ColorEditFlags.NoInputs flag you can hide all the slider/text inputs.
With the ColorEditFlags.NoLabel flag you can pass a non-empty label which will only be used for the tooltip and picker popup."u8);
        _ = Widgets.ColorEdit("MyColor##3"u8, ref _colorColor, ColorEditFlags.NoInputs | ColorEditFlags.NoLabel | _colorBaseFlags);

        Widgets.Text("Color button with Custom Picker Popup:"u8);

        var openPopup = Widgets.ColorButton("MyColor##3b"u8, _colorColor, _colorBaseFlags);
        Widgets.SameLine(0, Context.Style.ItemInnerSpacing.X);
        openPopup |= Widgets.Button("Palette"u8);
        if (openPopup)
        {
            Window.OpenPopup("mypicker"u8);
            _colorBackupColor = _colorColor;
        }

        if (Window.BeginPopup("mypicker"u8))
        {
            Widgets.Text("MY CUSTOM COLOR PICKER WITH AN ACTIVE PALETTE"u8);
            Widgets.Separator();
            _ = Widgets.ColorPicker("##picker"u8, ref _colorColor, ColorEditFlags.NoSidePreview | ColorEditFlags.NoSmallPreview | _colorBaseFlags);
            Widgets.SameLine();

            Widgets.BeginGroup();
            Widgets.Text("Current"u8);
            _ = Widgets.ColorButton("##current"u8, _colorColor, ColorEditFlags.NoPicker | ColorEditFlags.AlphaPreviewHalf, (60, 40));
            Widgets.Text("Previous"u8);
            if (Widgets.ColorButton("##previous"u8, _colorBackupColor, ColorEditFlags.NoPicker | ColorEditFlags.AlphaPreviewHalf, (60, 40)))
            {
                _colorColor = _colorBackupColor;
            }

            Widgets.Separator();
            Widgets.Text("Palette"u8);
            for (var n = 0; n < 32; n++)
            {
                Id.Push(n);
                if (n % 8 != 0)
                {
                    Widgets.SameLine(0.0f, Context.Style.ItemSpacing.Y);
                }

                ColorEditFlags paletteButtonFlags = ColorEditFlags.NoAlpha | ColorEditFlags.NoPicker | ColorEditFlags.NoTooltip;
                if (Widgets.ColorButton("##palette"u8, _colorSavedPalette[n], paletteButtonFlags, (20, 20)))
                {
                    _colorColor = new(_colorSavedPalette[n].Red, _colorSavedPalette[n].Green, _colorSavedPalette[n].Blue, _colorColor.Alpha);  // Preserve alpha!
                }

                // Allow user to drop colors into each palette entry. Note that ColorButton() is already a
                // drag source by default, unless specifying the ImGuiColorEditFlags_NoDragDrop flag.
                if (Widgets.BeginDragDropTarget())
                {
                    Payload payload = Widgets.AcceptDragDropPayload(Payload.TypeColor3F);
                    if (payload.IsValid)
                    {
                        _colorSavedPalette[n] = new(payload.GetData<ColorRGB>(), _colorSavedPalette[n].Alpha);
                    }

                    payload = Widgets.AcceptDragDropPayload(Payload.TypeColor4F);
                    if (payload.IsValid)
                    {
                        _colorSavedPalette[n] = payload.GetData<Color>();
                    }

                    Widgets.EndDragDropTarget();
                }

                Id.Pop();
            }

            Widgets.EndGroup();
            Window.EndPopup();
        }

        Widgets.Text("Color button only:"u8);
        _ = Widgets.Checkbox("ColorEditFlags.NoBorder"u8, ref _colorNoBorder);
        _ = Widgets.ColorButton("MyColor##3c"u8, _colorColor,
            _colorBaseFlags | (_colorNoBorder ? ColorEditFlags.NoBorder : ColorEditFlags.None), (80, 80));

        Id.Push("Color picker"u8);
        _ = Widgets.CheckboxFlags("No Alpha"u8, ref _colorColorPickerFlags, ColorEditFlags.NoAlpha);
        _ = Widgets.CheckboxFlags("Alpha Bar"u8, ref _colorColorPickerFlags, ColorEditFlags.AlphaBar);
        _ = Widgets.CheckboxFlags("No Side Preview"u8, ref _colorColorPickerFlags, ColorEditFlags.NoSidePreview);
        if (_colorColorPickerFlags.HasFlag(ColorEditFlags.NoSidePreview))
        {
            Widgets.SameLine();
            _ = Widgets.Checkbox("With Ref Color"u8, ref _colorRefColor);
            if (_colorRefColor)
            {
                Widgets.SameLine();
                _ = Widgets.ColorEdit("##RefColor"u8, ref _colorRefColorV, ColorEditFlags.NoInputs | _colorBaseFlags);
            }
        }

        _ = Widgets.Combo("Picker Mode"u8, ref _colorPickerMode, "Auto/Current\0ImGuiColorEditFlags_PickerHueBar\0ImGuiColorEditFlags_PickerHueWheel\0"u8);
        Widgets.SameLine();
        HelpMarker("When not specified explicitly, user can right-click the picker to change mode."u8);

        _ = Widgets.Combo("Display Mode"u8, ref _colorDisplayMode, "Auto/Current\0ImGuiColorEditFlags_NoInputs\0ImGuiColorEditFlags_DisplayRGB\0ImGuiColorEditFlags_DisplayHSV\0ImGuiColorEditFlags_DisplayHex\0"u8);
        Widgets.SameLine(); 
        HelpMarker(@"ColorEdit defaults to displaying RGB inputs if you don't specify a display mode, but the user can change it with a right-click on those inputs.

ColorPicker defaults to displaying RGB+HSV+Hex if you don't specify a display mode.

You can change the defaults using SetColorEditOptions()."u8);

        ColorEditFlags flags = _colorBaseFlags | _colorColorPickerFlags;
        if (_colorPickerMode == 1)
        {
            flags |= ColorEditFlags.PickerHueBar;
        }

        if (_colorPickerMode == 2)
        {
            flags |= ColorEditFlags.PickerHueWheel;
        }

        if (_colorDisplayMode == 1)
        {
            flags |= ColorEditFlags.NoInputs;       // Disable all RGB/HSV/Hex displays
        }

        if (_colorDisplayMode == 2)
        {
            flags |= ColorEditFlags.DisplayRGB;     // Override display mode
        }

        if (_colorDisplayMode == 3)
        {
            flags |= ColorEditFlags.DisplayHSV;
        }

        if (_colorDisplayMode == 4)
        {
            flags |= ColorEditFlags.DisplayHex;
        }

        _ = Widgets.ColorPicker("MyColor##4"u8, ref _colorColor, flags, _colorRefColor ? _colorRefColorV : null);

        Widgets.Text("Set defaults in code:"u8);
        Widgets.SameLine(); 
        HelpMarker(@"SetColorEditOptions() is designed to allow you to set boot-time default.
We don't have Push/Pop functions because you can force options on a per-widget basis if needed, and the user can change non-forced ones with the options menu.
We don't have a getter to avoid encouraging you to persistently save values that aren't forward-compatible."u8);
        if (Widgets.Button("Default: Uint8 + HSV + Hue Bar"u8))
        {
            Widgets.SetColorEditOptions(ColorEditFlags.Uint8 | ColorEditFlags.DisplayHSV | ColorEditFlags.PickerHueBar);
        }

        if (Widgets.Button("Default: Float + HDR + Hue Wheel"u8))
        {
            Widgets.SetColorEditOptions(ColorEditFlags.Float | ColorEditFlags.HDR | ColorEditFlags.PickerHueWheel);
        }

        // Always display a small version of both types of pickers
        // (that's in order to make it more visible in the demo to people who are skimming quickly through it)
        Widgets.Text("Both types:"u8);
        var w = (Window.ContentRegionAvail.Width - Context.Style.ItemSpacing.Y) * 0.40f;
        Style.SetNextItemWidth(w);
        _ = Widgets.ColorPicker("##MyColor##5"u8, ref _colorColor, ColorEditFlags.PickerHueBar | ColorEditFlags.NoSidePreview | ColorEditFlags.NoInputs | ColorEditFlags.NoAlpha);
        Widgets.SameLine();
        Style.SetNextItemWidth(w);
        _ = Widgets.ColorPicker("##MyColor##6"u8, ref _colorColor, ColorEditFlags.PickerHueWheel | ColorEditFlags.NoSidePreview | ColorEditFlags.NoInputs | ColorEditFlags.NoAlpha);
        Id.Pop();

        Widgets.Spacing();
        Widgets.Text("HSV encoded colors"u8);
        Widgets.SameLine(); 
        HelpMarker(@"By default, colors are given to ColorEdit and ColorPicker in RGB, but ImGuiColorEditFlags_InputHSV allows you to store colors as HSV and pass them to ColorEdit and ColorPicker as HSV. This comes with the added benefit that you can manipulate hue values with the picker even when saturation or value are zero."u8);
        Widgets.Text("Color widget with InputHSV:"u8);
        _ = Widgets.ColorEdit("HSV shown as RGB##1"u8, _colorColorHsv, ColorEditFlags.DisplayRGB | ColorEditFlags.InputHSV | ColorEditFlags.Float);
        _ = Widgets.ColorEdit("HSV shown as HSV##1"u8, _colorColorHsv, ColorEditFlags.DisplayHSV | ColorEditFlags.InputHSV | ColorEditFlags.Float);
        _ = Widgets.Drag("Raw HSV values"u8, _colorColorHsv, 0.01f, 0.0f, 1.0f);

        Widgets.TreePop();
    }

    private static void DemoWindowWidgetsComboBoxes()
    {
        if (!Widgets.TreeNode("Combo"u8))
        {
            return;
        }

        // Combo Boxes are also called "Dropdown" in other systems
        // Expose flags as checkbox for the demo
        _ = Widgets.CheckboxFlags("ComboFlags.PopupAlignLeft"u8, ref _comboFlags, ComboFlags.PopupAlignLeft);
        Widgets.SameLine();
        HelpMarker("Only makes a difference if the popup is larger than the combo"u8);
        if (Widgets.CheckboxFlags("ComboFlags.NoArrowButton"u8, ref _comboFlags, ComboFlags.NoArrowButton))
        {
            _comboFlags &= ~ComboFlags.NoPreview;
        }

        if (Widgets.CheckboxFlags("ComboFlags.NoPreview"u8, ref _comboFlags, ComboFlags.NoPreview))
        {
            _comboFlags &= ~(ComboFlags.NoArrowButton | ComboFlags.WidthFitPreview);
        }

        if (Widgets.CheckboxFlags("ComboFlags.WidthFitPreview"u8, ref _comboFlags, ComboFlags.WidthFitPreview))
        {
            _comboFlags &= ~ComboFlags.NoPreview;
        }

        // Override default popup height
        if (Widgets.CheckboxFlags("ComboFlags.HeightSmall"u8, ref _comboFlags, ComboFlags.HeightSmall))
        {
            _comboFlags &= ~(ComboFlags.HeightMask & ~ComboFlags.HeightSmall);
        }

        if (Widgets.CheckboxFlags("ComboFlags.HeightRegular"u8, ref _comboFlags, ComboFlags.HeightRegular))
        {
            _comboFlags &= ~(ComboFlags.HeightMask & ~ComboFlags.HeightRegular);
        }

        if (Widgets.CheckboxFlags("ComboFlags.HeightLargest"u8, ref _comboFlags, ComboFlags.HeightLargest))
        {
            _comboFlags &= ~(ComboFlags.HeightMask & ~ComboFlags.HeightLargest);
        }

        // Using the generic BeginCombo() API, you have full control over how to display the combo contents.
        // (your selection data could be an index, a pointer to the object, an id for the object, a flag intrusively
        // stored in the object itself, etc.)

        // Pass in the preview value visible before opening the combo (it could technically be different contents or not pulled from items[])
        var comboPreviewValue = _comboItems[_comboItemCurrent];
        if (Widgets.BeginCombo("combo 1"u8, comboPreviewValue, _comboFlags))
        {
            for (var i = 0; i < _comboItems.Length; i++)
            {
                var isSelected = _comboItemCurrent == i;
                if (Widgets.Selectable(_comboItems[i], isSelected))
                {
                    _comboItemCurrent = i;
                }

                if (isSelected)
                {
                    Widgets.SetItemDefaultFocus();
                }
            }

            Widgets.EndCombo();
        }

        // Show case embedding a filter using a simple trick: displaying the filter inside combo contents.
        // See https://github.com/ocornut/imgui/issues/718 for advanced/esoteric alternatives.
        if (Widgets.BeginCombo("combo 2 (w/ filter)"u8, comboPreviewValue, _comboFlags))
        {
            if (Window.IsAppearing)
            {
                Widgets.SetKeyboardFocusHere();
                _comboTextFilter.Clear();
            }

            Context.SetNextItemShortcut(Key.ModCtrl | Key.F);
            _ = _comboTextFilter.Draw("##Filter"u8, -float.Epsilon);

            for (var n = 0; n < _comboItems.Length; n++)
            {
                var is_selected = _comboItemCurrent == n;
                if (_comboTextFilter.PassFilter(_comboItems[n]))
                {
                    if (Widgets.Selectable(_comboItems[n], is_selected))
                    {
                        _comboItemCurrent = n;
                    }
                }
            }

            Widgets.EndCombo();
        }

        Widgets.Spacing();
        Widgets.SeparatorText("One-liner variants"u8);
        HelpMarker(@"The Combo() function is not greatly useful apart from cases were you want to embed all options in a single strings.
Flags above don't apply to this section."u8);

        // Combo with null-separated items string
        _ = Widgets.Combo("combo 3 (one-liner)"u8, ref _comboItemCurrent2, "aaaa\0bbbb\0cccc\0dddd\0eeee\0"u8);

        // Combo with items
        _ = Widgets.Combo("combo 4 (array)"u8, ref _comboItemCurrent3, _comboItems);

        // Combo with accessor function
        _ = Widgets.Combo("combo 5 (with height)"u8, ref _comboItemCurrent4, (items, item) => items[item], _comboItems, _comboItems.Length);

        Widgets.TreePop();
    }

    private static void DemoWindowWidgetsDataTypes()
    {
        if (!Widgets.TreeNode("Data Types"u8))
        {
            return;
        }

        Widgets.SeparatorText("Drags"u8);

        _ = Widgets.Checkbox("Clamp integers to 0..50"u8, ref _dataDragClamp);
        Widgets.SameLine(); 
        HelpMarker(@"As with every widget in dear imgui, we never modify values unless there is a user interaction.
You can override the clamping limits by using Ctrl+Click to input a value."u8);

        _ = Widgets.Drag("drag s8"u8, ref _dataS8, 0.2f, _dataDragClamp ? (sbyte)0 : sbyte.MinValue, _dataDragClamp ? (sbyte)50 : sbyte.MaxValue);
        _ = Widgets.Drag("drag u8"u8, ref _dataU8, 0.2f, _dataDragClamp ? (byte)0 : byte.MinValue, _dataDragClamp ? (byte)50 : byte.MaxValue, "%u ms"u8);
        _ = Widgets.Drag("drag s16"u8, ref _dataS16, 0.2f, _dataDragClamp ? (short)0 : short.MinValue, _dataDragClamp ? (short)50 : short.MaxValue);
        _ = Widgets.Drag("drag u16"u8, ref _dataU16, 0.2f, _dataDragClamp ? (ushort)0 : ushort.MinValue, _dataDragClamp ? (ushort)50 : ushort.MaxValue, "%u ms"u8);
        _ = Widgets.Drag("drag s32"u8, ref _dataS32, 0.2f, _dataDragClamp ? 0 : int.MinValue, _dataDragClamp ? 50 : int.MaxValue);
        _ = Widgets.Drag("drag s32 hex"u8, ref _dataS32, 0.2f, _dataDragClamp ? 0 : int.MinValue, _dataDragClamp ? 50 : int.MaxValue, "0x%08X"u8);
        _ = Widgets.Drag("drag u32"u8, ref _dataU32, 0.2f, _dataDragClamp ? 0 : uint.MinValue, _dataDragClamp ? 50 : uint.MaxValue, "%u"u8);
        _ = Widgets.Drag("drag s64"u8, ref _dataS64, 0.2f, _dataDragClamp ? 0 : long.MinValue, _dataDragClamp ? 50 : long.MaxValue);
        _ = Widgets.Drag("drag u64"u8, ref _dataU64, 0.2f, _dataDragClamp ? 0 : ulong.MinValue, _dataDragClamp ? 50 : ulong.MaxValue);
        _ = Widgets.Drag("drag float"u8, ref _dataF32, 0.005f, 0.0f, 1.0f, "%f"u8);
        _ = Widgets.Drag("drag float log"u8, ref _dataF32, 0.005f, 0.0f, 10.0f, "%f"u8, SliderFlags.Logarithmic);
        _ = Widgets.Drag("drag double"u8, ref _dataF64, 0.0005f, 0.0d, double.MaxValue, "%.10f grams"u8);
        _ = Widgets.Drag("drag double log"u8, ref _dataF64, 0.0005f, 0.0d, 10.0d, "0 < %.10f < 1"u8, SliderFlags.Logarithmic);

        Widgets.SeparatorText("Sliders"u8);

        _ = Widgets.Slider("slider s8 full"u8, ref _dataS8, sbyte.MinValue, sbyte.MaxValue, "%d"u8);
        _ = Widgets.Slider("slider u8 full"u8, ref _dataU8, byte.MinValue, byte.MaxValue, "%u"u8);
        _ = Widgets.Slider("slider s16 full"u8, ref _dataS16, short.MinValue, short.MaxValue, "%d"u8);
        _ = Widgets.Slider("slider u16 full"u8, ref _dataU16, ushort.MinValue, ushort.MaxValue, "%u"u8);
        _ = Widgets.Slider("slider s32 low"u8, ref _dataS32, 0, 50, "%d"u8);
        _ = Widgets.Slider("slider s32 high"u8, ref _dataS32, (int.MaxValue / 2) - 100, int.MaxValue / 2, "%d"u8);
        _ = Widgets.Slider("slider s32 full"u8, ref _dataS32, int.MinValue / 2, int.MaxValue / 2, "%d"u8);
        _ = Widgets.Slider("slider s32 hex"u8, ref _dataS32, 0, 50, "0x%04X"u8);
        _ = Widgets.Slider("slider u32 low"u8, ref _dataU32, 0, 50, "%u"u8);
        _ = Widgets.Slider("slider u32 high"u8, ref _dataU32, (uint.MaxValue / 2) - 100, uint.MaxValue / 2, "%u"u8);
        _ = Widgets.Slider("slider u32 full"u8, ref _dataU32, uint.MinValue / 2, uint.MaxValue / 2, "%u"u8);
        _ = Widgets.Slider("slider s64 low"u8, ref _dataS64, 0, 50, "%lld"u8);
        _ = Widgets.Slider("slider s64 high"u8, ref _dataS64, (long.MaxValue / 2) - 100, long.MaxValue / 2, "%lld"u8);
        _ = Widgets.Slider("slider s64 full"u8, ref _dataS64, long.MinValue / 2, long.MaxValue / 2, "%lld"u8);
        _ = Widgets.Slider("slider u64 low"u8, ref _dataU64, 0, 50, "%llu ms"u8);
        _ = Widgets.Slider("slider u64 high"u8, ref _dataU64, (ulong.MaxValue / 2) - 100, ulong.MaxValue / 2, "%llu ms"u8);
        _ = Widgets.Slider("slider u64 full"u8, ref _dataU64, ulong.MinValue / 2, ulong.MaxValue / 2, "%llu ms"u8);
        _ = Widgets.Slider("slider float low"u8, ref _dataF32, 0.0f, 1.0f);
        _ = Widgets.Slider("slider float low log"u8, ref _dataF32, 0.0f, 1.0f, "%.10f"u8, SliderFlags.Logarithmic);
        _ = Widgets.Slider("slider float high"u8, ref _dataF32, -10000000000.0f, +10000000000.0f, "%e"u8);
        _ = Widgets.Slider("slider double low"u8, ref _dataF64, 0.0d, 1.0d, "%.10f grams"u8);
        _ = Widgets.Slider("slider double low log"u8, ref _dataF64, 0.0d, 1.0d, "%.10f"u8, SliderFlags.Logarithmic);
        _ = Widgets.Slider("slider double high"u8, ref _dataF64, -1000000000000000.0, +1000000000000000.0, "%e grams"u8);

        Widgets.SeparatorText("Sliders (reverse)"u8);

        _ = Widgets.Slider("slider s8 reverse"u8, ref _dataS8, sbyte.MaxValue, sbyte.MinValue, "%d"u8);
        _ = Widgets.Slider("slider u8 reverse"u8, ref _dataU8, byte.MaxValue, byte.MinValue, "%u"u8);
        _ = Widgets.Slider("slider s32 reverse"u8, ref _dataS32, 50, 0, "%d"u8);
        _ = Widgets.Slider("slider u32 reverse"u8, ref _dataU32, 50, 0, "%u"u8);
        _ = Widgets.Slider("slider s64 reverse"u8, ref _dataS64, 50, 0, "%lld"u8);
        _ = Widgets.Slider("slider u64 reverse"u8, ref _dataU64, 50, 0, "%llu ms"u8);

        Widgets.SeparatorText("Inputs"u8);
        _ = Widgets.Checkbox("Show step buttons"u8, ref _dataInputsStep);
        _ = Widgets.CheckboxFlags("InputTextFlags.ReadOnly"u8, ref _dataFlags, InputTextFlags.ReadOnly);
        _ = Widgets.CheckboxFlags("InputTextFlags.ParseEmptyRefVal"u8, ref _dataFlags, InputTextFlags.ParseEmptyRefVal);
        _ = Widgets.CheckboxFlags("InputTextFlags.DisplayEmptyRefVal"u8, ref _dataFlags, InputTextFlags.DisplayEmptyRefVal);
        _ = Widgets.Input("input s8"u8, ref _dataS8, _dataInputsStep ? (sbyte)1 : (sbyte)0, 0, "%d"u8, _dataFlags);
        _ = Widgets.Input("input u8"u8, ref _dataU8, _dataInputsStep ? (byte)1 : (byte)0, 0, "%u"u8, _dataFlags);
        _ = Widgets.Input("input s16"u8, ref _dataS16, _dataInputsStep ? (short)1 : (short)0, 0, "%d"u8, _dataFlags);
        _ = Widgets.Input("input u16"u8, ref _dataU16, _dataInputsStep ? (ushort)1 : (ushort)0, 0, "%u"u8, _dataFlags);
        _ = Widgets.Input("input s32"u8, ref _dataS32, _dataInputsStep ? 1 : 0, 0, "%d"u8, _dataFlags);
        _ = Widgets.Input("input s32 hex"u8, ref _dataS32, _dataInputsStep ? 1 : 0, 0, "%04X"u8, _dataFlags | InputTextFlags.CharsHexadecimal);
        _ = Widgets.Input("input u32"u8, ref _dataU32, _dataInputsStep ? 1u : 0u, 0, "%u"u8, _dataFlags);
        _ = Widgets.Input("input u32 hex"u8, ref _dataU32, _dataInputsStep ? 1u : 0u, 0, "%08X"u8, _dataFlags | InputTextFlags.CharsHexadecimal);
        _ = Widgets.Input("input s64"u8, ref _dataS64, _dataInputsStep ? 1L : 0L, 0, "%lld"u8, _dataFlags);
        _ = Widgets.Input("input u64"u8, ref _dataU64, _dataInputsStep ? 1UL : 0UL, 0, "%llu"u8, _dataFlags);
        _ = Widgets.Input("input float"u8, ref _dataF32, _dataInputsStep ? 1f : 0f, 0, default, _dataFlags);
        _ = Widgets.Input("input double"u8, ref _dataF64, _dataInputsStep ? 1.0 : 0.0, 0, default, _dataFlags);

        Widgets.TreePop();
    }

    private static void ShowUserGuide()
    {
        IO io = Context.IO;
        Widgets.BulletText("Double-click on title bar to collapse window."u8);
        Widgets.BulletText(
            "Click and drag on lower corner or border to resize window.\n"u8 +    
            "(double-click to auto fit window to its contents)"u8);
        Widgets.BulletText("Ctrl+Click on a slider or drag box to input value as text."u8);
        Widgets.BulletText("Tab/Shift+Tab to cycle through keyboard editable fields."u8);
        Widgets.BulletText("Ctrl+Tab/Ctrl+Shift+Tab to focus windows."u8);
        if (io.FontAllowUserScaling)
        {
            Widgets.BulletText("Ctrl+Mouse Wheel to zoom window contents."u8);
        }

        Widgets.BulletText("While inputting text:\n"u8);
        Widgets.Indent();
        Widgets.BulletText("Ctrl+Left/Right to word jump."u8);
        Widgets.BulletText("Ctrl+A or double-click to select all."u8);
        Widgets.BulletText("Ctrl+X/C/V to use clipboard cut/copy/paste."u8);
        Widgets.BulletText("Ctrl+Z to undo, Ctrl+Y/Ctrl+Shift+Z to redo."u8);
        Widgets.BulletText("Escape to revert."u8);
        Widgets.Unindent();
        Widgets.BulletText("With keyboard navigation enabled:"u8);
        Widgets.Indent();
        Widgets.BulletText("Arrow keys or Home/End/PageUp/PageDown to navigate."u8);
        Widgets.BulletText("Space to activate a widget."u8);
        Widgets.BulletText("Return to input text into a widget."u8);
        Widgets.BulletText("Escape to deactivate a widget, close popup,\nexit a child window or the menu layer, clear focus."u8);
        Widgets.BulletText("Alt to jump to the menu layer of a window."u8);
        Widgets.Unindent();
    }

    // Note that shortcuts are currently provided for display only
    // (future version will add explicit flags to BeginMenu() to request processing shortcuts)
    private static void ShowExampleMenuFile()
    {
        _ = Widgets.MenuItem("(demo menu)"u8, null, false, false);
        if (Widgets.MenuItem("New"u8))
        {
        }

        if (Widgets.MenuItem("Open"u8, "Ctrl+O"u8))
        {
        }

        if (Widgets.BeginMenu("Open Recent"u8))
        {
            _ = Widgets.MenuItem("fish_hat.c"u8);
            _ = Widgets.MenuItem("fish_hat.inl"u8);
            _ = Widgets.MenuItem("fish_hat.h"u8);
            if (Widgets.BeginMenu("More.."u8))
            {
                _ = Widgets.MenuItem("Hello"u8);
                _ = Widgets.MenuItem("Sailor"u8);
                if (Widgets.BeginMenu("Recurse.."u8))
                {
                    ShowExampleMenuFile();
                    Widgets.EndMenu();
                }

                Widgets.EndMenu();
            }

            Widgets.EndMenu();
        }

        if (Widgets.MenuItem("Save"u8, "Ctrl+S"u8))
        {
        }

        if (Widgets.MenuItem("Save As.."u8))
        {
        }

        Widgets.Separator();
        if (Widgets.BeginMenu("Options"u8))
        {
            _ = Widgets.MenuItem("Enabled"u8, ""u8, ref _options_enabled);
            _ = Widgets.BeginChild("child"u8, (0, 60), ChildFlags.Borders);
            for (var i = 0; i < 10; i++)
            {
                Widgets.Text($"Scrolling Text {i}".ToUtf8());
            }

            Widgets.EndChild();
            _ = Widgets.Slider("Value"u8, ref _options_f, 0.0f, 1.0f);
            _ = Widgets.Input("Input"u8, ref _options_f, 0.1f);
            _ = Widgets.Combo("Combo"u8, ref _options_n, "Yes\0No\0Maybe\0\0"u8);
            Widgets.EndMenu();
        }

        if (Widgets.BeginMenu("Colors"u8))
        {
            var sz = Font.GetTextLineHeight();
            for (var i = (StyleColor)0; i < StyleColor.Max; i++)
            {
                ReadOnlySpan<byte> name = Style.GetStyleColorName(i);
                Point p = Window.CursorScreenPosition;
                Window.DrawList?.AddRectFilled((p, (p.X + sz, p.Y + sz)), Color.GetColorU32(i));
                Widgets.Dummy((sz, sz));
                Widgets.SameLine();
                _ = Widgets.MenuItem(name);
            }

            Widgets.EndMenu();
        }

        // Here we demonstrate appending again to the "Options" menu (which we already created above)
        // Of course in this demo it is a little bit silly that this function calls BeginMenu("Options") twice.
        // In a real code-base using it would make senses to use this feature from very different code locations.
        if (Widgets.BeginMenu("Options"u8)) // <-- Append!
        {
            _ = Widgets.Checkbox("SomeOption"u8, ref _options_b);
            Widgets.EndMenu();
        }

        if (Widgets.BeginMenu("Disabled"u8, false)) // Disabled
        {
            throw new InvalidOperationException();
        }

        if (Widgets.MenuItem("Checked"u8, null, true))
        {
        }

        Widgets.Separator();
        if (Widgets.MenuItem("Quit"u8, "Alt+F4"u8))
        {
        }
    }

    // Nested Types

    private enum PlotType
    {
        Sin,
        Saw
    }

    // Public Methods

    /// <summary>
    /// Shows the widgets demo section.
    /// </summary>
    public static void DemoWindowWidgets()
    {
        if (!Widgets.CollapsingHeader("Widgets"u8))
        {
            return;
        }

        var disableAll = DisableSections;
        if (disableAll)
        {
            Widgets.BeginDisabled();
        }

        DemoWindowWidgetsBasic();
        DemoWindowWidgetsBullets();
        DemoWindowWidgetsCollapsingHeaders();
        DemoWindowWidgetsComboBoxes();
        DemoWindowWidgetsColorAndPickers();
        DemoWindowWidgetsDataTypes();

        if (disableAll)
        {
            Widgets.EndDisabled();
        }

        ShowDisableBlocks();

        if (disableAll)
        {
            Widgets.BeginDisabled();
        }

        ShowDragAndDrop();
        ShowDragsAndSliders();
        ShowFonts();
        ShowImages();
        ShowListBoxes();
        ShowMultiComponents();
        ShowPlotting();
        ShowProgressBars();
        ShowQueryingStatuses();
        ShowSelectables();
        ShowSelectionAndMultiSelect();
        ShowTabs();
        ShowText();
        ShowTextFilter();
        ShowTextInput();
        ShowTooltips();
        ShowTreeNodes();
        ShowVerticalSliders();

        if (disableAll)
        {
            Widgets.EndDisabled();
        }
    }

    // Helper Methods

    // Private Section Methods

    private static void ShowDisableBlocks()
    {
        if (Widgets.TreeNode("Disable block"u8))
        {
            var disableSections = DisableSections;
            if (Widgets.Checkbox("Disable entire section above"u8, ref disableSections))
            {
                DisableSections = disableSections;
            }

            Widgets.SameLine();
            HelpMarker("Demonstrate using BeginDisabled()/EndDisabled() across this section."u8);
            Widgets.TreePop();
        }
    }

    private static void ShowDragAndDrop()
    {
        if (Widgets.TreeNode("Drag and Drop"u8))
        {
            // Drag and drop section - simplified for now
            Widgets.BulletText("Drag and drop in standard widgets"u8);
            Widgets.Indent();

            ColorRGB col1 = (1.0f, 0.0f, 0.2f);
            Color col2 = (0.4f, 0.7f, 0.0f, 0.5f);
            _ = Widgets.ColorEdit("color 1"u8, ref col1);
            _ = Widgets.ColorEdit("color 2"u8, ref col2);

            Widgets.Unindent();

            Widgets.TreePop();
        }
    }

    private static void ShowDragsAndSliders()
    {
        if (Widgets.TreeNode("Drag/Slider Flags"u8))
        {
            // Clamping flags
            _ = Widgets.CheckboxFlags("SliderFlags.AlwaysClamp"u8, ref _dragsClampsFlags, SliderFlags.AlwaysClamp);
            Widgets.SameLine();
            HelpMarker("Always clamp value to min/max bounds (if any) when input manually with Ctrl+Click. By default Ctrl+Click allows going out of bounds."u8);
            _ = Widgets.CheckboxFlags("SliderFlags.Logarithmic"u8, ref _dragsClampsFlags, SliderFlags.Logarithmic);
            Widgets.SameLine();
            HelpMarker("Enable logarithmic editing (more precision for small values)."u8);
            _ = Widgets.CheckboxFlags("SliderFlags.NoRoundToFormat"u8, ref _dragsClampsFlags, SliderFlags.NoRoundToFormat);
            Widgets.SameLine();
            HelpMarker("Disable rounding underlying value to match precision of the display format string (e.g. %.3f values are rounded to those 3 digits)."u8);
            _ = Widgets.CheckboxFlags("SliderFlags.NoInput"u8, ref _dragsClampsFlags, SliderFlags.NoInput);
            Widgets.SameLine();
            HelpMarker("Disable Ctrl+Click or Enter key allowing to input text directly into the widget."u8);
            _ = Widgets.CheckboxFlags("SliderFlags.WrapAround"u8, ref _dragsClampsFlags, SliderFlags.WrapAround);
            Widgets.SameLine();
            HelpMarker("Enable wrapping around from max to min and from min to max (only supported by DragXXX() functions)."u8);

            // Drags
            _ = Widgets.Drag("DragFloat (0 -> 1)"u8, ref _dragsClampingValue1, 0.005f, 0.0f, 1.0f, "%.3f"u8, _dragsClampsFlags);
            _ = Widgets.Drag("DragFloat (0 -> +inf)"u8, ref _dragsClampingValue2, 0.005f, 0.0f, float.MaxValue, "%.3f"u8, _dragsClampsFlags);
            _ = Widgets.Drag("DragFloat (-inf -> 1)"u8, ref _dragsClampingValue3, 0.005f, float.MinValue, 1.0f, "%.3f"u8, _dragsClampsFlags);

            Widgets.TreePop();
        }
    }

    private static void ShowFonts()
    {
        if (Widgets.TreeNode("Fonts"u8))
        {
            Widgets.Text("This section is for font showcasing."u8);
            Widgets.Text("Font size is controlled via the IO object."u8);
            Widgets.TreePop();
        }
    }

    private static void ShowImages()
    {
        if (Widgets.TreeNode("Images"u8))
        {
            _ = Widgets.Checkbox("Use text color for Tint"u8, ref _imageUseTextColor);
            Widgets.Text("Image loading/display functionality would be demonstrated here."u8);
            Widgets.Text("This requires texture handling which varies by backend."u8);
            Widgets.TreePop();
        }
    }

    private static void ShowListBoxes()
    {
        if (Widgets.TreeNode("List boxes"u8))
        {
            // Using the simpler BeginListBox/EndListBox API
            string[] items = ["Apple", "Banana", "Cherry", "Kiwi", "Mango", "Orange", "Pineapple", "Strawberry", "Watermelon"];

            if (Widgets.BeginListBox("listbox 1"u8))
            {
                for (var i = 0; i < items.Length; i++)
                {
                    var isSelected = _listBoxItemCurrent == i;
                    if (Widgets.Selectable(System.Text.Encoding.UTF8.GetBytes(items[i]), isSelected))
                    {
                        _listBoxItemCurrent = i;
                    }

                    if (isSelected)
                    {
                        Widgets.SetItemDefaultFocus();
                    }
                }

                Widgets.EndListBox();
            }

            Widgets.Text(System.Text.Encoding.UTF8.GetBytes($"Selected: {items[_listBoxItemCurrent]}"));

            // Custom sized list box
            Widgets.Text("Full-width:"u8);
            if (Widgets.BeginListBox("##listbox 2"u8, new Vec2(-float.Epsilon, 5 * Font.GetTextLineHeightWithSpacing())))
            {
                for (var i = 0; i < items.Length; i++)
                {
                    var isSelected = _listBoxItemCurrent == i;
                    if (Widgets.Selectable(System.Text.Encoding.UTF8.GetBytes(items[i]), isSelected))
                    {
                        _listBoxItemCurrent = i;
                    }

                    if (isSelected)
                    {
                        Widgets.SetItemDefaultFocus();
                    }
                }

                Widgets.EndListBox();
            }

            Widgets.TreePop();
        }
    }

    private static void ShowMultiComponents()
    {
        if (Widgets.TreeNode("Multi-component Widgets"u8))
        {
            _ = Widgets.Input("input float4"u8, _multiVec4f);
            _ = Widgets.Drag("drag float4"u8, _multiVec4f, 0.01f, 0.0f, 1.0f);
            _ = Widgets.Slider("slider float4"u8, _multiVec4f, 0.0f, 1.0f);
            _ = Widgets.Input("input int4"u8, _multiVec4i);
            _ = Widgets.Drag("drag int4"u8, _multiVec4i, 1, 0, 255);
            _ = Widgets.Slider("slider int4"u8, _multiVec4i, 0, 255);
            Widgets.Spacing();

            _ = Widgets.Input("input float3"u8, _multiVec4f.AsSpan(0, 3));
            _ = Widgets.Drag("drag float3"u8, _multiVec4f.AsSpan(0, 3), 0.01f, 0.0f, 1.0f);
            _ = Widgets.Slider("slider float3"u8, _multiVec4f.AsSpan(0, 3), 0.0f, 1.0f);

            Widgets.TreePop();
        }
    }

    private static void ShowPlotting()
    {
        if (Widgets.TreeNode("Plotting"u8))
        {
            _ = Widgets.Checkbox("Animate"u8, ref _plotAnimate);

            // Plot lines - using sin array
            Widgets.PlotLines("Frame Times"u8, _plotArrSin, 0, "avg 0.0"u8);

            // Fill the array with animated data
            if (_plotAnimate)
            {
                var time = Context.GetTime();
                if (_plotRefreshTime == 0.0)
                {
                    _plotRefreshTime = time;
                }

                while (_plotRefreshTime < time)
                {
                    _plotArrSin[_plotValuesOffset] = MathF.Cos(_plotPhase);
                    _plotValuesOffset = (_plotValuesOffset + 1) % _plotArrSin.Length;
                    _plotPhase += 0.10f * _plotValuesOffset;
                    _plotRefreshTime += 1.0 / 60.0;
                }
            }

            // Plot histogram
            Widgets.PlotHistogram("Histogram"u8, _plotArrCos, 0, default, -1.0f, 1.0f, new Vec2(0, 80.0f));

            Widgets.Separator();

            // Plot type selection
            _ = Widgets.RadioButton("Sin"u8, ref Unsafe.As<PlotType, int>(ref _plotFuncType), 0);
            Widgets.SameLine();
            _ = Widgets.RadioButton("Saw"u8, ref Unsafe.As<PlotType, int>(ref _plotFuncType), 1);

            _ = Widgets.Slider("Sample count"u8, ref _plotDisplayCount, 1, 400);

            // Generate data based on plot type
            var data = new float[_plotDisplayCount];
            for (var i = 0; i < _plotDisplayCount; i++)
            {
                data[i] = _plotFuncType == PlotType.Sin ? MathF.Sin(i * 0.1f) : (i & 1) == 1 ? 1.0f : -1.0f;
            }

            Widgets.PlotLines("Lines"u8, data, 0, default, -1.0f, 1.0f, new Vec2(0, 80));
            Widgets.PlotHistogram("Histogram"u8, data, 0, default, -1.0f, 1.0f, new Vec2(0, 80));

            Widgets.TreePop();
        }
    }

    private static void ShowProgressBars()
    {
        if (Widgets.TreeNode("Progress Bars"u8))
        {
            // Animate a simple progress bar
            if (_progressAnimate)
            {
                _progressProgress += _progressProgressDir * 0.4f * Context.IO.DeltaTime;
                if (_progressProgress >= +1.1f)
                {
                    _progressProgress = +1.1f;
                    _progressProgressDir *= -1.0f;
                }

                if (_progressProgress <= -0.1f)
                {
                    _progressProgress = -0.1f;
                    _progressProgressDir *= -1.0f;
                }
            }

            // Default progress bar
            Widgets.ProgressBar(_progressProgress, default, default);
            Widgets.SameLine(0.0f, Context.Style.ItemInnerSpacing.X);
            Widgets.Text("Progress Bar"u8);

            var progressSaturated = Math.Clamp(_progressProgress, 0.0f, 1.0f);
            Span<byte> buf = stackalloc byte[32];
            var len = System.Text.Encoding.UTF8.GetBytes($"{(int)(progressSaturated * 1753)}/1753", buf);
            Widgets.ProgressBar(_progressProgress, new Size(0.0f, 0.0f), buf[..len]);

            Widgets.TreePop();
        }
    }

    private static void ShowQueryingStatuses()
    {
        if (Widgets.TreeNode("Querying Item Status (Edited/Active/Hovered etc.)"u8))
        {
            // Select an item type
            _ = Widgets.Combo("Item Type"u8, ref _queryingItemType, "Text\0Button\0Button (w/ repeat)\0Checkbox\0SliderFloat\0InputText\0InputTextMultiline\0InputFloat\0InputFloat3\0ColorEdit4\0Selectable\0MenuItem\0TreeNode\0TreeNode (w/ double-click)\0Combo\0ListBox\0"u8);
            Widgets.SameLine();
            HelpMarker("Testing how various types of items are interacting with the IsItemXXX functions. Note that the bool return value of most ImGui function is generally equivalent to calling ImGui::IsItemClicked()."u8);

            // Submit selected item type
            var itemDisabled = false;
            var ret = false;
            Id.Push(_queryingItemType);
            if (_queryingItemType == 0)
            {
                Widgets.Text("ITEM: Text"u8);
                ret = true;
            }
            else if (_queryingItemType == 1)
            {
                ret = Widgets.Button("ITEM: Button"u8);
            }
            else if (_queryingItemType == 2)
            {
                Style.PushItemFlag(ItemFlags.ButtonRepeat, true);
                ret = Widgets.Button("ITEM: Button"u8);
                Style.PopItemFlag();
            }
            else if (_queryingItemType == 3)
            {
                ret = Widgets.Checkbox("ITEM: Checkbox"u8, ref _queryingB);
            }
            else if (_queryingItemType == 4)
            {
                ret = Widgets.Slider("ITEM: SliderFloat"u8, ref _queryingF, 0.0f, 1.0f);
            }
            else if (_queryingItemType == 5)
            {
                ret = Widgets.InputText("ITEM: InputText"u8, _queryingStr);
            }
            else if (_queryingItemType == 6)
            {
                ret = Widgets.InputTextMultiline("ITEM: InputTextMultiline"u8, _queryingStr, new Size(200, 100));
            }
            else if (_queryingItemType == 7)
            {
                ret = Widgets.Input("ITEM: InputFloat"u8, ref _queryingF, 1.0f);
            }
            else if (_queryingItemType == 8)
            {
                ret = Widgets.Input("ITEM: InputFloat3"u8, _queryingFArray);
            }
            else if (_queryingItemType == 9)
            {
                ret = Widgets.ColorEdit("ITEM: ColorEdit4"u8, ref _queryingCol4f);
            }
            else if (_queryingItemType == 10)
            {
                ret = Widgets.Selectable("ITEM: Selectable"u8, false);
            }
            else if (_queryingItemType == 11)
            {
                ret = Widgets.MenuItem("ITEM: MenuItem"u8);
            }
            else if (_queryingItemType == 12)
            {
                ret = Widgets.TreeNode("ITEM: TreeNode"u8);
                if (ret)
                {
                    Widgets.TreePop();
                }
            }
            else if (_queryingItemType == 13)
            {
                ret = Widgets.TreeNode("ITEM: TreeNode w/ TreeNodeFlags.OpenOnDoubleClick"u8, TreeNodeFlags.OpenOnDoubleClick | TreeNodeFlags.NoTreePushOnOpen);
            }
            else if (_queryingItemType == 14)
            {
                ret = Widgets.BeginCombo("ITEM: Combo"u8, "preview"u8);
                if (ret)
                {
                    Widgets.EndCombo();
                }
            }
            else if (_queryingItemType == 15)
            {
                _ = Widgets.BeginListBox("ITEM: ListBox"u8);
                Widgets.EndListBox();
                ret = true;
            }

            Id.Pop();

            if (itemDisabled)
            {
                Widgets.EndDisabled();
            }

            // Display item status
            var hoveredDelayNone = Widgets.IsItemHovered();
            var hoveredDelayShort = Widgets.IsItemHovered(HoveredFlags.DelayShort);
            var hoveredDelayNormal = Widgets.IsItemHovered(HoveredFlags.DelayNormal);
            var hoveredNoNav = Widgets.IsItemHovered(HoveredFlags.NoNavOverride);

            Widgets.Text(System.Text.Encoding.UTF8.GetBytes(
                $"Return value = {ret}\n" +
                $"IsItemFocused() = {Widgets.IsItemFocused()}\n" +
                $"IsItemHovered() = {Widgets.IsItemHovered()}\n" +
                $"IsItemHovered(_AllowWhenBlockedByPopup) = {Widgets.IsItemHovered(HoveredFlags.AllowWhenBlockedByPopup)}\n" +
                $"IsItemHovered(_AllowWhenBlockedByActiveItem) = {Widgets.IsItemHovered(HoveredFlags.AllowWhenBlockedByActiveItem)}\n" +
                $"IsItemHovered(_AllowWhenOverlappedByItem) = {Widgets.IsItemHovered(HoveredFlags.AllowWhenOverlappedByItem)}\n" +
                $"IsItemHovered(_AllowWhenOverlappedByWindow) = {Widgets.IsItemHovered(HoveredFlags.AllowWhenOverlappedByWindow)}\n" +
                $"IsItemHovered(_AllowWhenDisabled) = {Widgets.IsItemHovered(HoveredFlags.AllowWhenDisabled)}\n" +
                $"IsItemHovered(_RectOnly) = {Widgets.IsItemHovered(HoveredFlags.RectOnly)}\n" +
                $"IsItemActive() = {Widgets.IsItemActive()}\n" +
                $"IsItemEdited() = {Widgets.IsItemEdited()}\n" +
                $"IsItemActivated() = {Widgets.IsItemActivated()}\n" +
                $"IsItemDeactivated() = {Widgets.IsItemDeactivated()}\n" +
                $"IsItemDeactivatedAfterEdit() = {Widgets.IsItemDeactivatedAfterEdit()}\n" +
                $"IsItemVisible() = {Widgets.IsItemVisible()}\n" +
                $"IsItemClicked() = {Widgets.IsItemClicked()}\n" +
                $"IsItemToggledOpen() = {Widgets.IsItemToggledOpen()}\n" +
                $"GetItemRectSize() = ({Widgets.GetItemRectSize().Width:F1}, {Widgets.GetItemRectSize().Height:F1})"
            ));

            Widgets.Text(System.Text.Encoding.UTF8.GetBytes(
                $"w/ Hovering Delay: None = {hoveredDelayNone}, Short = {hoveredDelayShort}, Normal = {hoveredDelayNormal}"
            ));

            Widgets.TreePop();
        }
    }

    private static void ShowSelectables()
    {
        if (Widgets.TreeNode("Selectables"u8))
        {
            // Basic
            if (Widgets.TreeNode("Basic"u8))
            {
                _ = Widgets.Selectable("1. I am selectable"u8, ref _selectablesBasic[0]);
                _ = Widgets.Selectable("2. I am selectable"u8, ref _selectablesBasic[1]);
                Widgets.Text("(I am not selectable)"u8);
                _ = Widgets.Selectable("4. I am selectable"u8, ref _selectablesBasic[3]);
                if (Widgets.Selectable("5. I am double clickable"u8, _selectablesBasic[4], SelectableFlags.AllowDoubleClick))
                {
                    if (Context.IsMouseDoubleClicked(MouseButton.Left))
                    {
                        _selectablesBasic[4] = !_selectablesBasic[4];
                    }
                }

                Widgets.TreePop();
            }

            // Selection state - single selection
            if (Widgets.TreeNode("Selection State: Single Selection"u8))
            {
                for (var i = 0; i < 5; i++)
                {
                    if (Widgets.Selectable(System.Text.Encoding.UTF8.GetBytes($"Object {i}"), _selectablesSelectedSingleOnly == i))
                    {
                        _selectablesSelectedSingleOnly = i;
                    }
                }

                Widgets.TreePop();
            }

            // Selection state - multiple selection
            if (Widgets.TreeNode("Selection State: Multiple Selection"u8))
            {
                HelpMarker("Hold Ctrl and click to select multiple items."u8);
                for (var i = 0; i < 5; i++)
                {
                    if (Widgets.Selectable(System.Text.Encoding.UTF8.GetBytes($"Object {i}"), _selectablesSelected[i]))
                    {
                        if (!Context.IsKeyDown(Key.LeftCtrl) && !Context.IsKeyDown(Key.RightCtrl))
                        {
                            Array.Clear(_selectablesSelected);
                        }

                        _selectablesSelected[i] ^= true;
                    }
                }

                Widgets.TreePop();
            }

            // Rendering more items on the same line
            if (Widgets.TreeNode("Rendering more items on the same line"u8))
            {
                _ = Widgets.Selectable("main.c"u8, ref _selectablesSelectedSameLineFirst[0]);
                Widgets.SameLine(300);
                Widgets.Text(" 2,345 bytes"u8);
                _ = Widgets.Selectable("Hello.cpp"u8, ref _selectablesSelectedSameLineFirst[1]);
                Widgets.SameLine(300);
                Widgets.Text("12,345 bytes"u8);
                _ = Widgets.Selectable("Hello.h"u8, ref _selectablesSelectedSameLineFirst[2]);
                Widgets.SameLine(300);
                Widgets.Text(" 2,345 bytes"u8);
                Widgets.TreePop();
            }

            // In columns
            if (Widgets.TreeNode("In columns"u8))
            {
                if (Widgets.BeginTable("split1"u8, 3, TableFlags.Resizable | TableFlags.NoSavedSettings | TableFlags.Borders))
                {
                    for (var i = 0; i < 10; i++)
                    {
                        _ = Widgets.TableNextColumn();
                        _ = Widgets.Selectable(System.Text.Encoding.UTF8.GetBytes($"Item {i}"), ref _selectablesSelectedSameLineSecond[i % 3]);
                    }

                    Widgets.EndTable();
                }

                Widgets.Separator();

                if (Widgets.BeginTable("split2"u8, 3, TableFlags.Resizable | TableFlags.NoSavedSettings | TableFlags.Borders))
                {
                    for (var i = 0; i < 10; i++)
                    {
                        Widgets.TableNextRow();
                        _ = Widgets.TableNextColumn();
                        _ = Widgets.Selectable(System.Text.Encoding.UTF8.GetBytes($"Item {i}"), ref _selectablesSelectedSameLineThird[i % 3], SelectableFlags.SpanAllColumns);
                        _ = Widgets.TableNextColumn();
                        Widgets.Text("Some text"u8);
                        _ = Widgets.TableNextColumn();
                        Widgets.Text("123456"u8);
                    }

                    Widgets.EndTable();
                }

                Widgets.TreePop();
            }

            // Grid
            if (Widgets.TreeNode("Grid"u8))
            {
                var winningState = 0.0f;
                for (var i = 0; i < 16; i++)
                {
                    winningState += _selectablesGrid[i] ? 1.0f : 0.0f;
                }

                Widgets.Text(System.Text.Encoding.UTF8.GetBytes($"Squares clicked: {(int)winningState}/16"));

                var spacing = Context.Style.ItemInnerSpacing.X;
                Style.PushStyleVar(StyleVariable.ItemSpacing, new Vec2(spacing, spacing));
                for (var i = 0; i < 16; i++)
                {
                    Id.Push(i);
                    if (Widgets.Selectable("##square"u8, _selectablesGrid[i], SelectableFlags.None, new Size(50, 50)))
                    {
                        _selectablesGrid[i] = !_selectablesGrid[i];

                        // Toggle neighbors
                        var x = i % 4;
                        var y = i / 4;
                        if (x > 0)
                        {
                            _selectablesGrid[i - 1] ^= true;
                        }

                        if (x < 3)
                        {
                            _selectablesGrid[i + 1] ^= true;
                        }

                        if (y > 0)
                        {
                            _selectablesGrid[i - 4] ^= true;
                        }

                        if (y < 3)
                        {
                            _selectablesGrid[i + 4] ^= true;
                        }
                    }

                    if ((i % 4) < 3)
                    {
                        Widgets.SameLine();
                    }

                    Id.Pop();
                }

                Style.PopStyleVar();
                Widgets.TreePop();
            }

            // Alignment
            if (Widgets.TreeNode("Alignment"u8))
            {
                HelpMarker("By default, Selectables uses style.SelectableTextAlign but it can be overridden on a per-item basis using PushStyleVar(). You'll probably want to always keep your default situation to left-align otherwise it becomes difficult to layout multiple items on a same line"u8);
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        var alignment = new Vec2(x / 2.0f, y / 2.0f);
                        var index = (y * 3) + x;
                        Id.Push(index);
                        if (Widgets.Selectable("Yo"u8, _selectablesAlignment[index], SelectableFlags.None, new Size(80, 80)))
                        {
                            _selectablesAlignment[index] = !_selectablesAlignment[index];
                        }

                        if (x < 2)
                        {
                            Widgets.SameLine();
                        }

                        Id.Pop();
                    }
                }

                Widgets.TreePop();
            }

            Widgets.TreePop();
        }
    }

    private static void ShowSelectionAndMultiSelect()
    {
        if (Widgets.TreeNode("Selection (Adv), Multi-Select"u8))
        {
            // Note: BeginMultiSelect and EndMultiSelect are complex APIs not yet available in the bindings
            Widgets.Text("Multi-Select functionality requires BeginMultiSelect/EndMultiSelect APIs."u8);
            Widgets.Text("This section demonstrates the concept."u8);
            Widgets.Spacing();

            HelpMarker("This section would demonstrate advanced selection patterns.\nFor now showing a simple example using manual multi-select."u8);

            // Simple manual multi-select example
            for (var i = 0; i < 10; i++)
            {
                var selected = (_treeSelectionMask & (1 << i)) != 0;
                if (Widgets.Selectable(System.Text.Encoding.UTF8.GetBytes($"Object {i}"), selected))
                {
                    if (!Context.IsKeyDown(Key.LeftCtrl) && !Context.IsKeyDown(Key.RightCtrl))
                    {
                        _treeSelectionMask = 0;
                    }

                    _treeSelectionMask ^= 1 << i;
                }
            }

            Widgets.TreePop();
        }
    }

    private static void ShowTabs()
    {
        if (Widgets.TreeNode("Tabs"u8))
        {
            if (Widgets.TreeNode("Basic"u8))
            {
                if (Widgets.BeginTabBar("MyTabBar"u8, TabBarFlags.None))
                {
                    if (Widgets.BeginTabItem("Avocado"u8))
                    {
                        Widgets.Text("This is the Avocado tab!\nblah blah blah blah blah"u8);
                        Widgets.EndTabItem();
                    }

                    if (Widgets.BeginTabItem("Broccoli"u8))
                    {
                        Widgets.Text("This is the Broccoli tab!\nblah blah blah blah blah"u8);
                        Widgets.EndTabItem();
                    }

                    if (Widgets.BeginTabItem("Cucumber"u8))
                    {
                        Widgets.Text("This is the Cucumber tab!\nblah blah blah blah blah"u8);
                        Widgets.EndTabItem();
                    }

                    Widgets.EndTabBar();
                }

                Widgets.Separator();
                Widgets.TreePop();
            }

            if (Widgets.TreeNode("Advanced & Close Button"u8))
            {
                // Flags setup
                _ = Widgets.CheckboxFlags("TabBarFlags.Reorderable"u8, ref _tabsFlags, TabBarFlags.Reorderable);
                _ = Widgets.CheckboxFlags("TabBarFlags.AutoSelectNewTabs"u8, ref _tabsFlags, TabBarFlags.AutoSelectNewTabs);
                _ = Widgets.CheckboxFlags("TabBarFlags.TabListPopupButton"u8, ref _tabsFlags, TabBarFlags.TabListPopupButton);
                _ = Widgets.CheckboxFlags("TabBarFlags.NoCloseWithMiddleMouseButton"u8, ref _tabsFlags, TabBarFlags.NoCloseWithMiddleMouseButton);

                if (Widgets.CheckboxFlags("TabBarFlags.FittingPolicyShrink"u8, ref _tabsFlags, TabBarFlags.FittingPolicyShrink))
                {
                    _tabsFlags &= ~TabBarFlags.FittingPolicyScroll;
                }

                if (Widgets.CheckboxFlags("TabBarFlags.FittingPolicyScroll"u8, ref _tabsFlags, TabBarFlags.FittingPolicyScroll))
                {
                    _tabsFlags &= ~TabBarFlags.FittingPolicyShrink;
                }

                // Tab bar with close buttons
                string[] names = ["Artichoke", "Beetroot", "Celery", "Daikon"];

                if (Widgets.BeginTabBar("MyTabBar"u8, _tabsFlags))
                {
                    for (var i = 0; i < _tabsOpened.Length; i++)
                    {
                        if (_tabsOpened[i] && Widgets.BeginTabItem(System.Text.Encoding.UTF8.GetBytes(names[i]), ref _tabsOpened[i], TabItemFlags.None))
                        {
                            Widgets.Text(System.Text.Encoding.UTF8.GetBytes($"This is the {names[i]} tab!"));
                            if (Widgets.Button("Delete Me"u8))
                            {
                                _tabsOpened[i] = false;
                            }

                            Widgets.EndTabItem();
                        }
                    }

                    Widgets.EndTabBar();
                }

                Widgets.Separator();
                Widgets.Text(System.Text.Encoding.UTF8.GetBytes($"Opened: {_tabsOpened[0]}, {_tabsOpened[1]}, {_tabsOpened[2]}, {_tabsOpened[3]}"));

                Widgets.TreePop();
            }

            if (Widgets.TreeNode("TabItemButton & Leading/Trailing flags"u8))
            {
                if (Widgets.BeginTabBar("MyTabBar"u8, TabBarFlags.Reorderable | TabBarFlags.TabListPopupButton | TabBarFlags.FittingPolicyShrink))
                {
                    // Leading buttons
                    if (Widgets.TabItemButton("+"u8, TabItemFlags.Leading | TabItemFlags.NoTooltip))
                    {
                        _tabsActiveTabs.Add(_tabsNextTabId++);
                    }

                    // Active tabs
                    for (var i = 0; i < _tabsActiveTabs.Count; i++)
                    {
                        var open = true;
                        if (Widgets.BeginTabItem(System.Text.Encoding.UTF8.GetBytes($"{_tabsActiveTabs[i]:D4}"), ref open, TabItemFlags.None))
                        {
                            Widgets.Text(System.Text.Encoding.UTF8.GetBytes($"This is the {_tabsActiveTabs[i]:D4} tab!"));
                            Widgets.EndTabItem();
                        }

                        if (!open)
                        {
                            _tabsActiveTabs.RemoveAt(i);
                            i--;
                        }
                    }

                    Widgets.EndTabBar();
                }

                Widgets.TreePop();
            }

            Widgets.TreePop();
        }
    }

    private static void ShowText()
    {
        if (Widgets.TreeNode("Text"u8))
        {
            if (Widgets.TreeNode("Colorful Text"u8))
            {
                // Using shortcut.
                Widgets.TextColored(new Vec4(1.0f, 0.0f, 1.0f, 1.0f), "Pink"u8);
                Widgets.TextColored(new Vec4(1.0f, 1.0f, 0.0f, 1.0f), "Yellow"u8);
                Widgets.TextDisabled("Disabled"u8);
                Widgets.SameLine();
                HelpMarker("The TextDisabled color is stored in ImGuiStyle."u8);
                Widgets.TreePop();
            }

            if (Widgets.TreeNode("Word Wrapping"u8))
            {
                _ = Widgets.Checkbox("Enable word wrapping"u8, ref _textWordWrappingEnabled);
                if (_textWordWrappingEnabled)
                {
                    Widgets.TextWrapped("This text should automatically wrap on this window. The current implementation of text wrapping follows simple rules suitable for English and other languages that use Latin characters."u8);
                }
                else
                {
                    Widgets.Text("This text should automatically wrap on this window. The current implementation of text wrapping follows simple rules suitable for English and other languages that use Latin characters."u8);
                }

                Widgets.Spacing();

                _ = Widgets.Slider("Wrap position"u8, ref _textWordSpacing, 0.0f, 500.0f, "%.0f"u8);
                Style.PushTextWrapPosition(Window.CursorPosition.X + _textWordSpacing);
                Widgets.Text("The lazy dog is a good dog. This paragraph should fit within the wrap width without being clipped."u8);
                Size textSize = Widgets.GetItemRectSize();
                Style.PopTextWrapPosition();

                // Draw actual text bounding box, following by wrap position marker
                // Note: DrawList would require additional implementation
                Widgets.Dummy(new Size((_textWordSpacing > 0.0f ? _textWordSpacing : float.MaxValue) - textSize.Width, 0.0f));

                Widgets.TreePop();
            }

            if (Widgets.TreeNode("UTF-8 Text"u8))
            {
                // UTF-8 test with a japanese font:
                Widgets.TextWrapped("CJK: 日本語, 한국어, 中文. Japanese kana: こんにちは。Hiragana: あいうえお. Katakana: アイウエオ。"u8);
                Widgets.Text("Greek: Αα Ββ Γγ Δδ Εε Ζζ Ηη Θθ Ιι Κκ Λλ Μμ Νν Ξξ Οο Ππ Ρρ Σσ Ττ Υυ Φφ Χχ Ψψ Ωω"u8);
                Widgets.Text("Emoji: 💎🤖🖥️👾🎮"u8);
                Widgets.TreePop();
            }

            Widgets.TreePop();
        }
    }

    private static void ShowTextFilter()
    {
        if (Widgets.TreeNode("Text Filter"u8))
        {
            // Note: ImGuiTextFilter is not yet wrapped - showing concept
            HelpMarker("Not a widget per se, but ImGuiTextFilter is a helper to perform simple filtering on string data. It creates a text input with a filter pattern."u8);
            Widgets.Text("Filter text input would go here."u8);
            Widgets.Text("Filtered items would appear below."u8);

            string[] lines = ["aaa1.c", "bbb1.c", "ccc1.c", "aaa2.cpp", "bbb2.cpp", "ccc2.cpp", "abc.h", "hello, world"];
            for (var i = 0; i < lines.Length; i++)
            {
                Widgets.BulletText(System.Text.Encoding.UTF8.GetBytes(lines[i]));
            }

            Widgets.TreePop();
        }
    }

    private static void ShowTextInput()
    {
        if (Widgets.TreeNode("Text Input"u8))
        {
            if (Widgets.TreeNode("Multi-line Text Input"u8))
            {
                _ = Widgets.CheckboxFlags("InputTextFlags.ReadOnly"u8, ref _textInputFlags, InputTextFlags.ReadOnly);
                _ = Widgets.CheckboxFlags("InputTextFlags.AllowTabInput"u8, ref _textInputFlags, InputTextFlags.AllowTabInput);
                Widgets.SameLine();
                HelpMarker("When enabled, pressing TAB input a '\\t' character into the text field."u8);
                _ = Widgets.CheckboxFlags("InputTextFlags.CtrlEnterForNewLine"u8, ref _textInputFlags, InputTextFlags.CtrlEnterForNewLine);
                _ = Widgets.InputTextMultiline("##source"u8, _textInputBufMultiline, new Size(-float.Epsilon, Font.GetTextLineHeight() * 16), _textInputFlags);
                Widgets.TreePop();
            }

            if (Widgets.TreeNode("Filtered Text Input"u8))
            {
                _ = Widgets.InputText("default"u8, _textInputBuf1);
                _ = Widgets.InputText("decimal"u8, _textInputBuf2, InputTextFlags.CharsDecimal);
                _ = Widgets.InputText("hexadecimal"u8, _textInputBuf3, InputTextFlags.CharsHexadecimal | InputTextFlags.CharsUppercase);
                _ = Widgets.InputText("uppercase"u8, _textInputBuf4, InputTextFlags.CharsUppercase);
                _ = Widgets.InputText("no blank"u8, _textInputBuf5, InputTextFlags.CharsNoBlank);
                Widgets.TreePop();
            }

            if (Widgets.TreeNode("Password Input"u8))
            {
                _ = Widgets.InputText("password"u8, _textInputPassword, InputTextFlags.Password);
                Widgets.SameLine();
                HelpMarker("Display all characters as '*'.\nDisable clipboard cut and copy.\nDisable logging.\n"u8);
                _ = Widgets.InputTextWithHint("password (w/ hint)"u8, "<password>"u8, _textInputPassword, InputTextFlags.Password);
                _ = Widgets.InputText("password (clear)"u8, _textInputPassword);
                Widgets.TreePop();
            }

            Widgets.TreePop();
        }
    }

    private static void ShowTooltips()
    {
        if (Widgets.TreeNode("Tooltips"u8))
        {
            // Basic tooltip
            Widgets.TextWrapped("Hover the buttons to see the tooltips."u8);

            _ = Widgets.Button("Basic"u8);
            if (Widgets.IsItemHovered())
            {
                Widgets.SetTooltip("I am a tooltip"u8);
            }

            _ = Widgets.Button("Fancy"u8);
            if (Widgets.IsItemHovered() && Widgets.BeginTooltip())
            {
                Widgets.Text("I am a fancy tooltip"u8);
                Widgets.PlotLines("Curve"u8, _plotArrSin.AsSpan(0, 30), 0, default, -1.0f, 1.0f, new Vec2(0, 40));
                Widgets.Text(System.Text.Encoding.UTF8.GetBytes($"Sin(time) = {MathF.Sin((float)Context.GetTime())}"));
                Widgets.EndTooltip();
            }

            _ = Widgets.Button("Delayed"u8);
            if (Widgets.IsItemHovered(HoveredFlags.DelayNormal))
            {
                Widgets.SetTooltip("I am a tooltip with more delay!"u8);
            }

            _ = Widgets.Button("Stationary"u8);
            if (Widgets.IsItemHovered(HoveredFlags.Stationary))
            {
                Widgets.SetTooltip("I am a tooltip requiring mouse to be stationary before appearing."u8);
            }

            Widgets.SeparatorText("Custom"u8);

            HelpMarker("Tooltip are created by default when hovering an item.\nYou can disable this by setting ImGuiConfigFlags_NoMouseCursorChange in your IO config."u8);

            // Simple tooltip override
            var tooltipsAlwaysOn = _tooltipsAlwaysOn != 0;
            if (Widgets.Checkbox("Always On"u8, ref tooltipsAlwaysOn))
            {
                _tooltipsAlwaysOn = tooltipsAlwaysOn ? 1 : 0;
            }

            Widgets.SameLine();
            var tooltipsOnDisabledItems = _tooltipsOnDisabledItems != 0;
            if (Widgets.Checkbox("On Disabled Items"u8, ref tooltipsOnDisabledItems))
            {
                _tooltipsOnDisabledItems = tooltipsOnDisabledItems ? 1 : 0;
            }

            Widgets.SameLine();
            _ = Widgets.Checkbox("Wrap"u8, ref _tooltipsWrap);

            Widgets.TreePop();
        }
    }

    private static void ShowTreeNodes()
    {
        if (Widgets.TreeNode("Tree Nodes"u8))
        {
            if (Widgets.TreeNode("Basic trees"u8))
            {
                for (var i = 0; i < 5; i++)
                {
                    // Use SetNextItemOpen() to set default state
                    if (i == 0)
                    {
                        Widgets.SetNextItemOpen(true, Condition.Once);
                    }

                    if (Widgets.TreeNode(System.Text.Encoding.UTF8.GetBytes($"Child {i}")))
                    {
                        Widgets.Text("blah blah"u8);
                        Widgets.SameLine();
                        if (Widgets.SmallButton("button"u8))
                        {
                            // Button pressed
                        }

                        Widgets.TreePop();
                    }
                }

                Widgets.TreePop();
            }

            if (Widgets.TreeNode("Advanced, with Selectable nodes"u8))
            {
                HelpMarker("This is a more typical looking tree with selectable nodes.\nClick to select, Ctrl+Click to toggle, click on arrows or double-click to open."u8);
                _ = Widgets.CheckboxFlags("TreeNodeFlags.OpenOnArrow"u8, ref _treeBaseFlags, TreeNodeFlags.OpenOnArrow);
                _ = Widgets.CheckboxFlags("TreeNodeFlags.OpenOnDoubleClick"u8, ref _treeBaseFlags, TreeNodeFlags.OpenOnDoubleClick);
                _ = Widgets.CheckboxFlags("TreeNodeFlags.SpanAvailWidth"u8, ref _treeBaseFlags, TreeNodeFlags.SpanAvailWidth);
                _ = Widgets.CheckboxFlags("TreeNodeFlags.SpanFullWidth"u8, ref _treeBaseFlags, TreeNodeFlags.SpanFullWidth);
                _ = Widgets.CheckboxFlags("TreeNodeFlags.SpanLabelWidth"u8, ref _treeBaseFlags, TreeNodeFlags.SpanLabelWidth);
                _ = Widgets.CheckboxFlags("TreeNodeFlags.SpanAllColumns"u8, ref _treeBaseFlags, TreeNodeFlags.SpanAllColumns);
                _ = Widgets.Checkbox("Align label with current X position"u8, ref _treeAlignLabelWithCurrentXPosition);
                _ = Widgets.Checkbox("Test tree node as drag source"u8, ref _treeTestDragAndDrop);

                if (_treeAlignLabelWithCurrentXPosition)
                {
                    Widgets.Unindent(Widgets.GetTreeNodeToLabelSpacing());
                }

                // Tree node example
                var nodeClicked = -1;
                for (var i = 0; i < 6; i++)
                {
                    TreeNodeFlags nodeFlags = _treeBaseFlags;
                    var isSelected = (_treeSelectionMask & (1 << i)) != 0;
                    if (isSelected)
                    {
                        nodeFlags |= TreeNodeFlags.Selected;
                    }

                    if (i < 3)
                    {
                        // Nodes 0..2 are Tree Nodes
                        var nodeOpen = Widgets.TreeNode(System.Text.Encoding.UTF8.GetBytes($"Selectable Node {i}"), nodeFlags);
                        if (Widgets.IsItemClicked() && !Widgets.IsItemToggledOpen())
                        {
                            nodeClicked = i;
                        }

                        if (_treeTestDragAndDrop && Widgets.BeginDragDropSource())
                        {
                            _ = Widgets.SetDragDropPayload("_TREENODE"u8, default);
                            Widgets.Text("This is a drag and drop source"u8);
                            Widgets.EndDragDropSource();
                        }

                        if (nodeOpen)
                        {
                            Widgets.BulletText("Blah blah\nBlah Blah"u8);
                            Widgets.TreePop();
                        }
                    }
                    else
                    {
                        // Nodes 3..5 are Tree Leaves
                        nodeFlags |= TreeNodeFlags.Leaf | TreeNodeFlags.NoTreePushOnOpen;
                        _ = Widgets.TreeNode(System.Text.Encoding.UTF8.GetBytes($"Selectable Leaf {i}"), nodeFlags);
                        if (Widgets.IsItemClicked() && !Widgets.IsItemToggledOpen())
                        {
                            nodeClicked = i;
                        }

                        if (_treeTestDragAndDrop && Widgets.BeginDragDropSource())
                        {
                            _ = Widgets.SetDragDropPayload("_TREENODE"u8, default);
                            Widgets.Text("This is a drag and drop source"u8);
                            Widgets.EndDragDropSource();
                        }
                    }
                }

                if (nodeClicked != -1)
                {
                    // Update selection state
                    if (Context.IsKeyDown(Key.LeftCtrl) || Context.IsKeyDown(Key.RightCtrl))
                    {
                        _treeSelectionMask ^= 1 << nodeClicked;
                    }
                    else
                    {
                        _treeSelectionMask = 1 << nodeClicked;
                    }
                }

                if (_treeAlignLabelWithCurrentXPosition)
                {
                    Widgets.Indent(Widgets.GetTreeNodeToLabelSpacing());
                }

                Widgets.TreePop();
            }

            Widgets.TreePop();
        }
    }

    private static void ShowVerticalSliders()
    {
        if (Widgets.TreeNode("Vertical Sliders"u8))
        {
            _ = Widgets.Slider("Size"u8, ref _vslidersSize, 10.0f, 60.0f, "%.0f"u8);
            Widgets.Spacing();

            // Using the generic VSlider
            Style.PushStyleVar(StyleVariable.ItemSpacing, new Vec2(4, 4));
            _ = Widgets.VSlider("##int"u8, new Size(_vslidersSize, 160), ref _vslidersIntValue, 0, 100, "%d"u8);
            Widgets.SameLine();

            for (var i = 0; i < _vslidersValues.Length; i++)
            {
                if (i > 0)
                {
                    Widgets.SameLine();
                }

                Id.Push(i);
                _ = Widgets.VSlider("##v"u8, new Size(_vslidersSize, 160), ref _vslidersValues[i], 0.0f, 1.0f, default);
                if (Widgets.IsItemActive() || Widgets.IsItemHovered())
                {
                    Widgets.SetTooltip(System.Text.Encoding.UTF8.GetBytes($"{_vslidersValues[i]:F3}"));
                }

                Id.Pop();
            }

            Widgets.SameLine();

            // Vertical sliders showing values
            Id.Push("set2"u8);
            for (var i = 0; i < 4; i++)
            {
                if (i > 0)
                {
                    Widgets.SameLine();
                }

                Id.Push(i);
                _ = Widgets.VSlider("##v"u8, new Size(_vslidersSize, 160), ref _vslidersValues2[i], 0.0f, 1.0f, default);
                Id.Pop();
            }

            Id.Pop();

            Style.PopStyleVar();

            Widgets.TreePop();
        }
    }

    // Data to be shared across different functions of the demo.
    internal struct ImGuiDemoWindowData()
    {
        // Examples Apps (accessible from the "Examples" menu)
        public bool ShowMainMenuBar = false;
        public bool ShowAppAssetsBrowser = false;
        public bool ShowAppConsole = false;
        public bool ShowAppCustomRendering = false;
        public bool ShowAppDocuments = false;
        public bool ShowAppLog = false;
        public bool ShowAppLayout = false;
        public bool ShowAppPropertyEditor = false;
        public bool ShowAppSimpleOverlay = false;
        public bool ShowAppAutoResize = false;
        public bool ShowAppConstrainedResize = false;
        public bool ShowAppFullscreen = false;
        public bool ShowAppLongText = false;
        public bool ShowAppWindowTitles = false;

        // Dear ImGui Tools (accessible from the "Tools" menu)
        public bool ShowMetrics = false;
        public bool ShowDebugLog = false;
        public bool ShowIDStackTool = false;
        public bool ShowStyleEditor = false;
        public bool ShowAbout = false;

        // Other data
        public bool DisableSections = false;

        //~ImGuiDemoWindowData() { if (DemoTree) ExampleTree_DestroyNode(DemoTree); }
    }

    private enum Element
    {
        Fire,
        Earth,
        Air,
        Water,
        Max
    }
}