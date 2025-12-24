using Sdl3Sharp.ImGui;
using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.Demo;

public static class ImGuiDemoWindow
{
    public static void ShowDemoWindow(StateRef<bool> open)
    {
        // We specify a default position/size in case there's no data in the .ini file.
        // We only do it to make the demo applications a little more welcoming, but typically this isn't required.
        Viewport mainViewport = Context.MainViewport;
        Window.SetNextWindowPos(new Point(mainViewport.WorkPosition.X + 50, mainViewport.WorkPosition.Y + 20), Condition.FirstUseEver);
        Window.SetNextWindowSize(new Size(550, 680), Condition.FirstUseEver);

        using var window = new Window("Dear ImGui Demo (managed)"u8, open);
        if (!window.IsVisible)
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
        using var itemWidthScope = ItemWidthScope.Push(-labelWidth); // Right-align: framed items will leave 'label_width' available for the label.

    }
}
