using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// Keyboard related APIs.
    /// </summary>
    public static unsafe class Keyboard
    {
        /// <summary>
        /// The window that has keyboard focus, if any.
        /// </summary>
        public static Window? Focus => Window.PointerToInstance(Native.SDL_GetKeyboardFocus());

        /// <summary>
        /// The state of the keyboard.
        /// </summary>
        public static bool[] State
        {
            get
            {
                var state = Native.SDL_GetKeyboardState(out var numkeys);
                var current = new Span<byte>(state, numkeys);
                var ret = new bool[numkeys];

                for (var i = 0; i < numkeys; i++)
                {
                    ret[i] = current[i] != 0;
                }

                return ret;
            }
        }

        /// <summary>
        /// The state of the key modifiers.
        /// </summary>
        public static KeyModifier KeyModifierState
        {
            get => Native.SDL_GetModState();
            set => Native.SDL_SetModState(value);
        }

        /// <summary>
        /// Whether the machine has screen keyboard support.
        /// </summary>
        public static bool HasScreenKeyboardSupport => Native.SDL_HasScreenKeyboardSupport();

        /// <summary>
        /// Whether the text input is active.
        /// </summary>
        public static bool TextInputActive => Native.SDL_IsTextInputActive();

        /// <summary>
        /// An event that is fired when a key is pressed.
        /// </summary>
        public static event EventHandler<KeyboardEventArgs>? KeyUp;

        /// <summary>
        /// An event that is fired when a key is released.
        /// </summary>
        public static event EventHandler<KeyboardEventArgs>? KeyDown;

        /// <summary>
        /// An event that is fired when text is edited.
        /// </summary>
        public static event EventHandler<TextEditedEventArgs>? TextEdited;

        /// <summary>
        /// An event that is fired when text is input.
        /// </summary>
        public static event EventHandler<TextInputEventArgs>? TextInput;

        /// <summary>
        /// An event that is fired when the keymap is changed.
        /// </summary>
        public static event EventHandler<SdlEventArgs>? KeymapChanged;

        /// <summary>
        /// Starts text input.
        /// </summary>
        public static void StartTextInput() =>
            Native.SDL_StartTextInput();

        /// <summary>
        /// Stops text input.
        /// </summary>
        public static void StopTextInput() =>
            Native.SDL_StopTextInput();

        /// <summary>
        /// Sets the rectangle for text input.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public static void SetTextInputRectangle(Rectangle rectangle) =>
            Native.SDL_SetTextInputRect(ref rectangle);

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch (e.Type)
            {
                case Native.SDL_EventType.KeyUp:
                    KeyUp?.Invoke(null, new KeyboardEventArgs(e.Key));
                    break;

                case Native.SDL_EventType.KeyDown:
                    KeyDown?.Invoke(null, new KeyboardEventArgs(e.Key));
                    break;

                case Native.SDL_EventType.TextEditing:
                    TextEdited?.Invoke(null, new TextEditedEventArgs(e.Edit));
                    break;

                case Native.SDL_EventType.TextInput:
                    TextInput?.Invoke(null, new TextInputEventArgs(e.Text));
                    break;

                case Native.SDL_EventType.KeymapChanged:
                    KeymapChanged?.Invoke(null, new SdlEventArgs(e.Common));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
