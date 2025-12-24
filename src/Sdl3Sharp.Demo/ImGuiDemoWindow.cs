using Sdl3Sharp.ImGui;

namespace Sdl3Sharp.Demo;

public static class ImGuiDemoWindow
{
    public static void ShowDemoWindow(StateRef<bool> open)
    {
        using var window = new Window("Dear ImGui Demo (managed)"u8, open);
        if (!window.IsVisible)
        {
            return;
        }
    }
}
