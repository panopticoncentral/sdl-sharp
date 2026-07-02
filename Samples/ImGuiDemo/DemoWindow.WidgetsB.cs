// C# port of imgui_demo.cpp widget sections (part B):
//   [SECTION] DemoWindowWidgetsPlotting()
//   [SECTION] DemoWindowWidgetsProgressBars()
//   [SECTION] DemoWindowWidgetsQueryingStatuses()
//   [SECTION] DemoWindowWidgetsSelectables()
//   [SECTION] DemoWindowWidgetsSelectionAndMultiSelect()
//
// Upstream reference: imgui/imgui_demo.cpp (lines ~1986-3432).
// C++ function-static locals become private static fields, prefixed per section.

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using SdlSharp.ImGui;

namespace ImGuiDemo;

internal static unsafe partial class DemoWindow
{
    // -------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsPlotting()
    // -------------------------------------------------------------------------

    static bool s_plot_animate = true;
    static readonly float[] s_plot_arr = { 0.6f, 0.1f, 1.0f, 0.5f, 0.92f, 0.1f, 0.2f };
    static readonly float[] s_plot_values = new float[90];
    static int s_plot_values_offset;
    static double s_plot_refresh_time;
    static float s_plot_phase;
    static int s_plot_func_type;
    static int s_plot_display_count = 70;
    static readonly string[] s_plot_func_names = { "Sin", "Saw" };

    private static void DemoWindowWidgetsPlotting()
    {
        // Plot/Graph widgets are not very good.
        // Consider using a third-party library such as ImPlot: https://github.com/epezent/implot
        // (see others https://github.com/ocornut/imgui/wiki/Useful-Extensions)
        if (ImGui.TreeNode("Plotting"))
        {
            // DEMO MARKER: Widgets/Plotting
            ImGui.Text("Need better plotting and graphing? Consider using ImPlot:");
            ImGui.TextLinkOpenURL("https://github.com/epezent/implot", "https://github.com/epezent/implot");
            ImGui.Separator();

            ImGui.Checkbox("Animate", ref s_plot_animate);

            // Plot as lines and plot as histogram
            ImGui.PlotLines("Frame Times", s_plot_arr);
            ImGui.PlotHistogram("Histogram", s_plot_arr, 0, null, 0.0f, 1.0f, 0, 80.0f);
            //ImGui.SameLine(); HelpMarker("Consider using ImPlot instead!");

            // Fill an array of contiguous float values to plot
            // Tip: If your float aren't contiguous but part of a structure, you can pass a pointer to your first float
            // and the sizeof() of your structure in the "stride" parameter.
            if (!s_plot_animate || s_plot_refresh_time == 0.0)
                s_plot_refresh_time = ImGui.GetTime();
            while (s_plot_refresh_time < ImGui.GetTime()) // Create data at fixed 60 Hz rate for the demo
            {
                s_plot_values[s_plot_values_offset] = MathF.Cos(s_plot_phase);
                s_plot_values_offset = (s_plot_values_offset + 1) % s_plot_values.Length;
                s_plot_phase += 0.10f * s_plot_values_offset;
                s_plot_refresh_time += 1.0f / 60.0f;
            }

            // Plots can display overlay texts
            // (in this example, we will display an average value)
            {
                float average = 0.0f;
                for (int n = 0; n < s_plot_values.Length; n++)
                    average += s_plot_values[n];
                average /= (float)s_plot_values.Length;
                string overlay = $"avg {average:F6}";
                ImGui.PlotLines("Lines", s_plot_values, s_plot_values_offset, overlay, -1.0f, 1.0f, 0, 80.0f);
            }

            // Use functions to generate output
            // FIXME: This is actually VERY awkward because current plot API only pass in indices.
            // We probably want an API passing floats and user provide sample rate/count.
            ImGui.SeparatorText("Functions");
            ImGui.SetNextItemWidth(ImGui.GetFontSize() * 8);
            ImGui.Combo("func", ref s_plot_func_type, s_plot_func_names);
            ImGui.SameLine();
            ImGui.SliderInt("Sample count", ref s_plot_display_count, 1, 400);
            PlotValuesGetter func = (s_plot_func_type == 0)
                ? static i => MathF.Sin(i * 0.1f)
                : static i => (i & 1) != 0 ? 1.0f : -1.0f;
            ImGui.PlotLines("Lines##2", func, s_plot_display_count, 0, null, -1.0f, 1.0f, 0, 80);
            ImGui.PlotHistogram("Histogram##2", func, s_plot_display_count, 0, null, -1.0f, 1.0f, 0, 80);

            ImGui.TreePop();
        }
    }

    // -------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsProgressBars()
    // -------------------------------------------------------------------------

    static float s_progress_accum;
    static float s_progress_dir = 1.0f;

    private static void DemoWindowWidgetsProgressBars()
    {
        if (ImGui.TreeNode("Progress Bars"))
        {
            // DEMO MARKER: Widgets/Progress Bars
            // Animate a simple progress bar
            s_progress_accum += s_progress_dir * 0.4f * Io.DeltaTime;
            if (s_progress_accum >= +1.1f) { s_progress_accum = +1.1f; s_progress_dir *= -1.0f; }
            if (s_progress_accum <= -0.1f) { s_progress_accum = -0.1f; s_progress_dir *= -1.0f; }

            float progress = Math.Clamp(s_progress_accum, 0.0f, 1.0f);

            // Typically we would use ImVec2(-1.0f,0.0f) or ImVec2(-FLT_MIN,0.0f) to use all available width,
            // or ImVec2(width,0.0f) for a specified width. ImVec2(0.0f,0.0f) uses ItemWidth.
            ImGui.ProgressBar(progress, 0.0f, 0.0f);
            ImGui.SameLine(0.0f, Style.ItemInnerSpacing.X);
            ImGui.Text("Progress Bar");

            string buf = $"{(int)(progress * 1753)}/{1753}";
            ImGui.ProgressBar(progress, 0.0f, 0.0f, buf);

            // Pass an animated negative value, e.g. -1.0f * (float)ImGui::GetTime() is the recommended value.
            // Adjust the factor if you want to adjust the animation speed.
            ImGui.ProgressBar(-1.0f * (float)ImGui.GetTime(), 0.0f, 0.0f, "Searching..");
            ImGui.SameLine(0.0f, Style.ItemInnerSpacing.X);
            ImGui.Text("Indeterminate");

            ImGui.TreePop();
        }
    }

    // -------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsQueryingStatuses()
    // -------------------------------------------------------------------------

    static readonly string[] s_query_item_names =
    {
        "Text", "Button", "Button (w/ repeat)", "Checkbox", "SliderFloat", "InputText", "InputTextMultiline", "InputFloat",
        "InputFloat3", "ColorEdit4", "Selectable", "MenuItem", "TreeNode", "TreeNode (w/ double-click)", "Combo", "ListBox"
    };
    static int s_query_item_type = 4;
    static bool s_query_item_disabled;
    static bool s_query_b;
    static readonly float[] s_query_col4f = { 1.0f, 0.5f, 0.0f, 1.0f };
    static readonly byte[] s_query_str = new byte[16];
    static readonly string[] s_query_fruits = { "Apple", "Banana", "Cherry", "Kiwi" };
    static int s_query_combo_current = 1;
    static int s_query_listbox_current = 1;
    static readonly byte[] s_query_unused_buf = new byte[1];
    static bool s_query_embed_all_inside_a_child_window;
    static bool s_query_test_window;

    private static void DemoWindowWidgetsQueryingStatuses()
    {
        if (ImGui.TreeNode("Querying Item Status (Edited/Active/Hovered etc.)"))
        {
            // DEMO MARKER: Widgets/Querying Item Status (Edited,Active,Hovered etc.)
            // Select an item type
            ImGui.Combo("Item Type", ref s_query_item_type, s_query_item_names, s_query_item_names.Length);
            ImGui.SameLine();
            HelpMarker("Testing how various types of items are interacting with the IsItemXXX functions. Note that the bool return value of most ImGui function is generally equivalent to calling ImGui::IsItemHovered().");
            ImGui.Checkbox("Item Disabled", ref s_query_item_disabled);

            // Submit selected items so we can query their status in the code following it.
            bool ret = false;
            if (s_query_item_disabled)
                ImGui.BeginDisabled(true);
            if (s_query_item_type == 0) { ImGui.Text("ITEM: Text"); }                                       // Testing text items with no identifier/interaction
            if (s_query_item_type == 1) { ret = ImGui.Button("ITEM: Button"); }                             // Testing button
            if (s_query_item_type == 2) { ImGui.PushItemFlag(ItemFlags.ButtonRepeat, true); ret = ImGui.Button("ITEM: Button"); ImGui.PopItemFlag(); } // Testing button (with repeater)
            if (s_query_item_type == 3) { ret = ImGui.Checkbox("ITEM: Checkbox", ref s_query_b); }          // Testing checkbox
            if (s_query_item_type == 4) { ret = ImGui.SliderFloat("ITEM: SliderFloat", ref s_query_col4f[0], 0.0f, 1.0f); } // Testing basic item
            if (s_query_item_type == 5) { ret = ImGui.InputText("ITEM: InputText", s_query_str); }          // Testing input text (which handles tabbing)
            if (s_query_item_type == 6) { ret = ImGui.InputTextMultiline("ITEM: InputTextMultiline", s_query_str); } // Testing input text (which uses a child window)
            if (s_query_item_type == 7) { ret = ImGui.InputFloat("ITEM: InputFloat", ref s_query_col4f[0], 1.0f); } // Testing +/- buttons on scalar input
            if (s_query_item_type == 8) { ret = ImGui.InputFloat3("ITEM: InputFloat3", s_query_col4f); }    // Testing multi-component items (IsItemXXX flags are reported merged)
            if (s_query_item_type == 9) { ret = ImGui.ColorEdit4("ITEM: ColorEdit4", s_query_col4f); }      // Testing multi-component items (IsItemXXX flags are reported merged)
            if (s_query_item_type == 10) { ret = ImGui.Selectable("ITEM: Selectable"); }                    // Testing selectable item
            if (s_query_item_type == 11) { ret = ImGui.MenuItem("ITEM: MenuItem"); }                        // Testing menu item (they use ImGuiButtonFlags_PressedOnRelease button policy)
            if (s_query_item_type == 12) { ret = ImGui.TreeNode("ITEM: TreeNode"); if (ret) ImGui.TreePop(); } // Testing tree node
            if (s_query_item_type == 13) { ret = ImGui.TreeNodeEx("ITEM: TreeNode w/ ImGuiTreeNodeFlags_OpenOnDoubleClick", TreeNodeFlags.OpenOnDoubleClick | TreeNodeFlags.NoTreePushOnOpen); } // Testing tree node with ImGuiButtonFlags_PressedOnDoubleClick button policy.
            if (s_query_item_type == 14) { ret = ImGui.Combo("ITEM: Combo", ref s_query_combo_current, s_query_fruits, s_query_fruits.Length); }
            if (s_query_item_type == 15) { ret = ImGui.ListBox("ITEM: ListBox", ref s_query_listbox_current, s_query_fruits, s_query_fruits.Length); }

            bool hovered_delay_none = ImGui.IsItemHovered();
            bool hovered_delay_stationary = ImGui.IsItemHovered(HoveredFlags.Stationary);
            bool hovered_delay_short = ImGui.IsItemHovered(HoveredFlags.DelayShort);
            bool hovered_delay_normal = ImGui.IsItemHovered(HoveredFlags.DelayNormal);
            bool hovered_delay_tooltip = ImGui.IsItemHovered(HoveredFlags.ForTooltip); // = Normal + Stationary

            // Display the values of IsItemHovered() and other common item state functions.
            // Note that the ImGuiHoveredFlags_XXX flags can be combined.
            // Because BulletText is an item itself and that would affect the output of IsItemXXX functions,
            // we query every state in a single call to avoid storing them and to simplify the code.
            static int B(bool v) => v ? 1 : 0;
            ImGui.BulletText(
                $"Return value = {B(ret)}\n" +
                $"IsItemFocused() = {B(ImGui.IsItemFocused())}\n" +
                $"IsItemHovered() = {B(ImGui.IsItemHovered())}\n" +
                $"IsItemHovered(_AllowWhenBlockedByPopup) = {B(ImGui.IsItemHovered(HoveredFlags.AllowWhenBlockedByPopup))}\n" +
                $"IsItemHovered(_AllowWhenBlockedByActiveItem) = {B(ImGui.IsItemHovered(HoveredFlags.AllowWhenBlockedByActiveItem))}\n" +
                $"IsItemHovered(_AllowWhenOverlappedByItem) = {B(ImGui.IsItemHovered(HoveredFlags.AllowWhenOverlappedByItem))}\n" +
                $"IsItemHovered(_AllowWhenOverlappedByWindow) = {B(ImGui.IsItemHovered(HoveredFlags.AllowWhenOverlappedByWindow))}\n" +
                $"IsItemHovered(_AllowWhenDisabled) = {B(ImGui.IsItemHovered(HoveredFlags.AllowWhenDisabled))}\n" +
                $"IsItemHovered(_RectOnly) = {B(ImGui.IsItemHovered(HoveredFlags.RectOnly))}\n" +
                $"IsItemActive() = {B(ImGui.IsItemActive())}\n" +
                $"IsItemEdited() = {B(ImGui.IsItemEdited())}\n" +
                $"IsItemActivated() = {B(ImGui.IsItemActivated())}\n" +
                $"IsItemDeactivated() = {B(ImGui.IsItemDeactivated())}\n" +
                $"IsItemDeactivatedAfterEdit() = {B(ImGui.IsItemDeactivatedAfterEdit())}\n" +
                $"IsItemVisible() = {B(ImGui.IsItemVisible())}\n" +
                $"IsItemClicked() = {B(ImGui.IsItemClicked())}\n" +
                $"IsItemToggledOpen() = {B(ImGui.IsItemToggledOpen())}\n" +
                $"GetItemRectMin() = ({ImGui.GetItemRectMin().X:F1}, {ImGui.GetItemRectMin().Y:F1})\n" +
                $"GetItemRectMax() = ({ImGui.GetItemRectMax().X:F1}, {ImGui.GetItemRectMax().Y:F1})\n" +
                $"GetItemRectSize() = ({ImGui.GetItemRectSize().Width:F1}, {ImGui.GetItemRectSize().Height:F1})");
            ImGui.BulletText(
                "with Hovering Delay or Stationary test:\n" +
                $"IsItemHovered() = {B(hovered_delay_none)}\n" +
                $"IsItemHovered(_Stationary) = {B(hovered_delay_stationary)}\n" +
                $"IsItemHovered(_DelayShort) = {B(hovered_delay_short)}\n" +
                $"IsItemHovered(_DelayNormal) = {B(hovered_delay_normal)}\n" +
                $"IsItemHovered(_Tooltip) = {B(hovered_delay_tooltip)}");

            if (s_query_item_disabled)
                ImGui.EndDisabled();

            ImGui.InputText("unused", s_query_unused_buf, InputTextFlags.ReadOnly);
            ImGui.SameLine();
            HelpMarker("This widget is only here to be able to tab-out of the widgets above and see e.g. Deactivated() status.");

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Querying Window Status (Focused/Hovered etc.)"))
        {
            // DEMO MARKER: Widgets/Querying Window Status (Focused,Hovered etc.)
            ImGui.Checkbox("Embed everything inside a child window for testing _RootWindow flag.", ref s_query_embed_all_inside_a_child_window);
            if (s_query_embed_all_inside_a_child_window)
                ImGui.BeginChild("outer_child", 0, ImGui.GetFontSize() * 20.0f, ChildFlags.Borders);

            static int B(bool v) => v ? 1 : 0;

            // Testing IsWindowFocused() function with its various flags.
            ImGui.BulletText(
                $"IsWindowFocused() = {B(ImGui.IsWindowFocused())}\n" +
                $"IsWindowFocused(_ChildWindows) = {B(ImGui.IsWindowFocused(FocusedFlags.ChildWindows))}\n" +
                $"IsWindowFocused(_ChildWindows|_NoPopupHierarchy) = {B(ImGui.IsWindowFocused(FocusedFlags.ChildWindows | FocusedFlags.NoPopupHierarchy))}\n" +
                $"IsWindowFocused(_ChildWindows|_RootWindow) = {B(ImGui.IsWindowFocused(FocusedFlags.ChildWindows | FocusedFlags.RootWindow))}\n" +
                $"IsWindowFocused(_ChildWindows|_RootWindow|_NoPopupHierarchy) = {B(ImGui.IsWindowFocused(FocusedFlags.ChildWindows | FocusedFlags.RootWindow | FocusedFlags.NoPopupHierarchy))}\n" +
                $"IsWindowFocused(_RootWindow) = {B(ImGui.IsWindowFocused(FocusedFlags.RootWindow))}\n" +
                $"IsWindowFocused(_RootWindow|_NoPopupHierarchy) = {B(ImGui.IsWindowFocused(FocusedFlags.RootWindow | FocusedFlags.NoPopupHierarchy))}\n" +
                $"IsWindowFocused(_AnyWindow) = {B(ImGui.IsWindowFocused(FocusedFlags.AnyWindow))}\n");

            // Testing IsWindowHovered() function with its various flags.
            ImGui.BulletText(
                $"IsWindowHovered() = {B(ImGui.IsWindowHovered())}\n" +
                $"IsWindowHovered(_AllowWhenBlockedByPopup) = {B(ImGui.IsWindowHovered(HoveredFlags.AllowWhenBlockedByPopup))}\n" +
                $"IsWindowHovered(_AllowWhenBlockedByActiveItem) = {B(ImGui.IsWindowHovered(HoveredFlags.AllowWhenBlockedByActiveItem))}\n" +
                $"IsWindowHovered(_ChildWindows) = {B(ImGui.IsWindowHovered(HoveredFlags.ChildWindows))}\n" +
                $"IsWindowHovered(_ChildWindows|_NoPopupHierarchy) = {B(ImGui.IsWindowHovered(HoveredFlags.ChildWindows | HoveredFlags.NoPopupHierarchy))}\n" +
                $"IsWindowHovered(_ChildWindows|_RootWindow) = {B(ImGui.IsWindowHovered(HoveredFlags.ChildWindows | HoveredFlags.RootWindow))}\n" +
                $"IsWindowHovered(_ChildWindows|_RootWindow|_NoPopupHierarchy) = {B(ImGui.IsWindowHovered(HoveredFlags.ChildWindows | HoveredFlags.RootWindow | HoveredFlags.NoPopupHierarchy))}\n" +
                $"IsWindowHovered(_RootWindow) = {B(ImGui.IsWindowHovered(HoveredFlags.RootWindow))}\n" +
                $"IsWindowHovered(_RootWindow|_NoPopupHierarchy) = {B(ImGui.IsWindowHovered(HoveredFlags.RootWindow | HoveredFlags.NoPopupHierarchy))}\n" +
                $"IsWindowHovered(_ChildWindows|_AllowWhenBlockedByPopup) = {B(ImGui.IsWindowHovered(HoveredFlags.ChildWindows | HoveredFlags.AllowWhenBlockedByPopup))}\n" +
                $"IsWindowHovered(_AnyWindow) = {B(ImGui.IsWindowHovered(HoveredFlags.AnyWindow))}\n" +
                $"IsWindowHovered(_Stationary) = {B(ImGui.IsWindowHovered(HoveredFlags.Stationary))}\n");

            ImGui.BeginChild("child", 0, 50, ChildFlags.Borders);
            ImGui.Text("This is another child window for testing the _ChildWindows flag.");
            ImGui.EndChild();
            if (s_query_embed_all_inside_a_child_window)
                ImGui.EndChild();

            // Calling IsItemHovered() after begin returns the hovered status of the title bar.
            // This is useful in particular if you want to create a context menu associated to the title bar of a window.
            ImGui.Checkbox("Hovered/Active tests after Begin() for title bar testing", ref s_query_test_window);
            if (s_query_test_window)
            {
                ImGui.Begin("Title bar Hovered/Active tests", ref s_query_test_window);
                if (ImGui.BeginPopupContextItem()) // <-- This is using IsItemHovered()
                {
                    if (ImGui.MenuItem("Close")) { s_query_test_window = false; }
                    ImGui.EndPopup();
                }
                ImGui.Text(
                    $"IsItemHovered() after begin = {B(ImGui.IsItemHovered())} (== is title bar hovered)\n" +
                    $"IsItemActive() after begin = {B(ImGui.IsItemActive())} (== is window being clicked/moved)\n");
                ImGui.End();
            }

            ImGui.TreePop();
        }
    }

    // -------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsSelectables()
    // -------------------------------------------------------------------------

    static readonly bool[] s_sel_basic_selection = { false, true, false, false, false };
    static readonly bool[] s_sel_overlap_selected = new bool[3];
    static readonly bool[] s_sel_checked = new bool[5];
    static int s_sel_selected_n;
    static readonly bool[] s_sel_table_selected = new bool[10];
    static readonly bool[,] s_sel_grid_selected =
    {
        { true, false, false, false },
        { false, true, false, false },
        { false, false, true, false },
        { false, false, false, true },
    };
    static readonly bool[] s_sel_align_selected = { true, false, true, false, true, false, true, false, true };

    private static void DemoWindowWidgetsSelectables()
    {
        //ImGui.SetNextItemOpen(true, Cond.Once);
        if (ImGui.TreeNode("Selectables"))
        {
            // DEMO MARKER: Widgets/Selectables
            // Selectable() has 2 overloads:
            // - The one taking "bool selected" as a read-only selection information.
            //   When Selectable() has been clicked it returns true and you can alter selection state accordingly.
            // - The one taking "bool* p_selected" as a read-write selection information (convenient in some cases)
            // The earlier is more flexible, as in real application your selection may be stored in many different ways
            // and not necessarily inside a bool value (e.g. in flags within objects, as an external list, etc).
            // DEMO MARKER: Widgets/Selectables/Basic
            if (ImGui.TreeNode("Basic"))
            {
                ImGui.Selectable("1. I am selectable", ref s_sel_basic_selection[0]);
                ImGui.Selectable("2. I am selectable", ref s_sel_basic_selection[1]);
                ImGui.Selectable("3. I am selectable", ref s_sel_basic_selection[2]);
                if (ImGui.Selectable("4. I am double clickable", s_sel_basic_selection[3], SelectableFlags.AllowDoubleClick))
                    if (ImGui.IsMouseDoubleClicked(MouseButton.Left))
                        s_sel_basic_selection[3] = !s_sel_basic_selection[3];
                ImGui.TreePop();
            }

            // DEMO MARKER: Widgets/Selectables/Rendering more items on the same line
            if (ImGui.TreeNode("Multiple items on the same line"))
            {
                // DEMO MARKER: Widgets/Selectables/Multiple items on the same line
                // - Using SetNextItemAllowOverlap()
                // - Using the Selectable() override that takes "bool* p_selected" parameter, the bool value is toggled automatically.
                {
                    ImGui.SetNextItemAllowOverlap(); ImGui.Selectable("main.c", ref s_sel_overlap_selected[0]); ImGui.SameLine(); ImGui.SmallButton("Link 1");
                    ImGui.SetNextItemAllowOverlap(); ImGui.Selectable("hello.cpp", ref s_sel_overlap_selected[1]); ImGui.SameLine(); ImGui.SmallButton("Link 2");
                    ImGui.SetNextItemAllowOverlap(); ImGui.Selectable("hello.h", ref s_sel_overlap_selected[2]); ImGui.SameLine(); ImGui.SmallButton("Link 3");
                }

                // (2)
                // - Using ImGuiSelectableFlags_AllowOverlap is a shortcut for calling SetNextItemAllowOverlap()
                // - No visible label, display contents inside the selectable bounds.
                // - We don't maintain actual selection in this example to keep things simple.
                ImGui.Spacing();
                {
                    float color_marker_w = ImGui.CalcTextSize("x").Width;
                    for (int n = 0; n < 5; n++)
                    {
                        ImGui.PushID(n);
                        ImGui.AlignTextToFramePadding();
                        if (ImGui.Selectable("##selectable", s_sel_selected_n == n, SelectableFlags.AllowOverlap))
                            s_sel_selected_n = n;
                        ImGui.SameLine(0, 0);
                        ImGui.Checkbox("##check", ref s_sel_checked[n]);
                        ImGui.SameLine();
                        ImGui.ColorButton("##color", (n & 1) != 0 ? 1.0f : 0.2f, (n & 2) != 0 ? 1.0f : 0.2f, 0.2f, 1.0f, ColorEditFlags.NoTooltip, color_marker_w, 0);
                        ImGui.SameLine();
                        ImGui.Text("Some label");
                        ImGui.PopID();
                    }
                }

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("In Tables"))
            {
                // DEMO MARKER: Widgets/Selectables/In Tables
                if (ImGui.BeginTable("split1", 3, TableFlags.Resizable | TableFlags.NoSavedSettings | TableFlags.Borders))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        string label = $"Item {i}";
                        ImGui.TableNextColumn();
                        ImGui.Selectable(label, ref s_sel_table_selected[i]); // FIXME-TABLE: Selection overlap
                    }
                    ImGui.EndTable();
                }
                ImGui.Spacing();
                if (ImGui.BeginTable("split2", 3, TableFlags.Resizable | TableFlags.NoSavedSettings | TableFlags.Borders))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        string label = $"Item {i}";
                        ImGui.TableNextRow();
                        ImGui.TableNextColumn();
                        ImGui.Selectable(label, ref s_sel_table_selected[i], SelectableFlags.SpanAllColumns);
                        ImGui.TableNextColumn();
                        ImGui.Text("Some other contents");
                        ImGui.TableNextColumn();
                        ImGui.Text("123456");
                    }
                    ImGui.EndTable();
                }
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Grid"))
            {
                // DEMO MARKER: Widgets/Selectables/Grid
                // Add in a bit of silly fun...
                float time = (float)ImGui.GetTime();
                bool winning_state = true; // If all cells are selected...
                for (int y = 0; y < 4; y++)
                    for (int x = 0; x < 4; x++)
                        if (!s_sel_grid_selected[y, x])
                            winning_state = false;
                if (winning_state)
                    ImGui.PushStyleVar(StyleVar.SelectableTextAlign, 0.5f + 0.5f * MathF.Cos(time * 2.0f), 0.5f + 0.5f * MathF.Sin(time * 3.0f));

                float size = ImGui.CalcTextSize("Sailor").Width;
                for (int y = 0; y < 4; y++)
                    for (int x = 0; x < 4; x++)
                    {
                        if (x > 0)
                            ImGui.SameLine();
                        ImGui.PushID(y * 4 + x);
                        if (ImGui.Selectable("Sailor", s_sel_grid_selected[y, x], SelectableFlags.None, size, size))
                        {
                            // Toggle clicked cell + toggle neighbors
                            s_sel_grid_selected[y, x] ^= true;
                            if (x > 0) { s_sel_grid_selected[y, x - 1] ^= true; }
                            if (x < 3) { s_sel_grid_selected[y, x + 1] ^= true; }
                            if (y > 0) { s_sel_grid_selected[y - 1, x] ^= true; }
                            if (y < 3) { s_sel_grid_selected[y + 1, x] ^= true; }
                        }
                        ImGui.PopID();
                    }

                if (winning_state)
                    ImGui.PopStyleVar();
                ImGui.TreePop();
            }
            if (ImGui.TreeNode("Alignment"))
            {
                // DEMO MARKER: Widgets/Selectables/Alignment
                HelpMarker(
                    "By default, Selectables uses style.SelectableTextAlign but it can be overridden on a per-item " +
                    "basis using PushStyleVar(). You'll probably want to always keep your default situation to " +
                    "left-align otherwise it becomes difficult to layout multiple items on a same line");

                float size = ImGui.CalcTextSize("(1.0,1.0)").Width;
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        float ax = (float)x / 2.0f, ay = (float)y / 2.0f;
                        string name = $"({ax:F1},{ay:F1})";
                        if (x > 0) ImGui.SameLine();
                        ImGui.PushStyleVar(StyleVar.SelectableTextAlign, ax, ay);
                        ImGui.Selectable(name, ref s_sel_align_selected[3 * y + x], SelectableFlags.None, size, size);
                        ImGui.PopStyleVar();
                    }
                }
                ImGui.TreePop();
            }
            ImGui.TreePop();
        }
    }

    // -------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsSelectionAndMultiSelect()
    // -------------------------------------------------------------------------
    // Multi-selection demos
    // Also read: https://github.com/ocornut/imgui/wiki/Multi-Select
    // -------------------------------------------------------------------------

    static readonly string[] ExampleNames =
    {
        "Artichoke", "Arugula", "Asparagus", "Avocado", "Bamboo Shoots", "Bean Sprouts", "Beans", "Beet", "Belgian Endive", "Bell Pepper",
        "Bitter Gourd", "Bok Choy", "Broccoli", "Brussels Sprouts", "Burdock Root", "Cabbage", "Calabash", "Capers", "Carrot", "Cassava",
        "Cauliflower", "Celery", "Celery Root", "Celcuce", "Chayote", "Chinese Broccoli", "Corn", "Cucumber"
    };

    // Extra functions to add deletion support to SelectionBasicStorage.
    // PORT NOTE: upstream derives from ImGuiSelectionBasicStorage. The C# wrapper type is
    // sealed, so this uses composition and forwards the members the demo needs.
    private sealed class ExampleSelectionWithDeletion
    {
        public readonly SelectionBasicStorage Storage = new();

        public int Size => Storage.Size;
        public void ApplyRequests(MultiSelectIO msIo) => Storage.ApplyRequests(msIo);
        public bool Contains(uint id) => Storage.Contains(id);
        public void Clear() => Storage.Clear();
        public void SetItemSelected(uint id, bool selected) => Storage.SetItemSelected(id, selected);
        public uint GetStorageIdFromIndex(int index) => Storage.GetStorageIdFromIndex(index);
        public void SetIndexToStorageIdAdapter(SelectionIndexToStorageId? adapter) => Storage.SetIndexToStorageIdAdapter(adapter);
        public IEnumerable<uint> SelectedItems => Storage.SelectedItems;

        // Find which item should be Focused after deletion.
        // Call _before_ item submission. Return an index in the before-deletion item list, your item loop should call SetKeyboardFocusHere() on it.
        // The subsequent ApplyDeletionPostLoop() code will use it to apply Selection.
        // - We cannot provide this logic in core Dear ImGui because we don't have access to selection data.
        // - We don't actually manipulate the ImVector<> here, only in ApplyDeletionPostLoop(), but using similar API for consistency and flexibility.
        // - Important: Deletion only works if the underlying ImGuiID for your items are stable: aka not depend on their index, but on e.g. item id/ptr.
        // FIXME-MULTISELECT: Doesn't take account of the possibility focus target will be moved during deletion. Need refocus or scroll offset.
        public int ApplyDeletionPreLoop(MultiSelectIO msIo, int itemsCount)
        {
            if (Size == 0)
                return -1;

            // If focused item is not selected...
            int focusedIdx = (int)msIo.NavIdItem;   // Index of currently focused item
            if (msIo.NavIdSelected == false)        // This is merely a shortcut, == Contains(adapter->IndexToStorage(items, focused_idx))
            {
                msIo.RangeSrcReset = true;          // Request to recover RangeSrc from NavId next frame. Would be ok to reset even when NavIdSelected==true, but it would take an extra frame to recover RangeSrc when deleting a selected item.
                return focusedIdx;                  // Request to focus same item after deletion.
            }

            // If focused item is selected: land on first unselected item after focused item.
            for (int idx = focusedIdx + 1; idx < itemsCount; idx++)
                if (!Contains(GetStorageIdFromIndex(idx)))
                    return idx;

            // If focused item is selected: otherwise return last unselected item before focused item.
            for (int idx = Math.Min(focusedIdx, itemsCount) - 1; idx >= 0; idx--)
                if (!Contains(GetStorageIdFromIndex(idx)))
                    return idx;

            return -1;
        }

        // Rewrite item list (delete items) + update selection.
        // - Call after EndMultiSelect()
        // - We cannot provide this logic in core Dear ImGui because we don't have access to your items, nor to selection data.
        public void ApplyDeletionPostLoop<T>(MultiSelectIO msIo, List<T> items, int itemCurrIdxToSelect)
        {
            // Rewrite item list (delete items) + convert old selection index (before deletion) to new selection index (after selection).
            // If NavId was not part of selection, we will stay on same item.
            var newItems = new List<T>(Math.Max(items.Count - Size, 0));
            int itemNextIdxToSelect = -1;
            for (int idx = 0; idx < items.Count; idx++)
            {
                if (!Contains(GetStorageIdFromIndex(idx)))
                    newItems.Add(items[idx]);
                if (itemCurrIdxToSelect == idx)
                    itemNextIdxToSelect = newItems.Count - 1;
            }
            items.Clear();
            items.AddRange(newItems);

            // Update selection
            Clear();
            if (itemNextIdxToSelect != -1 && msIo.NavIdSelected)
                SetItemSelected(GetStorageIdFromIndex(itemNextIdxToSelect), true);
        }
    }

    // Example: Implement dual list box storage and interface
    private sealed class ExampleDualListBox
    {
        public readonly List<uint>[] Items = { new(), new() };                      // ID is index into ExampleNames[]
        public readonly SelectionBasicStorage[] Selections = { new(), new() };      // Store ExampleItemId into selection
        public bool OptKeepSorted = true;

        // PORT NOTE: upstream does Selections[src].Swap(Selections[dst]) followed by
        // Selections[src].Clear(), which nets out to "dst receives src's selection, src is
        // cleared". SelectionBasicStorage.Swap() is not wrapped, so replicate that net effect.
        private void MoveSelectionState(int src, int dst)
        {
            var moved = new List<uint>(Selections[src].SelectedItems);
            Selections[dst].Clear();
            foreach (uint id in moved)
                Selections[dst].SetItemSelected(id, true);
            Selections[src].Clear();
        }

        public void MoveAll(int src, int dst)
        {
            Debug.Assert((src == 0 && dst == 1) || (src == 1 && dst == 0));
            foreach (uint itemId in Items[src])
                Items[dst].Add(itemId);
            Items[src].Clear();
            SortItems(dst);
            MoveSelectionState(src, dst);
        }

        public void MoveSelected(int src, int dst)
        {
            for (int srcN = 0; srcN < Items[src].Count; srcN++)
            {
                uint itemId = Items[src][srcN];
                if (!Selections[src].Contains(itemId))
                    continue;
                Items[src].RemoveAt(srcN); // FIXME-OPT: Could be implemented more optimally (rebuild src items and swap)
                Items[dst].Add(itemId);
                srcN--;
            }
            if (OptKeepSorted)
                SortItems(dst);
            MoveSelectionState(src, dst);
        }

        public void ApplySelectionRequests(MultiSelectIO msIo, int side)
        {
            // In this example we store item id in selection (instead of item index)
            var items = Items[side];
            Selections[side].SetIndexToStorageIdAdapter(idx => items[idx]);
            Selections[side].ApplyRequests(msIo);
        }

        public void SortItems(int n)
        {
            Items[n].Sort(); // Upstream: qsort by value
        }

        public void Show()
        {
            //if (ImGui.Checkbox("Sorted", ref OptKeepSorted) && OptKeepSorted) { SortItems(0); SortItems(1); }
            if (ImGui.BeginTable("split", 3, TableFlags.None))
            {
                ImGui.TableSetupColumn("", TableColumnFlags.WidthStretch);  // Left side
                ImGui.TableSetupColumn("", TableColumnFlags.WidthFixed);    // Buttons
                ImGui.TableSetupColumn("", TableColumnFlags.WidthStretch);  // Right side
                ImGui.TableNextRow();

                int requestMoveSelected = -1;
                int requestMoveAll = -1;
                float childHeight0 = 0.0f;
                for (int side = 0; side < 2; side++)
                {
                    // FIXME-MULTISELECT: Dual List Box: Add context menus
                    // FIXME-NAV: Using ImGuiWindowFlags_NavFlattened exhibit many issues.
                    List<uint> items = Items[side];
                    SelectionBasicStorage selection = Selections[side];

                    ImGui.TableSetColumnIndex((side == 0) ? 0 : 2);
                    ImGui.Text($"{((side == 0) ? "Available" : "Basket")} ({items.Count})");

                    // Submit scrolling range to avoid glitches on moving/deletion
                    float itemsHeight = ImGui.GetTextLineHeightWithSpacing();
                    ImGui.SetNextWindowContentSize(0.0f, items.Count * itemsHeight);

                    bool childVisible;
                    if (side == 0)
                    {
                        // Left child is resizable
                        ImGui.SetNextWindowSizeConstraints(0.0f, ImGui.GetFrameHeightWithSpacing() * 4, float.MaxValue, float.MaxValue);
                        childVisible = ImGui.BeginChild("0", -float.Epsilon, ImGui.GetFontSize() * 20, ChildFlags.FrameStyle | ChildFlags.ResizeY);
                        childHeight0 = ImGui.GetWindowSize().Height;
                    }
                    else
                    {
                        // Right child use same height as left one
                        childVisible = ImGui.BeginChild("1", -float.Epsilon, childHeight0, ChildFlags.FrameStyle);
                    }
                    if (childVisible)
                    {
                        MultiSelectFlags flags = MultiSelectFlags.BoxSelect1d;
                        MultiSelectIO msIo = ImGui.BeginMultiSelect(flags, selection.Size, items.Count);
                        ApplySelectionRequests(msIo, side);

                        for (int itemN = 0; itemN < items.Count; itemN++)
                        {
                            uint itemId = items[itemN];
                            bool itemIsSelected = selection.Contains(itemId);
                            ImGui.SetNextItemSelectionUserData(itemN);
                            ImGui.Selectable(ExampleNames[(int)itemId], itemIsSelected, SelectableFlags.AllowDoubleClick);
                            if (ImGui.IsItemFocused())
                            {
                                // FIXME-MULTISELECT: Dual List Box: Transfer focus
                                if (ImGui.IsKeyPressed(Key.Enter) || ImGui.IsKeyPressed(Key.KeypadEnter))
                                    requestMoveSelected = side;
                                if (ImGui.IsMouseDoubleClicked(MouseButton.Left)) // FIXME-MULTISELECT: Double-click on multi-selection?
                                    requestMoveSelected = side;
                            }
                        }

                        msIo = ImGui.EndMultiSelect();
                        ApplySelectionRequests(msIo, side);
                    }
                    ImGui.EndChild();
                }

                // Buttons columns
                ImGui.TableSetColumnIndex(1);
                ImGui.NewLine();
                //Vec2 button_sz = { ImGui.CalcTextSize(">>").x + ImGui.GetStyle().FramePadding.x * 2.0f, ImGui.GetFrameHeight() + padding.y * 2.0f };
                float buttonSz = ImGui.GetFrameHeight();

                // (Using BeginDisabled()/EndDisabled() works but feels distracting given how it is currently visualized)
                if (ImGui.Button(">>", buttonSz, buttonSz))
                    requestMoveAll = 0;
                if (ImGui.Button(">", buttonSz, buttonSz))
                    requestMoveSelected = 0;
                if (ImGui.Button("<", buttonSz, buttonSz))
                    requestMoveSelected = 1;
                if (ImGui.Button("<<", buttonSz, buttonSz))
                    requestMoveAll = 1;

                // Process requests
                if (requestMoveAll != -1)
                    MoveAll(requestMoveAll, requestMoveAll ^ 1);
                if (requestMoveSelected != -1)
                    MoveSelected(requestMoveSelected, requestMoveSelected ^ 1);

                // FIXME-MULTISELECT: Support action from outside
                /*
                if (OptKeepSorted == false)
                {
                    ImGui.NewLine();
                    if (ImGui.ArrowButton("MoveUp", Dir.Up)) {}
                    if (ImGui.ArrowButton("MoveDown", Dir.Down)) {}
                }
                */

                ImGui.EndTable();
            }
        }
    }

    // The Multi-Select (trees) demo shares the ExampleTreeNode type and creators with the
    // Property Editor example (DemoWindow.ExamplesA.cs), like upstream shares them via
    // ImGuiDemoWindowData.DemoTree. The UID->node map below supports selection lookups.
    static ExampleTreeNode? s_msel_demo_tree;
    static readonly Dictionary<long, ExampleTreeNode> s_msel_tree_node_by_uid = new();

    private static void MselIndexTreeNodes(ExampleTreeNode node)
    {
        s_msel_tree_node_by_uid[node.UID] = node;
        foreach (var child in node.Childs)
            MselIndexTreeNodes(child);
    }

    // --- Multi-Select section state (C++ function-statics) ---

    // "Single-Select"
    static int s_msel_single_selected = -1;
    // "Multi-Select (manual/simplified, without BeginMultiSelect)"
    static readonly bool[] s_msel_manual_selection = new bool[5];
    // "Multi-Select"
    static readonly SelectionBasicStorage s_msel_basic_selection = new();
    // "Multi-Select (with clipper)"
    static readonly SelectionBasicStorage s_msel_clipper_selection = new();
    // "Multi-Select (with deletion)"
    static readonly List<uint> s_msel_del_items = new();
    static readonly ExampleSelectionWithDeletion s_msel_del_selection = new();
    static uint s_msel_del_items_next_id;
    // "Multi-Select (dual list box)"
    static readonly ExampleDualListBox s_msel_dlb = new();
    // "Multi-Select (in a table)"
    static readonly SelectionBasicStorage s_msel_table_selection = new();
    // "Multi-Select (checkboxes)"
    static readonly bool[] s_msel_checkbox_items = new bool[20];
    static int s_msel_checkbox_flags = (int)(MultiSelectFlags.NoAutoSelect | MultiSelectFlags.NoAutoClear | MultiSelectFlags.ClearOnEscape);
    // "Multi-Select (multiple scopes)"
    static readonly SelectionBasicStorage[] s_msel_scopes_selections = { new(), new(), new() };
    static int s_msel_scopes_flags = (int)(MultiSelectFlags.ScopeRect | MultiSelectFlags.ClearOnEscape);// | MultiSelectFlags.ClearOnClickVoid;
    // "Multi-Select (tiled assets browser)"
    // PORT NOTE: upstream stores this on ImGuiDemoWindowData.ShowAppAssetsBrowser; the assets
    // browser example app is not part of this port, so the checkbox only tracks local state.
    static bool s_msel_show_app_assets_browser;
    // "Multi-Select (trees)"
    static readonly SelectionBasicStorage s_msel_trees_selection = new();
    // "Multi-Select (advanced)"
    private enum MultiSelectWidgetType { Selectable, TreeNode }
    static bool s_msel_adv_use_clipper = true;
    static bool s_msel_adv_use_deletion = true;
    static bool s_msel_adv_use_drag_drop = true;
    static bool s_msel_adv_show_in_table;
    static bool s_msel_adv_show_color_button = true;
    static int s_msel_adv_flags = (int)(MultiSelectFlags.ClearOnEscape | MultiSelectFlags.BoxSelect1d);
    static MultiSelectWidgetType s_msel_adv_widget_type = MultiSelectWidgetType.Selectable;
    static readonly List<int> s_msel_adv_items = new();
    static int s_msel_adv_items_next_id;
    static readonly ExampleSelectionWithDeletion s_msel_adv_selection = new();
    static bool s_msel_adv_request_deletion_from_menu; // Queue deletion triggered from context menu


    private static void DemoWindowWidgetsSelectionAndMultiSelect(DemoWindowData data)
    {
        _ = data; // Upstream uses demo_data->ShowAppAssetsBrowser / demo_data->DemoTree; see PORT NOTEs above.

        if (ImGui.TreeNode("Selection State & Multi-Select"))
        {
            // DEMO MARKER: Widgets/Selection State & Multi-Select
            HelpMarker("Selections can be built using Selectable(), TreeNode() or other widgets. Selection state is owned by application code/data.");

            ImGui.BulletText("Wiki page:");
            ImGui.SameLine();
            ImGui.TextLinkOpenURL("imgui/wiki/Multi-Select", "https://github.com/ocornut/imgui/wiki/Multi-Select");

            // Without any fancy API: manage single-selection yourself.
            if (ImGui.TreeNode("Single-Select"))
            {
                // DEMO MARKER: Widgets/Selection State/Single-Select
                for (int n = 0; n < 5; n++)
                {
                    string buf = $"Object {n}";
                    if (ImGui.Selectable(buf, s_msel_single_selected == n))
                        s_msel_single_selected = n;
                }
                ImGui.TreePop();
            }

            // Demonstrate implementation a most-basic form of multi-selection manually
            // This doesn't support the Shift modifier which requires BeginMultiSelect()!
            if (ImGui.TreeNode("Multi-Select (manual/simplified, without BeginMultiSelect)"))
            {
                // DEMO MARKER: Widgets/Selection State/Multi-Select (manual/simplified, without BeginMultiSelect)
                HelpMarker("Hold Ctrl and Click to select multiple items.");
                for (int n = 0; n < 5; n++)
                {
                    string buf = $"Object {n}";
                    if (ImGui.Selectable(buf, s_msel_manual_selection[n]))
                    {
                        if (!Io.KeyCtrl) // Clear selection when Ctrl is not held
                            Array.Clear(s_msel_manual_selection);
                        s_msel_manual_selection[n] ^= true; // Toggle current item
                    }
                }
                ImGui.TreePop();
            }

            // Demonstrate handling proper multi-selection using the BeginMultiSelect/EndMultiSelect API.
            // Shift+Click w/ Ctrl and other standard features are supported.
            // We use the SelectionBasicStorage helper which you may freely reimplement.
            if (ImGui.TreeNode("Multi-Select"))
            {
                // DEMO MARKER: Widgets/Selection State/Multi-Select
                ImGui.Text("Supported features:");
                ImGui.BulletText("Keyboard navigation (arrows, page up/down, home/end, space).");
                ImGui.BulletText("Ctrl modifier to preserve and toggle selection.");
                ImGui.BulletText("Shift modifier for range selection.");
                ImGui.BulletText("Ctrl+A to select all.");
                ImGui.BulletText("Escape to clear selection.");
                ImGui.BulletText("Click and drag to box-select.");
                ImGui.Text("Tip: Use 'Demo->Tools->Debug Log->Selection' to see selection requests as they happen.");

                // Use default selection.Adapter: Pass index to SetNextItemSelectionUserData(), store index in Selection
                const int ITEMS_COUNT = 50;
                SelectionBasicStorage selection = s_msel_basic_selection;
                ImGui.Text($"Selection: {selection.Size}/{ITEMS_COUNT}");

                // The BeginChild() has no purpose for selection logic, other that offering a scrolling region.
                if (ImGui.BeginChild("##Basket", -float.Epsilon, ImGui.GetFontSize() * 20, ChildFlags.FrameStyle | ChildFlags.ResizeY))
                {
                    MultiSelectFlags flags = MultiSelectFlags.ClearOnEscape | MultiSelectFlags.BoxSelect1d;
                    MultiSelectIO msIo = ImGui.BeginMultiSelect(flags, selection.Size, ITEMS_COUNT);
                    selection.ApplyRequests(msIo);

                    for (int n = 0; n < ITEMS_COUNT; n++)
                    {
                        string label = $"Object {n:d5}: {ExampleNames[n % ExampleNames.Length]}";
                        bool itemIsSelected = selection.Contains((uint)n);
                        ImGui.SetNextItemSelectionUserData(n);
                        ImGui.Selectable(label, itemIsSelected);
                    }

                    msIo = ImGui.EndMultiSelect();
                    selection.ApplyRequests(msIo);
                }
                ImGui.EndChild();
                ImGui.TreePop();
            }

            // Demonstrate using the clipper with BeginMultiSelect()/EndMultiSelect()
            if (ImGui.TreeNode("Multi-Select (with clipper)"))
            {
                // DEMO MARKER: Widgets/Selection State/Multi-Select (with clipper)
                // Use default selection.Adapter: Pass index to SetNextItemSelectionUserData(), store index in Selection
                SelectionBasicStorage selection = s_msel_clipper_selection;

                ImGui.Text("Added features:");
                ImGui.BulletText("Using ImGuiListClipper.");

                const int ITEMS_COUNT = 10000;
                ImGui.Text($"Selection: {selection.Size}/{ITEMS_COUNT}");
                if (ImGui.BeginChild("##Basket", -float.Epsilon, ImGui.GetFontSize() * 20, ChildFlags.FrameStyle | ChildFlags.ResizeY))
                {
                    MultiSelectFlags flags = MultiSelectFlags.ClearOnEscape | MultiSelectFlags.BoxSelect1d;
                    MultiSelectIO msIo = ImGui.BeginMultiSelect(flags, selection.Size, ITEMS_COUNT);
                    selection.ApplyRequests(msIo);

                    using var clipper = new ListClipper();
                    clipper.Begin(ITEMS_COUNT);
                    if (msIo.RangeSrcItem != -1)
                        clipper.IncludeItemsByIndex((int)msIo.RangeSrcItem, (int)msIo.RangeSrcItem + 1); // Ensure RangeSrc item is not clipped.
                    while (clipper.Step())
                    {
                        for (int n = clipper.DisplayStart; n < clipper.DisplayEnd; n++)
                        {
                            string label = $"Object {n:d5}: {ExampleNames[n % ExampleNames.Length]}";
                            bool itemIsSelected = selection.Contains((uint)n);
                            ImGui.SetNextItemSelectionUserData(n);
                            ImGui.Selectable(label, itemIsSelected);
                        }
                    }

                    msIo = ImGui.EndMultiSelect();
                    selection.ApplyRequests(msIo);
                }
                ImGui.EndChild();
                ImGui.TreePop();
            }

            // Demonstrate dynamic item list + deletion support using the BeginMultiSelect/EndMultiSelect API.
            // In order to support Deletion without any glitches you need to:
            // - (1) If items are submitted in their own scrolling area, submit contents size SetNextWindowContentSize() ahead of time to prevent one-frame readjustment of scrolling.
            // - (2) Items needs to have persistent ID Stack identifier = ID needs to not depends on their index. PushID(index) = KO. PushID(item_id) = OK. This is in order to focus items reliably after a selection.
            // - (3) BeginXXXX process
            // - (4) Focus process
            // - (5) EndXXXX process
            if (ImGui.TreeNode("Multi-Select (with deletion)"))
            {
                // DEMO MARKER: Widgets/Selection State/Multi-Select (with deletion)
                // Storing items data separately from selection data.
                // (you may decide to store selection data inside your item (aka intrusive storage) if you don't need multiple views over same items)
                // Use a custom selection.Adapter: store item identifier in Selection (instead of index)
                List<uint> items = s_msel_del_items;
                ExampleSelectionWithDeletion selection = s_msel_del_selection;
                selection.SetIndexToStorageIdAdapter(idx => items[idx]); // Index -> ID

                ImGui.Text("Added features:");
                ImGui.BulletText("Dynamic list with Delete key support.");
                ImGui.Text($"Selection size: {selection.Size}/{items.Count}");

                // Initialize default list with 50 items + button to add/remove items.
                if (s_msel_del_items_next_id == 0)
                    for (uint n = 0; n < 50; n++)
                        items.Add(s_msel_del_items_next_id++);
                if (ImGui.SmallButton("Add 20 items")) { for (int n = 0; n < 20; n++) { items.Add(s_msel_del_items_next_id++); } }
                ImGui.SameLine();
                if (ImGui.SmallButton("Remove 20 items")) { for (int n = Math.Min(20, items.Count); n > 0; n--) { selection.SetItemSelected(items[^1], false); items.RemoveAt(items.Count - 1); } }

                // (1) Extra to support deletion: Submit scrolling range to avoid glitches on deletion
                float itemsHeight = ImGui.GetTextLineHeightWithSpacing();
                ImGui.SetNextWindowContentSize(0.0f, items.Count * itemsHeight);

                if (ImGui.BeginChild("##Basket", -float.Epsilon, ImGui.GetFontSize() * 20, ChildFlags.FrameStyle | ChildFlags.ResizeY))
                {
                    MultiSelectFlags flags = MultiSelectFlags.ClearOnEscape | MultiSelectFlags.BoxSelect1d;
                    MultiSelectIO msIo = ImGui.BeginMultiSelect(flags, selection.Size, items.Count);
                    selection.ApplyRequests(msIo);

                    bool wantDelete = ImGui.Shortcut(Key.Delete, InputFlags.Repeat) && (selection.Size > 0);
                    int itemCurrIdxToFocus = wantDelete ? selection.ApplyDeletionPreLoop(msIo, items.Count) : -1;

                    for (int n = 0; n < items.Count; n++)
                    {
                        uint itemId = items[n];
                        string label = $"Object {itemId:d5}: {ExampleNames[(int)(itemId % (uint)ExampleNames.Length)]}";

                        bool itemIsSelected = selection.Contains(itemId);
                        ImGui.SetNextItemSelectionUserData(n);
                        ImGui.Selectable(label, itemIsSelected);
                        if (itemCurrIdxToFocus == n)
                            ImGui.SetKeyboardFocusHere(-1);
                    }

                    // Apply multi-select requests
                    msIo = ImGui.EndMultiSelect();
                    selection.ApplyRequests(msIo);
                    if (wantDelete)
                        selection.ApplyDeletionPostLoop(msIo, items, itemCurrIdxToFocus);
                }
                ImGui.EndChild();
                ImGui.TreePop();
            }

            // Implement a Dual List Box (#6648)
            if (ImGui.TreeNode("Multi-Select (dual list box)"))
            {
                // DEMO MARKER: Widgets/Selection State/Multi-Select (dual list box)
                // Init default state
                if (s_msel_dlb.Items[0].Count == 0 && s_msel_dlb.Items[1].Count == 0)
                    for (int itemId = 0; itemId < ExampleNames.Length; itemId++)
                        s_msel_dlb.Items[0].Add((uint)itemId);

                // Show
                s_msel_dlb.Show();

                ImGui.TreePop();
            }

            // Demonstrate using the clipper with BeginMultiSelect()/EndMultiSelect()
            if (ImGui.TreeNode("Multi-Select (in a table)"))
            {
                // DEMO MARKER: Widgets/Selection State/Multi-Select (in a table)
                SelectionBasicStorage selection = s_msel_table_selection;

                const int ITEMS_COUNT = 10000;
                ImGui.Text($"Selection: {selection.Size}/{ITEMS_COUNT}");
                if (ImGui.BeginTable("##Basket", 2, TableFlags.ScrollY | TableFlags.RowBg | TableFlags.BordersOuter, 0.0f, ImGui.GetFontSize() * 20))
                {
                    ImGui.TableSetupColumn("Object");
                    ImGui.TableSetupColumn("Action");
                    ImGui.TableSetupScrollFreeze(0, 1);
                    ImGui.TableHeadersRow();

                    MultiSelectFlags flags = MultiSelectFlags.ClearOnEscape | MultiSelectFlags.BoxSelect1d;
                    MultiSelectIO msIo = ImGui.BeginMultiSelect(flags, selection.Size, ITEMS_COUNT);
                    selection.ApplyRequests(msIo);

                    using var clipper = new ListClipper();
                    clipper.Begin(ITEMS_COUNT);
                    if (msIo.RangeSrcItem != -1)
                        clipper.IncludeItemsByIndex((int)msIo.RangeSrcItem, (int)msIo.RangeSrcItem + 1); // Ensure RangeSrc item is not clipped.
                    while (clipper.Step())
                    {
                        for (int n = clipper.DisplayStart; n < clipper.DisplayEnd; n++)
                        {
                            ImGui.TableNextRow();
                            ImGui.TableNextColumn();
                            ImGui.PushID(n);
                            string label = $"Object {n:d5}: {ExampleNames[n % ExampleNames.Length]}";
                            bool itemIsSelected = selection.Contains((uint)n);
                            ImGui.SetNextItemSelectionUserData(n);
                            ImGui.Selectable(label, itemIsSelected, SelectableFlags.SpanAllColumns | SelectableFlags.AllowOverlap);
                            ImGui.TableNextColumn();
                            ImGui.SmallButton("hello");
                            ImGui.PopID();
                        }
                    }

                    msIo = ImGui.EndMultiSelect();
                    selection.ApplyRequests(msIo);
                    ImGui.EndTable();
                }
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Multi-Select (checkboxes)"))
            {
                // DEMO MARKER: Widgets/Selection State/Multi-Select (checkboxes)
                ImGui.Text("In a list of checkboxes (not selectable):");
                ImGui.BulletText("Using _NoAutoSelect + _NoAutoClear flags.");
                ImGui.BulletText("Shift+Click to check multiple boxes.");
                ImGui.BulletText("Shift+Keyboard to copy current value to other boxes.");

                // If you have an array of checkboxes, you may want to use NoAutoSelect + NoAutoClear and the SelectionExternalStorage helper.
                ImGui.CheckboxFlags("ImGuiMultiSelectFlags_NoAutoSelect", ref s_msel_checkbox_flags, (int)MultiSelectFlags.NoAutoSelect);
                ImGui.CheckboxFlags("ImGuiMultiSelectFlags_NoAutoClear", ref s_msel_checkbox_flags, (int)MultiSelectFlags.NoAutoClear);
                ImGui.CheckboxFlags("ImGuiMultiSelectFlags_BoxSelect2d", ref s_msel_checkbox_flags, (int)MultiSelectFlags.BoxSelect2d); // Cannot use MultiSelectFlags.BoxSelect1d as checkboxes are varying width.

                if (ImGui.BeginChild("##Basket", -float.Epsilon, ImGui.GetFontSize() * 20, ChildFlags.Borders | ChildFlags.ResizeY))
                {
                    MultiSelectIO msIo = ImGui.BeginMultiSelect((MultiSelectFlags)s_msel_checkbox_flags, -1, s_msel_checkbox_items.Length);
                    using var storageWrapper = new SelectionExternalStorage();
                    storageWrapper.SetItemSelectedAdapter((n, selected) => s_msel_checkbox_items[n] = selected);
                    storageWrapper.ApplyRequests(msIo);
                    for (int n = 0; n < 20; n++)
                    {
                        string label = $"Item {n}";
                        ImGui.SetNextItemSelectionUserData(n);
                        ImGui.Checkbox(label, ref s_msel_checkbox_items[n]);
                    }
                    msIo = ImGui.EndMultiSelect();
                    storageWrapper.ApplyRequests(msIo);
                }
                ImGui.EndChild();

                ImGui.TreePop();
            }

            // Demonstrate individual selection scopes in same window
            if (ImGui.TreeNode("Multi-Select (multiple scopes)"))
            {
                // DEMO MARKER: Widgets/Selection State/Multi-Select (multiple scopes)
                // Use default select: Pass index to SetNextItemSelectionUserData(), store index in Selection
                const int SCOPES_COUNT = 3;
                const int ITEMS_COUNT = 8; // Per scope

                // Use MultiSelectFlags.ScopeRect to not affect other selections in same window.
                if (ImGui.CheckboxFlags("ImGuiMultiSelectFlags_ScopeWindow", ref s_msel_scopes_flags, (int)MultiSelectFlags.ScopeWindow) && (s_msel_scopes_flags & (int)MultiSelectFlags.ScopeWindow) != 0)
                    s_msel_scopes_flags &= ~(int)MultiSelectFlags.ScopeRect;
                if (ImGui.CheckboxFlags("ImGuiMultiSelectFlags_ScopeRect", ref s_msel_scopes_flags, (int)MultiSelectFlags.ScopeRect) && (s_msel_scopes_flags & (int)MultiSelectFlags.ScopeRect) != 0)
                    s_msel_scopes_flags &= ~(int)MultiSelectFlags.ScopeWindow;
                ImGui.CheckboxFlags("ImGuiMultiSelectFlags_ClearOnClickVoid", ref s_msel_scopes_flags, (int)MultiSelectFlags.ClearOnClickVoid);
                ImGui.CheckboxFlags("ImGuiMultiSelectFlags_BoxSelect1d", ref s_msel_scopes_flags, (int)MultiSelectFlags.BoxSelect1d);

                for (int selectionScopeN = 0; selectionScopeN < SCOPES_COUNT; selectionScopeN++)
                {
                    ImGui.PushID(selectionScopeN);
                    SelectionBasicStorage selection = s_msel_scopes_selections[selectionScopeN];
                    MultiSelectIO msIo = ImGui.BeginMultiSelect((MultiSelectFlags)s_msel_scopes_flags, selection.Size, ITEMS_COUNT);
                    selection.ApplyRequests(msIo);

                    ImGui.SeparatorText("Selection scope");
                    ImGui.Text($"Selection size: {selection.Size}/{ITEMS_COUNT}");

                    for (int n = 0; n < ITEMS_COUNT; n++)
                    {
                        string label = $"Object {n:d5}: {ExampleNames[n % ExampleNames.Length]}";
                        bool itemIsSelected = selection.Contains((uint)n);
                        ImGui.SetNextItemSelectionUserData(n);
                        ImGui.Selectable(label, itemIsSelected);
                    }

                    // Apply multi-select requests
                    msIo = ImGui.EndMultiSelect();
                    selection.ApplyRequests(msIo);
                    ImGui.PopID();
                }
                ImGui.TreePop();
            }

            // See ShowExampleAppAssetsBrowser()
            if (ImGui.TreeNode("Multi-Select (tiled assets browser)"))
            {
                ImGui.Checkbox("Assets Browser", ref s_msel_show_app_assets_browser);
                ImGui.Text("(also access from 'Examples->Assets Browser' in menu)");
                ImGui.TreePop();
            }

            // Demonstrate supporting multiple-selection in a tree.
            // - We don't use linear indices for selection user data, but our node identity directly!
            //   (upstream stores the ExampleTreeNode* pointer; here we store the node UID and map it back)
            //   This showcase how SetNextItemSelectionUserData() never assume indices!
            // - The difficulty here is to "interpolate" from RangeSrcItem to RangeDstItem in the SetAll/SetRange request.
            //   We want this interpolation to match what the user sees: in visible order, skipping closed nodes.
            //   This is implemented by our TreeGetNextNodeInVisibleOrder() user-space helper.
            // - Important: In a real codebase aiming to implement full-featured selectable tree with custom filtering, you
            //   are more likely to build an array mapping sequential indices to visible tree nodes, since your
            //   filtering/search + clipping process will benefit from it. Having this will make this interpolation much easier.
            // - Consider this a prototype: we are working toward simplifying some of it.
            if (ImGui.TreeNode("Multi-Select (trees)"))
            {
                // DEMO MARKER: Widgets/Selection State/Multi-Select (trees)
                HelpMarker(
                    "This is rather advanced and experimental. If you are getting started with multi-select, " +
                    "please don't start by looking at how to use it for a tree!\n\n" +
                    "Future versions will try to simplify and formalize some of this.");

                SelectionBasicStorage selection = s_msel_trees_selection;
                if (s_msel_demo_tree == null) // Create tree once
                {
                    s_msel_demo_tree = ExampleTree_CreateDemoTree();
                    MselIndexTreeNodes(s_msel_demo_tree);
                }
                ImGui.Text($"Selection size: {selection.Size}");

                if (ImGui.BeginChild("##Tree", -float.Epsilon, ImGui.GetFontSize() * 20, ChildFlags.FrameStyle | ChildFlags.ResizeY))
                {
                    ExampleTreeNode tree = s_msel_demo_tree;
                    MultiSelectFlags msFlags = MultiSelectFlags.ClearOnEscape | MultiSelectFlags.BoxSelect2d;
                    MultiSelectIO msIo = ImGui.BeginMultiSelect(msFlags, selection.Size, -1);
                    TreeApplySelectionRequests(msIo, tree, selection);
                    foreach (ExampleTreeNode node in tree.Childs)
                        TreeDrawNode(node, selection);
                    msIo = ImGui.EndMultiSelect();
                    TreeApplySelectionRequests(msIo, tree, selection);
                }
                ImGui.EndChild();

                ImGui.TreePop();
            }

            // Advanced demonstration of BeginMultiSelect()
            // - Showcase clipping.
            // - Showcase deletion.
            // - Showcase basic drag and drop.
            // - Showcase TreeNode variant (note that tree node don't expand in the demo: supporting expanding tree nodes + clipping a separate thing).
            // - Showcase using inside a table.
            //ImGui.SetNextItemOpen(true, Cond.Once);
            if (ImGui.TreeNode("Multi-Select (advanced)"))
            {
                // DEMO MARKER: Widgets/Selection State/Multi-Select (advanced)
                if (ImGui.TreeNode("Options"))
                {
                    if (ImGui.RadioButton("Selectables", s_msel_adv_widget_type == MultiSelectWidgetType.Selectable)) { s_msel_adv_widget_type = MultiSelectWidgetType.Selectable; }
                    ImGui.SameLine();
                    if (ImGui.RadioButton("Tree nodes", s_msel_adv_widget_type == MultiSelectWidgetType.TreeNode)) { s_msel_adv_widget_type = MultiSelectWidgetType.TreeNode; }
                    ImGui.SameLine();
                    HelpMarker("TreeNode() is technically supported but... using this correctly is more complicated (you need some sort of linear/random access to your tree, which is suited to advanced trees setups already implementing filters and clipper. We will work toward simplifying and demoing this.\n\nFor now the tree demo is actually a little bit meaningless because it is an empty tree with only root nodes.");
                    ImGui.Checkbox("Enable clipper", ref s_msel_adv_use_clipper);
                    ImGui.Checkbox("Enable deletion", ref s_msel_adv_use_deletion);
                    ImGui.Checkbox("Enable drag & drop", ref s_msel_adv_use_drag_drop);
                    ImGui.Checkbox("Show in a table", ref s_msel_adv_show_in_table);
                    ImGui.Checkbox("Show color button", ref s_msel_adv_show_color_button);
                    ImGui.CheckboxFlags("ImGuiMultiSelectFlags_SingleSelect", ref s_msel_adv_flags, (int)MultiSelectFlags.SingleSelect);
                    ImGui.CheckboxFlags("ImGuiMultiSelectFlags_NoSelectAll", ref s_msel_adv_flags, (int)MultiSelectFlags.NoSelectAll);
                    ImGui.CheckboxFlags("ImGuiMultiSelectFlags_NoRangeSelect", ref s_msel_adv_flags, (int)MultiSelectFlags.NoRangeSelect);
                    ImGui.CheckboxFlags("ImGuiMultiSelectFlags_NoAutoSelect", ref s_msel_adv_flags, (int)MultiSelectFlags.NoAutoSelect);
                    ImGui.CheckboxFlags("ImGuiMultiSelectFlags_NoAutoClear", ref s_msel_adv_flags, (int)MultiSelectFlags.NoAutoClear);
                    ImGui.CheckboxFlags("ImGuiMultiSelectFlags_NoAutoClearOnReselect", ref s_msel_adv_flags, (int)MultiSelectFlags.NoAutoClearOnReselect);
                    ImGui.CheckboxFlags("ImGuiMultiSelectFlags_NoSelectOnRightClick", ref s_msel_adv_flags, (int)MultiSelectFlags.NoSelectOnRightClick);
                    ImGui.CheckboxFlags("ImGuiMultiSelectFlags_BoxSelect1d", ref s_msel_adv_flags, (int)MultiSelectFlags.BoxSelect1d);
                    ImGui.CheckboxFlags("ImGuiMultiSelectFlags_BoxSelect2d", ref s_msel_adv_flags, (int)MultiSelectFlags.BoxSelect2d);
                    ImGui.CheckboxFlags("ImGuiMultiSelectFlags_BoxSelectNoScroll", ref s_msel_adv_flags, (int)MultiSelectFlags.BoxSelectNoScroll);
                    ImGui.CheckboxFlags("ImGuiMultiSelectFlags_ClearOnEscape", ref s_msel_adv_flags, (int)MultiSelectFlags.ClearOnEscape);
                    ImGui.CheckboxFlags("ImGuiMultiSelectFlags_ClearOnClickVoid", ref s_msel_adv_flags, (int)MultiSelectFlags.ClearOnClickVoid);
                    if (ImGui.CheckboxFlags("ImGuiMultiSelectFlags_ScopeWindow", ref s_msel_adv_flags, (int)MultiSelectFlags.ScopeWindow) && (s_msel_adv_flags & (int)MultiSelectFlags.ScopeWindow) != 0)
                        s_msel_adv_flags &= ~(int)MultiSelectFlags.ScopeRect;
                    if (ImGui.CheckboxFlags("ImGuiMultiSelectFlags_ScopeRect", ref s_msel_adv_flags, (int)MultiSelectFlags.ScopeRect) && (s_msel_adv_flags & (int)MultiSelectFlags.ScopeRect) != 0)
                        s_msel_adv_flags &= ~(int)MultiSelectFlags.ScopeWindow;
                    const int SelectOnMask = (int)(MultiSelectFlags.SelectOnAuto | MultiSelectFlags.SelectOnClickAlways | MultiSelectFlags.SelectOnClickRelease); // ImGuiMultiSelectFlags_SelectOnMask_
                    if (ImGui.CheckboxFlags("ImGuiMultiSelectFlags_SelectOnAuto", ref s_msel_adv_flags, (int)MultiSelectFlags.SelectOnAuto))
                        s_msel_adv_flags &= ~(SelectOnMask ^ (int)MultiSelectFlags.SelectOnAuto);
                    ImGui.SameLine(); HelpMarker("Apply selection on mouse down when clicking on unselected item, on mouse up when clicking on selected item. (Default)");
                    if (ImGui.CheckboxFlags("ImGuiMultiSelectFlags_SelectOnClickAlways", ref s_msel_adv_flags, (int)MultiSelectFlags.SelectOnClickAlways))
                        s_msel_adv_flags &= ~(SelectOnMask ^ (int)MultiSelectFlags.SelectOnClickAlways);
                    ImGui.SameLine(); HelpMarker("Prevents Drag and Drop from being used on multi-selection, but allows e.g. BoxSelect to always reselect even when clicking inside an existing selection. (Excel style behavior)");
                    if (ImGui.CheckboxFlags("ImGuiMultiSelectFlags_SelectOnClickRelease", ref s_msel_adv_flags, (int)MultiSelectFlags.SelectOnClickRelease))
                        s_msel_adv_flags &= ~(SelectOnMask ^ (int)MultiSelectFlags.SelectOnClickRelease);
                    ImGui.SameLine(); HelpMarker("Allow dragging an unselected item without altering selection.");
                    ImGui.TreePop();
                }

                // Initialize default list with 1000 items.
                // Use default selection.Adapter: Pass index to SetNextItemSelectionUserData(), store index in Selection
                List<int> items = s_msel_adv_items;
                if (s_msel_adv_items_next_id == 0) { for (int n = 0; n < 1000; n++) { items.Add(s_msel_adv_items_next_id++); } }
                ExampleSelectionWithDeletion selection = s_msel_adv_selection;

                ImGui.Text($"Selection size: {selection.Size}/{items.Count}");

                MultiSelectWidgetType widgetType = s_msel_adv_widget_type;
                bool useClipper = s_msel_adv_use_clipper;
                bool useDeletion = s_msel_adv_use_deletion;
                bool useDragDrop = s_msel_adv_use_drag_drop;
                bool showInTable = s_msel_adv_show_in_table;
                bool showColorButton = s_msel_adv_show_color_button;

                float itemsHeight = (widgetType == MultiSelectWidgetType.TreeNode) ? ImGui.GetTextLineHeight() : ImGui.GetTextLineHeightWithSpacing();
                ImGui.SetNextWindowContentSize(0.0f, items.Count * itemsHeight);
                if (ImGui.BeginChild("##Basket", -float.Epsilon, ImGui.GetFontSize() * 20, ChildFlags.FrameStyle | ChildFlags.ResizeY))
                {
                    float colorButtonSz = ImGui.GetFontSize();
                    if (widgetType == MultiSelectWidgetType.TreeNode)
                        ImGui.PushStyleVarY(StyleVar.ItemSpacing, 0.0f);

                    MultiSelectIO msIo = ImGui.BeginMultiSelect((MultiSelectFlags)s_msel_adv_flags, selection.Size, items.Count);
                    selection.ApplyRequests(msIo);

                    bool wantDelete = (ImGui.Shortcut(Key.Delete, InputFlags.Repeat) && (selection.Size > 0)) || s_msel_adv_request_deletion_from_menu;
                    int itemCurrIdxToFocus = wantDelete ? selection.ApplyDeletionPreLoop(msIo, items.Count) : -1;
                    s_msel_adv_request_deletion_from_menu = false;

                    if (showInTable)
                    {
                        if (widgetType == MultiSelectWidgetType.TreeNode)
                            ImGui.PushStyleVar(StyleVar.CellPadding, 0.0f, 0.0f);
                        ImGui.BeginTable("##Split", 2, TableFlags.Resizable | TableFlags.NoSavedSettings | TableFlags.NoPadOuterX);
                        ImGui.TableSetupColumn("", TableColumnFlags.WidthStretch, 0.70f);
                        ImGui.TableSetupColumn("", TableColumnFlags.WidthStretch, 0.30f);
                        //ImGui.PushStyleVarY(StyleVar.ItemSpacing, 0.0f);
                    }

                    using var clipper = new ListClipper();
                    if (useClipper)
                    {
                        clipper.Begin(items.Count);
                        if (itemCurrIdxToFocus != -1)
                            clipper.IncludeItemsByIndex(itemCurrIdxToFocus, itemCurrIdxToFocus + 1); // Ensure focused item is not clipped.
                        if (msIo.RangeSrcItem != -1)
                            clipper.IncludeItemsByIndex((int)msIo.RangeSrcItem, (int)msIo.RangeSrcItem + 1); // Ensure RangeSrc item is not clipped.
                    }

                    while (!useClipper || clipper.Step())
                    {
                        int itemBegin = useClipper ? clipper.DisplayStart : 0;
                        int itemEnd = useClipper ? clipper.DisplayEnd : items.Count;
                        for (int n = itemBegin; n < itemEnd; n++)
                        {
                            if (showInTable)
                                ImGui.TableNextColumn();

                            int itemId = items[n];
                            string itemCategory = ExampleNames[itemId % ExampleNames.Length];
                            string label = $"Object {itemId:d5}: {itemCategory}";

                            // IMPORTANT: for deletion refocus to work we need object ID to be stable,
                            // aka not depend on their index in the list. Here we use our persistent item_id
                            // instead of index to build a unique ID that will persist.
                            // (If we used PushID(index) instead, focus wouldn't be restored correctly after deletion).
                            ImGui.PushID(itemId);

                            // Emit a color button, to test that Shift+LeftArrow landing on an item that is not part
                            // of the selection scope doesn't erroneously alter our selection.
                            if (showColorButton)
                            {
                                uint dummyCol = ((uint)n * 0xC250B74Bu) | 0xFF000000u; // | IM_COL32_A_MASK
                                ImGui.ColorButton("##",
                                    (dummyCol & 0xFF) / 255.0f,
                                    ((dummyCol >> 8) & 0xFF) / 255.0f,
                                    ((dummyCol >> 16) & 0xFF) / 255.0f,
                                    ((dummyCol >> 24) & 0xFF) / 255.0f,
                                    ColorEditFlags.NoTooltip, colorButtonSz, colorButtonSz);
                                ImGui.SameLine();
                            }

                            // Submit item
                            bool itemIsSelected = selection.Contains((uint)n);
                            bool itemIsOpen = false;
                            ImGui.SetNextItemSelectionUserData(n);
                            if (widgetType == MultiSelectWidgetType.Selectable)
                            {
                                ImGui.Selectable(label, itemIsSelected, SelectableFlags.None);
                            }
                            else if (widgetType == MultiSelectWidgetType.TreeNode)
                            {
                                TreeNodeFlags treeNodeFlags = TreeNodeFlags.SpanAvailWidth | TreeNodeFlags.OpenOnArrow | TreeNodeFlags.OpenOnDoubleClick;
                                if (itemIsSelected)
                                    treeNodeFlags |= TreeNodeFlags.Selected;
                                itemIsOpen = ImGui.TreeNodeEx(label, treeNodeFlags);
                            }

                            // Focus (for after deletion)
                            if (itemCurrIdxToFocus == n)
                                ImGui.SetKeyboardFocusHere(-1);

                            // Drag and Drop
                            if (useDragDrop && ImGui.BeginDragDropSource())
                            {
                                // Create payload with full selection OR single unselected item.
                                // (the later is only possible when using ImGuiMultiSelectFlags_SelectOnClickRelease)
                                if (!ImGui.GetDragDropPayload().IsValid)
                                {
                                    var payloadItems = new List<int>();
                                    if (!itemIsSelected)
                                        payloadItems.Add(itemId);
                                    else
                                        foreach (uint id in selection.SelectedItems)
                                            payloadItems.Add((int)id);
                                    ImGui.SetDragDropPayload("MULTISELECT_DEMO_ITEMS", MemoryMarshal.AsBytes<int>(payloadItems.ToArray()));
                                }

                                // Display payload content in tooltip
                                DragDropPayload payload = ImGui.GetDragDropPayload();
                                ReadOnlySpan<int> payloadInts = MemoryMarshal.Cast<byte, int>(payload.Data);
                                int payloadCount = payloadInts.Length;
                                if (payloadCount == 1)
                                    ImGui.Text($"Object {payloadInts[0]:d5}: {ExampleNames[payloadInts[0] % ExampleNames.Length]}");
                                else
                                    ImGui.Text($"Dragging {payloadCount} objects");

                                ImGui.EndDragDropSource();
                            }

                            if (widgetType == MultiSelectWidgetType.TreeNode && itemIsOpen)
                                ImGui.TreePop();

                            // Right-click: context menu
                            if (ImGui.BeginPopupContextItem())
                            {
                                ImGui.BeginDisabled(!useDeletion || selection.Size == 0);
                                string deleteLabel = $"Delete {selection.Size} item(s)###DeleteSelected";
                                if (ImGui.Selectable(deleteLabel))
                                    s_msel_adv_request_deletion_from_menu = true;
                                ImGui.EndDisabled();
                                ImGui.Selectable("Close");
                                ImGui.EndPopup();
                            }

                            // Demo content within a table
                            if (showInTable)
                            {
                                ImGui.TableNextColumn();
                                ImGui.SetNextItemWidth(-float.Epsilon);
                                ImGui.PushStyleVar(StyleVar.FramePadding, 0, 0);
                                byte[] categoryBuf = Encoding.UTF8.GetBytes(itemCategory + "\0");
                                ImGui.InputText("##NoLabel", categoryBuf, InputTextFlags.ReadOnly);
                                ImGui.PopStyleVar();
                            }

                            ImGui.PopID();
                        }
                        if (!useClipper)
                            break;
                    }

                    if (showInTable)
                    {
                        ImGui.EndTable();
                        if (widgetType == MultiSelectWidgetType.TreeNode)
                            ImGui.PopStyleVar();
                    }

                    // Apply multi-select requests
                    msIo = ImGui.EndMultiSelect();
                    selection.ApplyRequests(msIo);
                    if (wantDelete)
                        selection.ApplyDeletionPostLoop(msIo, items, itemCurrIdxToFocus);

                    if (widgetType == MultiSelectWidgetType.TreeNode)
                        ImGui.PopStyleVar();
                }
                ImGui.EndChild();
                ImGui.TreePop();
            }
            ImGui.TreePop();
        }
    }

    // --- Multi-Select (trees) helpers (upstream: local struct ExampleTreeFuncs) ---

    private static void TreeDrawNode(ExampleTreeNode node, SelectionBasicStorage selection)
    {
        TreeNodeFlags treeNodeFlags = TreeNodeFlags.SpanAvailWidth | TreeNodeFlags.OpenOnArrow | TreeNodeFlags.OpenOnDoubleClick;
        treeNodeFlags |= TreeNodeFlags.NavLeftJumpsToParent; // Enable pressing left to jump to parent
        if (node.Childs.Count == 0)
            treeNodeFlags |= TreeNodeFlags.Bullet | TreeNodeFlags.Leaf;
        if (selection.Contains((uint)node.UID))
            treeNodeFlags |= TreeNodeFlags.Selected;

        // Using SetNextItemStorageID() to specify storage id, so we can easily peek into
        // the storage holding open/close stage, using our TreeNodeGetOpen/TreeNodeSetOpen() functions.
        ImGui.SetNextItemSelectionUserData(node.UID); // (upstream stores the node pointer; we store the UID and map back)
        ImGui.SetNextItemStorageID((uint)node.UID);
        if (ImGui.TreeNodeEx(node.Name, treeNodeFlags))
        {
            foreach (ExampleTreeNode child in node.Childs)
                TreeDrawNode(child, selection);
            ImGui.TreePop();
        }
        else if (ImGui.IsItemToggledOpen())
        {
            TreeCloseAndUnselectChildNodes(node, selection);
        }
    }

    // When closing a node: 1) close and unselect all child nodes, 2) select parent if any child was selected.
    // FIXME: This is currently handled by user logic but I'm hoping to eventually provide tree node
    // features to do this automatically, e.g. a ImGuiTreeNodeFlags_AutoCloseChildNodes etc.
    private static int TreeCloseAndUnselectChildNodes(ExampleTreeNode node, SelectionBasicStorage selection, int depth = 0)
    {
        // Recursive close (the test for depth == 0 is because we call this on a node that was just closed!)
        int unselectedCount = selection.Contains((uint)node.UID) ? 1 : 0;
        if (depth == 0 || ImGui.TreeNodeGetOpen((uint)node.UID))
        {
            foreach (ExampleTreeNode child in node.Childs)
                unselectedCount += TreeCloseAndUnselectChildNodes(child, selection, depth + 1);
            // PORT GAP: ImGui.TreeNodeSetOpen is not wrapped; upstream calls
            // ImGui::TreeNodeSetOpen(node->UID, false) here to force-close child nodes.
            // Without it, children keep their previous open state when the parent is re-opened.
        }

        // Select root node if any of its child was selected, otherwise unselect
        selection.SetItemSelected((uint)node.UID, (depth == 0 && unselectedCount > 0));
        return unselectedCount;
    }

    // Apply multi-selection requests
    private static void TreeApplySelectionRequests(MultiSelectIO msIo, ExampleTreeNode tree, SelectionBasicStorage selection)
    {
        for (int reqN = 0; reqN < msIo.RequestsCount; reqN++)
        {
            SelectionRequest req = msIo.GetRequest(reqN);
            if (req.Type == SelectionRequestType.SetAll)
            {
                if (req.Selected)
                    TreeSetAllInOpenNodes(tree, selection, req.Selected);
                else
                    selection.Clear();
            }
            else if (req.Type == SelectionRequestType.SetRange)
            {
                ExampleTreeNode? firstNode = s_msel_tree_node_by_uid[req.RangeFirstItem];
                ExampleTreeNode lastNode = s_msel_tree_node_by_uid[req.RangeLastItem];
                for (ExampleTreeNode? node = firstNode; node != null; node = TreeGetNextNodeInVisibleOrder(node, lastNode))
                    selection.SetItemSelected((uint)node.UID, req.Selected);
            }
        }
    }

    private static void TreeSetAllInOpenNodes(ExampleTreeNode node, SelectionBasicStorage selection, bool selected)
    {
        if (node.Parent != null) // Root node isn't visible nor selectable in our scheme
            selection.SetItemSelected((uint)node.UID, selected);
        if (node.Parent == null || ImGui.TreeNodeGetOpen((uint)node.UID))
            foreach (ExampleTreeNode child in node.Childs)
                TreeSetAllInOpenNodes(child, selection, selected);
    }

    // Interpolate in *user-visible order* AND only *over opened nodes*.
    // If you have a sequential mapping tables (e.g. generated after a filter/search pass) this would be simpler.
    // Here the tricks are that:
    // - we store/maintain ExampleTreeNode::IndexInParent which allows implementing a linear iterator easily, without searches, without recursion.
    //   this could be replaced by a search in parent, aka 'int index_in_parent = curr_node->Parent->Childs.find_index(curr_node)'
    //   which would only be called when crossing from child to a parent, aka not too much.
    // - we call SetNextItemStorageID() before our TreeNode() calls with an ID which doesn't relate to UI stack,
    //   making it easier to call TreeNodeGetOpen()/TreeNodeSetOpen() from any location.
    private static ExampleTreeNode? TreeGetNextNodeInVisibleOrder(ExampleTreeNode currNode, ExampleTreeNode lastNode)
    {
        // Reached last node
        if (currNode == lastNode)
            return null;

        // Recurse into childs. Query storage to tell if the node is open.
        if (currNode.Childs.Count > 0 && ImGui.TreeNodeGetOpen((uint)currNode.UID))
            return currNode.Childs[0];

        // Next sibling, then into our own parent
        while (currNode.Parent != null)
        {
            if (currNode.IndexInParent + 1 < currNode.Parent.Childs.Count)
                return currNode.Parent.Childs[currNode.IndexInParent + 1];
            currNode = currNode.Parent;
        }
        return null;
    }
}
