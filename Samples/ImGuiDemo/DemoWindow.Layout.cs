// C# port of imgui_demo.cpp DemoWindowLayout() and DemoWindowPopups().
// Upstream reference: imgui_demo.cpp (lines ~4362-5541).
// C++ function-static locals become private static fields, prefixed per section.

using SdlSharp.ImGui;

namespace ImGuiDemo;

internal static unsafe partial class DemoWindow
{
    // -------------------------------------------------------------------------
    // SECTION: DemoWindowLayout() — static state
    // -------------------------------------------------------------------------

    // Layout/Child windows
    static bool s_cw_disable_mouse_wheel;
    static bool s_cw_disable_menu;
    static int s_cw_draw_lines = 3;
    static int s_cw_max_height_in_lines = 10;
    static int s_cw_offset_x = 0;
    static bool s_cw_override_bg_color = true;
    static int s_cw_child_flags = (int)(ChildFlags.Borders | ChildFlags.ResizeX | ChildFlags.ResizeY);

    // Layout/Widgets Width
    static float s_ww_f = 0.0f;
    static bool s_ww_show_indented_items = true;

    // Layout/Basic Horizontal Layout
    static bool s_hl_c1, s_hl_c2, s_hl_c3, s_hl_c4;
    static float s_hl_f0 = 1.0f, s_hl_f1 = 2.0f, s_hl_f2 = 3.0f;
    static int s_hl_item = -1;
    static readonly int[] s_hl_selection = { 0, 1, 2, 3 };

    // Layout/Scrolling
    static int s_scroll_track_item = 50;
    static bool s_scroll_enable_track = true;
    static bool s_scroll_enable_extra_decorations = false;
    static float s_scroll_to_off_px = 0.0f;
    static float s_scroll_to_pos_px = 200.0f;
    static int s_scroll_lines = 7;
    static bool s_scroll_show_horizontal_contents_size_demo_window = false;

    // Layout/Scrolling/Horizontal contents size demo window
    static bool s_hcs_show_h_scrollbar = true;
    static bool s_hcs_show_button = true;
    static bool s_hcs_show_tree_nodes = true;
    static bool s_hcs_show_text_wrapped = false;
    static bool s_hcs_show_columns = true;
    static bool s_hcs_show_tab_bar = true;
    static bool s_hcs_show_child = false;
    static bool s_hcs_explicit_content_size = false;
    static float s_hcs_contents_size_x = 300.0f;

    // Layout/Text Clipping
    static readonly float[] s_clip_size = { 100.0f, 100.0f };
    static float s_clip_offset_x = 30.0f;
    static float s_clip_offset_y = 30.0f;

    // Layout/Overlap Mode
    static bool s_overlap_enable_allow_overlap = true;

    // -------------------------------------------------------------------------
    // SECTION: DemoWindowPopups() — static state
    // -------------------------------------------------------------------------

    // Popups/Popups
    static int s_popups_selected_fish = -1;
    static readonly bool[] s_popups_toggles = { true, false, false, false, false };

    // Popups/Context menus
    static int s_ctx_selected = -1;
    static float s_ctx_value = 0.5f;
    static string s_ctx_name = "Label1";

    // Popups/Modals
    static bool s_modal_dont_ask_me_next_time = false;
    static int s_modal_item = 1;
    static readonly float[] s_modal_color = { 0.4f, 0.7f, 0.0f, 0.5f };

    // -------------------------------------------------------------------------
    // SECTION: DemoWindowLayout()
    // -------------------------------------------------------------------------
    private static void DemoWindowLayout()
    {
        if (!ImGui.CollapsingHeader("Layout & Scrolling"))
            return;

        if (ImGui.TreeNode("Child windows"))
        {
            // DEMO MARKER: Layout/Child windows
            ImGui.SeparatorText("Child windows");

            HelpMarker("Use child windows to begin into a self-contained independent scrolling/clipping regions within a host window.");
            ImGui.Checkbox("Disable Mouse Wheel", ref s_cw_disable_mouse_wheel);
            ImGui.Checkbox("Disable Menu", ref s_cw_disable_menu);

            // Child 1: no border, enable horizontal scrollbar
            {
                var windowFlags = WindowFlags.HorizontalScrollbar;
                if (s_cw_disable_mouse_wheel)
                    windowFlags |= WindowFlags.NoScrollWithMouse;
                ImGui.BeginChild("ChildL", ImGui.GetContentRegionAvail().Width * 0.5f, 260, ChildFlags.None, windowFlags);
                for (int i = 0; i < 100; i++)
                    ImGui.Text($"{i:0000}: scrollable region");
                ImGui.EndChild();
            }

            ImGui.SameLine();

            // Child 2: rounded border
            {
                var windowFlags = WindowFlags.None;
                if (s_cw_disable_mouse_wheel)
                    windowFlags |= WindowFlags.NoScrollWithMouse;
                if (!s_cw_disable_menu)
                    windowFlags |= WindowFlags.MenuBar;
                ImGui.PushStyleVar(StyleVar.ChildRounding, 5.0f);
                ImGui.BeginChild("ChildR", 0, 260, ChildFlags.Borders, windowFlags);
                if (!s_cw_disable_menu && ImGui.BeginMenuBar())
                {
                    if (ImGui.BeginMenu("Menu"))
                    {
                        ShowExampleMenuFile();
                        ImGui.EndMenu();
                    }
                    ImGui.EndMenuBar();
                }
                if (ImGui.BeginTable("split", 2, TableFlags.Resizable | TableFlags.NoSavedSettings))
                {
                    for (int i = 0; i < 100; i++)
                    {
                        var buf = $"{i:000}";
                        ImGui.TableNextColumn();
                        ImGui.Button(buf, -float.Epsilon, 0.0f);
                    }
                    ImGui.EndTable();
                }
                ImGui.EndChild();
                ImGui.PopStyleVar();
            }

            // Child 3: manual-resize
            ImGui.SeparatorText("Manual-resize");
            {
                HelpMarker("Drag bottom border to resize. Double-click bottom border to auto-fit to vertical contents.");
                //if (ImGui.Button("Set Height to 200"))
                //    ImGui.SetNextWindowSize(-float.Epsilon, 200.0f);

                var frameBg = ImGui.GetStyleColorVec4(Col.FrameBg);
                ImGui.PushStyleColor(Col.ChildBg, frameBg.X, frameBg.Y, frameBg.Z, frameBg.W);
                if (ImGui.BeginChild("ResizableChild", -float.Epsilon, ImGui.GetTextLineHeightWithSpacing() * 8, ChildFlags.Borders | ChildFlags.ResizeY))
                    for (int n = 0; n < 10; n++)
                        ImGui.Text($"Line {n:0000}");
                ImGui.PopStyleColor();
                ImGui.EndChild();
            }

            // Child 4: auto-resizing height with a limit
            ImGui.SeparatorText("Auto-resize with constraints");
            {
                ImGui.SetNextItemWidth(ImGui.GetFontSize() * 8);
                ImGui.DragInt("Lines Count", ref s_cw_draw_lines, 0.2f);
                ImGui.SetNextItemWidth(ImGui.GetFontSize() * 8);
                ImGui.DragInt("Max Height (in Lines)", ref s_cw_max_height_in_lines, 0.2f);

                ImGui.SetNextWindowSizeConstraints(0.0f, ImGui.GetTextLineHeightWithSpacing() * 1, float.MaxValue, ImGui.GetTextLineHeightWithSpacing() * s_cw_max_height_in_lines);
                if (ImGui.BeginChild("ConstrainedChild", -float.Epsilon, 0.0f, ChildFlags.Borders | ChildFlags.AutoResizeY))
                    for (int n = 0; n < s_cw_draw_lines; n++)
                        ImGui.Text($"Line {n:0000}");
                ImGui.EndChild();
            }

            ImGui.SeparatorText("Misc/Advanced");

            // Demonstrate a few extra things
            // - Changing ImGuiCol_ChildBg (which is transparent black in default styles)
            // - Using SetCursorPos() to position child window (the child window is an item from the POV of parent window)
            //   You can also call SetNextWindowPos() to position the child window. The parent window will effectively
            //   layout from this position.
            // - Using ImGui::GetItemRectMin/Max() to query the "item" state (because the child window is an item from
            //   the POV of the parent window). See 'Demo->Querying Status (Edited/Active/Hovered etc.)' for details.
            {
                ImGui.SetNextItemWidth(ImGui.GetFontSize() * 8);
                ImGui.DragInt("Offset X", ref s_cw_offset_x, 1.0f, -1000, 1000);
                ImGui.Checkbox("Override ChildBg color", ref s_cw_override_bg_color);
                ImGui.CheckboxFlags("ImGuiChildFlags_Borders", ref s_cw_child_flags, (int)ChildFlags.Borders);
                ImGui.CheckboxFlags("ImGuiChildFlags_AlwaysUseWindowPadding", ref s_cw_child_flags, (int)ChildFlags.AlwaysUseWindowPadding);
                ImGui.CheckboxFlags("ImGuiChildFlags_ResizeX", ref s_cw_child_flags, (int)ChildFlags.ResizeX);
                ImGui.CheckboxFlags("ImGuiChildFlags_ResizeY", ref s_cw_child_flags, (int)ChildFlags.ResizeY);
                ImGui.CheckboxFlags("ImGuiChildFlags_FrameStyle", ref s_cw_child_flags, (int)ChildFlags.FrameStyle);
                ImGui.SameLine(); HelpMarker("Style the child window like a framed item: use FrameBg, FrameRounding, FrameBorderSize, FramePadding instead of ChildBg, ChildRounding, ChildBorderSize, WindowPadding.");
                if ((s_cw_child_flags & (int)ChildFlags.FrameStyle) != 0)
                    s_cw_override_bg_color = false;

                ImGui.SetCursorPosX(ImGui.GetCursorPosX() + s_cw_offset_x);
                if (s_cw_override_bg_color)
                    ImGui.PushStyleColor(Col.ChildBg, 0x640000FFu); // IM_COL32(255, 0, 0, 100)
                ImGui.BeginChild("Red", 200, 100, (ChildFlags)s_cw_child_flags, WindowFlags.None);
                if (s_cw_override_bg_color)
                    ImGui.PopStyleColor();

                for (int n = 0; n < 50; n++)
                    ImGui.Text($"Some test {n}");
                ImGui.EndChild();
                bool childIsHovered = ImGui.IsItemHovered();
                var childRectMin = ImGui.GetItemRectMin();
                var childRectMax = ImGui.GetItemRectMax();
                ImGui.Text($"Hovered: {(childIsHovered ? 1 : 0)}");
                ImGui.Text($"Rect of child window is: ({childRectMin.X:F0},{childRectMin.Y:F0}) ({childRectMax.X:F0},{childRectMax.Y:F0})");
            }

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Widgets Width"))
        {
            // DEMO MARKER: Layout/Widgets Width
            ImGui.Checkbox("Show indented items", ref s_ww_show_indented_items);

            // Use SetNextItemWidth() to set the width of a single upcoming item.
            // Use PushItemWidth()/PopItemWidth() to set the width of a group of items.
            // In real code use you'll probably want to choose width values that are proportional to your font size
            // e.g. Using '20.0f * GetFontSize()' as width instead of '200.0f', etc.

            ImGui.Text("SetNextItemWidth/PushItemWidth(100)");
            ImGui.SameLine(); HelpMarker("Fixed width.");
            ImGui.PushItemWidth(100);
            ImGui.DragFloat("float##1b", ref s_ww_f);
            if (s_ww_show_indented_items)
            {
                ImGui.Indent();
                ImGui.DragFloat("float (indented)##1b", ref s_ww_f);
                ImGui.Unindent();
            }
            ImGui.PopItemWidth();

            ImGui.Text("SetNextItemWidth/PushItemWidth(-100)");
            ImGui.SameLine(); HelpMarker("Align to right edge minus 100");
            ImGui.PushItemWidth(-100);
            ImGui.DragFloat("float##2a", ref s_ww_f);
            if (s_ww_show_indented_items)
            {
                ImGui.Indent();
                ImGui.DragFloat("float (indented)##2b", ref s_ww_f);
                ImGui.Unindent();
            }
            ImGui.PopItemWidth();

            ImGui.Text("SetNextItemWidth/PushItemWidth(GetContentRegionAvail().x * 0.5f)");
            ImGui.SameLine(); HelpMarker("Half of available width.\n(~ right-cursor_pos)\n(works within a column set)");
            ImGui.PushItemWidth(ImGui.GetContentRegionAvail().Width * 0.5f);
            ImGui.DragFloat("float##3a", ref s_ww_f);
            if (s_ww_show_indented_items)
            {
                ImGui.Indent();
                ImGui.DragFloat("float (indented)##3b", ref s_ww_f);
                ImGui.Unindent();
            }
            ImGui.PopItemWidth();

            ImGui.Text("SetNextItemWidth/PushItemWidth(-GetContentRegionAvail().x * 0.5f)");
            ImGui.SameLine(); HelpMarker("Align to right edge minus half");
            ImGui.PushItemWidth(-ImGui.GetContentRegionAvail().Width * 0.5f);
            ImGui.DragFloat("float##4a", ref s_ww_f);
            if (s_ww_show_indented_items)
            {
                ImGui.Indent();
                ImGui.DragFloat("float (indented)##4b", ref s_ww_f);
                ImGui.Unindent();
            }
            ImGui.PopItemWidth();

            ImGui.Text("SetNextItemWidth/PushItemWidth(-Min(GetContentRegionAvail().x * 0.40f, GetFontSize() * 12))");
            ImGui.PushItemWidth(-MathF.Min(ImGui.GetFontSize() * 12, ImGui.GetContentRegionAvail().Width * 0.40f));
            ImGui.DragFloat("float##5a", ref s_ww_f);
            if (s_ww_show_indented_items)
            {
                ImGui.Indent();
                ImGui.DragFloat("float (indented)##5b", ref s_ww_f);
                ImGui.Unindent();
            }
            ImGui.PopItemWidth();

            // Demonstrate using PushItemWidth to surround three items.
            // Calling SetNextItemWidth() before each of them would have the same effect.
            ImGui.Text("SetNextItemWidth/PushItemWidth(-FLT_MIN)");
            ImGui.SameLine(); HelpMarker("Align to right edge");
            ImGui.PushItemWidth(-float.Epsilon);
            ImGui.DragFloat("##float6a", ref s_ww_f);
            if (s_ww_show_indented_items)
            {
                ImGui.Indent();
                ImGui.DragFloat("float (indented)##6b", ref s_ww_f);
                ImGui.Unindent();
            }
            ImGui.PopItemWidth();

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Basic Horizontal Layout"))
        {
            // DEMO MARKER: Layout/Basic Horizontal Layout
            ImGui.TextWrapped("(Use ImGui::SameLine() to keep adding items to the right of the preceding item)");

            // Text
            // DEMO MARKER: Layout/Basic Horizontal Layout/SameLine
            ImGui.Text("Two items: Hello"); ImGui.SameLine();
            ImGui.TextColored(1, 1, 0, 1, "Sailor");

            // Adjust spacing
            ImGui.Text("More spacing: Hello"); ImGui.SameLine(0, 20);
            ImGui.TextColored(1, 1, 0, 1, "Sailor");

            // Button
            ImGui.AlignTextToFramePadding();
            ImGui.Text("Normal buttons"); ImGui.SameLine();
            ImGui.Button("Banana"); ImGui.SameLine();
            ImGui.Button("Apple"); ImGui.SameLine();
            ImGui.Button("Corniflower");

            // Button
            ImGui.Text("Small buttons"); ImGui.SameLine();
            ImGui.SmallButton("Like this one"); ImGui.SameLine();
            ImGui.Text("can fit within a text block.");

            // Aligned to arbitrary position. Easy/cheap column.
            // DEMO MARKER: Layout/Basic Horizontal Layout/SameLine (with offset)
            ImGui.Text("Aligned");
            ImGui.SameLine(150); ImGui.Text("x=150");
            ImGui.SameLine(300); ImGui.Text("x=300");
            ImGui.Text("Aligned");
            ImGui.SameLine(150); ImGui.SmallButton("x=150");
            ImGui.SameLine(300); ImGui.SmallButton("x=300");

            // Checkbox
            // DEMO MARKER: Layout/Basic Horizontal Layout/SameLine (more)
            ImGui.Checkbox("My", ref s_hl_c1); ImGui.SameLine();
            ImGui.Checkbox("Tailor", ref s_hl_c2); ImGui.SameLine();
            ImGui.Checkbox("Is", ref s_hl_c3); ImGui.SameLine();
            ImGui.Checkbox("Rich", ref s_hl_c4);

            // Various
            ImGui.PushItemWidth(ImGui.CalcTextSize("AAAAAAA").Width);
            var items = new[] { "AAAA", "BBBB", "CCCC", "DDDD" };
            ImGui.Combo("Combo", ref s_hl_item, items); ImGui.SameLine();
            ImGui.SliderFloat("X", ref s_hl_f0, 0.0f, 5.0f); ImGui.SameLine();
            ImGui.SliderFloat("Y", ref s_hl_f1, 0.0f, 5.0f); ImGui.SameLine();
            ImGui.SliderFloat("Z", ref s_hl_f2, 0.0f, 5.0f);

            ImGui.Text("Lists:");
            for (int i = 0; i < 4; i++)
            {
                if (i > 0) ImGui.SameLine();
                ImGui.PushID(i);
                ImGui.ListBox("", ref s_hl_selection[i], items);
                ImGui.PopID();
                //ImGui.SetItemTooltip($"ListBox {i} hovered");
            }
            ImGui.PopItemWidth();

            // Dummy
            // DEMO MARKER: Layout/Basic Horizontal Layout/Dummy
            var buttonSz = new Vec2(40, 40);
            ImGui.Button("A", buttonSz.X, buttonSz.Y); ImGui.SameLine();
            ImGui.Dummy(buttonSz.X, buttonSz.Y); ImGui.SameLine();
            ImGui.Button("B", buttonSz.X, buttonSz.Y);

            // Manually wrapping
            // (we should eventually provide this as an automatic layout feature, but for now you can do it manually)
            // DEMO MARKER: Layout/Basic Horizontal Layout/Manual wrapping
            ImGui.Text("Manual wrapping:");
            int buttonsCount = 20;
            float windowVisibleX2 = ImGui.GetCursorScreenPos().X + ImGui.GetContentRegionAvail().Width;
            for (int n = 0; n < buttonsCount; n++)
            {
                ImGui.PushID(n);
                ImGui.Button("Box", buttonSz.X, buttonSz.Y);
                float lastButtonX2 = ImGui.GetItemRectMax().X;
                float nextButtonX2 = lastButtonX2 + Style.ItemSpacing.X + buttonSz.X; // Expected position if next button was on same line
                if (n + 1 < buttonsCount && nextButtonX2 < windowVisibleX2)
                    ImGui.SameLine();
                ImGui.PopID();
            }

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Groups"))
        {
            // DEMO MARKER: Layout/Groups
            HelpMarker(
                "BeginGroup() basically locks the horizontal position for new line. " +
                "EndGroup() bundles the whole group so that you can use \"item\" functions such as " +
                "IsItemHovered()/IsItemActive() or SameLine() etc. on the whole group.");
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
                ImGui.SetItemTooltip("First group hovered");
            }
            // Capture the group size and create widgets using the same size
            var size = ImGui.GetItemRectSize();
            float[] values = { 0.5f, 0.20f, 0.80f, 0.60f, 0.25f };
            ImGui.PlotHistogram("##values", values, 0, null, 0.0f, 1.0f, size.Width, size.Height);

            ImGui.Button("ACTION", (size.Width - Style.ItemSpacing.X) * 0.5f, size.Height);
            ImGui.SameLine();
            ImGui.Button("REACTION", (size.Width - Style.ItemSpacing.X) * 0.5f, size.Height);
            ImGui.EndGroup();
            ImGui.SameLine();

            ImGui.Button("LEVERAGE\nBUZZWORD", size.Width, size.Height);
            ImGui.SameLine();

            if (ImGui.BeginListBox("List", size.Width, size.Height))
            {
                ImGui.Selectable("Selected", true);
                ImGui.Selectable("Not Selected", false);
                ImGui.EndListBox();
            }

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Text Baseline Alignment"))
        {
            // DEMO MARKER: Layout/Text Baseline Alignment
            {
                ImGui.BulletText("Text baseline:");
                ImGui.SameLine(); HelpMarker(
                    "This is testing the vertical alignment that gets applied on text to keep it aligned with widgets. " +
                    "Lines only composed of text or \"small\" widgets use less vertical space than lines with framed widgets.");
                ImGui.Indent();

                ImGui.Text("KO Blahblah"); ImGui.SameLine();
                ImGui.Button("Some framed item"); ImGui.SameLine();
                HelpMarker("Baseline of button will look misaligned with text..");

                // If your line starts with text, call AlignTextToFramePadding() to align text to upcoming widgets.
                // (because we don't know what's coming after the Text() statement, we need to move the text baseline
                // down by FramePadding.y ahead of time)
                ImGui.AlignTextToFramePadding();
                ImGui.Text("OK Blahblah"); ImGui.SameLine();
                ImGui.Button("Some framed item##2"); ImGui.SameLine();
                HelpMarker("We call AlignTextToFramePadding() to vertically align the text baseline by +FramePadding.y");

                // SmallButton() uses the same vertical padding as Text
                ImGui.Button("TEST##1"); ImGui.SameLine();
                ImGui.Text("TEST"); ImGui.SameLine();
                ImGui.SmallButton("TEST##2");

                // If your line starts with text, call AlignTextToFramePadding() to align text to upcoming widgets.
                ImGui.AlignTextToFramePadding();
                ImGui.Text("Text aligned to framed item"); ImGui.SameLine();
                ImGui.Button("Item##1"); ImGui.SameLine();
                ImGui.Text("Item"); ImGui.SameLine();
                ImGui.SmallButton("Item##2"); ImGui.SameLine();
                ImGui.Button("Item##3");

                ImGui.Unindent();
            }

            ImGui.Spacing();

            {
                ImGui.BulletText("Multi-line text:");
                ImGui.Indent();
                ImGui.Text("One\nTwo\nThree"); ImGui.SameLine();
                ImGui.Text("Hello\nWorld"); ImGui.SameLine();
                ImGui.Text("Banana");

                ImGui.Text("Banana"); ImGui.SameLine();
                ImGui.Text("Hello\nWorld"); ImGui.SameLine();
                ImGui.Text("One\nTwo\nThree");

                ImGui.Button("HOP##1"); ImGui.SameLine();
                ImGui.Text("Banana"); ImGui.SameLine();
                ImGui.Text("Hello\nWorld"); ImGui.SameLine();
                ImGui.Text("Banana");

                ImGui.Button("HOP##2"); ImGui.SameLine();
                ImGui.Text("Hello\nWorld"); ImGui.SameLine();
                ImGui.Text("Banana");
                ImGui.Unindent();
            }

            ImGui.Spacing();

            {
                ImGui.BulletText("Misc items:");
                ImGui.Indent();

                // SmallButton() sets FramePadding to zero. Text baseline is aligned to match baseline of previous Button.
                ImGui.Button("80x80", 80, 80);
                ImGui.SameLine();
                ImGui.Button("50x50", 50, 50);
                ImGui.SameLine();
                ImGui.Button("Button()");
                ImGui.SameLine();
                ImGui.SmallButton("SmallButton()");

                // Tree
                // (here the node appears after a button and has odd intent, so we use ImGuiTreeNodeFlags_DrawLinesNone to disable hierarchy outline)
                float spacing = Style.ItemInnerSpacing.X;
                ImGui.Button("Button##1"); // Will make line higher
                ImGui.SameLine(0.0f, spacing);
                if (ImGui.TreeNodeEx("Node##1", TreeNodeFlags.DrawLinesNone))
                {
                    // Placeholder tree data
                    for (int i = 0; i < 6; i++)
                        ImGui.BulletText($"Item {i}..");
                    ImGui.TreePop();
                }

                float padding = (float)(int)(ImGui.GetFontSize() * 1.20f); // Large padding
                ImGui.PushStyleVarY(StyleVar.FramePadding, padding);
                ImGui.Button("Button##2");
                ImGui.PopStyleVar();
                ImGui.SameLine(0.0f, spacing);
                if (ImGui.TreeNodeEx("Node##2", TreeNodeFlags.DrawLinesNone))
                    ImGui.TreePop();

                // Vertically align text node a bit lower so it'll be vertically centered with upcoming widget.
                // Otherwise you can use SmallButton() (smaller fit).
                ImGui.AlignTextToFramePadding();

                // Common mistake to avoid: if we want to SameLine after TreeNode we need to do it before we add
                // other contents "inside" the node.
                bool nodeOpen = ImGui.TreeNode("Node##3");
                ImGui.SameLine(0.0f, spacing); ImGui.Button("Button##3");
                if (nodeOpen)
                {
                    // Placeholder tree data
                    for (int i = 0; i < 6; i++)
                        ImGui.BulletText($"Item {i}..");
                    ImGui.TreePop();
                }

                // Bullet
                ImGui.Button("Button##4");
                ImGui.SameLine(0.0f, spacing);
                ImGui.BulletText("Bullet text");

                ImGui.AlignTextToFramePadding();
                ImGui.BulletText("Node");
                ImGui.SameLine(0.0f, spacing); ImGui.Button("Button##5");
                ImGui.Unindent();
            }

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Scrolling"))
        {
            // DEMO MARKER: Layout/Scrolling/Vertical
            // Vertical scroll functions
            HelpMarker("Use SetScrollHereY() or SetScrollFromPosY() to scroll to a given vertical position.");

            ImGui.Checkbox("Decoration", ref s_scroll_enable_extra_decorations);

            ImGui.PushItemWidth(ImGui.GetFontSize() * 10);
            s_scroll_enable_track |= ImGui.DragInt("##item", ref s_scroll_track_item, 0.25f, 0, 99, "Item = %d");
            ImGui.SameLine();
            ImGui.Checkbox("Track", ref s_scroll_enable_track);

            bool scrollToOff = ImGui.DragFloat("##off", ref s_scroll_to_off_px, 1.00f, 0, float.MaxValue, "+%.0f px");
            ImGui.SameLine();
            scrollToOff |= ImGui.Button("Scroll Offset");

            bool scrollToPos = ImGui.DragFloat("##pos", ref s_scroll_to_pos_px, 1.00f, -10, float.MaxValue, "X/Y = %.0f px");
            ImGui.SameLine();
            scrollToPos |= ImGui.Button("Scroll To Pos");
            ImGui.PopItemWidth();

            if (scrollToOff || scrollToPos)
                s_scroll_enable_track = false;

            float childW = (ImGui.GetContentRegionAvail().Width - 4 * Style.ItemSpacing.X) / 5;
            if (childW < 1.0f)
                childW = 1.0f;
            ImGui.PushID("##VerticalScrolling");
            {
                var names = new[] { "Top", "25%", "Center", "75%", "Bottom" };
                for (int i = 0; i < 5; i++)
                {
                    if (i > 0) ImGui.SameLine();
                    ImGui.BeginGroup();
                    ImGui.TextUnformatted(names[i]);

                    var childFlags = s_scroll_enable_extra_decorations ? WindowFlags.MenuBar : WindowFlags.None;
                    uint childId = ImGui.GetID((nint)i);
                    bool childIsVisible = ImGui.BeginChild(childId, childW, 200.0f, ChildFlags.Borders, childFlags);
                    if (ImGui.BeginMenuBar())
                    {
                        ImGui.TextUnformatted("abc");
                        ImGui.EndMenuBar();
                    }
                    if (scrollToOff)
                        ImGui.SetScrollY(s_scroll_to_off_px);
                    if (scrollToPos)
                        ImGui.SetScrollFromPosY(ImGui.GetCursorStartPos().Y + s_scroll_to_pos_px, i * 0.25f);
                    if (childIsVisible) // Avoid calling SetScrollHereY when running with culled items
                    {
                        for (int item = 0; item < 100; item++)
                        {
                            if (s_scroll_enable_track && item == s_scroll_track_item)
                            {
                                ImGui.TextColored(1, 1, 0, 1, $"Item {item}");
                                ImGui.SetScrollHereY(i * 0.25f); // 0.0f:top, 0.5f:center, 1.0f:bottom
                            }
                            else
                            {
                                ImGui.Text($"Item {item}");
                            }
                        }
                    }
                    float scrollY = ImGui.GetScrollY();
                    float scrollMaxY = ImGui.GetScrollMaxY();
                    ImGui.EndChild();
                    ImGui.Text($"{scrollY:F0}/{scrollMaxY:F0}");
                    ImGui.EndGroup();
                }
            }
            ImGui.PopID();

            // Horizontal scroll functions
            // DEMO MARKER: Layout/Scrolling/Horizontal
            ImGui.Spacing();
            HelpMarker(
                "Use SetScrollHereX() or SetScrollFromPosX() to scroll to a given horizontal position.\n\n" +
                "Because the clipping rectangle of most window hides half worth of WindowPadding on the " +
                "left/right, using SetScrollFromPosX(+1) will usually result in clipped text whereas the " +
                "equivalent SetScrollFromPosY(+1) wouldn't.");
            ImGui.PushID("##HorizontalScrolling");
            {
                var names = new[] { "Left", "25%", "Center", "75%", "Right" };
                for (int i = 0; i < 5; i++)
                {
                    float childHeight = ImGui.GetTextLineHeight() + Style.ScrollbarSize + Style.WindowPadding.Y * 2.0f;
                    var childFlags = WindowFlags.HorizontalScrollbar | (s_scroll_enable_extra_decorations ? WindowFlags.AlwaysVerticalScrollbar : WindowFlags.None);
                    uint childId = ImGui.GetID((nint)i);
                    bool childIsVisible = ImGui.BeginChild(childId, -100, childHeight, ChildFlags.Borders, childFlags);
                    if (scrollToOff)
                        ImGui.SetScrollX(s_scroll_to_off_px);
                    if (scrollToPos)
                        ImGui.SetScrollFromPosX(ImGui.GetCursorStartPos().X + s_scroll_to_pos_px, i * 0.25f);
                    if (childIsVisible) // Avoid calling SetScrollHereY when running with culled items
                    {
                        for (int item = 0; item < 100; item++)
                        {
                            if (item > 0)
                                ImGui.SameLine();
                            if (s_scroll_enable_track && item == s_scroll_track_item)
                            {
                                ImGui.TextColored(1, 1, 0, 1, $"Item {item}");
                                ImGui.SetScrollHereX(i * 0.25f); // 0.0f:left, 0.5f:center, 1.0f:right
                            }
                            else
                            {
                                ImGui.Text($"Item {item}");
                            }
                        }
                    }
                    float scrollX = ImGui.GetScrollX();
                    float scrollMaxX = ImGui.GetScrollMaxX();
                    ImGui.EndChild();
                    ImGui.SameLine();
                    ImGui.Text($"{names[i]}\n{scrollX:F0}/{scrollMaxX:F0}");
                    ImGui.Spacing();
                }
            }
            ImGui.PopID();

            // Miscellaneous Horizontal Scrolling Demo
            // DEMO MARKER: Layout/Scrolling/Horizontal (more)
            HelpMarker(
                "Horizontal scrolling for a window is enabled via the ImGuiWindowFlags_HorizontalScrollbar flag.\n\n" +
                "You may want to also explicitly specify content width by using SetNextWindowContentWidth() before Begin().");
            ImGui.SliderInt("Lines", ref s_scroll_lines, 1, 15);
            ImGui.PushStyleVar(StyleVar.FrameRounding, 3.0f);
            ImGui.PushStyleVar(StyleVar.FramePadding, 2.0f, 1.0f);
            var scrollingChildSize = new Vec2(0, ImGui.GetFrameHeightWithSpacing() * 7 + 30);
            ImGui.BeginChild("scrolling", scrollingChildSize.X, scrollingChildSize.Y, ChildFlags.Borders, WindowFlags.HorizontalScrollbar);
            for (int line = 0; line < s_scroll_lines; line++)
            {
                // Display random stuff. For the sake of this trivial demo we are using basic Button() + SameLine()
                // If you want to create your own time line for a real application you may be better off manipulating
                // the cursor position yourself, aka using SetCursorPos/SetCursorScreenPos to position the widgets
                // yourself. You may also want to use the lower-level ImDrawList API.
                int numButtons = 10 + (((line & 1) != 0) ? line * 9 : line * 3);
                for (int n = 0; n < numButtons; n++)
                {
                    if (n > 0) ImGui.SameLine();
                    ImGui.PushID(n + line * 1000);
                    var label = (n % 15 == 0) ? "FizzBuzz" : (n % 3 == 0) ? "Fizz" : (n % 5 == 0) ? "Buzz" : n.ToString();
                    float hue = n * 0.05f;
                    var (r, g, b) = ImGui.ColorConvertHSVtoRGB(hue, 0.6f, 0.6f);
                    var (rh, gh, bh) = ImGui.ColorConvertHSVtoRGB(hue, 0.7f, 0.7f);
                    var (ra, ga, ba) = ImGui.ColorConvertHSVtoRGB(hue, 0.8f, 0.8f);
                    ImGui.PushStyleColor(Col.Button, r, g, b, 1.0f);
                    ImGui.PushStyleColor(Col.ButtonHovered, rh, gh, bh, 1.0f);
                    ImGui.PushStyleColor(Col.ButtonActive, ra, ga, ba, 1.0f);
                    ImGui.Button(label, 40.0f + MathF.Sin((float)(line + n)) * 20.0f, 0.0f);
                    ImGui.PopStyleColor(3);
                    ImGui.PopID();
                }
            }
            float miscScrollX = ImGui.GetScrollX();
            float miscScrollMaxX = ImGui.GetScrollMaxX();
            ImGui.EndChild();
            ImGui.PopStyleVar(2);
            float scrollXDelta = 0.0f;
            ImGui.SmallButton("<<");
            if (ImGui.IsItemActive())
                scrollXDelta = -Io.DeltaTime * 1000.0f;
            ImGui.SameLine();
            ImGui.Text("Scroll from code"); ImGui.SameLine();
            ImGui.SmallButton(">>");
            if (ImGui.IsItemActive())
                scrollXDelta = +Io.DeltaTime * 1000.0f;
            ImGui.SameLine();
            ImGui.Text($"{miscScrollX:F0}/{miscScrollMaxX:F0}");
            if (scrollXDelta != 0.0f)
            {
                // Demonstrate a trick: you can use Begin to set yourself in the context of another window
                // (here we are already out of your child window)
                ImGui.BeginChild("scrolling");
                ImGui.SetScrollX(ImGui.GetScrollX() + scrollXDelta);
                ImGui.EndChild();
            }
            ImGui.Spacing();

            ImGui.Checkbox("Show Horizontal contents size demo window", ref s_scroll_show_horizontal_contents_size_demo_window);

            if (s_scroll_show_horizontal_contents_size_demo_window)
            {
                if (s_hcs_explicit_content_size)
                    ImGui.SetNextWindowContentSize(s_hcs_contents_size_x, 0.0f);
                ImGui.Begin("Horizontal contents size demo window", ref s_scroll_show_horizontal_contents_size_demo_window, s_hcs_show_h_scrollbar ? WindowFlags.HorizontalScrollbar : WindowFlags.None);
                // DEMO MARKER: Layout/Scrolling/Horizontal contents size demo window
                ImGui.PushStyleVar(StyleVar.ItemSpacing, 2, 0);
                ImGui.PushStyleVar(StyleVar.FramePadding, 2, 0);
                HelpMarker(
                    "Test how different widgets react and impact the work rectangle growing when horizontal scrolling is enabled.\n\n" +
                    "Use 'Metrics->Tools->Show windows rectangles' to visualize rectangles.");
                ImGui.Checkbox("H-scrollbar", ref s_hcs_show_h_scrollbar);
                ImGui.Checkbox("Button", ref s_hcs_show_button);            // Will grow contents size (unless explicitly overwritten)
                ImGui.Checkbox("Tree nodes", ref s_hcs_show_tree_nodes);    // Will grow contents size and display highlight over full width
                ImGui.Checkbox("Text wrapped", ref s_hcs_show_text_wrapped);// Will grow and use contents size
                ImGui.Checkbox("Columns", ref s_hcs_show_columns);          // Will use contents size
                ImGui.Checkbox("Tab bar", ref s_hcs_show_tab_bar);          // Will use contents size
                ImGui.Checkbox("Child", ref s_hcs_show_child);              // Will grow and use contents size
                ImGui.Checkbox("Explicit content size", ref s_hcs_explicit_content_size);
                ImGui.Text($"Scroll {ImGui.GetScrollX():F1}/{ImGui.GetScrollMaxX():F1} {ImGui.GetScrollY():F1}/{ImGui.GetScrollMaxY():F1}");
                if (s_hcs_explicit_content_size)
                {
                    ImGui.SameLine();
                    ImGui.SetNextItemWidth(ImGui.CalcTextSize("123456").Width);
                    ImGui.DragFloat("##csx", ref s_hcs_contents_size_x);
                    var p = ImGui.GetCursorScreenPos();
                    ImGui.GetWindowDrawList().AddRectFilled(new Vec2(p.X, p.Y), new Vec2(p.X + 10, p.Y + 10), 0xFFFFFFFFu); // IM_COL32_WHITE
                    ImGui.GetWindowDrawList().AddRectFilled(new Vec2(p.X + s_hcs_contents_size_x - 10, p.Y), new Vec2(p.X + s_hcs_contents_size_x, p.Y + 10), 0xFFFFFFFFu); // IM_COL32_WHITE
                    ImGui.Dummy(0, 10);
                }
                ImGui.PopStyleVar(2);
                ImGui.Separator();
                if (s_hcs_show_button)
                {
                    ImGui.Button("this is a 300-wide button", 300, 0);
                }
                if (s_hcs_show_tree_nodes)
                {
                    bool open = true;
                    if (ImGui.TreeNode("this is a tree node"))
                    {
                        if (ImGui.TreeNode("another one of those tree node..."))
                        {
                            ImGui.Text("Some tree contents");
                            ImGui.TreePop();
                        }
                        ImGui.TreePop();
                    }
                    ImGui.CollapsingHeader("CollapsingHeader", ref open);
                }
                if (s_hcs_show_text_wrapped)
                {
                    ImGui.TextWrapped("This text should automatically wrap on the edge of the work rectangle.");
                }
                if (s_hcs_show_columns)
                {
                    ImGui.Text("Tables:");
                    if (ImGui.BeginTable("table", 4, TableFlags.Borders))
                    {
                        for (int n = 0; n < 4; n++)
                        {
                            ImGui.TableNextColumn();
                            ImGui.Text($"Width {ImGui.GetContentRegionAvail().Width:F2}");
                        }
                        ImGui.EndTable();
                    }
                    ImGui.Text("Columns:");
                    ImGui.Columns(4);
                    for (int n = 0; n < 4; n++)
                    {
                        ImGui.Text($"Width {ImGui.GetColumnWidth():F2}");
                        ImGui.NextColumn();
                    }
                    ImGui.Columns(1);
                }
                if (s_hcs_show_tab_bar && ImGui.BeginTabBar("Hello"))
                {
                    if (ImGui.BeginTabItem("OneOneOne")) { ImGui.EndTabItem(); }
                    if (ImGui.BeginTabItem("TwoTwoTwo")) { ImGui.EndTabItem(); }
                    if (ImGui.BeginTabItem("ThreeThreeThree")) { ImGui.EndTabItem(); }
                    if (ImGui.BeginTabItem("FourFourFour")) { ImGui.EndTabItem(); }
                    ImGui.EndTabBar();
                }
                if (s_hcs_show_child)
                {
                    ImGui.BeginChild("child", 0, 0, ChildFlags.Borders);
                    ImGui.EndChild();
                }
                ImGui.End();
            }

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Text Clipping"))
        {
            // DEMO MARKER: Layout/Text Clipping
            ImGui.DragFloat2("size", s_clip_size, 0.5f, 1.0f, 200.0f, "%.0f");
            ImGui.TextWrapped("(Click and drag to scroll)");

            HelpMarker(
                "(Left) Using ImGui::PushClipRect():\n" +
                "Will alter ImGui hit-testing logic + ImDrawList rendering.\n" +
                "(use this if you want your clipping rectangle to affect interactions)\n\n" +
                "(Center) Using ImDrawList::PushClipRect():\n" +
                "Will alter ImDrawList rendering only.\n" +
                "(use this as a shortcut if you are only using ImDrawList calls)\n\n" +
                "(Right) Using ImDrawList::AddText() with a fine ClipRect:\n" +
                "Will alter only this specific ImDrawList::AddText() rendering.\n" +
                "This is often used internally to avoid altering the clipping rectangle and minimize draw calls.");

            for (int n = 0; n < 3; n++)
            {
                if (n > 0)
                    ImGui.SameLine();

                ImGui.PushID(n);
                ImGui.InvisibleButton("##canvas", s_clip_size[0], s_clip_size[1]);
                if (ImGui.IsItemActive() && ImGui.IsMouseDragging(MouseButton.Left))
                {
                    s_clip_offset_x += Io.MouseDelta.X;
                    s_clip_offset_y += Io.MouseDelta.Y;
                }
                ImGui.PopID();
                if (!ImGui.IsItemVisible()) // Skip rendering as ImDrawList elements are not clipped.
                    continue;

                var (p0X, p0Y) = ImGui.GetItemRectMin();
                var (p1X, p1Y) = ImGui.GetItemRectMax();
                var p0 = new Vec2(p0X, p0Y);
                var p1 = new Vec2(p1X, p1Y);
                const string textStr = "Line 1 hello\nLine 2 clip me!";
                var textPos = new Vec2(p0.X + s_clip_offset_x, p0.Y + s_clip_offset_y);
                var drawList = ImGui.GetWindowDrawList();
                switch (n)
                {
                    case 0:
                        ImGui.PushClipRect(p0, p1, true);
                        drawList.AddRectFilled(p0, p1, 0xFF785A5Au); // IM_COL32(90, 90, 120, 255)
                        drawList.AddText(textPos, 0xFFFFFFFFu, textStr); // IM_COL32_WHITE
                        ImGui.PopClipRect();
                        break;
                    case 1:
                        drawList.PushClipRect(p0, p1, true);
                        drawList.AddRectFilled(p0, p1, 0xFF785A5Au); // IM_COL32(90, 90, 120, 255)
                        drawList.AddText(textPos, 0xFFFFFFFFu, textStr); // IM_COL32_WHITE
                        drawList.PopClipRect();
                        break;
                    case 2:
                        var clipRect = new Vec4(p0.X, p0.Y, p1.X, p1.Y); // AddText() takes a clip rect here.
                        drawList.AddRectFilled(p0, p1, 0xFF785A5Au); // IM_COL32(90, 90, 120, 255)
                        drawList.AddText(ImGui.GetFont(), ImGui.GetFontSize(), textPos, 0xFFFFFFFFu, textStr, 0.0f, clipRect); // IM_COL32_WHITE
                        break;
                }
            }

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Overlap Mode"))
        {
            // DEMO MARKER: Layout/Overlap Mode
            HelpMarker(
                "Hit-testing is by default performed in item submission order, which generally is perceived as 'back-to-front'.\n\n" +
                "By using SetNextItemAllowOverlap() you can notify that an item may be overlapped by another. " +
                "Doing so alters the hovering logic: items using AllowOverlap mode requires an extra frame to accept hovered state.");
            ImGui.Checkbox("Enable AllowOverlap", ref s_overlap_enable_allow_overlap);

            var button1Pos = ImGui.GetCursorScreenPos();
            var button2Pos = new Vec2(button1Pos.X + 50.0f, button1Pos.Y + 50.0f);
            if (s_overlap_enable_allow_overlap)
                ImGui.SetNextItemAllowOverlap();
            ImGui.Button("Button 1", 80, 80);
            ImGui.SetCursorScreenPos(button2Pos.X, button2Pos.Y);
            ImGui.Button("Button 2", 80, 80);

            // This is typically used with width-spanning items.
            // (note that Selectable() has a dedicated flag ImGuiSelectableFlags_AllowOverlap, which is a shortcut
            // for using SetNextItemAllowOverlap(). For demo purpose we use SetNextItemAllowOverlap() here.)
            if (s_overlap_enable_allow_overlap)
                ImGui.SetNextItemAllowOverlap();
            ImGui.Selectable("Some Selectable", false);
            ImGui.SameLine();
            ImGui.SmallButton("++");

            ImGui.TreePop();
        }
    }

    // -------------------------------------------------------------------------
    // SECTION: DemoWindowPopups()
    // -------------------------------------------------------------------------
    private static void DemoWindowPopups()
    {
        if (!ImGui.CollapsingHeader("Popups & Modal windows"))
            return;

        // The properties of popups windows are:
        // - They block normal mouse hovering detection outside them. (*)
        // - Unless modal, they can be closed by clicking anywhere outside them, or by pressing ESCAPE.
        // - Their visibility state (~bool) is held internally by Dear ImGui instead of being held by the programmer as
        //   we are used to with regular Begin() calls. User can manipulate the visibility state by calling OpenPopup().
        // (*) One can use IsItemHovered(ImGuiHoveredFlags_AllowWhenBlockedByPopup) to bypass it and detect hovering even
        //     when normally blocked by a popup.
        // Those three properties are connected. The library needs to hold their visibility state BECAUSE it can close
        // popups at any time.

        // Typical use for regular windows:
        //   bool my_tool_is_active = false; if (ImGui.Button("Open")) my_tool_is_active = true; [...] if (my_tool_is_active) Begin("My Tool", &my_tool_is_active) { [...] } End();
        // Typical use for popups:
        //   if (ImGui.Button("Open")) ImGui.OpenPopup("MyPopup"); if (ImGui.BeginPopup("MyPopup")) { [...] EndPopup(); }

        // With popups we have to go through a library call (here OpenPopup) to manipulate the visibility state.
        // This may be a bit confusing at first but it should quickly make sense. Follow on the examples below.

        if (ImGui.TreeNode("Popups"))
        {
            // DEMO MARKER: Popups/Popups
            ImGui.TextWrapped(
                "When a popup is active, it inhibits interacting with windows that are behind the popup. " +
                "Clicking outside the popup closes it.");

            var names = new[] { "Bream", "Haddock", "Mackerel", "Pollock", "Tilefish" };

            // Simple selection popup (if you want to show the current selection inside the Button itself,
            // you may want to build a string using the "###" operator to preserve a constant ID with a variable label)
            if (ImGui.Button("Select.."))
                ImGui.OpenPopup("my_select_popup");
            ImGui.SameLine();
            ImGui.TextUnformatted(s_popups_selected_fish == -1 ? "<None>" : names[s_popups_selected_fish]);
            if (ImGui.BeginPopup("my_select_popup"))
            {
                ImGui.SeparatorText("Aquarium");
                for (int i = 0; i < names.Length; i++)
                    if (ImGui.Selectable(names[i]))
                        s_popups_selected_fish = i;
                ImGui.EndPopup();
            }

            // Showing a menu with toggles
            if (ImGui.Button("Toggle.."))
                ImGui.OpenPopup("my_toggle_popup");
            if (ImGui.BeginPopup("my_toggle_popup"))
            {
                for (int i = 0; i < names.Length; i++)
                    ImGui.MenuItem(names[i], "", ref s_popups_toggles[i]);
                if (ImGui.BeginMenu("Sub-menu"))
                {
                    ImGui.MenuItem("Click me");
                    ImGui.EndMenu();
                }

                ImGui.Separator();
                ImGui.Text("Tooltip here");
                ImGui.SetItemTooltip("I am a tooltip over a popup");

                if (ImGui.Button("Stacked Popup"))
                    ImGui.OpenPopup("another popup");
                if (ImGui.BeginPopup("another popup"))
                {
                    for (int i = 0; i < names.Length; i++)
                        ImGui.MenuItem(names[i], "", ref s_popups_toggles[i]);
                    if (ImGui.BeginMenu("Sub-menu"))
                    {
                        ImGui.MenuItem("Click me");
                        if (ImGui.Button("Stacked Popup"))
                            ImGui.OpenPopup("another popup");
                        if (ImGui.BeginPopup("another popup"))
                        {
                            ImGui.Text("I am the last one here.");
                            ImGui.EndPopup();
                        }
                        ImGui.EndMenu();
                    }
                    ImGui.EndPopup();
                }
                ImGui.EndPopup();
            }

            // Call the more complete ShowExampleMenuFile which we use in various places of this demo
            if (ImGui.Button("With a menu.."))
                ImGui.OpenPopup("my_file_popup");
            if (ImGui.BeginPopup("my_file_popup", WindowFlags.MenuBar))
            {
                if (ImGui.BeginMenuBar())
                {
                    if (ImGui.BeginMenu("File"))
                    {
                        ShowExampleMenuFile();
                        ImGui.EndMenu();
                    }
                    if (ImGui.BeginMenu("Edit"))
                    {
                        ImGui.MenuItem("Dummy");
                        ImGui.EndMenu();
                    }
                    ImGui.EndMenuBar();
                }
                ImGui.Text("Hello from popup!");
                ImGui.Button("This is a dummy button..");
                ImGui.EndPopup();
            }

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Context menus"))
        {
            // DEMO MARKER: Popups/Context menus
            HelpMarker("\"Context\" functions are simple helpers to associate a Popup to a given Item or Window identifier.");

            // BeginPopupContextItem() is a helper to provide common/simple popup behavior of essentially doing:
            //     if (id == 0)
            //         id = GetItemID(); // Use last item id
            //     if (IsItemHovered() && IsMouseReleased(ImGuiMouseButton_Right))
            //         OpenPopup(id);
            //     return BeginPopup(id);
            // For advanced uses you may want to replicate and customize this code.
            // See more details in BeginPopupContextItem().

            // Example 1
            // When used after an item that has an ID (e.g. Button), we can skip providing an ID to BeginPopupContextItem(),
            // and BeginPopupContextItem() will use the last item ID as the popup ID.
            {
                var names = new[] { "Label1", "Label2", "Label3", "Label4", "Label5" };
                for (int n = 0; n < 5; n++)
                {
                    if (ImGui.Selectable(names[n], s_ctx_selected == n))
                        s_ctx_selected = n;
                    if (ImGui.BeginPopupContextItem()) // <-- use last item id as popup id
                    {
                        s_ctx_selected = n;
                        ImGui.Text($"This is a popup for \"{names[n]}\"!");
                        if (ImGui.Button("Close"))
                            ImGui.CloseCurrentPopup();
                        ImGui.EndPopup();
                    }
                    ImGui.SetItemTooltip("Right-click to open popup");
                }
            }

            // Example 2
            // Popup on a Text() element which doesn't have an identifier: we need to provide an identifier to BeginPopupContextItem().
            // Using an explicit identifier is also convenient if you want to activate the popups from different locations.
            {
                HelpMarker("Text() elements don't have stable identifiers so we need to provide one.");
                ImGui.Text($"Value = {s_ctx_value:F3} <-- (1) right-click this text");
                if (ImGui.BeginPopupContextItem("my popup"))
                {
                    if (ImGui.Selectable("Set to zero")) s_ctx_value = 0.0f;
                    if (ImGui.Selectable("Set to PI")) s_ctx_value = 3.1415f;
                    ImGui.SetNextItemWidth(-float.Epsilon);
                    ImGui.DragFloat("##Value", ref s_ctx_value, 0.1f, 0.0f, 0.0f);
                    ImGui.EndPopup();
                }

                // We can also use OpenPopupOnItemClick() to toggle the visibility of a given popup.
                // Here we make it that right-clicking this other text element opens the same popup as above.
                // The popup itself will be submitted by the code above.
                ImGui.Text("(2) Or right-click this text");
                ImGui.OpenPopupOnItemClick("my popup", PopupFlags.MouseButtonRight);

                // Back to square one: manually open the same popup.
                if (ImGui.Button("(3) Or click this button"))
                    ImGui.OpenPopup("my popup");
            }

            // Example 3
            // When using BeginPopupContextItem() with an implicit identifier (NULL == use last item ID),
            // we need to make sure your item identifier is stable.
            // In this example we showcase altering the item label while preserving its identifier, using the ### operator (see FAQ).
            {
                HelpMarker("Showcase using a popup ID linked to item ID, with the item having a changing label + stable ID using the ### operator.");
                ImGui.Button($"Button: {s_ctx_name}###Button"); // ### operator override ID ignoring the preceding label
                if (ImGui.BeginPopupContextItem())
                {
                    ImGui.Text("Edit name:");
                    ImGui.InputText("##edit", ref s_ctx_name);
                    if (ImGui.Button("Close"))
                        ImGui.CloseCurrentPopup();
                    ImGui.EndPopup();
                }
                ImGui.SameLine(); ImGui.Text("(<-- right-click here)");
            }

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Modals"))
        {
            // DEMO MARKER: Popups/Modals
            ImGui.TextWrapped("Modal windows are like popups but the user cannot close them by clicking outside.");

            if (ImGui.Button("Delete.."))
                ImGui.OpenPopup("Delete?");

            // Always center this window when appearing
            var center = ImGui.GetMainViewport().Center;
            ImGui.SetNextWindowPos(center.X, center.Y, Cond.Appearing, 0.5f, 0.5f);

            if (ImGui.BeginPopupModal("Delete?", WindowFlags.AlwaysAutoResize))
            {
                ImGui.Text("All those beautiful files will be deleted.\nThis operation cannot be undone!");
                ImGui.Separator();

                //static int unused_i = 0;
                //ImGui.Combo("Combo", ref unused_i, new[] { "Delete", "Delete harder" });

                ImGui.PushStyleVar(StyleVar.FramePadding, 0, 0);
                ImGui.Checkbox("Don't ask me next time", ref s_modal_dont_ask_me_next_time);
                ImGui.PopStyleVar();

                if (ImGui.Button("OK", 120, 0)) { ImGui.CloseCurrentPopup(); }
                ImGui.SetItemDefaultFocus();
                ImGui.SameLine();
                if (ImGui.Button("Cancel", 120, 0)) { ImGui.CloseCurrentPopup(); }
                ImGui.EndPopup();
            }

            if (ImGui.Button("Stacked modals.."))
                ImGui.OpenPopup("Stacked 1");
            if (ImGui.BeginPopupModal("Stacked 1", WindowFlags.MenuBar))
            {
                if (ImGui.BeginMenuBar())
                {
                    if (ImGui.BeginMenu("File"))
                    {
                        if (ImGui.MenuItem("Some menu item")) { }
                        ImGui.EndMenu();
                    }
                    ImGui.EndMenuBar();
                }
                ImGui.Text("Hello from Stacked The First\nUsing style.Colors[ImGuiCol_ModalWindowDimBg] behind it.");

                // Testing behavior of widgets stacking their own regular popups over the modal.
                ImGui.Combo("Combo", ref s_modal_item, new[] { "aaaa", "bbbb", "cccc", "dddd", "eeee" });
                ImGui.ColorEdit4("Color", s_modal_color);

                if (ImGui.Button("Add another modal.."))
                    ImGui.OpenPopup("Stacked 2");

                // Also demonstrate passing a bool* to BeginPopupModal(), this will create a regular close button which
                // will close the popup. Note that the visibility state of popups is owned by imgui, so the input value
                // of the bool actually doesn't matter here.
                bool unusedOpen = true;
                if (ImGui.BeginPopupModal("Stacked 2", ref unusedOpen))
                {
                    ImGui.Text("Hello from Stacked The Second!");
                    ImGui.ColorEdit4("Color", s_modal_color); // Allow opening another nested popup
                    if (ImGui.Button("Close"))
                        ImGui.CloseCurrentPopup();
                    ImGui.EndPopup();
                }

                if (ImGui.Button("Close"))
                    ImGui.CloseCurrentPopup();
                ImGui.EndPopup();
            }

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Menus inside a regular window"))
        {
            // DEMO MARKER: Popups/Menus inside a regular window
            ImGui.TextWrapped("Below we are testing adding menu items to a regular window. It's rather unusual but should work!");
            ImGui.Separator();

            ImGui.MenuItem("Menu item", "Ctrl+M");
            if (ImGui.BeginMenu("Menu inside a regular window"))
            {
                ShowExampleMenuFile();
                ImGui.EndMenu();
            }
            ImGui.Separator();
            ImGui.TreePop();
        }
    }
}
