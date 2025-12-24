using static Sdl3Sharp.ImGui.Native.ImGui;

namespace Sdl3Sharp.ImGui;

public static class Widgets
{
    /// <summary>
    /// Draws a separator, generally horizontal. Inside a menu bar or in horizontal layout mode, this becomes a vertical separator.
    /// </summary>
    public static void Separator()
    {
        ImGui_Separator();
    }

    /// <summary>
    /// Calls between widgets or groups to layout them horizontally with custom positioning.
    /// </summary>
    /// <param name="offsetFromStartX">X position from window start in window coordinates. 0.0f to use current position.</param>
    /// <param name="spacing">Spacing between the previous widget and current position. -1.0f to use default spacing.</param>
    public static void SameLine(float offsetFromStartX = 0.0f, float spacing = -1.0f)
    {
        ImGui_SameLineEx(offsetFromStartX, spacing);
    }

    /// <summary>
    /// Undoes a <see cref="SameLine"/> or forces a new line when in a horizontal-layout context.
    /// </summary>
    public static void NewLine()
    {
        ImGui_NewLine();
    }

    /// <summary>
    /// Adds vertical spacing.
    /// </summary>
    public static void Spacing()
    {
        ImGui_Spacing();
    }

    /// <summary>
    /// Adds a dummy item of the given size.
    /// </summary>
    /// <param name="size">The size of the dummy item.</param>
    /// <remarks>
    /// Unlike InvisibleButton(), Dummy() won't take mouse clicks or be navigable.
    /// </remarks>
    public static void Dummy(Size size)
    {
        ImGui_Dummy(size.Value);
    }

    /// <summary>
    /// Moves content position toward the right by the specified amount.
    /// </summary>
    /// <param name="indentW">The indentation amount. If less than or equal to 0, uses style.IndentSpacing.</param>
    public static void Indent(float indentW = 0.0f)
    {
        ImGui_IndentEx(indentW);
    }

    /// <summary>
    /// Moves content position back to the left by the specified amount.
    /// </summary>
    /// <param name="indentW">The unindentation amount. If less than or equal to 0, uses style.IndentSpacing.</param>
    public static void Unindent(float indentW = 0.0f)
    {
        ImGui_UnindentEx(indentW);
    }
}