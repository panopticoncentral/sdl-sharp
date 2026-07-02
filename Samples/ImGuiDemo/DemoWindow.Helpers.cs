// C# port of imgui_demo.cpp — [SECTION] Helpers.
//
// Small shared helpers used across all demo sections.
//
// Upstream reference: imgui/imgui_demo.cpp

using SdlSharp.ImGui;

namespace ImGuiDemo;

internal static unsafe partial class DemoWindow
{
    //-----------------------------------------------------------------------------
    // [SECTION] Helpers
    //-----------------------------------------------------------------------------

    // Helper to display a little (?) mark which shows a tooltip when hovered.
    // In your own code you may want to display an actual icon if you are using a merged icon fonts (see docs/FONTS.md)
    private static void HelpMarker(string desc)
    {
        ImGui.TextDisabled("(?)");
        if (ImGui.BeginItemTooltip())
        {
            ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);
            ImGui.TextUnformatted(desc);
            ImGui.PopTextWrapPos();
            ImGui.EndTooltip();
        }
    }
}
