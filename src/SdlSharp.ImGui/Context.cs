using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Manages a Dear ImGui context. Dispose to destroy the context.
/// </summary>
public sealed unsafe class Context : IDisposable
{
    private static readonly object Sync = new();
    private static readonly Dictionary<nint, Context> FontAtlasOwners = new();
    private readonly List<nint> _ownedFontRanges = [];

    private Context(IGSharp_Context* handle, IGSharp_FontAtlas* fontAtlas)
    {
        Handle = handle;
        FontAtlas = fontAtlas;
    }

    private IGSharp_Context* Handle { get; set; }
    private IGSharp_FontAtlas* FontAtlas { get; }

    /// <summary>
    /// Creates a new ImGui context and sets it as the current context.
    /// </summary>
    public static Context Create()
    {
        if (!IGSharp_ValidateLayouts(
                (nuint)sizeof(IGSharp_IO), (nuint)sizeof(IGSharp_Style), (nuint)sizeof(IGSharp_KeyData),
                (nuint)sizeof(IGSharp_PlatformImeData), (nuint)sizeof(IGSharp_DrawVert), (nuint)sizeof(IGSharp_FontAtlasRect)))
            throw new InvalidOperationException("imgui_sharp struct layout mismatch between the managed mirrors and the native library.");
        var ctx = IGSharp_CreateContext(null);
        if (ctx == null)
            throw new InvalidOperationException("Failed to create ImGui context.");
        IGSharp_SetCurrentContext(ctx);
        IGSharp_CheckVersion();
        var context = new Context(ctx, IGSharp_GetIO()->Fonts);
        lock (Sync)
        {
            FontAtlasOwners.Add((nint)context.FontAtlas, context);
        }
        return context;
    }

    /// <summary>Makes this context the active one for subsequent ImGui calls.</summary>
    public void MakeCurrent()
    {
        ObjectDisposedException.ThrowIf(Handle == null, typeof(Context));
        IGSharp_SetCurrentContext(Handle);
    }

    /// <summary>True if this is the currently active ImGui context.</summary>
    public bool IsCurrent => Handle != null && IGSharp_GetCurrentContext() == Handle;

    /// <inheritdoc/>
    public void Dispose()
    {
        if (Handle == null) return;
        var handle = Handle;
        lock (Sync)
        {
            FontAtlasOwners.Remove((nint)FontAtlas);
        }
        IGSharp_DestroyContext(handle);
        Handle = null;
        PlatformIO.ReleaseContext((nint)handle);
        Io.ReleaseContext((nint)handle);
        ImGui.ReleaseContext((nint)handle);
        DrawList.ReleaseContext((nint)handle);
        foreach (var range in _ownedFontRanges)
            IGSharp_MemFree((void*)range);
        _ownedFontRanges.Clear();
    }

    internal static bool AdoptFontRange(IGSharp_FontAtlas* atlas, ushort* range)
    {
        if (range == null) return true;
        lock (Sync)
        {
            if (!FontAtlasOwners.TryGetValue((nint)atlas, out var context))
                return false;
            context._ownedFontRanges.Add((nint)range);
            return true;
        }
    }
}
