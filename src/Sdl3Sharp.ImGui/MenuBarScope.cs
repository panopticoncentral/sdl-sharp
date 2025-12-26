using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public unsafe readonly ref struct MenuBarScope: IDisposable
{
    public readonly bool IsOpen;

    private MenuBarScope(bool isOpen)
    {
        IsOpen = isOpen;
    }

    /// <summary>
    /// Begins appending to a menu bar of the current window.
    /// </summary>
    /// <returns>True if the menu bar is visible. Only call <see cref="EndMenuBar"/> if this returns true.</returns>
    /// <remarks>
    /// Requires the parent window to have the MenuBar window flag set.
    /// </remarks>
    public static MenuBarScope Begin()
    {
        return new MenuBarScope(ImGui_BeginMenuBar());
    }

    public static void Begin(Action a)
    {
        var isOpen = ImGui_BeginMenuBar();
        if (isOpen)
        {
            a();
            ImGui_EndMenuBar();
        }
    }

    public void Dispose()
    {
        if (IsOpen)
        {
            ImGui_EndMenuBar();
        }
    }
}
