using static SdlSharp.Native.Common;
using static SdlSharp.Native.MessageBox;

namespace SdlSharp.Graphics;

/// <summary>
/// Provides simple modal message box functionality.
/// </summary>
public static unsafe class MessageBox
{
    /// <summary>
    /// Shows a simple modal message box.
    /// </summary>
    /// <param name="type">The type of message box (error, warning, information).</param>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message text.</param>
    /// <param name="parent">Optional parent window, or null for no parent.</param>
    public static void Show(MessageBoxType type, string title, string message, Window? parent = null)
    {
        Native.SDL_Window* windowPtr = parent != null ? parent.Handle : null;
        Check(SDL_ShowSimpleMessageBox(
            (Native.SDL_MessageBoxFlags)type,
            ToUtf8(title),
            ToUtf8(message),
            windowPtr));
    }
}
