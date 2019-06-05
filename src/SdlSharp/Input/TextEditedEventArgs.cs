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

        internal unsafe TextEditedEventArgs(Native.SDL_TextEditingEvent textEdit) : base(textEdit.Timestamp)
        {
            _windowId = textEdit.WindowId;
            Text = textEdit.Text;
            Start = textEdit.Start;
            Length = textEdit.Length;
        }
    }
}
