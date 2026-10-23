using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Splits a <see cref="DrawList"/> into multiple layers (channels) that are drawn out of order,
/// then merged back in channel order — useful for drawing backgrounds after foregrounds are known
/// (e.g. a selection highlight behind items). For simple cases prefer the convenience methods
/// <see cref="DrawList.ChannelsSplit"/> / <see cref="DrawList.ChannelsMerge"/> /
/// <see cref="DrawList.ChannelsSetCurrent"/>; use this standalone splitter to interleave splits
/// on the same draw list, or to reuse the channel storage across frames.
/// Typical use:
/// <code>
/// using var splitter = new DrawListSplitter();
/// splitter.Split(drawList, 2);
/// splitter.SetCurrentChannel(drawList, 1);
/// // ... draw foreground ...
/// splitter.SetCurrentChannel(drawList, 0);
/// // ... draw background ...
/// splitter.Merge(drawList);
/// </code>
/// </summary>
public sealed unsafe class DrawListSplitter : IDisposable
{
    private IGSharp_DrawListSplitter* _handle;
    private readonly Lifetime _lifetime;
    private nint _list;
    private int _channels;
    private int _frame;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2213", Justification = "The lease is released by lifetime cleanup and Merge/Clear.")]
    private IDisposable? _listLease;

    /// <summary>Creates a new draw list splitter.</summary>
    public DrawListSplitter()
    {
        Context.Current.CheckCurrent();
        _handle = IGSharp_DrawListSplitter_Create(); var pointer = (nint)_handle;
        _lifetime = Lifetime.Own(pointer, () => IGSharp_DrawListSplitter_Destroy((IGSharp_DrawListSplitter*)pointer));
        _lifetime.AddCleanup(ReleaseList);
    }

    /// <summary>Resets the splitter state (does not free channel memory, so it can be reused cheaply).</summary>
    public void Clear()
    {
        ThrowIfDisposed();
        IGSharp_DrawListSplitter_Clear(_handle); ReleaseList();
    }

    /// <summary>Resets the splitter state and frees the channel memory.</summary>
    public void ClearFreeMemory()
    {
        ThrowIfDisposed();
        IGSharp_DrawListSplitter_ClearFreeMemory(_handle); ReleaseList();
    }

    /// <summary>Splits <paramref name="drawList"/> into <paramref name="count"/> channels. Channel 0 is current after the split.</summary>
    public void Split(DrawList drawList, int count)
    {
        ThrowIfDisposed();
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        if (_channels != 0) throw new InvalidOperationException("Merge or clear the current split first.");
        var list = drawList.MutableHandle;
        _listLease = drawList.Lifetime.RetainFor(_lifetime); _list = (nint)list; _channels = count; _frame = IGSharp_GetFrameCount();
        IGSharp_DrawListSplitter_Split(_handle, list, count);
    }

    /// <summary>Merges all channels back into <paramref name="drawList"/> in channel order.</summary>
    public void Merge(DrawList drawList)
    {
        ThrowIfDisposed();
        CheckList(drawList); IGSharp_DrawListSplitter_Merge(_handle, drawList.MutableHandle); ReleaseList();
    }

    /// <summary>Redirects subsequent drawing on <paramref name="drawList"/> to the given channel.</summary>
    public void SetCurrentChannel(DrawList drawList, int channelIndex)
    {
        ThrowIfDisposed();
        CheckList(drawList); ArgumentOutOfRangeException.ThrowIfNegative(channelIndex); ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(channelIndex, _channels);
        IGSharp_DrawListSplitter_SetCurrentChannel(_handle, drawList.MutableHandle, channelIndex);
    }

    /// <summary>Releases the unmanaged splitter.</summary>
    public void Dispose()
    {
        if (_handle != null)
        {
            _lifetime.Dispose();
            _handle = null;
        }
    }

    private void CheckList(DrawList list)
    {
        if (_channels == 0 || (nint)list.MutableHandle != _list || IGSharp_GetFrameCount() != _frame)
            throw new InvalidOperationException("Use the draw list and frame that started this split.");
    }
    private void ReleaseList() { _listLease?.Dispose(); _listLease = null; _list = 0; _channels = 0; }

    private void ThrowIfDisposed()
    {
        _lifetime.Check();
    }
}
