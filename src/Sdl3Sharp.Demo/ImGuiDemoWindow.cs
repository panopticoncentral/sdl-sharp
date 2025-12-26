using Sdl3Sharp.ImGui;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.Demo;

public static class ImGuiDemoWindow
{
    private static ImGuiDemoWindowData _demoData;

    private static void DemoWindowMenuBar()
    {
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

    public static void ShowDemoWindow(StateRef<bool> open)
    {
        // We specify a default position/size in case there's no data in the .ini file.
        // We only do it to make the demo applications a little more welcoming, but typically this isn't required.
        Viewport mainViewport = Context.MainViewport;
        Window.SetNextWindowPos(new Point(mainViewport.WorkPosition.X + 50, mainViewport.WorkPosition.Y + 20), Condition.FirstUseEver);
        Window.SetNextWindowSize(new Size(550, 680), Condition.FirstUseEver);

        using var window = new Window("Dear ImGui Demo (managed)"u8, open);
        if (!window.IsVisible)
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
        using var itemWidthScope = ItemWidthScope.Push(-labelWidth); // Right-align: framed items will leave 'label_width' available for the label.

        DemoWindowMenuBar();
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
