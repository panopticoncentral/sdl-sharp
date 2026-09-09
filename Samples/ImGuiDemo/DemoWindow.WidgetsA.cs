// Port of imgui_demo.cpp [SECTION] DemoWindowWidgetsBasic() .. DemoWindowWidgetsMultiComponents()
// (upstream lines ~832-1986). C++ function-static locals become private static fields prefixed
// with the section name.

using System.Runtime.InteropServices;
using SdlSharp.ImGui;

namespace ImGuiDemo;

internal static unsafe partial class DemoWindow
{
    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsBasic()
    //-----------------------------------------------------------------------------

    private static int _basicClicked;
    private static bool _basicCheck = true;
    private static int _basicE;
    private static int _basicCounter;
    private static string _basicStr0 = "Hello, world!";
    private static string _basicStr1 = "";
    private static int _basicI0 = 123;
    private static float _basicF0 = 0.001f;
    private static double _basicD0 = 999999.00000001;
    private static float _basicF1 = 1e10f;
    private static readonly float[] _basicVec4a = [0.10f, 0.20f, 0.30f, 0.44f];
    private static int _basicDragI1 = 50;
    private static int _basicDragI2 = 42;
    private static int _basicDragI3 = 128;
    private static float _basicDragF1 = 1.00f;
    private static float _basicDragF2 = 0.0067f;
    private static int _basicSliderI1;
    private static float _basicSliderF1 = 0.123f;
    private static float _basicSliderF2;
    private static float _basicAngle;
    private static int _basicElem;
    private static readonly float[] _basicCol1 = [1.0f, 0.0f, 0.2f];
    private static readonly float[] _basicCol2 = [0.4f, 0.7f, 0.0f, 0.5f];
    private static int _basicComboItemCurrent;
    private static int _basicListboxItemCurrent = 1;

    private static void DemoWindowWidgetsBasic()
    {
        if (ImGui.TreeNode("Basic"))
        {
            // DEMO MARKER: Widgets/Basic
            ImGui.SeparatorText("General");

            // DEMO MARKER: Widgets/Basic/Button
            if (ImGui.Button("Button"))
                _basicClicked++;
            if ((_basicClicked & 1) != 0)
            {
                ImGui.SameLine();
                ImGui.Text("Thanks for clicking me!");
            }

            // DEMO MARKER: Widgets/Basic/Checkbox
            ImGui.Checkbox("checkbox", ref _basicCheck);

            // DEMO MARKER: Widgets/Basic/RadioButton
            ImGui.RadioButton("radio a", ref _basicE, 0); ImGui.SameLine();
            ImGui.RadioButton("radio b", ref _basicE, 1); ImGui.SameLine();
            ImGui.RadioButton("radio c", ref _basicE, 2);

            ImGui.AlignTextToFramePadding();
            ImGui.TextLinkOpenURL("Hyperlink", "https://github.com/ocornut/imgui/wiki/Error-Handling");

            // Color buttons, demonstrate using PushID() to add unique identifier in the ID stack, and changing style.
            // DEMO MARKER: Widgets/Basic/Buttons (Colored)
            for (var i = 0; i < 7; i++)
            {
                if (i > 0)
                    ImGui.SameLine();
                ImGui.PushID(i);
                var (r1, g1, b1) = ImGui.ColorConvertHSVtoRGB(i / 7.0f, 0.6f, 0.6f);
                var (r2, g2, b2) = ImGui.ColorConvertHSVtoRGB(i / 7.0f, 0.7f, 0.7f);
                var (r3, g3, b3) = ImGui.ColorConvertHSVtoRGB(i / 7.0f, 0.8f, 0.8f);
                ImGui.PushStyleColor(Col.Button, r1, g1, b1, 1.0f);
                ImGui.PushStyleColor(Col.ButtonHovered, r2, g2, b2, 1.0f);
                ImGui.PushStyleColor(Col.ButtonActive, r3, g3, b3, 1.0f);
                ImGui.Button("Click");
                ImGui.PopStyleColor(3);
                ImGui.PopID();
            }

            // Use AlignTextToFramePadding() to align text baseline to the baseline of framed widgets elements
            // (otherwise a Text+SameLine+Button sequence will have the text a little too high by default!)
            // See 'Demo->Layout->Text Baseline Alignment' for details.
            ImGui.AlignTextToFramePadding();
            ImGui.Text("Hold to repeat:");
            ImGui.SameLine();

            // Arrow buttons with Repeater
            // DEMO MARKER: Widgets/Basic/Buttons (Repeating)
            var spacing = Style.ItemInnerSpacing.X;
            ImGui.PushItemFlag(ItemFlags.ButtonRepeat, true);
            if (ImGui.ArrowButton("##left", Dir.Left)) { _basicCounter--; }
            ImGui.SameLine(0.0f, spacing);
            if (ImGui.ArrowButton("##right", Dir.Right)) { _basicCounter++; }
            ImGui.PopItemFlag();
            ImGui.SameLine();
            ImGui.Text($"{_basicCounter}");

            ImGui.Button("Tooltip");
            ImGui.SetItemTooltip("I am a tooltip");

            ImGui.LabelText("label", "Value");

            ImGui.SeparatorText("Inputs");

            {
                // If you want to use InputText() with std::string or any custom dynamic string type:
                // - For std::string: use the wrapper in misc/cpp/imgui_stdlib.h/.cpp
                // - Otherwise, see the 'Dear ImGui Demo->Widgets->Text Input->Resize Callback' for using ImGuiInputTextFlags_CallbackResize.
                // (The C# wrapper's ref-string InputText overload auto-grows the buffer for us.)
                // DEMO MARKER: Widgets/Basic/InputText
                ImGui.InputText("input text", ref _basicStr0);
                ImGui.SameLine(); HelpMarker(
                    "USER:\n" +
                    "Hold Shift or use mouse to select text.\n" +
                    "Ctrl+Left/Right to word jump.\n" +
                    "Ctrl+A or Double-Click to select all.\n" +
                    "Ctrl+X,Ctrl+C,Ctrl+V for clipboard.\n" +
                    "Ctrl+Z to undo, Ctrl+Y/Ctrl+Shift+Z to redo.\n" +
                    "Escape to revert.\n\n" +
                    "PROGRAMMER:\n" +
                    "You can use the ImGuiInputTextFlags_CallbackResize facility if you need to wire InputText() " +
                    "to a dynamic string type. See misc/cpp/imgui_stdlib.h for an example (this is not demonstrated " +
                    "in imgui_demo.cpp).");

                ImGui.InputTextWithHint("input text (w/ hint)", "enter text here", ref _basicStr1);

                // DEMO MARKER: Widgets/Basic/InputInt, InputFloat
                ImGui.InputInt("input int", ref _basicI0);

                ImGui.InputFloat("input float", ref _basicF0, 0.01f, 1.0f, "%.3f");

                ImGui.InputDouble("input double", ref _basicD0, 0.01, 1.0, "%.8f");

                ImGui.InputFloat("input scientific", ref _basicF1, 0.0f, 0.0f, "%e");
                ImGui.SameLine(); HelpMarker(
                    "You can input value using the scientific notation,\n" +
                    "  e.g. \"1e+8\" becomes \"100000000\".");

                ImGui.InputFloat3("input float3", _basicVec4a);
            }

            ImGui.SeparatorText("Drags");

            {
                // DEMO MARKER: Widgets/Basic/DragInt, DragFloat
                ImGui.DragInt("drag int", ref _basicDragI1, 1);
                ImGui.SameLine(); HelpMarker(
                    "Click and drag to edit value.\n" +
                    "Hold Shift/Alt for faster/slower edit.\n" +
                    "Double-Click or Ctrl+Click to input value.");
                ImGui.DragInt("drag int 0..100", ref _basicDragI2, 1, 0, 100, "%d%%", SliderFlags.AlwaysClamp);
                ImGui.DragInt("drag int wrap 100..200", ref _basicDragI3, 1, 100, 200, "%d", SliderFlags.WrapAround);

                ImGui.DragFloat("drag float", ref _basicDragF1, 0.005f);
                ImGui.DragFloat("drag small float", ref _basicDragF2, 0.0001f, 0.0f, 0.0f, "%.06f ns");
                //ImGui.DragFloat("drag wrap -1..1", ref f3, 0.005f, -1.0f, 1.0f, null, SliderFlags.WrapAround);
            }

            ImGui.SeparatorText("Sliders");

            {
                // DEMO MARKER: Widgets/Basic/SliderInt, SliderFloat
                ImGui.SliderInt("slider int", ref _basicSliderI1, -1, 3);
                ImGui.SameLine(); HelpMarker("Ctrl+Click to input value.");

                ImGui.SliderFloat("slider float", ref _basicSliderF1, 0.0f, 1.0f, "ratio = %.3f");
                ImGui.SliderFloat("slider float (log)", ref _basicSliderF2, -10.0f, 10.0f, "%.4f", SliderFlags.Logarithmic);

                // DEMO MARKER: Widgets/Basic/SliderAngle
                ImGui.SliderAngle("slider angle", ref _basicAngle);

                // Using the format string to display a name instead of an integer.
                // Here we completely omit '%d' from the format string, so it'll only display a name.
                // This technique can also be used with DragInt().
                // DEMO MARKER: Widgets/Basic/Slider (enum)
                string[] elemsNames = ["Fire", "Earth", "Air", "Water"];
                var elemName = (_basicElem >= 0 && _basicElem < elemsNames.Length) ? elemsNames[_basicElem] : "Unknown";
                ImGui.SliderInt("slider enum", ref _basicElem, 0, elemsNames.Length - 1, elemName); // Use SliderFlags.NoInput flag to disable Ctrl+Click here.
                ImGui.SameLine(); HelpMarker("Using the format string parameter to display a name instead of the underlying integer.");
            }

            ImGui.SeparatorText("Selectors/Pickers");

            {
                // DEMO MARKER: Widgets/Basic/ColorEdit3, ColorEdit4
                ImGui.ColorEdit3("color 1", ref _basicCol1[0], ref _basicCol1[1], ref _basicCol1[2]);
                ImGui.SameLine(); HelpMarker(
                    "Click on the color square to open a color picker.\n" +
                    "Click and hold to use drag and drop.\n" +
                    "Right-Click on the color square to show options.\n" +
                    "Ctrl+Click on individual component to input value.\n");

                ImGui.ColorEdit4("color 2", _basicCol2);
            }

            {
                // Using the _simplified_ one-liner Combo() api here
                // See "Combo" section for examples of how to use the more flexible BeginCombo()/EndCombo() api.
                // DEMO MARKER: Widgets/Basic/Combo
                string[] items = ["AAAA", "BBBB", "CCCC", "DDDD", "EEEE", "FFFF", "GGGG", "HHHH", "IIIIIII", "JJJJ", "KKKKKKK"];
                ImGui.Combo("combo", ref _basicComboItemCurrent, items);
                ImGui.SameLine(); HelpMarker(
                    "Using the simplified one-liner Combo API here.\n" +
                    "Refer to the \"Combo\" section below for an explanation of how to use the more flexible and general BeginCombo/EndCombo API.");
            }

            {
                // Using the _simplified_ one-liner ListBox() api here
                // See "List boxes" section for examples of how to use the more flexible BeginListBox()/EndListBox() api.
                // DEMO MARKER: Widgets/Basic/ListBox
                string[] items = ["Apple", "Banana", "Cherry", "Kiwi", "Mango", "Orange", "Pineapple", "Strawberry", "Watermelon"];
                ImGui.ListBox("listbox", ref _basicListboxItemCurrent, items, 4);
                ImGui.SameLine(); HelpMarker(
                    "Using the simplified one-liner ListBox API here.\n" +
                    "Refer to the \"List boxes\" section below for an explanation of how to use the more flexible and general BeginListBox/EndListBox API.");
            }

            // Testing ImGuiOnceUponAFrame helper.
            //static ImGuiOnceUponAFrame once;
            //for (int i = 0; i < 5; i++)
            //    if (once)
            //        ImGui.Text("This will be displayed only once.");

            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsBullets()
    //-----------------------------------------------------------------------------

    private static void DemoWindowWidgetsBullets()
    {
        if (ImGui.TreeNode("Bullets"))
        {
            // DEMO MARKER: Widgets/Bullets
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
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsCollapsingHeaders()
    //-----------------------------------------------------------------------------

    private static bool _headersClosableGroup = true;

    private static void DemoWindowWidgetsCollapsingHeaders()
    {
        if (ImGui.TreeNode("Collapsing Headers"))
        {
            // DEMO MARKER: Widgets/Collapsing Headers
            ImGui.Checkbox("Show 2nd header", ref _headersClosableGroup);
            if (ImGui.CollapsingHeader("Header", TreeNodeFlags.None))
            {
                ImGui.Text($"IsItemHovered: {(ImGui.IsItemHovered() ? 1 : 0)}");
                for (var i = 0; i < 5; i++)
                    ImGui.Text($"Some content {i}");
            }
            if (ImGui.CollapsingHeader("Header with a close button", ref _headersClosableGroup))
            {
                ImGui.Text($"IsItemHovered: {(ImGui.IsItemHovered() ? 1 : 0)}");
                for (var i = 0; i < 5; i++)
                    ImGui.Text($"More content {i}");
            }
            /*
            if (ImGui.CollapsingHeader("Header with a bullet", TreeNodeFlags.Bullet))
                ImGui.Text($"IsItemHovered: {(ImGui.IsItemHovered() ? 1 : 0)}");
            */
            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsColorAndPickers()
    //-----------------------------------------------------------------------------

    private static readonly float[] _colorColor = [114.0f / 255.0f, 144.0f / 255.0f, 154.0f / 255.0f, 200.0f / 255.0f];
    private static int _colorBaseFlags = (int)ColorEditFlags.None;
    private static bool _colorSavedPaletteInit = true;
    private static readonly float[][] _colorSavedPalette = new float[32][];
    private static readonly float[] _colorBackupColor = new float[4];
    private static bool _colorNoBorder;
    private static bool _colorRefColor;
    private static readonly float[] _colorRefColorV = [1.0f, 0.0f, 1.0f, 0.5f];
    private static int _colorPickerMode;
    private static int _colorDisplayMode;
    private static int _colorPickerFlags = (int)ColorEditFlags.AlphaBar;
    private static readonly float[] _colorHsv = [0.23f, 1.0f, 1.0f, 1.0f]; // Stored as HSV!

    private static void DemoWindowWidgetsColorAndPickers()
    {
        if (ImGui.TreeNode("Color/Picker Widgets"))
        {
            // DEMO MARKER: Widgets/Color
            ImGui.SeparatorText("Options");
            ImGui.CheckboxFlags("ImGuiColorEditFlags_NoAlpha", ref _colorBaseFlags, (int)ColorEditFlags.NoAlpha);
            ImGui.CheckboxFlags("ImGuiColorEditFlags_AlphaOpaque", ref _colorBaseFlags, (int)ColorEditFlags.AlphaOpaque);
            ImGui.CheckboxFlags("ImGuiColorEditFlags_AlphaNoBg", ref _colorBaseFlags, (int)ColorEditFlags.AlphaNoBg);
            ImGui.CheckboxFlags("ImGuiColorEditFlags_AlphaPreviewHalf", ref _colorBaseFlags, (int)ColorEditFlags.AlphaPreviewHalf);
            ImGui.CheckboxFlags("ImGuiColorEditFlags_NoOptions", ref _colorBaseFlags, (int)ColorEditFlags.NoOptions); ImGui.SameLine(); HelpMarker("Right-click on the individual color widget to show options.");
            ImGui.CheckboxFlags("ImGuiColorEditFlags_NoDragDrop", ref _colorBaseFlags, (int)ColorEditFlags.NoDragDrop);
            ImGui.CheckboxFlags("ImGuiColorEditFlags_NoColorMarkers", ref _colorBaseFlags, (int)ColorEditFlags.NoColorMarkers);
            ImGui.CheckboxFlags("ImGuiColorEditFlags_HDR", ref _colorBaseFlags, (int)ColorEditFlags.HDR); ImGui.SameLine(); HelpMarker("Currently all this does is to lift the 0..1 limits on dragging widgets.");

            var baseFlags = (ColorEditFlags)_colorBaseFlags;

            // DEMO MARKER: Widgets/Color/ColorEdit
            ImGui.SeparatorText("Inline color editor");
            ImGui.Text("Color widget:");
            ImGui.SameLine(); HelpMarker(
                "Click on the color square to open a color picker.\n" +
                "Ctrl+Click on individual component to input value.\n");
            ImGui.ColorEdit3("MyColor##1", ref _colorColor[0], ref _colorColor[1], ref _colorColor[2], baseFlags);

            // DEMO MARKER: Widgets/Color/ColorEdit (HSV, with Alpha)
            ImGui.Text("Color widget HSV with Alpha:");
            ImGui.ColorEdit4("MyColor##2", _colorColor, ColorEditFlags.DisplayHSV | baseFlags);

            // DEMO MARKER: Widgets/Color/ColorEdit (float display)
            ImGui.Text("Color widget with Float Display:");
            ImGui.ColorEdit4("MyColor##2f", _colorColor, ColorEditFlags.Float | baseFlags);

            // DEMO MARKER: Widgets/Color/ColorButton (with Picker)
            ImGui.Text("Color button with Picker:");
            ImGui.SameLine(); HelpMarker(
                "With the ImGuiColorEditFlags_NoInputs flag you can hide all the slider/text inputs.\n" +
                "With the ImGuiColorEditFlags_NoLabel flag you can pass a non-empty label which will only " +
                "be used for the tooltip and picker popup.");
            ImGui.ColorEdit4("MyColor##3", _colorColor, ColorEditFlags.NoInputs | ColorEditFlags.NoLabel | baseFlags);

            // DEMO MARKER: Widgets/Color/ColorButton (with custom Picker popup)
            ImGui.Text("Color button with Custom Picker Popup:");

            // Generate a default palette. The palette will persist and can be edited.
            if (_colorSavedPaletteInit)
            {
                for (var n = 0; n < _colorSavedPalette.Length; n++)
                {
                    var (r, g, b) = ImGui.ColorConvertHSVtoRGB(n / 31.0f, 0.8f, 0.8f);
                    _colorSavedPalette[n] = [r, g, b, 1.0f]; // Alpha
                }
                _colorSavedPaletteInit = false;
            }

            var openPopup = ImGui.ColorButton("MyColor##3b", _colorColor[0], _colorColor[1], _colorColor[2], _colorColor[3], baseFlags);
            ImGui.SameLine(0, Style.ItemInnerSpacing.X);
            openPopup |= ImGui.Button("Palette");
            if (openPopup)
            {
                ImGui.OpenPopup("mypicker");
                Array.Copy(_colorColor, _colorBackupColor, 4);
            }
            if (ImGui.BeginPopup("mypicker"))
            {
                ImGui.Text("MY CUSTOM COLOR PICKER WITH AN AMAZING PALETTE!");
                ImGui.Separator();
                ImGui.ColorPicker4("##picker", _colorColor, baseFlags | ColorEditFlags.NoSidePreview | ColorEditFlags.NoSmallPreview);
                ImGui.SameLine();

                ImGui.BeginGroup(); // Lock X position
                ImGui.Text("Current");
                ImGui.ColorButton("##current", _colorColor[0], _colorColor[1], _colorColor[2], _colorColor[3], ColorEditFlags.NoPicker | ColorEditFlags.AlphaPreviewHalf, 60, 40);
                ImGui.Text("Previous");
                if (ImGui.ColorButton("##previous", _colorBackupColor[0], _colorBackupColor[1], _colorBackupColor[2], _colorBackupColor[3], ColorEditFlags.NoPicker | ColorEditFlags.AlphaPreviewHalf, 60, 40))
                    Array.Copy(_colorBackupColor, _colorColor, 4);
                ImGui.Separator();
                ImGui.Text("Palette");
                for (var n = 0; n < _colorSavedPalette.Length; n++)
                {
                    ImGui.PushID(n);
                    if ((n % 8) != 0)
                        ImGui.SameLine(0.0f, Style.ItemSpacing.Y);

                    var paletteButtonFlags = ColorEditFlags.NoAlpha | ColorEditFlags.NoPicker | ColorEditFlags.NoTooltip;
                    if (ImGui.ColorButton("##palette", _colorSavedPalette[n][0], _colorSavedPalette[n][1], _colorSavedPalette[n][2], _colorSavedPalette[n][3], paletteButtonFlags, 20, 20))
                    {
                        // Preserve alpha!
                        _colorColor[0] = _colorSavedPalette[n][0];
                        _colorColor[1] = _colorSavedPalette[n][1];
                        _colorColor[2] = _colorSavedPalette[n][2];
                    }

                    // Allow user to drop colors into each palette entry. Note that ColorButton() is already a
                    // drag source by default, unless specifying the ImGuiColorEditFlags_NoDragDrop flag.
                    if (ImGui.BeginDragDropTarget())
                    {
                        var payload3 = ImGui.AcceptDragDropPayload("_COL3F"); // IMGUI_PAYLOAD_TYPE_COLOR_3F
                        if (payload3.IsValid)
                            MemoryMarshal.Cast<byte, float>(payload3.Data)[..3].CopyTo(_colorSavedPalette[n]);
                        var payload4 = ImGui.AcceptDragDropPayload("_COL4F"); // IMGUI_PAYLOAD_TYPE_COLOR_4F
                        if (payload4.IsValid)
                            MemoryMarshal.Cast<byte, float>(payload4.Data)[..4].CopyTo(_colorSavedPalette[n]);
                        ImGui.EndDragDropTarget();
                    }

                    ImGui.PopID();
                }
                ImGui.EndGroup();
                ImGui.EndPopup();
            }

            // DEMO MARKER: Widgets/Color/ColorButton (simple)
            ImGui.Text("Color button only:");
            ImGui.Checkbox("ImGuiColorEditFlags_NoBorder", ref _colorNoBorder);
            ImGui.ColorButton("MyColor##3c", _colorColor[0], _colorColor[1], _colorColor[2], _colorColor[3], baseFlags | (_colorNoBorder ? ColorEditFlags.NoBorder : 0), 80, 80);

            // DEMO MARKER: Widgets/Color/ColorPicker
            ImGui.SeparatorText("Color picker");

            ImGui.PushID("Color picker");
            ImGui.CheckboxFlags("ImGuiColorEditFlags_NoAlpha", ref _colorPickerFlags, (int)ColorEditFlags.NoAlpha);
            ImGui.CheckboxFlags("ImGuiColorEditFlags_AlphaBar", ref _colorPickerFlags, (int)ColorEditFlags.AlphaBar);
            ImGui.CheckboxFlags("ImGuiColorEditFlags_NoSidePreview", ref _colorPickerFlags, (int)ColorEditFlags.NoSidePreview);
            if ((_colorPickerFlags & (int)ColorEditFlags.NoSidePreview) != 0)
            {
                ImGui.SameLine();
                ImGui.Checkbox("With Ref Color", ref _colorRefColor);
                if (_colorRefColor)
                {
                    ImGui.SameLine();
                    ImGui.ColorEdit4("##RefColor", _colorRefColorV, ColorEditFlags.NoInputs | baseFlags);
                }
            }

            // (The C++ demo uses the packed "a\0b\0" single-string Combo variant; the wrapper takes an array instead.)
            ImGui.Combo("Picker Mode", ref _colorPickerMode, ["Auto/Current", "ImGuiColorEditFlags_PickerHueBar", "ImGuiColorEditFlags_PickerHueWheel"]);
            ImGui.SameLine(); HelpMarker("When not specified explicitly, user can right-click the picker to change mode.");

            ImGui.Combo("Display Mode", ref _colorDisplayMode, ["Auto/Current", "ImGuiColorEditFlags_NoInputs", "ImGuiColorEditFlags_DisplayRGB", "ImGuiColorEditFlags_DisplayHSV", "ImGuiColorEditFlags_DisplayHex"]);
            ImGui.SameLine(); HelpMarker(
                "ColorEdit defaults to displaying RGB inputs if you don't specify a display mode, " +
                "but the user can change it with a right-click on those inputs.\n\nColorPicker defaults to displaying RGB+HSV+Hex " +
                "if you don't specify a display mode.\n\nYou can change the defaults using SetColorEditOptions().");

            var flags = baseFlags | (ColorEditFlags)_colorPickerFlags;
            if (_colorPickerMode == 1) flags |= ColorEditFlags.PickerHueBar;
            if (_colorPickerMode == 2) flags |= ColorEditFlags.PickerHueWheel;
            if (_colorDisplayMode == 1) flags |= ColorEditFlags.NoInputs;    // Disable all RGB/HSV/Hex displays
            if (_colorDisplayMode == 2) flags |= ColorEditFlags.DisplayRGB;  // Override display mode
            if (_colorDisplayMode == 3) flags |= ColorEditFlags.DisplayHSV;
            if (_colorDisplayMode == 4) flags |= ColorEditFlags.DisplayHex;
            if (_colorRefColor)
                ImGui.ColorPicker4("MyColor##4", _colorColor, _colorRefColorV, flags);
            else
                ImGui.ColorPicker4("MyColor##4", _colorColor, flags);

            ImGui.Text("Set defaults in code:");
            ImGui.SameLine(); HelpMarker(
                "SetColorEditOptions() is designed to allow you to set boot-time default.\n" +
                "We don't have Push/Pop functions because you can force options on a per-widget basis if needed, " +
                "and the user can change non-forced ones with the options menu.\nWe don't have a getter to avoid " +
                "encouraging you to persistently save values that aren't forward-compatible.");
            if (ImGui.Button("Default: Uint8 + HSV + Hue Bar"))
                ImGui.SetColorEditOptions(ColorEditFlags.Uint8 | ColorEditFlags.DisplayHSV | ColorEditFlags.PickerHueBar);
            if (ImGui.Button("Default: Float + HDR + Hue Wheel"))
                ImGui.SetColorEditOptions(ColorEditFlags.Float | ColorEditFlags.HDR | ColorEditFlags.PickerHueWheel);

            // Always display a small version of both types of pickers
            // (that's in order to make it more visible in the demo to people who are skimming quickly through it)
            ImGui.Text("Both types:");
            var w = (ImGui.GetContentRegionAvail().Width - Style.ItemSpacing.Y) * 0.40f;
            ImGui.SetNextItemWidth(w);
            ImGui.ColorPicker3("##MyColor##5", _colorColor, ColorEditFlags.PickerHueBar | ColorEditFlags.NoSidePreview | ColorEditFlags.NoInputs | ColorEditFlags.NoAlpha);
            ImGui.SameLine();
            ImGui.SetNextItemWidth(w);
            ImGui.ColorPicker3("##MyColor##6", _colorColor, ColorEditFlags.PickerHueWheel | ColorEditFlags.NoSidePreview | ColorEditFlags.NoInputs | ColorEditFlags.NoAlpha);
            ImGui.PopID();

            // HSV encoded support (to avoid RGB<>HSV round trips and singularities when S==0 or V==0)
            ImGui.Spacing();
            ImGui.Text("HSV encoded colors");
            ImGui.SameLine(); HelpMarker(
                "By default, colors are given to ColorEdit and ColorPicker in RGB, but ImGuiColorEditFlags_InputHSV " +
                "allows you to store colors as HSV and pass them to ColorEdit and ColorPicker as HSV. This comes with the " +
                "added benefit that you can manipulate hue values with the picker even when saturation or value are zero.");
            ImGui.Text("Color widget with InputHSV:");
            ImGui.ColorEdit4("HSV shown as RGB##1", _colorHsv, ColorEditFlags.DisplayRGB | ColorEditFlags.InputHSV | ColorEditFlags.Float);
            ImGui.ColorEdit4("HSV shown as HSV##1", _colorHsv, ColorEditFlags.DisplayHSV | ColorEditFlags.InputHSV | ColorEditFlags.Float);
            ImGui.DragFloat4("Raw HSV values", _colorHsv, 0.01f, 0.0f, 1.0f);

            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsComboBoxes()
    //-----------------------------------------------------------------------------

    private static int _comboFlags = (int)ComboFlags.None;
    private static int _comboItemSelectedIdx;
    private static readonly TextFilter _comboFilter = new();
    private static int _comboItemCurrent2;
    private static int _comboItemCurrent3 = -1; // If the selection isn't within 0..count, Combo won't display a preview
    private static int _comboItemCurrent4;

    private static void DemoWindowWidgetsComboBoxes()
    {
        if (ImGui.TreeNode("Combo"))
        {
            // DEMO MARKER: Widgets/Combo
            // Combo Boxes are also called "Dropdown" in other systems
            // Expose flags as checkbox for the demo
            const int heightMask = (int)(ComboFlags.HeightSmall | ComboFlags.HeightRegular | ComboFlags.HeightLarge | ComboFlags.HeightLargest);
            ImGui.CheckboxFlags("ImGuiComboFlags_PopupAlignLeft", ref _comboFlags, (int)ComboFlags.PopupAlignLeft);
            ImGui.SameLine(); HelpMarker("Only makes a difference if the popup is larger than the combo");
            if (ImGui.CheckboxFlags("ImGuiComboFlags_NoArrowButton", ref _comboFlags, (int)ComboFlags.NoArrowButton))
                _comboFlags &= ~(int)ComboFlags.NoPreview;     // Clear incompatible flags
            if (ImGui.CheckboxFlags("ImGuiComboFlags_NoPreview", ref _comboFlags, (int)ComboFlags.NoPreview))
                _comboFlags &= ~(int)(ComboFlags.NoArrowButton | ComboFlags.WidthFitPreview); // Clear incompatible flags
            if (ImGui.CheckboxFlags("ImGuiComboFlags_WidthFitPreview", ref _comboFlags, (int)ComboFlags.WidthFitPreview))
                _comboFlags &= ~(int)ComboFlags.NoPreview;

            // Override default popup height
            if (ImGui.CheckboxFlags("ImGuiComboFlags_HeightSmall", ref _comboFlags, (int)ComboFlags.HeightSmall))
                _comboFlags &= ~(heightMask & ~(int)ComboFlags.HeightSmall);
            if (ImGui.CheckboxFlags("ImGuiComboFlags_HeightRegular", ref _comboFlags, (int)ComboFlags.HeightRegular))
                _comboFlags &= ~(heightMask & ~(int)ComboFlags.HeightRegular);
            if (ImGui.CheckboxFlags("ImGuiComboFlags_HeightLargest", ref _comboFlags, (int)ComboFlags.HeightLargest))
                _comboFlags &= ~(heightMask & ~(int)ComboFlags.HeightLargest);

            // Using the generic BeginCombo() API, you have full control over how to display the combo contents.
            // (your selection data could be an index, a pointer to the object, an id for the object, a flag intrusively
            // stored in the object itself, etc.)
            string[] items = ["AAAA", "BBBB", "CCCC", "DDDD", "EEEE", "FFFF", "GGGG", "HHHH", "IIII", "JJJJ", "KKKK", "LLLLLLL", "MMMM", "OOOOOOO"];

            // Pass in the preview value visible before opening the combo (it could technically be different contents or not pulled from items[])
            var comboPreviewValue = items[_comboItemSelectedIdx];
            if (ImGui.BeginCombo("combo 1", comboPreviewValue, (ComboFlags)_comboFlags))
            {
                for (var n = 0; n < items.Length; n++)
                {
                    var isSelected = (_comboItemSelectedIdx == n);
                    if (ImGui.Selectable(items[n], isSelected))
                        _comboItemSelectedIdx = n;

                    // Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
                    if (isSelected)
                        ImGui.SetItemDefaultFocus();
                }
                ImGui.EndCombo();
            }

            // Show case embedding a filter using a simple trick: displaying the filter inside combo contents.
            // See https://github.com/ocornut/imgui/issues/718 for advanced/esoteric alternatives.
            if (ImGui.BeginCombo("combo 2 (w/ filter)", comboPreviewValue, (ComboFlags)_comboFlags))
            {
                if (ImGui.IsWindowAppearing())
                {
                    ImGui.SetKeyboardFocusHere();
                    _comboFilter.Clear();
                }
                ImGui.SetNextItemShortcut(Key.ModCtrl | Key.F);
                _comboFilter.Draw("##Filter", -float.Epsilon);

                for (var n = 0; n < items.Length; n++)
                {
                    var isSelected = (_comboItemSelectedIdx == n);
                    if (_comboFilter.PassFilter(items[n]))
                        if (ImGui.Selectable(items[n], isSelected))
                            _comboItemSelectedIdx = n;
                }
                ImGui.EndCombo();
            }

            ImGui.Spacing();
            ImGui.SeparatorText("One-liner variants");
            HelpMarker("The Combo() function is not greatly useful apart from cases were you want to embed all options in a single strings.\nFlags above don't apply to this section.");

            // Simplified one-liner Combo() API, using values packed in a single constant string
            // This is a convenience for when the selection set is small and known at compile-time.
            // (The wrapper takes a string array instead of the C++ "aaaa\0bbbb\0..." packed string.)
            ImGui.Combo("combo 3 (one-liner)", ref _comboItemCurrent2, ["aaaa", "bbbb", "cccc", "dddd", "eeee"]);

            // Simplified one-liner Combo() using an array of const char*
            // This is not very useful (may obsolete): prefer using BeginCombo()/EndCombo() for full control.
            ImGui.Combo("combo 4 (array)", ref _comboItemCurrent3, items);

            // Simplified one-liner Combo() using an accessor function
            ImGui.Combo("combo 5 (function)", ref _comboItemCurrent4, n => items[n], items.Length);

            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsDataTypes()
    //-----------------------------------------------------------------------------

    // State
    private static sbyte _dataS8V = 127;
    private static byte _dataU8V = 255;
    private static short _dataS16V = 32767;
    private static ushort _dataU16V = 65535;
    private static int _dataS32V = -1;
    private static uint _dataU32V = unchecked((uint)-1);
    private static long _dataS64V = -1;
    private static ulong _dataU64V = unchecked((ulong)-1);
    private static float _dataF32V = 0.123f;
    private static double _dataF64V = 90000.01234567890123456789;
    private static bool _dataDragClamp;
    private static bool _dataInputsStep = true;
    private static int _dataInputFlags = (int)InputTextFlags.None;

    private static void DemoWindowWidgetsDataTypes()
    {
        if (ImGui.TreeNode("Data Types"))
        {
            // DEMO MARKER: Widgets/Data Types
            // The C++ demo uses the DragScalar/InputScalar/SliderScalar functions with an ImGuiDataType
            // enum and void* arguments. The wrapper exposes those as the generic ImGui.Drag<T>/
            // ImGui.Slider<T>/ImGui.Input<T> methods, where T selects the data type.

            // Setup limits (as helper variables so the calls below mirror the C++ demo)
            // Note: SliderScalar() functions have a maximum usable range of half the natural type maximum, hence the /2.
            const sbyte s8Zero = 0, s8One = 1, s8Fifty = 50, s8Min = -128, s8Max = 127;
            const byte u8Zero = 0, u8One = 1, u8Fifty = 50, u8Min = 0, u8Max = 255;
            const short s16Min = -32768, s16Max = 32767;
            const short s16Zero = 0, s16One = 1, s16Fifty = 50;
            const ushort u16Zero = 0, u16One = 1, u16Fifty = 50, u16Min = 0, u16Max = 65535;
            const int s32Zero = 0, s32One = 1, s32Fifty = 50, s32Min = int.MinValue / 2, s32Max = int.MaxValue / 2, s32HiA = int.MaxValue / 2 - 100, s32HiB = int.MaxValue / 2;
            const uint u32Zero = 0, u32One = 1, u32Fifty = 50, u32Min = 0, u32Max = uint.MaxValue / 2, u32HiA = uint.MaxValue / 2 - 100, u32HiB = uint.MaxValue / 2;
            const long s64Zero = 0, s64One = 1, s64Fifty = 50, s64Min = long.MinValue / 2, s64Max = long.MaxValue / 2, s64HiA = long.MaxValue / 2 - 100, s64HiB = long.MaxValue / 2;
            const ulong u64Zero = 0, u64One = 1, u64Fifty = 50, u64Min = 0, u64Max = ulong.MaxValue / 2, u64HiA = ulong.MaxValue / 2 - 100, u64HiB = ulong.MaxValue / 2;
            const float f32Zero = 0f, f32One = 1f, f32LoA = -10000000000.0f, f32HiA = +10000000000.0f;
            const double f64Zero = 0.0, f64One = 1.0, f64LoA = -1000000000000000.0, f64HiA = +1000000000000000.0;

            const float dragSpeed = 0.2f;
            // DEMO MARKER: Widgets/Data Types/Drags
            ImGui.SeparatorText("Drags");
            ImGui.Checkbox("Clamp integers to 0..50", ref _dataDragClamp);
            ImGui.SameLine(); HelpMarker(
                "As with every widget in dear imgui, we never modify values unless there is a user interaction.\n" +
                "You can override the clamping limits by using Ctrl+Click to input a value.");
            // Note: min == max == 0 means "no clamping" for drags, matching the C++ NULL min/max.
            var clamp = _dataDragClamp;
            ImGui.Drag("drag s8", ref _dataS8V, dragSpeed, clamp ? s8Zero : default, clamp ? s8Fifty : default);
            ImGui.Drag("drag u8", ref _dataU8V, dragSpeed, clamp ? u8Zero : default, clamp ? u8Fifty : default, "%u ms");
            ImGui.Drag("drag s16", ref _dataS16V, dragSpeed, clamp ? s16Zero : default, clamp ? s16Fifty : default);
            ImGui.Drag("drag u16", ref _dataU16V, dragSpeed, clamp ? u16Zero : default, clamp ? u16Fifty : default, "%u ms");
            ImGui.Drag("drag s32", ref _dataS32V, dragSpeed, clamp ? s32Zero : default, clamp ? s32Fifty : default);
            ImGui.Drag("drag s32 hex", ref _dataS32V, dragSpeed, clamp ? s32Zero : default, clamp ? s32Fifty : default, "0x%08X");
            ImGui.Drag("drag u32", ref _dataU32V, dragSpeed, clamp ? u32Zero : default, clamp ? u32Fifty : default, "%u ms");
            ImGui.Drag("drag s64", ref _dataS64V, dragSpeed, clamp ? s64Zero : default, clamp ? s64Fifty : default);
            ImGui.Drag("drag u64", ref _dataU64V, dragSpeed, clamp ? u64Zero : default, clamp ? u64Fifty : default);
            ImGui.Drag("drag float", ref _dataF32V, 0.005f, f32Zero, f32One, "%f");
            ImGui.Drag("drag float log", ref _dataF32V, 0.005f, f32Zero, f32One, "%f", SliderFlags.Logarithmic);
            ImGui.Drag("drag double", ref _dataF64V, 0.0005f, f64Zero, default, "%.10f grams");
            ImGui.Drag("drag double log", ref _dataF64V, 0.0005f, f64Zero, f64One, "0 < %.10f < 1", SliderFlags.Logarithmic);

            // DEMO MARKER: Widgets/Data Types/Sliders
            ImGui.SeparatorText("Sliders");
            ImGui.Slider("slider s8 full", ref _dataS8V, s8Min, s8Max, "%d");
            ImGui.Slider("slider u8 full", ref _dataU8V, u8Min, u8Max, "%u");
            ImGui.Slider("slider s16 full", ref _dataS16V, s16Min, s16Max, "%d");
            ImGui.Slider("slider u16 full", ref _dataU16V, u16Min, u16Max, "%u");
            ImGui.Slider("slider s32 low", ref _dataS32V, s32Zero, s32Fifty, "%d");
            ImGui.Slider("slider s32 high", ref _dataS32V, s32HiA, s32HiB, "%d");
            ImGui.Slider("slider s32 full", ref _dataS32V, s32Min, s32Max, "%d");
            ImGui.Slider("slider s32 hex", ref _dataS32V, s32Zero, s32Fifty, "0x%04X");
            ImGui.Slider("slider u32 low", ref _dataU32V, u32Zero, u32Fifty, "%u");
            ImGui.Slider("slider u32 high", ref _dataU32V, u32HiA, u32HiB, "%u");
            ImGui.Slider("slider u32 full", ref _dataU32V, u32Min, u32Max, "%u");
            ImGui.Slider("slider s64 low", ref _dataS64V, s64Zero, s64Fifty, "%lld");
            ImGui.Slider("slider s64 high", ref _dataS64V, s64HiA, s64HiB, "%lld");
            ImGui.Slider("slider s64 full", ref _dataS64V, s64Min, s64Max, "%lld");
            ImGui.Slider("slider u64 low", ref _dataU64V, u64Zero, u64Fifty, "%llu ms");
            ImGui.Slider("slider u64 high", ref _dataU64V, u64HiA, u64HiB, "%llu ms");
            ImGui.Slider("slider u64 full", ref _dataU64V, u64Min, u64Max, "%llu ms");
            ImGui.Slider("slider float low", ref _dataF32V, f32Zero, f32One);
            ImGui.Slider("slider float low log", ref _dataF32V, f32Zero, f32One, "%.10f", SliderFlags.Logarithmic);
            ImGui.Slider("slider float high", ref _dataF32V, f32LoA, f32HiA, "%e");
            ImGui.Slider("slider double low", ref _dataF64V, f64Zero, f64One, "%.10f grams");
            ImGui.Slider("slider double low log", ref _dataF64V, f64Zero, f64One, "%.10f", SliderFlags.Logarithmic);
            ImGui.Slider("slider double high", ref _dataF64V, f64LoA, f64HiA, "%e grams");

            ImGui.SeparatorText("Sliders (reverse)");
            ImGui.Slider("slider s8 reverse", ref _dataS8V, s8Max, s8Min, "%d");
            ImGui.Slider("slider u8 reverse", ref _dataU8V, u8Max, u8Min, "%u");
            ImGui.Slider("slider s32 reverse", ref _dataS32V, s32Fifty, s32Zero, "%d");
            ImGui.Slider("slider u32 reverse", ref _dataU32V, u32Fifty, u32Zero, "%u");
            ImGui.Slider("slider s64 reverse", ref _dataS64V, s64Fifty, s64Zero, "%lld");
            ImGui.Slider("slider u64 reverse", ref _dataU64V, u64Fifty, u64Zero, "%llu ms");

            // DEMO MARKER: Widgets/Data Types/Inputs
            ImGui.SeparatorText("Inputs");
            ImGui.Checkbox("Show step buttons", ref _dataInputsStep);
            ImGui.CheckboxFlags("ImGuiInputTextFlags_ReadOnly", ref _dataInputFlags, (int)InputTextFlags.ReadOnly);
            ImGui.CheckboxFlags("ImGuiInputTextFlags_ParseEmptyRefVal", ref _dataInputFlags, (int)InputTextFlags.ParseEmptyRefVal);
            ImGui.CheckboxFlags("ImGuiInputTextFlags_DisplayEmptyRefVal", ref _dataInputFlags, (int)InputTextFlags.DisplayEmptyRefVal);
            var step = _dataInputsStep;
            var inputFlags = (InputTextFlags)_dataInputFlags;
            ImGui.Input("input s8", ref _dataS8V, step ? s8One : null, null, "%d", inputFlags);
            ImGui.Input("input u8", ref _dataU8V, step ? u8One : null, null, "%u", inputFlags);
            ImGui.Input("input s16", ref _dataS16V, step ? s16One : null, null, "%d", inputFlags);
            ImGui.Input("input u16", ref _dataU16V, step ? u16One : null, null, "%u", inputFlags);
            ImGui.Input("input s32", ref _dataS32V, step ? s32One : null, null, "%d", inputFlags);
            ImGui.Input("input s32 hex", ref _dataS32V, step ? s32One : null, null, "%04X", inputFlags);
            ImGui.Input("input u32", ref _dataU32V, step ? u32One : null, null, "%u", inputFlags);
            ImGui.Input("input u32 hex", ref _dataU32V, step ? u32One : null, null, "%08X", inputFlags);
            ImGui.Input("input s64", ref _dataS64V, step ? s64One : null, null, null, inputFlags);
            ImGui.Input("input u64", ref _dataU64V, step ? u64One : null, null, null, inputFlags);
            ImGui.Input("input float", ref _dataF32V, step ? f32One : null, null, null, inputFlags);
            ImGui.Input("input double", ref _dataF64V, step ? f64One : null, null, null, inputFlags);

            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsDisableBlocks()
    //-----------------------------------------------------------------------------

    private static void DemoWindowWidgetsDisableBlocks(DemoWindowData data)
    {
        if (ImGui.TreeNode("Disable Blocks"))
        {
            // DEMO MARKER: Widgets/Disable Blocks
            ImGui.Checkbox("Disable entire section above", ref data.DisableSections);
            ImGui.SameLine(); HelpMarker("Demonstrate using BeginDisabled()/EndDisabled() across other sections.");
            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsDragAndDrop()
    //-----------------------------------------------------------------------------

    private const int DndModeCopy = 0;
    private const int DndModeMove = 1;
    private const int DndModeSwap = 2;
    private static int _dndMode;
    private static readonly string[] _dndNames =
    [
        "Bobby", "Beatrice", "Betty",
        "Brianna", "Barry", "Bernard",
        "Bibi", "Blaine", "Bryn"
    ];
    private static readonly string[] _dndItemNames = ["Item One", "Item Two", "Item Three", "Item Four", "Item Five"];
    private static readonly float[] _dndCol1 = [1.0f, 0.0f, 0.2f];
    private static readonly float[] _dndCol2 = [0.4f, 0.7f, 0.0f, 0.5f];
    private static readonly float[] _dndCol4 = [1.0f, 0.0f, 0.2f, 1.0f];

    private static void DemoWindowWidgetsDragAndDrop()
    {
        if (ImGui.TreeNode("Drag and Drop"))
        {
            // DEMO MARKER: Widgets/Drag and drop
            if (ImGui.TreeNode("Drag and drop in standard widgets"))
            {
                // DEMO MARKER: Widgets/Drag and drop/Standard widgets
                // ColorEdit widgets automatically act as drag source and drag target.
                // They are using standardized payload strings IMGUI_PAYLOAD_TYPE_COLOR_3F and IMGUI_PAYLOAD_TYPE_COLOR_4F
                // to allow your own widgets to use colors in their drag and drop interaction.
                // Also see 'Demo->Widgets->Color/Picker Widgets->Palette' demo.
                HelpMarker("You can drag from the color squares.");
                ImGui.ColorEdit3("color 1", ref _dndCol1[0], ref _dndCol1[1], ref _dndCol1[2]);
                ImGui.ColorEdit4("color 2", _dndCol2);
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Drag and drop to copy/swap items"))
            {
                // DEMO MARKER: Widgets/Drag and drop/Copy-swap items
                if (ImGui.RadioButton("Copy", _dndMode == DndModeCopy)) { _dndMode = DndModeCopy; } ImGui.SameLine();
                if (ImGui.RadioButton("Move", _dndMode == DndModeMove)) { _dndMode = DndModeMove; } ImGui.SameLine();
                if (ImGui.RadioButton("Swap", _dndMode == DndModeSwap)) { _dndMode = DndModeSwap; }
                for (var n = 0; n < _dndNames.Length; n++)
                {
                    ImGui.PushID(n);
                    if ((n % 3) != 0)
                        ImGui.SameLine();
                    ImGui.Button(_dndNames[n], 60, 60);

                    // Our buttons are both drag sources and drag targets here!
                    if (ImGui.BeginDragDropSource(DragDropFlags.None))
                    {
                        // Set payload to carry the index of our item (could be anything)
                        ImGui.SetDragDropPayload("DND_DEMO_CELL", n);

                        // Display preview (could be anything, e.g. when dragging an image we could decide to display
                        // the filename and a small preview of the image, etc.)
                        if (_dndMode == DndModeCopy) { ImGui.Text($"Copy {_dndNames[n]}"); }
                        if (_dndMode == DndModeMove) { ImGui.Text($"Move {_dndNames[n]}"); }
                        if (_dndMode == DndModeSwap) { ImGui.Text($"Swap {_dndNames[n]}"); }
                        ImGui.EndDragDropSource();
                    }
                    if (ImGui.BeginDragDropTarget())
                    {
                        if (ImGui.AcceptDragDropPayload("DND_DEMO_CELL", out int payloadN))
                        {
                            if (_dndMode == DndModeCopy)
                            {
                                _dndNames[n] = _dndNames[payloadN];
                            }
                            if (_dndMode == DndModeMove)
                            {
                                _dndNames[n] = _dndNames[payloadN];
                                _dndNames[payloadN] = "";
                            }
                            if (_dndMode == DndModeSwap)
                            {
                                (_dndNames[n], _dndNames[payloadN]) = (_dndNames[payloadN], _dndNames[n]);
                            }
                        }
                        ImGui.EndDragDropTarget();
                    }
                    ImGui.PopID();
                }
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Drag to reorder items (simple)"))
            {
                // DEMO MARKER: Widgets/Drag and Drop/Drag to reorder items (simple)
                // FIXME: there is temporary (usually single-frame) ID Conflict during reordering as a same item may be submitting twice.
                // This code was always slightly faulty but in a way which was not easily noticeable.
                // Until we fix this, enable ImGuiItemFlags_AllowDuplicateId to disable detecting the issue.
                ImGui.PushItemFlag(ItemFlags.AllowDuplicateId, true);

                // Simple reordering
                HelpMarker(
                    "We don't use the drag and drop api at all here! " +
                    "Instead we query when the item is held but not hovered, and order items accordingly.");
                for (var n = 0; n < _dndItemNames.Length; n++)
                {
                    var item = _dndItemNames[n];
                    ImGui.Selectable(item);

                    if (ImGui.IsItemActive() && !ImGui.IsItemHovered())
                    {
                        var nNext = n + (ImGui.GetMouseDragDelta(MouseButton.Left).Y < 0f ? -1 : 1);
                        if (nNext >= 0 && nNext < _dndItemNames.Length)
                        {
                            _dndItemNames[n] = _dndItemNames[nNext];
                            _dndItemNames[nNext] = item;
                            ImGui.ResetMouseDragDelta();
                        }
                    }
                }

                ImGui.PopItemFlag();
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Tooltip at target location"))
            {
                // DEMO MARKER: Widgets/Drag and Drop/Tooltip at target location
                for (var n = 0; n < 2; n++)
                {
                    // Drop targets
                    ImGui.Button(n != 0 ? "drop here##1" : "drop here##0");
                    if (ImGui.BeginDragDropTarget())
                    {
                        var dropTargetFlags = DragDropFlags.AcceptBeforeDelivery | DragDropFlags.AcceptNoPreviewTooltip;
                        var payload = ImGui.AcceptDragDropPayload("_COL4F", dropTargetFlags); // IMGUI_PAYLOAD_TYPE_COLOR_4F
                        if (payload.IsValid)
                        {
                            ImGui.SetMouseCursor(MouseCursor.NotAllowed);
                            ImGui.SetTooltip("Cannot drop here!");
                        }
                        ImGui.EndDragDropTarget();
                    }

                    // Drop source
                    if (n == 0)
                        ImGui.ColorButton("drag me", _dndCol4[0], _dndCol4[1], _dndCol4[2], _dndCol4[3]);
                }
                ImGui.TreePop();
            }

            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsDragsAndSliders()
    //-----------------------------------------------------------------------------

    private static int _dsFlags = (int)SliderFlags.None;
    private static float _dsDragF = 0.5f;
    private static readonly float[] _dsDragF4 = new float[4];
    private static int _dsDragI = 50;
    private static float _dsSliderF = 0.5f;
    private static readonly float[] _dsSliderF4 = new float[4];
    private static int _dsSliderI = 50;

    private static void DemoWindowWidgetsDragsAndSliders()
    {
        if (ImGui.TreeNode("Drag/Slider Flags"))
        {
            // DEMO MARKER: Widgets/Drag and Slider Flags
            // Demonstrate using advanced flags for DragXXX and SliderXXX functions. Note that the flags are the same!
            ImGui.CheckboxFlags("ImGuiSliderFlags_AlwaysClamp", ref _dsFlags, (int)SliderFlags.AlwaysClamp);
            ImGui.CheckboxFlags("ImGuiSliderFlags_ClampOnInput", ref _dsFlags, (int)SliderFlags.ClampOnInput);
            ImGui.SameLine(); HelpMarker("Clamp value to min/max bounds when input manually with Ctrl+Click. By default Ctrl+Click allows going out of bounds.");
            ImGui.CheckboxFlags("ImGuiSliderFlags_ClampZeroRange", ref _dsFlags, (int)SliderFlags.ClampZeroRange);
            ImGui.SameLine(); HelpMarker("Clamp even if min==max==0.0f. Otherwise DragXXX functions don't clamp.");
            ImGui.CheckboxFlags("ImGuiSliderFlags_Logarithmic", ref _dsFlags, (int)SliderFlags.Logarithmic);
            ImGui.SameLine(); HelpMarker("Enable logarithmic editing (more precision for small values).");
            ImGui.CheckboxFlags("ImGuiSliderFlags_NoRoundToFormat", ref _dsFlags, (int)SliderFlags.NoRoundToFormat);
            ImGui.SameLine(); HelpMarker("Disable rounding underlying value to match precision of the format string (e.g. %.3f values are rounded to those 3 digits).");
            ImGui.CheckboxFlags("ImGuiSliderFlags_NoInput", ref _dsFlags, (int)SliderFlags.NoInput);
            ImGui.SameLine(); HelpMarker("Disable Ctrl+Click or Enter key allowing to input text directly into the widget.");
            ImGui.CheckboxFlags("ImGuiSliderFlags_NoSpeedTweaks", ref _dsFlags, (int)SliderFlags.NoSpeedTweaks);
            ImGui.SameLine(); HelpMarker("Disable keyboard modifiers altering tweak speed. Useful if you want to alter tweak speed yourself based on your own logic.");
            ImGui.CheckboxFlags("ImGuiSliderFlags_WrapAround", ref _dsFlags, (int)SliderFlags.WrapAround);
            ImGui.SameLine(); HelpMarker("Enable wrapping around from max to min and from min to max (only supported by DragXXX() functions)");
            ImGui.CheckboxFlags("ImGuiSliderFlags_ColorMarkers", ref _dsFlags, (int)SliderFlags.ColorMarkers);

            var flags = (SliderFlags)_dsFlags;

            // Drags
            ImGui.Text($"Underlying float value: {_dsDragF:F6}");
            ImGui.DragFloat("DragFloat (0 -> 1)", ref _dsDragF, 0.005f, 0.0f, 1.0f, "%.3f", flags);
            ImGui.DragFloat("DragFloat (0 -> +inf)", ref _dsDragF, 0.005f, 0.0f, float.MaxValue, "%.3f", flags);
            ImGui.DragFloat("DragFloat (-inf -> 1)", ref _dsDragF, 0.005f, -float.MaxValue, 1.0f, "%.3f", flags);
            ImGui.DragFloat("DragFloat (-inf -> +inf)", ref _dsDragF, 0.005f, -float.MaxValue, +float.MaxValue, "%.3f", flags);
            //ImGui.DragFloat("DragFloat (0 -> 0)", ref _dsDragF, 0.005f, 0.0f, 0.0f, "%.3f", flags);           // To test ClampZeroRange
            //ImGui.DragFloat("DragFloat (100 -> 100)", ref _dsDragF, 0.005f, 100.0f, 100.0f, "%.3f", flags);
            ImGui.DragInt("DragInt (0 -> 100)", ref _dsDragI, 0.5f, 0, 100, "%d", flags);
            ImGui.DragFloat4("DragFloat4 (0 -> 1)", _dsDragF4, 0.005f, 0.0f, 1.0f, "%.3f", flags); // Multi-component item, mostly here to document the effect of ImGuiSliderFlags_ColorMarkers.

            // Sliders
            var flagsForSliders = flags & ~SliderFlags.WrapAround;
            ImGui.Text($"Underlying float value: {_dsSliderF:F6}");
            ImGui.SliderFloat("SliderFloat (0 -> 1)", ref _dsSliderF, 0.0f, 1.0f, "%.3f", flagsForSliders);
            ImGui.SliderInt("SliderInt (0 -> 100)", ref _dsSliderI, 0, 100, "%d", flagsForSliders);
            ImGui.SliderFloat4("SliderFloat4 (0 -> 1)", _dsSliderF4, 0.0f, 1.0f, "%.3f", flags); // Multi-component item, mostly here to document the effect of ImGuiSliderFlags_ColorMarkers.

            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsFonts()
    //-----------------------------------------------------------------------------

    private static bool _fontsShowPreview = true;

    private static void DemoWindowWidgetsFonts()
    {
        if (ImGui.TreeNode("Fonts"))
        {
            // DEMO MARKER: Widgets/Fonts
            var atlas = ImGui.GetFontAtlas();

            var backendFlags = (int)Io.BackendFlags;
            ImGui.BeginDisabled();
            ImGui.CheckboxFlags("io.BackendFlags: RendererHasTextures", ref backendFlags,
                (int)BackendFlags.RendererHasTextures);
            ImGui.EndDisabled();
            ImGui.ShowFontSelector("Font");

            var fontSizeBase = Style.FontSizeBase;
            if (ImGui.DragFloat("FontSizeBase", ref fontSizeBase, 0.2f, 5.0f, 100.0f, "%.0f"))
                Style.FontSizeBase = fontSizeBase;
            ImGui.SameLine(0.0f, 0.0f);
            ImGui.Text($" (out {ImGui.GetFontSize():F2})");
            ImGui.SameLine();
            HelpMarker("This scales fonts only. General scaling will come later.");

            var fontScaleMain = Style.FontScaleMain;
            if (ImGui.DragFloat("FontScaleMain", ref fontScaleMain, 0.02f, 0.5f, 4.0f))
                Style.FontScaleMain = fontScaleMain;
            var fontScaleDpi = Style.FontScaleDpi;
            if (ImGui.DragFloat("FontScaleDpi", ref fontScaleDpi, 0.02f, 0.5f, 4.0f))
                ImGui.SetFontScaleDpi(fontScaleDpi);

            if (!atlas.RendererHasTextures)
            {
                ImGui.BulletText("Warning: Font scaling will NOT be smooth, because\nRendererHasTextures is not set!");
                ImGui.BulletText("For instructions, see:");
                ImGui.SameLine();
                ImGui.TextLinkOpenURL("docs/BACKENDS.md", "https://github.com/ocornut/imgui/blob/master/docs/BACKENDS.md");
            }
            ImGui.BulletText("Load a nice font for better results!");
            ImGui.BulletText("Please submit feedback:");
            ImGui.SameLine();
            ImGui.TextLinkOpenURL("#8465", "https://github.com/ocornut/imgui/issues/8465");
            ImGui.BulletText("Read FAQ for more details:");
            ImGui.SameLine();
            ImGui.TextLinkOpenURL("dearimgui.com/faq", "https://www.dearimgui.com/faq/");

            ImGui.SeparatorText("Font List");
            ImGui.Checkbox("Show font preview", ref _fontsShowPreview);
            if (ImGui.TreeNode("Loader", $"Loader: '{atlas.FontLoaderName ?? "NULL"}'"))
            {
                ImGui.BulletText($"Flags: 0x{atlas.FontLoaderFlags:X8}");
                ImGui.BulletText($"Dynamic texture updates: {(atlas.RendererHasTextures ? "supported" : "unavailable")}");
                ImGui.TreePop();
            }

            for (var i = 0; i < atlas.FontCount; i++)
            {
                var font = atlas.GetFont(i);
                ImGui.PushID(i);
                if (ImGui.TreeNode("Font", $"Font \"{font.DebugName}\""))
                {
                    if (_fontsShowPreview)
                    {
                        ImGui.PushFont(font);
                        ImGui.Text("The quick brown fox jumps over the lazy dog");
                        ImGui.PopFont();
                    }
                    ImGui.BulletText($"Loaded: {(font.IsLoaded ? 1 : 0)}");
                    ImGui.BulletText($"Fallback character: '{font.FallbackChar}' (U+{(int)font.FallbackChar:X4})");
                    ImGui.BulletText($"Ellipsis character: '{font.EllipsisChar}' (U+{(int)font.EllipsisChar:X4})");
                    var baked = font.GetFontBaked(ImGui.GetFontSize());
                    if (baked.IsValid)
                        ImGui.BulletText($"Baked at {baked.Size:F1}px: Ascent {baked.Ascent:F1}, Descent {baked.Descent:F1}, {baked.GlyphCount} glyphs");
                    ImGui.TreePop();
                }
                ImGui.PopID();
            }

            ImGui.SeparatorText("Font Atlas");
            if (ImGui.Button("Compact"))
                atlas.CompactCache();
            ImGui.SetItemTooltip("Discard unused baked glyphs and sizes.");

            var texData = atlas.GetTexData();
            if (texData.IsValid)
            {
                ImGui.Text($"Texture: {texData.Width}x{texData.Height} pixels");
                if (ImGui.TreeNode("Texture", $"Texture #{texData.UniqueId:D3} ({texData.Width}x{texData.Height} pixels)"))
                {
                    ImGui.Text($"Status = {texData.Status}, Format = {texData.Format}, UseColors = {(texData.UseColors ? 1 : 0)}");
                    ImGui.Text($"Texture ID = 0x{texData.TextureId:X}, RefCount = {texData.RefCount}, UnusedFrames = {texData.UnusedFrames}");
                    if (texData.TextureId != 0 && texData.Width > 0 && texData.Height > 0)
                        ImGui.Image(texData.TextureId, texData.Width, texData.Height);
                    ImGui.TreePop();
                }
            }
            // FIXME-NEWATLAS: Provide a demo to add/create a procedural font?
            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsImages()
    //-----------------------------------------------------------------------------

    private static int _imagesPressedCount;

    private static void DemoWindowWidgetsImages()
    {
        if (ImGui.TreeNode("Images"))
        {
            // DEMO MARKER: Widgets/Images
            ImGui.TextWrapped(
                "Below we are displaying the font texture (which is the only texture we have access to in this demo). " +
                "Use the 'ImTextureID' type as storage to pass pointers or identifier to your own texture data. " +
                "Hover the texture for a zoomed view!");

            // Below we are displaying the font texture because it is the only texture we have access to inside the demo!
            // Read description about ImTextureID/ImTextureRef and FAQ for details about texture identifiers.
            // If you use one of the default imgui_impl_XXXX.cpp rendering backend, they all have comments at the top
            // of their respective source file to specify what they are using as texture identifier, for example:
            // - The imgui_impl_dx11.cpp renderer expect a 'ID3D11ShaderResourceView*' pointer.
            // - The imgui_impl_opengl3.cpp renderer expect a GLuint OpenGL texture identifier, etc.
            // (The SDL_GPU backend used by SdlSharp expects an SDL_GPUTexture* cast to ulong.)
            // - You can use ShowMetricsWindow() to inspect the draw data that are being passed to your renderer,
            //   it will help you debug issues if you are confused about it.
            // - Consider using the lower-level ImDrawList::AddImage() API, via ImGui.GetWindowDrawList().AddImage().
            // - Read https://github.com/ocornut/imgui/blob/master/docs/FAQ.md
            // - Read https://github.com/ocornut/imgui/wiki/Image-Loading-and-Displaying-Examples

            // Grab the current texture identifier used by the font atlas.
            var atlas = ImGui.GetFontAtlas();
            var myTexId = atlas.TextureId;

            // Regular user code should never have to care about TexData fields, but since we want to display the entire texture here, we pull Width/Height from it.
            var texData = atlas.GetTexData();
            var myTexW = (float)texData.Width;
            var myTexH = (float)texData.Height;

            {
                ImGui.Text($"{myTexW:F0}x{myTexH:F0}");
                var pos = ImGui.GetCursorScreenPos();
                var uvMin = new Vec2(0.0f, 0.0f); // Top-left
                var uvMax = new Vec2(1.0f, 1.0f); // Lower-right
                ImGui.PushStyleVar(StyleVar.ImageBorderSize, MathF.Max(1.0f, Style.ImageBorderSize));
                // ImageWithBg: the wrapper's border color parameters map to the background fill.
                ImGui.Image(myTexId, myTexW, myTexH, uvMin, uvMax, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f);
                if (ImGui.BeginItemTooltip())
                {
                    var regionSz = 32.0f;
                    var regionX = Io.MousePos.X - pos.X - regionSz * 0.5f;
                    var regionY = Io.MousePos.Y - pos.Y - regionSz * 0.5f;
                    var zoom = 4.0f;
                    if (regionX < 0.0f) { regionX = 0.0f; }
                    else if (regionX > myTexW - regionSz) { regionX = myTexW - regionSz; }
                    if (regionY < 0.0f) { regionY = 0.0f; }
                    else if (regionY > myTexH - regionSz) { regionY = myTexH - regionSz; }
                    ImGui.Text($"Min: ({regionX:F2}, {regionY:F2})");
                    ImGui.Text($"Max: ({regionX + regionSz:F2}, {regionY + regionSz:F2})");
                    var uv0 = new Vec2(regionX / myTexW, regionY / myTexH);
                    var uv1 = new Vec2((regionX + regionSz) / myTexW, (regionY + regionSz) / myTexH);
                    ImGui.Image(myTexId, regionSz * zoom, regionSz * zoom, uv0, uv1, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f);
                    ImGui.EndTooltip();
                }
                ImGui.PopStyleVar();
            }

            // DEMO MARKER: Widgets/Images/Textured buttons
            ImGui.TextWrapped("And now some textured buttons..");
            for (var i = 0; i < 8; i++)
            {
                // UV coordinates are often (0.0f, 0.0f) and (1.0f, 1.0f) to display an entire textures.
                // Here are trying to display only a 32x32 pixels area of the texture, hence the UV computation.
                // Read about UV coordinates here: https://github.com/ocornut/imgui/wiki/Image-Loading-and-Displaying-Examples
                ImGui.PushID(i);
                if (i > 0)
                    ImGui.PushStyleVar(StyleVar.FramePadding, i - 1.0f, i - 1.0f);
                var uv0 = new Vec2(0.0f, 0.0f);                             // UV coordinates for lower-left
                var uv1 = new Vec2(32.0f / myTexW, 32.0f / myTexH);         // UV coordinates for (32,32) in our texture
                // Size of the image we want to make visible: 32x32. Black background, no tint.
                if (ImGui.ImageButton("", myTexId, 32.0f, 32.0f, uv0, uv1, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f))
                    _imagesPressedCount += 1;
                if (i > 0)
                    ImGui.PopStyleVar();
                ImGui.PopID();
                ImGui.SameLine();
            }
            ImGui.NewLine();
            ImGui.Text($"Pressed {_imagesPressedCount} times.");
            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsListBoxes()
    //-----------------------------------------------------------------------------

    private static int _listItemSelectedIdx;
    private static bool _listItemHighlight;

    private static void DemoWindowWidgetsListBoxes()
    {
        if (ImGui.TreeNode("List Boxes"))
        {
            // DEMO MARKER: Widgets/List Boxes
            // BeginListBox() is essentially a thin wrapper to using BeginChild()/EndChild()
            // using the ImGuiChildFlags_FrameStyle flag for stylistic changes + displaying a label.
            // You may be tempted to simply use BeginChild() directly. However note that BeginChild() requires EndChild()
            // to always be called (inconsistent with BeginListBox()/EndListBox()).

            // Using the generic BeginListBox() API, you have full control over how to display the combo contents.
            // (your selection data could be an index, a pointer to the object, an id for the object, a flag intrusively
            // stored in the object itself, etc.)
            string[] items = ["AAAA", "BBBB", "CCCC", "DDDD", "EEEE", "FFFF", "GGGG", "HHHH", "IIII", "JJJJ", "KKKK", "LLLLLLL", "MMMM", "OOOOOOO"];

            var itemHighlightedIdx = -1; // Here we store our highlighted data as an index.
            ImGui.Checkbox("Highlight hovered item in second listbox", ref _listItemHighlight);

            if (ImGui.BeginListBox("listbox 1"))
            {
                for (var n = 0; n < items.Length; n++)
                {
                    var isSelected = (_listItemSelectedIdx == n);
                    if (ImGui.Selectable(items[n], isSelected))
                        _listItemSelectedIdx = n;

                    if (_listItemHighlight && ImGui.IsItemHovered())
                        itemHighlightedIdx = n;

                    // Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
                    if (isSelected)
                        ImGui.SetItemDefaultFocus();
                }
                ImGui.EndListBox();
            }
            ImGui.SameLine(); HelpMarker("Here we are sharing selection state between both boxes.");

            // Custom size: use all width, 5 items tall
            ImGui.Text("Full-width:");
            if (ImGui.BeginListBox("##listbox 2", -float.Epsilon, 5 * ImGui.GetTextLineHeightWithSpacing()))
            {
                for (var n = 0; n < items.Length; n++)
                {
                    var isSelected = (_listItemSelectedIdx == n);
                    var flags = (itemHighlightedIdx == n) ? SelectableFlags.Highlight : SelectableFlags.None;
                    if (ImGui.Selectable(items[n], isSelected, flags))
                        _listItemSelectedIdx = n;

                    // Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
                    if (isSelected)
                        ImGui.SetItemDefaultFocus();
                }
                ImGui.EndListBox();
            }

            ImGui.TreePop();
        }
    }

    //-----------------------------------------------------------------------------
    // [SECTION] DemoWindowWidgetsMultiComponents()
    //-----------------------------------------------------------------------------

    private static readonly float[] _multiVec4f = [0.10f, 0.20f, 0.30f, 0.44f];
    private static readonly int[] _multiVec4i = [1, 5, 100, 255];
    private static int _multiFlags = (int)SliderFlags.None;
    private static float _multiBegin = 10;
    private static float _multiEnd = 90;
    private static int _multiBeginI = 100;
    private static int _multiEndI = 1000;

    private static void DemoWindowWidgetsMultiComponents()
    {
        if (ImGui.TreeNode("Multi-component Widgets"))
        {
            // DEMO MARKER: Widgets/Multi-component Widgets
            ImGui.CheckboxFlags("ImGuiSliderFlags_ColorMarkers", ref _multiFlags, (int)SliderFlags.ColorMarkers); // Only passing this to Drag/Sliders
            var flags = (SliderFlags)_multiFlags;

            ImGui.SeparatorText("2-wide");
            ImGui.InputFloat2("input float2", _multiVec4f);
            ImGui.InputInt2("input int2", _multiVec4i);
            ImGui.DragFloat2("drag float2", _multiVec4f, 0.01f, 0.0f, 1.0f, null, flags);
            ImGui.DragInt2("drag int2", _multiVec4i, 1, 0, 255, null, flags);
            ImGui.SliderFloat2("slider float2", _multiVec4f, 0.0f, 1.0f, null, flags);
            ImGui.SliderInt2("slider int2", _multiVec4i, 0, 255, null, flags);

            ImGui.SeparatorText("3-wide");
            ImGui.InputFloat3("input float3", _multiVec4f);
            ImGui.InputInt3("input int3", _multiVec4i);
            ImGui.DragFloat3("drag float3", _multiVec4f, 0.01f, 0.0f, 1.0f, null, flags);
            ImGui.DragInt3("drag int3", _multiVec4i, 1, 0, 255, null, flags);
            ImGui.SliderFloat3("slider float3", _multiVec4f, 0.0f, 1.0f, null, flags);
            ImGui.SliderInt3("slider int3", _multiVec4i, 0, 255, null, flags);

            ImGui.SeparatorText("4-wide");
            ImGui.InputFloat4("input float4", _multiVec4f);
            ImGui.InputInt4("input int4", _multiVec4i);
            ImGui.DragFloat4("drag float4", _multiVec4f, 0.01f, 0.0f, 1.0f, null, flags);
            ImGui.DragInt4("drag int4", _multiVec4i, 1, 0, 255, null, flags);
            ImGui.SliderFloat4("slider float4", _multiVec4f, 0.0f, 1.0f, null, flags);
            ImGui.SliderInt4("slider int4", _multiVec4i, 0, 255, null, flags);

            ImGui.SeparatorText("Ranges");
            ImGui.DragFloatRange2("range float", ref _multiBegin, ref _multiEnd, 0.25f, 0.0f, 100.0f, "Min: %.1f %%", "Max: %.1f %%", SliderFlags.AlwaysClamp);
            ImGui.DragIntRange2("range int", ref _multiBeginI, ref _multiEndI, 5, 0, 1000, "Min: %d units", "Max: %d units");
            ImGui.DragIntRange2("range int (no bounds)", ref _multiBeginI, ref _multiEndI, 5, 0, 0, "Min: %d units", "Max: %d units");

            ImGui.TreePop();
        }
    }
}
