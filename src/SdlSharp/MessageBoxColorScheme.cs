namespace SdlSharp
{
    /// <summary>
    /// A color scheme for a message box.
    /// </summary>
    public readonly struct MessageBoxColorScheme
    {
        /// <summary>
        /// The background color.
        /// </summary>
        public readonly MessageBoxColor BackgroundColor { get; }

        /// <summary>
        /// The text color.
        /// </summary>
        public readonly MessageBoxColor TextColor { get; }

        /// <summary>
        /// The border color.
        /// </summary>
        public readonly MessageBoxColor BorderColor { get; }

        /// <summary>
        /// The button background color.
        /// </summary>
        public readonly MessageBoxColor ButtonBackgroundColor { get; }

        /// <summary>
        /// The button selected color.
        /// </summary>
        public readonly MessageBoxColor ButtonSelectedColor { get; }

        /// <summary>
        /// Creates a message box color scheme.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        /// <param name="textColor">The text color.</param>
        /// <param name="borderColor">The border color.</param>
        /// <param name="buttonBackgroundColor">The button background color.</param>
        /// <param name="buttonSelectedColor">The button selected color.</param>
        public MessageBoxColorScheme(MessageBoxColor backgroundColor, MessageBoxColor textColor, MessageBoxColor borderColor, MessageBoxColor buttonBackgroundColor, MessageBoxColor buttonSelectedColor)
        {
            BackgroundColor = backgroundColor;
            TextColor = textColor;
            BorderColor = borderColor;
            ButtonBackgroundColor = buttonBackgroundColor;
            ButtonSelectedColor = buttonSelectedColor;
        }

        internal Native.SDL_MessageBoxColorScheme ToNative()
        {
            return new Native.SDL_MessageBoxColorScheme
            {
                background = BackgroundColor.ToNative(),
                text = TextColor.ToNative(),
                button_border = BorderColor.ToNative(),
                button_background = ButtonBackgroundColor.ToNative(),
                button_selected = ButtonSelectedColor.ToNative()
            };
        }
    }
}
