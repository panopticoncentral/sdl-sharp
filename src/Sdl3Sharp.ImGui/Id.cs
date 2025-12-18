using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Identifier for ImGui.
/// </summary>
public readonly record struct Id
{
    internal readonly ImGuiID Value { get; }

    internal Id(ImGuiID value)
    {
        Value = value;
    }
}
