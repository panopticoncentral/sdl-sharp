using System.Runtime.InteropServices;
using System.Text;

using static Sdl3Sharp.Native.Clipboard;
using static Sdl3Sharp.Native.Common;
using static Sdl3Sharp.Native.StdInc;

namespace Sdl3Sharp;

/// <summary>
/// Provides access to the system clipboard for reading and writing text and data.
/// </summary>
/// <remarks>
/// <para>SDL provides access to the system clipboard, both for reading information
/// from other processes and publishing information of its own.</para>
/// <para>All clipboard operations should be called from the main thread.</para>
/// </remarks>
public static unsafe class Clipboard
{
    /// <summary>
    /// Gets or sets the UTF-8 text in the clipboard.
    /// </summary>
    /// <exception cref="SdlException">Thrown when setting the clipboard text fails.</exception>
    public static string? Text
    {
        get => !SDL_HasClipboardText() ? null : SDL_GetClipboardText();
        set => CheckErrorBool(SDL_SetClipboardText(value));
    }

    /// <summary>
    /// Gets a value indicating whether the clipboard contains non-empty text.
    /// </summary>
    public static bool HasText => SDL_HasClipboardText();

    /// <summary>
    /// Gets or sets the UTF-8 text in the primary selection.
    /// </summary>
    /// <remarks>
    /// The primary selection is a clipboard concept used on X11 and Wayland where
    /// highlighted text is automatically available for pasting. On platforms that
    /// don't support primary selection, SDL will maintain an internal copy.
    /// </remarks>
    /// <exception cref="SdlException">Thrown when setting the primary selection text fails.</exception>
    public static string PrimarySelectionText
    {
        get => SDL_GetPrimarySelectionText();
        set => CheckErrorBool(SDL_SetPrimarySelectionText(value));
    }

    /// <summary>
    /// Gets a value indicating whether the primary selection contains non-empty text.
    /// </summary>
    public static bool HasPrimarySelectionText => SDL_HasPrimarySelectionText();

    /// <summary>
    /// Clears the clipboard data.
    /// </summary>
    /// <exception cref="SdlException">Thrown when clearing the clipboard fails.</exception>
    public static void Clear()
    {
        _ = CheckErrorBool(SDL_ClearClipboardData());
    }

    /// <summary>
    /// Checks whether the clipboard contains data for the specified MIME type.
    /// </summary>
    /// <param name="mimeType">The MIME type to check for (e.g., "text/plain", "image/png").</param>
    /// <returns>true if data exists for the specified MIME type; otherwise, false.</returns>
    public static bool HasData(string mimeType)
    {
        return SDL_HasClipboardData(mimeType);
    }

    /// <summary>
    /// Gets the data from the clipboard for the specified MIME type.
    /// </summary>
    /// <param name="mimeType">The MIME type to retrieve (e.g., "text/plain", "image/png").</param>
    /// <returns>The clipboard data as a byte array, or null if no data is available.</returns>
    /// <exception cref="SdlException">Thrown when retrieving clipboard data fails.</exception>
    public static byte[]? GetData(string mimeType)
    {
        nuint size;
        var data = SDL_GetClipboardData(mimeType, &size);

        if (data is null)
        {
            return null;
        }

        try
        {
            var result = new byte[size];
            new Span<byte>(data, (int)size).CopyTo(result);
            return result;
        }
        finally
        {
            SDL_free(data);
        }
    }

    /// <summary>
    /// Gets the list of MIME types available in the clipboard.
    /// </summary>
    /// <returns>An array of MIME type strings available in the clipboard.</returns>
    /// <exception cref="SdlException">Thrown when retrieving MIME types fails.</exception>
    public static string[] GetMimeTypes()
    {
        nuint count;
        var mimeTypes = CheckErrorPointer(SDL_GetClipboardMimeTypes(&count));

        try
        {
            var result = new string[count];

            for (nuint i = 0; i < count; i++)
            {
                result[i] = Marshal.PtrToStringUTF8((nint)mimeTypes[i]) ?? string.Empty;
            }

            return result;
        }
        finally
        {
            SDL_free(mimeTypes);
        }
    }

    /// <summary>
    /// Sets clipboard data with callback-based lazy generation.
    /// </summary>
    /// <param name="mimeTypes">The MIME types being offered.</param>
    /// <param name="dataCallback">A callback that provides data when requested.
    /// The callback receives the MIME type and should return the data for that type.
    /// Return null to indicate no data is available.</param>
    /// <param name="cleanupCallback">An optional callback invoked when the clipboard is cleared or new data is set.</param>
    /// <exception cref="SdlException">Thrown when setting clipboard data fails.</exception>
    /// <remarks>
    /// <para>This method allows lazy generation of clipboard data. The data callback is only
    /// invoked when another application requests data for a specific MIME type.</para>
    /// <para>The callbacks must remain valid until the cleanup callback is invoked or the
    /// clipboard is cleared. Consider using static methods or prevent the delegates from
    /// being garbage collected.</para>
    /// </remarks>
    public static void SetData(string[] mimeTypes, Func<string?, byte[]?> dataCallback, Action? cleanupCallback = null)
    {
        var context = new ClipboardCallbackContext(dataCallback, cleanupCallback);
        var contextHandle = GCHandle.Alloc(context);

        var mimeTypeBytes = new byte*[mimeTypes.Length];
        var mimeTypeHandles = new GCHandle[mimeTypes.Length];

        try
        {
            for (var i = 0; i < mimeTypes.Length; i++)
            {
                var bytes = Encoding.UTF8.GetBytes(mimeTypes[i] + '\0');
                mimeTypeHandles[i] = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                mimeTypeBytes[i] = (byte*)mimeTypeHandles[i].AddrOfPinnedObject();
            }

            fixed (byte** mimeTypesPtr = mimeTypeBytes)
            {
                var result = SDL_SetClipboardData(
                    &ClipboardDataCallbackHandler,
                    &ClipboardCleanupCallbackHandler,
                    (nuint)GCHandle.ToIntPtr(contextHandle),
                    mimeTypesPtr,
                    (nuint)mimeTypes.Length);

                if (!result)
                {
                    contextHandle.Free();
                    throw new SdlException();
                }
            }
        }
        finally
        {
            foreach (GCHandle handle in mimeTypeHandles)
            {
                if (handle.IsAllocated)
                {
                    handle.Free();
                }
            }
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static void* ClipboardDataCallbackHandler(nuint userdata, byte* mimeType, nuint* size)
    {
        var handle = GCHandle.FromIntPtr((nint)userdata);
        var context = (ClipboardCallbackContext)handle.Target!;

        var mimeTypeString = mimeType is not null
            ? Marshal.PtrToStringUTF8((nint)mimeType)
            : null;

        var data = context.DataCallback(mimeTypeString);

        if (data is null || data.Length == 0)
        {
            *size = 0;
            return null;
        }

        // Free previous data if any
        if (context.CurrentData is not null)
        {
            Marshal.FreeHGlobal((nint)context.CurrentData);
        }

        // Allocate and copy data - must persist until next call or cleanup
        context.CurrentData = (byte*)Marshal.AllocHGlobal(data.Length);
        Marshal.Copy(data, 0, (nint)context.CurrentData, data.Length);
        *size = (nuint)data.Length;

        return context.CurrentData;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    private static void ClipboardCleanupCallbackHandler(nuint userdata)
    {
        var handle = GCHandle.FromIntPtr((nint)userdata);
        var context = (ClipboardCallbackContext)handle.Target!;

        // Free any allocated data
        if (context.CurrentData is not null)
        {
            Marshal.FreeHGlobal((nint)context.CurrentData);
            context.CurrentData = null;
        }

        // Invoke user cleanup callback
        context.CleanupCallback?.Invoke();

        // Free the GCHandle
        handle.Free();
    }

    private sealed class ClipboardCallbackContext(Func<string?, byte[]?> dataCallback, Action? cleanupCallback)
    {
        public Func<string?, byte[]?> DataCallback { get; } = dataCallback;
        public Action? CleanupCallback { get; } = cleanupCallback;
        public byte* CurrentData { get; set; }
    }
}
