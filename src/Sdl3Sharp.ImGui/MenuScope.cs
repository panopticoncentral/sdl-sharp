using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public unsafe readonly ref struct MenuScope: IDisposable
{
    private readonly bool _isOpen;

    private MenuScope(bool isOpen)
    {
        _isOpen = isOpen;
    }

    /// <summary>
    /// Creates a sub-menu entry with explicit enabled state.
    /// </summary>
    /// <param name="label">The menu label.</param>
    /// <param name="enabled">Whether the menu is enabled.</param>
    /// <returns>True if the menu is open. Only call <see cref="EndMenu"/> if this returns true.</returns>
    public static MenuScope Begin(ReadOnlySpan<byte> label, bool enabled = true)
    {
        fixed (byte* ptr = label)
        {
            return new MenuScope(ImGui_BeginMenuEx(ptr, enabled));
        }
    }

    public void Dispose()
    {
        if (_isOpen)
        {
            ImGui_EndMenu();
        }
    }
}
