using System.Runtime.InteropServices;

using static SdlSharp.Native.Clipboard;
using static SdlSharp.Native.Common;

namespace SdlSharp;

/// <summary>
/// Provides clipboard access (text and arbitrary MIME-type data).
/// </summary>
public static unsafe class Clipboard
{
    /// <summary>
    /// Gets or sets the clipboard text content.
    /// Returns null if the clipboard is empty. Setting to null clears the text.
    /// </summary>
    public static string? Text
    {
        get
        {
            var text = Check(SDL_GetClipboardText());
            try { return Marshal.PtrToStringUTF8((nint)text); }
            finally { SDL_free(text); }
        }
        set { CallbackErrors.EnsureOutsideCallback(); Check(SDL_SetClipboardText(ToUtf8(value))); }
    }

    /// <summary>
    /// Gets whether the clipboard contains non-empty text.
    /// </summary>
    public static bool HasText => SDL_HasClipboardText();

    /// <summary>
    /// A delegate that provides clipboard data for a requested MIME type.
    /// Return null to indicate no data. Called with null <paramref name="mimeType"/>
    /// when the clipboard is cleared or new data is set.
    /// </summary>
    /// <param name="mimeType">The requested MIME type, or null when clipboard is cleared.</param>
    /// <returns>The data for the requested MIME type, or null.</returns>
    public delegate ReadOnlySpan<byte> ClipboardDataProvider(string? mimeType);

    private sealed class ClipboardDataState
    {
        internal required ClipboardDataProvider Provider;
        internal readonly List<GCHandle> Pins = [];
    }

    /// <summary>Offers data for the listed MIME types until replaced or cleared. Returned buffers are copied and retained until SDL releases the offer.</summary>
    public static void SetData(ClipboardDataProvider provider, params string[] mimeTypes)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(mimeTypes);
        CallbackErrors.EnsureOutsideCallback();
        var state = new ClipboardDataState { Provider = provider };
        var handle = CallbackHandle.Alloc(state);
        var pins = new GCHandle[mimeTypes.Length];
        var addresses = new nint[mimeTypes.Length];
        try
        {
            for (var i = 0; i < mimeTypes.Length; i++)
            {
                ArgumentException.ThrowIfNullOrEmpty(mimeTypes[i]);
                pins[i] = GCHandle.Alloc(ToUtf8(mimeTypes[i])!, GCHandleType.Pinned);
                addresses[i] = pins[i].AddrOfPinnedObject();
            }
            fixed (nint* pointers = addresses)
                Check(SDL_SetClipboardData(_dataCallback, _cleanupCallback, (void*)(nint)handle, (byte**)pointers, (nuint)addresses.Length));
        }
        catch { CleanupCallbackImpl((void*)(nint)handle); throw; }
        finally { foreach (var pin in pins) if (pin.IsAllocated) pin.Free(); }
    }

    private static readonly SDL_ClipboardDataCallback _dataCallback = DataCallbackImpl;
    private static readonly SDL_ClipboardCleanupCallback _cleanupCallback = CleanupCallbackImpl;

    private static void* DataCallbackImpl(void* userdata, byte* mimeType, nuint* size)
    {
        *size = 0;
        try
        {
            using var scope = CallbackErrors.Enter();
            if (CallbackHandle.FromIntPtr((nint)userdata).Target is not ClipboardDataState state) return null;
            lock (state)
            {
                var data = state.Provider(Marshal.PtrToStringUTF8((nint)mimeType));
                if (data.IsEmpty) return null;
                var pin = GCHandle.Alloc(data.ToArray(), GCHandleType.Pinned);
                state.Pins.Add(pin);
                *size = (nuint)data.Length;
                return (void*)pin.AddrOfPinnedObject();
            }
        }
        catch (Exception error) { CallbackErrors.Capture(error); return null; }
    }

    private static void CleanupCallbackImpl(void* userdata)
    {
        var handle = CallbackHandle.FromIntPtr((nint)userdata);
        if (handle.Target is not ClipboardDataState state) return;
        handle.Free();
        lock (state)
        {
            foreach (var pin in state.Pins) pin.Free();
            state.Pins.Clear();
        }
    }

    /// <summary>
    /// Clears the clipboard data.
    /// </summary>
    public static void ClearData() { CallbackErrors.EnsureOutsideCallback(); Check(SDL_ClearClipboardData()); }

    /// <summary>
    /// Gets the clipboard data for the given MIME type.
    /// </summary>
    /// <param name="mimeType">The MIME type to request.</param>
    /// <returns>The clipboard data, or an empty array if not available.</returns>
    public static byte[] GetData(string mimeType)
    {
        var ptr = SDL_GetClipboardData(ToUtf8(mimeType), out var size);
        if (ptr == null) return [];
        try
        {
            var result = new byte[checked((int)size)];
            new ReadOnlySpan<byte>(ptr, checked((int)size)).CopyTo(result);
            return result;
        }
        finally
        {
            SDL_free(ptr);
        }
    }

    /// <summary>
    /// Gets whether the clipboard contains data for the given MIME type.
    /// </summary>
    /// <param name="mimeType">The MIME type to check.</param>
    public static bool HasData(string mimeType) => SDL_HasClipboardData(ToUtf8(mimeType));

    /// <summary>
    /// Gets the list of MIME types available in the clipboard.
    /// </summary>
    public static string[] GetMimeTypes()
    {
        var ptrs = SDL_GetClipboardMimeTypes(out var count);
        if (ptrs == null) return [];
        try
        {
            var result = new string[count];
            for (var i = 0; i < (int)count; i++)
                result[i] = Marshal.PtrToStringUTF8((nint)ptrs[i]) ?? "";
            return result;
        }
        finally
        {
            SDL_free(ptrs);
        }
    }
    /// <summary>Gets or sets the platform's primary selection text.</summary>
    public static string? PrimarySelectionText
    {
        get
        {
            var text = Check(SDL_GetPrimarySelectionText());
            try { return Marshal.PtrToStringUTF8((nint)text); }
            finally { SDL_free(text); }
        }
        set => Check(SDL_SetPrimarySelectionText(ToUtf8(value)));
    }
    /// <summary>Whether the primary selection contains text.</summary>
    public static bool HasPrimarySelectionText => SDL_HasPrimarySelectionText();
}
