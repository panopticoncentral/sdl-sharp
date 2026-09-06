using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// An owned draw list for custom drawing across frames. Create after starting a frame,
/// and dispose before destroying its context.
/// </summary>
public sealed unsafe class StandaloneDrawList : IDisposable
{
    private IGSharp_DrawList* _handle;

    private StandaloneDrawList(IGSharp_DrawList* handle) => _handle = handle;

    private IGSharp_DrawList* Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle == null, this);
            return _handle;
        }
    }

    /// <summary>
    /// Creates an initialized list using the current context's shared data.
    /// Call after starting a frame, then push a clip rectangle and texture before drawing.
    /// </summary>
    public static StandaloneDrawList Create()
    {
        if (IGSharp_GetCurrentContext() == null)
            throw new InvalidOperationException("A current ImGui context is required.");
        return new(IGSharp_DrawList_Create(IGSharp_GetDrawListSharedData()));
    }

    /// <summary>
    /// A borrowed drawing view. Do not retain or use it after this owner is disposed.
    /// </summary>
    public DrawList List => new(Handle);

    /// <summary>
    /// Clears the previous output after starting a new frame on the owning context.
    /// Push the clip rectangle and texture again before drawing.
    /// </summary>
    public void ResetForNewFrame() => IGSharp_DrawList_ResetForNewFrame(Handle);

    /// <summary>Releases the native list. Safe to call more than once.</summary>
    public void Dispose()
    {
        if (_handle == null) return;
        IGSharp_DrawList_Destroy(_handle);
        _handle = null;
    }
}
