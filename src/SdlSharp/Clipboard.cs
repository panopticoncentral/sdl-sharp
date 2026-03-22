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
        get => Marshal.PtrToStringUTF8((nint)SDL_GetClipboardText());
        set => Check(SDL_SetClipboardText(ToUtf8(value)));
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

    // Prevent the native delegates and the ClipboardDataState from being GC'd
    // while SDL holds references to them.
    private static GCHandle _clipboardDataHandle;

    private sealed class ClipboardDataState
    {
        public required ClipboardDataProvider Provider;
        public GCHandle LastResultPin;
    }

    /// <summary>
    /// Offer clipboard data to the OS. When another application requests the data,
    /// <paramref name="provider"/> will be called with the requested MIME type.
    /// </summary>
    /// <param name="provider">A delegate called when clipboard data is requested.</param>
    /// <param name="mimeTypes">The MIME types being offered.</param>
    public static void SetData(ClipboardDataProvider provider, params string[] mimeTypes)
    {
        // Free any previous state
        if (_clipboardDataHandle.IsAllocated)
            _clipboardDataHandle.Free();

        var state = new ClipboardDataState { Provider = provider };
        _clipboardDataHandle = GCHandle.Alloc(state);

        // Convert MIME types to null-terminated UTF-8 byte arrays
        var mimeTypeBytes = new byte[mimeTypes.Length][];
        for (var i = 0; i < mimeTypes.Length; i++)
            mimeTypeBytes[i] = ToUtf8(mimeTypes[i])!;

        // Pin all arrays and build pointer array
        var pins = new GCHandle[mimeTypes.Length];
        var ptrs = stackalloc byte*[mimeTypes.Length];
        try
        {
            for (var i = 0; i < mimeTypes.Length; i++)
            {
                pins[i] = GCHandle.Alloc(mimeTypeBytes[i], GCHandleType.Pinned);
                ptrs[i] = (byte*)pins[i].AddrOfPinnedObject();
            }

            Check(SDL_SetClipboardData(
                _dataCallback,
                _cleanupCallback,
                (void*)(nint)_clipboardDataHandle,
                ptrs,
                (nuint)mimeTypes.Length));
        }
        finally
        {
            foreach (var pin in pins)
                if (pin.IsAllocated) pin.Free();
        }
    }

    private static readonly SDL_ClipboardDataCallback _dataCallback = DataCallbackImpl;
    private static readonly SDL_ClipboardCleanupCallback _cleanupCallback = CleanupCallbackImpl;

    private static void* DataCallbackImpl(void* userdata, byte* mimeType, nuint* size)
    {
        var state = (ClipboardDataState)GCHandle.FromIntPtr((nint)userdata).Target!;
        var mimeTypeString = mimeType != null ? Marshal.PtrToStringUTF8((nint)mimeType) : null;
        var data = state.Provider(mimeTypeString);

        // Free previous pinned result
        if (state.LastResultPin.IsAllocated)
            state.LastResultPin.Free();

        if (data.IsEmpty)
        {
            *size = 0;
            return null;
        }

        // Copy to a pinned buffer — SDL requires the data to remain valid
        var buffer = data.ToArray();
        state.LastResultPin = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        *size = (nuint)buffer.Length;
        return (void*)state.LastResultPin.AddrOfPinnedObject();
    }

    private static void CleanupCallbackImpl(void* userdata)
    {
        var handle = GCHandle.FromIntPtr((nint)userdata);
        var state = (ClipboardDataState)handle.Target!;
        if (state.LastResultPin.IsAllocated)
            state.LastResultPin.Free();
        handle.Free();
        _clipboardDataHandle = default;
    }

    /// <summary>
    /// Clears the clipboard data.
    /// </summary>
    public static void ClearData() => Check(SDL_ClearClipboardData());

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
            var result = new byte[size];
            new ReadOnlySpan<byte>(ptr, (int)size).CopyTo(result);
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
}
