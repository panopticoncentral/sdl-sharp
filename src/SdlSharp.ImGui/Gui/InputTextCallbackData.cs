using static SdlSharp.Native.Common;
using static SdlSharp.ImGui.Native.ImGui;

namespace SdlSharp.Gui;

/// <summary>
/// Data surface for an InputText callback. Inspect <see cref="EventFlag"/> to
/// determine which event is firing, and use the mutation helpers (<see cref="InsertChars"/>,
/// <see cref="DeleteChars"/>, <see cref="SelectAll"/>, etc.) to react.
/// </summary>
public readonly unsafe struct InputTextCallbackData
{
    internal readonly void* Handle;

    internal InputTextCallbackData(void* handle) => Handle = handle;

    /// <summary>Which callback event fired (one of <c>InputTextFlags.Callback*</c>).</summary>
    public InputTextFlags EventFlag => (InputTextFlags)IGSharp_InputTextCallbackData_GetEventFlag(Handle);

    /// <summary>The original flags passed to InputText.</summary>
    public InputTextFlags Flags => (InputTextFlags)IGSharp_InputTextCallbackData_GetFlags(Handle);

    /// <summary>Key identifying the history/completion event (CallbackHistory/Completion).</summary>
    public Key EventKey => (Key)IGSharp_InputTextCallbackData_GetEventKey(Handle);

    /// <summary>Character being filtered (CallbackCharFilter). Set to 0 to discard the character.</summary>
    public ushort EventChar
    {
        get => IGSharp_InputTextCallbackData_GetEventChar(Handle);
        set => IGSharp_InputTextCallbackData_SetEventChar(Handle, value);
    }

    /// <summary>True on the first edit after the widget becomes active.</summary>
    public bool EventActivated => IGSharp_InputTextCallbackData_GetEventActivated(Handle);

    /// <summary>The current text buffer (UTF-8), null-terminated. Length is <see cref="BufTextLen"/>.</summary>
    public Span<byte> Buf
    {
        get
        {
            var ptr = IGSharp_InputTextCallbackData_GetBuf(Handle);
            var size = IGSharp_InputTextCallbackData_GetBufSize(Handle);
            return ptr == null ? default : new Span<byte>(ptr, size);
        }
    }

    /// <summary>Current text length in bytes (not including the null terminator).</summary>
    public int BufTextLen
    {
        get => IGSharp_InputTextCallbackData_GetBufTextLen(Handle);
        set => IGSharp_InputTextCallbackData_SetBufTextLen(Handle, value);
    }

    /// <summary>Buffer capacity in bytes.</summary>
    public int BufSize => IGSharp_InputTextCallbackData_GetBufSize(Handle);

    /// <summary>Set to true after mutating <see cref="Buf"/> directly so ImGui picks up the change.</summary>
    public bool BufDirty
    {
        get => IGSharp_InputTextCallbackData_GetBufDirty(Handle);
        set => IGSharp_InputTextCallbackData_SetBufDirty(Handle, value);
    }

    /// <summary>Cursor position in bytes.</summary>
    public int CursorPos
    {
        get => IGSharp_InputTextCallbackData_GetCursorPos(Handle);
        set => IGSharp_InputTextCallbackData_SetCursorPos(Handle, value);
    }

    /// <summary>Selection start position in bytes.</summary>
    public int SelectionStart
    {
        get => IGSharp_InputTextCallbackData_GetSelectionStart(Handle);
        set => IGSharp_InputTextCallbackData_SetSelectionStart(Handle, value);
    }

    /// <summary>Selection end position in bytes (exclusive).</summary>
    public int SelectionEnd
    {
        get => IGSharp_InputTextCallbackData_GetSelectionEnd(Handle);
        set => IGSharp_InputTextCallbackData_SetSelectionEnd(Handle, value);
    }

    /// <summary>Deletes <paramref name="bytesCount"/> bytes starting at <paramref name="pos"/>.</summary>
    public void DeleteChars(int pos, int bytesCount)
        => IGSharp_InputTextCallbackData_DeleteChars(Handle, pos, bytesCount);

    /// <summary>Inserts UTF-8 bytes at <paramref name="pos"/>.</summary>
    public void InsertChars(int pos, ReadOnlySpan<byte> text)
        => IGSharp_InputTextCallbackData_InsertChars(Handle, pos, text, null);

    /// <summary>Inserts a managed string (encoded as UTF-8) at <paramref name="pos"/>.</summary>
    public void InsertChars(int pos, string text)
        => IGSharp_InputTextCallbackData_InsertChars(Handle, pos, ToUtf8(text), null);

    /// <summary>Selects the entire buffer.</summary>
    public void SelectAll() => IGSharp_InputTextCallbackData_SelectAll(Handle);

    /// <summary>Clears the current selection.</summary>
    public void ClearSelection() => IGSharp_InputTextCallbackData_ClearSelection(Handle);

    /// <summary>True if there is a non-empty selection.</summary>
    public bool HasSelection() => IGSharp_InputTextCallbackData_HasSelection(Handle);

    /// <summary>
    /// Replaces the buffer pointer (used during a <see cref="InputTextFlags.CallbackResize"/> event
    /// to point ImGui at a freshly-grown buffer). The new buffer must remain valid until the
    /// callback returns.
    /// </summary>
    public void SetBuf(byte* newBuf) => IGSharp_InputTextCallbackData_SetBuf(Handle, newBuf);

    /// <summary>Sets the buffer capacity in bytes.</summary>
    public void SetBufSize(int size) => IGSharp_InputTextCallbackData_SetBufSize(Handle, size);

    /// <summary>Atomic helper: replace both buffer pointer and capacity, used during a resize callback.</summary>
    public void ResizeBuf(byte* newBuf, int newSize) => IGSharp_InputTextCallbackData_ResizeBuf(Handle, newBuf, newSize);
}

/// <summary>
/// Delegate signature for InputText callbacks. Return a non-zero value to signal
/// special handling (consult ImGui docs; most callbacks return 0).
/// </summary>
public delegate int InputTextCallback(InputTextCallbackData data);
