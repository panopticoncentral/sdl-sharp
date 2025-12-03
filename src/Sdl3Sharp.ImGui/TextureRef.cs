using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents a reference to a texture, which can be either a <see cref="TextureData"/> or a raw texture ID.
/// </summary>
/// <remarks>
/// <para>
/// A TextureRef can hold either a pointer to texture data (owned by a FontAtlas) or a low-level
/// backend texture identifier. Only one is set at a time, never both.
/// </para>
/// <para>
/// When TexData is set, it will convert to a texture ID during the render loop after the texture
/// has been uploaded. When TexId is set directly, it represents a texture already uploaded or
/// created by the user/application.
/// </para>
/// </remarks>
public readonly unsafe struct TextureRef
{
    private readonly ImTextureRef _native;

    /// <summary>
    /// Creates a new TextureRef wrapper from a native ImTextureRef value.
    /// </summary>
    /// <param name="native">The native ImTextureRef value.</param>
    internal TextureRef(ImTextureRef native)
    {
        _native = native;
    }

    /// <summary>
    /// Gets the underlying native structure.
    /// </summary>
    internal ImTextureRef Native => _native;

    /// <summary>
    /// Gets whether this TextureRef has a valid texture data reference.
    /// </summary>
    public bool HasTexData => _native.TexData != null;

    /// <summary>
    /// Gets the texture data if this reference points to texture data.
    /// </summary>
    /// <remarks>
    /// Check <see cref="HasTexData"/> before accessing this property.
    /// </remarks>
    public TextureData TexData => TextureData.FromNative(_native.TexData);

    /// <summary>
    /// Gets the raw texture ID. This is valid when TexData is null.
    /// </summary>
    public ImTextureID TexId => _native.TexID;

    /// <summary>
    /// Gets the effective texture ID. Returns TexData's TexID if TexData is set, otherwise returns TexID directly.
    /// </summary>
    /// <returns>The texture ID.</returns>
    public ImTextureID GetTexId()
    {
        fixed (ImTextureRef* ptr = &_native)
        {
            return ImTextureRef.GetTexID(ptr);
        }
    }

    /// <summary>
    /// Implicitly converts a native ImTextureRef to a managed TextureRef.
    /// </summary>
    /// <param name="native">The native ImTextureRef.</param>
    public static implicit operator TextureRef(ImTextureRef native)
    {
        return new(native);
    }

    /// <summary>
    /// Implicitly converts a managed TextureRef to a native ImTextureRef.
    /// </summary>
    /// <param name="textureRef">The managed TextureRef.</param>
    public static implicit operator ImTextureRef(TextureRef textureRef)
    {
        return textureRef._native;
    }
}
