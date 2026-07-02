using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Manages a Dear ImGui context. Dispose to destroy the context.
/// </summary>
public sealed unsafe class Context : IDisposable
{
    private Context(IGSharp_Context* handle) { Handle = handle; }

    private IGSharp_Context* Handle { get; set; }

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
        IGSharp_CheckVersion();
        return new Context(ctx);
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
        IGSharp_DestroyContext(Handle);
        Handle = null;
    }
}
