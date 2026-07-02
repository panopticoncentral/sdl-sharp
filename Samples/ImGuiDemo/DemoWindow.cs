// C# port of imgui_demo.cpp — [SECTION] Demo Window / ShowDemoWindow() and
// [SECTION] DemoWindowMenuBar().
//
// This is a 1:1 structural port of Dear ImGui's demo window, used as a coverage
// test of the SdlSharp.ImGui wrappers. Section order, labels, strings and
// default values mirror upstream. C++ function-static locals become private
// static fields. IMGUI_DEMO_MARKER() calls are kept as "// DEMO MARKER: ..."
// comments. Wrapper gaps are marked with "// PORT GAP: ..." comments.
//
// Upstream reference: imgui/imgui_demo.cpp

using SdlSharp.ImGui;

namespace ImGuiDemo;

// Data to be shared across different functions of the demo.
// (mirrors upstream 'struct ImGuiDemoWindowData')
internal sealed class DemoWindowData
{
    // Examples Apps (accessible from the "Examples" menu)
    public bool ShowMainMenuBar;
    public bool ShowAppAssetsBrowser;
    public bool ShowAppConsole;
    public bool ShowAppCustomRendering;
    public bool ShowAppDocuments;
    public bool ShowAppLog;
    public bool ShowAppLayout;
    public bool ShowAppPropertyEditor;
    public bool ShowAppSimpleOverlay;
    public bool ShowAppAutoResize;
    public bool ShowAppConstrainedResize;
    public bool ShowAppFullscreen;
    public bool ShowAppLongText;
    public bool ShowAppWindowTitles;

    // Dear ImGui Tools (accessible from the "Tools" menu)
    public bool ShowMetrics;
    public bool ShowDebugLog;
    public bool ShowIDStackTool;
    public bool ShowStyleEditor;
    public bool ShowAbout;

    // Other data
    public bool DisableSections;
    public ExampleTreeNode? DemoTree;

    // (upstream destructor destroys DemoTree; the GC handles that for us)
}

internal static unsafe partial class DemoWindow
{
    //-----------------------------------------------------------------------------
    // [SECTION] Demo Window / ShowDemoWindow()
    //-----------------------------------------------------------------------------

    // Stored data (C++ 'static ImGuiDemoWindowData demo_data;')
    private static readonly DemoWindowData s_demo_data = new();

    // Demonstrate the various window flags. Typically you would just use the default!
    // (C++ function-static locals of ShowDemoWindow())
    private static bool s_no_titlebar;
    private static bool s_no_scrollbar;
    private static bool s_no_menu;
    private static bool s_no_move;
    private static bool s_no_resize;
    private static bool s_no_collapse;
    private static bool s_no_close;
    private static bool s_no_nav;
    private static bool s_no_background;
    private static bool s_no_bring_to_front;
    private static bool s_unsaved_document;

    // Helper to bind an io.XXX bool exposed as a static Io property to a Checkbox.
    // (upstream passes &io.XXX directly; C# properties can't be passed by ref)
    private static bool IoCheckbox(string label, Func<bool> get, Action<bool> set)
    {
        bool v = get();
        bool pressed = ImGui.Checkbox(label, ref v);
        if (pressed)
            set(v);
        return pressed;
    }

    // Demonstrate most Dear ImGui features (this is big function!)
    // You may execute this function to experiment with the UI and understand what it does.
    // You may then search for keywords in the code when you are interested by a specific feature.
    // (upstream: void ImGui::ShowDemoWindow(bool* p_open))
    public static void Show(ref bool open)
    {
        // Exceptionally add an extra assert here for people confused about initial Dear ImGui setup
        // Most functions would normally just assert/crash if the context is missing.
        // (Context creation and IMGUI_CHECKVERSION are handled by SdlSharp.ImGui.Context.Create.)

        // Stored data
        DemoWindowData demo_data = s_demo_data;

        // Examples Apps (accessible from the "Examples" menu)
        if (demo_data.ShowMainMenuBar) { ShowExampleAppMainMenuBar(); }
        if (demo_data.ShowAppDocuments) { ShowExampleAppDocuments(ref demo_data.ShowAppDocuments); }
        if (demo_data.ShowAppAssetsBrowser) { ShowExampleAppAssetsBrowser(ref demo_data.ShowAppAssetsBrowser); }
        if (demo_data.ShowAppConsole) { ShowExampleAppConsole(ref demo_data.ShowAppConsole); }
        if (demo_data.ShowAppCustomRendering) { ShowExampleAppCustomRendering(ref demo_data.ShowAppCustomRendering); }
        if (demo_data.ShowAppLog) { ShowExampleAppLog(ref demo_data.ShowAppLog); }
        if (demo_data.ShowAppLayout) { ShowExampleAppLayout(ref demo_data.ShowAppLayout); }
        if (demo_data.ShowAppPropertyEditor) { ShowExampleAppPropertyEditor(ref demo_data.ShowAppPropertyEditor, demo_data); }
        if (demo_data.ShowAppSimpleOverlay) { ShowExampleAppSimpleOverlay(ref demo_data.ShowAppSimpleOverlay); }
        if (demo_data.ShowAppAutoResize) { ShowExampleAppAutoResize(ref demo_data.ShowAppAutoResize); }
        if (demo_data.ShowAppConstrainedResize) { ShowExampleAppConstrainedResize(ref demo_data.ShowAppConstrainedResize); }
        if (demo_data.ShowAppFullscreen) { ShowExampleAppFullscreen(ref demo_data.ShowAppFullscreen); }
        if (demo_data.ShowAppLongText) { ShowExampleAppLongText(ref demo_data.ShowAppLongText); }
        if (demo_data.ShowAppWindowTitles) { ShowExampleAppWindowTitles(ref demo_data.ShowAppWindowTitles); }

        // Dear ImGui Tools (accessible from the "Tools" menu)
        if (demo_data.ShowMetrics) { ImGui.ShowMetricsWindow(ref demo_data.ShowMetrics); }
        if (demo_data.ShowDebugLog) { ImGui.ShowDebugLogWindow(ref demo_data.ShowDebugLog); }
        if (demo_data.ShowIDStackTool) { ImGui.ShowIDStackToolWindow(ref demo_data.ShowIDStackTool); }
        if (demo_data.ShowAbout) { ImGui.ShowAboutWindow(ref demo_data.ShowAbout); }
        if (demo_data.ShowStyleEditor)
        {
            ImGui.Begin("Dear ImGui Style Editor", ref demo_data.ShowStyleEditor);
            ImGui.ShowStyleEditor();
            ImGui.End();
        }

        WindowFlags window_flags = WindowFlags.None;
        if (s_no_titlebar) window_flags |= WindowFlags.NoTitleBar;
        if (s_no_scrollbar) window_flags |= WindowFlags.NoScrollbar;
        if (!s_no_menu) window_flags |= WindowFlags.MenuBar;
        if (s_no_move) window_flags |= WindowFlags.NoMove;
        if (s_no_resize) window_flags |= WindowFlags.NoResize;
        if (s_no_collapse) window_flags |= WindowFlags.NoCollapse;
        if (s_no_nav) window_flags |= WindowFlags.NoNav;
        if (s_no_background) window_flags |= WindowFlags.NoBackground;
        if (s_no_bring_to_front) window_flags |= WindowFlags.NoBringToFrontOnFocus;
        if (s_unsaved_document) window_flags |= WindowFlags.UnsavedDocument;
        // if (s_no_close) — handled below: don't pass our ref bool to Begin (upstream sets p_open = NULL)

        // We specify a default position/size in case there's no data in the .ini file.
        // We only do it to make the demo applications a little more welcoming, but typically this isn't required.
        var main_viewport = ImGui.GetMainViewport();
        ImGui.SetNextWindowPos(main_viewport.WorkPos.X + 650, main_viewport.WorkPos.Y + 20, Cond.FirstUseEver);
        ImGui.SetNextWindowSize(550, 680, Cond.FirstUseEver);

        // Main body of the Demo window starts here.
        bool window_open = s_no_close
            ? ImGui.Begin("Dear ImGui Demo", window_flags)
            : ImGui.Begin("Dear ImGui Demo", ref open, window_flags);
        if (!window_open)
        {
            // Early out if the window is collapsed, as an optimization.
            ImGui.End();
            return;
        }

        // Most framed widgets share a common width settings. Remaining width is used for the label.
        // The width of the frame may be changed with PushItemWidth() or SetNextItemWidth().
        // - Positive value for absolute size, negative value for right-alignment.
        // - The default value is about GetWindowWidth() * 0.65f.
        // - See 'Demo->Layout->Widgets Width' for details.
        // Here we change the frame width based on how much width we want to give to the label.
        float label_width_base = ImGui.GetFontSize() * 12;                    // Some amount of width for label, based on font size.
        float label_width_max = ImGui.GetContentRegionAvail().Width * 0.40f;  // ...but always leave some room for framed widgets.
        float label_width = MathF.Min(label_width_base, label_width_max);
        ImGui.PushItemWidth(-label_width);                                    // Right-align: framed items will leave 'label_width' available for the label.
        //ImGui.PushItemWidth(ImGui.GetContentRegionAvail().Width * 0.40f);   // e.g. Use 40% width for framed widgets, leaving 60% width for labels.
        //ImGui.PushItemWidth(-ImGui.GetContentRegionAvail().Width * 0.40f);  // e.g. Use 40% width for labels, leaving 60% width for framed widgets.
        //ImGui.PushItemWidth(ImGui.GetFontSize() * -12);                     // e.g. Use XXX width for labels, leaving the rest for framed widgets.

        // Menu Bar
        DemoWindowMenuBar(demo_data);

        // PORT GAP: IMGUI_VERSION_NUM is not exposed by the wrapper; only the version string is shown.
        ImGui.Text($"dear imgui says hello! ({ImGui.GetVersion()})");
        ImGui.Spacing();

        if (ImGui.CollapsingHeader("Help"))
        {
            // DEMO MARKER: Help
            ImGui.SeparatorText("ABOUT THIS DEMO:");
            ImGui.BulletText("Sections below are demonstrating many aspects of the library.");
            ImGui.BulletText("The \"Examples\" menu above leads to more demo contents.");
            ImGui.BulletText("The \"Tools\" menu above gives access to: About Box, Style Editor,\n" +
                             "and Metrics/Debugger (general purpose Dear ImGui debugging tool).");
            ImGui.BulletText("Web demo (w/ source code browser): ");
            ImGui.SameLine(0, 0);
            ImGui.TextLinkOpenURL("https://pthom.github.io/imgui_explorer", "https://pthom.github.io/imgui_explorer");

            ImGui.SeparatorText("PROGRAMMER GUIDE:");
            ImGui.BulletText("See the ShowDemoWindow() code in imgui_demo.cpp. <- you are here!");
            ImGui.BulletText("See comments in imgui.cpp.");
            ImGui.BulletText("See example applications in the examples/ folder.");
            ImGui.BulletText("Read the FAQ at ");
            ImGui.SameLine(0, 0);
            ImGui.TextLinkOpenURL("https://www.dearimgui.com/faq/", "https://www.dearimgui.com/faq/");
            ImGui.BulletText("Set 'io.ConfigFlags |= NavEnableKeyboard' for keyboard controls.");
            ImGui.BulletText("Set 'io.ConfigFlags |= NavEnableGamepad' for gamepad controls.");

            ImGui.SeparatorText("USER GUIDE:");
            ImGui.ShowUserGuide();
        }

        if (ImGui.CollapsingHeader("Configuration"))
        {
            // (upstream: ImGuiIO& io = ImGui::GetIO(); — the wrapper exposes IO via the static Io class)

            if (ImGui.TreeNode("Configuration##2"))
            {
                // DEMO MARKER: Configuration
                ImGui.SeparatorText("General");
                int config_flags = (int)Io.ConfigFlags;
                ImGui.CheckboxFlags("io.ConfigFlags: NavEnableKeyboard", ref config_flags, (int)ConfigFlags.NavEnableKeyboard);
                ImGui.SameLine(); HelpMarker("Enable keyboard controls.");
                ImGui.CheckboxFlags("io.ConfigFlags: NavEnableGamepad", ref config_flags, (int)ConfigFlags.NavEnableGamepad);
                ImGui.SameLine(); HelpMarker("Enable gamepad controls. Require backend to set io.BackendFlags |= ImGuiBackendFlags_HasGamepad.\n\nRead instructions in imgui.cpp for details.");
                ImGui.CheckboxFlags("io.ConfigFlags: NoMouse", ref config_flags, (int)ConfigFlags.NoMouse);
                ImGui.SameLine(); HelpMarker("Instruct dear imgui to disable mouse inputs and interactions.");

                // The "NoMouse" option can get us stuck with a disabled mouse! Let's provide an alternative way to fix it:
                if ((config_flags & (int)ConfigFlags.NoMouse) != 0)
                {
                    if ((float)(ImGui.GetTime() % 0.40) < 0.20f)
                    {
                        ImGui.SameLine();
                        ImGui.Text("<<PRESS SPACE TO DISABLE>>");
                    }
                    // Prevent both being checked
                    if (ImGui.IsKeyPressed(Key.Space) || (config_flags & (int)ConfigFlags.NoKeyboard) != 0)
                        config_flags &= ~(int)ConfigFlags.NoMouse;
                }

                ImGui.CheckboxFlags("io.ConfigFlags: NoMouseCursorChange", ref config_flags, (int)ConfigFlags.NoMouseCursorChange);
                ImGui.SameLine(); HelpMarker("Instruct backend to not alter mouse cursor shape and visibility.");
                ImGui.CheckboxFlags("io.ConfigFlags: NoKeyboard", ref config_flags, (int)ConfigFlags.NoKeyboard);
                ImGui.SameLine(); HelpMarker("Instruct dear imgui to disable keyboard inputs and interactions.");
                Io.ConfigFlags = (ConfigFlags)config_flags;

                IoCheckbox("io.ConfigInputTrickleEventQueue", static () => Io.ConfigInputTrickleEventQueue, static v => Io.ConfigInputTrickleEventQueue = v);
                ImGui.SameLine(); HelpMarker("Enable input queue trickling: some types of events submitted during the same frame (e.g. button down + up) will be spread over multiple frames, improving interactions with low framerates.");
                IoCheckbox("io.MouseDrawCursor", static () => Io.MouseDrawCursor, static v => Io.MouseDrawCursor = v);
                ImGui.SameLine(); HelpMarker("Instruct Dear ImGui to render a mouse cursor itself. Note that a mouse cursor rendered via your application GPU rendering path will feel more laggy than hardware cursor, but will be more in sync with your other visuals.\n\nSome desktop applications may use both kinds of cursors (e.g. enable software cursor only when resizing/dragging something).");

                ImGui.SeparatorText("Keyboard/Gamepad Navigation");
                IoCheckbox("io.ConfigNavSwapGamepadButtons", static () => Io.ConfigNavSwapGamepadButtons, static v => Io.ConfigNavSwapGamepadButtons = v);
                IoCheckbox("io.ConfigNavMoveSetMousePos", static () => Io.ConfigNavMoveSetMousePos, static v => Io.ConfigNavMoveSetMousePos = v);
                ImGui.SameLine(); HelpMarker("Directional/tabbing navigation teleports the mouse cursor. May be useful on TV/console systems where moving a virtual mouse is difficult");
                IoCheckbox("io.ConfigNavCaptureKeyboard", static () => Io.ConfigNavCaptureKeyboard, static v => Io.ConfigNavCaptureKeyboard = v);
                IoCheckbox("io.ConfigNavEscapeClearFocusItem", static () => Io.ConfigNavEscapeClearFocusItem, static v => Io.ConfigNavEscapeClearFocusItem = v);
                ImGui.SameLine(); HelpMarker("Pressing Escape clears focused item.");
                IoCheckbox("io.ConfigNavEscapeClearFocusWindow", static () => Io.ConfigNavEscapeClearFocusWindow, static v => Io.ConfigNavEscapeClearFocusWindow = v);
                ImGui.SameLine(); HelpMarker("Pressing Escape clears focused window.");
                IoCheckbox("io.ConfigNavCursorVisibleAuto", static () => Io.ConfigNavCursorVisibleAuto, static v => Io.ConfigNavCursorVisibleAuto = v);
                ImGui.SameLine(); HelpMarker("Using directional navigation key makes the cursor visible. Mouse click hides the cursor.");
                IoCheckbox("io.ConfigNavCursorVisibleAlways", static () => Io.ConfigNavCursorVisibleAlways, static v => Io.ConfigNavCursorVisibleAlways = v);
                ImGui.SameLine(); HelpMarker("Navigation cursor is always visible.");

                ImGui.SeparatorText("Windows");
                IoCheckbox("io.ConfigWindowsResizeFromEdges", static () => Io.ConfigWindowsResizeFromEdges, static v => Io.ConfigWindowsResizeFromEdges = v);
                ImGui.SameLine(); HelpMarker("Enable resizing of windows from their edges and from the lower-left corner.\nThis requires ImGuiBackendFlags_HasMouseCursors for better mouse cursor feedback.");
                IoCheckbox("io.ConfigWindowsMoveFromTitleBarOnly", static () => Io.ConfigWindowsMoveFromTitleBarOnly, static v => Io.ConfigWindowsMoveFromTitleBarOnly = v);
                IoCheckbox("io.ConfigWindowsCopyContentsWithCtrlC", static () => Io.ConfigWindowsCopyContentsWithCtrlC, static v => Io.ConfigWindowsCopyContentsWithCtrlC = v); // [EXPERIMENTAL]
                ImGui.SameLine(); HelpMarker("*EXPERIMENTAL* Ctrl+C copy the contents of focused window into the clipboard.\n\nExperimental because:\n- (1) has known issues with nested Begin/End pairs.\n- (2) text output quality varies.\n- (3) text output is in submission order rather than spatial order.");
                IoCheckbox("io.ConfigScrollbarScrollByPage", static () => Io.ConfigScrollbarScrollByPage, static v => Io.ConfigScrollbarScrollByPage = v);
                ImGui.SameLine(); HelpMarker("Enable scrolling page by page when clicking outside the scrollbar grab.\nWhen disabled, always scroll to clicked location.\nWhen enabled, Shift+Click scrolls to clicked location.");

                ImGui.SeparatorText("Widgets");
                IoCheckbox("io.ConfigInputTextCursorBlink", static () => Io.ConfigInputTextCursorBlink, static v => Io.ConfigInputTextCursorBlink = v);
                ImGui.SameLine(); HelpMarker("Enable blinking cursor (optional as some users consider it to be distracting).");
                IoCheckbox("io.ConfigInputTextEnterKeepActive", static () => Io.ConfigInputTextEnterKeepActive, static v => Io.ConfigInputTextEnterKeepActive = v);
                ImGui.SameLine(); HelpMarker("Pressing Enter will reactivate item and select all text (single-line only).");
                IoCheckbox("io.ConfigDragClickToInputText", static () => Io.ConfigDragClickToInputText, static v => Io.ConfigDragClickToInputText = v);
                ImGui.SameLine(); HelpMarker("Enable turning DragXXX widgets into text input with a simple mouse click-release (without moving).");
                IoCheckbox("io.ConfigMacOSXBehaviors", static () => Io.ConfigMacOSXBehaviors, static v => Io.ConfigMacOSXBehaviors = v);
                ImGui.SameLine(); HelpMarker("Swap Cmd<>Ctrl keys, enable various MacOS style behaviors.");
                ImGui.Text("Also see Style->Rendering for rendering options.");

                // Also read: https://github.com/ocornut/imgui/wiki/Error-Handling
                ImGui.SeparatorText("Error Handling");

                IoCheckbox("io.ConfigErrorRecovery", static () => Io.ConfigErrorRecovery, static v => Io.ConfigErrorRecovery = v);
                ImGui.SameLine(); HelpMarker(
                    "Options to configure how we handle recoverable errors.\n" +
                    "- Error recovery is not perfect nor guaranteed! It is a feature to ease development.\n" +
                    "- You not are not supposed to rely on it in the course of a normal application run.\n" +
                    "- Possible usage: facilitate recovery from errors triggered from a scripting language or after specific exceptions handlers.\n" +
                    "- Always ensure that on programmers seat you have at minimum Asserts or Tooltips enabled when making direct imgui API call! " +
                    "Otherwise it would severely hinder your ability to catch and correct mistakes!");
                IoCheckbox("io.ConfigErrorRecoveryEnableAssert", static () => Io.ConfigErrorRecoveryEnableAssert, static v => Io.ConfigErrorRecoveryEnableAssert = v);
                IoCheckbox("io.ConfigErrorRecoveryEnableDebugLog", static () => Io.ConfigErrorRecoveryEnableDebugLog, static v => Io.ConfigErrorRecoveryEnableDebugLog = v);
                IoCheckbox("io.ConfigErrorRecoveryEnableTooltip", static () => Io.ConfigErrorRecoveryEnableTooltip, static v => Io.ConfigErrorRecoveryEnableTooltip = v);
                if (!Io.ConfigErrorRecoveryEnableAssert && !Io.ConfigErrorRecoveryEnableDebugLog && !Io.ConfigErrorRecoveryEnableTooltip)
                {
                    Io.ConfigErrorRecoveryEnableAssert = true;
                    Io.ConfigErrorRecoveryEnableDebugLog = true;
                    Io.ConfigErrorRecoveryEnableTooltip = true;
                }

                // Also read: https://github.com/ocornut/imgui/wiki/Debug-Tools
                ImGui.SeparatorText("Debug");
                IoCheckbox("io.ConfigDebugIsDebuggerPresent", static () => Io.ConfigDebugIsDebuggerPresent, static v => Io.ConfigDebugIsDebuggerPresent = v);
                ImGui.SameLine(); HelpMarker("Enable various tools calling IM_DEBUG_BREAK().\n\nRequires a debugger being attached, otherwise IM_DEBUG_BREAK() options will appear to crash your application.");
                IoCheckbox("io.ConfigDebugHighlightIdConflicts", static () => Io.ConfigDebugHighlightIdConflicts, static v => Io.ConfigDebugHighlightIdConflicts = v);
                ImGui.SameLine(); HelpMarker("Highlight and show an error message when multiple items have conflicting identifiers.");
                ImGui.BeginDisabled();
                IoCheckbox("io.ConfigDebugBeginReturnValueOnce", static () => Io.ConfigDebugBeginReturnValueOnce, static v => Io.ConfigDebugBeginReturnValueOnce = v);
                ImGui.EndDisabled();
                ImGui.SameLine(); HelpMarker("First calls to Begin()/BeginChild() will return false.\n\nTHIS OPTION IS DISABLED because it needs to be set at application boot-time to make sense. Showing the disabled option is a way to make this feature easier to discover.");
                IoCheckbox("io.ConfigDebugBeginReturnValueLoop", static () => Io.ConfigDebugBeginReturnValueLoop, static v => Io.ConfigDebugBeginReturnValueLoop = v);
                ImGui.SameLine(); HelpMarker("Some calls to Begin()/BeginChild() will return false.\n\nWill cycle through window depths then repeat. Windows should be flickering while running.");
                IoCheckbox("io.ConfigDebugIgnoreFocusLoss", static () => Io.ConfigDebugIgnoreFocusLoss, static v => Io.ConfigDebugIgnoreFocusLoss = v);
                ImGui.SameLine(); HelpMarker("Option to deactivate io.AddFocusEvent(false) handling. May facilitate interactions with a debugger when focus loss leads to clearing inputs data.");
                IoCheckbox("io.ConfigDebugIniSettings", static () => Io.ConfigDebugIniSettings, static v => Io.ConfigDebugIniSettings = v);
                ImGui.SameLine(); HelpMarker("Option to save .ini data with extra comments (particularly helpful for Docking, but makes saving slower).");

                ImGui.TreePop();
                ImGui.Spacing();
            }

            if (ImGui.TreeNode("Backend Flags"))
            {
                // DEMO MARKER: Configuration/Backend Flags
                HelpMarker(
                    "Those flags are set by the backends (imgui_impl_xxx files) to specify their capabilities.\n" +
                    "Here we expose them as read-only fields to avoid breaking interactions with your backend.");

                // FIXME: Maybe we need a BeginReadonly() equivalent to keep label bright?
                ImGui.BeginDisabled();
                int backend_flags = (int)Io.BackendFlags;
                ImGui.CheckboxFlags("io.BackendFlags: HasGamepad", ref backend_flags, (int)BackendFlags.HasGamepad);
                ImGui.CheckboxFlags("io.BackendFlags: HasMouseCursors", ref backend_flags, (int)BackendFlags.HasMouseCursors);
                ImGui.CheckboxFlags("io.BackendFlags: HasSetMousePos", ref backend_flags, (int)BackendFlags.HasSetMousePos);
                ImGui.CheckboxFlags("io.BackendFlags: RendererHasVtxOffset", ref backend_flags, (int)BackendFlags.RendererHasVtxOffset);
                ImGui.CheckboxFlags("io.BackendFlags: RendererHasTextures", ref backend_flags, (int)BackendFlags.RendererHasTextures);
                ImGui.EndDisabled();

                ImGui.TreePop();
                ImGui.Spacing();
            }

            if (ImGui.TreeNode("Style, Fonts"))
            {
                // DEMO MARKER: Configuration/Style, Fonts
                ImGui.Checkbox("Style Editor", ref demo_data.ShowStyleEditor);
                ImGui.SameLine();
                HelpMarker("The same contents can be accessed in 'Tools->Style Editor' or by calling the ShowStyleEditor() function.");
                ImGui.TreePop();
                ImGui.Spacing();
            }

            if (ImGui.TreeNode("Capture/Logging"))
            {
                // DEMO MARKER: Configuration/Capture, Logging
                HelpMarker(
                    "The logging API redirects all text output so you can easily capture the content of " +
                    "a window or a block. Tree nodes can be automatically expanded.\n" +
                    "Try opening any of the contents below in this window and then click one of the \"Log To\" button.");
                ImGui.LogButtons();

                HelpMarker("You can also call ImGui::LogText() to output directly to the log without a visual output.");
                if (ImGui.Button("Copy \"Hello, world!\" to clipboard"))
                {
                    ImGui.LogToClipboard();
                    ImGui.LogText("Hello, world!");
                    ImGui.LogFinish();
                }
                ImGui.TreePop();
            }
        }

        if (ImGui.CollapsingHeader("Window options"))
        {
            // DEMO MARKER: Window options
            if (ImGui.BeginTable("split", 3))
            {
                ImGui.TableNextColumn(); ImGui.Checkbox("No titlebar", ref s_no_titlebar);
                ImGui.TableNextColumn(); ImGui.Checkbox("No scrollbar", ref s_no_scrollbar);
                ImGui.TableNextColumn(); ImGui.Checkbox("No menu", ref s_no_menu);
                ImGui.TableNextColumn(); ImGui.Checkbox("No move", ref s_no_move);
                ImGui.TableNextColumn(); ImGui.Checkbox("No resize", ref s_no_resize);
                ImGui.TableNextColumn(); ImGui.Checkbox("No collapse", ref s_no_collapse);
                ImGui.TableNextColumn(); ImGui.Checkbox("No close", ref s_no_close);
                ImGui.TableNextColumn(); ImGui.Checkbox("No nav", ref s_no_nav);
                ImGui.TableNextColumn(); ImGui.Checkbox("No background", ref s_no_background);
                ImGui.TableNextColumn(); ImGui.Checkbox("No bring to front", ref s_no_bring_to_front);
                ImGui.TableNextColumn(); ImGui.Checkbox("Unsaved document", ref s_unsaved_document);
                ImGui.EndTable();
            }
        }

        // All demo contents
        DemoWindowWidgets(demo_data);
        DemoWindowLayout();
        DemoWindowPopups();
        DemoWindowTables();
        // (upstream calls DemoWindowColumns() from the end of DemoWindowTables(), not from here)
        DemoWindowInputs();

        // End of ShowDemoWindow()
        ImGui.PopItemWidth();
        ImGui.End();
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowMenuBar()
    //-----------------------------------------------------------------------------

    private static void DemoWindowMenuBar(DemoWindowData demo_data)
    {
        if (ImGui.BeginMenuBar())
        {
            if (ImGui.BeginMenu("Menu"))
            {
                // DEMO MARKER: Menu/File
                ShowExampleMenuFile();
                ImGui.EndMenu();
            }
            if (ImGui.BeginMenu("Examples"))
            {
                // DEMO MARKER: Menu/Examples
                ImGui.MenuItem("Main menu bar", null, ref demo_data.ShowMainMenuBar);

                ImGui.SeparatorText("Mini apps");
                ImGui.MenuItem("Assets Browser", null, ref demo_data.ShowAppAssetsBrowser);
                ImGui.MenuItem("Console", null, ref demo_data.ShowAppConsole);
                ImGui.MenuItem("Custom rendering", null, ref demo_data.ShowAppCustomRendering);
                ImGui.MenuItem("Documents", null, ref demo_data.ShowAppDocuments);
                ImGui.MenuItem("Log", null, ref demo_data.ShowAppLog);
                ImGui.MenuItem("Property editor", null, ref demo_data.ShowAppPropertyEditor);
                ImGui.MenuItem("Simple layout", null, ref demo_data.ShowAppLayout);
                ImGui.MenuItem("Simple overlay", null, ref demo_data.ShowAppSimpleOverlay);

                ImGui.SeparatorText("Concepts");
                ImGui.MenuItem("Auto-resizing window", null, ref demo_data.ShowAppAutoResize);
                ImGui.MenuItem("Constrained-resizing window", null, ref demo_data.ShowAppConstrainedResize);
                ImGui.MenuItem("Fullscreen window", null, ref demo_data.ShowAppFullscreen);
                ImGui.MenuItem("Long text display", null, ref demo_data.ShowAppLongText);
                ImGui.MenuItem("Manipulating window titles", null, ref demo_data.ShowAppWindowTitles);

                ImGui.EndMenu();
            }
            //if (ImGui.MenuItem("MenuItem")) {} // You can also use MenuItem() inside a menu bar!
            if (ImGui.BeginMenu("Tools"))
            {
                // DEMO MARKER: Menu/Tools
                // (upstream: IMGUI_DISABLE_DEBUG_TOOLS is not defined in this build)
                bool has_debug_tools = true;
                ImGui.MenuItem("Metrics/Debugger", null, ref demo_data.ShowMetrics, has_debug_tools);
                if (ImGui.BeginMenu("Debug Options"))
                {
                    ImGui.BeginDisabled(!has_debug_tools);
                    IoCheckbox("Highlight ID Conflicts", static () => Io.ConfigDebugHighlightIdConflicts, static v => Io.ConfigDebugHighlightIdConflicts = v);
                    ImGui.EndDisabled();
                    IoCheckbox("Assert on error recovery", static () => Io.ConfigErrorRecoveryEnableAssert, static v => Io.ConfigErrorRecoveryEnableAssert = v);
                    ImGui.TextDisabled("(see Demo->Configuration for details & more)");
                    ImGui.EndMenu();
                }
                ImGui.MenuItem("Debug Log", null, ref demo_data.ShowDebugLog, has_debug_tools);
                ImGui.MenuItem("ID Stack Tool", null, ref demo_data.ShowIDStackTool, has_debug_tools);
                bool is_debugger_present = Io.ConfigDebugIsDebuggerPresent;
                if (ImGui.MenuItem("Item Picker", null, false, has_debug_tools))// && is_debugger_present))
                    ImGui.DebugStartItemPicker();
                if (!is_debugger_present)
                    ImGui.SetItemTooltip("Requires io.ConfigDebugIsDebuggerPresent=true to be set.\n\nWe otherwise disable some extra features to avoid casual users crashing the application.");
                ImGui.MenuItem("Style Editor", null, ref demo_data.ShowStyleEditor);
                ImGui.MenuItem("About Dear ImGui", null, ref demo_data.ShowAbout);

                ImGui.EndMenu();
            }
            ImGui.EndMenuBar();
        }
    }
}
