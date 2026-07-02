using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Manually clips large lists of items to render only the ones currently visible.
/// Typical use:
/// <code>
/// using var clipper = new ListClipper();
/// clipper.Begin(items.Count);
/// while (clipper.Step())
///     for (int i = clipper.DisplayStart; i &lt; clipper.DisplayEnd; i++)
///         ImGui.Text(items[i]);
/// </code>
/// </summary>
public sealed unsafe class ListClipper : IDisposable
{
    private IGSharp_ListClipper* _handle;

    /// <summary>Creates a new list clipper.</summary>
    public ListClipper()
    {
        _handle = IGSharp_ListClipper_New();
    }

    /// <summary>
    /// Starts clipping. <paramref name="itemsCount"/> is the total number of items.
    /// <paramref name="itemsHeight"/> is the per-item height; pass -1 to let ImGui auto-measure
    /// after the first visible item.
    /// </summary>
    public void Begin(int itemsCount, float itemsHeight = -1f)
    {
        ThrowIfDisposed();
        IGSharp_ListClipper_Begin(_handle, itemsCount, itemsHeight);
    }

    /// <summary>Ends clipping (called automatically when stepping finishes, but safe to call explicitly).</summary>
    public void End()
    {
        ThrowIfDisposed();
        IGSharp_ListClipper_End(_handle);
    }

    /// <summary>Advances to the next visible range. Returns false when done.</summary>
    public bool Step()
    {
        ThrowIfDisposed();
        return IGSharp_ListClipper_Step(_handle);
    }

    /// <summary>Forces a range of items to be included in the display range (useful for ensuring a selection stays visible).</summary>
    public void IncludeItemsByIndex(int begin, int end)
    {
        ThrowIfDisposed();
        IGSharp_ListClipper_IncludeItemsByIndex(_handle, begin, end);
    }

    /// <summary>Seeks the internal cursor to the position where <paramref name="itemIndex"/> would be.</summary>
    public void SeekCursorForItem(int itemIndex)
    {
        ThrowIfDisposed();
        IGSharp_ListClipper_SeekCursorForItem(_handle, itemIndex);
    }

    /// <summary>First item index to display (inclusive) in the current step.</summary>
    public int DisplayStart
    {
        get
        {
            ThrowIfDisposed();
            return IGSharp_ListClipper_GetDisplayStart(_handle);
        }
    }

    /// <summary>Last item index to display (exclusive) in the current step.</summary>
    public int DisplayEnd
    {
        get
        {
            ThrowIfDisposed();
            return IGSharp_ListClipper_GetDisplayEnd(_handle);
        }
    }

    /// <summary>Releases the unmanaged list clipper.</summary>
    public void Dispose()
    {
        if (_handle != null)
        {
            IGSharp_ListClipper_Delete(_handle);
            _handle = null;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_handle == null) throw new ObjectDisposedException(nameof(ListClipper));
    }
}
