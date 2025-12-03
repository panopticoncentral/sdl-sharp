using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Flags for font atlas build configuration.
/// </summary>
[Flags]
public enum FontAtlasFlags
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = ImFontAtlasFlags.None,

    /// <summary>
    /// Don't round the height to next power of two.
    /// </summary>
    NoPowerOfTwoHeight = ImFontAtlasFlags.NoPowerOfTwoHeight,

    /// <summary>
    /// Don't build software mouse cursors into the atlas (saves a little texture memory).
    /// </summary>
    NoMouseCursors = ImFontAtlasFlags.NoMouseCursors,

    /// <summary>
    /// Don't build thick line textures into the atlas (saves a little texture memory, allows support for point/nearest filtering).
    /// The AntiAliasedLinesUseTex feature uses them, otherwise they will be rendered using polygons (more expensive for CPU/GPU).
    /// </summary>
    NoBakedLines = ImFontAtlasFlags.NoBakedLines
}
