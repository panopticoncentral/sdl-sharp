namespace SdlSharp.Input
{
    /// <summary>
    /// Modifiers to keys on the keyboard.
    /// </summary>
    [Flags]
    public enum KeyModifier : ushort
    {
        /// <summary>
        /// None
        /// </summary>
        None = Native.SDL_Keymod.KMOD_NONE,

        /// <summary>
        /// Left shift is pressed.
        /// </summary>
        LeftShift = Native.SDL_Keymod.KMOD_LSHIFT,

        /// <summary>
        /// Right shift is pressed.
        /// </summary>
        RightShift = Native.SDL_Keymod.KMOD_RSHIFT,

        /// <summary>
        /// A shift key is pressed.
        /// </summary>
        Shift = Native.SDL_Keymod.KMOD_SHIFT,

        /// <summary>
        /// Left ctrl is pressed.
        /// </summary>
        LeftCtrl = Native.SDL_Keymod.KMOD_LCTRL,

        /// <summary>
        /// Right ctrl is pressed.
        /// </summary>
        RightCtrl = Native.SDL_Keymod.KMOD_RCTRL,

        /// <summary>
        /// A ctrl key is pressed.
        /// </summary>
        Ctrl = Native.SDL_Keymod.KMOD_CTRL,

        /// <summary>
        /// Left alt is pressed.
        /// </summary>
        LeftAlt = Native.SDL_Keymod.KMOD_LALT,

        /// <summary>
        /// Right alt is pressed.
        /// </summary>
        RightAlt = Native.SDL_Keymod.KMOD_RALT,

        /// <summary>
        /// An alt key is pressed.
        /// </summary>
        Alt = Native.SDL_Keymod.KMOD_ALT,

        /// <summary>
        /// Left GUI key is pressed.
        /// </summary>
        LeftGui = Native.SDL_Keymod.KMOD_LGUI,

        /// <summary>
        /// Right GUI key is pressed.
        /// </summary>
        RightGui = Native.SDL_Keymod.KMOD_RGUI,

        /// <summary>
        /// A GUI key is pressed.
        /// </summary>
        Gui = Native.SDL_Keymod.KMOD_GUI,

        /// <summary>
        /// NumLock key is active.
        /// </summary>
        Num = Native.SDL_Keymod.KMOD_NUM,

        /// <summary>
        /// CapsLock key is active.
        /// </summary>
        Caps = Native.SDL_Keymod.KMOD_CAPS,

        /// <summary>
        /// Mode key is active.
        /// </summary>
        Mode = Native.SDL_Keymod.KMOD_MODE,

        /// <summary>
        /// Scroll key is active.
        /// </summary>
        Scroll = Native.SDL_Keymod.KMOD_SCROLL
    }
}
