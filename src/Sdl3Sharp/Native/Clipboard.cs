using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Sdl3Sharp.Native.Common;

// We are intentionally exposing the P/Invoke calls so people can do low-level calls if needed
#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Sdl3Sharp.Native;

/// <summary>
/// P/Invoke bindings for SDL_clipboard.h - Clipboard handling.
/// </summary>
/// <remarks>
/// <para>SDL provides access to the system clipboard, both for reading information
/// from other processes and publishing information of its own.</para>
/// <para>This is not just text! SDL apps can access and publish data by mimetype.</para>
/// </remarks>
public static unsafe partial class Clipboard
{
    // SDL_ClipboardDataCallback is: delegate* unmanaged[Cdecl]<nuint userdata, byte* mime_type, nuint* size, void*>
    // SDL_ClipboardCleanupCallback is: delegate* unmanaged[Cdecl]<nuint userdata, void>

    /// <summary>
    /// Put UTF-8 text into the clipboard.
    /// </summary>
    /// <param name="text">The text to store in the clipboard.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>This function should only be called on the main thread.</remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetClipboardText([MarshalUsing(typeof(Utf8StringMarshaller))] string text);

    /// <summary>
    /// Get UTF-8 text from the clipboard.
    /// </summary>
    /// <returns>The clipboard text on success or an empty string on failure; call
    /// SDL_GetError() for more information. This should be freed with
    /// SDL_free() when it is no longer needed.</returns>
    /// <remarks>
    /// <para>This function returns an empty string if there is not enough memory left
    /// for a copy of the clipboard's content.</para>
    /// <para>This function should only be called on the main thread.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string SDL_GetClipboardText();

    /// <summary>
    /// Query whether the clipboard exists and contains a non-empty text string.
    /// </summary>
    /// <returns>true if the clipboard has text, or false if it does not.</returns>
    /// <remarks>This function should only be called on the main thread.</remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasClipboardText();

    /// <summary>
    /// Put UTF-8 text into the primary selection.
    /// </summary>
    /// <param name="text">The text to store in the primary selection.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>
    /// <para>The primary selection is a clipboard concept used on X11 and Wayland where
    /// highlighted text is automatically available for pasting.</para>
    /// <para>This function should only be called on the main thread.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetPrimarySelectionText([MarshalUsing(typeof(Utf8StringMarshaller))] string text);

    /// <summary>
    /// Get UTF-8 text from the primary selection.
    /// </summary>
    /// <returns>The primary selection text on success or an empty string on
    /// failure; call SDL_GetError() for more information. This should be
    /// freed with SDL_free() when it is no longer needed.</returns>
    /// <remarks>
    /// <para>This function returns an empty string if there is not enough memory left
    /// for a copy of the primary selection's content.</para>
    /// <para>This function should only be called on the main thread.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(Utf8StringMarshaller))]
    public static partial string SDL_GetPrimarySelectionText();

    /// <summary>
    /// Query whether the primary selection exists and contains a non-empty text string.
    /// </summary>
    /// <returns>true if the primary selection has text, or false if it does not.</returns>
    /// <remarks>This function should only be called on the main thread.</remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasPrimarySelectionText();

    /// <summary>
    /// Offer clipboard data to the OS.
    /// </summary>
    /// <param name="callback">A function pointer to the function that provides the clipboard data.</param>
    /// <param name="cleanup">A function pointer to the function that cleans up the clipboard data.</param>
    /// <param name="userdata">An opaque pointer that will be forwarded to the callbacks.</param>
    /// <param name="mime_types">A list of mime-types that are being offered. SDL copies the given list.</param>
    /// <param name="num_mime_types">The number of mime-types in the mime_types list.</param>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>
    /// <para>Tell the operating system that the application is offering clipboard data
    /// for each of the provided mime-types. Once another application requests the
    /// data the callback function will be called, allowing it to generate and
    /// respond with the data for the requested mime-type.</para>
    /// <para>The size of text data does not include any terminator, and the text does
    /// not need to be null-terminated (e.g., you can directly copy a portion of a
    /// document).</para>
    /// <para>This function should only be called on the main thread.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetClipboardData(
        delegate* unmanaged[Cdecl]<nuint, byte*, nuint*, void*> callback,
        delegate* unmanaged[Cdecl]<nuint, void> cleanup,
        nuint userdata,
        byte** mime_types,
        nuint num_mime_types);

    /// <summary>
    /// Clear the clipboard data.
    /// </summary>
    /// <returns>true on success or false on failure; call SDL_GetError() for more information.</returns>
    /// <remarks>This function should only be called on the main thread.</remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ClearClipboardData();

    /// <summary>
    /// Get the data from the clipboard for a given mime type.
    /// </summary>
    /// <param name="mime_type">The mime type to read from the clipboard.</param>
    /// <param name="size">A pointer filled in with the length of the returned data.</param>
    /// <returns>The retrieved data buffer or NULL on failure; call SDL_GetError()
    /// for more information. This should be freed with SDL_free() when it
    /// is no longer needed.</returns>
    /// <remarks>
    /// <para>The size of text data does not include the terminator, but the text is
    /// guaranteed to be null-terminated.</para>
    /// <para>This function should only be called on the main thread.</para>
    /// </remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* SDL_GetClipboardData([MarshalUsing(typeof(Utf8StringMarshaller))] string mime_type, nuint* size);

    /// <summary>
    /// Query whether there is data in the clipboard for the provided mime type.
    /// </summary>
    /// <param name="mime_type">The mime type to check for data.</param>
    /// <returns>true if data exists in the clipboard for the provided mime type,
    /// false if it does not.</returns>
    /// <remarks>This function should only be called on the main thread.</remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasClipboardData([MarshalUsing(typeof(Utf8StringMarshaller))] string mime_type);

    /// <summary>
    /// Retrieve the list of mime types available in the clipboard.
    /// </summary>
    /// <param name="num_mime_types">A pointer filled with the number of mime types, may be NULL.</param>
    /// <returns>A null-terminated array of strings with mime types, or NULL on
    /// failure; call SDL_GetError() for more information. This should be
    /// freed with SDL_free() when it is no longer needed.</returns>
    /// <remarks>This function should only be called on the main thread.</remarks>
    [LibraryImport(Sdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte** SDL_GetClipboardMimeTypes(nuint* num_mime_types);
}
