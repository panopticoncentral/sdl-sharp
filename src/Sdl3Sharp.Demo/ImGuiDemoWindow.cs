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
                    if (((float)ImGui.ImGui.GetTime() % 0.40f) < 0.20f)
                    {
                        Widgets.SameLine();
                        Widgets.Text("<<PRESS SPACE TO DISABLE>>"u8);
                    }

                    // Prevent both being checked
                    if (ImGui.ImGui.IsKeyPressed(Key.Space) || io.ConfigFlags.HasFlag(ConfigFlags.NoKeyboard))
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
                    io.ConfigErrorRecoveryEnableAssert = io.ConfigErrorRecoveryEnableDebugLog = io.ConfigErrorRecoveryEnableTooltip = true;

                // Also read: https://github.com/ocornut/imgui/wiki/Debug-Tools
                Widgets.SeparatorText("Debug"u8);
                _ = Widgets.Checkbox("io.ConfigDebugIsDebuggerPresent"u8, ref io.ConfigDebugIsDebuggerPresent);
                Widgets.SameLine();
                HelpMarker("Enable various tools calling IM_DEBUG_BREAK().\n\nRequires a debugger being attached, otherwise IM_DEBUG_BREAK() options will appear to crash your application."u8);
                _ = Widgets.Checkbox("io.ConfigDebugHighlightIdConflicts"u8, ref io.ConfigDebugHighlightIdConflicts);
                Widgets.SameLine();
                HelpMarker("Highlight and show an error message when multiple items have conflicting identifiers."u8);
                // TODO: Widgets.BeginDisabled();
                _ = Widgets.Checkbox("io.ConfigDebugBeginReturnValueOnce"u8, ref io.ConfigDebugBeginReturnValueOnce);
                // TODO: Widgets.EndDisabled();
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
                // TODO: Widgets.BeginDisabled();
                _ = Widgets.CheckboxFlags("io.BackendFlags: HasGamepad"u8, ref io.BackendFlags, BackendFlags.HasGamepad);
                _ = Widgets.CheckboxFlags("io.BackendFlags: HasMouseCursors"u8, ref io.BackendFlags, BackendFlags.HasMouseCursors);
                _ = Widgets.CheckboxFlags("io.BackendFlags: HasSetMousePos"u8, ref io.BackendFlags, BackendFlags.HasSetMousePos);
                _ = Widgets.CheckboxFlags("io.BackendFlags: RendererHasVtxOffset"u8, ref io.BackendFlags, BackendFlags.RendererHasVtxOffset);
                _ = Widgets.CheckboxFlags("io.BackendFlags: RendererHasTextures"u8, ref io.BackendFlags, BackendFlags.RendererHasTextures);
                // TODO: Widgets.EndDisabled();

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
                ImGui.ImGui.LogButtons();

                HelpMarker("You can also call ImGui::LogText() to output directly to the log without a visual output."u8);
                if (Widgets.Button("Copy \"Hello, world!\" to clipboard"u8))
                {
                    ImGui.ImGui.LogToClipboard();
                    ImGui.ImGui.LogText("Hello, world!"u8);
                    ImGui.ImGui.LogFinish();
                }
                Widgets.TreePop();
            }
        }

        /*
    IMGUI_DEMO_MARKER("Help");

    IMGUI_DEMO_MARKER("Configuration");

    IMGUI_DEMO_MARKER("Window options");
    if (ImGui::CollapsingHeader("Window options"))
    {
        if (ImGui::BeginTable("split", 3))
        {
            ImGui::TableNextColumn(); ImGui::Checkbox("No titlebar", &no_titlebar);
            ImGui::TableNextColumn(); ImGui::Checkbox("No scrollbar", &no_scrollbar);
            ImGui::TableNextColumn(); ImGui::Checkbox("No menu", &no_menu);
            ImGui::TableNextColumn(); ImGui::Checkbox("No move", &no_move);
            ImGui::TableNextColumn(); ImGui::Checkbox("No resize", &no_resize);
            ImGui::TableNextColumn(); ImGui::Checkbox("No collapse", &no_collapse);
            ImGui::TableNextColumn(); ImGui::Checkbox("No close", &no_close);
            ImGui::TableNextColumn(); ImGui::Checkbox("No nav", &no_nav);
            ImGui::TableNextColumn(); ImGui::Checkbox("No background", &no_background);
            ImGui::TableNextColumn(); ImGui::Checkbox("No bring to front", &no_bring_to_front);
            ImGui::TableNextColumn(); ImGui::Checkbox("Unsaved document", &unsaved_document);
            ImGui::EndTable();
        }
    }

    // All demo contents
    DemoWindowWidgets(&demo_data);
    DemoWindowLayout();
    DemoWindowPopups();
    DemoWindowTables();
    DemoWindowInputs();

         */

        Style.PopItemWidth();
        Window.End();
    }

    // Helper to display a little (?) mark which shows a tooltip when hovered.
    // In your own code you may want to display an actual icon if you are using a merged icon fonts (see docs/FONTS.md)
    private static void HelpMarker(ReadOnlySpan<byte> desc)
    {
        Widgets.TextDisabled("(?)"u8);
        if (Widgets.BeginItemTooltip())
        {
            Style.PushTextWrapPosition(Font.GetFontSize() * 35.0f);
            Widgets.TextUnformatted(desc);
            Style.PopTextWrapPosition();
            Widgets.EndTooltip();
        }
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
                Widgets.Checkbox("Highlight ID Conflicts"u8, ref io.ConfigDebugHighlightIdConflicts);
                // TODO: Widgets.EndDisabled();
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
