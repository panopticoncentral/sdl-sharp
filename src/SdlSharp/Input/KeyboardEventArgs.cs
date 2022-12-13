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

        internal KeyboardEventArgs(Native.SDL_KeyboardEvent keyboard) : base(keyboard.timestamp)
        {
            _windowId = keyboard.windowID;
            IsPressed = keyboard.state != 0;
            Repeat = keyboard.repeat;
            Scancode = keyboard.keysym.Scancode;
            Keycode = keyboard.keysym.Sym;
            Modifiers = keyboard.keysym.Mod;
        }
    }
}
