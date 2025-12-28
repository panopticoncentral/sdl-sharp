using Sdl3Sharp.ImGui;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.Demo;

public static unsafe class ImGuiDemoWindow
{
    private static ImGuiDemoWindowData _demoData;

    // Demonstrate the various window flags. Typically you would just use the default!
    private static bool _no_titlebar = false;
    private static bool _no_scrollbar = false;
    private static bool _no_menu = false;
    private static bool _no_move = false;
    private static bool _no_resize = false;
    private static bool _no_collapse = false;
    private static bool _no_close = false;
    private static bool _no_nav = false;
    private static bool _no_background = false;
    private static bool _no_bring_to_front = false;
    private static bool _unsaved_document = false;

    private static bool _options_enabled = true;
    private static float _options_f = 0.5f;
    private static int _options_n = 0;
    private static bool _options_b = true;

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
                ReadOnlySpan<byte> name = ImGui.ImGui.GetStyleColorName(i);
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

    private static void DemoWindowMenuBar()
    {
        if (Widgets.BeginMenuBar())
        {
            if (Widgets.BeginMenu("Menu"u8))
            {
                ShowExampleMenuFile();
                Widgets.EndMenu();
            }
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
        //if (Widgets.MenuItem("MenuItem"u8)) {} // You can also use MenuItem() inside a menu bar!
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
                // TODO: Widgets.BeginDisabled(!has_debug_tools);
                var configDebugHighlightIdConflicts = io.ConfigDebugHighlightIdConflicts;
                Widgets.Checkbox("Highlight ID Conflicts"u8, ref configDebugHighlightIdConflicts);
                io.ConfigDebugHighlightIdConflicts = configDebugHighlightIdConflicts;
                // TODO: Widgets.EndDisabled();
                var configErrorRecoveryEnableAssert = io.ConfigErrorRecoveryEnableAssert;
                Widgets.Checkbox("Assert on error recovery"u8, ref configErrorRecoveryEnableAssert);
                io.ConfigErrorRecoveryEnableAssert = configErrorRecoveryEnableAssert;
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
                // TODO: Widgets.SetItemTooltip("Requires io.ConfigDebugIsDebuggerPresent=true to be set.\n\nWe otherwise disable some extra features to avoid casual users crashing the application."u8);
            }

            Widgets.MenuItem("Style Editor"u8, null, ref _demoData.ShowStyleEditor);
            Widgets.MenuItem("About Dear ImGui"u8, null, ref _demoData.ShowAbout);

            Widgets.EndMenu();
        }

        Widgets.EndMenuBar();
    }

    public static void ShowDemoWindow()
    {
        var open = true;
        ShowDemoWindow(ref open);
    }

    public static void ShowDemoWindow(ref bool open)
    {
        // We specify a default position/size in case there's no data in the .ini file.
        // We only do it to make the demo applications a little more welcoming, but typically this isn't required.
        Viewport mainViewport = Context.MainViewport;
        Window.SetNextWindowPos((mainViewport.WorkPosition.X + 50, mainViewport.WorkPosition.Y + 20), Condition.FirstUseEver);
        Window.SetNextWindowSize((550, 680), Condition.FirstUseEver);

        WindowFlags windowFlags = 0;
        if (_no_titlebar)
        {
            windowFlags |= WindowFlags.NoTitleBar;
        }

        if (_no_scrollbar)
        {
            windowFlags |= WindowFlags.NoScrollbar;
        }

        if (!_no_menu)
        {
            windowFlags |= WindowFlags.MenuBar;
        }

        if (_no_move)
        {
            windowFlags |= WindowFlags.NoMove;
        }

        if (_no_resize)
        {
            windowFlags |= WindowFlags.NoResize;
        }

        if (_no_collapse)
        {
            windowFlags |= WindowFlags.NoCollapse;
        }

        if (_no_nav)
        {
            windowFlags |= WindowFlags.NoNav;
        }

        if (_no_background)
        {
            windowFlags |= WindowFlags.NoBackground;
        }

        if (_no_bring_to_front)
        {
            windowFlags |= WindowFlags.NoBringToFrontOnFocus;
        }

        if (_unsaved_document)
        {
            windowFlags |= WindowFlags.UnsavedDocument;
        }

        if (_no_close)
        {
            open = true;
        }

        if (!Window.Begin("Dear ImGui Demo (managed)"u8, ref open, windowFlags))
        {
            return;
        }

        // Most framed widgets share a common width settings. Remaining width is used for the label.
        // The width of the frame may be changed with PushItemWidth() or SetNextItemWidth().
        // - Positive value for absolute size, negative value for right-alignment.
        // - The default value is about GetWindowWidth() * 0.65f.
        // - See 'Demo->Layout->Widgets Width' for details.
        // Here we change the frame width based on how much width we want to give to the label.
        var labelWidthBase = Font.GetFontSize() * 12; // Some amount of width for label, based on font size.
        var labelWidthMax = Window.ContentRegionAvail.Width * 0.40f; // ...but always leave some room for framed widgets.
        var labelWidth = Math.Min(labelWidthBase, labelWidthMax);
        Style.PushItemWidth(-labelWidth); // Right-align: framed items will leave 'label_width' available for the label.

        DemoWindowMenuBar();

        Style.PopItemWidth();
        Window.End();
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
}
