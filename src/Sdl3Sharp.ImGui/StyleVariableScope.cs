using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public readonly ref struct StyleVariableScope : IDisposable
{
    /// <summary>
    /// Pushes a style variable modification onto the stack using a float value.
    /// </summary>
    /// <param name="variable">The style variable to modify.</param>
    /// <param name="value">The new float value.</param>
    public static StyleVariableScope Push(StyleVariable variable, float value)
    {
        ImGui_PushStyleVar((Native.ImGuiStyleVar)variable, value);
        return new();
    }

    /// <summary>
    /// Pushes a style variable modification onto the stack using a Vec2 value.
    /// </summary>
    /// <param name="variable">The style variable to modify.</param>
    /// <param name="value">The new Vec2 value.</param>
    public static StyleVariableScope Push(StyleVariable variable, Vec2 value)
    {
        ImGui_PushStyleVarImVec2((Native.ImGuiStyleVar)variable, value.Value);
        return new();
    }

    /// <summary>
    /// Pushes a modification to the X component of a style Vec2 variable.
    /// </summary>
    /// <param name="variable">The style variable to modify.</param>
    /// <param name="value">The new X component value.</param>
    public static StyleVariableScope PushX(StyleVariable variable, float value)
    {
        ImGui_PushStyleVarX((Native.ImGuiStyleVar)variable, value);
        return new();
    }

    /// <summary>
    /// Pushes a modification to the Y component of a style Vec2 variable.
    /// </summary>
    /// <param name="variable">The style variable to modify.</param>
    /// <param name="value">The new Y component value.</param>
    public static StyleVariableScope PushY(StyleVariable variable, float value)
    {
        ImGui_PushStyleVarY((Native.ImGuiStyleVar)variable, value);
        return new();
    }

    public void Dispose()
    {
        ImGui_PopStyleVarEx(1);
    }
}
