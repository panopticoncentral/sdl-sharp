// C# port of imgui_demo.cpp's example apps, part A (upstream lines ~8770-9787 + helpers ~736-830):
//   - ShowExampleAppMainMenuBar() / ShowExampleMenuFile()
//   - ShowExampleAppConsole()          (ExampleAppConsole)
//   - ShowExampleAppLog()              (ExampleAppLog)
//   - ShowExampleAppLayout()
//   - ShowExampleAppPropertyEditor()   (ExampleTreeNode / ExampleMemberInfo helpers + ExampleAppPropertyEditor)
//   - ShowExampleAppLongText()

using System.Text;
using SdlSharp.ImGui;

namespace ImGuiDemo;

//-----------------------------------------------------------------------------
// [SECTION] Helpers: ExampleTreeNode, ExampleMemberInfo (for use by Property Editor & Multi-Select demos)
//-----------------------------------------------------------------------------

// Simple representation for a tree
// (this is designed to be simple to understand for our demos, not to be fancy or efficient etc.)
internal sealed class ExampleTreeNode
{
    // Tree structure
    public string Name = "";
    public int UID;
    public ExampleTreeNode? Parent;
    public readonly List<ExampleTreeNode> Childs = new();
    public int IndexInParent;   // Maintaining this allows us to implement linear traversal more easily

    // Leaf Data
    public bool HasData;        // All leaves have data
    public bool DataMyBool = true;
    public int DataMyInt = 128;
    public readonly float[] DataMyVec2 = { 0.0f, 3.141592f };
}

// Simple representation of struct metadata/serialization data.
// (this is a minimal version of what a typical advanced application may provide)
// (unlike the C++ demo we cannot use offsetof() to locate the member, so the property
//  editor maps each entry back to the corresponding ExampleTreeNode member by type)
internal sealed record ExampleMemberInfo(string Name, DataType DataType, int DataCount);

internal static unsafe partial class DemoWindow
{
    // Metadata description of ExampleTreeNode struct.
    private static readonly ExampleMemberInfo[] ExampleTreeNodeMemberInfos =
    {
        new("MyName", DataType.String, 1),
        new("MyBool", DataType.Bool,   1),
        new("MyInt",  DataType.S32,    1),
        new("MyVec2", DataType.Float,  2),
    };

    private static ExampleTreeNode ExampleTree_CreateNode(string name, int uid, ExampleTreeNode? parent)
    {
        var node = new ExampleTreeNode
        {
            Name = name,
            UID = uid,
            Parent = parent,
            IndexInParent = parent?.Childs.Count ?? 0,
        };
        parent?.Childs.Add(node);
        return node;
    }

    // Create example tree data
    // (warning: this can allocates MANY MANY more times than other code in all of Dear ImGui + demo combined)
    // (a real application managing one million nodes would likely store its tree data differently)
    internal static ExampleTreeNode ExampleTree_CreateDemoTree()
    {
        const int ROOT_ITEMS_COUNT = 20;

        string[] category_names = { "Apple", "Banana", "Cherry", "Kiwi", "Mango", "Orange", "Pear", "Pineapple", "Strawberry", "Watermelon" };
        int category_count = category_names.Length;
        int uid = 0;
        ExampleTreeNode node_L0 = ExampleTree_CreateNode("<ROOT>", ++uid, null);
        for (int idx_L0 = 0; idx_L0 < ROOT_ITEMS_COUNT; idx_L0++)
        {
            string name_buf = $"{category_names[idx_L0 / (ROOT_ITEMS_COUNT / category_count)]} {idx_L0 % (ROOT_ITEMS_COUNT / category_count)}";
            ExampleTreeNode node_L1 = ExampleTree_CreateNode(name_buf, ++uid, node_L0);
            int number_of_childs = node_L1.Name.Length;
            for (int idx_L1 = 0; idx_L1 < number_of_childs; idx_L1++)
            {
                ExampleTreeNode node_L2 = ExampleTree_CreateNode($"Child {idx_L1}", ++uid, node_L1);
                node_L2.HasData = true;
                if (idx_L1 == 0)
                {
                    ExampleTreeNode node_L3 = ExampleTree_CreateNode($"Sub-child {0}", ++uid, node_L2);
                    node_L3.HasData = true;
                }
            }
        }
        return node_L0;
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Main Menu Bar / ShowExampleAppMainMenuBar()
    //-----------------------------------------------------------------------------

    private static void ShowExampleAppMainMenuBar()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("File"))
            {
                // DEMO MARKER: Menu/File
                ShowExampleMenuFile();
                ImGui.EndMenu();
            }
            if (ImGui.BeginMenu("Edit"))
            {
                // DEMO MARKER: Menu/Edit
                if (ImGui.MenuItem("Undo", "Ctrl+Z")) { }
                if (ImGui.MenuItem("Redo", "Ctrl+Y", false, false)) { } // Disabled item
                ImGui.Separator();
                if (ImGui.MenuItem("Cut", "Ctrl+X")) { }
                if (ImGui.MenuItem("Copy", "Ctrl+C")) { }
                if (ImGui.MenuItem("Paste", "Ctrl+V")) { }
                ImGui.EndMenu();
            }
            ImGui.EndMainMenuBar();
        }
    }

    // "Options" menu state (C++ function-static locals)
    static bool s_menu_file_enabled = true;
    static float s_menu_file_f = 0.5f;
    static int s_menu_file_n;
    static bool s_menu_file_b = true;

    // Note that shortcuts are currently provided for display only
    // (future version will add explicit flags to BeginMenu() to request processing shortcuts)
    private static void ShowExampleMenuFile()
    {
        // DEMO MARKER: Examples/Menu
        ImGui.MenuItem("(demo menu)", null, false, false);
        if (ImGui.MenuItem("New")) { }
        if (ImGui.MenuItem("Open", "Ctrl+O")) { }
        if (ImGui.BeginMenu("Open Recent"))
        {
            ImGui.MenuItem("fish_hat.c");
            ImGui.MenuItem("fish_hat.inl");
            ImGui.MenuItem("fish_hat.h");
            if (ImGui.BeginMenu("More.."))
            {
                ImGui.MenuItem("Hello");
                ImGui.MenuItem("Sailor");
                if (ImGui.BeginMenu("Recurse.."))
                {
                    ShowExampleMenuFile();
                    ImGui.EndMenu();
                }
                ImGui.EndMenu();
            }
            ImGui.EndMenu();
        }
        if (ImGui.MenuItem("Save", "Ctrl+S")) { }
        if (ImGui.MenuItem("Save As..")) { }

        ImGui.Separator();
        if (ImGui.BeginMenu("Options"))
        {
            // DEMO MARKER: Examples/Menu/Options
            ImGui.MenuItem("Enabled", "", ref s_menu_file_enabled);
            ImGui.BeginChild("child", 0, 60, ChildFlags.Borders);
            for (int i = 0; i < 10; i++)
                ImGui.Text($"Scrolling Text {i}");
            ImGui.EndChild();
            ImGui.SliderFloat("Value", ref s_menu_file_f, 0.0f, 1.0f);
            ImGui.InputFloat("Input", ref s_menu_file_f, 0.1f);
            ImGui.Combo("Combo", ref s_menu_file_n, new[] { "Yes", "No", "Maybe" });
            ImGui.EndMenu();
        }

        if (ImGui.BeginMenu("Colors"))
        {
            // DEMO MARKER: Examples/Menu/Colors
            float sz = ImGui.GetTextLineHeight();
            foreach (Col col in Enum.GetValues<Col>())
            {
                string name = ImGui.GetStyleColorName(col) ?? "";
                var p = ImGui.GetCursorScreenPos();
                ImGui.GetWindowDrawList().AddRectFilled(new Vec2(p.X, p.Y), new Vec2(p.X + sz, p.Y + sz), ImGui.GetColorU32(col));
                ImGui.Dummy(sz, sz);
                ImGui.SameLine();
                ImGui.MenuItem(name);
            }
            ImGui.EndMenu();
        }

        // Here we demonstrate appending again to the "Options" menu (which we already created above)
        // Of course in this demo it is a little bit silly that this function calls BeginMenu("Options") twice.
        // In a real code-base using it would make senses to use this feature from very different code locations.
        if (ImGui.BeginMenu("Options")) // <-- Append!
        {
            // DEMO MARKER: Examples/Menu/Append to an existing menu
            ImGui.Checkbox("SomeOption", ref s_menu_file_b);
            ImGui.EndMenu();
        }

        if (ImGui.BeginMenu("Disabled", false)) // Disabled
        {
            ImGui.EndMenu(); // (unreachable — upstream asserts here)
        }
        if (ImGui.MenuItem("Checked", null, true)) { }
        ImGui.Separator();
        if (ImGui.MenuItem("Quit", "Alt+F4")) { }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Debug Console / ShowExampleAppConsole()
    //-----------------------------------------------------------------------------

    // Demonstrate creating a simple console window, with scrolling, filtering, completion and history.
    // For the console example, we are using a more C++ like approach of declaring a class to hold both data and functions.
    private sealed class ExampleAppConsole
    {
        private readonly byte[] InputBuf = new byte[256];
        private readonly List<string> Items = new();
        private readonly string[] Commands;
        private readonly List<string> History = new();
        private int HistoryPos;    // -1: new line, 0..History.Count-1 browsing history.
        private readonly TextFilter Filter = new();
        private bool AutoScroll;
        private bool ScrollToBottom;

        public ExampleAppConsole()
        {
            ClearLog();
            HistoryPos = -1;

            // "CLASSIFY" is here to provide the test case where "C"+[tab] completes to "CL" and display multiple matches.
            Commands = new[] { "HELP", "HISTORY", "CLEAR", "CLASSIFY" };
            AutoScroll = true;
            ScrollToBottom = false;
            AddLog("Welcome to Dear ImGui!");
        }

        public void ClearLog()
        {
            Items.Clear();
        }

        public void AddLog(string text)
        {
            Items.Add(text);
        }

        public void Draw(string title, ref bool open)
        {
            ImGui.SetNextWindowSize(520, 600, Cond.FirstUseEver);
            if (!ImGui.Begin(title, ref open))
            {
                ImGui.End();
                return;
            }
            // DEMO MARKER: Examples/Console

            // As a specific feature guaranteed by the library, after calling Begin() the last Item represent the title bar.
            // So e.g. IsItemHovered() will return true when hovering the title bar.
            // Here we create a context menu only available from the title bar.
            if (ImGui.BeginPopupContextItem())
            {
                if (ImGui.MenuItem("Close Console"))
                    open = false;
                ImGui.EndPopup();
            }

            ImGui.TextWrapped(
                "This example implements a console with basic coloring, completion (TAB key) and history (Up/Down keys). A more elaborate " +
                "implementation may want to store entries along with extra data such as timestamp, emitter, etc.");
            ImGui.TextWrapped("Enter 'HELP' for help.");

            // TODO: display items starting from the bottom

            if (ImGui.SmallButton("Add Debug Text")) { AddLog($"{Items.Count} some text"); AddLog("some more text"); AddLog("display very important message here!"); }
            ImGui.SameLine();
            if (ImGui.SmallButton("Add Debug Error")) { AddLog("[error] something went wrong"); }
            ImGui.SameLine();
            if (ImGui.SmallButton("Clear")) { ClearLog(); }
            ImGui.SameLine();
            bool copy_to_clipboard = ImGui.SmallButton("Copy");

            ImGui.Separator();

            // Options menu
            if (ImGui.BeginPopup("Options"))
            {
                ImGui.Checkbox("Auto-scroll", ref AutoScroll);
                ImGui.EndPopup();
            }

            // Options, Filter
            ImGui.SetNextItemShortcut(Key.ModCtrl | Key.O, InputFlags.Tooltip);
            if (ImGui.Button("Options"))
                ImGui.OpenPopup("Options");
            ImGui.SameLine();
            Filter.Draw("Filter (\"incl,-excl\") (\"error\")", 180);
            ImGui.Separator();

            // Reserve enough left-over height for 1 separator + 1 input text
            float footer_height_to_reserve = Style.SeparatorSize + Style.ItemSpacing.Y + ImGui.GetFrameHeightWithSpacing();
            if (ImGui.BeginChild("ScrollingRegion", 0, -footer_height_to_reserve, ChildFlags.NavFlattened, WindowFlags.HorizontalScrollbar))
            {
                if (ImGui.BeginPopupContextWindow())
                {
                    if (ImGui.Selectable("Clear")) ClearLog();
                    ImGui.EndPopup();
                }

                // Display every line as a separate entry so we can change their color or add custom widgets.
                // If you only want raw text you can use a single call to TextUnformatted() with the whole buffer.
                // NB- if you have thousands of entries this approach may be too inefficient and may require user-side clipping
                // to only process visible items. The clipper will automatically measure the height of your first item and then
                // "seek" to display only items in the visible area. See ShowExampleAppLog() for a clipped display example.
                // You cannot use the clipper as-is if a filter is active because it breaks the 'cheap random-access' property:
                // we would need random-access on the post-filtered list. A typical application wanting coarse clipping and
                // filtering may want to pre-compute an array of indices of items that passed the filtering test.
                ImGui.PushStyleVar(StyleVar.ItemSpacing, 4, 1); // Tighten spacing
                if (copy_to_clipboard)
                    ImGui.LogToClipboard();
                foreach (string item in Items)
                {
                    if (!Filter.PassFilter(item))
                        continue;

                    // Normally you would store more information in your item than just a string.
                    // (e.g. make Items[] an array of structure, store color/type etc.)
                    bool has_color = false;
                    if (item.Contains("[error]")) { ImGui.PushStyleColor(Col.Text, 1.0f, 0.4f, 0.4f, 1.0f); has_color = true; }
                    else if (item.StartsWith("# ")) { ImGui.PushStyleColor(Col.Text, 1.0f, 0.8f, 0.6f, 1.0f); has_color = true; }
                    ImGui.TextUnformatted(item);
                    if (has_color)
                        ImGui.PopStyleColor();
                }
                if (copy_to_clipboard)
                    ImGui.LogFinish();

                // Keep up at the bottom of the scroll region if we were already at the bottom at the beginning of the frame.
                // Using a scrollbar or mouse-wheel will take away from the bottom edge.
                if (ScrollToBottom || (AutoScroll && ImGui.GetScrollY() >= ImGui.GetScrollMaxY()))
                    ImGui.SetScrollHereY(1.0f);
                ScrollToBottom = false;

                ImGui.PopStyleVar();
            }
            ImGui.EndChild();
            ImGui.Separator();

            // Command-line
            bool reclaim_focus = false;
            var input_text_flags = InputTextFlags.EnterReturnsTrue | InputTextFlags.EscapeClearsAll | InputTextFlags.CallbackCompletion | InputTextFlags.CallbackHistory;
            if (ImGui.InputText("Input", InputBuf, input_text_flags, TextEditCallback))
            {
                string s = GetInputBufText().TrimEnd(' ');
                if (s.Length > 0)
                    ExecCommand(s);
                Array.Clear(InputBuf);
                reclaim_focus = true;
            }

            // Auto-focus on window apparition
            ImGui.SetItemDefaultFocus();
            if (reclaim_focus)
                ImGui.SetKeyboardFocusHere(-1); // Auto focus previous widget

            ImGui.End();
        }

        private string GetInputBufText()
        {
            int len = Array.IndexOf(InputBuf, (byte)0);
            if (len < 0)
                len = InputBuf.Length;
            return Encoding.UTF8.GetString(InputBuf, 0, len);
        }

        private void ExecCommand(string command_line)
        {
            AddLog($"# {command_line}\n");

            // Insert into history. First find match and delete it so it can be pushed to the back.
            // This isn't trying to be smart or optimal.
            HistoryPos = -1;
            for (int i = History.Count - 1; i >= 0; i--)
                if (string.Equals(History[i], command_line, StringComparison.OrdinalIgnoreCase))
                {
                    History.RemoveAt(i);
                    break;
                }
            History.Add(command_line);

            // Process command
            if (string.Equals(command_line, "CLEAR", StringComparison.OrdinalIgnoreCase))
            {
                ClearLog();
            }
            else if (string.Equals(command_line, "HELP", StringComparison.OrdinalIgnoreCase))
            {
                AddLog("Commands:");
                for (int i = 0; i < Commands.Length; i++)
                    AddLog($"- {Commands[i]}");
            }
            else if (string.Equals(command_line, "HISTORY", StringComparison.OrdinalIgnoreCase))
            {
                int first = History.Count - 10;
                for (int i = first > 0 ? first : 0; i < History.Count; i++)
                    AddLog($"{i,3}: {History[i]}\n");
            }
            else
            {
                AddLog($"Unknown command: '{command_line}'\n");
            }

            // On command input, we scroll to bottom even if AutoScroll==false
            ScrollToBottom = true;
        }

        private int TextEditCallback(InputTextCallbackData data)
        {
            //AddLog($"cursor: {data.CursorPos}, selection: {data.SelectionStart}-{data.SelectionEnd}");
            switch (data.EventFlag)
            {
                case InputTextFlags.CallbackCompletion:
                {
                    // Example of TEXT COMPLETION

                    // Locate beginning of current word (positions are UTF-8 byte offsets into data.Buf)
                    Span<byte> buf = data.Buf;
                    int word_end = data.CursorPos;
                    int word_start = word_end;
                    while (word_start > 0)
                    {
                        byte c = buf[word_start - 1];
                        if (c == (byte)' ' || c == (byte)'\t' || c == (byte)',' || c == (byte)';')
                            break;
                        word_start--;
                    }
                    string word = Encoding.UTF8.GetString(buf.Slice(word_start, word_end - word_start));

                    // Build a list of candidates
                    var candidates = new List<string>();
                    for (int i = 0; i < Commands.Length; i++)
                        if (Commands[i].StartsWith(word, StringComparison.OrdinalIgnoreCase))
                            candidates.Add(Commands[i]);

                    if (candidates.Count == 0)
                    {
                        // No match
                        AddLog($"No match for \"{word}\"!\n");
                    }
                    else if (candidates.Count == 1)
                    {
                        // Single match. Delete the beginning of the word and replace it entirely so we've got nice casing.
                        data.DeleteChars(word_start, word_end - word_start);
                        data.InsertChars(data.CursorPos, candidates[0]);
                        data.InsertChars(data.CursorPos, " ");
                    }
                    else
                    {
                        // Multiple matches. Complete as much as we can..
                        // So inputting "C"+Tab will complete to "CL" then display "CLEAR" and "CLASSIFY" as matches.
                        int match_len = word.Length;
                        for (; ; )
                        {
                            char c = '\0';
                            bool all_candidates_matches = true;
                            for (int i = 0; i < candidates.Count && all_candidates_matches; i++)
                                if (i == 0)
                                    c = match_len < candidates[i].Length ? char.ToUpperInvariant(candidates[i][match_len]) : '\0';
                                else if (c == '\0' || match_len >= candidates[i].Length || c != char.ToUpperInvariant(candidates[i][match_len]))
                                    all_candidates_matches = false;
                            if (!all_candidates_matches)
                                break;
                            match_len++;
                        }

                        if (match_len > 0)
                        {
                            data.DeleteChars(word_start, word_end - word_start);
                            data.InsertChars(data.CursorPos, candidates[0][..match_len]);
                        }

                        // List matches
                        AddLog("Possible matches:\n");
                        for (int i = 0; i < candidates.Count; i++)
                            AddLog($"- {candidates[i]}\n");
                    }

                    break;
                }
                case InputTextFlags.CallbackHistory:
                {
                    // Example of HISTORY
                    int prev_history_pos = HistoryPos;
                    if (data.EventKey == Key.UpArrow)
                    {
                        if (HistoryPos == -1)
                            HistoryPos = History.Count - 1;
                        else if (HistoryPos > 0)
                            HistoryPos--;
                    }
                    else if (data.EventKey == Key.DownArrow)
                    {
                        if (HistoryPos != -1)
                            if (++HistoryPos >= History.Count)
                                HistoryPos = -1;
                    }

                    // A better implementation would preserve the data on the current input line along with cursor position.
                    if (prev_history_pos != HistoryPos)
                    {
                        string history_str = (HistoryPos >= 0) ? History[HistoryPos] : "";
                        data.DeleteChars(0, data.BufTextLen);
                        data.InsertChars(0, history_str);
                    }
                    break;
                }
            }
            return 0;
        }
    }

    static ExampleAppConsole? s_console;

    private static void ShowExampleAppConsole(ref bool open)
    {
        s_console ??= new ExampleAppConsole();
        s_console.Draw("Example: Console", ref open);
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Debug Log / ShowExampleAppLog()
    //-----------------------------------------------------------------------------

    // Usage:
    //  static ExampleAppLog my_log;
    //  my_log.AddLog("Hello 123 world\n");
    //  my_log.Draw("title");
    private sealed class ExampleAppLog
    {
        // Instead of the C++ ImGuiTextBuffer + LineOffsets pair we keep a list of complete
        // lines (which gives us the same cheap random access the clipper needs) plus a
        // pending buffer for a trailing partial line.
        private readonly List<string> Lines = new();
        private string Pending = "";
        private readonly TextFilter Filter = new();
        private bool AutoScroll;   // Keep scrolling if already at the bottom.

        public ExampleAppLog()
        {
            AutoScroll = true;
            Clear();
        }

        public void Clear()
        {
            Lines.Clear();
            Pending = "";
        }

        public void AddLog(string text)
        {
            Pending += text;
            int idx;
            while ((idx = Pending.IndexOf('\n')) >= 0)
            {
                Lines.Add(Pending[..idx]);
                Pending = Pending[(idx + 1)..];
            }
        }

        public void Draw(string title, ref bool open)
        {
            if (!ImGui.Begin(title, ref open))
            {
                ImGui.End();
                return;
            }

            // Options menu
            if (ImGui.BeginPopup("Options"))
            {
                ImGui.Checkbox("Auto-scroll", ref AutoScroll);
                ImGui.EndPopup();
            }

            // Main window
            if (ImGui.Button("Options"))
                ImGui.OpenPopup("Options");
            ImGui.SameLine();
            bool clear = ImGui.Button("Clear");
            ImGui.SameLine();
            bool copy = ImGui.Button("Copy");
            ImGui.SameLine();
            Filter.Draw("Filter", -100.0f);

            ImGui.Separator();

            if (ImGui.BeginChild("scrolling", 0, 0, ChildFlags.None, WindowFlags.HorizontalScrollbar))
            {
                if (clear)
                    Clear();
                if (copy)
                    ImGui.LogToClipboard();

                ImGui.PushStyleVar(StyleVar.ItemSpacing, 0, 0);
                if (Filter.IsActive)
                {
                    // In this example we don't use the clipper when Filter is enabled.
                    // This is because we don't have random access to the result of our filter.
                    // A real application processing logs with ten of thousands of entries may want to store the result of
                    // search/filter.. especially if the filtering function is not trivial (e.g. reg-exp).
                    for (int line_no = 0; line_no < Lines.Count; line_no++)
                    {
                        if (Filter.PassFilter(Lines[line_no]))
                            ImGui.TextUnformatted(Lines[line_no]);
                    }
                }
                else
                {
                    // The simplest and easy way to display the entire buffer:
                    //   ImGui.TextUnformatted(whole_buffer);
                    // And it'll just work. TextUnformatted() has specialization for large blob of text and will fast-forward
                    // to skip non-visible lines. Here we instead demonstrate using the clipper to only process lines that are
                    // within the visible area.
                    // If you have tens of thousands of items and their processing cost is non-negligible, coarse clipping them
                    // on your side is recommended. Using ImGuiListClipper requires
                    // - A) random access into your data
                    // - B) items all being the same height,
                    // both of which we can handle since we have a list of lines.
                    // When using the filter (in the block of code above) we don't have random access into the data to display
                    // anymore, which is why we don't use the clipper. Storing or skimming through the search result would make
                    // it possible (and would be recommended if you want to search through tens of thousands of entries).
                    using var clipper = new ListClipper();
                    clipper.Begin(Lines.Count);
                    while (clipper.Step())
                    {
                        for (int line_no = clipper.DisplayStart; line_no < clipper.DisplayEnd; line_no++)
                            ImGui.TextUnformatted(Lines[line_no]);
                    }
                    clipper.End();
                }
                ImGui.PopStyleVar();

                // Keep up at the bottom of the scroll region if we were already at the bottom at the beginning of the frame.
                // Using a scrollbar or mouse-wheel will take away from the bottom edge.
                if (AutoScroll && ImGui.GetScrollY() >= ImGui.GetScrollMaxY())
                    ImGui.SetScrollHereY(1.0f);
            }
            ImGui.EndChild();
            ImGui.End();
        }
    }

    static ExampleAppLog? s_log;
    static int s_log_counter;

    // Demonstrate creating a simple log window with basic filtering.
    private static void ShowExampleAppLog(ref bool open)
    {
        s_log ??= new ExampleAppLog();

        // For the demo: add a debug button _BEFORE_ the normal log window contents
        // We take advantage of a rarely used feature: multiple calls to Begin()/End() are appending to the _same_ window.
        // Most of the contents of the window will be added by the log.Draw() call.
        ImGui.SetNextWindowSize(500, 400, Cond.FirstUseEver);
        ImGui.Begin("Example: Log", ref open);
        // DEMO MARKER: Examples/Log
        if (ImGui.SmallButton("[Debug] Add 5 entries"))
        {
            string[] categories = { "info", "warn", "error" };
            string[] words = { "Bumfuzzled", "Cattywampus", "Snickersnee", "Abibliophobia", "Absquatulate", "Nincompoop", "Pauciloquent" };
            for (int n = 0; n < 5; n++)
            {
                string category = categories[s_log_counter % categories.Length];
                string word = words[s_log_counter % words.Length];
                s_log.AddLog($"[{ImGui.GetFrameCount():D5}] [{category}] Hello, current time is {ImGui.GetTime():0.0}, here's a word: '{word}'\n");
                s_log_counter++;
            }
        }
        ImGui.End();

        // Actually call in the regular Log helper (which will Begin() into the same window as we just did)
        s_log.Draw("Example: Log", ref open);
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Simple Layout / ShowExampleAppLayout()
    //-----------------------------------------------------------------------------

    static int s_layout_selected;

    // Demonstrate create a window with multiple child windows.
    private static void ShowExampleAppLayout(ref bool open)
    {
        ImGui.SetNextWindowSize(500, 440, Cond.FirstUseEver);
        if (ImGui.Begin("Example: Simple layout", ref open, WindowFlags.MenuBar))
        {
            // DEMO MARKER: Examples/Simple layout
            if (ImGui.BeginMenuBar())
            {
                if (ImGui.BeginMenu("File"))
                {
                    if (ImGui.MenuItem("Close", "Ctrl+W")) { open = false; }
                    ImGui.EndMenu();
                }
                ImGui.EndMenuBar();
            }

            // Left
            {
                ImGui.BeginChild("left pane", 150, 0, ChildFlags.Borders | ChildFlags.ResizeX);
                for (int i = 0; i < 100; i++)
                {
                    string label = $"MyObject {i}";
                    if (ImGui.Selectable(label, s_layout_selected == i, SelectableFlags.SelectOnNav))
                        s_layout_selected = i;
                }
                ImGui.EndChild();
            }
            ImGui.SameLine();

            // Right
            {
                ImGui.BeginGroup();
                ImGui.BeginChild("item view", 0, -ImGui.GetFrameHeightWithSpacing()); // Leave room for 1 line below us
                ImGui.Text($"MyObject: {s_layout_selected}");
                ImGui.Separator();
                if (ImGui.BeginTabBar("##Tabs", TabBarFlags.None))
                {
                    if (ImGui.BeginTabItem("Description"))
                    {
                        ImGui.TextWrapped("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ");
                        ImGui.EndTabItem();
                    }
                    if (ImGui.BeginTabItem("Details"))
                    {
                        ImGui.Text("ID: 0123456789");
                        ImGui.EndTabItem();
                    }
                    ImGui.EndTabBar();
                }
                ImGui.EndChild();
                if (ImGui.Button("Revert")) { }
                ImGui.SameLine();
                if (ImGui.Button("Save")) { }
                ImGui.EndGroup();
            }
        }
        ImGui.End();
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Property Editor / ShowExampleAppPropertyEditor()
    //-----------------------------------------------------------------------------
    // Some of the interactions are a bit lack-luster:
    // - We would want pressing validating or leaving the filter to somehow restore focus.
    // - We may want more advanced filtering (child nodes) and clipper support: both will need extra work.
    // - We would want to customize some keyboard interactions to easily keyboard navigate between the tree and the properties.
    //-----------------------------------------------------------------------------

    private sealed class ExampleAppPropertyEditor
    {
        private readonly TextFilter Filter = new();
        private ExampleTreeNode? SelectedNode;
        private bool UseClipper;

        public void Draw(ExampleTreeNode root_node)
        {
            // DEMO MARKER: Examples/Property editor

            // Left side: draw tree
            // - Currently using a table to benefit from RowBg feature
            // - Our tree node are all of equal height, facilitating the use of a clipper.
            if (ImGui.BeginChild("##tree", 300, 0, ChildFlags.ResizeX | ChildFlags.Borders | ChildFlags.NavFlattened))
            {
                ImGui.PushItemFlag(ItemFlags.NoNavDefaultFocus, true);
                ImGui.Checkbox("Use Clipper", ref UseClipper);
                ImGui.SameLine();
                ImGui.Text($"({root_node.Childs.Count} root nodes)");
                ImGui.SetNextItemWidth(-FltMin);
                ImGui.SetNextItemShortcut(Key.ModCtrl | Key.F, InputFlags.Tooltip);
                // PORT GAP: ImGuiTextFilter::InputBuf is not exposed by the wrapper, so we cannot
                // use InputTextWithHint(..., ImGuiInputTextFlags_EscapeClearsAll) + Filter.Build()
                // like upstream. TextFilter.Draw() provides the same filtering behavior.
                Filter.Draw("##Filter");
                ImGui.PopItemFlag();

                if (ImGui.BeginTable("##list", 1, TableFlags.RowBg))
                {
                    if (UseClipper)
                        DrawClippedTree(root_node);
                    else
                        DrawTree(root_node);
                    ImGui.EndTable();
                }
            }
            ImGui.EndChild();

            // Right side: draw properties
            ImGui.SameLine();

            ImGui.BeginGroup(); // Lock X position
            if (SelectedNode is ExampleTreeNode node)
            {
                ImGui.Text(node.Name);
                ImGui.TextDisabled($"UID: 0x{node.UID:X8}");
                ImGui.Separator();
                if (ImGui.BeginTable("##properties", 2, TableFlags.Resizable | TableFlags.ScrollY))
                {
                    // Push object ID after we entered the table, so table is shared for all objects
                    ImGui.PushID(node.UID);
                    ImGui.TableSetupColumn("", TableColumnFlags.WidthFixed);
                    ImGui.TableSetupColumn("", TableColumnFlags.WidthStretch, 2.0f); // Default twice larger
                    if (node.HasData)
                    {
                        // In a typical application, the structure description would be derived from a data-driven system.
                        // - We try to mimic this with our ExampleMemberInfo structure and the ExampleTreeNodeMemberInfos[] array.
                        // - Limits and some details are hard-coded to simplify the demo.
                        // - Without offsetof() in C#, each data type maps back to the single corresponding member of this demo struct.
                        foreach (ExampleMemberInfo field_desc in ExampleTreeNodeMemberInfos)
                        {
                            ImGui.TableNextRow();
                            ImGui.PushID(field_desc.Name);
                            ImGui.TableNextColumn();
                            ImGui.AlignTextToFramePadding();
                            ImGui.TextUnformatted(field_desc.Name);
                            ImGui.TableNextColumn();
                            switch (field_desc.DataType)
                            {
                                case DataType.Bool:
                                {
                                    // (field_desc.DataCount == 1)
                                    ImGui.Checkbox("##Editor", ref node.DataMyBool);
                                    break;
                                }
                                case DataType.S32:
                                {
                                    ImGui.SetNextItemWidth(-FltMin);
                                    ImGui.Drag("##Editor", ref node.DataMyInt, 1.0f, int.MinValue, int.MaxValue);
                                    break;
                                }
                                case DataType.Float:
                                {
                                    ImGui.SetNextItemWidth(-FltMin);
                                    ImGui.Slider<float>("##Editor", node.DataMyVec2.AsSpan(0, field_desc.DataCount), 0.0f, 1.0f);
                                    break;
                                }
                                case DataType.String:
                                {
                                    ImGui.InputText("##Editor", ref node.Name);
                                    break;
                                }
                            }
                            ImGui.PopID();
                        }
                    }
                    ImGui.PopID();
                    ImGui.EndTable();
                }
            }
            ImGui.EndGroup();
        }

        // Custom search filter
        // - Here we apply on root node only.
        // - This does a case insensitive stristr which is pretty heavy. In a real large-scale app you would likely store a filtered list which in turns would be trivial to linearize.
        private bool IsNodePassingFilter(ExampleTreeNode node)
        {
            return node.Parent!.Parent != null || Filter.PassFilter(node.Name);
        }

        // Basic version, recursive. This is how you would generally draw a tree.
        // - Simple but going to be noticeably costly if you have a large amount of nodes as DrawTreeNode() is called for all of them.
        // - Unlike arrays or grids which are very easy to clip, trees are currently more difficult to clip.
        private void DrawTree(ExampleTreeNode node)
        {
            foreach (ExampleTreeNode child in node.Childs)
                if (IsNodePassingFilter(child) && DrawTreeNode(child))
                {
                    DrawTree(child);
                    ImGui.TreePop();
                }
        }

        // More advanced version. Use a alternative clipping technique: fast-forwarding through non-visible chunks.
        // - 1. Use clipper with indeterminate count (items_count = INT_MAX): we need to call SeekCursorForItem() at the end once we know the count.
        // - 2. Use SetNextItemStorageID() to specify ID used for open/close storage, making it easy to call TreeNodeGetOpen() on any arbitrary node.
        // - 3. Linearize tree during traversal: our tree data structure makes it easy to access sibling and parents.
        // - Unlike clipping for a regular array or grid which may be done using random access limited to visible areas,
        //   this technique requires traversing most accessible nodes. This could be made more optimal with extra work,
        //   but this is a decent simplicity<>speed trade-off.
        // See https://github.com/ocornut/imgui/issues/3823 for discussions about this.
        // (The wrapper doesn't expose ImGuiListClipper::UserIndex, which is plain user storage,
        //  so we carry the equivalent counter in a local passed by ref.)
        private void DrawClippedTree(ExampleTreeNode root_node)
        {
            ExampleTreeNode? node = root_node.Childs[0]; // First node
            using var clipper = new ListClipper();
            int user_index = 0;
            clipper.Begin(int.MaxValue);
            while (clipper.Step())
                while (user_index < clipper.DisplayEnd && node != null)
                    node = DrawClippedTreeNodeAndAdvanceToNext(clipper, ref user_index, node);

            // Keep going to count nodes and submit final count so we have a reliable scrollbar.
            // - One could consider caching this value and only refreshing it occasionally e.g. window is focused and an action occurs.
            // - If that is implemented, the general cost will approach zero when scrolling is at the top of the tree.
            while (node != null)
                node = DrawClippedTreeNodeAndAdvanceToNext(clipper, ref user_index, node);
            clipper.SeekCursorForItem(user_index);
            clipper.End();
        }

        private ExampleTreeNode? DrawClippedTreeNodeAndAdvanceToNext(ListClipper clipper, ref int user_index, ExampleTreeNode node)
        {
            if (IsNodePassingFilter(node))
            {
                // Draw node if within visible range
                bool is_open;
                if (user_index >= clipper.DisplayStart && user_index < clipper.DisplayEnd)
                {
                    is_open = DrawTreeNode(node);
                }
                else
                {
                    is_open = node.Childs.Count > 0 && ImGui.TreeNodeGetOpen((uint)node.UID);
                    if (is_open)
                        ImGui.TreePush(node.Name);
                }
                user_index++;

                // Next node: recurse into childs
                if (is_open)
                    return node.Childs[0];
            }

            // Next node: next sibling, otherwise move back to parent
            ExampleTreeNode? n = node;
            while (n != null)
            {
                if (n.IndexInParent + 1 < n.Parent!.Childs.Count)
                    return n.Parent.Childs[n.IndexInParent + 1];
                n = n.Parent;
                if (n.Parent == null)
                    break;
                ImGui.TreePop();
            }
            return null;
        }

        // To support node with same name we incorporate node->UID into the item ID.
        // (this would more naturally be done using PushID(node.UID) + TreeNodeEx(node.Name, tree_flags),
        //   but it would require in DrawClippedTreeNodeAndAdvanceToNext() to add PushID() before TreePush(), and PopID() after TreePop(),
        //   so instead we use TreeNodeEx(node.UID, tree_flags, node.Name) here)
        private bool DrawTreeNode(ExampleTreeNode node)
        {
            ImGui.TableNextRow();
            ImGui.TableNextColumn();
            var tree_flags = TreeNodeFlags.None;
            tree_flags |= TreeNodeFlags.OpenOnArrow | TreeNodeFlags.OpenOnDoubleClick; // Standard opening mode as we are likely to want to add selection afterwards
            tree_flags |= TreeNodeFlags.NavLeftJumpsToParent;   // Left arrow support
            tree_flags |= TreeNodeFlags.SpanFullWidth;          // Span full width for easier mouse reach
            tree_flags |= TreeNodeFlags.DrawLinesToNodes;       // Always draw hierarchy outlines
            if (node == SelectedNode)
                tree_flags |= TreeNodeFlags.Selected;           // Draw selection highlight
            if (node.Childs.Count == 0)
                tree_flags |= TreeNodeFlags.Leaf | TreeNodeFlags.Bullet | TreeNodeFlags.NoTreePushOnOpen; // Use _NoTreePushOnOpen + set is_open=false to avoid unnecessarily push/pop on leaves.
            if (node.DataMyBool == false)
            {
                Vec4 disabled = Style.GetColor(Col.TextDisabled);
                ImGui.PushStyleColor(Col.Text, disabled.X, disabled.Y, disabled.Z, disabled.W);
            }
            ImGui.SetNextItemStorageID((uint)node.UID);         // Use node.UID as storage id
            bool is_open = ImGui.TreeNodeEx((nint)node.UID, tree_flags, node.Name);
            if (node.Childs.Count == 0)
                is_open = false;
            if (node.DataMyBool == false)
                ImGui.PopStyleColor();
            if (ImGui.IsItemFocused())
                SelectedNode = node;
            return is_open;
        }
    }

    static ExampleAppPropertyEditor? s_property_editor;

    // Demonstrate creating a simple property editor.
    private static void ShowExampleAppPropertyEditor(ref bool open, DemoWindowData data)
    {
        ImGui.SetNextWindowSize(430, 450, Cond.FirstUseEver);
        if (!ImGui.Begin("Example: Property editor", ref open))
        {
            ImGui.End();
            return;
        }

        // DEMO MARKER: Examples/Property Editor
        s_property_editor ??= new ExampleAppPropertyEditor();
        data.DemoTree ??= ExampleTree_CreateDemoTree();
        s_property_editor.Draw(data.DemoTree);

        ImGui.End();
    }

    //-----------------------------------------------------------------------------
    // [SECTION] Example App: Long Text / ShowExampleAppLongText()
    //-----------------------------------------------------------------------------

    static int s_long_text_test_type;
    static readonly StringBuilder s_long_text_log = new();
    static int s_long_text_lines;

    // Demonstrate/test rendering huge amount of text, and the incidence of clipping.
    private static void ShowExampleAppLongText(ref bool open)
    {
        ImGui.SetNextWindowSize(520, 600, Cond.FirstUseEver);
        if (!ImGui.Begin("Example: Long text display", ref open))
        {
            ImGui.End();
            return;
        }
        // DEMO MARKER: Examples/Long text display

        ImGui.Text("Printing unusually long amount of text.");
        ImGui.Combo("Test type", ref s_long_text_test_type, new[]
        {
            "Single call to TextUnformatted()",
            "Multiple calls to Text(), clipped",
            "Multiple calls to Text(), not clipped (slow)",
        });
        ImGui.Text($"Buffer contents: {s_long_text_lines} lines, {s_long_text_log.Length} bytes");
        if (ImGui.Button("Clear")) { s_long_text_log.Clear(); s_long_text_lines = 0; }
        ImGui.SameLine();
        if (ImGui.Button("Add 1000 lines"))
        {
            for (int i = 0; i < 1000; i++)
                s_long_text_log.Append($"{s_long_text_lines + i} The quick brown fox jumps over the lazy dog\n");
            s_long_text_lines += 1000;
        }
        ImGui.BeginChild("Log");
        switch (s_long_text_test_type)
        {
            case 0:
                // Single call to TextUnformatted() with a big buffer
                ImGui.TextUnformatted(s_long_text_log.ToString());
                break;
            case 1:
            {
                // Multiple calls to Text(), manually coarsely clipped - demonstrate how to use the ImGuiListClipper helper.
                ImGui.PushStyleVar(StyleVar.ItemSpacing, 0, 0);
                using (var clipper = new ListClipper())
                {
                    clipper.Begin(s_long_text_lines);
                    while (clipper.Step())
                        for (int i = clipper.DisplayStart; i < clipper.DisplayEnd; i++)
                            ImGui.Text($"{i} The quick brown fox jumps over the lazy dog");
                    clipper.End();
                }
                ImGui.PopStyleVar();
                break;
            }
            case 2:
                // Multiple calls to Text(), not clipped (slow)
                ImGui.PushStyleVar(StyleVar.ItemSpacing, 0, 0);
                for (int i = 0; i < s_long_text_lines; i++)
                    ImGui.Text($"{i} The quick brown fox jumps over the lazy dog");
                ImGui.PopStyleVar();
                break;
        }
        ImGui.EndChild();
        ImGui.End();
    }
}
