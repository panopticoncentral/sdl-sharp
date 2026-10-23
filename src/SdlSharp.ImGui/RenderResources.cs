using System.Runtime.InteropServices;
using static SdlSharp.ImGui.Native;
namespace SdlSharp.ImGui;

/// <summary>A copied vertex in an ImGui mesh.</summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct DrawVertex(Vec2 Position, Vec2 Uv, uint Color);
internal static unsafe class DrawGeometry
{
    internal static DrawVertex[] CopyVertices(IGSharp_DrawList* list)
    {
        var count = IGSharp_DrawList_GetVtxBufferSize(list);
        return new ReadOnlySpan<DrawVertex>(IGSharp_DrawList_GetVtxBufferData(list), count).ToArray();
    }
    internal static ushort[] CopyIndices(IGSharp_DrawList* list)
        => new ReadOnlySpan<ushort>(IGSharp_DrawList_GetIdxBufferData(list), IGSharp_DrawList_GetIdxBufferSize(list)).ToArray();
}

/// <summary>Owns CPU texture data for a custom renderer. GPU resources remain the renderer's responsibility.</summary>
public sealed unsafe class OwnedTextureData : IDisposable
{
    private readonly IGSharp_TextureData* _handle;
    internal Lifetime Lifetime { get; }
    /// <summary>Creates a texture with zeroed CPU pixels.</summary>
    public OwnedTextureData(TextureFormat format, int width, int height)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width); ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);
        if (format is not TextureFormat.RGBA32 and not TextureFormat.Alpha8) throw new ArgumentOutOfRangeException(nameof(format));
        _ = checked(width * height * (format == TextureFormat.RGBA32 ? 4 : 1));
        _handle = IGSharp_TextureData_Create(); var pointer = (nint)_handle;
        Lifetime = Lifetime.Own(pointer, () => IGSharp_TextureData_Destroy((IGSharp_TextureData*)pointer));
        IGSharp_TextureData_AllocatePixels(_handle, (int)format, width, height);
        Pixels.Clear();
    }
    /// <summary>A guarded view of the texture.</summary>
    public TextureData Texture { get { Lifetime.Check(); return new(_handle, Lifetime, frame: false); } }
    /// <summary>Writable CPU pixels. The span expires at disposal or DestroyPixels; copy data when retaining it.</summary>
    public Span<byte> Pixels { get { var texture = Texture; var pointer = IGSharp_TextureData_GetPixels(texture.Handle); return pointer == null ? default : new(pointer, texture.SizeInBytes); } }
    /// <summary>Frees CPU pixels after a successful upload.</summary>
    public void DestroyPixels() { Lifetime.EnsureUnretained(); IGSharp_TextureData_DestroyPixels(Texture.Handle); }
    /// <summary>Releases CPU storage after all containers and snapshots have released this texture.</summary>
    public void Dispose() => Lifetime.Dispose();
}

/// <summary>An owned list that retains each texture while it is attached to the list.</summary>
public sealed unsafe class TextureList : IDisposable
{
    private readonly IGSharp_TextureList* _handle;
    private readonly List<IDisposable> _leases = [];
    internal Lifetime Lifetime { get; }
    internal IGSharp_TextureList* Handle { get { Lifetime.Check(); return _handle; } }
    /// <summary>Creates an empty texture list.</summary>
    public TextureList()
    {
        _handle = IGSharp_TextureList_Create(); var pointer = (nint)_handle;
        Lifetime = Lifetime.Own(pointer, () => IGSharp_TextureList_Destroy((IGSharp_TextureList*)pointer));
        Lifetime.AddCleanup(ReleaseTextures);
    }
    /// <summary>Number of textures.</summary>
    public int Count => IGSharp_TextureList_GetCount(Handle);
    /// <summary>Gets a guarded texture view.</summary>
    public TextureData this[int index]
    {
        get { ArgumentOutOfRangeException.ThrowIfNegative(index); ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count); var pointer = IGSharp_TextureList_Get(Handle, index); var lifetime = SdlSharp.ImGui.Lifetime.Capture((nint)pointer); return new(pointer, lifetime, frame: !lifetime.Owned); }
    }
    /// <summary>Adds and retains a texture until Clear or disposal.</summary>
    public void Add(TextureData texture)
    {
        Lifetime.EnsureUnretained(); var target = texture.Handle; var handle = Handle;
        var lease = TextureRetention.Retain(target, Lifetime);
        try { IGSharp_TextureList_Add(handle, target); _leases.Add(lease); } catch { lease.Dispose(); throw; }
    }
    /// <summary>Removes all textures, releasing their leases.</summary>
    public void Clear() { Lifetime.EnsureUnretained(); IGSharp_TextureList_Clear(Handle); ReleaseTextures(); }
    private void ReleaseTextures() { foreach (var lease in _leases) lease.Dispose(); _leases.Clear(); }
    /// <summary>Releases this list after attached draw-data owners have released it.</summary>
    public void Dispose() => Lifetime.Dispose();
}

/// <summary>Owns render submission metadata and retains immutable lists and an optional texture list.</summary>
public sealed unsafe class OwnedDrawData : IDisposable
{
    private readonly IGSharp_DrawData* _handle;
    private readonly List<IDisposable> _leases = [];
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2213", Justification = "Released by ReleaseDependencies registered on Lifetime, including context-driven disposal.")]
    private IDisposable? _textureLease;
    private TextureList? _textures;
    internal Lifetime Lifetime { get; }
    private IGSharp_DrawData* Handle { get { Lifetime.Check(); return _handle; } }
    /// <summary>Creates an empty render submission.</summary>
    public OwnedDrawData()
    {
        _handle = IGSharp_DrawData_Create(); var pointer = (nint)_handle;
        Lifetime = Lifetime.Own(pointer, () => IGSharp_DrawData_Destroy((IGSharp_DrawData*)pointer));
        Lifetime.AddCleanup(ReleaseDependencies);
    }
    /// <summary>A borrowed view that remains valid until this owner is disposed or cleared.</summary>
    public DrawData Data => new(Handle, Lifetime, frame: false);
    /// <summary>Whether this submission is ready to render.</summary>
    public bool Valid { get => IGSharp_DrawData_GetValid(Handle); set => IGSharp_DrawData_SetValid(Handle, value); }
    /// <summary>Frame number associated with the submission.</summary>
    public int FrameCount { get => IGSharp_DrawData_GetFrameCount(Handle); set => IGSharp_DrawData_SetFrameCount(Handle, value); }
    /// <summary>Top-left display coordinate.</summary>
    public Vec2 DisplayPosition { get => Data.DisplayPos; set => IGSharp_DrawData_SetDisplayPos(Handle, new(value.X, value.Y)); }
    /// <summary>Display dimensions in logical coordinates.</summary>
    public Vec2 DisplaySize { get => Data.DisplaySize; set => IGSharp_DrawData_SetDisplaySize(Handle, new(value.X, value.Y)); }
    /// <summary>Ratio of framebuffer pixels to logical display coordinates.</summary>
    public Vec2 FramebufferScale { get => Data.FramebufferScale; set => IGSharp_DrawData_SetFramebufferScale(Handle, new(value.X, value.Y)); }
    /// <summary>Texture updates needed for this submission; the list cannot be changed or disposed while attached.</summary>
    public TextureList? Textures
    {
        get { Lifetime.Check(); return _textures; }
        set
        {
            var handle = Handle; var pointer = value == null ? null : value.Handle;
            var lease = value?.Lifetime.RetainFor(Lifetime);
            IGSharp_DrawData_SetTextures(handle, pointer); _textureLease?.Dispose(); _textureLease = lease; _textures = value;
        }
    }
    /// <summary>Assigns the submission's viewport. Query a current viewport when preparing each submission.</summary>
    public void SetOwnerViewport(Viewport viewport) => IGSharp_DrawData_SetOwnerViewport(Handle, viewport.Handle);
    /// <summary>Retains a cloned draw list until this submission is cleared or disposed.</summary>
    public void Add(ClonedDrawList list)
    {
        ArgumentNullException.ThrowIfNull(list); var handle = Handle; var pointer = list.Handle;
        var lease = list.Lifetime.RetainFor(Lifetime);
        try { IGSharp_DrawData_AddDrawList(handle, pointer); _leases.Add(lease); } catch { lease.Dispose(); throw; }
    }
    /// <summary>Clears metadata and releases all dependencies.</summary>
    public void Clear() { IGSharp_DrawData_Clear(Handle); ReleaseDependencies(); Lifetime.InvalidateViews(); }
    private void ReleaseDependencies() { foreach (var lease in _leases) lease.Dispose(); _leases.Clear(); _textureLease?.Dispose(); _textureLease = null; _textures = null; }
    /// <summary>Releases submission storage and retained dependencies.</summary>
    public void Dispose() => Lifetime.Dispose();
}
