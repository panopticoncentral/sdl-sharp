using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Status of a texture to communicate with the renderer backend.
/// </summary>
public enum TextureStatus
{
    /// <summary>
    /// Texture is ready for use.
    /// </summary>
    Ok = ImTextureStatus.OK,

    /// <summary>
    /// Backend has destroyed the texture.
    /// </summary>
    Destroyed = ImTextureStatus.Destroyed,

    /// <summary>
    /// Requesting backend to create the texture. Set status to Ok when done.
    /// </summary>
    WantCreate = ImTextureStatus.WantCreate,

    /// <summary>
    /// Requesting backend to update specific blocks of pixels. Set status to Ok when done.
    /// </summary>
    WantUpdates = ImTextureStatus.WantUpdates,

    /// <summary>
    /// Requesting backend to destroy the texture. Set status to Destroyed when done.
    /// </summary>
    WantDestroy = ImTextureStatus.WantDestroy
}
