using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public readonly ref struct ItemFlagScope : IDisposable
{
    /// <summary>
    /// Pushes an item flag modification onto the stack.
    /// </summary>
    /// <param name="option">The item flag to modify.</param>
    /// <param name="enabled">Whether the flag should be enabled.</param>
    public static ItemFlagScope Push(ItemFlags option, bool enabled)
    {
        ImGui_PushItemFlag((Native.ImGuiItemFlags)option, enabled);
        return new();
    }

    public void Dispose()
    {
        ImGui_PopItemFlag();
    }
}
