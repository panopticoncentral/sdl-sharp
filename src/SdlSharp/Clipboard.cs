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
            get
            {
                var text = Native.SDL_GetClipboardText();
                var result = Native.Utf8ToString(text);
                Native.SDL_free(text);
                return result;
            }
            set
            {
                var text = Native.StringToUtf8(value);
                _ = Native.CheckError(Native.SDL_SetClipboardText(text));
                Native.SDL_free(text);
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
