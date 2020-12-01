namespace SdlSharp
{
    /// <summary>
    /// A button in a message box.
    /// </summary>
    public readonly struct MessageBoxButton
    {
        /// <summary>
        /// The flags.
        /// </summary>
        public MessageBoxButtonOptions Flags { get; }

        /// <summary>
        /// The ID of the button.
        /// </summary>
        public int ButtonId { get; }

        /// <summary>
        /// The text of the button.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Constructs a new message box button.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="buttonId">The ID of the button.</param>
        /// <param name="text">The text of the button.</param>
        public MessageBoxButton(MessageBoxButtonOptions flags, int buttonId, string text)
        {
            Flags = flags;
            ButtonId = buttonId;
            Text = text;
        }
    }
}
