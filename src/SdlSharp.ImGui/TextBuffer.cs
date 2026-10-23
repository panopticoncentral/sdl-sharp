using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;
using System.Text;
namespace SdlSharp.ImGui;

/// <summary>Reusable, disposable UTF-8 editing storage for ImGui.InputText and its multiline and hint variants.</summary>
public sealed unsafe class TextBuffer : IDisposable
{
    private readonly BufferHandle _buffer;
    private bool _editing;
    private ExceptionDispatchInfo? _exception;
    internal InputTextCallback? Callback;
    internal void Check() => ObjectDisposedException.ThrowIf(_buffer.IsClosed, this);
    internal byte* Ptr => (byte*)_buffer.DangerousGetHandle();
    internal int Size { get; private set; }
    /// <summary>Gets or replaces the text. During editing, modify text through InputTextCallbackData instead.</summary>
    public string Text
    {
        get { Check(); using var lease = new NativeCallLease(_buffer); return ReadString(); }
        set
        {
            Check(); CheckNotEditing(); using var lease = new NativeCallLease(_buffer); Validate(value);
            var needed = checked(Encoding.UTF8.GetByteCount(value) + 1);
            if (needed > Size) Resize(needed);
            var written = Encoding.UTF8.GetBytes(value, new Span<byte>(Ptr, Size)); Ptr[written] = 0;
        }
    }
    /// <summary>Current allocated UTF-8 capacity including the terminator.</summary>
    public int Capacity { get { Check(); return Size; } }
    /// <summary>Creates a reusable buffer. Capacity grows automatically during editing.</summary>
    public TextBuffer(string value = "", int initialSize = 256)
    {
        Validate(value); ArgumentOutOfRangeException.ThrowIfNegativeOrZero(initialSize);
        Size = Math.Max(initialSize, checked(Encoding.UTF8.GetByteCount(value) + 1));
        _buffer = new(Size);
        var written = Encoding.UTF8.GetBytes(value, new Span<byte>(Ptr, Size)); Ptr[written] = 0;
    }
    private static void Validate(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value.Contains('\0')) throw new ArgumentException("Text cannot contain NUL.", nameof(value));
    }
    private void CheckNotEditing() { if (_editing) throw new InvalidOperationException("The buffer is currently in an InputText call."); }
    internal void BeginEdit(InputTextCallback? callback) { Check(); CheckNotEditing(); _editing = true; Callback = callback; }
    internal void EndEdit() { _editing = false; Callback = null; }
    internal void Resize(int size) { Check(); if (size == Size) return; _buffer.Resize(size); Size = size; }
    internal string ReadString()
    {
        Check(); var bytes = new ReadOnlySpan<byte>(Ptr, Size); var length = bytes.IndexOf((byte)0);
        if (length < 0) throw new InvalidOperationException("The native text callback removed the NUL terminator.");
        return Encoding.UTF8.GetString(bytes[..length]);
    }
    internal void Capture(Exception exception) => _exception ??= ExceptionDispatchInfo.Capture(exception);
    internal void ThrowIfFailed() { var failure = _exception; _exception = null; failure?.Throw(); }
    /// <summary>Releases the UTF-8 buffer. Cannot run during an active editing callback.</summary>
    public void Dispose() { CheckNotEditing(); _buffer.Dispose(); }
    private sealed class BufferHandle : SafeHandle
    {
        internal BufferHandle(int size) : base(0, true) => SetHandle(Marshal.AllocHGlobal(size));
        public override bool IsInvalid => handle == 0;
        internal void Resize(int size) => SetHandle(Marshal.ReAllocHGlobal(handle, size));
        protected override bool ReleaseHandle() { Marshal.FreeHGlobal(handle); handle = 0; return true; }
    }
}
