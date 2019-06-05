using SdlSharp.Graphics;

namespace SdlSharp.Input
{
    /// <summary>
    /// Event arguments for keyboard events.
    /// </summary>
    public sealed class KeyboardEventArgs : SdlEventArgs
    {
        private readonly uint _windowId;

        /// <summary>
        /// The window that had focus, if any.
        /// </summary>
        public Window Window => Window.Get(_windowId);

        /// <summary>
        /// Whether the key is pressed.
        /// </summary>
        public bool IsPressed { get; }

        /// <summary>
        /// The repeat count of the key.
        /// </summary>
        public byte Repeat { get; }

        /// <summary>
        /// The scan code of the key.
        /// </summary>
        public Scancode Scancode { get; }

        /// <summary>
        /// The key code of the key.
        /// </summary>
        public Keycode Keycode { get; }

        /// <summary>
        /// The key modifiers.
        /// </summary>
        public KeyModifier Modifiers { get; }

        internal KeyboardEventArgs(Native.SDL_KeyboardEvent keyboard) : base(keyboard.Timestamp)
        {
            _windowId = keyboard.WindowId;
            IsPressed = keyboard.State;
            Repeat = keyboard.Repeat;
            Scancode = keyboard.Keysym.Scancode;
            Keycode = keyboard.Keysym.Sym;
            Modifiers = keyboard.Keysym.Mod;
        }
    }
}
