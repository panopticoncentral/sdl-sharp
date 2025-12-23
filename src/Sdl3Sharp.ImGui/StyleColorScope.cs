using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public readonly ref struct StyleColorScope: IDisposable
{
    /// <summary>
    /// Pushes a style color modification onto the stack using a 32-bit color value.
    /// </summary>
    /// <param name="systemColor">The color index to modify.</param>
    /// <param name="color">The color value as a 32-bit packed RGBA value.</param>
    public static StyleColorScope Push(StyleColor systemColor, uint color)
    {
        ImGui_PushStyleColor((Native.ImGuiCol)systemColor, color);
        return new();
    }

    /// <summary>
    /// Pushes a style color modification onto the stack using a Vec4 color value.
    /// </summary>
    /// <param name="systemColor">The color index to modify.</param>
    /// <param name="color">The color value.</param>
    public static StyleColorScope Push(StyleColor systemColor, Color color)
    {
        ImGui_PushStyleColorImVec4((Native.ImGuiCol)systemColor, color.Value);
        return new();
    }

    public void Dispose()
    {
        ImGui_PopStyleColorEx(1);
    }
}
