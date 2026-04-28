using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Manages a Dear ImGui context. Dispose to destroy the context.
/// </summary>
public sealed unsafe class Context : IDisposable
{
    private Context(ImGuiContext* handle) { Handle = handle; }

    private ImGuiContext* Handle { get; set; }

    /// <summary>
    /// Creates a new ImGui context and sets it as the current context.
    /// </summary>
    public static Context Create()
    {
        var ctx = IGSharp_CreateContext();
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
