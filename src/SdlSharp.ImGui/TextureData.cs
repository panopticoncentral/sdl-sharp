using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>A rectangular region of a texture, in texels.</summary>
public readonly record struct TextureRect(int X, int Y, int Width, int Height);

/// <summary>
/// A borrowed view of an ImGui-managed texture (currently the font atlas texture). Obtain via
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
    /// <summary>Borrowed texture reference, resolved by the renderer after upload.</summary>
    public TextureReference Reference => new(Handle);

    /// <summary>Rendering-queue retention token. Clear when the queue releases the texture.</summary>
    public nint QueueUserData
    {
        get => (nint)IGSharp_TextureData_GetQueueUserData(Handle);
        set { if (IsRetainedForRendering) throw new InvalidOperationException("The managed render queue owns this association."); IGSharp_TextureData_SetQueueUserData(Handle, (void*)value); }
    }

    private readonly NativeView<IGSharp_TextureData> _view;
    internal IGSharp_TextureData* Handle => _view.Handle;
    internal Lifetime Lifetime => _view.Lifetime!;

    /// <summary>Renderer-owned opaque association. Its ownership is managed by the renderer.</summary>
    public nint BackendUserData { get => (nint)IGSharp_TextureData_GetBackendUserData(Handle); set => IGSharp_TextureData_SetBackendUserData(Handle, (void*)value); }
    /// <summary>Copies CPU pixels into a managed array.</summary>
    public byte[] CopyPixels() => GetPixelSpan().ToArray();
    internal TextureData(IGSharp_TextureData* handle, Lifetime? lifetime = null, bool? frame = null)
    {
        lifetime ??= handle == null ? null : SdlSharp.ImGui.Lifetime.Capture((nint)handle,
            Context.TryGetCurrent() is null ? null : SdlSharp.ImGui.Lifetime.Capture((nint)IGSharp_GetIO()->Fonts));
        _view = new(handle, lifetime, frame ?? !(lifetime?.Owned ?? false));
    }

    /// <summary>True if this handle refers to a valid texture.</summary>
    public bool IsValid => _view.IsValid;

    /// <summary>Whether a managed render snapshot retains this texture. Custom renderers must defer destruction while true.</summary>
    public bool IsRetainedForRendering => TextureRetention.IsRetained(Handle);

    /// <summary>Sequential unique id, useful for debugging and correlating with backend resources.</summary>
    public int UniqueId => IGSharp_TextureData_GetUniqueID(Handle);

    /// <summary>Current lifecycle status (what ImGui wants the backend to do with this texture).</summary>
    public TextureStatus Status { get => (TextureStatus)IGSharp_TextureData_GetStatus(Handle); set { if (value == TextureStatus.Destroyed && IsRetainedForRendering) throw new InvalidOperationException("A render snapshot still retains this texture."); IGSharp_TextureData_SetStatus(Handle, (int)value); } }

    /// <summary>
    /// Backend-specific texture identifier once created (SDL_GPU backend: an SDL_GPUTexture*),
    /// usable with <see cref="DrawList.AddImage(TextureReference, Vec2, Vec2, Vec2, Vec2, uint)"/>.
    /// 0 until the backend creates the texture.
    /// </summary>
    public ulong TextureId { get => IGSharp_TextureData_GetTexID(Handle); set => IGSharp_TextureData_SetTexID(Handle, value); }

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
        ArgumentOutOfRangeException.ThrowIfNegative(index); ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, UpdateCount);
        ushort x, y, w, h;
        IGSharp_TextureData_GetUpdate(Handle, index, &x, &y, &w, &h);
        return new TextureRect(x, y, w, h);
    }

    /// <summary>Raw pointer to the CPU-side pixel buffer (may be 0 after the backend uploads and frees it).</summary>
    public nint Pixels => (nint)IGSharp_TextureData_GetPixels(Handle);

    /// <summary>Raw pointer to the pixel at (<paramref name="x"/>, <paramref name="y"/>).</summary>
    public nint GetPixelsAt(int x, int y) { ArgumentOutOfRangeException.ThrowIfNegative(x); ArgumentOutOfRangeException.ThrowIfNegative(y); ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(x, Width); ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(y, Height); if (IGSharp_TextureData_GetPixels(Handle) == null) throw new InvalidOperationException("CPU pixels have been released."); return (nint)IGSharp_TextureData_GetPixelsAt(Handle, x, y); }

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
