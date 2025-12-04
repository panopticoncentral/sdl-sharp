using Sdl3Sharp.ImGui.Native;

namespace Sdl3Sharp.ImGui;

/// <summary>
/// Represents specs and pixel storage for a texture used by Dear ImGui.
/// </summary>
public readonly unsafe struct TextureData
{
    internal readonly ImTextureData* Native { get; }

    /// <summary>
    /// Gets the unique identifier for this texture. Unique per atlas.
    /// </summary>
    public readonly int UniqueId => Native->UniqueID;

    /// <summary>
    /// The current status of the texture.
    /// </summary>
    public TextureStatus Status
    {
        readonly get => (TextureStatus)Native->Status;
        set => ImTextureData.SetStatus(Native, (ImTextureStatus)value);
    }

    /// <summary>
    /// Gets or sets backend-specific storage for convenience.
    /// </summary>
    public nint BackendUserData
    {
        readonly get => Native->BackendUserData;
        set => Native->BackendUserData = value;
    }

    /// <summary>
    /// Gets the backend-specific texture identifier.
    /// </summary>
    public ImTextureID TexId
    {
        readonly get => ImTextureData.GetTexID(Native);
        set => ImTextureData.SetTexID(Native, value);
    }

    /// <summary>
    /// Gets the texture format (RGBA32 or Alpha8).
    /// </summary>
    public readonly TextureFormat Format => (TextureFormat)Native->Format;

    /// <summary>
    /// The texture width in pixels.
    /// </summary>
    public readonly int Width => Native->Width;

    /// <summary>
    /// The texture height in pixels.
    /// </summary>
    public readonly int Height => Native->Height;

    /// <summary>
    /// The bytes per pixel (1 or 4).
    /// </summary>
    public readonly int BytesPerPixel => Native->BytesPerPixel;

    /// <summary>
    /// Gets the total size of the pixel buffer in bytes.
    /// </summary>
    public readonly int SizeInBytes => Native->Width * Native->Height * Native->BytesPerPixel;

    /// <summary>
    /// Gets the pitch (bytes per row) of the texture.
    /// </summary>
    public readonly int Pitch => Native->Width * Native->BytesPerPixel;

    /// <summary>
    /// A pointer to the pixel buffer.
    /// </summary>
    public readonly Span<byte> Pixels
    {
        get
        {
            var pixels = (byte*)ImTextureData.GetPixels(Native);
            return new Span<byte>(pixels, Native->Width * Native->Height * Native->BytesPerPixel);
        }
    }

    /// <summary>
    /// The number of successive frames where the texture was not used.
    /// </summary>
    public readonly int UnusedFrames => Native->UnusedFrames;

    /// <summary>
    /// The number of contexts using this texture.
    /// </summary>
    public readonly ushort RefCount => Native->RefCount;

    /// <summary>
    /// Whether the texture data uses colors (rather than just white + alpha).
    /// </summary>
    public readonly bool UseColors => Native->UseColors;

    /// <summary>
    /// The bounding box encompassing all past and queued updates.
    /// </summary>
    public readonly TextureRect UsedRect => new(Native->UsedRect);

    /// <summary>
    /// The bounding box encompassing all queued updates.
    /// </summary>
    public readonly TextureRect UpdateRect => new(Native->UpdateRect);

    internal TextureData(ImTextureData* native)
    {
        Native = native;
    }

    /// <summary>
    /// Gets a pointer to pixel data at a specific position.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>A pointer to the pixel at the specified position.</returns>
    public readonly Span<byte> GetPixelsAt(int x, int y)
    {
        var pixels = (byte*)ImTextureData.GetPixelsAt(Native, x, y);
        var size = ((Native->Width * (Native->Height - y)) - x) * Native->BytesPerPixel;
        return new Span<byte>(pixels, size);
    }
}
