using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for child window creation.
/// </summary>
/// <remarks>
/// <para>Legacy: bit 0 must always correspond to Borders to be backward compatible with old API using 'bool border = false'.</para>
/// <para>About using AutoResizeX/AutoResizeY flags:</para>
/// <list type="bullet">
/// <item>May be combined with SetNextWindowSizeConstraints() to set a min/max size for each axis.</item>
/// <item>Size measurement for a given axis is only performed when the child window is within visible boundaries, or is just appearing.</item>
/// <item>This allows BeginChild() to return false when not within boundaries (e.g. when scrolling), which is more optimal.</item>
/// </list>
/// </remarks>
[Flags]
public enum ChildFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImGuiChildFlags.None,

    /// <summary>
    /// Show an outer border and enable WindowPadding. (IMPORTANT: this is always == 1 == true for legacy reason)
    /// </summary>
    Borders = ImGuiChildFlags.Borders,

    /// <summary>
    /// Pad with style.WindowPadding even if no border are drawn (no padding by default for non-bordered child windows because it makes more sense).
    /// </summary>
    AlwaysUseWindowPadding = ImGuiChildFlags.AlwaysUseWindowPadding,

    /// <summary>
    /// Allow resize from right border (layout direction). Enable .ini saving (unless NoSavedSettings passed to window flags).
    /// </summary>
    ResizeX = ImGuiChildFlags.ResizeX,

    /// <summary>
    /// Allow resize from bottom border (layout direction).
    /// </summary>
    ResizeY = ImGuiChildFlags.ResizeY,

    /// <summary>
    /// Enable auto-resizing width. Read "IMPORTANT: Size measurement" details in remarks.
    /// </summary>
    AutoResizeX = ImGuiChildFlags.AutoResizeX,

    /// <summary>
    /// Enable auto-resizing height. Read "IMPORTANT: Size measurement" details in remarks.
    /// </summary>
    AutoResizeY = ImGuiChildFlags.AutoResizeY,

    /// <summary>
    /// Combined with AutoResizeX/AutoResizeY. Always measure size even when child is hidden, always return true, always disable clipping optimization! NOT RECOMMENDED.
    /// </summary>
    AlwaysAutoResize = ImGuiChildFlags.AlwaysAutoResize,

    /// <summary>
    /// Style the child window like a framed item: use FrameBg, FrameRounding, FrameBorderSize, FramePadding instead of ChildBg, ChildRounding, ChildBorderSize, WindowPadding.
    /// </summary>
    FrameStyle = ImGuiChildFlags.FrameStyle,

    /// <summary>
    /// [BETA] Share focus scope, allow keyboard/gamepad navigation to cross over parent border to this child or between sibling child windows.
    /// </summary>
    NavFlattened = ImGuiChildFlags.NavFlattened
}
