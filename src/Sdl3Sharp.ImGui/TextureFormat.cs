using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Texture format options for font atlas textures.
/// </summary>
/// <remarks>
/// Most standard backends only support RGBA32, but a single channel option is provided for low-resource/embedded systems.
/// </remarks>
public enum TextureFormat
{
    /// <summary>
    /// 4 components per pixel, each is unsigned 8-bit. Total size = TexWidth * TexHeight * 4.
    /// </summary>
    Rgba32 = ImTextureFormat.RGBA32,

    /// <summary>
    /// 1 component per pixel, each is unsigned 8-bit. Total size = TexWidth * TexHeight.
    /// </summary>
    Alpha8 = ImTextureFormat.Alpha8
}
