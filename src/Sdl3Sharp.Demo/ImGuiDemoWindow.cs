using Sdl3Sharp.ImGui;
using Sdl3Sharp.ImGui.Native;
using System.Text;

namespace Sdl3Sharp.Demo;

public static class ImGuiDemoWindow
{
    private static ImGuiDemoWindowData _demoData;

    // Demonstrate the various window flags. Typically you would just use the default!
    private readonly static StateRef<bool> _no_titlebar = StateStore.Instance.Create(false);
    private readonly static StateRef<bool> _no_scrollbar = StateStore.Instance.Create(false);
    private readonly static StateRef<bool> _no_menu = StateStore.Instance.Create(false);
    private readonly static StateRef<bool> _no_move = StateStore.Instance.Create(false);
    private readonly static StateRef<bool> _no_resize = StateStore.Instance.Create(false);
    private readonly static StateRef<bool> _no_collapse = StateStore.Instance.Create(false);
    private readonly static StateRef<bool> _no_close = StateStore.Instance.Create(false);
    private readonly static StateRef<bool> _no_nav = StateStore.Instance.Create(false);
    private readonly static StateRef<bool> _no_background = StateStore.Instance.Create(false);
    private readonly static StateRef<bool> _no_bring_to_front = StateStore.Instance.Create(false);
    private readonly static StateRef<bool> _unsaved_document = StateStore.Instance.Create(false);

    private readonly static StateRef<bool> _options_enabled = StateStore.Instance.Create(true);
    private readonly static StateRef<float> _options_f = StateStore.Instance.Create(0.5f);
    private readonly static StateRef<int> _options_n = StateStore.Instance.Create(0);
    private readonly static StateRef<bool> _options_b = StateStore.Instance.Create(true);

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
            _ = Widgets.MenuItem("Enabled"u8, ""u8, _options_enabled);
            _ = Widgets.BeginChild("child"u8, (0, 60), ChildFlags.Borders);
            for (var i = 0; i < 10; i++)
            {
                Widgets.Text($"Scrolling Text {i}".ToUtf8());
            }

            Widgets.EndChild();
            _ = Widgets.Slider("Value"u8, _options_f, 0.0f, 1.0f);
            _ = Widgets.Input("Input"u8, _options_f, 0.1f);
            _ = Widgets.Combo("Combo"u8, _options_n, "Yes\0No\0Maybe\0\0"u8);
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
            _ = Widgets.Checkbox("SomeOption"u8, _options_b);
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

            Widgets.EndMenuBar();
        }
    }

    /*
     * static void DemoWindowMenuBar(ImGuiDemoWindowData* demo_data)
{
    IMGUI_DEMO_MARKER("Menu");
    if (ImGui::BeginMenuBar())
    {
        if (ImGui::BeginMenu("Menu"))
        {
            IMGUI_DEMO_MARKER("Menu/File");
            ShowExampleMenuFile();
            ImGui::EndMenu();
        }
        if (ImGui::BeginMenu("Examples"))
        {
            IMGUI_DEMO_MARKER("Menu/Examples");
            ImGui::MenuItem("Main menu bar", NULL, &demo_data->ShowMainMenuBar);

            ImGui::SeparatorText("Mini apps");
            ImGui::MenuItem("Assets Browser", NULL, &demo_data->ShowAppAssetsBrowser);
            ImGui::MenuItem("Console", NULL, &demo_data->ShowAppConsole);
            ImGui::MenuItem("Custom rendering", NULL, &demo_data->ShowAppCustomRendering);
            ImGui::MenuItem("Documents", NULL, &demo_data->ShowAppDocuments);
            ImGui::MenuItem("Log", NULL, &demo_data->ShowAppLog);
            ImGui::MenuItem("Property editor", NULL, &demo_data->ShowAppPropertyEditor);
            ImGui::MenuItem("Simple layout", NULL, &demo_data->ShowAppLayout);
            ImGui::MenuItem("Simple overlay", NULL, &demo_data->ShowAppSimpleOverlay);

            ImGui::SeparatorText("Concepts");
            ImGui::MenuItem("Auto-resizing window", NULL, &demo_data->ShowAppAutoResize);
            ImGui::MenuItem("Constrained-resizing window", NULL, &demo_data->ShowAppConstrainedResize);
            ImGui::MenuItem("Fullscreen window", NULL, &demo_data->ShowAppFullscreen);
            ImGui::MenuItem("Long text display", NULL, &demo_data->ShowAppLongText);
            ImGui::MenuItem("Manipulating window titles", NULL, &demo_data->ShowAppWindowTitles);

            ImGui::EndMenu();
        }
        //if (ImGui::MenuItem("MenuItem")) {} // You can also use MenuItem() inside a menu bar!
        if (ImGui::BeginMenu("Tools"))
        {
            IMGUI_DEMO_MARKER("Menu/Tools");
            ImGuiIO& io = ImGui::GetIO();
#ifndef IMGUI_DISABLE_DEBUG_TOOLS
            const bool has_debug_tools = true;
#else
            const bool has_debug_tools = false;
#endif
            ImGui::MenuItem("Metrics/Debugger", NULL, &demo_data->ShowMetrics, has_debug_tools);
            if (ImGui::BeginMenu("Debug Options"))
            {
                ImGui::BeginDisabled(!has_debug_tools);
                ImGui::Checkbox("Highlight ID Conflicts", &io.ConfigDebugHighlightIdConflicts);
                ImGui::EndDisabled();
                ImGui::Checkbox("Assert on error recovery", &io.ConfigErrorRecoveryEnableAssert);
                ImGui::TextDisabled("(see Demo->Configuration for details & more)");
                ImGui::EndMenu();
            }
            ImGui::MenuItem("Debug Log", NULL, &demo_data->ShowDebugLog, has_debug_tools);
            ImGui::MenuItem("ID Stack Tool", NULL, &demo_data->ShowIDStackTool, has_debug_tools);
            bool is_debugger_present = io.ConfigDebugIsDebuggerPresent;
            if (ImGui::MenuItem("Item Picker", NULL, false, has_debug_tools))// && is_debugger_present))
                ImGui::DebugStartItemPicker();
            if (!is_debugger_present)
                ImGui::SetItemTooltip("Requires io.ConfigDebugIsDebuggerPresent=true to be set.\n\nWe otherwise disable some extra features to avoid casual users crashing the application.");
            ImGui::MenuItem("Style Editor", NULL, &demo_data->ShowStyleEditor);
            ImGui::MenuItem("About Dear ImGui", NULL, &demo_data->ShowAbout);

            ImGui::EndMenu();
        }
        ImGui::EndMenuBar();
    }
}

     */

    public static void ShowDemoWindow(StateRef<bool>? open)
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
            open = null; // Don't pass our bool* to Begin
        }

        if (!Window.Begin("Dear ImGui Demo (managed)"u8, open, windowFlags))
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
        public StateRef<bool> ShowMainMenuBar = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppAssetsBrowser = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppConsole = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppCustomRendering = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppDocuments = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppLog = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppLayout = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppPropertyEditor = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppSimpleOverlay = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppAutoResize = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppConstrainedResize = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppFullscreen = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppLongText = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAppWindowTitles = StateStore.Instance.Create(false);

        // Dear ImGui Tools (accessible from the "Tools" menu)
        public StateRef<bool> ShowMetrics = StateStore.Instance.Create(false);
        public StateRef<bool> ShowDebugLog = StateStore.Instance.Create(false);
        public StateRef<bool> ShowIDStackTool = StateStore.Instance.Create(false);
        public StateRef<bool> ShowStyleEditor = StateStore.Instance.Create(false);
        public StateRef<bool> ShowAbout = StateStore.Instance.Create(false);

        // Other data
        public StateRef<bool> DisableSections = StateStore.Instance.Create(false);

        //~ImGuiDemoWindowData() { if (DemoTree) ExampleTree_DestroyNode(DemoTree); }
    }
}
