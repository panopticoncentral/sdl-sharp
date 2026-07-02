// C# port of imgui_demo.cpp — Widgets section, part C:
//   DemoWindowWidgetsTabs, ...Text, ...TextFilter, ...TextInput, ...Tooltips,
//   ...TreeNodes, ...VerticalSliders, and the DemoWindowWidgets() aggregator.
//
// Upstream reference: imgui_demo.cpp lines ~3450-4362.

using System.Text;

using SdlSharp.ImGui;

namespace ImGuiDemo;

internal static unsafe partial class DemoWindow
{
    // Creates a fixed-size, zero-terminated UTF-8 buffer with optional initial content,
    // mirroring the C++ demo's `static char buf[N] = "..."` locals.
    private static byte[] WidgetsCMakeBuffer(string init, int size)
    {
        var buf = new byte[size];
        Encoding.UTF8.GetBytes(init, 0, init.Length, buf, 0); // Buffer is zero-initialized; remaining bytes act as the terminator.
        return buf;
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsTabs()
    //-----------------------------------------------------------------------------

    // Upstream helper (imgui_demo.cpp ~3438): edit the fitting-policy portion of a TabBarFlags value.
    private static void EditTabBarFittingPolicyFlags(ref int flags)
    {
        // ImGuiTabBarFlags_FittingPolicyMask_ / ImGuiTabBarFlags_FittingPolicyDefault_ are not
        // exposed as named enum members by the wrapper; recompose them here.
        const int fittingPolicyMask = (int)(TabBarFlags.FittingPolicyMixed | TabBarFlags.FittingPolicyShrink | TabBarFlags.FittingPolicyScroll);
        const int fittingPolicyDefault = (int)TabBarFlags.FittingPolicyMixed;

        if ((flags & fittingPolicyMask) == 0)
            flags |= fittingPolicyDefault;
        if (ImGui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyMixed", ref flags, (int)TabBarFlags.FittingPolicyMixed))
            flags &= ~(fittingPolicyMask ^ (int)TabBarFlags.FittingPolicyMixed);
        if (ImGui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyShrink", ref flags, (int)TabBarFlags.FittingPolicyShrink))
            flags &= ~(fittingPolicyMask ^ (int)TabBarFlags.FittingPolicyShrink);
        if (ImGui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyScroll", ref flags, (int)TabBarFlags.FittingPolicyScroll))
            flags &= ~(fittingPolicyMask ^ (int)TabBarFlags.FittingPolicyScroll);
    }

    // "Advanced & Close Button"
    static int s_tabs_advanced_tab_bar_flags = (int)TabBarFlags.Reorderable;
    static readonly string[] s_tabs_advanced_names = { "Artichoke", "Beetroot", "Celery", "Daikon" };
    static readonly bool[] s_tabs_advanced_opened = { true, true, true, true }; // Persistent user state

    // "TabItemButton & Leading/Trailing flags"
    static readonly List<int> s_tabs_active_tabs = new();
    static int s_tabs_next_tab_id = 0;
    static bool s_tabs_show_leading_button = true;
    static bool s_tabs_show_trailing_button = true;
    static int s_tabs_tib_tab_bar_flags = (int)(TabBarFlags.AutoSelectNewTabs | TabBarFlags.Reorderable | TabBarFlags.FittingPolicyShrink);

    private static void DemoWindowWidgetsTabs()
    {
        if (ImGui.TreeNode("Tabs"))
        {
            // DEMO MARKER: Widgets/Tabs
            if (ImGui.TreeNode("Basic"))
            {
                // DEMO MARKER: Widgets/Tabs/Basic
                var tab_bar_flags = TabBarFlags.None;
                if (ImGui.BeginTabBar("MyTabBar", tab_bar_flags))
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
                ImGui.Separator();
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Advanced & Close Button"))
            {
                // DEMO MARKER: Widgets/Tabs/Advanced & Close Button
                // Expose a couple of the available flags. In most cases you may just call BeginTabBar() with no flags (0).
                ImGui.CheckboxFlags("ImGuiTabBarFlags_Reorderable", ref s_tabs_advanced_tab_bar_flags, (int)TabBarFlags.Reorderable);
                ImGui.CheckboxFlags("ImGuiTabBarFlags_AutoSelectNewTabs", ref s_tabs_advanced_tab_bar_flags, (int)TabBarFlags.AutoSelectNewTabs);
                ImGui.CheckboxFlags("ImGuiTabBarFlags_TabListPopupButton", ref s_tabs_advanced_tab_bar_flags, (int)TabBarFlags.TabListPopupButton);
                ImGui.CheckboxFlags("ImGuiTabBarFlags_NoCloseWithMiddleMouseButton", ref s_tabs_advanced_tab_bar_flags, (int)TabBarFlags.NoCloseWithMiddleMouseButton);
                ImGui.CheckboxFlags("ImGuiTabBarFlags_DrawSelectedOverline", ref s_tabs_advanced_tab_bar_flags, (int)TabBarFlags.DrawSelectedOverline);
                EditTabBarFittingPolicyFlags(ref s_tabs_advanced_tab_bar_flags);

                // Tab Bar
                ImGui.AlignTextToFramePadding();
                ImGui.Text("Opened:");
                var names = s_tabs_advanced_names;
                var opened = s_tabs_advanced_opened;
                for (int n = 0; n < opened.Length; n++)
                {
                    ImGui.SameLine();
                    ImGui.Checkbox(names[n], ref opened[n]);
                }

                // Passing a bool* to BeginTabItem() is similar to passing one to Begin():
                // the underlying bool will be set to false when the tab is closed.
                if (ImGui.BeginTabBar("MyTabBar", (TabBarFlags)s_tabs_advanced_tab_bar_flags))
                {
                    for (int n = 0; n < opened.Length; n++)
                        if (opened[n] && ImGui.BeginTabItem(names[n], ref opened[n], TabItemFlags.None))
                        {
                            ImGui.Text($"This is the {names[n]} tab!");
                            if ((n & 1) != 0)
                                ImGui.Text("I am an odd tab.");
                            ImGui.EndTabItem();
                        }
                    ImGui.EndTabBar();
                }
                ImGui.Separator();
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("TabItemButton & Leading/Trailing flags"))
            {
                // DEMO MARKER: Widgets/Tabs/TabItemButton & Leading-Trailing flags
                if (s_tabs_next_tab_id == 0) // Initialize with some default tabs
                    for (int i = 0; i < 3; i++)
                        s_tabs_active_tabs.Add(s_tabs_next_tab_id++);

                // TabItemButton() and Leading/Trailing flags are distinct features which we will demo together.
                // (It is possible to submit regular tabs with Leading/Trailing flags, or TabItemButton tabs without Leading/Trailing flags...
                // but they tend to make more sense together)
                ImGui.Checkbox("Show Leading TabItemButton()", ref s_tabs_show_leading_button);
                ImGui.Checkbox("Show Trailing TabItemButton()", ref s_tabs_show_trailing_button);

                // Expose some other flags which are useful to showcase how they interact with Leading/Trailing tabs
                EditTabBarFittingPolicyFlags(ref s_tabs_tib_tab_bar_flags);

                if (ImGui.BeginTabBar("MyTabBar", (TabBarFlags)s_tabs_tib_tab_bar_flags))
                {
                    // Demo a Leading TabItemButton(): click the "?" button to open a menu
                    if (s_tabs_show_leading_button)
                        if (ImGui.TabItemButton("?", TabItemFlags.Leading | TabItemFlags.NoTooltip))
                            ImGui.OpenPopup("MyHelpMenu");
                    if (ImGui.BeginPopup("MyHelpMenu"))
                    {
                        ImGui.Selectable("Hello!");
                        ImGui.EndPopup();
                    }

                    // Demo Trailing Tabs: click the "+" button to add a new tab.
                    // (In your app you may want to use a font icon instead of the "+")
                    // We submit it before the regular tabs, but thanks to the ImGuiTabItemFlags_Trailing flag it will always appear at the end.
                    if (s_tabs_show_trailing_button)
                        if (ImGui.TabItemButton("+", TabItemFlags.Trailing | TabItemFlags.NoTooltip))
                            s_tabs_active_tabs.Add(s_tabs_next_tab_id++); // Add new tab

                    // Submit our regular tabs
                    for (int n = 0; n < s_tabs_active_tabs.Count;)
                    {
                        bool open = true;
                        string name = s_tabs_active_tabs[n].ToString("0000");
                        if (ImGui.BeginTabItem(name, ref open, TabItemFlags.None))
                        {
                            ImGui.Text($"This is the {name} tab!");
                            ImGui.EndTabItem();
                        }

                        if (!open)
                            s_tabs_active_tabs.RemoveAt(n);
                        else
                            n++;
                    }

                    ImGui.EndTabBar();
                }
                ImGui.Separator();
                ImGui.TreePop();
            }
            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsText()
    //-----------------------------------------------------------------------------

    // "Font Size"
    static float s_text_custom_size = 16.0f;
    static float s_text_custom_scale = 1.0f;

    // "Word Wrapping"
    static float s_text_wrap_width = 200.0f;

    // "UTF-8 Text" ("nihongo" as initial value)
    static readonly byte[] s_text_utf8_buf = WidgetsCMakeBuffer("日本語", 32);

    private static void DemoWindowWidgetsText()
    {
        if (ImGui.TreeNode("Text"))
        {
            // DEMO MARKER: Widgets/Text
            if (ImGui.TreeNode("Colorful Text"))
            {
                // DEMO MARKER: Widgets/Text/Colored Text
                // Using shortcut. You can use PushStyleColor()/PopStyleColor() for more flexibility.
                ImGui.TextColored(1.0f, 0.0f, 1.0f, 1.0f, "Pink");
                ImGui.TextColored(1.0f, 1.0f, 0.0f, 1.0f, "Yellow");
                ImGui.TextDisabled("Disabled");
                ImGui.SameLine(); HelpMarker("The TextDisabled color is stored in ImGuiStyle.");
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Font Size"))
            {
                // DEMO MARKER: Widgets/Text/Font Size
                float global_scale = Style.FontScaleMain * Style.FontScaleDpi;
                ImGui.Text($"style.FontScaleMain = {Style.FontScaleMain:0.00}");
                ImGui.Text($"style.FontScaleDpi = {Style.FontScaleDpi:0.00}");
                ImGui.Text($"global_scale = ~{global_scale:0.00}"); // This is not technically accurate as internal scales may apply, but conceptually let's pretend it is.
                ImGui.Text($"FontSize = {ImGui.GetFontSize():0.00}");

                ImGui.SeparatorText("");
                ImGui.SliderFloat("custom_size", ref s_text_custom_size, 10.0f, 100.0f, "%.0f");
                ImGui.Text("ImGui::PushFont(nullptr, custom_size);");
                ImGui.PushFont(default, s_text_custom_size); // default(Font) == null font: keep current font, change size
                ImGui.Text($"FontSize = {ImGui.GetFontSize():0.00} (== {s_text_custom_size:0.00} * global_scale)");
                ImGui.PopFont();

                ImGui.SeparatorText("");
                ImGui.SliderFloat("custom_scale", ref s_text_custom_scale, 0.5f, 4.0f, "%.2f");
                ImGui.Text("ImGui::PushFont(nullptr, style.FontSizeBase * custom_scale);");
                ImGui.PushFont(default, Style.FontSizeBase * s_text_custom_scale);
                ImGui.Text($"FontSize = {ImGui.GetFontSize():0.00} (== style.FontSizeBase * {s_text_custom_scale:0.00} * global_scale)");
                ImGui.PopFont();

                ImGui.SeparatorText("");
                for (float scaling = 0.5f; scaling <= 4.0f; scaling += 0.5f)
                {
                    ImGui.PushFont(default, Style.FontSizeBase * scaling);
                    ImGui.Text($"FontSize = {ImGui.GetFontSize():0.00} (== style.FontSizeBase * {scaling:0.00} * global_scale)");
                    ImGui.PopFont();
                }

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Word Wrapping"))
            {
                // DEMO MARKER: Widgets/Text/Word Wrapping
                // Using shortcut. You can use PushTextWrapPos()/PopTextWrapPos() for more flexibility.
                ImGui.TextWrapped(
                    "This text should automatically wrap on the edge of the window. The current implementation " +
                    "for text wrapping follows simple rules suitable for English and possibly other languages.");
                ImGui.Spacing();

                ImGui.SliderFloat("Wrap width", ref s_text_wrap_width, -20, 600, "%.0f");

                var draw_list = ImGui.GetWindowDrawList();
                for (int n = 0; n < 2; n++)
                {
                    ImGui.Text($"Test paragraph {n}:");
                    var pos = ImGui.GetCursorScreenPos();
                    var marker_min = new Vec2(pos.X + s_text_wrap_width, pos.Y);
                    var marker_max = new Vec2(pos.X + s_text_wrap_width + 10, pos.Y + ImGui.GetTextLineHeight());
                    ImGui.PushTextWrapPos(ImGui.GetCursorPos().X + s_text_wrap_width);
                    if (n == 0)
                        ImGui.Text($"The lazy dog is a good dog. This paragraph should fit within {s_text_wrap_width:0} pixels. Testing a 1 character word. The quick brown fox jumps over the lazy dog.");
                    else
                        ImGui.Text("aaaaaaaa bbbbbbbb, c cccccccc,dddddddd. d eeeeeeee   ffffffff. gggggggg!hhhhhhhh");

                    // Draw actual text bounding box, following by marker of our expected limit (should not overlap!)
                    var rect_min = ImGui.GetItemRectMin();
                    var rect_max = ImGui.GetItemRectMax();
                    draw_list.AddRect(new Vec2(rect_min.X, rect_min.Y), new Vec2(rect_max.X, rect_max.Y), 0xFF00FFFF);     // IM_COL32(255, 255, 0, 255)
                    draw_list.AddRectFilled(marker_min, marker_max, 0xFFFF00FF);                                           // IM_COL32(255, 0, 255, 255)
                    ImGui.PopTextWrapPos();
                }

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("UTF-8 Text"))
            {
                // DEMO MARKER: Widgets/Text/UTF-8 Text
                // UTF-8 test with Japanese characters
                // (Needs a suitable font? Try "Google Noto" or "Arial Unicode". See docs/FONTS.md for details.)
                // The C++ demo encodes these strings as hexadecimal constants to support old compilers;
                // C# strings are UTF-16 and converted to UTF-8 by the wrapper, so we can use literal text.
                // Note that characters values are preserved even by InputText() if the font cannot be displayed,
                // so you can safely copy & paste garbled characters into another application.
                ImGui.TextWrapped(
                    "CJK text will only appear if the font was loaded with the appropriate CJK character ranges. " +
                    "Call io.Fonts->AddFontFromFileTTF() manually to load extra character ranges. " +
                    "Read docs/FONTS.md for details.");
                ImGui.Text("Hiragana: かきくけこ (kakikukeko)");
                ImGui.Text("Kanjis: 日本語 (nihongo)");
                ImGui.InputText("UTF-8 input", s_text_utf8_buf);
                ImGui.TreePop();
            }
            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsTextFilter()
    //-----------------------------------------------------------------------------

    static readonly TextFilter s_textfilter_filter = new();

    private static void DemoWindowWidgetsTextFilter()
    {
        if (ImGui.TreeNode("Text Filter"))
        {
            // DEMO MARKER: Widgets/Text Filter
            // Helper class to easy setup a text filter.
            // You may want to implement a more feature-full filtering scheme in your own application.
            HelpMarker("Not a widget per-se, but ImGuiTextFilter is a helper to perform simple filtering on text strings.");
            ImGui.Text("Filter usage:\n" +
                "  \"\"         display all lines\n" +
                "  \"xxx\"      display lines containing \"xxx\"\n" +
                "  \"xxx,yyy\"  display lines containing \"xxx\" or \"yyy\"\n" +
                "  \"-xxx\"     hide lines containing \"xxx\"");
            s_textfilter_filter.Draw();
            string[] lines = { "aaa1.c", "bbb1.c", "ccc1.c", "aaa2.cpp", "bbb2.cpp", "ccc2.cpp", "abc.h", "hello, world" };
            for (int i = 0; i < lines.Length; i++)
                if (s_textfilter_filter.PassFilter(lines[i]))
                    ImGui.BulletText(lines[i]);
            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsTextInput()
    //-----------------------------------------------------------------------------

    // "Multi-line Text Input"
    static readonly byte[] s_textinput_multiline_text = WidgetsCMakeBuffer(
        "/*\n" +
        " The Pentium F00F bug, shorthand for F0 0F C7 C8,\n" +
        " the hexadecimal encoding of one offending instruction,\n" +
        " more formally, the invalid operand with locked CMPXCHG8B\n" +
        " instruction bug, is a design flaw in the majority of\n" +
        " Intel Pentium, Pentium MMX, and Pentium OverDrive\n" +
        " processors (all in the P5 microarchitecture).\n" +
        "*/\n\n" +
        "label:\n" +
        "\tlock cmpxchg8b eax\n", 1024 * 16);
    static int s_textinput_multiline_flags = (int)InputTextFlags.AllowTabInput;

    // "Filtered Text Input"

    // Modify character input by altering 'data->EventChar' (ImGuiInputTextFlags_CallbackCharFilter callback)
    private static int TextInputFilterCasingSwap(InputTextCallbackData data)
    {
        if (data.EventChar >= 'a' && data.EventChar <= 'z') { data.EventChar -= 'a' - 'A'; } // Lowercase becomes uppercase
        else if (data.EventChar >= 'A' && data.EventChar <= 'Z') { data.EventChar += 'a' - 'A'; } // Uppercase becomes lowercase
        return 0;
    }

    // Return 0 (pass) if the character is 'i' or 'm' or 'g' or 'u' or 'i', otherwise return 1 (filter out)
    private static int TextInputFilterImGuiLetters(InputTextCallbackData data)
    {
        if (data.EventChar < 256 && "imgui".Contains((char)data.EventChar))
            return 0;
        return 1;
    }

    static readonly InputTextCallback s_textinput_filter_casing_swap_cb = TextInputFilterCasingSwap;
    static readonly InputTextCallback s_textinput_filter_imgui_letters_cb = TextInputFilterImGuiLetters;

    static readonly byte[] s_textinput_filtered_buf1 = new byte[32];
    static readonly byte[] s_textinput_filtered_buf2 = new byte[32];
    static readonly byte[] s_textinput_filtered_buf3 = new byte[32];
    static readonly byte[] s_textinput_filtered_buf4 = new byte[32];
    static readonly byte[] s_textinput_filtered_buf5 = new byte[32];
    static readonly byte[] s_textinput_filtered_buf6 = new byte[32];
    static readonly byte[] s_textinput_filtered_buf7 = new byte[32];

    // "Password Input"
    static readonly byte[] s_textinput_password = WidgetsCMakeBuffer("password123", 64);

    // "Completion, History, Edit Callbacks"
    private static int TextInputMyCallback(InputTextCallbackData data)
    {
        if (data.EventFlag == InputTextFlags.CallbackCompletion)
        {
            data.InsertChars(data.CursorPos, "..");
        }
        else if (data.EventFlag == InputTextFlags.CallbackHistory)
        {
            if (data.EventKey == Key.UpArrow)
            {
                data.DeleteChars(0, data.BufTextLen);
                data.InsertChars(0, "Pressed Up!");
                data.SelectAll();
            }
            else if (data.EventKey == Key.DownArrow)
            {
                data.DeleteChars(0, data.BufTextLen);
                data.InsertChars(0, "Pressed Down!");
                data.SelectAll();
            }
        }
        else if (data.EventFlag == InputTextFlags.CallbackEdit)
        {
            // Toggle casing of first character
            var buf = data.Buf;
            if (buf.Length > 0)
            {
                byte c = buf[0];
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')) buf[0] ^= 32;
            }
            data.BufDirty = true;

            // Increment a counter (the C++ demo passes &edit_count as callback user data;
            // the wrapper callback carries no user data pointer, so use the static field directly)
            s_textinput_edit_count++;
        }
        return 0;
    }

    static readonly InputTextCallback s_textinput_my_callback = TextInputMyCallback;
    static readonly byte[] s_textinput_callbacks_buf1 = new byte[64];
    static readonly byte[] s_textinput_callbacks_buf2 = new byte[64];
    static readonly byte[] s_textinput_callbacks_buf3 = new byte[64];
    static int s_textinput_edit_count = 0;

    // "Resize Callback"
    static int s_textinput_resize_flags = (int)InputTextFlags.None;
    static string s_textinput_resize_str = "";

    // "Eliding, Alignment"
    static readonly byte[] s_textinput_eliding_buf1 = WidgetsCMakeBuffer("/path/to/some/folder/with/long/filename.cpp", 128);
    static int s_textinput_eliding_flags = (int)InputTextFlags.ElideLeft;

    // "Miscellaneous"
    static readonly byte[] s_textinput_misc_buf1 = new byte[16];
    static int s_textinput_misc_flags = (int)InputTextFlags.EscapeClearsAll;

    private static void DemoWindowWidgetsTextInput()
    {
        // To wire InputText() with std::string or any other custom string type,
        // see the "Text Input > Resize Callback" section of this demo, and the misc/cpp/imgui_stdlib.h file.
        if (ImGui.TreeNode("Text Input"))
        {
            // DEMO MARKER: Widgets/Text Input
            if (ImGui.TreeNode("Multi-line Text Input"))
            {
                // DEMO MARKER: Widgets/Text Input/Multi-line Text Input
                // WE ARE USING A FIXED-SIZE BUFFER FOR SIMPLICITY HERE.
                // If you want to use InputText() with a dynamic string, use the `ref string` overloads
                // of ImGui.InputTextMultiline() (they use ImGuiInputTextFlags_CallbackResize internally).
                HelpMarker("You can use the ImGuiInputTextFlags_CallbackResize facility if you need to wire InputTextMultiline() to a dynamic string type. See misc/cpp/imgui_stdlib.h for an example. (This is not demonstrated in imgui_demo.cpp because we don't want to include <string> in here)");
                ImGui.CheckboxFlags("ImGuiInputTextFlags_ReadOnly", ref s_textinput_multiline_flags, (int)InputTextFlags.ReadOnly);
                ImGui.CheckboxFlags("ImGuiInputTextFlags_WordWrap", ref s_textinput_multiline_flags, (int)InputTextFlags.WordWrap);
                ImGui.SameLine(); HelpMarker("Feature is currently in Beta. Please read comments in imgui.h");
                ImGui.CheckboxFlags("ImGuiInputTextFlags_AllowTabInput", ref s_textinput_multiline_flags, (int)InputTextFlags.AllowTabInput);
                ImGui.SameLine(); HelpMarker("When _AllowTabInput is set, passing through the widget with Tabbing doesn't automatically activate it, in order to also cycling through subsequent widgets.");
                ImGui.CheckboxFlags("ImGuiInputTextFlags_CtrlEnterForNewLine", ref s_textinput_multiline_flags, (int)InputTextFlags.CtrlEnterForNewLine);
                ImGui.InputTextMultiline("##source", s_textinput_multiline_text, -float.Epsilon, ImGui.GetTextLineHeight() * 16, (InputTextFlags)s_textinput_multiline_flags);
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Filtered Text Input"))
            {
                // DEMO MARKER: Widgets/Text Input/Filtered Text Input
                ImGui.InputText("default", s_textinput_filtered_buf1);
                ImGui.InputText("decimal", s_textinput_filtered_buf2, InputTextFlags.CharsDecimal);
                ImGui.InputText("hexadecimal", s_textinput_filtered_buf3, InputTextFlags.CharsHexadecimal | InputTextFlags.CharsUppercase);
                ImGui.InputText("uppercase", s_textinput_filtered_buf4, InputTextFlags.CharsUppercase);
                ImGui.InputText("no blank", s_textinput_filtered_buf5, InputTextFlags.CharsNoBlank);
                ImGui.InputText("casing swap", s_textinput_filtered_buf6, InputTextFlags.CallbackCharFilter, s_textinput_filter_casing_swap_cb); // Use CharFilter callback to replace characters.
                ImGui.InputText("\"imgui\"", s_textinput_filtered_buf7, InputTextFlags.CallbackCharFilter, s_textinput_filter_imgui_letters_cb); // Use CharFilter callback to disable some characters.
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Password Input"))
            {
                // DEMO MARKER: Widgets/Text Input/Password input
                ImGui.InputText("password", s_textinput_password, InputTextFlags.Password);
                ImGui.SameLine(); HelpMarker("Display all characters as '*'.\nDisable clipboard cut and copy.\nDisable logging.\n");
                ImGui.InputTextWithHint("password (w/ hint)", "<password>", s_textinput_password, InputTextFlags.Password);
                ImGui.InputText("password (clear)", s_textinput_password);
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Completion, History, Edit Callbacks"))
            {
                // DEMO MARKER: Widgets/Text Input/Completion, History, Edit Callbacks
                ImGui.InputText("Completion", s_textinput_callbacks_buf1, InputTextFlags.CallbackCompletion, s_textinput_my_callback);
                ImGui.SameLine(); HelpMarker(
                    "Here we append \"..\" each time Tab is pressed. " +
                    "See 'Examples>Console' for a more meaningful demonstration of using this callback.");

                ImGui.InputText("History", s_textinput_callbacks_buf2, InputTextFlags.CallbackHistory, s_textinput_my_callback);
                ImGui.SameLine(); HelpMarker(
                    "Here we replace and select text each time Up/Down are pressed. " +
                    "See 'Examples>Console' for a more meaningful demonstration of using this callback.");

                ImGui.InputText("Edit", s_textinput_callbacks_buf3, InputTextFlags.CallbackEdit, s_textinput_my_callback);
                ImGui.SameLine(); HelpMarker(
                    "Here we toggle the casing of the first character on every edit + count edits.");
                ImGui.SameLine(); ImGui.Text($"({s_textinput_edit_count})");

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Resize Callback"))
            {
                // DEMO MARKER: Widgets/Text Input/Resize Callback
                // The C++ demo wires an ImVector<char> to InputText() using ImGuiInputTextFlags_CallbackResize.
                // The managed wrapper already provides this wiring: the `ref string` overloads of
                // InputText()/InputTextMultiline() use CallbackResize internally to auto-grow the buffer.
                HelpMarker(
                    "Using ImGuiInputTextFlags_CallbackResize to wire your custom string type to InputText().\n\n" +
                    "The SdlSharp.ImGui `ref string` overloads implement this internally (see misc/cpp/imgui_stdlib.h for the C++ std::string equivalent).");
                ImGui.CheckboxFlags("ImGuiInputTextFlags_WordWrap", ref s_textinput_resize_flags, (int)InputTextFlags.WordWrap);

                ImGui.InputTextMultiline("##MyStr", ref s_textinput_resize_str, -float.Epsilon, ImGui.GetTextLineHeight() * 16, (InputTextFlags)s_textinput_resize_flags);
                // The C++ demo prints the buffer pointer/size/capacity of its ImVector<char>; the managed
                // string hides those details, so report the UTF-16 length instead.
                ImGui.Text($"Size: {s_textinput_resize_str.Length}");
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Eliding, Alignment"))
            {
                // DEMO MARKER: Widgets/Text Input/Eliding, Alignment
                ImGui.CheckboxFlags("ImGuiInputTextFlags_ElideLeft", ref s_textinput_eliding_flags, (int)InputTextFlags.ElideLeft);
                ImGui.InputText("Path", s_textinput_eliding_buf1, (InputTextFlags)s_textinput_eliding_flags);
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Miscellaneous"))
            {
                // DEMO MARKER: Widgets/Text Input/Miscellaneous
                ImGui.CheckboxFlags("ImGuiInputTextFlags_EscapeClearsAll", ref s_textinput_misc_flags, (int)InputTextFlags.EscapeClearsAll);
                ImGui.CheckboxFlags("ImGuiInputTextFlags_ReadOnly", ref s_textinput_misc_flags, (int)InputTextFlags.ReadOnly);
                ImGui.CheckboxFlags("ImGuiInputTextFlags_NoUndoRedo", ref s_textinput_misc_flags, (int)InputTextFlags.NoUndoRedo);
                ImGui.InputText("Hello", s_textinput_misc_buf1, (InputTextFlags)s_textinput_misc_flags);
                ImGui.TreePop();
            }

            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsTooltips()
    //-----------------------------------------------------------------------------

    static readonly float[] s_tooltips_arr = { 0.6f, 0.1f, 1.0f, 0.5f, 0.92f, 0.1f, 0.2f };
    static int s_tooltips_always_on = 0;

    private static void DemoWindowWidgetsTooltips()
    {
        if (ImGui.TreeNode("Tooltips"))
        {
            // DEMO MARKER: Widgets/Tooltips
            // Tooltips are windows following the mouse. They do not take focus away.
            ImGui.SeparatorText("General");

            // Typical use cases:
            // - Short-form (text only):      SetItemTooltip("Hello");
            // - Short-form (any contents):   if (BeginItemTooltip()) { Text("Hello"); EndTooltip(); }

            // - Full-form (text only):       if (IsItemHovered(...)) { SetTooltip("Hello"); }
            // - Full-form (any contents):    if (IsItemHovered(...) && BeginTooltip()) { Text("Hello"); EndTooltip(); }

            HelpMarker(
                "Tooltip are typically created by using a IsItemHovered() + SetTooltip() sequence.\n\n" +
                "We provide a helper SetItemTooltip() function to perform the two with standards flags.");

            const float sz_w = -float.Epsilon; // ImVec2 sz = ImVec2(-FLT_MIN, 0.0f);
            const float sz_h = 0.0f;

            ImGui.Button("Basic", sz_w, sz_h);
            ImGui.SetItemTooltip("I am a tooltip");

            ImGui.Button("Fancy", sz_w, sz_h);
            if (ImGui.BeginItemTooltip())
            {
                ImGui.Text("I am a fancy tooltip");
                ImGui.PlotLines("Curve", s_tooltips_arr);
                ImGui.Text($"Sin(time) = {MathF.Sin((float)ImGui.GetTime()):F6}");
                ImGui.EndTooltip();
            }

            ImGui.SeparatorText("Always On");

            // Showcase NOT relying on a IsItemHovered() to emit a tooltip.
            // Here the tooltip is always emitted when 'always_on == true'.
            ImGui.RadioButton("Off", ref s_tooltips_always_on, 0);
            ImGui.SameLine();
            ImGui.RadioButton("Always On (Simple)", ref s_tooltips_always_on, 1);
            ImGui.SameLine();
            ImGui.RadioButton("Always On (Advanced)", ref s_tooltips_always_on, 2);
            if (s_tooltips_always_on == 1)
                ImGui.SetTooltip("I am following you around.");
            else if (s_tooltips_always_on == 2 && ImGui.BeginTooltip())
            {
                ImGui.ProgressBar(MathF.Sin((float)ImGui.GetTime()) * 0.5f + 0.5f, ImGui.GetFontSize() * 25, 0.0f);
                ImGui.EndTooltip();
            }

            ImGui.SeparatorText("Custom");

            HelpMarker(
                "Passing ImGuiHoveredFlags_ForTooltip to IsItemHovered() is the preferred way to standardize " +
                "tooltip activation details across your application. You may however decide to use custom " +
                "flags for a specific tooltip instance.");

            // The following examples are passed for documentation purpose but may not be useful to most users.
            // Passing ImGuiHoveredFlags_ForTooltip to IsItemHovered() will pull ImGuiHoveredFlags flags values from
            // 'style.HoverFlagsForTooltipMouse' or 'style.HoverFlagsForTooltipNav' depending on whether mouse or keyboard/gamepad is being used.
            // With default settings, ImGuiHoveredFlags_ForTooltip is equivalent to ImGuiHoveredFlags_DelayShort + ImGuiHoveredFlags_Stationary.
            ImGui.Button("Manual", sz_w, sz_h);
            if (ImGui.IsItemHovered(HoveredFlags.ForTooltip))
                ImGui.SetTooltip("I am a manually emitted tooltip.");

            ImGui.Button("DelayNone", sz_w, sz_h);
            if (ImGui.IsItemHovered(HoveredFlags.DelayNone))
                ImGui.SetTooltip("I am a tooltip with no delay.");

            ImGui.Button("DelayShort", sz_w, sz_h);
            if (ImGui.IsItemHovered(HoveredFlags.DelayShort | HoveredFlags.NoSharedDelay))
                ImGui.SetTooltip($"I am a tooltip with a short delay ({Style.HoverDelayShort:F2} sec).");

            ImGui.Button("DelayLong", sz_w, sz_h);
            if (ImGui.IsItemHovered(HoveredFlags.DelayNormal | HoveredFlags.NoSharedDelay))
                ImGui.SetTooltip($"I am a tooltip with a long delay ({Style.HoverDelayNormal:F2} sec).");

            ImGui.Button("Stationary", sz_w, sz_h);
            if (ImGui.IsItemHovered(HoveredFlags.Stationary))
                ImGui.SetTooltip("I am a tooltip requiring mouse to be stationary before activating.");

            // Using ImGuiHoveredFlags_ForTooltip will pull flags from 'style.HoverFlagsForTooltipMouse' or 'style.HoverFlagsForTooltipNav',
            // which default value include the ImGuiHoveredFlags_AllowWhenDisabled flag.
            ImGui.BeginDisabled();
            ImGui.Button("Disabled item", sz_w, sz_h);
            if (ImGui.IsItemHovered(HoveredFlags.ForTooltip))
                ImGui.SetTooltip("I am a a tooltip for a disabled item.");
            ImGui.EndDisabled();

            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsTreeNodes()
    //-----------------------------------------------------------------------------

    // "Hierarchy lines"
    static int s_treenodes_hierarchy_base_flags = (int)(TreeNodeFlags.DrawLinesFull | TreeNodeFlags.DefaultOpen);

    // "Advanced, with Selectable nodes"
    static int s_treenodes_adv_base_flags = (int)(TreeNodeFlags.OpenOnArrow | TreeNodeFlags.OpenOnDoubleClick | TreeNodeFlags.SpanAvailWidth);
    static bool s_treenodes_align_label_with_current_x_position = false;
    static bool s_treenodes_test_drag_and_drop = false;
    static int s_treenodes_selection_mask = 1 << 2;

    private static void DemoWindowWidgetsTreeNodes()
    {
        if (ImGui.TreeNode("Tree Nodes"))
        {
            // DEMO MARKER: Widgets/Tree Nodes
            // See see "Examples -> Property Editor" (ShowExampleAppPropertyEditor() function) for a fancier, data-driven tree.
            if (ImGui.TreeNode("Basic trees"))
            {
                // DEMO MARKER: Widgets/Tree Nodes/Basic trees
                for (int i = 0; i < 5; i++)
                {
                    // Use SetNextItemOpen() so set the default state of a node to be open. We could
                    // also use TreeNodeEx() with the ImGuiTreeNodeFlags_DefaultOpen flag to achieve the same thing!
                    if (i == 0)
                        ImGui.SetNextItemOpen(true, Cond.Once);

                    // Here we use PushID() to generate a unique base ID, and then the "" used as TreeNode id won't conflict.
                    // An alternative to using 'PushID() + TreeNode("", ...)' to generate a unique ID is to use 'TreeNode((void*)(intptr_t)i, ...)',
                    // aka generate a dummy pointer-sized value to be hashed. The demo below uses that technique. Both are fine.
                    ImGui.PushID(i);
                    if (ImGui.TreeNode("", $"Child {i}"))
                    {
                        ImGui.Text("blah blah");
                        ImGui.SameLine();
                        if (ImGui.SmallButton("button")) { }
                        ImGui.TreePop();
                    }
                    ImGui.PopID();
                }
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Hierarchy lines"))
            {
                // DEMO MARKER: Widgets/Tree Nodes/Hierarchy lines
                HelpMarker("Default option for DrawLinesXXX is stored in style.TreeLinesFlags");
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_DrawLinesNone", ref s_treenodes_hierarchy_base_flags, (int)TreeNodeFlags.DrawLinesNone);
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_DrawLinesFull", ref s_treenodes_hierarchy_base_flags, (int)TreeNodeFlags.DrawLinesFull);
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_DrawLinesToNodes", ref s_treenodes_hierarchy_base_flags, (int)TreeNodeFlags.DrawLinesToNodes);

                var base_flags = (TreeNodeFlags)s_treenodes_hierarchy_base_flags;
                if (ImGui.TreeNodeEx("Parent", base_flags))
                {
                    if (ImGui.TreeNodeEx("Child 1", base_flags))
                    {
                        ImGui.Button("Button for Child 1");
                        ImGui.TreePop();
                    }
                    if (ImGui.TreeNodeEx("Child 2", base_flags))
                    {
                        ImGui.Button("Button for Child 2");
                        ImGui.TreePop();
                    }
                    ImGui.Text("Remaining contents");
                    ImGui.Text("Remaining contents");
                    ImGui.TreePop();
                }

                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Clipping Large Trees"))
            {
                // DEMO MARKER: Widgets/Tree Nodes/Clipping Large Trees
                ImGui.TextWrapped(
                    "- Using ImGuiListClipper with trees is a less easy than on arrays or grids.\n" +
                    "- Refer to 'Demo->Examples->Property Editor' for an example of how to do that.\n" +
                    "- Discuss in #3823");
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Advanced, with Selectable nodes"))
            {
                // DEMO MARKER: Widgets/Tree Nodes/Advanced, with Selectable nodes
                HelpMarker(
                    "This is a more typical looking tree with selectable nodes.\n" +
                    "Click to select, Ctrl+Click to toggle, click on arrows or double-click to open.");
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_OpenOnArrow", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.OpenOnArrow);
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_OpenOnDoubleClick", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.OpenOnDoubleClick);
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_SpanAvailWidth", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.SpanAvailWidth); ImGui.SameLine(); HelpMarker("Extend hit area to all available width instead of allowing more items to be laid out after the node.");
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_SpanFullWidth", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.SpanFullWidth);
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_SpanLabelWidth", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.SpanLabelWidth); ImGui.SameLine(); HelpMarker("Reduce hit area to the text label and a bit of margin.");
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_SpanAllColumns", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.SpanAllColumns); ImGui.SameLine(); HelpMarker("For use in Tables only.");
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_AllowOverlap", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.AllowOverlap);
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_Framed", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.Framed); ImGui.SameLine(); HelpMarker("Draw frame with background (e.g. for CollapsingHeader)");
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_FramePadding", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.FramePadding);
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_NavLeftJumpsToParent", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.NavLeftJumpsToParent);

                HelpMarker("Default option for DrawLinesXXX is stored in style.TreeLinesFlags");
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_DrawLinesNone", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.DrawLinesNone);
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_DrawLinesFull", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.DrawLinesFull);
                ImGui.CheckboxFlags("ImGuiTreeNodeFlags_DrawLinesToNodes", ref s_treenodes_adv_base_flags, (int)TreeNodeFlags.DrawLinesToNodes);

                ImGui.Checkbox("Align label with current X position", ref s_treenodes_align_label_with_current_x_position);
                ImGui.Checkbox("Test tree node as drag source", ref s_treenodes_test_drag_and_drop);
                ImGui.Text("Hello!");
                if (s_treenodes_align_label_with_current_x_position)
                    ImGui.Unindent(ImGui.GetTreeNodeToLabelSpacing());

                // 'selection_mask' is dumb representation of what may be user-side selection state.
                //  You may retain selection state inside or outside your objects in whatever format you see fit.
                // 'node_clicked' is temporary storage of what node we have clicked to process selection at the end
                /// of the loop. May be a pointer to your own node type, etc.
                int node_clicked = -1;
                for (int i = 0; i < 6; i++)
                {
                    // Disable the default "open on single-click behavior" + set Selected flag according to our selection.
                    // To alter selection we use IsItemClicked() && !IsItemToggledOpen(), so clicking on an arrow doesn't alter selection.
                    var node_flags = (TreeNodeFlags)s_treenodes_adv_base_flags;
                    bool is_selected = (s_treenodes_selection_mask & (1 << i)) != 0;
                    if (is_selected)
                        node_flags |= TreeNodeFlags.Selected;
                    if (i < 3)
                    {
                        // Items 0..2 are Tree Node
                        bool node_open = ImGui.TreeNodeEx((nint)i, node_flags, $"Selectable Node {i}");
                        if (ImGui.IsItemClicked() && !ImGui.IsItemToggledOpen())
                            node_clicked = i;
                        if (s_treenodes_test_drag_and_drop && ImGui.BeginDragDropSource())
                        {
                            ImGui.SetDragDropPayload("_TREENODE", ReadOnlySpan<byte>.Empty);
                            ImGui.Text("This is a drag and drop source");
                            ImGui.EndDragDropSource();
                        }
                        if (i == 2 && ((TreeNodeFlags)s_treenodes_adv_base_flags & TreeNodeFlags.SpanLabelWidth) != 0)
                        {
                            // Item 2 has an additional inline button to help demonstrate SpanLabelWidth.
                            ImGui.SameLine();
                            if (ImGui.SmallButton("button")) { }
                        }
                        if (node_open)
                        {
                            ImGui.BulletText("Blah blah\nBlah Blah");
                            ImGui.SameLine();
                            ImGui.SmallButton("Button");
                            ImGui.TreePop();
                        }
                    }
                    else
                    {
                        // Items 3..5 are Tree Leaves
                        // The only reason we use TreeNode at all is to allow selection of the leaf. Otherwise we can
                        // use BulletText() or advance the cursor by GetTreeNodeToLabelSpacing() and call Text().
                        node_flags |= TreeNodeFlags.Leaf | TreeNodeFlags.NoTreePushOnOpen; // TreeNodeFlags.Bullet
                        ImGui.TreeNodeEx((nint)i, node_flags, $"Selectable Leaf {i}");
                        if (ImGui.IsItemClicked() && !ImGui.IsItemToggledOpen())
                            node_clicked = i;
                        if (s_treenodes_test_drag_and_drop && ImGui.BeginDragDropSource())
                        {
                            ImGui.SetDragDropPayload("_TREENODE", ReadOnlySpan<byte>.Empty);
                            ImGui.Text("This is a drag and drop source");
                            ImGui.EndDragDropSource();
                        }
                    }
                }
                if (node_clicked != -1)
                {
                    // Update selection state
                    // (process outside of tree loop to avoid visual inconsistencies during the clicking frame)
                    if (Io.KeyCtrl)
                        s_treenodes_selection_mask ^= 1 << node_clicked;    // Ctrl+Click to toggle
                    else //if (!(selection_mask & (1 << node_clicked))) // Depending on selection behavior you want, may want to preserve selection when clicking on item that is part of the selection
                        s_treenodes_selection_mask = 1 << node_clicked;     // Click to single-select
                }
                if (s_treenodes_align_label_with_current_x_position)
                    ImGui.Indent(ImGui.GetTreeNodeToLabelSpacing());
                ImGui.TreePop();
            }
            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsVerticalSliders()
    //-----------------------------------------------------------------------------

    static int s_vsliders_int_value = 0;
    static readonly float[] s_vsliders_values = { 0.0f, 0.60f, 0.35f, 0.9f, 0.70f, 0.20f, 0.0f };
    static readonly float[] s_vsliders_values2 = { 0.20f, 0.80f, 0.40f, 0.25f };

    private static void DemoWindowWidgetsVerticalSliders()
    {
        if (ImGui.TreeNode("Vertical Sliders"))
        {
            // DEMO MARKER: Widgets/Vertical Sliders
            const float spacing = 4;
            ImGui.PushStyleVar(StyleVar.ItemSpacing, spacing, spacing);

            ImGui.VSliderInt("##int", 18, 160, ref s_vsliders_int_value, 0, 5);
            ImGui.SameLine();

            var values = s_vsliders_values;
            ImGui.PushID("set1");
            for (int i = 0; i < 7; i++)
            {
                if (i > 0) ImGui.SameLine();
                ImGui.PushID(i);
                // ImColor::HSV(h, s, v) equivalents (alpha = 1.0f)
                var (r0, g0, b0) = ImGui.ColorConvertHSVtoRGB(i / 7.0f, 0.5f, 0.5f);
                ImGui.PushStyleColor(Col.FrameBg, r0, g0, b0, 1.0f);
                var (r1, g1, b1) = ImGui.ColorConvertHSVtoRGB(i / 7.0f, 0.6f, 0.5f);
                ImGui.PushStyleColor(Col.FrameBgHovered, r1, g1, b1, 1.0f);
                var (r2, g2, b2) = ImGui.ColorConvertHSVtoRGB(i / 7.0f, 0.7f, 0.5f);
                ImGui.PushStyleColor(Col.FrameBgActive, r2, g2, b2, 1.0f);
                var (r3, g3, b3) = ImGui.ColorConvertHSVtoRGB(i / 7.0f, 0.9f, 0.9f);
                ImGui.PushStyleColor(Col.SliderGrab, r3, g3, b3, 1.0f);
                ImGui.VSliderFloat("##v", 18, 160, ref values[i], 0.0f, 1.0f, "");
                if (ImGui.IsItemActive() || ImGui.IsItemHovered())
                    ImGui.SetTooltip($"{values[i]:0.000}");
                ImGui.PopStyleColor(4);
                ImGui.PopID();
            }
            ImGui.PopID();

            ImGui.SameLine();
            ImGui.PushID("set2");
            var values2 = s_vsliders_values2;
            const int rows = 3;
            float small_slider_w = 18;
            float small_slider_h = (int)((160.0f - (rows - 1) * spacing) / rows);
            for (int nx = 0; nx < 4; nx++)
            {
                if (nx > 0) ImGui.SameLine();
                ImGui.BeginGroup();
                for (int ny = 0; ny < rows; ny++)
                {
                    ImGui.PushID(nx * rows + ny);
                    ImGui.VSliderFloat("##v", small_slider_w, small_slider_h, ref values2[nx], 0.0f, 1.0f, "");
                    if (ImGui.IsItemActive() || ImGui.IsItemHovered())
                        ImGui.SetTooltip($"{values2[nx]:0.000}");
                    ImGui.PopID();
                }
                ImGui.EndGroup();
            }
            ImGui.PopID();

            ImGui.SameLine();
            ImGui.PushID("set3");
            for (int i = 0; i < 4; i++)
            {
                if (i > 0) ImGui.SameLine();
                ImGui.PushID(i);
                ImGui.PushStyleVar(StyleVar.GrabMinSize, 40);
                ImGui.VSliderFloat("##v", 40, 160, ref values[i], 0.0f, 1.0f, "%.2f\nsec");
                ImGui.PopStyleVar();
                ImGui.PopID();
            }
            ImGui.PopID();
            ImGui.PopStyleVar();
            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgets()
    //-----------------------------------------------------------------------------

    private static void DemoWindowWidgets(DemoWindowData data)
    {
        //ImGui.SetNextItemOpen(true, Cond.Once);
        if (!ImGui.CollapsingHeader("Widgets"))
            return;
        // DEMO MARKER: Widgets

        bool disable_all = data.DisableSections; // The Checkbox for that is inside the "Disabled" section at the bottom
        if (disable_all)
            ImGui.BeginDisabled();

        DemoWindowWidgetsBasic();
        DemoWindowWidgetsBullets();
        DemoWindowWidgetsCollapsingHeaders();
        DemoWindowWidgetsComboBoxes();
        DemoWindowWidgetsColorAndPickers();
        DemoWindowWidgetsDataTypes();

        if (disable_all)
            ImGui.EndDisabled();
        DemoWindowWidgetsDisableBlocks(data);
        if (disable_all)
            ImGui.BeginDisabled();

        DemoWindowWidgetsDragAndDrop();
        DemoWindowWidgetsDragsAndSliders();
        DemoWindowWidgetsFonts();
        DemoWindowWidgetsImages();
        DemoWindowWidgetsListBoxes();
        DemoWindowWidgetsMultiComponents();
        DemoWindowWidgetsPlotting();
        DemoWindowWidgetsProgressBars();
        DemoWindowWidgetsQueryingStatuses();
        DemoWindowWidgetsSelectables();
        DemoWindowWidgetsSelectionAndMultiSelect(data);
        DemoWindowWidgetsTabs();
        DemoWindowWidgetsText();
        DemoWindowWidgetsTextFilter();
        DemoWindowWidgetsTextInput();
        DemoWindowWidgetsTooltips();
        DemoWindowWidgetsTreeNodes();
        DemoWindowWidgetsVerticalSliders();

        if (disable_all)
            ImGui.EndDisabled();
    }
}
