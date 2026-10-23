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
    private readonly Lifetime _lifetime;

    /// <summary>Creates a new text filter, optionally pre-populated with a filter expression.</summary>
    public TextFilter(string? defaultFilter = null)
    {
        _text = defaultFilter ?? string.Empty;
        ValidateText(_text);
        _handle = IGSharp_TextFilter_Create(ToUtf8(_text)); var pointer = (nint)_handle;
        _lifetime = Lifetime.Own(pointer, () => IGSharp_TextFilter_Destroy((IGSharp_TextFilter*)pointer), threadIndependent: true);
    }

    /// <summary>The current filter expression.</summary>
    public string Text
    {
        get { ThrowIfDisposed(); using var nativeLease = _lifetime.Borrow(); return _text; }
        set
        {
            ThrowIfDisposed(); using var nativeLease = _lifetime.Borrow();
            value ??= string.Empty;
            if (value == _text) return;
            ValidateText(value);
            var buffer = new Span<byte>(IGSharp_TextFilter_GetInputBuf(_handle), IGSharp_TextFilter_GetInputBufSize(_handle));
            buffer.Clear(); System.Text.Encoding.UTF8.GetBytes(value, buffer);
            _text = value; IGSharp_TextFilter_Build(_handle);
        }
    }

    /// <summary>
    /// Draws the filter input box. Returns true when the filter text changed.
    /// <paramref name="width"/> of 0 uses the default width.
    /// </summary>
    public bool Draw(string label = "Filter (inc,-exc)", float width = 0f, InputTextFlags flags = InputTextFlags.None)
    {
        ThrowIfDisposed(); using var nativeLease = _lifetime.Borrow();
        using var lease = _lifetime.Retain();
        var buffer = new Span<byte>(IGSharp_TextFilter_GetInputBuf(_handle), IGSharp_TextFilter_GetInputBufSize(_handle));
        if (width != 0) ImGui.SetNextItemWidth(width);
        var result = ImGui.InputText(label, buffer, flags);
        _text = System.Text.Encoding.UTF8.GetString(buffer[..buffer.IndexOf((byte)0)]);
        IGSharp_TextFilter_Build(_handle);
        return result;
    }

    /// <summary>Draws the filter with a placeholder hint, retaining the native 255-byte UTF-8 limit.</summary>
    public bool DrawWithHint(string label, string hint, float width = 0, InputTextFlags flags = InputTextFlags.None)
    {
        ThrowIfDisposed(); using var nativeLease = _lifetime.Borrow();
        using var lease = _lifetime.Retain();
        var buffer = new Span<byte>(IGSharp_TextFilter_GetInputBuf(_handle), IGSharp_TextFilter_GetInputBufSize(_handle));
        if (width != 0) ImGui.SetNextItemWidth(width);
        var result = ImGui.InputTextWithHint(label, hint, buffer, flags);
        _text = System.Text.Encoding.UTF8.GetString(buffer[..buffer.IndexOf((byte)0)]);
        IGSharp_TextFilter_Build(_handle); return result;
    }

    /// <summary>Returns true if <paramref name="text"/> passes the current filter.</summary>
    public bool PassFilter(string text)
    {
        ThrowIfDisposed(); using var nativeLease = _lifetime.Borrow();
        return IGSharp_TextFilter_PassFilter(_handle, ToUtf8Range(text));
    }

    /// <summary>Rebuilds the internal filter ranges (call after mutating the filter text out-of-band).</summary>
    public void Build()
    {
        ThrowIfDisposed(); using var nativeLease = _lifetime.Borrow();
        IGSharp_TextFilter_Build(_handle);
    }

    /// <summary>Clears the filter text.</summary>
    public void Clear()
    {
        ThrowIfDisposed(); using var nativeLease = _lifetime.Borrow();
        Text = string.Empty;
    }

    /// <summary>True if the filter is non-empty (i.e. filtering is happening).</summary>
    public bool IsActive
    {
        get
        {
            ThrowIfDisposed(); using var nativeLease = _lifetime.Borrow();
            return IGSharp_TextFilter_IsActive(_handle);
        }
    }

    /// <summary>Releases the unmanaged text filter.</summary>
    public void Dispose()
    {
        if (_handle != null)
        {
            _lifetime.Dispose();
            _handle = null;
            _text = string.Empty;
        }
    }

    private static void ValidateText(string text)
    {
        if (text.Contains('\0') || System.Text.Encoding.UTF8.GetByteCount(text) >= 256)
            throw new ArgumentException("A text filter must fit in 255 UTF-8 bytes and contain no NUL.", nameof(text));
    }
    private void ThrowIfDisposed()
    {
        _lifetime.Check();
    }
}
