using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// An owned draw list for custom drawing across frames. Create after starting a frame,
/// and dispose before destroying its context.
/// </summary>
public sealed unsafe class StandaloneDrawList : IDisposable
{
    private readonly IGSharp_DrawList* _handle;
    private readonly Lifetime _lifetime;
    private int _frame;

    private StandaloneDrawList(IGSharp_DrawList* handle)
    {
        _frame = IGSharp_GetFrameCount();
        _handle = handle; var pointer = (nint)handle;
        _lifetime = Lifetime.Own(pointer, () => IGSharp_DrawList_Destroy((IGSharp_DrawList*)pointer));
    }

    private IGSharp_DrawList* Handle
    {
        get
        {
            _lifetime.Check();
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
    public DrawList List
    {
        get { var handle = Handle; if (_frame != IGSharp_GetFrameCount()) throw new InvalidOperationException("Reset this standalone list before using it in a new frame."); return new(handle, _lifetime, frame: true); }
    }

    /// <summary>
    /// Clears the previous output after starting a new frame on the owning context.
    /// Push the clip rectangle and texture again before drawing.
    /// </summary>
    public void ResetForNewFrame() { _lifetime.EnsureUnretained(); IGSharp_DrawList_ResetForNewFrame(Handle); _lifetime.ReleaseAttachments(); _lifetime.InvalidateViews(); _frame = IGSharp_GetFrameCount(); }

    /// <summary>Releases the native list. Safe to call more than once.</summary>
    public void Dispose() => _lifetime.Dispose();
}
