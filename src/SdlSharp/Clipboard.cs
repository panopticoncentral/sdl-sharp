using System;

namespace SdlSharp
{
    /// <summary>
    /// The system clipboard.
    /// </summary>
    public static class Clipboard
    {
        /// <summary>
        /// Whether the clipboard has text on it.
        /// </summary>
        public static bool HasText => Native.SDL_HasClipboardText();

        /// <summary>
        /// The text on the clipboard.
        /// </summary>
        public static unsafe string? Text
        {
            get => Native.SDL_GetClipboardText().ToString();
            set
            {
                using var utf8Value = Utf8String.ToUtf8String(value);
                _ = Native.CheckError(Native.SDL_SetClipboardText(utf8Value));
            }
        }

        /// <summary>
        /// An event that is raised when the clipboard has been updated.
        /// </summary>
        public static EventHandler<SdlEventArgs>? Updated;

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch (e.Type)
            {
                case Native.SDL_EventType.ClipboardUpdate:
                    Updated?.Invoke(null, new SdlEventArgs(e.Common));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
