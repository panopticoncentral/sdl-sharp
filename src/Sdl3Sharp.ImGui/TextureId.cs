using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Backend specific, low-level identifier for a texture uploaded in GPU/graphics system.
/// </summary>
public readonly record struct TextureId
{
    /// <summary>
    /// Represents an invalid texture ID.
    /// </summary>
    public static readonly TextureId Invalid = new(0);

    internal readonly ImTextureID Value { get; }

    internal TextureId(ImTextureID value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets a value indicating whether this ID is valid.
    /// </summary>
    public readonly bool IsValid => Value != -1;
}
