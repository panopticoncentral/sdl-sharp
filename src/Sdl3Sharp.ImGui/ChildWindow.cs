using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public readonly unsafe ref struct ChildWindow: IDisposable
{
    public bool IsVisible { get; }

    /// <summary>
    /// Creates a child window with the specified string identifier.
    /// </summary>
    /// <param name="id">The UTF-8 encoded string identifier for the child window.</param>
    /// <param name="size">The size of the child window. Use default for auto-sizing.</param>
    /// <param name="childFlags">Flags controlling child window behavior.</param>
    /// <param name="windowFlags">Flags controlling window appearance and behavior.</param>
    public ChildWindow(ReadOnlySpan<byte> id, Size size = default, ChildFlags childFlags = ChildFlags.None, WindowFlags windowFlags = WindowFlags.None)
    {
        fixed (byte* ptr = id)
        {
            IsVisible = ImGui_BeginChild(ptr, size.Value, (Native.ImGuiChildFlags)childFlags, (Native.ImGuiWindowFlags)windowFlags);
        }
    }

    /// <summary>
    /// Creates a child window with the specified numeric identifier.
    /// </summary>
    /// <param name="id">The numeric identifier for the child window.</param>
    /// <param name="size">The size of the child window. Use default for auto-sizing.</param>
    /// <param name="childFlags">Flags controlling child window behavior.</param>
    /// <param name="windowFlags">Flags controlling window appearance and behavior.</param>
    public ChildWindow(Id id, Size size = default, ChildFlags childFlags = ChildFlags.None, WindowFlags windowFlags = WindowFlags.None)
    {
        IsVisible = ImGui_BeginChildID(id.Value, size.Value, (Native.ImGuiChildFlags)childFlags, (Native.ImGuiWindowFlags)windowFlags);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ImGui_EndChild();
    }
}
