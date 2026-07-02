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

    /// <summary>Creates a new draw list splitter.</summary>
    public DrawListSplitter()
    {
        _handle = IGSharp_DrawListSplitter_Create();
    }

    /// <summary>Resets the splitter state (does not free channel memory, so it can be reused cheaply).</summary>
    public void Clear()
    {
        ThrowIfDisposed();
        IGSharp_DrawListSplitter_Clear(_handle);
    }

    /// <summary>Resets the splitter state and frees the channel memory.</summary>
    public void ClearFreeMemory()
    {
        ThrowIfDisposed();
        IGSharp_DrawListSplitter_ClearFreeMemory(_handle);
    }

    /// <summary>Splits <paramref name="drawList"/> into <paramref name="count"/> channels. Channel 0 is current after the split.</summary>
    public void Split(DrawList drawList, int count)
    {
        ThrowIfDisposed();
        IGSharp_DrawListSplitter_Split(_handle, drawList.Handle, count);
    }

    /// <summary>Merges all channels back into <paramref name="drawList"/> in channel order.</summary>
    public void Merge(DrawList drawList)
    {
        ThrowIfDisposed();
        IGSharp_DrawListSplitter_Merge(_handle, drawList.Handle);
    }

    /// <summary>Redirects subsequent drawing on <paramref name="drawList"/> to the given channel.</summary>
    public void SetCurrentChannel(DrawList drawList, int channelIndex)
    {
        ThrowIfDisposed();
        IGSharp_DrawListSplitter_SetCurrentChannel(_handle, drawList.Handle, channelIndex);
    }

    /// <summary>Releases the unmanaged splitter.</summary>
    public void Dispose()
    {
        if (_handle != null)
        {
            IGSharp_DrawListSplitter_Destroy(_handle);
            _handle = null;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_handle == null) throw new ObjectDisposedException(nameof(DrawListSplitter));
    }
}
