using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for text editing.
    /// </summary>
    public sealed class TextEditedEventArgs : SdlEventArgs
    {
        private readonly uint _windowId;

        /// <summary>
        /// The window that had focus, if any.
        /// </summary>
        public Window Window => Window.Get(_windowId);

        /// <summary>
        /// The text that was edited.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// The start of the edit.
        /// </summary>
        public int Start { get; }

        /// <summary>
        /// The length of the edit.
        /// </summary>
        public int Length { get; }

        internal unsafe TextEditedEventArgs(Native.SDL_TextEditingEvent textEdit) : base(textEdit.timestamp)
        {
            _windowId = textEdit.windowID;
            Text = Native.Utf8ToString(textEdit.text)!;
            Start = textEdit.start;
            Length = textEdit.length;
        }

        internal unsafe TextEditedEventArgs(Native.SDL_TextEditingExtEvent textEdit) : base(textEdit.timestamp)
        {
            _windowId = textEdit.windowID;
            Text = Native.Utf8ToString(textEdit.text)!;
            Native.SDL_free(textEdit.text);
            Start = textEdit.start;
            Length = textEdit.length;
        }
    }
}
