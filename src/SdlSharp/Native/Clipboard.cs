using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace SdlSharp.Native;

// Deferred: SDL_SetClipboardData, SDL_ClearClipboardData, SDL_GetClipboardData,
// SDL_HasClipboardData, SDL_GetClipboardMimeTypes (custom MIME type clipboard —
// complex callback-based API, rarely needed). Only wrapping text clipboard operations.
// Also deferred: SDL_SetPrimarySelectionText, SDL_GetPrimarySelectionText,
// SDL_HasPrimarySelectionText (X11 primary selection — niche, platform-specific).

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
}
