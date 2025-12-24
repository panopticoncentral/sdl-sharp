using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public unsafe readonly ref struct MainMenuBarScope: IDisposable
{
    private readonly bool _isOpen;

    private MainMenuBarScope(bool isOpen)
    {
        _isOpen = isOpen;
    }

    /// <summary>
    /// Creates and appends to a full-screen menu bar.
    /// </summary>
    /// <returns>True if the main menu bar is visible. Only call <see cref="EndMainMenuBar"/> if this returns true.</returns>
    public static MainMenuBarScope Begin()
    {
        return new MainMenuBarScope(ImGui_BeginMainMenuBar());
    }

    public void Dispose()
    {
        if (_isOpen)
        {
            ImGui_EndMainMenuBar();
        }
    }
}
