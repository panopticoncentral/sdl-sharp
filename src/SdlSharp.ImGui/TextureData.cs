using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>Pixel format of an ImGui-managed texture.</summary>
public enum TextureFormat
{
    /// <summary>4 unsigned 8-bit components per pixel (total size = Width * Height * 4).</summary>
    Rgba32 = 0,
    /// <summary>1 unsigned 8-bit component per pixel (total size = Width * Height).</summary>
    Alpha8 = 1,
}

/// <summary>Lifecycle status of an ImGui-managed texture, driven by the renderer backend.</summary>
public enum TextureStatus
{
    /// <summary>The backend has created the texture and it is up to date.</summary>
    Ok = 0,
    /// <summary>The texture has been destroyed by the backend (or never created).</summary>
    Destroyed = 1,
    /// <summary>ImGui wants the backend to create the texture.</summary>
    WantCreate = 2,
    /// <summary>ImGui wants the backend to upload one or more dirty regions.</summary>
    WantUpdates = 3,
    /// <summary>ImGui wants the backend to destroy the texture.</summary>
    WantDestroy = 4,
}

/// <summary>A rectangular region of a texture, in texels.</summary>
public readonly record struct TextureRect(int X, int Y, int Width, int Height);

/// <summary>
/// A read-only view of an ImGui-managed texture (currently the font atlas texture). Obtain via
/// <see cref="FontAtlas.GetTexData"/>.
/// <para>
/// The texture is owned by ImGui and serviced by the renderer backend (the SDL_GPU backend
/// handles creation, updates, and destruction automatically). The underlying pointer stays valid
/// while the owning atlas is alive, but the CPU-side pixel data may be freed by the backend once
/// uploaded to the GPU, and size/contents can change any frame as glyphs are baked — read what
/// you need immediately rather than caching.
/// </para>
/// </summary>
public readonly unsafe struct TextureData
{
    internal readonly IGSharp_TextureData* Handle;

    internal TextureData(IGSharp_TextureData* handle) => Handle = handle;

    /// <summary>True if this handle refers to a valid texture.</summary>
    public bool IsValid => Handle != null;

    /// <summary>Sequential unique id, useful for debugging and correlating with backend resources.</summary>
    public int UniqueId => IGSharp_TextureData_GetUniqueID(Handle);

    /// <summary>Current lifecycle status (what ImGui wants the backend to do with this texture).</summary>
    public TextureStatus Status => (TextureStatus)IGSharp_TextureData_GetStatus(Handle);

    /// <summary>
    /// Backend-specific texture identifier once created (SDL_GPU backend: an SDL_GPUTexture*),
    /// usable with <see cref="DrawList.AddImage(ulong, Vec2, Vec2, Vec2, Vec2, uint)"/>.
    /// 0 until the backend creates the texture.
    /// </summary>
    public ulong TextureId => IGSharp_TextureData_GetTexID(Handle);

    /// <summary>Pixel format of the texture data.</summary>
    public TextureFormat Format => (TextureFormat)IGSharp_TextureData_GetFormat(Handle);

    /// <summary>Texture width in texels.</summary>
    public int Width => IGSharp_TextureData_GetWidth(Handle);

    /// <summary>Texture height in texels.</summary>
    public int Height => IGSharp_TextureData_GetHeight(Handle);

    /// <summary>Bytes per pixel for <see cref="Format"/> (4 for RGBA32, 1 for Alpha8).</summary>
    public int BytesPerPixel => IGSharp_TextureData_GetBytesPerPixel(Handle);

    /// <summary>Bytes per row of pixel data (= Width * BytesPerPixel).</summary>
    public int Pitch => IGSharp_TextureData_GetPitch(Handle);

    /// <summary>Total size of the CPU-side pixel buffer in bytes (= Height * Pitch).</summary>
    public int SizeInBytes => IGSharp_TextureData_GetSizeInBytes(Handle);

    /// <summary>True if the pixel data contains colors other than white (affects shaders that sample alpha only).</summary>
    public bool UseColors => IGSharp_TextureData_GetUseColors(Handle);

    /// <summary>Number of frames since the texture was last used by a draw command (for backend garbage collection).</summary>
    public int UnusedFrames => IGSharp_TextureData_GetUnusedFrames(Handle);

    /// <summary>Number of atlases/contexts sharing this texture.</summary>
    public int RefCount => IGSharp_TextureData_GetRefCount(Handle);

    /// <summary>The region of the texture that contains packed data.</summary>
    public TextureRect UsedRect
    {
        get
        {
            ushort x, y, w, h;
            IGSharp_TextureData_GetUsedRect(Handle, &x, &y, &w, &h);
            return new TextureRect(x, y, w, h);
        }
    }

    /// <summary>Bounding box of all pending updates (valid when <see cref="Status"/> is <see cref="TextureStatus.WantUpdates"/>).</summary>
    public TextureRect UpdateRect
    {
        get
        {
            ushort x, y, w, h;
            IGSharp_TextureData_GetUpdateRect(Handle, &x, &y, &w, &h);
            return new TextureRect(x, y, w, h);
        }
    }

    /// <summary>Number of individual dirty regions pending upload this frame.</summary>
    public int UpdateCount => IGSharp_TextureData_GetUpdatesCount(Handle);

    /// <summary>Gets one pending dirty region by index (0 to <see cref="UpdateCount"/> - 1).</summary>
    public TextureRect GetUpdate(int index)
    {
        ushort x, y, w, h;
        IGSharp_TextureData_GetUpdate(Handle, index, &x, &y, &w, &h);
        return new TextureRect(x, y, w, h);
    }

    /// <summary>Raw pointer to the CPU-side pixel buffer (may be 0 after the backend uploads and frees it).</summary>
    public nint Pixels => (nint)IGSharp_TextureData_GetPixels(Handle);

    /// <summary>Raw pointer to the pixel at (<paramref name="x"/>, <paramref name="y"/>).</summary>
    public nint GetPixelsAt(int x, int y) => (nint)IGSharp_TextureData_GetPixelsAt(Handle, x, y);

    /// <summary>
    /// The CPU-side pixel buffer as a span (empty if the backend has freed it). Valid only until
    /// the atlas next bakes glyphs or the backend destroys the pixel data — copy out promptly.
    /// </summary>
    public ReadOnlySpan<byte> GetPixelSpan()
    {
        var pixels = IGSharp_TextureData_GetPixels(Handle);
        return pixels == null ? default : new ReadOnlySpan<byte>(pixels, IGSharp_TextureData_GetSizeInBytes(Handle));
    }
}
