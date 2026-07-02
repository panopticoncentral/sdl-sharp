// C# port of imgui_demo.cpp's DemoWindowTables() and DemoWindowColumns()
// (upstream lines ~5544-7818): the "Tables & Columns" section of the demo,
// including all table demos (basic, borders, sizing, scrolling, sorting,
// advanced, tree view, ...) and the legacy Columns API demos.
//
// Upstream reference: imgui_demo.cpp

using SdlSharp.ImGui;

namespace ImGuiDemo;

internal static unsafe partial class DemoWindow
{
    // -------------------------------------------------------------------------
    // SECTION: Table demo helpers (MyItem + sorting infrastructure)
    // -------------------------------------------------------------------------

    // Dummy data structure that we use for the Table demo.
    // We are passing our own identifier to TableSetupColumn() to facilitate identifying columns in the sorting code.
    // This identifier will be passed down into ImGuiTableSortSpec::ColumnUserID.
    // But it is possible to omit the user id parameter of TableSetupColumn() and just use the column index instead! (ImGuiTableSortSpec::ColumnIndex)
    // If you don't use sorting, you will generally never care about giving column an ID!
    private enum MyItemColumnId : uint
    {
        Id,
        Name,
        Action,
        Quantity,
        Description,
    }

    private sealed class MyItem
    {
        public int ID;
        public string Name = "";
        public int Quantity;

        // In the C++ demo, qsort() cannot receive user data so the sort specs are stored in a
        // static for the compare function to access. We keep the same structure here for a 1:1 port,
        // even though a C# closure could capture the specs directly.
        private static TableSortSpecs s_current_sort_specs;

        public static void SortWithSortSpecs(TableSortSpecs sortSpecs, MyItem[] items)
        {
            s_current_sort_specs = sortSpecs; // Store in variable accessible by the sort function.
            if (items.Length > 1)
                Array.Sort(items, CompareWithSortSpecs);
            s_current_sort_specs = default;
        }

        // Compare function used by Array.Sort() (qsort() in the C++ demo)
        private static int CompareWithSortSpecs(MyItem a, MyItem b)
        {
            for (int n = 0; n < s_current_sort_specs.SpecsCount; n++)
            {
                // Here we identify columns using the ColumnUserID value that we ourselves passed to TableSetupColumn()
                // We could also choose to identify columns based on their index (sort_spec->ColumnIndex), which is simpler!
                TableColumnSortSpecs sortSpec = s_current_sort_specs.GetSpec(n);
                int delta = 0;
                switch ((MyItemColumnId)sortSpec.ColumnUserId)
                {
                    case MyItemColumnId.Id: delta = a.ID - b.ID; break;
                    case MyItemColumnId.Name: delta = string.CompareOrdinal(a.Name, b.Name); break;
                    case MyItemColumnId.Quantity: delta = a.Quantity - b.Quantity; break;
                    case MyItemColumnId.Description: delta = string.CompareOrdinal(a.Name, b.Name); break;
                    default: break;
                }
                if (delta > 0)
                    return (sortSpec.SortDirection == SortDirection.Ascending) ? +1 : -1;
                if (delta < 0)
                    return (sortSpec.SortDirection == SortDirection.Ascending) ? -1 : +1;
            }

            // qsort() is instable so always return a way to differentiate items.
            // Your own compare function may want to avoid fallback on implicit sort specs.
            // e.g. a Name compare if it wasn't already part of the sort specs.
            return a.ID - b.ID;
        }
    }

    // Enum helpers over the wrapper's int-based CheckboxFlags, local to the tables demos.
    private static bool CheckboxFlags(string label, ref TableFlags flags, TableFlags flagsValue)
    {
        int v = (int)flags;
        bool ret = ImGui.CheckboxFlags(label, ref v, (int)flagsValue);
        flags = (TableFlags)v;
        return ret;
    }

    private static bool CheckboxFlags(string label, ref TableColumnFlags flags, TableColumnFlags flagsValue)
    {
        int v = (int)flags;
        bool ret = ImGui.CheckboxFlags(label, ref v, (int)flagsValue);
        flags = (TableColumnFlags)v;
        return ret;
    }

    // Make the UI compact because there are so many fields
    private static void PushStyleCompact()
    {
        ImGui.PushStyleVarY(StyleVar.FramePadding, (float)(int)(Style.FramePadding.Y * 0.60f));
        ImGui.PushStyleVarY(StyleVar.ItemSpacing, (float)(int)(Style.ItemSpacing.Y * 0.60f));
    }

    private static void PopStyleCompact()
    {
        ImGui.PopStyleVar(2);
    }

    private const TableFlags TableSizingMask = TableFlags.SizingFixedFit | TableFlags.SizingFixedSame | TableFlags.SizingStretchProp | TableFlags.SizingStretchSame;
    private const TableColumnFlags TableColumnWidthMask = TableColumnFlags.WidthStretch | TableColumnFlags.WidthFixed;

    private static readonly (TableFlags Value, string Name, string Tooltip)[] s_table_sizing_policies =
    [
        (TableFlags.None,               "Default",                            "Use default sizing policy:\n- ImGuiTableFlags_SizingFixedFit if ScrollX is on or if host window has ImGuiWindowFlags_AlwaysAutoResize.\n- ImGuiTableFlags_SizingStretchSame otherwise."),
        (TableFlags.SizingFixedFit,     "ImGuiTableFlags_SizingFixedFit",     "Columns default to _WidthFixed (if resizable) or _WidthAuto (if not resizable), matching contents width."),
        (TableFlags.SizingFixedSame,    "ImGuiTableFlags_SizingFixedSame",    "Columns are all the same width, matching the maximum contents width.\nImplicitly disable ImGuiTableFlags_Resizable and enable ImGuiTableFlags_NoKeepColumnsVisible."),
        (TableFlags.SizingStretchProp,  "ImGuiTableFlags_SizingStretchProp",  "Columns default to _WidthStretch with weights proportional to their widths."),
        (TableFlags.SizingStretchSame,  "ImGuiTableFlags_SizingStretchSame",  "Columns default to _WidthStretch with same weights."),
    ];

    // Show a combo box with a choice of sizing policies
    private static void EditTableSizingFlags(ref TableFlags flags)
    {
        var policies = s_table_sizing_policies;
        int idx;
        for (idx = 0; idx < policies.Length; idx++)
            if (policies[idx].Value == (flags & TableSizingMask))
                break;
        string previewText = (idx < policies.Length) ? policies[idx].Name[(idx > 0 ? "ImGuiTableFlags".Length : 0)..] : "";
        if (ImGui.BeginCombo("Sizing Policy", previewText))
        {
            for (int n = 0; n < policies.Length; n++)
                if (ImGui.Selectable(policies[n].Name, idx == n))
                    flags = (flags & ~TableSizingMask) | policies[n].Value;
            ImGui.EndCombo();
        }
        ImGui.SameLine();
        ImGui.TextDisabled("(?)");
        if (ImGui.BeginItemTooltip())
        {
            ImGui.PushTextWrapPos(ImGui.GetFontSize() * 50.0f);
            for (int m = 0; m < policies.Length; m++)
            {
                ImGui.Separator();
                ImGui.Text($"{policies[m].Name}:");
                ImGui.Separator();
                ImGui.SetCursorPosX(ImGui.GetCursorPosX() + Style.IndentSpacing * 0.5f);
                ImGui.TextUnformatted(policies[m].Tooltip);
            }
            ImGui.PopTextWrapPos();
            ImGui.EndTooltip();
        }
    }

    private static void EditTableColumnsFlags(ref TableColumnFlags flags)
    {
        CheckboxFlags("_Disabled", ref flags, TableColumnFlags.Disabled); ImGui.SameLine(); HelpMarker("Master disable flag (also hide from context menu)");
        CheckboxFlags("_DefaultHide", ref flags, TableColumnFlags.DefaultHide);
        CheckboxFlags("_DefaultSort", ref flags, TableColumnFlags.DefaultSort);
        if (CheckboxFlags("_WidthStretch", ref flags, TableColumnFlags.WidthStretch))
            flags &= ~(TableColumnWidthMask ^ TableColumnFlags.WidthStretch);
        if (CheckboxFlags("_WidthFixed", ref flags, TableColumnFlags.WidthFixed))
            flags &= ~(TableColumnWidthMask ^ TableColumnFlags.WidthFixed);
        CheckboxFlags("_NoResize", ref flags, TableColumnFlags.NoResize);
        CheckboxFlags("_NoReorder", ref flags, TableColumnFlags.NoReorder);
        CheckboxFlags("_NoHide", ref flags, TableColumnFlags.NoHide);
        CheckboxFlags("_NoClip", ref flags, TableColumnFlags.NoClip);
        CheckboxFlags("_NoSort", ref flags, TableColumnFlags.NoSort);
        CheckboxFlags("_NoSortAscending", ref flags, TableColumnFlags.NoSortAscending);
        CheckboxFlags("_NoSortDescending", ref flags, TableColumnFlags.NoSortDescending);
        CheckboxFlags("_NoHeaderLabel", ref flags, TableColumnFlags.NoHeaderLabel);
        CheckboxFlags("_NoHeaderWidth", ref flags, TableColumnFlags.NoHeaderWidth);
        CheckboxFlags("_PreferSortAscending", ref flags, TableColumnFlags.PreferSortAscending);
        CheckboxFlags("_PreferSortDescending", ref flags, TableColumnFlags.PreferSortDescending);
        CheckboxFlags("_IndentEnable", ref flags, TableColumnFlags.IndentEnable); ImGui.SameLine(); HelpMarker("Default for column 0");
        CheckboxFlags("_IndentDisable", ref flags, TableColumnFlags.IndentDisable); ImGui.SameLine(); HelpMarker("Default for column >0");
        CheckboxFlags("_AngledHeader", ref flags, TableColumnFlags.AngledHeader);
    }

    private static void ShowTableColumnsStatusFlags(TableColumnFlags flags)
    {
        CheckboxFlags("_IsEnabled", ref flags, TableColumnFlags.IsEnabled);
        CheckboxFlags("_IsVisible", ref flags, TableColumnFlags.IsVisible);
        CheckboxFlags("_IsSorted", ref flags, TableColumnFlags.IsSorted);
        CheckboxFlags("_IsHovered", ref flags, TableColumnFlags.IsHovered);
    }

    // -------------------------------------------------------------------------
    // SECTION: DemoWindowTables() static state (C++ function-static locals)
    // -------------------------------------------------------------------------

    // Options
    static bool s_tables_disable_indent;

    // Borders, background
    static TableFlags s_tables_borders_flags = TableFlags.Borders | TableFlags.RowBg;
    static bool s_tables_borders_display_headers;
    static int s_tables_borders_contents_type; // CT_Text

    // Resizable, stretch
    static TableFlags s_tables_resize_stretch_flags = TableFlags.SizingStretchSame | TableFlags.Resizable | TableFlags.BordersOuter | TableFlags.BordersV | TableFlags.ContextMenuInBody;

    // Resizable, fixed
    static TableFlags s_tables_resize_fixed_flags = TableFlags.SizingFixedFit | TableFlags.Resizable | TableFlags.BordersOuter | TableFlags.BordersV | TableFlags.ContextMenuInBody;

    // Resizable, mixed
    static TableFlags s_tables_resize_mixed_flags = TableFlags.SizingFixedFit | TableFlags.RowBg | TableFlags.Borders | TableFlags.Resizable | TableFlags.Reorderable | TableFlags.Hideable;

    // Reorderable, hideable, with headers
    static TableFlags s_tables_reorder_flags = TableFlags.Resizable | TableFlags.Reorderable | TableFlags.Hideable | TableFlags.BordersOuter | TableFlags.BordersV;

    // Padding
    static TableFlags s_tables_padding_flags1 = TableFlags.BordersV;
    static bool s_tables_padding_show_headers;
    static TableFlags s_tables_padding_flags2 = TableFlags.Borders | TableFlags.RowBg;
    static readonly float[] s_tables_padding_cell_padding = [0.0f, 0.0f];
    static bool s_tables_padding_show_widget_frame_bg = true;
    static readonly string[] s_tables_padding_text_bufs = new string[3 * 5]; // Mini text storage for 3x5 cells
    static bool s_tables_padding_init = true;

    // Sizing policies
    static TableFlags s_tables_sizing_flags1 = TableFlags.BordersV | TableFlags.BordersOuterH | TableFlags.RowBg | TableFlags.ContextMenuInBody;
    static readonly TableFlags[] s_tables_sizing_policy_flags = [TableFlags.SizingFixedFit, TableFlags.SizingFixedSame, TableFlags.SizingStretchProp, TableFlags.SizingStretchSame];
    static TableFlags s_tables_sizing_adv_flags = TableFlags.ScrollY | TableFlags.Borders | TableFlags.RowBg | TableFlags.Resizable;
    static int s_tables_sizing_adv_contents_type; // CT_ShowWidth
    static int s_tables_sizing_adv_column_count = 3;
    static string s_tables_sizing_adv_text_buf = "";
    static readonly string[] s_tables_sizing_adv_contents_names = ["Show width", "Short Text", "Long Text", "Button", "Fill Button", "InputText"];

    // Vertical scrolling, with clipping
    static TableFlags s_tables_vscroll_flags = TableFlags.ScrollY | TableFlags.RowBg | TableFlags.BordersOuter | TableFlags.BordersV | TableFlags.Resizable | TableFlags.Reorderable | TableFlags.Hideable;

    // Horizontal scrolling
    static TableFlags s_tables_hscroll_flags = TableFlags.ScrollX | TableFlags.ScrollY | TableFlags.RowBg | TableFlags.BordersOuter | TableFlags.BordersV | TableFlags.Resizable | TableFlags.Reorderable | TableFlags.Hideable;
    static int s_tables_hscroll_freeze_cols = 1;
    static int s_tables_hscroll_freeze_rows = 1;
    static TableFlags s_tables_hscroll_flags2 = TableFlags.SizingStretchSame | TableFlags.ScrollX | TableFlags.ScrollY | TableFlags.BordersOuter | TableFlags.RowBg | TableFlags.ContextMenuInBody;
    static float s_tables_hscroll_inner_width = 1000.0f;

    // Columns flags
    static readonly TableColumnFlags[] s_tables_colflags_column_flags = [TableColumnFlags.DefaultSort, TableColumnFlags.None, TableColumnFlags.DefaultHide];
    static readonly TableColumnFlags[] s_tables_colflags_column_flags_out = [0, 0, 0]; // Output from TableGetColumnFlags()

    // Columns widths
    static TableFlags s_tables_widths_flags1 = TableFlags.Borders | TableFlags.NoBordersInBodyUntilResize;
    static TableFlags s_tables_widths_flags2 = TableFlags.None;

    // Outer size
    static TableFlags s_tables_outer_flags = TableFlags.Borders | TableFlags.Resizable | TableFlags.ContextMenuInBody | TableFlags.RowBg | TableFlags.SizingFixedFit | TableFlags.NoHostExtendX;

    // Background color
    static TableFlags s_tables_bg_flags = TableFlags.RowBg;
    static int s_tables_bg_row_bg_type = 1;
    static int s_tables_bg_row_bg_target = 1;
    static int s_tables_bg_cell_bg_type = 1;
    static readonly string[] s_tables_bg_row_bg_type_names = ["None", "Red", "Gradient"];
    static readonly string[] s_tables_bg_row_bg_target_names = ["RowBg0", "RowBg1"];
    static readonly string[] s_tables_bg_cell_bg_type_names = ["None", "Blue"];

    // Tree view
    static TableFlags s_tables_treeview_flags = TableFlags.BordersV | TableFlags.BordersOuterH | TableFlags.Resizable | TableFlags.RowBg | TableFlags.NoBordersInBody;
    static int s_tables_treeview_tree_node_flags_base = (int)(TreeNodeFlags.SpanAllColumns | TreeNodeFlags.DefaultOpen | TreeNodeFlags.DrawLinesFull);

    // Simple storage to output a dummy file-system.
    private struct MyTreeNode
    {
        public string Name;
        public string Type;
        public int Size;
        public int ChildIdx;
        public int ChildCount;

        public MyTreeNode(string name, string type, int size, int childIdx, int childCount)
        {
            Name = name; Type = type; Size = size; ChildIdx = childIdx; ChildCount = childCount;
        }
    }

    static readonly MyTreeNode[] s_tables_treeview_nodes =
    [
        new("Root with Long Name",           "Folder",       -1,       1, 3),  // 0
        new("Music",                         "Folder",       -1,       4, 2),  // 1
        new("Textures",                      "Folder",       -1,       6, 3),  // 2
        new("desktop.ini",                   "System file",  1024,    -1, -1), // 3
        new("File1_a.wav",                   "Audio file",   123000,  -1, -1), // 4
        new("File1_b.wav",                   "Audio file",   456000,  -1, -1), // 5
        new("Image001.png",                  "Image file",   203128,  -1, -1), // 6
        new("Copy of Image001.png",          "Image file",   203256,  -1, -1), // 7
        new("Copy of Image001 (Final2).png", "Image file",   203512,  -1, -1), // 8
    ];

    // Item width
    static float s_tables_item_width_dummy_f;

    // Custom headers
    static readonly bool[] s_tables_custom_headers_column_selected = new bool[3];

    // Angled headers
    static TableFlags s_tables_angled_flags = TableFlags.SizingFixedFit | TableFlags.ScrollX | TableFlags.ScrollY | TableFlags.BordersOuter | TableFlags.BordersInnerH | TableFlags.Hideable | TableFlags.Resizable | TableFlags.Reorderable | TableFlags.HighlightHoveredColumn;
    static TableColumnFlags s_tables_angled_column_flags = TableColumnFlags.AngledHeader | TableColumnFlags.WidthFixed;
    static readonly bool[] s_tables_angled_bools = new bool[14 * 12]; // Dummy storage selection storage
    static int s_tables_angled_frozen_cols = 1;
    static int s_tables_angled_frozen_rows = 2;
    static readonly string[] s_tables_angled_column_names = ["Track", "cabasa", "ride", "smash", "tom-hi", "tom-mid", "tom-low", "hihat-o", "hihat-c", "snare-s", "snare-c", "clap", "rim", "kick"];

    // Context menus
    static TableFlags s_tables_ctx_flags1 = TableFlags.Resizable | TableFlags.Reorderable | TableFlags.Hideable | TableFlags.Borders | TableFlags.ContextMenuInBody;

    // Synced instances
    static TableFlags s_tables_synced_flags = TableFlags.Resizable | TableFlags.Reorderable | TableFlags.Hideable | TableFlags.Borders | TableFlags.SizingFixedFit | TableFlags.NoSavedSettings;

    // Sorting
    static readonly string[] s_tables_template_items_names =
    [
        "Banana", "Apple", "Cherry", "Watermelon", "Grapefruit", "Strawberry", "Mango",
        "Kiwi", "Orange", "Pineapple", "Blueberry", "Plum", "Coconut", "Pear", "Apricot"
    ];
    static MyItem[] s_tables_sorting_items = [];
    static TableFlags s_tables_sorting_flags =
        TableFlags.Resizable | TableFlags.Reorderable | TableFlags.Hideable | TableFlags.Sortable | TableFlags.SortMulti
        | TableFlags.RowBg | TableFlags.BordersOuter | TableFlags.BordersV | TableFlags.NoBordersInBody
        | TableFlags.ScrollY;

    // Advanced
    static TableFlags s_tables_adv_flags =
        TableFlags.Resizable | TableFlags.Reorderable | TableFlags.Hideable
        | TableFlags.Sortable | TableFlags.SortMulti
        | TableFlags.RowBg | TableFlags.Borders | TableFlags.NoBordersInBody
        | TableFlags.ScrollX | TableFlags.ScrollY
        | TableFlags.SizingFixedFit;
    static TableColumnFlags s_tables_adv_columns_base_flags = TableColumnFlags.None;
    static int s_tables_adv_contents_type = 5; // CT_SelectableSpanRow
    static readonly string[] s_tables_adv_contents_type_names = ["Text", "Button", "SmallButton", "FillButton", "Selectable", "Selectable (span row)"];
    static int s_tables_adv_freeze_cols = 1;
    static int s_tables_adv_freeze_rows = 1;
    static int s_tables_adv_items_count = 15 * 2; // IM_COUNTOF(template_items_names) * 2
    static readonly float[] s_tables_adv_outer_size_value = [0.0f, 0.0f]; // Y initialized on first use (needs TEXT_BASE_HEIGHT)
    static bool s_tables_adv_outer_size_initialized;
    static float s_tables_adv_row_min_height; // Auto
    static float s_tables_adv_inner_width_with_scroll; // Auto-extend
    static bool s_tables_adv_outer_size_enabled = true;
    static bool s_tables_adv_show_headers = true;
    static bool s_tables_adv_show_wrapped_text;
    static MyItem[] s_tables_adv_items = [];
    static readonly List<int> s_tables_adv_selection = [];
    static bool s_tables_adv_items_need_sort;
    static bool s_tables_adv_show_debug_details;

    // -------------------------------------------------------------------------
    // SECTION: DemoWindowTables()
    // -------------------------------------------------------------------------

    private static void DemoWindowTables()
    {
        //ImGui.SetNextItemOpen(true, Cond.Once);
        if (!ImGui.CollapsingHeader("Tables & Columns"))
            return;

        // Using those as a base value to create width/height that are factor of the size of our font
        float TEXT_BASE_WIDTH = ImGui.CalcTextSize("A").Width;
        float TEXT_BASE_HEIGHT = ImGui.GetTextLineHeightWithSpacing();

        ImGui.PushID("Tables");

        int open_action = -1;
        if (ImGui.Button("Expand all"))
            open_action = 1;
        ImGui.SameLine();
        if (ImGui.Button("Collapse all"))
            open_action = 0;
        ImGui.SameLine();

        // Options
        ImGui.Checkbox("Disable tree indentation", ref s_tables_disable_indent);
        ImGui.SameLine();
        HelpMarker("Disable the indenting of tree nodes so demo tables can use the full window width.");
        ImGui.Separator();
        if (s_tables_disable_indent)
            ImGui.PushStyleVar(StyleVar.IndentSpacing, 0.0f);

        // About Styling of tables
        // Most settings are configured on a per-table basis via the flags passed to BeginTable() and TableSetupColumns APIs.
        // There are however a few settings that a shared and part of the ImGuiStyle structure:
        //   style.CellPadding                          // Padding within each cell
        //   style.Colors[ImGuiCol_TableHeaderBg]       // Table header background
        //   style.Colors[ImGuiCol_TableBorderStrong]   // Table outer and header borders
        //   style.Colors[ImGuiCol_TableBorderLight]    // Table inner borders
        //   style.Colors[ImGuiCol_TableRowBg]          // Table row background when ImGuiTableFlags_RowBg is enabled (even rows)
        //   style.Colors[ImGuiCol_TableRowBgAlt]       // Table row background when ImGuiTableFlags_RowBg is enabled (odds rows)

        // Demos
        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Basic"))
        {
            // DEMO MARKER: Tables/Basic
            // Here we will showcase three different ways to output a table.
            // They are very simple variations of a same thing!

            // [Method 1] Using TableNextRow() to create a new row, and TableSetColumnIndex() to select the column.
            // In many situations, this is the most flexible and easy to use pattern.
            HelpMarker("Using TableNextRow() + calling TableSetColumnIndex() _before_ each cell, in a loop.");
            if (ImGui.BeginTable("table1", 3))
            {
                for (int row = 0; row < 4; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 3; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        ImGui.Text($"Row {row} Column {column}");
                    }
                }
                ImGui.EndTable();
            }

            // [Method 2] Using TableNextColumn() called multiple times, instead of using a for loop + TableSetColumnIndex().
            // This is generally more convenient when you have code manually submitting the contents of each column.
            HelpMarker("Using TableNextRow() + calling TableNextColumn() _before_ each cell, manually.");
            if (ImGui.BeginTable("table2", 3))
            {
                for (int row = 0; row < 4; row++)
                {
                    ImGui.TableNextRow();
                    ImGui.TableNextColumn();
                    ImGui.Text($"Row {row}");
                    ImGui.TableNextColumn();
                    ImGui.Text("Some contents");
                    ImGui.TableNextColumn();
                    ImGui.Text("123.456");
                }
                ImGui.EndTable();
            }

            // [Method 3] We call TableNextColumn() _before_ each cell. We never call TableNextRow(),
            // as TableNextColumn() will automatically wrap around and create new rows as needed.
            // This is generally more convenient when your cells all contains the same type of data.
            HelpMarker(
                "Only using TableNextColumn(), which tends to be convenient for tables where every cell contains "
                + "the same type of contents.\n This is also more similar to the old NextColumn() function of the "
                + "Columns API, and provided to facilitate the Columns->Tables API transition.");
            if (ImGui.BeginTable("table3", 3))
            {
                for (int item = 0; item < 14; item++)
                {
                    ImGui.TableNextColumn();
                    ImGui.Text($"Item {item}");
                }
                ImGui.EndTable();
            }

            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Borders, background"))
        {
            // DEMO MARKER: Tables/Borders, background
            // Expose a few Borders related flags interactively
            const int CT_Text = 0, CT_FillButton = 1;

            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_RowBg", ref s_tables_borders_flags, TableFlags.RowBg);
            CheckboxFlags("ImGuiTableFlags_Borders", ref s_tables_borders_flags, TableFlags.Borders);
            ImGui.SameLine(); HelpMarker("ImGuiTableFlags_Borders\n = ImGuiTableFlags_BordersInnerV\n | ImGuiTableFlags_BordersOuterV\n | ImGuiTableFlags_BordersInnerH\n | ImGuiTableFlags_BordersOuterH");
            ImGui.Indent();

            CheckboxFlags("ImGuiTableFlags_BordersH", ref s_tables_borders_flags, TableFlags.BordersH);
            ImGui.Indent();
            CheckboxFlags("ImGuiTableFlags_BordersOuterH", ref s_tables_borders_flags, TableFlags.BordersOuterH);
            CheckboxFlags("ImGuiTableFlags_BordersInnerH", ref s_tables_borders_flags, TableFlags.BordersInnerH);
            ImGui.Unindent();

            CheckboxFlags("ImGuiTableFlags_BordersV", ref s_tables_borders_flags, TableFlags.BordersV);
            ImGui.Indent();
            CheckboxFlags("ImGuiTableFlags_BordersOuterV", ref s_tables_borders_flags, TableFlags.BordersOuterV);
            CheckboxFlags("ImGuiTableFlags_BordersInnerV", ref s_tables_borders_flags, TableFlags.BordersInnerV);
            ImGui.Unindent();

            CheckboxFlags("ImGuiTableFlags_BordersOuter", ref s_tables_borders_flags, TableFlags.BordersOuter);
            CheckboxFlags("ImGuiTableFlags_BordersInner", ref s_tables_borders_flags, TableFlags.BordersInner);
            ImGui.Unindent();

            ImGui.AlignTextToFramePadding(); ImGui.Text("Cell contents:");
            ImGui.SameLine(); ImGui.RadioButton("Text", ref s_tables_borders_contents_type, CT_Text);
            ImGui.SameLine(); ImGui.RadioButton("FillButton", ref s_tables_borders_contents_type, CT_FillButton);
            ImGui.Checkbox("Display headers", ref s_tables_borders_display_headers);
            CheckboxFlags("ImGuiTableFlags_NoBordersInBody", ref s_tables_borders_flags, TableFlags.NoBordersInBody); ImGui.SameLine(); HelpMarker("Disable vertical borders in columns Body (borders will always appear in Headers)");
            PopStyleCompact();

            if (ImGui.BeginTable("table1", 3, s_tables_borders_flags))
            {
                // Display headers so we can inspect their interaction with borders
                // (Headers are not the main purpose of this section of the demo, so we are not elaborating on them now. See other sections for details)
                if (s_tables_borders_display_headers)
                {
                    ImGui.TableSetupColumn("One");
                    ImGui.TableSetupColumn("Two");
                    ImGui.TableSetupColumn("Three");
                    ImGui.TableHeadersRow();
                }

                for (int row = 0; row < 5; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 3; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        string buf = $"Hello {column},{row}";
                        if (s_tables_borders_contents_type == CT_Text)
                            ImGui.TextUnformatted(buf);
                        else if (s_tables_borders_contents_type == CT_FillButton)
                            ImGui.Button(buf, -float.Epsilon, 0.0f);
                    }
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Resizable, stretch"))
        {
            // DEMO MARKER: Tables/Resizable, stretch
            // By default, if we don't enable ScrollX the sizing policy for each column is "Stretch"
            // All columns maintain a sizing weight, and they will occupy all available width.
            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_Resizable", ref s_tables_resize_stretch_flags, TableFlags.Resizable);
            CheckboxFlags("ImGuiTableFlags_BordersV", ref s_tables_resize_stretch_flags, TableFlags.BordersV);
            ImGui.SameLine(); HelpMarker(
                "Using the _Resizable flag automatically enables the _BordersInnerV flag as well, "
                + "this is why the resize borders are still showing when unchecking this.");
            PopStyleCompact();

            if (ImGui.BeginTable("table1", 3, s_tables_resize_stretch_flags))
            {
                for (int row = 0; row < 5; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 3; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        ImGui.Text($"Hello {column},{row}");
                    }
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Resizable, fixed"))
        {
            // DEMO MARKER: Tables/Resizable, fixed
            // Here we use ImGuiTableFlags_SizingFixedFit (even though _ScrollX is not set)
            // So columns will adopt the "Fixed" policy and will maintain a fixed width regardless of the whole available width (unless table is small)
            // If there is not enough available width to fit all columns, they will however be resized down.
            // FIXME-TABLE: Providing a stretch-on-init would make sense especially for tables which don't have saved settings
            HelpMarker(
                "Using _Resizable + _SizingFixedFit flags.\n"
                + "Fixed-width columns generally makes more sense if you want to use horizontal scrolling.\n\n"
                + "Double-click a column border to auto-fit the column to its contents.");
            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_NoHostExtendX", ref s_tables_resize_fixed_flags, TableFlags.NoHostExtendX);
            PopStyleCompact();

            if (ImGui.BeginTable("table1", 3, s_tables_resize_fixed_flags))
            {
                for (int row = 0; row < 5; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 3; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        ImGui.Text($"Hello {column},{row}");
                    }
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Resizable, mixed"))
        {
            // DEMO MARKER: Tables/Resizable, mixed
            HelpMarker(
                "Using TableSetupColumn() to alter resizing policy on a per-column basis.\n\n"
                + "When combining Fixed and Stretch columns, generally you only want one, maybe two trailing columns to use _WidthStretch.");

            if (ImGui.BeginTable("table1", 3, s_tables_resize_mixed_flags))
            {
                ImGui.TableSetupColumn("AAA", TableColumnFlags.WidthFixed);
                ImGui.TableSetupColumn("BBB", TableColumnFlags.WidthFixed);
                ImGui.TableSetupColumn("CCC", TableColumnFlags.WidthStretch);
                ImGui.TableHeadersRow();
                for (int row = 0; row < 5; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 3; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        ImGui.Text($"{(column == 2 ? "Stretch" : "Fixed")} {column},{row}");
                    }
                }
                ImGui.EndTable();
            }
            if (ImGui.BeginTable("table2", 6, s_tables_resize_mixed_flags))
            {
                ImGui.TableSetupColumn("AAA", TableColumnFlags.WidthFixed);
                ImGui.TableSetupColumn("BBB", TableColumnFlags.WidthFixed);
                ImGui.TableSetupColumn("CCC", TableColumnFlags.WidthFixed | TableColumnFlags.DefaultHide);
                ImGui.TableSetupColumn("DDD", TableColumnFlags.WidthStretch);
                ImGui.TableSetupColumn("EEE", TableColumnFlags.WidthStretch);
                ImGui.TableSetupColumn("FFF", TableColumnFlags.WidthStretch | TableColumnFlags.DefaultHide);
                ImGui.TableHeadersRow();
                for (int row = 0; row < 5; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 6; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        ImGui.Text($"{(column >= 3 ? "Stretch" : "Fixed")} {column},{row}");
                    }
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Reorderable, hideable, with headers"))
        {
            // DEMO MARKER: Tables/Reorderable, hideable, with headers
            HelpMarker(
                "Click and drag column headers to reorder columns.\n\n"
                + "Right-click on a header to open a context menu.");
            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_Resizable", ref s_tables_reorder_flags, TableFlags.Resizable);
            CheckboxFlags("ImGuiTableFlags_Reorderable", ref s_tables_reorder_flags, TableFlags.Reorderable);
            CheckboxFlags("ImGuiTableFlags_Hideable", ref s_tables_reorder_flags, TableFlags.Hideable);
            CheckboxFlags("ImGuiTableFlags_NoBordersInBody", ref s_tables_reorder_flags, TableFlags.NoBordersInBody);
            CheckboxFlags("ImGuiTableFlags_NoBordersInBodyUntilResize", ref s_tables_reorder_flags, TableFlags.NoBordersInBodyUntilResize); ImGui.SameLine(); HelpMarker("Disable vertical borders in columns Body until hovered for resize (borders will always appear in Headers)");
            CheckboxFlags("ImGuiTableFlags_HighlightHoveredColumn", ref s_tables_reorder_flags, TableFlags.HighlightHoveredColumn);
            PopStyleCompact();

            if (ImGui.BeginTable("table1", 3, s_tables_reorder_flags))
            {
                // Submit columns name with TableSetupColumn() and call TableHeadersRow() to create a row with a header in each column.
                // (Later we will show how TableSetupColumn() has other uses, optional flags, sizing weight etc.)
                ImGui.TableSetupColumn("One");
                ImGui.TableSetupColumn("Two");
                ImGui.TableSetupColumn("Three");
                ImGui.TableHeadersRow();
                for (int row = 0; row < 6; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 3; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        ImGui.Text($"Hello {column},{row}");
                    }
                }
                ImGui.EndTable();
            }

            // Use outer_size.x == 0.0f instead of default to make the table as tight as possible
            // (only valid when no scrolling and no stretch column)
            if (ImGui.BeginTable("table2", 3, s_tables_reorder_flags | TableFlags.SizingFixedFit, 0.0f, 0.0f))
            {
                ImGui.TableSetupColumn("One");
                ImGui.TableSetupColumn("Two");
                ImGui.TableSetupColumn("Three");
                ImGui.TableHeadersRow();
                for (int row = 0; row < 6; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 3; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        ImGui.Text($"Fixed {column},{row}");
                    }
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Padding"))
        {
            // DEMO MARKER: Tables/Padding
            // First example: showcase use of padding flags and effect of BorderOuterV/BorderInnerV on X padding.
            // We don't expose BorderOuterH/BorderInnerH here because they have no effect on X padding.
            HelpMarker(
                "We often want outer padding activated when any using features which makes the edges of a column visible:\n"
                + "e.g.:\n"
                + "- BorderOuterV\n"
                + "- any form of row selection\n"
                + "Because of this, activating BorderOuterV sets the default to PadOuterX. "
                + "Using PadOuterX or NoPadOuterX you can override the default.\n\n"
                + "Actual padding values are using style.CellPadding.\n\n"
                + "In this demo we don't show horizontal borders to emphasize how they don't affect default horizontal padding.");

            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_PadOuterX", ref s_tables_padding_flags1, TableFlags.PadOuterX);
            ImGui.SameLine(); HelpMarker("Enable outer-most padding (default if ImGuiTableFlags_BordersOuterV is set)");
            CheckboxFlags("ImGuiTableFlags_NoPadOuterX", ref s_tables_padding_flags1, TableFlags.NoPadOuterX);
            ImGui.SameLine(); HelpMarker("Disable outer-most padding (default if ImGuiTableFlags_BordersOuterV is not set)");
            CheckboxFlags("ImGuiTableFlags_NoPadInnerX", ref s_tables_padding_flags1, TableFlags.NoPadInnerX);
            ImGui.SameLine(); HelpMarker("Disable inner padding between columns (double inner padding if BordersOuterV is on, single inner padding if BordersOuterV is off)");
            CheckboxFlags("ImGuiTableFlags_BordersOuterV", ref s_tables_padding_flags1, TableFlags.BordersOuterV);
            CheckboxFlags("ImGuiTableFlags_BordersInnerV", ref s_tables_padding_flags1, TableFlags.BordersInnerV);
            ImGui.Checkbox("show_headers", ref s_tables_padding_show_headers);
            PopStyleCompact();

            if (ImGui.BeginTable("table_padding", 3, s_tables_padding_flags1))
            {
                if (s_tables_padding_show_headers)
                {
                    ImGui.TableSetupColumn("One");
                    ImGui.TableSetupColumn("Two");
                    ImGui.TableSetupColumn("Three");
                    ImGui.TableHeadersRow();
                }

                for (int row = 0; row < 5; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 3; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        if (row == 0)
                        {
                            ImGui.Text($"Avail {ImGui.GetContentRegionAvail().Width:F2}");
                        }
                        else
                        {
                            string buf = $"Hello {column},{row}";
                            ImGui.Button(buf, -float.Epsilon, 0.0f);
                        }
                        //if ((ImGui.TableGetColumnFlags() & TableColumnFlags.IsHovered) != 0)
                        //    ImGui.TableSetBgColor(TableBgTarget.CellBg, 0xFF006400);
                    }
                }
                ImGui.EndTable();
            }

            // Second example: set style.CellPadding to (0.0) or a custom value.
            // FIXME-TABLE: Vertical border effectively not displayed the same way as horizontal one...
            HelpMarker("Setting style.CellPadding to (0,0) or a custom value.");

            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_Borders", ref s_tables_padding_flags2, TableFlags.Borders);
            CheckboxFlags("ImGuiTableFlags_BordersH", ref s_tables_padding_flags2, TableFlags.BordersH);
            CheckboxFlags("ImGuiTableFlags_BordersV", ref s_tables_padding_flags2, TableFlags.BordersV);
            CheckboxFlags("ImGuiTableFlags_BordersInner", ref s_tables_padding_flags2, TableFlags.BordersInner);
            CheckboxFlags("ImGuiTableFlags_BordersOuter", ref s_tables_padding_flags2, TableFlags.BordersOuter);
            CheckboxFlags("ImGuiTableFlags_RowBg", ref s_tables_padding_flags2, TableFlags.RowBg);
            CheckboxFlags("ImGuiTableFlags_Resizable", ref s_tables_padding_flags2, TableFlags.Resizable);
            ImGui.Checkbox("show_widget_frame_bg", ref s_tables_padding_show_widget_frame_bg);
            ImGui.SliderFloat2("CellPadding", s_tables_padding_cell_padding, 0.0f, 10.0f, "%.0f");
            PopStyleCompact();

            ImGui.PushStyleVar(StyleVar.CellPadding, s_tables_padding_cell_padding[0], s_tables_padding_cell_padding[1]);
            if (ImGui.BeginTable("table_padding_2", 3, s_tables_padding_flags2))
            {
                if (!s_tables_padding_show_widget_frame_bg)
                    ImGui.PushStyleColor(Col.FrameBg, 0);
                for (int cell = 0; cell < 3 * 5; cell++)
                {
                    ImGui.TableNextColumn();
                    if (s_tables_padding_init)
                        s_tables_padding_text_bufs[cell] = "edit me";
                    ImGui.SetNextItemWidth(-float.Epsilon);
                    ImGui.PushID(cell);
                    ImGui.InputText("##cell", ref s_tables_padding_text_bufs[cell]);
                    ImGui.PopID();
                }
                if (!s_tables_padding_show_widget_frame_bg)
                    ImGui.PopStyleColor();
                s_tables_padding_init = false;
                ImGui.EndTable();
            }
            ImGui.PopStyleVar();

            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Sizing policies"))
        {
            // DEMO MARKER: Tables/Explicit widths
            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_Resizable", ref s_tables_sizing_flags1, TableFlags.Resizable);
            CheckboxFlags("ImGuiTableFlags_NoHostExtendX", ref s_tables_sizing_flags1, TableFlags.NoHostExtendX);
            PopStyleCompact();

            for (int table_n = 0; table_n < 4; table_n++)
            {
                ImGui.PushID(table_n);
                ImGui.SetNextItemWidth(TEXT_BASE_WIDTH * 30);
                EditTableSizingFlags(ref s_tables_sizing_policy_flags[table_n]);

                // To make it easier to understand the different sizing policy,
                // For each policy: we display one table where the columns have equal contents width,
                // and one where the columns have different contents width.
                if (ImGui.BeginTable("table1", 3, s_tables_sizing_policy_flags[table_n] | s_tables_sizing_flags1))
                {
                    for (int row = 0; row < 3; row++)
                    {
                        ImGui.TableNextRow();
                        ImGui.TableNextColumn(); ImGui.Text("Oh dear");
                        ImGui.TableNextColumn(); ImGui.Text("Oh dear");
                        ImGui.TableNextColumn(); ImGui.Text("Oh dear");
                    }
                    ImGui.EndTable();
                }
                if (ImGui.BeginTable("table2", 3, s_tables_sizing_policy_flags[table_n] | s_tables_sizing_flags1))
                {
                    for (int row = 0; row < 3; row++)
                    {
                        ImGui.TableNextRow();
                        ImGui.TableNextColumn(); ImGui.Text("AAAA");
                        ImGui.TableNextColumn(); ImGui.Text("BBBBBBBB");
                        ImGui.TableNextColumn(); ImGui.Text("CCCCCCCCCCCC");
                    }
                    ImGui.EndTable();
                }
                ImGui.PopID();
            }

            ImGui.Spacing();
            ImGui.TextUnformatted("Advanced");
            ImGui.SameLine();
            HelpMarker(
                "This section allows you to interact and see the effect of various sizing policies "
                + "depending on whether Scroll is enabled and the contents of your columns.");

            const int CT_ShowWidth = 0, CT_ShortText = 1, CT_LongText = 2, CT_Button = 3, CT_FillButton = 4, CT_InputText = 5;

            PushStyleCompact();
            ImGui.PushID("Advanced");
            ImGui.PushItemWidth(TEXT_BASE_WIDTH * 30);
            EditTableSizingFlags(ref s_tables_sizing_adv_flags);
            ImGui.Combo("Contents", ref s_tables_sizing_adv_contents_type, s_tables_sizing_adv_contents_names);
            if (s_tables_sizing_adv_contents_type == CT_FillButton)
            {
                ImGui.SameLine();
                HelpMarker(
                    "Be mindful that using right-alignment (e.g. size.x = -FLT_MIN) creates a feedback loop "
                    + "where contents width can feed into auto-column width can feed into contents width.");
            }
            ImGui.DragInt("Columns", ref s_tables_sizing_adv_column_count, 0.1f, 1, 64, "%d", SliderFlags.AlwaysClamp);
            CheckboxFlags("ImGuiTableFlags_Resizable", ref s_tables_sizing_adv_flags, TableFlags.Resizable);
            CheckboxFlags("ImGuiTableFlags_PreciseWidths", ref s_tables_sizing_adv_flags, TableFlags.PreciseWidths);
            ImGui.SameLine(); HelpMarker("Disable distributing remainder width to stretched columns (width allocation on a 100-wide table with 3 columns: Without this flag: 33,33,34. With this flag: 33,33,33). With larger number of columns, resizing will appear to be less smooth.");
            CheckboxFlags("ImGuiTableFlags_ScrollX", ref s_tables_sizing_adv_flags, TableFlags.ScrollX);
            CheckboxFlags("ImGuiTableFlags_ScrollY", ref s_tables_sizing_adv_flags, TableFlags.ScrollY);
            CheckboxFlags("ImGuiTableFlags_NoClip", ref s_tables_sizing_adv_flags, TableFlags.NoClip);
            ImGui.PopItemWidth();
            ImGui.PopID();
            PopStyleCompact();

            if (ImGui.BeginTable("table2", s_tables_sizing_adv_column_count, s_tables_sizing_adv_flags, 0.0f, TEXT_BASE_HEIGHT * 7))
            {
                for (int cell = 0; cell < 10 * s_tables_sizing_adv_column_count; cell++)
                {
                    ImGui.TableNextColumn();
                    int column = ImGui.TableGetColumnIndex();
                    int row = ImGui.TableGetRowIndex();

                    ImGui.PushID(cell);
                    string label = $"Hello {column},{row}";
                    switch (s_tables_sizing_adv_contents_type)
                    {
                        case CT_ShortText: ImGui.TextUnformatted(label); break;
                        case CT_LongText: ImGui.Text($"Some {(column == 0 ? "long" : "longeeer")} text {column},{row}\nOver two lines.."); break;
                        case CT_ShowWidth: ImGui.Text($"W: {ImGui.GetContentRegionAvail().Width:F1}"); break;
                        case CT_Button: ImGui.Button(label); break;
                        case CT_FillButton: ImGui.Button(label, -float.Epsilon, 0.0f); break;
                        case CT_InputText: ImGui.SetNextItemWidth(-float.Epsilon); ImGui.InputText("##", ref s_tables_sizing_adv_text_buf); break;
                    }
                    ImGui.PopID();
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Vertical scrolling, with clipping"))
        {
            // DEMO MARKER: Tables/Vertical scrolling, with clipping
            HelpMarker(
                "Here we activate ScrollY, which will create a child window container to allow hosting scrollable contents.\n\n"
                + "We also demonstrate using ImGuiListClipper to virtualize the submission of many items.");

            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_ScrollY", ref s_tables_vscroll_flags, TableFlags.ScrollY);
            PopStyleCompact();

            // When using ScrollX or ScrollY we need to specify a size for our table container!
            // Otherwise by default the table will fit all available space, like a BeginChild() call.
            if (ImGui.BeginTable("table_scrolly", 3, s_tables_vscroll_flags, 0.0f, TEXT_BASE_HEIGHT * 8))
            {
                ImGui.TableSetupScrollFreeze(0, 1); // Make top row always visible
                ImGui.TableSetupColumn("One", TableColumnFlags.None);
                ImGui.TableSetupColumn("Two", TableColumnFlags.None);
                ImGui.TableSetupColumn("Three", TableColumnFlags.None);
                ImGui.TableHeadersRow();

                // Demonstrate using clipper for large vertical lists
                using (var clipper = new ListClipper())
                {
                    clipper.Begin(1000);
                    while (clipper.Step())
                    {
                        for (int row = clipper.DisplayStart; row < clipper.DisplayEnd; row++)
                        {
                            ImGui.TableNextRow();
                            for (int column = 0; column < 3; column++)
                            {
                                ImGui.TableSetColumnIndex(column);
                                ImGui.Text($"Hello {column},{row}");
                            }
                        }
                    }
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Horizontal scrolling"))
        {
            // DEMO MARKER: Tables/Horizontal scrolling
            HelpMarker(
                "When ScrollX is enabled, the default sizing policy becomes ImGuiTableFlags_SizingFixedFit, "
                + "as automatically stretching columns doesn't make much sense with horizontal scrolling.\n\n"
                + "Also note that as of the current version, you will almost always want to enable ScrollY along with ScrollX, "
                + "because the container window won't automatically extend vertically to fix contents "
                + "(this may be improved in future versions).");

            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_Resizable", ref s_tables_hscroll_flags, TableFlags.Resizable);
            CheckboxFlags("ImGuiTableFlags_ScrollX", ref s_tables_hscroll_flags, TableFlags.ScrollX);
            CheckboxFlags("ImGuiTableFlags_ScrollY", ref s_tables_hscroll_flags, TableFlags.ScrollY);
            ImGui.SetNextItemWidth(ImGui.GetFrameHeight());
            ImGui.DragInt("freeze_cols", ref s_tables_hscroll_freeze_cols, 0.2f, 0, 9, null, SliderFlags.NoInput);
            ImGui.SetNextItemWidth(ImGui.GetFrameHeight());
            ImGui.DragInt("freeze_rows", ref s_tables_hscroll_freeze_rows, 0.2f, 0, 9, null, SliderFlags.NoInput);
            PopStyleCompact();

            // When using ScrollX or ScrollY we need to specify a size for our table container!
            // Otherwise by default the table will fit all available space, like a BeginChild() call.
            if (ImGui.BeginTable("table_scrollx", 7, s_tables_hscroll_flags, 0.0f, TEXT_BASE_HEIGHT * 8))
            {
                ImGui.TableSetupScrollFreeze(s_tables_hscroll_freeze_cols, s_tables_hscroll_freeze_rows);
                ImGui.TableSetupColumn("Line #", TableColumnFlags.NoHide); // Make the first column not hideable to match our use of TableSetupScrollFreeze()
                ImGui.TableSetupColumn("One");
                ImGui.TableSetupColumn("Two");
                ImGui.TableSetupColumn("Three");
                ImGui.TableSetupColumn("Four");
                ImGui.TableSetupColumn("Five");
                ImGui.TableSetupColumn("Six");
                ImGui.TableHeadersRow();
                for (int row = 0; row < 20; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 7; column++)
                    {
                        // Both TableNextColumn() and TableSetColumnIndex() return true when a column is visible or performing width measurement.
                        // Because here we know that:
                        // - A) all our columns are contributing the same to row height
                        // - B) column 0 is always visible,
                        // We only always submit this one column and can skip others.
                        // More advanced per-column clipping behaviors may benefit from polling the status flags via TableGetColumnFlags().
                        if (!ImGui.TableSetColumnIndex(column) && column > 0)
                            continue;
                        if (column == 0)
                            ImGui.Text($"Line {row}");
                        else
                            ImGui.Text($"Hello world {column},{row}");
                    }
                }
                ImGui.EndTable();
            }

            ImGui.Spacing();
            ImGui.TextUnformatted("Stretch + ScrollX");
            ImGui.SameLine();
            HelpMarker(
                "Showcase using Stretch columns + ScrollX together: "
                + "this is rather unusual and only makes sense when specifying an 'inner_width' for the table!\n"
                + "Without an explicit value, inner_width is == outer_size.x and therefore using Stretch columns "
                + "along with ScrollX doesn't make sense.");
            PushStyleCompact();
            ImGui.PushID("flags3");
            ImGui.PushItemWidth(TEXT_BASE_WIDTH * 30);
            CheckboxFlags("ImGuiTableFlags_ScrollX", ref s_tables_hscroll_flags2, TableFlags.ScrollX);
            ImGui.DragFloat("inner_width", ref s_tables_hscroll_inner_width, 1.0f, 0.0f, float.MaxValue, "%.1f");
            ImGui.PopItemWidth();
            ImGui.PopID();
            PopStyleCompact();
            if (ImGui.BeginTable("table2", 7, s_tables_hscroll_flags2, 0.0f, TEXT_BASE_HEIGHT * 8, s_tables_hscroll_inner_width))
            {
                for (int cell = 0; cell < 20 * 7; cell++)
                {
                    ImGui.TableNextColumn();
                    ImGui.Text($"Hello world {ImGui.TableGetColumnIndex()},{ImGui.TableGetRowIndex()}");
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Columns flags"))
        {
            // DEMO MARKER: Tables/Columns flags
            // Create a first table just to show all the options/flags we want to make visible in our example!
            const int column_count = 3;
            string[] column_names = ["One", "Two", "Three"];

            if (ImGui.BeginTable("table_columns_flags_checkboxes", column_count, TableFlags.None))
            {
                PushStyleCompact();
                for (int column = 0; column < column_count; column++)
                {
                    ImGui.TableNextColumn();
                    ImGui.PushID(column);
                    ImGui.AlignTextToFramePadding(); // FIXME-TABLE: Workaround for wrong text baseline propagation across columns
                    ImGui.Text($"'{column_names[column]}'");
                    ImGui.Spacing();
                    ImGui.Text("Input flags:");
                    EditTableColumnsFlags(ref s_tables_colflags_column_flags[column]);
                    ImGui.Spacing();
                    ImGui.Text("Output flags:");
                    ImGui.BeginDisabled();
                    ShowTableColumnsStatusFlags(s_tables_colflags_column_flags_out[column]);
                    ImGui.EndDisabled();
                    ImGui.PopID();
                }
                PopStyleCompact();
                ImGui.EndTable();
            }

            // Create the real table we care about for the example!
            // We use a scrolling table to be able to showcase the difference between the _IsEnabled and _IsVisible flags above,
            // otherwise in a non-scrolling table columns are always visible (unless using ImGuiTableFlags_NoKeepColumnsVisible
            // + resizing the parent window down).
            const TableFlags flags
                = TableFlags.SizingFixedFit | TableFlags.ScrollX | TableFlags.ScrollY
                | TableFlags.RowBg | TableFlags.BordersOuter | TableFlags.BordersV
                | TableFlags.Resizable | TableFlags.Reorderable | TableFlags.Hideable | TableFlags.Sortable;
            if (ImGui.BeginTable("table_columns_flags", column_count, flags, 0.0f, TEXT_BASE_HEIGHT * 9))
            {
                bool has_angled_header = false;
                for (int column = 0; column < column_count; column++)
                {
                    has_angled_header |= (s_tables_colflags_column_flags[column] & TableColumnFlags.AngledHeader) != 0;
                    ImGui.TableSetupColumn(column_names[column], s_tables_colflags_column_flags[column]);
                }
                if (has_angled_header)
                    ImGui.TableAngledHeadersRow();
                ImGui.TableHeadersRow();
                for (int column = 0; column < column_count; column++)
                    s_tables_colflags_column_flags_out[column] = ImGui.TableGetColumnFlags(column);
                float indent_step = (int)TEXT_BASE_WIDTH / 2;
                for (int row = 0; row < 8; row++)
                {
                    // Add some indentation to demonstrate usage of per-column IndentEnable/IndentDisable flags.
                    ImGui.Indent(indent_step);
                    ImGui.TableNextRow();
                    for (int column = 0; column < column_count; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        ImGui.Text($"{(column == 0 ? "Indented" : "Hello")} {ImGui.TableGetColumnName(column)}");
                    }
                }
                ImGui.Unindent(indent_step * 8.0f);

                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Columns widths"))
        {
            // DEMO MARKER: Tables/Columns widths
            HelpMarker("Using TableSetupColumn() to setup default width.");

            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_Resizable", ref s_tables_widths_flags1, TableFlags.Resizable);
            CheckboxFlags("ImGuiTableFlags_NoBordersInBodyUntilResize", ref s_tables_widths_flags1, TableFlags.NoBordersInBodyUntilResize);
            PopStyleCompact();
            if (ImGui.BeginTable("table1", 3, s_tables_widths_flags1))
            {
                // We could also set ImGuiTableFlags_SizingFixedFit on the table and all columns will default to ImGuiTableColumnFlags_WidthFixed.
                ImGui.TableSetupColumn("one", TableColumnFlags.WidthFixed, 100.0f); // Default to 100.0f
                ImGui.TableSetupColumn("two", TableColumnFlags.WidthFixed, 200.0f); // Default to 200.0f
                ImGui.TableSetupColumn("three", TableColumnFlags.WidthFixed);       // Default to auto
                ImGui.TableHeadersRow();
                for (int row = 0; row < 4; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 3; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        if (row == 0)
                            ImGui.Text($"(w: {ImGui.GetContentRegionAvail().Width,5:F1})");
                        else
                            ImGui.Text($"Hello {column},{row}");
                    }
                }
                ImGui.EndTable();
            }

            HelpMarker(
                "Using TableSetupColumn() to setup explicit width.\n\nUnless _NoKeepColumnsVisible is set, "
                + "fixed columns with set width may still be shrunk down if there's not enough space in the host.");

            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_NoKeepColumnsVisible", ref s_tables_widths_flags2, TableFlags.NoKeepColumnsVisible);
            CheckboxFlags("ImGuiTableFlags_BordersInnerV", ref s_tables_widths_flags2, TableFlags.BordersInnerV);
            CheckboxFlags("ImGuiTableFlags_BordersOuterV", ref s_tables_widths_flags2, TableFlags.BordersOuterV);
            PopStyleCompact();
            if (ImGui.BeginTable("table2", 4, s_tables_widths_flags2))
            {
                // We could also set ImGuiTableFlags_SizingFixedFit on the table and then all columns
                // will default to ImGuiTableColumnFlags_WidthFixed.
                ImGui.TableSetupColumn("", TableColumnFlags.WidthFixed, 100.0f);
                ImGui.TableSetupColumn("", TableColumnFlags.WidthFixed, TEXT_BASE_WIDTH * 15.0f);
                ImGui.TableSetupColumn("", TableColumnFlags.WidthFixed, TEXT_BASE_WIDTH * 30.0f);
                ImGui.TableSetupColumn("", TableColumnFlags.WidthFixed, TEXT_BASE_WIDTH * 15.0f);
                for (int row = 0; row < 5; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 4; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        if (row == 0)
                            ImGui.Text($"(w: {ImGui.GetContentRegionAvail().Width,5:F1})");
                        else
                            ImGui.Text($"Hello {column},{row}");
                    }
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Nested tables"))
        {
            // DEMO MARKER: Tables/Nested tables
            HelpMarker("This demonstrates embedding a table into another table cell.");

            if (ImGui.BeginTable("table_nested1", 2, TableFlags.Borders | TableFlags.Resizable | TableFlags.Reorderable | TableFlags.Hideable))
            {
                ImGui.TableSetupColumn("A0");
                ImGui.TableSetupColumn("A1");
                ImGui.TableHeadersRow();

                ImGui.TableNextColumn();
                ImGui.Text("A0 Row 0");
                {
                    float rows_height = (TEXT_BASE_HEIGHT * 2.0f) + (Style.CellPadding.Y * 2.0f);
                    if (ImGui.BeginTable("table_nested2", 2, TableFlags.Borders | TableFlags.Resizable | TableFlags.Reorderable | TableFlags.Hideable))
                    {
                        ImGui.TableSetupColumn("B0");
                        ImGui.TableSetupColumn("B1");
                        ImGui.TableHeadersRow();

                        ImGui.TableNextRow(TableRowFlags.None, rows_height);
                        ImGui.TableNextColumn();
                        ImGui.Text("B0 Row 0");
                        ImGui.TableNextColumn();
                        ImGui.Text("B1 Row 0");
                        ImGui.TableNextRow(TableRowFlags.None, rows_height);
                        ImGui.TableNextColumn();
                        ImGui.Text("B0 Row 1");
                        ImGui.TableNextColumn();
                        ImGui.Text("B1 Row 1");

                        ImGui.EndTable();
                    }
                }
                ImGui.TableNextColumn(); ImGui.Text("A1 Row 0");
                ImGui.TableNextColumn(); ImGui.Text("A0 Row 1");
                ImGui.TableNextColumn(); ImGui.Text("A1 Row 1");
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Row height"))
        {
            // DEMO MARKER: Tables/Row height
            HelpMarker(
                "You can pass a 'min_row_height' to TableNextRow().\n\nRows are padded with 'style.CellPadding.y' on top and bottom, "
                + "so effectively the minimum row height will always be >= 'style.CellPadding.y * 2.0f'.\n\n"
                + "We cannot honor a _maximum_ row height as that would require a unique clipping rectangle per row.");
            if (ImGui.BeginTable("table_row_height", 1, TableFlags.Borders))
            {
                for (int row = 0; row < 8; row++)
                {
                    float min_row_height = (int)(TEXT_BASE_HEIGHT * 0.30f * row + Style.CellPadding.Y * 2.0f);
                    ImGui.TableNextRow(TableRowFlags.None, min_row_height);
                    ImGui.TableNextColumn();
                    ImGui.Text($"min_row_height = {min_row_height:F2}");
                }
                ImGui.EndTable();
            }

            HelpMarker(
                "Showcase using SameLine(0,0) to share Current Line Height between cells.\n\n"
                + "Please note that Tables Row Height is not the same thing as Current Line Height, "
                + "as a table cell may contains multiple lines.");
            if (ImGui.BeginTable("table_share_lineheight", 2, TableFlags.Borders))
            {
                ImGui.TableNextRow();
                ImGui.TableNextColumn();
                ImGui.ColorButton("##1", 0.13f, 0.26f, 0.40f, 1.0f, ColorEditFlags.None, 40, 40);
                ImGui.TableNextColumn();
                ImGui.Text("Line 1");
                ImGui.Text("Line 2");

                ImGui.TableNextRow();
                ImGui.TableNextColumn();
                ImGui.ColorButton("##2", 0.13f, 0.26f, 0.40f, 1.0f, ColorEditFlags.None, 40, 40);
                ImGui.TableNextColumn();
                ImGui.SameLine(0.0f, 0.0f); // Reuse line height from previous column
                ImGui.Text("Line 1, with SameLine(0,0)");
                ImGui.Text("Line 2");

                ImGui.EndTable();
            }

            HelpMarker("Showcase altering CellPadding.y between rows. Note that CellPadding.x is locked for the entire table.");
            if (ImGui.BeginTable("table_changing_cellpadding_y", 1, TableFlags.Borders))
            {
                for (int row = 0; row < 8; row++)
                {
                    if ((row % 3) == 2)
                        ImGui.PushStyleVarY(StyleVar.CellPadding, 20.0f);
                    ImGui.TableNextRow(TableRowFlags.None);
                    ImGui.TableNextColumn();
                    ImGui.Text($"CellPadding.y = {Style.CellPadding.Y:F2}");
                    if ((row % 3) == 2)
                        ImGui.PopStyleVar();
                }
                ImGui.EndTable();
            }

            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Outer size"))
        {
            // DEMO MARKER: Tables/Outer size
            // Showcasing use of ImGuiTableFlags_NoHostExtendX and ImGuiTableFlags_NoHostExtendY
            // Important to that note how the two flags have slightly different behaviors!
            ImGui.Text("Using NoHostExtendX and NoHostExtendY:");
            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_NoHostExtendX", ref s_tables_outer_flags, TableFlags.NoHostExtendX);
            ImGui.SameLine(); HelpMarker("Make outer width auto-fit to columns, overriding outer_size.x value.\n\nOnly available when ScrollX/ScrollY are disabled and Stretch columns are not used.");
            CheckboxFlags("ImGuiTableFlags_NoHostExtendY", ref s_tables_outer_flags, TableFlags.NoHostExtendY);
            ImGui.SameLine(); HelpMarker("Make outer height stop exactly at outer_size.y (prevent auto-extending table past the limit).\n\nOnly available when ScrollX/ScrollY are disabled. Data below the limit will be clipped and not visible.");
            PopStyleCompact();

            if (ImGui.BeginTable("table1", 3, s_tables_outer_flags, 0.0f, TEXT_BASE_HEIGHT * 5.5f))
            {
                for (int row = 0; row < 10; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 3; column++)
                    {
                        ImGui.TableNextColumn();
                        ImGui.Text($"Cell {column},{row}");
                    }
                }
                ImGui.EndTable();
            }
            ImGui.SameLine();
            ImGui.Text("Hello!");

            ImGui.Spacing();

            ImGui.Text("Using explicit size:");
            if (ImGui.BeginTable("table2", 3, TableFlags.Borders | TableFlags.RowBg, TEXT_BASE_WIDTH * 30, 0.0f))
            {
                for (int row = 0; row < 5; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 3; column++)
                    {
                        ImGui.TableNextColumn();
                        ImGui.Text($"Cell {column},{row}");
                    }
                }
                ImGui.EndTable();
            }
            ImGui.SameLine();
            if (ImGui.BeginTable("table3", 3, TableFlags.Borders | TableFlags.RowBg, TEXT_BASE_WIDTH * 30, 0.0f))
            {
                float rows_height = TEXT_BASE_HEIGHT * 1.5f + Style.CellPadding.Y * 2.0f;
                for (int row = 0; row < 3; row++)
                {
                    ImGui.TableNextRow(0, rows_height);
                    for (int column = 0; column < 3; column++)
                    {
                        ImGui.TableNextColumn();
                        ImGui.Text($"Cell {column},{row}");
                    }
                }
                ImGui.EndTable();
            }

            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Background color"))
        {
            // DEMO MARKER: Tables/Background color
            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_Borders", ref s_tables_bg_flags, TableFlags.Borders);
            CheckboxFlags("ImGuiTableFlags_RowBg", ref s_tables_bg_flags, TableFlags.RowBg);
            ImGui.SameLine(); HelpMarker("ImGuiTableFlags_RowBg automatically sets RowBg0 to alternative colors pulled from the Style.");
            ImGui.Combo("row bg type", ref s_tables_bg_row_bg_type, s_tables_bg_row_bg_type_names);
            ImGui.Combo("row bg target", ref s_tables_bg_row_bg_target, s_tables_bg_row_bg_target_names); ImGui.SameLine(); HelpMarker("Target RowBg0 to override the alternating odd/even colors,\nTarget RowBg1 to blend with them.");
            ImGui.Combo("cell bg type", ref s_tables_bg_cell_bg_type, s_tables_bg_cell_bg_type_names); ImGui.SameLine(); HelpMarker("We are colorizing cells to B1->C2 here.");
            PopStyleCompact();

            if (ImGui.BeginTable("table1", 5, s_tables_bg_flags))
            {
                for (int row = 0; row < 6; row++)
                {
                    ImGui.TableNextRow();

                    // Demonstrate setting a row background color with 'ImGui.TableSetBgColor(TableBgTarget.RowBgX, ...)'
                    // We use a transparent color so we can see the one behind in case our target is RowBg1 and RowBg0 was already targeted by the ImGuiTableFlags_RowBg flag.
                    if (s_tables_bg_row_bg_type != 0)
                    {
                        uint row_bg_color = s_tables_bg_row_bg_type == 1
                            ? ImGui.GetColorU32(0.7f, 0.3f, 0.3f, 0.65f)
                            : ImGui.GetColorU32(0.2f + row * 0.1f, 0.2f, 0.2f, 0.65f); // Flat or Gradient?
                        ImGui.TableSetBgColor((TableBgTarget)((int)TableBgTarget.RowBg0 + s_tables_bg_row_bg_target), row_bg_color);
                    }

                    // Fill cells
                    for (int column = 0; column < 5; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        ImGui.Text($"{(char)('A' + row)}{(char)('0' + column)}");

                        // Change background of Cells B1->C2
                        // Demonstrate setting a cell background color with 'ImGui.TableSetBgColor(TableBgTarget.CellBg, ...)'
                        // (the CellBg color will be blended over the RowBg and ColumnBg colors)
                        // We can also pass a column number as a third parameter to TableSetBgColor() and do this outside the column loop.
                        if (row >= 1 && row <= 2 && column >= 1 && column <= 2 && s_tables_bg_cell_bg_type == 1)
                        {
                            uint cell_bg_color = ImGui.GetColorU32(0.3f, 0.3f, 0.7f, 0.65f);
                            ImGui.TableSetBgColor(TableBgTarget.CellBg, cell_bg_color);
                        }
                    }
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Tree view"))
        {
            // DEMO MARKER: Tables/Tree view
            ImGui.CheckboxFlags("ImGuiTreeNodeFlags_SpanFullWidth", ref s_tables_treeview_tree_node_flags_base, (int)TreeNodeFlags.SpanFullWidth);
            ImGui.CheckboxFlags("ImGuiTreeNodeFlags_SpanLabelWidth", ref s_tables_treeview_tree_node_flags_base, (int)TreeNodeFlags.SpanLabelWidth);
            ImGui.CheckboxFlags("ImGuiTreeNodeFlags_SpanAllColumns", ref s_tables_treeview_tree_node_flags_base, (int)TreeNodeFlags.SpanAllColumns);
            ImGui.CheckboxFlags("ImGuiTreeNodeFlags_LabelSpanAllColumns", ref s_tables_treeview_tree_node_flags_base, (int)TreeNodeFlags.LabelSpanAllColumns);
            ImGui.SameLine(); HelpMarker("Useful if you know that you aren't displaying contents in other columns");

            HelpMarker("See \"Columns flags\" section to configure how indentation is applied to individual columns.");
            if (ImGui.BeginTable("3ways", 3, s_tables_treeview_flags))
            {
                // The first column will use the default _WidthStretch when ScrollX is Off and _WidthFixed when ScrollX is On
                ImGui.TableSetupColumn("Name", TableColumnFlags.NoHide);
                ImGui.TableSetupColumn("Size", TableColumnFlags.WidthFixed, TEXT_BASE_WIDTH * 12.0f);
                ImGui.TableSetupColumn("Type", TableColumnFlags.WidthFixed, TEXT_BASE_WIDTH * 18.0f);
                ImGui.TableHeadersRow();

                DisplayNode(s_tables_treeview_nodes, 0);

                ImGui.EndTable();

                static void DisplayNode(MyTreeNode[] all_nodes, int node_idx)
                {
                    ref readonly MyTreeNode node = ref all_nodes[node_idx];
                    ImGui.TableNextRow();
                    ImGui.TableNextColumn();
                    bool is_folder = node.ChildCount > 0;

                    TreeNodeFlags node_flags = (TreeNodeFlags)s_tables_treeview_tree_node_flags_base;
                    if (node_idx != 0)
                        node_flags &= ~TreeNodeFlags.LabelSpanAllColumns; // Only demonstrate this on the root node.

                    if (is_folder)
                    {
                        bool open = ImGui.TreeNodeEx(node.Name, node_flags);
                        if ((node_flags & TreeNodeFlags.LabelSpanAllColumns) == 0)
                        {
                            ImGui.TableNextColumn();
                            ImGui.TextDisabled("--");
                            ImGui.TableNextColumn();
                            ImGui.TextUnformatted(node.Type);
                        }
                        if (open)
                        {
                            for (int child_n = 0; child_n < node.ChildCount; child_n++)
                                DisplayNode(all_nodes, node.ChildIdx + child_n);
                            ImGui.TreePop();
                        }
                    }
                    else
                    {
                        ImGui.TreeNodeEx(node.Name, node_flags | TreeNodeFlags.Leaf | TreeNodeFlags.Bullet | TreeNodeFlags.NoTreePushOnOpen);
                        ImGui.TableNextColumn();
                        ImGui.Text($"{node.Size}");
                        ImGui.TableNextColumn();
                        ImGui.TextUnformatted(node.Type);
                    }
                }
            }
            ImGui.TreePop();
        }

        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Item width"))
        {
            // DEMO MARKER: Tables/Item width
            HelpMarker(
                "Showcase using PushItemWidth() and how it is preserved on a per-column basis.\n\n"
                + "Note that on auto-resizing non-resizable fixed columns, querying the content width for "
                + "e.g. right-alignment doesn't make sense.");
            if (ImGui.BeginTable("table_item_width", 3, TableFlags.Borders))
            {
                ImGui.TableSetupColumn("small");
                ImGui.TableSetupColumn("half");
                ImGui.TableSetupColumn("right-align");
                ImGui.TableHeadersRow();

                for (int row = 0; row < 3; row++)
                {
                    ImGui.TableNextRow();
                    if (row == 0)
                    {
                        // Setup ItemWidth once (instead of setting up every time, which is also possible but less efficient)
                        ImGui.TableSetColumnIndex(0);
                        ImGui.PushItemWidth(TEXT_BASE_WIDTH * 3.0f); // Small
                        ImGui.TableSetColumnIndex(1);
                        ImGui.PushItemWidth(-ImGui.GetContentRegionAvail().Width * 0.5f);
                        ImGui.TableSetColumnIndex(2);
                        ImGui.PushItemWidth(-float.Epsilon); // Right-aligned
                    }

                    // Draw our contents
                    ImGui.PushID(row);
                    ImGui.TableSetColumnIndex(0);
                    ImGui.SliderFloat("float0", ref s_tables_item_width_dummy_f, 0.0f, 1.0f);
                    ImGui.TableSetColumnIndex(1);
                    ImGui.SliderFloat("float1", ref s_tables_item_width_dummy_f, 0.0f, 1.0f);
                    ImGui.TableSetColumnIndex(2);
                    ImGui.SliderFloat("##float2", ref s_tables_item_width_dummy_f, 0.0f, 1.0f); // No visible label since right-aligned
                    ImGui.PopID();
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        // Demonstrate using TableHeader() calls instead of TableHeadersRow()
        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Custom headers"))
        {
            // DEMO MARKER: Tables/Custom headers
            const int COLUMNS_COUNT = 3;
            if (ImGui.BeginTable("table_custom_headers", COLUMNS_COUNT, TableFlags.Borders | TableFlags.Reorderable | TableFlags.Hideable))
            {
                ImGui.TableSetupColumn("Apricot");
                ImGui.TableSetupColumn("Banana");
                ImGui.TableSetupColumn("Cherry");

                // Dummy entire-column selection storage
                // FIXME: It would be nice to actually demonstrate full-featured selection using those checkbox.
                bool[] column_selected = s_tables_custom_headers_column_selected;

                // Instead of calling TableHeadersRow() we'll submit custom headers ourselves.
                // (A different approach is also possible:
                //    - Specify ImGuiTableColumnFlags_NoHeaderLabel in some TableSetupColumn() call.
                //    - Call TableHeadersRow() normally. This will submit TableHeader() with no name.
                //    - Then call TableSetColumnIndex() to position yourself in the column and submit your stuff e.g. Checkbox().)
                ImGui.TableNextRow(TableRowFlags.Headers);
                for (int column = 0; column < COLUMNS_COUNT; column++)
                {
                    ImGui.TableSetColumnIndex(column);
                    string? column_name = ImGui.TableGetColumnName(column); // Retrieve name passed to TableSetupColumn()
                    ImGui.PushID(column);
                    ImGui.PushStyleVar(StyleVar.FramePadding, 0, 0);
                    ImGui.Checkbox("##checkall", ref column_selected[column]);
                    ImGui.PopStyleVar();
                    ImGui.SameLine(0.0f, Style.ItemInnerSpacing.X);
                    ImGui.TableHeader(column_name ?? "");
                    ImGui.PopID();
                }

                // Submit table contents
                for (int row = 0; row < 5; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < 3; column++)
                    {
                        string buf = $"Cell {column},{row}";
                        ImGui.TableSetColumnIndex(column);
                        ImGui.Selectable(buf, column_selected[column]);
                    }
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        // Demonstrate using ImGuiTableColumnFlags_AngledHeader flag to create angled headers
        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Angled headers"))
        {
            // DEMO MARKER: Tables/Angled headers
            string[] column_names = s_tables_angled_column_names;
            int columns_count = column_names.Length;
            const int rows_count = 12;

            CheckboxFlags("_ScrollX", ref s_tables_angled_flags, TableFlags.ScrollX);
            CheckboxFlags("_ScrollY", ref s_tables_angled_flags, TableFlags.ScrollY);
            CheckboxFlags("_Resizable", ref s_tables_angled_flags, TableFlags.Resizable);
            CheckboxFlags("_Sortable", ref s_tables_angled_flags, TableFlags.Sortable);
            CheckboxFlags("_NoBordersInBody", ref s_tables_angled_flags, TableFlags.NoBordersInBody);
            CheckboxFlags("_HighlightHoveredColumn", ref s_tables_angled_flags, TableFlags.HighlightHoveredColumn);
            ImGui.SetNextItemWidth(ImGui.GetFontSize() * 8);
            ImGui.SliderInt("Frozen columns", ref s_tables_angled_frozen_cols, 0, 2);
            ImGui.SetNextItemWidth(ImGui.GetFontSize() * 8);
            ImGui.SliderInt("Frozen rows", ref s_tables_angled_frozen_rows, 0, 2);
            CheckboxFlags("Disable header contributing to column width", ref s_tables_angled_column_flags, TableColumnFlags.NoHeaderWidth);

            if (ImGui.TreeNode("Style settings"))
            {
                ImGui.SameLine();
                HelpMarker("Giving access to some ImGuiStyle value in this demo for convenience.");
                ImGui.SetNextItemWidth(ImGui.GetFontSize() * 8);
                float angledHeadersAngle = Style.TableAngledHeadersAngle;
                if (ImGui.SliderAngle("style.TableAngledHeadersAngle", ref angledHeadersAngle, -50.0f, +50.0f))
                    Style.TableAngledHeadersAngle = angledHeadersAngle;
                ImGui.SetNextItemWidth(ImGui.GetFontSize() * 8);
                var angledHeadersTextAlign = Style.TableAngledHeadersTextAlign;
                Span<float> angledAlign2 = stackalloc float[] { angledHeadersTextAlign.X, angledHeadersTextAlign.Y };
                if (ImGui.SliderFloat2("style.TableAngledHeadersTextAlign", angledAlign2, 0.0f, 1.0f, "%.2f"))
                    Style.TableAngledHeadersTextAlign = new Vec2(angledAlign2[0], angledAlign2[1]);
                ImGui.TreePop();
            }

            if (ImGui.BeginTable("table_angled_headers", columns_count, s_tables_angled_flags, 0.0f, TEXT_BASE_HEIGHT * 12))
            {
                ImGui.TableSetupColumn(column_names[0], TableColumnFlags.NoHide | TableColumnFlags.NoReorder);
                for (int n = 1; n < columns_count; n++)
                    ImGui.TableSetupColumn(column_names[n], s_tables_angled_column_flags);
                ImGui.TableSetupScrollFreeze(s_tables_angled_frozen_cols, s_tables_angled_frozen_rows);

                ImGui.TableAngledHeadersRow(); // Draw angled headers for all columns with the ImGuiTableColumnFlags_AngledHeader flag.
                ImGui.TableHeadersRow();       // Draw remaining headers and allow access to context-menu and other functions.
                for (int row = 0; row < rows_count; row++)
                {
                    ImGui.PushID(row);
                    ImGui.TableNextRow();
                    ImGui.TableSetColumnIndex(0);
                    ImGui.AlignTextToFramePadding();
                    ImGui.Text($"Track {row}");
                    for (int column = 1; column < columns_count; column++)
                        if (ImGui.TableSetColumnIndex(column))
                        {
                            ImGui.PushID(column);
                            ImGui.Checkbox("", ref s_tables_angled_bools[row * columns_count + column]);
                            ImGui.PopID();
                        }
                    ImGui.PopID();
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        // Demonstrate creating custom context menus inside columns,
        // while playing it nice with context menus provided by TableHeadersRow()/TableHeader()
        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Context menus"))
        {
            // DEMO MARKER: Tables/Context menus
            HelpMarker(
                "By default, right-clicking over a TableHeadersRow()/TableHeader() line will open the default context-menu.\n"
                + "Using ImGuiTableFlags_ContextMenuInBody we also allow right-clicking over columns body.");

            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_ContextMenuInBody", ref s_tables_ctx_flags1, TableFlags.ContextMenuInBody);
            PopStyleCompact();

            // Context Menus: first example
            // [1.1] Right-click on the TableHeadersRow() line to open the default table context menu.
            // [1.2] Right-click in columns also open the default table context menu (if ImGuiTableFlags_ContextMenuInBody is set)
            const int COLUMNS_COUNT = 3;
            if (ImGui.BeginTable("table_context_menu", COLUMNS_COUNT, s_tables_ctx_flags1))
            {
                ImGui.TableSetupColumn("One");
                ImGui.TableSetupColumn("Two");
                ImGui.TableSetupColumn("Three");

                // [1.1]] Right-click on the TableHeadersRow() line to open the default table context menu.
                ImGui.TableHeadersRow();

                // Submit dummy contents
                for (int row = 0; row < 4; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < COLUMNS_COUNT; column++)
                    {
                        ImGui.TableSetColumnIndex(column);
                        ImGui.Text($"Cell {column},{row}");
                    }
                }
                ImGui.EndTable();
            }

            // Context Menus: second example
            // [2.1] Right-click on the TableHeadersRow() line to open the default table context menu.
            // [2.2] Right-click on the ".." to open a custom popup
            // [2.3] Right-click in columns to open another custom popup
            HelpMarker(
                "Demonstrate mixing table context menu (over header), item context button (over button) "
                + "and custom per-column context menu (over column body).");
            TableFlags flags2 = TableFlags.Resizable | TableFlags.SizingFixedFit | TableFlags.Reorderable | TableFlags.Hideable | TableFlags.Borders;
            if (ImGui.BeginTable("table_context_menu_2", COLUMNS_COUNT, flags2))
            {
                ImGui.TableSetupColumn("One");
                ImGui.TableSetupColumn("Two");
                ImGui.TableSetupColumn("Three");

                // [2.1] Right-click on the TableHeadersRow() line to open the default table context menu.
                ImGui.TableHeadersRow();
                for (int row = 0; row < 4; row++)
                {
                    ImGui.TableNextRow();
                    for (int column = 0; column < COLUMNS_COUNT; column++)
                    {
                        // Submit dummy contents
                        ImGui.TableSetColumnIndex(column);
                        ImGui.Text($"Cell {column},{row}");
                        ImGui.SameLine();

                        // [2.2] Right-click on the ".." to open a custom popup
                        ImGui.PushID(row * COLUMNS_COUNT + column);
                        ImGui.SmallButton("..");
                        if (ImGui.BeginPopupContextItem())
                        {
                            ImGui.Text($"This is the popup for Button(\"..\") in Cell {column},{row}");
                            if (ImGui.Button("Close"))
                                ImGui.CloseCurrentPopup();
                            ImGui.EndPopup();
                        }
                        ImGui.PopID();
                    }
                }

                // [2.3] Right-click anywhere in columns to open another custom popup
                // (instead of testing for !IsAnyItemHovered() we could also call OpenPopup() with ImGuiPopupFlags_NoOpenOverExistingPopup
                // to manage popup priority as the popups triggers, here "are we hovering a column" are overlapping)
                int hovered_column = -1;
                for (int column = 0; column < COLUMNS_COUNT + 1; column++)
                {
                    ImGui.PushID(column);
                    if ((ImGui.TableGetColumnFlags(column) & TableColumnFlags.IsHovered) != 0)
                        hovered_column = column;
                    if (hovered_column == column && !ImGui.IsAnyItemHovered() && ImGui.IsMouseReleased(MouseButton.Right))
                        ImGui.OpenPopup("MyPopup");
                    if (ImGui.BeginPopup("MyPopup"))
                    {
                        if (column == COLUMNS_COUNT)
                            ImGui.Text("This is a custom popup for unused space after the last column.");
                        else
                            ImGui.Text($"This is a custom popup for Column {column}");
                        if (ImGui.Button("Close"))
                            ImGui.CloseCurrentPopup();
                        ImGui.EndPopup();
                    }
                    ImGui.PopID();
                }

                ImGui.EndTable();
                ImGui.Text($"Hovered column: {hovered_column}");
            }
            ImGui.TreePop();
        }

        // Demonstrate creating multiple tables with the same ID
        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Synced instances"))
        {
            // DEMO MARKER: Tables/Synced instances
            HelpMarker("Multiple tables with the same identifier will share their settings, width, visibility, order etc.");

            CheckboxFlags("ImGuiTableFlags_Resizable", ref s_tables_synced_flags, TableFlags.Resizable);
            CheckboxFlags("ImGuiTableFlags_ScrollY", ref s_tables_synced_flags, TableFlags.ScrollY);
            CheckboxFlags("ImGuiTableFlags_SizingFixedFit", ref s_tables_synced_flags, TableFlags.SizingFixedFit);
            CheckboxFlags("ImGuiTableFlags_HighlightHoveredColumn", ref s_tables_synced_flags, TableFlags.HighlightHoveredColumn);
            for (int n = 0; n < 3; n++)
            {
                string buf = $"Synced Table {n}";
                bool open = ImGui.CollapsingHeader(buf, TreeNodeFlags.DefaultOpen);
                if (open && ImGui.BeginTable("Table", 3, s_tables_synced_flags, 0.0f, ImGui.GetTextLineHeightWithSpacing() * 5))
                {
                    ImGui.TableSetupColumn("One");
                    ImGui.TableSetupColumn("Two");
                    ImGui.TableSetupColumn("Three");
                    ImGui.TableHeadersRow();
                    int cell_count = (n == 1) ? 27 : 9; // Make second table have a scrollbar to verify that additional decoration is not affecting column positions.
                    for (int cell = 0; cell < cell_count; cell++)
                    {
                        ImGui.TableNextColumn();
                        ImGui.Text($"this cell {cell}");
                    }
                    ImGui.EndTable();
                }
            }
            ImGui.TreePop();
        }

        // Demonstrate using Sorting facilities
        // This is a simplified version of the "Advanced" example, where we mostly focus on the code necessary to handle sorting.
        // Note that the "Advanced" example also showcase manually triggering a sort (e.g. if item quantities have been modified)
        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Sorting"))
        {
            // DEMO MARKER: Tables/Sorting
            // Create item list
            if (s_tables_sorting_items.Length == 0)
            {
                s_tables_sorting_items = new MyItem[50];
                for (int n = 0; n < s_tables_sorting_items.Length; n++)
                {
                    int template_n = n % s_tables_template_items_names.Length;
                    s_tables_sorting_items[n] = new MyItem
                    {
                        ID = n,
                        Name = s_tables_template_items_names[template_n],
                        Quantity = (n * n - n) % 20, // Assign default quantities
                    };
                }
            }

            // Options
            PushStyleCompact();
            CheckboxFlags("ImGuiTableFlags_SortMulti", ref s_tables_sorting_flags, TableFlags.SortMulti);
            ImGui.SameLine(); HelpMarker("When sorting is enabled: hold shift when clicking headers to sort on multiple column. TableGetSortSpecs() may return specs where (SpecsCount > 1).");
            CheckboxFlags("ImGuiTableFlags_SortTristate", ref s_tables_sorting_flags, TableFlags.SortTristate);
            ImGui.SameLine(); HelpMarker("When sorting is enabled: allow no sorting, disable default sorting. TableGetSortSpecs() may return specs where (SpecsCount == 0).");
            PopStyleCompact();

            if (ImGui.BeginTable("table_sorting", 4, s_tables_sorting_flags, 0.0f, TEXT_BASE_HEIGHT * 15, 0.0f))
            {
                // Declare columns
                // We use the "user_id" parameter of TableSetupColumn() to specify a user id that will be stored in the sort specifications.
                // This is so our sort function can identify a column given our own identifier. We could also identify them based on their index!
                // Demonstrate using a mixture of flags among available sort-related flags:
                // - ImGuiTableColumnFlags_DefaultSort
                // - ImGuiTableColumnFlags_NoSort / ImGuiTableColumnFlags_NoSortAscending / ImGuiTableColumnFlags_NoSortDescending
                // - ImGuiTableColumnFlags_PreferSortAscending / ImGuiTableColumnFlags_PreferSortDescending
                ImGui.TableSetupColumn("ID",       TableColumnFlags.DefaultSort          | TableColumnFlags.WidthFixed,   0.0f, (uint)MyItemColumnId.Id);
                ImGui.TableSetupColumn("Name",                                             TableColumnFlags.WidthFixed,   0.0f, (uint)MyItemColumnId.Name);
                ImGui.TableSetupColumn("Action",   TableColumnFlags.NoSort               | TableColumnFlags.WidthFixed,   0.0f, (uint)MyItemColumnId.Action);
                ImGui.TableSetupColumn("Quantity", TableColumnFlags.PreferSortDescending | TableColumnFlags.WidthStretch, 0.0f, (uint)MyItemColumnId.Quantity);
                ImGui.TableSetupScrollFreeze(0, 1); // Make row always visible
                ImGui.TableHeadersRow();

                // Sort our data if sort specs have been changed!
                TableSortSpecs sort_specs = ImGui.TableGetSortSpecs();
                if (sort_specs.IsValid && sort_specs.SpecsDirty)
                {
                    MyItem.SortWithSortSpecs(sort_specs, s_tables_sorting_items);
                    sort_specs.SpecsDirty = false;
                }

                // Demonstrate using clipper for large vertical lists
                using (var clipper = new ListClipper())
                {
                    clipper.Begin(s_tables_sorting_items.Length);
                    while (clipper.Step())
                        for (int row_n = clipper.DisplayStart; row_n < clipper.DisplayEnd; row_n++)
                        {
                            // Display a data item
                            MyItem item = s_tables_sorting_items[row_n];
                            ImGui.PushID(item.ID);
                            ImGui.TableNextRow();
                            ImGui.TableNextColumn();
                            ImGui.Text($"{item.ID:0000}");
                            ImGui.TableNextColumn();
                            ImGui.TextUnformatted(item.Name);
                            ImGui.TableNextColumn();
                            ImGui.SmallButton("None");
                            ImGui.TableNextColumn();
                            ImGui.Text($"{item.Quantity}");
                            ImGui.PopID();
                        }
                }
                ImGui.EndTable();
            }
            ImGui.TreePop();
        }

        // In this example we'll expose most table flags and settings.
        // For specific flags and settings refer to the corresponding section for more detailed explanation.
        // This section is mostly useful to experiment with combining certain flags or settings with each others.
        //ImGui.SetNextItemOpen(true, Cond.Once); // [DEBUG]
        if (open_action != -1)
            ImGui.SetNextItemOpen(open_action != 0);
        if (ImGui.TreeNode("Advanced"))
        {
            // DEMO MARKER: Tables/Advanced
            const int CT_Text = 0, CT_Button = 1, CT_SmallButton = 2, CT_FillButton = 3, CT_Selectable = 4, CT_SelectableSpanRow = 5;

            if (!s_tables_adv_outer_size_initialized)
            {
                s_tables_adv_outer_size_value[1] = TEXT_BASE_HEIGHT * 12;
                s_tables_adv_outer_size_initialized = true;
            }

            //static ImGuiTextFilter filter;
            //ImGui.SetNextItemOpen(true, Cond.Once); // FIXME-TABLE: Enabling this results in initial clipped first pass on table which tend to affect column sizing
            if (ImGui.TreeNode("Options"))
            {
                // Make the UI compact because there are so many fields
                PushStyleCompact();
                ImGui.PushItemWidth(TEXT_BASE_WIDTH * 28.0f);

                if (ImGui.TreeNodeEx("Features:", TreeNodeFlags.DefaultOpen))
                {
                    CheckboxFlags("ImGuiTableFlags_Resizable", ref s_tables_adv_flags, TableFlags.Resizable);
                    CheckboxFlags("ImGuiTableFlags_Reorderable", ref s_tables_adv_flags, TableFlags.Reorderable);
                    CheckboxFlags("ImGuiTableFlags_Hideable", ref s_tables_adv_flags, TableFlags.Hideable);
                    CheckboxFlags("ImGuiTableFlags_Sortable", ref s_tables_adv_flags, TableFlags.Sortable);
                    CheckboxFlags("ImGuiTableFlags_NoSavedSettings", ref s_tables_adv_flags, TableFlags.NoSavedSettings);
                    CheckboxFlags("ImGuiTableFlags_ContextMenuInBody", ref s_tables_adv_flags, TableFlags.ContextMenuInBody);
                    ImGui.TreePop();
                }

                if (ImGui.TreeNodeEx("Decorations:", TreeNodeFlags.DefaultOpen))
                {
                    CheckboxFlags("ImGuiTableFlags_RowBg", ref s_tables_adv_flags, TableFlags.RowBg);
                    CheckboxFlags("ImGuiTableFlags_BordersV", ref s_tables_adv_flags, TableFlags.BordersV);
                    CheckboxFlags("ImGuiTableFlags_BordersOuterV", ref s_tables_adv_flags, TableFlags.BordersOuterV);
                    CheckboxFlags("ImGuiTableFlags_BordersInnerV", ref s_tables_adv_flags, TableFlags.BordersInnerV);
                    CheckboxFlags("ImGuiTableFlags_BordersH", ref s_tables_adv_flags, TableFlags.BordersH);
                    CheckboxFlags("ImGuiTableFlags_BordersOuterH", ref s_tables_adv_flags, TableFlags.BordersOuterH);
                    CheckboxFlags("ImGuiTableFlags_BordersInnerH", ref s_tables_adv_flags, TableFlags.BordersInnerH);
                    CheckboxFlags("ImGuiTableFlags_NoBordersInBody", ref s_tables_adv_flags, TableFlags.NoBordersInBody); ImGui.SameLine(); HelpMarker("Disable vertical borders in columns Body (borders will always appear in Headers)");
                    CheckboxFlags("ImGuiTableFlags_NoBordersInBodyUntilResize", ref s_tables_adv_flags, TableFlags.NoBordersInBodyUntilResize); ImGui.SameLine(); HelpMarker("Disable vertical borders in columns Body until hovered for resize (borders will always appear in Headers)");
                    ImGui.TreePop();
                }

                if (ImGui.TreeNodeEx("Sizing:", TreeNodeFlags.DefaultOpen))
                {
                    EditTableSizingFlags(ref s_tables_adv_flags);
                    ImGui.SameLine(); HelpMarker("In the Advanced demo we override the policy of each column so those table-wide settings have less effect that typical.");
                    CheckboxFlags("ImGuiTableFlags_NoHostExtendX", ref s_tables_adv_flags, TableFlags.NoHostExtendX);
                    ImGui.SameLine(); HelpMarker("Make outer width auto-fit to columns, overriding outer_size.x value.\n\nOnly available when ScrollX/ScrollY are disabled and Stretch columns are not used.");
                    CheckboxFlags("ImGuiTableFlags_NoHostExtendY", ref s_tables_adv_flags, TableFlags.NoHostExtendY);
                    ImGui.SameLine(); HelpMarker("Make outer height stop exactly at outer_size.y (prevent auto-extending table past the limit).\n\nOnly available when ScrollX/ScrollY are disabled. Data below the limit will be clipped and not visible.");
                    CheckboxFlags("ImGuiTableFlags_NoKeepColumnsVisible", ref s_tables_adv_flags, TableFlags.NoKeepColumnsVisible);
                    ImGui.SameLine(); HelpMarker("Only available if ScrollX is disabled.");
                    CheckboxFlags("ImGuiTableFlags_PreciseWidths", ref s_tables_adv_flags, TableFlags.PreciseWidths);
                    ImGui.SameLine(); HelpMarker("Disable distributing remainder width to stretched columns (width allocation on a 100-wide table with 3 columns: Without this flag: 33,33,34. With this flag: 33,33,33). With larger number of columns, resizing will appear to be less smooth.");
                    CheckboxFlags("ImGuiTableFlags_NoClip", ref s_tables_adv_flags, TableFlags.NoClip);
                    ImGui.SameLine(); HelpMarker("Disable clipping rectangle for every individual columns (reduce draw command count, items will be able to overflow into other columns). Generally incompatible with ScrollFreeze options.");
                    ImGui.TreePop();
                }

                if (ImGui.TreeNodeEx("Padding:", TreeNodeFlags.DefaultOpen))
                {
                    CheckboxFlags("ImGuiTableFlags_PadOuterX", ref s_tables_adv_flags, TableFlags.PadOuterX);
                    CheckboxFlags("ImGuiTableFlags_NoPadOuterX", ref s_tables_adv_flags, TableFlags.NoPadOuterX);
                    CheckboxFlags("ImGuiTableFlags_NoPadInnerX", ref s_tables_adv_flags, TableFlags.NoPadInnerX);
                    ImGui.TreePop();
                }

                if (ImGui.TreeNodeEx("Scrolling:", TreeNodeFlags.DefaultOpen))
                {
                    CheckboxFlags("ImGuiTableFlags_ScrollX", ref s_tables_adv_flags, TableFlags.ScrollX);
                    ImGui.SameLine();
                    ImGui.SetNextItemWidth(ImGui.GetFrameHeight());
                    ImGui.DragInt("freeze_cols", ref s_tables_adv_freeze_cols, 0.2f, 0, 9, null, SliderFlags.NoInput);
                    CheckboxFlags("ImGuiTableFlags_ScrollY", ref s_tables_adv_flags, TableFlags.ScrollY);
                    ImGui.SameLine();
                    ImGui.SetNextItemWidth(ImGui.GetFrameHeight());
                    ImGui.DragInt("freeze_rows", ref s_tables_adv_freeze_rows, 0.2f, 0, 9, null, SliderFlags.NoInput);
                    ImGui.TreePop();
                }

                if (ImGui.TreeNodeEx("Sorting:", TreeNodeFlags.DefaultOpen))
                {
                    CheckboxFlags("ImGuiTableFlags_SortMulti", ref s_tables_adv_flags, TableFlags.SortMulti);
                    ImGui.SameLine(); HelpMarker("When sorting is enabled: hold shift when clicking headers to sort on multiple column. TableGetSortSpecs() may return specs where (SpecsCount > 1).");
                    CheckboxFlags("ImGuiTableFlags_SortTristate", ref s_tables_adv_flags, TableFlags.SortTristate);
                    ImGui.SameLine(); HelpMarker("When sorting is enabled: allow no sorting, disable default sorting. TableGetSortSpecs() may return specs where (SpecsCount == 0).");
                    ImGui.TreePop();
                }

                if (ImGui.TreeNodeEx("Headers:", TreeNodeFlags.DefaultOpen))
                {
                    ImGui.Checkbox("show_headers", ref s_tables_adv_show_headers);
                    CheckboxFlags("ImGuiTableFlags_HighlightHoveredColumn", ref s_tables_adv_flags, TableFlags.HighlightHoveredColumn);
                    CheckboxFlags("ImGuiTableColumnFlags_AngledHeader", ref s_tables_adv_columns_base_flags, TableColumnFlags.AngledHeader);
                    ImGui.SameLine(); HelpMarker("Enable AngledHeader on all columns. Best enabled on selected narrow columns (see \"Angled headers\" section of the demo).");
                    ImGui.TreePop();
                }

                if (ImGui.TreeNodeEx("Other:", TreeNodeFlags.DefaultOpen))
                {
                    ImGui.Checkbox("show_wrapped_text", ref s_tables_adv_show_wrapped_text);

                    ImGui.DragFloat2("##OuterSize", s_tables_adv_outer_size_value);
                    ImGui.SameLine(0.0f, Style.ItemInnerSpacing.X);
                    ImGui.Checkbox("outer_size", ref s_tables_adv_outer_size_enabled);
                    ImGui.SameLine();
                    HelpMarker("If scrolling is disabled (ScrollX and ScrollY not set):\n"
                        + "- The table is output directly in the parent window.\n"
                        + "- OuterSize.x < 0.0f will right-align the table.\n"
                        + "- OuterSize.x = 0.0f will narrow fit the table unless there are any Stretch columns.\n"
                        + "- OuterSize.y then becomes the minimum size for the table, which will extend vertically if there are more rows (unless NoHostExtendY is set).");

                    // From a user point of view we will tend to use 'inner_width' differently depending on whether our table is embedding scrolling.
                    // To facilitate toying with this demo we will actually pass 0.0f to the BeginTable() when ScrollX is disabled.
                    ImGui.DragFloat("inner_width (when ScrollX active)", ref s_tables_adv_inner_width_with_scroll, 1.0f, 0.0f, float.MaxValue);

                    ImGui.DragFloat("row_min_height", ref s_tables_adv_row_min_height, 1.0f, 0.0f, float.MaxValue);
                    ImGui.SameLine(); HelpMarker("Specify height of the Selectable item.");

                    ImGui.DragInt("items_count", ref s_tables_adv_items_count, 0.1f, 0, 9999);
                    ImGui.Combo("items_type (first column)", ref s_tables_adv_contents_type, s_tables_adv_contents_type_names);
                    //filter.Draw("filter");
                    ImGui.TreePop();
                }

                ImGui.PopItemWidth();
                PopStyleCompact();
                ImGui.Spacing();
                ImGui.TreePop();
            }

            // Update item list if we changed the number of items
            if (s_tables_adv_items.Length != s_tables_adv_items_count)
            {
                s_tables_adv_items = new MyItem[s_tables_adv_items_count];
                for (int n = 0; n < s_tables_adv_items_count; n++)
                {
                    int template_n = n % s_tables_template_items_names.Length;
                    s_tables_adv_items[n] = new MyItem
                    {
                        ID = n,
                        Name = s_tables_template_items_names[template_n],
                        Quantity = (template_n == 3) ? 10 : (template_n == 4) ? 20 : 0, // Assign default quantities
                    };
                }
            }

            DrawList parent_draw_list = ImGui.GetWindowDrawList();
            int parent_draw_list_draw_cmd_count = parent_draw_list.CmdBufferSize;
            (float X, float Y) table_scroll_cur = default, table_scroll_max = default; // For debug display
            DrawList table_draw_list = default;                                        // "

            // Submit table
            float inner_width_to_use = (s_tables_adv_flags & TableFlags.ScrollX) != 0 ? s_tables_adv_inner_width_with_scroll : 0.0f;
            float outer_size_x = s_tables_adv_outer_size_enabled ? s_tables_adv_outer_size_value[0] : 0.0f;
            float outer_size_y = s_tables_adv_outer_size_enabled ? s_tables_adv_outer_size_value[1] : 0.0f;
            if (ImGui.BeginTable("table_advanced", 6, s_tables_adv_flags, outer_size_x, outer_size_y, inner_width_to_use))
            {
                // Declare columns
                // We use the "user_id" parameter of TableSetupColumn() to specify a user id that will be stored in the sort specifications.
                // This is so our sort function can identify a column given our own identifier. We could also identify them based on their index!
                ImGui.TableSetupColumn("ID",          s_tables_adv_columns_base_flags | TableColumnFlags.DefaultSort | TableColumnFlags.WidthFixed | TableColumnFlags.NoHide, 0.0f, (uint)MyItemColumnId.Id);
                ImGui.TableSetupColumn("Name",        s_tables_adv_columns_base_flags | TableColumnFlags.WidthFixed, 0.0f, (uint)MyItemColumnId.Name);
                ImGui.TableSetupColumn("Action",      s_tables_adv_columns_base_flags | TableColumnFlags.NoSort | TableColumnFlags.WidthFixed, 0.0f, (uint)MyItemColumnId.Action);
                ImGui.TableSetupColumn("Quantity",    s_tables_adv_columns_base_flags | TableColumnFlags.PreferSortDescending, 0.0f, (uint)MyItemColumnId.Quantity);
                ImGui.TableSetupColumn("Description", s_tables_adv_columns_base_flags | ((s_tables_adv_flags & TableFlags.NoHostExtendX) != 0 ? TableColumnFlags.None : TableColumnFlags.WidthStretch), 0.0f, (uint)MyItemColumnId.Description);
                ImGui.TableSetupColumn("Hidden",      s_tables_adv_columns_base_flags | TableColumnFlags.DefaultHide | TableColumnFlags.NoSort);
                ImGui.TableSetupScrollFreeze(s_tables_adv_freeze_cols, s_tables_adv_freeze_rows);

                // Sort our data if sort specs have been changed!
                TableSortSpecs sort_specs = ImGui.TableGetSortSpecs();
                if (sort_specs.IsValid && sort_specs.SpecsDirty)
                    s_tables_adv_items_need_sort = true;
                if (sort_specs.IsValid && s_tables_adv_items_need_sort && s_tables_adv_items.Length > 1)
                {
                    MyItem.SortWithSortSpecs(sort_specs, s_tables_adv_items);
                    sort_specs.SpecsDirty = false;
                }
                s_tables_adv_items_need_sort = false;

                // Take note of whether we are currently sorting based on the Quantity field,
                // we will use this to trigger sorting when we know the data of this column has been modified.
                bool sorts_specs_using_quantity = (ImGui.TableGetColumnFlags(3) & TableColumnFlags.IsSorted) != 0;

                // Show headers
                if (s_tables_adv_show_headers && (s_tables_adv_columns_base_flags & TableColumnFlags.AngledHeader) != 0)
                    ImGui.TableAngledHeadersRow();
                if (s_tables_adv_show_headers)
                    ImGui.TableHeadersRow();

                // Show data
                // FIXME-TABLE FIXME-NAV: How we can get decent up/down even though we have the buttons here?
                // Demonstrate using clipper for large vertical lists
                using (var clipper = new ListClipper())
                {
                    clipper.Begin(s_tables_adv_items.Length);
                    while (clipper.Step())
                    {
                        for (int row_n = clipper.DisplayStart; row_n < clipper.DisplayEnd; row_n++)
                        {
                            MyItem item = s_tables_adv_items[row_n];
                            //if (!filter.PassFilter(item.Name))
                            //    continue;

                            bool item_is_selected = s_tables_adv_selection.Contains(item.ID);
                            ImGui.PushID(item.ID);
                            ImGui.TableNextRow(TableRowFlags.None, s_tables_adv_row_min_height);

                            // For the demo purpose we can select among different type of items submitted in the first column
                            ImGui.TableSetColumnIndex(0);
                            string label = $"{item.ID:0000}";
                            if (s_tables_adv_contents_type == CT_Text)
                                ImGui.TextUnformatted(label);
                            else if (s_tables_adv_contents_type == CT_Button)
                                ImGui.Button(label);
                            else if (s_tables_adv_contents_type == CT_SmallButton)
                                ImGui.SmallButton(label);
                            else if (s_tables_adv_contents_type == CT_FillButton)
                                ImGui.Button(label, -float.Epsilon, 0.0f);
                            else if (s_tables_adv_contents_type == CT_Selectable || s_tables_adv_contents_type == CT_SelectableSpanRow)
                            {
                                SelectableFlags selectable_flags = (s_tables_adv_contents_type == CT_SelectableSpanRow)
                                    ? SelectableFlags.SpanAllColumns | SelectableFlags.AllowOverlap
                                    : SelectableFlags.None;
                                if (ImGui.Selectable(label, item_is_selected, selectable_flags, 0, s_tables_adv_row_min_height))
                                {
                                    if (Io.KeyCtrl)
                                    {
                                        if (item_is_selected)
                                            s_tables_adv_selection.Remove(item.ID);
                                        else
                                            s_tables_adv_selection.Add(item.ID);
                                    }
                                    else
                                    {
                                        s_tables_adv_selection.Clear();
                                        s_tables_adv_selection.Add(item.ID);
                                    }
                                }
                            }

                            if (ImGui.TableSetColumnIndex(1))
                                ImGui.TextUnformatted(item.Name);

                            // Here we demonstrate marking our data set as needing to be sorted again if we modified a quantity,
                            // and we are currently sorting on the column showing the Quantity.
                            // To avoid triggering a sort while holding the button, we only trigger it when the button has been released.
                            // You will probably need some extra logic if you want to automatically sort when a specific entry changes.
                            if (ImGui.TableSetColumnIndex(2))
                            {
                                if (ImGui.SmallButton("Chop")) { item.Quantity += 1; }
                                if (sorts_specs_using_quantity && ImGui.IsItemDeactivated()) { s_tables_adv_items_need_sort = true; }
                                ImGui.SameLine();
                                if (ImGui.SmallButton("Eat")) { item.Quantity -= 1; }
                                if (sorts_specs_using_quantity && ImGui.IsItemDeactivated()) { s_tables_adv_items_need_sort = true; }
                            }

                            if (ImGui.TableSetColumnIndex(3))
                                ImGui.Text($"{item.Quantity}");

                            ImGui.TableSetColumnIndex(4);
                            if (s_tables_adv_show_wrapped_text)
                                ImGui.TextWrapped("Lorem ipsum dolor sit amet");
                            else
                                ImGui.Text("Lorem ipsum dolor sit amet");

                            if (ImGui.TableSetColumnIndex(5))
                                ImGui.Text("1234");

                            ImGui.PopID();
                        }
                    }
                }

                // Store some info to display debug details below
                table_scroll_cur = (ImGui.GetScrollX(), ImGui.GetScrollY());
                table_scroll_max = (ImGui.GetScrollMaxX(), ImGui.GetScrollMaxY());
                table_draw_list = ImGui.GetWindowDrawList();
                ImGui.EndTable();
            }
            ImGui.Checkbox("Debug details", ref s_tables_adv_show_debug_details);
            if (s_tables_adv_show_debug_details && table_draw_list.IsValid)
            {
                ImGui.SameLine(0.0f, 0.0f);
                int table_draw_list_draw_cmd_count = table_draw_list.CmdBufferSize;
                if (table_draw_list.Equals(parent_draw_list))
                    ImGui.Text($": DrawCmd: +{table_draw_list_draw_cmd_count - parent_draw_list_draw_cmd_count} (in same window)");
                else
                    ImGui.Text($": DrawCmd: +{table_draw_list_draw_cmd_count - 1} (in child window), Scroll: ({table_scroll_cur.X:F0}/{table_scroll_max.X:F0}) ({table_scroll_cur.Y:F0}/{table_scroll_max.Y:F0})");
            }
            ImGui.TreePop();
        }

        ImGui.PopID();

        DemoWindowColumns();

        if (s_tables_disable_indent)
            ImGui.PopStyleVar();
    }

    // -------------------------------------------------------------------------
    // SECTION: DemoWindowColumns() static state (C++ function-static locals)
    // -------------------------------------------------------------------------

    static int s_columns_basic_selected = -1;
    static bool s_columns_borders_h_borders = true;
    static bool s_columns_borders_v_borders = true;
    static int s_columns_borders_columns_count = 4;
    static float s_columns_mixed_foo = 1.0f;
    static float s_columns_mixed_bar = 1.0f;

    // -------------------------------------------------------------------------
    // SECTION: DemoWindowColumns()
    // -------------------------------------------------------------------------

    // Demonstrate old/legacy Columns API!
    // [2020: Columns are under-featured and not maintained. Prefer using the more flexible and powerful BeginTable() API!]
    private static void DemoWindowColumns()
    {
        bool open = ImGui.TreeNode("Legacy Columns API");
        ImGui.SameLine();
        HelpMarker("Columns() is an old API! Prefer using the more flexible and powerful BeginTable() API!");
        if (!open)
            return;

        // Basic columns
        if (ImGui.TreeNode("Basic"))
        {
            // DEMO MARKER: Columns (legacy API)/Basic
            ImGui.Text("Without border:");
            ImGui.Columns(3, "mycolumns3", false);  // 3-ways, no border
            ImGui.Separator();
            for (int n = 0; n < 14; n++)
            {
                string label = $"Item {n}";
                if (ImGui.Selectable(label)) { }
                //if (ImGui.Button(label, -float.Epsilon, 0.0f)) { }
                ImGui.NextColumn();
            }
            ImGui.Columns(1);
            ImGui.Separator();

            ImGui.Text("With border:");
            ImGui.Columns(4, "mycolumns"); // 4-ways, with border
            ImGui.Separator();
            ImGui.Text("ID"); ImGui.NextColumn();
            ImGui.Text("Name"); ImGui.NextColumn();
            ImGui.Text("Path"); ImGui.NextColumn();
            ImGui.Text("Hovered"); ImGui.NextColumn();
            ImGui.Separator();
            string[] names = ["One", "Two", "Three"];
            string[] paths = ["/path/one", "/path/two", "/path/three"];
            for (int i = 0; i < 3; i++)
            {
                string label = $"{i:0000}";
                if (ImGui.Selectable(label, s_columns_basic_selected == i, SelectableFlags.SpanAllColumns))
                    s_columns_basic_selected = i;
                bool hovered = ImGui.IsItemHovered();
                ImGui.NextColumn();
                ImGui.Text(names[i]); ImGui.NextColumn();
                ImGui.Text(paths[i]); ImGui.NextColumn();
                ImGui.Text($"{(hovered ? 1 : 0)}"); ImGui.NextColumn();
            }
            ImGui.Columns(1);
            ImGui.Separator();
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Borders"))
        {
            // DEMO MARKER: Columns (legacy API)/Borders
            // NB: Future columns API should allow automatic horizontal borders.
            const int lines_count = 3;
            ImGui.SetNextItemWidth(ImGui.GetFontSize() * 8);
            ImGui.DragInt("##columns_count", ref s_columns_borders_columns_count, 0.1f, 2, 10, "%d columns");
            if (s_columns_borders_columns_count < 2)
                s_columns_borders_columns_count = 2;
            ImGui.SameLine();
            ImGui.Checkbox("horizontal", ref s_columns_borders_h_borders);
            ImGui.SameLine();
            ImGui.Checkbox("vertical", ref s_columns_borders_v_borders);
            ImGui.Columns(s_columns_borders_columns_count, null, s_columns_borders_v_borders);
            for (int i = 0; i < s_columns_borders_columns_count * lines_count; i++)
            {
                if (s_columns_borders_h_borders && ImGui.GetColumnIndex() == 0)
                    ImGui.Separator();
                ImGui.PushID(i);
                ImGui.Text($"{(char)('a' + i)}{(char)('a' + i)}{(char)('a' + i)}");
                ImGui.Text($"Width {ImGui.GetColumnWidth():F2}");
                ImGui.Text($"Avail {ImGui.GetContentRegionAvail().Width:F2}");
                ImGui.Text($"Offset {ImGui.GetColumnOffset():F2}");
                ImGui.Text("Long text that is likely to clip");
                ImGui.Button("Button", -float.Epsilon, 0.0f);
                ImGui.PopID();
                ImGui.NextColumn();
            }
            ImGui.Columns(1);
            if (s_columns_borders_h_borders)
                ImGui.Separator();
            ImGui.TreePop();
        }

        // Create multiple items in a same cell before switching to next column
        if (ImGui.TreeNode("Mixed items"))
        {
            // DEMO MARKER: Columns (legacy API)/Mixed items
            ImGui.Columns(3, "mixed");
            ImGui.Separator();

            ImGui.Text("Hello");
            ImGui.Button("Banana");
            ImGui.NextColumn();

            ImGui.Text("ImGui");
            ImGui.Button("Apple");
            ImGui.InputFloat("red", ref s_columns_mixed_foo, 0.05f, 0, "%.3f");
            ImGui.Text("An extra line here.");
            ImGui.NextColumn();

            ImGui.Text("Sailor");
            ImGui.Button("Corniflower");
            ImGui.InputFloat("blue", ref s_columns_mixed_bar, 0.05f, 0, "%.3f");
            ImGui.NextColumn();

            if (ImGui.CollapsingHeader("Category A")) { ImGui.Text("Blah blah blah"); }
            ImGui.NextColumn();
            if (ImGui.CollapsingHeader("Category B")) { ImGui.Text("Blah blah blah"); }
            ImGui.NextColumn();
            if (ImGui.CollapsingHeader("Category C")) { ImGui.Text("Blah blah blah"); }
            ImGui.NextColumn();
            ImGui.Columns(1);
            ImGui.Separator();
            ImGui.TreePop();
        }

        // Word wrapping
        if (ImGui.TreeNode("Word-wrapping"))
        {
            // DEMO MARKER: Columns (legacy API)/Word-wrapping
            ImGui.Columns(2, "word-wrapping");
            ImGui.Separator();
            ImGui.TextWrapped("The quick brown fox jumps over the lazy dog.");
            ImGui.TextWrapped("Hello Left");
            ImGui.NextColumn();
            ImGui.TextWrapped("The quick brown fox jumps over the lazy dog.");
            ImGui.TextWrapped("Hello Right");
            ImGui.Columns(1);
            ImGui.Separator();
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Horizontal Scrolling"))
        {
            // DEMO MARKER: Columns (legacy API)/Horizontal Scrolling
            ImGui.SetNextWindowContentSize(1500.0f, 0.0f);
            ImGui.BeginChild("##ScrollingRegion", 0, ImGui.GetFontSize() * 20.0f, ChildFlags.None, WindowFlags.HorizontalScrollbar);
            ImGui.Columns(10);

            // Also demonstrate using clipper for large vertical lists
            const int ITEMS_COUNT = 2000;
            using (var clipper = new ListClipper())
            {
                clipper.Begin(ITEMS_COUNT);
                while (clipper.Step())
                {
                    for (int i = clipper.DisplayStart; i < clipper.DisplayEnd; i++)
                        for (int j = 0; j < 10; j++)
                        {
                            ImGui.Text($"Line {i} Column {j}...");
                            ImGui.NextColumn();
                        }
                }
            }
            ImGui.Columns(1);
            ImGui.EndChild();
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Tree"))
        {
            // DEMO MARKER: Columns (legacy API)/Tree
            ImGui.Columns(2, "tree", true);
            for (int x = 0; x < 3; x++)
            {
                bool open1 = ImGui.TreeNode((nint)x, $"Node{x}");
                ImGui.NextColumn();
                ImGui.Text("Node contents");
                ImGui.NextColumn();
                if (open1)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        bool open2 = ImGui.TreeNode((nint)y, $"Node{x}.{y}");
                        ImGui.NextColumn();
                        ImGui.Text("Node contents");
                        if (open2)
                        {
                            ImGui.Text("Even more contents");
                            if (ImGui.TreeNode("Tree in column"))
                            {
                                ImGui.Text("The quick brown fox jumps over the lazy dog");
                                ImGui.TreePop();
                            }
                        }
                        ImGui.NextColumn();
                        if (open2)
                            ImGui.TreePop();
                    }
                    ImGui.TreePop();
                }
            }
            ImGui.Columns(1);
            ImGui.TreePop();
        }

        ImGui.TreePop();
    }
}
