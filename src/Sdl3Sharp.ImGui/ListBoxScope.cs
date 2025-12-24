using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public unsafe readonly ref struct ListBoxScope: IDisposable
{
    private readonly bool _isOpen;

    private ListBoxScope(bool isOpen)
    {
        _isOpen = isOpen;
    }

    /// <summary>
    /// Begins a list box. Must be followed by <see cref="EndListBox"/> if this returns true.
    /// </summary>
    /// <param name="label">The label for the list box.</param>
    /// <param name="size">The size of the list box.</param>
    /// <returns>True if the list box is open and items should be rendered.</returns>
    public static ListBoxScope BeginListBox(ReadOnlySpan<byte> label, Vec2 size = default)
    {
        fixed (byte* ptr = label)
        {
            return new(ImGui_BeginListBox(ptr, size.Value));
        }
    }

    public void Dispose()
    {
        if (_isOpen)
        {
            ImGui_EndListBox();
        }
    }
}
