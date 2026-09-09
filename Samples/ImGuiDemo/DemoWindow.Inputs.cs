// C# port of imgui_demo.cpp's DemoWindowInputs() (upstream lines ~7824-8130).
// Covers: inputs status, WantCapture overrides, shortcuts & routing policies,
// mouse cursors, tabbing, focus from code, and dragging.

using System.Text;
using SdlSharp.ImGui;

namespace ImGuiDemo;

internal static unsafe partial class DemoWindow
{
    // -------------------------------------------------------------------------
    // SECTION: Inputs & Focus — state mirroring the C++ function-static locals
    // -------------------------------------------------------------------------

    // Smallest positive normalized float, i.e. C's FLT_MIN. Used as -FltMin to
    // mean "right-align to the far right edge" like the C++ demo's -FLT_MIN.
    private const float FltMin = 1.175494351e-38f;

    // "WantCapture override" section
    static int s_inputs_capture_override_mouse = -1;
    static int s_inputs_capture_override_keyboard = -1;

    // "Shortcuts" section
    // (kept as int backing fields so CheckboxFlags/RadioButton can edit the raw routing bits;
    //  cast to the InputFlags enum at Shortcut/SetNextItemShortcut call sites)
    static int s_inputs_route_options = (int)InputFlags.Repeat;
    static int s_inputs_route_type = (int)InputFlags.RouteFocused;
    static float s_inputs_shortcut_factor = 0.5f;

    // "Tabbing" section (shared buffer, like the C++ 'static char buf[32] = "hello"')
    static readonly byte[] s_inputs_tabbing_buf = MakeBuffer("hello", 32);

    // "Focus from code" section
    static readonly byte[] s_inputs_focus_buf = MakeBuffer("click on a button to set focus", 128);
    static readonly float[] s_inputs_focus_f3 = { 0.0f, 0.0f, 0.0f };

    private static byte[] MakeBuffer(string initial, int size)
    {
        var buf = new byte[size];
        Encoding.UTF8.GetBytes(initial, buf);
        return buf;
    }

    private static void DemoWindowInputs()
    {
        if (ImGui.CollapsingHeader("Inputs & Focus"))
        {
            // Display inputs submitted to ImGuiIO
            ImGui.SetNextItemOpen(true, Cond.Once);
            bool inputs_opened = ImGui.TreeNode("Inputs");
            ImGui.SameLine();
            HelpMarker(
                "This is a simplified view. See more detailed input state:\n" +
                "- in 'Tools->Metrics/Debugger->Inputs'.\n" +
                "- in 'Tools->Debug Log->IO'.");
            if (inputs_opened)
            {
                // DEMO MARKER: Inputs & Focus/Inputs
                if (ImGui.IsMousePosValid())
                    ImGui.Text($"Mouse pos: ({Io.MousePos.X:0.##}, {Io.MousePos.Y:0.##})");
                else
                    ImGui.Text("Mouse pos: <INVALID>");
                ImGui.Text($"Mouse delta: ({Io.MouseDelta.X:0.##}, {Io.MouseDelta.Y:0.##})");
                ImGui.Text("Mouse down:");
                for (int i = 0; i < 5; i++) if (ImGui.IsMouseDown((MouseButton)i)) { ImGui.SameLine(); ImGui.Text($"b{i} ({Io.GetMouseDownDuration((MouseButton)i):0.00} secs)"); }
                ImGui.Text($"Mouse wheel: {Io.MouseWheel:0.0}");
                ImGui.Text("Mouse clicked count:");
                for (int i = 0; i < 5; i++)
                {
                    int clicked_count = ImGui.GetMouseClickedCount((MouseButton)i);
                    if (clicked_count > 0) { ImGui.SameLine(); ImGui.Text($"b{i}: {clicked_count}"); }
                }

                // We iterate the named ImGuiKey range. (The C++ demo also handles legacy native
                // indices for old backends; the wrapper only exposes named keys, which is the
                // recommended range to iterate anyway.)
                ImGui.Text("Keys down:");
                for (Key key = Key.Tab; key <= Key.MouseWheelY; key++)
                {
                    if (!ImGui.IsKeyDown(key))
                        continue;
                    ImGui.SameLine();
                    ImGui.Text($"\"{ImGui.GetKeyName(key)}\" {(int)key}");
                }
                ImGui.Text($"Keys mods: {(Io.KeyCtrl ? "CTRL " : "")}{(Io.KeyShift ? "SHIFT " : "")}{(Io.KeyAlt ? "ALT " : "")}{(Io.KeySuper ? "SUPER " : "")}");
                ImGui.Text("Chars queue:");
                foreach (var c in Io.InputQueueCharacters)
                {
                    ImGui.SameLine();
                    ImGui.Text($"'{c}' ({(int)c})");
                }

                ImGui.TreePop();
            }

            // Display ImGuiIO output flags
            ImGui.SetNextItemOpen(true, Cond.Once);
            bool outputs_opened = ImGui.TreeNode("Outputs");
            ImGui.SameLine();
            HelpMarker(
                "The value of io.WantCaptureMouse and io.WantCaptureKeyboard are normally set by Dear ImGui " +
                "to instruct your application of how to route inputs. Typically, when a value is true, it means " +
                "Dear ImGui wants the corresponding inputs and we expect the underlying application to ignore them.\n\n" +
                "The most typical case is: when hovering a window, Dear ImGui set io.WantCaptureMouse to true, " +
                "and underlying application should ignore mouse inputs (in practice there are many and more subtle " +
                "rules leading to how those flags are set).");
            if (outputs_opened)
            {
                // DEMO MARKER: Inputs & Focus/Outputs
                ImGui.Text($"io.WantCaptureMouse: {(Io.WantCaptureMouse ? 1 : 0)}");
                ImGui.Text($"io.WantCaptureMouseUnlessPopupClose: {(Io.WantCaptureMouseUnlessPopupClose ? 1 : 0)}");
                ImGui.Text($"io.WantCaptureKeyboard: {(Io.WantCaptureKeyboard ? 1 : 0)}");
                ImGui.Text($"io.WantTextInput: {(Io.WantTextInput ? 1 : 0)}");
                ImGui.Text($"io.WantSetMousePos: {(Io.WantSetMousePos ? 1 : 0)}");
                ImGui.Text($"io.NavActive: {(Io.NavActive ? 1 : 0)}, io.NavVisible: {(Io.NavVisible ? 1 : 0)}");

                // DEMO MARKER: Inputs & Focus/Outputs/WantCapture override
                if (ImGui.TreeNode("WantCapture override"))
                {
                    HelpMarker(
                        "Hovering the colored canvas will override io.WantCaptureXXX fields.\n" +
                        "Notice how normally (when set to none), the value of io.WantCaptureKeyboard would be false when hovering " +
                        "and true when clicking.");
                    string[] capture_override_desc = { "None", "Set to false", "Set to true" };
                    ImGui.SetNextItemWidth(ImGui.GetFontSize() * 15);
                    ImGui.SliderInt("SetNextFrameWantCaptureMouse() on hover", ref s_inputs_capture_override_mouse, -1, +1, capture_override_desc[s_inputs_capture_override_mouse + 1], SliderFlags.AlwaysClamp);
                    ImGui.SetNextItemWidth(ImGui.GetFontSize() * 15);
                    ImGui.SliderInt("SetNextFrameWantCaptureKeyboard() on hover", ref s_inputs_capture_override_keyboard, -1, +1, capture_override_desc[s_inputs_capture_override_keyboard + 1], SliderFlags.AlwaysClamp);

                    ImGui.ColorButton("##panel", 0.7f, 0.1f, 0.7f, 1.0f, ColorEditFlags.NoTooltip | ColorEditFlags.NoDragDrop, 128.0f, 96.0f); // Dummy item
                    if (ImGui.IsItemHovered() && s_inputs_capture_override_mouse != -1)
                        ImGui.SetNextFrameWantCaptureMouse(s_inputs_capture_override_mouse == 1);
                    if (ImGui.IsItemHovered() && s_inputs_capture_override_keyboard != -1)
                        ImGui.SetNextFrameWantCaptureKeyboard(s_inputs_capture_override_keyboard == 1);

                    ImGui.TreePop();
                }
                ImGui.TreePop();
            }

            // Demonstrate using Shortcut() and Routing Policies.
            // The general flow is:
            // - Code interested in a chord (e.g. "Ctrl+A") declares their intent.
            // - Multiple locations may be interested in same chord! Routing helps find a winner.
            // - Every frame, we resolve all claims and assign one owner if the modifiers are matching.
            // - The lower-level function is 'bool SetShortcutRouting()', returns true when caller got the route.
            // - Most of the times, SetShortcutRouting() is not called directly. User mostly calls Shortcut() with routing flags.
            // - If you call Shortcut() WITHOUT any routing option, it uses ImGuiInputFlags_RouteFocused.
            // TL;DR: Most uses will simply be:
            // - Shortcut(ImGuiMod_Ctrl | ImGuiKey_A); // Use ImGuiInputFlags_RouteFocused policy.
            if (ImGui.TreeNode("Shortcuts"))
            {
                // DEMO MARKER: Inputs & Focus/Shortcuts
                ImGui.CheckboxFlags("ImGuiInputFlags_Repeat", ref s_inputs_route_options, (int)InputFlags.Repeat);
                ImGui.RadioButton("ImGuiInputFlags_RouteActive", ref s_inputs_route_type, (int)InputFlags.RouteActive);
                ImGui.RadioButton("ImGuiInputFlags_RouteFocused (default)", ref s_inputs_route_type, (int)InputFlags.RouteFocused);
                ImGui.Indent();
                ImGui.BeginDisabled(s_inputs_route_type != (int)InputFlags.RouteFocused);
                ImGui.CheckboxFlags("ImGuiInputFlags_RouteOverActive##0", ref s_inputs_route_options, (int)InputFlags.RouteOverActive);
                ImGui.EndDisabled();
                ImGui.Unindent();
                ImGui.RadioButton("ImGuiInputFlags_RouteGlobal", ref s_inputs_route_type, (int)InputFlags.RouteGlobal);
                ImGui.Indent();
                ImGui.BeginDisabled(s_inputs_route_type != (int)InputFlags.RouteGlobal);
                ImGui.CheckboxFlags("ImGuiInputFlags_RouteOverFocused", ref s_inputs_route_options, (int)InputFlags.RouteOverFocused);
                ImGui.CheckboxFlags("ImGuiInputFlags_RouteOverActive", ref s_inputs_route_options, (int)InputFlags.RouteOverActive);
                ImGui.CheckboxFlags("ImGuiInputFlags_RouteUnlessBgFocused", ref s_inputs_route_options, (int)InputFlags.RouteUnlessBgFocused);
                ImGui.EndDisabled();
                ImGui.Unindent();
                ImGui.RadioButton("ImGuiInputFlags_RouteAlways", ref s_inputs_route_type, (int)InputFlags.RouteAlways);
                InputFlags flags = (InputFlags)(s_inputs_route_type | s_inputs_route_options); // Merged flags
                if (s_inputs_route_type != (int)InputFlags.RouteGlobal)
                    flags &= ~(InputFlags.RouteOverFocused | InputFlags.RouteOverActive | InputFlags.RouteUnlessBgFocused);

                ImGui.SeparatorText("Using SetNextItemShortcut()");
                ImGui.Text("Ctrl+S");
                ImGui.SetNextItemShortcut(Key.ModCtrl | Key.S, flags | InputFlags.Tooltip);
                ImGui.Button("Save");
                ImGui.Text("Alt+F");
                ImGui.SetNextItemShortcut(Key.ModAlt | Key.F, flags | InputFlags.Tooltip);
                ImGui.SliderFloat("Factor", ref s_inputs_shortcut_factor, 0.0f, 1.0f);

                ImGui.SeparatorText("Using Shortcut()");
                float line_height = ImGui.GetTextLineHeightWithSpacing();
                const Key key_chord = Key.ModCtrl | Key.A;

                ImGui.Text("Ctrl+A");
                ImGui.Text($"IsWindowFocused: {(ImGui.IsWindowFocused() ? 1 : 0)}, Shortcut: {(ImGui.Shortcut(key_chord, flags) ? "PRESSED" : "...")}");

                ImGui.PushStyleColor(Col.ChildBg, 1.0f, 0.0f, 1.0f, 0.1f);

                ImGui.BeginChild("WindowA", -FltMin, line_height * 14, ChildFlags.Borders);
                ImGui.Text("Press Ctrl+A and see who receives it!");
                ImGui.Separator();

                // 1: Window polling for Ctrl+A
                ImGui.Text("(in WindowA)");
                ImGui.Text($"IsWindowFocused: {(ImGui.IsWindowFocused() ? 1 : 0)}, Shortcut: {(ImGui.Shortcut(key_chord, flags) ? "PRESSED" : "...")}");

                // 2: InputText also polling for Ctrl+A: it always uses _RouteFocused internally (gets priority when active)
                // (Commented in upstream because the owner-aware version of Shortcut() is still in imgui_internal.h)

                // 3: Dummy child is not claiming the route: focusing them shouldn't steal route away from WindowA
                ImGui.BeginChild("ChildD", -FltMin, line_height * 4, ChildFlags.Borders);
                ImGui.Text("(in ChildD: not using same Shortcut)");
                ImGui.Text($"IsWindowFocused: {(ImGui.IsWindowFocused() ? 1 : 0)}");
                ImGui.EndChild();

                // 4: Child window polling for Ctrl+A. It is deeper than WindowA and gets priority when focused.
                ImGui.BeginChild("ChildE", -FltMin, line_height * 4, ChildFlags.Borders);
                ImGui.Text("(in ChildE: using same Shortcut)");
                ImGui.Text($"IsWindowFocused: {(ImGui.IsWindowFocused() ? 1 : 0)}, Shortcut: {(ImGui.Shortcut(key_chord, flags) ? "PRESSED" : "...")}");
                ImGui.EndChild();

                // 5: In a popup
                if (ImGui.Button("Open Popup"))
                    ImGui.OpenPopup("PopupF");
                if (ImGui.BeginPopup("PopupF"))
                {
                    ImGui.Text("(in PopupF)");
                    ImGui.Text($"IsWindowFocused: {(ImGui.IsWindowFocused() ? 1 : 0)}, Shortcut: {(ImGui.Shortcut(key_chord, flags) ? "PRESSED" : "...")}");
                    ImGui.EndPopup();
                }
                ImGui.EndChild();
                ImGui.PopStyleColor();

                ImGui.TreePop();
            }

            // Display mouse cursors
            if (ImGui.TreeNode("Mouse Cursors"))
            {
                // DEMO MARKER: Inputs & Focus/Mouse Cursors
                string[] mouse_cursors_names = { "Arrow", "TextInput", "ResizeAll", "ResizeNS", "ResizeEW", "ResizeNESW", "ResizeNWSE", "Hand", "Wait", "Progress", "NotAllowed" };

                MouseCursor current = ImGui.GetMouseCursor();
                string cursor_name = (current >= MouseCursor.Arrow) && ((int)current < mouse_cursors_names.Length) ? mouse_cursors_names[(int)current] : "N/A";
                ImGui.Text($"Current mouse cursor = {(int)current}: {cursor_name}");
                ImGui.BeginDisabled(true);
                int backend_flags = (int)Io.BackendFlags;
                ImGui.CheckboxFlags("io.BackendFlags: HasMouseCursors", ref backend_flags, (int)BackendFlags.HasMouseCursors);
                ImGui.EndDisabled();

                ImGui.Text("Hover to see mouse cursors:");
                ImGui.SameLine(); HelpMarker(
                    "Your application can render a different mouse cursor based on what ImGui::GetMouseCursor() returns. " +
                    "If software cursor rendering (io.MouseDrawCursor) is set ImGui will draw the right cursor for you, " +
                    "otherwise your backend needs to handle it.");
                for (int i = 0; i < mouse_cursors_names.Length; i++)
                {
                    string label = $"Mouse cursor {i}: {mouse_cursors_names[i]}";
                    ImGui.Bullet(); ImGui.Selectable(label, false);
                    if (ImGui.IsItemHovered())
                        ImGui.SetMouseCursor((MouseCursor)i);
                }
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Tabbing"))
            {
                // DEMO MARKER: Inputs & Focus/Tabbing
                ImGui.Text("Use Tab/Shift+Tab to cycle through keyboard editable fields.");
                ImGui.InputText("1", s_inputs_tabbing_buf);
                ImGui.InputText("2", s_inputs_tabbing_buf);
                ImGui.InputText("3", s_inputs_tabbing_buf);
                ImGui.PushItemFlag(ItemFlags.NoTabStop, true);
                ImGui.InputText("4 (tab skip)", s_inputs_tabbing_buf);
                ImGui.SameLine(); HelpMarker("Item won't be cycled through when using TAB or Shift+Tab.");
                ImGui.PopItemFlag();
                ImGui.InputText("5", s_inputs_tabbing_buf);
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Focus from code"))
            {
                // DEMO MARKER: Inputs & Focus/Focus from code
                bool focus_1 = ImGui.Button("Focus on 1"); ImGui.SameLine();
                bool focus_2 = ImGui.Button("Focus on 2"); ImGui.SameLine();
                bool focus_3 = ImGui.Button("Focus on 3");
                int has_focus = 0;

                if (focus_1) ImGui.SetKeyboardFocusHere();
                ImGui.InputText("1", s_inputs_focus_buf);
                if (ImGui.IsItemActive()) has_focus = 1;

                if (focus_2) ImGui.SetKeyboardFocusHere();
                ImGui.InputText("2", s_inputs_focus_buf);
                if (ImGui.IsItemActive()) has_focus = 2;

                ImGui.PushItemFlag(ItemFlags.NoTabStop, true);
                if (focus_3) ImGui.SetKeyboardFocusHere();
                ImGui.InputText("3 (tab skip)", s_inputs_focus_buf);
                if (ImGui.IsItemActive()) has_focus = 3;
                ImGui.SameLine(); HelpMarker("Item won't be cycled through when using TAB or Shift+Tab.");
                ImGui.PopItemFlag();

                if (has_focus != 0)
                    ImGui.Text($"Item with focus: {has_focus}");
                else
                    ImGui.Text("Item with focus: <none>");

                // Use >= 0 parameter to SetKeyboardFocusHere() to focus an upcoming item
                int focus_ahead = -1;
                if (ImGui.Button("Focus on X")) { focus_ahead = 0; } ImGui.SameLine();
                if (ImGui.Button("Focus on Y")) { focus_ahead = 1; } ImGui.SameLine();
                if (ImGui.Button("Focus on Z")) { focus_ahead = 2; }
                if (focus_ahead != -1) ImGui.SetKeyboardFocusHere(focus_ahead);
                ImGui.SliderFloat3("Float3", s_inputs_focus_f3, 0.0f, 1.0f);

                ImGui.TextWrapped("NB: Cursor & selection are preserved when refocusing last used item in code.");
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Dragging"))
            {
                // DEMO MARKER: Inputs & Focus/Dragging
                ImGui.TextWrapped("You can use ImGui::GetMouseDragDelta(0) to query for the dragged amount on any widget.");
                for (int button = 0; button < 3; button++)
                {
                    ImGui.Text($"IsMouseDragging({button}):");
                    ImGui.Text($"  w/ default threshold: {(ImGui.IsMouseDragging((MouseButton)button) ? 1 : 0)},");
                    ImGui.Text($"  w/ zero threshold: {(ImGui.IsMouseDragging((MouseButton)button, 0.0f) ? 1 : 0)},");
                    ImGui.Text($"  w/ large threshold: {(ImGui.IsMouseDragging((MouseButton)button, 20.0f) ? 1 : 0)},");
                }

                ImGui.Button("Drag Me");
                if (ImGui.IsItemActive())
                {
                    // Draw a line between the button and the mouse cursor.
                    var clicked_pos = Io.GetMouseClickedPosition(MouseButton.Left);
                    ImGui.GetForegroundDrawList().AddLine(clicked_pos, Io.MousePos, ImGui.GetColorU32(Col.Button), 4.0f);
                }

                // Drag operations gets "unlocked" when the mouse has moved past a certain threshold
                // (the default threshold is stored in io.MouseDragThreshold). You can request a lower or higher
                // threshold using the second parameter of IsMouseDragging() and GetMouseDragDelta().
                Vec2 value_raw = ImGui.GetMouseDragDelta(MouseButton.Left, 0.0f);
                Vec2 value_with_lock_threshold = ImGui.GetMouseDragDelta(MouseButton.Left);
                Vec2 mouse_delta = Io.MouseDelta;
                ImGui.Text("GetMouseDragDelta(0):");
                ImGui.Text($"  w/ default threshold: ({value_with_lock_threshold.X:0.0}, {value_with_lock_threshold.Y:0.0})");
                ImGui.Text($"  w/ zero threshold: ({value_raw.X:0.0}, {value_raw.Y:0.0})");
                ImGui.Text($"io.MouseDelta: ({mouse_delta.X:0.0}, {mouse_delta.Y:0.0})");
                ImGui.TreePop();
            }
        }
    }
}
