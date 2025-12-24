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
    }
}
