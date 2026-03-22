using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Skipped: SDL_SetPrimarySelectionText, SDL_GetPrimarySelectionText,
// SDL_HasPrimarySelectionText (X11/Wayland primary selection — niche, platform-specific).

/// <summary>
/// Native bindings for SDL_clipboard.h — clipboard access.
/// </summary>
[SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible")]
public static partial class Clipboard
{
    /// <summary>Put UTF-8 text into the clipboard.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetClipboardText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_SetClipboardText(ReadOnlySpan<byte> text);

    /// <summary>Get UTF-8 text from the clipboard. Returns SDL-owned string.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetClipboardText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* SDL_GetClipboardText();

    /// <summary>Query whether the clipboard exists and contains a non-empty text string.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasClipboardText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasClipboardText();

    /// <summary>Callback that provides clipboard data for a requested MIME type.</summary>
    /// <remarks>Called with null mime_type when clipboard is cleared or new data is set.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void* SDL_ClipboardDataCallback(void* userdata, byte* mime_type, nuint* size);

    /// <summary>Callback that cleans up clipboard data when the clipboard is cleared or new data is set.</summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void SDL_ClipboardCleanupCallback(void* userdata);

    /// <summary>Offer clipboard data to the OS via callbacks.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_SetClipboardData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static unsafe partial bool SDL_SetClipboardData(
        SDL_ClipboardDataCallback callback,
        SDL_ClipboardCleanupCallback? cleanup,
        void* userdata,
        byte** mime_types,
        nuint num_mime_types);

    /// <summary>Clear the clipboard data.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_ClearClipboardData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_ClearClipboardData();

    /// <summary>Get the data from the clipboard for a given MIME type.</summary>
    /// <remarks>The returned data must be freed with SDL_free.</remarks>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetClipboardData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void* SDL_GetClipboardData(ReadOnlySpan<byte> mime_type, out nuint size);

    /// <summary>Query whether there is data in the clipboard for the provided MIME type.</summary>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_HasClipboardData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool SDL_HasClipboardData(ReadOnlySpan<byte> mime_type);

    /// <summary>Retrieve the list of MIME types available in the clipboard.</summary>
    /// <remarks>The returned array must be freed with SDL_free.</remarks>
    [LibraryImport(Common.Sdl3, EntryPoint = "SDL_GetClipboardMimeTypes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte** SDL_GetClipboardMimeTypes(out nuint num_mime_types);
}
