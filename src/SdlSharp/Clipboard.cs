using System.Runtime.InteropServices;

using static SdlSharp.Native.Clipboard;
using static SdlSharp.Native.Common;

namespace SdlSharp;

/// <summary>
/// Provides clipboard text access.
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
}
