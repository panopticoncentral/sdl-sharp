using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a Dear ImGui context that manages the state for a single ImGui instance.
/// </summary>
public unsafe readonly struct Context
{
    internal Context(ImGuiContext* native)
    {
        Native = native;
    }

    internal readonly ImGuiContext* Native { get; init; }
}
