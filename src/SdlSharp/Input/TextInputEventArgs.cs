using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for text input.
    /// </summary>
    public sealed class TextInputEventArgs : SdlEventArgs
    {
        private readonly uint _windowId;

        /// <summary>
        /// The window that had focus, if any.
        /// </summary>
        public Window Window => Window.Get(_windowId);

        /// <summary>
        /// The text.
        /// </summary>
        public string Text { get; }

        internal unsafe TextInputEventArgs(Native.SDL_TextInputEvent textEdit) : base(textEdit.timestamp)
        {
            _windowId = textEdit.windowID;
            Text = Native.Utf8ToString(textEdit.text)!;
        }
    }
}
