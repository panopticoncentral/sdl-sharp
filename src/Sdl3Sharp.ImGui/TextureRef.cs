using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a reference to a texture, which can be either a <see cref="TextureData"/> or a raw texture ID.
/// </summary>
public readonly unsafe struct TextureRef
{
    internal TextureRef(ImTextureRef native)
    {
        Native = native;
    }

    /// <summary>
    /// Initializes a new instance of the TextureRef class with the specified texture identifier.
    /// </summary>
    /// <param name="id">The texture identifier used to reference the native texture resource.</param>
    public TextureRef(TextureId id)
    {
        Native = new ImTextureRef
        {
            TexID = id.Value,
            TexData = null
        };
    }

    /// <summary>
    /// Initializes a new instance of the TextureRef class using the specified texture data.
    /// </summary>
    /// <param name="data">The texture data to associate with this texture reference. Cannot be null.</param>
    public TextureRef(TextureData data)
    {
        Native = new ImTextureRef
        {
            TexID = TextureId.Invalid.Value,
            TexData = data.Native
        };
    }

    internal ImTextureRef Native { get; }

    /// <summary>
    /// Gets the texture data if this reference points to texture data.
    /// </summary>
    public readonly TextureData? Data => Native.TexData != null ? new(Native.TexData) : null;

    /// <summary>
    /// Gets the raw texture ID. This is valid when xData is null.
    /// </summary>
    public readonly TextureId Id => Native.TexData == null ? new(Native.TexID) : TextureId.Invalid;
}
