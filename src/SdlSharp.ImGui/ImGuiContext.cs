using static SdlSharp.ImGui.Native;
using NativeImGuiContext = SdlSharp.ImGui.Native.ImGuiContext;

namespace SdlSharp.ImGui;

/// <summary>
/// Manages a Dear ImGui context. Dispose to destroy the context.
/// </summary>
public sealed unsafe class ImGuiContext : IDisposable
{
    private NativeImGuiContext* _handle;

    private ImGuiContext(NativeImGuiContext* handle) { _handle = handle; }

    internal NativeImGuiContext* Handle => _handle;

    /// <summary>
    /// Creates a new ImGui context and sets it as the current context.
    /// </summary>
    public static ImGuiContext Create()
    {
        var ctx = IGSharp_CreateContext();
        if (ctx == null)
            throw new InvalidOperationException("Failed to create ImGui context.");
        IGSharp_CheckVersion();
        return new ImGuiContext(ctx);
    }

    /// <summary>Makes this context the active one for subsequent ImGui calls.</summary>
    public void MakeCurrent()
    {
        if (_handle == null) throw new ObjectDisposedException(nameof(ImGuiContext));
        IGSharp_SetCurrentContext(_handle);
    }

    /// <summary>True if this is the currently active ImGui context.</summary>
    public bool IsCurrent => _handle != null && IGSharp_GetCurrentContext() == _handle;

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_handle != null)
        {
            IGSharp_DestroyContext(_handle);
            _handle = null;
        }
    }
}
