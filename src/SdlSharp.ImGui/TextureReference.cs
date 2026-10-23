using SdlSharp.Graphics.Gpu;
using static SdlSharp.ImGui.Native;
namespace SdlSharp.ImGui;

/// <summary>A backend ID or a guarded deferred ImGui texture reference. Borrowed atlas references expire on the next frame.</summary>
public readonly unsafe struct TextureReference
{
    private readonly IGSharp_TextureRef _value;
    private readonly Lifetime? _dataLifetime;
    private readonly Lifetime? _snapshotLifetime;
    internal readonly GpuTextureReference? GpuTexture;
    private readonly int _frameVersion;
    internal IGSharp_TextureRef Value
    {
        get
        {
            _dataLifetime?.Check(); _snapshotLifetime?.Check();
            if (_dataLifetime != null && _frameVersion >= 0 && _dataLifetime.FrameVersion != _frameVersion)
                throw new InvalidOperationException("The atlas texture reference expired. Query it again for this frame.");
            GpuTexture?.Check();
            return _value;
        }
    }
    /// <summary>Resolves the backend ID, or zero before upload.</summary>
    public ulong Id { get { var value = Value; return value.Data == null ? value.ID : IGSharp_TextureData_GetTexID(value.Data); } }
    internal TextureReference(IGSharp_TextureData* data) : this(new IGSharp_TextureRef(data)) { }
    internal TextureReference(IGSharp_TextureRef value, Lifetime? snapshot = null, GpuTextureReference? gpuTexture = null)
    {
        _value = value; _snapshotLifetime = snapshot; GpuTexture = gpuTexture;
        _dataLifetime = value.Data == null ? null : Lifetime.Capture((nint)value.Data,
            Context.TryGetCurrent() is null ? null : Lifetime.Capture((nint)IGSharp_GetIO()->Fonts));
        _frameVersion = _dataLifetime == null || _dataLifetime.Owned || snapshot != null ? -1 : _dataLifetime.FrameVersion;
    }
    /// <summary>Creates a reference to an application-owned backend ID. Keep the GPU resource alive through rendering.</summary>
    public TextureReference(ulong id) { _value = new(id); _dataLifetime = null; _snapshotLifetime = null; GpuTexture = null; _frameVersion = -1; }
    private TextureReference(GpuTexture texture) : this((ulong)texture.NativeHandle) => GpuTexture = new(texture);
    /// <summary>Converts an application-owned backend ID.</summary>
    public static implicit operator TextureReference(ulong id) => new(id);
    /// <summary>Converts a GPU texture. Recording it retains its native lifetime through the draw list and its snapshots.</summary>
    public static implicit operator TextureReference(GpuTexture texture) { ArgumentNullException.ThrowIfNull(texture); return new(texture); }
    /// <summary>Converts a guarded texture view.</summary>
    public static implicit operator TextureReference(TextureData texture) => texture.Reference;
}
