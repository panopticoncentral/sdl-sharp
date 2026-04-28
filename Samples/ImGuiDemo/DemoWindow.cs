// C# port of imgui_demo.cpp's ShowDemoWindow, used as a coverage test of the
// SdlSharp.ImGui wrappers. Mirrors the upstream organization: a single main
// entry point that shows a tabbed/sectioned window exercising as many widgets
// and APIs as the wrapper currently supports.
//
// Upstream reference: ../imgui/imgui_demo.cpp (~11k lines)
//
// What is ported (covers most common widgets and layout patterns):
//   - Menu bar (File / Examples / Tools / Help) — non-functional placeholders
//   - Help section (CollapsingHeader, BulletText, separators, links, ShowUserGuide)
//   - Window options (Checkbox in BeginTable controlling window flags)
//   - Widgets:
//       Basic, Bullets, Trees (subset), Collapsing Headers, Color/Pickers,
//       Combo (Begin/End form), Drags & Sliders (subset), List Boxes (Begin/End),
//       Plotting (Lines/Histogram), Progress Bars, Selectables (subset),
//       Tabs, Text (subset), Tooltips, Disabled blocks
//   - Layout: Child Windows, Widgets Width, Basic Horizontal Layout, Groups,
//             Text Baseline Alignment, Scrolling (subset)
//   - Popups: basic OpenPopup/BeginPopup, modals, context menus
//   - Tables: basic table, borders, row backgrounds, sortable
//
// What is NOT ported and would still need work — see TODO.txt at the bottom of
// this file for a full punch-list, including the wrapper additions required.

using SdlSharp.ImGui;

namespace ImGuiDemo;

/// <summary>
/// C# port of imgui_demo.cpp's ShowDemoWindow. State that mirrors the C++
/// "static" locals in each section is held as private static fields here.
/// </summary>
internal static class DemoWindow
{
    // -------------------------------------------------------------------------
    // SECTION: Top-level window options
    // -------------------------------------------------------------------------
    static bool s_no_titlebar;
    static bool s_no_scrollbar;
    static bool s_no_menu;
    static bool s_no_move;
    static bool s_no_resize;
    static bool s_no_collapse;
    static bool s_no_close;
    static bool s_no_nav;
    static bool s_no_background;
    static bool s_no_bring_to_front;
    static bool s_unsaved_document;

    // Tool windows (toggled from the menu bar).
    static bool s_show_about;
    static bool s_show_metrics;
    static bool s_show_debug_log;
    static bool s_show_id_stack_tool;
    static bool s_show_style_editor_window;

    public static void Show(ref bool open)
    {
        // Demonstrate the use of the menus to allow toggling of the demo's own
        // tool windows.
        if (s_show_about) ImGui.ShowAboutWindow(ref s_show_about);
        if (s_show_metrics) ImGui.ShowMetricsWindow(ref s_show_metrics);
        if (s_show_debug_log) ImGui.ShowDebugLogWindow(ref s_show_debug_log);
        if (s_show_id_stack_tool) ImGui.ShowIDStackToolWindow(ref s_show_id_stack_tool);
        if (s_show_style_editor_window)
        {
            ImGui.SetNextWindowSize(420, 540, Cond.FirstUseEver);
            if (ImGui.Begin("Dear ImGui Style Editor", ref s_show_style_editor_window))
                ImGui.ShowStyleEditor();
            ImGui.End();
        }

        var flags = WindowFlags.None;
        if (s_no_titlebar)        flags |= WindowFlags.NoTitleBar;
        if (s_no_scrollbar)       flags |= WindowFlags.NoScrollbar;
        if (!s_no_menu)           flags |= WindowFlags.MenuBar;
        if (s_no_move)            flags |= WindowFlags.NoMove;
        if (s_no_resize)          flags |= WindowFlags.NoResize;
        if (s_no_collapse)        flags |= WindowFlags.NoCollapse;
        if (s_no_nav)             flags |= WindowFlags.NoNav;
        if (s_no_background)      flags |= WindowFlags.NoBackground;
        if (s_no_bring_to_front)  flags |= WindowFlags.NoBringToFrontOnFocus;
        if (s_unsaved_document)   flags |= WindowFlags.UnsavedDocument;

        // Default position/size mirrors the C++ demo so first-launch placement
        // looks the same.
        var viewport = ImGui.GetMainViewport();
        ImGui.SetNextWindowPos(viewport.WorkPos.X + 20, viewport.WorkPos.Y + 20, Cond.FirstUseEver);
        ImGui.SetNextWindowSize(620, 700, Cond.FirstUseEver);

        bool windowOpen;
        if (s_no_close)
        {
            windowOpen = ImGui.Begin("Dear ImGui Demo (C# port)", flags);
        }
        else
        {
            windowOpen = ImGui.Begin("Dear ImGui Demo (C# port)", ref open, flags);
        }
        if (!windowOpen)
        {
            ImGui.End();
            return;
        }

        // The label width trick from the upstream demo: reserve fixed pixels
        // for labels so framed widgets get the rest.
        var labelWidth = MathF.Min(ImGui.GetFontSize() * 12f, ImGui.GetContentRegionAvail().Width * 0.40f);
        ImGui.PushItemWidth(-labelWidth);

        ShowMenuBar();

        ImGui.Text($"dear imgui says hello! ({ImGui.GetVersion()})");
        ImGui.Spacing();

        ShowHelpSection();
        ShowWindowOptionsSection();
        ShowWidgetsSection();
        ShowLayoutSection();
        ShowPopupsSection();
        ShowTablesSection();

        ImGui.PopItemWidth();
        ImGui.End();
    }

    // -------------------------------------------------------------------------
    // SECTION: HelpMarker — equivalent to the static helper in imgui_demo.cpp
    // -------------------------------------------------------------------------
    public static void HelpMarker(string desc)
    {
        ImGui.TextDisabled("(?)");
        if (ImGui.BeginItemTooltip())
        {
            ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35f);
            ImGui.Text(desc);
            ImGui.PopTextWrapPos();
            ImGui.EndTooltip();
        }
    }

    // -------------------------------------------------------------------------
    // SECTION: Menu bar
    // -------------------------------------------------------------------------
    static void ShowMenuBar()
    {
        if (!ImGui.BeginMenuBar()) return;

        if (ImGui.BeginMenu("File"))
        {
            // The upstream demo here also has Open/Save/etc. We expose a small
            // subset; "Quit" is handled by the close button in the title bar.
            ImGui.MenuItem("(demo menu)", null, false, enabled: false);
            if (ImGui.MenuItem("New")) { /* placeholder */ }
            if (ImGui.MenuItem("Open", "Ctrl+O")) { /* placeholder */ }
            if (ImGui.BeginMenu("Open Recent"))
            {
                ImGui.MenuItem("fish_hat.c");
                ImGui.MenuItem("fish_hat.inl");
                ImGui.MenuItem("fish_hat.h");
                ImGui.EndMenu();
            }
            if (ImGui.MenuItem("Save", "Ctrl+S")) { /* placeholder */ }
            ImGui.Separator();
            if (ImGui.BeginMenu("Options"))
            {
                ImGui.MenuItem("Enabled", null, ref s_unsaved_document);
                ImGui.EndMenu();
            }
            ImGui.Separator();
            if (ImGui.MenuItem("Quit", "Alt+F4")) { /* placeholder — close via X */ }
            ImGui.EndMenu();
        }

        if (ImGui.BeginMenu("Examples"))
        {
            // The full demo has lots of "Show ExampleApp*" toggles. We only
            // wire the ones we ship.
            ImGui.MenuItem("(no example apps ported yet)", null, false, enabled: false);
            ImGui.EndMenu();
        }

        if (ImGui.BeginMenu("Tools"))
        {
            ImGui.MenuItem("Metrics/Debugger", null, ref s_show_metrics);
            ImGui.MenuItem("Debug Log", null, ref s_show_debug_log);
            ImGui.MenuItem("ID Stack Tool", null, ref s_show_id_stack_tool);
            ImGui.MenuItem("Style Editor", null, ref s_show_style_editor_window);
            ImGui.MenuItem("About Dear ImGui", null, ref s_show_about);
            ImGui.EndMenu();
        }
        ImGui.EndMenuBar();
    }

    // -------------------------------------------------------------------------
    // SECTION: Help
    // -------------------------------------------------------------------------
    static void ShowHelpSection()
    {
        if (!ImGui.CollapsingHeader("Help")) return;

        ImGui.SeparatorText("ABOUT THIS DEMO:");
        ImGui.BulletText("This is a C# port of imgui_demo.cpp covering a subset of widgets.");
        ImGui.BulletText("It exercises the SdlSharp.ImGui wrappers as a coverage test.");
        ImGui.BulletText("The \"Tools\" menu above gives access to: About Box, Style Editor,");
        ImGui.BulletText("    Metrics/Debugger, Debug Log, ID Stack Tool.");

        ImGui.SeparatorText("PROGRAMMER GUIDE:");
        ImGui.BulletText("See Samples/ImGuiDemo/DemoWindow.cs (the C# port) and");
        ImGui.BulletText("    src/SdlSharp.ImGui/ImGui.cs (the wrapper).");
        ImGui.BulletText("Read the FAQ at ");
        ImGui.SameLine(0, 0);
        ImGui.TextLinkOpenURL("https://www.dearimgui.com/faq/", "https://www.dearimgui.com/faq/");

        ImGui.SeparatorText("USER GUIDE:");
        ImGui.ShowUserGuide();
    }

    // -------------------------------------------------------------------------
    // SECTION: Window options (table of checkboxes for window flags)
    // -------------------------------------------------------------------------
    static void ShowWindowOptionsSection()
    {
        if (!ImGui.CollapsingHeader("Window options")) return;

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

    // -------------------------------------------------------------------------
    // SECTION: Widgets — the bulk of what this port exercises
    // -------------------------------------------------------------------------
    static void ShowWidgetsSection()
    {
        if (!ImGui.CollapsingHeader("Widgets")) return;

        ShowWidgetsBasic();
        ShowWidgetsBullets();
        ShowWidgetsCollapsingHeaders();
        ShowWidgetsTrees();
        ShowWidgetsTextWidgets();
        ShowWidgetsTooltips();
        ShowWidgetsColor();
        ShowWidgetsCombo();
        ShowWidgetsListBoxes();
        ShowWidgetsSelectables();
        ShowWidgetsTabs();
        ShowWidgetsDragsAndSliders();
        ShowWidgetsPlotting();
        ShowWidgetsProgressBars();
        ShowWidgetsImages();
        ShowWidgetsDisabled();
    }

    // -- Widgets/Basic --
    static int s_basic_clicked;
    static bool s_basic_check = true;
    static int s_basic_radio;
    static int s_basic_counter;
    static byte[] s_basic_input_text = StringBuffer("Hello, world!", 128);
    static byte[] s_basic_input_hint = new byte[128];
    static int s_basic_int = 123;
    static float s_basic_float = 0.001f;
    static double s_basic_double = 999999.00000001;
    static float s_basic_float_sci = 1e10f;
    static float[] s_basic_vec3 = new float[] { 0.10f, 0.20f, 0.30f };
    static int s_basic_drag_int1 = 50;
    static int s_basic_drag_int2 = 42;
    static int s_basic_drag_int3 = 128;
    static float s_basic_drag_float1 = 1.0f;
    static float s_basic_drag_float2 = 0.0067f;
    static int s_basic_slider_int;
    static float s_basic_slider_float1 = 0.123f;
    static float s_basic_slider_float2;
    static float s_basic_slider_angle;
    static int s_basic_elem;
    static float[] s_basic_col1 = { 1.0f, 0.0f, 0.2f };
    static float[] s_basic_col2 = { 0.4f, 0.7f, 0.0f, 0.5f };

    static void ShowWidgetsBasic()
    {
        if (!ImGui.TreeNode("Basic")) return;

        ImGui.SeparatorText("General");

        if (ImGui.Button("Button")) s_basic_clicked++;
        if ((s_basic_clicked & 1) != 0)
        {
            ImGui.SameLine();
            ImGui.Text("Thanks for clicking me!");
        }

        ImGui.Checkbox("checkbox", ref s_basic_check);

        ImGui.RadioButton("radio a", ref s_basic_radio, 0); ImGui.SameLine();
        ImGui.RadioButton("radio b", ref s_basic_radio, 1); ImGui.SameLine();
        ImGui.RadioButton("radio c", ref s_basic_radio, 2);

        ImGui.AlignTextToFramePadding();
        ImGui.TextLinkOpenURL("Hyperlink", "https://github.com/ocornut/imgui/wiki/Error-Handling");

        // Color buttons demonstrating PushID and per-button style colors.
        for (int i = 0; i < 7; i++)
        {
            if (i > 0) ImGui.SameLine();
            ImGui.PushID(i);
            var (r, g, b) = ImGui.ColorConvertHSVtoRGB(i / 7.0f, 0.6f, 0.6f);
            var (rh, gh, bh) = ImGui.ColorConvertHSVtoRGB(i / 7.0f, 0.7f, 0.7f);
            var (ra, ga, ba) = ImGui.ColorConvertHSVtoRGB(i / 7.0f, 0.8f, 0.8f);
            ImGui.PushStyleColor(Col.Button, r, g, b, 1f);
            ImGui.PushStyleColor(Col.ButtonHovered, rh, gh, bh, 1f);
            ImGui.PushStyleColor(Col.ButtonActive, ra, ga, ba, 1f);
            ImGui.Button("Click");
            ImGui.PopStyleColor(3);
            ImGui.PopID();
        }

        ImGui.AlignTextToFramePadding();
        ImGui.Text("Hold to repeat:");
        ImGui.SameLine();

        ImGui.PushItemFlag(ItemFlags.ButtonRepeat, true);
        if (ImGui.ArrowButton("##left", Dir.Left)) s_basic_counter--;
        ImGui.SameLine(0, 2);
        if (ImGui.ArrowButton("##right", Dir.Right)) s_basic_counter++;
        ImGui.PopItemFlag();
        ImGui.SameLine();
        ImGui.Text(s_basic_counter.ToString());

        ImGui.Button("Tooltip");
        if (ImGui.IsItemHovered()) ImGui.SetTooltip("I am a tooltip");

        ImGui.LabelText("label", "Value");

        ImGui.SeparatorText("Inputs");

        ImGui.InputText("input text", s_basic_input_text);
        ImGui.SameLine(); HelpMarker(
            "USER:\n" +
            "Hold Shift or use mouse to select text.\n" +
            "Ctrl+Left/Right to word jump.\n" +
            "Ctrl+A or Double-Click to select all.\n" +
            "Ctrl+X,Ctrl+C,Ctrl+V for clipboard.\n" +
            "Ctrl+Z to undo, Ctrl+Y/Ctrl+Shift+Z to redo.\n" +
            "Escape to revert.");

        ImGui.InputTextWithHint("input text (w/ hint)", "enter text here", s_basic_input_hint);

        ImGui.InputInt("input int", ref s_basic_int);
        ImGui.InputFloat("input float", ref s_basic_float, 0.01f, 1.0f, "%.3f");
        ImGui.InputDouble("input double", ref s_basic_double, 0.01, 1.0, "%.8f");
        ImGui.InputFloat("input scientific", ref s_basic_float_sci, 0f, 0f, "%e");
        ImGui.InputFloat3("input float3", s_basic_vec3);

        ImGui.SeparatorText("Drags");

        ImGui.DragInt("drag int", ref s_basic_drag_int1, 1);
        ImGui.SameLine(); HelpMarker("Click and drag to edit value.\nHold Shift/Alt for faster/slower edit.\nDouble-Click or Ctrl+Click to input value.");
        ImGui.DragInt("drag int 0..100", ref s_basic_drag_int2, 1, 0, 100, "%d%%", SliderFlags.AlwaysClamp);
        ImGui.DragInt("drag int wrap 100..200", ref s_basic_drag_int3, 1, 100, 200, "%d", SliderFlags.WrapAround);

        ImGui.DragFloat("drag float", ref s_basic_drag_float1, 0.005f);
        ImGui.DragFloat("drag small float", ref s_basic_drag_float2, 0.0001f, 0f, 0f, "%.06f ns");

        ImGui.SeparatorText("Sliders");

        ImGui.SliderInt("slider int", ref s_basic_slider_int, -1, 3);
        ImGui.SameLine(); HelpMarker("Ctrl+Click to input value.");

        ImGui.SliderFloat("slider float", ref s_basic_slider_float1, 0f, 1f, "ratio = %.3f");
        ImGui.SliderFloat("slider float (log)", ref s_basic_slider_float2, -10f, 10f, "%.4f", SliderFlags.Logarithmic);
        ImGui.SliderAngle("slider angle", ref s_basic_slider_angle);

        // Slider as enum: format string holds the chosen name, "%d" omitted.
        var elemNames = new[] { "Fire", "Earth", "Air", "Water" };
        var elemName = (s_basic_elem >= 0 && s_basic_elem < elemNames.Length) ? elemNames[s_basic_elem] : "Unknown";
        ImGui.SliderInt("slider enum", ref s_basic_elem, 0, elemNames.Length - 1, elemName);
        ImGui.SameLine(); HelpMarker("Using the format string parameter to display a name instead of the underlying integer.");

        ImGui.SeparatorText("Selectors/Pickers");

        ImGui.ColorEdit3("color 1", ref s_basic_col1[0], ref s_basic_col1[1], ref s_basic_col1[2]);
        ImGui.SameLine(); HelpMarker(
            "Click on the color square to open a color picker.\n" +
            "Click and hold to use drag and drop.\n" +
            "Right-Click on the color square to show options.\n" +
            "Ctrl+Click on individual component to input value.");
        ImGui.ColorEdit4("color 2", s_basic_col2);

        ImGui.TreePop();
    }

    // -- Widgets/Bullets --
    static void ShowWidgetsBullets()
    {
        if (!ImGui.TreeNode("Bullets")) return;
        ImGui.BulletText("Bullet point 1");
        ImGui.BulletText("Bullet point 2\nOn multiple lines");
        if (ImGui.TreeNode("Tree node"))
        {
            ImGui.BulletText("Another bullet point");
            ImGui.TreePop();
        }
        ImGui.Bullet(); ImGui.Text("Bullet point 3 (two calls)");
        ImGui.Bullet(); ImGui.SmallButton("Button");
        ImGui.TreePop();
    }

    // -- Widgets/Collapsing Headers --
    static bool s_ch_closable_group = true;
    static void ShowWidgetsCollapsingHeaders()
    {
        if (!ImGui.TreeNode("Collapsing Headers")) return;

        ImGui.Checkbox("Show 2nd header", ref s_ch_closable_group);
        if (ImGui.CollapsingHeader("Header", TreeNodeFlags.None))
        {
            ImGui.Text("IsItemHovered: " + ImGui.IsItemHovered());
            for (int i = 0; i < 5; i++) ImGui.Text($"Some content {i}");
        }
        if (s_ch_closable_group)
        {
            if (ImGui.CollapsingHeader("Header with a close button", ref s_ch_closable_group))
            {
                ImGui.Text("IsItemHovered: " + ImGui.IsItemHovered());
                for (int i = 0; i < 5; i++) ImGui.Text($"More content {i}");
            }
        }
        ImGui.TreePop();
    }

    // -- Widgets/Trees --
    static int s_tree_selection_mask = 1 << 2;
    static bool s_tree_align_label = true;
    static bool s_tree_test_drag = false;

    static void ShowWidgetsTrees()
    {
        if (!ImGui.TreeNode("Trees")) return;

        if (ImGui.TreeNode("Basic trees"))
        {
            for (int i = 0; i < 5; i++)
            {
                if (i == 0) ImGui.SetNextItemOpen(true, Cond.Once);
                if (ImGui.TreeNode($"Child {i}"))
                {
                    ImGui.Text("blah blah");
                    ImGui.SameLine();
                    if (ImGui.SmallButton("button")) { /* no-op */ }
                    ImGui.TreePop();
                }
            }
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Advanced, with Selectable nodes"))
        {
            ImGui.Checkbox("Align label with current X position", ref s_tree_align_label);
            ImGui.Checkbox("Test tree node as drag source", ref s_tree_test_drag);
            ImGui.Text("Hello!");
            if (s_tree_align_label) ImGui.Unindent(ImGui.GetTreeNodeToLabelSpacing());

            int nodeClicked = -1;
            for (int i = 0; i < 6; i++)
            {
                var nodeFlags = TreeNodeFlags.OpenOnArrow | TreeNodeFlags.OpenOnDoubleClick | TreeNodeFlags.SpanAvailWidth;
                if ((s_tree_selection_mask & (1 << i)) != 0) nodeFlags |= TreeNodeFlags.Selected;

                if (i < 3)
                {
                    bool nodeOpen = ImGui.TreeNodeEx($"Selectable Node {i}##{i}", nodeFlags);
                    if (ImGui.IsItemClicked() && !ImGui.IsItemToggledOpen()) nodeClicked = i;
                    if (s_tree_test_drag && ImGui.BeginDragDropSource())
                    {
                        ImGui.SetDragDropPayload("_TREENODE", ReadOnlySpan<byte>.Empty);
                        ImGui.Text($"This is a drag and drop source: {i}");
                        ImGui.EndDragDropSource();
                    }
                    if (nodeOpen)
                    {
                        ImGui.BulletText("Blah blah\nBlah Blah");
                        ImGui.SameLine();
                        ImGui.SmallButton("Button");
                        ImGui.TreePop();
                    }
                }
                else
                {
                    nodeFlags |= TreeNodeFlags.Leaf | TreeNodeFlags.NoTreePushOnOpen;
                    ImGui.TreeNodeEx($"Selectable Leaf {i}##{i}", nodeFlags);
                    if (ImGui.IsItemClicked() && !ImGui.IsItemToggledOpen()) nodeClicked = i;
                }
            }
            if (nodeClicked != -1)
            {
                if (ImGui.IsKeyDown(Key.LeftCtrl) || ImGui.IsKeyDown(Key.RightCtrl))
                    s_tree_selection_mask ^= (1 << nodeClicked);
                else
                    s_tree_selection_mask = (1 << nodeClicked);
            }
            if (s_tree_align_label) ImGui.Indent(ImGui.GetTreeNodeToLabelSpacing());
            ImGui.TreePop();
        }

        ImGui.TreePop();
    }

    // -- Widgets/Text --
    static float s_text_wrap_width = 200f;

    static void ShowWidgetsTextWidgets()
    {
        if (!ImGui.TreeNode("Text")) return;

        if (ImGui.TreeNode("Colorful Text"))
        {
            ImGui.TextColored(1f, 0f, 1f, 1f, "Pink");
            ImGui.TextColored(1f, 1f, 0f, 1f, "Yellow");
            ImGui.TextDisabled("Disabled");
            ImGui.SameLine(); HelpMarker("The TextDisabled color is stored in ImGuiCol_TextDisabled.");
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Word Wrapping"))
        {
            ImGui.TextWrapped("This text should automatically wrap on the edge of the window. " +
                "The current implementation for text wrapping follows simple rules suitable for English/Latin languages.");
            ImGui.Spacing();

            ImGui.SliderFloat("Wrap width", ref s_text_wrap_width, -20f, 600f, "%.0f");

            var draw = ImGui.GetWindowDrawList();
            for (int n = 0; n < 2; n++)
            {
                ImGui.Text($"Test paragraph {n}:");
                var (px, py) = ImGui.GetCursorScreenPos();
                var markerMinX = px + s_text_wrap_width;
                var markerMinY = py;
                var markerMaxX = markerMinX + 10f;
                var markerMaxY = py + ImGui.GetTextLineHeight();

                ImGui.PushTextWrapPos(ImGui.GetCursorPos().X + s_text_wrap_width);
                if (n == 0)
                    ImGui.Text($"The lazy dog jumps over the brown fox, and looks for the sleepy cat at {s_text_wrap_width:F1} pixels");
                else
                    ImGui.Text("aaaaaaaa bbbbbbbb, c cccccccc,dddddddd. d eeeeeeee   ffffffff. gggggggg!hhhhhhhh");

                var (rectMinX, rectMinY) = ImGui.GetItemRectMin();
                var (rectMaxX, rectMaxY) = ImGui.GetItemRectMax();
                draw.AddRect(new Vec2(rectMinX, rectMinY), new Vec2(rectMaxX, rectMaxY), ImGui.GetColorU32(1f, 1f, 0f, 1f));
                draw.AddRectFilled(new Vec2(markerMinX, markerMinY), new Vec2(markerMaxX, markerMaxY), ImGui.GetColorU32(1f, 0f, 1f, 1f));
                ImGui.PopTextWrapPos();
            }
            ImGui.TreePop();
        }

        ImGui.TreePop();
    }

    // -- Widgets/Tooltips --
    static int s_tooltip_curve_idx;
    static void ShowWidgetsTooltips()
    {
        if (!ImGui.TreeNode("Tooltips")) return;

        ImGui.SeparatorText("General");

        ImGui.Button("Basic");
        if (ImGui.IsItemHovered()) ImGui.SetTooltip("I am a tooltip");

        ImGui.Button("Fancy");
        if (ImGui.BeginItemTooltip())
        {
            ImGui.Text("I am a fancy tooltip");
            float[] arr = { 0.6f, 0.1f, 1f, 0.5f, 0.92f, 0.1f, 0.2f };
            ImGui.PlotLines("Curve", arr);
            ImGui.Text($"Sin(time) = {MathF.Sin((float)Environment.TickCount / 1000f):F3}");
            ImGui.EndTooltip();
        }

        ImGui.SeparatorText("Always-on");
        if (ImGui.RadioButton("Off", s_tooltip_curve_idx == 0)) s_tooltip_curve_idx = 0; ImGui.SameLine();
        if (ImGui.RadioButton("Always On (Simple)", s_tooltip_curve_idx == 1)) s_tooltip_curve_idx = 1; ImGui.SameLine();
        if (ImGui.RadioButton("Always On (Advanced)", s_tooltip_curve_idx == 2)) s_tooltip_curve_idx = 2;
        if (s_tooltip_curve_idx == 1)
            ImGui.SetTooltip("I am a simple always-on tooltip");
        else if (s_tooltip_curve_idx == 2 && ImGui.BeginTooltip())
        {
            ImGui.ProgressBar(MathF.Sin((float)Environment.TickCount / 500f) * 0.5f + 0.5f, 200f);
            ImGui.EndTooltip();
        }

        ImGui.TreePop();
    }

    // -- Widgets/Color --
    static float[] s_color_picker_col = { 0.45f, 0.55f, 0.60f, 1f };
    static float[] s_color_ref_col = { 0.45f, 0.55f, 0.60f, 1f };
    static ColorEditFlags s_color_picker_flags = ColorEditFlags.None;

    static void ShowWidgetsColor()
    {
        if (!ImGui.TreeNode("Color/Picker Widgets")) return;

        ImGui.Text("Color widget:");
        ImGui.ColorEdit3("MyColor##1", ref s_color_picker_col[0], ref s_color_picker_col[1], ref s_color_picker_col[2]);

        ImGui.Text("Color picker:");
        ImGui.ColorPicker4("MyColor##4", s_color_picker_col, s_color_picker_flags);

        ImGui.Text("Color picker with reference color:");
        ImGui.ColorPicker4("MyColor##refcol", s_color_picker_col, s_color_ref_col, s_color_picker_flags | ColorEditFlags.NoSidePreview | ColorEditFlags.NoSmallPreview);

        ImGui.Text("Color button:");
        ImGui.ColorButton("MyColor##btn", s_color_picker_col[0], s_color_picker_col[1], s_color_picker_col[2], s_color_picker_col[3]);

        ImGui.TreePop();
    }

    // -- Widgets/Combo --
    static int s_combo_current = 0;
    static readonly string[] s_combo_items = { "AAAA", "BBBB", "CCCC", "DDDD", "EEEE", "FFFF", "GGGG", "HHHH", "IIIIIII", "JJJJ", "KKKKKKK" };
    static ComboFlags s_combo_flags = ComboFlags.None;

    static void ShowWidgetsCombo()
    {
        if (!ImGui.TreeNode("Combo")) return;

        bool popup = (s_combo_flags & ComboFlags.PopupAlignLeft) != 0;
        if (ImGui.Checkbox("ComboFlags.PopupAlignLeft", ref popup)) s_combo_flags ^= ComboFlags.PopupAlignLeft;
        ImGui.SameLine(); HelpMarker("Only makes a difference if the popup is wider than the combo.");

        if (ImGui.BeginCombo("combo 1", s_combo_items[s_combo_current], s_combo_flags))
        {
            for (int n = 0; n < s_combo_items.Length; n++)
            {
                bool isSelected = s_combo_current == n;
                if (ImGui.Selectable(s_combo_items[n], isSelected))
                    s_combo_current = n;
                if (isSelected) ImGui.SetItemDefaultFocus();
            }
            ImGui.EndCombo();
        }

        ImGui.TreePop();
    }

    // -- Widgets/List Boxes --
    static int s_listbox_current = 1;
    static readonly string[] s_listbox_items = { "Apple", "Banana", "Cherry", "Kiwi", "Mango", "Orange", "Pineapple", "Strawberry", "Watermelon" };

    static void ShowWidgetsListBoxes()
    {
        if (!ImGui.TreeNode("List Boxes")) return;

        // Simulate a one-liner ListBox using BeginListBox/Selectable/EndListBox.
        if (ImGui.BeginListBox("listbox 1", -float.Epsilon, 5 * ImGui.GetTextLineHeightWithSpacing()))
        {
            for (int n = 0; n < s_listbox_items.Length; n++)
            {
                bool isSelected = s_listbox_current == n;
                if (ImGui.Selectable(s_listbox_items[n], isSelected))
                    s_listbox_current = n;
                if (isSelected) ImGui.SetItemDefaultFocus();
            }
            ImGui.EndListBox();
        }

        ImGui.TreePop();
    }

    // -- Widgets/Selectables --
    static bool[] s_sel_basic = new bool[5];
    static bool[] s_sel_single = new bool[5];

    static void ShowWidgetsSelectables()
    {
        if (!ImGui.TreeNode("Selectables")) return;

        if (ImGui.TreeNode("Basic"))
        {
            ImGui.Selectable("1. I am selectable", ref s_sel_basic[0]);
            ImGui.Selectable("2. I am selectable", ref s_sel_basic[1]);
            ImGui.Text("(I am NOT selectable)");
            ImGui.Selectable("4. I am selectable", ref s_sel_basic[3]);
            if (ImGui.Selectable("5. I am double-clickable", s_sel_basic[4], SelectableFlags.AllowDoubleClick))
                if (ImGui.IsMouseDoubleClicked(MouseButton.Left)) s_sel_basic[4] = !s_sel_basic[4];
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Selection State: Single Selection"))
        {
            for (int n = 0; n < 5; n++)
            {
                if (ImGui.Selectable($"Object {n}", s_sel_single[n]))
                {
                    Array.Clear(s_sel_single, 0, s_sel_single.Length);
                    s_sel_single[n] = true;
                }
            }
            ImGui.TreePop();
        }

        ImGui.TreePop();
    }

    // -- Widgets/Tabs --
    static bool[] s_tab_opened = { true, true, true, true };

    static void ShowWidgetsTabs()
    {
        if (!ImGui.TreeNode("Tab Bars and Tabs")) return;

        if (ImGui.TreeNode("Basic"))
        {
            var tabFlags = TabBarFlags.None;
            if (ImGui.BeginTabBar("MyTabBar", tabFlags))
            {
                if (ImGui.BeginTabItem("Avocado"))
                {
                    ImGui.Text("This is the Avocado tab!\nblah blah blah blah blah");
                    ImGui.EndTabItem();
                }
                if (ImGui.BeginTabItem("Broccoli"))
                {
                    ImGui.Text("This is the Broccoli tab!\nblah blah blah blah blah");
                    ImGui.EndTabItem();
                }
                if (ImGui.BeginTabItem("Cucumber"))
                {
                    ImGui.Text("This is the Cucumber tab!\nblah blah blah blah blah");
                    ImGui.EndTabItem();
                }
                ImGui.EndTabBar();
            }
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Tabs with close button"))
        {
            var names = new[] { "Artichoke", "Beetroot", "Celery", "Daikon" };
            for (int n = 0; n < 4; n++)
            {
                if (n > 0) ImGui.SameLine();
                ImGui.Checkbox(names[n], ref s_tab_opened[n]);
            }
            if (ImGui.BeginTabBar("MyTabBar2", TabBarFlags.Reorderable))
            {
                for (int n = 0; n < 4; n++)
                {
                    if (s_tab_opened[n] && ImGui.BeginTabItem(names[n], ref s_tab_opened[n]))
                    {
                        ImGui.Text($"This is the {names[n]} tab!");
                        ImGui.EndTabItem();
                    }
                }
                ImGui.EndTabBar();
            }
            ImGui.TreePop();
        }

        ImGui.TreePop();
    }

    // -- Widgets/Drags & Sliders --
    static bool s_ds_clamp;
    static int s_ds_drag_int = 50;
    static float s_ds_drag_float = 1.5f;
    static int s_ds_slider_int = 50;
    static float s_ds_slider_float;
    static int[] s_ds_drag2 = { 1, 5 };

    static void ShowWidgetsDragsAndSliders()
    {
        if (!ImGui.TreeNode("Drag and Slider Flags")) return;

        ImGui.Checkbox("SliderFlags.AlwaysClamp", ref s_ds_clamp);
        ImGui.SameLine(); HelpMarker("Always clamp value to min/max bounds (if any) when typing it manually.");

        var flags = s_ds_clamp ? SliderFlags.AlwaysClamp : SliderFlags.None;

        ImGui.SeparatorText("Drags");
        ImGui.DragInt("DragInt (0 -> 100)", ref s_ds_drag_int, 0.5f, 0, 100, "%d", flags);
        ImGui.DragFloat("DragFloat (0 -> 1)", ref s_ds_drag_float, 0.005f, 0f, 1f, "%.3f", flags);
        ImGui.DragInt2("DragInt2 (0 -> 10)", s_ds_drag2, 0.5f, 0, 10, "%d", flags);

        ImGui.SeparatorText("Sliders");
        ImGui.SliderInt("SliderInt (0 -> 100)", ref s_ds_slider_int, 0, 100, "%d", flags);
        ImGui.SliderFloat("SliderFloat (0 -> 1)", ref s_ds_slider_float, 0f, 1f, "%.3f", flags);

        ImGui.TreePop();
    }

    // -- Widgets/Plotting --
    static readonly float[] s_plot_arr = { 0.6f, 0.1f, 1f, 0.5f, 0.92f, 0.1f, 0.2f };
    static readonly float[] s_plot_values = new float[90];
    static int s_plot_values_offset;
    static double s_plot_refresh_time;
    static float s_plot_phase;

    static void ShowWidgetsPlotting()
    {
        if (!ImGui.TreeNode("Plots Widgets")) return;

        ImGui.PlotLines("Frame Times", s_plot_arr);
        ImGui.PlotHistogram("Histogram", s_plot_arr, 0, "Avg 0.487", 0f, 1f, 0, 80f);

        // Animated sin curve. Mirrors the upstream demo.
        if (s_plot_refresh_time == 0) s_plot_refresh_time = Environment.TickCount / 1000.0;
        while (s_plot_refresh_time < Environment.TickCount / 1000.0)
        {
            s_plot_values[s_plot_values_offset] = MathF.Cos(s_plot_phase);
            s_plot_values_offset = (s_plot_values_offset + 1) % s_plot_values.Length;
            s_plot_phase += 0.10f * s_plot_values_offset;
            s_plot_refresh_time += 1.0 / 60.0;
        }
        var avg = 0f;
        for (int i = 0; i < s_plot_values.Length; i++) avg += s_plot_values[i];
        avg /= s_plot_values.Length;
        ImGui.PlotLines("Lines", s_plot_values, s_plot_values_offset, $"avg {avg:F6}", -1f, 1f, 0, 80f);

        ImGui.TreePop();
    }

    // -- Widgets/Progress Bars --
    static float s_progress;
    static float s_progress_dir = 1f;

    static void ShowWidgetsProgressBars()
    {
        if (!ImGui.TreeNode("Progress Bars")) return;

        s_progress += s_progress_dir * 0.4f * DeltaTimeSafe();
        if (s_progress >= 1.1f) { s_progress = 1.1f; s_progress_dir *= -1f; }
        if (s_progress <= -0.1f) { s_progress = -0.1f; s_progress_dir *= -1f; }

        ImGui.ProgressBar(s_progress, -float.Epsilon);
        ImGui.SameLine(); ImGui.Text("Progress Bar");

        var progressSat = MathF.Max(0f, MathF.Min(s_progress, 1f));
        ImGui.ProgressBar(progressSat, -float.Epsilon, 0f, $"{(int)(progressSat * 1753)}/{1753}");

        ImGui.ProgressBar(-1f * (float)Environment.TickCount * 0.001f, -float.Epsilon, 0f, "Searching..");
        ImGui.SameLine(); ImGui.Text("Indeterminate");

        ImGui.TreePop();
    }

    // -- Widgets/Images --
    static void ShowWidgetsImages()
    {
        if (!ImGui.TreeNode("Images")) return;

        ImGui.TextWrapped("Images require an actual texture handle from the active backend " +
            "(SDL_GPU: SDL_GPUTexture*). The C# port doesn't ship a sample texture, so this " +
            "section is a placeholder. The wrapper exposes ImGui.Image(GpuTexture, ...) and " +
            "ImGui.ImageButton(GpuTexture, ...) overloads.");

        ImGui.TreePop();
    }

    // -- Widgets/Disabled --
    static bool s_disabled_check = true;

    static void ShowWidgetsDisabled()
    {
        if (!ImGui.TreeNode("Disabled Blocks")) return;

        ImGui.Checkbox("Disable", ref s_disabled_check);
        ImGui.BeginDisabled(s_disabled_check);
        if (ImGui.Button("Click me")) { /* no-op */ }
        ImGui.SliderFloat("float", ref s_basic_drag_float1, 0f, 1f);
        ImGui.EndDisabled();

        ImGui.TreePop();
    }

    // -------------------------------------------------------------------------
    // SECTION: Layout
    // -------------------------------------------------------------------------
    static int s_layout_widget_width_mode;
    static float s_layout_h_scroll = 4f;
    static int s_layout_h_lines = 7;

    static void ShowLayoutSection()
    {
        if (!ImGui.CollapsingHeader("Layout & Scrolling")) return;

        if (ImGui.TreeNode("Child windows"))
        {
            ShowLayoutChildWindows();
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Widgets Width"))
        {
            ImGui.RadioButton("PushItemWidth(GetWindowWidth() * 0.5f)", ref s_layout_widget_width_mode, 0);
            ImGui.RadioButton("PushItemWidth(GetContentRegionAvail().x * 0.5f)", ref s_layout_widget_width_mode, 1);
            ImGui.RadioButton("PushItemWidth(-100)", ref s_layout_widget_width_mode, 2);
            ImGui.RadioButton("PushItemWidth(-1) (align to right edge)", ref s_layout_widget_width_mode, 3);
            switch (s_layout_widget_width_mode)
            {
                case 0: ImGui.PushItemWidth(ImGui.GetWindowWidth() * 0.5f); break;
                case 1: ImGui.PushItemWidth(ImGui.GetContentRegionAvail().Width * 0.5f); break;
                case 2: ImGui.PushItemWidth(-100f); break;
                case 3: ImGui.PushItemWidth(-1f); break;
            }
            ImGui.DragFloat("float", ref s_basic_drag_float1, 1f);
            ImGui.PopItemWidth();
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Basic Horizontal Layout"))
        {
            ImGui.TextWrapped("Use SameLine() to keep adding items to the right of the preceding item.");
            ImGui.Text("Two items: Hello"); ImGui.SameLine();
            ImGui.TextColored(1f, 1f, 0f, 1f, "World");
            ImGui.Text("More spacing: Hello"); ImGui.SameLine(0, 20f);
            ImGui.TextColored(1f, 1f, 0f, 1f, "World");
            ImGui.AlignTextToFramePadding();
            ImGui.Text("Aligned: "); ImGui.SameLine();
            if (ImGui.Button("Press me!")) { /* no-op */ }

            ImGui.SeparatorText("Horizontal scrolling");
            ImGui.SliderFloat("Scroll x", ref s_layout_h_scroll, 1f, 100f);
            ImGui.SliderInt("Lines", ref s_layout_h_lines, 1, 15);
            ImGui.PushStyleVar(StyleVar.FrameRounding, 3.0f);
            ImGui.PushStyleVar(StyleVar.FramePadding, 2f, 1f);
            var scrollingChildSize = new Vec2(0f, ImGui.GetFrameHeightWithSpacing() * s_layout_h_lines + 30f);
            if (ImGui.BeginChild("scrolling", scrollingChildSize.X, scrollingChildSize.Y, ChildFlags.Borders, WindowFlags.HorizontalScrollbar))
            {
                for (int line = 0; line < s_layout_h_lines; line++)
                {
                    var numButtons = 10 + ((line & 1) != 0 ? line * 9 : line * 3);
                    for (int n = 0; n < numButtons; n++)
                    {
                        if (n > 0) ImGui.SameLine();
                        ImGui.PushID(n + line * 1000);
                        var label = (n % 15 == 0) ? "FizzBuzz" : (n % 3 == 0) ? "Fizz" : (n % 5 == 0) ? "Buzz" : n.ToString();
                        var hue = (float)n * 0.05f;
                        var (r, g, b) = ImGui.ColorConvertHSVtoRGB(hue, 0.6f, 0.6f);
                        var (rh, gh, bh) = ImGui.ColorConvertHSVtoRGB(hue, 0.7f, 0.7f);
                        var (ra, ga, ba) = ImGui.ColorConvertHSVtoRGB(hue, 0.8f, 0.8f);
                        ImGui.PushStyleColor(Col.Button, r, g, b, 1f);
                        ImGui.PushStyleColor(Col.ButtonHovered, rh, gh, bh, 1f);
                        ImGui.PushStyleColor(Col.ButtonActive, ra, ga, ba, 1f);
                        ImGui.Button(label, 40f + MathF.Sin((float)(line + n)) * 20f);
                        ImGui.PopStyleColor(3);
                        ImGui.PopID();
                    }
                }
                ImGui.EndChild();
            }
            ImGui.PopStyleVar(2);

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Groups"))
        {
            ImGui.TextWrapped("BeginGroup/EndGroup lets a series of items be treated as one (for IsItemHovered, etc.).");
            ImGui.BeginGroup();
            {
                ImGui.BeginGroup();
                ImGui.Button("AAA");
                ImGui.SameLine();
                ImGui.Button("BBB");
                ImGui.SameLine();
                ImGui.BeginGroup();
                ImGui.Button("CCC");
                ImGui.Button("DDD");
                ImGui.EndGroup();
                ImGui.SameLine();
                ImGui.Button("EEE");
                ImGui.EndGroup();
                if (ImGui.IsItemHovered()) ImGui.SetTooltip("First group hovered");
            }
            var sz = ImGui.GetItemRectSize();
            ImGui.Button("Group bound", sz.Width, sz.Height);
            ImGui.EndGroup();
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Text Baseline Alignment"))
        {
            ImGui.SeparatorText("Text baseline:");
            ImGui.AlignTextToFramePadding();
            ImGui.Text("Frame-padded text:"); ImGui.SameLine();
            if (ImGui.Button("Button A")) { }
            ImGui.Text("Plain text:"); ImGui.SameLine();
            if (ImGui.Button("Button B")) { }
            ImGui.TreePop();
        }
    }

    static bool s_child_borders = true;
    static bool s_child_resizable = true;
    static int s_child_lines = 7;
    static void ShowLayoutChildWindows()
    {
        HelpMarker(
            "Use child windows to begin into a self-contained independent scrolling/clipping region. " +
            "Child windows can have their own drawing list, scrollbar, etc.");

        ImGui.Checkbox("Borders", ref s_child_borders);
        ImGui.SliderInt("Lines", ref s_child_lines, 1, 100);
        var childFlags = (s_child_borders ? ChildFlags.Borders : ChildFlags.None);
        if (s_child_resizable) childFlags |= ChildFlags.ResizeY;

        if (ImGui.BeginChild("ChildL", ImGui.GetContentRegionAvail().Width * 0.5f, 260f, childFlags))
        {
            for (int i = 0; i < s_child_lines; i++)
                ImGui.Text($"{i}: scrollable region");
            ImGui.EndChild();
        }
        ImGui.SameLine();

        if (ImGui.BeginChild("ChildR", 0f, 260f, childFlags | ChildFlags.Borders, WindowFlags.MenuBar))
        {
            if (ImGui.BeginMenuBar())
            {
                if (ImGui.BeginMenu("Menu"))
                {
                    ImGui.MenuItem("(item)", null, false, false);
                    ImGui.EndMenu();
                }
                ImGui.EndMenuBar();
            }
            if (ImGui.BeginTable("split", 2, TableFlags.Resizable | TableFlags.NoSavedSettings))
            {
                for (int i = 0; i < 100; i++)
                {
                    var buf = $"{i:D3}";
                    ImGui.TableNextColumn();
                    ImGui.Button(buf, -float.Epsilon, 0f);
                }
                ImGui.EndTable();
            }
            ImGui.EndChild();
        }
    }

    // -------------------------------------------------------------------------
    // SECTION: Popups
    // -------------------------------------------------------------------------
    static int s_popup_selected = -1;
    static int s_popup_modal_chosen = -1;

    static void ShowPopupsSection()
    {
        if (!ImGui.CollapsingHeader("Popups & Modal windows")) return;

        if (ImGui.TreeNode("Popups"))
        {
            ImGui.TextWrapped("Click a button to open a popup; click outside or press Escape to dismiss.");
            var names = new[] { "Bream", "Haddock", "Mackerel", "Pollock", "Tilefish" };
            for (int n = 0; n < names.Length; n++)
            {
                if (n > 0) ImGui.SameLine();
                if (ImGui.Button(names[n])) ImGui.OpenPopup("popup_select");
            }
            if (ImGui.BeginPopup("popup_select"))
            {
                ImGui.SeparatorText("Aquarium");
                for (int i = 0; i < names.Length; i++)
                    if (ImGui.Selectable(names[i])) s_popup_selected = i;
                ImGui.EndPopup();
            }
            ImGui.SameLine();
            ImGui.Text("Selected: " + (s_popup_selected >= 0 ? names[s_popup_selected] : "<None>"));
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Context menus"))
        {
            ImGui.Text("Right-click me");
            if (ImGui.BeginPopupContextItem())
            {
                if (ImGui.Selectable("Edit name")) { /* no-op */ }
                if (ImGui.Selectable("Reset")) s_popup_modal_chosen = -1;
                ImGui.EndPopup();
            }

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Modals"))
        {
            if (ImGui.Button("Delete..")) ImGui.OpenPopup("Delete?");
            if (ImGui.BeginPopupModal("Delete?", WindowFlags.AlwaysAutoResize))
            {
                ImGui.Text("All items will be deleted.\nThis cannot be undone!");
                ImGui.Separator();
                if (ImGui.Button("OK", 120f, 0)) { s_popup_modal_chosen = 1; ImGui.CloseCurrentPopup(); }
                ImGui.SameLine();
                if (ImGui.Button("Cancel", 120f, 0)) { s_popup_modal_chosen = 0; ImGui.CloseCurrentPopup(); }
                ImGui.EndPopup();
            }
            ImGui.SameLine();
            ImGui.Text("Last choice: " + (s_popup_modal_chosen == 1 ? "OK" : s_popup_modal_chosen == 0 ? "Cancel" : "<None>"));
            ImGui.TreePop();
        }
    }

    // -------------------------------------------------------------------------
    // SECTION: Tables
    // -------------------------------------------------------------------------
    static TableFlags s_table_flags = TableFlags.Borders | TableFlags.RowBg;
    static readonly string[] s_table_items = { "Apple", "Banana", "Cherry", "Kiwi", "Mango", "Orange", "Pineapple", "Strawberry", "Watermelon" };

    static void ShowTablesSection()
    {
        if (!ImGui.CollapsingHeader("Tables")) return;

        if (ImGui.TreeNode("Basic"))
        {
            if (ImGui.BeginTable("table1", 3))
            {
                for (int row = 0; row < 4; row++)
                {
                    ImGui.TableNextRow();
                    for (int col = 0; col < 3; col++)
                    {
                        ImGui.TableNextColumn();
                        ImGui.Text($"Row {row} Column {col}");
                    }
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Borders, background"))
        {
            // Flag toggles
            bool b;
            b = (s_table_flags & TableFlags.RowBg) != 0;       if (ImGui.Checkbox("RowBg", ref b)) s_table_flags ^= TableFlags.RowBg;
            b = (s_table_flags & TableFlags.Borders) != 0;     if (ImGui.Checkbox("Borders (all)", ref b)) s_table_flags ^= TableFlags.Borders;
            b = (s_table_flags & TableFlags.Resizable) != 0;   if (ImGui.Checkbox("Resizable", ref b)) s_table_flags ^= TableFlags.Resizable;

            if (ImGui.BeginTable("table_borders", 4, s_table_flags))
            {
                ImGui.TableSetupColumn("One");
                ImGui.TableSetupColumn("Two");
                ImGui.TableSetupColumn("Three");
                ImGui.TableSetupColumn("Four");
                ImGui.TableHeadersRow();
                for (int row = 0; row < 5; row++)
                {
                    ImGui.TableNextRow();
                    for (int col = 0; col < 4; col++)
                    {
                        ImGui.TableNextColumn();
                        ImGui.Text($"R{row}C{col}");
                    }
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Sorting"))
        {
            // Demonstrate TableSortSpecs roundtripping. We sort the items array
            // alphabetically based on the user's choice.
            var sortFlags = TableFlags.Reorderable | TableFlags.Sortable | TableFlags.Borders | TableFlags.RowBg | TableFlags.SizingStretchProp;
            if (ImGui.BeginTable("table_sort", 2, sortFlags))
            {
                ImGui.TableSetupColumn("Index", TableColumnFlags.WidthFixed | TableColumnFlags.DefaultSort);
                ImGui.TableSetupColumn("Name", TableColumnFlags.WidthStretch);
                ImGui.TableHeadersRow();

                var specs = ImGui.TableGetSortSpecs();
                if (specs.IsValid && specs.SpecsDirty)
                {
                    // We're not going to actually sort; just acknowledge the dirty bit.
                    specs.SpecsDirty = false;
                }

                for (int i = 0; i < s_table_items.Length; i++)
                {
                    ImGui.TableNextRow();
                    ImGui.TableNextColumn(); ImGui.Text(i.ToString());
                    ImGui.TableNextColumn(); ImGui.Text(s_table_items[i]);
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }
    }

    // -------------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------------
    static byte[] StringBuffer(string initial, int capacity)
    {
        var buf = new byte[capacity];
        var n = System.Text.Encoding.UTF8.GetBytes(initial, buf);
        if (n < capacity) buf[n] = 0;
        return buf;
    }

    // The wrapper exposes ImGui.Framerate but not Io.DeltaTime; this is a safe
    // approximation good enough for the demo's animation timing.
    static float DeltaTimeSafe() => 1f / MathF.Max(1f, ImGui.Framerate);
}

// =============================================================================
// TODO — sections of imgui_demo.cpp NOT YET PORTED, and the wrapper additions
// they would require. Listed in upstream order for easy cross-reference.
// (Line numbers refer to ../imgui/imgui_demo.cpp.)
// =============================================================================
//
// SHOWDEMOWINDOW SCAFFOLDING (lines 350–650):
//   - "Configuration" CollapsingHeader (line 464) — toggles ConfigFlags &
//     BackendFlags via CheckboxFlags. Wrapper needs:
//       * ImGui.GetIO() exposure (most fields exist as IGSharp_IO_* getters/setters
//         in Native.cs but no public Io accessor type).
//       * ImGui.CheckboxFlags<TEnum>(string, ref TEnum, TEnum) helper.
//       * ImGui.GetTime() — currently no IGSharp_GetTime binding; would need one
//         (or expose IO.DeltaTime for accumulation).
//   - "Style, Fonts" sub-tree (line 594) — could call ImGui.ShowStyleSelector
//     and ShowFontSelector which are already wrapped.
//   - LogButtons / LogText / LogToClipboard / LogFinish (line 611–619) — no
//     bindings exist (IGSharp_LogButtons etc. are not in Native.cs). Would need
//     adding to imgui-sharp-native and rebinding.
//
// MENU BAR (line 660):
//   - The full ShowExampleMenuFile pattern with Disabled, Checked items, and
//     extensive nested "Colors" submenu (looping 0..ImGuiCol_COUNT) is omitted.
//     Mostly cosmetic; works with current wrapper.
//
// WIDGETS:
//   - Widgets/DataTypes (line 1398) — exercises Drag<T>/Slider<T>/Input<T> for
//     all 10 scalar types plus min/max formatting. Already supported by the
//     generic wrappers; just hadn't time to write the demo code.
//   - Widgets/DragAndDrop (line 1548) — color-square drag/drop and reorder list.
//     Fully supported by ImGui.BeginDragDropSource/SetDragDropPayload<T>.
//     Skipped to keep scope small.
//   - Widgets/Fonts (line 1763) — needs Font/FontAtlas usage; wrappers exist
//     (ImGui.GetFontAtlas, PushFont/PopFont) but the demo loads Roboto/etc.
//     from disk which would require shipping fonts in the sample.
//   - Widgets/Images (line 1779) — placeholder included but shows no actual
//     texture; would need a sample SDL_GPU texture loaded via stb_image or
//     similar in the sample.
//   - Widgets/MultiComponents (line 1937) — uses InputFloat2..4, InputInt2..4
//     and DragInt4/SliderFloat4 with custom formats; partially covered above.
//   - Widgets/QueryingStatuses (line 2097) — exhaustive demo of IsItemHovered,
//     IsItemActive, IsItemFocused, etc. Wrappers already exist for all of these.
//   - Widgets/SelectionAndMultiSelect (line 2696) — exercises BeginMultiSelect,
//     ImGuiSelectionBasicStorage, range-based selection. Wrappers exist
//     (BeginMultiSelect/EndMultiSelect/MultiSelectIO/SelectionRequest) but the
//     demo does ~1k lines of state management with multiple storage shapes.
//   - Widgets/TextFilter (line 3707) — needs ImGuiTextFilter type. NO wrapper.
//   - Widgets/TextInput (line 3734) — multi-line edit, completion callback,
//     resize callback (auto-grow string is supported in our InputText overload).
//     The custom completion callback demo would exercise InputTextCallback /
//     InputTextCallbackData wrappers (already present).
//   - Widgets/Vertical Sliders (line 4233) — uses VSliderFloat/Int; wrapper
//     exposes generic ImGui.VSlider<T>; not yet ported into demo.
//
// LAYOUT:
//   - Layout/Clipping (line 4500-ish) — uses ImGui::PushClipRect on the
//     window draw list; DrawList wrapper has PushClipRect/PopClipRect but the
//     demo's clipping showcase wasn't ported.
//   - Layout/Tabs (already covered partially via Widgets/Tabs).
//   - Layout/Drag/Drop (covered by separate Widgets/DragAndDrop above).
//   - Layout/Tabs and Tab Bar Reorderable (TabBarFlags.Reorderable is wired,
//     but the dynamic tabs/active-tab-button demo not done).
//
// POPUPS (line 5241):
//   - Sub-menus inside popups (line 5430-ish) — supported but not ported.
//   - "Modals stacked" example with multiple modal levels — supported but not
//     ported.
//
// COLUMNS (line 7609):
//   - Old-style ImGui::Columns / NextColumn API — has NO wrapper; superseded by
//     Tables. Probably not worth porting.
//
// INPUTS (line 7818):
//   - Keyboard, Navigation, Mouse demos exercising IsKeyDown/IsKeyPressed,
//     GetMouseDragDelta, IsMouseHoveringRect, etc. All wrappers exist; just not
//     ported.
//
// EXAMPLE APPS (each is its own full window):
//   - ShowExampleAppMainMenuBar (line 235)
//   - ShowExampleAppDocuments — multi-document with tab bars
//   - ShowExampleAppConsole — text history + filter + completion
//   - ShowExampleAppLog — auto-scrolling log viewer
//   - ShowExampleAppLayout — vertical splitter
//   - ShowExampleAppPropertyEditor — recursive tree node table
//   - ShowExampleAppCustomRendering — heavy DrawList usage. Wrapper exposes
//     DrawList but the upstream demo uses every primitive (lines, paths, polys,
//     gradient fills, anti-aliased curves, triangles, ImageQuad).
//   - ShowExampleAppSimpleOverlay — CornerOverlay window with ConfigFlags.
//   - ShowExampleAppAutoResize / ConstrainedResize / Fullscreen / LongText /
//     WindowTitles — all minor.
//   - ShowExampleAppAssetsBrowser — virtualized icon grid w/ ListClipper
//     (wrapper has ListClipper).
//
// WRAPPER ADDITIONS that would unlock multiple sections:
//   1. ImGui.GetIO() returning an Io accessor exposing ConfigFlags/BackendFlags
//      and DeltaTime. Native bindings exist; only the public type is missing.
//   2. ImGui.CheckboxFlags<TEnum>(label, ref TEnum, TEnum mask) helper.
//   3. ImGui.GetTime() / IGSharp_GetTime binding (currently absent).
//   4. ImGui.SetItemTooltip(string) (currently faked via BeginItemTooltip).
//   5. ImGui.LogButtons / LogText / LogToClipboard / LogFinish bindings.
//   6. ImGuiTextFilter wrapper.
//   7. One-liner ImGui.Combo(string, ref int, string[]) and ImGui.ListBox helpers.
//   8. ImColor::HSV / packed color helper (we manually build via ColorConvertHSVtoRGB
//      + GetColorU32(r,g,b,a)).
