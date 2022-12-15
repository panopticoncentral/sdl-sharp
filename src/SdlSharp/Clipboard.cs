using static System.Net.Mime.MediaTypeNames;

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
            get => Native.Utf8ToStringAndFree(Native.SDL_GetClipboardText());
            set
            {
                fixed (byte* ptr = Native.StringToUtf8(value))
                {
                    _ = Native.CheckError(Native.SDL_SetClipboardText(ptr));
                }
            }
        }

        /// <summary>
        /// An event that is raised when the clipboard has been updated.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? Updated;

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch ((Native.SDL_EventType)e.type)
            {
                case Native.SDL_EventType.SDL_CLIPBOARDUPDATE:
                    Updated?.Invoke(null, new SdlEventArgs(e.common));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
