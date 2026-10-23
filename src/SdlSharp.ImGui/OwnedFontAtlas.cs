using static SdlSharp.ImGui.Native;
namespace SdlSharp.ImGui;

/// <summary>Owns a font atlas that can be shared by contexts on the creating thread.</summary>
public sealed unsafe class OwnedFontAtlas : IDisposable
{
    private readonly IGSharp_FontAtlas* _handle;
    private readonly Lifetime _lifetime;
    /// <summary>Creates an independent atlas. Dispose its contexts before disposing the atlas.</summary>
    public OwnedFontAtlas()
    {
        _handle = IGSharp_FontAtlas_Create();
        if (_handle == null) throw new OutOfMemoryException();
        var pointer = (nint)_handle;
        _lifetime = new(pointer, null, destroy: () => IGSharp_FontAtlas_Destroy((IGSharp_FontAtlas*)pointer));
    }
    /// <summary>A guarded view of the owned atlas.</summary>
    public FontAtlas Atlas { get { _lifetime.Check(); return new(_handle, _lifetime); } }
    /// <summary>Releases the atlas; throws while a context or render snapshot still retains it.</summary>
    public void Dispose() => _lifetime.Dispose();
}
