using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native;

namespace SdlSharp.ImGui;

/// <summary>
/// Helper for text filtering. Parses expressions like <c>"aaaaa[,bbbbb][,ccccc]"</c>
/// where a leading <c>-</c> excludes matches. Typical use:
/// <code>
/// using var filter = new TextFilter();
/// filter.Draw();
/// foreach (var line in lines)
///     if (filter.PassFilter(line))
///         ImGui.Text(line);
/// </code>
/// </summary>
public sealed unsafe class TextFilter : IDisposable
{
    private IGSharp_TextFilter* _handle;
    private string _text;

    /// <summary>Creates a new text filter, optionally pre-populated with a filter expression.</summary>
    public TextFilter(string? defaultFilter = null)
    {
        _text = defaultFilter ?? string.Empty;
        _handle = IGSharp_TextFilter_New(ToUtf8(_text));
    }

    /// <summary>The current filter expression.</summary>
    public string Text
    {
        get { ThrowIfDisposed(); return _text; }
        set
        {
            ThrowIfDisposed();
            value ??= string.Empty;
            if (value == _text) return;
            IGSharp_TextFilter_Delete(_handle);
            _text = value;
            _handle = IGSharp_TextFilter_New(ToUtf8(_text));
        }
    }

    /// <summary>
    /// Draws the filter input box. Returns true when the filter text changed.
    /// <paramref name="width"/> of 0 uses the default width.
    /// </summary>
    public bool Draw(string label = "Filter (inc,-exc)", float width = 0f, InputTextFlags flags = InputTextFlags.None)
    {
        ThrowIfDisposed();
        if (width != 0) ImGui.SetNextItemWidth(width);
        var text = _text;
        if (!ImGui.InputText(label, ref text, flags)) return false;
        Text = text;
        return true;
    }

    /// <summary>Returns true if <paramref name="text"/> passes the current filter.</summary>
    public bool PassFilter(string text)
    {
        ThrowIfDisposed();
        return IGSharp_TextFilter_PassFilter(_handle, ToUtf8(text), default);
    }

    /// <summary>Rebuilds the internal filter ranges (call after mutating the filter text out-of-band).</summary>
    public void Build()
    {
        ThrowIfDisposed();
        IGSharp_TextFilter_Build(_handle);
    }

    /// <summary>Clears the filter text.</summary>
    public void Clear()
    {
        ThrowIfDisposed();
        Text = string.Empty;
    }

    /// <summary>True if the filter is non-empty (i.e. filtering is happening).</summary>
    public bool IsActive
    {
        get
        {
            ThrowIfDisposed();
            return IGSharp_TextFilter_IsActive(_handle);
        }
    }

    /// <summary>Releases the unmanaged text filter.</summary>
    public void Dispose()
    {
        if (_handle != null)
        {
            IGSharp_TextFilter_Delete(_handle);
            _handle = null;
            _text = string.Empty;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_handle == null) throw new ObjectDisposedException(nameof(TextFilter));
    }
}
