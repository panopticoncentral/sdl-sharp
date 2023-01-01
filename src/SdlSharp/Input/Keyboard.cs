using System.Collections;

using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// Keyboard related APIs.
    /// </summary>
    public static unsafe class Keyboard
    {
        private sealed class KeyboardStateWrapperEnumerator : IEnumerator<bool>
        {
            private readonly KeyboardStateWrapper _wrapper;
            private int _current;

            public bool Current =>
                (_current == -1 || _current == _wrapper.Count)
                    ? throw new InvalidOperationException()
                    : _wrapper[_current];

            object IEnumerator.Current => Current;

            public KeyboardStateWrapperEnumerator(KeyboardStateWrapper wrapper)
            {
                _wrapper = wrapper;
                _current = -1;
            }

            public void Dispose() { }

            public bool MoveNext()
            {
                if (_current < _wrapper.Count)
                {
                    _current++;
                }

                return _current != _wrapper.Count;
            }

            public void Reset() => _current = -1;
        }

        private sealed unsafe class KeyboardStateWrapper : IReadOnlyList<bool>
        {
            private readonly byte* _buffer;

            public bool this[int index] => _buffer[Count] != 0;

            public int Count { get; }

            public KeyboardStateWrapper(byte* buffer, int count)
            {
                _buffer = buffer;
                Count = count;
            }

            public IEnumerator<bool> GetEnumerator() => new KeyboardStateWrapperEnumerator(this);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        /// <summary>
        /// The window that has keyboard focus, if any.
        /// </summary>
        public static Window? Focus => new(Native.SDL_GetKeyboardFocus());

        /// <summary>
        /// The state of the keyboard.
        /// </summary>
        public static IReadOnlyList<bool> State
        {
            get
            {
                int numkeys;
                var state = Native.SDL_GetKeyboardState(&numkeys);
                return new KeyboardStateWrapper(state, numkeys);
            }
        }

        /// <summary>
        /// The state of the key modifiers.
        /// </summary>
        public static KeyModifier KeyModifierState
        {
            get => (KeyModifier)Native.SDL_GetModState();
            set => Native.SDL_SetModState((Native.SDL_Keymod)value);
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
        /// Whether an IME composite or candidate window is currently shown.
        /// </summary>
        public static bool TextInputShown => Native.SDL_IsTextInputShown();

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
        /// Resets the state of the keyboard.
        /// </summary>
        public static void Reset() => Native.SDL_ResetKeyboard();

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
        /// Dismiss the composition window/IME without disabling the subsystem.
        /// </summary>
        public static void ClearComposition() => Native.SDL_ClearComposition();

        /// <summary>
        /// Sets the rectangle for text input.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public static void SetTextInputRectangle(Rectangle rectangle) =>
            Native.SDL_SetTextInputRect(ref rectangle);

        internal static void DispatchEvent(Native.SDL_Event e)
        {
            switch ((Native.SDL_EventType)e.type)
            {
                case Native.SDL_EventType.SDL_KEYUP:
                    KeyUp?.Invoke(null, new KeyboardEventArgs(e.key));
                    break;

                case Native.SDL_EventType.SDL_KEYDOWN:
                    KeyDown?.Invoke(null, new KeyboardEventArgs(e.key));
                    break;

                case Native.SDL_EventType.SDL_TEXTEDITING:
                    TextEdited?.Invoke(null, new TextEditedEventArgs(e.edit));
                    break;

                case Native.SDL_EventType.SDL_TEXTINPUT:
                    TextInput?.Invoke(null, new TextInputEventArgs(e.text));
                    break;

                case Native.SDL_EventType.SDL_KEYMAPCHANGED:
                    KeymapChanged?.Invoke(null, new SdlEventArgs(e.common));
                    break;

                case Native.SDL_EventType.SDL_TEXTEDITING_EXT:
                    TextEdited?.Invoke(null, new TextEditedEventArgs(e.editExt));
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
