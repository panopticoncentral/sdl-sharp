using static SdlSharp.ImGui.Native.ImGui;

namespace SdlSharp.Gui;

/// <summary>
/// Manages a Dear ImGui context. Dispose to destroy the context.
/// </summary>
public sealed unsafe class GuiContext : IDisposable
{
    private ImGuiContext* _handle;

    private GuiContext(ImGuiContext* handle) { _handle = handle; }

    /// <summary>
    /// Creates a new ImGui context and sets it as the current context.
    /// </summary>
    public static GuiContext Create()
    {
        var ctx = IGSharp_CreateContext();
        if (ctx == null)
            throw new InvalidOperationException("Failed to create ImGui context.");
        IGSharp_CheckVersion();
        return new GuiContext(ctx);
    }

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
